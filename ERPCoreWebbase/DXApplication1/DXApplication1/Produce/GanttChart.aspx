<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GanttChart.aspx.cs" Inherits="WebModule.Produce.GanttChart" %>
<%@ Register src="UserControl/uGanttChart.ascx" tagname="uGanttChart" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uGanttChart ID="uGanttChart1" runat="server" />
</asp:Content>
