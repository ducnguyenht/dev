<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalTest.aspx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.LocalTest" %>

<%@ Register Src="Control/NASCustomFieldDataGridView.ascx" TagName="NASCustomFieldDataGridView"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="../../../scripts/shortcut.js" type="text/javascript"></script>
    <script src="../../../scripts/script.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxCallbackPanel ID="panel" ClientInstanceName="panel" runat="server" 
            Width="400px" oncallback="panel_Callback">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="Edit">
                        <ClientSideEvents Click="function(s,e) { panel.PerformCallback(); }" />
                    </dx:ASPxButton>
                    <uc1:NASCustomFieldDataGridView ID="gridview" runat="server" />
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
    </form>
</body>
</html>
