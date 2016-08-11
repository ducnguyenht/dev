using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using BLL.NASID;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using NAS.DAL;
using System.Net.Mail;
using System.Net;
//using DAL.NASID;

namespace DXApplication1.GUI
{
    public partial class invite_newuser : System.Web.UI.UserControl
    {
        //private OrganizationBLO organizationBLO;
        private Session session;
        protected void Page_Init(object sender, EventArgs e)
        {
            session = XpoHelper.GetNewSession();
            ds_Department.Session = session;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ////organizationBLO = new OrganizationBLO(session);
            ////Guid root = Guid.Parse("f3ca4e28-3bb4-47ec-8673-5bc5c8bd43ed");
            ////trlOrganization.DataSource = organizationBLO.getOrganizationHierachy(root);
            ////trlOrganization.DataBind();
            //var datasource = new[] {
            //    new { OrganizationId = 1, ParentOrganizationId = 0, Name = "Phòng Nhân sự" },
            //    new { OrganizationId = 2, ParentOrganizationId = 0, Name = "Phòng Kế toán" },
            //    new { OrganizationId = 3, ParentOrganizationId = 2, Name = "Bộ phận Kế toán tổng hợp" },
            //    new { OrganizationId = 4, ParentOrganizationId = 2, Name = "Bộ phận Kế toán nội bộ" },
            //};
            ////var datasource = new[] {
            ////    new { OrganizationId = 1, ParentOrganizationId = 0, Name = "Công ty CP TM Dược Sâm Ngọc Linh Quảng Nam" },
            ////    new { OrganizationId = 2, ParentOrganizationId = 1, Name = "Đại lý 1" },
            ////    new { OrganizationId = 3, ParentOrganizationId = 1, Name = "Đại lý 2" },
            ////    new { OrganizationId = 4, ParentOrganizationId = 1, Name = "Đại lý 3" },
            ////    new { OrganizationId = 5, ParentOrganizationId = 1, Name = "Đại lý 4" },
            ////    new { OrganizationId = 6, ParentOrganizationId = 1, Name = "Đại lý 5" },
            ////    new { OrganizationId = 7, ParentOrganizationId = 1, Name = "Đại lý 6" },
            ////    new { OrganizationId = 8, ParentOrganizationId = 1, Name = "Đại lý 7" },
            ////    new { OrganizationId = 9, ParentOrganizationId = 1, Name = "Đại lý 8" },
            ////    new { OrganizationId = 10, ParentOrganizationId = 1, Name = "Đại lý 9" }
            ////};
            //trlOrganization.DataSource = datasource;
            //trlOrganization.DataBind();

        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            //session.Dispose();
        }

        //Send mail
        public void SendMail(String address, String content)
        {
            // Gmail Address from where you send the mail
            var fromAddress = "bataipro@gmail.com";
            // any address where the email will be sending
            var toAddress = address;
            //Password of your gmail address
            const String fromPassword = "ng0batai";
            // Passing the values and make a email formate to display
            String subject = "Mail Invite";
            content = contentMail.Html;
            String body = content;
            
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }

        protected void cp_InviteUser_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {     
            switch (e.Parameter)
            {
                case "insertUserInvite":
                    //Insert Person
                    NAS.DAL.Nomenclature.Organization.Person newPerson = new NAS.DAL.Nomenclature.Organization.Person(session);
                    newPerson.Code = tbx_UserInvite.Text;
                    newPerson.Name = tbx_UserInvite.Text;
                    newPerson.Description = "";
                    newPerson.RowCreationTimeStamp = DateTime.Now;
                    newPerson.RowStatus = Utility.Constant.ROWSTATUS_INACTIVE;
                    newPerson.Save();

                    //Insert LoginAccount
                    NAS.DAL.Nomenclature.Organization.LoginAccount newAccount = new NAS.DAL.Nomenclature.Organization.LoginAccount(session);
                    newAccount.RowCreationTimeStamp = DateTime.Now;
                    newAccount.Email = tbx_Email.Text;
                    newAccount.PersonId = newPerson;
                    newAccount.RowStatus = Utility.Constant.ROWSTATUS_INACTIVE;
                    newAccount.Save();

                    //Insert DepartmentPerson
                    if (ckb_Invite.Checked)
                    {

                        NAS.DAL.Nomenclature.Organization.DepartmentPerson newDepartmentPerson = new NAS.DAL.Nomenclature.Organization.DepartmentPerson(session);
                        Guid guid = Guid.Parse(trlDepartment.FocusedNode.GetValue(trlDepartment.KeyFieldName).ToString());//.FindNodeByKeyValue("DepartmentId").ToString());

                        CriteriaOperator filter = new BinaryOperator("DepartmentId", guid, BinaryOperatorType.Equal);
                        NAS.DAL.Nomenclature.Organization.Department depart = session.FindObject<NAS.DAL.Nomenclature.Organization.Department>(filter);

                        if (depart != null)
                        {
                            newDepartmentPerson.DepartmentId = depart;
                        }                        
                                                
                        newDepartmentPerson.PersonId = newPerson;
                        //newDepartmentPerson.RowCreationTimeStamp = DateTime.Now;
                        newDepartmentPerson.RowStatus = Utility.Constant.ROWSTATUS_INACTIVE;
                        newDepartmentPerson.Save();
                    }               
                    
                    //Send mail
                    string eMail = tbx_Email.Text;                    
                    SendMail(eMail, contentMail.Html);
                    //Response.Write("<script LANGUAGE='JavaScript' >alert('Gửi thư mời thành công!')</script>");
                    break;
            }
        }
    }
}