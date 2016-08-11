<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="WarehouseUnit.aspx.cs" Inherits="WebModule.WarehouseUnit" %>
    <%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo" TagPrefix="uc3" %>
<%@ Register Src="UserControl/uWarehouseUnit.ascx" TagName="uWarehouseUnit" TagPrefix="uc1" %>
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

                grdDataWarehouseUnit.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                grdDataWarehouseUnit.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
            };

            ManuWarehouseUnitEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataWarehouseUnit.Refresh();
                }
            });

            //Raise when popup shown
            ManuWarehouseUnitEditForm.BindShownEvent(function (event) {
                ldpn_grdDataWarehouseUnit.Hide();
            });

        });
        //WarehouseUnit gridview custom button click event handler
        function grdDataWarehouseUnit_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add
                case "btNewUnit":
                    var headerText = 'Thông Tin đơn vị lưu trữ - Thêm Mới';
                    ldpn_grdDataWarehouseUnit.Show();
                    ManuWarehouseUnitEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit
                case "btEditUnit":
                    //alert(s.GetRowKey(e.visibleIndex));
                    ldpn_grdDataWarehouseUnit.Show();
                    s.GetRowValues(e.visibleIndex, 'InventoryUnitId;Name', grdDataWarehouseUnit_OnGetRowValues);
                    //ManuWarehouseUnitEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete      
                case "btDeleteUnit":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataWarehouseUnit.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataWarehouseUnit_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông tin đơn vị lưu trữ - ' + values[1];
            ManuWarehouseUnitEditForm.Show(headerText, recordId);
        }

        function grdDataWarehouseUnit_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataWarehouseUnit.Refresh();
            }
            delete s.cpEvent;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagWarehouse" RenderMode="Lightweight" runat="server" ActiveTabIndex="0"
        Width="100%" ContentStyle-HorizontalAlign="Center">
        <TabPages>
           <dx:TabPage Text="Đơn vị lưu trữ">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                         <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataWarehouseUnit" ClientInstanceName="ldpn_grdDataWarehouseUnit" ContainerElementID="grdDataWarehouseUnit" runat="server">
                                        <LoadingDivStyle BackColor="Transparent">
                                        </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                         <dx:XpoDataSource ID="XpoInventoryUnit" runat="server" DefaultSorting="" 
                             TypeName="NAS.DAL.Nomenclature.Inventory.InventoryUnit" 
                             Criteria="[RowStatus] &gt; 0">
                         </dx:XpoDataSource>
                        <dx:ASPxGridView ID="grdDataWarehouseUnit" ClientInstanceName="grdDataWarehouseUnit" runat="server"
                            KeyFieldName="InventoryUnitId" AutoGenerateColumns="False" Width="100%" OnCustomCallback="grdDataWarehouseUnit_CustomCallback"
                            KeyboardSupport="True" DataSourceID="XpoInventoryUnit" OnCustomColumnDisplayText="grdDataWarehouseUnit_CustomColumnDisplayText">
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                            <SettingsEditing Mode="Inline"></SettingsEditing>
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
                            <ClientSideEvents CustomButtonClick="grdDataWarehouseUnit_CustomButtonClick" EndCallback="grdDataWarehouseUnit_EndCallback" />
<ClientSideEvents CustomButtonClick="grdDataWarehouseUnit_CustomButtonClick" EndCallback="grdDataWarehouseUnit_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tên đơn vị lưu trữ" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="100px" Visible="false">
                                    <Settings FilterMode="DisplayText" />
                                    <Settings FilterMode="DisplayText"></Settings>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                               <%-- <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail3">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>--%>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="100px">
                                    <ClearFilterButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Clear" />
                                        </Image>
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btEditUnit">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="btNewUnit">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="btDeleteUnit">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>

                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="WarehouseUnitId" 
                                    FieldName="InventoryUnitId" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Visible="False">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True" ColumnResizeMode="Control" />
                            <SettingsPager PageSize="20" Mode="ShowPager" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="True" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                                <HeaderPanel HorizontalAlign="Center">
                                </HeaderPanel>
                                <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                </CommandColumn>
                            </Styles>
                            <Templates>
                                <EmptyDataRow>
                                    <div style="margin: 0 auto">
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                                            <ClientSideEvents Click="function(s, e) {
ManuWarehouseUnitEditForm.Show();
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
        <ClientSideEvents ActiveTabChanged="pagWarehouse_ActiveTabChanged" />

<ClientSideEvents ActiveTabChanged="pagWarehouse_ActiveTabChanged"></ClientSideEvents>

<ContentStyle HorizontalAlign="Center"></ContentStyle>
    </dx:ASPxPageControl>
    <uc1:uWarehouseUnit ID="uWarehouseUnit" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
