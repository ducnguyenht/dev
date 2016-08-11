<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uSettingTestSalesTotal.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uSettingTestSalesTotal" %>
<script type="text/javascript">
    function show_settingTestSalesTotal(s, e) {
        popup_settingTestSalesTotal.Show();
    }
</script>
<dx:aspxpopupcontrol id="popup_settingTestSalesTotal" clientinstancename="popup_settingTestSalesTotal" 
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Giả lập tổng doanh số của khách hàng" 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="800px" height="400px" ScrollBars="Auto">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="form_TestSalesTotal" runat="server">
                <Items>
                    <dx:LayoutItem Caption="Doanh số hiện tại">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="form_TestSalesTotal_E1" runat="server">
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