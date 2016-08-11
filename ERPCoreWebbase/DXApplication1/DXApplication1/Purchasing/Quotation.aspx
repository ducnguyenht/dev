<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="ERPCore.Purchasing.quotation" %>
<%@ Register src="UserControl/uQuotationEdit.ascx" tagname="uQuotationEdit" tagprefix="uc1" %>
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

    function buttonCancelDevice_Click(s, e) {
    }

    function buttonSaveDevice_Click(s, e) {
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">

<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Báo Giá" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
<table style="width:100%">
<tr>
    <td style="vertical-align:top">
        <div class="gridContainer">
            <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                            ClientInstanceName="cpHeader" 
                            HideContentOnCallback="True" >
                            <ClientSideEvents EndCallback="cpHeader_EndCallback" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                            KeyFieldName="SupplierId" 
                            OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                             Width="100%" OnInitNewRow="grdData_InitNewRow" 
                            OnBeforePerformDataSelect="grdData_BeforePerformDataSelect">
                            <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã Báo Giá" FieldName="Code" Name="Code" 
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                                    VisibleIndex="8" Width="9%" ButtonType="Image">
                                    <EditButton Visible="True">
                                        <Image SpriteProperties-CssClass="Sprite_Edit">
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image SpriteProperties-CssClass="Sprite_New">
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" 
                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" 
                                    Name="SupplierId">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Hiệu Lực Tới" FieldName="Dateto" 
                                    Name="Description" ShowInCustomizationForm="True" VisibleIndex="3" 
                                    Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="">
                                    </PropertiesDateEdit>                                
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Ngày Giao Hàng" FieldName="Datesent" 
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="">
                                    </PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                    <HeaderStyle Wrap="True" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Nhà Cung Cấp" FieldName="Supplier" 
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                    <DataItemTemplate>
	                                    <div class="wrapColumn">
		                                    <%# Eval("Supplier")%>
	                                    </div>
                                    </DataItemTemplate>                                                                    
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Chi Phí Khác" 
                                    ShowInCustomizationForm="True" VisibleIndex="6" FieldName="Cost" 
                                    Width="100px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Chiết Khấu" ShowInCustomizationForm="True" 
                                    VisibleIndex="5" FieldName="Modify" Width="100px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Thành Tiền" ShowInCustomizationForm="True" 
                                    VisibleIndex="7" FieldName="Amount" Width="120px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                AllowSelectSingleRowOnly="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                            <SettingsPager PageSize="22" ShowEmptyDataRows="true">
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
                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="No" VisibleIndex="0" Width="60px">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="Code" VisibleIndex="1" Width="150px">                                                
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="Name" VisibleIndex="2">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Unit" VisibleIndex="3" Width="100px">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành Tiền Sau CK" FieldName="Amount2" 
                                                VisibleIndex="4" Width="130px">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsDetail IsDetailGrid="True"/>
                                        <SettingsPager PageSize="10" ShowEmptyDataRows="true"></SettingsPager>
                                        <SettingsBehavior ColumnResizeMode="Control" />
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                        </dx:ASPxGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
                </div>
    </td>
</tr>
<tr>
    <td>
        <uc1:uQuotationEdit ID="uQuotationEdit1" runat="server" />
    </td>
</tr>
<tr>
    <td>               
        <dx:XpoDataSource ID="QuoXDS" runat="server" ServerMode="True" 
    TypeName="" Criteria="">
</dx:XpoDataSource>
    </td>
</tr>
</table>
</asp:Content>
