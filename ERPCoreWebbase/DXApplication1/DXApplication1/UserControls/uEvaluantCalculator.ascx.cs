using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using Evaluant.Calculator;
using NAS.BO.Sale.PricePolicy;
using System.Collections;
using NAS.DAL.Accounting.Tax;
using NAS.DAL.Invoice;

namespace WebModule.UserControls
{
    public class PriceCaculator: Expression
    {
        public string ExpressionStr{
            get {
                return this.expression;
            }

            set {
                this.expression = value;
            }
        }

        public List<TaxTypeSelection> TaxTypeSelections
        {
            get;
            set;
        }

        public PriceCaculator(string expression)
            : base(expression)
        {
            TaxTypeSelections = new List<TaxTypeSelection>();
        }

        public bool checkIsValid(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                if (this.ExpressionStr.IndexOf("?") >= 0)
                {
                    errorMessage = "Công thức giá không hợp lệ";
                    throw new EvaluationException(errorMessage);
                }

                if (this.ExpressionStr.Equals(string.Empty))
                {
                    errorMessage = "Lỗi chưa nhập công thức giá";
                    throw new EvaluationException(errorMessage);
                }

                if (this.ExpressionStr.IndexOf("..") >= 0)
                {
                    errorMessage = "Công thức giá có lỗi ở dấu .";
                    throw new EvaluationException(errorMessage);
                }

                if (this.ExpressionStr.IndexOf(".") == 0 || this.ExpressionStr.IndexOf(".") == ExpressionStr.Length - 1)
                {
                    errorMessage = "Công thức giá có lỗi ở dấu .";
                    throw new EvaluationException(errorMessage);
                }

                string Numberic = "0123456789";

                for (int pos = 1; pos < this.ExpressionStr.Length; pos++)
                {
                    if ((ExpressionStr[pos].Equals('.') && Numberic.IndexOf(ExpressionStr[pos - 1]) < 0) ||
                        (ExpressionStr[pos].Equals('.') && Numberic.IndexOf(ExpressionStr[pos + 1]) < 0)
                        )
                    {
                        errorMessage = "Công thức giá có lỗi ở dấu .";
                        throw new EvaluationException(errorMessage);
                    }
                }

                this.Evaluate();
            }
            catch (EvaluationException)
            {
                if (errorMessage.Equals(string.Empty))
                    errorMessage = "Công thức giá không hợp lệ";
                return false;
            }
            catch (ArgumentException ax)
            {
                PricePolicyBO bo = new PricePolicyBO();
                List<TaxType> list = bo.GetTaxesForPricePolicyFormulaSetting();
                if (list == null || list.Count == 0)
                    throw new Exception("Chưa cấu hình loại thuế cho hệ thống");

                TaxType type = list.Find(r => r.Code.Equals(ax.ParamName));

                if (type == null)
                {
                    errorMessage = "Công thức giá có lỗi ở tham số [" + ax.ParamName + "]. Chỉ được truyền các tham số đã quy định sẵn";
                    return false;    
                }

                //if (!ax.ParamName.Equals("TAXTYPE_VAT_PRODUCT") &&
                //        !ax.ParamName.Equals("TAXTYPE_SPECIAL_PRODUCT") &&
                //        !ax.ParamName.Equals("TAXTYPE_RESOURCE_PRODUCT") &&
                //        !ax.ParamName.Equals("TAXTYPE_VAT_SERVICE") &&
                //        !ax.ParamName.Equals("COGS")
                //        )
                //{
                //    errorMessage = "Công thức giá có lỗi ở tham số [" + ax.ParamName +"]. Chỉ được truyền các tham số đã quy định sẵn";
                //    return false;    
                //}

                errorMessage = "Vui lòng nhập giá trị cho tham số " + ax.ParamName;
                return false;
            }

