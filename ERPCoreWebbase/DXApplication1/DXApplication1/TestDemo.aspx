<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TestDemo.aspx.cs"
    Inherits="WebModule.TestDemo" %>

<%@ Register Src="ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <script type="text/javascript">

        var lastCountry = null;

        function ItemId_SelectedIndexChanged(s, e) {
            var ItemId = s.GetSelectedItem();
            if (ItemId != null) {
                var Code = "Item|" + ItemId.GetColumnText('Code');
                if (Code != null) {
//                    if (grid.GetEditor("City").InCallback())
//                        lastCountry = s.GetValue().toString();
//                    else
                        Grid_ArtifactDetail.PerformCallback(Code);
                }
            }
        }
        function Grid_ArtifactDetail_UnitId_SelectedIndexChanged(s, e) {
            var Code = "Unit|" + s.GetText();
            Grid_ArtifactDetail.PerformCallback(Code);
        }
    </script>
    <dx:ASPxCallbackPanel ID="cpCurrency" runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="Grid_ArtifactDetail" runat="server" AutoGenerateColumns="False"
                    DataSourceID="DBLegalInvoiceArtifactDetail" KeyFieldName="LegalInvoiceArtifactDetailId"
                    OnCustomCallback="Grid_ArtifactDetail_CustomCallback" OnRowDeleting="Grid_ArtifactDetail_RowDeleting"
                    OnRowInserting="Grid_ArtifactDetail_RowInserting" OnRowUpdating="Grid_ArtifactDetail_RowUpdating"
                    Width="100%" 
                    OnCellEditorInitialize="Grid_ArtifactDetail_CellEditorInitialize">
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="Tổng Cộng : {0:n0}" FieldName="Total" SummaryType="Sum" />
                    </TotalSummary>
                    <GroupSummary>
                        <dx:ASPxSummaryItem SummaryType="Count" />
                    </GroupSummary>
                    <Columns>
                        <dx:GridViewDataComboBoxColumn Caption="Mã Hàng Hóa" FieldName="ItemId!Key" ShowInCustomizationForm="True"
                            SortIndex="0" SortOrder="Descending" VisibleIndex="0" Width="25%">
                            <PropertiesComboBox DataSourceID="DBItem" EnableCallbackMode="true" IncrementalFilteringMode="StartsWith"
                                TextField="Code" TextFormatString="{0}" ValueField="ItemId">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="ItemId" Visible="False"></dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Mã hàng hóa" FieldName="Code" Width="150px"></dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Tên hàng hóa" FieldName="Name" Width="200px"></dx:ListBoxColumn>
                                </Columns>
                                <ClientSideEvents SelectedIndexChanged="ItemId_SelectedIndexChanged"></ClientSideEvents>
                            </PropertiesComboBox>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá" FieldName="Price" ShowInCustomizationForm="True"
                            VisibleIndex="1" Width="10%">
                            <PropertiesSpinEdit DisplayFormatString="g">
                            </PropertiesSpinEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="Amount" ShowInCustomizationForm="True"
                            VisibleIndex="2" Width="10%">
                            <PropertiesSpinEdit DisplayFormatString="g">
                            </PropertiesSpinEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="UnitId!Key" ShowInCustomizationForm="True"
                            VisibleIndex="5" Width="10%">
                            <PropertiesComboBox DataSourceID="DBUnit" EnableCallbackMode="True" IncrementalFilteringMode="StartsWith"
                                TextField="Name" TextFormatString="{0}" ValueField="UnitId">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="UnitId" Visible="False"></dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Mã Đơn Vị" FieldName="Code" Width="150px"></dx:ListBoxColumn>
                                    <dx:ListBoxColumn Caption="Tên Đơn Vị" FieldName="Name" Width="200px"></dx:ListBoxColumn>
                                </Columns>
                                <ClientSideEvents EndCallback="" SelectedIndexChanged="Grid_ArtifactDetail_UnitId_SelectedIndexChanged">
                                </ClientSideEvents>
                            </PropertiesComboBox>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Thành Giá" FieldName="Total" ShowInCustomizationForm="True"
                            VisibleIndex="6" Width="15%">
                            <PropertiesSpinEdit DisplayFormatString="g">
                            </PropertiesSpinEdit>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                            VisibleIndex="7" Width="10%">
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
                            <CancelButton>
                                <Image ToolTip="Bỏ qua">
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </CancelButton>
                            <UpdateButton>
                                <Image ToolTip="Cập nhật">
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </UpdateButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                            </CellStyle>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsPager PageSize="20">
                        <PageSizeItemSettings Items="10, 20, 50" Visible="True">
                        </PageSizeItemSettings>
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowFooter="True" />
                    <SettingsText ConfirmDelete="Bạn Có Chắc Xóa Không?" />
                </dx:ASPxGridView>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:XpoDataSource ID="DBLegalInvoiceArtifactDetail" runat="server" TypeName="NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactDetail"
        Criteria="[RowStatus] &gt; 0s">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="DBItem" runat="server" TypeName="NAS.DAL.Nomenclature.Item.Item"
        Criteria="[RowStatus] > 0">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="DBUnit" runat="server" TypeName="NAS.DAL.Nomenclature.Item.ItemUnit"
        Criteria="[RowStatus] > 0 And [UnitId] = ?">
        <CriteriaParameters>
            <asp:Parameter Name="UnitId"  />
        </CriteriaParameters>
    </dx:XpoDataSource>
</asp:Content>
