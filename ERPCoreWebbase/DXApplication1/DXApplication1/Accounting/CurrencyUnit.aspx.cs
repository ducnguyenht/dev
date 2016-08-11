using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
using NAS.BO.Accounting;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Currency;
using DevExpress.Web.ASPxEditors;
using NAS.DAL.Vouches;


namespace WebModule.Accounting
{
    public partial class CurrencyUnit : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {


        #region *
        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_ACCOUNT_CURRENCY_UNIT_ID;
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
        #endregion

        CurrencyBO bo = new CurrencyBO();
        Session session = XpoHelper.GetNewUnitOfWork();

        protected void Page_Init(object sender, EventArgs e)
        {
            DBCurrencyType.Session = session;
            DBCurrency.Session = session;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //NAS.DAL.Accounting.Currency.Currency test = bo.get_Currency_true_master(session, "e6cbe93d-72eb-479b-8e04-5878102c161a", true, 1);
        }

        protected object GetMasterRowKeyValue(ASPxTreeList treeList)
        {
            GridViewBaseRowTemplateContainer container = null;
            Control control = treeList;
            while (control.Parent != null)
            {
                container = control.Parent as GridViewBaseRowTemplateContainer;
                if (container != null) break;
                control = control.Parent;
            }
            return container.KeyValue;
        }

