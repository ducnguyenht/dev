<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uDetailOutComm.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uDetailOutComm" %>
<style type="text/css">
    .style25
    {
        width: 589px;
    }
    </style>

<div id="lineContainer"> 
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine" oncallback="cpLine_Callback">
    <ClientSideEvents EndCallback="cpLine_EndCallback" />
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formSupplierGroupEdit" runat="server" 
            HeaderText="Chi tiết phiếu xuất kho" Height="617px" Modal="True" 
            RenderMode="Lightweight"  
            Width="850px" ClientInstanceName="formSupplierGroupEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        Height="520px" RenderMode="Lightweight" Width="100%">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Thông tin phiếu xuất kho">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" EnableTheming="True" 
                            Theme="Aqua" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="Người tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Enabled="False" 
                                                Text="Nhân viên 1" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ngày tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Enabled="False" 
                                                Text="27-07-2013" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Mã phiếu xuất">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Text="MS0001" Width="170px" 
                                                Enabled="False">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Thủ kho">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Enabled="False" 
                                                Text="Nhân viên A" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Kho">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Enabled="False" Text="Kho 1" 
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                                            <SettingsItemCaptions HorizontalAlign="Right" />
                        </dx:ASPxFormLayout>
                        <br />
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách mặt hàng xuất kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
<dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True" Caption="Thuộc chứng từ" 
                                    VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="code" 
                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="name" 
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" 
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" 
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" 
                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                    <table style="width:100%; margin-top:10px">
                        <tr>
                            <td>
                                <table align="right" style="width:100%;">
                                <tr>
                                    <td align="left" class="style25">
                                        <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                                            Text="Trợ Giúp" >
                                            <Image ToolTip="Trợ giúp">
                                                <SpriteProperties CssClass="Sprite_Help" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        &nbsp;</td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonCancel" Text="Bỏ Qua" 
                                             OnClick="buttonCancel_Click">
                                            <Image ToolTip="Bỏ qua">
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
</div>