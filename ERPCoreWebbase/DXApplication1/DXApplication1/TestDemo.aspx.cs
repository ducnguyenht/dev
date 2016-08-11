using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evaluant.Calculator;
using NAS.DAL.Accounting.Currency;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Accounting.Period;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.CMS.ObjectDocument;
using DevExpress.Data.Filtering;
using WebModule.Accounting.UserControl.uLegalInvoiceArtifact.State;
using NAS.DAL.Nomenclature.Item;
using NAS.BO.Accounting.LegalInvoiceArtifact;
using DevExpress.Web.ASPxGridView;
using NAS.BO.Accounting;
using NAS.DAL.Vouches;
using NAS.DAL.Nomenclature.Bank;
using NAS.DAL.Accounting.LegalInvoice;
using DevExpress.Web.ASPxEditors;
using System.Data;
using DevExpress.Web.ASPxClasses;
using NAS.DAL.BI.Accounting.Finance.SalesOrManufactureExpense;



namespace WebModule
{
    public partial class TestDemo : System.Web.UI.Page
    {
        Session session;
        LegalInvoiceArtifactBO BO = new LegalInvoiceArtifactBO();
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();

            DBLegalInvoiceArtifactDetail.Session = session;
            DBItem.Session = session;

            DBUnit.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public Guid LegalInvoiceArtifactId
        {
            get { return (Guid)Session["LegalInvoiceArtifactId"]; }
            set
            {
                Session["LegalInvoiceArtifactId"] = value;
            }
        }

        private string itemId
        {
            get { return (string)Session["itemId"]; }
            set { Session["itemId"] = value; }
        }

        private string unitID
        {
            get { return (string)Session["unitID"]; }
            set { Session["unitID"] = value; }
        }

        #region setting grid artifactDetail
        protected void Grid_ArtifactDetail_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                session.BeginTransaction();
                e.Cancel = true;
                string rowstatus = Utility.Constant.ROWSTATUS_ACTIVE.ToString();

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
                ASPxComboBox cbo = sender as ASPxComboBox;


                if (param[0].Equals("Item"))
                {
                    if (param[1] != null)
                    {
                        //    Item item_Id = session.FindObject<Item>(
                        //        CriteriaOperator.And(
                        //            new BinaryOperator("Code", param[1], BinaryOperatorType.Equal),
                        //            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                        //        ));
                        //    if (item_Id == null)
                        //        throw new Exception("Item is not exist system");
                        //    ItemUnit itemUnitId = BO.get_ItemUnit_UnitId(session, item_Id.ItemId, Utility.Constant.ROWSTATUS_ACTIVE);
                        //    itemId = item_Id.ItemId.ToString();
                        //    //Session["UnitId"] = itemUnitId.UnitId.UnitId.ToString();
                        //}
                        //else
                        //{
                        //    NAS.DAL.Nomenclature.Item.Unit u = session.FindObject<NAS.DAL.Nomenclature.Item.Unit>(new BinaryOperator("Name", "NAAN_DEFAULT", BinaryOperatorType.Equal));
                        //    Session["UnitId"] = u.UnitId.ToString();
                        //}
                        Item item_Id = session.FindObject<Item>(
                            CriteriaOperator.And(
                                new BinaryOperator("Code", param[1], BinaryOperatorType.Equal),
                                new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                            ));
                        if (item_Id == null)
                            throw new Exception("Item is not exist system");
                        XPCollection<ItemUnit> itemUnit = BO.get_xp_ItemUnit(session, item_Id.ItemId, Utility.Constant.ROWSTATUS_ACTIVE);
                        if (itemUnit != null)
                        {
                            foreach (ItemUnit itu in itemUnit)
                            {
                                //NAS.DAL.Nomenclature.Item.Unit unit = BO.get_UnitId_1(session, itu.UnitId.UnitId, Utility.Constant.ROWSTATUS_ACTIVE);
                                cbo.Items.Add(itu.UnitId.Code);
                            }
                        }
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

        protected void Grid_ArtifactDetail_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            object val = Grid_ArtifactDetail.GetRowValuesByKeyValue(e.KeyValue, "ItemId!Key");
            if (val == DBNull.Value || val == null)
                return;
            string item = (string)val;

            ASPxComboBox combo = e.Editor as ASPxComboBox;

            
            combo.Callback += new CallbackEventHandlerBase(cmb_Unit_OnCallback);
        }

        private void FillUnitCombo(ASPxComboBox cbo, string item)
        {
            if (string.IsNullOrEmpty(item))
                return;
            List<string> unit = get_Unit(item);
            cbo.Items.Clear();
            foreach (string u in unit)
            {
                cbo.Items.Add(u);
            }
        }

        List<string> get_Unit(string item)
        {
            List<string> list = new List<string>();
            Item item_Id = session.FindObject<Item>(
                CriteriaOperator.And(
                    new BinaryOperator("ItemId", item, BinaryOperatorType.Equal),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.Equal)
                ));
            if (item_Id == null)
                throw new Exception("Item is not exist system");
            XPCollection<ItemUnit> itemUnit = BO.get_xp_ItemUnit(session, item_Id.ItemId, Utility.Constant.ROWSTATUS_ACTIVE);
            if (itemUnit != null)
            {
                foreach (ItemUnit itu in itemUnit)
                {
                    NAS.DAL.Nomenclature.Item.Unit unit = BO.get_UnitId_1(session, itu.UnitId.UnitId, Utility.Constant.ROWSTATUS_ACTIVE);
                    list.Add(unit.Code);
                }
            }
            return list;
        }

        private void cmb_Unit_OnCallback(object source, CallbackEventArgsBase e)
        {
            FillUnitCombo(source as ASPxComboBox, e.Parameter);
        }
    }
}
