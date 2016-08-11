<%@ Control ClientIDMode="AutoID" Language="C#" AutoEventWireup="true" CodeBehind="BillTotalSummary.ascx.cs"
    Inherits="WebModule.Invoice.Control.BillTotalSummary.BillTotalSummary" %>
<style type="text/css">
    .style1
    {
        width: 181px;
    }
</style>
<script type="text/javascript">

    var BillTotalSummary = function () {
        this.PromotionInfoUpdated = new NASClientEvent();
        this.RaisePromotionInfoUpdated = function()
        {
            this.PromotionInfoUpdated.FireEvent(this, null);
        };

        this.TaxInfoUpdated = new NASClientEvent();
        this.RaiseTaxInfoUpdated = function()
        {
            this.TaxInfoUpdated.FireEvent(this, null);
        };

        this.PromotionInfoClosing = new NASClientEvent();
        this.RaisePromotionInfoClosing = function()
        {
            this.PromotionInfoClosing.FireEvent(this, null);
        };

        this.TaxInfoClosing = new NASClientEvent();
        this.RaiseTaxInfoClosing = function()
        {
            this.TaxInfoClosing.FireEvent(this, null);
        };
    };
    
    BillTotalSummary.prototype.Refresh = function()
    {
        if(!panelBillTotalSummary.InCallback())
        {
            panelBillTotalSummary.PerformCallback();
        }
    };

    var nasObj = new BillTotalSummary();
    window['<%= _ClientInstanceName %>'] = nasObj;

    <% if(PromotionInfoUpdated != null && PromotionInfoUpdated.Trim().Length > 0) { %>
        nasObj.PromotionInfoUpdated.AddHandler(<%= PromotionInfoUpdated %>);
    <% } %>

    <% if(TaxInfoUpdated != null && TaxInfoUpdated.Trim().Length > 0) { %>
        nasObj.TaxInfoUpdated.AddHandler(<%= TaxInfoUpdated %>);
    <% } %>

    <% if(PromotionInfoClosing != null && PromotionInfoClosing.Trim().Length > 0) { %>
        nasObj.PromotionInfoClosing.AddHandler(<%= PromotionInfoClosing %>);
    <% } %>

    <% if(TaxInfoClosing != null && TaxInfoClosing.Trim().Length > 0) { %>
        nasObj.TaxInfoClosing.AddHandler(<%= TaxInfoClosing %>);
    <% } %>

    function popupBillPromotionInfo_Closing(s, e)
    {
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.Refresh();
        tempObj.RaisePromotionInfoClosing();
    }

    function popupBillTaxInfo_Closing(s, e)
    {
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.Refresh();
        tempObj.RaiseTaxInfoClosing();
    }

    var isUpdateBillPromotionInfo = true;
    function UpdateBillPromotionInfo() {
        if (panelBillPromotionInfo.InCallback())
            isUpdateBillPromotionInfo = false;
        else
            panelBillPromotionInfo.PerformCallback('Update');
    }

    function panelBillPromotionInfo_EndCallback(s, e)
    {
        if(!isUpdateBillPromotionInfo)
        {
            panelBillPromotionInfo.PerformCallback('Update');
            isUpdateBillPromotionInfo = true;
        }

        if(s.cpEvent == 'PromotionInfoUpdated')
        {
            var tempObj = window['<%= _ClientInstanceName %>'];
            tempObj.RaisePromotionInfoUpdated();
            delete s.cpEvent;
        }
    }

    function spinPromotionOnBillByPercentage_LostFocus(s, e) 
    {
        if(radPromotionOnBillByPercentage.GetChecked())
        {
            Validate_spinPromotionOnBillByPercentage();
            if(s.GetIsValid()) {
                if (panelBillPromotionInfo.InCallback())
                    isUpdateBillPromotionInfo = false;
                else
                    panelBillPromotionInfo.PerformCallback('UpdatePromotionOnBillByPercentage');
            }
        }
    }

    function spinPromotionOnBillByAmount_LostFocus(s, e) 
    {
        if(radPromotionOnBillByAmount.GetChecked())
        {
            Validate_spinPromotionOnBillByAmount();
            if(s.GetIsValid()) {
                if (panelBillPromotionInfo.InCallback())
                    isUpdateBillPromotionInfo = false;
                else
                    panelBillPromotionInfo.PerformCallback('UpdatePromotionOnBillByAmount');
            }
        }
    }

    function spinPromotionOnBillByPercentage_NumberChanged(s, e)
    {
        var percentage = s.GetNumber();
        var subtotal = parseFloat(hfSubtotal.Get("subtotal"));
        if(percentage >= 0) {
            spinPromotionOnBillByAmount.SetValue((percentage * subtotal)/100);
        }
    }

    function Validate_spinPromotionOnBillByPercentage()
    {
        try {
            //Check greater or equal 0
            var num = spinPromotionOnBillByPercentage.GetNumber();
            if(num < 0)
            {
                spinPromotionOnBillByPercentage.SetIsValid(false);
                spinPromotionOnBillByPercentage.SetErrorText("Phần trăm chiết khấu phải >= 0");
                return false;
            }
            return true;
        } catch (e) {
    
        }
    }

    function Validate_spinPromotionOnBillByAmount()
    {
        try {
            var num = spinPromotionOnBillByAmount.GetNumber();
            //Check greater or equal 0
            if(num < 0)
            {
                spinPromotionOnBillByAmount.SetIsValid(false);
                spinPromotionOnBillByAmount.SetErrorText("Số tiền chiết khấu phải >= 0");
                return false;
            }
            return true;
        } catch (e) {
    
        }
    }

    var isUpdateBillTaxInfo = true;

    function UpdateBillTaxInfo() {
        if (panelBillTaxInfo.InCallback())
            isUpdateBillTaxInfo = false;
        else
            panelBillTaxInfo.PerformCallback('Update');
    }

    function panelBillTaxInfo_EndCallback(s, e)
    {
        if(!isUpdateBillTaxInfo)
        {
            panelBillTaxInfo.PerformCallback('Update');
            isUpdateBillTaxInfo = true;
        }

        if(s.cpEvent == 'TaxInfoUpdated')
        {
            var tempObj = window['<%= _ClientInstanceName %>'];
            tempObj.RaiseTaxInfoUpdated();
            delete s.cpEvent;
        }
    }

