<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TempWarehoure.aspx.cs" Inherits="WebModule.ImExporting.TempWarehoure" %>
<%@ Register src="~/ImExporting/UserControl/uWarehouse.ascx" tagname="uWarehouse" tagprefix="uc1" %>
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

    
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Kho Trung Chuyển" 
        Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                 Width="100%" OnInitNewRow="grdData_InitNewRow">
                    <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                        ShowInCustomizationForm="True" VisibleIndex="6">
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
                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" 
                        ShowInCustomizationForm="True" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" 
                        ShowInCustomizationForm="True" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thể loại kho" FieldName="warehousetype" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tên " FieldName="name" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" 
                        ShowInCustomizationForm="True" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                    AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
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
    <div id="clientContainer">
        <uc1:uWarehouse ID="uWarehouse" runat="server" />
    </div>
</asp:Content>
