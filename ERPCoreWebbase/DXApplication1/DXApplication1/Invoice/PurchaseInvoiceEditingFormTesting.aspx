<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseInvoiceEditingFormTesting.aspx.cs"
    Inherits="WebModule.Invoice.PurchaseInvoiceEditingFormTesting" %>

<%@ Register src="PurchaseInvoice/GUI/PurchaseInvoiceEditingForm.ascx" tagname="PurchaseInvoiceEditingForm" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="../scripts/shortcut.js" type="text/javascript"></script>
    <script src="../scripts/script.js" type="text/javascript"></script>
    <link href="../Content/styles.css" rel="stylesheet" type="text/css" />
    <link href="../Content/sprite.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <uc1:PurchaseInvoiceEditingForm ID="PurchaseInvoiceEditingForm1" 
            runat="server" ClientInstanceName="purchaseInvoiceEditingForm" 
            BillType="PRODUCT" />
        <dx:ASPxButton AutoPostBack="false" ID="ASPxButton1" runat="server" Text="New">
            <ClientSideEvents Click="function(s, e) { purchaseInvoiceEditingForm.Show(); }" />
        </dx:ASPxButton>
        <dx:ASPxButton AutoPostBack="false" ID="ASPxButton2" runat="server" Text="Edit">
            <ClientSideEvents Click="function(s, e) { purchaseInvoiceEditingForm.Show('35D1A8E0-AC39-41FF-A8AA-F5380831D75D'); }" />
        </dx:ASPxButton>
    </div>
    </form>
</body>
</html>
