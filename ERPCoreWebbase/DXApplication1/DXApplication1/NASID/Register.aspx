<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DXApplication1.NASID.Register" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../styles/styles.css" />
    <style type="text/css">
        .formLayoutContainer
        {
            width: 700px;
            margin: auto;
        }
        .layoutGroupBoxCaption
        {
            font-size: 16px;
        }
    </style>
    <script type="text/javascript">
    // <![CDATA[
        var passwordMinLength = 6;
        function GetPasswordRating(password) {
            var result = 0;
            if (password) {
                result++;
                if (password.length >= passwordMinLength) {
                    if (/[a-z]/.test(password))
                        result++;
                    if (/[A-Z]/.test(password))
                        result++;
                    if (/\d/.test(password))
                        result++;
                    if (!(/^[a-z0-9]+$/i.test(password)))
                        result++;
                }
            }
            return result;
        }
        function OnPasswordTextBoxInit(s, e) {
            ApplyCurrentPasswordRating();
        }
        function OnPassChanged(s, e) {
            ApplyCurrentPasswordRating();
        }
        function ApplyCurrentPasswordRating() {
            var password = passwordTextBox.GetText();
            var passwordRating = GetPasswordRating(password);
            ApplyPasswordRating(passwordRating);
        }
        function ApplyPasswordRating(value) {
            ratingControl.SetValue(value);
            switch (value) {
                case 0:
                    ratingLabel.SetValue("Độ an toàn mật khẩu");
                    break;
                case 1:
                    ratingLabel.SetValue("Too simple");
                    break;
                case 2:
                    ratingLabel.SetValue("Not safe");
                    break;
                case 3:
                    ratingLabel.SetValue("Normal");
                    break;
                case 4:
                    ratingLabel.SetValue("Safe");
                    break;
                case 5:
                    ratingLabel.SetValue("Very safe");
                    break;
                default:
                    ratingLabel.SetValue("Password safety");
            }
        }
        function GetErrorText(editor) {
            if (editor === passwordTextBox) {
                if (ratingControl.GetValue() === 1)
                    return "The password is too simple";
            } else if (editor === confirmPasswordTextBox) {
                if (passwordTextBox.GetText() !== confirmPasswordTextBox.GetText())
                    return "The password you entered do not match";
            }
            return "";
        }
        function OnPassValidation(s, e) {
            var errorText = GetErrorText(s);
            if (errorText) {
                e.isValid = false;
                e.errorText = errorText;
            }
        }
    // ]]> 
    </script>
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
                <dx:LayoutGroup Caption="Thông tin cá nhân" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right">
                    <Items>
                        <dx:LayoutItem Caption="Tên tài khoản">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="Callbackpanel"
                                        runat="server" Width="200px" OnCallback="ASPxCallbackPanel1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox runat="server" ID="usernametb" ClientInstanceName="usernametb" Width="170"
                                                    OnValidation="usernametb_Validation">
                                                    <ClientSideEvents Validation="function(s, e) {
	Callbackpanel.PerformCallback();
}" />
                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" EnableCustomValidation="true"
                                                        ErrorTextPosition="Bottom" SetFocusOnError="true">
                                                        <RegularExpression ErrorText="Invalid e-mail" />
                                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tên">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <table cellspacing="3">
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="firstNametb" runat="server" NullText="Tên" Width="170px">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" SetFocusOnError="True">
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="padding-left:4px">
                                                <dx:ASPxTextBox ID="lastNametb" runat="server" NullText="Họ" Width="170px">
                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="Text" SetFocusOnError="True">
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giới tính">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRadioButtonList ID="genderRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                        <Items>
                                            <dx:ListEditItem Text="Nam" Value="M" Selected="True" />
                                            <dx:ListEditItem Text="Nữ" Value="F" />
                                        </Items>
                                        <Border BorderStyle="None" />
                                    </dx:ASPxRadioButtonList>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Thông tin bảo mật" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right"
                    ColCount="2">
                    <Items>
                        <dx:LayoutItem Caption="E-mail" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" ClientInstanceName="Callbackpanel_Email"
                                        runat="server" Width="200px" OnCallback="ASPxCallbackPanel1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox runat="server" ID="emailtb" ClientInstanceName="emailtb" Width="170"
                                                    OnValidation="emailtb_Validation">
                                                    <ClientSideEvents Validation="function(s, e) {
	Callbackpanel_Email.PerformCallback();
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
                        <dx:LayoutItem Caption="Mật khẩu">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="passwordtb" runat="server" ClientInstanceName="passwordTextBox"
                                        Password="true" Width="170">
                                        <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic"
                                            SetFocusOnError="true">
                                            <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        </ValidationSettings>
                                        <ClientSideEvents Init="OnPasswordTextBoxInit" KeyUp="OnPassChanged" Validation="OnPassValidation" />
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxRatingControl ID="ratingControl" runat="server" ClientInstanceName="ratingControl"
                                        ReadOnly="True" Value="0">
                                    </dx:ASPxRatingControl>
                                    <dx:ASPxLabel ID="ratingLabel" runat="server" ClientInstanceName="ratingLabel" Text="Độ an toàn">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Xác nhận mật khẩu" ColSpan="2">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxTextBox ID="confirmPasswordTextBox" runat="server" ClientInstanceName="confirmPasswordTextBox"
                                        Password="true" Width="170">
                                        <ValidationSettings ErrorTextPosition="Bottom" ErrorDisplayMode="Text" Display="Dynamic"
                                            SetFocusOnError="true">
                                            <RequiredField IsRequired="True" ErrorText="The value is required" />
                                        </ValidationSettings>
                                        <ClientSideEvents Validation="OnPassValidation" />
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
                        <dx:LayoutItem Caption=" ">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:EmptyLayoutItem Height="20" />
                        <dx:LayoutItem Caption=" ">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer>
                                    <dx:ASPxButton runat="server" ID="signUp" Text="Đăng ký" Width="100px" OnClick="signUp_Click">
                                    </dx:ASPxButton>
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
