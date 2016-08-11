<%-- BeginRegion TagPrefix and control properties --%>
<%@ Control Language="C#" AutoEventWireup="True" Inherits="WebCustomUserControl" Codebehind="WebCustomUserControl.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors"  TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPager" TagPrefix="dxwp" %>
<%-- EndRegion --%>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" AutoGenerateColumns="True">
    </dxwgv:ASPxGridView>
