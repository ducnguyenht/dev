<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPurchaseEdit.ascx.cs" Inherits="WebModule.ImExporting.UserControl.uPurchaseEdit" %>
<style type="text/css">
    .style25
    {
    }
    .style31
    {
    }
    .style35
    {
        width: 270px;
    }
    .style38
    {
    }
    .style39
    {
        height: 17px;
    }
    .style42
    {
        width: 122px;
    }
    .style43
    {
        width: 122px;
    }
    .style46
    {
        width: 589px;
    }
    
    table.controlContain td
    {
        height: 5px;
    }
    .style56
    {
    }
    .style58
    {
        width: 23%;
    }
    .style59
    {
        width: 153px;
    }
    .style60
    {
    }
    .style61
    {
        height: 453px;
    }
    .style62
    {
        width: 122px;
        height: 13px;
    }
    .style63
    {
        height: 13px;
    }
    .style64
    {
        width: 180px;
    }
    .style65
    {
        width: 180px;
        height: 13px;
    }
    .style_pd
    {
        padding-left: 10px;
        padding-right: 10px;
    }
    .style68
    {
        width: 100%;
    }
    .style70
    {
        width: 237px;
    }
    .style71
    {
        width: 60px;
    }
    .style72
    {
        width: 277px;
    }
    .style73
    {
        width: 148px;
    }
    .style75
    {
        width: 10%;
    }
    .style79
    {
        width: 54%;
    }
    .style80
    {
        width: 108px;
    }
    .style81
    {
        width: 124px;
    }
