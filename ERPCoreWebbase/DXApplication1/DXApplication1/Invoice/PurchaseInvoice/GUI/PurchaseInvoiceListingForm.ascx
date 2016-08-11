<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="PurchaseInvoiceListingForm.ascx.cs"
    Inherits="WebModule.Invoice.PurchaseInvoice.GUI.PurchaseInvoiceListingForm" %>
<%@ Register Src="PurchaseInvoiceEditingForm.ascx" TagName="PurchaseInvoiceEditingForm"
    TagPrefix="uc1" %>
<script type="text/javascript">
    function grdPurchase_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'buttonNew':
                purchaseInvoiceEditingForm.Show();
                break;
            case 'buttonEdit':
                var recordId = s.GetRowKey(e.visibleIndex);
                purchaseInvoiceEditingForm.Show(recordId);
                break;
            case 'buttonCopy':
                var copyConfirm = confirm('Bạn có chắc chắn muốn sao chép phiếu mua hàng này?');
                if (copyConfirm == true) {
                    var recordId = s.GetRowKey(e.visibleIndex);
                    if (!grdPurchase.InCallback()) {
                        grdPurchase.PerformCallback('Copy|' + recordId);
                    } 
                }
                break;
            case 'buttonDelete':
                var deleteConfirm = confirm('Bạn có chắc chắn muốn xóa phiếu mua hàng này?');
                if (deleteConfirm == true) {
                    var recordId = s.GetRowKey(e.visibleIndex);
                    if (!grdPurchase.InCallback()) {
                        grdPurchase.PerformCallback('Delete|' + recordId);
                    } 
                }
                break;
            default:
                break;
        }
    }
</script>
<dx:ASPxGridView runat="server" ClientInstanceName="grdPurchase" KeyFieldName="BillId"
    AutoGenerateColumns="False" KeyboardSupport="True" DataSourceID="dsPurchaseInvoice"
    Width="100%" ID="grdPurchase" OnCustomCallback="grdPurchase_CustomCallback">
    <Templates>
        <EmptyDataRow>
            <div style="margin: 0 auto">
                <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                    <ClientSideEvents Click="function(s, e) {
                                    purchaseInvoiceEditingForm.Show();
                                }" />
                </dx:ASPxImage>
                <br />
            </div>
        </EmptyDataRow>
    </Templates>
    <ClientSideEvents CustomButtonClick="grdPurchase_CustomButtonClick"></ClientSideEvents>
    <Columns>
        <dx:GridViewDataTextColumn FieldName="SourceOrganizationId.Name" Caption="Nhà cung cấp"
            VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Width="100px" Caption="Thao tác" VisibleIndex="10">
            <ClearFilterButton Visible="True">
                <Image>
                    <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                </Image>
            </ClearFilterButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="buttonNew">
                    <Image ToolTip="Thêm">
                        <SpriteProperties CssClass="Sprite_New" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
                <dx:GridViewCommandColumnCustomButton ID="buttonEdit">
                    <Image ToolTip="Chỉnh sửa">
                        <SpriteProperties CssClass="Sprite_Edit" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
                <dx:GridViewCommandColumnCustomButton ID="buttonDelete">
                    <Image ToolTip="Xóa">
                        <SpriteProperties CssClass="Sprite_Delete" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
                <dx:GridViewCommandColumnCustomButton ID="buttonCopy">
                    <Image ToolTip="Sao chép">
                        <SpriteProperties CssClass="Sprite_Copy" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="Code" Name="Code" Caption="Số phiếu mua" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn FieldName="CreateDate" Caption="Ngày" VisibleIndex="1">
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="Total" Caption="Tổng giá trị" VisibleIndex="8">
            <PropertiesTextEdit DisplayFormatString="{0:#,###}">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tổng tiền hàng" FieldName="SumOfItemPrice" VisibleIndex="3">
            <PropertiesTextEdit DisplayFormatString="{0:#,###}">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tổng tiền chiết khấu" FieldName="SumOfPromotion"
            VisibleIndex="4">
            <PropertiesTextEdit DisplayFormatString="{0:#,###}">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tổng tiền thuế" FieldName="SumOfTax" VisibleIndex="6">
            <PropertiesTextEdit DisplayFormatString="{0:#,###}">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior AllowGroup="False" AllowFocusedRow="True" AllowSelectByRowClick="True"
        AllowSelectSingleRowOnly="True" ConfirmDelete="True"></SettingsBehavior>
    <SettingsEditing Mode="Inline"></SettingsEditing>
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
    </Settings>
    <Styles>
        <Header HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" Wrap="True">
        </Header>
        <Cell Wrap="True">
        </Cell>
        <HeaderPanel HorizontalAlign="Center">
        </HeaderPanel>
        <CommandColumn Spacing="4px" HorizontalAlign="Center">
        </CommandColumn>
    </Styles>
</dx:ASPxGridView>
<dx:XpoDataSource ID="dsPurchaseInvoice" runat="server" TypeName="NAS.DAL.Invoice.PurchaseInvoice">
</dx:XpoDataSource>
<uc1:PurchaseInvoiceEditingForm ID="purchaseInvoiceEditingForm" runat="server" ClientInstanceName="purchaseInvoiceEditingForm"
    Closing="function(s, e) { grdPurchase.Refresh(); }" BillType="PRODUCT" />
