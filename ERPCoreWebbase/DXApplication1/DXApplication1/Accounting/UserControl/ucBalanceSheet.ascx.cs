using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NAS.DAL;
using DevExpress.Xpo;
using NAS.BO;
using NAS.DAL.Accounting.AccountChart;
using DevExpress.Web.ASPxEditors;
using NAS.BO.Accounting;
using NAS.BO.Accounting.Journal;

namespace WebModule.Accounting.UserControl
{
    public partial class ucBalanceSheet : System.Web.UI.UserControl
    {
        Session session;
        
        double totalAsset = 0;
        double totalLiability = 0;
        double totalEquity = 0;
        public string ParseStringNumber(String numberstring)
        {            

            string result = numberstring;
            int i = result.Length-3;            
            while (i > 0)
            {
                if (i == 1 && result[0] == '-')
                {
                    return result;
                }
                result = result.Insert(i, ".");
                i -= 3;                
            }
            return result;
        }

        protected void Page_Init(object sender, EventArgs e)               
        {
            session = XpoHelper.GetNewSession();
            AssetAccountXPO.Session = session;
            LiabilityAccountXPO.Session = session;
            EquityAccountXPO.Session = session;            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingBO accountBO = new AccountingBO();
            //accountBO.InitBalanceForward(session, AccountingPeriodBO.getCurrentAccountingPeriod(session));
            var datasource1 = accountBO.getLedgerListByAccountCategory(session, "ASSET");
            var datasource2 = accountBO.getLedgerListByAccountCategory(session, "LIABILITY");
            var datasource3 = accountBO.getLedgerListByAccountCategory(session, "EQUITY");

            ASPxTreeList1.DataSource = datasource1;
            ASPxTreeList2.DataSource = datasource2;
            ASPxTreeList3.DataSource = datasource3;

            ASPxTreeList1.DataBind();
            ASPxTreeList2.DataBind();
            ASPxTreeList3.DataBind();
            XPQuery<AccountCategory> AccCateQuery = session.Query<AccountCategory>();
            General gen = new General();

            AccountCategory AccCate = AccCateQuery.Where(r => r.Code == "ASSET").FirstOrDefault();
            totalAsset = gen.TotalBalance(session, AccCate);

            AccCate = AccCateQuery.Where(r => r.Code == "LIABILITY").FirstOrDefault();
            totalLiability = gen.TotalBalance(session, AccCate);

            AccCate = AccCateQuery.Where(r => r.Code == "EQUITY").FirstOrDefault();
            totalEquity = gen.TotalBalance(session, AccCate);
            ASPxTextBox tb1 = ASPxTreeList1.FindFooterTemplateControl(ASPxTreeList1.Columns[2],"AssetTotal") as ASPxTextBox;
            tb1.Value = ParseStringNumber(totalAsset.ToString());
            tb1.Text = ParseStringNumber(totalAsset.ToString());
            ASPxTextBox tb2 = ASPxTreeList2.FindFooterTemplateControl(ASPxTreeList2.Columns[2], "LiabilityTotal") as ASPxTextBox;
            tb2.Value = ParseStringNumber(totalLiability.ToString());
            tb2.Text = ParseStringNumber(totalLiability.ToString());
            ASPxTextBox tb3 = ASPxTreeList3.FindFooterTemplateControl(ASPxTreeList3.Columns[2], "EquityTotal") as ASPxTextBox;
            tb3.Value = ParseStringNumber(totalEquity.ToString());
            tb3.Text = ParseStringNumber(totalEquity.ToString());
            ASPxTextBox tb4 = ASPxTreeList3.FindFooterTemplateControl(ASPxTreeList3.Columns[2], "DebitTotal") as ASPxTextBox;
            tb4.Value = ParseStringNumber((totalLiability + totalEquity).ToString());
            tb4.Text = ParseStringNumber((totalLiability + totalEquity).ToString());
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            e.Result = ParseStringNumber(totalAsset.ToString()) + "||"
                     + ParseStringNumber(totalLiability.ToString()) + "||"
                     + ParseStringNumber(totalEquity.ToString()) + "||"
                     + ParseStringNumber((totalLiability + totalEquity).ToString());
        }
    }
}