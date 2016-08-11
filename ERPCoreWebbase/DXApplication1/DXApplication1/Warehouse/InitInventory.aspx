<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="InitInventory.aspx.cs" Inherits="WebModule.Warehouse.InitInventory" %>

<%@ Register Src="~/Warehouse/Command/PopupCommand/AddNewLotsToItem/uAddNewLotsToItem.ascx" TagName="uAddNewLotsToItem" TagPrefix="uc1" %>
<%@ Register src="UserControl/MessageBox.ascx" tagname="MessageBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function treeInventory_NodeClick(s, e) {
            //treeInventory.SetFocusedNodeKey(e.nodeKey);
            //grdataproduct.PerformCallback("selected");
        }

        function trlItemUnit_NodeClick(s, e) {
            trlItemUnit.SetFocusedNodeKey(e.nodeKey);
            pcBalanceItems.PerformCallback();
        }

        var pagManufacturer_ActiveTabIndex = 0;

        function pagWarehouse_ActiveTabChanged(s, e) {
            pagManufacturer_ActiveTabIndex = e.tab.index;
            ERPCore.RaiseMainPaneResizeEvent();
        }

        $(document).ready(function () {

            ERPCore.WindowResize_Adjust = function (s, e) {
                var pagManufacturerPaddingX =
                                $("#pagWarehouse .dxtc-content").outerWidth(true)
                              - $("#pagWarehouse .dxtc-content").width();
                var pagManufacturerPaddingY =
                                $("#pagWarehouse .dxtc-content").outerHeight(true)
                              - $("#pagWarehouse .dxtc-content").height();
                var pagManufacturerTabHeight = $("#pagWarehouse .dxtc-strip").outerHeight(true);

                //grdBalanceOfItemsNoInventory.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                //grdBalanceOfItemsNoInventory.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
            };

        });

        function grdDataWarehouseUnit_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdBalanceOfItemsNoInventory.Refresh();
                delete s.cpEvent;
                grdataproduct.Refresh();
            }

            if (s.cpCodeNull) {
                if (!ASPxClientEdit.ValidateEditorsInContainerById('InitInventoryContainer')) {
                    e.processOnServer = false;
                    return;
                }                              
                delete (s.cpCodeNull);
            }

            if (s.cpAccountNull) {
                if (!ASPxClientEdit.ValidateEditorsInContainerById('InitInventoryContainer')) {
                    e.processOnServer = false;
                    return;
                }                
                delete (s.cpAccountNull);
            }

            if (s.cpEmptyControl) {
//                cboInitInventoryAccount.SetText("");
//                txtInitInventoryCode.SetText("");
                delete (s.cpEmptyControl);
            }

            if (s.cpRefresh) {
                grdataproduct.Refresh();
//                cboInitInventoryAccount.SetEnabled(false);
//                txtInitInventoryCode.SetEnabled(false);
                delete (s.cpEmptyControl)
            }

            if (s.cpEnableMasterControl) {
                if (s.cpEnableMasterControl == 'true') {                   
                    cboInitInventoryAccount.SetEnabled(true);
                    txtInitInventoryCode.SetEnabled(true);
                }
                else {                    
                    cboInitInventoryAccount.SetEnabled(false);
                    txtInitInventoryCode.SetEnabled(false);
                }
                delete (s.cpEnableMasterControl);
            }
        }

        function daiaryZoneWidget_AfterResizing(s, e) {
            var BottomSplitterPane = splilterReport.GetPaneByName('BottomSplitterPane');
            var size = BottomSplitterPane.GetClientHeight();
            grdBalanceOfItemsNoInventory.SetHeight(size * 0.95);

            var TopSplitterPane = splilterReport.GetPaneByName('TopSplitterPane');
            var size = TopSplitterPane.GetClientHeight();
            grdataproduct.SetHeight(size * 0.95);
            treeInventory.SetHeight(size * 0.95);
        }

        function daiaryZoneWidget_Init(s, e) {
            var height = Math.max(0, document.documentElement.clientHeight);
            var BottomSplitterPane = splilterReport.GetPaneByName('BottomSplitterPane');
            var TopSplitterPane = splilterReport.GetPaneByName('TopSplitterPane');

            BottomSplitterPane.SetSize(height * 2 / 5);
            TopSplitterPane.SetSize(height * 2 / 5);
            grdataproduct.SetHeight(height * 2 / 5 * 0.9);
            treeInventory.SetHeight(height * 2 / 5 * 0.9);
            grdBalanceOfItemsNoInventory.SetHeight(height * 2 / 5 * 0.9);
        }

