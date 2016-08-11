using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using DevExpress.Web.ASPxFormLayout;
using DevExpress.Data.Filtering;
using NAS.BO.System.ArtifactCode;
using NAS.DAL.System.ArtifactCode;
using DevExpress.Web.ASPxEditors;

namespace WebModule.ERPSystem.ArtifactCode.GUI
{
    public partial class CodeRuleDefinitionEditingForm : System.Web.UI.UserControl
    {

        private Session session;
        private CodeRuleDataBO codeRuleDataBO;
        private CodeRuleDefinitionBO codeRuleDefinitionBO;
        private CodeRuleNumberDataBO codeRuleNumberDataBO;
        private CodeRuleStringDataBO codeRuleStringDataBO;

        /// <summary>
        /// ArtifactCodeRuleId of editing ArtifactCodeRule
        /// </summary>
        private Guid CodeRuleDefinitionId
        {
            get
            {
                return (Guid)Session["CodeRuleDefinitionId"];
            }
            set
            {
                Session["CodeRuleDefinitionId"] = value;
            }
        }

        /// <summary>
        /// ParentCodeRuleDefinitionId of creating ArtifactCodeRule
        /// </summary>
        private Guid ParentCodeRuleDefinitionId
        {
            get
            {
                return (Guid)Session["ParentCodeRuleDefinitionId"];
            }
            set
            {
                Session["ParentCodeRuleDefinitionId"] = value;
            }
        }

