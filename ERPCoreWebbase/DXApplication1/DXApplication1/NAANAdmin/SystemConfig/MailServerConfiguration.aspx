<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="MailServerConfiguration.aspx.cs" Inherits="WebModule.NAANAdmin.SystemConfig.MailServerConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitleSize="16px"
        Style="display: block;" Title="Cấu hình Mail Server" TitleSize="26px" />
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
        Width="50%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin cấu hình" GroupBoxDecoration="HeadingLine">
                <Items> 
                    <dx:LayoutItem Caption="Máy chủ SMTP" RequiredMarkDisplayMode="Required">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E5" runat="server" Width="170px">
                                    <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Cổng mạng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E4" runat="server" Width="170px" Text="25">
                                    <ValidationSettings SetFocusOnError="True">
                                        <RegularExpression ErrorText="Cổng mạng không hợp lệ" ValidationExpression="\d{1,5}" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Email" RequiredMarkDisplayMode="Required">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                    <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True">
                                        <RegularExpression ErrorText="Định dạng mail không đúng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Bí danh">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" Width="170px">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Mật khẩu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E6" runat="server" Password="True" Width="170px">
                                    <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True">
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxCheckBox ID="ASPxFormLayout1_E8" runat="server" CheckState="Unchecked" Text="Máy chủ yêu cầu xác thực">
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxCheckBox ID="ASPxFormLayout1_E9" runat="server" CheckState="Unchecked" Text="Sử dụng SSL">
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Kiểm tra cấu hình" GroupBoxDecoration="HeadingLine" VerticalAlign="Middle">
                <Items>
                    <dx:LayoutItem Caption="Gửi email đến">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E7" runat="server" Width="170px">
                                    <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True" ValidationGroup="TestEmail">
                                        <RegularExpression ErrorText="Định dạng mail không đúng" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                        <RequiredField ErrorText="Chưa nhập email để gửi đến" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                                <div style="margin-top: 4px">
                                    <dx:ASPxButton AutoPostBack="false" ID="ASPxButton2" runat="server" Text="Gửi" ValidationGroup="TestEmail"
                                        UseSubmitBehavior="False">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_SendMail" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
<asp:Content ID="CenterSubmitContainer" ContentPlaceHolderID="CenterSubmitContainer"
    runat="server">
    <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" CausesValidation="False"
        Text="Khôi phục" UseSubmitBehavior="False">
        <Image>
            <SpriteProperties CssClass="Sprite_Refresh" />
        </Image>
    </dx:ASPxButton>
    <dx:ASPxButton ID="ASPxButton4" runat="server" AutoPostBack="False" Text="Lưu cấu hình">
        <Image>
            <SpriteProperties CssClass="Sprite_Apply" />
        </Image>
    </dx:ASPxButton>
</asp:Content>
