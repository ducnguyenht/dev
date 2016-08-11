using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Web.ASPxGridView;
using Utility;
using NAS.DAL;
using NAS.DAL.Nomenclature.Organization;
using DevExpress.Data.Filtering;
using NAS.DAL.CMS.ObjectDocument;

namespace ERPCore.Purchasing.UserControl
{
    public partial class uManufacturerEdit : System.Web.UI.UserControl
    {

        private class PrivateSession
        {
            //private constructor
            private PrivateSession()
            {
                this.ManufacturerOrgId = Guid.Empty;
            }
            // Gets the current session
            public static PrivateSession Instance
            {
                get
                {
                    PrivateSession session =
                        (PrivateSession)HttpContext.Current.Session["ERPCore_Purchasing_UserControl_uManufacturerEdit"];
                    if (session == null)
                    {
                        session = new PrivateSession();
                        HttpContext.Current.Session["ERPCore_Purchasing_UserControl_uManufacturerEdit"] = session;
                    }
                    return session;
                }
            }

            /// <summary>
            /// Clear instance of PrivateSession
            /// </summary>
            public static void ClearInstance()
            {
                HttpContext.Current.Session.Remove("ERPCore_Purchasing_UserControl_uManufacturerEdit");
            }

            /////Declares all session properties here
            public Guid ManufacturerOrgId { get; set; }
            public string CurrentManufacturerOrgCode { get; set; }

        }

