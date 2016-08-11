<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="DeclareVat.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.DeclareVat" %>
<style type="text/css">
    .dxgvControl
    {
        border: 1px Solid #9F9F9F;
        font: 12px Tahoma, Geneva, sans-serif;
        background-color: #F2F2F2;
        color: Black;
        cursor: default;
    }
    
    .dxgvTable
    {
        background-color: White;
        border-width: 0;
        border-collapse: separate !important;
        overflow: hidden;
        color: Black;
    }
    .dxgvTable
    {
        -webkit-tap-highlight-color: rgba(0,0,0,0);
    }
    
    .dxgvHeader
    {
        cursor: pointer;
        white-space: nowrap;
        padding: 4px 6px 5px;
        border: 1px Solid #9F9F9F;
        background-color: #DCDCDC;
        overflow: hidden;
        font-weight: normal;
        text-align: left;
    }
    
    .dxgvPagerBottomPanel
    {
        padding-top: 4px;
        padding-bottom: 4px;
    }
</style>
<script type="text/javascript">
    var billId;

    function DeclareVatFromBill(BillId) {
        billId = BillId;
        cpDeclareVat.PerformCallback('show|' + billId);
        formDeclareVat.Show();
        ASPxClientEdit.ClearEditorsInContainerById('DeclareVatContainer');
    }

    function buttonAccept_Click(s, e) {
        if (!ASPxClientEdit.ValidateEditorsInContainerById('DeclareVatContainer')) {
            e.processOnServer = false;
            return;
        }
        cpDeclareVat.PerformCallback('declare|' + billId);
        alert('Đã lưu kê khai thuế');
    }

    function buttonCancel_Click(s, e) {
        ASPxClientEdit.ClearEditorsInContainerById('DeclareVatContainer');
        formDeclareVat.Hide();
    }

    function cpDeclareVat_EndCallback(s, e) {
        if (s.cpDisableAccept) {
            buttonAccept.SetEnabled(false);
            delete (s.cpDisableAccept);
        }
    }

