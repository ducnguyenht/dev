<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FunctionNavBar.ascx.cs"
    Inherits="WebModule.UserControls.FunctionNavBar" %>
<dx:ASPxNavBar CssClass="nav-left" ID="ASPxNavBar1" 
    ClientInstanceName="navSideBar" runat="server" DataSourceID="FunctionsXmlDataSource"
    RenderMode="Lightweight" Width="100%" Height="100%" AllowSelectItem="True">
    <Paddings Padding="0px" />
    <GroupHeaderStyle>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
    </GroupHeaderStyle>
    <GroupContentStyle>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
    </GroupContentStyle>
</dx:ASPxNavBar>
<asp:XmlDataSource ID="FunctionsXmlDataSource" runat="server" DataFile="~/App_Data/Functions.xml">
</asp:XmlDataSource>
