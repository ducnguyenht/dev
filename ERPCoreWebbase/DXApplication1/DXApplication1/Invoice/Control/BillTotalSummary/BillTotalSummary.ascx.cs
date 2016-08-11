using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using NAS.DAL.Accounting.Tax;
using NAS.BO.Invoice;
using NAS.BO.Accounting.Journal;

namespace WebModule.Invoice.Control.BillTotalSummary
{
    public partial class BillTotalSummary : System.Web.UI.UserControl
    {
        public bool IsInCallback
        {
            get
            {
                return panelBillTotalSummary.IsCallback | 
                    panelBillPromotionInfo.IsCallback | 
                    panelBillTaxInfo.IsCallback;
            }
        }
        
        public Guid BillId
        {
            get;
            set;
        }

        public string _ClientInstanceName { 
            get 
            {
                if (ClientInstanceName == null || ClientInstanceName.Trim().Length == 0)
                    return ClientID;
                return ClientInstanceName;
            } 
        }

        public string ClientInstanceName
        {
            get;
            set;
        }

        public string PromotionInfoUpdated { get; set; }

        public string TaxInfoUpdated { get; set; }

        public string PromotionInfoClosing
        {
            get;
            set;
        }

        public string TaxInfoClosing
        {
            get;
            set;
        }

        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsTax.Session = session;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dsTax.Criteria = BinaryOperator.And(
                BinaryOperator.Or(
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT)
                ),
                //new BinaryOperator("TaxTypeId.Code", "GTGT")).ToString();
                new BinaryOperator("TaxTypeId.Code", "VAT")).ToString();
            comboVAT.DataBindItems();
        }

        public void UpdateTotalSummary()
        {
            if (BillId != null && !BillId.Equals(Guid.Empty))
            {
                Bill bill = session.GetObjectByKey<Bill>(BillId);

                //use default TaxCalculationType
                //if (!bill.TaxCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
                //{
                //    bill.TaxCalculationType = Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE;
                //    bill.Save();
                //}

                //Update subtotal
                UpdateSubtotal(bill);
                //Update bill promotion
                UpdateBillPromotion();
                //Update bill tax
                UpdateBillTax(bill);
                //Update total
                UpdateTotal(bill);
                //Update paid
                UpdatePaid(bill);
                //Update owned
                UpdateOwned(bill);
            }
        }

        private void UpdateOwned(Bill bill)
        {
            lblOwned.Text = String.Format("{0:N0}", (bill.Total - Paid));
        }

        private double Paid { get; set; }

        private void UpdatePaid(Bill bill)
        {
            VoucherTransactionBOBase voucherTransactionBO = null;
            if (bill is NAS.DAL.Invoice.SalesInvoice)
                voucherTransactionBO = new ReceiptVoucherTransactionBO();
            else if (bill is NAS.DAL.Invoice.PurchaseInvoice)
                voucherTransactionBO = new PaymentVoucherTransactionBO();
            double paid = 0;
            var genaralJournal = voucherTransactionBO.GetActuallyCollectedOfBill(session, bill.BillId);
            if (genaralJournal == null)
            {
                paid = 0;
            }
            else
            {
                paid = Math.Abs(genaralJournal.Sum(r => r.Debit) - genaralJournal.Sum(r => r.Credit));
            }
            Paid = paid;
            lblPaid.Text = String.Format("{0:N0}", paid);
        }

        private double Subtotal
        {
            get;
            set;
        }

        private double BillPromotionInNumber
        {
            get;
            set;
        }

        private double BillTaxInNumber
        {
            get;
            set;
        }

        private void UpdateTotal(Bill bill)
        {
            double total = bill.Total;
            if (total != 0)
                lblTotal.Text = String.Format("{0:#,###}", total);
            else
                lblTotal.Text = "0";
        }

        private void UpdateSubtotal(Bill bill)
        {
            double subtotal = bill.SumOfItemPrice;
            lblSubtotal.Text = String.Format("{0:#,###}", subtotal);
            Subtotal = subtotal;
        }

        private void UpdateBillPromotion()
        {
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                Bill bill = uow.GetObjectByKey<Bill>(BillId);
                BillPromotion billPromotion;

                //If have not any BillPromotion record
                if (bill.BillPromotions == null || bill.BillPromotions.Count == 0)
                {
                    //Create new BillPromotion
                    bill.PromotionCalculationType = Utility.Constant.CALCULATION_TYPE_ON_ITEMS;

                    double promotionInNumber = bill.BillItems.Where(r => r.PromotionInNumber > 0).Sum(r => r.PromotionInNumber);

                    billPromotion = new BillPromotion(uow)
                    {
                        BillId = bill,
                        PromotionInNumber = promotionInNumber
                    };

                    bill.SumOfPromotion = promotionInNumber;

                    uow.CommitChanges();
                }

                if (bill.SumOfPromotion != 0)
                {
                    lblPromotionTotal.Text = String.Format("{0:#,###}", bill.SumOfPromotion);
                }
                else
                {
                    lblPromotionTotal.Text = "0";
                }

                BillPromotionInNumber = bill.SumOfPromotion;
            }
        }

        private void UpdateBillTax(Bill bill)
        {
            double taxTotal = bill.SumOfTax;
            if (taxTotal != 0)
                lblTaxTotal.Text = String.Format("{0:#,###}", taxTotal);
            else
                lblTaxTotal.Text = "0";
            BillTaxInNumber = taxTotal;
        }

        private void ClearBillPromotionValidationState()
        {
            if (spinPromotionOnBillByAmount.Number < 0)
                spinPromotionOnBillByAmount.Number = 0;
            if (spinPromotionOnBillByPercentage.Number < 0)
                spinPromotionOnBillByPercentage.Number = 0;
            spinPromotionOnBillByAmount.IsValid = true;
            spinPromotionOnBillByPercentage.IsValid = true;
        }

        private void BillPromotionInfo_PromotionOnItems_UpdateGUI()
        {
            ClearBillPromotionValidationState();
            Bill bill = session.GetObjectByKey<Bill>(BillId);
            radPromotionOnItems.Checked = true;
            panelPromotionOnItems.Visible = true;
            panelPromotionOnBill.Visible = false;
            //Display data to GUI
            //Get promotion total
            double promotionInNumber = 0;
            if (bill.BillPromotions != null && bill.BillPromotions.Count > 0)
            {
                promotionInNumber = bill.BillPromotions.FirstOrDefault().PromotionInNumber;
            }
            if (promotionInNumber != 0)
                panelPromotionOnItems_lblPromotionTotal.Text = String.Format("{0:#,###}", promotionInNumber);
            else
                panelPromotionOnItems_lblPromotionTotal.Text = "0";
        }

        private void BillPromotionInfo_PromotionOnBillByAmount_UpdateGUI()
        {
            ClearBillPromotionValidationState();
            Bill bill = session.GetObjectByKey<Bill>(BillId);

            radPromotionOnItems.Checked = false;
            radPromotionOnBill.Checked = true;

            radPromotionOnBillByPercentage.Checked = false;
            radPromotionOnBillByAmount.Checked = true;

            spinPromotionOnBillByPercentage.ReadOnly = true;
            panelPromotionOnItems.Visible = false;
            panelPromotionOnBill.Visible = true;
            //Display data to GUI
            //Get promotion total
            double promotionInNumber = 0;
            if (bill.BillPromotions != null && bill.BillPromotions.Count > 0)
            {
                promotionInNumber = bill.BillPromotions.FirstOrDefault().PromotionInNumber;
            }
            spinPromotionOnBillByAmount.Number = (decimal)promotionInNumber;

            double subTotal = bill.BillItems.Sum(r => r.TotalPrice);
            panelPromotionOnBill_lblSubtotal.Text = String.Format("{0:#,###}", subTotal);

            hfSubtotal["subtotal"] = subTotal;
        }

        private void BillPromotionInfo_PromotionOnBillByPercentage_UpdateGUI()
        {
            ClearBillPromotionValidationState();
            Bill bill = session.GetObjectByKey<Bill>(BillId);

            radPromotionOnItems.Checked = false;
            radPromotionOnBill.Checked = true;

            radPromotionOnBillByAmount.Checked = false;
            radPromotionOnBillByPercentage.Checked = true;

            spinPromotionOnBillByAmount.ReadOnly = true;
            panelPromotionOnItems.Visible = false;
            panelPromotionOnBill.Visible = true;
            //Display data to GUI
            //Get promotion total
            double promotionInPercentage = 0;
            double promotionInNumber = 0;
            if (bill.BillPromotions != null && bill.BillPromotions.Count > 0)
            {
                BillPromotion billPromotion = bill.BillPromotions.FirstOrDefault();
                promotionInPercentage = billPromotion.PromotionInPercentage;
                promotionInNumber = billPromotion.PromotionInNumber;
            }
            spinPromotionOnBillByPercentage.Number = (decimal)promotionInPercentage;
            spinPromotionOnBillByAmount.Number = (decimal)promotionInNumber;

            double subTotal = bill.BillItems.Sum(r => r.TotalPrice);
            panelPromotionOnBill_lblSubtotal.Text = String.Format("{0:#,###}", subTotal);

            hfSubtotal["subtotal"] = subTotal;
        }

        private void BillPromotionInfo_PromotionOnItems_UpdateData()
        {
            char promotionCalculationType = Utility.Constant.CALCULATION_TYPE_ON_ITEMS;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                Bill bill = uow.GetObjectByKey<Bill>(BillId);
                BillPromotion billPromotion = bill.BillPromotions.FirstOrDefault();

                bill.PromotionCalculationType = promotionCalculationType;

                double promotionInNumber =
                    bill.BillItems.Where(r => r.PromotionInNumber > 0).Sum(r => r.PromotionInNumber);

                billPromotion.PromotionInNumber = promotionInNumber;
                billPromotion.PromotionInPercentage = 0;

                bill.SumOfPromotion = promotionInNumber;

                BillBOBase.UpdateSumOfTax(uow, bill);

                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;

                uow.CommitChanges();
            }
        }

        private void BillPromotionInfo_PromotionOnBillByAmount_UpdateData()
        {
            char promotionCalculationType = Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_AMOUNT;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                Bill bill = uow.GetObjectByKey<Bill>(BillId);
                BillPromotion billPromotion = bill.BillPromotions.FirstOrDefault();

                bill.PromotionCalculationType = promotionCalculationType;

                billPromotion.PromotionInNumber = (double)spinPromotionOnBillByAmount.Number;
                billPromotion.PromotionInPercentage = 0;

                bill.SumOfPromotion = billPromotion.PromotionInNumber;

                BillBOBase.UpdateSumOfTax(uow, bill);

                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;

                uow.CommitChanges();
            }
        }

        private void BillPromotionInfo_PromotionOnBillByPercentage_UpdateData()
        {
            char promotionCalculationType = Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                Bill bill = uow.GetObjectByKey<Bill>(BillId);
                BillPromotion billPromotion = bill.BillPromotions.FirstOrDefault();

                bill.PromotionCalculationType = promotionCalculationType;

                billPromotion.PromotionInPercentage = (double)spinPromotionOnBillByPercentage.Number;

                double subTotal = bill.BillItems.Sum(r => r.TotalPrice);
                billPromotion.PromotionInNumber = (subTotal * billPromotion.PromotionInPercentage) / 100;

                bill.SumOfPromotion = billPromotion.PromotionInNumber;

                BillBOBase.UpdateSumOfTax(uow, bill);

                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;

                uow.CommitChanges();
            }
        }

        protected void panelBillPromotionInfo_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string command = e.Parameter;
            switch (command)
            {
                case "Edit":
                    //Load bill promotion data
                    //Get bill
                    Bill bill = session.GetObjectByKey<Bill>(BillId);
                    if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                    {
                        //Update GUI
                        BillPromotionInfo_PromotionOnItems_UpdateGUI();
                    }
                    else if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_AMOUNT))
                    {
                        //Update GUI
                        BillPromotionInfo_PromotionOnBillByAmount_UpdateGUI();
                    }
                    else if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
                    {
                        //Update GUI
                        BillPromotionInfo_PromotionOnBillByPercentage_UpdateGUI();
                    }
                    break;
                case "Update":
                    if (radPromotionOnItems.Checked)
                    {
                        //Update BillPromotion data
                        BillPromotionInfo_PromotionOnItems_UpdateData();
                        //Update GUI
                        BillPromotionInfo_PromotionOnItems_UpdateGUI();
                    }
                    else if (radPromotionOnBill.Checked && radPromotionOnBillByAmount.Checked)
                    {
                        //Update BillPromotion data
                        BillPromotionInfo_PromotionOnBillByAmount_UpdateData();
                        //Update GUI
                        BillPromotionInfo_PromotionOnBillByAmount_UpdateGUI();
                    }
                    else if (radPromotionOnBill.Checked && radPromotionOnBillByPercentage.Checked)
                    {
                        //Update BillPromotion data
                        BillPromotionInfo_PromotionOnBillByPercentage_UpdateData();
                        //Update GUI
                        BillPromotionInfo_PromotionOnBillByPercentage_UpdateGUI();
                    }
                    panelBillPromotionInfo.JSProperties["cpEvent"] = "PromotionInfoUpdated";
                    break;
                case "UpdatePromotionOnBillByPercentage":
                    //Update BillPromotion data
                    BillPromotionInfo_PromotionOnBillByPercentage_UpdateData();
                    //Update GUI
                    BillPromotionInfo_PromotionOnBillByPercentage_UpdateGUI();

                    panelBillPromotionInfo.JSProperties["cpEvent"] = "PromotionInfoUpdated";
                    break;
                case "UpdatePromotionOnBillByAmount":
                    //Update BillPromotion data
                    BillPromotionInfo_PromotionOnBillByAmount_UpdateData();
                    //Update GUI
                    BillPromotionInfo_PromotionOnBillByAmount_UpdateGUI();

                    panelBillPromotionInfo.JSProperties["cpEvent"] = "PromotionInfoUpdated";
                    break;
                default:
                    break;
            }
        }

        protected void panelBillTotalSummary_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            UpdateTotalSummary();
        }

        private void ClearBillTaxValidationState()
        {
            comboVAT.IsValid = true;
        }

        private void BillTaxInfo_TaxOnItems_UpdateGUI()
        {
            ClearBillTaxValidationState();


            radBillTaxInfoOnBill.Checked = false;
            radBillTaxInfoOnItems.Checked = true;

            panelBillTaxInfoOnBill.ClientVisible = false;
            panelBillTaxInfoOnItems.ClientVisible = true;

            Bill bill = session.GetObjectByKey<Bill>(BillId, true);

            double taxInNumber = bill.SumOfTax;

            if (taxInNumber != 0)
                panelBillTaxInfoOnItems_lblTaxAmount.Text = String.Format("{0:#,###}", taxInNumber);
            else
                panelBillTaxInfoOnItems_lblTaxAmount.Text = "0";

            var distinctTaxPercentageList = BillBOBase.GetDistinctTaxPercentageList(bill.BillId);
            if (distinctTaxPercentageList == null
                    || distinctTaxPercentageList.Count() != 1)
                lblVATOnBillPercentage.Text = "N/A";
            else
            {
                int countBillItems = 
                    bill.BillItems.Count(r => r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);
                if (countBillItems != distinctTaxPercentageList.First().Count)
                {
                    lblVATOnBillPercentage.Text = "N/A";
                }
                else
                {
                    lblVATOnBillPercentage.Text = String.Format("{0:#,###}", distinctTaxPercentageList.First().Percentage);
                }
            }
        }

        private void BillTaxInfo_TaxOnBill_UpdateGUI()
        {
            ClearBillTaxValidationState();
            Bill bill = session.GetObjectByKey<Bill>(BillId);

            radBillTaxInfoOnItems.Checked = false;
            radBillTaxInfoOnBill.Checked = true;

            panelBillTaxInfoOnItems.ClientVisible = false;
            panelBillTaxInfoOnBill.ClientVisible = true;

            double subtotalAfterPromotion = bill.SumOfItemPrice - bill.SumOfPromotion;
            if (subtotalAfterPromotion != 0)
                lblSubtotalAfterPromotion.Text = String.Format("{0:#,###}", subtotalAfterPromotion);
            else
                lblSubtotalAfterPromotion.Text = "0";

            BillTax billTax = bill.BillTaxs.FirstOrDefault();
            if (billTax == null)
            {
                billTax = new BillTax(session)
                {
                    BillId = bill,
                    TaxInNumber = 0,
                    TaxInPercentage = 0
                };
                billTax.Save();
            }
            if (billTax.TaxId != null)
            {
                comboVAT.Value = billTax.TaxId.TaxId;
                comboVAT.DataBindItems();

                double taxTotal = subtotalAfterPromotion * billTax.TaxId.Percentage / 100;
                if (taxTotal != 0)
                    panelBillTaxInfoOnBill_lblVATAmount.Text = String.Format("{0:#,###}", taxTotal);
                else
                    panelBillTaxInfoOnBill_lblVATAmount.Text = "0";
            }
            else
            {
                comboVAT.SelectedIndex = -1;
                panelBillTaxInfoOnBill_lblVATAmount.Text = "0";
            }
        }

        private void BillTaxInfo_TaxOnItems_UpdateData()
        {
            char taxCalculationType = Utility.Constant.CALCULATION_TYPE_ON_ITEMS;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                Bill bill = uow.GetObjectByKey<Bill>(BillId);
                bill.TaxCalculationType = taxCalculationType;

                double taxInNumber = 0;
                var billItemTaxs = bill.BillItems.SelectMany(r => r.BillItemTaxs);
                if (billItemTaxs != null && billItemTaxs.Count() > 0)
                {
                    taxInNumber = billItemTaxs.Sum(r =>
                        (r.BillItemId.TotalPrice - r.BillItemId.PromotionInNumber) * r.TaxInPercentage / 100);
                }

                bill.SumOfTax = taxInNumber;

                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;

                uow.CommitChanges();
            }
        }

        private void BillTaxInfo_TaxOnBill_UpdateData()
        {
            char taxCalculationType = Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                Bill bill = uow.GetObjectByKey<Bill>(BillId);
                bill.TaxCalculationType = taxCalculationType;

                BillTax billTax = bill.BillTaxs.FirstOrDefault();
                if (billTax == null)
                {
                    billTax = new BillTax(uow)
                    {
                        BillId = bill,
                        TaxInNumber = 0,
                        TaxInPercentage = 0
                    };
                }

                //Get selected VAT
                var val = comboVAT.Value;
                if (val != null)
                {
                    Tax tax = uow.GetObjectByKey<Tax>(val);

                    billTax.TaxId = tax;
                    billTax.TaxInPercentage = tax.Percentage;
                    billTax.TaxInNumber = tax.Amount;

                    double subtotalAfterPromotion = bill.SumOfItemPrice - bill.SumOfPromotion;
                    bill.SumOfTax = subtotalAfterPromotion * tax.Percentage / 100;

                    bill.Total = subtotalAfterPromotion + bill.SumOfTax;
                }
                else
                {
                    bill.SumOfTax = 0;
                    bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion;
                }

                uow.CommitChanges();
            }
        }

        protected void panelBillTaxInfo_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string command = e.Parameter;
            switch (command)
            {
                case "Edit":
                    //Get bill
                    Bill bill = session.GetObjectByKey<Bill>(BillId);
                    if (bill.TaxCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                    {
                        BillTaxInfo_TaxOnItems_UpdateGUI();
                    }
                    else if (bill.TaxCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
                    {
                        BillTaxInfo_TaxOnBill_UpdateGUI();
                    }
                    break;
                case "Update":
                    if (radBillTaxInfoOnItems.Checked)
                    {
                        BillTaxInfo_TaxOnItems_UpdateData();
                        BillTaxInfo_TaxOnItems_UpdateGUI();
                    }
                    else if (radBillTaxInfoOnBill.Checked)
                    {
                        BillTaxInfo_TaxOnBill_UpdateData();
                        BillTaxInfo_TaxOnBill_UpdateGUI();
                    }
                    panelBillTaxInfo.JSProperties["cpEvent"] = "TaxInfoUpdated";
                    break;
                default:
                    break;
            }
        }
    }
}