//        function grdBalanceOfItemsNoInventory_Init(s, e) {
//            var height = Math.max(0, document.documentElement.clientHeight);
//            s.SetHeight(height * 2 / 5);
//            Utils.AttachStandardShortcutToGridview(s);
//        }

        function grdataproduct_RowClick(s, e) {
        }

        function cpInitInventoryDetail_EndCallback(s, e) {
            if (s.cpWarning) {
                formMessageBox.Show();
                delete (s.cpWarning);

            }
            
            
            grdBalanceOfItemsNoInventory.Refresh();
            grdataproduct.Refresh();

        }

        function grdataproduct_FocusedRowChanged(s, e) {
            if (grdataproduct.GetFocusedRowIndex() >= 0) {
                var id = grdataproduct.GetRowKey(grdataproduct.GetFocusedRowIndex());
                cpInitInventoryDetail.PerformCallback("itemchange|" + id + "|" + treeInventory.GetFocusedNodeKey());
            }
        }

        function treeInventory_FocusedNodeChanged(s, e) {
            //alert(s.GetFocusedRowIndex());
            //var id = grdataproduct.GetRowKey(0);
            
            grdataproduct.SetFocusedRowIndex(0);
            var id = grdataproduct.GetRowKey(grdataproduct.GetFocusedRowIndex());
            //alert(id);
            cpInitInventoryDetail.PerformCallback("inventorychange|" + id + "|" + s.GetFocusedNodeKey());

        }

        /////Handler event from Input InventoryCommand
        var uAddNewLotsToItem1Target = new EventTarget();
        function uAddNewLotsToItem1Target_handle(event) {
            //var params = new Array('External', event.OutParam);
            cboSelectLotItem.PerformCallback(event.OutParam);
        };
        uAddNewLotsToItem1Target.addListener("UpdatedLotItem", uAddNewLotsToItem1Target_handle);

        function grdBalanceOfItemsNoInventory_FocusedRowChanged(s, e) {
            
        }

        function cboAccountPeriod_SelectedIndexChanged(s, e) {
            cpInitInventoryDetail.PerformCallback("comboChange");
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="98%" Height="99%" Paddings-Padding="4px">
        <Paddings Padding="4px"></Paddings>
        <PanelCollection>
            <dx:PanelContent>
    <dx:XpoDataSource ID="dsInventory" runat="server" Criteria="[RowStatus] &gt;= 0 And [Name] Is Not Null And [Code] &lt;&gt; 'N/A'"
        TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
        <CriteriaParameters>
            <asp:Parameter Name="OrganizationId" />
        </CriteriaParameters>
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="dsItemUnit" runat="server" Criteria="[RowStatus] &gt; 0 And [ItemId.ItemId] = ?"
        TypeName="NAS.DAL.Nomenclature.Item.ItemUnit">
        <CriteriaParameters>
            <asp:Parameter Name="ItemId" DefaultValue="" />
        </CriteriaParameters>
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="dsItem" runat="server" Criteria="[RowStatus] &gt; 0" TypeName="NAS.DAL.Nomenclature.Item.Item">
    </dx:XpoDataSource>

    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" Font-Size="Medium"
        Font-Strikeout="False" Height="35px" Text="TỒN KHO BAN ĐẦU">
    </dx:ASPxLabel><br />
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                    <Items>
                        <dx:LayoutItem Caption="Chu kỳ kế toán">
                            <layoutitemnestedcontrolcollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxComboBox ID="cboAccountPeriod" runat="server" 
                                        ClientInstanceName="cboAccountPeriod" EnableCallbackMode="True" 
                                        IncrementalFilteringMode="Contains" OnInit="cboAccountPeriod_Init" 
                                        OnItemRequestedByValue="cboAccountPeriod_ItemRequestedByValue" 
                                        OnItemsRequestedByFilterCondition="cboAccountPeriod_ItemsRequestedByFilterCondition" 
                                        TextField="Code" TextFormatString="{0} - {1}" ValueField="AccountingPeriodId" 
                                        ValueType="System.Guid" Width="400px">
                                        <clientsideevents selectedindexchanged="cboAccountPeriod_SelectedIndexChanged" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã chu kỳ" FieldName="Code" Width="100px" />
                                            <dx:ListBoxColumn Caption="Tên chu kỳ" FieldName="Description" Width="100px" />
                                            <dx:ListBoxColumn Caption="Từ ngày" FieldName="FromDateTime" Width="100px" />
                                            <dx:ListBoxColumn Caption="Đến ngày" FieldName="ToDateTime" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </layoutitemnestedcontrolcollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
    <dx:ASPxSplitter ID="splilterReport" ClientInstanceName="splilterReport" runat="server" Height="90%" Width="100%" ResizingMode="Postponed" 
        Orientation="Vertical">
        <ClientSideEvents PaneResizeCompleted="daiaryZoneWidget_AfterResizing" Init="daiaryZoneWidget_Init" />
        <Panes>
            <dx:SplitterPane Name="TopSplitterPane" Size="240px" MaxSize="450px">
                <Panes>
                    <dx:SplitterPane Size="500px">
                        <ContentCollection>
                            <dx:SplitterContentControl>
                            <dx:ASPxTreeList ID="treeInventory" runat="server" AutoGenerateColumns="False" KeyFieldName="InventoryId"
                                ParentFieldName="ParentInventoryId!Key" ClientInstanceName="treeInventory" DataSourceID="dsInventory"
                                Width="100%" OnCustomCallback="treeInventory_CustomCallback" 
                                    OnCustomDataCallback="treeInventory_CustomDataCallback">
                                <Columns>
                                    <dx:TreeListTextColumn FieldName="Code" ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2" Width="100%">
                                    </dx:TreeListTextColumn>
                                </Columns>
                                <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" ShowTreeLines="true" ScrollableHeight="390"  ShowFooter="True"></Settings>
                                <SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True"></SettingsBehavior>
                                <SettingsPager Mode="ShowPager" PageSize="14" >
                                </SettingsPager>
                                <SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True"></SettingsBehavior>                                
                                <ClientSideEvents NodeClick="treeInventory_NodeClick" 
                                    FocusedNodeChanged="treeInventory_FocusedNodeChanged"></ClientSideEvents>
                            </dx:ASPxTreeList>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane Size="100%">
                        <ContentCollection>
                            <dx:SplitterContentControl>
                                <dx:ASPxGridView ID="grdataproduct" ClientInstanceName="grdataproduct" 
                                    runat="server" AutoGenerateColumns="False"
                                    KeyFieldName="ItemUnitId"
                                    KeyboardSupport="true"
                                    OnCustomCallback="grdataproduct_CustomCallback"
                                    Width="100%" 
                                    OnCustomUnboundColumnData="grdataproduct_CustomUnboundColumnData" 
                                    OnDataBound="grdataproduct_DataBound">

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                    <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" VerticalScrollableHeight="140" ShowFilterRow="true" ShowFilterRowMenu="true"></Settings>
                                    <SettingsText EmptyDataRow="Danh sách rỗng" />
                                    <ClientSideEvents RowClick="grdataproduct_RowClick" 
                                        FocusedRowChanged="grdataproduct_FocusedRowChanged" />
<ClientSideEvents RowClick="grdataproduct_RowClick"></ClientSideEvents>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="ItemCode" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="25%">
                                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Nhà sản xuất" VisibleIndex="2" 
                                            FieldName="Manufacturer"></dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemName" 
                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="35%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="UnitName" 
                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="15%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="IsComplete" 
                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="25%">
                                            <PropertiesComboBox>
                                                <Items>
                                                    


<dx:ListEditItem Value="0" Text="Chưa khởi tạo" />
                                                    


<dx:ListEditItem Value="1" Text="Đã khởi tạo" />
                                                


</Items>
                                            


</PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="true"/>

                                    <SettingsPager Mode="ShowPager" PageSize="6" ShowEmptyDataRows="true">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" 
                                        ShowFilterRowMenu="True" HorizontalScrollBarMode="Visible" />
                                    <SettingsText EmptyDataRow="Danh sách rỗng"></SettingsText>

<SettingsLoadingPanel ShowImage="False"></SettingsLoadingPanel>
                                </dx:ASPxGridView>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
<ContentCollection>
<dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True"></dx:SplitterContentControl>
</ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane Name="BottomSplitterPane" Size="100%">
                <ContentCollection>
                    <dx:SplitterContentControl>
                        <dx:ASPxCallbackPanel ID="cpInitInventoryDetail" runat="server" 
                            ClientInstanceName="cpInitInventoryDetail" 
                            OnCallback="cpInitInventoryDetail_Callback" Width="100%">
                            <ClientSideEvents EndCallback="cpInitInventoryDetail_EndCallback" />
<ClientSideEvents EndCallback="cpInitInventoryDetail_EndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <div id="InitInventoryContainer">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                                        <Items>
                                            <dx:LayoutGroup Caption="Chi tiết tồn kho" ColCount="3">
                                                <Items>
                                                    <dx:LayoutItem Caption="Mã thẻ nhập">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtInitInventoryCode" runat="server" 
                                                                    ClientInstanceName="txtInitInventoryCode" Width="150px">
                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" 
                                                                        ErrorText="Chưa nhập mã thẻ">                                                                        
                                                                        <RequiredField IsRequired="True" ErrorText="Chưa nhập mã thẻ"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tài khoản">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="cboInitInventoryAccount" runat="server" 
                                                                    CallbackPageSize="20" ClientInstanceName="cboInitInventoryAccount" 
                                                                    EnableCallbackMode="True" IncrementalFilteringMode="Contains" 
                                                                    OnItemRequestedByValue="cboInitInventoryAccount_ItemRequestedByValue" 
                                                                    OnItemsRequestedByFilterCondition="cboInitInventoryAccount_ItemsRequestedByFilterCondition" 
                                                                    TextField="Name" TextFormatString="{0} - {1}" ValueField="Code" 
                                                                    ValueType="System.String" Width="300px">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" Width="100px" />
                                                                        <dx:ListBoxColumn Caption="Tài khoản" FieldName="Name" Width="200px" />
                                                                    </Columns>
                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True" 
                                                                        ErrorText="Chưa chọn tài khoản">                                                                      
                                                                        <RequiredField IsRequired="True" ErrorText="Chưa chọn tài khoản"></RequiredField>
                                                                    </ValidationSettings>
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tiền tệ mặc định">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxLabel ID="lblCurrencyDefault" ClientInstanceName="lblCurrencyDefault" runat="server" Text="">
                                                                </dx:ASPxLabel>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem ColSpan="3" ShowCaption="False">
                                                        <layoutitemnestedcontrolcollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <uc2:MessageBox ID="MessageBox1" runat="server" />
                                                                <dx:ASPxGridView ID="grdBalanceOfItemsNoInventory" runat="server" 
                                                                    AutoGenerateColumns="False" ClientInstanceName="grdBalanceOfItemsNoInventory" 
                                                                    DataSourceID="InventoryJournalBalanceForwardXDS" KeyboardSupport="True" 
                                                                    KeyFieldName="InventoryJournalId" 
                                                                    OnCellEditorInitialize="grdBalanceOfItemsNoInventory_CellEditorInitialize" 
                                                                    OnCommandButtonInitialize="grdBalanceOfItemsNoInventory_CommandButtonInitialize" 
                                                                    OnCustomCallback="grdBalanceOfItemsNoInventory_CustomCallback" 
                                                                    OnCustomUnboundColumnData="grdBalanceOfItemsNoInventory_CustomUnboundColumnData" 
                                                                    OnInit="grdBalanceOfItemsNoInventory_Init" 
                                                                    OnInitNewRow="grdBalanceOfItemsNoInventory_InitNewRow" 
                                                                    OnRowDeleting="grdBalanceOfItemsNoInventory_RowDeleting" 
                                                                    OnRowInserted="grdBalanceOfItemsNoInventory_RowInserted" 
                                                                    OnRowInserting="grdBalanceOfItemsNoInventory_RowInserting" 
                                                                    OnRowUpdating="grdBalanceOfItemsNoInventory_RowUpdating" 
                                                                    OnRowValidating="grdBalanceOfItemsNoInventory_RowValidating" 
                                                                    OnStartRowEditing="grdBalanceOfItemsNoInventory_StartRowEditing" Width="100%">
                                                                    <clientsideevents endcallback="grdDataWarehouseUnit_EndCallback"
                                                                        FocusedRowChanged="grdBalanceOfItemsNoInventory_FocusedRowChanged" />
                                                                    <settingsbehavior allowfocusedrow="True" columnresizemode="Control" />

                                                                    <settingsediting mode="Inline" />
                                                                    <settings horizontalscrollbarmode="Auto" showfooter="True" 
                                                                        verticalscrollableheight="50" verticalscrollbarmode="Auto" />
                                                                    <settingstext emptydatarow="Không có dữ liệu" />

<ClientSideEvents EndCallback="grdDataWarehouseUnit_EndCallback"></ClientSideEvents>
                                                                    <totalsummary>
                                                                        <dx:ASPxSummaryItem DisplayFormat="Tổng cộng : {0:n0}" FieldName="Amount" 
                                                                            SummaryType="Sum" />
                                                                    </totalsummary>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="Số lượng tồn" FieldName="Balance" 
                                                                            Name="Balance" ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                                            <propertiestextedit displayformatstring="n0"></propertiestextedit>
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxSpinEdit ID="txtBalance" runat="server" AllowNull="False" 
                                                                                    ClientInstanceName="txtBalance" DecimalPlaces="2" DisplayFormatString="F" 
                                                                                    Height="21px" MaxValue="10000000000" Number="0" oninit="txtBalance_Init" 
                                                                                    Width="100%">
                                                                                </dx:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Giá nhập" FieldName="Debit" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                                            <propertiestextedit displayformatstring="n0"></propertiestextedit>
                                                                            <EditItemTemplate>
                                                                                <dx:ASPxSpinEdit ID="txtPrice" runat="server" AllowNull="False" 
                                                                                    ClientInstanceName="txtPrice" DecimalPlaces="2" DisplayFormatString="F" 
                                                                                    Height="21px" MaxValue="10000000000" Number="0" oninit="txtPrice_Init" 
                                                                                    Width="100%">
                                                                                </dx:ASPxSpinEdit>
                                                                            </EditItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" Name="Action" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                                            <editbutton visible="True">
                                                                                <image>
                                                                                    <spriteproperties cssclass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                </image>
                                                                            </editbutton>
                                                                            <newbutton visible="True">
                                                                                <image>
                                                                                    <spriteproperties cssclass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                </image>
                                                                            </newbutton>
                                                                            <deletebutton visible="True">
                                                                                <image>
                                                                                    <spriteproperties cssclass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                </image>
                                                                            </deletebutton>
                                                                            <cancelbutton>
                                                                                <image>
                                                                                    <spriteproperties cssclass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                </image>
                                                                            </cancelbutton>
                                                                            <updatebutton>
                                                                                <image>
                                                                                    <spriteproperties cssclass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                </image>
                                                                            </updatebutton>
                                                                            <clearfilterbutton visible="True">
                                                                            </clearfilterbutton>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="Amount" 
                                                                            ReadOnly="True" ShowInCustomizationForm="True" UnboundType="Decimal" 
                                                                            VisibleIndex="3" Width="200px">
                                                                            <propertiestextedit displayformatstring="n0"></propertiestextedit>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataComboBoxColumn Caption="Số lô" FieldName="LotId!Key" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                                            <settings allowsort="False" />

                                                                            <propertiescombobox callbackpagesize="20" clientinstancename="cboSelectLotItem" 
                                                                                enablecallbackmode="True" enablesynchronization="True" 
                                                                                incrementalfilteringmode="Contains" textfield="Code" textformatstring="{0}" 
                                                                                valuefield="LotId" valuetype="System.Guid"
                                                                                OnItemRequestedByValue="cboSelectLotItem_OnItemRequestedByValue"
                                                                                OnItemsRequestedByFilterCondition="cboSelectLotItem_OnItemsRequestedByFilterCondition"><Columns>
<dx:ListBoxColumn Caption="Số lô" FieldName="Code" Width="100px"></dx:ListBoxColumn>
<dx:ListBoxColumn Caption="Hạn sử dụng" FieldName="ExpireDate" Width="100px"></dx:ListBoxColumn>
</Columns>
</propertiescombobox>

<Settings AllowSort="False"></Settings>
                                                                            <HeaderTemplate>
                                                                                <div>
                                                                                    <div style="display:inline-block;vertical-align:middle;">
                                                                                        Số lô
                                                                                    </div>
                                                                                    <div style="display:inline-block;vertical-align:middle;float:right">
                                                                                        <dx:ASPxButton ID="btnAddLot" runat="server" AutoPostBack="false" 
                                                                                            FocusRectBorder-BorderColor="Transparent" FocusRectBorder-BorderWidth="0px" 
                                                                                            FocusRectPaddings-Padding="0px" Image-SpriteProperties-CssClass="Sprite_New" 
                                                                                            onload="btnAddLot_Load">
                                                                                        </dx:ASPxButton>
                                                                                    </div>
                                                                                </div>
                                                                            </HeaderTemplate>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                        <dx:GridViewDataDateColumn Caption="Hạn dùng" FieldName="LotId.ExpireDate" 
                                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                                                        </dx:GridViewDataDateColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="RowStatus" ReadOnly="True" 
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" Width="0px">
                                                                            <propertiestextedit width="0px"></propertiestextedit>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="AccountId!Key" ReadOnly="True" 
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="11" Width="0px">
                                                                            <propertiestextedit width="0px"></propertiestextedit>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="InventoryTransactionId!Key" 
                                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="0px">
                                                                            <propertiestextedit width="0px"></propertiestextedit>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="InventoryJournalId" 
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="7" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="InventoryId!Key" 
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="9" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="ItemUnitId!Key" 
                                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="8" Width="0px">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>

<SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>

                                                                    <settingspager showemptydatarows="True" Mode="ShowPager" PageSize="12">
                                                                    </settingspager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFooter="True" VerticalScrollableHeight="180" HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"></Settings>

<SettingsText EmptyDataRow="Kh&#244;ng c&#243; dữ liệu"></SettingsText>

<SettingsLoadingPanel ShowImage="False"></SettingsLoadingPanel>

                                                                    <styles>
                                                                        <header font-bold="True" horizontalalign="Center">
                                                                        </header>
                                                                    </styles>
                                                                </dx:ASPxGridView>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </layoutitemnestedcontrolcollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                    <div>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
                <dx:XpoDataSource ID="InventoryJournalBalanceForwardXDS" runat="server" 
                    DefaultSorting="" 
                    TypeName="NAS.DAL.Inventory.Ledger.InventoryJournalBalanceForward" 
                    Criteria="">
                </dx:XpoDataSource>
    <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataWarehouseUnit" 
                    ClientInstanceName="ldpn_grdDataWarehouseUnit" Width="100%"
        ContainerElementID="grdDataWarehouseUnit" runat="server" ShowImage="False">
        <LoadingDivStyle BackColor="Transparent">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>    
    </dx:PanelContent>
    </PanelCollection>
    </dx:ASPxPanel>
    <uc1:uAddNewLotsToItem ID="uAddNewLotsToItem1" runat="server" ObjectHandler="uAddNewLotsToItem1Target" EventHandler="UpdatedLotItem"/>
</asp:Content>
