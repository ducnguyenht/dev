<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PhieuBan.aspx.cs" Inherits="WebModule.Accounting.PhieuBan" %>
<%@ Register src="UserControl/uPhieuBanHang.ascx" tagname="uPhieuBanHang" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uPhieuBanHang ID="uPhieuBanHang1" runat="server" />
</asp:Content>


