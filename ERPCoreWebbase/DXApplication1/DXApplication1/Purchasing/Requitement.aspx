<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Requitement.aspx.cs" Inherits="ERPCore.Purchasing.requitement" %>
<%@ Register src="UserControl/uRequitementEdit.ascx" tagname="uRequitementEdit" tagprefix="uc1" %>
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

    function buttonSaveDevice_Click(s, e) {
    }

    function buttonCancelDevice_Click(s, e) {
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Yêu Cầu" 
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
                             Width="100%" OnInitNewRow="grdData_InitNewRow">
                            <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick"/>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã Phiếu YC" FieldName="Code" Name="Name" 
                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                                    VisibleIndex="5" Width="9%" ButtonType="Image">
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
                                <dx:GridViewDataTextColumn Caption="Nhân Viên Yêu Cầu" FieldName="Department" 
                                    Name="Department" ShowInCustomizationForm="True" VisibleIndex="3">
                                    <DataItemTemplate>
	                                    <div class="wrapColumn">
		                                    <%# Eval("Department")%>
	                                    </div>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mục Đích" FieldName="Purpose" 
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="300px">
                                      <DataItemTemplate>
	                                    <div class="wrapColumn">
		                                    <%# Eval("Purpose")%>
	                                    </div>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Ngày Yêu Cầu" FieldName="Date" 
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="">
                                    </PropertiesDateEdit>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                AllowSelectSingleRowOnly="True" ConfirmDelete="True" ColumnResizeMode="Control" />
                            <SettingsPager PageSize="22" ShowEmptyDataRows="true">
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

                </div>
    </td>
</tr>
<tr>
    <td>
       <uc1:uRequitementEdit ID="uRequitementEdit1" runat="server" />
    </td>
</tr>
<tr>
    <td>               
        <dx:XpoDataSource ID="RequiteXDS" runat="server" ServerMode="True" 
            TypeName="" Criteria="">
        </dx:XpoDataSource>
    </td>
</tr>
</table>
</asp:Content>
