<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MaterialRequirement.aspx.cs" Inherits="WebModule.Produce.MaterialRequirement" %>
<%@ Register src="UserControl/uMaterialRequirement.ascx" tagname="uMaterialRequirement" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uMaterialRequirement ID="uMaterialRequirement1" runat="server" />
</asp:Content>