<%@ Page Title="" Language="C#" ClientIDMode="AutoID" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="ProductionCommandExecution.aspx.cs" Inherits="WebModule.Produce.ProductionCommandExecution" %>

<%@ Register Src="UserControl/uProductionCommandExecution.ascx" TagName="uProductionCommandExecution"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uProductionCommandExecution ID="uProductionCommandExecution1" runat="server" />
</asp:Content>
