using System;
using E911.Module.Editors;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;

namespace E911.Module.Web.Editors
{
    /// <summary>
    /// A custom Application Model element extension for the View Item node to be able to specify custom ASP.NET controls via the Model Editor.
    /// </summary>
    public interface IModelWebCustomUserControlViewItem : IModelCustomUserControlViewItem
    {
        [Category("Data")]
        string CustomControlPath { get; set; }
    }
    /// <summary>
    /// An custom View Item that hosts a custom ASP.NET user control (http://documentation.devexpress.com/#Xaf/CustomDocument2612) to show it in the XAF View.
    /// </summary>
    [ViewItem(typeof(IModelCustomUserControlViewItem))]
    public class WebCustomUserControlViewItem : CustomUserControlViewItem
    {
        protected IModelWebCustomUserControlViewItem model;
     
        public WebCustomUserControlViewItem(IModelViewItem model, Type objectType)
            : base(model, objectType)
        {
            this.model = model as IModelWebCustomUserControlViewItem;
            if (this.model == null)
                throw new ArgumentNullException("IModelWebCustomUserControlViewItem must extend IModelCustomUserControlViewItem in the ExtendModelInterfaces method of your Web ModuleBase descendant.");
        }

        protected override object CreateControlCore()
        {
            // You can access the View and other properties here to additionally initialize your control.
            System.Web.UI.Control userControl = WebWindow.CurrentRequestPage.LoadControl(model.CustomControlPath);
            userControl.ID = GetType().Name;
            return userControl;
        }
    }
}