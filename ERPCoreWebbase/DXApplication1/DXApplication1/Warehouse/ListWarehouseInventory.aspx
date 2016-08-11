<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ListWarehouseInventory.aspx.cs" Inherits="WebModule.Warehouse.ListWarehouseInventory" %>
<%@ Register src="UserControl/uWarehouseInventory.ascx" tagname="uWarehouseInventory" tagprefix="uc1" %>
<%@ Register src="UserControl/uVerifyingInventoryInfo.ascx" tagname="uVerifyingInventoryInfo" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">

    // MainForm Event
    function grdData_EndCallback(s, e) {
        formEntryDetail.Show();

    }

    function ShowInfoclick(s, e) {
        popup_VerifyingInventoryInfo.Show();
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách kiểm kho" 
        Font-Bold="True" Font-Size="Small">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
        oninitnewrow="ASPxGridView1_InitNewRow" Width = "100%"
        onstartrowediting="ASPxGridView1_StartRowEditing">
        <ClientSideEvents RowDblClick="ShowInfoclick" EndCallback = "grdData_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Nhân viên 2" FieldName="staff2" 
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nhân viên 1" FieldName="staff1" 
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trưởng ban" FieldName="admin" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã kiểm kho" FieldName="code" 
                VisibleIndex="0">
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("code") %>'>
                        <ClientSideEvents Click="ShowInfoclick" />
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngày kiểm kho" FieldName="date" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="5" 
                ButtonType="Image">
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
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
        <SettingsPager PageSize="22" ShowEmptyDataRows="true">
        </SettingsPager>
        <Styles>
            <Header HorizontalAlign="Center" Font-Bold="true">
            </Header>             
            <CommandColumn Spacing="10px">
            </CommandColumn>
        </Styles>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"/>
    </dx:ASPxGridView>
    <uc1:uWarehouseInventory ID="uWarehouseInventory" runat="server" /> 
    <dx:ASPxPopupControl ID="popup_VerifyingInventoryInfo" runat="server" HeaderText="Thông Tin Kiểm Kho" Height="400px" ScrollBars="Auto"
        Modal="True" RenderMode="Classic" Width="1000px" ClientInstanceName="popup_VerifyingInventoryInfo"
        AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        LoadingPanelDelay="1000" ShowSizeGrip="False" ShowMaximizeButton="true">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc2:uVerifyingInventoryInfo ID="uVerifyingInventoryInfo1" runat="server" /> 
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