</script>
<dx:ASPxCallbackPanel ID="panelBillTotalSummary" ClientInstanceName="panelBillTotalSummary"
    runat="server" Width="100%" OnCallback="panelBillTotalSummary_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <div style="float: right; min-width: 240px">
                <dx:ASPxFormLayout ID="formlayoutBillTotalSummary" runat="server" Width="100%">
                    <Items>
                        <dx:LayoutItem Caption="Tổng tiền hàng" HorizontalAlign="Right" VerticalAlign="Middle">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblSubtotal" runat="server" Text="0">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <CaptionStyle Font-Bold="True">
                            </CaptionStyle>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Chiết khấu" HorizontalAlign="Right" VerticalAlign="Middle">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxHyperLink CssClass="v-align" ID="hyperlinkConfigBillPromotion" runat="server"
                                        Cursor="pointer" ImageUrl="~/images/icon/Edit/Edit_16x16.png" ToolTip="Chỉnh sửa">
                                        <ClientSideEvents Click="function(s, e) { 
                                            popupBillPromotionInfo.Show(); 
                                            if (!panelBillPromotionInfo.InCallback())
                                                panelBillPromotionInfo.PerformCallback('Edit'); }" />
<ClientSideEvents Click="function(s, e) { 
                                            popupBillPromotionInfo.Show(); 
                                            if (!panelBillPromotionInfo.InCallback())
                                                panelBillPromotionInfo.PerformCallback(&#39;Edit&#39;); }"></ClientSideEvents>
                                    </dx:ASPxHyperLink>
                                    &nbsp;
                                    <dx:ASPxLabel ID="lblPromotionTotal" runat="server" Text="0">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <CaptionStyle Font-Bold="True">
                            </CaptionStyle>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Thuế" HorizontalAlign="Right" VerticalAlign="Middle">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxHyperLink CssClass="v-align" ID="hyperlinkConfigBillTax" runat="server" Cursor="pointer"
                                        ImageUrl="~/images/icon/Edit/Edit_16x16.png" ToolTip="Chỉnh sửa">
                                        <ClientSideEvents Click="function(s, e) { 
                                                popupBillTaxInfo.Show(); 
                                                if(!panelBillTaxInfo.InCallback())
                                                    panelBillTaxInfo.PerformCallback('Edit');
                                            }" />
<ClientSideEvents Click="function(s, e) { 
                                                popupBillTaxInfo.Show(); 
                                                if(!panelBillTaxInfo.InCallback())
                                                    panelBillTaxInfo.PerformCallback(&#39;Edit&#39;);
                                            }"></ClientSideEvents>
                                    </dx:ASPxHyperLink>
                                    &nbsp;
                                    <dx:ASPxLabel ID="lblTaxTotal" runat="server" Text="0">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <CaptionStyle Font-Bold="True" Font-Italic="False">
                            </CaptionStyle>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tổng cộng" HorizontalAlign="Right">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblTotal" runat="server" Font-Size="16px" Text="0" Font-Bold="True">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <BorderTop BorderStyle="Solid" BorderWidth="1px" />

