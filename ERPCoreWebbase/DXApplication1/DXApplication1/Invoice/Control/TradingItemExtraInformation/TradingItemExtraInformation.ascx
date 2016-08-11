<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TradingItemExtraInformation.ascx.cs"
    Inherits="WebModule.Invoice.Control.TradingItemExtraInformation.TradingItemExtraInformation" %>

<script type="text/javascript">
    var TradingItemExtraInformationClass = function () {
        this.CallbackPending = null;

        this.Show = function (itemId, unitId) {
            var args;
            if (!itemId)
                return;
            args = itemId;
            if (unitId)
                args += '|' + unitId;
            var panel = <%= _ClientInstanceName + "_panelTradingItemExtraInformation" %>;
            if(panel.InCallback())
                this.CallbackPending = args;
            else
                panel.PerformCallback(args);
        };

        this.RePerformCallback = function()
        {
            if(this.CallbackPending)
            {
                var panel = <%= _ClientInstanceName + "_panelTradingItemExtraInformation" %>;
                console.log('this.CallbackPending:' + this.CallbackPending);
                panel.PerformCallback(this.CallbackPending);
                this.CallbackPending = null;
            }
        };
    };

    var nasObj = new TradingItemExtraInformationClass();
    window['<%= _ClientInstanceName %>'] = nasObj;

</script>

<dx:ASPxCallbackPanel ID="panelTradingItemExtraInformation" runat="server" 
    Width="100%" oncallback="panelTradingItemExtraInformation_Callback">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxFormLayout ID="formlayoutTradingItemExtraInformation" runat="server" Width="100%">
                <Items>
                    <dx:LayoutGroup Caption="Thông tin chi tiết hàng hóa/dịch vụ" 
                        Name="TradingItemExtraInformationLayoutGroup">
                        <Items>
                            <dx:LayoutItem Caption="Mã">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblItemCode" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tên">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblItemName" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Nhà sản xuất">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblManufacturer" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Thuế GTGT">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblTax" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Đơn vị tính">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblUnit" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tồn kho">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblBalance" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Visible="false" Caption="Tồn kho quy đổi">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblConvertedBalance" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
