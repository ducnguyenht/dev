<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BookAccount.aspx.cs" Inherits="ERPCore.Accounting.BookAccount" %>

<%@ Register Src="UserControl/BookAccountEdit1.ascx" TagName="BookAccountEdit1" TagPrefix="uc1" %>
<%@ Register Src="UserControl/BookAccountEdit2.ascx" TagName="BookAccountEdit2" TagPrefix="uc2" %>
<%@ Register Src="UserControl/uConfigEntry.ascx" TagName="uConfigEntry" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grdData_EndCallback(s, e) {
            if (s.cpEdit) {
                if (s.cpEdit == "1") {
                    formBookAccountEdit1.Show();
                    formBookAccountEdit1.SetHeaderText("Sơ đồ định khoản: " + s.cpTypeName);
                }
                else if (s.cpEdit == "2") {
                    pop_chdkthuchi.Show();
                    cp1.PerformCallback(s.cpTypeName);
                }
                else {
                    pop_configentry.Show();
                    pop_configentry.SetHeaderText("Sơ đồ định khoản: " + s.cpTypeName);
                }

                delete s.cpEdit;
                delete s.cpTypeName;
            }
        }
        function btncancelconfigentry_click(s, e) {
            pop_configentry.Hide();
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Sơ Đồ Định Khoản" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
        OnStartRowEditing="grdData_StartRowEditing" KeyFieldName="Stt" Width="100%" OnInitNewRow="grdData_InitNewRow">
        <ClientSideEvents EndCallback="grdData_EndCallback"></ClientSideEvents>
        <SettingsEditing Mode="Inline"></SettingsEditing>
        <ClientSideEvents EndCallback="grdData_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="STT" FieldName="Stt" VisibleIndex="0" 
                Width="35px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" Caption="Tên Sơ Đồ Định Khoản" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Phân Loại" FieldName="Type" Name="Type" VisibleIndex="3">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Chung" Value="Chung" />
                        <dx:ListEditItem Text="NhapKho" Value="NhapKho" />
                        <dx:ListEditItem Text="XuatKho" Value="XuatKho" />
                        <dx:ListEditItem Text="Chi" Value="Chi" />
                        <dx:ListEditItem Text="Thu" Value="Thu" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="5">
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
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear" />
                    </Image>
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Hệ Thống TK" FieldName="Account" 
                VisibleIndex="4" Width="80px">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsEditing Mode="Inline" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowFilterRowMenuLikeItem="True" ShowHeaderFilterButton="True" />
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridView>
    <uc1:BookAccountEdit1 ID="BookAccountEdit11" runat="server" />
    <uc2:BookAccountEdit2 ID="BookAccountEdit21" runat="server" />
    <uc3:uConfigEntry ID="uConfigEntry1" runat="server" />
</asp:Content>
