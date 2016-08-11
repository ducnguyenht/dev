<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="DXApplication1.NASID.ForgotPassword" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxSplitter ID="MainSplitter" runat="server" Height="70px" Width="100%" FullscreenMode="false"
        Orientation="Vertical">
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
                <Border BorderWidth="0px" />
            </Pane>
        </Styles>
        <Panes>
            <dx:SplitterPane Name="HeaderPane" Size="87px">
                <PaneStyle>
                    <BorderBottom BorderWidth="1px" />
                </PaneStyle>
                <ContentCollection>
                    <dx:SplitterContentControl>
                        <div class="header">
                            <div class="title">
                                <img alt="banner" src="../images/NASID/NASERPBanner.png" />
                            </div>
                        </div>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
    <div>
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
            Width="100%" Height="495px">
            <Items>
                <dx:LayoutGroup Caption="Thông tin" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right"
                    ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="E-mail" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="Callbackpanel"
                                        runat="server" Width="200px" OnCallback="ASPxCallbackPanel1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox runat="server" ID="emailtb" ClientInstanceName="emailtb" 
                                                    Width="170" OnValidation="emailtb_Validation">
                                                    <ClientSideEvents Validation="function(s, e) {
	Callbackpanel.PerformCallback();
}" />
                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" EnableCustomValidation="true"
                                                        ErrorTextPosition="Bottom" SetFocusOnError="true">
                                                        <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                </dx:LayoutGroup>
                <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right">
                    <Items>
                        <dx:LayoutItem Caption=" ">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCaptcha runat="server" ID="captcha" TextBox-Position="Bottom" TextBox-ShowLabel="false"
                                        TextBoxStyle-Width="170">
                                        <TextBoxStyle Width="170px"></TextBoxStyle>
                                        <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                            <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        </ValidationSettings>
                                        <RefreshButton Text="Làm mới">
                                        </RefreshButton>
                                        <TextBox Position="Bottom" ShowLabel="False"></TextBox>
                                        <ChallengeImage ForegroundColor="#000000">
                                        </ChallengeImage>
                                    </dx:ASPxCaptcha>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Height="20" />
                        <dx:LayoutItem Caption=" ">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton runat="server" ID="btsubmit" Text="Xác nhận" Width="100px" 
                                        OnClick="btsubmit_Click" CausesValidation="False" />
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="110px" Width="100%" FullscreenMode="false"
        Orientation="Vertical">
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
                <Border BorderWidth="0px" />
            </Pane>
        </Styles>
        <Panes>
            <dx:SplitterPane Name="FooterPane" Size="40px" PaneStyle-BackColor="#EDEDED">
                <Separator Visible="False">
                </Separator>
                <PaneStyle BackColor="#EDEDED">
                </PaneStyle>
                <ContentCollection>
                    <dx:SplitterContentControl>
                        <img style="padding-top: 20px; height: 24px;" alt="footer" src="../images/NASID/NASERPFooter.png" />
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
    </form>
</body>
</html>
