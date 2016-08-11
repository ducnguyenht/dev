<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreditNoteEdit.ascx.cs"
    Inherits="WebModule.PayReceiving.UserControl.CreditNoteEdit" %>
<style type="text/css">
    .dxtcControl
    {
        font: 12px Tahoma, Geneva, sans-serif;
        color: black;
    }
    .dxfmControl, .dxnbGroupHeader, .dxnbGroupHeaderCollapsed, .dxnbGroupContent > TABLE > TBODY > TR, .dxtcTab, .dxtcActiveTab, .dxtv-nd
    {
        -webkit-tap-highlight-color: rgba(0,0,0,0);
    }
    
    .dxtcActiveTab, .dxtcActiveTabWithTabPositionBottom, .dxtcActiveTabWithTabPositionLeft, .dxtcActiveTabWithTabPositionRight
    {
        border: 1px solid #A8A8A8;
        padding: 3px 12px 4px;
        background-color: #FFFFFF;
        text-align: center;
    }
    .dxtcActiveTab td.dxtc, .dxtcActiveTabWithTabPositionBottom td.dxtc, .dxtcActiveTabWithTabPositionLeft td.dxtc, .dxtcActiveTabWithTabPositionRight td.dxtc
    {
        white-space: nowrap;
        background: transparent none !important;
        border-width: 0px !important;
        padding: 0px !important;
    }
    .dxtcTab, .dxtcTabWithTabPositionLeft, .dxtcTabWithTabPositionBottom, .dxtcTabWithTabPositionRight
    {
        background-color: #E0E0E0;
        border: 1px solid #A8A8A8;
        padding: 3px 12px 4px;
        text-align: center;
    }
    .dxtcTab td.dxtc, .dxtcTabWithTabPositionBottom td.dxtc, .dxtcTabWithTabPositionLeft td.dxtc, .dxtcTabWithTabPositionRight td.dxtc
    {
        white-space: nowrap;
        background: transparent none !important;
        border-width: 0px !important;
        padding: 0px !important;
    }
    .dxtcRightAlignCell, .dxtcTabsCellWithTabPositionBottom .dxtcRightAlignCell
    {
        text-align: right;
    }
    .dxtcPageContent, .dxtcPageContentWithTabPositionBottom, .dxtcPageContentWithTabPositionLeft, .dxtcPageContentWithTabPositionRight, .dxtcPageContentWithoutTabs
    {
        background-color: white;
        vertical-align: top;
    }
    img
    {
        border-width: 0;
    }
    
    .dxpcControl
    {
        font: 12px Tahoma, Geneva, sans-serif;
        color: black;
        background-color: white;
        border: 1px solid #8B8B8B;
        width: 200px;
    }
    .dxpcHeader
    {
        color: #404040;
        background-color: #DCDCDC;
        border-bottom: 1px solid #C9C9C9;
    }
    .dxpcHeader td.dxpc
    {
        color: #404040;
        white-space: nowrap;
    }
    .dxpcHBCell
    {
        padding: 1px 1px 1px 2px;
    }
    .dxpcHBCellSys
    {
        -webkit-tap-highlight-color: rgba(0,0,0,0);
        -webkit-touch-callout: none;
    }
    
    .dxpcContent
    {
        color: #010000;
        white-space: normal;
        vertical-align: top;
    }
    .dxpcContentPaddings
    {
        padding: 9px 12px;
    }
    .style1
    {
        width: 100%;
    }
    .style26
    {
        width: 120px;
    }
    .style29
    {
        width: 112px;
    }
    .style27
    {
        height: 14px;
        width: 120px;
    }
    .style2
    {
        height: 14px;
    }
    .style28
    {
        height: 21px;
        width: 120px;
    }
    .style3
    {
        height: 21px;
    }
