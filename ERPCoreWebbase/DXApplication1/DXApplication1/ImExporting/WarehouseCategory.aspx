<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="WarehouseCategory.aspx.cs" Inherits="WebModule.ImExporting.WarehouseCategory" %>
<%@ Register src="~/Warehouse/UserControl/uWarehouseCategory.ascx" tagname="uWarehouseCategory" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formMaterialCategoryEdit.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
            //cboRowStatus.SetValue('A');

            if (s.cpEdit == 'edit') {
                cpLine.PerformCallback('edit');
            }
            else {
                cpLine.PerformCallback('new');
            }

            delete (cpEdit);
            return;
        }
    }

    function cpHeader_EndCallback(s, e) {
    }

    function cpLine_EndCallback(s, e) {
        if (s.cpCodeExisting) {

        }

    }

    function buttonSave_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
            return;
        }

        //cpLine.PerformCallback('validate');

        cpLine.PerformCallback('save');

        cpHeader.PerformCallback('refresh');

    }

    function buttonCancel_Click(s, e) {
        formMaterialCategoryEdit.Hide();
    }

    function grdData_CustomButtonClick(s, e) {
        //s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValues);
        formMaterialCategoryEdit.Show();
    }

    function OnGetRowValues(values) {
        formMaterialCategoryEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }

 

    function buttonCancelDevice_Click(s, e) {
    }

    function buttonSaveDevice_Click(s, e) {
    }


</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div class="captionFormName">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh Mục Thể Loại Kho" Font-Bold="True"
        Font-Size="Medium">
    </dx:ASPxLabel>    
</div>

<table style="width:100%;">
<tr>
    <td style="vertical-align:top;">
        <div class="gridContainer">
            <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                            ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                            HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeader_EndCallback" />
            <ClientSideEvents EndCallback="cpHeader_EndCallback"></ClientSideEvents>
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">     
                        <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                            KeyFieldName="SupplierId" 
                            OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                             Width="100%" OnInitNewRow="grdData_InitNewRow">
                            <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
            <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" Name="Name" 
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                                    VisibleIndex="6" Width="100px" ButtonType="Image">
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
                                    <CancelButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </CancelButton>
                                    <UpdateButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Update" />
                                        </Image>
                                    </UpdateButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã" FieldName="code" Name="Code" 
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" 
                                    Name="Description" ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" 
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="60px">
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton>
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="rowstatus" 
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
                            </Columns>
                
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ConfirmDelete="True" ColumnResizeMode="NextColumn">
                            </SettingsBehavior>
                            <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />

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
        </div>              
    </td>
</tr>
<tr>
    <td>
        <uc1:uWarehouseCategory ID="uWarehouseCategory" runat="server" />            
    </td>
</tr>
        
</table>
          


</asp:Content>
