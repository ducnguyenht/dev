<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StockCart.aspx.cs" Inherits="WebModule.Warehouse.StockCart" %>
<%@ Register src="UserControl/uInitInventory.ascx" tagname="uInitInventory" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uInitInventory ID="uInitInventory1" runat="server" />
</asp:Content>
