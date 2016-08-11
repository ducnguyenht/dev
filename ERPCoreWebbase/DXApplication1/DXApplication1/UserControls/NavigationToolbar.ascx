<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationToolbar.ascx.cs" Inherits="WebModule.UserControls.NavigationToolbar" %>
<table class="NavigationToolbar"><tr><td style="width: 100%">
    <dx:ASPxMenu ID="NavigationMenu" runat="server" RenderMode="Lightweight" CssClass="NavigationMenu"
        OnInit="NavigationMenu_Init" ShowAsToolbar="True" AppearAfter="5000" 
        HorizontalAlign="Left" AllowSelectItem="True">
        <ItemStyle Font-Size="19px" Wrap="True" />
        <SubMenuStyle CssClass="SubMenu" />
        <SubMenuItemStyle Font-Size="15px" />
        <Border BorderWidth="0" />
    </dx:ASPxMenu>
</td><td>
    <dx:ASPxImage ID="CollapsePaneImage" ClientIDMode="Static" runat="server" Cursor="pointer" SpriteCssClass="Sprite_CollapsePane" ToolTip="Collapse" AlternateText="Collapse" ClientInstanceName="ClientCollapsePaneImage">
        <ClientSideEvents Click="ERPCore.ClientCollapsePaneImage_Click" />
    </dx:ASPxImage>
</td></tr></table>