        #region check_isvalue
        private bool check_ExchangeRate_IsValue(string Crurrency_Id, string CurrencyType_CurrencyId)
        {
            try
            {
                bool check = false;
                if (CurrencyType_CurrencyId != null)
                {
                    ExchangeRate ex_DenomiratorCurrencyId = bo.get_ExchangeRate_DenomiratorCurrencyId_IsValue(session, CurrencyType_CurrencyId, Utility.Constant.ROWSTATUS_ACTIVE);
                    if (ex_DenomiratorCurrencyId != null)
                        check = true;
                }
                if (Crurrency_Id != null)
                {
                    ExchangeRate ex_NumeratorCurrencyId = bo.get_ExchangeRate_NumeratorCurrencyId_IsValue(session, Crurrency_Id, Utility.Constant.ROWSTATUS_ACTIVE);
                    if (ex_NumeratorCurrencyId != null)
                        check = true;
                }

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected object GetMasterRowKeyValue(ASPxGridView Grid)
        {
            GridViewBaseRowTemplateContainer container = null;
            Control control = Grid;
            while (control.Parent != null)
            {
                container = control.Parent as GridViewBaseRowTemplateContainer;
                if (container != null) break;
                control = control.Parent;
            }
            return container.KeyValue;
        }
        #endregion

        #region GridCurrencyUnit
        protected void GridCurrencyUnit_Init(object sender, EventArgs e)
        {
            //gom row masterpage
            GridCurrencyUnit.SettingsDetail.AllowOnlyOneMasterRowExpanded = true;
            if (GridCurrencyUnit.SettingsDetail.AllowOnlyOneMasterRowExpanded)
            {
                GridCurrencyUnit.DetailRows.CollapseAllRows();
            }
            //end gom row masterpage
        }

        protected void GridCurrencyUnit_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();

                #region ExchangeRate_IsMaster
                bool CancelIsMaster = true;
                CurrencyType currencyType_ID = bo.get_CurrencyType_true(session, true, Utility.Constant.ROWSTATUS_ACTIVE);
                NAS.DAL.Accounting.Currency.Currency currency_ID = bo.get_Currency_true_master(session, currencyType_ID.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);
                if (currency_ID != null)
                {
                    if (e.NewValues["IsMaster"] != null)
                        if (e.NewValues["IsMaster"].Equals(true))
                            if (bo.IsUsedInExchangeRate(session, currency_ID.CurrencyId))
                                CancelIsMaster = false;

                    if (!CancelIsMaster)
                        throw new Exception(String.Format("Không được chọn Sử Dụng Chính! Vì Tên Tiền Tệ {0} đã sử dụng trong Tỷ Giá", currencyType_ID.Name));
                }
                #endregion

                CurrencyType ct = new CurrencyType(session);
                if (bo.checkCurrencyType_Name(session, e.NewValues["Name"].ToString().Trim()))
                {
                    e.Cancel = true;
                    throw new Exception(String.Format("Lỗi trùng đơn vị Tiền Tệ"));
                }
                else
                {
                    if (e.NewValues["IsMaster"] == null)
                    {
                        e.NewValues["IsMaster"] = false;
                    }
                    bool isMaster = bool.Parse(e.NewValues["IsMaster"].ToString());

                    if (isMaster)
                    {
                        if (bo.changeIsMasterCurrencyType(session))
                            e.NewValues["IsMaster"] = true;
                    }
                    ct.Name = e.NewValues["Name"].ToString();
                    if (e.NewValues["Description"] == null)
                    {
                        e.NewValues["Description"] = "";
                    }
                    ct.Description = e.NewValues["Description"].ToString();
                    ct.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    ct.IsMaster = bool.Parse(e.NewValues["IsMaster"].ToString());
                    session.FlushChanges();
                    session.CommitTransaction();
                    GridCurrencyUnit.DataBind();
                }
                GridCurrencyUnit.CancelEdit();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void GridCurrencyUnit_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction();

                #region ExchangeRate_IsMaster
                bool CancelIsMaster = true;
                CurrencyType currencyType_ID = bo.get_CurrencyType_true(session, true, Utility.Constant.ROWSTATUS_ACTIVE);
                NAS.DAL.Accounting.Currency.Currency currency_ID = bo.get_Currency_true_master(session, currencyType_ID.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);
                if (currency_ID != null)
                {
                    if (e.NewValues["IsMaster"] != null)
                        if (e.NewValues["IsMaster"].Equals(true))
                            if (bo.IsUsedInExchangeRate(session, currency_ID.CurrencyId))
                                CancelIsMaster = false;
                    if (!CancelIsMaster)
                    {
                        throw new Exception(String.Format("Không được chọn Sử Dụng Chính! Vì Tên Tiền Tệ {0} đã sử dụng trong Tỷ Giá", currencyType_ID.Name));
                    }
                }
                #endregion

                CurrencyType ct = session.GetObjectByKey<CurrencyType>(Guid.Parse(e.Keys[0].ToString()));

                #region check Name
                if (!e.OldValues["Name"].Equals(e.NewValues["Name"]))
                {
                    if (bo.checkCurrencyType_Name(session, e.NewValues["Name"].ToString().Trim()))
                    {
                        e.Cancel = true;
                        throw new Exception(String.Format("Lỗi trùng đơn vị Tiền Tệ"));
                    }
                }
                #endregion

                if (e.NewValues["IsMaster"] == null)
                {
                    e.NewValues["IsMaster"] = false;
                }
                bool isMaster = bool.Parse(e.NewValues["IsMaster"].ToString());

                ct.Name = e.NewValues["Name"].ToString();
                if (e.NewValues["Description"] == null)
                {
                    e.NewValues["Description"] = "";
                }
                ct.Description = e.NewValues["Description"].ToString();
                #region ct.isMaster
                ct.IsMaster = bool.Parse(e.NewValues["IsMaster"].ToString());
                if (isMaster)
                {
                    if (bo.changeIsMasterCurrencyType(session))
                    {
                        e.NewValues["IsMaster"] = true;
                    }
                }
                ct.IsMaster = bool.Parse(e.NewValues["IsMaster"].ToString());
                #endregion
                session.FlushChanges();
                session.CommitTransaction();
                GridCurrencyUnit.DataBind();
                GridCurrencyUnit.CancelEdit();
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        protected void GridCurrencyUnit_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                #region ExchangeRate_IsMaster
                bool CancelIsMaster = true;
                CurrencyType currencyType_ID = bo.get_CurrencyType_true(session, true, Utility.Constant.ROWSTATUS_ACTIVE); //truong hop da chon ismaster
                NAS.DAL.Accounting.Currency.Currency currency_ID = bo.get_Currency_true_master(session, currencyType_ID.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);
                CurrencyType currencyType_id = bo.get_CurrencyTypeId(session, e.Keys[0].ToString(), Utility.Constant.ROWSTATUS_ACTIVE);//truong hop chua chon ismaster
                NAS.DAL.Accounting.Currency.Currency currency_id = bo.get_Currency_true_master(session, currencyType_id.CurrencyTypeId.ToString(), true, Utility.Constant.ROWSTATUS_ACTIVE);
                if (currency_id != null)
                {
                    if (bo.IsUsedInExchangeRate(session, currency_id.CurrencyId))
                    {
                        if (bo.IsUsedInExchangeRate(session, currency_ID.CurrencyId))
                        {
                            CancelIsMaster = false;
                        }

                        if (!CancelIsMaster)
                        {
                            throw new Exception(String.Format("Không được Xóa! Vì Tên Tiền Tệ {0} đã sử dụng trong Tỷ Giá", currencyType_ID.Name));
                        }
                    }
                }
                #endregion

                if (bo.checkIsCurrencyTypeIdInCurrency(session, e.Values["CurrencyTypeId"].ToString().Trim()))
                {
                    e.Cancel = true;
                    throw new Exception(String.Format("Lỗi không thể xóa vì có chứa Đơn Vị Tiền Tệ"));
                }
                else
                {
                    e.Cancel = true;
                    session.BeginTransaction(); //tao session luu gia tri hien tai

                    Guid a = Guid.Parse(e.Keys[0].ToString());
                    NAS.DAL.Accounting.Currency.CurrencyType currT = session.GetObjectByKey<NAS.DAL.Accounting.Currency.CurrencyType>(a);
                    currT.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    currT.Save();
                    GridCurrencyUnit.DataBind();
                    session.CommitTransaction();
                }
            }
            catch (Exception)
            {
                session.RollbackTransaction();//loi session luu gia tri se rollback
                e.Cancel = true;
                throw;
            }
        }
        #endregion

