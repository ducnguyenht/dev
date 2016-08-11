<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="EquipmentRequirement.aspx.cs" Inherits="WebModule.Produce.EquipmentRequirement" %>

<%@ Register Src="UserControl/uEquipmentRequirement.ascx" TagName="uEquipmentRequirement"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uEquipmentRequirement ID="uEquipmentRequirement1" runat="server" />
</asp:Content>