<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs" Inherits="WebModule.Warehouse.UserControl.MessageBox" %>
<dx:ASPxCallbackPanel ID="cpMessageBox" runat="server" 
    ClientInstanceName="cpMessageBox" Width="200px">
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formMessageBox" runat="server" 
        HeaderText="Thông báo" Height="196px" RenderMode="Lightweight" 
        ShowCloseButton="False" Width="559px" ClientInstanceName="formMessageBox" 
        CloseAction="None" Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                    <Items>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblMassage" runat="server" ClientInstanceName="lblMassage">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" 
                                        NavigateUrl="~/Default.aspx" Text="Trở về trang chủ">
                                    </dx:ASPxHyperLink>
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