        private Guid CodeRuleDataId {
            get
            {
                return (Guid)Session["CodeRuleDataId"];
            }
            set
            {
                Session["CodeRuleDataId"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsCodeRuleDataFormat.Session = session;
            dsCodeRuleDataType.Session = session;
            dsRuleRepeaterType.Session = session;
            dsCodeRuleData.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            codeRuleDataBO = new CodeRuleDataBO();
            codeRuleDefinitionBO = new CodeRuleDefinitionBO();
            codeRuleNumberDataBO = new CodeRuleNumberDataBO();
            codeRuleStringDataBO = new CodeRuleStringDataBO();

            if (!IsPostBack)
            {
                GenerateViewStateControlId();
                GUIContext = new Context();
                CodeRuleDefinitionId = Guid.Empty;
                ParentCodeRuleDefinitionId = Guid.Empty;
                CodeRuleDataId = Guid.Empty;
            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }


        #region State Pattern

        private void GenerateViewStateControlId()
        {
            ViewStateControlId = Guid.NewGuid().ToString().Replace("-", "");
        }

        private string ViewStateControlId
        {
            get
            {
                return (string)ViewState["ViewStateControlId"];
            }
            set
            {
                ViewState["ViewStateControlId"] = value;
            }
        }

        private Context GUIContext
        {
            get { return (NAS.GUI.Pattern.Context)Session["GUIContext_" + ViewStateControlId]; }
            set { Session["GUIContext_" + ViewStateControlId] = value; }
        }


        #region UpdateGUI

        #region Use in internal
        private void UpdateLayoutItemVisibleForStringData()
        {
            BeginNumberLayoutItem.Visible = false;
            StepLayoutItem.Visible = false;
            EndNumberLayoutItem.Visible = false;
            RepeaterTypeLayoutItem.Visible = false;
        }
        private void UpdateLayoutItemVisibleForNumberData()
        {
            ValueDataLayoutItem.Visible = false;
        }
        private void UpdateLayoutItemVisibleForDateTimeData()
        {
            BeginNumberLayoutItem.Visible = false;
            StepLayoutItem.Visible = false;
            EndNumberLayoutItem.Visible = false;
            RepeaterTypeLayoutItem.Visible = false;
            ValueDataLayoutItem.Visible = false;
        }
        private void SetCodeRuleDataFormat()
        {
            Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
            CodeRuleData codeRuleData =
                codeRuleDataBO.GetCodeRuleData(session, CodeRuleDefinitionId, selectedCodeRuleDataTypeId);
            if (codeRuleData == null)
            {
                cbCodeRuleDataFormat.SelectedIndex = 0;
            }
        }
        #endregion

        public bool CodeRuleDefinifionCreatingDateTimeData_UpdateGUI()
        {
            UpdateLayoutItemVisibleForDateTimeData();
            return true;
        }
        public bool CodeRuleDefinifionCreatingNumberData_UpdateGUI()
        {
            UpdateLayoutItemVisibleForNumberData();
            return true;
        }
        public bool CodeRuleDefinifionCreatingStringData_UpdateGUI()
        {
            UpdateLayoutItemVisibleForStringData();
            return true;
        }
        public bool CodeRuleDefinifionEditingDateTimeData_UpdateGUI()
        {
            UpdateLayoutItemVisibleForDateTimeData();

            SetCodeRuleDataFormat();

            return true;
        }
        public bool CodeRuleDefinifionEditingNumberData_UpdateGUI()
        {
            UpdateLayoutItemVisibleForNumberData();

            SetCodeRuleDataFormat();

            return true;
        }
        public bool CodeRuleDefinifionEditingStringData_UpdateGUI()
        {
            UpdateLayoutItemVisibleForStringData();

            SetCodeRuleDataFormat();

            return true;
        }
        public bool CodeRuleDefinitionCanceling_UpdateGUI()
        {
            popupCodeRuleDefinitionEditingForm.ShowOnPageLoad = false;
            return true;
        }
        public bool CodeRuleDefinitionCreating_UpdateGUI()
        {
            ClearForm();
            popupCodeRuleDefinitionEditingForm.ShowOnPageLoad = true;
            return true;
        }
        public bool CodeRuleDefinitionEditing_UpdateGUI()
        {
            ClearForm();
            CodeRuleData codeRuleData =
                codeRuleDataBO.GetCodeRuleData(session, CodeRuleDefinitionId);
            cbCodeRuleDataType.Value =
                codeRuleData.CodeRuleDefinitionId.CodeRuleDataTypeId.CodeRuleDataTypeId.ToString();
            popupCodeRuleDefinitionEditingForm.ShowOnPageLoad = true;
            return true;
        }

        #endregion


        #region CRUD

        public bool CodeRuleDefinifionCreatingDateTimeData_CRUD()
        {
            SetCodeRuleDataFormatCriteria();
            return true;
        }
        public bool CodeRuleDefinifionCreatingNumberData_CRUD()
        {
            SetCodeRuleDataFormatCriteria();
            return true;
        }
        public bool CodeRuleDefinifionCreatingStringData_CRUD()
        {
            SetCodeRuleDataFormatCriteria();
            return true;
        }
        public bool CodeRuleDefinifionEditingDateTimeData_CRUD()
        {
            //Set data to input control
            //Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
            //CodeRuleData codeRuleData =
            //    (CodeRuleData)codeRuleDataBO.GetCodeRuleData(session, CodeRuleDefinitionId, selectedCodeRuleDataTypeId);
            //if (codeRuleData != null)
            //{
            //    cbCodeRuleDataType.Value =
            //        codeRuleData.CodeRuleDataFormatId.CodeRuleDataTypeId.CodeRuleDataTypeId.ToString();

            //    SetCodeRuleDataFormatCriteria();

            //    cbCodeRuleDataFormat.Value =
            //        codeRuleData.CodeRuleDataFormatId.CodeRuleDataFormatId.ToString();
            //}
            //else
            //{
            //    SetCodeRuleDataFormatCriteria();
            //}

            SetCodeRuleDataFormatCriteria();

            dsCodeRuleData.TypeName = typeof(CodeRuleData).FullName;
            CriteriaOperator criteria = CriteriaOperator.And(
                new BinaryOperator("CodeRuleDataId", CodeRuleDataId),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );
            dsCodeRuleData.Criteria = criteria.ToString();
            formlayoutCodeRuleDefinitionEditingForm.DataBind();

            return true;
        }
        public bool CodeRuleDefinifionEditingNumberData_CRUD()
        {
            //Set data to input control
            //Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
            //CodeRuleNumberData codeRuleNumberData =
            //    (CodeRuleNumberData)codeRuleDataBO.GetCodeRuleData(session, CodeRuleDefinitionId, selectedCodeRuleDataTypeId);
            //if (codeRuleNumberData != null)
            //{
            //    cbCodeRuleDataType.Value =
            //        codeRuleNumberData.CodeRuleDataFormatId.CodeRuleDataTypeId.CodeRuleDataTypeId.ToString();

            //    SetCodeRuleDataFormatCriteria();

            //    spinBeginNumberValue.Number = codeRuleNumberData.BeginNumberValue;
            //    spinStep.Number = codeRuleNumberData.Step;
            //    spinEndNumberValue.Number = codeRuleNumberData.EndNumberValue;

            //    cbCodeRuleDataFormat.Value =
            //        codeRuleNumberData.CodeRuleDataFormatId.CodeRuleDataFormatId.ToString();
            //    cbRuleRepeaterType.Value =
            //        codeRuleNumberData.RuleRepeaterTypeId.RuleRepeaterTypeId.ToString();
            //}
            //else
            //{
            //    SetCodeRuleDataFormatCriteria();
            //}

            SetCodeRuleDataFormatCriteria();

            dsCodeRuleData.TypeName = typeof(CodeRuleNumberData).FullName;
            CriteriaOperator criteria = CriteriaOperator.And(
                new BinaryOperator("CodeRuleDataId", CodeRuleDataId),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );
            dsCodeRuleData.Criteria = criteria.ToString();
            formlayoutCodeRuleDefinitionEditingForm.DataBind();

            return true;
        }
        public bool CodeRuleDefinifionEditingStringData_CRUD()
        {
            //Set data to input control
            //Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
            //CodeRuleStringData codeRuleStringData =
            //    (CodeRuleStringData)codeRuleDataBO.GetCodeRuleData(session, CodeRuleDefinitionId, selectedCodeRuleDataTypeId);
            //if (codeRuleStringData != null)
            //{
            //    cbCodeRuleDataType.Value =
            //        codeRuleStringData.CodeRuleDataFormatId.CodeRuleDataTypeId.CodeRuleDataTypeId.ToString();

            //    SetCodeRuleDataFormatCriteria();

            //    cbCodeRuleDataFormat.Value =
            //        codeRuleStringData.CodeRuleDataFormatId.CodeRuleDataFormatId.ToString();
            //    txtStringValue.Text = codeRuleStringData.StringValue;
            //}
            //else
            //{
            //    SetCodeRuleDataFormatCriteria();
            //}

            SetCodeRuleDataFormatCriteria();

            dsCodeRuleData.TypeName = typeof(CodeRuleStringData).FullName;
            CriteriaOperator criteria = CriteriaOperator.And(
                new BinaryOperator("CodeRuleDataId", CodeRuleDataId),
                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE)
            );
            dsCodeRuleData.Criteria = criteria.ToString();
            formlayoutCodeRuleDefinitionEditingForm.DataBind();

            return true;
        }
        public bool CodeRuleDefinitionCanceling_CRUD()
        {
            SetCodeRuleDataFormatCriteria();
            return true;
        }
        public bool CodeRuleDefinitionCreating_CRUD()
        {
            return true;
        }
        public bool CodeRuleDefinitionEditing_CRUD()
        {
            return true;
        }

        #endregion


        #region PreTransitionCRUD

        public bool CodeRuleDefinifionCreatingDateTimeData_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    {
                        try
                        {
                            ArtifactCodeRuleEditingForm artifactCodeRuleEditingForm = (ArtifactCodeRuleEditingForm)Parent;

                            //Get CodeRuleDataType
                            Guid codeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());

                            //Get CodeRuleDataFormat
                            Guid codeRuleDataFormatId = Guid.Parse(cbCodeRuleDataFormat.SelectedItem.Value.ToString());

                            CodeRuleDefinition codeRuleDefinition = codeRuleDefinitionBO.Insert(session,
                                artifactCodeRuleEditingForm.ArtifactCodeRuleId,
                                codeRuleDataTypeId,
                                ParentCodeRuleDefinitionId);

                            CodeRuleData codeRuleData = codeRuleDataBO.Insert(session,
                                codeRuleDefinition.CodeRuleDefinitionId,
                                codeRuleDataFormatId);

                            CodeRuleDefinitionId = codeRuleDefinition.CodeRuleDefinitionId;

                            CodeRuleDataId = codeRuleData.CodeRuleDataId;

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                default:
                    break;
            }
            return true;

        }
        public bool CodeRuleDefinifionCreatingNumberData_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    {
                        try
                        {
                            ArtifactCodeRuleEditingForm artifactCodeRuleEditingForm = (ArtifactCodeRuleEditingForm)Parent;

                            //Get CodeRuleDataType
                            Guid codeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());

                            //Get CodeRuleDataFormat
                            Guid codeRuleDataFormatId = Guid.Parse(cbCodeRuleDataFormat.SelectedItem.Value.ToString());

                            Guid ruleRepeaterTypeId = Guid.Parse(cbRuleRepeaterType.SelectedItem.Value.ToString());

                            CodeRuleDefinition codeRuleDefinition = codeRuleDefinitionBO.Insert(session,
                                artifactCodeRuleEditingForm.ArtifactCodeRuleId,
                                codeRuleDataTypeId,
                                ParentCodeRuleDefinitionId);

                            int beginNum = (int)spinBeginNumberValue.Number;
                            int step = (int)spinStep.Number;
                            int endNum = (int)spinEndNumberValue.Number;

                            CodeRuleData codeRuleData = codeRuleNumberDataBO.Insert(session,
                                codeRuleDefinition.CodeRuleDefinitionId,
                                codeRuleDataFormatId,
                                beginNum,
                                step,
                                endNum,
                                ruleRepeaterTypeId);

                            CodeRuleDefinitionId = codeRuleDefinition.CodeRuleDefinitionId;

                            CodeRuleDataId = codeRuleData.CodeRuleDataId;

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                default:
                    break;
            }
            return true;
        }
        public bool CodeRuleDefinifionCreatingStringData_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    {
                        try
                        {
                            ArtifactCodeRuleEditingForm artifactCodeRuleEditingForm = (ArtifactCodeRuleEditingForm)Parent;

                            //Get CodeRuleDataType
                            Guid codeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());

                            //Get CodeRuleDataFormat
                            Guid codeRuleDataFormatId = Guid.Parse(cbCodeRuleDataFormat.SelectedItem.Value.ToString());

                            CodeRuleDefinition codeRuleDefinition = codeRuleDefinitionBO.Insert(session,
                                artifactCodeRuleEditingForm.ArtifactCodeRuleId,
                                codeRuleDataTypeId,
                                ParentCodeRuleDefinitionId);

                            CodeRuleData codeRuleData = codeRuleStringDataBO.Insert(session,
                                codeRuleDefinition.CodeRuleDefinitionId,
                                codeRuleDataFormatId,
                                txtStringValue.Text);

                            CodeRuleDefinitionId = codeRuleDefinition.CodeRuleDefinitionId;

                            CodeRuleDataId = codeRuleData.CodeRuleDataId;

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                default:
                    break;
            }
            return true;

        }
        public bool CodeRuleDefinifionEditingDateTimeData_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    {
                        try
                        {
                            //Get CodeRuleDataType
                            Guid codeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());

                            //Get CodeRuleDataFormat
                            Guid codeRuleDataFormatId = Guid.Parse(cbCodeRuleDataFormat.SelectedItem.Value.ToString());

                            codeRuleDefinitionBO.Update(session, CodeRuleDefinitionId, codeRuleDataTypeId);

                            CodeRuleData codeRuleData = codeRuleDataBO.Update(session, CodeRuleDefinitionId, codeRuleDataFormatId);

                            CodeRuleDataId = codeRuleData.CodeRuleDataId;

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                default:
                    break;
            }
            return true;
        }
        public bool CodeRuleDefinifionEditingNumberData_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    {
                        try
                        {
                            //Get CodeRuleDataType
                            Guid codeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());

                            //Get CodeRuleDataFormat
                            Guid codeRuleDataFormatId = Guid.Parse(cbCodeRuleDataFormat.SelectedItem.Value.ToString());

                            Guid ruleRepeaterTypeId = Guid.Parse(cbRuleRepeaterType.SelectedItem.Value.ToString());

                            int beginNum = (int)spinBeginNumberValue.Number;
                            int step = (int)spinStep.Number;
                            int endNum = (int)spinEndNumberValue.Number;

                            codeRuleDefinitionBO.Update(session, CodeRuleDefinitionId, codeRuleDataTypeId);

                            CodeRuleData codeRuleData = codeRuleNumberDataBO.Update(session,
                                CodeRuleDefinitionId,
                                codeRuleDataFormatId,
                                beginNum,
                                step,
                                endNum,
                                ruleRepeaterTypeId);

                            CodeRuleDataId = codeRuleData.CodeRuleDataId;

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                default:
                    break;
            }
            return true;
        }
        public bool CodeRuleDefinifionEditingStringData_PreTransitionCRUD(string transition)
        {
            switch (transition)
            {
                case "Save":
                    {
                        try
                        {
                            ArtifactCodeRuleEditingForm artifactCodeRuleEditingForm = (ArtifactCodeRuleEditingForm)Parent;

                            //Get CodeRuleDataType
                            Guid codeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());

                            //Get CodeRuleDataFormat
                            Guid codeRuleDataFormatId = Guid.Parse(cbCodeRuleDataFormat.SelectedItem.Value.ToString());

                            CodeRuleDefinition codeRuleDefinition =
                                codeRuleDefinitionBO.Update(session, CodeRuleDefinitionId, codeRuleDataTypeId);

                            CodeRuleData codeRuleData = codeRuleStringDataBO.Update(session, CodeRuleDefinitionId, codeRuleDataFormatId, txtStringValue.Text);

                            CodeRuleDataId = codeRuleData.CodeRuleDataId;

                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                default:
                    break;
            }
            return true;
        }
        public bool CodeRuleDefinitionCanceling_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool CodeRuleDefinitionCreating_PreTransitionCRUD(string transition)
        {
            return true;
        }
        public bool CodeRuleDefinitionEditing_PreTransitionCRUD(string transition)
        {
            return true;
        }

        #endregion
        #endregion

        private void ClearForm()
        {
            ASPxEdit.ClearEditorsInContainer(formlayoutCodeRuleDefinitionEditingForm);
            //cbCodeRuleDataType.SelectedIndex = 0;
            txtStringValue.IsValid = true;
            cbCodeRuleDataType.IsValid = true;
            cbCodeRuleDataFormat.IsValid = true;
            spinStep.IsValid = true;
            spinBeginNumberValue.IsValid = true;
        }

        /// <summary>
        /// Set criteria for CodeRuleDataFormat datasource
        /// </summary>
        private void SetCodeRuleDataFormatCriteria()
        {
            Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
            CriteriaOperator criteria = new BinaryOperator("CodeRuleDataTypeId!Key", selectedCodeRuleDataTypeId);
            dsCodeRuleDataFormat.Criteria = criteria.ToString();
            cbCodeRuleDataFormat.DataBind();
            //cbCodeRuleDataFormat.SelectedIndex = 0;
        }

        private LayoutItemBase ValueDataLayoutItem
        {
            get
            {
                return formlayoutCodeRuleDefinitionEditingForm.FindItemOrGroupByName("ValueData");
            }
        }
        private LayoutItemBase BeginNumberLayoutItem
        {
            get
            {
                return formlayoutCodeRuleDefinitionEditingForm.FindItemOrGroupByName("BeginNumber");
            }
        }
        private LayoutItemBase StepLayoutItem
        {
            get
            {
                return formlayoutCodeRuleDefinitionEditingForm.FindItemOrGroupByName("Step");
            }
        }
        private LayoutItemBase EndNumberLayoutItem
        {
            get
            {
                return formlayoutCodeRuleDefinitionEditingForm.FindItemOrGroupByName("EndNumber");
            }
        }
        private LayoutItemBase RepeaterTypeLayoutItem
        {
            get
            {
                return formlayoutCodeRuleDefinitionEditingForm.FindItemOrGroupByName("RepeaterType");
            }
        }

        private string GetTransitionByDataTypeCode(string dataTypeCode)
        {
            string transition = null;
            switch (dataTypeCode)
            {
                case "STRING":
                    transition = "UseStringData";
                    break;
                case "NUMBER":
                    transition = "UseNumberData";
                    break;
                case "DATETIME":
                    transition = "UseDateTimeData";
                    break;
                default:
                    break;
            }
            return transition;
        }

        protected void cpnCodeRuleDefinitionEditingForm_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string transition = null;
            bool isSuccess = false;
            try
            {
                string[] args = e.Parameter.Split('|');
                transition = args[0];

                if (transition.Equals("Edit"))
                {
                    if (args.Length > 1)
                    {
                        CodeRuleDefinitionId = Guid.Parse(args[1]);
                        ParentCodeRuleDefinitionId = Guid.Empty;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    GUIContext.State =
                        new ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm.CodeRuleDefinitionEditing(this);
                    CodeRuleData codeRuleData =
                        codeRuleDataBO.GetCodeRuleData(session, CodeRuleDefinitionId);

                    CodeRuleDataId = codeRuleData.CodeRuleDataId;

                    transition =
                        GetTransitionByDataTypeCode(codeRuleData.CodeRuleDefinitionId.CodeRuleDataTypeId.Code);
                    //Request change state
                    GUIContext.Request(transition, this);

                }
                else if (transition.Equals("Create"))
                {
                    if (args.Length > 1)
                    {
                        CodeRuleDefinitionId = Guid.Empty;
                        Guid temp;
                        if (!Guid.TryParse(args[1], out temp))
                        {
                            temp = Guid.Empty;
                        }
                        ParentCodeRuleDefinitionId = temp;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    GUIContext.State =
                        new ERPSystem.ArtifactCode.State.CodeRuleDefinitionForm.CodeRuleDefinitionCreating(this);

                    cbCodeRuleDataType.SelectedIndex = 0;
                    Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
                    CodeRuleDataType codeRuleDataType =
                        session.GetObjectByKey<CodeRuleDataType>(selectedCodeRuleDataTypeId);
                    transition =
                        GetTransitionByDataTypeCode(codeRuleDataType.Code);
                    //Request change state
                    GUIContext.Request(transition, this);

                }
                else if (transition.Equals("ChangeDataType"))
                {
                    Guid selectedCodeRuleDataTypeId = Guid.Parse(cbCodeRuleDataType.SelectedItem.Value.ToString());
                    CodeRuleDataType codeRuleDataType =
                        session.GetObjectByKey<CodeRuleDataType>(selectedCodeRuleDataTypeId);
                    transition =
                        GetTransitionByDataTypeCode(codeRuleDataType.Code);
                    //Request change state
                    GUIContext.Request(transition, this);
                }
                else
                {
                    //Request change state
                    GUIContext.Request(transition, this);
                }

                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (transition != null)
                {
                    cpnCodeRuleDefinitionEditingForm.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"transition\": \"{0}\", \"success\": {1} }}",
                                                            transition, isSuccess.ToString().ToLower()));
                }
            }
        }
    }
}