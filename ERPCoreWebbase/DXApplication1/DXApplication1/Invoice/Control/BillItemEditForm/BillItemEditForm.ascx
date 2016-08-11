<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BillItemEditForm.ascx.cs"
    Inherits="WebModule.Invoice.Control.BillItemEditForm.BillItemEditForm" %>
<script type="text/javascript">
    
    var BillItemEditForm = function () {
        this.GridViewDataChanged = new NASClientEvent();
        this.RaiseGridViewDataChanged = function()
        {
            this.GridViewDataChanged.FireEvent(this, null);
        };

        this.SelectedTradingItemIndexChanged = new NASClientEvent();
        this.RaiseSelectedTradingItemIndexChanged = function(s, e)
        {
            this.SelectedTradingItemIndexChanged.FireEvent(s, e);
        };

        this.SelectedTradingUnitIndexChanged = new NASClientEvent();
        this.RaiseSelectedTradingUnitIndexChanged = function(s, e)
        {
            this.SelectedTradingUnitIndexChanged.FireEvent(s, e);
        };

        this.StartRowEditing = new NASClientEvent();
        this.RaiseStartRowEditing = function(s, e)
        {
            this.StartRowEditing.FireEvent(s, e);
        };

        this.Refresh = function()
        {
            gridviewBillItem.Refresh();
        };

        this.GetComboBoxItem = function()
        {
            return gridviewBillItem.GetEditor("ItemId!Key");
        }; 

        this.GetComboBoxUnit = function()
        {
            return gridviewBillItem.GetEditor("UnitId!Key");
        }; 

        this.FieldNames = {
            Item : 'ItemId!Key',
            Unit : 'UnitId!Key'
        };

        this.GetGridView = function() {
            return gridviewBillItem;
        };
    };
    
    var nasObj = new BillItemEditForm();
    window['<%= _ClientInstanceName %>'] = nasObj;

    <% if(ClientSideEvents.GridViewDataChanged != null && ClientSideEvents.GridViewDataChanged.Trim().Length > 0) { %>
        nasObj.GridViewDataChanged.AddHandler(<%= ClientSideEvents.GridViewDataChanged %>);
    <% } %>

    <% if(ClientSideEvents.SelectedTradingItemIndexChanged != null && ClientSideEvents.SelectedTradingItemIndexChanged.Trim().Length > 0) { %>
        nasObj.SelectedTradingItemIndexChanged.AddHandler(<%= ClientSideEvents.SelectedTradingItemIndexChanged %>);
    <% } %>

    <% if(ClientSideEvents.SelectedTradingUnitIndexChanged != null && ClientSideEvents.SelectedTradingUnitIndexChanged.Trim().Length > 0) { %>
        nasObj.SelectedTradingUnitIndexChanged.AddHandler(<%= ClientSideEvents.SelectedTradingUnitIndexChanged %>);
    <% } %>

    <% if(ClientSideEvents.StartRowEditing != null && ClientSideEvents.StartRowEditing.Trim().Length > 0) { %>
        nasObj.StartRowEditing.AddHandler(<%= ClientSideEvents.StartRowEditing %>);
    <% } %>

    var lastItemForUnitLoading = null;
    var lastItemForVATLoading = null;
    function comboItem_SelectedIndexChanged(s, e) {
        //process unit cascading loading by item
        if (gridviewBillItem.GetEditor("UnitId!Key").InCallback())
            lastItemForUnitLoading = s.GetValue().toString();
        else
            gridviewBillItem.GetEditor("UnitId!Key").PerformCallback(s.GetValue().toString());
        //process get VAT of item
        if(panelItemVAT.InCallback())
            lastItemForVATLoading = s.GetValue().toString();
        else
            panelItemVAT.PerformCallback(s.GetValue().toString());

        //Raise event
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.RaiseSelectedTradingItemIndexChanged(s, e);
    }

    function comboUnit_SelectedIndexChanged(s, e)
    {
        //Raise event
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.RaiseSelectedTradingUnitIndexChanged(s, e);
    }

    function comboUnit_EndCallback(s, e) {
        if (lastItemForUnitLoading) {
            gridviewBillItem.GetEditor("UnitId!Key").PerformCallback(lastItemForUnitLoading);
            lastItemForUnitLoading = null;
        } 
    }

    function panelItemVAT_EndCallback(s, e) {
        if (lastItemForVATLoading) {
            panelItemVAT.PerformCallback(lastItemForVATLoading);
            lastItemForVATLoading = null;
        }
    }

    function DisableTabIndex() {
        $(":text[readonly='readonly']").attr("tabindex", "-1");
    }
    function gridviewBillItem_EndCallback(s, e)
    {
        DisableTabIndex();
//        if(s.IsEditing == true)
//        {
//            var tempObj = window['<%= _ClientInstanceName %>']; 
//            tempObj.ComboBoxItem = gridviewBillItem.GetEditor("ItemId!Key");
//            tempObj.ComboBoxUnit = gridviewBillItem.GetEditor("UnitId!Key");
//        }
        if(s.cpEvent == 'DataChanged')
        {
            var tempObj = window['<%= _ClientInstanceName %>']; 
            gridviewBillItem.Refresh();
            tempObj.RaiseGridViewDataChanged();
            delete s.cpEvent;
        }
        else if(s.cpEvent == 'StartRowEditing')
        {
            var tempObj = window['<%= _ClientInstanceName %>']; 
            tempObj.RaiseStartRowEditing(s, e);
            delete s.cpEvent;
        }
    }

    function gridviewBillItem_Init(s, e)
    {
//        if(s.IsEditing == true)
//        {
//            var tempObj = window['<%= _ClientInstanceName %>']; 
//            tempObj.ComboBoxItem = gridviewBillItem.GetEditor("ItemId!Key");
//            tempObj.ComboBoxUnit = gridviewBillItem.GetEditor("UnitId!Key");
//        }
    }

    function TotalPrice_Update(s, e) {
        ReCalculate_TotalPrice();
    }

    function ReCalculate_TotalPrice() {
        var price = spinPrice.GetNumber();
        var quantity = spinQuantity .GetNumber();
        if (price > 0 && quantity > 0) {
            spinTotalPrice.SetValue(price * quantity);
        }
    }
