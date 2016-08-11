<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReportProduct.aspx.cs" Inherits="WebModule.Warehouse.ReportProduct" %>
<%@ Register src="UserControl/uReportPivot.ascx" tagname="uReportPivot" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
//        function trlItemUnit_NodeClick(s, e) {
//            trlItemUnit.SetFocusedNodeKey(e.nodeKey);
//            //pcBalanceItems.PerformCallback();
//        }

        function treeInventory_NodeClick(s, e) {
            treeInventory.SetFocusedNodeKey(e.nodeKey);
            grdataproduct.PerformCallback();
        }

//        function pcBalanceItems_EndCallback(s, e) {
//            pcBalanceItems.SetActiveTabIndex(0);
//            //grdBalanceOfItems.Refresh();
//            grdBalanceOfInventoryCart.Refresh();
//        }


//        function daiaryZoneWidget_AfterResizing(s, e) {
//            var BottomSplitterPane = splilterReport.GetPaneByName('BottomSplitterPane');
//            var size = BottomSplitterPane.GetClientHeight();
//            grdBalanceOfItems.SetHeight(size);
//            grdBalanceOfInventoryCart.SetHeight(size);

//        }

        function daiaryZoneWidget_AfterResizing(s, e) {
            var BottomSplitterPane = splilterReport.GetPaneByName('BottomSplitterPane');
            var size = BottomSplitterPane.GetClientHeight();
            //grdBalanceOfItems.SetHeight(size * 0.75);
            grdBalanceOfInventoryCart.SetHeight(size * 0.9);

            var TopSplitterPane = splilterReport.GetPaneByName('TopSplitterPane');
            var size = TopSplitterPane.GetClientHeight();
            grdataproduct.SetHeight(size * 0.95);
            treeInventory.SetHeight(size * 0.95);
        }

        function daiaryZoneWidget_Init(s, e) {
            settingInitHeight();
        }

        function settingInitHeight() {
            var height = Math.max(0, document.documentElement.clientHeight);
            var BottomSplitterPane = splilterReport.GetPaneByName('BottomSplitterPane');
            var TopSplitterPane = splilterReport.GetPaneByName('TopSplitterPane');

            BottomSplitterPane.SetSize(height * 2 / 5);
            TopSplitterPane.SetSize(height * 2 / 5 );
            grdataproduct.SetHeight(height * 2 / 5 * 0.9);
            treeInventory.SetHeight(height * 2 / 5 * 0.9);
            //grdBalanceOfItems.SetHeight(height * 2 / 5 * 0.7);
            grdBalanceOfInventoryCart.SetHeight(height * 2 / 5 * 0.9);
        }

        function grdataproduct_RowClick(s, e) {
            var itemUnitId = s.GetRowKey(e.visibleIndex);
            cpReportProduct.PerformCallback(itemUnitId);
            //pcBalanceItems.PerformCallback(itemUnitId);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
 <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Medium"
    Font-Strikeout="False" Height="35px" Text="TỒN KHO">
</dx:ASPxLabel>
    <dx:XpoDataSource ID="dsInventory" runat="server" 
        Criteria="[RowStatus] &gt;= 0 And [Name] Is Not Null" 
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
    <dx:ASPxSplitter ID="splilterReport" runat="server" 
        ClientInstanceName="splilterReport" Height="90%" Orientation="Vertical" 
        Width="100%">
        <Panes>
            <dx:SplitterPane MaxSize="400px" Name="TopSplitterPane" Size="240px">
                <Panes>
                    <dx:SplitterPane Size="500px">
                        <ContentCollection>
                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTreeList ID="treeInventory" runat="server" AutoGenerateColumns="False" 
                                    ClientInstanceName="treeInventory" DataSourceID="dsInventory" 
                                    KeyFieldName="InventoryId" ParentFieldName="ParentInventoryId!Key" Width="100%">
                                    <Columns>
                                        <dx:TreeListTextColumn FieldName="Code" Caption="Mã kho" ShowInCustomizationForm="True" 
                                            VisibleIndex="0" Width="30%">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn FieldName="Name" Caption="Tên kho" ShowInCustomizationForm="True" 
                                            VisibleIndex="1" Width="70%">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn FieldName="InventoryId" ShowInCustomizationForm="True" 
                                            Visible="False" VisibleIndex="2">
                                        </dx:TreeListTextColumn>
                                    </Columns>
                                    <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="390" 
                                        ShowFooter="True" VerticalScrollBarMode="Auto" />
                                    <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
                                    <SettingsPager Mode="ShowPager" PageSize="14">
                                    </SettingsPager>
                                    <ClientSideEvents NodeClick="treeInventory_NodeClick" />
                                </dx:ASPxTreeList>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane Size="100%">
                        <ContentCollection>
                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxGridView ID="grdataproduct" runat="server" AutoGenerateColumns="False" 
                                    KeyFieldName="ItemUnitId" OnCustomCallback="grdataproduct_CustomCallback" 
                                    Width="100%">
                                    <ClientSideEvents RowClick="grdataproduct_RowClick" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="ItemId.Code" 
                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="40%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemId.Name" 
                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="60%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="UnitId.Name" 
                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="60%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsPager PageSize="10" ShowEmptyDataRows="True">
                                    </SettingsPager>
                                    <Settings HorizontalScrollBarMode="Auto" ShowFilterRow="True" 
                                        ShowFilterRowMenu="True" VerticalScrollableHeight="170" 
                                        VerticalScrollBarMode="Auto" />
                                    <SettingsText EmptyDataRow="Danh sách rỗng" />
                                </dx:ASPxGridView>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane Name="BottomSplitterPane" Size="100%">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxCallbackPanel ID="cpReportProduct" runat="server" 
                            ClientInstanceName="cpReportProduct" oncallback="cpReportProduct_Callback" 
                            Width="100%" Height="100%">
                            <ClientSideEvents EndCallback="function(s, e){ settingInitHeight(); }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdBalanceOfInventoryCart" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="grdBalanceOfInventoryCart" 
                                        KeyFieldName="InventoryLedgerId" 
                                        OnHtmlDataCellPrepared="grdBalanceOfInventoryCart_OnHtmlDataCellPrepared" 
                                        Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã giao dịch" 
                                                FieldName="InventoryTransactionId.Code" ShowInCustomizationForm="True" 
                                                VisibleIndex="0" Width="180px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ngày" 
                                                FieldName="InventoryTransactionId.IssueDate" ShowInCustomizationForm="True" 
                                                VisibleIndex="1" Width="180px">
                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesTextEdit> 
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Xuất" FieldName="Credit" 
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhập" FieldName="Debit" 
                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tồn" FieldName="Balance" 
                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="IsOriginal" 
                                                Name="IsOriginal" ShowInCustomizationForm="True" VisibleIndex="5" Width="100%">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
                                        <SettingsPager PageSize="13" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <Settings HorizontalScrollBarMode="Auto" ShowFilterRow="True" 
                                            ShowFilterRowMenu="True" VerticalScrollableHeight="320" 
                                            VerticalScrollBarMode="Auto" />
                                        <SettingsText EmptyDataRow="Danh sách rỗng" />
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <ClientSideEvents Init="daiaryZoneWidget_Init" PaneResizeCompleted="daiaryZoneWidget_AfterResizing" />
    </dx:ASPxSplitter>
</asp:Content>
