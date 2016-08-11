<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CombineManufacturer.aspx.cs" Inherits="WebModule.ImExporting.CombineManufacturer" %>
<%@ Register src="~/ImExporting/UserControl/uMaufacturerGroupEdit.ascx" tagname="uMaufacturerGroupEdit" tagprefix="uc2" %>
<%@ Register src="~/ImExporting/UserControl/uManufacturerEdit.ascx" tagname="uManufacturerEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">

       //Manufacturer
       function grdDataManufacturer_EndCallback(s, e) {
           if (s.cpEditManufacturer) {
               formManufacturerEdit.Show();

               ASPxClientEdit.ClearEditorsInContainerById('lineContainerManufacturer');
               cboRowStatusManufacturer.SetValue('A');

               if (s.cpEditManufacturer == 'edit') {
                   cpLineManufacturer.PerformCallback('edit');
               }
               else {
                   cpLineManufacturer.PerformCallback('new');
               }

               delete (s.cpEditManufacturer);
               return;
           }
       }

       function cpHeaderManufacturer_EndCallback(s, e) {
       }

       function cpLineManufacturer_EndCallback(s, e) {
           if (s.cpCodeExisting) {

           }

       }

       function buttonSaveManufacturer_Click(s, e) {
           if (ASPxClientControl.GetControlCollection().GetByName('txtCodeManufacturer').GetValue() == null) {
               ASPxClientControl.GetControlCollection().GetByName('txtCodeManufacturer').Validate();
               return;
           }

           if (ASPxClientControl.GetControlCollection().GetByName('txtNameManufacturer').GetValue() == null) {
               ASPxClientControl.GetControlCollection().GetByName('txtNameManufacturer').Validate();
               return;
           }

           //cpLine.PerformCallback('validate');

           cpLineManufacturer.PerformCallback('save');

           cpHeaderManufacturer.PerformCallback('refresh');

       }

       function buttonCancelManufacturer_Click(s, e) {
           formManufacturerEdit.Hide();
       }

       function grdDataManufacturer_CustomButtonClick(s, e) {
           s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesManufacturer);
       }

       function OnGetRowValuesManufacturer(values) {
           formManufacturerEdit.Show();
           //ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

           //var str = 'view|' + values;
           //cpLine.PerformCallback(str);     
       }

       //Manufacturer

       //ManufacturerGroup

       function grdDataManufacturerGroup_EndCallback(s, e) {
           if (s.cpEditManufacturerGroup) {
               formManufacturerGroupEdit.Show();

               ASPxClientEdit.ClearEditorsInContainerById('lineContainerManufacturerGroup');
               cboRowStatusManufacturerGroup.SetValue('A');

               if (s.cpEditManufacturerGroup == 'edit') {
                   cpLineManufacturerGroup.PerformCallback('edit');
               }
               else {
                   cpLineManufacturerGroup.PerformCallback('new');
               }

               delete (s.cpEditManufacturerGroup);
               return;
           }
       }

       function cpHeaderManufacturerGroup_EndCallback(s, e) {
       }

       function cpLineManufacturerGroup_EndCallback(s, e) {
           if (s.cpCodeExisting) {

           }

       }

       function buttonSaveManufacturerGroup_Click(s, e) {
           if (ASPxClientControl.GetControlCollection().GetByName('txtCodeManufacturerGroup').GetValue() == null) {
               ASPxClientControl.GetControlCollection().GetByName('txtCodeManufacturerGroup').Validate();
               return;
           }

           if (ASPxClientControl.GetControlCollection().GetByName('txtNameManufacturerGroup').GetValue() == null) {
               ASPxClientControl.GetControlCollection().GetByName('txtNameManufacturerGroup').Validate();
               return;
           }

           //cpLine.PerformCallback('validate');

           cpLineManufacturerGroup.PerformCallback('save');

           cpHeaderManufacturerGroup.PerformCallback('refresh');

       }

       function buttonCancelManufacturerGroup_Click(s, e) {
           formManufacturerGroupEdit.Hide();
       }

       function grdDataManufacturerGroup_CustomButtonClick(s, e) {
           s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesManufacturerGroup);
       }

       function OnGetRowValuesManufacturerGroup(values) {
           formManufacturerGroupEdit.Show();
           //ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

           //var str = 'view|' + values;
           //cpLine.PerformCallback(str);     

           //ManufacturerCategory
       }
</script>    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uManufacturerEdit ID="uManufacturerEdit1" runat="server" />
    <uc2:uMaufacturerGroupEdit ID="uMaufacturerGroupEdit1" runat="server" />
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" RenderMode="Lightweight">
        <TabPages>
            <dx:TabPage Text="Nhà Sản Xuất">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderManufacturer" runat="server" Text="Danh mục Nhà Sản Xuất"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderManufacturer" runat="server" Width="100%" ClientInstanceName="cpHeaderManufacturer"
                            HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderManufacturer_EndCallback" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataManufacturer" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="SupplierId" OnRowDeleting="grdDataManufacturer_RowDeleting" OnStartRowEditing="grdDataManufacturer_StartRowEditing"
                                         Width="90%" OnInitNewRow="grdDataManufacturer_InitNewRow">
                                        <ClientSideEvents EndCallback="grdDataManufacturer_EndCallback" CustomButtonClick="grdDataManufacturer_CustomButtonClick" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhà Sản Xuất" FieldName="Name" Name="Name"
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="6"
                                                Width="10%" ButtonType="Image">
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
                                            <dx:GridViewDataTextColumn Caption="Mã Nhà Sản Xuất" FieldName="Code" Name="Code"
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="10%">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" ShowInCustomizationForm="True"
                                                Visible="False" VisibleIndex="0" Name="SupplierId">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" Name="Description"
                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="10%">
                                                <EditCellStyle Wrap="True">
                                                </EditCellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                VisibleIndex="5" Width="5%">
                                                <ClearFilterButton Visible="True">
                                                    <Image ToolTip="Xóa">
                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                    </Image>
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
                                                VisibleIndex="3" Width="5%">
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
            <dx:TabPage Text="Nhóm Nhà Sản Xuất">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderManufacturerGroup" runat="server" Text="Danh mục Nhóm Nhà Sản Xuất"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderManufacturerGroup" runat="server" Width="100%"
                            ClientInstanceName="cpHeaderManufacturerGroup" OnCallback="cpHeaderManufacturerGroup_Callback"
                            HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderManufacturerGroup_EndCallback" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataManufacturerGroup" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="SupplierId" OnRowDeleting="grdDataManufacturerGroup_RowDeleting"
                                        OnStartRowEditing="grdDataManufacturerGroup_StartRowEditing" 
                                        Width="100%" OnInitNewRow="grdDataManufacturerGroup_InitNewRow">
                                        <ClientSideEvents EndCallback="grdDataManufacturerGroup_EndCallback" CustomButtonClick="grdDataManufacturerGroup_CustomButtonClick" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Nhà Sản Xuất" FieldName="Name" Name="Name"
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
                                            <dx:GridViewDataTextColumn Caption="Mã  Nhóm Nhà Sản Xuất" FieldName="Code" Name="Code"
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
                                                    <Image ToolTip="Xóa">
                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                    </Image>
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
