<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uAdjustingHistory.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uAdjustingHistory" %>
<dx:ASPxPopupControl ID="formEntryDetailHistory" runat="server" 
    HeaderText="Lịch sử điều chỉnh tồn kho" Height="400px" Modal="True" 
    RenderMode="Classic"
    Width="1200px" ScrollBars="Auto" ClientInstanceName="formEntryDetailHistory" AllowResize="True" 
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" ShowFooter="true"
    PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                <Items>
                    <dx:LayoutItem Caption="Mã hàng hóa">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="MH001">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:ASPxFormLayout>
            <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Ngày" FieldName="date" 
                        ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mã kiểm kho" FieldName="verifyid" 
                        ShowInCustomizationForm="True" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Vị trí kho" FieldName="position" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SL theo CT" FieldName="recieptamount" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" 
                        ShowInCustomizationForm="True" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="amountdiff" 
                        ShowInCustomizationForm="True" VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewBandColumn Caption="Phẩm chất" HeaderStyle-HorizontalAlign="Center" VisibleIndex="6">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Kém phẩm chất">
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                                <FooterTemplate>
                                    .........
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mất phẩm chất">
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                                <FooterTemplate>
                                    .........
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataTextColumn 
                        ShowInCustomizationForm="True" Caption="Đề xuất điều chỉnh">
                        <DataItemTemplate>
                            .........
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SL điều chỉnh" FieldName="editamount" Name="editamount"
                        ShowInCustomizationForm="True" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" Name="note" 
                        ShowInCustomizationForm="True" VisibleIndex="8">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:ASPxPopupControl>