</style>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formPurchaseEdit" runat="server" HeaderText="Phiếu Mua Hàng"
                    Height="650px" Modal="True" RenderMode="Classic" Width="850px" ScrollBars="Auto"
                    ClientInstanceName="formPurchaseEdit" AllowResize="True" AllowDragging="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                    LoadingPanelDelay="1000" ClientIDMode="AutoID" ShowFooter="true">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <table style="width: 100%; margin-top: 10px" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td align="left" class="style25">
                                                    <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                                        <tr>
                                                            <td class="style42">
                                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Mã Số">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="style35">
                                                                <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" NullText="Tối đa 128 ký tự"
                                                                     Width="200px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa nhập M&#227; Nh&#224; Cung Cấp"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="style73">
                                                                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Nhân Viên Đề Nghị">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxComboBox ID="cboRowStatus0" runat="server" ClientInstanceName="cboRowStatus"
                                                                    Width="200px">
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Nguyễn Quang Đông" Value="A" />
                                                                        <dx:ListEditItem Text="Huỳnh Quốc Việt" Value="&quot;I&quot;" />
                                                                    </Items>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style31" colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style42">
                                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Ngày Lập">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="style35">
                                                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="200px">
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                            <td class="style73">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style62">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style63" colspan="3">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style43">
                                                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Nhà Cung Cấp">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="style38">
                                                                <dx:ASPxComboBox ID="cboRowStatus" runat="server" ClientInstanceName="cboRowStatus"
                                                                    Width="200px">
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Công ty TNHH VOAC" Value="A" />
                                                                        <dx:ListEditItem Text="Công Ty Dược Đông Hưng" Value="&quot;I&quot;" />
                                                                    </Items>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                            <td class="style73">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style38">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style42">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="style73">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style61">
                                                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="100%"
                                                        RenderMode="Classic" Width="100%" 
                                                        ScrollButtonStyle-HorizontalAlign="Right">
                                                        <TabPages>
                                                            <dx:TabPage Name="tabGeneral" Text="Hàng Hóa">
                                                                <ContentCollection>
                                                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                        <table style="width:100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxGridView ID="grdProduct" runat="server" AutoGenerateColumns="False"
                                                                                        Width="100%" KeyFieldName="No">
                                                                                        <SettingsEditing Mode="Inline" />
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Số" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="1" FieldName="Code">
                                                                                                <PropertiesComboBox>
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                        <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="3" FieldName="Unit">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Giá" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="5" FieldName="Price">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Thành Tiền" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="6" FieldName="AmountB">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Thành Tiền Quy Đổi" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="10" FieldName="AmountA">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tỉ giá" FieldName="Exchange" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="9">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Loại Ngoại Tệ" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="8" FieldName="Currency">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="13" FieldName="Note">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="14">
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
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="11" FieldName="Lot">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="12" FieldName="Time">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng " ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="4" FieldName="Quantity">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Hàng Hóa" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="2" FieldName="Name">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="0" FieldName="No">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                                                        <SettingsPager PageSize="20" ShowEmptyDataRows="True">
                                                                                        </SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

                                                                                        <Styles>
                                                                                            <CommandColumn Spacing="10px">
                                                                                            </CommandColumn>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" class="controlContain" width="100%">
                                                                                        <tr>
                                                                                            <td class="style75">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>                                                                                 
                                                                                        <tr>
                                                                                            <td class="style75">
                                                                                                <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="Thuế GTGT">
                                                                                                </dx:ASPxLabel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit10" runat="server" Height="21px" Number="0" Width="80px">
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td class="style81">
                                                                                                <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="Tiền Thuế GTGT">
                                                                                                </dx:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="style79">
                                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit22" runat="server" Height="21px" Number="0" Width="150px">
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style75">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style75">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td class="style81">
                                                                                                <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Tổng Tiền Hàng">
                                                                                                </dx:ASPxLabel>
                                                                                            </td>
                                                                                            <td class="style79">
                                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit23" runat="server" Height="21px" Number="0" Width="150px">
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style75">
                                                                                            </td>
                                                                                            <td colspan="3" class="style60">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td height="15" class="style75">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td class="style58">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td class="style81">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td class="style79">
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style75">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Dịch Vụ">
                                                                <ContentCollection>
                                                                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                                        <table cellpadding="0" cellspacing="0" style="width:100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <table style="width:100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <div style="width:800px; height:400px; overflow:scroll">
                                                                                                <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" AutoGenerateColumns="False"
                                                                                                    Width="100%">
                                                                                                    <Columns>
                                                                                                        <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                            Width="5%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Dịch Vụ" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="1" Width="10%">
                                                                                                            <PropertiesComboBox>
                                                                                                                <Columns>
                                                                                                                    <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                                    <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                                </Columns>
                                                                                                            </PropertiesComboBox>
                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Tên Dịch Vụ" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                                                            Width="30%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                                                            Width="5%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số Lượng" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                                            Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành Tiền" 
                                                                                                            VisibleIndex="5" Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="10"
                                                                                                            Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="11" Width="10%">
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
                                                                                                        <dx:GridViewDataTextColumn Caption="Loại Ngoại Tệ" 
                                                                                                            ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Tỉ Giá" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="8">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Thành Tiền Qui Đổi" 
                                                                                                            ShowInCustomizationForm="True" VisibleIndex="9">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                    <SettingsPager PageSize="30">
                                                                                                    </SettingsPager>
                                                                                                    <Styles>
                                                                                                        <CommandColumn Spacing="10px">
                                                                                                        </CommandColumn>
                                                                                                    </Styles>
                                                                                                </dx:ASPxGridView>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table cellpadding="0" cellspacing="0" class="controlContain" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td class="style38" colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="Thuế GTGT">
                                                                                                            </dx:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit27" runat="server" Height="21px" Number="0" Width="80px">
                                                                                                            </dx:ASPxSpinEdit>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Tiền Thuế GTGT">
                                                                                                            </dx:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit28" runat="server" Height="21px" Number="0" Width="150px">
                                                                                                            </dx:ASPxSpinEdit>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="Tổng Tiền Dịch Vụ">
                                                                                                            </dx:ASPxLabel>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit29" runat="server" Height="21px" Number="0" Width="150px">
                                                                                                            </dx:ASPxSpinEdit>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style59">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td class="style60" colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56" height="15">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td class="style58">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="2">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="style56">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                        <td colspan="3">
                                                                                                            &nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Khuyến Mãi">
                                                                <ContentCollection>
                                                                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" 
                                                                                        Width="100%">
                                                                                        <PanelCollection>
                                                                                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                                        <table class="dxflInternalEditorTable_DevEx" width="100%">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    &nbsp;

                                                                                     
&nbsp;<table class="style68">
                                                                                        <tr>
                                                                                            <td class="style72">
                                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" CheckState="Unchecked" 
                                                                                                    Text="Chiết Khấu">
                                                                                                </dx:ASPxCheckBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style72" rowspan="3" valign="top">
                                                                                                <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" Width="100%">
                                                                                                    <Items>
                                                                                                        <dx:ListEditItem Text="Tiền Giảm Theo Phiếu" Value="Tiền Giảm Theo Phiếu" />
                                                                                                        <dx:ListEditItem Text="Tỉ lệ CK Theo Phiếu (%)" Value="Tỉ lệ Chiết Khấu (%)" />
                                                                                                        <dx:ListEditItem Text="CK Theo Từng Hàng Hóa" Value="CK Theo Từng" />
                                                                                                    </Items>
                                                                                                    <Border BorderStyle="Solid" />

