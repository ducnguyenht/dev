<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="MailServerConfiguration.aspx.cs" Inherits="WebModule.GUI.system.MailServerConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxSplitter ID="ASPxSplitter3" runat="server" Height="100%" Orientation="Vertical"
        Width="100%" SeparatorVisible="False">
        <Border BorderWidth="0px" />
        <Styles>
            <Pane>
                <Paddings Padding="0px"></Paddings>
                <Border BorderWidth="0px" />
            </Pane>
        </Styles>
        <Panes>
            <dx:SplitterPane MinSize="80px" Size="80px" AutoHeight="true">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitle="Mail Server"
                            ParentTitleSize="16px" Style="display: block;" Title="Cấu hình Mail Server"
                            TitleSize="26px" />
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                        
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                            AlignItemCaptionsInAllGroups="True" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin Email">
                                    <Items>
                                        <dx:LayoutItem Caption="Email" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True">
                                                            <RegularExpression ErrorText="Định dạng mail không đúng" 
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Bí danh">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Thông tin máy chủ">
                                    <Items>
                                        <dx:LayoutItem Caption="Máy chủ SMTP" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
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
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E4" runat="server" Text="25" Width="170px">
                                                        <ValidationSettings SetFocusOnError="True">
                                                            <RegularExpression ErrorText="Cổ mạng không hợp lệ" 
                                                                ValidationExpression="\d{1,5}" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Thông tin đăng nhập">
                                    <Items>
                                        <dx:LayoutItem Caption="Mật khẩu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E6" runat="server" Password="True" 
                                                        Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="Text" SetFocusOnError="True">
                                                            <RequiredField IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Xác nhận mật khẩu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E7" runat="server" Password="True" 
                                                        Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <div class="float-left">
                                                <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" AutoPostBack="False" 
                                                    CausesValidation="False" Text="Khôi phục">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Refresh" />
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                            <div class="float-left" style="margin-left: 4px">
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                                                    Text="Lưu cấu hình">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                        
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
            </Pane>
        </Styles>
    </dx:ASPxSplitter>
</asp:Content>
