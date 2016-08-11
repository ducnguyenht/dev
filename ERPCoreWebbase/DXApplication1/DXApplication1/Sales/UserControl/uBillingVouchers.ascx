<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBillingVouchers.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uBillingVouchers" %>
<%@ Register src="~/Sales/UserControl/uViewApprovedPromotionSale.ascx" tagname="uViewApprovedPromotionSale" tagprefix="uc1" %>

<dx:ASPxFormLayout ID="form_billingVoucher" runat="server" ColCount="3">
    <Items>
        <dx:LayoutItem Caption="Mã hóa đơn">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="000000001">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ký hiệu hóa đơn">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="HDSYM">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ngày phát hành">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="20/12/2013">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Danh mục" ShowCaption="False" ColSpan="3">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxPageControl ID="pc_billingdetail" ClientInstanceName="pc_billingdetail" runat="server" ActiveTabIndex="0"
                        Width="100%" Height="75%" EnableViewState="False">
                        <TabPages>
                            <dx:TabPage Text="Hàng hóa">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="grv_products" ClientInstanceName="grv_products"
                                            runat="server" AutoGenerateColumns="False"
                                            Width="100%" KeyFieldName="productid">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Phân loại thuế" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        .........
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="number" ShowInCustomizationForm="True">
                                                    <FooterTemplate>
                                                        Tổng tiền hàng
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                    <FooterTemplate>
                                                        .........
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="% thuế" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        10
                                                    </DataItemTemplate>
                                                    <FooterTemplate>
                                                        Tổng tiền thuế
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tiền thuế" ShowInCustomizationForm="True">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                    <FooterTemplate>
                                                        .........
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                                    VisibleIndex="9">
                                                    <DataItemTemplate>
                                                        .........
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="True"></Settings>
                                            <Styles>
                                                <Header HorizontalAlign="Center">
                                                </Header>
                                            </Styles>
                                        </dx:ASPxGridView>
                                        <br />
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Dịch vụ">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="grv_services" runat="server" AutoGenerateColumns="False"
                                            Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Mã số dịch vụ" FieldName="serviceid" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tên dịch vụ" FieldName="servicename" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Phân loại thuế" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        .........
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="number" ShowInCustomizationForm="True">
                                                    <FooterTemplate>
                                                        Tổng tiền dv
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="price" ShowInCustomizationForm="True" Caption="Đơn giá">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                    <FooterTemplate>
                                                        .........
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="% thuế" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        10
                                                    </DataItemTemplate>
                                                    <FooterTemplate>
                                                        Tổng tiền thuế
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tiền thuế" ShowInCustomizationForm="True">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                    <FooterTemplate>
                                                        .........
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        .........
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="True"></Settings>
                                            <Styles>
                                                <Header HorizontalAlign="Center">
                                                </Header>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Khuyến mãi">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                            <uc1:uViewApprovedPromotionSale ID="uViewApprovedPromotionSale1" runat="server" />
                                        </dx:ContentControl>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                        <Paddings Padding="5px"></Paddings>
                    </dx:ASPxPageControl>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tổng tiền hàng">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="90.000.000">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tổng tiền dịch vụ">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer  runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="2.000.000">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tổng tiền thuế">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="920.000">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>

