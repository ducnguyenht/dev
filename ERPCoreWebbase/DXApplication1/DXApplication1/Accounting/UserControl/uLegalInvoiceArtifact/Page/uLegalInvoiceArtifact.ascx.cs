using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.GUI.Pattern;
using WebModule.Accounting.UserControl.uLegalInvoiceArtifact.State;
using NAS.DAL.Accounting.LegalInvoice;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using NAS.BO.Accounting.LegalInvoiceArtifact;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Nomenclature.Item;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace WebModule.Accounting.UserControl.uLegalInvoiceArtifact
{
    public partial class uLegalInvoiceArtifact : System.Web.UI.UserControl
    {
        Session session = XpoHelper.GetNewUnitOfWork();
        LegalInvoiceArtifactBO BO = new LegalInvoiceArtifactBO();

        protected void Page_Init(object sender, EventArgs e)
        {
            DBLegalInvoiceArtifactDetail.Session = session;
            DBItem.Session = session;
            DBOrganization.Session = session;
            DBLegalInvoiceArtifactType.Session = session;
            DBUnit.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (GUIContext == null)
                GUIContext = new Context();

            //create popuplate LegalInvoiceArtifactIdentifierType
            NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactIdentifierType.Populate();
        }

        #region tạo biến session
        public Guid LegalInvoiceArtifactId
        {
            get { return (Guid)Session["LegalInvoiceArtifactId"]; }
            set
            {
                Session["LegalInvoiceArtifactId"] = value;
            }
        }

        private string using_state
        {
            get { return (string)Session["using_state"]; }
            set { Session["using_state"] = value; }
        }

        private Context GUIContext
        {
            get { return (Context)Session["LegalInvoiceArtifact_GUIContext"]; }
            set { Session["LegalInvoiceArtifact_GUIContext"] = value; }
        }

        private string unitID
        {
            get { return (string)Session["unitID"]; }
            set { Session["unitID"] = value; }
        }
        private string itemId
        {
            get { return (string)Session["itemId"]; }
            set { Session["itemId"] = value; }
        }

        #endregion

        private void Clear_AllFrom()
        {
            cbo_Code_ArtifactType.Text = "";
            txt_Code_Artifact.Text = "";
            txt_IssuedDate_Artifact.Text = "";
            txt_Template_Identifier.Text = "";
            txt_Series_Identifier.Text = "";
            txt_Number_Identifier.Text = "";
            cbo_Code_sell_Organization.Text = "";
            txt_AccountNumber_sell_OrgActor.Text = "";
            txt_PhoneFax_sell_OrgActor.Text = "";
            txt_TaxCode_sell_OrgActor.Text = "";
            memo_Address_sell_OrgActor.Text = "";
            cbo_Code_buy_Organization.Text = "";
            memo_Address_buy_OrgActor.Text = "";
            txt_Name_buy_OrgActor.Text = "";
            txt_Description_buy_OrgActor.Text = "";
            txt_AccountNumber_buy_OrgActor.Text = "";
            txt_TaxCode_buy_OrgActor.Text = "";
            txt_ItemTaxInPercentage_ArtifactDetail.Text = "";
            txt_thuesuatGTGT.Text = "";
            txt_total_ArtifactDetail.Text = "";
            txt_tienbangchu.Text = "";
            txt_TelephoneFax_buy_OrgActor.Text = "";
        }

        private void clock_textBox(bool name)
        {
            cbo_Code_ArtifactType.ReadOnly = name;
            txt_Code_Artifact.ReadOnly = name;
            txt_IssuedDate_Artifact.ReadOnly = name;
            txt_Template_Identifier.ReadOnly = name;
            txt_Series_Identifier.ReadOnly = name;
            txt_Number_Identifier.ReadOnly = name;
            cbo_Code_sell_Organization.ReadOnly = name;
            txt_AccountNumber_sell_OrgActor.ReadOnly = name;
            txt_PhoneFax_sell_OrgActor.ReadOnly = name;
            txt_TaxCode_sell_OrgActor.ReadOnly = name;
            memo_Address_sell_OrgActor.ReadOnly = name;
            cbo_Code_buy_Organization.ReadOnly = name;
            memo_Address_buy_OrgActor.ReadOnly = name;
            txt_Name_buy_OrgActor.ReadOnly = name;
            txt_Description_buy_OrgActor.ReadOnly = name;
            txt_AccountNumber_buy_OrgActor.ReadOnly = name;
            txt_TaxCode_buy_OrgActor.ReadOnly = name;
            txt_ItemTaxInPercentage_ArtifactDetail.ReadOnly = name;
            txt_thuesuatGTGT.ReadOnly = name;
            txt_total_ArtifactDetail.ReadOnly = name;
            txt_tienbangchu.ReadOnly = name;
            txt_TelephoneFax_buy_OrgActor.ReadOnly = name;
        }

        #region using State CRUD
        public void CRUD_LegalInvoiceArtifact_Creating()
        {
            try
            {
                Clear_AllFrom();
                #region Create new LegalInvoiceArtifact
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact legalInvoiceArtifact_ID = new NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact(session);
                legalInvoiceArtifact_ID.LegalInvoiceArtifactId = Guid.NewGuid();
                legalInvoiceArtifact_ID.RowStatus = Utility.Constant.ROWSTATUS_TEMP;
                legalInvoiceArtifact_ID.CreateDate = DateTime.Now;
                session.CommitTransaction();

                //set session LegalInvoiceArtifactId
                LegalInvoiceArtifactId = legalInvoiceArtifact_ID.LegalInvoiceArtifactId;
                #endregion

                #region LegalInvoiceArtifactIdentifier
                string[] Identifier_name = { "SERIES", "NUMBER", "TEMPLATE" };
                for (int i = 0; i < Identifier_name.Length; i++)
                {
                    LegalInvoiceArtifactIdentifierType identifiertype = BO.get_IdentifierTypeId(session, Identifier_name[i]);
                    if (identifiertype != null)
                    {
                        LegalInvoiceArtifactIdentifier identifier = new LegalInvoiceArtifactIdentifier(session);
                        identifier.LegalInvoiceArtifactId = legalInvoiceArtifact_ID;
                        identifier.LegalInvoiceArtifactIdentifierTypeId = identifiertype;
                        identifier.RowStatus = Utility.Constant.ROWSTATUS_TEMP;
                        session.CommitTransaction();
                    }
                }
                #endregion

                #region LegalInvoiceArtifactOrgActor
                string[] orgActorType = { "S", "B" };
                for (int i = 0; i < orgActorType.Length; i++)
                {
                    LegalInvoiceArtifactOrgActor orgActor_sell = new LegalInvoiceArtifactOrgActor(session);
                    orgActor_sell.LegalInvoiceArtifactId = legalInvoiceArtifact_ID;
                    orgActor_sell.OrgActorType = char.Parse(orgActorType[i]);
                    orgActor_sell.RowStatus = Utility.Constant.ROWSTATUS_TEMP;
                    session.CommitTransaction();
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void CRUD_LegalInvoiceArtifact_Saving()
        {
            try
            {
                string rowstatus = Utility.Constant.ROWSTATUS_ACTIVE.ToString();
                if (GUIContext.State is LegalInvoiceArtifact_Creating)
                    rowstatus = Utility.Constant.ROWSTATUS_TEMP.ToString();

                #region Hoa don GTGT

                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact artifact = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact>(LegalInvoiceArtifactId);
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType artifactTypeId = BO.get_LegalInvoiceArtifactTypeId(session, cbo_Code_ArtifactType.Text.ToString(), Utility.Constant.ROWSTATUS_ACTIVE.ToString());
                if (artifact == null)
                    throw new Exception("LegalInvoiceArtifactId is not exist System");

                artifact.LegalInvoiceArtifactTypeId = artifactTypeId;
                artifact.Code = txt_Code_Artifact.Text;
                artifact.IssuedDate = DateTime.Parse(txt_IssuedDate_Artifact.Text);
                artifact.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                artifact.LastUpdateDate = DateTime.Now;
                session.CommitTransaction(); //save

                string[] Identifier_name = { "SERIES", "NUMBER", "TEMPLATE" };
                for (int i = 0; i < Identifier_name.Length; i++)
                {
                    LegalInvoiceArtifactIdentifierType identifiertype = BO.get_IdentifierTypeId(session, Identifier_name[i]);
                    LegalInvoiceArtifactIdentifier identifier = BO.get_LegalInvoiceArtifactIdentifier(session, LegalInvoiceArtifactId, identifiertype.LegalInvoiceArtifactIdentifierTypeId, rowstatus);
                    if (identifiertype != null && identifier.LegalInvoiceArtifactId.LegalInvoiceArtifactId.Equals(LegalInvoiceArtifactId))
                    {
                        if (Identifier_name[i].Equals("TEMPLATE"))
                            identifier.Identifier = txt_Template_Identifier.Text;
                        if (Identifier_name[i].Equals("SERIES"))
                            identifier.Identifier = txt_Series_Identifier.Text;
                        if (Identifier_name[i].Equals("NUMBER"))
                            identifier.Identifier = txt_Number_Identifier.Text;
                        identifier.LegalInvoiceArtifactIdentifierTypeId = identifiertype; //identifierTypeID
                        if (!rowstatus.Equals(Utility.Constant.ROWSTATUS_ACTIVE.ToString()))
                            identifier.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;//RowStatus
                        session.CommitTransaction();  //save
                    }
                }
                #endregion

                #region Thong tin ban
                string[] orgActorType = { "S", "B" };
                for (int i = 0; i < orgActorType.Length; i++)
                {
                    LegalInvoiceArtifactOrgActor orgActor = BO.get_OrgActorId(session, LegalInvoiceArtifactId, orgActorType[i], rowstatus);
                    Organization Organization = BO.get_OrganizationId(session, cbo_Code_buy_Organization.Text.ToString());

                    if (orgActorType[i].Equals("S"))
                    {
                        Organization = BO.get_OrganizationId(session, cbo_Code_sell_Organization.Text.ToString());
                        orgActor.Code = Organization.Code;
                        orgActor.AccountNumber = txt_AccountNumber_sell_OrgActor.Text;
                        orgActor.TaxCode = txt_TaxCode_sell_OrgActor.Text;
                        orgActor.Address = memo_Address_sell_OrgActor.Text;
                        orgActor.TelephoneFax = txt_PhoneFax_sell_OrgActor.Text;
                        orgActor.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    }
                    else
                    {
                        orgActor.Code = Organization.Code;
                        orgActor.AccountNumber = txt_AccountNumber_buy_OrgActor.Text;
                        orgActor.TaxCode = txt_TaxCode_buy_OrgActor.Text;
                        orgActor.Address = memo_Address_buy_OrgActor.Text;
                        orgActor.TelephoneFax = txt_TelephoneFax_buy_OrgActor.Text;
                        orgActor.Description = txt_Description_buy_OrgActor.Text;
                        orgActor.Name = txt_Name_buy_OrgActor.Text;
                    }
                    session.CommitTransaction();   //save
                }
                #endregion

                #region thanh toan

                #endregion

                #region don vi tham gia
                #endregion

                using_state = "";
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void CRUD_LegalInvoiceArtifact_Editing()
        {
            try
            {
                #region load LegalInvoiceArtifact
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact legalInvoiceArtifact_ID = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact>(LegalInvoiceArtifactId);
                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType artifactType_code = session.GetObjectByKey<NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType>(Guid.Parse(legalInvoiceArtifact_ID.LegalInvoiceArtifactTypeId.LegalInvoiceArtifactTypeId.ToString()));
                if (artifactType_code == null)
                    throw new Exception("LegalInvoiceArtifactType is not exist system");
                txt_Code_Artifact.Text = legalInvoiceArtifact_ID.Code;
                txt_IssuedDate_Artifact.Date = legalInvoiceArtifact_ID.IssuedDate;
                cbo_Code_ArtifactType.Text = artifactType_code.Code;
                #endregion
                #region load LegalInvoiceArtifactIdentifier
                string[] Identifier_name = { "SERIES", "NUMBER", "TEMPLATE" };
                for (int i = 0; i < Identifier_name.Length; i++)
                {
                    LegalInvoiceArtifactIdentifierType identifiertype = BO.get_IdentifierTypeId(session, Identifier_name[i]);
                    LegalInvoiceArtifactIdentifier identifier = BO.get_LegalInvoiceArtifactIdentifier(session, LegalInvoiceArtifactId, identifiertype.LegalInvoiceArtifactIdentifierTypeId, Utility.Constant.ROWSTATUS_ACTIVE.ToString());
                    if (identifiertype != null && identifier.LegalInvoiceArtifactId.LegalInvoiceArtifactId.Equals(LegalInvoiceArtifactId))
                    {
                        if (Identifier_name[i].Equals("TEMPLATE"))
                            txt_Template_Identifier.Text = identifier.Identifier;
                        if (Identifier_name[i].Equals("SERIES"))
                            txt_Series_Identifier.Text = identifier.Identifier;
                        if (Identifier_name[i].Equals("NUMBER"))
                            txt_Number_Identifier.Text = identifier.Identifier;
                    }
                }
                #endregion
                #region load LegalInvoiceArtifactOrgActor
                string[] orgActorType = { "S", "B" };
                for (int i = 0; i < orgActorType.Length; i++)
                {
                    LegalInvoiceArtifactOrgActor orgActor = BO.get_OrgActorId(session, LegalInvoiceArtifactId, orgActorType[i], Utility.Constant.ROWSTATUS_ACTIVE.ToString());
                    if (orgActorType[i].Equals("S"))
                    {
                        cbo_Code_sell_Organization.Text = orgActor.Code;
                        txt_AccountNumber_sell_OrgActor.Text = orgActor.AccountNumber;
                        txt_TaxCode_sell_OrgActor.Text = orgActor.TaxCode;
                        txt_PhoneFax_sell_OrgActor.Text = orgActor.TelephoneFax;
                        memo_Address_sell_OrgActor.Text = orgActor.Address;
                    }
                    else
                    {
                        cbo_Code_buy_Organization.Text = orgActor.Code;
                        txt_AccountNumber_buy_OrgActor.Text = orgActor.AccountNumber;
                        txt_Name_buy_OrgActor.Text = orgActor.Name;
                        txt_TaxCode_buy_OrgActor.Text = orgActor.TaxCode;
                        txt_Description_buy_OrgActor.Text = orgActor.Description;
                        memo_Address_buy_OrgActor.Text = orgActor.Address;
                        txt_TelephoneFax_buy_OrgActor.Text = orgActor.TelephoneFax;
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CRUD_LegalInvoiceArtifact_Deleting()
        {
            try
            {
                if (using_state.Equals("Creating"))
                {
                    #region Hoa don GTGT
                    NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact artifact = BO.get_LegalInvoiceArtifactId(session, LegalInvoiceArtifactId, Utility.Constant.ROWSTATUS_TEMP.ToString());
                    artifact.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    session.CommitTransaction(); //save

                    string[] Identifier_name = { "SERIES", "NUMBER", "TEMPLATE" };
                    for (int i = 0; i < Identifier_name.Length; i++)
                    {
                        LegalInvoiceArtifactIdentifierType identifiertype = BO.get_IdentifierTypeId(session, Identifier_name[i]);
                        LegalInvoiceArtifactIdentifier identifier = BO.get_LegalInvoiceArtifactIdentifier(session, LegalInvoiceArtifactId, identifiertype.LegalInvoiceArtifactIdentifierTypeId, Utility.Constant.ROWSTATUS_TEMP.ToString());
                        if (identifiertype != null && identifier.LegalInvoiceArtifactId.LegalInvoiceArtifactId.Equals(LegalInvoiceArtifactId))
                        {
                            identifier.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                            session.CommitTransaction(); //save
                        }
                    }
                    #endregion

                    #region thong tin mua - ban
                    string[] orgActorType = { "S", "B" };
                    for (int i = 0; i < orgActorType.Length; i++)
                    {
                        LegalInvoiceArtifactOrgActor orgActor_sell = BO.get_OrgActorId(session, LegalInvoiceArtifactId, orgActorType[i], Utility.Constant.ROWSTATUS_TEMP.ToString());
                        orgActor_sell.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        session.CommitTransaction();
                    }
                    #endregion
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CRUD_LegalInvoiceArtifact_Cancel()
        {
            if (GUIContext.State is LegalInvoiceArtifact_Creating)
            {
                using_state = "Creating";
                GUIContext.State = new LegalInvoiceArtifact_Deleting(this);
            }
            else { using_state = ""; }
        }
        #endregion

        #region using State UpdateGUI
        public void UpdateGUI_LegalInvoiceArtifact_Creating()
        {
            PopupLegalInvoiceArtifact.ShowOnPageLoad = true;
            PopupLegalInvoiceArtifact.FindControl("btn_Edit_legal").Visible = false;
        }

        public void UpdateGUI_LegalInvoiceArtifact_Deleting()
        {
        }

        public void UpdateGUI_LegalInvoiceArtifact_Editing()
        {
            PopupLegalInvoiceArtifact.ShowOnPageLoad = true;
            PopupLegalInvoiceArtifact.FindControl("btn_Edit_legal").Visible = true;
            PopupLegalInvoiceArtifact.FindControl("btn_Save_legal").Visible = false;
        }

        public void Update_LegalInvoiceArtifact_Saving()
        {
            PopupLegalInvoiceArtifact.FindControl("btn_Edit_legal").Visible = false;
            PopupLegalInvoiceArtifact.FindControl("btn_Save_legal").Visible = true;
        }

        public void UpdateGUI_LegalInvoiceArtifact_Cancel()
        {
            PopupLegalInvoiceArtifact.ShowOnPageLoad = false;
            LegalInvoiceArtifactId = Guid.Empty;
        }
        #endregion

        #region setting grid artifactDetail
        protected void Grid_ArtifactDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                session.BeginTransaction();
                e.Cancel = true;
                string rowstatus = Utility.Constant.ROWSTATUS_ACTIVE.ToString();
                if (GUIContext.State is LegalInvoiceArtifact_Creating)
                    rowstatus = Utility.Constant.ROWSTATUS_TEMP.ToString();
                if (LegalInvoiceArtifactId == null)
                    throw new Exception("LegalInvoiceArtifactId is null");

                NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact legalInvoiceArtifactId = BO.get_LegalInvoiceArtifactId(session, LegalInvoiceArtifactId, rowstatus);
                LegalInvoiceArtifactDetail detail = new LegalInvoiceArtifactDetail(session);
                Item itemid = BO.get_ItemId(session, itemId, rowstatus);
                NAS.DAL.Nomenclature.Item.Unit unitid = BO.get_UnitId(session, unitID, rowstatus);

                //detail.ItemId = itemid;
                detail.Price = int.Parse(e.NewValues["Price"].ToString());
                detail.Amount = int.Parse(e.NewValues["Amount"].ToString());
                //detail.UnitId = unitid;
                detail.Total = int.Parse(e.NewValues["Total"].ToString());
                detail.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                detail.LegalInvoiceArtifactId = legalInvoiceArtifactId;

                session.FlushChanges();
                session.CommitTransaction();
                Grid_ArtifactDetail.CancelEdit();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_ArtifactDetail_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                session.BeginTransaction();
                e.Cancel = true;
                string rowstatus = Utility.Constant.ROWSTATUS_ACTIVE.ToString();

                LegalInvoiceArtifactDetail detail = session.FindObject<LegalInvoiceArtifactDetail>(
                    CriteriaOperator.And(
                        new BinaryOperator("LegalInvoiceArtifactId", LegalInvoiceArtifactId, BinaryOperatorType.Equal),
                        new BinaryOperator("ItemId", e.NewValues["ItemId"].ToString(), BinaryOperatorType.Equal),
                        new BinaryOperator("UnitId", e.NewValues["UnitId"].ToString(), BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", rowstatus, BinaryOperatorType.Equal)
                    ));
                Item itemid = BO.get_ItemId(session, e.NewValues["ItemId"].ToString(), rowstatus);
                NAS.DAL.Nomenclature.Item.Unit unitid = BO.get_UnitId(session, e.NewValues["UnitId"].ToString(), rowstatus);

                //detail.ItemId = itemid;
                detail.Price = int.Parse(e.NewValues["Price"].ToString());
                detail.Amount = int.Parse(e.NewValues["Amount"].ToString());
                //detail.UnitId = unitid;
                detail.Total = int.Parse(e.NewValues["Total"].ToString());
                session.FlushChanges();
                session.CommitTransaction();
                Grid_ArtifactDetail.CancelEdit();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_ArtifactDetail_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                session.BeginTransaction();
                string rowstatus = Utility.Constant.ROWSTATUS_ACTIVE.ToString();

                LegalInvoiceArtifactDetail detail = BO.get_LegalInvoiceArtifactDetailId(session, LegalInvoiceArtifactId, rowstatus);
                if (detail == null)
                    throw new Exception("LegalInvoiceArtifactDetail is not exist system");
                detail.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                session.FlushChanges();
                session.CommitTransaction();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void Grid_ArtifactDetail_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                string[] param = e.Parameters.Split('|');
                if (param[0].Equals("Item"))
                {
                    if (param[1] != null)
                    {
                        Item item_Id = session.FindObject<Item>(
                            CriteriaOperator.And(
                                new BinaryOperator("Code", param[1], BinaryOperatorType.Equal),
                                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                            ));
                        if (item_Id == null)
                            throw new Exception("Item is not exist system");
                        ItemUnit itemUnitId = BO.get_ItemUnit_UnitId(session, item_Id.ItemId, Utility.Constant.ROWSTATUS_ACTIVE);
                        itemId = item_Id.ItemId.ToString();
                        Session["UnitId"] = itemUnitId.UnitId.UnitId.ToString();
                    }
                    else
                    {
                        NAS.DAL.Nomenclature.Item.Unit u = session.FindObject<NAS.DAL.Nomenclature.Item.Unit>(new BinaryOperator("Name", "NAAN_DEFAULT", BinaryOperatorType.Equal));
                        Session["UnitId"] = u.UnitId.ToString();
                    }
                }
                if (param[0].Equals("Unit"))
                {
                    if (param[1] != null)
                    {
                        NAS.DAL.Nomenclature.Item.Unit Unitid = session.FindObject<NAS.DAL.Nomenclature.Item.Unit>(
                            CriteriaOperator.And(
                                new BinaryOperator("Code", param[1].ToString(), BinaryOperatorType.Equal),
                                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                            ));

                        unitID = Unitid.UnitId.ToString();
                    }
                }
            }
            catch (Exception) { throw; }
        }





        #endregion

        protected void cpLegalInvoiceArtifact_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string transition = param[0];
            switch (transition)
            {
                case "Create":
                    GUIContext.State = new LegalInvoiceArtifact_Creating(this);
                    break;
                case "Edit":
                    clock_textBox(true);
                    LegalInvoiceArtifactId = Guid.Parse(param[1]);
                    GUIContext.State = new LegalInvoiceArtifact_Editing(this);
                    break;
                case "Cancel":
                    GUIContext.State = new LegalInvoiceArtifact_Canceling(this);
                    break;
                case "Change_Edit":
                    Update_LegalInvoiceArtifact_Saving();
                    clock_textBox(false);
                    break;
                case "Save":
                    GUIContext.State = new LegalInvoiceArtifact_Saving(this);
                    break;
                default:
                    GUIContext.Request(transition);
                    break;
            }
        }






    }
}