using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Entity;
using DevExpress.Web.ASPxEditors;
using BLL.SystemConfig;

namespace WebModule.NAANAdmin.SystemConfig.UserControls
{
    public partial class ucDbConnection : System.Web.UI.UserControl
    {

        DbConfigBLO dbConfigBLO;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbConfigBLO = new DbConfigBLO();
            if (!IsPostBack)
            {
                pagDbConnection.ActiveTabIndex = 0;
                LoadActiveDbConfig();
            }
            
        }

        public void LoadActiveDbConfig()
        {
            MSSQLDbConfigEntity entity = dbConfigBLO.getActiveDbConfig();
            if (entity != null)
            {
                txtMSSQLServer.Text = entity.Server;
                txtMSSQLDbName.Text = entity.Database;
                txtMSSQLUsername.Text = entity.UserId;
                txtMSSQLPassword.Text = entity.Password;
                radSQLServerAuth.Checked = entity.isAuth;
                radWindowsAuth.Checked = !entity.isAuth;
            }
        }

        protected void Reset()
        {
            LoadActiveDbConfig();
        }

        public MSSQLDbConfigEntity getInputData()
        {
            MSSQLDbConfigEntity entity = new MSSQLDbConfigEntity();
            entity.Server = txtMSSQLServer.Text;
            entity.Database = txtMSSQLDbName.Text;
            entity.UserId = txtMSSQLUsername.Text;
            entity.Password = txtMSSQLPassword.Text;
            entity.isAuth = radSQLServerAuth.Checked;
            entity.Status = "active";
            return entity;
        }

        protected bool isServerConnected()
        {
            return getInputData().isServerConnected();
        }

        protected void Save()
        {
            dbConfigBLO.Save(getInputData());
        }

        protected void pagDbConnection_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] args = e.Parameter.Split('|');
            switch (args[0])
            {
                case "reset":
                    Reset();
                    break;
                case "save":
                    bool isSaveOk = true;
                    try
                    {
                        Save();
                        NAS.DAL.XpoHelper.ClearDataLayer();
                    }
                    catch (Exception)
                    {
                        isSaveOk = false;
                    }
                    finally
                    {
                        pagDbConnection.JSProperties.Add("cpSaveOk", isSaveOk.ToString().ToLower());
                    }
                    break;
                case "check":
                    bool isCheckOk = true;
                    try
                    {
                        if (isServerConnected())
                        {
                            isCheckOk = true;
                        }
                        else
                        {
                            isCheckOk = false;
                        }
                    }
                    catch (Exception)
                    {
                        isCheckOk = false;
                    }
                    finally
                    {
                        pagDbConnection.JSProperties.Add("cpCheckOk", isCheckOk.ToString().ToLower());
                    }
                    break;
                case "populate":
                    bool isPopulateOk = true;
                    try
                    {
                        NAS.DAL.Util.Populate();
                    }
                    catch (Exception)
                    {
                        isPopulateOk = false;
                    }
                    finally
                    {
                        pagDbConnection.JSProperties.Add("cpPopulateOk", isPopulateOk.ToString().ToLower());
                    }
                    break;
                default:
                    break;
            }
        }

        protected void txtMSSQLPassword_PreRender(object sender, EventArgs e)
        {
            ASPxTextBox edit = sender as ASPxTextBox;
            edit.ClientSideEvents.Init = "function(s, e) {s.SetText('" + edit.Text + "');}";
        }
    }
}