<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Customer.aspx.cs" Inherits="WebModule.GUI.Sales.Customer" %>

<%@ Register Src="~/Sales/UserControl/uEditCustomer.ascx" TagName="uEditCustomer"
    TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">
        //        var pagManufacturer_ActiveTabIndex = 0;

        //        function pagManufacturer_ActiveTabChanged(s, e) {
        //            pagManufacturer_ActiveTabIndex = e.tab.index;
        //            ERPCore.RaiseMainPaneResizeEvent();
        //        }

        $(document).ready(function () {

            /////START: Adjust layout when window resized
            ERPCore.WindowResize_Adjust = function (s, e) {

            };
            /////END: Adjust layout when window resized

            ///START: Bind events to CustomerEditForm
            //Raise when saved
            CustomerEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdCustomer.Refresh();
                }
            });

            //Raise when popup shown
            CustomerEditForm.BindShownEvent(function (event) {
                ldpn_grdCustomer.Hide();
            });
            ///END: Bind events to CustomerEditForm


        });

        //Manufacturer gridview custom button click event handler
        function grdCustomer_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add          
                case "grdCustomer_Add":
                    //Show loading panel for preventing multi-click
                    ldpn_grdCustomer.Show();
                    var headerText = 'Thông Tin Khách Hàng - Thêm Mới';
                    CustomerEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit          
                case "grdCustomer_Edit":
                    //Show loading panel for preventing multi-click
                    ldpn_grdCustomer.Show();
                    //Get data of ManufacturerId, Code column in clicked row
                    s.GetRowValues(e.visibleIndex, 'OrganizationId;Code', grdCustomerOnGetRowValues);
                    break;
                //When custom button ID is Delete   
                case "grdCustomer_Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdCustomer.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        //GetRowValues callback of grdCustomer gridview
        function grdCustomerOnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Khách Hàng - ' + values[1];
            CustomerEditForm.Show(headerText, recordId);
        }

        //EndCallback Handler of grdCustomer gridview
        function grdCustomer_EndCallback(s, e) {
            //Refresh the gridview when data is deleted
            if (s.cpEvent == 'deleted') {
                grdCustomer.Refresh();
            }
            delete s.cpEvent;
        }

        function grdCustomer_Init(s, e) {
            //Press F2 to show edit popup
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                //Show loading panel for preventing multi-click
                ldpn_grdCustomer.Show();
                //Get data of ManufacturerId, Code column in clicked row
                s.GetRowValues(focusedRowIndex, 'OrganizationId;Code', grdCustomerOnGetRowValues);
            });
            //Press Insert to show insert popup
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                ldpn_grdCustomer.Show();
                var headerText = 'Thông Tin Khách Hàng - Thêm Mới';
                CustomerEditForm.Show(headerText);
            });
            //Press Delete to delete record
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                if (confirmMessage == true) {
                    var args = 'delete';
                    args += '|' + s.GetRowKey(focusedRowIndex);
                    grdCustomer.PerformCallback(args);
                }
            });

            s.GetMainElement().focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">

    <dx:ASPxPageControl ID="pagCustomer" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
        Width="100%">
        <TabPages>
            <dx:TabPage Text="Khách Hàng">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="dsCustomer" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
                            Criteria="[RowStatus] > 0">
                        </dx:XpoDataSource>
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdCustomer" ClientInstanceName="ldpn_grdCustomer"
                            ContainerElementID="grdCustomer" runat="server">
                            <LoadingDivStyle BackColor="Transparent">
                            </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="grdCustomer" ClientInstanceName="grdCustomer" KeyboardSupport="true" runat="server"
                            AutoGenerateColumns="False" KeyFieldName="OrganizationId" Width="100%" DataSourceID="dsCustomer"
                            OnCustomColumnDisplayText="grdCustomer_CustomColumnDisplayText" OnCustomCallback="grdCustomer_CustomCallback">
                            <ClientSideEvents CustomButtonClick="grdCustomer_CustomButtonClick" EndCallback="grdCustomer_EndCallback"
                                Init="grdCustomer_Init">
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
                                        <dx:GridViewCommandColumnCustomButton ID="grdCustomer_Edit">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdCustomer_Add">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdCustomer_Delete">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã số thuế" FieldName="TaxNumber" ShowInCustomizationForm="True"
                                    VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Visible="false" ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="60px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="100px" Visible="False">
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
                            <Templates>
                                <EmptyDataRow>
                                    <div style="margin: 0 auto">
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                                            <ClientSideEvents Click="function(s, e) {
                    //Show loading panel for preventing multi-click
                    ldpn_grdCustomer.Show();
                    var headerText = 'Thông Tin Khách Hàng - Thêm Mới';
                    CustomerEditForm.Show(headerText);
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
        <ContentStyle>
            <BorderLeft BorderWidth="0px"></BorderLeft>
            <BorderRight BorderWidth="0px"></BorderRight>
            <BorderBottom BorderWidth="0px"></BorderBottom>
        </ContentStyle>
    </dx:ASPxPageControl>

    <uc2:uEditCustomer ID="uEditCustomer1" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
