<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DXApplication1.NASID.Login" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxFormLayout" tagprefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
<dx:ASPxSplitter ID="MainSplitter" runat="server" Height="70px" Width="100%" FullscreenMode="false" Orientation="Vertical">
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
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
            AlignItemCaptionsInAllGroups="true" Width="100%" Height="495px">
            <Items>
               <dx:LayoutGroup Caption="" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right" ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="E-mail" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox runat="server" ID="tbemail"  ClientInstanceName="tbemail" Width="170" >
                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" EnableCustomValidation="true"
                                                        ErrorTextPosition="Bottom" SetFocusOnError="true">
                                                        <RegularExpression ErrorText="Invalid e-mail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                        <RequiredField IsRequired="True" ErrorText="The email is required" />
                                     </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection> 
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Password" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
	                                    Width="170px">
                                        <ValidationSettings  ErrorDisplayMode="Text" Display="Dynamic" EnableCustomValidation="true"
                                                        ErrorTextPosition="Bottom" SetFocusOnError="true">
	                                        <RequiredField ErrorText="Password is required." IsRequired="true" />
	                                    </ValidationSettings>
	                                </dx:ASPxTextBox>
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
                                    <dx:ASPxCaptcha runat="server" ID="captcha" TextBox-Position="Bottom" TextBox-ShowLabel="false" TextBoxStyle-Width="170">
<TextBoxStyle Width="170px"></TextBoxStyle>

                                        <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" SetFocusOnError="true">
                                            <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        </ValidationSettings>

<TextBox Position="Bottom" ShowLabel="False"></TextBox>

<ChallengeImage ForegroundColor="#000000"></ChallengeImage>
                                    </dx:ASPxCaptcha>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Height="20" />
                        <dx:LayoutItem Caption=" ">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton runat="server" ID="btsubmit" Text="Log In" Width="100px" 
                                        OnClick="btsubmit_Click" />
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>

<SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
     <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="110px" Width="100%" FullscreenMode="false" Orientation="Vertical">
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
                <Border BorderWidth="0px" />
            </Pane>
        </Styles>
         <Panes>
          <dx:SplitterPane Name="FooterPane" Size="40px" PaneStyle-BackColor="#EDEDED">
                <Separator Visible="False"></Separator>
                <PaneStyle BackColor="#EDEDED"></PaneStyle>
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

