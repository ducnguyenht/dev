<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CombineDevice.aspx.cs" Inherits="WebModule.Purchasing.CombineDevice" %>

<%@ Register Src="UserControl/uBuyingDevice.ascx" TagName="uBuyingDevice" TagPrefix="uc1" %>
<%@ Register Src="UserControl/uBuyingDeviceCategory.ascx" TagName="uBuyingDeviceCategory"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl/uDeviceUnit.ascx" TagName="uDeviceUnit" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">    
    <script type="text/javascript">
        var pagManufacturer_ActiveTabIndex = 0;

        function pagDevice_ActiveTabChanged(s, e) {
            pagManufacturer_ActiveTabIndex = e.tab.index;
            ERPCore.RaiseMainPaneResizeEvent();
        }

        $(document).ready(function () {

            ERPCore.WindowResize_Adjust = function (s, e) {
                var pagManufacturerPaddingX =
                                $("#pagDevice .dxtc-content").outerWidth(true)
                              - $("#pagDevice .dxtc-content").width();
                var pagManufacturerPaddingY =
                                $("#pagDevice .dxtc-content").outerHeight(true)
                              - $("#pagDevice .dxtc-content").height();
                var pagManufacturerTabHeight = $("#pagDevice .dxtc-strip").outerHeight(true);
                if (pagManufacturer_ActiveTabIndex == 0) {
                    grdDataDevice.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    grdDataDevice.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);

                } else if (pagManufacturer_ActiveTabIndex == 1) {
                    console.log(e.pane.GetClientWidth() + '-' + pagManufacturerPaddingX);
                    grdDataDeviceCategory.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    grdDataDeviceCategory.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
                } else if (pagManufacturer_ActiveTabIndex == 2) {
                    grdDataDeviceUnit.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    grdDataDeviceUnit.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
                }

            };

            ManuDeviceCategoryEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataDeviceCategory.Refresh();
                }
            });

            ManuDeviceEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataDevice.Refresh();
                }
            });

            ManuDeviceUnitEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataDeviceUnit.Refresh();
                }
            });
            //Raise when popup shown
            ManuDeviceEditForm.BindShownEvent(function (event) {
                ldpn_grdDataDevice.Hide();
            });

            //Raise when popup shown
            ManuDeviceCategoryEditForm.BindShownEvent(function (event) {
                ldpn_grdDataDeviceCategory.Hide();
            });
            //Raise when popup shown
            ManuDeviceUnitEditForm.BindShownEvent(function (event) {
                ldpn_grdDataDeviceUnit.Hide();
            });
        });

        //DeviceUnit gridview custom button click event handler
        function grdDataDeviceUnit_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add 
                case "btNewUnit":
                    var headerText = 'Thông Tin Đơn Vị Tính - Thêm Mới';
                    ldpn_grdDataDeviceUnit.Show();
                    ManuDeviceUnitEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit     
                case "btEditUnit":
                    //alert(s.GetRowKey(e.visibleIndex));
                    ldpn_grdDataDeviceUnit.Show();
                    s.GetRowValues(e.visibleIndex, 'ToolUnitId;Code', grdDataDeviceUnit_OnGetRowValues);
                    //ManuDeviceUnitEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete     
                case "btDeleteUnit":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataDeviceUnit.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataDeviceUnit_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Đơn Vị Tính - ' + values[1];
            ManuDeviceUnitEditForm.Show(headerText, recordId);
        }

        function grdDataDeviceUnit_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataDeviceUnit.Refresh();
            }
            delete s.cpEvent;
        }

        //DeviceCategory gridview custom button click event handler
        function grdDataDeviceCategory_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add    
                case "btNewCategory":
                    var headerText = 'Thông Tin Nhóm Công Cụ Dụng Cụ - Thêm Mới';
                    ldpn_grdDataDeviceCategory.Show();
                    ManuDeviceCategoryEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit    
                case "btEditCategory":
                    //alert(s.GetRowKey(e.visibleIndex));
                    ldpn_grdDataDeviceCategory.Show();
                    s.GetRowValues(e.visibleIndex, 'BuyingToolCategoryId;Code', grdDataDeviceCategory_OnGetRowValues);
                    //ManuDeviceCategoryEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete    
                case "btDeleteCategory":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataDeviceCategory.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataDeviceCategory_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Nhóm Công Cụ Dụng Cụ - ' + values[1];
            ManuDeviceCategoryEditForm.Show(headerText, recordId);
        }

        function grdDataDeviceCategory_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataDeviceCategory.Refresh();
            }
            delete s.cpEvent;
        }

        //Device gridview custom button click event handler
        function grdDataDevice_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add
                case "btNew":
                    var headerText = 'Thông Tin Công Cụ Dụng Cụ - Thêm Mới';
                    ldpn_grdDataDevice.Show();
                    ManuDeviceEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit   
                case "btEdit":
                    //alert(s.GetRowKey(e.visibleIndex));
                    ldpn_grdDataDevice.Show();
                    s.GetRowValues(e.visibleIndex, 'ToolId;Code', grdDataDevice_OnGetRowValues);
                    //ManuDeviceEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete   
                case "btDelete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataDevice.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataDevice_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Công Cụ Dụng Cụ - ' + values[1];
            ManuDeviceEditForm.Show(headerText, recordId);
        }

        function grdDataDevice_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataDevice.Refresh();
            }
            delete s.cpEvent;
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagDevice" RenderMode="Lightweight" runat="server" ActiveTabIndex="0"
        Width="100%" ContentStyle-HorizontalAlign="Center">
        <TabPages>
            <dx:TabPage Text="Công cụ dụng cụ">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataDevice" ClientInstanceName="ldpn_grdDataDevice" ContainerElementID="grdDataDevice" runat="server">
                                        <LoadingDivStyle BackColor="Transparent">
                                        </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="grdDataDevice" runat="server" AutoGenerateColumns="False" Width="100%"
                            KeyFieldName="ToolId" OnCustomCallback="grdDataDevice_CustomCallback" ClientInstanceName="grdDataDevice"
                            KeyboardSupport="True">
                            <ClientSideEvents CustomButtonClick="grdDataDevice_CustomButtonClick" EndCallback="grdDataDevice_EndCallback" />
