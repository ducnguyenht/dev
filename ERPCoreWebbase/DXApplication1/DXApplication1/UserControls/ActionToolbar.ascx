<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionToolbar.ascx.cs"
    Inherits="WebModule.UserControls.ActionToolbar" %>
<table class="ActionToolbar">
    <tr>
        <td class="Strut">
            <div style="float: left">
                <dx:ASPxImage ID="ExpandPaneImage" ClientIDMode="Static" runat="server" Cursor="pointer"
                    SpriteCssClass="Sprite_ExpandPane" ToolTip="Expand" AlternateText="Expand" ClientInstanceName="ClientExpandPaneImage">
                    <ClientSideEvents Click="ERPCore.ClientExpandPaneImage_Click" />
                </dx:ASPxImage>
            </div>
            <div style="float: left">
                <dx:ASPxMenu ID="ActionMenu" runat="server" DataSourceID="ActionMenuDataSource" RenderMode="Lightweight"
                    ShowAsToolbar="true" ClientInstanceName="ClientActionMenu" CssClass="ActionMenu"
                    SeparatorWidth="0" OnItemDataBound="ActionMenu_ItemDataBound">
                    <ClientSideEvents ItemClick="ERPCore.ClientActionMenu_ItemClick" />
                    <Border BorderWidth="0" />
                    <SubMenuStyle CssClass="SubMenu" />
                </dx:ASPxMenu>
            </div>
            <div style="float: right">
                <dx:ASPxMenu ID="InfoMenu" EnableCallBacks="True" runat="server" RenderMode="Lightweight"
                    DataSourceID="InfoMenuDataSource" ClientInstanceName="ClientInfoMenu" ShowAsToolbar="True"
                    SeparatorWidth="0px" CssClass="InfoMenu" OnItemDataBound="InfoMenu_OnItemDataBound">
                    <ClientSideEvents ItemClick="ERPCore.ClientInfoMenu_ItemClick" />
                    <Border BorderWidth="0" />
                    <SubMenuStyle CssClass="SubMenu" />
                </dx:ASPxMenu>
            </div>
            <b class="clear"></b>
        </td>
        <td id="SearchBoxSpacer" class="Spacer" runat="server">
            <b></b>
        </td>
        <td>
            <dx:ASPxButtonEdit runat="server" ID="SearchBox" Width="220" Height="31" NullText="Type to Search..."
                CssClass="SearchBox" ClientInstanceName="ClientSearchBox" Font-Size="12px">
                <ClientSideEvents TextChanged="ERPCore.ClientSearchBox_TextChanged" KeyDown="ERPCore.ClientSearchBox_KeyDown"
                    KeyPress="ERPCore.ClientSearchBox_KeyPress" />
                <Buttons>
                    <dx:EditButton>
                        <Image>
                            <SpriteProperties CssClass="Sprite_Search" HottrackedCssClass="Sprite_Search_Hover"
                                PressedCssClass="Sprite_Search_Pressed" />
                        </Image>
                    </dx:EditButton>
                </Buttons>
                <ButtonStyle CssClass="SearchBoxButton" />
                <NullTextStyle Font-Italic="true" />
            </dx:ASPxButtonEdit>
        </td>
        <td>
            <dx:ASPxMenu ID="PersonalMenu" runat="server" RenderMode="Lightweight" CssClass="InfoMenu"
                SeparatorWidth="0px" ShowAsToolbar="True" EnableTheming="True">
                <GutterBackgroundImage Repeat="NoRepeat" />
                <Items>
                    <dx:MenuItem Text="" NavigateUrl="javascript:void();">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Personal" />
                        </Image>
                    </dx:MenuItem>
                </Items>
                <SubMenuStyle GutterImageSpacing="0px" GutterWidth="0px" />
                <Border BorderWidth="0px" />
            </dx:ASPxMenu>
            <dx:ASPxPopupControl PopupElementID="PersonalMenu" ID="pcPersonal" ClientInstanceName="pcPersonal" runat="server"
                RenderMode="Lightweight" AutoUpdatePosition="True" ShowHeader="False" PopupHorizontalAlign="LeftSides"
                PopupVerticalAlign="Below" onwindowcallback="pcPersonal_WindowCallback">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="200px">
                            <Paddings Padding="10px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <%--<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="14px" Text="ABC" 
                                        Font-Bold="True">
                                    </dx:ASPxLabel>--%>
                                    <asp:Label ID="Label1" Font-Size="14px" Font-Bold="True" runat="server" Text="">
                                        <%= Utility.CurrentSession.Instance.FriendlyLoginName %>
                                    </asp:Label>
                                    <br />
                                    <%--<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Size="14px" Text="" 
                                        Font-Bold="True">
                                    </dx:ASPxLabel>--%>
                                    <asp:Label ID="Label2" runat="server" Text="">
                                        <%= Utility.CurrentSession.Instance.LoginEmail %>
                                    </asp:Label>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <dx:ASPxMenu AutoPostBack="false" ID="mnPersonal" ClientInstanceName="mnPersonal" runat="server" Orientation="Vertical"
                            Width="100%" ShowAsToolbar="True" OnItemClick="mnPersonal_ItemClick">
                            <Items>
                                <dx:MenuItem Visible="false" Name="MyAccount" Text="Tài khoản của tôi">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_MyAccount" />
                                    </Image>
                                </dx:MenuItem>
                                <dx:MenuItem Name="SignOut" Text="Thoát">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_SignOut" />
                                    </Image>
                                </dx:MenuItem>
                            </Items>
                            <Border BorderWidth="0px" />
                            <BorderTop BorderWidth="1px" />
                            <ClientSideEvents ItemClick="function(s,e) { 
                                if(e.item.name == 'SignOut') {
                                    e.processOnServer = true; 
                                }
                                else {
                                    alert('Chức năng này đang trong quá trình xây dựng.'); 
                                } }" />
                        </dx:ASPxMenu>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <Border BorderWidth="1px" />
            </dx:ASPxPopupControl>
        </td>
    </tr>
</table>
<asp:XmlDataSource ID="ActionMenuDataSource" runat="server" DataFile="~/App_Data/Actions.xml" />
<asp:XmlDataSource ID="InfoMenuDataSource" runat="server" DataFile="~/App_Data/InfoLayout.xml"
    XPath="Items/Item" />
