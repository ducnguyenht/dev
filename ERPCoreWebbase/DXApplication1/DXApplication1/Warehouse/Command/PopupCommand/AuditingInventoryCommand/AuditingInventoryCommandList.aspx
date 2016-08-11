<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="AuditingInventoryCommandList.aspx.cs" Inherits="WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.AuditingInventoryCommandList" %>
<%@ Register src="uAuditingInventoryCommand.ascx" tagname="uAuditingInventoryCommand" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdInventoryCommand_CustomButtonClick(s, e) {
        if (e.buttonID == 'buttonApprove') {
            grdInventoryCommand.PerformCallback(grdInventoryCommand.GetRowKey(grdInventoryCommand.GetFocusedRowIndex()));
        }
    }  

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
  <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách phiếu kiểm kê" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="grdInventoryCommand" 
        ClientInstanceName="grdInventoryCommand" runat="server" AutoGenerateColumns="False" 
        DataSourceID="InventoryAuditArtifactXDS" KeyFieldName="InventoryCommandId" 
        Width="100%" oninitnewrow="grdInventoryCommand_InitNewRow" 
        onrowdeleting="grdInventoryCommand_RowDeleting" 
        onstartrowediting="grdInventoryCommand_StartRowEditing" 
        oncustomcolumndisplaytext="grdInventoryCommand_CustomColumnDisplayText" 
        oncustombuttoninitialize="grdInventoryCommand_CustomButtonInitialize" 
        oncustomcallback="grdInventoryCommand_CustomCallback" 
        oncustomunboundcolumndata="grdInventoryCommand_CustomUnboundColumnData">
<ClientSideEvents CustomButtonClick="grdInventoryCommand_CustomButtonClick" 
            EndCallback="grdInventoryCommand_EndCallback"></ClientSideEvents>

<SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" 
            ConfirmDelete="True"></SettingsBehavior>

<Settings ShowFilterRow="True"></Settings>

<SettingsText EmptyDataRow="Chưa có phiếu kiểm kê" 
            confirmdelete="Xóa phiếu kiểm kê này ?"></SettingsText>
        <ClientSideEvents CustomButtonClick="grdInventoryCommand_CustomButtonClick" 
            EndCallback="grdInventoryCommand_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã phiếu kiểm kê" FieldName="Code" 
                VisibleIndex="2" Width="100px">
            </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="InventoryCommandId" VisibleIndex="11" ReadOnly="True" 
                Visible="False"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CommandType" VisibleIndex="12" 
                Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="CreateDate" 
                VisibleIndex="13" Visible="False">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
                VisibleIndex="5" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày tạo" FieldName="IssueDate" 
                VisibleIndex="4" Width="100px">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn FieldName="LastUpdateDate" Visible="False" 
                VisibleIndex="14">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Tên phiếu kiểm kê" FieldName="Name" 
                VisibleIndex="3" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RelevantInventoryId!Key" Visible="False" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ParentInventoryCommandId!Key" 
                Visible="False" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RowStatus" Visible="False" 
                VisibleIndex="9">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Kho" FieldName="InventoryId.Name" 
                VisibleIndex="0" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="ApprovalStatus" 
                VisibleIndex="6" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trưởng ban kiểm kê" FieldName="PersonName" 
                UnboundType="String" VisibleIndex="8" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                VisibleIndex="10" Width="100px">
                <EditButton Visible="True">
                    <Image ToolTip="Sửa">
                        <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                    </Image>
                </EditButton>
                <NewButton Visible="True">
                    <Image ToolTip="Thêm">
                        <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                    </Image>
                </NewButton>
                <DeleteButton Visible="True">
                    <Image ToolTip="Xóa">
                        <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                    </Image>
                </DeleteButton>
                <CancelButton>
                    <Image ToolTip="Bỏ qua">
                        <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                    </Image>
                </CancelButton>
                <UpdateButton>
                    <Image ToolTip="Cập nhật">
                        <SpriteProperties CssClass="Sprite_Update" />
<SpriteProperties CssClass="Sprite_Update"></SpriteProperties>
                    </Image>
                </UpdateButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="buttonApprove">
                        <Image ToolTip="Duyệt">
                            <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>       
        <SettingsBehavior AllowSort="true" AllowSelectSingleRowOnly="true" 
            AllowFocusedRow="True" ConfirmDelete="True" />
        <Settings ShowFilterRow="true" GridLines="Both" />
        <SettingsText EmptyDataRow="Chưa có phiếu kho" />

        <SettingsLoadingPanel Mode="Disabled" ShowImage="False" />
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="InventoryAuditArtifactXDS" runat="server" 
        Criteria="RowStatus &gt;= 1" 
        TypeName="NAS.DAL.Inventory.Audit.InventoryAuditArtifact">
    </dx:XpoDataSource>
    <uc1:uAuditingInventoryCommand ID="uAuditingInventoryCommand1" runat="server" />
</asp:Content>
