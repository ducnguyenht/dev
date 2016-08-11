<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookingTest.aspx.cs" Inherits="WebModule.Test.BookingTest" %>

<%@ Register src="../Invoice/PurchaseInvoice/Control/BookingEntriesForm/BookingEntriesForm.ascx" tagname="BookingEntriesForm" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" AutoPostBack="false">
            <ClientSideEvents Click="function(s, e) {
	BookingEntriesFromBill('F434AE21-8CAE-4B24-BE8E-A0DF794DF9BF');
}" />
        </dx:ASPxButton>
    
    </div>
    <uc1:BookingEntriesForm ID="BookingEntriesForm1" runat="server" />
    </form>
</body>
</html>