        #region treelistCurrency
        protected void TLCurrency_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.Cancel = true;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                try
                {
                    uow.BeginTransaction();
                    ASPxTreeList treelistCurrency = sender as ASPxTreeList;
                    object keyValue = GetMasterRowKeyValue(treelistCurrency);

                    #region Check ExchangeRate

                    bool CanInsert = true;
                    CurrencyType type = session.GetObjectByKey<CurrencyType>(keyValue);
                    NAS.DAL.Accounting.Currency.Currency defaultCurrency = bo.get_Currency_true_master(session, type.CurrencyTypeId.ToString(), true, Constant.ROWSTATUS_ACTIVE);
                    if (e.NewValues["IsDefault"] != null)
                    {
                        if (e.NewValues["IsDefault"].Equals(true))
                        {
                            if (defaultCurrency != null)
                            {
                                if (bo.IsUsedInExchangeRate(session, defaultCurrency.CurrencyId))
                                {
                                    CanInsert = false;
                                }
                            }
                            else
                            {
                                CanInsert = true;
                            }
                        }
                        else
                        {
                            CanInsert = true;
                        }
                        if (!CanInsert)
                        {
                            throw new Exception(String.Format("Không được chọn Đơn Vị Mặc Định! Vì Đơn Vị Tiền Tệ {0} đã sử dụng trong Tỷ Giá", defaultCurrency.Code));
                        }
                    }
                    #endregion

                    #region CHECK CODE
                    CurrencyType cur = uow.GetObjectByKey<CurrencyType>(Guid.Parse(keyValue.ToString()));
                    if (bo.checkCurrency_Code(uow, e.NewValues["Code"].ToString().Trim(), cur.CurrencyTypeId.ToString()))
                    {
                        e.Cancel = true;
                        throw new Exception(String.Format("Lỗi Đơn Vị Tiền Tệ đã có"));
                    }
                    #endregion

                    if (e.NewValues["IsDefault"] == null)
                        e.NewValues["IsDefault"] = false;

                    bool isDefault = bool.Parse(e.NewValues["IsDefault"].ToString());

                    NAS.DAL.Accounting.Currency.Currency currency = new NAS.DAL.Accounting.Currency.Currency(uow);

                    currency.CurrencyTypeId = cur;
                    currency.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    currency.Code = e.NewValues["Code"].ToString();
                    currency.Name = e.NewValues["Name"].ToString();
                    string parentKeyStr = treelistCurrency.NewNodeParentKey.ToString();
                    #region currency.ParentCurrencyId
                    if (!parentKeyStr.Equals(string.Empty) && parentKeyStr != null)
                    {
                        NAS.DAL.Accounting.Currency.Currency ParentCurrency =
                            uow.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(Guid.Parse(parentKeyStr));

                        if (ParentCurrency == null)
                            throw new Exception("Currency is not exist in system");

                        currency.ParentCurrencyId = ParentCurrency;
                    }
                    #endregion
                    currency.NumRequired = double.Parse(e.NewValues["NumRequired"].ToString());
                    uow.FlushChanges();

                    #region currency.IsDefault
                    if (isDefault)
                    {

                        //if (bo.changeIsDefaultCurrency(uow))//, Guid.Parse(treelistCurrency.FocusedNode.Key.ToString())
                        //{
                        currency.IsDefault = true;
                        //}
                        //bo.changeCoefficientCurrency(uow); // Coefficient = 0;
                        if (isDefault)
                        {
                            bo.updateIsDefaultCurrency(uow, Guid.Parse(keyValue.ToString()), currency.CurrencyId, isDefault);
                        }
                        //if (cur != null)
                        //{
                        //    if (bo.changeIsMasterCurrencyType(uow))
                        //        cur.IsMaster = true;
                        //    cur.Save();
                        //}
                    }
                    else
                    {
                        bo.updateIsDefaultCurrency(uow, Guid.Parse(keyValue.ToString()), currency.CurrencyId, isDefault);
                        e.NewValues["IsDefault"] = false;
                        e.NewValues["Description"] = "";
                    }
                    #endregion

                    uow.CommitChanges();
                    treelistCurrency.CancelEdit();
                    treelistCurrency.DataBind();
                    GridCurrencyUnit.DataBind();
                    treelistCurrency.JSProperties.Add("cpSaved", true);

                }

