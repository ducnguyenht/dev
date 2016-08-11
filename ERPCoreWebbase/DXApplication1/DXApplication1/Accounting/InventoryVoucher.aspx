<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="InventoryVoucher.aspx.cs" Inherits="ERPCore.Accounting.InventoryVoucher" %>
<%@ Register src="UserControl/InventoryVoucherEdit.ascx" tagname="InventoryVoucherEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    
                <uc1:InventoryVoucherEdit ID="InventoryVoucherEdit1" runat="server" />                
    
</asp:Content>
