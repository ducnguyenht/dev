<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ActiveElement.aspx.cs" Inherits="WebModule.Purchasing.ActiveElement" %>
<%@ Register Src="UserControl/uActiveElement.ascx" TagName="uActiveElement"
    TagPrefix="uc2" %>
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
            ERPCore.WindowResize_Adjust = function (s, e) {
                var pagManufacturerPaddingX =
                                $("#pagManufacturer .dxtc-content").outerWidth(true)
                              - $("#pagManufacturer .dxtc-content").width();
                var pagManufacturerPaddingY = 
                                $("#pagManufacturer .dxtc-content").outerHeight(true)
                              - $("#pagManufacturer .dxtc-content").height();
                var pagManufacturerTabHeight = $("#pagManufacturer .dxtc-strip").outerHeight(true);

                if (pagManufacturer_ActiveTabIndex == 0) {
                    grdDataActiveElement.SetHeight(e.pane.GetClientHeight() - pagManufacturerPaddingY - pagManufacturerTabHeight);
                    grdDataActiveElement.SetWidth(e.pane.GetClientWidth() - pagManufacturerPaddingX);
                }
            };
            /////2013-09-20 Khoa.Truong DEL START
//            $(ManufacturerCategoryEditForm).on('saved', function (event, args) {
//                alert('==>' + args.isSuccess);
//                if (args.isSuccess == true) {
//                    grdDataActiveElement.Refresh();
//                    alert('s:' + args.isSuccess);
//                }
//                else {
//                    alert('f:' + args.isSuccess);
//                }
//                alert(args.recordId + "\n" + args.isSuccess);
//            });
            /////2013-09-20 Khoa.Truong DEL END

            /////2013-09-20 Khoa.Truong INS START
            ManufacturerCategoryEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdDataActiveElement.Refresh();
                }
            });
            /////2013-09-20 Khoa.Truong INS END

            /////2013-09-20 ERP-572 Khoa.Truong INS START
            //Raise when popup shown
            ManufacturerCategoryEditForm.BindShownEvent(function (event) {
                ldpn_grdDataActiveElement.Hide();
            });
            /////2013-09-20 ERP-572 Khoa.Truong INS END

        });

        /////2013-09-20 Khoa.Truong DEL START
//        ManufacturerCategoryEditForm.BindSavedEvent(function (event, args) {
//            if (args.isSuccess == true) {
//                grdDataActiveElement.Refresh();
//            }
//        });

//        ManufacturerEditForm.BindSavedEvent(function (event, args) {
//            if (args.isSuccess == true) {
//                grdDataActiveElement.Refresh();
//            }
//        });
        /////2013-09-20 Khoa.Truong DEL END

        //Manufacturer Category gridview custom button click event handler
        function grdDataManufacturerGroup_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add 
                case "Add":

                    /////2013-09-20 ERP-572 Khoa.Truong INS START
                    ldpn_grdDataActiveElement.Show();
                    /////2013-09-20 ERP-572 Khoa.Truong INS END

                    ManufacturerCategoryEditForm.Show();
                    break;
                //When custom button ID is Edit 
                case "Edit":
                    //alert(s.GetRowKey(e.visibleIndex));

                    /////2013-09-20 ERP-572 Khoa.Truong INS START
                    ldpn_grdDataActiveElement.Show();
                    /////2013-09-20 ERP-572 Khoa.Truong INS END

                    s.GetRowValues(e.visibleIndex, 'ActiveElementId;Code', UpdateActiveElementAction);
                    //ManufacturerCategoryEditForm.Show(s.GetRowKey(e.visibleIndex));
                    break;
                //When custom button ID is Delete
                case "Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdDataActiveElement.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }
        function UpdateActiveElementAction(values) {
            ManufacturerCategoryEditForm.Show(values[0], values[1]);
        }
        function grdDataManufacturerGroup_EndCallback(s, e) {
            if (s.cpEvent == 'deleted') {
                grdDataActiveElement.Refresh();
            }
            delete s.cpEvent;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pagManufacturer" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
        Width="100%"> 
        <TabPages>
            <dx:TabPage Text="Hoạt chất">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataActiveElement" ClientInstanceName="ldpn_grdDataActiveElement" ContainerElementID="grdDataActiveElement" runat="server">
                                        <LoadingDivStyle BackColor="Transparent">
                                        </LoadingDivStyle>
                                    </dx:ASPxLoadingPanel>
                                    <dx:ASPxGridView ID="grdDataActiveElement" 
                                        ClientInstanceName="grdDataActiveElement" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="ActiveElementId" Width="100%"
                                        OnCustomCallback="grdDataManufacturerGroup_CustomCallback" 
                                        OnDataBinding="grdDataActiveElement_DataBinding">
<ClientSideEvents CustomButtonClick="grdDataManufacturerGroup_CustomButtonClick" EndCallback="grdDataManufacturerGroup_EndCallback"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên hoạt chất" FieldName="Name"
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="6"
                                                Width="100px" ButtonType="Image">
                                                <ClearFilterButton Visible="True">
                                                    <Image ToolTip="Hủy">
                                                        <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                    </Image>
                                                </ClearFilterButton>
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="Edit">
                                                        <Image ToolTip="Sửa">
                                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="Add">
                                                        <Image ToolTip="Thêm">
                                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="Delete">
                                                        <Image ToolTip="Xóa">
                                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã hoạt chất" FieldName="Code" Name="Code"
                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành phần" FieldName="Component" Name="Description"
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Chức năng" ShowInCustomizationForm="True" 
                                                VisibleIndex="3" FieldName="ActiveFunction">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" ShowInCustomizationForm="True"
                                                VisibleIndex="5" Width="60px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="showCommonDetail2">
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
                                            <dx:GridViewDataTextColumn Caption="ActiveElementId" FieldName="ActiveElementId" ShowInCustomizationForm="True"
                                                Visible="false">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>

                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ColumnResizeMode="Control" ConfirmDelete="True" />
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True" Mode="ShowPager" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="True" />

<SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>

<SettingsPager PageSize="20" ShowEmptyDataRows="True"></SettingsPager>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>

                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                            </Header>
                                            <HeaderPanel HorizontalAlign="Center">
                                            </HeaderPanel>
                                            <CommandColumn HorizontalAlign="Center" Spacing="4px">
                                            </CommandColumn>
                                        </Styles>

                                        <ClientSideEvents CustomButtonClick="grdDataManufacturerGroup_CustomButtonClick" EndCallback="grdDataManufacturerGroup_EndCallback" />

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
    <uc2:uActiveElement ID="uActiveElement" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>

