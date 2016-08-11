using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL.CMS.ObjectDocument;
namespace NAS.DAL.Nomenclature.Item
{

    public partial class ItemCustomType
    {
        public ItemCustomType(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region Logic

        public static void Populate()
        {
            Session session = null;
            try
            {
                session = XpoHelper.GetNewSession();

                //Product
                Guid objectType_Product = Guid.Parse("5817B239-E150-4C8E-A313-EAA8BD6944C4");
                if (!Util.isExistXpoObject<ItemCustomType>("ObjectTypeId", objectType_Product))
                {
                    ItemCustomType itemCustomTypeBO = new ItemCustomType(session)
                    {
                        ObjectTypeId = session.GetObjectByKey<ObjectType>(objectType_Product)
                    };
                    itemCustomTypeBO.Save();
                }

                //Service
                Guid objectType_Service = Guid.Parse("BEBAB7E7-8294-4EB0-81DF-B5E33ACBFE76");
                if (!Util.isExistXpoObject<ItemCustomType>("ObjectTypeId", objectType_Service))
                {
                    ItemCustomType itemCustomTypeBO = new ItemCustomType(session)
                    {
                        ObjectTypeId = session.GetObjectByKey<ObjectType>(objectType_Service)
                    };
                    itemCustomTypeBO.Save();
                }

                //Service
                Guid objectType_Material = Guid.Parse("7C32F816-23D1-4B67-97F5-940461E06305");
                if (!Util.isExistXpoObject<ItemCustomType>("ObjectTypeId", objectType_Material))
                {
                    ItemCustomType itemCustomTypeBO = new ItemCustomType(session)
                    {
                        ObjectTypeId = session.GetObjectByKey<ObjectType>(objectType_Material)
                    };
                    itemCustomTypeBO.Save();
                }

                //Tool
                Guid objectType_Tool = Guid.Parse("F34C4E28-04C5-492E-AEE2-09416914F950");
                if (!Util.isExistXpoObject<ItemCustomType>("ObjectTypeId", objectType_Tool))
                {
                    ItemCustomType itemCustomTypeBO = new ItemCustomType(session)
                    {
                        ObjectTypeId = session.GetObjectByKey<ObjectType>(objectType_Tool)
                    };
                    itemCustomTypeBO.Save();
                }

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
