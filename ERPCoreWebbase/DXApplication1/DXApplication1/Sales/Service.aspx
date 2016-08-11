<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Service.aspx.cs" Inherits="WebModule.GUI.Sales.Service" %>
<%@ Register Src="~/Purchasing/UserControl/uBuyingService.ascx" TagName="uBuyingService"
    TagPrefix="uc1" %>
<%@ Register Src="~/Purchasing/UserControl/uBuyingServiceCategory.ascx" TagName="uBuyingServiceCategory"
    TagPrefix="uc1" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        //Service
        // MainForm Event
        function grdDataService_EndCallback(s, e) {
            if (s.cpEditService) {
                formServiceEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerService');
                cboRowStatusService.SetValue('A');

                if (s.cpEditService == 'edit') {
                    cpLineService.PerformCallback('edit');
                }
                else {
                    cpLineService.PerformCallback('new');
                }
                delete (s.cpEditService);
                return;
            }
        }

        function grdDataService_CustomButtonClick(s, e) {
            //s.GetRowValues(e.visibleIndex, 'code', OnGetRowValues);
            if (e.buttonID == 'showCommonDetail1')
                popupCommonDetailInfo.Show();
        }

        function OnGetRowValues(values) {
            formServiceEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerService');

            var str = 'view|' + values;
            cpLineService.PerformCallback(str);
        }

        // EditForm Event 
        function buttonSaveService_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCodeService').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCodeService').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtNameService').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtNameService').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturerService').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('cboManufacturerService').Validate();
                return;
            }

            cpLineService.PerformCallback('save');
            cpHeaderService.PerformCallback('refresh');

        }

        function buttonCancelService_Click(s, e) {
            formServiceEdit.Hide();
        }

        //Service

        //ServiceCategory
        function grdDataServiceCategory_EndCallback(s, e) {
            if (s.cpEditServiceCategory) {
                formServiceCategoryEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerServiceCategory');
                cboRowStatusServiceCategory.SetValue('A');
                if (s.cpEditServiceCategory == 'edit') {
                    cpLineServiceCategory.PerformCallback('edit');
                }
                else {
                    cpLineServiceCategory.PerformCallback('new');
                }
                delete (s.cpEditServiceCategory);
                return;
            }
        }

        function cpHeaderServiceCategory_EndCallback(s, e) {
        }

        function cpLineServiceCategory_EndCallback(s, e) {
            if (s.cpCodeExisting) {
            }
        }

        function buttonSaveServiceCategory_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCodeServiceCategory').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCodeServiceCategory').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtNameServiceCategory').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtNameServiceCategory').Validate();
                return;
            }

            //cpLine.PerformCallback('validate');
            cpLineServiceCategory.PerformCallback('save');
            cpHeaderServiceCategory.PerformCallback('refresh');

        }

        function buttonCancelServiceCategory_Click(s, e) {
            formServiceCategoryEdit.Hide();
        }

        function grdDataServiceCategory_CustomButtonClick(s, e) {
            //s.GetRowValues(e.visibleIndex, 'code', OnGetRowValuesServiceCategory);
            if (e.buttonID == 'showCommonDetail2')
                popupCommonDetailInfo.Show();
        }

        function OnGetRowValuesServiceCategory(values) {
            formServiceCategoryEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerServiceCategory');

            var str = 'view|' + values;
            cpLineServiceCategory.PerformCallback(str);
        }
        //ServiceCategory
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<table style="width:100%;">
<tr>
    <td style="vertical-align:top;">
            <div class="gridContainer">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                    RenderMode="Lightweight">
                    <TabPages>
                        <dx:TabPage Text="Dịch Vụ">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">                     
                                    <dx:ASPxCallbackPanel ID="cpHeaderService" runat="server" Width="100%" ClientInstanceName="cpHeaderService"
                                        OnCallback="cpHeaderService_Callback" HideContentOnCallback="True" >
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdDataService" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdDataService_RowDeleting"
                                                    OnStartRowEditing="grdDataService_StartRowEditing"  Width="100%"
                                                    OnInitNewRow="grdDataService_InitNewRow" KeyFieldName="code">
                                                    <ClientSideEvents EndCallback="grdDataService_EndCallback" CustomButtonClick="grdDataService_CustomButtonClick" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã Dịch Vụ" FieldName="code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Dịch Vụ" FieldName="name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="rowstatus" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="100px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="60px">
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
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Width="100px">
                                                            <EditButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                </Image>
                                                            </NewButton>                                                
                                                            <DeleteButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                        ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                                                    <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowFilterRowMenu="True" />
                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                        <HeaderPanel HorizontalAlign="Center">
                                                        </HeaderPanel>
                                                        <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Nhóm Dịch Vụ">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">                                   
                                    <dx:ASPxCallbackPanel ID="cpHeaderServiceCategory" runat="server" Width="100%" ClientInstanceName="cpHeaderServiceCategory"
                                        OnCallback="cpHeaderServiceCategory_Callback" HideContentOnCallback="True" >
                                        <ClientSideEvents EndCallback="cpHeaderServiceCategory_EndCallback" />
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdDataServiceCategory" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="code" OnRowDeleting="grdDataServiceCategory_RowDeleting" OnStartRowEditing="grdDataServiceCategory_StartRowEditing"
                                                     Width="100%" OnInitNewRow="grdDataServiceCategory_InitNewRow">
                                                    <ClientSideEvents EndCallback="grdDataServiceCategory_EndCallback" CustomButtonClick="grdDataServiceCategory_CustomButtonClick" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã Nhóm Dịch Vụ" FieldName="code" Name="Code"
                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm Dịch Vụ" FieldName="name" Name="Name"
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="rowstatus" ShowInCustomizationForm="True"
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
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" Name="Description"
                                                            ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="60px">
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="showCommonDetail2">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>   
                                                        <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="5"
                                                            Width="100px" ButtonType="Image">
                                                            <EditButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                </Image>
                                                            </NewButton>                                                
                                                            <DeleteButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                        ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                                                    <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" ShowFilterRowMenu="True" />
                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                        <HeaderPanel HorizontalAlign="Center">
                                                        </HeaderPanel>
                                                        <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
   

          </div> 
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
             <div id="clientContainer">
                <uc1:uBuyingService ID="uBuyingService" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:uBuyingServiceCategory ID="uBuyingServiceCategory" runat="server" />
        </td>
    </tr>
</table>
<uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
