<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingForm.aspx.cs" Inherits="WebModule.Invoice.TestingForm" %>

<%@ Register Src="BookingEntriesPopup.ascx" TagName="BookingEntriesPopup" TagPrefix="uc1" %>
<%@ Register src="Control/TradingItemExtraInformation/TradingItemExtraInformation.ascx" tagname="TradingItemExtraInformation" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="../scripts/shortcut.js" type="text/javascript"></script>
    <script src="../scripts/script.js" type="text/javascript"></script>
    <link href="../Content/sprite.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <uc2:TradingItemExtraInformation ID="TradingItemExtraInformation1" 
            runat="server" ClientInstanceName="info" />
        
    </div>
    </form>
</body>
</html>
