<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uCurrency.ascx.cs" Inherits="WebModule.Accounting.UserControl.uCurrency" %>
<script type="text/javascript">
    function Grid_ExchangeRate_Init(s, e) {
        s.ExpandAll();
    }
</script>
<dx:ASPxCallbackPanel ID="cpCurrency" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="Grid_CurrencyType" ClientInstanceName="Grid_CurrencyType" runat="server"
                AutoGenerateColumns="False" DataSourceID="DBCurrencyType" KeyFieldName="CurrencyTypeId"
                Width="100%" KeyboardSupport="true" OnInit="Grid_CurrencyType_Init" OnCommandButtonInitialize="Grid_CurrencyType_CommandButtonInitialize">
                <Columns>
                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0" Width="5%">
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Tên Tiền Tệ" FieldName="Name" ShowInCustomizationForm="True"
                        VisibleIndex="1" Width="45%">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="Description" ShowInCustomizationForm="True"
                        VisibleIndex="2" Width="50%">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager>
                    <PageSizeItemSettings Visible="True">
                    </PageSizeItemSettings>
                </SettingsPager>
                <Settings ShowFilterRow="True" />
                <SettingsDetail ShowDetailRow="True" />
                <Templates>
                    <DetailRow>
                        <dx:ASPxGridView ID="Grid_ExchangeRate" runat="server" AutoGenerateColumns="False"
                            DataSourceID="DBExchangeRate" KeyFieldName="ExchangeRateId" ClientInstanceName="Grid_ExchangeRate"
                            OnInit="Grid_ExchangeRate_Init" Width="100%" OnRowDeleting="Grid_ExchangeRate_RowDeleting"
                            OnRowInserting="Grid_ExchangeRate_RowInserting" OnHtmlDataCellPrepared="Grid_ExchangeRate_HtmlDataCellPrepared"
                            OnCellEditorInitialize="Grid_ExchangeRate_CellEditorInitialize" OnBeforePerformDataSelect="Grid_ExchangeRate_BeforePerformDataSelect"
                            OnRowUpdating="Grid_ExchangeRate_RowUpdating" PreviewFieldName="CurrencyTypeId!Key">
                            <Columns>
                                <dx:GridViewDataDateColumn Caption="Ngày" Name="AffectedDate" FieldName="AffectedDate"
                                    VisibleIndex="0" Width="10%">
                                    <PropertiesDateEdit>
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField ErrorText="Chưa nhập ngày." IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Ngân Hàng" FieldName="BankId!Key" VisibleIndex="1"
                                    Width="20%">
                                    <PropertiesComboBox EnableCallbackMode="true" DataSourceID="DBBank" IncrementalFilteringMode="Contains"
                                        TextField="Code" TextFormatString="{0}" ValueField="BankId">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="Code" />
                                            <dx:ListBoxColumn FieldName="Description" />
                                        </Columns>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Tỉ giá" FieldName="Rate" VisibleIndex="2"
                                    Width="10%">
                                    <PropertiesSpinEdit>
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField ErrorText="Chưa nhập tỉ giá." IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesSpinEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataTextColumn Caption="Nội Tệ" Name="DenomiratorCurrencyId" VisibleIndex="3"
                                    Width="5%">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Quy Tắc Tính Tỷ Giá" FieldName="Description"
                                    VisibleIndex="4" Width="15%">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Diễn giải" ReadOnly="true" Name="Description_edit"
                                    VisibleIndex="5" Width="17%">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="Status" VisibleIndex="6"
                                    Width="8%">
                                    <PropertiesComboBox IncrementalFilteringMode="Contains">
                                        <Items>
                                            <dx:ListEditItem Text="Đang sử dụng" Value="1" />
                                            <dx:ListEditItem Text="Ngưng sử dụng" Value="0" />
                                        </Items>
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField ErrorText="Chưa chọn trạng thái." IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesComboBox>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="7"
                                    Width="10%">
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
                                    <CancelButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </CancelButton>
                                    <UpdateButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply" />
                                        </Image>
                                    </UpdateButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <SettingsBehavior ColumnResizeMode="Control" ConfirmDelete="True" />
                            <SettingsPager>
                                <PageSizeItemSettings Visible="True">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" />
                            <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa không?" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center">
                                </Header>
                            </Styles>
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
            </dx:ASPxGridView>
            <dx:XpoDataSource ID="DBExchangeRate" runat="server" Criteria="[NumeratorCurrencyId] = ?  And [RowStatus] > 0"
                DefaultSorting="" TypeName="NAS.DAL.Vouches.ExchangeRate">
                <CriteriaParameters>
                    <asp:SessionParameter DefaultValue="" Name="NumeratorCurrencyId" SessionField="SessionNumeratorCurrencyId" />
                </CriteriaParameters>
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="DBBank" runat="server" TypeName="NAS.DAL.Nomenclature.Bank.Bank">
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="DBCurrency" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Currency.Currency"
                Criteria="[RowStatus] > 0 And [IsDefault] = True">
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="DBCurrencyType" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Currency.CurrencyType"
                Criteria="[RowStatus] > 0 And [IsMaster] <> True">
            </dx:XpoDataSource>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
