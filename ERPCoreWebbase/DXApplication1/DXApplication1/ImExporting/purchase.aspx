<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="purchase.aspx.cs" Inherits="WebModule.ImExporting.purchase" %>
<%@ Register src="~/ImExporting/UserControl/uPurchaseEdit.ascx" tagname="uPurchaseEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formPurchaseEdit.Show();

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
        formPurchaseEdit.Hide();
    }

    function grdData_CustomButtonClick(s, e) {
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValues());
    }

    function OnGetRowValues(values) {
        formPurchaseEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Mua Hàng" 
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
                OnRowDeleting="grdData_RowDeleting" 
                OnStartRowEditing="grdData_StartRowEditing" Width="100%" 
                OnInitNewRow="grdData_InitNewRow">
                    <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Nhà Cung Cấp" FieldName="Supplier" Name="Supplier" 
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="20%">
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
                        <dx:GridViewDataTextColumn Caption="Mã Đơn Hàng" FieldName="Code" Name="Code" 
                        ShowInCustomizationForm="True" VisibleIndex="1" Width="5%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" 
                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" 
                        Name="SupplierId">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Nhân viên Đề Nghị" FieldName="Department" 
                        Name="Description" ShowInCustomizationForm="True" VisibleIndex="4" 
                        Width="20%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" 
                        ShowInCustomizationForm="True" VisibleIndex="5" Width="5%">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Info" />
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Ngày Mua Hàng" FieldName="Date" 
                        ShowInCustomizationForm="True" VisibleIndex="3" Width="5%">
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
    <uc1:uPurchaseEdit ID="uPurchaseEdit1" runat="server" />
</asp:Content>
