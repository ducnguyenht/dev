using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL.Accounting.Journal;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Accounting.AccountChart;
using NAS.BO.ETL.Bill.TempData;
using NAS.DAL.Accounting.Currency;
using NAS.DAL.BI.Accounting;
using NAS.DAL.Invoice;
using NAS.DAL;
using NAS.DAL.System.ShareDim;
using Utility;
using NAS.DAL.BI.Item;
using NAS.DAL.BI.Actor;
using NAS.DAL.BI.BillDim;
using DevExpress.Data.Filtering;

namespace NAS.BO.ETL.Accounting
{
    public class ETLFinancialRevenueByItemBO
    {
        public void CreateFinancialRevenueByItem(Session session,
                                                 ETL_Bill bill)
        {
            try
            {
                foreach (ETL_BillItem billItem in bill.BillItemList)
                {
                    CreateFinancialRevenueByItem(session, bill, billItem);
                }
            }
            catch (Exception)
            {
                throw;
                return;
            }
        }

        public void CreateFinancialRevenueByItem(Session session,
                                                 ETL_Bill bill,
                                                 ETL_BillItem billItem)
        {
            SalesInvoice invoice = session.GetObjectByKey<SalesInvoice>(bill.BillId);
            if (invoice == null) return;
            #region customerOrg
            Guid customerOrgId = bill.CustomerOrgId;            
            #endregion 
            #region ownerOrg
            Guid ownerOrgId = bill.OwnerOrgId;
            #endregion
            #region ItemUnit
            Item item = billItem.item;
            if (item == null) return;
            Unit unit = billItem.unit;
            if (unit == null) return;
            #endregion
            #region AssetDim
            string assetDim = "VND";                
            #endregion
            #region BillId
            Guid billId = bill.BillId;
            #endregion
            CreateFinancialRevenueByItem(session, billId, customerOrgId, ownerOrgId, item, unit, assetDim);
        }

