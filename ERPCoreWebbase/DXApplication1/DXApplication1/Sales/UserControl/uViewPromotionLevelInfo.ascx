<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uViewPromotionLevelInfo.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uViewPromotionLevelInfo" %>
<dx:ASPxPageControl ID="pc_viewPricePolicyInfo" 
    ClientInstanceName="pc_viewPricePolicyInfo" Width="100%" Height="100%" runat="server" 
    RenderMode="Classic" ActiveTabIndex="0">
    <TabPages>
        <dx:TabPage Text="Điều Kiện Áp Dụng">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxNavBar ID="navbar_info" runat="server" RenderMode="Lightweight" 
                        Width="100%">
                        <Groups>
                            <dx:NavBarGroup Text="Điều Kiện">
                                <ContentTemplate>
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="#0000CC" 
                                                            Text="Nhóm khách hàng trong số [Nhóm miền Nam, Nhóm miền Trung]">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#CC0000" Text="và">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" ForeColor="#0000CC" 
                                                            Text="Nhóm hàng hóa trong số [Dinh dưỡng chức năng, Dược phẩm giảm đau]">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" ForeColor="#CC0000" Text="và">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" ForeColor="#0000CC" 
                                                            Text="Doanh số tích lũy từ 10.000.000 VNĐ trở lên">
                                                        </dx:ASPxLabel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                </ContentTemplate>
                            </dx:NavBarGroup>
                            <dx:NavBarGroup Text="Loại Trừ">
                                <ContentTemplate>
                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="#0000CC" 
                                                            Text="Nhóm khách hàng trong số [Nhóm miền Nam, Nhóm miền Trung]">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#CC0000" Text="và">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" ForeColor="#0000CC" 
                                                            Text="Nhóm hàng hóa trong số [Dinh dưỡng chức năng, Dược phẩm giảm đau]">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" ForeColor="#CC0000" Text="và">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" ForeColor="#0000CC" 
                                                            Text="Doanh số tích lũy từ 10.000.000 VNĐ trở lên">
                                                        </dx:ASPxLabel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                </ContentTemplate>
                            </dx:NavBarGroup>
                        </Groups>
                    </dx:ASPxNavBar>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Quyền Lợi">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxFormLayout ID="form_infoquyenloi" runat="server">
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxRoundPanel ID="round_chietkhau" runat="server" ShowHeader="true" HeaderText="Chiết khấu" View="GroupBox"
                                            Width="100%" ClientInstanceName="roundpanelchietkhau">
                                            <PanelCollection>
                                                <dx:PanelContent ID="PanelContent6" runat="server" SupportsDisabledAttribute="True">
                                                    <table class="form">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="lbl_chietkhau1" runat="server" AssociatedControlID="cbo_chietkhau1"
                                                                    GroupName="type_select" Text="Tiền giảm theo phiếu (VNĐ)" Width="100%" 
                                                                    Wrap="False">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td style="width: 109px;">
                                                                <dx:ASPxTextBox ID="cbo_chietkhau1" ReadOnly="true" Text="1.000.000" runat="server" Width="170px">
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxRoundPanel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False" Width="100%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <br />
                                        <dx:ASPxLabel ID="lbl_title_khuyenmai2" runat="server" 
                                            Text="Danh mục chiết khấu theo số lượng" Font-Bold="True" />
                                        <dx:ASPxGridView ID="gridview_hanghoatang" runat="server" Width="100%"
                                                        AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="STT" FieldName="sequenceno" ShowInCustomizationForm="True" 
                                                    VisibleIndex="0" Visible="false">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True" 
                                                    VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname"
                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="productunitid" ShowInCustomizationForm="True" 
                                                    VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True" 
                                                    VisibleIndex="4">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewBandColumn Caption="Điều kiện tặng hàng" 
                                                    ShowInCustomizationForm="True" VisibleIndex="5">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mua" FieldName="condition_buy"
                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn 
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <DataItemTemplate>
                                                                ->
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give"
                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                        <br />
                                        <dx:ASPxLabel ID="lbl_title_khuyenmai3" runat="server" 
                                            Text="Danh mục tặng phẩm kèm theo" Font-Bold="True" />
                                        <dx:ASPxGridView ID="gridview_hanghoabonus" runat="server" 
                                            Width="100%" AutoGenerateColumns="False">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                                    VisibleIndex="0">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Giá Trị" FieldName="GiaTri" ShowInCustomizationForm="True"
                                                    VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="SoLuong" ShowInCustomizationForm="True" 
                                                    VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành tiền" FieldName="ThanhTien"  VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                                    VisibleIndex="4">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
