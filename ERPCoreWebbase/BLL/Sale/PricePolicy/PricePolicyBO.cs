using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using NAS.DAL;
using NAS.DAL.Sales.Price;
using DevExpress.Data.Filtering;
using NAS.DAL.Nomenclature.Organization;
using NAS.DAL.Invoice;
using NAS.DAL.Inventory.Ledger;
using NAS.BO.Accounting.Journal;
using NAS.DAL.Inventory.Journal;
using NAS.DAL.Accounting.Journal;
using Evaluant.Calculator;
using NAS.DAL.Accounting.Tax;
using System.Dynamic;

namespace NAS.BO.Sale.PricePolicy
{
    public class DataGrdSupplierListSelection
    {
        public Guid OrganizationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DataGrdManufacturerListSelection
    {
        public Guid OrganizationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DataGrdItemUnitListSelection
    {
        public Guid ItemUnitId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public short RowStatus { get; set; }
    }

    public class DemoPrice {
        public Guid COGSId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public string InventoryIdName { get; set; }
        public DateTime UpdateDate { get; set; }
        public double COGSPrice { get; set; }
        public double COGSReview { get; set; }
        public string Note { get; set; }
    }

    public class TaxTypeSelection {
        public Guid TaxTypeId { get; set; }
        public double ByValue { get; set; }
        public double ByPercentTage { get; set; }
    }



    // The class derived from DynamicObject. 

    public class DynamicPriceReview : DynamicObject
    {
        // The inner captions.
        private Dictionary<string, string> captions
            = new Dictionary<string, string>();

        public void addCaption(string fieldName, string caption)
        {
            if (!captions.ContainsKey(fieldName.ToLower()))
                captions.Add(fieldName.ToLower(), caption);
            else
                captions[fieldName.ToLower()] = caption;
        }

        public string getCaption(string fieldName)
        {
            return captions[fieldName.ToLower()];
        }

        private Dictionary<string, object> dictionary
            = new Dictionary<string, object>();

        public Dictionary<string, object> Dictionary
        {
            get {
                return dictionary;
            }
        }

        // This property returns the number of elements 
        // in the inner dictionary. 
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        // If you try to get a value of a property  
        // not defined in the class, this method is called. 
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase 
            // so that property names become case-insensitive. 
            string name = binder.Name.ToLower();

            // If the property name is found in a dictionary, 
            // set the result parameter to the property value and return true. 
            // Otherwise, return false. 
            return dictionary.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is 
        // not defined in the class, this method is called. 
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase 
            // so that property names become case-insensitive.
            dictionary[binder.Name.ToLower()] = value;

            // You can always add a value to a dictionary, 
            // so this method always returns true. 
            return true;
        }
    }

    public class PricePolicyBO
    {
        public void Init_DefaultData(Session session)
        {
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Organization table
                if (!Util.isExistXpoObject<PricePolicyType>("Code", "BANSI"))
                {
                    OwnerOrg.Populate();
                    OwnerOrg org = session.FindObject<OwnerOrg>(
                            new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal)
                        );
                    PricePolicyType pricePolicyType = new PricePolicyType(session)
                    {
                        Code = "BANSI",
                        CreateDate = DateTime.Now,
                        Description = "",
                        IssueDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "Chính sách giá bán sỉ",
                        RowStatus = 1,
                        OwnerOrgId = org
                    };
                    pricePolicyType.Save();
                }

                if (!Util.isExistXpoObject<PricePolicyType>("Code", "BANLE"))
                {
                    OwnerOrg.Populate();
                    OwnerOrg org = session.FindObject<OwnerOrg>(
                            new BinaryOperator("Code", Utility.Constant.NAAN_DEFAULT_CODE, BinaryOperatorType.Equal)
                        );
                    PricePolicyType pricePolicyType = new PricePolicyType(session)
                    {
                        Code = "BANLE",
                        CreateDate = DateTime.Now,
                        Description = "",
                        IssueDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "Chính sách giá bán lẻ",
                        RowStatus = 1,
                        OwnerOrgId = org
                    };
                    pricePolicyType.Save();
                }
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

