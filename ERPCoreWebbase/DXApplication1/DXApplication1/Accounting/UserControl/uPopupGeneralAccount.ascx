<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPopupGeneralAccount.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uPopupGeneralAccount" %>
<%@ Register Src="uChungTuGoc_2.ascx" TagName="uChungTuGoc_2" TagPrefix="uc1" %>
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="cphtc"
    OnCallback="ASPxCallbackPanel1_Callback" Width="200px">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="pophtc"
                HeaderText="Hạch toán" CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" RenderMode="Classic" Width="850px" ShowFooter="true"
                ShowSizeGrip="False" AllowResize="true" ShowMaximizeButton="true" Height="600px">
                <FooterContentTemplate>
                    <div id="Footer">
                        <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin "
                            Text="Trợ Giúp" Wrap="False">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="ASPxButton2" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin hd "
                            Text="Tiếp theo" Wrap="False">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Forward" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin "
                            Text="Duyệt" Wrap="False">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Approve" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="BTctg" runat="server" Text="Chứng từ liên quan" Width="150px"
                            CssClass="float-right button-right-margin " AutoPostBack="false">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Document" />
                            </Image>
                            <ClientSideEvents Click="function(s,e){popctg.Show();}" />
                        </dx:ASPxButton>
                </FooterContentTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chung" ColCount="3" GroupBoxDecoration="HeadingLine">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã chứng từ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E1" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Sơ đồ định khoản">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E3" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Hệ thống tài khoản">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E2" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Hạch toán" GroupBoxDecoration="HeadingLine">
                                    <Items>
                                        <dx:LayoutItem Caption="Diễn giải">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout2_E2" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridView_ht" runat="server" AutoGenerateColumns="False"
                                                        Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="No" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Des" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="TK nợ" FieldName="TKno" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="TK có" FieldName="TKco" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                                <EditButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                    </Image>
                                                                </DeleteButton>
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Caption="Giá trị" FieldName="Total" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="Inline" />
                                                        <Styles>
                                                            <Header HorizontalAlign="Center">
                                                            </Header>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc1:uChungTuGoc_2 ID="uChungTuGoc_21" runat="server" />
