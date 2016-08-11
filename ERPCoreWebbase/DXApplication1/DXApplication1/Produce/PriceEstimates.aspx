<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PriceEstimates.aspx.cs" Inherits="WebModule.Produce.PriceEstimates" %>
<%@ Register src="UserControl/uPriceEstimates.ascx" tagname="uPriceEstimates" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uPriceEstimates ID="uPriceEstimates1" runat="server" />
</asp:Content>