</script>
<div id="DeclareVatContainer" style="width:100%">
    <dx:ASPxPopupControl ID="formDeclareVat" runat="server" CloseAction="CloseButton"
        Height="500px" Modal="True" RenderMode="Lightweight" Width="1100px" HeaderText="Kê khai thuế"
        ClientInstanceName="formDeclareVat" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" ScrollBars="Auto" AllowDragging="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxCallbackPanel ID="cpDeclareVat" runat="server" ClientInstanceName="cpDeclareVat"
                    Width="100%" OnCallback="cpDeclareVat_Callback" ShowLoadingPanelImage="False">
                    <ClientSideEvents EndCallback="cpDeclareVat_EndCallback" />
                    <PanelCollection>
                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                                <Items>
                                    <dx:LayoutGroup Caption="Thông tin kê khai" ShowCaption="False">
                                        <Items>
                                            <dx:LayoutItem Caption="Phân loại hóa đơn">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxComboBox ID="cboLegalInvoiceArtifactType" runat="server" ClientInstanceName="cboLegalInvoiceArtifactType"
                                                            OnItemRequestedByValue="cboLegalInvoiceArtifactType_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cboLegalInvoiceArtifactType_ItemsRequestedByFilterCondition"
                                                            Width="300px" EnableCallbackMode="True" IncrementalFilteringMode="Contains" TextField="Code"
                                                            TextFormatString="{0} - {1}" ValueField="LegalInvoiceArtifactTypeId" ValueType="System.Guid">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	buttonAccept.SetEnabled(true);
}" />
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="Mã số phân loại" FieldName="Code" Width="100px" />
                                                                <dx:ListBoxColumn Caption="Tên phân loại" FieldName="Name" Width="200px" />
                                                            </Columns>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True">
                                                                <RequiredField ErrorText="Chưa chọn phân loại hóa đơn !" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Ký hiệu hóa đơn">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="txtSeriesNumber" runat="server" ClientInstanceName="txtSeriesNumber"
                                                            Width="300px">
                                                            <ClientSideEvents TextChanged="function(s, e) {
	buttonAccept.SetEnabled(true);
}" />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Số hóa đơn">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="txtIssuedArtifactCode" runat="server" ClientInstanceName="txtIssuedArtifactCode"
                                                            Width="300px">
                                                            <ClientSideEvents TextChanged="function(s, e) {
	buttonAccept.SetEnabled(true);
}" />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tên Hàng hóa/Dịch vụ">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxComboBox ID="cboItem" runat="server" OnItemRequestedByValue="cboItem_ItemRequestedByValue"
                                                            OnItemsRequestedByFilterCondition="cboItem_ItemsRequestedByFilterCondition" Width="300px"
                                                            TextField="ItemUnitId.ItemId.Name" TextFormatString="{1}" ValueField="ItemUnitId.ItemId.ItemId"
                                                            ValueType="System.Guid">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	buttonAccept.SetEnabled(true);
}" />
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="Mã hàng hóa (dịch vụ)" FieldName="ItemUnitId.ItemId.Code"
                                                                    Width="150px" />
                                                                <dx:ListBoxColumn Caption="Tên hàng hóa (dịch vụ)" FieldName="ItemUnitId.ItemId.Name"
                                                                    Width="200px" />
                                                            </Columns>
                                                            <ValidationSettings SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField ErrorText="Chưa chọn HHDV !" IsRequired="True" />
                                                                <RequiredField IsRequired="True" ErrorText="Chưa chọn HHDV !"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Giá trị HHDV mua vào/bán ra">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxLabel ID="lblAmount" runat="server" Text="" Font-Bold="True">
                                                        </dx:ASPxLabel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Thuế suất(%)">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxLabel ID="lblTaxPercent" runat="server" Text="" Font-Bold="True">
                                                        </dx:ASPxLabel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Thuế GTGT">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxLabel ID="lblTaxAmount" runat="server" Text="" Font-Bold="True">
                                                        </dx:ASPxLabel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Ghi chú">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="txtDescription" runat="server" Width="300px">
                                                            <ClientSideEvents TextChanged="function(s, e) {
	buttonAccept.SetEnabled(true);
}" />
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxGridView ID="grdBillTaxClaim" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="grdBillTaxClaim" KeyFieldName="BillCode" Width="100%">
                                                            <TotalSummary>
                                                                <dx:ASPxSummaryItem DisplayFormat="Tổng :" FieldName="SeriesNumber" SummaryType="Count" />
                                                                <dx:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="Amount" SummaryType="Sum" />
                                                                <dx:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="TaxInNumber" SummaryType="Sum" />
                                                            </TotalSummary>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Ký hiệu HĐ" FieldName="SeriesNumber" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Width="80px">
                                                                    <PropertiesTextEdit DisplayFormatString="f0">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số HĐ" FieldName="BillCode" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1" Width="80px">
                                                                    <PropertiesTextEdit DisplayFormatString="n0">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn Caption="Ngày tháng năm phát hành" FieldName="CreateDate"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên người bán" FieldName="ObjectName" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" Width="150px">
                                                                    <PropertiesTextEdit DisplayFormatString="f0">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã số thuế người bán" FieldName="ObjectTax" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4" Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mặt hàng" FieldName="ClaimItem" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5" Width="150px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Giá trị HHDV mua vào chưa có thuế" FieldName="Amount"
                                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                                    <PropertiesTextEdit DisplayFormatString="n0">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Thuế suất" FieldName="TaxInPercentage" ShowInCustomizationForm="True"
                                                                    VisibleIndex="7" Width="50px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Thuế GTGT" FieldName="TaxInNumber" ShowInCustomizationForm="True"
                                                                    VisibleIndex="8" Width="100px">
                                                                    <PropertiesTextEdit DisplayFormatString="n0">
                                                                    </PropertiesTextEdit>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Ghi chú hoặc thời gian thanh toán trả chậm" FieldName="Comment"
                                                                    ShowInCustomizationForm="True" VisibleIndex="9" Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Phân loại HĐ" FieldName="LegalInvoiceArtifactTypeName"
                                                                    ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" ColumnResizeMode="Control" />
                                                            <SettingsPager PageSize="1" ShowEmptyDataRows="True">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Inline" />
                                                            <Settings HorizontalScrollBarMode="Visible" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True"
                                                                ShowFooter="True" />
                                                            <Styles>
                                                                <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
                                                                </Header>
                                                                <Cell Wrap="False">
                                                                </Cell>
                                                            </Styles>
                                                        </dx:ASPxGridView>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False" HorizontalAlign="Center" Width="200px">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <div style="" align="center">
                                                            <div style="float: left; margin-right: 5px; text-align: center;">
                                                                <dx:ASPxButton ID="buttonAccept" runat="server" Text="Lưu lại" AutoPostBack="False"
                                                                    UseSubmitBehavior="False" ClientInstanceName="buttonAccept">
                                                                    <ClientSideEvents Click="buttonAccept_Click" />
                                                                    <Image ToolTip="Lưu lại">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                    </Image>
                                                                </dx:ASPxButton>
                                                            </div>
                                                            <div style="float: left; text-align: center;">
                                                                <dx:ASPxButton ID="buttonCancel" runat="server" Text="Thoát" AutoPostBack="False"
                                                                    UseSubmitBehavior="False">
                                                                    <ClientSideEvents Click="buttonCancel_Click" />
                                                                    <Image ToolTip="Thoát">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                    </Image>
                                                                </dx:ASPxButton>
                                                            </div>
                                                        </div>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                                <SettingsItems HorizontalAlign="Left" />
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>
