<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CombineProduct.aspx.cs" Inherits="WebModule.ImExporting.CombineProduct" %>
<%@ Register src="UserControl/uUnitEdit.ascx" tagname="uUnitEdit" tagprefix="uc3" %>
<%@ Register src="UserControl/uProductGroupEdit.ascx" tagname="uProductGroupEdit" tagprefix="uc1" %>
<%@ Register src="UserControl/uProductEdit.ascx" tagname="uProductEdit" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    // MainForm Event
    function grdDataProduct_EndCallback(s, e) {
        if (s.cpEditProduct) {
            formProductEdit.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainerProduct');
            cboRowStatusProduct.SetValue('A');

            if (s.cpEditProduct == 'edit') {
                cpLineProduct.PerformCallback('edit');
            }
            else {
                cpLineProduct.PerformCallback('new');
            }

            delete (s.cpEditProduct);
            return;
        }
    }

    function grdDataProduct_CustomButtonClick(s, e) {
        //s.GetRowValues(e.visibleIndex, 'ProductId', OnGetRowValuesProduct);
    }

    function OnGetRowValuesProduct(values) {
        /*
        formProductEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
        */
    }

    // EditForm Event 
    function buttonSaveProduct_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCodeProduct').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCodeProduct').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtNameProduct').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtNameProduct').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturerProduct').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('cboManufacturerProduct').Validate();
            return;
        }

        cpLineProduct.PerformCallback('save');
        cpHeaderProduct.PerformCallback('refresh');

    }

    function buttonCancelProduct_Click(s, e) {
        formProductEdit.Hide();
    }




    function grdDataProductGroup_EndCallback(s, e) {
        if (s.cpEditProductGroup) {
            formProductGroupEdit.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainerProductGroup');
            cboRowStatusProductGroup.SetValue('A');

            if (s.cpEditProductGroup == 'edit') {
                cpLineProductGroup.PerformCallback('edit');
            }
            else {
                cpLineProductGroup.PerformCallback('new');
            }

            delete (s.cpEditProductGroup);
            return;
        }
    }

    function cpHeaderProductGroup_EndCallback(s, e) {
    }

    function cpLineProductGroup_EndCallback(s, e) {
        if (s.cpCodeExisting) {

        }

    }

    function buttonSaveProductGroup_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCodeProductGroup').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCodeProductGroup').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtNameProductGroup').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtNameProductGroup').Validate();
            return;
        }

        //cpLine.PerformCallback('validate');

        cpLineProductGroup.PerformCallback('save');

        cpHeaderProductGroup.PerformCallback('refresh');

    }

    function buttonCancelProductGroup_Click(s, e) {
        formProductGroupEdit.Hide();
    }

    function grdDataProductGroup_CustomButtonClick(s, e) {
        //            s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesProductGroup);
    }

    function OnGetRowValuesProductGroup(values) {
        //            formProductGroupEdit.Show();
        //            ASPxClientEdit.ClearEditorsInContainerById('lineContainerProductGroup');

        //            var str = 'view|' + values;
        //            cpLineProductGroup.PerformCallback(str);
    }




    function grdDataUnit_EndCallback(s, e) {
        if (s.cpEditUnit) {
            formUnitEdit.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnit');
            cboRowStatusUnit.SetValue('A');

            if (s.cpEditUnit == 'edit') {
                cpLineUnit.PerformCallback('edit');
            }
            else {
                cpLineUnit.PerformCallback('new');
            }

            delete (s.cpEditUnit);
            return;
        }
    }

    function cpHeaderUnit_EndCallback(s, e) {
    }

    function cpLineUnit_EndCallback(s, e) {
        if (s.cpCodeExisting) {

        }

    }

    function buttonSaveUnit_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCodeUnit').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCodeUnit').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtNameUnit').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtNameUnit').Validate();
            return;
        }

        //cpLine.PerformCallback('validate');

        cpLineUnit.PerformCallback('save');

        cpHeaderUnit.PerformCallback('refresh');

    }

    function buttonCancelUnit_Click(s, e) {
        formUnitEdit.Hide();
    }

    function grdDataUnit_CustomButtonClick(s, e) {
        // s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesUnit);
    }

    function OnGetRowValuesUnit(values) {
        //            formUnitEdit.Show();
        //            ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnit');

        //            var str = 'view|' + values;
        //            cpLineUnit.PerformCallback(str);
    }
    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
 <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" 
        RenderMode="Lightweight" ClientIDMode="AutoID">
        <TabPages>
            <dx:TabPage Text="Danh Mục Hàng Hóa">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderProduct" runat="server" Text="Danh mục Hàng Hóa" Font-Bold="True"
                                Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderProduct" runat="server" Width="100%" ClientInstanceName="cpHeaderProduct"
                            HideContentOnCallback="True" >
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataProduct" runat="server" AutoGenerateColumns="False" KeyFieldName="Code"
                                        OnRowDeleting="grdDataProduct_RowDeleting" OnStartRowEditing="grdDataProduct_StartRowEditing"
                                         Width="100%" OnInitNewRow="grdDataProduct_InitNewRow" 
                                        ClientIDMode="AutoID">
                                        <ClientSideEvents EndCallback="grdDataProduct_EndCallback" CustomButtonClick="grdDataProduct_CustomButtonClick" />
                                        <ClientSideEvents CustomButtonClick="grdDataProduct_CustomButtonClick" EndCallback="grdDataProduct_EndCallback">
                                        </ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Hàng Hóa" FieldName="Name" Name="Name" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="300px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="7"
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
                                            <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                                VisibleIndex="1" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ProductId" FieldName="ProductId" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="0" Name="ProductId">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" FieldName="ManufacturerName" Name="ManufacturerName"
                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                VisibleIndex="6" Width="20px">
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
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
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
                                            ConfirmDelete="True"></SettingsBehavior>
                                        <SettingsPager PageSize="50">
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
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Nhóm Hàng Hóa">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderProductGroup" runat="server" Text="Danh mục Nhóm Hàng Hóa"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderProductGroup" runat="server" Width="100%" ClientInstanceName="cpHeaderProductGroup"
                            HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderProductGroup_EndCallback" />
