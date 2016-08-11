<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CombineMaterial.aspx.cs" Inherits="WebModule.ImExporting.CombineMaterial" %>
<%@ Register src="UserControl/uBuyingMaterialUnitEdit.ascx" tagname="uBuyingMaterialUnitEdit" tagprefix="uc3" %><%@ Register src="UserControl/uBuyingMaterialCategory.ascx" tagname="uBuyingMaterialCategory" tagprefix="uc2" %><%@ Register src="UserControl/uBuyingMaterial.ascx" tagname="uBuyingMaterial" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    //Material
    // MainForm Event
    function grdDataMaterial_EndCallback(s, e) {
        if (s.cpEditMaterial) {
            formMaterialEdit.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterial');
            //cboRowStatusMaterial.SetValue('A');

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
        s.GetRowValues(e.visibleIndex, 'ProductId', OnGetRowValuesMaterial);
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
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesMaterialCategory);
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
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesMaterialUnit);
    }

    function OnGetRowValuesMaterialUnit(values) {
        formMaterialUnitEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialUnit');

        var str = 'view|' + values;
        cpLineMaterialUnit.PerformCallback(str);
    }

    function buttonCancelDevice_Click(s, e) {
    }

    function buttonSaveDevice_Click(s, e) {
    }

    //MaterialUnit
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uBuyingMaterial ID="uBuyingMaterial1" runat="server" />
    <uc3:uBuyingMaterialUnitEdit ID="uBuyingMaterialUnitEdit1" runat="server" />
    <uc2:uBuyingMaterialCategory ID="uBuyingMaterialCategory1" runat="server" />
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        RenderMode="Lightweight" ClientIDMode="AutoID" Width="100%">
        <TabPages>
            <dx:TabPage Text="Danh Mục Nguyên Vật Liệu">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderMaterial" runat="server" Text="Danh mục nguyên vật liệu"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderMaterial" runat="server" Width="100%" ClientInstanceName="cpHeaderMaterial"
                            OnCallback="cpHeaderMaterial_Callback" HideContentOnCallback="True" >
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataMaterial" runat="server" AutoGenerateColumns="False"
                                        OnRowDeleting="grdDataMaterial_RowDeleting" OnStartRowEditing="grdDataMaterial_StartRowEditing"
                                         Width="100%" OnInitNewRow="grdDataMaterial_InitNewRow">
                                        <ClientSideEvents EndCallback="grdDataMaterial_EndCallback" CustomButtonClick="grdDataMaterial_CustomButtonClick" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                VisibleIndex="6">
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
                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" ShowInCustomizationForm="True"
                                                VisibleIndex="5">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" ShowInCustomizationForm="True"
                                                VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manuname" ShowInCustomizationForm="True"
                                                VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên nguyên vật liệu" FieldName="name" ShowInCustomizationForm="True"
                                                VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã nguyên vật liệu" FieldName="code" ShowInCustomizationForm="True"
                                                VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ConfirmDelete="True" />
                                        <SettingsPager PageSize="50">
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
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderMaterialCategory" runat="server" Text="Danh mục Nhóm Nguyên Vật Liệu"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderMaterialCategory" runat="server" Width="100%" ClientInstanceName="cpHeaderMaterialCategory"
                            OnCallback="cpHeaderMaterialCategory_Callback" HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderMaterialCategory_EndCallback" />
                            <ClientSideEvents EndCallback="cpHeaderMaterialCategory_EndCallback">
                            </ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataMaterialCategory" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="SupplierId" OnRowDeleting="grdDataMaterialCategory_RowDeleting"
                                        OnStartRowEditing="grdDataMaterialCategory_StartRowEditing"  Width="100%"
                                        OnInitNewRow="grdDataMaterialCategory_InitNewRow">
                                        <ClientSideEvents EndCallback="grdDataMaterialCategory_EndCallback" CustomButtonClick="grdDataMaterialCategory_CustomButtonClick" />
                                        <ClientSideEvents CustomButtonClick="grdDataMaterialCategory_CustomButtonClick" EndCallback="grdDataMaterialCategory_EndCallback">
                                        </ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Nguyên Vật Liệu" FieldName="name" Name="Name"
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="300px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="6"
                                                Width="100px" ButtonType="Image">
                                                <EditButton Visible="True">
    <Image ToolTip="Sửa">
        <SpriteProperties CssClass="Sprite_Edit" />
    </Image>
</EditButton>
<NewButton Visible="True">
    <Image ToolTip="Thêm">
        <SpriteProperties CssClass="Sprite_New" />
    </Image>
</NewButton>
<DeleteButton Visible="True">
    <Image ToolTip="Xóa">
        <SpriteProperties CssClass="Sprite_Delete" />
    </Image>
</DeleteButton>
<ClearFilterButton Visible="True">
    <Image ToolTip="Hủy">
        <SpriteProperties CssClass="Sprite_Clear" />
    </Image>
</ClearFilterButton>
<UpdateButton>
    <Image ToolTip="Cập nhật">
        <SpriteProperties CssClass="Sprite_Apply" />
    </Image>
</UpdateButton>
<CancelButton>
    <Image ToolTip="Bỏ qua">
        <SpriteProperties CssClass="Sprite_Cancel" />
    </Image>
</CancelButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã  Nhóm Nguyên Vật Liệu" FieldName="code" Name="Code"
                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" Name="Description"
                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                VisibleIndex="5" Width="20px">
                                                <ClearFilterButton Visible="True">
                                                </ClearFilterButton>
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton>
                                                        <Image ToolTip="Chi tiết">
    <SpriteProperties CssClass="Sprite_Document" />
</Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="rowstatus" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="50px">
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
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ConfirmDelete="True">
                                        </SettingsBehavior>
                                        <SettingsPager PageSize="50">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                        <SettingsEditing Mode="Inline">
                                        </SettingsEditing>
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True">
                                        </Settings>
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
            <dx:TabPage Text="Đơn Vị Tính">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderMaterialUnit" runat="server" Text="Danh mục đơn vị tính nguyên vật liệu"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderMaterialUnit" runat="server" Width="100%" ClientInstanceName="cpHeaderMaterialUnit"
                            OnCallback="cpHeaderMaterialUnit_Callback" HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderMaterialUnit_EndCallback" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataMaterialUnit" runat="server" AutoGenerateColumns="False"
                                        OnRowDeleting="grdDataMaterialUnit_RowDeleting" OnStartRowEditing="grdDataMaterialUnit_StartRowEditing"
                                        OnInitNewRow="grdDataMaterialUnit_InitNewRow"  Width="100%">
                                        <ClientSideEvents EndCallback="grdDataMaterialUnit_EndCallback" CustomButtonClick="grdDataMaterialUnit_CustomButtonClick" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã" FieldName="code" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="100px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Width="120px">
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
                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="400px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="50px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                VisibleIndex="1" Width="300px">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                            ConfirmDelete="True" />
                                        <SettingsPager PageSize="50">
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
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
