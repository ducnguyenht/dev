<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewVoucherAllocation_Backup.ascx.cs"
    Inherits="WebModule.Voucher.Controls.GridViewVoucherAllocation_Backup" %>
<%@ Register src="../../Accounting/AllocationConfigure/Controls/AccountActorComboBox.ascx" tagname="AccountActorComboBox" tagprefix="uc1" %>
<script type="text/javascript">
    var GridViewVoucherAllocation = {
        events: { eListChanged: 'GridViewVoucherAllocationChanged' }
    };

    function gridVoucherAllocation_EndCallback(s, e) {
        if (s.cpEvent == 'ListChanged') {
            $(GridViewVoucherAllocation).triggerHandler(GridViewVoucherAllocation.events.eListChanged);
            delete s.cpEvent;
        }
        else if (s.cpEvent == 'StartRowEditing') {
            if (!cpnMasterAcountActor.InCallback()) {
                cpnMasterAcountActor.PerformCallback();
            }
            delete s.cpEvent;
        }
    } 
</script>
<dx:ASPxGridView ID="gridVoucherAllocation" ClientInstanceName="gridVoucherAllocation"
    runat="server" AutoGenerateColumns="False" DataSourceID="dsVoucherAllocation"
    KeyFieldName="VoucherAllocationId" Width="100%" OnCustomColumnDisplayText="gridVoucherAllocation_CustomColumnDisplayText"
    OnRowDeleted="gridVoucherAllocation_RowDeleted" OnRowInserted="gridVoucherAllocation_RowInserted"
    OnRowInserting="gridVoucherAllocation_RowInserting" 
    OnRowUpdated="gridVoucherAllocation_RowUpdated" 
    onstartrowediting="gridVoucherAllocation_StartRowEditing" ValidateRequestMode="Enabled">
    <ClientSideEvents EndCallback="gridVoucherAllocation_EndCallback"/>
    <Columns>
        <dx:GridViewDataTextColumn Caption="Số tiền" FieldName="Amount" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mục đích phân bổ" FieldName="Name" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Thể loại" FieldName="AllocationId!Key" VisibleIndex="1">
            <EditItemTemplate>
                <dx:ASPxComboBox ID="cboAllocation" runat="server" CallbackPageSize="10" EnableCallbackMode="True"
                    IncrementalFilteringMode="Contains" Value='<%# Bind("[AllocationId!Key]") %>'
                    TextField="Name" ValueField="AllocationId" ValueType="System.Guid" OnInit="cboAllocation_Init">
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                    </ValidationSettings>
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { 
                        //Update description
                        //Update master account actor combobox
                        if(!cpnMasterAcountActor.InCallback()) {
                            cpnMasterAcountActor.PerformCallback();
                        }
                     }" />
                </dx:ASPxComboBox>
                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="ASPxButton">
                </dx:ASPxButton>
            </EditItemTemplate>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataTextColumn Caption="Đối tượng chính" VisibleIndex="4" Name="MasterAccountActor">
            <EditItemTemplate>
                <dx:ASPxCallbackPanel ID="cpnMasterAcountActor" ClientInstanceName="cpnMasterAcountActor"
                    runat="server" Width="100%" OnCallback="cpnMasterAcountActor_Callback" 
                    oninit="cpnMasterAcountActor_Init" onload="cpnMasterAcountActor_Load">
                    <PanelCollection>
                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <uc1:AccountActorComboBox ID="accountActorComboBox" runat="server" />
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đối tượng liên quan" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Description" VisibleIndex="6">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="8" Width="100px">
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
    <SettingsBehavior ColumnResizeMode="NextColumn" />
    <SettingsCookies Enabled="True" StoreFiltering="False" StorePaging="False" />
    <SettingsEditing Mode="Inline" />
</dx:ASPxGridView>
<dx:XpoDataSource ID="dsVoucherAllocation" runat="server" TypeName="NAS.DAL.Vouches.Allocation.VoucherAllocation"
    Criteria="[VouchesId!Key] = ?">
</dx:XpoDataSource>
