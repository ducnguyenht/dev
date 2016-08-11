<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uQuotationEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uQuotationEdit" %>
<style type="text/css">
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }
</style>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formQuotationEdit" runat="server" HeaderText="Thông Tin Phiếu Báo Giá - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formQuotationEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
                                <Items>
                                    <dx:LayoutItem Caption="Mã Số">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" NullText="Tối đa 128 ký tự"
                                                    Width="200px">
                                                    <NullTextStyle ForeColor="Silver">
                                                    </NullTextStyle>
                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                        <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Thời Lượng Giao Hàng">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit3" runat="server" Height="21px" Number="0" Width="200px">
                                                </dx:ASPxSpinEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Nhà Cung Cấp">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboRowStatus0" runat="server" ClientInstanceName="cboRowStatus"
                                                    Width="200px">
                                                    <Items>
                                                        <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Hiệu Lực Đến">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="200px">
                                                </dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Thành Tiền Phải Trả" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit2" runat="server" Height="21px" Number="0" Width="200px">
                                                </dx:ASPxSpinEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="txtCode0" runat="server" ClientInstanceName="txtCode" Height="18px"
                                                    NullText="Tối đa 128 ký tự" Width="646px">
                                                    <NullTextStyle ForeColor="Silver">
                                                    </NullTextStyle>
                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                        <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" Height="70%"
                                Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Hàng Hóa">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl11" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Số" ShowInCustomizationForm="True" VisibleIndex="1"
                                                            Width="10%">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                    <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="3"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Thành Tiền Sau CK" ShowInCustomizationForm="True"
                                                            VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Chiết Khấu" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Thành Tiền Trước CK" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="8" Width="10%">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Thêm">
                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="Xóa">
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                                </Image>
                                                            </ClearFilterButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Số Lượng Yêu Cầu" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Hàng Hóa" ShowInCustomizationForm="True" VisibleIndex="2"
                                                            Width="30%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" VisibleIndex="0"
                                                            Width="5%">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30" Mode="ShowAllRecords">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="300px">
                                                    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                </dx:ASPxFileManager>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
