<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CombineManufacturer.aspx.cs" Inherits="WebModule.Purchasing.CombineManufacturer" %>

<%@ Register Src="UserControl/uManufacturerEdit.ascx" TagName="uManufacturerEdit"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var pagManufacturer_ActiveTabIndex = 0;
        function pagManufacturer_ActiveTabChanged(s, e) {
            pagManufacturer_ActiveTabIndex = e.tab.index;
            ERPCore.RaiseMainPaneResizeEvent();
        }

        $(document).ready(function () {

            /////START: Adjust layout when window resized
            ERPCore.WindowResize_Adjust = function (s, e) {
                var pagManufacturerPaddingX =
                                $("#pagManufacturer .dxtc-content").outerWidth(true)
                              - $("#pagManufacturer .dxtc-content").width();
                var pagManufacturerPaddingY =
                                $("#pagManufacturer .dxtc-content").outerHeight(true)
                              - $("#pagManufacturer .dxtc-content").height();
                var pagManufacturerTabHeight = $("#pagManufacturer .dxtc-strip").outerHeight(true);

                if (pagManufacturer_ActiveTabIndex == 0) {
                    grdDataManufacturer.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    //grdDataManufacturer.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
                }
            };
            /////END: Adjust layout when window resized

            ///START: Bind events to ManufacturerEditForm
            //Raise when saved
            ManufacturerEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataManufacturer.Refresh();
                }
            });
            /////2013-09-20 Khoa.Truong INS START
            //Raise when popup shown
            ManufacturerEditForm.BindShownEvent(function (event) {
                ldpn_grdDataManufacturer.Hide();
            });
            /////2013-09-20 Khoa.Truong INS END
            ///END: Bind events to ManufacturerEditForm


        });

        //Manufacturer gridview custom button click event handler
        function grdDataManufacturer_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add       
                case "grdDataManufacturer_Add":
                    /////2013-09-20 Khoa.Truong INS START
                    //Show loading panel for preventing multi-click
                    ldpn_grdDataManufacturer.Show();
                    /////2013-09-20 Khoa.Truong INS END
                    var headerText = 'Thông Tin Nhà Sản Xuất - Thêm Mới';
                    ManufacturerEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit       
                case "grdDataManufacturer_Edit":
                    /////2013-09-20 Khoa.Truong INS START
                    //Show loading panel for preventing multi-click
                    ldpn_grdDataManufacturer.Show();
                    /////2013-09-20 Khoa.Truong INS END
                    //Get data of ManufacturerId, Code column in clicked row
                    s.GetRowValues(e.visibleIndex, 'OrganizationId;Code', grdDataManufacturerOnGetRowValues);
                    break;
                //When custom button ID is Delete
                case "grdDataManufacturer_Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataManufacturer.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        //GetRowValues callback of grdDataManufacturer gridview
        function grdDataManufacturerOnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Nhà Sản Xuất - ' + values[1];
            console.log('Edit');
            ManufacturerEditForm.Show(headerText, recordId);
        }

        //EndCallback Handler of grdDataManufacturer gridview
        function grdDataManufacturer_EndCallback(s, e) {
            //Refresh the gridview when data is deleted
            if (s.cpEvent == 'deleted') {
                grdDataManufacturer.Refresh();
            }
            delete s.cpEvent;
        }


        function grdDataManufacturer_Init(s, e) {
            //Press F2 to show edit popup
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                ldpn_grdDataManufacturer.Show();
                s.GetRowValues(focusedRowIndex, 'OrganizationId;Code', grdDataManufacturerOnGetRowValues);
            });
            //Press Insert to show insert popup
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                ldpn_grdDataManufacturer.Show();
                var headerText = 'Thông Tin Nhà Sản Xuất - Thêm Mới';
                ManufacturerEditForm.Show(headerText);
            });
            //Press Delete to delete record
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                if (confirmMessage == true) {
                    var args = 'delete';
                    args += '|' + s.GetRowKey(focusedRowIndex);
                    grdDataManufacturer.PerformCallback(args);
                }
            });

            s.GetMainElement().focus();
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagManufacturer" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
        Width="100%">
        <TabPages>
            <dx:TabPage Text="Nhà Sản Xuất">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="dsManufacturer" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.ManufacturerOrg"
                            Criteria="[RowStatus] > 0">
                        </dx:XpoDataSource>
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataManufacturer" ClientInstanceName="ldpn_grdDataManufacturer"
                            ContainerElementID="grdDataManufacturer" runat="server">
                            <LoadingDivStyle BackColor="Transparent">
                            </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="grdDataManufacturer" ClientInstanceName="grdDataManufacturer"
                            KeyboardSupport="true"
                            runat="server" AutoGenerateColumns="False" KeyFieldName="OrganizationId" Width="100%"
                            DataSourceID="dsManufacturer" 
                            OnCustomColumnDisplayText="grdDataManufacturer_CustomColumnDisplayText" 
                            OnCustomCallback="grdDataManufacturer_CustomCallback">
                            <ClientSideEvents CustomButtonClick="grdDataManufacturer_CustomButtonClick" 
                                              EndCallback="grdDataManufacturer_EndCallback"
                                              Init="grdDataManufacturer_Init">
                            </ClientSideEvents>
                            <Columns>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="5"
                                    Width="100px" ButtonType="Image">
                                    <ClearFilterButton Visible="True">
                                        <Image ToolTip="Hủy">
                                            <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataManufacturer_Edit">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataManufacturer_Add">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataManufacturer_Delete">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã nhà sản xuất" FieldName="Code" Name="Code"
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên nhà sản xuất" FieldName="Name" Name="Code"
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="60px" Visible="false">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="100px" Visible="False">
                                    <Settings FilterMode="DisplayText" />
                                    <Settings FilterMode="DisplayText"></Settings>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="OrganizationId" ShowInCustomizationForm="True"
                                    Visible="false" VisibleIndex="-1" Width="150px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ColumnResizeMode="Control" ConfirmDelete="True" />
                            <SettingsPager PageSize="20" ShowEmptyDataRows="True" Mode="ShowPager" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="True" />
                            <SettingsBehavior ConfirmDelete="True" AllowGroup="False" AllowSelectByRowClick="True"
                                AllowSelectSingleRowOnly="True" ColumnResizeMode="Control"></SettingsBehavior>
                            <SettingsPager PageSize="20" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                            </Settings>
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                                <HeaderPanel HorizontalAlign="Center">
                                </HeaderPanel>
                                <CommandColumn HorizontalAlign="Center" Spacing="4px">
                                </CommandColumn>
                            </Styles>
                            <ClientSideEvents CustomButtonClick="grdDataManufacturer_CustomButtonClick" EndCallback="grdDataManufacturer_EndCallback" />
                            <Templates>
                                <EmptyDataRow>
                                    <div style="margin: 0 auto">
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                                            <ClientSideEvents Click="function(s, e) {
/////2013-09-20 Khoa.Truong INS START
                    //Show loading panel for preventing multi-click
                    ldpn_grdDataManufacturer.Show();
                    /////2013-09-20 Khoa.Truong INS END
                    var headerText = 'Thông Tin Nhà Sản Xuất - Thêm Mới';
                    ManufacturerEditForm.Show(headerText);
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
        <ClientSideEvents ActiveTabChanged="pagManufacturer_ActiveTabChanged"></ClientSideEvents>
        <ContentStyle>
            <BorderLeft BorderWidth="0px" />
            <BorderRight BorderWidth="0px" />
            <BorderBottom BorderWidth="0px" />
            <BorderLeft BorderWidth="0px"></BorderLeft>
            <BorderRight BorderWidth="0px"></BorderRight>
            <BorderBottom BorderWidth="0px"></BorderBottom>
        </ContentStyle>
        <ClientSideEvents ActiveTabChanged="pagManufacturer_ActiveTabChanged" />
    </dx:ASPxPageControl>
    <uc1:uManufacturerEdit ID="uManufacturerEdit1" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
