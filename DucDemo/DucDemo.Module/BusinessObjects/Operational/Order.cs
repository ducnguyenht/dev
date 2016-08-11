using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;

namespace WinWebSolution.Module {
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    public class Order : BaseObject {
        public Order(Session session) : base(session) { }
        private string fDescription;
        [ImmediatePostData]
        public string Description {
            get {
                if (this.Product!=null)
                {
                    return Product.Name;
                }
                return fDescription;
            }
            set {
                if (Product!=null)
                {
                    fDescription = Product.Name;
                    value = Product.Name;
                }
               
                SetPropertyValue("Description", ref fDescription, value); 
            }
        }
        private decimal fTotal;
        public decimal Total {
            get { return fTotal; }
            set {
                SetPropertyValue("Total", ref fTotal, value);
                if(!IsLoading && !IsSaving && Product != null) {
                    Product.UpdateOrdersTotal(true);
                    Product.UpdateMaximumOrder(true);
                }
            }
        }
        private Product fProduct;
        [Association("Product-Orders")][ImmediatePostData]
        [EditorAlias("CustomWebLookup")]
        public Product Product {
            get { return fProduct; }
            set {
                Product oldProduct = fProduct;
                SetPropertyValue("Product", ref fProduct, value);
               // OnChanged("Description");
               // OnChanged("Description", oldProduct.Name, Product.Name);
                if (!IsLoading && !IsSaving && oldProduct != fProduct) {
                    oldProduct = oldProduct ?? fProduct;
                    oldProduct.UpdateOrdersCount(true);
                    oldProduct.UpdateOrdersTotal(true);
                    oldProduct.UpdateMaximumOrder(true);
                }
            }
        }
    }
}