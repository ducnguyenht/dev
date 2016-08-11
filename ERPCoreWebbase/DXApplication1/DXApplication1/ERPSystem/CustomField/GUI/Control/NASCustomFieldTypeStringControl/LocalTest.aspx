<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalTest.aspx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeStringControl.LocalTest" %>

<%@ Register Src="NASCustomFieldTypeStringControl.ascx" TagName="NASCustomFieldTypeStringControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../../../scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="../../../../../scripts/shortcut.js" type="text/javascript"></script>
    <script src="../../../../../scripts/script.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <uc1:NASCustomFieldTypeStringControl ID="NASCustomFieldTypeStringControl1" runat="server" />
        </div>
        <div>
            <uc1:NASCustomFieldTypeStringControl ID="TestControl" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
