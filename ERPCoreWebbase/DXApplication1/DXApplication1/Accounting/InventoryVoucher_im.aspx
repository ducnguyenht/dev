<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="InventoryVoucher_im.aspx.cs" Inherits="ERPCore.Accounting.InventoryVoucher_im" %>
<%@ Register src="UserControl/InventoryVoucherEdit_im.ascx" tagname="InventoryVoucherEdit_im" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    
                <uc1:InventoryVoucherEdit_im ID="InventoryVoucherEdit1" runat="server" />                
    
</asp:Content>
