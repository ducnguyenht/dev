<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="BookingEntriesForm_Backup.ascx.cs" Inherits="WebModule.Invoice.PurchaseInvoice.Control.BookingEntriesForm.BookingEntriesForm_Backup" %>
<%@ Register src="../../../../Accounting/Journal/Transaction/Control/GridViewBookingEntries.ascx" tagname="GridViewBookingEntries" tagprefix="uc1" %>
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
    .style1
    {
        font-weight: bold;
    }
</style>
<script type="text/javascript">
    function BookingEntriesFromBill(BillId) {
        formBookingEntriesForm.Show();
        cpBookingEntriesForm.PerformCallback("show|"+BillId);        
    }

    function buttonBookingEntries_Click(s, e) {
        cpBookingEntriesForm.PerformCallback("booking");
    }

    function buttonBookingEntriesFormCancel_Click(s, e) {
        formBookingEntriesForm.Hide();
    }

    function cpBookingEntriesForm_EndCallback(s, e) {
        if (s.cpInvalidAccount) {
            alert(s.cpInvalidAccount);
            delete (cpInvalidAccount);
        }

        if (s.cpNotBalance) {
            alert(s.cpNotBalance);
            delete (cpNotBalance);
        }
    }

</script>

<dx:ASPxCallbackPanel ID="cpBookingEntriesForm" runat="server" 
    ClientInstanceName="cpBookingEntriesForm" Width="100%" 
    oncallback="cpBookingEntriesForm_Callback">
    <ClientSideEvents EndCallback="cpBookingEntriesForm_EndCallback" />
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formBookingEntriesForm" runat="server" 
        ClientInstanceName="formBookingEntriesForm" CloseAction="CloseButton" 
        HeaderText="Hạch toán" Height="581px" Maximized="True" Modal="True" 
        RenderMode="Lightweight" ShowFooter="True" Width="949px">
          <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
        <FooterContentTemplate>
            <div id="Footer" style="display: inline; width: 100%;">            
                <div style="display: inline; float: right;">
                    <dx:ASPxButton ID="buttonBookingEntriesFormCancel" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                        ClientInstanceName="buttonBookingEntriesFormCancel" Text="Thoát" Wrap="False" 
                        ToolTip="Thoát  - Ctrl + C">
                        <ClientSideEvents Click="buttonBookingEntriesFormCancel_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div style="display: inline; float: right;">
                    <dx:ASPxButton ID="buttonBookingEntries" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                        ClientInstanceName="buttonBookingEntries" Text="Ghi sổ" Wrap="False" 
                        ToolTip="">
                        <ClientSideEvents Click="buttonBookingEntries_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Balance" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
        </FooterContentTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3">
                    <Items>
                        <dx:LayoutItem Caption="Số phiếu">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblCode" ClientInstanceName="lblCode" runat="server" Text="" 
                                        CssClass="style1" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Ngày lập">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblIssuedDate" ClientInstanceName="lblIssuedDate" 
                                        runat="server" Text="" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Nhà cung cấp">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblSupplier" ClientInstanceName="lblSupplier" runat="server" 
                                        Text="" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tổng tiền hàng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblSumOfItemPrice" ClientInstanceName="lblSumOfItemPrice" 
                                        runat="server" Text="" CssClass="style1" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tiền chiết khấu">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblSumOfPromotion" ClientInstanceName="lblSumOfPromotion" 
                                        runat="server" Text="" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tiền thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblSumOfTax" ClientInstanceName="lblSumOfTax" runat="server" 
                                        Text="" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tổng cộng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblTotal" ClientInstanceName="lblTotal" runat="server" 
                                        Text="" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ColSpan="3" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <uc1:GridViewBookingEntries ID="GridViewBookingEntries1" runat="server" />
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>