<BorderTop BorderWidth="1px" BorderStyle="Solid"></BorderTop>

                            <CaptionStyle Font-Bold="True" Font-Size="16px">
                            </CaptionStyle>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đã trả" HorizontalAlign="Right">
                            <layoutitemnestedcontrolcollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblPaid" runat="server" Text="0">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </layoutitemnestedcontrolcollection>
                            <captionstyle font-bold="True">
                            </captionstyle>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Còn nợ" HorizontalAlign="Right">
                            <layoutitemnestedcontrolcollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblOwned" runat="server" Font-Bold="True" Font-Size="16px" 
                                        Text="0">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </layoutitemnestedcontrolcollection>
                            <bordertop borderstyle="Solid" borderwidth="1px" />
                            <captionstyle font-bold="True" font-size="16px">
                            </captionstyle>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </div>
            <div style="clear: both; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:ASPxPopupControl ID="popupBillPromotionInfo" ClientInstanceName="popupBillPromotionInfo"
    runat="server" AllowDragging="True" AllowResize="True" AutoUpdatePosition="True"
    CloseAction="CloseButton" HeaderText="Thông tin chiết khấu hóa đơn" Height="300px"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    RenderMode="Lightweight" Width="460px">
    <ClientSideEvents Closing="popupBillPromotionInfo_Closing" />
    <ModalBackgroundStyle BackColor="Transparent">
    </ModalBackgroundStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxCallbackPanel ID="panelBillPromotionInfo" ClientInstanceName="panelBillPromotionInfo"
                runat="server" Width="100%" OnCallback="panelBillPromotionInfo_Callback">
                <ClientSideEvents EndCallback="panelBillPromotionInfo_EndCallback" />
                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <table border="0">
                            <tr>
                                <td>
                                    <dx:ASPxRadioButton ID="radPromotionOnItems" Checked="true" Text="Chiết khấu theo mặt hàng"
                                        GroupName="PromotionCalculationType" runat="server">
                                        <ClientSideEvents CheckedChanged="function(s, e) { 
                                            if(s.GetChecked()) { 
                                                UpdateBillPromotionInfo();
                                            } }" />
                                    </dx:ASPxRadioButton>
                                </td>
                                <td>
                                    <dx:ASPxRadioButton ID="radPromotionOnBill" Text="Chiết khấu trên toàn hóa đơn" GroupName="PromotionCalculationType"
                                        runat="server">
                                        <ClientSideEvents CheckedChanged="function(s, e) { 
                                            if(s.GetChecked()) { 
                                                UpdateBillPromotionInfo();
                                            } }" />
                                    </dx:ASPxRadioButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" Width="100%" runat="server">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chiết khấu">
                                    <Items>
                                        <dx:LayoutItem Caption=" " ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxPanel ID="panelPromotionOnItems" runat="server" Width="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="panelPromotionOnItems_lblPromotionTotalLabel" runat="server" Text="Tổng cộng:">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="panelPromotionOnItems_lblPromotionTotal" runat="server">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxPanel>
                                                    <dx:ASPxPanel ID="panelPromotionOnBill" runat="server" Width="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="panelPromotionOnBill_lblSubtotalLabel" runat="server" Text="Tổng tiền hàng:">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxLabel ID="panelPromotionOnBill_lblSubtotal" runat="server">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxRadioButton Checked="true" GroupName="BillPromotion" ClientInstanceName="radPromotionOnBillByPercentage"
                                                                                ID="radPromotionOnBillByPercentage" runat="server" Text="Theo phần trăm(%): ">
                                                                                <ClientSideEvents CheckedChanged="function(s, e) { 
                                                                                    if(s.GetChecked()) { 
                                                                                        UpdateBillPromotionInfo();
                                                                                    } }" />
                                                                            </dx:ASPxRadioButton>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxSpinEdit ID="spinPromotionOnBillByPercentage" runat="server" Height="21px"
                                                                                ClientInstanceName="spinPromotionOnBillByPercentage" Number="0" DisplayFormatString="#,###"
                                                                                HorizontalAlign="Right">
                                                                                <ClientSideEvents LostFocus="spinPromotionOnBillByPercentage_LostFocus" NumberChanged="spinPromotionOnBillByPercentage_NumberChanged" />
                                                                                <ValidationSettings EnableCustomValidation="true" ErrorDisplayMode="ImageWithTooltip"
                                                                                    Display="Dynamic">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EFEFEF">
                                                                                </ReadOnlyStyle>
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <dx:ASPxRadioButton GroupName="BillPromotion" ClientInstanceName="radPromotionOnBillByAmount"
                                                                                ID="radPromotionOnBillByAmount" runat="server" Text="Theo giá trị: ">
                                                                                <ClientSideEvents CheckedChanged="function(s, e) { 
                                                                                    if(s.GetChecked()) { 
                                                                                        UpdateBillPromotionInfo();
                                                                                    } }" />
                                                                            </dx:ASPxRadioButton>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxSpinEdit ID="spinPromotionOnBillByAmount" ClientInstanceName="spinPromotionOnBillByAmount"
                                                                                runat="server" Height="21px" Number="0" DisplayFormatString="#,###" HorizontalAlign="Right">
                                                                                <ClientSideEvents LostFocus="spinPromotionOnBillByAmount_LostFocus" />
                                                                                <ValidationSettings EnableCustomValidation="true" ErrorDisplayMode="ImageWithTooltip"
                                                                                    Display="Dynamic">
                                                                                </ValidationSettings>
                                                                                <ReadOnlyStyle BackColor="#EFEFEF">
                                                                                </ReadOnlyStyle>
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxPanel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:ASPxHiddenField ID="hfSubtotal" ClientInstanceName="hfSubtotal" runat="server">
                        </dx:ASPxHiddenField>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="popupBillTaxInfo" ClientInstanceName="popupBillTaxInfo"
    runat="server" AllowDragging="True" AllowResize="True" AutoUpdatePosition="True"
    CloseAction="CloseButton" HeaderText="Thông tin thuế hóa đơn" Height="300px"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    RenderMode="Lightweight" Width="460px">
    <ClientSideEvents Closing="popupBillTaxInfo_Closing" />
    <ModalBackgroundStyle BackColor="Transparent">
    </ModalBackgroundStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxCallbackPanel ID="panelBillTaxInfo" ClientInstanceName="panelBillTaxInfo"
                runat="server" Width="100%" OnCallback="panelBillTaxInfo_Callback">
                <ClientSideEvents EndCallback="panelBillTaxInfo_EndCallback" />
                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <table border="0">
                            <tr>
                               
                                <td>
                                    <dx:ASPxRadioButton ID="radBillTaxInfoOnItems" Checked="true" Text="Thuế theo mặt hàng"
                                        GroupName="PromotionCalculationType" runat="server">
                                        <ClientSideEvents CheckedChanged="function(s, e) { 
                                                if(s.GetChecked()) {
                                                    UpdateBillTaxInfo(); 
                                                } 
                                            }" />
                                    </dx:ASPxRadioButton>
                                </td>
                                <td style="display: none">
                                    <dx:ASPxRadioButton ID="radBillTaxInfoOnBill" Text="Thuế trên toàn hóa đơn" GroupName="PromotionCalculationType"
                                        runat="server">
                                        <ClientSideEvents CheckedChanged="function(s, e) { 
                                                if(s.GetChecked()) {
                                                    UpdateBillTaxInfo(); 
                                                } 
                                            }" />
                                    </dx:ASPxRadioButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxFormLayout ID="formlayoutBillTaxInfo" Width="100%" runat="server">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin thuế">
                                    <Items>
                                        <dx:LayoutItem Caption=" " ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    
                                                    <dx:ASPxPanel ID="panelBillTaxInfoOnItems" runat="server" Width="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                                                                    <Items>
                                                                        <dx:LayoutItem Caption="Tổng cộng">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="panelBillTaxInfoOnItems_lblTaxAmount" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Thuế GTGT(%)">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="lblVATOnBillPercentage" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                    </Items>
                                                                </dx:ASPxFormLayout>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxPanel>
                                                    <dx:ASPxPanel Visible="false" ID="panelBillTaxInfoOnBill" runat="server" Width="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent4" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="formlayoutBillTaxInfoOnBill" runat="server">
                                                                    <Items>
                                                                        <dx:LayoutItem Caption="Tổng tiền hàng sau chiết khấu">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="lblSubtotalAfterPromotion" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Thuế GTGT" RequiredMarkDisplayMode="Required">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxComboBox ID="comboVAT" runat="server" CallbackPageSize="10" DataSourceID="dsTax"
                                                                                        EnableCallbackMode="True" IncrementalFilteringMode="Contains" ValueType="System.Guid"
                                                                                        ValueField="TaxId">
                                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateBillTaxInfo(); }" />
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                                            <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                            <dx:ListBoxColumn Caption="Tỉ lệ(%)" FieldName="Percentage" />
                                                                                        </Columns>
                                                                                    </dx:ASPxComboBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Tiền thuế">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="panelBillTaxInfoOnBill_lblVATAmount" runat="server">
                                                                                    </dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                    </Items>
                                                                </dx:ASPxFormLayout>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxPanel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="dsTax" runat="server" 
                            TypeName="NAS.DAL.Accounting.Tax.Tax">
                        </dx:XpoDataSource>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
