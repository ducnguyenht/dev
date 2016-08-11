<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TaxTypeSetting.aspx.cs" Inherits="WebModule.Accounting.TaxTypeSetting" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="ASPxPageControlTaxSetting" runat="server" RenderMode="Lightweight"
        ActiveTabIndex="0" Width="100%" Height="100%">
        <TabPages>
            <dx:TabPage Text="Cấu hình thuế suất">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxLabel ID="lblRemark" runat="server" Font-Italic="True" Font-Size="Small"
                            ForeColor="#CCCCCC" Text="(*) Mã loại thuế chỉ chứa các kí tự chữ cái/số. Kí tự đầu tiên của mã loại phải bắt đầu bằng 1 chứ cái">
                        </dx:ASPxLabel>
                        <br />
                        <dx:ASPxLabel ID="lblTitleCategory" runat="server" Font-Italic="false" Font-Size="Small"
                            Text="Danh mục loại thuế">
                        </dx:ASPxLabel>
                        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" FullscreenMode="true">
                            <Panes>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="100%" ScrollBars="Auto">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxGridView ID="grdTaxTypeSetting" runat="server" AutoGenerateColumns="False"
                                                            Width="100%" DataSourceID="TaxTypeSettingXDS" KeyFieldName="TaxTypeId" ClientInstanceName="grdTaxTypeSetting"
                                                            KeyboardSupport="True" OnRowDeleting="grdTaxTypeSetting_RowDeleting" OnRowInserting="grdTaxTypeSetting_RowInserting"
                                                            OnRowUpdating="grdTaxTypeSetting_RowUpdating" OnCommandButtonInitialize="grdTaxTypeSetting_CommandButtonInitialize">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="TaxTypeId" ReadOnly="True" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Code" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                    Caption="Mã Loại" Width="20%">
                                                                    <PropertiesTextEdit Width="100%" MaxLength="36">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Bắt buộc nhập Mã Loại" IsRequired="true" />
                                                                            <RegularExpression ValidationExpression="^[A-Za-z]{1}[A-Za-z0-9]{0,35}$" ErrorText="Mã thuế chưa đúng định dạng" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                    Caption="Tên Loại" Width="30%">
                                                                    <PropertiesTextEdit Width="100%" MaxLength="255">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Bắt buộc nhập Tên Loại" IsRequired="true" />
                                                                        </ValidationSettings>
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" Caption="Mô tả" Width="30%">
                                                                    <PropertiesTextEdit Width="100%" MaxLength="255">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" Width="20%">
                                                                    <PropertiesComboBox Width="100%" IncrementalFilteringMode="Contains">
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="Bắt buộc nhập Trạng Thái" IsRequired="true" />
                                                                        </ValidationSettings>
                                                                        <Items>
                                                                            <dx:ListEditItem Text="Sử dụng" Value="1" />
                                                                            <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                                                                        </Items>
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5">
                                                                    <EditButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                                        </Image>
                                                                    </EditButton>
                                                                    <NewButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_New" />
                                                                        </Image>
                                                                    </NewButton>
                                                                    <DeleteButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                                        </Image>
                                                                    </DeleteButton>
                                                                    <CancelButton>
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <UpdateButton>
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                    <ClearFilterButton Visible="True">
                                                                    </ClearFilterButton>
                                                                </dx:GridViewCommandColumn>
                                                            </Columns>
                                                            <SettingsEditing Mode="EditForm" />
                                                            <SettingsDetail ShowDetailRow="true" />
                                                            <SettingsEditing Mode="Inline" />
                                                            <SettingsBehavior ConfirmDelete="True" />
                                                            <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                                                            <Settings ShowFilterRow="True" />
                                                            <SettingsPager>
                                                                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                                            </SettingsPager>
                                                            <Templates>
                                                                <DetailRow>
                                                                    <dx:ASPxLabel ID="lblTitleCategory" runat="server" Font-Italic="false" Font-Size="Small"
                                                                        Text="Danh mục phân loại thuế">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxGridView ID="grdTaxSetting" runat="server" AutoGenerateColumns="False" DataSourceID="TaxSettingXDS"
                                                                        KeyFieldName="TaxId" Width="100%" OnBeforePerformDataSelect="grdTaxTypeSetting_BeforePerformDataSelect"
                                                                        OnRowInserting="grdTaxSetting_RowInserting" OnRowDeleting="grdTaxSetting_RowDeleting"
                                                                        OnRowUpdating="grdTaxSetting_RowUpdating" PreviewFieldName="TaxTypeId!Key">
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn FieldName="TaxId" ReadOnly="True" ShowInCustomizationForm="True"
                                                                                VisibleIndex="0" Visible="false">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Code" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                                Caption="Mã Phân Loại">
                                                                                <PropertiesTextEdit MaxLength="36">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Bắt buộc Mã Phân Loại" IsRequired="true" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                                Caption="Tên Phân Loại">
                                                                                <PropertiesTextEdit MaxLength="255">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Bắt buộc nhập Tên Phân Loại" IsRequired="true" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesTextEdit>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataSpinEditColumn Caption="Tỉ lệ (%)" FieldName="Percentage" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3">
                                                                                <PropertiesSpinEdit DisplayFormatString="g">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Bắt buộc nhập Tỉ lệ %" IsRequired="true" />
                                                                                    </ValidationSettings>
                                                                                </PropertiesSpinEdit>
                                                                            </dx:GridViewDataSpinEditColumn>
                                                                            <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                                                VisibleIndex="4">
                                                                                <PropertiesComboBox IncrementalFilteringMode="Contains">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField ErrorText="Bắt buộc nhập Trạng thái" IsRequired="true" />
                                                                                    </ValidationSettings>
                                                                                    <Items>
                                                                                        <dx:ListEditItem Text="Sử dụng" Value="1" />
                                                                                        <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                                                                                    </Items>
                                                                                </PropertiesComboBox>
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                VisibleIndex="5">
                                                                                <EditButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                                    </Image>
                                                                                </EditButton>
                                                                                <NewButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                                    </Image>
                                                                                </NewButton>
                                                                                <DeleteButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                                    </Image>
                                                                                </DeleteButton>
                                                                                <CancelButton>
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                    </Image>
                                                                                </CancelButton>
                                                                                <UpdateButton>
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                                    </Image>
                                                                                </UpdateButton>
                                                                            </dx:GridViewCommandColumn>
                                                                        </Columns>
                                                                        <SettingsEditing Mode="Inline" />
                                                                        <SettingsBehavior AllowFocusedRow="true" ConfirmDelete="True" />
                                                                        <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                                                                        <Settings ShowFilterRow="True" />
                                                                        <SettingsPager>
                                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                                                        </SettingsPager>
                                                                    </dx:ASPxGridView>
                                                                </DetailRow>
                                                            </Templates>
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                </dx:SplitterPane> </Panes> </dx:ASPxSplitter> </dx:ContentControl> </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <dx:XpoDataSource ID="TaxTypeSettingXDS" runat="server" TypeName="NAS.DAL.Invoice.TaxType"
        Criteria="[RowStatus] &gt; 0">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="TaxSettingXDS" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Tax.Tax"
        Criteria="[RowStatus] &gt; 0 And [TaxTypeId] = ?">
        <CriteriaParameters>
            <asp:SessionParameter Name="TaxTypeId" SessionField="TaxTypeId_Tax" />
        </CriteriaParameters>
    </dx:XpoDataSource>
</asp:Content>
