<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Warehouse.aspx.cs" Inherits="WebModule.Warehouse.Warehouse" %>

<%@ Register Src="UserControl/uWarehouse.ascx" TagName="uWarehouse" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

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
                if (pagManufacturer_ActiveTabIndex == 0) {
                    grdDataWarehouse.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    grdDataWarehouse.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);

                }

            };

            ManuWarehouseEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataWarehouse.Refresh();
                }
            });

            ManuWarehouseEditForm.BindClosingEvent(function (event, args) {
                grdDataWarehouse.Refresh();
                grdDataWarehouse.Focus();
            });

            //Raise when popup shown
            ManuWarehouseEditForm.BindShownEvent(function (event) {
                ldpn_grdDataWarehouse.Hide();
            });
        });

        //Warehouse gridview custom button click event handler
        function grdDataWarehouse_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add  
                case "btNew":
                    var headerText = 'Thông tin kho - Thêm Mới';
                    ldpn_grdDataWarehouse.Show();
                    ManuWarehouseEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit     
                case "btEdit":
                    //alert(s.GetRowKey(e.visibleIndex));
                    ldpn_grdDataWarehouse.Show();
                    s.GetRowValues(e.visibleIndex, 'InventoryId;Code', grdDataWarehouse_OnGetRowValues);
                    //ManuWarehouseEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete     
                case "btDelete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataWarehouse.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataWarehouse_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông tin kho - ' + values[1];
            ManuWarehouseEditForm.Show(headerText, recordId);
        }

        function grdDataWarehouse_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataWarehouse.Refresh();
            }
            delete s.cpEvent;
        }
        //DND ERP 850
        var clientAction = '';
        function grdProduct_CustomButtonClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);

            if (e.buttonID == 'AddItem') {
                clientAction = 'Add';
                cpItemEdit.PerformCallback('Add');
            }
            else if (e.buttonID == 'EditItem') {
                clientAction = 'Edit';
                UpdateItemAction(key);
            }
            else if (e.buttonID == 'DeleteItem') {
                clientAction = 'Delete';
                DeleteItemAction(key);
            }
            else if (e.buttonID == 'showCommonDetail3') {
                s.GetRowValues(e.visibleIndex, 'Description', ShowDetail);
                popupCommonDetailInfo.Show();
            }
        }

        function grdDataWarehouse_Init(s, e) {
            //press F2 to show edit popup
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var key = s.GetRowKey(focusedRowIndex);
                clientAction = 'Edit';
                //UpdateItemAction(key);
                ldpn_grdDataWarehouse.Show()
                s.GetRowValues(focusedRowIndex, 'InventoryId;Code', grdDataWarehouse_OnGetRowValues);
            });
            //press Insert to show insert popup
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                var headerText = 'Thông tin kho - Thêm Mới';
                ldpn_grdDataWarehouse.Show();
                ManuWarehouseEditForm.Show(headerText);
            });
            //press Delete to show record
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var key = s.GetRowKey(focusedRowIndex);
                DeleteItemAction(key);
            });
            s.GetMainElement().focus();
        }
        function UpdateItemAction(values) {
            var params = new Array('Edit', values);
            cpLineWarehouse.PerformCallback(params);
        }
        function DeleteItemAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                var args = 'delete';
                args += '|' + values;
                grdDataWarehouse.PerformCallback(args);
            } else
                LoadingPanelCombineMaterial.Hide();
        }
        //END DND ERP 850
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagWarehouse" RenderMode="Lightweight" runat="server" ActiveTabIndex="0"
        Width="100%" ContentStyle-HorizontalAlign="Center" ClientInstanceName="pagWarehouse">
        <TabPages>
            <dx:TabPage Text="Danh mục kho">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataWarehouse" ClientInstanceName="ldpn_grdDataWarehouse"
                            ContainerElementID="grdDataWarehouse" runat="server">
                            <LoadingDivStyle BackColor="Transparent">
                            </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:XpoDataSource ID="dsInventory" runat="server" 
                            Criteria="[RowStatus] &gt; 0 And [Code] &lt;&gt; 'N/A'" DefaultSorting=""
                            TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
                        </dx:XpoDataSource>
                        <dx:ASPxGridView ID="grdDataWarehouse" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="InventoryId" OnCustomCallback="grdDataWarehouse_CustomCallback"
                            ClientInstanceName="grdDataWarehouse" KeyboardSupport="True" DataSourceID="dsInventory"
                            OnCustomColumnDisplayText="grdDataWarehouse_CustomColumnDisplayText" CssClass="grdDataWarehouse_keyboardshortcut">
                            <ClientSideEvents CustomButtonClick="grdDataWarehouse_CustomButtonClick" EndCallback="grdDataWarehouse_EndCallback"
                                Init="grdDataWarehouse_Init" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="Address" ShowInCustomizationForm="True"
                                    VisibleIndex="2">
                                    <Settings FilterMode="DisplayText"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="100px">
                                    <EditButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Clear" />
                                            <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btEdit">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="btNew">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New" />
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="btDelete">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Tên kho" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã kho" FieldName="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectByRowClick="True"
                                AllowSelectSingleRowOnly="True" ColumnResizeMode="NextColumn" ConfirmDelete="True">
                            </SettingsBehavior>
                            <SettingsPager PageSize="20" ShowEmptyDataRows="True" Mode="ShowPager">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline"></SettingsEditing>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                            </Settings>
                            <Styles>
                                <CommandColumn HorizontalAlign="Center" Spacing="4px">
                                </CommandColumn>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                            </Styles>
                            <Templates>
                                <EmptyDataRow>
                                    <div style="margin: 0 auto">
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                                            <ClientSideEvents Click="function(s, e) {
ManuWarehouseEditForm.Show();
}" />
                                        </dx:ASPxImage>
                                        No data to display
                                    </div>
                                </EmptyDataRow>
                            </Templates>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle HorizontalAlign="Center">
        </ContentStyle>
    </dx:ASPxPageControl>
    <uc1:uWarehouse ID="uWarehouse" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
