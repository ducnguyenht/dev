<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uDeliveryEdit.ascx.cs" Inherits="ERPCore.Purchasing.UserControl.uDeliveryEdit" %>
<style type="text/css">
    .style25
    {
    }
    .style39
    {
        height: 18px;
    }
    .style42
    {
    }
    .style46
    {
        width: 571px;
    }
</style>

<div id="lineContainer"> 
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine">
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formDeliveryEdit" runat="server" 
            HeaderText="Tiến Độ Đon Hàng" Height="617px" Modal="True" 
            RenderMode="Lightweight"  
            Width="850px" ClientInstanceName="formDeliveryEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000">
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
                                                <td class="style42">
                                                    <dx:ASPxLabel ID="ASPxLabel16" runat="server" Font-Bold="True" 
                                                        Font-Names="Arial" Font-Size="Medium" Text="Danh sách đơn hàng" >
                                                    </dx:ASPxLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" 
                                                AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <dx:GridViewDataComboBoxColumn Caption="Mã Đơn Hàng" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="10%">
                                                        <PropertiesComboBox>
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                            </Columns>
                                                        </PropertiesComboBox>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewDataTextColumn Caption="Ngày " 
                                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhà Cung Cấp" ShowInCustomizationForm="True" 
                                                        VisibleIndex="2" Width="20%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tổng Thành Tiền" ShowInCustomizationForm="True" 
                                                        VisibleIndex="4" Width="10%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhân Viên Đề Nghị" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="20%">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                        ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
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
                                                <SettingsPager PageSize="30">
                                                </SettingsPager>
                                                <SettingsEditing Mode="Inline" />
                                                <Styles>
                                                    <CommandColumn Spacing="10px">
                                                    </CommandColumn>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                                OnObjectCreated="ObjectDataSource1_ObjectCreated"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style25" colspan="3">
                                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                                                Height="340px" RenderMode="Lightweight" Width="100%">
                                                <TabPages>
                                                    <dx:TabPage Text="Tiến Độ Giao Hàng">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                                <table class="dxflInternalEditorTable_DevEx">
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxScheduler ID="ASPxScheduler1" runat="server" ClientIDMode="AutoID" 
                                                                                Start="2013-08-24" Width="100%">
                                                                                <Views>
                                                                                    <DayView>
                                                                                        <TimeRulers>
                                                                                            <dx:TimeRuler />
                                                                                        </TimeRulers>
                                                                                        <DayViewStyles ScrollAreaHeight="320px">
                                                                                        </DayViewStyles>
                                                                                    </DayView>
                                                                                    <WorkWeekView>
                                                                                        <TimeRulers>
                                                                                            <dx:TimeRuler />
                                                                                        </TimeRulers>
                                                                                    </WorkWeekView>
                                                                                </Views>
                                                                            </dx:ASPxScheduler>
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
                                                    <dx:TabPage Name="tabDetail" Text="Tiến Độ Thanh Toán">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxScheduler ID="ASPxScheduler2" runat="server" ClientIDMode="AutoID" 
                                                                    Start="2013-08-24" Width="100%">
                                                                    <Views>
                                                                        <DayView>
                                                                            <TimeRulers>
                                                                                <dx:TimeRuler />
                                                                            </TimeRulers>
                                                                            <DayViewStyles ScrollAreaHeight="320px">
                                                                            </DayViewStyles>
                                                                        </DayView>
                                                                        <WorkWeekView>
                                                                            <TimeRulers>
                                                                                <dx:TimeRuler />
                                                                            </TimeRulers>
                                                                        </WorkWeekView>
                                                                    </Views>
                                                                </dx:ASPxScheduler>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                </TabPages>
                                            </dx:ASPxPageControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style39" colspan="3">
                                            
                                            </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="style46">
                                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                                                Text="Trợ Giúp"  Wrap="False">
                                                <Image ToolTip="Trợ giúp">
                                                    <SpriteProperties CssClass="Sprite_Help" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="right">
                                            <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="buttonSave" Text="Lưu Lại"  Wrap="False">
                                                <ClientSideEvents Click="buttonSave_Click" />
                                                <Image ToolTip="Lưu">
                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="right">
                                            <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="buttonCancel" Text="Bỏ Qua"  Wrap="False">
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