</script>
<dx:ASPxCallbackPanel ID="panelBillItem" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div style="padding: 4px;">
                <dx:ASPxButton CssClass="text-underline" ID="btnAddNew" AutoPostBack="false" runat="server"
                    Text="Thêm mới" EnableTheming="False" UseSubmitBehavior="False" CausesValidation="false"
                    Cursor="pointer" EnableDefaultAppearance="False" ForeColor="#0072C6">
                    <ClientSideEvents Click="function(s ,e) { gridviewBillItem.AddNewRow(); }"></ClientSideEvents>
                    <Image ToolTip="Thêm mới" Url="~/images/icon/Actions/AddFile_16x16.png">
                    </Image>
                    <ClientSideEvents Click="function(s ,e) { gridviewBillItem.AddNewRow(); }" />
                </dx:ASPxButton>
            </div>
            <dx:ASPxGridView ID="gridviewBillItem" KeyFieldName="BillItemId" ClientInstanceName="gridviewBillItem"
                runat="server" AutoGenerateColumns="False" Width="100%" OnRowDeleting="gridviewBillItem_RowDeleting"
                OnRowInserting="gridviewBillItem_RowInserting" OnRowUpdating="gridviewBillItem_RowUpdating"
                OnCellEditorInitialize="gridviewBillItem_CellEditorInitialize" OnCustomColumnDisplayText="gridviewBillItem_CustomColumnDisplayText"
                OnRowValidating="gridviewBillItem_RowValidating" OnHtmlDataCellPrepared="gridviewBillItem_HtmlDataCellPrepared"
                OnInitNewRow="gridviewBillItem_InitNewRow" OnStartRowEditing="gridviewBillItem_StartRowEditing">
                <ClientSideEvents EndCallback="gridviewBillItem_EndCallback" Init="gridviewBillItem_Init"/>
                <Columns>
                    <dx:GridViewDataComboBoxColumn FieldName="ItemId!Key" Caption="Hàng hóa" ShowInCustomizationForm="True"
                        VisibleIndex="0">
                        <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                            TextFormatString="{0} - {1}" ValueField="ItemId" ValueType="System.Guid" OnItemRequestedByValue="comboItem_ItemRequestedByValue"
                            OnItemsRequestedByFilterCondition="comboItem_ItemsRequestedByFilterCondition">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <ClientSideEvents SelectedIndexChanged="comboItem_SelectedIndexChanged" />
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã hàng hóa" FieldName="Code" />
                                <dx:ListBoxColumn Caption="Tên hàng hóa" FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn FieldName="UnitId!Key" Caption="Đơn vị tính" ShowInCustomizationForm="True"
                        VisibleIndex="1">
                        <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                            TextFormatString="{0} - {1}" ValueField="UnitId" ValueType="System.Guid">
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <ClientSideEvents EndCallback="comboUnit_EndCallback" SelectedIndexChanged="comboUnit_SelectedIndexChanged" />
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã đơn vị tính" FieldName="Code" />
                                <dx:ListBoxColumn Caption="Tên đơn vị tính" FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="Quantity" Caption="Số lượng" ShowInCustomizationForm="True"
                        VisibleIndex="2">
                        <PropertiesSpinEdit ClientInstanceName="spinQuantity" DisplayFormatString="#,###"
                            DisplayFormatInEditMode="true">
                            <ClientSideEvents NumberChanged="TotalPrice_Update" />
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <Style HorizontalAlign="Right">
                                
                            </Style>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn FieldName="Price" Caption="Đơn giá" ShowInCustomizationForm="True"
                        VisibleIndex="3">
                        <PropertiesSpinEdit ClientInstanceName="spinPrice" DisplayFormatString="#,###" DisplayFormatInEditMode="true">
                            <ClientSideEvents NumberChanged="TotalPrice_Update" />
                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            <Style HorizontalAlign="Right">
                                
                            </Style>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataSpinEditColumn Caption="Chiết khấu(%)" FieldName="PromotionInPercentage"
                        ShowInCustomizationForm="True" VisibleIndex="5">
                        <PropertiesSpinEdit DisplayFormatString="#,###" DisplayFormatInEditMode="true">
                            <Style HorizontalAlign="Right">
                                
                            </Style>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewDataTextColumn Caption="VAT(%)" ShowInCustomizationForm="True" VisibleIndex="6"
                        ReadOnly="True" Name="VATInPercentage">
                        <PropertiesTextEdit>
                            <ReadOnlyStyle BackColor="#EFEFEF">
                            </ReadOnlyStyle>
                            <Style HorizontalAlign="Right">
                                
                            </Style>
                        </PropertiesTextEdit>
                        <EditItemTemplate>
                            <dx:ASPxCallbackPanel ID="panelItemVAT" runat="server" ClientInstanceName="panelItemVAT"
                                OnCallback="panelItemVAT_Callback" Width="100%" ShowLoadingPanel="False">
                                <ClientSideEvents EndCallback="panelItemVAT_EndCallback" />
                                <PanelCollection>
                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit Width="100%" ID="spinItemTax" runat="server" DisplayFormatString="#,###"
                                            Height="21px" HorizontalAlign="Right" Number="0" OnLoad="spinItemTax_Load" ReadOnly="True">
                                            <ReadOnlyStyle BackColor="#EFEFEF">
                                            </ReadOnlyStyle>
                                        </dx:ASPxSpinEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </EditItemTemplate>
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn Caption="Thành tiền" ShowInCustomizationForm="True"
                        VisibleIndex="4" ReadOnly="True" FieldName="TotalPrice">
                        <PropertiesSpinEdit ClientInstanceName="spinTotalPrice" DisplayFormatString="#,###"
                            DisplayFormatInEditMode="true">
                            <ReadOnlyStyle BackColor="#EFEFEF">
                            </ReadOnlyStyle>
                            <Style HorizontalAlign="Right">
                                
                            </Style>
                        </PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="#" ShowInCustomizationForm="True"
                        VisibleIndex="8" Width="80px">
                        <EditButton Visible="True">
                            <Image ToolTip="Sửa">
                                <SpriteProperties CssClass="Sprite_Edit" />
                            </Image>
                        </EditButton>
                        <NewButton>
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
                    <dx:GridViewDataTextColumn FieldName="Comment" Caption="Ghi chú" ShowInCustomizationForm="True"
                        VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" 
                    AllowFocusedRow="True" />
                <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsCookies CookiesID="BillItemEditForm" Enabled="True" StoreFiltering="False"
                    StorePaging="False" />
                <SettingsEditing Mode="Inline"></SettingsEditing>
                <Styles>
                    <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
                    </Header>
                    <Cell Wrap="True">
                    </Cell>
                    <Footer Wrap="True">
                    </Footer>
                </Styles>
            </dx:ASPxGridView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
