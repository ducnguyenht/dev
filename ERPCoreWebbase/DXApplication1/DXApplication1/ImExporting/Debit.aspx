<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Debit.aspx.cs" Inherits="WebModule.ImExporting.Debit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Công Nợ Nhà Cung Cấp" 
        Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="ProductId" 
                OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing" 
                 Width="100%" OnInitNewRow="grdData_InitNewRow">
                    <ClientSideEvents CustomButtonClick="" EndCallback="">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Nợ Đầu Kỳ" FieldName="DK" Name="Name" 
                        ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True"
                        VisibleIndex="7" Width="10%" ButtonType="Image">
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
                        <dx:GridViewDataTextColumn Caption="Nhà Cung Cấp" FieldName="Supplier" Name="Code" 
                        ShowInCustomizationForm="True" VisibleIndex="1" Width="20%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ProductId" FieldName="ProductId" 
                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" 
                        Name="ProductId">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Đã Thanh Toán" FieldName="TT" 
                        Name="ManufacturerName" ShowInCustomizationForm="True" VisibleIndex="4" 
                        Width="10%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Nợ Cuối Kỳ" FieldName="CK" 
                        ShowInCustomizationForm="True" VisibleIndex="6" Width="10%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Phát Sinh" FieldName="PS" 
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
                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ConfirmDelete="True">
                    </SettingsBehavior>
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                    <SettingsEditing Mode="Inline">
                    </SettingsEditing>
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True">
                    </Settings>
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
</asp:Content>