        public void CreateFinancialRevenueByItem(Session session,
                                                 Guid _BillId,
                                                 Guid _CuntomerOrgId,
                                                 Guid _OwnerOrgId,
                                                 Item _Item,
                                                 Unit _unit,   
                                                 string _AssetDim)
        {            
            try{            
                SalesInvoice invoice = session.GetObjectByKey<SalesInvoice>(_BillId);
                if (invoice == null) return;
                DateTime issueDate = invoice.IssuedDate;
                int Day = issueDate.Day;
                int Month = issueDate.Month;
                int Year = issueDate.Year;
                CreateFinancialRevenueByItem(session, _BillId, _CuntomerOrgId, _OwnerOrgId, _Item, _unit, _AssetDim);
            }catch(Exception)
            {
                return;
            }
        }
        public void CreateFinancialRevenueByItem(Session session,
                                                 Guid _BillId,
                                                 Guid _CustomerOrgId,
                                                 Guid _OwnerOrgId,
                                                 Item _Item,
                                                 Unit _Unit,
                                                 int Day,
                                                 int Month,
                                                 int Year,
                                                 string _AssetDim)
        {
            FinancialRevenueByItem_Fact result = new FinancialRevenueByItem_Fact(session);
            try
            {
                SalesInvoice invoice = session.GetObjectByKey<SalesInvoice>(_BillId);
                if (!Util.IsExistXpoObject<InvoiceDim>(session, "RefId", _BillId))
                {
                    InvoiceDim invoiceDim = new InvoiceDim(session);
                    invoiceDim.RefId = _BillId;
                    invoiceDim.Code = invoice.Code;
                    invoiceDim.Name = "";
                    invoiceDim.Description = "";
                    invoiceDim.IssueDate = invoice.IssuedDate;
                    invoiceDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    invoiceDim.Save();
                }
                if (!Util.IsExistXpoObject<DayDim>(session, "Name", Day.ToString()))
                {
                    DayDim dayDim = new DayDim(session);
                    dayDim.Description = Day.ToString();
                    dayDim.Name = Day.ToString();
                    dayDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    dayDim.Save();
                }
                if (!Util.IsExistXpoObject<MonthDim>(session, "Name", Month.ToString()))
                {
                    MonthDim MonthDim = new MonthDim(session);
                    MonthDim.Description = Month.ToString();
                    MonthDim.Name = Month.ToString();
                    MonthDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    MonthDim.Save();
                }
                if (!Util.IsExistXpoObject<YearDim>(session, "Name", Year.ToString()))
                {
                    YearDim YearDim = new YearDim(session);
                    YearDim.Description = Year.ToString();
                    YearDim.Name = Year.ToString();
                    YearDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    YearDim.Save();
                }
                if (!Util.IsExistXpoObject<FinancialAssetDim>(session, "Name", _AssetDim))
                {
                    FinancialAssetDim assetDim = new FinancialAssetDim(session);
                    assetDim.Name = _AssetDim;
                    assetDim.Description = _AssetDim;
                    assetDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    assetDim.Save();
                }

                if (!Util.IsExistXpoObject<ItemDim>(session, "RefId", _Item.ItemId))
                {
                    ItemDim itemDim = new ItemDim(session);
                    itemDim.RefId = _Item.ItemId;
                    itemDim.Code = _Item.Code;
                    itemDim.Name = _Item.Name;
                    itemDim.Description = _Item.Description;
                    itemDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    itemDim.Save();
                }
                if (!Util.IsExistXpoObject<UnitDim>(session, "RefId", _Unit.UnitId))
                {
                    UnitDim unitDim = new UnitDim(session);
                    unitDim.RefId = _Unit.UnitId;
                    unitDim.Code = _Unit.Code;
                    unitDim.Name = _Unit.Name;
                    unitDim.Description = _Unit.Description;
                    unitDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                    unitDim.Save();
                }
                if (!Util.IsExistXpoObject<CustomerOrgDim>(session, "RefId",_CustomerOrgId))
                {
                    Organization customerOrg = session.GetObjectByKey<Organization>(_CustomerOrgId);
                    if (customerOrg != null)
                    {
                        CustomerOrgDim customerOrgDim = new CustomerOrgDim(session);
                        customerOrgDim.RefId = _CustomerOrgId;
                        customerOrgDim.Code = customerOrg.Code;
                        customerOrgDim.Name = customerOrg.Name;
                        customerOrgDim.Description = customerOrg.Description;
                        customerOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                        customerOrgDim.Save();
                    }
                }
                if (!Util.IsExistXpoObject<OwnerOrgDim>(session, "RefId", _OwnerOrgId))
                {
                    Organization ownerOrg = session.GetObjectByKey<Organization>(_OwnerOrgId);
                    if (ownerOrg != null)
                    {
                        OwnerOrgDim ownerOrgDim = new OwnerOrgDim(session);
                        ownerOrgDim.RefId = _OwnerOrgId;
                        ownerOrgDim.Code = ownerOrg.Code;
                        ownerOrgDim.Name = ownerOrg.Name;
                        ownerOrgDim.Description = ownerOrg.Description;
                        ownerOrgDim.RowStatus = Constant.ROWSTATUS_ACTIVE;
                        ownerOrgDim.Save();
                    }
                }
                Util util = new Util();

                result.InvoiceDimId = util.GetXpoObjectByFieldName<InvoiceDim, Guid>(session, "RefId", _BillId, DevExpress.Data.Filtering.BinaryOperatorType.Equal);
                result.FinancialAssetDimId = Util.getXpoObjectByName<FinancialAssetDim>(session, _AssetDim);
                result.DayDimId = Util.getXpoObjectByName<DayDim>(session, Day.ToString());
                result.MonthDimId = Util.getXpoObjectByName<MonthDim>(session, Month.ToString());
                result.YearDimId = Util.getXpoObjectByName<YearDim>(session, Year.ToString());
                result.ItemDimId = util.GetXpoObjectByFieldName<ItemDim, string>(session, "Code", _Item.Code, DevExpress.Data.Filtering.BinaryOperatorType.Equal);
                result.UnitDimId = util.GetXpoObjectByFieldName<UnitDim, string>(session, "Code", _Unit.Code, DevExpress.Data.Filtering.BinaryOperatorType.Equal);
                result.CustomerOrgDimId = util.GetXpoObjectByFieldName<CustomerOrgDim, Guid>(session, "RefId", _CustomerOrgId, DevExpress.Data.Filtering.BinaryOperatorType.Equal);
                result.OwnerOrgDimId = util.GetXpoObjectByFieldName<OwnerOrgDim, Guid>(session, "RefId", _OwnerOrgId, DevExpress.Data.Filtering.BinaryOperatorType.Equal);
                result.Save();

            }
            catch (Exception)
            {                
            }
            
        }

        public void ClearFinancialRevenueByItem(Session session, Guid _InvoiceId)
        {
            CriteriaOperator criteria_RowStatus = new BinaryOperator("RowStatus", Constant.ROWSTATUS_ACTIVE, BinaryOperatorType.GreaterOrEqual);
            CriteriaOperator criteria_Invoice = new BinaryOperator("InvoiceDim.RefId", _InvoiceId, BinaryOperatorType.Equal);
            CriteriaOperator criteria = CriteriaOperator.And(criteria_RowStatus, criteria_Invoice);
            XPCollection<FinancialRevenueByItem_Fact> FinancialRevenueByItem_FactCol = new XPCollection<FinancialRevenueByItem_Fact>(session, criteria);

            foreach (FinancialRevenueByItem_Fact record in FinancialRevenueByItem_FactCol)
            {
                record.RowStatus = Constant.ROWSTATUS_DELETED;
                record.Save();
            }
        }
    }
}
