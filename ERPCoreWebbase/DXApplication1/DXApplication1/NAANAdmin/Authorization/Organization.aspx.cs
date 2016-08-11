using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using WebModule.NAANAdmin.Authorization.UserControl;

namespace WebModule.NAANAdmin.Authorization
{
    public partial class Organization : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_GROUPID;
            }
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_AUTHORIZATION_ORGANIZATIONID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
                tList_Organization.ExpandAll();
                
        }

        public bool OrganizationCreatingUpdateGUI()
        {
            try
            {
                //popupOrgan.ShowOnPageLoad = true;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dS_Organization.Session = session;
            OrganizationType.Session = session;

            OrganizationType subOrgOrganizationType =
                Util.getXPCollection<OrganizationType>(session, "Name", OrganizationTypeConstant.NAAN_CUSTOMER_SUB_ORGANIZATION.Value).FirstOrDefault();
            dS_Organization.CriteriaParameters["OrganizationTypeId"].DefaultValue = subOrgOrganizationType.OrganizationTypeId.ToString();
            tList_Organization.RootValue = Utility.CurrentSession.Instance.AccessingOrganizationId;
        }

        protected void tList_Organization_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["RowCreationTimeStamp"] = DateTime.Now;
            e.NewValues["RowStatus"] = Constant.ROWSTATUS_ACTIVE;
            OrganizationType subOrgOrganizationType =
                Util.getXPCollection<OrganizationType>(session, "Name", OrganizationTypeConstant.NAAN_CUSTOMER_SUB_ORGANIZATION.Value).FirstOrDefault();
            e.NewValues["OrganizationTypeId"] = subOrgOrganizationType;
            string parentKey = tList_Organization.NewNodeParentKey;
            if (parentKey.Equals(String.Empty))
            {
                e.NewValues["ParentOrganizationId!Key"] = tList_Organization.RootValue;
            }


        }

        protected void tList_Organization_NodeValidating(object sender, DevExpress.Web.ASPxTreeList.TreeListNodeValidationEventArgs e)
        {
            ASPxTreeList tree = (ASPxTreeList)sender;
            string organizationCode = e.NewValues["Code"].ToString();
            //New mode
            if (tree.IsNewNodeEditing)
            {
                bool isExist = Util.isExistXpoObject<NAS.DAL.Nomenclature.Organization.Organization>("Code", organizationCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    CriteriaOperator criteria = new BinaryOperator("Code", organizationCode, BinaryOperatorType.Equal);
                    NAS.DAL.Nomenclature.Organization.Organization organ = session.FindObject<NAS.DAL.Nomenclature.Organization.Organization>(criteria);
                    short rowstatus = organ.RowStatus;
                    if (rowstatus > 0)
                    {
                        Utility.Helpers.AddErrorToTreeListNode(e.Errors, "Code", "Mã tổ chức đã tồn tại");
                    }
                }
            }

            //Edit mode            
            else
            {
                if (tree.IsEditing)
                {
                    if (e.NewValues["Code"].ToString().Trim() != e.OldValues["Code"].ToString().Trim())
                    {
                        bool isExist = Util.isExistXpoObject<NAS.DAL.Nomenclature.Organization.Organization>("Code", organizationCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                        if (isExist)
                        {
                            CriteriaOperator criteria = new BinaryOperator("Code", organizationCode, BinaryOperatorType.Equal);
                            NAS.DAL.Nomenclature.Organization.Organization organ = session.FindObject<NAS.DAL.Nomenclature.Organization.Organization>(criteria);
                            short rowstatus = organ.RowStatus;
                            if (rowstatus > 0)
                            {
                                Utility.Helpers.AddErrorToTreeListNode(e.Errors, "Code", "Mã tổ chức đã tồn tại");
                            }
                        }
                    }
                }
            }
        }

        protected void tList_Organization_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            e.Cancel = true;
            OrganizationBO checkID = new OrganizationBO();
            Guid organizationId = (Guid)e.Keys[0];
            //bool checkParent = checkID.checkExistParentInOrgan(session, organizationId);
            bool checkDepart = checkID.checkExistDepartmentInOrgan(session, organizationId);
            bool checkBillActor = checkID.checkExistBillActorInOrgan(session, organizationId);
            bool checkInventory = checkID.checkExistInventoryInOrgan(session, organizationId);
            bool checkInventoryUnit = checkID.checkExistInventoryUnitInOrgan(session, organizationId);
            bool checkStockCartActor = checkID.checkExistStockCartActorInOrgan(session, organizationId);
            bool checkVouchesActor = checkID.checkExistVouchesActorInOrgan(session, organizationId);
            bool checkBillSource = checkID.checkExistBillSourceOrgainzationInOrgan(session, organizationId);
            bool checkVouchesSource = checkID.checkExistVouchesSourceOrgainzationInOrgan(session, organizationId);
            bool checkVouchesTarget = checkID.checkExistVouchesTargetOrgainzationInOrgan(session, organizationId);
            bool checkAccount = checkID.checkExistAccountInOrgan(session, organizationId);
            bool checkAccounting = checkID.checkExistAccountingInOrgan(session, organizationId);
            if (checkDepart)
            {
                throw new Exception("Không được xóa tổ chức này!");
            }
            else
            {
                if (checkBillActor || checkInventory || checkInventoryUnit ||
                checkStockCartActor || checkVouchesActor || checkBillSource || checkVouchesSource ||
                checkVouchesTarget || checkAccount || checkAccounting)
                {
                    throw new Exception("Không được xóa tổ chức này!");
                }
                else
                {
                    //e.Values["RowStatus"] = Utility.Constant.ROWSTATUS_DELETED;
                    tList_Organization.JSProperties.Add("cpQuestion", organizationId.ToString());
                }
            }
        }

        protected void tList_Organization_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                e.Editor.Focus();
            }
        }

        protected void tList_Organization_CustomCallback(object sender, TreeListCustomCallbackEventArgs e)
        {
            string[] para = e.Argument.Split('|');
            if (para[0] == "Delete")
            {
                Guid organId = Guid.Parse(para[1]);
                NAS.DAL.Nomenclature.Organization.Organization organ = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.Organization>(organId);
                organ.RowStatus = Constant.ROWSTATUS_DELETED;
                organ.Save();
                tList_Organization.JSProperties.Add("cpRefresh", "Refresh");
            }
            tList_Organization.DataBind();
        }
    }
}
