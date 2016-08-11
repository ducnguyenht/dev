using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxFileManager;
using System.IO;

//using DAL.Purchasing;

namespace ERPCore.ImExporting.UserControl
{
    public partial class uSupplierEdit : System.Web.UI.UserControl
    {
        //UnitOfWork uow;
        //ViewSupplier viewSupplier;
        //Supplier supllier;
        //SupplierProperty supllierProperty;

        //private bool validate()
        //{
        //    bool isValid = true;

        //    UnitOfWork uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
        //    viewSupplier = (ViewSupplier) Session["supplierMode"];

        //    string oldCode = "";
        //    string oldName = "";

        //    if (viewSupplier != null)
        //    {
        //        oldCode = viewSupplier.Code;
        //        oldName = viewSupplier.Name;
        //    }

        //    //supllier = uow.FindObject<Supplier>(new BinaryOperator("Code", txtCodeSupplier.Text));

        //    //if (supllier != null && oldCode != txtCodeSupplier.Text)
        //    //{
        //    //    ///txtCodeSupplier.ValidationSettings.ErrorText = "Mã Hàng Hóa này đã tồn tại !";
        //    //    isValid = false;
        //    //}

        //    ////supllierProperty = uow.FindObject<SupplierProperty>(new BinaryOperator("Name", txtNameSupplier.Text));

        //    //if (supllierProperty != null && oldName != txtNameSupplier.Text)
        //    //{
        //    //    //txtNameSupplier.ValidationSettings.ErrorText = "Tên Hàng Hóa này đã tồn tại !";
        //    //    isValid = false;
        //    //}

        //    return isValid;            
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
              
        }

        protected void cpLineSupplier_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            //String[] aParam = e.Parameter.Split('|');
            
            //ASPxFileManager file = (ASPxFileManager)navbarDetail.Items[0].FindControl("fileDetail");

            //switch (aParam[0])
            //{
            //    case "new":
            //        Session["url"] = Guid.NewGuid();

            //        var rootPath = Server.MapPath("~/Purchasing/Temp/");
            //        if (Directory.Exists(rootPath))
            //        {
            //            Directory.CreateDirectory(rootPath + Session["url"].ToString());
            //            Directory.CreateDirectory(rootPath + Session["url"].ToString() + "/Tài liệu"); 
            //            Directory.CreateDirectory(rootPath + Session["url"].ToString() + "/Hình ảnh");
            //        }


            //        file.Settings.RootFolder = "~/Purchasing/Temp/" + Session["url"].ToString();                                                            

            //        break;
            //    case "edit":
            //        viewSupplier = (ViewSupplier)Session["supplierMode"];
            //        if (viewSupplier != null)
            //        {
            //            txtCodeSupplier.Text = viewSupplier.Code;
            //            txtDescription.Text = viewSupplier.Description;
            //            txtNameSupplier.Text = viewSupplier.Name;

            //            cboRowStatusSupplier.Value = viewSupplier.RowStatus.ToString();

            //            file.Settings.RootFolder = "~/Purchasing/Temp/" + viewSupplier.LibraryUrl;                      
                        
            //        }
                    
            //        break;

            //    case "validate":
                    
                    
            //        break;

            //    case "save":
            //        Supplier s;

            //        if (!validate())
            //        {
            //            return;
            //        }

            //        using (uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists)))
            //        {                                                
            //            if (Session["supplierMode"] == null)
            //            {                                                     
            //                s = new Supplier(uow);
            //                s.SupplierId = Guid.NewGuid();
            //                s.Code = txtCodeSupplier.Text;
            //                s.RowCreationTimeStamp = DateTime.Now;
            //                s.RowStatus = Char.Parse(cboRowStatusSupplier.Value.ToString());

            //                SupplierProperty spV = new SupplierProperty(uow);
            //                spV.SupplierId = s;
            //                spV.Language = "VN";
            //                spV.Name = txtNameSupplier.Text;
            //                spV.Description = txtDescription.Text;

