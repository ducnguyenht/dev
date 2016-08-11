<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ProdctPriceInventCaculation.aspx.cs" Inherits="WebModule.Accounting.ProdctPriceInventCaculation" %>
<%@ Register src="UserControl/ProdctPriceInventCaculation.ascx" tagname="ProdctPriceInventCaculation" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainHolder" runat="server">
    
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="11pt" 
        Text="Phương pháp tính giá tồn kho">
    </dx:ASPxLabel>
    <uc1:ProdctPriceInventCaculation ID="ProdctPriceInventCaculation1" 
        runat="server" />
    
</asp:Content>
