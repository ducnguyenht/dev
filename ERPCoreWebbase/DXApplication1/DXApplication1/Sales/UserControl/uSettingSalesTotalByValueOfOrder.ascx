<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uSettingSalesTotalByValueOfOrder.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uSettingSalesTotalByValueOfOrder" %>
<script type="text/javascript">
    function show_settingSalesTotalByValueOfOrder(s, e) {
        popup_settingSalesTotalByValueOfOrder.Show();
    }
</script>
<dx:aspxpopupcontrol id="popup_settingSalesTotalByValueOfOrder" clientinstancename="popup_settingSalesTotalByValueOfOrder" 
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Áp dụng điều kiện doanh số cho phiếu bán" 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="800px" height="400px" ScrollBars="Auto">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="form_SalesTotalByValueOfOrder" runat="server">
                <Items>
                    <dx:LayoutItem Caption="Giá trị phiếu bán từ">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txt_minSales" runat="server">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá trị phiếu bán đến">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txt_maxSales" runat="server">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:ASPxFormLayout>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <div style="float: left; margin-right: 4px">
                        <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                            Text="Thoát">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right">
                        <dx:ASPxButton ID="buttonAccept" ClientInstanceName="buttonSave" runat="server" Text="Lưu lại"
                            clientvisible="true">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:aspxpopupcontrol>