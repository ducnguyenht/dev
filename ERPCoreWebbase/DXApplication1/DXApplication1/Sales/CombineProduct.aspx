<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CombineProduct.aspx.cs" Inherits="WebModule.Sales.CombineProduct" %>

<%@ Register Src="UserControl/uProductEdit.ascx" TagName="uProductEdit" TagPrefix="uc1" %>
<%@ Register Src="UserControl/uUnitEdit.ascx" TagName="uUnitEdit" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/uCommonDetailInfo.ascx" TagName="uCommonDetailInfo"
    TagPrefix="uc4" %>
<%@ Register Src="UserControl/uProductGroupEdit.ascx" TagName="uProductGroupEdit"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        function buttonCancelDevice_Click(s, e) {
        }
        function buttonSaveDevice_Click(s, e) {
        }

        //////////////////////////////////////////////////////// Product

        var sMode;

        function grdProduct_EndCallback(s, e) {
            if (s.cpProductEdit) {
                hProductEditId.Clear();

                if (s.cpProductEdit == 'edit') {
                    sMode = 'edit';
                    hProductEditId.Set("id", grdProduct.GetRowKey(grdProduct.GetFocusedRowIndex()));
                }
                else {
                    sMode = 'new';
                    hProductEditId.Set("id", '');
                }

                formProductEdit.Show();

                delete (s.cpProductEdit);
                return;
            }
        }

        function formProductEdit_Shown(s, e) {
            pcProduct.SetActiveTabIndex(0);
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerProduct');

            if (hProductEditId.Get("id") == '') {
                cpProductEdit.PerformCallback('new');
            }
            else {
                cpProductEdit.PerformCallback('edit');
                if (sMode == 'view') {
                }
            }

        }

        function grdProduct_CustomButtonClick(s, e) {
            sMode = 'view';
            hProductEditId.Set("id", grdProduct.GetRowKey(grdProduct.GetFocusedRowIndex()));
            formProductEdit.Show();

            //pcProduct.SetActiveTabIndex(0);
            //ASPxClientEdit.ClearEditorsInContainerById('lineContainerProduct');

        }

        function bProductEditSave_Click(s, e) {
            pcProduct.SetActiveTabIndex(0);

            if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainerPartner')) {
                e.processOnServer = false;
                return;
            }

            cpProductEdit.PerformCallback('save');
        }

        function bProductEditCancel_Click(s, e) {
            formProductEdit.Hide();
        }

        function bProductEditHelp_Click(s, e) {
        }

        function formProductEdit_CloseUp(s, e) {
            grdSalingProductCategory.CancelEdit();
            grdProductSupplier.CancelEdit();
            grdProductUnit.CancelEdit();
            grdActiveElement.CancelEdit();
        }

        function cpProductEdit_EndCallback(s, e) {
            if (s.cpRefresh) {
                formProductEdit.Hide();
                grdProduct.Refresh();

                delete (s.cpRefresh);
                return;
            }
        }

        function txtProductCode_Validation(s, e) {
            cpProductCode.PerformCallback("txtProductCode_Validation");
        }
    
        //////////////////////////////////////////////////////// ProductCategory

        function grdProductCategory_EndCallback(s, e) {

            if (s.cpEditProductGroup) {
                formProductGroupEdit.Show();

                pcProductGroup.SetActiveTabIndex(0);
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerProductGroup');

                if (s.cpEditProductGroup == 'edit') {
                    hEditProductGroupId.Set("id", grdProductCategory.GetRowKey(grdProductCategory.GetFocusedRowIndex()));
                    cpProductGroupEdit.PerformCallback('edit');
                }
                else {
                    cpProductGroupEdit.PerformCallback('new');
                }

                delete (s.cpEditProductGroup);
                return;
            }
        }

        function grdProductCategory_CustomButtomClick(s, e) {
            formProductGroupEdit.Show();
            pcProductGroup.SetActiveTabIndex(0);
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerProductGroup');
            pcProductGroup.SetActiveTabIndex(1);

            hEditProductGroupId.Set("id", grdProductCategory.GetRowKey(grdProductCategory.GetFocusedRowIndex()));
            cpProductGroupEdit.PerformCallback('edit');
        }

        function bProductGroupEditSave_Click(s, e) {
            pcProductGroup.SetActiveTabIndex(0);

            if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainerProductGroup')) {
                e.processOnServer = false;
                return;
            }

            cpProductGroupEdit.PerformCallback('save');
            grdProductCategory.Refresh();
        }
        function bProductGroupEditCancel_Click(s, e) {
            formProductGroupEdit.Hide();
        }
        function bProductGroupEditHelp_Click(s, e) {
        }

        //////////////////////////////////////////////////////// ProductUnit

        function grdDataUnit_EndCallback(s, e) {
            if (s.cpUnitEdit) {

                formUnitEdit.Show();
                grdUnit.ExpandAll();

                pcUnit.SetActiveTabIndex(0);
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnit');

                if (s.cpUnitEdit == 'edit') {
                    hUnitEditId.Set("id", grdUnit.GetRowKey(grdUnit.GetFocusedRowIndex()));
                    cpUnitEdit.PerformCallback('edit');
                }
                else {
                    cpUnitEdit.PerformCallback('new');
                }

                delete (s.cpUnitEdit);
                return;
            }

            if (s.cpRefresh) {
                grdUnit.Refresh();

                delete (s.cpRefresh);
                return;
            }
        }

        function grdUnit_CustomButtonClick(s, e) {
            formUnitEdit.Show();
            pcUnit.SetActiveTabIndex(0);

            ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnit');
            pcUnit.SetActiveTabIndex(1);

            hUnitEditId.Set("id", grdUnit.GetRowKey(grdUnit.GetFocusedRowIndex()));
            cpUnitEdit.PerformCallback('edit');
        }

        function bUnitEditSave_Click(s, e) {
            pcUnit.SetActiveTabIndex(0);

            if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainerUnit')) {
                e.processOnServer = false;
                return;
            }

            cpUnitEdit.PerformCallback('save');
            grdUnit.Refresh();
        }
        function bUnitEditCancel_Click(s, e) {
            formUnitEdit.Hide();
        }
        function bUnitEditHelp_Click(s, e) {
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top;">
                <div class="gridContainer">
                    <dx:aspxpagecontrol id="pHeaders" runat="server" activetabindex="0" rendermode="Lightweight"
                        width="100%" clientinstancename="pHeaders">
        <TabPages>
            <dx:TabPage Text="Hàng Hóa">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">    
                                    <dx:ASPxGridView ID="grdProduct" runat="server" AutoGenerateColumns="False" 
                                        ClientInstanceName="grdProduct" DataSourceID="ProductXDS" 
                                        KeyFieldName="ProductPropertyId" OnInitNewRow="grdProduct_InitNewRow" 
                                        OnRowDeleting="grdProduct_RowDeleting" 
                                        OnStartRowEditing="grdProduct_StartRowEditing" Width="100%">
                                        <ClientSideEvents CustomButtonClick="grdProduct_CustomButtonClick" 
                                            EndCallback="grdProduct_EndCallback" />
<ClientSideEvents CustomButtonClick="grdProduct_CustomButtonClick" EndCallback="grdProduct_EndCallback"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="Name" Name="Name" 
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="350px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="10px">
                                                <EditButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                    </Image>
                                                </EditButton>
                                                <NewButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                    </Image>
                                                </NewButton>
                                                <DeleteButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                    </Image>
                                                </DeleteButton>
                                                <clearfilterbutton visible="True">
                                                    <image>
                                                        <spriteproperties cssclass="Sprite_Clear" />
                                                    </image>
                                                </clearfilterbutton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="Code" Name="Code" 
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ProductId" FieldName="ProductId" 
                                                Name="ProductId" ShowInCustomizationForm="True" Visible="False" 
                                                VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" FieldName="ManufacturerName" 
                                                Name="ManufacturerName" ShowInCustomizationForm="True" VisibleIndex="4" 
                                                Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" 
                                                ShowInCustomizationForm="True" VisibleIndex="5" Width="60px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="showCommonDetail3">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Document" />
<SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" 
                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
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
                                            <dx:GridViewDataTextColumn FieldName="ProductPropertyId" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" 
                                            AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" 
                                            ColumnResizeMode="Control" ConfirmDelete="True" />

<SettingsBehavior AllowGroup="False" AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>

                                        <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                            ShowHeaderFilterButton="True" VerticalScrollableHeight="600" 
                                            VerticalScrollBarMode="Auto" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" VerticalScrollableHeight="600" VerticalScrollBarMode="Auto"></Settings>

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
            <dx:TabPage Text="Nhóm Hàng Hóa">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdProductCategory" runat="server" 
                            AutoGenerateColumns="False" ClientInstanceName="grdProductCategory" 
                            DataSourceID="ProductCategoryXDS" 
                            KeyFieldName="SalingProductCategoryPropertyId" 
                            OnInitNewRow="grdProductCategory_InitNewRow" 
                            OnRowDeleting="grdProductCategory_RowDeleting" 
                            OnStartRowEditing="grdProductCategory_StartRowEditing" Width="100%">
                            <ClientSideEvents EndCallback="grdProductCategory_EndCallback" 
                                CustomButtonClick="grdProductCategory_CustomButtomClick" />
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                            <SettingsEditing Mode="Inline" />
<ClientSideEvents CustomButtonClick="grdProductCategory_CustomButtomClick" EndCallback="grdProductCategory_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="SalingProductCategoryPropertyId" 
                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" 
                                    Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SalingProductCategoryId" 
                                    ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" 
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                    <propertiescombobox><Items><dx:ListEditItem Text="Sử dụng" Value="A"></dx:ListEditItem><dx:ListEditItem Text="Tạm ngưng" Value="I"></dx:ListEditItem></Items></propertiescombobox>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn FieldName="Language" ShowInCustomizationForm="True" 
                                    VisibleIndex="2" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" ShowInCustomizationForm="True" 
                                    VisibleIndex="3" Caption="Mã Nhóm Hàng Hóa" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" 
                                    VisibleIndex="4" Caption="Tên Nhóm Hàng Hóa">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True" 
                                    VisibleIndex="6" Caption="Mô Tả" Width="200px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                    ShowInCustomizationForm="True" VisibleIndex="9" 
                                    Width="100px">
                                    <EditButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                        <image>
                                            <spriteproperties cssclass="Sprite_Clear" />
                                        </image>
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn Caption="Chi Tiết" ShowInCustomizationForm="True" 
                                    VisibleIndex="7" Width="50px" ButtonType="Image">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="DetailGroup">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document" />
<SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                            </Columns>

<SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ConfirmDelete="True"></SettingsBehavior>

                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <Settings ShowFilterBar="Auto" ShowFilterRow="True" 
                                ShowHeaderFilterButton="True" ShowFilterRowMenu="True" 
                                VerticalScrollableHeight="600" VerticalScrollBarMode="Auto" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" VerticalScrollableHeight="600" ShowFilterBar="Auto" VerticalScrollBarMode="Auto"></Settings>

                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center">
                                </Header>
                                <CommandColumn Spacing="10px">
                                </CommandColumn>
                            </Styles>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Đơn Vị Tính">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdUnit" runat="server" AutoGenerateColumns="False" KeyFieldName="ProductUnitPropertyId"
                            OnRowDeleting="grdDataUnit_RowDeleting" OnStartRowEditing="grdDataUnit_StartRowEditing"
                                Width="100%" OnInitNewRow="grdDataUnit_InitNewRow" 
                            DataSourceID="ProductUnitXDS" ClientInstanceName="grdUnit">
                            <ClientSideEvents EndCallback="grdDataUnit_EndCallback" 
                                CustomButtonClick="grdUnit_CustomButtonClick"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tên Đơn Vị Tính" FieldName="Name" Name="Name"
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="350px">                              
                                </dx:GridViewDataTextColumn>                                      
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="100">
                                    <EditButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>  
                                    <clearfilterbutton visible="True">
                                        <image>
                                            <spriteproperties cssclass="Sprite_Clear" />
                                        </image>
                                    </clearfilterbutton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Đơn Vị Tính" FieldName="Code" Name="Code"
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ProductUnitId" FieldName="ProductUnitId" ShowInCustomizationForm="True"
                                    Visible="False" VisibleIndex="0" Name="ProductUnitId">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" Name="Description"
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">                                   
                                </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                        VisibleIndex="5" Width="60px">
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Document" />
<SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                </Image>
                                            </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="100px">
                                    <PropertiesComboBox>
                                        <Items><dx:ListEditItem Text="Sử dụng" Value="A" /><dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                        


</Items></PropertiesComboBox>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="ProductUnitPropertyId" 
                                    FieldName="ProductUnitPropertyId" ShowInCustomizationForm="True" 
                                    Visible="False" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                            </Columns>                        
<SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" 
                                ColumnResizeMode="Control" ConfirmDelete="True" AllowFocusedRow="True"></SettingsBehavior>
                            <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                                ShowFilterRowMenu="True" VerticalScrollableHeight="600" 
                                VerticalScrollBarMode="Auto" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" VerticalScrollableHeight="600" VerticalScrollBarMode="Auto"></Settings>

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
        </TabPages>
        <TabStyle Cursor="pointer">
        </TabStyle>
    </dx:aspxpagecontrol>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <dx:xpodatasource id="ProductUnitXDS" runat="server" typename="DAL.Purchasing.ViewProductUnit">
                </dx:xpodatasource>
                <dx:xpodatasource id="ProductCategoryXDS" runat="server" typename="DAL.Purchasing.ViewSalingProductCategory">
                </dx:xpodatasource>
                <dx:xpodatasource id="ProductXDS" runat="server" typename="DAL.Purchasing.ViewProduct">
                </dx:xpodatasource>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:uProductGroupEdit ID="uProductGroupEdit1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc3:uUnitEdit ID="uUnitEdit1" runat="server" />
                <uc1:uProductEdit ID="uProductEdit1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
