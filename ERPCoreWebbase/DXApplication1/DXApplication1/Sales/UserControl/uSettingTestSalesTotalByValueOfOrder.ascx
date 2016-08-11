<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uSettingTestSalesTotalByValueOfOrder.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uSettingTestSalesTotalByValueOfOrder" %>
<script type="text/javascript">
    function show_settingTestSalesTotalByValueOfOrder(s, e) {
        popup_settingTestSalesTotalByValueOfOrder.Show();
    }
</script>
<dx:aspxpopupcontrol id="popup_settingTestSalesTotalByValueOfOrder" clientinstancename="popup_settingTestSalesTotalByValueOfOrder" 
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Giá lập tổng giá trị cho phiếu bán" 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="800px" height="400px" ScrollBars="Auto">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="form_SalesTotalByValueOfOrder" runat="server">
                <Items>
                    <dx:LayoutItem Caption="Giá trị phiếu bán">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txt_minSales" runat="server">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:ASPxFormLayout>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
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