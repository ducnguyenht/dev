<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="WarehouseCategory.aspx.cs" Inherits="WebModule.WarehouseCategory" %>
<%@ Register src="UserControl/uWarehouseCategory.ascx" tagname="uWarehouseCategory" tagprefix="uc1" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo" TagPrefix="uc3" %>
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
              
                    grdDataWarehouseCategory.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    grdDataWarehouseCategory.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
            };

            ManuWarehouseCategoryEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataWarehouseCategory.Refresh();
                }
            });

            //Raise when popup shown
            ManuWarehouseCategoryEditForm.BindShownEvent(function (event) {
                ldpn_grdDataWarehouseCategory.Hide();
            });
         
        });
        //WarehouseCategory gridview custom button click event handler
        function grdDataWarehouseCategory_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add     
                case "btNewCategory":
                    var headerText = 'Thông tin thể loại kho - Thêm mới';
                    ldpn_grdDataWarehouseCategory.Show();
                    ManuWarehouseCategoryEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit
                case "btEditCategory":
                    //alert(s.GetRowKey(e.visibleIndex));
                    ldpn_grdDataWarehouseCategory.Show();
                    s.GetRowValues(e.visibleIndex, 'WarehouseCategoryId;Code', grdDataWarehouseCategory_OnGetRowValues);
                    //ManuWarehouseCategoryEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete     
                case "btDeleteCategory":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataWarehouseCategory.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataWarehouseCategory_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông tin thể loại kho - ' + values[1];
            ManuWarehouseCategoryEditForm.Show(headerText, recordId);
        }

        function grdDataWarehouseCategory_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataWarehouseCategory.Refresh();
            }
            delete s.cpEvent;
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagWarehouse" RenderMode="Lightweight" runat="server" ActiveTabIndex="0"
        Width="100%" ContentStyle-HorizontalAlign="Center">
        <TabPages>
           <dx:TabPage Text="Thể loại kho">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataWarehouseCategory" ClientInstanceName="ldpn_grdDataWarehouseCategory" ContainerElementID="grdDataWarehouseCategory" runat="server">
                                        <LoadingDivStyle BackColor="Transparent">
                                        </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="grdDataWarehouseCategory" runat="server" KeyFieldName="WarehouseCategoryId"
                            AutoGenerateColumns="False" OnCustomCallback="grdDataWarehouseCategory_CustomCallback"
                            ClientInstanceName="grdDataWarehouseCategory" KeyboardSupport="True">

<SettingsEditing Mode="Inline"></SettingsEditing>

                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
                            <ClientSideEvents CustomButtonClick="grdDataWarehouseCategory_CustomButtonClick" EndCallback="grdDataWarehouseCategory_EndCallback" />
<ClientSideEvents CustomButtonClick="grdDataWarehouseCategory_CustomButtonClick" EndCallback="grdDataWarehouseCategory_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tên thể loại kho" FieldName="Name" Name="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Width="100px">
                                    <ClearFilterButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btEditCategory">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="btNewCategory">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="btDeleteCategory">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã thể loại kho" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail2">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document" />
<SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="100px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Sử dụng" Value="A" />
                                            <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True" />

<SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ConfirmDelete="True"></SettingsBehavior>

                            <SettingsPager Mode="ShowPager" PageSize="20" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowFilterRowMenu="true" />
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
ManuWarehouseCategoryEditForm.Show();
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
    <uc1:uWarehouseCategory ID="uWarehouseCategory" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
