<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="PriceProductSales.aspx.cs" Inherits="WebModule.Accounting.Report.PriceProductSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function treeInventory_NodeClick(s, e) {
            treeInventory.SetFocusedNodeKey(e.nodeKey);
            grdataproduct.PerformCallback("selected");
        }

        function grdataproduct_RowClick(s, e) {
            var itemUnitId = s.GetRowKey(e.visibleIndex);
            grdBalanceOfItems.PerformCallback(itemUnitId);
        }

        function pcBalanceItems_EndCallback(s, e) {
            grdBalanceOfItems.Refresh();
        }

        function daiaryZoneWidget_AfterResizing(s, e) {
            var BottomSplitterPane = splilterReport.GetPaneByName('BottomSplitterPane');
            var size = BottomSplitterPane.GetClientHeight();
            grdBalanceOfItems.SetHeight(size * 0.95);

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
            grdBalanceOfItems.SetHeight(height * 2 / 5 * 0.9);
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="98%" Height="85%" Paddings-Padding="4px">
        <Paddings Padding="4px"></Paddings>
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Medium"
                    Font-Strikeout="False" Height="35px" Text="GIÁ VỐN HÀNG BÁN">
                </dx:ASPxLabel>
                    <%--<dx:XpoDataSource ID="dsInventory" runat="server" 
                        Criteria="[RowStatus] = 1 And [Name] Is Not Null" 
                        TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
                        <CriteriaParameters>
                            <asp:Parameter Name="OrganizationId" />
                        </CriteriaParameters>
                    </dx:XpoDataSource>--%>
                    <dx:XpoDataSource ID="dsInventory" runat="server" 
                        Criteria="[RowStatus] = 1 And [Name] Is Not Null" 
                        TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
                    </dx:XpoDataSource>
                    <dx:XpoDataSource ID="dsItemUnit" runat="server" Criteria="[RowStatus] &gt; 0 And [ItemId.ItemId] = ?" 
                        TypeName="NAS.DAL.Nomenclature.Item.ItemUnit">
                        <CriteriaParameters>
                            <asp:Parameter Name="ItemId" DefaultValue="" />
                        </CriteriaParameters>
                    </dx:XpoDataSource>
                <dx:ASPxSplitter ID="splilterReport" ClientInstanceName="splilterReport" runat="server" Height="100%" Width="100%" ResizingMode="Postponed" 
                    Orientation="Vertical">
                    <ClientSideEvents PaneResizeCompleted="daiaryZoneWidget_AfterResizing" Init="daiaryZoneWidget_Init" />
                    <Panes>
                        <dx:SplitterPane Name="TopSplitterPane" Size="240px" MaxSize="400px">
                            <Panes>
                                <dx:SplitterPane Size="500px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl>
                                            <dx:ASPxTreeList ID="treeInventory" runat="server" AutoGenerateColumns="False" KeyFieldName="InventoryId"
                                                ParentFieldName="ParentInventoryId!Key" ClientInstanceName="treeInventory" 
                                                DataSourceID="dsInventory" Width="100%">
                                                <Columns>
                                                    <dx:TreeListTextColumn FieldName="Code" Caption="Mã kho" Width="150px" ShowInCustomizationForm="True"
                                                        VisibleIndex="1">
                                                    </dx:TreeListTextColumn>
                                                    <dx:TreeListTextColumn FieldName="Name" Caption="Tên kho" Width="100%" ShowInCustomizationForm="True" 
                                                        VisibleIndex="2">
                                                    </dx:TreeListTextColumn>
                                                </Columns>
                                                <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="390" 
                                                    ShowFooter="True" VerticalScrollBarMode="Auto" />
                                                <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
                                                <SettingsPager Mode="ShowPager" PageSize="14">
                                                </SettingsPager>
                                                <ClientSideEvents  NodeClick="treeInventory_NodeClick"/>
                                            </dx:ASPxTreeList>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane Size="100%">
                                    <ContentCollection>
                                        <dx:SplitterContentControl>
                                            <dx:ASPxGridView ID="grdataproduct" ClientInstanceName="grdataproduct" runat="server" AutoGenerateColumns="False" 
                                                KeyFieldName="ItemUnitId"
                                                KeyboardSupport="true"
                                                OnCustomCallback="grdataproduct_CustomCallback" Width="100%" 
                                                OnHtmlRowPrepared="grdataproduct_HtmlRowPrepared">
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
                                                <ClientSideEvents RowClick="grdataproduct_RowClick"/>
                                            </dx:ASPxGridView>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                        <ContentCollection>
                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True"></dx:SplitterContentControl>
                        </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane Name="BottomSplitterPane" Size="100%">
                            <ContentCollection>
                                <dx:SplitterContentControl>
                                    <dx:ASPxGridView ID="grdBalanceOfItems" ClientInstanceName="grdBalanceOfItems" 
                                        runat="server" AutoGenerateColumns="False" Width="100%"  KeyboardSupport="true"
                                        KeyFieldName="COGSId" Settings-HorizontalScrollBarMode="Visible" 
                                        OnHtmlDataCellPrepared="grdBalanceOfItems_OnHtmlDataCellPrepared"
                                        OnCustomCallback="grdBalanceOfItems_CustomCallback">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã giao dịch" FieldName="InventoryTransactionId.Code" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="90px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ngày" FieldName="InventoryTransactionId.IssueDate" ShowInCustomizationForm="True"
                                                VisibleIndex="1" Width="80px">
                                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                </PropertiesTextEdit> 
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Xuất" FieldName="Credit" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="80px">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhập" FieldName="Debit" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="80px">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tồn" FieldName="Balance" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Width="100px">
                                                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Giá" FieldName="Price" ShowInCustomizationForm="True"
                                                VisibleIndex="5" Width="130px">
                                                <PropertiesTextEdit DisplayFormatString="{0:c0}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="Amount" ShowInCustomizationForm="True"
                                                VisibleIndex="6">
                                                <PropertiesTextEdit DisplayFormatString="{0:c0}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Giá vốn hàng bán" FieldName="COGSPrice" ShowInCustomizationForm="True"
                                                VisibleIndex="7" Width="130px">
                                                <PropertiesTextEdit DisplayFormatString="{0:c0}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Giá trị tồn kho" FieldName="Total" ShowInCustomizationForm="True"
                                                VisibleIndex="8" Width="100%">
                                                <PropertiesTextEdit DisplayFormatString="{0:c0}">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>
                                        <SettingsPager Mode="ShowPager" PageSize="12" ShowEmptyDataRows="true">
                                        </SettingsPager>
                                        <Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" VerticalScrollableHeight="180" ShowFilterRow="true" ShowFilterRowMenu="true"></Settings>
                                        <SettingsText EmptyDataRow="Danh sách rỗng" />
                                    </dx:ASPxGridView>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                </dx:ASPxSplitter>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
</asp:Content>

