using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Evaluant.Calculator;
using NAS.DAL;
using NAS.BO.Nomenclature.Items;
using DevExpress.Xpo;
using NAS.DAL.Nomenclature.Item;
using DevExpress.Data.Filtering;
//using Evaluant.Calculator;

namespace WebModule
{
    public partial class PopulateMUM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //NAS.DAL.Nomenclature.UnitItem.UnitType.Populate();
            //NAS.DAL.Nomenclature.Item.Unit.Populate();
            //UnitBO bo1 = new UnitBO();
            //bo1.populateAllUnitInCombineUnitToSPCEIFICATIONType();
            //ItemBO bo2 = new ItemBO();
            //bo2.PopulateDefaultUnitTypeConfigForAllItems();

            //PopulateMUMDefaultConfig();
        }

        private void PopulateMUMDefaultConfig()
        {
            using (Session session = XpoHelper.GetNewSession())
            {
                //Get item list
                XPCollection<Item> itemList = new XPCollection<Item>(session, 
                    new BinaryOperator("RowStatus", Utility.Constant.ROWSTATUS_ACTIVE));

                //Get specification unit type
                NAS.DAL.Nomenclature.UnitItem.UnitType specUnitType = 
                    session.FindObject<NAS.DAL.Nomenclature.UnitItem.UnitType>(new BinaryOperator("Code", "SPECIFICATION"));

                foreach (var item in itemList)
                {
                    int countHasConfigSpecUnit = item.itemUnitTypeConfigs.Count(r =>
                        r.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE
                        && r.UnitTypeId == specUnitType);

                    if (countHasConfigSpecUnit != 0)
                        continue;

                    //Config SPECIFICATION for the item
                    int countSpecConfigUnit = item.itemUnitTypeConfigs.Count(r =>
                        r.UnitTypeId == specUnitType);

                    ItemUnitTypeConfig config;

                    if (countSpecConfigUnit != 0)
                    {
                        config = item.itemUnitTypeConfigs.Where(r =>
                            r.UnitTypeId == specUnitType).FirstOrDefault();
                        config.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                    }
                    else
                    {
                        config = new ItemUnitTypeConfig(session)
                        {
                            ItemId = item,
                            RowStatus = Utility.Constant.ROWSTATUS_ACTIVE,
                            UnitTypeId = specUnitType
                        };
                    }

                    //Config isMaster flag
                    int countIsMaster = item.itemUnitTypeConfigs.Count(r => r.IsMaster);
                    
                    if (countIsMaster == 0) 
                        config.IsMaster = true;

                    config.Save();

                    //Active SPECIFICATION units of the item
                    var specItemUnitList = item.ItemUnits.Where(r => r.UnitId != null 
                        && r.UnitId.UnitTypeId == specUnitType
                        && r.UnitId.RowStatus == Utility.Constant.ROWSTATUS_ACTIVE);
                    foreach (var specItemUnit in specItemUnitList)
                    {
                        specItemUnit.RowStatus = Utility.Constant.ROWSTATUS_ACTIVE;
                        specItemUnit.Save();
                    }

                }
            }
        }
    }
}