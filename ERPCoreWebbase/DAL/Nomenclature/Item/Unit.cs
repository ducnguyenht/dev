using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace NAS.DAL.Nomenclature.Item
{

    public partial class Unit
    {
        public Unit(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        protected override void OnSaving()
        {
            base.OnSaving();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
        }
        #region
        public static Unit addDefaultUnitBO()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                Unit unit = new Unit(session)
                {
                    Code = Guid.NewGuid().ToString(),
                    Description = Utility.Constant.NAAN_DEFAULT_NAME,
                    RowStatus = 0,
                    RowCreationTimeStamp = DateTime.Now
                };
                unit.Save();
                session.Dispose();
                return unit;
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
        }

        public int CheckUniqueName()
        {
            return 1;
        }

        public int CheckConstraint()
        {
            return 1;
        }

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();
                //insert default data into Unit table
                if (!Util.isExistXpoObject<Unit>("Name", Utility.Constant.NAAN_DEFAULT_NAME))
                {
                    Unit unit = new Unit(session)
                    {
                        Name = Utility.Constant.NAAN_DEFAULT_NAME,
                        Description = "",
                        RowStatus = -1,
                        RowCreationTimeStamp = DateTime.Now
                    };
                    unit.Save();
                }
                //DND
                NAS.DAL.Nomenclature.Organization.Organization orgID = session.FindObject<NAS.DAL.Nomenclature.Organization.Organization>(new BinaryOperator("Code", "QUASAPHARCO"));
                NAS.DAL.Nomenclature.UnitItem.UnitType unitTypeID_LENGTH = session.FindObject<NAS.DAL.Nomenclature.UnitItem.UnitType>(new BinaryOperator("Code", "LENGTH"));
                NAS.DAL.Nomenclature.UnitItem.UnitType unitTypeID_WEIGHT = session.FindObject<NAS.DAL.Nomenclature.UnitItem.UnitType>(new BinaryOperator("Code", "WEIGHT"));
                NAS.DAL.Nomenclature.UnitItem.UnitType unitTypeID_CAPACITY = session.FindObject<NAS.DAL.Nomenclature.UnitItem.UnitType>(new BinaryOperator("Code", "CAPACITY"));
                NAS.DAL.Nomenclature.UnitItem.UnitType unitTypeID_ACREAGE = session.FindObject<NAS.DAL.Nomenclature.UnitItem.UnitType>(new BinaryOperator("Code", "AREA"));

                if (orgID == null)
                    throw new Exception(@"Không có dữ liệu tổ chức QUASAPHARCO trong Database");
                if (unitTypeID_ACREAGE == null)
                    throw new Exception(@"không có dữ liệu ACREAGE trong Database");
                if (unitTypeID_LENGTH == null)
                    throw new Exception(@"Không có dữ liệu LENGTH trong Database");
                if (unitTypeID_WEIGHT == null)
                    throw new Exception(@"Không có dữ liệu WEIGHT trong Database");
                if (unitTypeID_CAPACITY == null)
                    throw new Exception(@"Không có dữ liệu CAPACITY trong Database");

                string[] Lenghts = { "mm", "cm", "dm", "m", "km" };
                string[] Weights = { "mg", "g", "kg", "tan" };
                string[] Capacitys = { "ml", "lit", "m³" };
                string[] AREA = { "mm²", "cm²", "dm²", "m²" };
                string[] Name_Lenght = { "Millimeter", "Centimeter", "Decimeter", "Meter", "Kilometer" };
                string[] Name_Weight = { "Milligram", "Gram", "Kilogram", "Ton" };
                string[] Name_Capacity = { "Milliliter", "Liter", "Cubic Meters" };
                string[] Name_AREA = { "Square Milliliter", "Square Centimeter", "Square Decimeter", "Square Meter" };
                int i = 0;

                #region ADD Lenghts
                if (unitTypeID_LENGTH != null)
                {
                    for (i = 0; i < Lenghts.Length; i++)
                    {
                        if (!Util.isExistXpoObject<Unit>("Code", Lenghts[i], Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                        {
                            Unit unit = new Unit(session)
                            {
                                Code = Lenghts[i],
                                Name = Name_Lenght[i],
                                RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                                OrganizationId = orgID,
                                UnitTypeId = unitTypeID_LENGTH,
                                RowCreationTimeStamp = DateTime.Now
                            };
                            unit.Save();
                        }
                        else { 
                            Unit unit =  session.FindObject<Unit>(new BinaryOperator("Code", Lenghts[i], BinaryOperatorType.Equal));
                            unit.Name = Name_Lenght[i];
                            unit.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                            unit.OrganizationId = orgID;
                            unit.UnitTypeId = unitTypeID_LENGTH;
                            unit.Save();
                        }
                    }
                }
                else
                {
                    throw new Exception(@"Không có dữ liệu Length trong Database");
                }
                #endregion
                #region ADD Weights
                if (unitTypeID_WEIGHT != null)
                {
                    for (i = 0; i < Weights.Length; i++)
                    {
                        if (!Util.isExistXpoObject<Unit>("Code", Weights[i], Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                        {
                            Unit unit = new Unit(session)
                            {
                                Code = Weights[i],
                                Name = Name_Weight[i],
                                RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                                OrganizationId = orgID,
                                UnitTypeId = unitTypeID_WEIGHT,
                                RowCreationTimeStamp = DateTime.Now
                            };
                            unit.Save();
                        }
                        else {
                            Unit unit = session.FindObject<Unit>(new BinaryOperator("Code", Weights[i], BinaryOperatorType.Equal));
                            unit.Name = Name_Weight[i];
                            unit.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                            unit.OrganizationId = orgID;
                            unit.UnitTypeId = unitTypeID_WEIGHT;
                            unit.Save();
                        }
                    }
                }
                else
                {
                    throw new Exception(@"Không có dữ liệu Weight trong Database");
                }
                #endregion
                #region ADD Capacitys
                if (unitTypeID_CAPACITY != null)
                {
                    for (i = 0; i < Capacitys.Length; i++)
                    {
                        if (!Util.isExistXpoObject<Unit>("Code", Capacitys[i], Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                        {
                            Unit unit = new Unit(session)
                            {
                                Code = Capacitys[i],
                                Name = Name_Capacity[i],
                                RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                                OrganizationId = orgID,
                                UnitTypeId = unitTypeID_CAPACITY,
                                RowCreationTimeStamp = DateTime.Now
                            };
                            unit.Save();
                        }
                        else {
                            Unit unit = session.FindObject<Unit>(new BinaryOperator("Code", Capacitys[i], BinaryOperatorType.Equal));
                            unit.Name = Name_Capacity[i];
                            unit.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                            unit.OrganizationId = orgID;
                            unit.UnitTypeId = unitTypeID_CAPACITY;
                            unit.Save();
                        }
                    }
                }
                else
                {
                    throw new Exception(@"Không có dữ liệu Capacitys trong Database");
                }
                #endregion
                #region ADD Acreage
                if (unitTypeID_ACREAGE != null)
                {
                    for (i = 0; i < AREA.Length; i++)
                    {
                        if (!Util.isExistXpoObject<Unit>("Code", AREA[i], Utility.Constant.ROWSTATUS_DEFAULT, Utility.Constant.ROWSTATUS_ACTIVE))
                        {
                            Unit unit = new Unit(session)
                            {
                                Code = AREA[i],
                                Name = Name_AREA[i],
                                RowStatus = Utility.Constant.ROWSTATUS_DEFAULT,
                                OrganizationId = orgID,
                                UnitTypeId = unitTypeID_ACREAGE,
                                RowCreationTimeStamp = DateTime.Now
                            };
                            unit.Save();
                        }
                        else
                        {
                            Unit unit = session.FindObject<Unit>(new BinaryOperator("Code", AREA[i], BinaryOperatorType.Equal));
                            unit.Name = Name_AREA[i];
                            unit.RowStatus = Utility.Constant.ROWSTATUS_DEFAULT;
                            unit.OrganizationId = orgID;
                            unit.UnitTypeId = unitTypeID_ACREAGE;
                            unit.Save();
                        }
                    }
                }
                else
                {
                    throw new Exception(@"Không có dữ liệu Acreage trong Database");
                }
                #endregion
                //END DND
            }
            catch (Exception)
            {
                session.RollbackTransaction();
                throw;
            }
            finally
            {
                if (session != null) session.Dispose();
            }
        }
        #endregion
    }

}
