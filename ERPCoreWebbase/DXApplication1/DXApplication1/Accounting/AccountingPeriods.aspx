<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AccountingPeriods.aspx.cs" Inherits="WebModule.Accounting.AccountingPeriods" %>
<%@ Register src="UserControl/ucAccountingPeriod.ascx" tagname="ucAccountingPeriod" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:ucAccountingPeriod ID="ucAccountingPeriod1" runat="server" />
</asp:Content>
