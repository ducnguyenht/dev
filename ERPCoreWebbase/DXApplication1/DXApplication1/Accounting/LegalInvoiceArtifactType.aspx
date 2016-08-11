<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="LegalInvoiceArtifactType.aspx.cs" Inherits="WebModule.Accounting.LegalInvoiceArtifactType" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pageLegalInvoiceArtifactType" RenderMode="Lightweight" runat="server"
        ActiveTabIndex="0" Width="100%" ContentStyle-HorizontalAlign="Center" ClientInstanceName="pageLegalInvoiceArtifactType">
        <TabPages>
            <dx:TabPage Text="Cấu hình phân loại hóa đơn">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="Grid_LegalInvoiceArtifactType" runat="server" AutoGenerateColumns="False"
                            DataSourceID="DBLegalInvoiceArtifactType" KeyFieldName="LegalInvoiceArtifactTypeId"
                            ClientInstanceName="Grid_LegalInvoiceArtifactType" Width="100%" OnRowDeleting="Grid_LegalInvoiceArtifactType_RowDeleting"
                            OnRowInserting="Grid_LegalInvoiceArtifactType_RowInserting" OnRowUpdating="Grid_LegalInvoiceArtifactType_RowUpdating"
                            OnRowValidating="Grid_LegalInvoiceArtifactType_RowValidating" 
                            OnCellEditorInitialize="Grid_LegalInvoiceArtifactType_CellEditorInitialize">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã số phân loại" FieldName="Code" VisibleIndex="1"
                                    ShowInCustomizationForm="true" Width="10%">
                                    <PropertiesTextEdit>
                                        <ValidationSettings EnableCustomValidation="True" 
                                            ErrorText="Mã số phân loại không được trống">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên phân loại" FieldName="Name" VisibleIndex="2"
                                    ShowInCustomizationForm="true" Width="57%">
                                    <PropertiesTextEdit>
                                        <ValidationSettings EnableCustomValidation="True" 
                                            ErrorText="Tên Phân Loại không được trống">
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Loại" FieldName="Category" VisibleIndex="3"
                                    ShowInCustomizationForm="true" Width="8%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Value="O" Text="Đầu Ra" />
                                            <dx:ListEditItem Value="I" Text="Đầu vào" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="Description" VisibleIndex="4"
                                    ShowInCustomizationForm="true" Width="15%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Value="T" Text="Đang Sử Dụng" />
                                            <dx:ListEditItem Value="F" Text="Ngưng Sử Dụng" />
                                        </Items>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ButtonType="Image" VisibleIndex="6" ShowInCustomizationForm="true" Width="10%">
                                    <EditButton Visible="True">
                                        <Image ToolTip="Sửa">
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image ToolTip="Thêm">
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image ToolTip="Xóa">
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <UpdateButton>
                                        <Image ToolTip="Cập nhật">
                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton>
                                        <Image ToolTip="Bỏ qua">
                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                        </Image>
                                    </CancelButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <SettingsEditing Mode="Inline" />

<SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" allowfocusedrow="True"></SettingsBehavior>

                            <SettingsPager Position="Bottom">
                                <PageSizeItemSettings Items="10, 20, 50">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                            <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                            <Settings ShowFilterRow="True" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True"></Settings>

<SettingsText ConfirmDelete="Bạn C&#243; Chắc Muốn X&#243;a Kh&#244;ng?"></SettingsText>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle HorizontalAlign="Center">
        </ContentStyle>
    </dx:ASPxPageControl>
    <dx:XpoDataSource ID="DBLegalInvoiceArtifactType" runat="server" 
        TypeName="NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType" 
        Criteria="[RowStatus] &gt; 0s">
    </dx:XpoDataSource>
</asp:Content>
