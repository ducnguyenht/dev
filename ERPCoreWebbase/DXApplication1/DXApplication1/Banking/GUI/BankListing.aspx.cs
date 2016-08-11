using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Nomenclature.Bank;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data.Filtering;
using System.Text.RegularExpressions;
using Utility;

namespace WebModule
{
    public partial class WebForm1 : System.Web.UI.Page, WebModule.Interfaces.IERPCoreWebModuleBase
    {
        Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsBanking.Session = session;
            dsBankBranch.Session = session;
        }

        public string AccessObjectId
        {
            get
            {
                return Constant.ACCESSOBJECT_BANK_LIST_ID;
            }
        }
        public string AccessObjectGroupId
        {
            get
            {
                return Constant.ACCESSOBJECT_BANK_GROUPID;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Utils.ApplyTheme(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gridviewBanking_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                Guid bankId = (Guid)e.Keys["BankId"];
                Bank bank = session.GetObjectByKey<Bank>(bankId);
                bank.Delete();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void gridviewBanking_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            try
            {
                string code = (string)e.NewValues["Code"];
                string name = (string)e.NewValues["Name"];
                string description = (string)e.NewValues["Description"];
                string swiffcode = (string)e.NewValues["SwiffCode"];

                if (name == null) { name = String.Empty; }
                if (description == null) { description = String.Empty; }
                if (swiffcode == null) { swiffcode = String.Empty; }

                Bank bank = new Bank(session);
                bank.Code = code;
                bank.Name = name;
                bank.Description = description;
                bank.SwiffCode = swiffcode;
                bank.Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                gridviewBanking.CancelEdit();
            }
        }

        protected void gridviewBanking_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                string code = (string)e.NewValues["Code"];
                string name = (string)e.NewValues["Name"];
                string description = (string)e.NewValues["Description"];
                string swiffcode = (string)e.NewValues["SwiffCode"];

                if (name == null) { name = String.Empty; }
                if (description == null) { description = String.Empty; }
                if (swiffcode == null) { swiffcode = String.Empty; }

                Guid bankId = (Guid)e.Keys["BankId"];
                Bank bank = session.GetObjectByKey<Bank>(bankId);
                bank.Code = code;
                bank.Name = name;
                bank.Description = description;
                bank.SwiffCode = swiffcode;
                bank.Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                gridviewBanking.CancelEdit();
            }
        }




        protected void gridviewBankBranch_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView gridview = (ASPxGridView)sender;
            try
            {
                string code = (string)e.NewValues["Code"];
                string name = (string)e.NewValues["Name"];
                string description = (string)e.NewValues["Description"];
                string address = (string)e.NewValues["Address"];
                string phonefax = (string)e.NewValues["PhoneFax"];

                if (name == null) { name = String.Empty; }
                if (description == null) { description = String.Empty; }
                if (address == null) { address = String.Empty; }
                if (phonefax == null) { phonefax = String.Empty; }

                CriteriaOperator criteria = new BinaryOperator("BankId", gridview.GetMasterRowKeyValue());
                Bank bank = session.FindObject<Bank>(criteria);

                BankBranch bankBranch = new BankBranch(session);
                bankBranch.Code = code;
                bankBranch.Name = name;
                bankBranch.Description = description;
                bankBranch.Address = address;
                bankBranch.PhoneFax = phonefax;
                bankBranch.BankId = bank;
                bankBranch.Save();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                e.Cancel = true;
                gridview.CancelEdit();
            }
        }

        protected void gridviewBankBranch_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gridview = (ASPxGridView)sender;
            try
            {
                string code = (string)e.NewValues["Code"];
                string name = (string)e.NewValues["Name"];
                string description = (string)e.NewValues["Description"];
                string address = (string)e.NewValues["Address"];
                string phonefax = (string)e.NewValues["PhoneFax"];

                if (name == null) { name = String.Empty; }
                if (description == null) { description = String.Empty; }
                if (address == null) { address = String.Empty; }
                if (phonefax == null) { phonefax = String.Empty; }

                Guid bankBranchId = (Guid)e.Keys["BankBranchId"];


                BankBranch bankBranch = session.GetObjectByKey<BankBranch>(bankBranchId);
                bankBranch.Code = code;
                bankBranch.Name = name;
                bankBranch.Description = description;
                bankBranch.Address = address;
                bankBranch.PhoneFax = phonefax;
                bankBranch.Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                e.Cancel = true;
                gridview.CancelEdit();
            }
        }

        protected void gridviewBankBranch_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            try
            {
                Guid bankBranchId = (Guid)e.Keys["BankBranchId"];
                BankBranch bankBranch = session.GetObjectByKey<BankBranch>(bankBranchId);
                bankBranch.Delete();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                e.Cancel = true;
            }
        }

        protected void gridviewBankBranch_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            Guid bankId = (Guid)grid.GetMasterRowKeyValue();
            dsBankBranch.Criteria = new BinaryOperator("BankId!Key", bankId).ToString();
        }

        protected void gridviewBankBranch_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //Guid bankId = (Guid)grid.GetMasterRowKeyValue();
            //e.NewValues["BankId!Key"] = bankId;
        }

        protected void gridviewBanking_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["Code"] == null)
            { 
                throw new Exception("Mã số Ngân hàng không được để trống.");
            }
            else
            {
                XPCollection<Bank> b = new XPCollection<Bank>(session, CriteriaOperator.Parse("Code = '" + e.NewValues["Code"].ToString() + "'"));
                if (e.IsNewRow)
                {
                    if (b.Count > 0) throw new Exception("Mã số Ngân hàng đã tồn tại.");
                }
                else
                {
                    if (e.NewValues["Code"].ToString() != e.OldValues["Code"].ToString())
                    {
                        if (b.Count > 0) throw new Exception("Mã số Ngân hàng đã tồn tại.");
                    }
                }
            }
        }

        protected void gridviewBankBranch_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;

            if (e.NewValues["Code"] == null)
            {
                throw new Exception("Mã số chi nhánh không được để trống.");
            }
            else
            {
                XPCollection<BankBranch> b = new XPCollection<BankBranch>(session, CriteriaOperator.Parse("Code = '" + e.NewValues["Code"].ToString() + "' AND BankId='" + grid.GetMasterRowKeyValue() + "'"));
                if (e.IsNewRow)
                {
                    if (b.Count > 0) throw new Exception("Mã số chi nhánh đã tồn tại.");
                }
                else
                {
                    if (e.NewValues["Code"].ToString() != e.OldValues["Code"].ToString())
                    {
                        if (b.Count > 0) throw new Exception("Mã số chi nhánh đã tồn tại.");
                    }
                }
            }

            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            if (e.NewValues["PhoneFax"] == null) return;
            if (!regex.IsMatch(e.NewValues["PhoneFax"].ToString())) throw new Exception("Điện thoại/Fax phải là dạng số.");
        }
    }
}