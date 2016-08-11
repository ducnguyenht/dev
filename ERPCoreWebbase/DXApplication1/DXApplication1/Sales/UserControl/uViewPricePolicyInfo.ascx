<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uViewPricePolicyInfo.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uViewPricePolicyInfo" %>

<dx:ASPxPageControl ID="pc_viewPricePolicyInfo" 
    ClientInstanceName="pc_viewPricePolicyInfo" Width="100%" runat="server" 
    RenderMode="Lightweight" ActiveTabIndex="0">
    <TabPages>
        <dx:TabPage Text="Điều kiện áp dụng">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxNavBar ID="navbar_info" runat="server" RenderMode="Lightweight" 
                        Width="100%">
                        <Groups>
                            <dx:NavBarGroup Text="Điều kiện">
                                <ContentTemplate>
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
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
                            <dx:NavBarGroup Text="Loại trừ">
                                <ContentTemplate>
                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
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
        <dx:TabPage Text="Thông tin giá bán">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxLabel ID="lbl_titlethamkhao" runat="server" Font-Bold="True" 
                        Text="Giá bán theo từng mặt hàng" ClientInstanceName="lbl_titlethamkhao">
                        </dx:ASPxLabel>
                    <dx:ASPxGridView ID="ASPxGridView_thamkhaogia" runat="server" Width="100%"
                        AutoGenerateColumns="False" ClientInstanceName="grid_thamkhaogia" 
                        KeyFieldName="productid">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" 
                                ShowInCustomizationForm="True" VisibleIndex="0">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="product_name" 
                                ShowInCustomizationForm="True" VisibleIndex="1">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nhóm hàng hóa" FieldName="productgrpid" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nhóm nhà sản xuất" 
                                FieldName="manufacturergrpid" ShowInCustomizationForm="True" Visible="False" 
                                VisibleIndex="3">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manufacturerpid" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nhóm nhà cung cấp" 
                                FieldName="suppliergrppid" ShowInCustomizationForm="True" Visible="False" 
                                VisibleIndex="5">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplierpid" 
                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="cost" 
                                ShowInCustomizationForm="True" VisibleIndex="7">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Lợi nhuận" FieldName="profit" 
                                ShowInCustomizationForm="True" VisibleIndex="8">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataMemoColumn Caption="Thuế (%)" FieldName="tax" 
                                ShowInCustomizationForm="True" VisibleIndex="9">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataTextColumn Caption="Giá mua" FieldName="income_price" 
                                ShowInCustomizationForm="True" VisibleIndex="10">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Giá bán tham khảo" FieldName="ref_price" 
                                ShowInCustomizationForm="True" VisibleIndex="11">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Giá bán hiệu chỉnh" FieldName="fixedprice" 
                                ShowInCustomizationForm="True" VisibleIndex="12">
                                <Settings AllowDragDrop="False" />
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