<ClientSideEvents CustomButtonClick="grdDataDevice_CustomButtonClick" EndCallback="grdDataDevice_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="6" Width="100px">
                                    <ClearFilterButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>

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
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="100px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Hoạt động" Value="A" />
                                            <dx:ListEditItem Text="Ngưng hoạt động" Value="I" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="ManufacturerName" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên công cụ dụng cụ" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã công cụ dụng cụ" FieldName="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectByRowClick="True"
                                AllowSelectSingleRowOnly="True" ColumnResizeMode="NextColumn" ConfirmDelete="True">
                            </SettingsBehavior>
                            <SettingsPager PageSize="20" ShowEmptyDataRows="True" Mode="ShowPager">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>

                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                                <CommandColumn HorizontalAlign="Center" Spacing="4px">
                                </CommandColumn>
                            </Styles>
                            <Templates>
                                <EmptyDataRow>
                                    <div style="margin: 0 auto">
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                                            <ClientSideEvents Click="function(s, e) {
ManuDeviceEditForm.Show();
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
            <dx:TabPage Text="Nhóm công cụ dụng cụ">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataDeviceCategory" ClientInstanceName="ldpn_grdDataDeviceCategory" ContainerElementID="grdDataDeviceCategory" runat="server">
                                        <LoadingDivStyle BackColor="Transparent">
                                        </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="grdDataDeviceCategory" runat="server" KeyFieldName="BuyingToolCategoryId"
                            AutoGenerateColumns="False" OnCustomCallback="grdDataDeviceCategory_CustomCallback"
                            ClientInstanceName="grdDataDeviceCategory" KeyboardSupport="True">

<SettingsEditing Mode="Inline"></SettingsEditing>

                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
                            <ClientSideEvents CustomButtonClick="grdDataDeviceCategory_CustomButtonClick" EndCallback="grdDataDeviceCategory_EndCallback" />
<ClientSideEvents CustomButtonClick="grdDataDeviceCategory_CustomButtonClick" EndCallback="grdDataDeviceCategory_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tên nhóm CCDC" FieldName="Name" Name="Name" ShowInCustomizationForm="True"
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
                                <dx:GridViewDataTextColumn Caption="Mã nhóm CCDC" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
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
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Đơn vị tính">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                         <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataDeviceUnit" ClientInstanceName="ldpn_grdDataDeviceUnit" ContainerElementID="grdDataDeviceUnit" runat="server">
                                        <LoadingDivStyle BackColor="Transparent">
                                        </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="grdDataDeviceUnit" ClientInstanceName="grdDataDeviceUnit" runat="server"
                            KeyFieldName="ToolUnitId" AutoGenerateColumns="False" Width="100%" OnCustomCallback="grdDataDeviceUnit_CustomCallback"
                            KeyboardSupport="True">
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                            <SettingsEditing Mode="Inline"></SettingsEditing>
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
                            <ClientSideEvents CustomButtonClick="grdDataDeviceUnit_CustomButtonClick" EndCallback="grdDataDeviceUnit_EndCallback" />
<ClientSideEvents CustomButtonClick="grdDataDeviceUnit_CustomButtonClick" EndCallback="grdDataDeviceUnit_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã ĐVT" FieldName="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên ĐVT" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="100px">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Sử dụng" Value="A" />
                                            <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail3">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="100px">
                                    <ClearFilterButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
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
                                <dx:GridViewDataTextColumn Caption="MaterialUnitId" FieldName="MaterialUnitId" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Visible="false">
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
ManuDeviceUnitEditForm.Show();
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
        <ClientSideEvents ActiveTabChanged="pagDevice_ActiveTabChanged" />

<ClientSideEvents ActiveTabChanged="pagDevice_ActiveTabChanged"></ClientSideEvents>

<ContentStyle HorizontalAlign="Center"></ContentStyle>
    </dx:ASPxPageControl>
    <uc1:uBuyingDevice ID="uBuyingDevice1" runat="server" />
    <uc2:uBuyingDeviceCategory ID="uBuyingDeviceCategory1" runat="server" />
    <uc4:uDeviceUnit ID="uDeviceUnit" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
