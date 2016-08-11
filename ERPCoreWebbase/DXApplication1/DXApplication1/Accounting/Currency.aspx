<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="WebModule.Accounting.Currency" %>
<%@ Register src="UserControl/uCurrency.ascx" tagname="uCurrency" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uCurrency ID="uCurrency1" runat="server" />
</asp:Content>
