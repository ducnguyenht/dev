<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestItemSetting.aspx.cs" Inherits="WebModule.Nomenclature.UserControl.ItemSetting.TestItemSetting" %>

<%@ Register src="uItemSetting.ascx" tagname="uItemSetting" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:uItemSetting ID="uItemSetting1" runat="server" />
        <dx:ASPxButton ID="btnTest" runat="server" Text="Test" AutoPostBack="false">
            <ClientSideEvents Click="function(s, e){ formProductEdit.Show(); }" />
        </dx:ASPxButton>
    </div>
    </form>
</body>
</html>

