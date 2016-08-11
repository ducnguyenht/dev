<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PurchaseMaterials.aspx.cs" Inherits="WebModule.Purchasing.PurchaseMaterials" %>
<%@ Register Src="UserControl/uPurchaseMaterialsEdit.ascx" TagName="uPurchaseMaterialsEdit" TagPrefix="uc1" %>
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

        function buttonCancelDevice_Click(s, e) {
        }

        function buttonSaveDevice_Click(s, e) {
        }
    </script>
    <style type="text/css">
        .float_right
        {
            float: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">

<div style="margin-bottom: 10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Mua Nguyên Vật Liệu" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>
</div>

<table style="width:100%">
<tr>
    <td style="vertical-align:top">
        <div class="gridContainer">
            <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                HideContentOnCallback="True" >                               
                <ClientSideEvents EndCallback="cpHeader_EndCallback"></ClientSideEvents>
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" KeyFieldName="SupplierId"
                            OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing"
                            Width="100%" OnInitNewRow="grdData_InitNewRow" 
                            ClientInstanceName="grdData">
                            <ClientSideEvents EndCallback="grdData_EndCallback" CustomButtonClick="grdData_CustomButtonClick" />
                            <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback">
                            </ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Nhà Cung Cấp" FieldName="Supplier" 
                                    Name="Supplier" ShowInCustomizationForm="True" VisibleIndex="2">
                                    <DataItemTemplate>
                                        <div class="wrapColumn">
                                            <%# Eval("Supplier")%>
                                        </div>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="6"
                                    ButtonType="Image" Width="150px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="copy">
                                            <Image ToolTip="Sao chép">                                          
                                                <SpriteProperties CssClass="Sprite_Copy"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <EditButton Visible="True">
                                        <Image>                                          
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <CancelButton Visible="True">
                                    </CancelButton>
                                    <UpdateButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                        </Image>
                                    </UpdateButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Phiếu" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="1" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" ShowInCustomizationForm="True"
                                    Visible="False" VisibleIndex="0" Name="SupplierId">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Nhân viên Đề Nghị" FieldName="Department" Name="Description"
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                    <DataItemTemplate>
	                                    <div class="wrapColumn">
		                                    <%# Eval("Department")%>
	                                    </div>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>                               
                                <dx:GridViewDataDateColumn Caption="Ngày Mua" FieldName="Date" 
                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                    <PropertiesDateEdit DisplayFormatString="">
                                    </PropertiesDateEdit>                               
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True" ColumnResizeMode="Control" />
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True"></SettingsBehavior>
                            <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                            </SettingsPager>                                                       
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
         <uc1:uPurchaseMaterialsEdit ID="uPurchaseMaterialsEdit1" runat="server" />
    </td>
</tr>
<tr>
    <td>               
    
    </td>
</tr>
</table>
</asp:Content>
