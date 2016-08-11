<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="AccountingEntry.aspx.cs" Inherits="WebModule.Accounting.AccountingEntry" %>
<%@ Register src="UserControl/ucAccountEntry.ascx" tagname="ucAccountEntry" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
        Font-Size="Medium" Height="45px" Text="Bút Toán">
    </dx:ASPxLabel>
    <uc1:ucAccountEntry ID="ucAccountEntry1" runat="server" />
</asp:Content>

