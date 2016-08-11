<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testPage.aspx.cs" Inherits="WebModule.testPage" %>

<%@ Register src="Purchasing/UserControl/ucPaymentPlanning.ascx" tagname="ucPaymentPlanning" tagprefix="uc1" %>
<%@ Register src="Purchasing/UserControl/ucDeliverySchedule.ascx" tagname="ucDeliverySchedule" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
    <link href="styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Content/sprite.css" rel="stylesheet" type="text/css" />
    <link href="Content/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:ucPaymentPlanning ID="ucPaymentPlanning1" runat="server" />
    
        <uc2:ucDeliverySchedule ID="ucDeliverySchedule1" runat="server" />
    
    </div>
    </form>
</body>
</html>
