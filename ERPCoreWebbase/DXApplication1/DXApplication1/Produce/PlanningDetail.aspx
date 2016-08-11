<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PlanningDetail.aspx.cs" Inherits="WebModule.Produce.PlanningDetail" %>
<%@ Register src="UserControl/uPlanningDetail.ascx" tagname="uPlanningDetail" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uPlanningDetail ID="uPlanningDetail1" runat="server" />
</asp:Content>
