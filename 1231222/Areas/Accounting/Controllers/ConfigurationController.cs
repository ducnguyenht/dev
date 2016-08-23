using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using MVC.Areas.Accounting.Models.Configuration;
using MVC.Models.Accounting.Configuration;

namespace MVC.Areas.Accounting.Controllers
{
    [Authorize]
    public class ConfigurationController : Controller
    {
        //
        // GET: /Accounting/Configuration/

        #region Bank
        public ActionResult Bank()
        {
            return View();
        }


        [ValidateInput(false)]
        public ActionResult BankGridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_BankGridViewPartial", BankModelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BankGridViewPartialAddNew(APIBank item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    BankModelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_BankGridViewPartial", BankModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BankGridViewPartialUpdate(APIBank item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    BankModelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_BankGridViewPartial", BankModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult BankGridViewPartialDelete(System.String Code)
        {
            //var model = new object[0];
            if (Code != null)
            {
                try
                {
                    BankModelList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_BankGridViewPartial", BankModelList.GetData());
        }
        #endregion Bank

        #region AccountingPeriod
        public ActionResult AccountingPeriod()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult AccountingPeriodGridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_AccountingPeriodGridViewPartial", AccountingPeriodModelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AccountingPeriodGridViewPartialAddNew(MVC.Areas.Accounting.Models.Configuration.APIAccountingPeriod item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    AccountingPeriodModelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_AccountingPeriodGridViewPartial", AccountingPeriodModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AccountingPeriodGridViewPartialUpdate(MVC.Areas.Accounting.Models.Configuration.APIAccountingPeriod item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    AccountingPeriodModelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_AccountingPeriodGridViewPartial", AccountingPeriodModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AccountingPeriodGridViewPartialDelete(System.String Code)
        {
            //var model = new object[0];
            if (Code != null)
            {
                try
                {
                    AccountingPeriodModelList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_AccountingPeriodGridViewPartial", AccountingPeriodModelList.GetData());
        }
        #endregion AccountingPeriod

        #region InternalBankAccount
        public ActionResult InternalBankAccount()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InternalBankAccountGridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_InternalBankAccountGridViewPartial", InternalBankAccountModelList.GetData());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InternalBankAccountGridViewPartialAddNew(APIInternalBankAccount item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    InternalBankAccountModelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_InternalBankAccountGridViewPartial", InternalBankAccountModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InternalBankAccountGridViewPartialUpdate(APIInternalBankAccount item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    InternalBankAccountModelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_InternalBankAccountGridViewPartial", InternalBankAccountModelList.GetData());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InternalBankAccountGridViewPartialDelete(System.String Code)
        {
            //var model = new object[0];
            if (Code != null)
            {
                try
                {
                    InternalBankAccountModelList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InternalBankAccountGridViewPartial", InternalBankAccountModelList.GetData());
        }
        #endregion InternalBankAccount

        #region InputVATInvoice
        public ActionResult InputVATInvoice()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InputVATInvoiceGridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_InputVATInvoiceGridViewPartial", InputVATInvoiceModelList.InputVATInvoiceModels());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InputVATInvoiceGridViewPartialAddNew(APIInputVATInvoice item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    InputVATInvoiceModelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_InputVATInvoiceGridViewPartial", InputVATInvoiceModelList.InputVATInvoiceModels());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InputVATInvoiceGridViewPartialUpdate(APIInputVATInvoice item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    InputVATInvoiceModelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_InputVATInvoiceGridViewPartial", InputVATInvoiceModelList.InputVATInvoiceModels());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InputVATInvoiceGridViewPartialDelete(System.Int32 Id)
        {
            //var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    InputVATInvoiceModelList.DeleteItem(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InputVATInvoiceGridViewPartial", InputVATInvoiceModelList.InputVATInvoiceModels());
        }
        #endregion InputVATInvoice


        #region OutputVATInvoice
        public ActionResult OutputVATInvoice()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult OutputVATInvoiceGridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_OutputVATInvoiceGridViewPartial", OutputVATInvoiceModelList.OutputVATInvoiceModels());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult OutputVATInvoiceGridViewPartialAddNew(APIOutputVATInvoice item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    OutputVATInvoiceModelList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_OutputVATInvoiceGridViewPartial", OutputVATInvoiceModelList.OutputVATInvoiceModels());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult OutputVATInvoiceGridViewPartialUpdate(APIOutputVATInvoice item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    OutputVATInvoiceModelList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_OutputVATInvoiceGridViewPartial", OutputVATInvoiceModelList.OutputVATInvoiceModels());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult OutputVATInvoiceGridViewPartialDelete(System.Int32 Id)
        {
            //var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    OutputVATInvoiceModelList.DeleteItem(Id);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_OutputVATInvoiceGridViewPartial", OutputVATInvoiceModelList.OutputVATInvoiceModels());
        }
        #endregion OutputVATInvoice


        #region AccountOrder master
        public ActionResult AccountOrder()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult AccountOrderGridViewPartial()
        {
            //var model = new object[0];
            return PartialView("_AccountOrderGridViewPartial", AccountOrderModelList.AccountOrders);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AccountOrderGridViewPartialAddNew(MVC.Models.Accounting.Configuration.APIAccountOrder item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    AccountOrderModelList.InsertAccountOrder(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_AccountOrderGridViewPartial", AccountOrderModelList.AccountOrders);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AccountOrderGridViewPartialUpdate(MVC.Models.Accounting.Configuration.APIAccountOrder item)
        {
            //var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    AccountOrderModelList.UpdateAccountOrder(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_AccountOrderGridViewPartial", AccountOrderModelList.AccountOrders);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AccountOrderGridViewPartialDelete(System.Int32 IDAccountOrder)
        {
            //var model = new object[0];
            if (IDAccountOrder >= 0)
            {
                try
                {
                    AccountOrderModelList.RemoveAccountOrderByID(IDAccountOrder);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_AccountOrderGridViewPartial", AccountOrderModelList.AccountOrders);
        }




        public ActionResult AccountDetailGridViewPartial(int accountOrderID)
        {
            ViewData["AccountOrderID"] = accountOrderID;
            return PartialView("_AccountDetailGridViewPartial", AccountOrderModelList.GetAccountsByAccountOrder(accountOrderID));
        }
        public ActionResult GridViewDetailAddNewPartial(APIAccount account, int accountOrderID)
        {
            ViewData["AccountOrderID"] = accountOrderID;
            account.AccountOrderID = accountOrderID;
            if (ModelState.IsValid)
                AccountOrderModelList.InsertAccount(account);
            return PartialView("_AccountDetailGridViewPartial", AccountOrderModelList.GetAccountsByAccountOrder(accountOrderID));
        }
        public ActionResult GridViewDetailUpdatePartial(APIAccount account, int accountOrderID)
        {
            ViewData["AccountOrderID"] = accountOrderID;
            if (ModelState.IsValid)
                AccountOrderModelList.UpdateAccount(account);
            return PartialView("_AccountDetailGridViewPartial", AccountOrderModelList.GetAccountsByAccountOrder(accountOrderID));
        }
        public ActionResult GridViewDetailDeletePartial(int ID, int accountOrderID)
        {
            ViewData["AccountOrderID"] = accountOrderID;
            AccountOrderModelList.RemoveAccountByID(ID);
            return PartialView("_AccountDetailGridViewPartial", AccountOrderModelList.GetAccountsByAccountOrder(accountOrderID));
        }

        #endregion AccountOrder

        #region InvoiceType

        public ActionResult InvoiceType()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult InvoiceTypeGridViewPartial()
        {
            var model = InvoiceTypeList.GetData();
            return PartialView("_InvoiceTypeGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult InvoiceTypeGridViewPartialAddNew(MVC.Areas.Accounting.Models.Configuration.APIInvoiceType item)
        {
            var model = InvoiceTypeList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    InvoiceTypeList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_InvoiceTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InvoiceTypeGridViewPartialUpdate(MVC.Areas.Accounting.Models.Configuration.APIInvoiceType item)
        {
            var model = InvoiceTypeList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    InvoiceTypeList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_InvoiceTypeGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult InvoiceTypeGridViewPartialDelete(System.String Code)
        {
            var model = InvoiceTypeList.GetData();
            if (Code != null)
            {
                try
                {
                    InvoiceTypeList.DeleteItem(Code);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_InvoiceTypeGridViewPartial", model);
        }

        #endregion InvoiceType


        #region OutputVATInvoiceConfig
        public ActionResult OutputVATInvoiceConfig()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult OutputVATInvoiceConfigGridViewPartial()
        {
            var model = OutputVATInvoiceConfigList.GetData();
            return PartialView("_OutputVATInvoiceConfigGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult OutputVATInvoiceConfigGridViewPartialAddNew(MVC.Areas.Accounting.Models.Configuration.APIOutputVATInvoiceConfig item)
        {
            var model = OutputVATInvoiceConfigList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    OutputVATInvoiceConfigList.AddItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_OutputVATInvoiceConfigGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult OutputVATInvoiceConfigGridViewPartialUpdate(MVC.Areas.Accounting.Models.Configuration.APIOutputVATInvoiceConfig item)
        {
            var model = OutputVATInvoiceConfigList.GetData();
            if (ModelState.IsValid)
            {
                try
                {
                    OutputVATInvoiceConfigList.UpdateItem(item);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_OutputVATInvoiceConfigGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult OutputVATInvoiceConfigGridViewPartialDelete(System.Int32 ID)
        {
            var model = OutputVATInvoiceConfigList.GetData();
            if (ID >= 0)
            {
                try
                {
                    OutputVATInvoiceConfigList.DeleteItem(ID);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_OutputVATInvoiceConfigGridViewPartial", model);
        }
        #endregion OutputVATInvoiceConfig
    }
}