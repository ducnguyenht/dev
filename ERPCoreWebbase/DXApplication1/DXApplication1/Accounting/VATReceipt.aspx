<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="VATReceipt.aspx.cs" Inherits="WebModule.Accounting.VATReceipt" %>
<%@ Register src="UserControl/uVATReceipt.ascx" tagname="uVATReceipt" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uVATReceipt ID="uVATReceipt1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LeftSubmitContainer" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CenterSubmitContainer" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightSubmitContainer" runat="server">
</asp:Content>
