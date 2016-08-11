<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailInputCommWahoure.ascx.cs"
    Inherits="WebModule.Warehouse.UserControl.DetailInputCommWahoure" %>
<style type="text/css">
    .style25
    {
        width: 589px;
    }
</style>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine"
        OnCallback="cpLine_Callback">
        <ClientSideEvents EndCallback="cpLine_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formSupplierGroupEdit" runat="server" HeaderText="Chi tiết phiếu nhập kho"
                    Height="600px" Modal="True" RenderMode="Classic" Width="850px" ClientInstanceName="formSupplierGroupEdit"
                    AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    LoadingPanelDelay="1000" ShowFooter="true" ShowSizeGrip="False" ShowMaximizeButton="true">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="100%"
                                RenderMode="Classic" Width="520px">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông tin phiếu nhập kho">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách mặt hàng nhập kho" Font-Bold="True"
                                                    Font-Size="Small">
                                                </dx:ASPxLabel>
                                                <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" ColCount="2" Width = "100%">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Người tạo">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Enabled="False" Text="Nhân viên 1">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Ngày tạo">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Enabled="False" Text="27-07-2013">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Mã phiếu nhập">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer  runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Text="MS0001" Enabled="False">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Thủ kho">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox9" runat="server" Enabled="False" Text="Nhân viên A">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:EmptyLayoutItem>
                                                        </dx:EmptyLayoutItem>
                                                        <dx:LayoutItem Caption="Kho">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox10" runat="server" Enabled="False" Text="Kho 1">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:EmptyLayoutItem>
                                                        </dx:EmptyLayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True"
                                                            Caption="Thuộc chứng từ" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                                            VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Hạn sử dụng" FieldName="date" ShowInCustomizationForm="True"
                                                            VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <FooterContentTemplate>
                        <div style="width: 100%;">
                            <dx:ASPxButton ID="buttonHelp" CssClass = "float-left button-left-margin" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image ToolTip="Trợ giúp">
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonCancel" CssClass = "float-right button-right-margin" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                                Text="Thoát" OnClick="buttonCancel_Click">
                                <Image ToolTip="Thoát">
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </FooterContentTemplate>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
