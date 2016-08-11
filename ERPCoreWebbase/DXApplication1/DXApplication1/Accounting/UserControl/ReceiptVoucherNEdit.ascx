<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptVoucherNEdit.ascx.cs" Inherits="ERPCore.Accounting.UserControl.ReceiptVoucherNEdit" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        height: 14px;
    }
    .style3
    {
        height: 21px;
    }
    .style26
    {
        width: 120px;
    }
    .style27
    {
        height: 14px;
        width: 120px;
    }
    .style28
    {
        height: 21px;
        width: 120px;
    }
    .style29
    {
        width: 112px;
    }
        
    .dxgv 
{
    white-space: nowrap;
    text-overflow: ellipsis;
}

    </style>

<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px">
    <PanelCollection>
<dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formReceiptVoucherNEdit" runat="server" 
        ClientInstanceName="formReceiptVoucherNEdit" HeaderText="Chi Tiết Phiếu Thu" 
        Height="541px" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" Width="800px" 
        AllowDragging="True">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <table class="style1">
                    <tr>
                        <td>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                                Height="467px" RenderMode="Lightweight" Width="100%">                                
                                <TabPages>
                                    <dx:TabPage Text="Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>                                                            
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Mã Phiếu Thu">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Mã Phân Loại">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ASPxComboBox8" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Quyển Số">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Ngày Nộp">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="style29" colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Đơn Vị Nộp Tiền">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox7" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td colspan="4">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Người Nộp Tiền">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Width="600px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td colspan="4">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Địa Chỉ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="600px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style27">
                                                                    </td>
                                                                    <td class="style2" colspan="4">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Lý Do Nộp">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="600px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td colspan="4">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style28">
                                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Số Tiền Nộp">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="style3" colspan="2">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit1" runat="server" Height="21px" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="style3">
                                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Loại Tiền">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="style3">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox6" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td colspan="4">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="Số Tiền Qui Đổi">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit2" runat="server" Height="21px" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Tỉ Giá ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        &nbsp;</td>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style26">
                                                                        <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Diễn Giải">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="600px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Chứng Từ Gốc">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxGridView ID="grdPurpose0" runat="server" AutoGenerateColumns="False" 
                                                                ClientInstanceName="grdPurpose" KeyboardSupport="True" KeyFieldName="Code" 
                                                                Width="100%">
                                                                <ClientSideEvents EndCallback="grdData_EndCallback" />
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Mã Chứng Từ Gốc" FieldName="Code" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="10%">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Ngày Chứng Từ" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Serri" ShowInCustomizationForm="True" 
                                                                        VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="File Đính Kèm" FieldName="Date" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                                        <EditItemTemplate>
                                                                            <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" UploadMode="Auto" 
                                                                                Width="280px">
                                                                            </dx:ASPxUploadControl>
                                                                        </EditItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="7" Width="5%">
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
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Xóa" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="8" Width="5%">
                                                                        <CustomButtons>
                                                                            <dx:GridViewCommandColumnCustomButton>
                                                                                <Image ToolTip="Xóa">
                                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                                </Image>
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                    </dx:GridViewCommandColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                                                    AllowSelectSingleRowOnly="True" />
                                                                <SettingsEditing Mode="Inline" />
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Mục Đích">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table style="table-layout:auto">
                                                                <tr>
                                                                    <td style="width:auto">
                                                                        <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="TK Nợ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ASPxComboBox9" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:auto">
                                                                        <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="Phát Sinh Nợ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit3" runat="server" Height="21px" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:auto">
                                                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Sơ Đồ ĐK">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ASPxComboBox10" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width:auto">
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxGridView ID="grdPurpose" runat="server" AutoGenerateColumns="False" 
                                                                ClientInstanceName="grdPurpose" KeyFieldName="Code" Width="100%">
                                                                <ClientSideEvents EndCallback="grdData_EndCallback" />
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Mã Đối Tượng" FieldName="Code" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Tên Đối Tượng" FieldName="Date" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Thanh Toán" FieldName="Credit" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="5" Width="5%">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Còn Nợ" FieldName="Debit" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="20%">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="7" Width="5%">
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
                                                                    <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Desc" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="6" Width="10%">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Xóa" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="9" Width="5%">
                                                                        <CustomButtons>
                                                                            <dx:GridViewCommandColumnCustomButton>
                                                                                <Image ToolTip="Xóa">
                                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                                </Image>
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                    </dx:GridViewCommandColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TK Có" ShowInCustomizationForm="True" 
                                                                        VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                                                    AllowSelectSingleRowOnly="True" />
                                                                <SettingsEditing Mode="Inline" />
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="right" style=" table-layout:auto; width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" style="width:80%">
                                        &nbsp;</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style1">
                                            <tr>
                                                <td align="right">
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Duyệt">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Bỏ Qua">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
