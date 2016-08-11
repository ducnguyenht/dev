<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhaseProductEdit.ascx.cs"
    Inherits="WebModule.Produce.UserControl.PhaseProductEdit" %>
<style type="text/css">
    .style25
    {
        width: 609px;
    }
    .style26
    {
        width: 110px;
    }
</style>
<div id="lineContainerProduct">
    <dx:ASPxCallbackPanel ID="cpLineProduct" runat="server" Width="100%" ClientInstanceName="cpLineProduct"
        OnCallback="cpLineProduct_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formPhaseProductEdit" runat="server" HeaderText="Cập Nhật Công Đoạn Sản Phẩm"
                    Height="617px" Modal="True" RenderMode="Lightweight" Width="850px" ClientInstanceName="formPhaseProductEdit"
                    AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    LoadingPanelDelay="1000" ShowFooter="true" ScrollBars="Auto" ShowSizeGrip = "False">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="width: 100%;">
                            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                                Text="Trợ Giúp" Wrap="False">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float-right button-right-margin"
                                ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                <ClientSideEvents Click="buttonCancelProduct_Click" />
                                <ClientSideEvents Click="buttonCancelProduct_Click"></ClientSideEvents>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonAcceptProduct" runat="server" AutoPostBack="False" CssClass="float-right button-right-margin"
                                ClientInstanceName="buttonSaveProduct" Text="Lưu Lại">
                                <ClientSideEvents Click="buttonSaveProduct_Click" />
                                <ClientSideEvents Click="buttonSaveProduct_Click"></ClientSideEvents>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                                <Items>
                                    <dx:LayoutItem Caption="Mã sản phẩm">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="ASPxFormLayout1_E1" runat="server">
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tên sản phẩm">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="50%">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="STT" FieldName="No" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                    <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã Công Đoạn" FieldName="Code" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="250px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Công Đoạn" FieldName="Name" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="100px">
                                                            <EditButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass = "Sprite_Edit" />
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass = "Sprite_New" />
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass = "Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                            <CancelButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass = "Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass = "Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <%--<table style="width: 100%; margin-top: 10px">
                                <tr>
                                    <td>
                                        <table align="right" style="width: 100%;">
                                            <tr>
                                                <td align="left" class="style25">
                                                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                                        <Image Url="~/images/icon/32x32/help.png">
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="buttonAcceptProduct" runat="server" AutoPostBack="False" ClientInstanceName="buttonSaveProduct"
                                                        Text="Lưu Lại">
                                                        <ClientSideEvents Click="buttonSaveProduct_Click" />
                                                        <ClientSideEvents Click="buttonSaveProduct_Click"></ClientSideEvents>
                                                        <Image Url="~/images/icon/32x32/save.png">
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="buttonCancelProduct" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancelProduct"
                                                        Text="Bỏ Qua">
                                                        <ClientSideEvents Click="buttonCancelProduct_Click" />
                                                        <ClientSideEvents Click="buttonCancelProduct_Click"></ClientSideEvents>
                                                        <Image Url="~/images/icon/32x32/cancel.png">
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>--%>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
