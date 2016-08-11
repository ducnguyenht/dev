<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GeneralAccount.aspx.cs" Inherits="WebModule.Accounting.GeneralAccount" %>
<%@ Register src="UserControl/uGeneralAccount.ascx" tagname="uGeneralAccount" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uGeneralAccount ID="uGeneralAccount1" runat="server" />
</asp:Content>
