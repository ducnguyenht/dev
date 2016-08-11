<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uViewBalanceInfo.ascx.cs" Inherits="WebModule.Warehouse.Command.PopupCommand.ViewBalanceInfo.uViewBalanceInfo" %>
<dx:ASPxLoadingPanel ID="ldpnInventoryCommand" runat="server" HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
</dx:ASPxLoadingPanel>
<dx:ASPxCallbackPanel ID="cpItemUnitBalanceDetail" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    Width="100%" OnCallback="cpItemUnitBalanceDetail_OnCallback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="frmDetailOfLine" runat="server" Height="100%" 
                Width="95%">
                <Items>
                    <dx:LayoutGroup Caption="Chi tiết tồn kho">
                        <Items>
                            <dx:LayoutItem Caption="Nhà sản xuất">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblManufacturer" runat="server" 
                                            ClientInstanceName="lblManufacturer">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Đơn giá">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblPrice" runat="server" ClientInstanceName="lblPrice">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="SL tồn">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblBalance" runat="server" ClientInstanceName="lblBalance">
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