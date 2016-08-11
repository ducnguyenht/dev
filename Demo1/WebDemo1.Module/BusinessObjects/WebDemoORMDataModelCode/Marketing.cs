using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WebDemo1.Module.BusinessObjects.WebDemoORMDataModelCode
{
    [NavigationItem("Marketing")]
    public class Customer
    {
        public Customer()
        {
            Testimonials = new List<Testimonial>();
        }

        [Browsable(false)]
        public int Id { get; protected set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public string Company { get; set; }
        public string Occupation { get; set; }

        [Aggregated]
        public virtual IList<Testimonial> Testimonials { get; set; }

        public String FullName
        {
            get
            {
                string namePart = string.Format("{0} {1}", FirstName, LastName);
                return Company != null ? string.Format("{0} ({1})", namePart, Company) : namePart;
            }
        }

        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        public byte[] Photo { get; set; }
    }

    [NavigationItem("Marketing")]
    public class Testimonial
    {
        public Testimonial()
        {
            CreatedOn = DateTime.Now;
        }

        [Browsable(false)]
        public int Id { get; protected set; }

        [FieldSize(FieldSizeAttribute.Unlimited)]
        public string Quote { get; set; }

        [FieldSize(512)]
        public string Highlight { get; set; }

        [VisibleInListView(false)]
        public DateTime CreatedOn { get; internal set; }

        public string Tags { get; set; }
        public virtual Customer Customer { get; set; }
    }
}