            //                SupplierProperty spE = new SupplierProperty(uow);
            //                spE.SupplierId = s;
            //                spE.Language = "EN";
            //                spE.Name = txtNameSupplier.Text;
            //                spE.Description = txtDescription.Text;

            //                SupplierDetail sd = new SupplierDetail(uow);
            //                sd.SupplierId = s;
            //                sd.DocumentUrl = Session["url"].ToString();
            //                sd.LibraryUrl = Session["url"].ToString();                          
            //            }
            //            else
            //            {
            //                viewSupplier = (ViewSupplier)Session["supplierMode"];
                  
            //                s = uow.FindObject<Supplier>(new BinaryOperator("SupplierId", viewSupplier.SupplierId));
            //                s.Code = txtCodeSupplier.Text;
            //                s.RowStatus = Char.Parse(cboRowStatusSupplier.Value.ToString());

            //                if (s != null)
            //                {
            //                    CriteriaOperator co = CriteriaOperator.Parse("[SupplierId] = ? And Language = ?", viewSupplier.SupplierId, Session["language"].ToString());
            //                    SupplierProperty sp = uow.FindObject<SupplierProperty>(new BinaryOperator("SupplierId", s));

            //                    if (sp != null)
            //                    {
            //                        sp.Name = txtNameSupplier.Text;
            //                        sp.Description = txtDescription.Text;                                    
            //                    }                                
            //                }
            //            }

            //            uow.CommitChanges();

            //            Session["supplierMode"] = null;
            //            Session["url"] = null;

            //            formSupplierEdit.ShowOnPageLoad = false;
            //        }

            //        break;
            //    case "view":
            //        uow = new UnitOfWork(ConnectionHelper.GetDataLayer(DevExpress.Xpo.DB.AutoCreateOption.SchemaAlreadyExists));
            //        Guid guid = new Guid(aParam[1]);

            //        viewSupplier = uow.FindObject<ViewSupplier>(new BinaryOperator("SupplierId", guid));

            //        if (viewSupplier != null)
            //        {
            //            txtCodeSupplier.Text = viewSupplier.Code;
            //            txtDescription.Text = viewSupplier.Description;
            //            txtNameSupplier.Text = viewSupplier.Name;

            //            cboRowStatusSupplier.Value = viewSupplier.RowStatus;

            //            txtCodeSupplier.ReadOnly = true;
            //            txtDescription.ReadOnly = true;
            //            txtNameSupplier.ReadOnly = true;

            //            cboRowStatusSupplier.ReadOnly = true;

            //            ASPxFileManager fm = (ASPxFileManager) navbarDetail.Items[0].FindControl("fileDetail");
            //            fm.Enabled = false;

            //            buttonAcceptSupplier.Enabled = false;                        
            //        }

            //        formSupplierEdit.ShowOnPageLoad = true;

            //        break;
            //    default:
            //        break;
            //}
        }

        protected void txtCodeSupplier_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            //e.IsValid = validate();
            //if (txtCodeSupplier.ValidationSettings.ErrorText == "")
            //{
            //    e.IsValid = true;
            //}
           
        }

        protected void txtNameSupplier_Validation(object sender, DevExpress.Web.ASPxEditors.ValidationEventArgs e)
        {
            //e.IsValid = validate();
            //if (txtNameSupplier.ValidationSettings.ErrorText == "")
            //{
            //    e.IsValid = true;
            //}
        }

        protected void fileDetail_Init(object sender, EventArgs e)
        {
            //viewSupplier = (ViewSupplier)Session["supplierMode"];

            //if (viewSupplier != null)
            //{
            //    ASPxFileManager file = (ASPxFileManager)sender;
            //    //file.Settings.RootFolder = "~/Purchasing/Temp/" + viewSupplier.LibraryUrl;
            //}
        }
    }
}