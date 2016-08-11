<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ProduceRequirement.aspx.cs" Inherits="WebModule.Produce.ProduceRequirement" %>
<%@ Register src="UserControl/uProduceRequirement.ascx" tagname="uProduceRequirement" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uProduceRequirement ID="uProduceRequirement1" runat="server" />
</asp:Content>