<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CombineService.aspx.cs" Inherits="WebModule.Purchasing.CombineService" %>

<%@ Register Src="UserControl/uBuyingService.ascx" TagName="uBuyingService" TagPrefix="uc1" %>
<%@ Register Src="UserControl/uBuyingServiceCategory.ascx" TagName="uBuyingServiceCategory"
    TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            BuyingServiceEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataService.Refresh();
                }
            });

            BuyingServiceCategoryEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataServiceCategory.Refresh();
                }
            });
        });

        //BuyingService gridview custom button click event handler
        function grdDataService_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add    
                case "grdDataService_Add":
                    var headerText = 'Thông Tin Dịch Vụ - Thêm Mới';
                    BuyingServiceEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit  
                case "grdDataService_Edit":
                    s.GetRowValues(e.visibleIndex, 'BuyingServiceId;Code', grdDataService_OnGetRowValues);
                    break;
                //When custom button ID is Delete  
                case "grdDataService_Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataService.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataService_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Dịch Vụ - ' + values[1];
            BuyingServiceEditForm.Show(headerText, recordId);
        }

        function grdDataService_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataService.Refresh();
            }
            delete s.cpEvent;
        }


        //BuyingServiceCategory gridview custom button click event handler
        function grdDataServiceCategory_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add   
                case "grdDataServiceCategory_Add":
                    var headerText = 'Thông Tin Nhóm Dịch Vụ - Thêm Mới';
                    BuyingServiceCategoryEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit 
                case "grdDataServiceCategory_Edit":
                    s.GetRowValues(e.visibleIndex, 'BuyingServiceCategoryId!Key;BuyingServiceCategoryId.Code', grdDataServiceCategory_OnGetRowValues);
                    break;
                //When custom button ID is Delete 
                case "grdDataServiceCategory_Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataServiceCategory.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function grdDataServiceCategory_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Nhóm Dịch Vụ - ' + values[1];
            BuyingServiceCategoryEditForm.Show(headerText, recordId);
        }

        function grdDataServiceCategory_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataServiceCategory.Refresh();
            }
            delete s.cpEvent;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagCombineService" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
        Width="100%">
        <TabPages>
            <dx:TabPage Text="Dịch Vụ">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="dsBuyingService" runat="server" 
                            Criteria="[Language] = ? And [RowStatus] In ('A', 'I')" DefaultSorting="" 
                            TypeName="DAL.Purchasing.vwBuyingServiceBuyingServiceDetailBuyingServiceProperty">
                            <CriteriaParameters>
                                <asp:Parameter DefaultValue="VN" Name="Language" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:ASPxGridView ID="grdDataService" runat="server" ClientInstanceName="grdDataService"
                            AutoGenerateColumns="False"  Width="100%" 
                            DataSourceID="dsBuyingService" KeyFieldName="BuyingServiceId" OnCustomCallback="grdDataService_CustomCallback" 
                            OnCustomColumnDisplayText="grdDataService_CustomColumnDisplayText">
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="4" Width="100px">
                                    <ClearFilterButton Visible="True">
                                        <Image ToolTip="Hủy">
                                            <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataService_Edit">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataService_Add">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataService_Delete">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="60px">
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="100px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên dịch vụ" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã dịch vụ" FieldName="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True" ColumnResizeMode="Control" />
                            <SettingsPager PageSize="20" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                                <HeaderPanel HorizontalAlign="Center">
                                </HeaderPanel>
                                <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                </CommandColumn>
                            </Styles>
                            <ClientSideEvents CustomButtonClick="grdDataService_CustomButtonClick" 
                                EndCallback="grdDataService_EndCallback" />
                            <Templates>
                                <EmptyDataRow>
                                    <dx:ASPxImage ID="ASPxImage1" runat="server" ToolTip="Thêm" SpriteCssClass="Sprite_New"
                                        Cursor="pointer">
                                        <ClientSideEvents Click="function(s,e) { BuyingServiceEditForm.Show('Thông Tin Dịch Vụ - Thêm Mới'); }" />
                                    </dx:ASPxImage>
                                    <div>
                                        No data to display</div>
                                </EmptyDataRow>
                            </Templates>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Nhóm Dịch Vụ">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="dsBuyingServiceCategoryProperty" runat="server" DefaultSorting="" 
                            TypeName="DAL.Purchasing.BuyingServiceCategoryProperty" 
                            
                            Criteria="[Language] = ? And [BuyingServiceCategoryId.RowStatus] In ('A', 'I')">
                            <CriteriaParameters>
                                <asp:Parameter DefaultValue="VN" Name="Language" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:ASPxGridView ID="grdDataServiceCategory" ClientInstanceName="grdDataServiceCategory" runat="server" 
                            AutoGenerateColumns="False" KeyFieldName="BuyingServiceCategoryId!Key" 
                            Width="100%" DataSourceID="dsBuyingServiceCategoryProperty" 
                            OnCustomCallback="grdDataServiceCategory_CustomCallback" 
                            
                            OnCustomColumnDisplayText="grdDataServiceCategory_CustomColumnDisplayText" >
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tên nhóm dịch vụ" FieldName="Name"
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Width="100px">
                                    <ClearFilterButton Visible="True">
                                        <Image ToolTip="Hủy">
                                            <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataServiceCategory_Edit">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataServiceCategory_Add">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="grdDataServiceCategory_Delete">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã nhóm dịch vụ" FieldName="BuyingServiceCategoryId.Code"
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Chi tiết" ShowInCustomizationForm="True" VisibleIndex="4"
                                    Width="60px" ButtonType="Image">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail2">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng thái" 
                                    FieldName="BuyingServiceCategoryId.RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="100px">
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
                            </Columns>
                            <SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                            <SettingsPager PageSize="20" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                                ShowFilterRowMenu="True" />
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
                                    <dx:ASPxImage ID="ASPxImage1" runat="server" ToolTip="Thêm" SpriteCssClass="Sprite_New"
                                        Cursor="pointer">
                                        <ClientSideEvents Click="function(s,e) { BuyingServiceCategoryEditForm.Show('Thông Tin Nhóm Dịch Vụ - Thêm Mới'); }" />
                                    </dx:ASPxImage>
                                    <div>
                                        No data to display</div>
                                </EmptyDataRow>
                            </Templates>
                            <ClientSideEvents EndCallback="grdDataServiceCategory_EndCallback" CustomButtonClick="grdDataServiceCategory_CustomButtonClick" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <uc1:uBuyingService ID="uBuyingService1" runat="server" />
    <uc2:uBuyingServiceCategory ID="uBuyingServiceCategory1" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
