using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAS.BO.Nomenclature.Items;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Invoice;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL;

namespace NAS.BO.Invoice
{
    public abstract class BillBOBase
    {
        public abstract Bill GetBillById(Session session, Guid billId);

        public abstract bool Delete(Session session, Guid billId);

        public abstract Bill InitTemporary(Session session, BillTypeEnum billType);

        public abstract bool Save(
            Session session,
            Guid billId,
            string billCode,
            DateTime issuedDate,
            Organization sourceOrganizationBill,
            Person targetOrganizationBill);

        public virtual void CreateBillItem(
            DevExpress.Xpo.Session session,
            Guid billId,
            Guid itemId,
            Guid unitId,
            double quantity,
            double price,
            double promotionInPercentage,
            string comment)
        {
            try
            {
                NAS.DAL.Invoice.Bill bill =
                    session.GetObjectByKey<NAS.DAL.Invoice.Bill>(billId);
                if (bill == null)
                    throw new Exception("Could not found specific bill");
                //Get ItemUnit
                Item item = session.GetObjectByKey<Item>(itemId);
                Unit unit = session.GetObjectByKey<Unit>(unitId);

                ItemUnit itemUnit = session.FindObject<ItemUnit>(
                    CriteriaOperator.And(
                        new BinaryOperator("ItemId", item),
                        new BinaryOperator("UnitId", unit),
                        CriteriaOperator.Or(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT)
                        )
                    ));
                if (itemUnit == null)
                    throw new Exception("Could not found item unit");

                BillItem billItem = new BillItem(session)
                {
                    BillId = bill,
                    Comment = comment,
                    //Currency
                    ItemUnitId = itemUnit,
                    Price = price,
                    PromotionInNumber = promotionInPercentage != -1
                        ? (price * quantity * promotionInPercentage) / 100 : 0,
                    PromotionInPercentage = promotionInPercentage,
                    Quantity = quantity,
                    RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                    TotalPrice = price * quantity
                };
                billItem.Save();

                //update SumOfItemPrice
                bill.SumOfItemPrice = bill.BillItems.Sum(r => r.TotalPrice);
                bill.Save();

                if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                {
                    UpdatePromotionOnItems(session, billId);
                }

                //Insert into BillItemTax...
                //Get VAT TaxType of Item
                ItemBO itemBO = new ItemBO();
                ItemTax itemTax = itemBO.GetCurrentVATOfItem(session, itemId);

                if (itemTax != null)
                {
                    BillItemTax billItemTax = new BillItemTax(session)
                    {
                        BillItemId = billItem,
                        ItemTaxId = itemTax,
                        TaxInNumber = itemTax.TaxId.Amount,
                        TaxInPercentage = itemTax.TaxId.Percentage
                    };
                    billItemTax.Save();
                }

                UpdateSumOfTax(session, bill);

                #region Update bill Total
                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;
                bill.Save();
                #endregion Update bill Total

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void UpdateBillItem(
            DevExpress.Xpo.Session session,
            Guid billItemId,
            Guid itemId,
            Guid unitId,
            double quantity,
            double price,
            double promotionInPercentage,
            string comment)
        {
            try
            {
                NAS.DAL.Invoice.BillItem billItem =
                    session.GetObjectByKey<NAS.DAL.Invoice.BillItem>(billItemId);

                if (billItem == null)
                    throw new Exception("Could not found specific bill item");
                //Get ItemUnit
                Item item = session.GetObjectByKey<Item>(itemId);
                Unit unit = session.GetObjectByKey<Unit>(unitId);

                ItemUnit itemUnit = session.FindObject<ItemUnit>(
                    CriteriaOperator.And(
                        new BinaryOperator("ItemId", item),
                        new BinaryOperator("UnitId", unit),
                        CriteriaOperator.Or(
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE),
                            new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT)
                        )
                    ));

                if (itemUnit == null)
                    throw new Exception("Could not found item unit");

                billItem.Comment = comment;
                //Currency
                billItem.ItemUnitId = itemUnit;
                billItem.Price = price;
                billItem.PromotionInNumber = promotionInPercentage != -1 ?
                    (price * quantity * promotionInPercentage) / 100 : 0;
                billItem.PromotionInPercentage = promotionInPercentage;
                billItem.Quantity = quantity;
                billItem.TotalPrice = price * quantity;

                session.Delete(billItem.BillItemTaxs);

                billItem.Save();

                //update SumOfItemPrice
                Bill bill = billItem.BillId;
                bill.SumOfItemPrice = bill.BillItems.Sum(r => r.TotalPrice);
                bill.Save();

                if (billItem.BillId.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                {
                    UpdatePromotionOnItems(session, billItem.BillId.BillId);
                }

                //Insert into BillItemTax...
                //Get VAT TaxType of Item
                ItemBO itemBO = new ItemBO();
                ItemTax itemTax = itemBO.GetCurrentVATOfItem(session, itemId);

                if (itemTax != null)
                {
                    BillItemTax billItemTax = new BillItemTax(session)
                    {
                        BillItemId = billItem,
                        ItemTaxId = itemTax,
                        TaxInNumber = itemTax.TaxId.Amount,
                        TaxInPercentage = itemTax.TaxId.Percentage
                    };
                    billItemTax.Save();
                }

                UpdateSumOfTax(session, bill);

                #region Update bill Total
                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;
                bill.Save();
                #endregion Update bill Total

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void DeleteBillItem(DevExpress.Xpo.Session session, Guid billItemId)
        {
            try
            {
                NAS.DAL.Invoice.BillItem billItem =
                    session.GetObjectByKey<NAS.DAL.Invoice.BillItem>(billItemId);
                Bill bill = billItem.BillId;
                if (billItem == null)
                    return;
                billItem.Delete();

                //Update totalprice
                bill.SumOfItemPrice = bill.BillItems.Sum(r => r.TotalPrice);
                bill.Save();

                if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                {
                    UpdatePromotionOnItems(session, bill.BillId);
                }

                UpdateSumOfTax(session, bill);

                #region Update bill Total
                bill.Total = bill.SumOfItemPrice - bill.SumOfPromotion + bill.SumOfTax;
                bill.Save();
                #endregion Update bill Total
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Bill Clone(Session session, Guid billId)
        {
            try
            {
                Bill source = session.GetObjectByKey<Bill>(billId);
                if (source == null)
                    throw new Exception("Source bill null");
                Bill ret = InitTemporary(session, (BillTypeEnum)source.BillType);
                ret.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                
                int sequence = 0;
                string sequenceStr = String.Empty;
                string code = String.Format("Copy_{0}{1}", source.Code, sequenceStr);
                while (NAS.DAL.Util.IsExistXpoObject<Bill>(
                    session,
                    "Code",
                    code,
                    Utility.Constant.ROWSTATUS_ACTIVE,
                    Utility.Constant.ROWSTATUS_INACTIVE,
                    Utility.Constant.ROWSTATUS_BOOKED_ENTRY))
                {
                    //Generate new code
                    sequence++;
                    sequenceStr = String.Format("({0})", sequence);
                    code = String.Format("Copy_{0}{1}", source.Code, sequenceStr);
                }
                ret.Code = code;
                session.FlushChanges();
                
                CloneBillData(session, billId, ref ret);
                CloneBillItemData(session, billId, ref ret);
                CloneBillActorData(session, billId, ref ret);
                CloneBillPromotionData(session, billId, ref ret);
                CloneBillPlaningInventoryTransactionData(session, billId, ref ret);
                CloneBillPlaningAccountingTransactionData(session, billId, ref ret);
                CloneBillTransaction(session, billId, ref ret);
                ret.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                ret.Save();
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected abstract void CloneBillData(Session session, Guid billId, ref Bill ret);

        protected abstract void CloneBillItemData(Session session, Guid billId, ref Bill ret);

        protected abstract void CloneBillActorData(Session session, Guid billId, ref Bill ret);

        protected abstract void CloneBillPromotionData(Session session, Guid billId, ref Bill ret);

        protected abstract void CloneBillPlaningAccountingTransactionData(Session session, Guid billId, ref Bill ret);

        protected abstract void CloneBillPlaningInventoryTransactionData(Session session, Guid billId, ref Bill ret);

        protected abstract void CloneBillTransaction(Session session, Guid billId, ref Bill ret);

        public void UpdatePromotionOnItems(Session session, Guid billId)
        {
            try
            {
                Bill bill = session.GetObjectByKey<Bill>(billId);

                if (!bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                    return;

                BillPromotion billPromotion;

                //If have not any BillPromotion record
                if (bill.BillPromotions == null || bill.BillPromotions.Count == 0)
                {
                    //Create new BillPromotion
                    bill.PromotionCalculationType = Utility.Constant.CALCULATION_TYPE_ON_ITEMS;
                    bill.Save();

                    billPromotion = new BillPromotion(session)
                    {
                        BillId = bill
                    };
                    billPromotion.Save();
                }
                else
                {
                    billPromotion = bill.BillPromotions.FirstOrDefault();
                }
                //update promotion for the bill
                double promotionInNumber =
                    bill.BillItems.Where(r => r.PromotionInNumber > 0).Sum(r => r.PromotionInNumber);
                billPromotion.PromotionInNumber = promotionInNumber;
                billPromotion.Save();

                bill.SumOfPromotion = promotionInNumber;
                bill.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateSumOfTax(Session session, NAS.DAL.Invoice.Bill bill)
        {
            #region Update SumOfTax of the bill
            if (bill.TaxCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
            {
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
                if (billTax.TaxId == null)
                {
                    bill.SumOfTax = 0;
                }
                else
                {
                    bill.SumOfTax = (bill.SumOfItemPrice - bill.SumOfPromotion)
                        * billTax.TaxId.Percentage / 100;
                }
            }
            else if (bill.TaxCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
            {
                if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_ITEMS))
                {
                    var billItemTaxs = bill.BillItems.SelectMany(r => r.BillItemTaxs);
                    double taxInNumber = billItemTaxs.Sum(r =>
                        (r.BillItemId.TotalPrice - r.BillItemId.PromotionInNumber) * r.TaxInPercentage / 100);
                    bill.SumOfTax = taxInNumber;
                    bill.Save();
                }
                else if (bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_AMOUNT)
                    || bill.PromotionCalculationType.Equals(Utility.Constant.CALCULATION_TYPE_ON_BILL_BY_PERCENTAGE))
                {
                    var billItemTaxs = bill.BillItems.SelectMany(r => r.BillItemTaxs);
                    double taxInNumber = billItemTaxs.Sum(r =>
                        r.BillItemId.TotalPrice * r.TaxInPercentage / 100);
                    bill.SumOfTax = taxInNumber;
                    bill.Save();
                }
            }
            session.FlushChanges();
            #endregion Update SumOfTax of the bill
        }

        public static IEnumerable<BillItemTaxPercentageCounter> GetDistinctTaxPercentageList(Guid billId)
        {
            IEnumerable<BillItemTaxPercentageCounter> ret = null;
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                Bill bill = session.GetObjectByKey<Bill>(billId);
                ret = bill.BillItems.SelectMany(r => r.BillItemTaxs).Select(r => r.TaxInPercentage)
                    .GroupBy(r => r)
                    .Select(r => new BillItemTaxPercentageCounter(r.Key, r.Count()));
                ret = ret.OrderByDescending(r => r.Count).ThenByDescending(r => r.Percentage);
                return ret.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public static bool IsMostAppearTaxPercentage(Guid billItemId)
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //Get BillItem
                BillItem billItem = session.GetObjectByKey<BillItem>(billItemId);
                if (billItem == null)
                    throw new Exception("Could not found BillItem");

                if (billItem.BillId == null)
                    return false;

                BillItemTaxPercentageCounter percentageCounter =
                    GetDistinctTaxPercentageList(billItem.BillId.BillId).FirstOrDefault();

                if (percentageCounter == null)
                    return true;

                if (billItem.BillItemTaxs == null || billItem.BillItemTaxs.Count == 0)
                    return false;

                if (billItem.BillItemTaxs.First().TaxInPercentage != percentageCounter.Percentage)
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }

        public class BillItemTaxPercentageCounter
        {
            public BillItemTaxPercentageCounter(double percentage, int count)
            {
                Percentage = percentage;
                Count = count;
            }
            public double Percentage { get; set; }
            public int Count { get; set; }
        }

    }
}