<Border BorderStyle="Solid"></Border>
                                                                                                </dx:ASPxRadioButtonList>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit33" runat="server" Height="21px" Number="0" 
                                                                                                    Width="150px">
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table class="style68" cellpadding="0" cellspacing="0">
                                                                                                    <tr>
                                                                                                        <td class="style71">
                                                                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit34" runat="server" Height="21px" Number="0" 
                                                                                                                Width="50px">
                                                                                                            </dx:ASPxSpinEdit>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit35" runat="server" Height="21px" Number="0" 
                                                                                                                Width="150px">
                                                                                                            </dx:ASPxSpinEdit>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxGridView ID="grdProductSupplier" runat="server" 
                                                                                                    AutoGenerateColumns="False" ClientInstanceName="grdProductSupplier" 
                                                                                                    KeyFieldName="ProductSupplierId" Width="100%">
                                                                                                    <Columns>
                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Số" FieldName="SupplierCode" 
                                                                                                            Name="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                                                                                                            <propertiescombobox callbackpagesize="20" dropdownstyle="DropDown" 
                                                                                                                enablecallbackmode="True" incrementalfilteringmode="StartsWith" 
                                                                                                                textfield="Code" textformatstring="{0} {1}" valuefield="Code" width="400px">
                                                                                                                <Columns>
                                                                                                                    <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" Width="150px"></dx:ListBoxColumn>
                                                                                                                    <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" Width="300px"></dx:ListBoxColumn>
                                                                                                                </Columns>
                                                                                                            </propertiescombobox>
                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Hàng Hóa" FieldName="SupplierName" 
                                                                                                            Name="SupplierName" ReadOnly="True" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="3" Width="30%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="8" Width="5%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="9" Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Description" 
                                                                                                            Name="Description" ReadOnly="True" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="4" Width="5%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="11" Width="10%">
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
                                                                                                        <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="0">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ProductSupplierId" 
                                                                                                            FieldName="ProductSupplierId" Name="ProductSupplierId" 
                                                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Đơn Giá" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="6">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Chiết Khấu (%)" 
                                                                                                            ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số Lượng" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="5" Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" 
                                                                                                            VisibleIndex="10" Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                    <settingspager pagesize="30">
                                                                                                    </settingspager>
                                                                                                    <settingsediting mode="Inline" />