<ClientSideEvents EndCallback="cpHeaderProductGroup_EndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataProductGroup" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="SupplierId" OnRowDeleting="grdDataProductGroup_RowDeleting" OnStartRowEditing="grdDataProductGroup_StartRowEditing"
                                         Width="100%" OnInitNewRow="grdDataProductGroup_InitNewRow" 
                                        ClientIDMode="AutoID">
                                        <ClientSideEvents EndCallback="grdDataProductGroup_EndCallback" CustomButtonClick="grdDataProductGroup_CustomButtonClick" />
<ClientSideEvents CustomButtonClick="grdDataProductGroup_CustomButtonClick" EndCallback="grdDataProductGroup_EndCallback"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Hàng Hóa" FieldName="Name" Name="Name"
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
                                            <dx:GridViewDataTextColumn Caption="Mã  Nhóm Hàng Hóa" FieldName="Code" Name="Code"
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="0" Name="SupplierId">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" Name="Description"
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
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
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

<SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ConfirmDelete="True"></SettingsBehavior>

                                        <SettingsPager PageSize="50">
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
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Đơn Vị Tính">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderUnit" runat="server" Text="Danh mục Đơn Vị Tính Hàng Hóa"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderUnit" runat="server" Width="100%" 
                            ClientInstanceName="cpHeaderUnit" HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderUnit_EndCallback" />
<ClientSideEvents EndCallback="cpHeaderUnit_EndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataUnit" runat="server" AutoGenerateColumns="False" KeyFieldName="SupplierId"
                                        OnRowDeleting="grdDataUnit_RowDeleting" OnStartRowEditing="grdDataUnit_StartRowEditing"
                                         Width="100%" OnInitNewRow="grdDataUnit_InitNewRow" 
                                        ClientIDMode="AutoID">
                                        <ClientSideEvents EndCallback="grdDataUnit_EndCallback" CustomButtonClick="grdDataUnit_CustomButtonClick" />
<ClientSideEvents CustomButtonClick="grdDataUnit_CustomButtonClick" EndCallback="grdDataUnit_EndCallback"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Đơn Vị Tính" FieldName="Name" Name="Name"
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
                                            <dx:GridViewDataTextColumn Caption="Mã  Đơn Vị Tính" FieldName="Code" Name="Code"
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ManufacturerId" FieldName="SupplierId" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="0" Name="SupplierId">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" Name="Description"
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
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
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

<SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ConfirmDelete="True"></SettingsBehavior>

                                        <SettingsPager PageSize="50">
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
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <table>
    <tr>
        <td>
        
       
            <uc3:uUnitEdit ID="uUnitEdit1" runat="server" />
        
            <uc2:uProductEdit ID="uProductEdit1" runat="server" />
        
            <uc1:uProductGroupEdit ID="uProductGroupEdit1" runat="server" />
        
        </td>
    </tr>
    </table>
    </asp:Content>