            return true;
        }
    }

    public partial class uEvaluantCalculator : System.Web.UI.UserControl
    {
        Session session;

        public PriceCaculator PriceCaculator
        {
            get{
                return Session["PriceCaculator" + this.ClientID  + this.Session.SessionID] as PriceCaculator;
            }

            set{
                Session["PriceCaculator" + this.ClientID + this.Session.SessionID] = value;
            }
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            TaxTypeXDS.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PriceCaculator = new PriceCaculator("1+2+3");
            }
        }

        public void getFormToPriceCaculator()
        {
            PriceCaculator = new PriceCaculator(txtFormulaExpress.Text.Trim());

            for (int idx = 0; idx < grdVariableOfTax.VisibleRowCount; idx++)
            {
                /*2013-11-24 ERP-1125 Khoa.Truong MOD START*/
                //string TaxTypeName = grdVariableOfTax.GetRowValues(idx, "TaxTypeName").ToString();
                string Code = (string)grdVariableOfTax.GetRowValues(idx, "Code");
                //ASPxButton btn = grdVariableOfTax.FindRowCellTemplateControlByKey(TaxTypeName, 
                //    grdVariableOfTax.Columns["TaxTypeName"] as GridViewDataColumn, "btnSelectVariable") as ASPxButton;
                ASPxButton btn = grdVariableOfTax.FindRowCellTemplateControlByKey(Code,
                    grdVariableOfTax.Columns["Code"] as GridViewDataColumn, "btnSelectVariable") as ASPxButton;
                /*2013-11-24 ERP-1125 Khoa.Truong MOD END*/
                //string placeHolder = "[" + Convert.ToChar(65 + idx) + "]";
                if (txtFormulaExpress.Text.Trim().IndexOf(Code) > -1)
                {
                    //PriceCaculator.ExpressionStr = PriceCaculator.ExpressionStr.Replace(placeHolder, "[" + TaxTypeName + "]");
                    TaxTypeSelection t = new TaxTypeSelection();
                    t.TaxTypeId = Guid.Parse(grdVariableOfTax.GetRowValues(idx, "TaxTypeId").ToString());
                    t.ByValue = 0;

                    /*2013-11-24 ERP-1125 Khoa.Truong MOD START
                        Tam thoi hard code t.ByPercentTage
                     */
                    //t.ByPercentTage = double.Parse(grdVariableOfTax.GetRowValuesByKeyValue(TaxTypeName, "Percentage").ToString());
                    t.ByPercentTage = (double)0;                    
                    /*2013-11-24 ERP-1125 Khoa.Truong MOD END*/
                    PriceCaculator.Parameters[Code] = t.ByPercentTage;
                    PriceCaculator.TaxTypeSelections.Add(t);
                }
            }

            if (txtFormulaExpress.Text.Trim().IndexOf("[COGS]") > -1)
            {
                PriceCaculator.Parameters["COGS"] = 1;
            }
        }

        //public string getCharacterInButtonByTaxCode(string TypeCode)
        //{
        //    if (TypeCode.Equals("TAXTYPE_VAT_PRODUCT"))
        //        return "A";
        //    if (TypeCode.Equals("TAXTYPE_SPECIAL_PRODUCT"))
        //        return "B";
        //    if (TypeCode.Equals("TAXTYPE_RESOURCE_PRODUCT"))
        //        return "C";
        //    if (TypeCode.Equals("TAXTYPE_VAT_SERVICE"))
        //        return "D";
        //    return string.Empty;
        //}

        protected void grdVariableOfTax_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableDataCellEventArgs e)
        {
            /*2013-11-24 ERP-1125 Khoa.Truong MOD START*/
            //if (e.DataColumn.FieldName.Equals("TaxTypeName"))
            //{
            //    ASPxButton btn = grdVariableOfTax.FindRowCellTemplateControlByKey(e.KeyValue,
            //        grdVariableOfTax.Columns["TaxTypeName"] as GridViewDataColumn, "btnSelectVariable") as ASPxButton;
            //    btn.Text = string.Format("[{0}]", Convert.ToChar(65 + e.VisibleIndex).ToString());
            //    btn.ClientSideEvents.Click = "function(s, e){uEvaluantCalculator_InsertText(" + 
            //                        txtFormulaExpress.ClientInstanceName +  
            //                        ", '" + 
            //                        btn.Text +"')}";
            //}

            if (e.DataColumn.FieldName.Equals("Code"))
            {
                ASPxButton btn = grdVariableOfTax.FindRowCellTemplateControlByKey(e.KeyValue,
                    grdVariableOfTax.Columns["Code"] as GridViewDataColumn, "btnSelectVariable") as ASPxButton;
                //btn.Text = string.Format("[{0}]", Convert.ToChar(65 + e.VisibleIndex).ToString());
                btn.Text = string.Format("[{0}]", e.CellValue.ToString());//string.Format("[{0}]", Convert.ToChar(65 + e.VisibleIndex).ToString());
                btn.ClientSideEvents.Click = "function(s, e){uEvaluantCalculator_InsertText(" +
                                    txtFormulaExpress.ClientInstanceName +
                                    ", '" +
                                    btn.Text + "')}";
            }
            /*2013-11-24 ERP-1125 Khoa.Truong MOD END*/

            //if (e.DataColumn.Name.Equals("Desciption"))
            //{
            //    e.Cell.Text = getDesciption(e.KeyValue.ToString());
            //}
        }

        protected void txtFormulaExpress_Validation(object sender, ValidationEventArgs e)
        {
            //this.settingPriceCaculator();
            //if (!this.PriceCaculator.checkIsValid() && !ASPxEdit.AreEditorsValid(FormCaculator, "groupConfigFormula"))
            //{
            //    e.IsValid = false;
            //    e.ErrorText = "Công thức giá không hợp lệ";
            //}
            //else
            //    e.IsValid = true;
        }

        public void resetForm()
        {
            txtFormulaExpress.Text = string.Empty;
        }

        public void settingInit(string expression, List<TaxTypeSelection> list) {
            this.PriceCaculator = new PriceCaculator(expression);
            this.PriceCaculator.ExpressionStr = expression;
            this.PriceCaculator.TaxTypeSelections = list;
            resetForm();

            //expression = expression.Replace("TAXTYPE_VAT_PRODUCT", getCharacterInButtonByTaxCode("TAXTYPE_VAT_PRODUCT"));
            //expression = expression.Replace("TAXTYPE_SPECIAL_PRODUCT", getCharacterInButtonByTaxCode("TAXTYPE_SPECIAL_PRODUCT"));
            //expression = expression.Replace("TAXTYPE_RESOURCE_PRODUCT", getCharacterInButtonByTaxCode("TAXTYPE_RESOURCE_PRODUCT"));
            //expression = expression.Replace("TAXTYPE_VAT_SERVICE", getCharacterInButtonByTaxCode("TAXTYPE_VAT_SERVICE"));
            txtFormulaExpress.Text = expression;

            //for (int idx = 0; idx < grdVariableOfTax.VisibleRowCount; idx++)
            //{
            //    Guid TaxTypeId = Guid.Parse(grdVariableOfTax.GetRowValues(idx, "TaxTypeId").ToString());
            //    /*2013-11-24 ERP-1125 Khoa.Truong MOD START*/
            //    //string TaxTypeName = grdVariableOfTax.GetRowValues(idx, "TaxTypeName").ToString();
            //    string TaxTypeName = (string)grdVariableOfTax.GetRowValues(idx, "Code");
            //    /*2013-11-24 ERP-1125 Khoa.Truong MOD END*/

            //    TaxTypeSelection ptmp = this.PriceCaculator.TaxTypeSelections.Find(p => p.TaxTypeId == TaxTypeId);
            //}
        }
    }
}