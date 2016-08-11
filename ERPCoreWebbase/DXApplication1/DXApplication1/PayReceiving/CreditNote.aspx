<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CreditNote.aspx.cs" Inherits="WebModule.PayReceiving.CreditNote" %>

<%@ Register Src="UserControl/ReceiptVoucherEdit.ascx" TagName="ReceiptVoucherEdit"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/CreditNoteEdit.ascx" TagName="CreditNoteEdit" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grdData_EndCallback(s, e) {
            formCreditNoteEdit.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:aspxlabel id="lblHeader" runat="server" text="Giấy Báo Có" font-bold="True" font-size="Small">
        </dx:aspxlabel>
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <dx:aspxgridview id="grdData" runat="server" autogeneratecolumns="False" clientinstancename="grdData"
                    onstartrowediting="grdData_StartRowEditing" keyfieldname="Code" width="100%"
                    oncustomcallback="grdData_CustomCallback" oninitnewrow="grdData_InitNewRow">
                    <ClientSideEvents EndCallback="grdData_EndCallback" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" 
            VisibleIndex="0" Width="10%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày " 
            FieldName="Date" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Đối Tượng " FieldName="Customer" 
                            VisibleIndex="2" Width="30%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="Status" 
            VisibleIndex="6" Width="5%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Lý Do Nộp" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Địa Chỉ" FieldName="Address" 
            VisibleIndex="3" Width="20%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
            VisibleIndex="8" Width="5%">
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
                        <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" 
            VisibleIndex="5" Width="10%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Xóa" VisibleIndex="9" 
            Width="5%">
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
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
        AllowSelectSingleRowOnly="True">
                    </SettingsBehavior>
                    <SettingsEditing Mode="Inline">
                    </SettingsEditing>
                </dx:aspxgridview>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:CreditNoteEdit ID="CreditNoteEdit1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
