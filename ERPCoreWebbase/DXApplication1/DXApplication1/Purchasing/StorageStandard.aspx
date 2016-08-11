<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="StorageStandard.aspx.cs" Inherits="DXApplication1.Purchasing.StorageStandard" %>
<%@ Register src="UserControl/uStorageStandardEdit.ascx" tagname="uStorageStandardEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formStorageStandardEdit.Show();

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
        formStorageStandardEdit.Hide();
    }

    function grdData_CustomButtonClick(s, e) {
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValues);
    }

    function OnGetRowValues(values) {
        formStorageStandardEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh mục Tiêu Chuẩn Lưu Trữ" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
<dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
                <ClientSideEvents EndCallback="cpHeader_EndCallback" />
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
            OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
            OnInitNewRow="grdData_InitNewRow" 
                 Width="100%">
                <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
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
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="400px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="rowstatus" 
                        ShowInCustomizationForm="True" VisibleIndex="1" Width="50px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" 
                        ShowInCustomizationForm="True" VisibleIndex="0" Width="300px">
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

          
<dx:XpoDataSource ID="SupplierXDS" runat="server" ServerMode="True" 
    TypeName="DAL.Purchasing.ViewSupplier" Criteria="">
</dx:XpoDataSource>
            
 <div id="clientContainer">
     <uc1:uStorageStandardEdit ID="uStorageStandardEdit1" runat="server" />
</div>                          
        

</asp:Content>
