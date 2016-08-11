<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="requitement.aspx.cs" Inherits="WebModule.ImExporting.requitement" %>
<%@ Register src="~/ImExporting/UserControl/uRequitementEdit.ascx" tagname="uRequitementEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formRequitementEdit.Show();

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
        formRequitementEdit.Hide();
    }

    function grdData_CustomButtonClick(s, e) {
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValues);
    }

    function OnGetRowValues(values) {
        formRequitementEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Yêu Cầu" 
        Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" 
                HideContentOnCallback="True" >
        <ClientSideEvents EndCallback="cpHeader_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="SupplierId" 
                OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                 Width="100%" OnInitNewRow="grdData_InitNewRow">
                    <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" Name="Name" 
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                        VisibleIndex="6" Width="10%" ButtonType="Image">
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
                        <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" 
                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" 
                        Name="SupplierId">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Nhân Viên Yêu Cầu" FieldName="Department" 
                        Name="Description" ShowInCustomizationForm="True" VisibleIndex="4" 
                        Width="20%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mục Đích" FieldName="Purpose" 
                        ShowInCustomizationForm="True" VisibleIndex="5" Width="20%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Ngày Yêu Cầu" FieldName="Date" 
                        ShowInCustomizationForm="True" VisibleIndex="3" Width="10%">
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
    <dx:XpoDataSource ID="RequiteXDS" runat="server" ServerMode="True" 
    TypeName="" Criteria="">
    </dx:XpoDataSource>
    <uc1:uRequitementEdit ID="uRequitementEdit1" runat="server" />
</asp:Content>
