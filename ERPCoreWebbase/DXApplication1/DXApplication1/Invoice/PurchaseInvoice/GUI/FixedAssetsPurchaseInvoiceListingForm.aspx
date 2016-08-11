<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="FixedAssetsPurchaseInvoiceListingForm.aspx.cs"
    Inherits="WebModule.Invoice.PurchaseInvoice.GUI.FixedAssetsPurchaseInvoiceListingForm" %>

<%@ Register Src="PurchaseInvoiceListingForm.ascx" TagName="PurchaseInvoiceListingForm"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px;">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Large" Text="Phiếu mua tài sản cố định">
        </dx:ASPxLabel>
    </div>
    <uc1:PurchaseInvoiceListingForm ID="purchaseInvoiceListingForm" runat="server" BillType="REAL_ESTATE" />
</asp:Content>
