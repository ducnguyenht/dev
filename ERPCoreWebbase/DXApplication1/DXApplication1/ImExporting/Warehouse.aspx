<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Warehouse.aspx.cs" Inherits="WebModule.ImExporting.Warehouse" %>
<%@ Register src="~/Warehouse/UserControl/uWarehouse.ascx" tagname="uWarehouse" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">

    // MainForm Event
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formMaterialEdit.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
            cboRowStatus.SetValue('A');

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

    function grdData_CustomButtonClick(s, e) {
        s.GetRowValues(e.visibleIndex, 'ProductId', OnGetRowValues);
    }

    function OnGetRowValues(values) {
        formMaterialEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }

    // EditForm Event 
    function buttonSave_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').Validate();
            return;
        }

        cpLine.PerformCallback('save');
        cpHeader.PerformCallback('refresh');

    }

    function buttonCancel_Click(s, e) {
        formMaterialEdit.Hide();
    }

    function buttonSaveDevice_Click(s, e) {
    }

    function buttonCancelDevice_Click(s, e) {
    }
    
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div class="captionFormName">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh Mục Kho" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>    
</div>

<table style="width:100%;">
<tr>
    <td style="vertical-align:top;">
        <div class="gridContainer">
            <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                            ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                            HideContentOnCallback="True" >
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                            OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                             Width="100%" OnInitNewRow="grdData_InitNewRow" KeyFieldName="code">
                            <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
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
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" 
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="200px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" 
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Thể loại kho" FieldName="warehousetype" 
                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên Kho" FieldName="name" 
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Kho" FieldName="code" 
                                    ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                AllowSelectSingleRowOnly="True" ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                            <SettingsPager PageSize="22" ShowEmptyDataRows="true">
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
        </div>              
    </td>
</tr>
<tr>
    <td>
        <div id="clientContainer">       
            <uc1:uWarehouse ID="uWarehouse" runat="server" />       
        </div>                
    </td>
</tr>
</table>
 
</asp:Content>

