using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
//using BLL.PurchasingBLO;
//using BLL.BO.Purchasing;
//using DAL.PurchasingCode;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxEditors;

namespace DXApplication1.Purchasing.UserControl
{
    public partial class uBuyingMaterial
    {
        private Session session;

        //private MaterialBLO materialBLO = new MaterialBLO();

        //private MaterialCategoryBLO categoryBLO = new MaterialCategoryBLO();

        //private MaterialUnitBLO materialUnitBLO = new MaterialUnitBLO();

        //private MaterialEntity materialEntity = new MaterialEntity();

        //public MaterialEntity MaterialEntity
        //{
        //    get
        //    {
        //        return materialEntity;
        //    }
        //    set
        //    {
        //        materialEntity = value;
        //    }
        //}

        //public MaterialEntity FirstMaterialEntity
        //{
        //    get
        //    {
        //        MaterialEntity o = Session["FirstMaterialEntity"] as MaterialEntity;
        //        if (o == null)
        //            o = new MaterialEntity();
        //        return o;
        //    }

        //    set
        //    {
        //        Session["FirstMaterialEntity"] = value;
        //    }
        //}

        //public List<MaterialBuyingMaterialCategoryEntity> CategoryEntityList
        //{
        //    get
        //    {
        //        List<MaterialBuyingMaterialCategoryEntity> table = Session["CategoriesTable"] as List<MaterialBuyingMaterialCategoryEntity>;
        //        if (table == null)
        //            table = new List<MaterialBuyingMaterialCategoryEntity>();
        //        return table;
        //    }

        //    set
        //    {
        //        Session["CategoriesTable"] = value;
        //    }

        //}

        //public List<MaterialSupplierEntity> SupplierEntityList
        //{
        //    get
        //    {
        //        List<MaterialSupplierEntity> table = Session["SuppliersTable"] as List<MaterialSupplierEntity>;
        //        if (table == null)
        //            table = new List<MaterialSupplierEntity>();
        //        return table;
        //    }

        //    set
        //    {
        //        Session["SuppliersTable"] = value;
        //    }
        //}

        //public XPCollection<ViewMaterialUnitConstruction> MaterialUnitConstruction
        //{
        //    get
        //    {
        //        return Session["MaterialUnitConstruction"] as XPCollection<ViewMaterialUnitConstruction>;
        //    }

        //    set
        //    {
        //        Session["MaterialUnitConstruction"] = value;
        //    }
        //}

        public ASPxHtmlEditor HtmlEditDescription
        {
            get
            {
                ASPxHtmlEditor editDescription = navBarMaterialDetail.Groups[0].FindControl("htmlEditDescription")
                                                as ASPxHtmlEditor;
                return editDescription;
            }
        }

        public ASPxButton ButtonEditMaterial{
            get
            {
                ASPxButton button = formMaterialEdit.FindControl("buttonEditMaterial") as ASPxButton;
                return button;
            }
        }

        public ASPxButton ButtonSaveMaterial
        {
            get
            {
                ASPxButton button = formMaterialEdit.FindControl("buttonSaveMaterial") as ASPxButton;
                return button;
            }
        }

        public string Mode
        {
            get
            {
                if (hiddenMode.Contains("Mode"))
                    return hiddenMode["Mode"].ToString();
                return null;
            }
            set
            {
                if (hiddenMode.Contains("Mode"))
                    hiddenMode.Set("Mode", value);
                else
                    hiddenMode.Add("Mode", value);
            }
        }

        public Guid KeyValue
        {
            get
            {
                if (hiddenMode.Contains("KeyValue"))
                    return (Guid)hiddenMode["KeyValue"];
                return Guid.NewGuid();
            }

            set
            {
                if (hiddenMode.Contains("KeyValue"))
                    hiddenMode.Set("KeyValue", value);
                else
                    hiddenMode.Add("KeyValue", value);
            }
        }
    }
}