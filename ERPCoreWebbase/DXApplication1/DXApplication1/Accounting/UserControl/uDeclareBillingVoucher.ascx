<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uDeclareBillingVoucher.ascx.cs" Inherits="WebModule.Accounting.UserControl.uDeclareBillingVoucher" %>

<dx:ASPxFormLayout ID="form_billingVoucher" runat="server" ColCount="3">
    <Items>
        <dx:LayoutItem Caption="Số hóa đơn">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_billingVoucher_E1" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ký hiệu hóa đơn">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_billingVoucher_E2" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ngày phát hành">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxDateEdit ID="form_billingVoucher_E3" runat="server">
                    </dx:ASPxDateEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Phân loại thuế" ShowCaption="true" ColSpan="3">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxComboBox ID="cbo_taxtype" runat="server" 
                        ValueType="System.String" IncrementalFilteringMode="Contains">
                        <Items>
                            <dx:ListEditItem Value="1" Text="Hàng hóa dv dùng riêng cho sxkd chịu thuế gtgt đã phát sinh doanh thu" />
                            <dx:ListEditItem Value="2" Text="Hàng hóa dv dùng riêng cho sxkd không chịu thuế gtgt" />
                            <dx:ListEditItem Value="3" Text="Hàng hóa dv dùng chung cho sxkd chịu thuế gtgt đã và không chịu thuế gtgt" />
                            <dx:ListEditItem Value="4" Text="Hàng hóa dv dùng chung cho tscđ chưa phát sinh doanh thu được khấu trừ dần theo quý" />
                            <dx:ListEditItem Value="5" Text="Hàng hóa dv không phải tổng hợp lên tờ khai 01/GTGT" />
                        </Items>
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Chi tiết hóa đơn" ShowCaption="false" ColSpan="3">
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
                                                <%--<dx:GridViewDataTextColumn Caption="Phân loại thuế" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        <dx:ASPxComboBox ID="cbo_taxtype" runat="server" 
                                                            ValueType="System.String" IncrementalFilteringMode="Contains">
                                                            <Items>
                                                                <dx:ListEditItem Value="1" Text="Hàng hóa dv dùng riêng cho sxkd chịu thuế gtgt đã phát sinh doanh thu" />
                                                                <dx:ListEditItem Value="2" Text="Hàng hóa dv dùng riêng cho sxkd không chịu thuế gtgt" />
                                                                <dx:ListEditItem Value="3" Text="Hàng hóa dv dùng chung cho sxkd chịu thuế gtgt đã và không chịu thuế gtgt" />
                                                                <dx:ListEditItem Value="4" Text="Hàng hóa dv dùng chung cho tscđ chưa phát sinh doanh thu được khấu trừ dần theo quý" />
                                                                <dx:ListEditItem Value="5" Text="Hàng hóa dv không phải tổng hợp lên tờ khai 01/GTGT" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>--%>
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
                                                        <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100%" Text="7.500.000" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                                                        <dx:ASPxTextBox ID="txtTaxTotal" runat="server" Width="100%" Text="........." ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                                    VisibleIndex="9">
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="txtNote" runat="server" ></dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="True" />
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
                                                <%--<dx:GridViewDataTextColumn Caption="Phân loại thuế" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        <dx:ASPxComboBox ID="cbo_taxtype" runat="server" 
                                                            ValueType="System.String" IncrementalFilteringMode="Contains">
                                                            <Items>
                                                                <dx:ListEditItem Value="1" Text="Hàng hóa dv dùng riêng cho sxkd chịu thuế gtgt đã phát sinh doanh thu" />
                                                                <dx:ListEditItem Value="2" Text="Hàng hóa dv dùng riêng cho sxkd không chịu thuế gtgt" />
                                                                <dx:ListEditItem Value="3" Text="Hàng hóa dv dùng chung cho sxkd chịu thuế gtgt đã và không chịu thuế gtgt" />
                                                                <dx:ListEditItem Value="4" Text="Hàng hóa dv dùng chung cho tscđ chưa phát sinh doanh thu được khấu trừ dần theo quý" />
                                                                <dx:ListEditItem Value="5" Text="Hàng hóa dv không phải tổng hợp lên tờ khai 01/GTGT" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>--%>
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
                                                        <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100%" Text="10.325.000" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                                                        <dx:ASPxTextBox ID="txtTaxTotal" runat="server" Width="100%" Text="........." ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True">
                                                    <DataItemTemplate>
                                                        <dx:ASPxTextBox ID="txtNote" runat="server" ></dx:ASPxTextBox>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Settings ShowFooter="True" />
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
                                            
                                        </dx:ContentControl>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="File chứng từ đính kèm">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
                                                <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                            </dx:ASPxFileManager>
                                        </dx:ContentControl>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                        <Paddings Padding="5px" />
                        <Paddings Padding="5px"></Paddings>
                    </dx:ASPxPageControl>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tổng tiền hàng">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_billingVoucher_E4" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tổng tiền dịch vụ">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6"  runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_billingVoucher_E5" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tổng tiền thuế">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_billingVoucher_E6" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>

