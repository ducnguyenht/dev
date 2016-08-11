<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Material.aspx.cs" Inherits="WebModule.GUI.Sales.Material" %>

<%@ Register Src="~/Purchasing/UserControl/uBuyingMaterial.ascx" TagName="uBuyingMaterial"
    TagPrefix="uc1" %>
<%@ Register Src="~/Purchasing/UserControl/uBuyingMaterialCategory.ascx" TagName="uBuyingMaterialCategory"
    TagPrefix="uc1" %>
<%@ Register Src="~/Purchasing/UserControl/uBuyingMaterialUnitEdit.ascx" TagName="uBuyingMaterialUnitEdit"
    TagPrefix="uc2" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        //Material
        // MainForm Event
        function grdDataMaterial_EndCallback(s, e) {
            if (s.cpEditMaterial) {
                formMaterialEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterial');
                cboRowStatusMaterial.SetValue('A');
                if (s.cpEditMaterial == 'edit') {
                    cpLineMaterial.PerformCallback('edit');
                }
                else {
                    cpLineMaterial.PerformCallback('new');
                }
                delete (s.cpEditMaterial);
                return;
            }
        }

        function grdDataMaterial_CustomButtonClick(s, e) {
            //s.GetRowValues(e.visibleIndex, 'code', OnGetRowValuesMaterial);
            if (e.buttonID == 'showCommonDetail1')
                popupCommonDetailInfo.Show();
        }

        function OnGetRowValuesMaterial(values) {
            formMaterialEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterial');

            var str = 'view|' + values;
            cpLineMaterial.PerformCallback(str);
        }

        // EditForm Event 
        function buttonSaveMaterial_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCodeMaterial').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCodeMaterial').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtNameMaterial').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtNameMaterial').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturerMaterial').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('cboManufacturerMaterial').Validate();
                return;
            }

            cpLineMaterial.PerformCallback('save');
            cpHeaderMaterial.PerformCallback('refresh');

        }

        function buttonCancelMaterial_Click(s, e) {
            formMaterialEdit.Hide();
        }

        //Material

        //MaterialCategory
        function grdDataMaterialCategory_EndCallback(s, e) {
            if (s.cpEditMaterialCategory) {
                formMaterialCategoryEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialCategory');
                cboRowStatusMaterialCategory.SetValue('A');
                if (s.cpEditMaterialCategory == 'edit') {
                    cpLineMaterialCategory.PerformCallback('edit');
                }
                else {
                    cpLineMaterialCategory.PerformCallback('new');
                }
                delete (s.cpEditMaterialCategory);
                return;
            }
        }

        function cpHeaderMaterialCategory_EndCallback(s, e) {
        }

        function cpLineMaterialCategory_EndCallback(s, e) {
            if (s.cpCodeExisting) {

            }

        }

        function buttonSaveMaterialCategory_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCodeMaterialCategory').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCodeMaterialCategory').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtNameMaterialCategory').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtNameMaterialCategory').Validate();
                return;
            }

            //cpLine.PerformCallback('validate');
            cpLineMaterialCategory.PerformCallback('save');
            cpHeaderMaterialCategory.PerformCallback('refresh');

        }

        function buttonCancelMaterialCategory_Click(s, e) {
            formMaterialCategoryEdit.Hide();
        }

        function grdDataMaterialCategory_CustomButtonClick(s, e) {
            //s.GetRowValues(e.visibleIndex, 'code', OnGetRowValuesMaterialCategory);
            if (e.buttonID == 'showCommonDetail2')
                popupCommonDetailInfo.Show();
        }

        function OnGetRowValuesMaterialCategory(values) {
            formMaterialCategoryEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialCategory');
            var str = 'view|' + values;
            cpLineMaterialCategory.PerformCallback(str);
        }
        //MaterialCategory

        //MaterialUnit
        function grdDataMaterialUnit_EndCallback(s, e) {
            if (s.cpEditMaterialUnit) {
                formMaterialUnitEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialUnit');
                cboRowStatusMaterialUnit.SetValue('A');
                if (s.cpEditMaterialUnit == 'edit') {
                    cpLineMaterialUnit.PerformCallback('edit');
                }
                else {
                    cpLineMaterialUnit.PerformCallback('new');
                }

                delete (s.cpEditMaterialUnit);
                return;
            }
        }

        function cpHeaderMaterialUnit_EndCallback(s, e) {
        }

        function cpLineMaterialUnit_EndCallback(s, e) {
            if (s.cpCodeExisting) {
            }

        }

        function buttonSaveMaterialUnit_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCodeMaterialUnit').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCodeMaterialUnit').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtNameMaterialUnit').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtNameMaterialUnit').Validate();
                return;
            }

            //cpLine.PerformCallback('validate');
            cpLineMaterialUnit.PerformCallback('save');
            cpHeaderMaterialUnit.PerformCallback('refresh');

        }

        function buttonCancelMaterialUnit_Click(s, e) {
            formMaterialUnitEdit.Hide();
        }

        function grdDataMaterialUnit_CustomButtonClick(s, e) {
            //s.GetRowValues(e.visibleIndex, 'code', OnGetRowValuesMaterialUnit);
            if (e.buttonID == 'showCommonDetail3')
                popupCommonDetailInfo.Show();
        }

        function OnGetRowValuesMaterialUnit(values) {
            formMaterialUnitEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialUnit');
            var str = 'view|' + values;
            cpLineMaterialUnit.PerformCallback(str);
        }
        //MaterialUnit
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<table style="width:100%;">
<tr>
    <td style="vertical-align:top;">
            <div class="gridContainer">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
                    Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Nguyên Vật Liệu">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">                                 
                                    <dx:ASPxCallbackPanel ID="cpHeaderMaterial" runat="server" Width="100%" ClientInstanceName="cpHeaderMaterial"
                                        OnCallback="cpHeaderMaterial_Callback" HideContentOnCallback="True" >
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdDataMaterial" runat="server" AutoGenerateColumns="False"
                                                    OnRowDeleting="grdDataMaterial_RowDeleting" OnStartRowEditing="grdDataMaterial_StartRowEditing"
                                                     Width="100%" OnInitNewRow="grdDataMaterial_InitNewRow" KeyFieldName="code">
                                                    <ClientSideEvents EndCallback="grdDataMaterial_EndCallback" CustomButtonClick="grdDataMaterial_CustomButtonClick" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã nguyên vật liệu" FieldName="code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên nguyên vật liệu" FieldName="name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manuname" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="100px">
                                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Width="60px">
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
                                                            VisibleIndex="6" Width="100px">
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
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
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
                        <dx:TabPage Text="Nhóm Nguyên Vật Liệu">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">                                  
                                    <dx:ASPxCallbackPanel ID="cpHeaderMaterialCategory" runat="server" Width="100%" ClientInstanceName="cpHeaderMaterialCategory"
                                        OnCallback="cpHeaderMaterialCategory_Callback" HideContentOnCallback="True" >
                                        <ClientSideEvents EndCallback="cpHeaderMaterialCategory_EndCallback" />
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdDataMaterialCategory" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="code" OnRowDeleting="grdDataMaterialCategory_RowDeleting" OnStartRowEditing="grdDataMaterialCategory_StartRowEditing"
                                                     Width="100%" OnInitNewRow="grdDataMaterialCategory_InitNewRow">
                                                    <ClientSideEvents EndCallback="grdDataMaterialCategory_EndCallback" CustomButtonClick="grdDataMaterialCategory_CustomButtonClick" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã Nhóm NVL" FieldName="code" Name="Code"
                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm NVL" FieldName="name" Name="Name"
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
                        <dx:TabPage Text="Đơn Vị Tính Nguyên Vật Liệu">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">                                    
                                    <dx:ASPxCallbackPanel ID="cpHeaderMaterialUnit" runat="server" Width="100%" ClientInstanceName="cpHeaderMaterialUnit"
                                        OnCallback="cpHeaderMaterialUnit_Callback" HideContentOnCallback="True" >
                                        <ClientSideEvents EndCallback="cpHeaderMaterialUnit_EndCallback" />
                                        <ClientSideEvents EndCallback="cpHeaderMaterialUnit_EndCallback"></ClientSideEvents>
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdDataMaterialUnit" runat="server" AutoGenerateColumns="False"
                                                    OnRowDeleting="grdDataMaterialUnit_RowDeleting" OnStartRowEditing="grdDataMaterialUnit_StartRowEditing"
                                                    OnInitNewRow="grdDataMaterialUnit_InitNewRow"  Width="100%" KeyFieldName="code">
                                                    <ClientSideEvents EndCallback="grdDataMaterialUnit_EndCallback" CustomButtonClick="grdDataMaterialUnit_CustomButtonClick" />
                                                    <ClientSideEvents CustomButtonClick="grdDataMaterialUnit_CustomButtonClick" EndCallback="grdDataMaterialUnit_EndCallback">
                                                    </ClientSideEvents>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã ĐVT" FieldName="code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="100px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên ĐVT" FieldName="name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="100px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="60px">
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
                                                        ConfirmDelete="True" ColumnResizeMode="NextColumn"></SettingsBehavior>                                                    
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>
                                                    <SettingsPager PageSize="22" ShowEmptyDataRows="True"></SettingsPager>
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
                <uc1:uBuyingMaterial ID="uBuyingMaterial" runat="server" />
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:uBuyingMaterialCategory ID="uBuyingMaterialCategory" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <uc2:uBuyingMaterialUnitEdit ID="uBuyingMaterialUnitEdit" runat="server" />
        </td>
    </tr>
</table>
<uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
   
</asp:Content>
