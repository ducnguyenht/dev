<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uQuotationEdit.ascx.cs" Inherits="WebModule.ImExporting.UserControl.uQuotationEdit" %>
<style type="text/css">
    .style25
    {
    }
    .style31
    {
    }
    .style33
    {
        width: 98px;
    }
    .style37
    {
        width: 98px;
        height: 13px;
    }
    .style38
    {
        height: 13px;
    }
    .style39
    {
        height: 17px;
    }
    .style41
    {
        width: 254px;
        height: 13px;
    }
    .style42
    {
        width: 135px;
    }
    .style43
    {
    }
    .style44
    {
        width: 575px;
    }
    .style45
    {
        width: 135px;
        height: 13px;
    }
</style>

<div id="lineContainer"> 
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine">
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formQuotationEdit" runat="server" 
            HeaderText="Phiếu Báo Giá" Height="617px" Modal="True" 
            RenderMode="Lightweight"  
            Width="850px" ClientInstanceName="formQuotationEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000" 
            ClientIDMode="AutoID">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
                    <table style="width:100%; margin-top:10px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table align="right" style="width:100%;">
                                <tr>
                                    <td align="left" class="style25" colspan="3">
                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                            <tr>
                                                <td class="style33">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Mã Số" >
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="style43">
                                                    <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" 
                                                        NullText="Tối đa 128 ký tự"  
                                                        Width="200px">
                                                        <NullTextStyle ForeColor="Silver">
                                                        </NullTextStyle>
                                                        <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                            <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td class="style42">
                                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="Thời Lượng Giao Hàng" 
                                                        >
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit3" runat="server" Height="21px" Number="0" 
                                                        Width="200px">
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style31" colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style33">
                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Nhà Cung Cấp" >
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="style43">
                                                    <dx:ASPxComboBox ID="cboRowStatus0" runat="server" 
                                                        ClientInstanceName="cboRowStatus" Width="200px">
                                                        <Items>
                                                            <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                            <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td class="style42">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                </td>
                                                <td class="style38" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Hiệu Lực Tới" >
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="style38">
                                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="200px">
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td class="style38">
                                                    <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Thành Tiền Phải Trả" 
                                                        >
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="style38">
                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit2" runat="server" Height="21px" Number="0" 
                                                        Width="200px">
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                    </td>
                                                <td class="style38">
                                                    </td>
                                                <td class="style45">
                                                    </td>
                                                <td class="style38">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style33">
                                                    <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Ghi Chú" >
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td class="style43" colspan="3">
                                                    <dx:ASPxTextBox ID="txtCode0" runat="server" ClientInstanceName="txtCode" 
                                                        Height="18px" NullText="Tối đa 128 ký tự"  Width="646px">
                                                        <NullTextStyle ForeColor="Silver">
                                                        </NullTextStyle>
                                                        <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                            <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style33">
                                                    &nbsp;</td>
                                                <td class="style43">
                                                    &nbsp;</td>
                                                <td class="style42">
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style33">
                                                    &nbsp;</td>
                                                <td class="style43" colspan="3">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                                                Height="386px" RenderMode="Lightweight" Width="100%">
                                                <TabPages>
                                                    <dx:TabPage Name="tabGeneral" Text="Hàng Hóa">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                                <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                                                    <tr>
                                                                        <td>
                                                                            <table class="dxflInternalEditorTable_DevEx">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" 
                                                                                            AutoGenerateColumns="False" 
                                                                                            Width="100%">
                                                                                            <Columns>
                                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã Số" 
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                                                                                    <PropertiesComboBox>
                                                                                                        <Columns>
                                                                                                            <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                            <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                        </Columns>
                                                                                                    </PropertiesComboBox>
                                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" 
                                                                                                    VisibleIndex="3" Width="10%">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="Thành Tiền Sau CK" 
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="Chiết Khấu" ShowInCustomizationForm="True" 
                                                                                                    VisibleIndex="6">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="Thành Tiền Trước CK" 
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="10%">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="8" Width="10%">
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
                                                                                                <dx:GridViewDataTextColumn Caption="Số Lượng Yêu Cầu" 
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="10%">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="Hàng Hóa" 
                                                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="30%">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" 
                                                                                                    VisibleIndex="0" Width="5%">
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
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Text="Chi Tiết">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="340px">
                                                                    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                                </dx:ASPxFileManager>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                </TabPages>
                                            </dx:ASPxPageControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style39" colspan="3">
                                            
                                            <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                                <tr>
                                                    <td class="style41">
                                                        &nbsp;</td>
                                                    <td class="style38">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style44">
                                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                                                Text="Trợ Giúp" >
                                                <Image ToolTip="Trợ giúp">
                                                    <SpriteProperties CssClass="Sprite_Help" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="right">
                                            <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="buttonSave" Text="Lưu Lại" >
                                                <ClientSideEvents Click="buttonSave_Click" />
                                                <Image ToolTip="Lưu">
                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="right">
                                            <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="buttonCancel" Text="Bỏ Qua" >
                                                <ClientSideEvents Click="buttonCancel_Click" />
                                                <Image ToolTip="Bỏ qua">
                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
</div>