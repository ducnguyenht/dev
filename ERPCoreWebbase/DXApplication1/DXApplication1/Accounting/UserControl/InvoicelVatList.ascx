<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoicelVatList.ascx.cs" Inherits="WebModule.Accounting.UserControl.InvoicelVatList" %>
<%@ Register src="InvoiceVatReport.ascx" tagname="InvoiceVatReport" tagprefix="uc1" %>
<style type="text/css">
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }      
    .dxgvControl, .dxgvDisabled
    {
        border: 1px Solid #9F9F9F;
        font: 12px Tahoma, Geneva, sans-serif;
        background-color: #F2F2F2;
        color: Black;
        cursor: default;
    }
    .dxgvTable
    {
        -webkit-tap-highlight-color: rgba(0,0,0,0);
    }
    
    .dxgvTable
    {
        background-color: White;
        border-width: 0;
        border-collapse: separate !important;
        overflow: hidden;
        color: Black;
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
    
    .dxgvPagerTopPanel, .dxgvPagerBottomPanel
    {
        padding-top: 4px;
        padding-bottom: 4px;
    }
</style>
<script type="text/javascript">
    function buttonShowReport_Click(s, e) {
                    
        formInvoiceVatReport.PerformCallback();
    }

    function buttonInvoiceVatListCancel_Click(s, e) {
        formInvoiceVatList.Hide();
    }

    function ShowInvoiceVatList(name, period) {
        ReportHiddenField.Clear();
        ReportHiddenField.Set("typeReport", name);
        cpInvoiceVatList.PerformCallback(name + '|' + period);

        if (name == '01-1/GTGT') {
            formInvoiceVatList.SetHeaderText('BẢNG KÊ HOÁ ĐƠN, CHỨNG TỪ HÀNG HOÁ, DỊCH VỤ BÁN RA');
        }
        else {
            formInvoiceVatList.SetHeaderText('BẢNG KÊ HOÁ ĐƠN, CHỨNG TỪ HÀNG HOÁ, DỊCH VỤ MUA VÀO');
        }
        formInvoiceVatList.Show();
    }

    function cpInvoiceVatList_EndCallback(s, e) {
        
    }

</script>

<dx:ASPxPopupControl ID="formInvoiceVatList" runat="server" 
    HeaderText="BẢNG KÊ HOÁ ĐƠN, CHỨNG TỪ HÀNG HOÁ, DỊCH VỤ" Height="572px" 
    Maximized="True" Modal="True" RenderMode="Lightweight" ShowFooter="True" 
    Width="966px" ClientInstanceName="formInvoiceVatList" 
    CloseAction="CloseButton" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" ScrollBars="Auto">
    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
    <FooterContentTemplate>
        <div id="Footer" style="display: inline; width: 100%;">            
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonInvoiceVatListCancel" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                    ClientInstanceName="buttonInvoiceVatListCancel" Text="Thoát" Wrap="False" 
                    ToolTip="Thoát  - Ctrl + C">
                    <ClientSideEvents Click="buttonInvoiceVatListCancel_Click" />
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonShowReport" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                    ClientInstanceName="buttonShowReport" Text="In bảng kê" Wrap="False" 
                    ToolTip="">
                    <ClientSideEvents Click="buttonShowReport_Click" />
                    <Image>
                        <SpriteProperties CssClass="Sprite_Print" />
                    </Image>
                </dx:ASPxButton>
            </div>
        </div>
    </FooterContentTemplate>
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxCallbackPanel ID="cpInvoiceVatList" runat="server" 
        ClientInstanceName="cpInvoiceVatList" OnCallback="cpInvoiceVatList_Callback" 
        Width="100%">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="grdBillTaxClaim" runat="server" 
                    AutoGenerateColumns="False" ClientInstanceName="grdBillTaxClaim" 
                    Width="100%" KeyFieldName="BillCode">
                    <ClientSideEvents EndCallback="cpInvoiceVatList_EndCallback" />
<ClientSideEvents EndCallback="cpInvoiceVatList_EndCallback"></ClientSideEvents>
                    <TotalSummary>
                        <dx:ASPxSummaryItem DisplayFormat="Tổng :" FieldName="SeriesNumber" 
                            SummaryType="Count" />
                        <dx:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="Amount" 
                            SummaryType="Sum" />
                        <dx:ASPxSummaryItem DisplayFormat="{0:n0}" FieldName="TaxInNumber" 
                            SummaryType="Sum" />
                    </TotalSummary>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Ký hiệu hóa đơn" 
                            ShowInCustomizationForm="True" VisibleIndex="0" FieldName="SeriesNumber" 
                            Width="100px">
                            <PropertiesTextEdit DisplayFormatString="n0">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Số hóa đơn" ShowInCustomizationForm="True" 
                            VisibleIndex="1" FieldName="BillCode" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="n0">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="Ngày tháng năm phát hành" 
                            FieldName="CreateDate" ShowInCustomizationForm="True" VisibleIndex="2" 
                            Width="100px">
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="Tên người bán" ShowInCustomizationForm="True" 
                            VisibleIndex="3" FieldName="ObjectName" Width="200px">
                            <PropertiesTextEdit DisplayFormatString="n0">
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mã số thuế người bán" 
                            ShowInCustomizationForm="True" VisibleIndex="4" 
                            FieldName="ObjectTax" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mặt hàng" ShowInCustomizationForm="True" 
                            VisibleIndex="5" FieldName="ClaimItem" Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Giá trị HHDV mua vào chưa có thuế" 
                            ShowInCustomizationForm="True" VisibleIndex="6" Width="120px" 
                            FieldName="Amount">
<PropertiesTextEdit DisplayFormatString="n0"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thuế suất (%)" ShowInCustomizationForm="True" 
                            VisibleIndex="7" FieldName="TaxInPercentage" Width="70px">
<PropertiesTextEdit DisplayFormatString="n0"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thuế GTGT" 
                            ShowInCustomizationForm="True" VisibleIndex="8" 
                            FieldName="TaxInNumber" Width="100px">
<PropertiesTextEdit DisplayFormatString="n0"></PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ghi chú hoặc thời gian thanh toán trả chậm" 
                            ShowInCustomizationForm="True" VisibleIndex="9" FieldName="Comment" 
                            Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Phân loại hóa đơn" 
                            FieldName="LegalInvoiceArtifactTypeName" GroupIndex="0" 
                            ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" 
                            VisibleIndex="10">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" 
                        AutoExpandAllGroups="True" />

<SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="True" AutoExpandAllGroups="True"></SettingsBehavior>

                    <SettingsPager ShowEmptyDataRows="True" PageSize="50">
                    </SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <Settings HorizontalScrollBarMode="Visible" ShowFilterRow="True" 
                        ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" 
                        VerticalScrollableHeight="500" VerticalScrollBarMode="Visible" 
                        ShowFooter="True" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterRowMenuLikeItem="True" ShowFooter="True" VerticalScrollableHeight="500" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible"></Settings>

                    <Styles>
                        <Header Font-Bold="True" horizontalalign="Center" wrap="True">
                        </Header>
                        <grouprow font-bold="True">
                        </grouprow>
                        <Cell Wrap="False">
                        </Cell>
                        <footer font-bold="True">
                        </footer>
                        <grouppanel font-bold="True">
                        </grouppanel>
                    </Styles>
                </dx:ASPxGridView>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

<dx:XpoDataSource ID="BillTaxClaimXDS" runat="server">
</dx:XpoDataSource>


<uc1:InvoiceVatReport ID="InvoiceVatReport1" runat="server" />