        public NAS.DAL.Sales.Price.PricePolicy addDefaultPricePolicy(Session session)
        {
            try
            {
                session.BeginTransaction();
                //insert default data into PricePolicy table
                NAS.DAL.Sales.Price.PricePolicy pricePolicy = new NAS.DAL.Sales.Price.PricePolicy(session)
                {
                    Code = Guid.NewGuid().ToString(),
                    CreateDate = DateTime.Now,
                    Description = Utility.Constant.NAAN_DEFAULT_NAME,
                    IssueDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    Name = Utility.Constant.NAAN_DEFAULT_NAME,
                    Priority = 0,
                    ValidFrom = DateTime.Now,
                    ValidTo = DateTime.Now,
                    RowStatus = 0
                };
                pricePolicy.Save();
                session.CommitTransaction();
                //session.Dispose();
                return pricePolicy;
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public void SavingPricePolicy(Session session, Guid pricePolicyId, string code, string description,
                string name, int priority,
                DateTime validFrom, DateTime validTo, short rowStatus, Guid pricePolicyTypeId )
        {
            try
            {
                NAS.DAL.Sales.Price.PricePolicy p = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(pricePolicyId);
                if (p == null)
                    throw new Exception("Key is not Exist in PricePolicy");
                NAS.DAL.Sales.Price.PricePolicyType pt = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicyType>(pricePolicyTypeId);
                if (pt == null)
                    throw new Exception("Key is not Exist in PricePolicyType");
                p.Code = code;
                p.Description = description;
                p.Name = name;
                p.Priority = priority;
                p.ValidFrom = validFrom;
                p.ValidTo = validTo;
                p.RowStatus = rowStatus;
                p.PricePolicyTypeId = pt;
                p.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Supplier Condition/Exception

        public void addSupplierConditionToPolicy(Session session, Guid supplierId, Guid pricePolicyId, bool isIncluding) {
            try
            {
                NAS.DAL.Sales.Price.PricePolicy p = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(pricePolicyId);
                if (p == null)
                    throw new Exception("Key is not Exist in PricePolicy");

                NAS.DAL.Nomenclature.Organization.SupplierOrg s = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.SupplierOrg>(supplierId);
                if (s == null)
                    throw new Exception("Key is not Exist in SupplierOrg");

                //CriteriaOperator criteria = CriteriaOperator.And(
                //        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                //        new BinaryOperator("IsIncluding", isIncluding, BinaryOperatorType.Equal),
                //        new BinaryOperator("SupplierOrgId!Key", supplierId, BinaryOperatorType.Greater)
                //    );

                //PricePolicySupplierCondition ppsc = session.FindObject<PricePolicySupplierCondition>(criteria);
                //if (ppsc == null)
                //{
                PricePolicySupplierCondition ppsc = new PricePolicySupplierCondition(session);
                ppsc.CreateDate = DateTime.Now;
                ppsc.IsIncluding = isIncluding;
                ppsc.IssueDate = DateTime.Now;
                ppsc.LastUpdateDate = DateTime.Now;
                ppsc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                ppsc.PricePolicyId = p;
                ppsc.SupplierOrgId = s;
                ppsc.Save();
                //}
                //else {
                //    ppsc.SupplierOrgId = s;
                //    ppsc.IsIncluding = isIncluding;
                //    ppsc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                //    ppsc.Save();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateSupplierConditionInPolicy(Session session, Guid pricePolicyId, List<DataGrdSupplierListSelection> list)
        {
            if (list == null)
                return;

            try
            {
                if (list.Count == 0)
                {
                    session.BeginTransaction();
                    CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );

                    XPCollection<PricePolicySupplierCondition> ppscl = new XPCollection<PricePolicySupplierCondition>(session, criteria);

                    foreach (PricePolicyCondition ppc in ppscl)
                    {
                        ppc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        ppc.Save();
                    }
                    session.CommitTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }

            try {
                session.BeginTransaction();
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                XPCollection<PricePolicySupplierCondition> ppscl = new XPCollection<PricePolicySupplierCondition>(session, criteria);

                foreach (PricePolicyCondition ppc  in ppscl)
                {
                    ppc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    ppc.Save();
                }

                foreach (DataGrdSupplierListSelection d in list) {
                    addSupplierConditionToPolicy(session, d.OrganizationId, pricePolicyId, true);
                }

                session.CommitTransaction();
            }
            catch(Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public void updateSupplierExceptionInPolicy(Session session, Guid pricePolicyId, List<DataGrdSupplierListSelection> list)
        {
            if (list == null)
                return;

            try
            {
                if (list.Count == 0)
                {
                    session.BeginTransaction();
                    CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );

                    XPCollection<PricePolicySupplierCondition> ppscl = new XPCollection<PricePolicySupplierCondition>(session, criteria);

                    foreach (PricePolicyCondition ppc in ppscl)
                    {
                        ppc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        ppc.Save();
                    }
                    session.CommitTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }

            try
            {
                session.BeginTransaction();
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                XPCollection<PricePolicySupplierCondition> ppscl = new XPCollection<PricePolicySupplierCondition>(session, criteria);

                foreach (PricePolicyCondition ppc in ppscl)
                {
                    ppc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    ppc.Save();
                }

                foreach (DataGrdSupplierListSelection d in list)
                {
                    addSupplierConditionToPolicy(session, d.OrganizationId, pricePolicyId, false);
                }

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public List<DataGrdSupplierListSelection> loadSupplierConditionInPolicy(Session session, Guid pricePolicyId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );
                XPCollection<PricePolicySupplierCondition> ppscl = new XPCollection<PricePolicySupplierCondition>(session, criteria);

                List<DataGrdSupplierListSelection> rs = new List<DataGrdSupplierListSelection>();

                foreach (PricePolicySupplierCondition ppsc in ppscl)
                {
                    DataGrdSupplierListSelection d = new DataGrdSupplierListSelection();
                    d.OrganizationId = ppsc.SupplierOrgId.OrganizationId;
                    d.Code = ppsc.SupplierOrgId.Code;
                    d.Name = ppsc.SupplierOrgId.Name;
                    rs.Add(d);
                }
                return rs;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public List<DataGrdSupplierListSelection> loadSupplierExceptionInPolicy(Session session, Guid pricePolicyId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );
                XPCollection<PricePolicySupplierCondition> ppscl = new XPCollection<PricePolicySupplierCondition>(session, criteria);

                List<DataGrdSupplierListSelection> rs = new List<DataGrdSupplierListSelection>();

                foreach (PricePolicySupplierCondition ppsc in ppscl)
                {
                    DataGrdSupplierListSelection d = new DataGrdSupplierListSelection();
                    d.OrganizationId = ppsc.SupplierOrgId.OrganizationId;
                    d.Code = ppsc.SupplierOrgId.Code;
                    d.Name = ppsc.SupplierOrgId.Name;
                    rs.Add(d);
                }
                return rs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Manufacturer Condition/Exception

        public void addManufacturerConditionToPolicy(Session session, Guid manufacturerId, Guid pricePolicyId, bool isIncluding)
        {
            try
            {
                NAS.DAL.Sales.Price.PricePolicy p = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(pricePolicyId);
                if (p == null)
                    throw new Exception("Key is not Exist in PricePolicy");

                NAS.DAL.Nomenclature.Organization.ManufacturerOrg m = session.GetObjectByKey<NAS.DAL.Nomenclature.Organization.ManufacturerOrg>(manufacturerId);
                if (m == null)
                    throw new Exception("Key is not Exist in ManufacturerOrg");

                //CriteriaOperator criteria = CriteriaOperator.And(
                //        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                //        new BinaryOperator("IsIncluding", isIncluding, BinaryOperatorType.Equal),
                //        new BinaryOperator("ManufacturerOrgId!Key", manufacturerId, BinaryOperatorType.Greater)
                //    );

                //PricePolicyManufacturerCondition ppsc = session.FindObject<PricePolicyManufacturerCondition>(criteria);
                //if (ppsc == null)
                //{
                PricePolicyManufacturerCondition ppsc = new PricePolicyManufacturerCondition(session);
                ppsc.CreateDate = DateTime.Now;
                ppsc.IsIncluding = isIncluding;
                ppsc.IssueDate = DateTime.Now;
                ppsc.LastUpdateDate = DateTime.Now;
                ppsc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                ppsc.PricePolicyId = p;
                ppsc.ManufacturerOrgId = m;
                ppsc.Save();
                //}
                //else
                //{
                //    ppsc.ManufacturerOrgId = m;
                //    ppsc.IsIncluding = isIncluding;
                //    ppsc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                //    ppsc.Save();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateManufacturerConditionInPolicy(Session session, Guid pricePolicyId, List<DataGrdManufacturerListSelection> list)
        {
            if (list == null)
                return;

            try
            {
                if (list.Count == 0)
                {
                    session.BeginTransaction();
                    CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );

                    XPCollection<PricePolicyManufacturerCondition> ppmcl = new XPCollection<PricePolicyManufacturerCondition>(session, criteria);

                    foreach (PricePolicyCondition ppmc in ppmcl)
                    {
                        ppmc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        ppmc.Save();
                    }
                    session.CommitTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }

            try
            {
                session.BeginTransaction();
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                XPCollection<PricePolicyManufacturerCondition> ppmcl = new XPCollection<PricePolicyManufacturerCondition>(session, criteria);

                foreach (PricePolicyCondition ppmc in ppmcl)
                {
                    ppmc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    ppmc.Save();
                }

                foreach (DataGrdManufacturerListSelection d in list)
                {
                    addManufacturerConditionToPolicy(session, d.OrganizationId, pricePolicyId, true);
                }

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public void updateManufacturerExceptionInPolicy(Session session, Guid pricePolicyId, List<DataGrdManufacturerListSelection> list)
        {
            if (list == null)
                return;

            try
            {
                if (list.Count == 0)
                {
                    session.BeginTransaction();
                    CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );

                    XPCollection<PricePolicyManufacturerCondition> ppmcl = new XPCollection<PricePolicyManufacturerCondition>(session, criteria);

                    foreach (PricePolicyCondition ppmc in ppmcl)
                    {
                        ppmc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        ppmc.Save();
                    }
                    session.CommitTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }

            try
            {
                session.BeginTransaction();
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                XPCollection<PricePolicyManufacturerCondition> ppmcl = new XPCollection<PricePolicyManufacturerCondition>(session, criteria);

                foreach (PricePolicyCondition ppmc in ppmcl)
                {
                    ppmc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    ppmc.Save();
                }

                foreach (DataGrdManufacturerListSelection d in list)
                {
                    addManufacturerConditionToPolicy(session, d.OrganizationId, pricePolicyId, false);
                }

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public List<DataGrdManufacturerListSelection> loadManufacturerConditionInPolicy(Session session, Guid pricePolicyId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );
                XPCollection<PricePolicyManufacturerCondition> ppmcl = new XPCollection<PricePolicyManufacturerCondition>(session, criteria);

                List<DataGrdManufacturerListSelection> rs = new List<DataGrdManufacturerListSelection>();

                foreach (PricePolicyManufacturerCondition ppmc in ppmcl)
                {
                    DataGrdManufacturerListSelection d = new DataGrdManufacturerListSelection();
                    d.OrganizationId = ppmc.ManufacturerOrgId.OrganizationId;
                    d.Code = ppmc.ManufacturerOrgId.Code;
                    d.Name = ppmc.ManufacturerOrgId.Name;
                    rs.Add(d);
                }
                return rs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DataGrdManufacturerListSelection> loadManufacturerExceptionInPolicy(Session session, Guid pricePolicyId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );
                XPCollection<PricePolicyManufacturerCondition> ppmcl = new XPCollection<PricePolicyManufacturerCondition>(session, criteria);

                List<DataGrdManufacturerListSelection> rs = new List<DataGrdManufacturerListSelection>();

                foreach (PricePolicyManufacturerCondition ppmc in ppmcl)
                {
                    DataGrdManufacturerListSelection d = new DataGrdManufacturerListSelection();
                    d.OrganizationId = ppmc.ManufacturerOrgId.OrganizationId;
                    d.Code = ppmc.ManufacturerOrgId.Code;
                    d.Name = ppmc.ManufacturerOrgId.Name;
                    rs.Add(d);
                }
                return rs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Manufacturer Condition/Exception

        #region ItemUnit Condition/Exception

        public void addItemUnitConditionToPolicy(Session session, Guid itemUnitId, Guid pricePolicyId, bool isIncluding)
        {
            try
            {
                NAS.DAL.Sales.Price.PricePolicy p = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(pricePolicyId);
                if (p == null)
                    throw new Exception("Key is not Exist in PricePolicy");

                NAS.DAL.Nomenclature.Item.ItemUnit i = session.GetObjectByKey<NAS.DAL.Nomenclature.Item.ItemUnit>(itemUnitId);
                if (i == null)
                    throw new Exception("Key is not Exist in ItemUnit");

                //CriteriaOperator criteria = CriteriaOperator.And(
                //        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                //        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater),
                //        new BinaryOperator("IsIncluding", isIncluding, BinaryOperatorType.Equal),
                //        new BinaryOperator("ItemUnitId!Key", itemUnitId, BinaryOperatorType.Greater)
                //    );

                //PricePolicyItemUnitCondition ppiuc = session.FindObject<PricePolicyItemUnitCondition>(criteria);
                //if (ppiuc == null)
                //{
                PricePolicyItemUnitCondition ppiuc = new PricePolicyItemUnitCondition(session);
                ppiuc.CreateDate = DateTime.Now;
                ppiuc.IsIncluding = isIncluding;
                ppiuc.IssueDate = DateTime.Now;
                ppiuc.LastUpdateDate = DateTime.Now;
                ppiuc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                ppiuc.PricePolicyId = p;
                ppiuc.ItemUnitId = i;
                ppiuc.Save();
                //}
                //else
                //{
                //    ppiuc.ItemUnitId = i;
                //    ppiuc.IsIncluding = isIncluding;
                //    ppiuc.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                //    ppiuc.Save();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateItemUnitConditionInPolicy(Session session, Guid pricePolicyId, List<DataGrdItemUnitListSelection> list)
        {
            if (list == null)
                return;

            try
            {
                if (list.Count == 0)
                {
                    session.BeginTransaction();
                    CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );

                    XPCollection<PricePolicyItemUnitCondition> ppiucl = new XPCollection<PricePolicyItemUnitCondition>(session, criteria);

                    foreach (PricePolicyCondition ppiuc in ppiucl)
                    {
                        ppiuc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        ppiuc.Save();
                    }
                    session.CommitTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }

            try
            {
                session.BeginTransaction();
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                XPCollection<PricePolicyItemUnitCondition> ppiucl = new XPCollection<PricePolicyItemUnitCondition>(session, criteria);

                foreach (PricePolicyCondition ppiuc in ppiucl)
                {
                    ppiuc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    ppiuc.Save();
                }

                foreach (DataGrdItemUnitListSelection d in list)
                {
                    addItemUnitConditionToPolicy(session, d.ItemUnitId, pricePolicyId, true);
                }

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public void updateItemUnitExceptionInPolicy(Session session, Guid pricePolicyId, List<DataGrdItemUnitListSelection> list)
        {
            if (list == null)
                return;

            try
            {
                if (list.Count == 0)
                {
                    session.BeginTransaction();
                    CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                    XPCollection<PricePolicyItemUnitCondition> ppiucl = new XPCollection<PricePolicyItemUnitCondition>(session, criteria);

                    foreach (PricePolicyCondition ppiuc in ppiucl)
                    {
                        ppiuc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                        ppiuc.Save();
                    }
                    session.CommitTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }

            try
            {
                session.BeginTransaction();
                CriteriaOperator criteria = CriteriaOperator.And(
                        new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                        new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                XPCollection<PricePolicyItemUnitCondition> ppiucl = new XPCollection<PricePolicyItemUnitCondition>(session, criteria);

                foreach (PricePolicyCondition ppiuc in ppiucl)
                {
                    ppiuc.RowStatus = Utility.Constant.ROWSTATUS_DELETED;
                    ppiuc.Save();
                }

                foreach (DataGrdItemUnitListSelection d in list)
                {
                    addItemUnitConditionToPolicy(session, d.ItemUnitId, pricePolicyId, false);
                }

                session.CommitTransaction();
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public List<DataGrdItemUnitListSelection> loadItemUnitConditionInPolicy(Session session, Guid pricePolicyId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", true, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );
                XPCollection<PricePolicyItemUnitCondition> ppiucl = new XPCollection<PricePolicyItemUnitCondition>(session, criteria);

                List<DataGrdItemUnitListSelection> rs = new List<DataGrdItemUnitListSelection>();

                foreach (PricePolicyItemUnitCondition ppic in ppiucl)
                {
                    DataGrdItemUnitListSelection d = new DataGrdItemUnitListSelection();
                    d.ItemUnitId = ppic.ItemUnitId.ItemUnitId;
                    d.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    NAS.DAL.Nomenclature.Item.ItemUnit iu =
                    session.FindObject<NAS.DAL.Nomenclature.Item.ItemUnit>(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL, BinaryOperatorType.Equal));
                    if (ppic.ItemUnitId.ItemUnitId.Equals(iu.ItemUnitId))
                    {
                        d.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL;
                        rs.Add(d);
                        return rs;
                    }
                    d.ItemCode = ppic.ItemUnitId.ItemId.Code;
                    d.ItemName = ppic.ItemUnitId.ItemId.Name;
                    d.UnitName = ppic.ItemUnitId.UnitId.Name;
                    rs.Add(d);
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DataGrdItemUnitListSelection> loadItemUnitExceptionInPolicy(Session session, Guid pricePolicyId)
        {
            try
            {
                CriteriaOperator criteria = CriteriaOperator.And(
                            new BinaryOperator("PricePolicyId!Key", pricePolicyId, BinaryOperatorType.Equal),
                            new BinaryOperator("IsIncluding", false, BinaryOperatorType.Equal),
                            new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                        );
                XPCollection<PricePolicyItemUnitCondition> ppiucl = new XPCollection<PricePolicyItemUnitCondition>(session, criteria);

                List<DataGrdItemUnitListSelection> rs = new List<DataGrdItemUnitListSelection>();

                foreach (PricePolicyItemUnitCondition ppic in ppiucl)
                {
                    DataGrdItemUnitListSelection d = new DataGrdItemUnitListSelection();
                    d.ItemUnitId = ppic.ItemUnitId.ItemUnitId;
                    d.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    NAS.DAL.Nomenclature.Item.ItemUnit iu =
                    session.FindObject<NAS.DAL.Nomenclature.Item.ItemUnit>(
                        new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL, BinaryOperatorType.Equal));
                    if (ppic.ItemUnitId.ItemUnitId.Equals(iu.ItemUnitId))
                    {
                        d.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT_SELECTEDALL;
                        rs.Add(d);
                        return rs;
                    }
                    d.ItemCode = ppic.ItemUnitId.ItemId.Code;
                    d.ItemName = ppic.ItemUnitId.ItemId.Name;
                    d.UnitName = ppic.ItemUnitId.UnitId.Name;
                    rs.Add(d);
                }
                return rs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Price Formula

        public void SaveFormulaPricePolicy(Session session, Guid pricePolicyId, byte[] priceExpression, List<TaxTypeSelection> listTaxType)
        {
            try
            {
                session.BeginTransaction();
                NAS.DAL.Sales.Price.PricePolicy p = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(pricePolicyId);
                if (p == null)
                    throw new Exception("Key is not Exist in PricePolicy");

                if (p.PriceCaculators != null && p.PriceCaculators.Count > 0)
                {
                    p.PriceCaculators[0].PriceExpression = priceExpression;
                    p.PriceCaculators[0].Save();

                    session.Delete(p.PriceCaculators[0].PriceFormulaTaxAddeds);

                    foreach (TaxTypeSelection t in listTaxType)
                    {
                        TaxType tt = session.GetObjectByKey<TaxType>(t.TaxTypeId);
                        if (tt == null)
                            throw new Exception("Key is not Exist in TaxType");

                        PriceFormulaTaxAdded pfta = new PriceFormulaTaxAdded(session);
                        pfta.PriceCaculatorId = p.PriceCaculators[0];
                        pfta.ByValue = t.ByValue;
                        pfta.ByPercentage = t.ByPercentTage;
                        pfta.TaxTypeId = tt;
                        pfta.Save();
                    }
                }

                else {
                    PriceCaculator pc = new PriceCaculator(session);
                    pc.PriceExpression = priceExpression;
                    pc.PricePolicyId = p;
                    pc.Save();

                    foreach (TaxTypeSelection t in listTaxType)
                    {
                        TaxType tt = session.GetObjectByKey<TaxType>(t.TaxTypeId);
                        if (tt == null)
                            throw new Exception("Key is not Exist in TaxType");

                        PriceFormulaTaxAdded pfta = new PriceFormulaTaxAdded(session);
                        pfta.PriceCaculatorId = pc;
                        pfta.ByValue = t.ByValue;
                        pfta.ByPercentage = t.ByPercentTage;
                        pfta.TaxTypeId = tt;
                        pfta.Save();
                    }

                }

                session.CommitTransaction();
                
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                throw ex;
            }
        }

        public PriceCaculator LoadFormulaPricePolicy(Session session, Guid pricePolicyId, ref List<TaxTypeSelection> listTaxType)
        {
            try
            {
                listTaxType = new List<TaxTypeSelection>();
                NAS.DAL.Sales.Price.PricePolicy p = session.GetObjectByKey<NAS.DAL.Sales.Price.PricePolicy>(pricePolicyId);
                if (p == null)
                    throw new Exception("Key is not Exist in PricePolicy");

                if (p.PriceCaculators != null && p.PriceCaculators.Count > 0)
                {

                    foreach (PriceFormulaTaxAdded pfta in p.PriceCaculators[0].PriceFormulaTaxAddeds)
                    {
                        TaxTypeSelection tts = new TaxTypeSelection();
                        tts.TaxTypeId = pfta.TaxTypeId.TaxTypeId;
                        tts.ByPercentTage = pfta.ByPercentage;
                        tts.ByValue = pfta.ByValue;
                        listTaxType.Add(tts);
                    }

                    return p.PriceCaculators[0];
                }
            } 
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public List<COGS> getCOGSInAllInventory(Session session)
        {
            try
            {
                List<COGS> rs = new List<COGS>();
                AccountingPeriod currentAP = AccountingPeriodBO.getCurrentAccountingPeriod(session);
                if (currentAP == null)
                    return null;

                XPCollection<InventoryTransaction> ITLst = new XPCollection<InventoryTransaction>(session);
                ITLst.Criteria = CriteriaOperator.And(
                        new BinaryOperator("AccountingPeriodId", currentAP, BinaryOperatorType.Equal),
                        new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater)
                    );

                if (ITLst == null || ITLst.Count == 0)
                    return null;

                XPCollection<NAS.DAL.Nomenclature.Inventory.Inventory> il = new XPCollection<DAL.Nomenclature.Inventory.Inventory>(session,
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));

                XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit> iul = new XPCollection<NAS.DAL.Nomenclature.Item.ItemUnit>(session,
                    new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));

                foreach(NAS.DAL.Nomenclature.Inventory.Inventory i in il){
                    foreach(NAS.DAL.Nomenclature.Item.ItemUnit iu in iul)
                    {
                        CriteriaOperator criteriaIN = new InOperator("InventoryTransactionId", ITLst);
                        XPCollection<COGS> ILLst = new XPCollection<COGS>(session);
                        ILLst.Criteria = CriteriaOperator.And(
                            new InOperator("InventoryTransactionId", ITLst),
                            new BinaryOperator("ItemUnitId!Key", iu.ItemUnitId, BinaryOperatorType.Equal),
                            new BinaryOperator("InventoryId!Key", i.InventoryId, BinaryOperatorType.Equal)
                            );

                        if (ILLst != null && ILLst.Count > 0)
                        {
                            SortingCollection sortCollection = new SortingCollection();
                            sortCollection.Add(new SortProperty("InventoryTransactionId.IssueDate", DevExpress.Xpo.DB.SortingDirection.Descending));
                            ILLst.Sorting = sortCollection;
                            rs.Add(ILLst[0]);
                        }

                    }
                }

                SortingCollection sortRs = new SortingCollection();
                sortRs.Add(new SortProperty("ItemUnitId.ItemId.Code", DevExpress.Xpo.DB.SortingDirection.Descending));
                //sortRs.Add(new SortProperty("InventoryId.Code", DevExpress.Xpo.DB.SortingDirection.Descending));
                return rs;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<dynamic> getListOfDemoPrices(Session session, List<DemoPrice> cogsList, RulesWiz.Rules.Rule ruleObject, Guid pricePolicyId)
        {
            //List<DemoPrice> rs = new List<DemoPrice>();
            List<dynamic> rs = new List<dynamic>();
            List<COGS> passedException = new List<COGS>();
            List<TaxTypeSelection> listTaxType = new List<TaxTypeSelection>();
            PriceCaculator p = LoadFormulaPricePolicy(session, pricePolicyId, ref listTaxType);
            
            if (p == null)
                throw new Exception("The key not exist in PriceCaculator");

            foreach (DemoPrice cdemo in cogsList)
            {
                COGS c = session.GetObjectByKey<COGS>(cdemo.COGSId);
                //DemoPrice demo = new DemoPrice();
                dynamic demo = new DynamicPriceReview();
                
                demo.COGSId = cdemo.COGSId;
                demo.ItemCode = c.ItemUnitId.ItemId.Code;
                demo.ItemName = c.ItemUnitId.ItemId.Name;
                demo.UnitName = c.ItemUnitId.UnitId.Name;
                demo.InventoryIdName = c.InventoryId.Name;
                demo.UpdateDate = c.IssueDate;
                demo.COGSPrice = c.COGSPrice;

                if (ruleObject.Exceptions.EvaluateAll(c) && ruleObject.Conditions.EvaluateAll(c))
                {
                    string expression = Encoding.ASCII.GetString(p.PriceExpression);
                    Expression caculator = new Expression(expression);
                    foreach(PriceFormulaTaxAdded pfta in p.PriceFormulaTaxAddeds)
                    {
                        Tax tmpTax = null;
                        if (c.ItemUnitId.ItemId.ItemTaxes != null && c.ItemUnitId.ItemId.ItemTaxes.Count > 0)
                            foreach (Tax t in c.ItemUnitId.ItemId.ItemTaxes.Select(ds => ds.TaxId))
                                if (pfta.TaxTypeId.Taxs.Contains<Tax>(t)){
                                    tmpTax = t;
                                    break;
                                }

                        if (tmpTax == null)
                        {
                            ((DynamicPriceReview)demo).Dictionary.Add(pfta.TaxTypeId.Code,  "0%");
                            caculator.Parameters[pfta.TaxTypeId.Code] = (double)0;
                        }else{
                            ((DynamicPriceReview)demo).Dictionary.Add(pfta.TaxTypeId.Code,  tmpTax.Percentage + "%");
                            caculator.Parameters[pfta.TaxTypeId.Code] = tmpTax.Percentage/100;
                        }
                        
                        ((DynamicPriceReview)demo).addCaption(pfta.TaxTypeId.Code, pfta.TaxTypeId.Name);
                    }

                    if (expression.IndexOf("[COGS]") >= 0)
                    {
                        caculator.Parameters["COGS"] = c.COGSPrice;
                    }

                    demo.COGSReview = double.Parse( caculator.Evaluate().ToString());
                }
                else {
                    demo.Note = "Ngoài phạm vi áp dụng";
                }

                ((DynamicPriceReview)demo).addCaption("COGSId", "COGSId");
                ((DynamicPriceReview)demo).addCaption("ItemCode", "Mã hàng hóa");
                ((DynamicPriceReview)demo).addCaption("ItemName", "Tên hàng hóa");
                ((DynamicPriceReview)demo).addCaption("UnitName", "Đơn vị tính");
                ((DynamicPriceReview)demo).addCaption("InventoryIdName", "Kho");
                ((DynamicPriceReview)demo).addCaption("UpdateDate", "Ngày cập nhật giá");
                ((DynamicPriceReview)demo).addCaption("COGSPrice", "Giá vốn");
                ((DynamicPriceReview)demo).addCaption("COGSReview", "Giá bán tham khảo");
                ((DynamicPriceReview)demo).addCaption("Note", "Ghi chú");

                rs.Add(demo);
            }


            return rs;
        }

        #endregion Price Formula

        public List<TaxType> GetTaxesForPricePolicyFormulaSetting()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                XPCollection<TaxType> taxTypes = new XPCollection<TaxType>(session);
                if (taxTypes != null && taxTypes.Count > 0)
                    return taxTypes.ToList();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                session.Dispose();
            }
        }
    }
}
