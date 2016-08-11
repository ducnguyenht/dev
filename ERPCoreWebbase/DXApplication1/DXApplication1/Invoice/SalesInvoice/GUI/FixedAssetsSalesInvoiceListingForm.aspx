<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="FixedAssetsSalesInvoiceListingForm.aspx.cs" Inherits="WebModule.Invoice.SalesInvoice.GUI.FixedAssetsSalesInvoiceListingForm" %>

<%@ Register Src="SalesInvoiceListingForm.ascx" TagName="SalesInvoiceListingForm"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px;">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Large" Text="Phiếu bán tài sản cố định">
        </dx:ASPxLabel>
    </div>
    <uc1:SalesInvoiceListingForm ID="salesInvoiceListingForm" runat="server" BillType="REAL_ESTATE" />
</asp:Content>

