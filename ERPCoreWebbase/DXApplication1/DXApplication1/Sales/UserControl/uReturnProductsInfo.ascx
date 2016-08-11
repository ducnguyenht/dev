<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uReturnProductsInfo.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uReturnProductsInfo" %>
<dx:ASPxFormLayout ID="form_typeofinput" runat="server" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Thông tin chung" ShowCaption="True" Width="100%" 
            ColCount="2">
            <Items>
                <dx:LayoutItem ShowCaption="true" Caption="Mã khách hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text=".........">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="true" Caption="Tên khách hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text=".........">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="true" Caption="Người trả hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text=".........">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="true" Caption="Nguyên nhân trả hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text=".........">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="true" Caption="Ngày trả hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5"  runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text=".../.../...">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="true" Caption="Hình thức trả hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text=".........">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="true" Caption="Hình thức thanh toán">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text=".........">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Chi tiết trả hàng và trách nhiệm thu hồi" ShowCaption="true">
            <Items>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True" Text="Danh mục hàng trả lại">
                            </dx:ASPxLabel>
                            <dx:ASPxGridView ID="grd_returnhanghoa" runat="server"
                                AutoGenerateColumns="False" Width="100%" KeyFieldName="productid">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                        VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                        VisibleIndex="1">
                                        <FooterTemplate>
                                            Cộng
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số lượng trả" FieldName="number" ShowInCustomizationForm="True"
                                        VisibleIndex="3">
                                        <DataItemTemplate>
                                            ..........
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn giá ban đầu" FieldName="priceunit" ShowInCustomizationForm="True"
                                        VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn giá thu hồi" FieldName="priceunit" ShowInCustomizationForm="True"
                                        VisibleIndex="5">
                                        <DataItemTemplate>
                                            ..........
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành tiền ban đầu" FieldName="total" ShowInCustomizationForm="True"
                                        VisibleIndex="6">
                                        <FooterTemplate>
                                            (A)................
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành tiền thu hồi" FieldName="total" ShowInCustomizationForm="True"
                                        VisibleIndex="7">
                                        <FooterTemplate>
                                            (B)................
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                        VisibleIndex="8">
                                        <PropertiesDateEdit DisplayFormatString="">
                                        </PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                        VisibleIndex="9">
                                        <DataItemTemplate>
                                            ..........
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="True" />
                                <Styles>
                                    <Header HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                            <%--<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                <Items>
                                    <dx:LayoutItem Caption="Phí phát sinh trên hàng hóa thu hồi (A - B)">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>--%>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_thuhoikm" runat="server" Font-Bold="True" Text="Danh mục hàng khuyến mãi thu hồi">
                            </dx:ASPxLabel>
                            <dx:ASPxGridView ID="gridview_tangpham" runat="server" AutoGenerateColumns="False"
                                Width="100%">
                                <Columns>
                                    <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                        Caption="Áp dụng" ShowSelectCheckbox="True" Visible="false">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="Mã quà tặng" FieldName="MaQuaTang" ShowInCustomizationForm="True"
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                        VisibleIndex="3">
                                        <FooterTemplate>
                                            Tổng giá trị
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="DonViTinh" ShowInCustomizationForm="True"
                                        VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số lượng tặng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                        VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số lượng thu hồi" ShowInCustomizationForm="True"
                                        VisibleIndex="5">
                                        <DataItemTemplate>
                                            ........
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="GiaTri" ShowInCustomizationForm="True"
                                        VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Phân loại" FieldName="PhanLoai" ShowInCustomizationForm="True"
                                        VisibleIndex="7">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Giá trị tặng" FieldName="ThanhTien"
                                        VisibleIndex="8">
                                        <FooterTemplate>
                                            (C)..........
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành tiền thu hồi"  ShowInCustomizationForm="True"
                                        VisibleIndex="9">
                                        <FooterTemplate>
                                            (D)..........
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                        VisibleIndex="10">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="true" />
                            </dx:ASPxGridView>
                            <dx:ASPxFormLayout ID="form_thuhoikm" runat="server">
                                <Items>
                                    <dx:LayoutItem Caption="E: Tiền C.K thu hồi">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="........">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="F: Chi phí quà tặng thu hồi (C - D)">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="........">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <%--<dx:LayoutItem Caption="G: Tổng giá trị K.M thu hồi (E + F)">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="form_thuhoikm_E3" runat="server" Width="170px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>--%>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" Text="Dịch vụ phát sinh">
                            </dx:ASPxLabel>
                            <dx:ASPxGridView ID="grv_serviceIssue" runat="server" AutoGenerateColumns="False"
                                Width="100%">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Mã số dịch vụ" FieldName="serviceid" ShowInCustomizationForm="True"
                                        VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên dịch vụ" FieldName="servicename" ShowInCustomizationForm="True"
                                        VisibleIndex="1">
                                        <FooterTemplate>
                                            Tổng tiền dịch vụ
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
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                        <FooterTemplate>
                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="........">
                                            </dx:ASPxLabel>
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                        VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="True" />
                                <Settings ShowFooter="True"></Settings>
                                <Styles>
                                    <Header HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>

                            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Layout Item" ColCount="2" GroupBoxDecoration="None">
                                    <Items>
                                        <dx:LayoutItem Caption="Thuế suất GTGT">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="........">
                                                </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tiền thuế GTGT">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="........">
                                                </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px" Visible="false">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="........">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                            <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                        </dx:ASPxFormLayout>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Chi tiết thanh toán" ShowCaption="true">
            <Items>
                <dx:LayoutGroup ShowCaption="False" ColCount="2">
                    <Items>
                        <%--<dx:LayoutItem Caption="Tổng G.T hàng thu hồi theo giá ban đầu (A)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="130px">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tổng G.T hàng thu hồi theo giá nhận (B)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="130px">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>--%>
                        <dx:LayoutItem Caption="Phí phát sinh trên hàng hóa thu hồi (A - B)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="........">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tổng giá trị K.M thu hồi (E + F)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="........">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem><%--
                        <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Width="130px">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>--%>
                        <dx:LayoutItem Caption="Số tiền khách hàng thanh toán">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="........">
                                    </dx:ASPxLabel>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                                                                                
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPanel runat="server" Width="100%" ID="panel_grp1"
                                Visible="true">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Font-Bold="True" Text="Cấn trừ công nợ">
                                        </dx:ASPxLabel>
                                        <dx:ASPxGridView ID="grv_debt" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Ngày" FieldName="date" ShowInCustomizationForm="True"
                                                    VisibleIndex="0">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="customerid" ShowInCustomizationForm="True"
                                                    VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="customername" ShowInCustomizationForm="True"
                                                    VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Nợ đầu kì" FieldName="firstdebt" ShowInCustomizationForm="True"
                                                    VisibleIndex="3">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Phát sinh" FieldName="issue" ShowInCustomizationForm="True"
                                                    VisibleIndex="4">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Đã thanh toán" FieldName="payment" ShowInCustomizationForm="True"
                                                    VisibleIndex="5">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                    <DataItemTemplate>
                                                        .........
                                                    </DataItemTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Nợ cuối kì" FieldName="lastdebt" ShowInCustomizationForm="True"
                                                    VisibleIndex="6" Visible="false">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="7" Caption="In biên nhận">
                                                    <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="print_1">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Print" />
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPanel runat="server" Width="100%" ID="panel_grp2" 
                            Visible="false">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Font-Bold="True" Text="Đề nghị chi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxGridView ID="grv_givecash" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Ngày" FieldName="date" ShowInCustomizationForm="True"
                                                VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="customerid" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="customername" ShowInCustomizationForm="True"
                                                VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số tiền chi" FieldName="cash" VisibleIndex="3">
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                                <DataItemTemplate>
                                                    .........
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Cách chuyển" VisibleIndex="4">
                                                <DataItemTemplate>
                                                    <dx:ASPxComboBox ID="cbo_paymentp" runat="server" Width="100px">
                                                        <Items>
                                                            <dx:ListEditItem Value="SP00001" Text="Tiền mặt" />
                                                            <dx:ListEditItem Value="SP00002" Text="Chuyển khoản" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="description" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Visible="false"><%--
                                                <DataItemTemplate>
                                                    <dx:ASPxMemo ID="txt_description" runat="server" Width="100%" />
                                                </DataItemTemplate>--%>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="5" Caption="In biên nhận">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="print_2">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Print" />
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPanel runat="server" ID="panel_grp3" Visible="false">
                            <PanelCollection>
                                <dx:PanelContent>
                                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Font-Bold="True" Text="Đề nghị xuất kho" />
                                    <dx:ASPxGridView ID="grd_givehanghoa" runat="server"
                                        AutoGenerateColumns="False" Width="100%" KeyFieldName="productid">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                                VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="number" ShowInCustomizationForm="True"
                                                VisibleIndex="3"><%--
                                                <DataItemTemplate>
                                                    <dx:ASPxTextBox ID="txt_price" runat="server" Text='<%# Bind("number") %>' Width="100%">
                                                    </dx:ASPxTextBox>
                                                </DataItemTemplate>--%>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" ShowInCustomizationForm="True"
                                                VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
                                                Visible="false" VisibleIndex="5">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                                VisibleIndex="6">
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                                VisibleIndex="7"><%--
                                                <DataItemTemplate>
                                                    <dx:ASPxMemo ID="txt_note" runat="server" Width="100%">
                                                    </dx:ASPxMemo>
                                                </DataItemTemplate>--%>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <Styles>
                                            <Header HorizontalAlign="Center">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <dx:ASPxButton ID="btn_printReceiptAllproduct" Text="In mẫu đề nghị xuất" runat="server" AutoPostBack="false">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Print" />
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>