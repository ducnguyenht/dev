<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CombineSupplier.aspx.cs" Inherits="WebModule.ImExporting.CombineSupplier" %>
<%@ Register src="~/ImExporting/UserControl/uSupplierGroupEdit.ascx" tagname="uSupplierGroupEdit" tagprefix="uc2" %>
<%@ Register src="~/ImExporting/UserControl/uSupplierEdit.ascx" tagname="uSupplierEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
 <script type="text/javascript">

     //Supplier
     function grdDataSupplier_EndCallback(s, e) {
         if (s.cpEditSupplier) {
             formSupplierEdit.Show();

             ASPxClientEdit.ClearEditorsInContainerById('lineContainerSupplier');
             cboRowStatusSupplier.SetValue('A');

             if (s.cpEditSupplier == 'edit') {
                 cpLineSupplier.PerformCallback('edit');
             }
             else {
                 cpLineSupplier.PerformCallback('new');
             }

             delete (s.cpEditSupplier);
             return;
         }
     }

     function cpHeaderSupplier_EndCallback(s, e) {
     }

     function cpLineSupplier_EndCallback(s, e) {
         if (s.cpCodeExisting) {

         }

     }

     function buttonSaveSupplier_Click(s, e) {
         if (ASPxClientControl.GetControlCollection().GetByName('txtCodeSupplier').GetValue() == null) {
             ASPxClientControl.GetControlCollection().GetByName('txtCodeSupplier').Validate();
             return;
         }

         if (ASPxClientControl.GetControlCollection().GetByName('txtNameSupplier').GetValue() == null) {
             ASPxClientControl.GetControlCollection().GetByName('txtNameSupplier').Validate();
             return;
         }

         //cpLine.PerformCallback('validate');

         cpLineSupplier.PerformCallback('save');

         cpHeaderSupplier.PerformCallback('refresh');

     }

     function buttonCancelSupplier_Click(s, e) {
         formSupplierEdit.Hide();
     }

     function grdDataSupplier_CustomButtonClick(s, e) {
         s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesSupplier);
     }

     function OnGetRowValuesSupplier(values) {
         formSupplierEdit.Show();
         //ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

         //var str = 'view|' + values;
         //cpLine.PerformCallback(str);     
     }

     //Supplier

     //SupplierGroup
     function grdDataSupplierGroup_EndCallback(s, e) {
         formSupplierEdit.Show();
     }

     function cpHeaderSupplierGroup_EndCallback(s, e) {
     }

     function cpLineSupplierGroup_EndCallback(s, e) {
         if (s.cpCodeExisting) {

         }

     }

     function buttonSaveSupplierGroup_Click(s, e) {
         if (ASPxClientControl.GetControlCollection().GetByName('txtCodeSupplierGroup').GetValue() == null) {
             ASPxClientControl.GetControlCollection().GetByName('txtCodeSupplierGroup').Validate();
             return;
         }

         if (ASPxClientControl.GetControlCollection().GetByName('txtNameSupplierGroup').GetValue() == null) {
             ASPxClientControl.GetControlCollection().GetByName('txtNameSupplierGroup').Validate();
             return;
         }

         //cpLine.PerformCallback('validate');

         cpLineSupplierGroup.PerformCallback('save');

         cpHeaderSupplierGroup.PerformCallback('refresh');

     }

     function buttonCancelSupplierGroup_Click(s, e) {
         formSupplierEdit.Hide();
     }

     function grdDataSupplierGroup_CustomButtonClick(s, e) {
         s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValuesSupplierGroup);
     }

     function OnGetRowValuesSupplierGroup(values) {
         formSupplierEdit.Show();
         ASPxClientEdit.ClearEditorsInContainerById('lineContainerSupplierGroup');

         var str = 'view|' + values;
         cpLineSupplierGroup.PerformCallback(str);
     }
     //SupplierGroup
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">

    <uc1:uSupplierEdit ID="uSupplierEdit1" runat="server" />
    <uc2:uSupplierGroupEdit ID="uSupplierGroupEdit1" runat="server" />
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" RenderMode="Lightweight">
        <TabPages>
            <dx:TabPage Text="Nhà Cung Cấp">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderSupplier" runat="server" Text="Danh mục Nhà Cung Cấp"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderSupplier" runat="server" Width="100%" ClientInstanceName="cpHeaderSupplier"
                            OnCallback="cpHeaderSupplier_Callback" HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderSupplier_EndCallback" />
                            <ClientSideEvents EndCallback="cpHeaderSupplier_EndCallback">
                            </ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataSupplier" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="SupplierId" OnRowDeleting="grdDataSupplier_RowDeleting" OnStartRowEditing="grdDataSupplier_StartRowEditing"
                                         Width="100%" OnInitNewRow="grdDataSupplier_InitNewRow">
                                        <ClientSideEvents EndCallback="grdDataSupplier_EndCallback" CustomButtonClick="grdDataSupplier_CustomButtonClick" />
                                        <ClientSideEvents CustomButtonClick="grdDataSupplier_CustomButtonClick" EndCallback="grdDataSupplier_EndCallback">
                                        </ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name"
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
                                            <dx:GridViewDataTextColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code"
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
            <dx:TabPage Text="Nhóm Nhà Cung Cấp">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderSupplierGroup" runat="server" Text="Danh mục Nhóm Nhà Cung Cấp" Font-Bold="True"
                                Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cpHeaderSupplierGroup" runat="server" Width="100%" ClientInstanceName="cpHeaderSupplierGroup"
                            OnCallback="cpHeaderSupplierGroup_Callback" HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeaderSupplierGroup_EndCallback" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdDataSupplierGroup" runat="server" AutoGenerateColumns="False" KeyFieldName="SupplierId"
                                        OnRowDeleting="grdDataSupplierGroup_RowDeleting" OnStartRowEditing="grdDataSupplierGroup_StartRowEditing"
                                         Width="100%" OnInitNewRow="grdDataSupplierGroup_InitNewRow">
                                        <ClientSideEvents EndCallback="grdDataSupplierGroup_EndCallback" CustomButtonClick="grdDataSupplierGroup_CustomButtonClick" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Nhà Cung Cấp" FieldName="Name" Name="Name"
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
                                            <dx:GridViewDataTextColumn Caption="Mã  Nhóm Nhà Cung Cấp" FieldName="Code" Name="Code"
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
