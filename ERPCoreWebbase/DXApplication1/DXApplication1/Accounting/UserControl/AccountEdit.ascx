<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountEdit.ascx.cs"
    Inherits="ERPCore.Accounting.UserControl.AccountEdit" %>
<style type="text/css">
    .style25
    {
        width: 609px;
    }
    .style26
    {
        height: 17px;
    }
    
    
    .dxgv
    {
        white-space: nowrap;
        text-overflow: ellipsis;
    }
    
    .style27
    {
        height: 78px;
    }
</style>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine"
        OnCallback="cpLine_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formAccountEdit" runat="server" HeaderText="Cấu Hình Hệ Thống Tài Khoản"
                    Height="617px" Modal="True" RenderMode="Lightweight" Width="850px" ClientInstanceName="formAccountEdit"
                    AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    LoadingPanelDelay="1000">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="520px"
                                RenderMode="Lightweight" Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Cấu Hình">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1A" runat="server" SupportsDisabledAttribute="True">
                                                <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx" style="height: 300px;
                                                    width: 100%;">
                                                    <tr>
                                                        <td valign="top" width="800px">
                                                            <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" KeyFieldName="OrganizationId"
                                                                ParentFieldName="ParentOrganizationId" Width="100%">
                                                                <Columns>
                                                                    <dx:TreeListTextColumn Caption="Loại Tài Khoản" FieldName="LoaiTaiKhoan" ShowInCustomizationForm="True"
                                                                        VisibleIndex="0">
                                                                    </dx:TreeListTextColumn>
                                                                    <dx:TreeListTextColumn Caption="Số Tài Khoản" FieldName="SoTaiKhoan" ShowInCustomizationForm="True"
                                                                        VisibleIndex="1">
                                                                    </dx:TreeListTextColumn>
                                                                    <dx:TreeListTextColumn Caption="Cấp" FieldName="Cap" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                    </dx:TreeListTextColumn>
                                                                    <dx:TreeListTextColumn Caption="Tên Tài Khoản" FieldName="TenTaiKhoan" ShowInCustomizationForm="True"
                                                                        VisibleIndex="3">
                                                                    </dx:TreeListTextColumn>
                                                                    <dx:TreeListTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                                                        VisibleIndex="4">
                                                                    </dx:TreeListTextColumn>
                                                                    <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                        VisibleIndex="5">
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
                                                                    </dx:TreeListCommandColumn>
                                                                </Columns>
                                                            </dx:ASPxTreeList>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" UploadMode="Auto" Width="280px"
                                                                NullText="Chọn tập tin để Upload">
                                                            </dx:ASPxUploadControl>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Upload">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Tải Mẫu">
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                            <table style="width: 100%; margin-top: 10px">
                                <tr>
                                    <td>
                                        <table align="right" style="width: 100%;">
                                            <tr>
                                                <td align="left" class="style25">
                                                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                                        <Image ToolTip="Trợ giúp">
                                                            <SpriteProperties CssClass="Sprite_Help" />
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" ClientInstanceName="buttonSave"
                                                        Text="Lưu Lại">
                                                        <ClientSideEvents Click="buttonSave_Click" />
                                                        <Image ToolTip="Lưu">
                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                                                        Text="Bỏ Qua">
                                                        <ClientSideEvents Click="buttonCancel_Click" />
                                                        <Image ToolTip="Bỏ qua">
                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                        </Image>
                                                    </dx:ASPxButton>
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
</div>
