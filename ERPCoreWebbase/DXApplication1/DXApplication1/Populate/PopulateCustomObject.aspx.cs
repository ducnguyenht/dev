using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using NAS.DAL;
using DevExpress.Data.Filtering;
using NAS.BO.Nomenclature.Items;
using NAS.BO.CMS.ObjectDocument;
using NAS.DAL.Nomenclature.Item;
using NAS.DAL.Nomenclature.Organization;

namespace WebModule
{
    public partial class PopulateCustomObject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Unnamed1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            ItemBO itemBO = new ItemBO();
            int cnt = 0;
            using (UnitOfWork uow = XpoHelper.GetNewUnitOfWork())
            {
                try
                {
                    XPCollection<NAS.DAL.Nomenclature.Item.Item> items = new XPCollection<NAS.DAL.Nomenclature.Item.Item>(uow, new BinaryOperator("RowStatus", 0, BinaryOperatorType.Greater));
                    foreach (NAS.DAL.Nomenclature.Item.Item i in items)
                    {
                        ObjectBO objectBO = new ObjectBO();
                        Item item = uow.GetObjectByKey<Item>(i.ItemId);
                        if (item == null)
                            throw new Exception("The key is not exist in Item");

                        List<NAS.DAL.CMS.ObjectDocument.ObjectType> typeList = null;

                        if (item.ItemCustomTypes != null)
                            typeList = item.ItemCustomTypes.Select(r => r.ObjectTypeId).ToList();

                        if (typeList != null)
                            foreach (NAS.DAL.CMS.ObjectDocument.ObjectType type in typeList)
                            {
                                if (!itemBO.checkAlreadyHasObjectWithObjectType(uow, i.ItemId, type.ObjectTypeId))
                                {
                                    NAS.DAL.CMS.ObjectDocument.Object o = objectBO.CreateCMSObject(uow, type.ObjectTypeId);
                                    ItemObject it = new ItemObject(uow);
                                    it.ObjectId = o;
                                    it.ItemId = item;
                                }
                            }

                        uow.FlushChanges();
                        cnt++;
                    }
                    lblnum.Text = cnt.ToString();

                    ////Get MANUFACTURER object type
                    //NAS.DAL.CMS.ObjectDocument.ObjectType objectType = Util.getXPCollection<NAS.DAL.CMS.ObjectDocument.ObjectType>(uow, "Name", "MANUFACTURER").FirstOrDefault();
                    //XPCollection<NAS.DAL.Nomenclature.Organization.ManufacturerOrg> manufacturers = new XPCollection<NAS.DAL.Nomenclature.Organization.ManufacturerOrg>(uow);
                    //manufacturers.Criteria = new UnaryOperator(UnaryOperatorType.IsNull, "ObjectId");
                    //foreach (var manufacturer in manufacturers)
                    //{
                    //    //Create new CMS object
                    //    NAS.DAL.CMS.ObjectDocument.Object CMSObject = new NAS.DAL.CMS.ObjectDocument.Object(uow)
                    //    {
                    //        ObjectId = Guid.NewGuid(),
                    //        ObjectTypeId = objectType
                    //    };
                    //    CMSObject.Save();
                    //    manufacturer.ObjectTypeId = objectType;
                    //    manufacturer.ObjectId = CMSObject;
                    //    manufacturer.Save();
                    //}
                    uow.CommitChanges();
                }
                catch
                {
                    throw;
                }
            }

        }
    }
}