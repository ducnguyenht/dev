<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TransactionHistory.aspx.cs" Inherits="WebModule.Accounting.TransactionHistory" %>
<%@ Register src="UserControl/ucTransactionHistory.ascx" tagname="ucTransactionHistory" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
        Font-Size="Medium" Height="45px" Text="Lịch Sử Giao Dịch">
    </dx:ASPxLabel>
    <uc1:ucTransactionHistory ID="ucTransactionHistory1" runat="server" />
</asp:Content>