</style>
<dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AllowDragging="True" ClientInstanceName="formCreditNoteEdit" HeaderText="Giấy Báo Có"
    RenderMode="Lightweight" Width="800px" Height="541px" ID="formCreditNoteEdit">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <table class="style1">
                <tr>
                    <td>
                        <dx:ASPxPageControl runat="server" ActiveTabIndex="0" RenderMode="Lightweight" Width="100%"
                            Height="467px" ID="ASPxPageControl1">
                            <TabPages>
                                <dx:TabPage Text="Chi Tiết">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3">
                                                <Items>
                                                    <dx:LayoutItem Caption="Mã số" Width="35%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E6" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Mã phân loại" Width="35%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="ASPxFormLayout1_E2" runat="server" Width="100%">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Ngày nộp" Width="30%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="ASPxFormLayout_NgayNop" runat="server" Width="100%">
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Đơn vị nộp tiền" Width="70%" ColSpan="2">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="ASPxFormLayout_DonViNopTien" runat="server" Width="100%">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Người nộp tiền" Width="30%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="ASPxFormLayout_NguoiNopTien" runat="server" Height="21px" Number="0"
                                                                    Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Địa chỉ" ColSpan="3" Width="100%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="ASPxFormLayout_DiaChi" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Lý do nộp" ColSpan="3" Width="100%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Số tiền nộp" Width="35%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxSpinEdit ID="ASPxFormLayout_SoTienNop" runat="server" Width="100px">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Loại tiền" Width="35%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="ASPxFormLayout1_E5" runat="server" Width="100%">
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tỉ giá" Width="30%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E12" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Số tiền quy đổi" Width="35%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:EmptyLayoutItem>
                                                    </dx:EmptyLayoutItem>
                                                    <dx:EmptyLayoutItem>
                                                    </dx:EmptyLayoutItem>                                                    
                                                </Items>
                                            </dx:ASPxFormLayout>
                                            <br />
                                            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="3">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Trả cho phiếu bán hàng" ColCount="3" ColSpan="3" Width="100%">
                                                        <Items>
                                                            <dx:EmptyLayoutItem Width="45%">
                                                            </dx:EmptyLayoutItem>
                                                            <dx:LayoutItem Caption="Sơ đồ định khoản" Width="30%">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="100%">
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tài khoản nợ" Width="25%">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="ASPxFormLayout2_E1" runat="server" Width="100%">
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem ColSpan="3" ShowCaption="False" Width="100%">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdPurpose1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdPurpose"
                                                                            KeyFieldName="Code" Width="100%">
                                                                            <ClientSideEvents EndCallback="grdData_EndCallback" />
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="Mã phiếu mua" FieldName="Code" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="10%">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ngày " FieldName="Date" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="10%">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Thanh Toán" FieldName="Credit" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Width="10%">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Dư nợ" FieldName="Debit" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="15%">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="7" Width="10%">
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
                                                                                    <CancelButton>
                                                                                        <Image ToolTip="Bỏ qua">
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image ToolTip="Cập nhật">
                                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                        <Image ToolTip="Hủy">
                                                                                            <SpriteProperties CssClass="Sprite_Clear" />
                                                                                        </Image>
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Desc" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="5" Width="35%">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="TK Có" ShowInCustomizationForm="True" VisibleIndex="6"
                                                                                    Width="10%">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                                            <SettingsEditing Mode="Inline" />
                                                                        </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Chứng Từ Gốc">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView runat="server" ClientInstanceName="grdPurpose" KeyFieldName="Code"
                                                AutoGenerateColumns="False" KeyboardSupport="True" Width="100%" ID="grdPurpose0">
                                                <ClientSideEvents EndCallback="grdData_EndCallback"></ClientSideEvents>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Code" ShowInCustomizationForm="True" Width="10%"
                                                        Caption="Mã Chứng Từ Gốc" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Ngay" ShowInCustomizationForm="True" Width="10%"
                                                        Caption="Ngày" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="FileDinhKem" ShowInCustomizationForm="True"
                                                        Caption="File Đính Kèm" VisibleIndex="2" Width="70%">
                                                        <EditItemTemplate>
                                                            <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" UploadMode="Auto" Width="280px">
                                                            </dx:ASPxUploadControl>
                                                        </EditItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%"
                                                        Caption="Thao Tác" VisibleIndex="3">
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
                                                    <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="5%"
                                                        Caption="Xóa" VisibleIndex="8">
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton>
                                                                <Image ToolTip="Xóa">
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True">
                                                </SettingsBehavior>
                                                <SettingsEditing Mode="Inline"></SettingsEditing>
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="right" style="table-layout: auto; width: 100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" style="width: 80%">
                                    &nbsp;
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td align="right">
                                                <dx:ASPxButton runat="server" Text="Duyệt" ID="ASPxButton2">
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right">
                                                <dx:ASPxButton runat="server" Text="Bỏ Qua" ID="ASPxButton3">
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
