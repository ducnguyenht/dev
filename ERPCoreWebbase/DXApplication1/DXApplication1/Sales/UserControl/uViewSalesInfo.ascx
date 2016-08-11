<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uViewSalesInfo.ascx.cs"
    Inherits="WebModule.GUI.usercontrol.uViewSalesInfo" %>
<%@ Register Src="~/Sales/UserControl/uEdit_ProductToSalesOrder.ascx" TagName="uEdit_ProductToSalesOrder"
    TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/uEditServiceToSalesOrder.ascx" TagName="uEditServiceToSalesOrder"
    TagPrefix="uc2" %>
<%@ Register Src="~/Sales/UserControl/uViewApprovedPromotionSale.ascx" TagName="uViewApprovedPromotionSale"
    TagPrefix="uc3" %>
<%@ Register Src="~/Sales/UserControl/uBillingVouchers.ascx" TagName="uBillingVouchers"
    TagPrefix="uc4" %>

<dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Layout Group" ColCount="2" GroupBoxDecoration="None">
            <Items>
                <dx:LayoutItem Caption="Mã số">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxFormLayout3_E2" runat="server" Text="PH00001">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tên khách hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxFormLayout3_E3" runat="server" Text="Cty cổ phần dược Sài Gòn">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxFormLayout3_E4" runat="server" Text="20/03/2012">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Cộng tác viên" ShowCaption="True">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxFormLayout3_E5" runat="server">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="NV duyệt giá">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxFormLayout3_E6" runat="server" Text="Nguyễn Văn A">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="NV bán hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxFormLayout3_E7" runat="server" Text="Nguyễn Văn A">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
    <SettingsItemCaptions HorizontalAlign="Right" />
    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
</dx:ASPxFormLayout>
<dx:ASPxPageControl ID="pagetab_orderdetail" runat="server" ActiveTabIndex="0" Height="75%"
    Width="100%" EnableViewState="False" RenderMode="Classic">
    <TabPages>
        <dx:TabPage Text="Hàng hóa">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="ASPxGridView2_hanghoa" ClientInstanceName="ASPxGridView2_hanghoa"
                        runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="productid">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="STT" FieldName="stt" ShowInCustomizationForm="True"
                                VisibleIndex="0" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                                <FooterTemplate>
                                    Cộng
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="number" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                                <FooterTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100%" Text="7.500.000" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
                                VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                VisibleIndex="8">
                                <PropertiesDateEdit DisplayFormatString="">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                VisibleIndex="9">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager ShowEmptyDataRows="True">
                        </SettingsPager>
                        <Settings ShowFooter="True" VerticalScrollableHeight="300" />
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>                 
                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                        <Items>
                            <dx:LayoutGroup Caption="Layout Item" ColCount="2" GroupBoxDecoration="None">
                                <Items>
                                    <dx:LayoutItem Caption="Thuế suất GTGT">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxFormLayout1_3" runat="server" Text="10%">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tiền thuế GTGT">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxFormLayout1_E3" runat="server" Text="750.000">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px" Visible="false">
                                                    <Border BorderStyle="None" />
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng tiền hàng hóa">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxFormLayout1_E4" runat="server" Text="8.250.000">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                        <SettingsItemCaptions HorizontalAlign="Right" />
                        <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Dịch vụ">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="ASPxGridView3_dichvu" runat="server" AutoGenerateColumns="False"
                        Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã số dịch vụ" FieldName="serviceid" ShowInCustomizationForm="True"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên dịch vụ" FieldName="servicename" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                                <FooterTemplate>
                                    Cộng
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="number" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="price" ShowInCustomizationForm="True" Caption="Đơn giá"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                                <FooterTemplate>
                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100%" Text="10.325.000" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFooter="True" />
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                    <br />
                    <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" Width="100%">
                        <Items>
                            <dx:LayoutGroup Caption="Layout Item" ColCount="2" GroupBoxDecoration="None">
                                <Items>
                                    <dx:LayoutItem Caption="Thuế suất GTGT">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxFormLayout4_E1" runat="server" Text="10%">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tiền thuế GTGT">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxFormLayout4_E2" runat="server" Text="1.035.000">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px" Visible="false">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxFormLayout4_E3" runat="server" Text="11.360.000">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:LayoutGroup>
                        </Items>
                        <SettingsItemCaptions HorizontalAlign="Right" />
                        <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Khuyến mãi">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                    <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                        <uc3:uViewApprovedPromotionSale ID="uViewApprovedPromotionSale1" runat="server" />
                    </dx:ContentControl>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Tiến độ giao hàng">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="gridview_tdgh" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="STT" FieldName="stt" ShowInCustomizationForm="True"
                                VisibleIndex="0" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày giao dự kiến" FieldName="plandate" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="SL giao dự kiến" FieldName="plannumber" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày giao thực tế" FieldName="realdate" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="SL giao thực tế" FieldName="realnumber" ShowInCustomizationForm="True"
                                VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
                                VisibleIndex="8">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                VisibleIndex="9">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Tiến độ thanh toán">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="gridview_tdtt" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="STT" FieldName="c1" ShowInCustomizationForm="True"
                                VisibleIndex="0" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày trả dự kiến" FieldName="c2" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số tiền trả dự kiến" FieldName="c3" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày trả thực tế" FieldName="c4" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số tiền trả thực tế" FieldName="c5" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Hình thức thanh toán" FieldName="c6" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Chi tiết" FieldName="c7" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="c8" ShowInCustomizationForm="True"
                                VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Hóa đơn">
            <ContentCollection>
                <dx:ContentControl>
                    <uc4:uBillingVouchers ID="uBillingVouchers1" runat="server" />
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
    <Paddings Padding="5px" />
</dx:ASPxPageControl>
<dx:ASPxFormLayout ID="form_total" runat="server" Width="100%">
    <Items>
        <dx:LayoutItem Caption="Tổng giá trị phiếu bán hàng">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="11.360.000">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Chú thích">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server"
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>