        private Session session;

        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            dsManufacturer.Session = session;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pagMunufacturer.ActiveTabIndex = 0;
            }
            //2013-11-22 Khoa.Truong DEL START
            //gridviewCustomFields.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            //gridviewCustomFields.SettingsPager.PageSize = 20;
            //2013-11-22 Khoa.Truong DEL END
            frmManufacturerEdit.DataBind();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            session.Dispose();
        }

        protected void cpLineManufacturer_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {

        }

        private void ClearForm()
        {
            //clear all fields inside form layout
            ASPxEdit.ClearEditorsInContainer(frmManufacturerEdit);
            pagMunufacturer.ActiveTabIndex = 0;
            cbRowStatus.SelectedIndex = 0;
            txtCode.IsValid = true;
            txtName.IsValid = true;
        }


        protected void popManufacturerEdit_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "new":

                    ManufacturerOrg tempManufacturerOrg = ManufacturerOrg.InitNewRow(session);
                    PrivateSession.Instance.ManufacturerOrgId = tempManufacturerOrg.OrganizationId;
                    frmManufacturerEdit.DataSourceID = "dsManufacturer";
                    dsManufacturer.CriteriaParameters["ManufacturerOrgId"].DefaultValue = PrivateSession.Instance.ManufacturerOrgId.ToString();
                    ClearForm();
                    //Get object id
                    //Bind data to gridview

                    #region add manufacturer
                    session.BeginTransaction();
                    try
                    {
                        //ObjectType
                        ObjectType objectType =
                            ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUFACTURER);

                        // object
                        NAS.BO.CMS.ObjectDocument.ObjectBO objectBO = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                        NAS.DAL.CMS.ObjectDocument.Object cmsobject =
                            objectBO.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUFACTURER);

                        // OrganizationObject
                        OrganizationObject organizatoinObject = new OrganizationObject(session)
                        {
                            ObjectId = cmsobject,
                            OrganizationId = tempManufacturerOrg
                        };
                        organizatoinObject.Save();

                        // OrganizationCustomType
                        OrganizationCustomType organizationCustomType = new OrganizationCustomType(session)
                        {
                            ObjectTypeId = objectType,
                            OrganizationId = tempManufacturerOrg
                        };
                        organizationCustomType.Save();
                        session.CommitTransaction();
                    }
                    catch
                    {
                        session.RollbackTransaction();
                    }


                    OrganizationObject organizationObject = tempManufacturerOrg.OrganizationObjects.FirstOrDefault();
                    grid_of_Manufacturer.CMSObjectId = organizationObject.ObjectId.ObjectId;
                    grid_of_Manufacturer.DataBind();
                    #endregion

                    //2013-11-22 Khoa.Truong DEL START
                    //gridviewCustomFields.CMSObjectId = CurrentManufacturerOrg.ObjectId.ObjectId;
                    //gridviewCustomFields.DataBind();
                    //2013-11-22 Khoa.Truong DEL END
                    break;
                case "edit":
                    ClearForm();
                    frmManufacturerEdit.DataSourceID = "dsManufacturer";
                    if (args.Length > 1)
                    {
                        PrivateSession.Instance.ManufacturerOrgId = Guid.Parse(args[1]);
                        dsManufacturer.CriteriaParameters["ManufacturerOrgId"].DefaultValue = PrivateSession.Instance.ManufacturerOrgId.ToString();
                        txtCode.Text = CurrentManufacturerOrg.Code;
                        //Get object id
                        //Bind data to gridview

                        #region edit manufacturer
                        if (CurrentManufacturerOrg.OrganizationObjects.FirstOrDefault() == null)
                        {
                            session.BeginTransaction();
                            try
                            {
                                ObjectType objectType1 =
                                ObjectType.GetDefault(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUFACTURER);

                                // object
                                NAS.BO.CMS.ObjectDocument.ObjectBO objectBO1 = new NAS.BO.CMS.ObjectDocument.ObjectBO();
                                NAS.DAL.CMS.ObjectDocument.Object cmsobject1 =
                                    objectBO1.CreateCMSObject(session, NAS.DAL.CMS.ObjectDocument.ObjectTypeEnum.MANUFACTURER);

                                OrganizationObject organizatoinObject1 = new OrganizationObject(session)
                                {
                                    ObjectId = cmsobject1,
                                    OrganizationId = CurrentManufacturerOrg
                                };
                                organizatoinObject1.Save();

                                // OrganizationCustomType
                                OrganizationCustomType organizationCustomType1 = new OrganizationCustomType(session)
                                {
                                    ObjectTypeId = objectType1,
                                    OrganizationId = CurrentManufacturerOrg
                                };
                                organizationCustomType1.Save();
                                session.CommitTransaction();
                            }
                            catch (Exception)
                            {
                                session.RollbackTransaction();
                                throw;
                            }

                            OrganizationObject organizationObject1 = CurrentManufacturerOrg.OrganizationObjects.FirstOrDefault();
                            grid_of_Manufacturer.CMSObjectId = organizationObject1.ObjectId.ObjectId;
                            grid_of_Manufacturer.DataBind();
                        }

                        else
                        {
                            OrganizationObject organizationObject1 = CurrentManufacturerOrg.OrganizationObjects.FirstOrDefault();
                            grid_of_Manufacturer.CMSObjectId = organizationObject1.ObjectId.ObjectId;
                            grid_of_Manufacturer.DataBind();
                        }
                        #endregion



                        //2013-11-22 Khoa.Truong DEL START
                        //gridviewCustomFields.CMSObjectId = CurrentManufacturerOrg.ObjectId.ObjectId;
                        //gridviewCustomFields.DataBind();
                        //2013-11-22 Khoa.Truong DEL END
                    }
                    break;
                case "save":
                    bool isSuccess = true;
                    string recordIdStr = null;
                    try
                    {
                        //Check validation
                        if (!ASPxEdit.AreEditorsValid(pagMunufacturer, true))
                        {
                            popManufacturerEdit.JSProperties.Add("cpInvalid", true);
                            pagMunufacturer.ActiveTabIndex = 0;
                            return;
                        }
                        //Logic to save data 
                        if (args.Length > 1)
                        {
                            //Update mode
                            //Update general information
                            recordIdStr = args[1];
                            Guid recordId = Guid.Parse(recordIdStr);
                            ManufacturerOrg editManufacturerOrg =
                                session.GetObjectByKey<ManufacturerOrg>(PrivateSession.Instance.ManufacturerOrgId);
                            editManufacturerOrg.Code = txtCode.Text;
                            editManufacturerOrg.Name = txtName.Text;
                            editManufacturerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            editManufacturerOrg.Save();
                        }
                        else
                        {
                            //Insert mode
                            ManufacturerOrg newManufacturerOrg =
                                session.GetObjectByKey<ManufacturerOrg>(PrivateSession.Instance.ManufacturerOrgId);
                            newManufacturerOrg.Code = txtCode.Text;
                            newManufacturerOrg.Name = txtName.Text;
                            newManufacturerOrg.RowStatus = short.Parse(cbRowStatus.SelectedItem.Value.ToString());
                            newManufacturerOrg.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        throw;
                    }
                    finally
                    {
                        popManufacturerEdit.JSProperties.Add("cpCallbackArgs",
                                    String.Format("{{ \"recordId\": \"{0}\", \"isSuccess\": {1} }}", recordIdStr, isSuccess.ToString().ToLower()));
                    }
                    break;
                default:
                    break;
            }
        }

        private ManufacturerOrg CurrentManufacturerOrg
        {
            get
            {
                if (PrivateSession.Instance.ManufacturerOrgId == Guid.Empty)
                {
                    return null;
                }
                return
                    session.GetObjectByKey<ManufacturerOrg>(PrivateSession.Instance.ManufacturerOrgId);
            }
        }

        protected void txtCode_Validation(object sender, ValidationEventArgs e)
        {
            String manufacturerCode = e.Value.ToString().Trim();
            //New mode
            if (PrivateSession.Instance.ManufacturerOrgId == Guid.Empty)
            {
                bool isExist = Util.isExistXpoObject<ManufacturerOrg>("Code", manufacturerCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                if (isExist)
                {
                    e.IsValid = false;
                    e.ErrorText =
                        String.Format("Mã nhà sản xuất '{0}' đã được sử dụng", manufacturerCode);
                }
                else
                {
                    e.IsValid = true;
                    e.ErrorText = String.Empty;
                }
            }
            //Edit mode  
            else
            {
                //Validate if new code not equal old code
                if (!manufacturerCode.Equals(CurrentManufacturerOrg.Code))
                {
                    bool isExist = Util.isExistXpoObject<ManufacturerOrg>("Code", manufacturerCode,
                            Constant.ROWSTATUS_ACTIVE, Constant.ROWSTATUS_DEFAULT, Constant.ROWSTATUS_INACTIVE);
                    if (isExist)
                    {
                        e.IsValid = false;
                        e.ErrorText = String.Format("Mã nhà sản xuất '{0}' đã được sử dụng", manufacturerCode);
                    }
                    else
                    {
                        e.IsValid = true;
                        e.ErrorText = String.Empty;
                    }
                }
            }
        }
    }
}