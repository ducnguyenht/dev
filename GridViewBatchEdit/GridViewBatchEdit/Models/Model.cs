using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Models {
    public class BatchEditRepository {
        public static List<GridDataItem> GridData {
            get {
                return Enumerable.Range(0, 100).Select(i => new GridDataItem {
                    ID = i,
                    C1 = i % 2,
                    C2 = i * 0.5 % 3,
                    C3 = "C3 " + i,
                    C4 = i % 2 == 0,
                    C5 = new DateTime(2013 + i, 12, 16)
                }).ToList();
            }
        }
        public static GridDataItem InsertNewItem(GridDataItem postedItem) {
            var newItem = new GridDataItem() { ID = GridData.Count };
            LoadNewValues(newItem, postedItem);
            GridData.Add(newItem);
            return newItem;
        }
        public static GridDataItem UpdateItem(GridDataItem postedItem) {
            var editedItem = GridData.First(i => i.ID == postedItem.ID);
            LoadNewValues(editedItem, postedItem);
            return editedItem;
        }
        public static GridDataItem DeleteItem(int itemKey) {
            var item = GridData.First(i => i.ID == itemKey);
            GridData.Remove(item);
            return item;
        }
        protected static void LoadNewValues(GridDataItem newItem, GridDataItem postedItem) {
            newItem.C1 = postedItem.C1;
            newItem.C2 = postedItem.C2;
            newItem.C3 = postedItem.C3;
            newItem.C4 = postedItem.C4;
            newItem.C5 = postedItem.C5;
        }
    }

    public class GridDataItem {
        public int ID { get; set; }
        public int C1 { get; set; }
        public double C2 { get; set; }
        //[System.ComponentModel.DisplayName(GridViewBatchEdit.Views.Shared.SharedStrings.String1)]
        [CustomDisplayName("String1")]
        //[Display(ResourceType = typeof(GridViewBatchEdit.Views.Shared.SharedStrings), Name = "String1")]
        public string C3 { get; set; }
        //[System.ComponentModel.DataAnnotations.Display(Name="4")]
        public bool C4 { get; set; }
        public DateTime C5 { get; set; }
    }

    public class CustomDisplayName : System.ComponentModel.DisplayNameAttribute {
        public CustomDisplayName()
            : base() {
        }
        public CustomDisplayName(string resourceId)
            : base(resourceId) {
        }

        public override string DisplayName { get { return GetMessageFromResource(base.DisplayName); } }

        private static string GetMessageFromResource(string resourceId) {
            Type type = typeof(GridViewBatchEdit.Views.Shared.SharedStrings);
            PropertyInfo nameProperty = type.GetProperty(resourceId, BindingFlags.Static | BindingFlags.Public);
            object result = nameProperty.GetValue(nameProperty.DeclaringType, null);

            return result.ToString();
        }
    }

}