<SettingsEditing Mode="Inline"></SettingsEditing>

                                                                                                    <styles>
                                                                                                        <commandcolumn spacing="10px">
                                                                                                        </commandcolumn>
                                                                                                    </styles>
                                                                                                </dx:ASPxGridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table cellpadding="0" cellspacing="0" class="controlContain" width="100%">
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                &nbsp;
                                                                                                <table class="style68">
                                                                                                    <tr>
                                                                                                        <td class="style70">
                                                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox2" runat="server" CheckState="Unchecked" 
                                                                                                                Text="Quà Tặng">
                                                                                                            </dx:ASPxCheckBox>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dx:ASPxGridView ID="grdProductSupplier0" runat="server" 
                                                                                                                AutoGenerateColumns="False" ClientInstanceName="grdProductSupplier" 
                                                                                                                KeyFieldName="ProductSupplierId" Width="100%">
                                                                                                                <Columns>
                                                                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã Số" FieldName="SupplierCode" 
                                                                                                                        Name="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                                                                                                                        <propertiescombobox callbackpagesize="20" dropdownstyle="DropDown" 
                                                                                                                            enablecallbackmode="True" incrementalfilteringmode="StartsWith" 
                                                                                                                            textfield="Code" textformatstring="{0} {1}" valuefield="Code" width="400px"><Columns>
                                                                                                                            <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" Width="150px"></dx:ListBoxColumn>
                                                                                                                            <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" Width="300px"></dx:ListBoxColumn>
                                                                                                                            </Columns>
                                                                                                                        </propertiescombobox>
                                                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Hàng Hóa" FieldName="SupplierName" 
                                                                                                                        Name="SupplierName" ReadOnly="True" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="3" Width="30%">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Giá Trị" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="6">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="7" Width="5%">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="8" Width="10%">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Description" 
                                                                                                                        Name="Description" ReadOnly="True" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="4" Width="5%">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" 
                                                                                                                        Width="10%">
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
                                                                                                                    <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="0">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="ProductSupplierId" 
                                                                                                                        FieldName="ProductSupplierId" Name="ProductSupplierId" 
                                                                                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Số Lượng" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="5" Width="10%">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="9" Width="10%">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                </Columns>
                                                                                                                <settingspager pagesize="30">
                                                                                                                </settingspager>
                                                                                                                <settingsediting mode="Inline" />

                                                                                                                <styles>
                                                                                                                    <commandcolumn spacing="10px">
                                                                                                                    </commandcolumn>
                                                                                                                </styles>
                                                                                                            </dx:ASPxGridView>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                &nbsp;
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                               </dx:PanelContent>
                                                                                        </PanelCollection>
                                                                                    </dx:ASPxCallbackPanel>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Kế hoạch thanh toán">
                                                                <ContentCollection>
                                                                    <dx:ContentControl>
                                                                        <dx:ASPxGridView ID="grv_SelectPaymentPlan" ClientInstanceName="grv_SelectPaymentPlan" KeyFieldName="Name" 
                                                                            Width="100%" runat="server" OnHtmlRowCreated="grv_SelectPaymentPlan_OnHtmlRowCreated">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Hình thức TT" FieldName="Name" Width="100px">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxComboBox ID="cbo_selectPaytype" runat="server" Value='<%# Eval("Name") %>' >
                                                                                            <Items>
                                                                                                <dx:ListEditItem  Value="L/C" Text="L/C"/>
                                                                                                <dx:ListEditItem  Value="TT" Text="TT"/>
                                                                                                <dx:ListEditItem  Value="DA" Text="DA"/>
                                                                                                <dx:ListEditItem  Value="DP" Text="DP"/>
                                                                                            </Items>
                                                                                        </dx:ASPxComboBox>
                                                                                    </DataItemTemplate>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mô tả" FieldName="Description" Width="150px">
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ghi chú" Width="100%">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxMemo ID="txt_note" runat="server" Width="100%">
                                                                                        </dx:ASPxMemo>
                                                                                    </DataItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác">
                                                                                    <CustomButtons>
                                                                                        <dx:GridViewCommandColumnCustomButton ID="add_payment">
                                                                                            <Image>
                                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                                            </Image>
                                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                                        <dx:GridViewCommandColumnCustomButton ID="delete_payment">
                                                                                            <Image>
                                                                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                                                            </Image>
                                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                                    </CustomButtons>
                                                                                </dx:GridViewCommandColumn>
                                                                            </Columns>
                                                                            <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="false" />
                                                                            <Templates>
                                                                                <DetailRow>
                                                                                    <dx:ASPxGridView ID="grv_PaymentProcess" ClientInstanceName="grv_PaymentProcess" runat="server" KeyFieldName="id" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="Bước" FieldName="seq" Width="30px" >
                                                                                                </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Bước xử lý" FieldName="id" Width="50% ">
                                                                                                <DataItemTemplate>
                                                                                                    <dx:ASPxComboBox ID="cbo_processID" runat="server" Width="100%" ValueField="id">
                                                                                                        <Items>
                                                                                                            <dx:ListEditItem  Value="0" Text="Ký hợp đồng"/>
                                                                                                            <dx:ListEditItem  Value="1" Text="Chuyển tiền"/>
                                                                                                            <dx:ListEditItem  Value="2" Text="Chyển hàng"/>
                                                                                                            <dx:ListEditItem  Value="3" Text="Chứng từ về"/>
                                                                                                            <dx:ListEditItem  Value="4" Text="Hàng về"/>
                                                                                                            <dx:ListEditItem  Value="5" Text="Kê khai và làm thủ tục hải quan"/>
                                                                                                            <dx:ListEditItem  Value="6" Text="Lấy hàng"/>
                                                                                                        </Items>
                                                                                                    </dx:ASPxComboBox>
                                                                                                </DataItemTemplate>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" Width="50%" >
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Thời gian dự kiến" FieldName="duration">
                                                                                                <DataItemTemplate>
                                                                                                    <dx:ASPxTextBox ID="txt_duration" Width="50px" runat="server" Text='<%# Eval("duration") %>'>
                                                                                                    </dx:ASPxTextBox>
                                                                                                </DataItemTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn Caption="Thao tác" ButtonType="Image" Width="100px">
                                                                                                <CustomButtons>
                                                                                                    <dx:GridViewCommandColumnCustomButton>
                                                                                                        <Image ToolTip="Thêm xử lý">
                                                                                                            <SpriteProperties CssClass="Sprite_New" />
                                                                                                        </Image>
                                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                                    <dx:GridViewCommandColumnCustomButton>
                                                                                                        <Image ToolTip="Xóa xử lý">
                                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                        </Image>
                                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                                </CustomButtons>
                                                                                            </dx:GridViewCommandColumn>
                                                                                        </Columns>
                                                                                    </dx:ASPxGridView>
                                                                                </DetailRow>
                                                                            </Templates>
                                                                        </dx:ASPxGridView>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Tiến Độ Giao Hàng">
                                                                <ContentCollection>
                                                                    <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                                        <table style="width:100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxGridView ID="grdBuyingProductCategory4" runat="server" AutoGenerateColumns="False"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="Ngày" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Hàng Hóa" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="2" Width="10%">
                                                                                                <PropertiesComboBox>
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                        <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                                Width="10%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="10"
                                                                                                Width="10%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="12" Width="10%">
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
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="SL Dự Kiến" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="6">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="SL Thực Tế" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="9" Width="10%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="3" Width="30%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                Width="5%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Ngày Giao Dự Kiến" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Ngày Giao Thực Tế" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                        <Styles>
                                                                                            <CommandColumn Spacing="10px">
                                                                                            </CommandColumn>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Name="tabDetail" Text="Tiến Độ Thanh Toán">
                                                                <ContentCollection>
                                                                    <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdBuyingProductCategory5" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%">
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="Hình Thức Thanh Toán" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ngày Trả Dự Kiến" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ngày Trả Thực Tế" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Số Tiền Trả Dự Kiến" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3">
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số Tiền Trả Thực Tế" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Loại Ngoại Tệ" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tỉ Giá" ShowInCustomizationForm="True" 
                                                                                    VisibleIndex="6">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số Tiền Qui Đổi" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Chi Tiết" ShowInCustomizationForm="True" 
                                                                                    VisibleIndex="9">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="10">
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
                                                                            </Columns>
                                                                            <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                                            </SettingsPager>
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <Settings HorizontalScrollBarMode="Visible" />
                                                                            <Styles>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Chi Tiết">
                                                                <ContentCollection>
                                                                    <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="300px" Width="100%">
                                                                            <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                                        </dx:ASPxHtmlEditor>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                        </TabPages>
                                                        <Paddings Padding="5px"></Paddings>
                                                        <ScrollButtonStyle HorizontalAlign="Right">
                                                        </ScrollButtonStyle>
                                                    </dx:ASPxPageControl>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="style25">
                                                    <table cellpadding="0" cellspacing="0"
                                                        width="100%">
                                                        <tr>
                                                            <td class="style64">
                                                                <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Tổng giá trị phiếu mua hàng">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" ValueType="System.String">
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style65">
                                                                &nbsp;
                                                            </td>
                                                            <td class="style63">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style64">
                                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Chú thích">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td>
                                                                <dx:ASPxTextBox ID="ASPxTXTChuThich" runat="server" Width="80%" NullText="Không quá 128 kí tự">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style39" colspan="2">
                                                                &nbsp;
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
                    <FooterContentTemplate>
                        <div style="float:left">
                            <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                                Text="Trợ Giúp" Wrap="False">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                            
                        <div style="float:right;">
                            <dx:ASPxButton ID="ASPxButton4" runat="server" AutoPostBack="False" ClientInstanceName="buttonSave"
                                    Text="Lưu Lại" Wrap="False">
                                        <ClientSideEvents Click="buttonSave_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                        </div> 
                        <div style="float:right;">
                            <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                                    Text="Bỏ Qua" Wrap="False">
                                <ClientSideEvents Click="buttonCancel_Click"></ClientSideEvents>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>       
                          
                        <div style="float:right;">
                            <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" ClientInstanceName="buttonPrint"
                                Text="In">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Print"></SpriteProperties>
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </FooterContentTemplate>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>

