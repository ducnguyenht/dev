<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEntryDetail.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uEntryDetail" %>
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine" oncallback="cpLine_Callback">
    <ClientSideEvents EndCallback="cpLine_EndCallback" />
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="formEntryDetail" runat="server" 
                HeaderText="Chi tiết điều chỉnh tồn kho" Height="400px" Modal="True" 
                RenderMode="Classic"
                Width="1200px" ScrollBars="Auto" ClientInstanceName="formEntryDetail" AllowResize="True" 
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
                            Width="100%" OnHtmlRowCreated="grdData_HtmlRowCreated">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Ngày" FieldName="date" 
                                    ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã kiểm kho" FieldName="verifyid" 
                                    ShowInCustomizationForm="True" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Vị trí kho" FieldName="position" 
                                    ShowInCustomizationForm="True" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="SL theo CT" FieldName="recieptamount" 
                                    ShowInCustomizationForm="True" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" 
                                    ShowInCustomizationForm="True" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="amountdiff" 
                                    ShowInCustomizationForm="True" >
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewBandColumn Caption="Phẩm chất" HeaderStyle-HorizontalAlign="Center">
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
                                    ShowInCustomizationForm="True">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtAmount" runat="server">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel ID="lblAmount" runat="server" Text='<%# Eval("editamount") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" Name="note" 
                                    ShowInCustomizationForm="True">
                                    <DataItemTemplate>
                                        <dx:ASPxTextBox ID="txtNote" runat="server">
                                        </dx:ASPxTextBox>
                                        <dx:ASPxLabel ID="lblNote" runat="server" Text='<%# Eval("note") %>'>
                                        </dx:ASPxLabel>
                                    </DataItemTemplate>
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
                                    <div style="float: right; margin-left: 4px">
                                        <dx:ASPxButton ID="btnCancel" clientinstancename="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>
                                     <div style="float: right; margin-left: 4px">
                                        <dx:ASPxButton ID="btnApply" clientinstancename="btnApply" runat="server" Text="Duyệt">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </FooterContentTemplate>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>