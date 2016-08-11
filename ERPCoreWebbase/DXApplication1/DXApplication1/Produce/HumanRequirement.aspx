<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="HumanRequirement.aspx.cs" Inherits="WebModule.Produce.HumanRequirement" %>
<%@ Register src="UserControl/uHumanRequirement.ascx" tagname="uHumanRequirement" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uHumanRequirement ID="uHumanRequirement1" runat="server" />
</asp:Content>