using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIFSOrderPromotion
    {
        public int NO { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public string UoM { get; set; }
        public string QuantityInUoM { get; set; }
        public string Quantity { get; set; }
        public string Note { get; set; }
    }
    public class APIFSOrderPromotionList
    {
        public List<APIFSOrderPromotion> GetData()
        {
            string key = "9A1BD996-76A7-40D7-920E-0EBAAA2F013A";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APIFSOrderPromotion> Sale = new List<APIFSOrderPromotion>();
                Sale.Add(
                    new APIFSOrderPromotion()
                    {
                        NO = 1,
                        Code = 1,
                        Name = "Á Châu",
                        Specification = "",
                        UoM = "Hộp",
                        QuantityInUoM = "",
                        Quantity = "2",
                        Note = "",

                    }
                );
                Sale.Add(
                    new APIFSOrderPromotion()
                    {
                        NO = 2,
                        Code = 2,
                        Name = "Anh Nhật",
                        Specification = "",
                        UoM = "Hộp",
                        QuantityInUoM = "",
                        Quantity = "2",
                        Note = "",

                    }
                );
                Sale.Add(
                    new APIFSOrderPromotion()
                    {
                        NO = 3,
                        Code = 3,
                        Name = "An Khánh",
                        Specification = "",
                        UoM = "Hộp",
                        QuantityInUoM = "",
                        Quantity = "2",
                        Note = "",

                    }
                );
                Sale.Add(
                   new APIFSOrderPromotion()
                   {
                       NO = 4,
                       Code = 4,
                       Name = "An Khánh",
                       Specification = "",
                       UoM = "Hộp",
                       QuantityInUoM = "",
                       Quantity = "2",
                       Note = "",


                   }
               );
                Sale.Add(
                   new APIFSOrderPromotion()
                   {
                       NO = 5,
                       Code = 5,
                       Name = "Anh Nhật",
                       Specification = "",
                       UoM = "Hộp",
                       QuantityInUoM = "",
                       Quantity = "2",
                       Note = "",

                   }
               );
                Sale.Add(
                    new APIFSOrderPromotion()
                    {
                        NO = 6,
                        Code = 6,
                        Name = "Á Châu",
                        Specification = "",
                        UoM = "Hộp",
                        QuantityInUoM = "",
                        Quantity = "2",
                        Note = "",

                    }
                );
                HttpContext.Current.Session[key] = Sale;
            }
            return (List<APIFSOrderPromotion>)HttpContext.Current.Session[key];
        }
        public void AddItem(APIFSOrderPromotion postedItem)
        {
            List<APIFSOrderPromotion> list = GetData();
            postedItem.Code = (list.Count + 1);
            list.Add(postedItem);


        }


        public void UpdateItem(APIFSOrderPromotion postedItem)
        {
            var editedModel = GetData().First(i => i.Code == postedItem.Code);

            editedModel.NO = postedItem.NO;
            editedModel.Code = postedItem.Code;
            editedModel.Name = postedItem.Name;
            editedModel.Specification = postedItem.Specification;
            editedModel.UoM = postedItem.UoM;
            editedModel.QuantityInUoM = postedItem.QuantityInUoM;
            editedModel.Quantity = postedItem.Quantity;
            editedModel.Note = postedItem.Note;

        }
        public void DeleteItem(int id)
        {
            List<APIFSOrderPromotion> list = GetData();
            list.Remove(list.Where(w => w.Code == id).First());
        }
    }
}
