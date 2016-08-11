<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="ProductSalesInvoiceListingForm.aspx.cs" Inherits="WebModule.Invoice.SalesInvoice.GUI.ProductSalesInvoiceListingForm" %>

<%@ Register Src="SalesInvoiceListingForm.ascx" TagName="SalesInvoiceListingForm"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px;">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Large" Text="Phiếu bán hàng hóa">
        </dx:ASPxLabel>
    </div>
    <uc1:SalesInvoiceListingForm ID="salesInvoiceListingForm" runat="server" BillType="PRODUCT" />
</asp:Content>

