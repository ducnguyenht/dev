<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ProductionCommand.aspx.cs" Inherits="WebModule.Produce.ProductionCommand" %>
<%@ Register src="UserControl/uProductionCommand.ascx" tagname="uProductionCommand" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uProductionCommand ID="uProductionCommand1" runat="server" />
</asp:Content>