                catch (Exception)
                {
                    uow.RollbackTransaction();
                    throw;
                }
        }

        protected void treelistCurrency_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.Cancel = true;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
                try
                {
                    ASPxTreeList treelistCurrency = sender as ASPxTreeList;
                    object keyValue = GetMasterRowKeyValue(treelistCurrency);

                    #region Check ExchangeRate

                    bool CanInsert = true;
                    CurrencyType type = session.GetObjectByKey<CurrencyType>(keyValue);
                    NAS.DAL.Accounting.Currency.Currency defaultCurrency = bo.get_Currency_true_master(session, type.CurrencyTypeId.ToString(), true, Constant.ROWSTATUS_ACTIVE);
                    if (e.NewValues["IsDefault"] != null)
                    {
                        if (e.NewValues["IsDefault"].Equals(true))
                        {
                            if (defaultCurrency != null)
                            {
                                if (bo.IsUsedInExchangeRate(session, defaultCurrency.CurrencyId))
                                {
                                    CanInsert = false;
                                }
                            }
                            else
                            {
                                CanInsert = true;
                            }
                        }
                        else
                        {
                            CanInsert = true;
                        }
                        if (!CanInsert)
                        {
                            throw new Exception(String.Format("Không được chọn Đơn Vị Mặc Định! Vì Đơn Vị Tiền Tệ {0} đã sử dụng trong Tỷ Giá", defaultCurrency.Code));
                        }
                    }
                    #endregion

                    #region CHECK CODE
                    CurrencyType cur = uow.GetObjectByKey<CurrencyType>(Guid.Parse(keyValue.ToString()));
                    if (!e.OldValues["Code"].Equals(e.NewValues["Code"]))
                    {
                        if (bo.checkCurrency_Code(session, e.NewValues["Code"].ToString().Trim(), cur.CurrencyTypeId.ToString()))
                        {
                            e.Cancel = true;
                            throw new Exception(String.Format("Lỗi Đơn Vị Tiền Tệ đã có"));
                        }
                    }
                    #endregion
                    object MasterKey = GetMasterRowKeyValue(treelistCurrency);
                    Guid key = Guid.Parse(e.Keys[0].ToString());
                    if (e.NewValues["IsDefault"] == null)
                        e.NewValues["IsDefault"] = false;

                    bool IsDefault = bool.Parse(e.NewValues["IsDefault"].ToString());

                    NAS.DAL.Accounting.Currency.Currency currency = uow.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(key);
                    #region Currency.IsDefault
                    if (IsDefault)
                    {
                        //if (bo.changeIsDefaultCurrency(uow))//, Guid.Parse(treelistCurrency.FocusedNode.Key.ToString())
                        //{
                        currency.IsDefault = true;
                        //}
                        //bo.changeCoefficientCurrency(uow); // Coefficient = 0;
                        if (IsDefault)
                        {
                            bo.updateIsDefaultCurrency(uow, Guid.Parse(MasterKey.ToString()), key, IsDefault);
                        }

                        //if (cur != null)
                        //{
                        //    if (bo.changeIsMasterCurrencyType(uow))
                        //        cur.IsMaster = true;
                        //    cur.Save();
                        //}
                    }
                    else
                    {
                        bo.updateIsDefaultCurrency(uow, Guid.Parse(MasterKey.ToString()), key, IsDefault);
                        currency.IsDefault = false;
                        e.NewValues["Description"] = "";
                    }
                    #endregion
                    currency.Code = e.NewValues["Code"].ToString();
                    currency.Name = e.NewValues["Name"].ToString();
                    currency.NumRequired = double.Parse(e.NewValues["NumRequired"].ToString());

                    uow.FlushChanges();
                    treelistCurrency.CancelEdit();
                    treelistCurrency.DataBind();
                    GridCurrencyUnit.DataBind();
                    treelistCurrency.JSProperties.Add("cpSaved", true);

                }
                catch (Exception)
                {
                    uow.RollbackTransaction();
                    throw;
                }
        }

        protected void treelistCurrency_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                e.Cancel = true;
                session.BeginTransaction(); //tao session luu gia tri hien tai

                ASPxTreeList treelistCurrency = sender as ASPxTreeList;
                object keyValue = GetMasterRowKeyValue(treelistCurrency);

                #region Check ExchangeRate

                bool CanInsert = true;
                CurrencyType type = session.GetObjectByKey<CurrencyType>(keyValue);
                NAS.DAL.Accounting.Currency.Currency defaultCurrency = bo.get_Currency_true_master(session, type.CurrencyTypeId.ToString(), true, Constant.ROWSTATUS_ACTIVE);

                if (defaultCurrency != null)
                {
                    if (bo.IsUsedInExchangeRate(session, defaultCurrency.CurrencyId))
                        CanInsert = false;
                }
                else
                {
                    CanInsert = true;
                }
                if (!CanInsert)
                    throw new Exception(String.Format("Không được Xóa! Vì Đơn Vị Tiền Tệ {0} đã sử dụng trong Tỷ Giá", defaultCurrency.Code));

                #endregion

                Guid a = Guid.Parse(e.Keys[0].ToString());
                NAS.DAL.Accounting.Currency.Currency curr = session.GetObjectByKey<NAS.DAL.Accounting.Currency.Currency>(a);
                curr.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                curr.Save();
                session.CommitTransaction();

            }
            catch (Exception)
            {
                session.RollbackTransaction();//loi session luu gia tri se rollback
                e.Cancel = true;
                throw;
            }
        }

        protected void TLCurrency_OnInit(object sender, EventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            object keyValue = GetMasterRowKeyValue(treeList);
            Session["SessionCurrencyTypeId"] = keyValue.ToString();
        }

        protected void treelistCurrency_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            try
            {
                float a;
                if (e.OldValues["NumRequired"] != null)
                {
                    if (e.OldValues["NumRequired"].ToString().Equals(e.NewValues["NumRequired"].ToString()))
                    {
                        a = float.Parse(e.OldValues["NumRequired"].ToString());

                    }
                    a = float.Parse(e.NewValues["NumRequired"].ToString());
                }
                else
                {
                    a = float.Parse(e.NewValues["NumRequired"].ToString());
                }
                if (a <= 0)
                {
                    throw new Exception(String.Format("Số phải lớn hơn 0"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void treelistCurrency_CellEditorInitialize(object sender, DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs e)
        {
            ASPxTreeList treelistCurrency = sender as ASPxTreeList;
            if (e.Column.FieldName.Equals("NumRequired") && !treelistCurrency.IsNewNodeEditing)
            {
                TreeListNode node = treelistCurrency.FindNodeByKeyValue(e.NodeKey);
                if (node.Level == 1)
                {
                    ASPxTextEdit txtEdit = (ASPxTextEdit)e.Editor;
                    txtEdit.ReadOnly = true;
                }
            }

            if (e.Column.FieldName.Equals("NumRequired") && treelistCurrency.IsNewNodeEditing
                && treelistCurrency.NewNodeParentKey == treelistCurrency.RootNode.Key)
            {
                e.Editor.Value = 1;
                e.Editor.ReadOnly = true;
            }


            if (e.Column.FieldName.Equals("NumRequired") && !treelistCurrency.IsNewNodeEditing)
            {
                e.Editor.Focus();
            }
        }

        protected void treelistCurrency_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            try
            {
                ASPxTreeList tree = sender as ASPxTreeList;
                TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                NAS.DAL.Accounting.Currency.Currency currency = node.DataItem as NAS.DAL.Accounting.Currency.Currency;

                if (currency != null)
                {
                    //Setting Description for node
                    if (e.Column.Name == "Description" && e.Level > 1)
                    {

                        e.Cell.Text = String.Format("1 {0} = {1} {2}",
                            currency.ParentCurrencyId.Name,
                            currency.NumRequired,
                            currency.Name);
                    }
                    else if (e.Column.Name == "Description" && e.Level == 1)
                    {
                        e.Cell.Text = String.Format("Là đơn vị cao nhất");
                    }

                    if (e.Column.FieldName == "NumRequired" && e.Level == 1)
                        e.Cell.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


    }
}