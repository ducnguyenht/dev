using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Sell.Models
{
    public class APIGTOrderPromotion
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
    public class APIGTOrderPromotionList
    {
        public List<APIGTOrderPromotion> GetData()
        {
            string key = "900CC5DF-4BDC-4DE5-93F0-3BE8844663EF";
            if (HttpContext.Current.Session[key] == null)
            {
                List<APIGTOrderPromotion> Sale = new List<APIGTOrderPromotion>();
                Sale.Add(
                    new APIGTOrderPromotion()
                    {
                        NO=1,
                        Code=1,
                        Name="Á Châu",
                        Specification="",
                        UoM="Hộp",
                        QuantityInUoM="",
                        Quantity="2",
                        Note="",
                       
                    }
                );
                Sale.Add(
                    new APIGTOrderPromotion()
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
                    new APIGTOrderPromotion()
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
                   new APIGTOrderPromotion()
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
                   new APIGTOrderPromotion()
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
                    new APIGTOrderPromotion()
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
            return (List<APIGTOrderPromotion>)HttpContext.Current.Session[key];
        }
        public void AddItem(APIGTOrderPromotion postedItem)
        {
            List<APIGTOrderPromotion> list = GetData();
            postedItem.Code = (list.Count + 1);
            list.Add(postedItem);


        }


        public void UpdateItem(APIGTOrderPromotion postedItem)
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
            List<APIGTOrderPromotion> list = GetData();
            list.Remove(list.Where(w => w.Code == id).First());
        }
    }
}
