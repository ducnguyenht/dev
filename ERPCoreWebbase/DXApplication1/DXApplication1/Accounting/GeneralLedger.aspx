<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" 
CodeBehind="GeneralLedger.aspx.cs" Inherits="WebModule.Accounting.GeneralLedger" %>
<%@ Register src="UserControl/ucGeneralLedger.ascx" tagname="ucGeneralLedger" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
        Font-Size="Medium" Text="Nhật Ký Sổ Cái">
    </dx:ASPxLabel>
    <uc1:ucGeneralLedger ID="ucGeneralLedger1" runat="server" />
</asp:Content>