<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PaymentConfig.aspx.cs" Inherits="ERPCore.PayReceiving.PaymentConfig" %>
<%@ Register src="UserControl/PaymentVoucherEdit.ascx" tagname="PaymentVoucherEdit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdData_EndCallback(s, e) {
        formPaymentVoucherEdit.Show();
    }
</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">

    <table class="style1">
        <tr>
            <td>
                <div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Cấu Hình Phiếu Chi" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
<dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" 
                HideContentOnCallback="True" >                
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">                        
            <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="Code" 
                OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                 Width="100%" OnInitNewRow="grdData_InitNewRow">
                <ClientSideEvents EndCallback="grdData_EndCallback"/>
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Tên Thể Loại" FieldName="Name" Name="Name" 
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="300px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Xóa" 
                        ShowInCustomizationForm="True" VisibleIndex="6" Width="20px">
                        <ClearFilterButton Visible="True">
                            <Image ToolTip="Xóa">
                                <SpriteProperties CssClass="Sprite_Clear" />
                            </Image>
                        </ClearFilterButton>
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton>
                                <Image ToolTip="Xóa">
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" Name="Code" 
                        ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" 
                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" 
                        Name="SupplierId">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" 
                        Name="Description" ShowInCustomizationForm="True" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                        VisibleIndex="5" Width="50px" ButtonType="Image">
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
                    <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" 
                        ShowInCustomizationForm="True" VisibleIndex="4" Width="50px">
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
    <dx:XpoDataSource ID="XpoDataSource1" runat="server">
    </dx:XpoDataSource>
                
            </td>
        </tr>
        <tr>
            <td>
                <uc1:PaymentVoucherEdit ID="PaymentVoucherEdit1" runat="server" />
            </td>
        </tr>
    </table>

</asp:Content>
