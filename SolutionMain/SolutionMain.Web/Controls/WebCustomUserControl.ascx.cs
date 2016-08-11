using System;
using System.Web.UI;
using DevExpress.Xpo;
using DevExpress.Utils;
using E911.Module.Editors;

/// <summary>
/// This is a custom WinForms user control that displays persistent data received from XPO.
/// You do not need to implement the IXpoSessionAwareControl interface if your control gets data from other sources or does not require data at all.
/// </summary>
public partial class WebCustomUserControl : UserControl, IXpoSessionAwareControl {
    void IXpoSessionAwareControl.UpdateDataSource(Session session) {
        //Guard.ArgumentNotNull(session, "session");
        //XpoDataSource dataSource = new XpoDataSource();
        //dataSource.Criteria = "Status = 'NotStarted'";
        //dataSource.TypeName = typeof(DevExpress.Persistent.BaseImpl.Task).FullName;
        //dataSource.Session = session;
        //grid.DataSource = dataSource;
        //grid.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}