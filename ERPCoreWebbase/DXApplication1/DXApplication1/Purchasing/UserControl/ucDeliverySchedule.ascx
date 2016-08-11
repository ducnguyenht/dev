<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDeliverySchedule.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.ucDeliverySchedule" %>
<script>
    function cbItemsLostFocus() {
        if (!cb_Lot.InCallback()) {
            cb_Lot.PerformCallback(cb_Item.lastSuccessValue);
        }
    }
    function RefreshucDeliverySchedule() {
        
    }
</script>
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Kế Hoạch">
            <Items>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxGridView ID="GridDeliveryPlanning" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="GridDeliveryPlanning" KeyFieldName="TransactionId" Width="100%"
                                OnRowDeleting="GridDeliveryPlanning_RowDeleting" OnRowInserting="GridDeliveryPlanning_RowInserting"
                                OnRowUpdating="GridDeliveryPlanning_RowUpdating" OnInitNewRow="GridDeliveryPlanning_InitNewRow"
                                OnStartRowEditing="GridDeliveryPlanning_StartRowEditing" OnCellEditorInitialize="GridDeliveryPlanning_CellEditorInitialize"
                                OnCustomColumnDisplayText="GridDeliveryPlanning_CustomColumnDisplayText">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Tên đợt giao" FieldName="Code" ShowInCustomizationForm="True"
                                        VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="3"
                                        Width="120px" ButtonType="Image">
                                        <EditButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </EditButton>
                                        <NewButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_New" />
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </NewButton>
                                        <DeleteButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </DeleteButton>
                                        <CancelButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                            </Image>
                                        </CancelButton>
                                        <UpdateButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                            </Image>
                                        </UpdateButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" ShowInCustomizationForm="True"
                                        VisibleIndex="0" Width="100px">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                        </PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                </Columns>
                                <SettingsEditing Mode="Inline" />
                                <SettingsDetail ShowDetailRow="True" />
                                <Styles>
                                    <Header HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                                <Templates>
                                    <DetailRow>
                                        <dx:ASPxGridView ID="GridPlanningJournal" runat="server" AutoGenerateColumns="False"
                                            Width="100%" OnInitNewRow="GridPlanningJournal_InitNewRow" OnRowDeleting="GridPlanningJournal_RowDeleting"
                                            OnRowInserting="GridPlanningJournal_RowInserting" OnRowUpdating="GridPlanningJournal_RowUpdating"
                                            OnStartRowEditing="GridPlanningJournal_StartRowEditing" OnBeforePerformDataSelect="GridPlanningJournal_BeforePerformDataSelect"
                                            ClientInstanceName="GridPlanningJournal" OnCellEditorInitialize="GridPlanningJournal_CellEditorInitialize"
                                            OnCustomColumnDisplayText="GridPlanningJournal_CustomColumnDisplayText">
                                            <Columns>
                                                <dx:GridViewDataComboBoxColumn Caption="Hàng hóa- Dịch vụ" FieldName="ItemUnitId!Key"
                                                    VisibleIndex="0" Width="120px">
                                                    <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                        OnItemsRequestedByFilterCondition="comboItemUnit_ItemsRequestedByFilterCondition"
                                                        OnItemRequestedByValue="comboItemUnit_ItemRequestedByValue" ValueField="ItemUnitId!Key"
                                                        ValueType="System.Guid" TextFormatString="{0}" 
                                                        TextField="ItemUnitId.ItemId.Code" ClientInstanceName="cb_Item">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Tên" FieldName="ItemUnitId.ItemId.Name" />
                                                            <dx:ListBoxColumn Caption="Mã HHDV" FieldName="ItemUnitId.ItemId.Code" />
                                                            <dx:ListBoxColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Name" />
                                                        </Columns>
                                                        <ClientSideEvents SelectedIndexChanged="cbItemsLostFocus" />
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" VisibleIndex="1" FieldName="ItemUnitId.UnitId.Name"
                                                    Width="70px">
                                                    <PropertiesTextEdit DisplayFormatString="g">
                                                    </PropertiesTextEdit>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Số lượng dự kiến" VisibleIndex="2" FieldName="Credit"
                                                    Width="70px">
                                                    <PropertiesSpinEdit DisplayFormatString="g">
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="Số lô" FieldName="LotId!Key" VisibleIndex="3"
                                                    Width="100px">
                                                    <PropertiesComboBox TextField="Code" TextFormatString="{0}" ValueField="LotId" ValueType="System.Guid"
                                                        EnableCallbackMode="true" IncrementalFilteringMode="Contains" OnItemRequestedByValue="comboLot_ItemRequestedByValue"                                                        
                                                        OnItemsRequestedByFilterCondition="comboLot_ItemsRequestedByFilterCondition" 
                                                        ClientInstanceName="cb_Lot">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Số lô" FieldName="Code" Width="100px" />
                                                            <dx:ListBoxColumn Caption="Hạn sử dụng" FieldName="ExpireDate" Width="100px" />
                                                        </Columns>
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="Kho" FieldName="InventoryId!Key" VisibleIndex="4">
                                                    <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                        OnItemRequestedByValue="comboInventory_ItemRequestedByValue" OnItemsRequestedByFilterCondition="comboInventory_ItemsRequestedByFilterCondition"
                                                        TextField="Name" TextFormatString="{1}" ValueField="InventoryId" ValueType="System.Guid">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Mã kho" FieldName="Code" Width="100px" />
                                                            <dx:ListBoxColumn Caption="Tên kho" FieldName="Name" Width="200px" />
                                                        </Columns>
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataTextColumn Caption="Diễn giải" VisibleIndex="5" FieldName="Description">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="6" Width="100px" ButtonType="Image">
                                                    <EditButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                        </Image>
                                                    </EditButton>
                                                    <NewButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New" />
                                                        </Image>
                                                    </NewButton>
                                                    <DeleteButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                        </Image>
                                                    </DeleteButton>
                                                    <CancelButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                        </Image>
                                                    </CancelButton>
                                                    <UpdateButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                        </Image>
                                                    </UpdateButton>
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <SettingsEditing Mode="Inline" />
                                            <Styles>
                                                <Header HorizontalAlign="Center">
                                                </Header>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </DetailRow>
                                </Templates>
                            </dx:ASPxGridView>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Thực Giao">
            <Items>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxGridView ID="GridDeliveryActual" runat="server" AutoGenerateColumns="False"
                                ClientInstanceName="GridDeliveryActual" Width="100%" KeyFieldName="InventoryJournalId">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Ngày" FieldName="InventoryTransactionId.IssueDate"
                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên đợt giao" FieldName="InventoryTransactionId.Code"
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Hàng hóa - Dịch vụ" FieldName="ItemUnitId.ItemId.Name"
                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="ItemUnitId.UnitId.Name" ShowInCustomizationForm="True"
                                        VisibleIndex="3" Width="60px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Credit" ShowInCustomizationForm="True"
                                        VisibleIndex="4" Width="60px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="LotId.Code" ShowInCustomizationForm="True"
                                        VisibleIndex="5" Width="70px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Kho" FieldName="ToInventoryId.Code" ShowInCustomizationForm="True"
                                        VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                        VisibleIndex="7">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                    <Header HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>
