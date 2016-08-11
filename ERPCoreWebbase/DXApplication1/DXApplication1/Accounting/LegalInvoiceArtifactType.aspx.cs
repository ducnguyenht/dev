using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using Utility;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting
{
    public partial class LegalInvoiceArtifactType : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session = XpoHelper.GetNewUnitOfWork();
        NAS.BO.Accounting.LegalInvoiceArtifact.LegalInvoiceArtifactBO BO = new NAS.BO.Accounting.LegalInvoiceArtifact.LegalInvoiceArtifactBO();

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            DBLegalInvoiceArtifactType.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool check
        {
            get { return (bool)Session["check"]; }
            set { Session["check"] = value; }
        }

        protected void Grid_LegalInvoiceArtifactType_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();
                string description = "";
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType artifactType = new NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType(session);
                if (BO.check_LegalInvoiceArtifacType(session, e.NewValues["Code"].ToString(), Utility.Constant.ROWSTATUS_ACTIVE.ToString()))
                    throw new Exception("Mã số phân loại trùng");
                if (e.NewValues["Description"] != null)
                    description = e.NewValues["Description"].ToString();
                artifactType.Code = e.NewValues["Code"].ToString();
                artifactType.Name = e.NewValues["Name"].ToString();
                artifactType.Category = char.Parse(e.NewValues["Category"].ToString());
                artifactType.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                artifactType.Description = description;
                session.CommitTransaction();
                session.FlushChanges();
                Grid_LegalInvoiceArtifactType.CancelEdit();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_LegalInvoiceArtifactType_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();
                string description = "";

                if (!e.OldValues["Code"].Equals(e.NewValues["Code"]))
                {
                    if (BO.check_LegalInvoiceArtifacType(session, e.NewValues["Code"].ToString(), Utility.Constant.ROWSTATUS_ACTIVE.ToString()))
                        throw new Exception("Mã số phân loại trùng");
                }
                if (e.NewValues["Description"] != null)
                    description = e.NewValues["Description"].ToString();

                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType artifactType = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(Guid.Parse(e.Keys[0].ToString()));
                artifactType.Code = e.NewValues["Code"].ToString();
                artifactType.Name = e.NewValues["Name"].ToString();
                artifactType.Category = char.Parse(e.NewValues["Category"].ToString());
                artifactType.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                artifactType.Description = description;
                session.CommitTransaction();
                session.FlushChanges();
                Grid_LegalInvoiceArtifactType.CancelEdit();

            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_LegalInvoiceArtifactType_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType artifactType = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(Guid.Parse(e.Keys[0].ToString()));
                if (artifactType == null)
                    throw new Exception("LegalInvoiceArtifactType is not exist system");
                SaleInvoiceArtiface saleId = session.FindObject<SaleInvoiceArtiface>(
                    CriteriaOperator.And(
                        new BinaryOperator("LegalInvoiceArtifactTypeId", artifactType.LegalInvoiceArtifactTypeId, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                ));
                if (saleId == null)
                {
                    artifactType.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                }
                else
                {
                    throw new Exception("Đã có thông tin nên không được xóa!");
                }
                session.CommitTransaction();
                session.FlushChanges();
                Grid_LegalInvoiceArtifactType.CancelEdit();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_LegalInvoiceArtifactType_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
                throw;
            }
        }



        protected void Grid_LegalInvoiceArtifactType_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            {
                string[] m = { "Code", "Name", "Category" };
                for (int i = 0; i < m.Length; i++)
                {
                    if (e.Column.FieldName.Equals(m[i]) && Grid_LegalInvoiceArtifactType.IsEditing && !Grid_LegalInvoiceArtifactType.IsNewRowEditing)
                    {
                        SaleInvoiceArtiface saleId = session.FindObject<SaleInvoiceArtiface>(
                            CriteriaOperator.And(
                                  new BinaryOperator("LegalInvoiceArtifactTypeId", e.KeyValue.ToString(), BinaryOperatorType.Equal),
                                  new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                          ));
                        if (saleId!=null)
                            e.Editor.ReadOnly = true;
                    }
                }
            }
        }
    }
}