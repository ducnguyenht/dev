<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uRequitementEdit.ascx.cs" Inherits="WebModule.ImExporting.UserControl.uRequitementEdit" %>
<style type="text/css">
    .dxflInternalEditorTable_DevEx {
    width: 100%;
}
img
{
	border-width: 0;
}

.dxtcControl 
{
	font: 12px Tahoma, Geneva, sans-serif;
	color: black;
}
.dxfmControl,
.dxnbGroupHeader,
.dxnbGroupHeaderCollapsed,
.dxnbGroupContent > TABLE > TBODY > TR,
.dxtcTab,
.dxtcActiveTab,
.dxtv-nd
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxtcActiveTab,
.dxtcActiveTabWithTabPositionBottom,
.dxtcActiveTabWithTabPositionLeft,
.dxtcActiveTabWithTabPositionRight
{
	border: 1px solid #A8A8A8;
	padding: 3px 12px 4px;
	background-color: #FFFFFF;
	text-align: center;
}
.dxtcActiveTab td.dxtc,
.dxtcActiveTabWithTabPositionBottom td.dxtc,
.dxtcActiveTabWithTabPositionLeft td.dxtc,
.dxtcActiveTabWithTabPositionRight td.dxtc
{
	white-space: nowrap;
	background: transparent none!important;
	border-width: 0px!important;
	padding: 0px!important;
}
.dxtcTab,
.dxtcTabWithTabPositionLeft, 
.dxtcTabWithTabPositionBottom,
.dxtcTabWithTabPositionRight
{
	background-color: #E0E0E0;
	border: 1px solid #A8A8A8;
	padding: 3px 12px 4px;
	text-align: center;
}
.dxtcTab td.dxtc,
.dxtcTabWithTabPositionBottom td.dxtc, 
.dxtcTabWithTabPositionLeft td.dxtc,
.dxtcTabWithTabPositionRight td.dxtc
{
	white-space: nowrap;
	background: transparent none!important;
	border-width: 0px!important;
	padding: 0px!important;
}
.dxtcRightAlignCell,
.dxtcTabsCellWithTabPositionBottom .dxtcRightAlignCell 
{
	text-align: right;
}
.dxtcPageContent,
.dxtcPageContentWithTabPositionBottom, 
.dxtcPageContentWithTabPositionLeft,
.dxtcPageContentWithTabPositionRight,
.dxtcPageContentWithoutTabs
{
	background-color: white;
	vertical-align: top;
}
.dxpcHBCellSys
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
    -webkit-touch-callout: none;
}

    .style33
    {
        width: 98px;
    }
    .style43
    {
        width: 171px;
    }
    .style42
    {
        width: 135px;
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
    .style44
    {
        width: 575px;
    }
</style>
        <dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" Modal="True" AllowDragging="True" 
    AllowResize="True" ClientInstanceName="formRequitementEdit" 
    HeaderText="Phiếu Y&#234;u Cầu" LoadingPanelDelay="1000" 
    RenderMode="Lightweight"  Width="850px" Height="617px" 
    ID="formRequitementEdit"><ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">                         
                    <table style="width:100%; margin-top:10px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <table align="right" style="width:100%;">
                                <tr>
                                    <td align="left" class="style25" colspan="3">
                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                            <tr>
                                                <td class="style33">
                                                    <dx:ASPxLabel runat="server" Text="M&#227; Số"  ID="ASPxLabel1"></dx:ASPxLabel>

                                                </td>
                                                <td class="style43">
                                                    <dx:ASPxTextBox runat="server" NullText="Tối đa 128 k&#253; tự" Width="200px" 
                                                        ClientInstanceName="txtCode"  ID="txtCode">
<NullTextStyle ForeColor="Silver"></NullTextStyle>

<ValidationSettings ErrorText="" SetFocusOnError="True">
<RequiredField IsRequired="True" ErrorText="Chưa nhập M&#227; Nh&#224; Cung Cấp"></RequiredField>
</ValidationSettings>
</dx:ASPxTextBox>

                                                </td>
                                                <td class="style42">
                                                    <dx:ASPxLabel runat="server" Text="Nh&#226;n Vi&#234;n Y&#234;u Cầu" 
                                                         ID="ASPxLabel15"></dx:ASPxLabel>

                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox runat="server" NullText="Tự động tạo mới" Width="200px" 
                                                        ClientInstanceName="cboRowStatus" ID="cboRowStatus0"><Items>
<dx:ListEditItem Text="Đang sử dụng" Value="A"></dx:ListEditItem>
<dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;"></dx:ListEditItem>
</Items>
</dx:ASPxComboBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style31" colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style33">
                                                    <dx:ASPxLabel runat="server" Text="Ng&#224;y y&#234;u cầu"  
                                                        ID="ASPxLabel7"></dx:ASPxLabel>

                                                </td>
                                                <td class="style43">
                                                    <dx:ASPxDateEdit runat="server" Width="200px" ID="ASPxDateEdit1"></dx:ASPxDateEdit>

                                                </td>
                                                <td class="style42">
                                                    <dx:ASPxLabel runat="server" Text="Mục Đ&#237;ch"  
                                                        ID="ASPxLabel16"></dx:ASPxLabel>

                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox runat="server" NullText="Tự động tạo mới" Width="200px" 
                                                        ClientInstanceName="cboRowStatus" ID="cboRowStatus1"><Items>
<dx:ListEditItem Text="Đang sử dụng" Value="A"></dx:ListEditItem>
<dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;"></dx:ListEditItem>
</Items>
</dx:ASPxComboBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                </td>
                                                <td class="style38" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style37">
                                                    <dx:ASPxLabel runat="server" Text="Ghi Ch&#250;"  ID="ASPxLabel17"></dx:ASPxLabel>

                                                </td>
                                                <td class="style38" colspan="3">
                                                    <dx:ASPxTextBox runat="server" NullText="Tối đa 128 k&#253; tự" Width="565px" 
                                                        Height="16px" ClientInstanceName="txtCode"  ID="txtCode0">
<NullTextStyle ForeColor="Silver"></NullTextStyle>

<ValidationSettings ErrorText="" SetFocusOnError="True">
<RequiredField IsRequired="True" ErrorText="Chưa nhập M&#227; Nh&#224; Cung Cấp"></RequiredField>
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
                                                <td class="style43">
                                                    &nbsp;</td>
                                                <td class="style42">
                                                    &nbsp;</td>
                                                <td>
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
                                            <dx:ASPxPageControl runat="server" ActiveTabIndex="0" RenderMode="Lightweight" 
                                                Width="100%" Height="386px" ID="ASPxPageControl1"><TabPages>
<dx:TabPage Name="tabGeneral" Text="H&#224;ng H&#243;a"><ContentCollection>
<dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                                                    <tr>
                                                                        <td>
                                                                            <table class="dxflInternalEditorTable_DevEx">
                                                                                <tr>
                                                                                    <td>
                                                                                        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" 
                                                                                            ID="grdBuyingProductCategory"><Columns>
<dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số" VisibleIndex="1">
<PropertiesComboBox><Columns>
<dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
<dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
</Columns>
</PropertiesComboBox>
</dx:GridViewDataComboBoxColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT" VisibleIndex="3"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần " VisibleIndex="5"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;" VisibleIndex="6"></dx:GridViewDataTextColumn>
<dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%" Caption="Thao T&#225;c" VisibleIndex="7">
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
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu" VisibleIndex="4"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="H&#224;ng H&#243;a" VisibleIndex="2"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT" VisibleIndex="0"></dx:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords" PageSize="30"></SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Styles>
<CommandColumn Spacing="10px"></CommandColumn>
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
<dx:TabPage Text="Nguy&#234;n Vật Liệu"><ContentCollection>
<dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <table class="dxflInternalEditorTable_DevEx">
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" 
                                                                                ID="grdBuyingProductCategory0"><Columns>
<dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số" VisibleIndex="1">
<PropertiesComboBox><Columns>
<dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
<dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
</Columns>
</PropertiesComboBox>
</dx:GridViewDataComboBoxColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT" VisibleIndex="3"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần " VisibleIndex="5"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;" VisibleIndex="6"></dx:GridViewDataTextColumn>
<dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%" Caption="Thao T&#225;c" VisibleIndex="7">
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
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu" VisibleIndex="4"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="H&#224;ng H&#243;a" VisibleIndex="2"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT" VisibleIndex="0"></dx:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords" PageSize="30"></SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Styles>
<CommandColumn Spacing="10px"></CommandColumn>
</Styles>
</dx:ASPxGridView>

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
<dx:TabPage Text="Dịch Vụ"><ContentCollection>
<dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" 
                                                                    ID="grdBuyingProductCategory1"><Columns>
<dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số" VisibleIndex="1">
<PropertiesComboBox><Columns>
<dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
<dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
</Columns>
</PropertiesComboBox>
</dx:GridViewDataComboBoxColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT" VisibleIndex="3"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần " VisibleIndex="5"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;" VisibleIndex="6"></dx:GridViewDataTextColumn>
<dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%" Caption="Thao T&#225;c" VisibleIndex="7">
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
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu" VisibleIndex="4"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="Dịch Vụ" VisibleIndex="2"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT" VisibleIndex="0"></dx:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords" PageSize="30"></SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Styles>
<CommandColumn Spacing="10px"></CommandColumn>
</Styles>
</dx:ASPxGridView>

                                                            </dx:ContentControl>
</ContentCollection>
</dx:TabPage>
<dx:TabPage Text="C&#244;ng Cụ Dụng Cụ"><ContentCollection>
<dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" 
                                                                    ID="grdBuyingProductCategory2"><Columns>
<dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số" VisibleIndex="1">
<PropertiesComboBox><Columns>
<dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
<dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
</Columns>
</PropertiesComboBox>
</dx:GridViewDataComboBoxColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT" VisibleIndex="3"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần " VisibleIndex="5"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;" VisibleIndex="6"></dx:GridViewDataTextColumn>
<dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%" Caption="Thao T&#225;c" VisibleIndex="7">
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
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu" VisibleIndex="4"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="C&#244;ng Cụ" VisibleIndex="2"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT" VisibleIndex="0"></dx:GridViewDataTextColumn>
</Columns>

<SettingsPager Mode="ShowAllRecords" PageSize="30"></SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Styles>
<CommandColumn Spacing="10px"></CommandColumn>
</Styles>
</dx:ASPxGridView>

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
                                            <dx:ASPxButton runat="server" AutoPostBack="False" Text="Trợ Gi&#250;p" 
                                                 ID="buttonHelp">
                                                <Image ToolTip="Trợ giúp">
                                                    <SpriteProperties CssClass="Sprite_Help" />
                                                </Image>
</dx:ASPxButton>

                                        </td>
                                        <td align="right">
                                            <dx:ASPxButton runat="server" AutoPostBack="False" 
                                                ClientInstanceName="buttonSave" Text="Lưu Lại"  ID="buttonAccept">
<ClientSideEvents Click="buttonSave_Click"></ClientSideEvents>

                                                <Image ToolTip="Lưu">
                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                </Image>
</dx:ASPxButton>

                                        </td>
                                        <td align="right">
                                            <dx:ASPxButton runat="server" AutoPostBack="False" 
                                                ClientInstanceName="buttonCancel" Text="Bỏ Qua"  ID="buttonCancel">
<ClientSideEvents Click="buttonCancel_Click"></ClientSideEvents>

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


    