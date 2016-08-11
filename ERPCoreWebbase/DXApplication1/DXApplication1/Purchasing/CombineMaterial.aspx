<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CombineMaterial.aspx.cs" Inherits="WebModule.Purchasing.CombineMaterial" %>
<%@ Register Src="UserControl/uBuyingMaterial.ascx" TagName="uBuyingMaterial" TagPrefix="uc1" %>
<%@ Register Src="UserControl/uBuyingMaterialCategory.ascx" TagName="uBuyingMaterialCategory"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl/uBuyingMaterialUnitEdit.ascx" TagName="uBuyingMaterialUnitEdit"
    TagPrefix="uc3" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        /*********START MATERIAL CATEGORY**********/
        function grdDataMaterial_CustomButtonClick(s, e) {
            LoadingPanelCombineMaterial.Show();
            if (e.buttonID == 'showCommonDetail1')
                popupCommonDetailInfo.Show();
            else if (e.buttonID == 'AddMaterial') {
                cpLineMaterial.PerformCallback('AddMaterial');
            }
            else if (e.buttonID == 'EditMaterial') {
                s.GetRowValues(e.visibleIndex, 'MaterialId', UpdateMaterialAction);
            }
            else if (e.buttonID == 'DeleteMaterial') {
                s.GetRowValues(e.visibleIndex, 'MaterialId', DeleteMaterialAction);
            }
        }

        function UpdateMaterialAction(values) {
            var params = new Array('EditMaterial', values);
            if (cpLineMaterial.InCallback()) {
                LoadingPanelCombineMaterial.Hide();
                console.log("Server's busy");
            }
            else {
                cpLineMaterial.PerformCallback(params);
            }
        }

        function DeleteMaterialAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                var params = new Array('DeleteMaterial', values);
                cpLineMaterial.PerformCallback(params);
            } else
                LoadingPanelCombineMaterial.Hide();
        }

        function btnAddMaterialclick(s, e) {
            cpLineMaterial.PerformCallback('AddMaterial');
        }
        /*********END MATERIAL CATEGORY**********/
        /*********START MATERIAL CATEGORY**********/
        function grdDataMaterialCategory_CustomButtonClick(s, e) {
            LoadingPanelCombineMaterial.Show();
            if (e.buttonID == 'showCommonDetail2')
                popupCommonDetailInfo.Show();
            else if (e.buttonID == 'AddCategory') {
                cpLineMaterialCategory.PerformCallback('AddCategory');
            }
            else if (e.buttonID == 'EditCategory') {
                s.GetRowValues(e.visibleIndex, 'BuyingMaterialCategoryId', UpdateCategoryAction);
            }
            else if (e.buttonID == 'DeleteCategory') {
                s.GetRowValues(e.visibleIndex, 'BuyingMaterialCategoryId', DeleteCategoryAction);
            }
        }

        function UpdateCategoryAction(values) {
            var params = new Array('EditCategory', values);
            cpLineMaterialCategory.PerformCallback(params);
        }

        function DeleteCategoryAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                var params = new Array('DeleteCategory', values);
                cpLineMaterialCategory.PerformCallback(params);
            } else
                LoadingPanelCombineMaterial.Hide();
        }

        function btnAddCategoryclick(s, e) {
            cpLineMaterialCategory.PerformCallback('AddCategory');
        }
        /*********END MATERIAL CATEGORY**********/

        /*********START MATERIAL UNIT**********/
        function grdDataMaterialUnit_CustomButtonClick(s, e) {
            LoadingPanelCombineMaterial.Show();
            if (e.buttonID == 'showCommonDetail3')
                popupCommonDetailInfo.Show();
            else if (e.buttonID == 'AddUnit') {
                cpLineMaterialUnit.PerformCallback('AddUnit');
            }
            else if (e.buttonID == 'EditUnit') {
                s.GetRowValues(e.visibleIndex, 'MaterialUnitId', UpdateUnitAction);
            }
            else if (e.buttonID == 'DeleteUnit') {
                s.GetRowValues(e.visibleIndex, 'MaterialUnitId', DeleteUnitAction);
            }
        }

        function UpdateUnitAction(values) {
            var params = new Array('EditUnit', values);
            cpLineMaterialUnit.PerformCallback(params);
        }

        function DeleteUnitAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                var params = new Array('DeleteUnit', values);
                cpLineMaterialUnit.PerformCallback(params);
            } else
                LoadingPanelCombineMaterial.Hide();
        }

        function btnAddUnitclick(s, e) {
            cpLineMaterialUnit.PerformCallback('AddUnit');
        }
        /*********END MATERIAL UNIT**********/
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div>
    <table id="CombineMaterialZone" style="width: 100%">
        <tr>
            <td style="vertical-align: top">
                <dx:ASPxPageControl ID="pcMaterialMenu" ClientInstanceName="pcMaterialMenu" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
                    Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Nguyên Vật Liệu">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataMaterial" ClientInstanceName="grdDataMaterial" KeyFieldName="MaterialId" runat="server" AutoGenerateColumns="False"
                                        Width="100%">
                                        <ClientSideEvents CustomButtonClick="grdDataMaterial_CustomButtonClick">
                                        </ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã nguyên vật liệu" FieldName="Code" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên nguyên vật liệu" FieldName="Name" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="100px">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        
                                                <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        
                                                <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    
                                                </Items>
                                                
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="ManufacturerName" ShowInCustomizationForm="True"
                                                VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Chi Tiết" ButtonType="Image" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Width="60px">
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
                                                <ClearFilterButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                    </Image>
			                                    </ClearFilterButton>
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="EditMaterial">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="AddMaterial">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="DeleteMaterial">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="MaterialId" FieldName="MaterialId" ShowInCustomizationForm="True"
                                                Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ManufacturerId" FieldName="ManufacturerId" ShowInCustomizationForm="True"
                                                Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Description" FieldName="Description" ShowInCustomizationForm="True"
                                                Visible="false">
                                            </dx:GridViewDataTextColumn>                                            
                                        </Columns>
                                        <Templates>
                                            <EmptyDataRow>
                                                <dx:ASPxButton ID="btnAddCategory" runat="server" AutoPostBack="false">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                        </Image>
                                                        <ClientSideEvents Click="btnAddMaterialclick" />
                                                </dx:ASPxButton>
                                            </EmptyDataRow>
                                        </Templates>
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
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
                        <dx:TabPage Text="Nhóm Nguyên Vật Liệu">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataMaterialCategory" ClientInstanceName="grdDataMaterialCategory" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="BuyingMaterialCategoryId" Width="100%">
                                        <ClientSideEvents CustomButtonClick="grdDataMaterialCategory_CustomButtonClick">
                                        </ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã  Nhóm NVL" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm NVL" FieldName="Name" Name="Name" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="100px">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        
<dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        
<dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    
</Items>
                                                
</PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="60px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="showCommonDetail2">
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
                                                    <dx:GridViewCommandColumnCustomButton ID="EditCategory">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="AddCategory">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="DeleteCategory">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="Description" Name="Description"
                                                ShowInCustomizationForm="True" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="BuyingMaterialCategoryId" Name="BuyingMaterialCategoryId"
                                                ShowInCustomizationForm="True" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <EmptyDataRow>
                                                <dx:ASPxButton ID="btnAddCategory" runat="server" AutoPostBack="false">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                    </Image>
                                                    <ClientSideEvents Click="btnAddCategoryclick" />
                                                </dx:ASPxButton>
                                            </EmptyDataRow>
                                        </Templates>
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ConfirmDelete="True" ColumnResizeMode="Control" />
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
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
                        <dx:TabPage Text="Đơn Vị Tính">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataMaterialUnit" ClientInstanceName="grdDataMaterialUnit" runat="server" 
                                        KeyFieldName="MaterialUnitId"
                                        AutoGenerateColumns="False" Width="100%">
                                        <ClientSideEvents CustomButtonClick="grdDataMaterialUnit_CustomButtonClick">
                                        </ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã ĐVT" FieldName="Code" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên ĐVT" FieldName="Name" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="100px">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        
<dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        
<dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    
</Items>
                                                
</PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="60px">
                                                <ClearFilterButton Visible="True">
                                                </ClearFilterButton>
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
                                                    <dx:GridViewCommandColumnCustomButton ID="EditUnit">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="AddUnit">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="DeleteUnit">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
				                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="MaterialUnitId" FieldName="MaterialUnitId" ShowInCustomizationForm="True"
                                                VisibleIndex="5"  Visible="false">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Templates>
                                            <EmptyDataRow>
                                                <dx:ASPxButton ID="btnAddUnit" runat="server" AutoPostBack="false">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                    </Image>
                                                    <ClientSideEvents Click="btnAddUnitclick" />
                                                </dx:ASPxButton>
                                            </EmptyDataRow>
                                        </Templates>
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ConfirmDelete="True" ColumnResizeMode="Control" />
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
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
                </dx:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:uBuyingMaterial ID="uBuyingMaterial1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc3:uBuyingMaterialUnitEdit ID="uBuyingMaterialUnitEdit1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc2:uBuyingMaterialCategory ID="uBuyingMaterialCategory1" runat="server" />
            </td>
        </tr>
    </table>
    <uc4:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
    
    </div>
    <dx:ASPxLoadingPanel ID="LoadingPanelCombineMaterial" runat="server" ClientInstanceName="LoadingPanelCombineMaterial"
        Modal="True" ShowImage="false">
        <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
    </dx:ASPxLoadingPanel>
</asp:Content>
