<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="quotation.aspx.cs" Inherits="WebModule.ImExporting.quotation" %>
<%@ Register src="~/ImExporting/UserControl/uQuotationEdit.ascx" tagname="uQuotationEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formQuotationEdit.Show();

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
        formQuotationEdit.Hide();
    }

    function grdData_CustomButtonClick(s, e) {
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValues);
    }

    function OnGetRowValues(values) {
        formQuotationEdit.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Báo Giá" 
        Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" 
                HideContentOnCallback="True" >
        <ClientSideEvents EndCallback="cpHeader_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <div style="width:100%; height:500px; overflow:scroll">
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="SupplierId" 
                OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                 Width="100%" OnInitNewRow="grdData_InitNewRow" 
                OnBeforePerformDataSelect="grdData_BeforePerformDataSelect">
                    <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" Name="Code" 
                        ShowInCustomizationForm="True" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tỉ Giá" FieldName="Exchange" 
                            ShowInCustomizationForm="True" VisibleIndex="9">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thành Tiền Quy Đổi" FieldName="AmountA" 
                            ShowInCustomizationForm="True" VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                        VisibleIndex="11" Width="10%" ButtonType="Image">
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
                        <dx:GridViewDataTextColumn Caption="Hiệu Lực Tới" FieldName="Dateto" 
                        Name="Description" ShowInCustomizationForm="True" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thời Gian Giao Hàng" FieldName="Datesent" 
                        ShowInCustomizationForm="True" VisibleIndex="4">
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Nhà Cung Cấp" FieldName="Supplier" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
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
                        <dx:GridViewDataTextColumn Caption="Chi Phí Khác" 
                        ShowInCustomizationForm="True" VisibleIndex="6" FieldName="Cost">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Chiết Khấu" ShowInCustomizationForm="True" 
                        VisibleIndex="5" FieldName="Modify">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Loại Ngoại Tệ" FieldName="Currency" 
                            ShowInCustomizationForm="True" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thành Tiền" ShowInCustomizationForm="True" 
                        VisibleIndex="8" FieldName="Amount">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                    AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <Styles>
                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                        </Header>
                        <HeaderPanel HorizontalAlign="Center">
                        </HeaderPanel>
                        <CommandColumn HorizontalAlign="Center" Spacing="10px">
                        </CommandColumn>
                    </Styles>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="grdLine" runat="server" AutoGenerateColumns="False" 
                            KeyFieldName="No" 
                            onbeforeperformdataselect="grdLine_BeforePerformDataSelect" Width="100%">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="STT" FieldName="No" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Hàng Hóa" FieldName="Name" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Unit" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành Tiền Sau CK" FieldName="Amount2" 
                                    VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Loại Ngoại Tệ" FieldName="Currency" 
                                        VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tỉ Giá" FieldName="Exchange" 
                                        VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành Tiền Quy Đổi" FieldName="AmountA" 
                                        VisibleIndex="7">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsDetail IsDetailGrid="True"  />
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
                </div>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:XpoDataSource ID="QuoXDS" runat="server" ServerMode="True" 
    TypeName="" Criteria="">
    </dx:XpoDataSource>
    <uc1:uQuotationEdit ID="uQuotationEdit1" runat="server" />
</asp:Content>
