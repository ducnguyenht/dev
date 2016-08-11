<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uCreateVirtualSalesTestingPromotion.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uCreateVirtualSalesTestingPromotion" %>
<%@ Register Src="~/Sales/UserControl/uApprovePromotionOnSales.ascx" TagName="uApprovePromotionOnSales"
    TagPrefix="uc3" %>
<dx:ASPxPopupControl ID="popup_testPromotion" runat="server" CloseAction="CloseButton"
    ClientInstanceName="popup_testPromotion" AllowDragging="True" AllowResize="True" 
    PopupAnimationType="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    EnableViewState="False" HeaderText="Kiểm Tra Chương Trình Khuyến Mãi" Width="1000px" Height="600px" ScrollBars="Auto" ShowFooter="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" Width="100%">
                <Items>
                    <dx:LayoutGroup Caption="Layout Group" ColCount="3" GroupBoxDecoration="None">
                        <Items>
                            <dx:LayoutItem Caption="Mã số">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tên khách hàng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="170px" ValueType="System.String">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ngày lập">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="ASPxFormLayout3_E1" runat="server">
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="NV bán hàng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="NV duyệt giá">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Cộng tác viên" ShowCaption="True">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="cbo_ctvien" runat="server">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
                <SettingsItemCaptions HorizontalAlign="Right" />
                <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
            </dx:ASPxFormLayout>
            <dx:ASPxPageControl ID="pagetab_orderdetail" ClientInstanceName="pagetab_orderdetail" runat="server" ActiveTabIndex="0"
                Width="100%" Height="75%" EnableViewState="False">
                <TabPages>
                    <dx:TabPage Text="Hàng hóa">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxGridView ID="grv_products" ClientInstanceName="grv_products"
                                    runat="server" AutoGenerateColumns="False"
                                    Width="100%" KeyFieldName="productid">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="STT" FieldName="stt" ShowInCustomizationForm="True"
                                            VisibleIndex="0" Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="cbo_dshanghoa" runat="server" ValueField="productid" TextField="productid"
                                                    ValueType="System.String"
                                                    IncrementalFilteringMode="Contains" Value='<%# Eval("productid") %>'>
                                                    <Items>
                                                        <dx:ListEditItem Value="SP00001" Text="SP00001" />
                                                        <dx:ListEditItem Value="SP00002" Text="SP00002" />
                                                        <dx:ListEditItem Value="SP00003" Text="SP00003" />
                                                        <dx:ListEditItem Value="SP00004" Text="SP00004" />
                                                        <dx:ListEditItem Value="SP00005" Text="SP00005" />
                                                        <dx:ListEditItem Value="SP00007" Text="SP00007" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </DataItemTemplate>
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
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="txt_price" runat="server" Text='<%# Bind("number") %>' Width="80px">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="txt_price" runat="server" Text='<%# Bind("priceunit") %>' Width="150px">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
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
                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                            VisibleIndex="10">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="add_product">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_New" />
                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <DeleteButton Visible="True">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                </Image>
                                            </DeleteButton>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <Settings ShowFooter="True" />
                                    <Settings ShowFooter="True"></Settings>
                                    <Styles>
                                        <Header HorizontalAlign="Center">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridView>
                                <br />
                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                                    <Items>
                                        <dx:LayoutGroup Caption="Layout Item" ColCount="2" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem Caption="Thuế suất GTGT">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxSpinEdit HorizontalAlign="Right" ID="ASPxSpinEdit3" runat="server" Height="21px"
                                                                Number="0">
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxSpinEdit HorizontalAlign="Right" ID="ASPxSpinEdit6" runat="server" Height="21px"
                                                                Number="0">
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px" Visible="false">
                                                                <Border BorderStyle="None" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tổng tiền hàng hóa" HorizontalAlign="Right">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxSpinEdit HorizontalAlign="Right" ID="ASPxSpinEdit4" runat="server" Height="21px"
                                                                Number="0">
                                                            </dx:ASPxSpinEdit>
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
                                <dx:ASPxGridView ID="grv_services" runat="server" AutoGenerateColumns="False"
                                    Width="100%">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã số dịch vụ" FieldName="serviceid" ShowInCustomizationForm="True"
                                            VisibleIndex="0">
                                            <DataItemTemplate>
                                                <dx:ASPxComboBox ID="cbo_dsdichvu" runat="server" ValueField="serviceid" TextField="serviceid"
                                                    ValueType="System.String" IncrementalFilteringMode="Contains" Value='<%# Eval("serviceid") %>'>
                                                    <Items>
                                                        <dx:ListEditItem Value="DV00001" Text="DV00001" />
                                                        <dx:ListEditItem Value="DV00002" Text="DV00002" />
                                                        <dx:ListEditItem Value="DV00003" Text="DV00003" />
                                                        <dx:ListEditItem Value="DV00004" Text="DV00004" />
                                                        <dx:ListEditItem Value="DV00005" Text="DV00005" />
                                                        <dx:ListEditItem Value="DV00006" Text="DV00006" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </DataItemTemplate>
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
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="txt_numberdv" runat="server" Text='<%# Bind("number") %>' Width="80px">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="price" ShowInCustomizationForm="True" Caption="Đơn giá"
                                            VisibleIndex="4">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="txt_pricedv" runat="server" Text='<%# Bind("price") %>' Width="150px">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                            <FooterTemplate>
                                                <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100%" Text="10.325.000" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </FooterTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="txt_notedv" runat="server" Text='<%# Bind("note") %>' Width="150px">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="8"
                                            ButtonType="Image">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="add_service">
                                                    <Image ToolTip="Thêm">
                                                        <SpriteProperties CssClass="Sprite_New" />
                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <DeleteButton Visible="True">
                                                <Image ToolTip="Xóa">
                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                </Image>
                                            </DeleteButton>
                                            <ClearFilterButton Visible="True">
                                                <Image ToolTip="Xóa">
                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                    <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                </Image>
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <Settings ShowFooter="True" />
                                    <Settings ShowFooter="True"></Settings>
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
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit14" runat="server" Height="21px" Number="0">
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit15" runat="server" Height="21px" Number="0">
                                                            </dx:ASPxSpinEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px" Visible="false">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxSpinEdit ID="ASPxSpinEdit16" runat="server" Height="21px" Number="0">
                                                            </dx:ASPxSpinEdit>
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
                                    <uc3:uApprovePromotionOnSales ID="uApprovePromotionOnSales1" runat="server"/>
                                </dx:ContentControl>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <Paddings Padding="5px" />
                <Paddings Padding="5px"></Paddings>
            </dx:ASPxPageControl>
            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="3" Width="100%">
                <Items>
                    <dx:LayoutItem Caption="Tổng giá trị phiếu bán hàng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxSpinEdit ID="ASPxSpinEdit2" runat="server" Height="21px" Number="0" HorizontalAlign="Right">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Xuất hóa đơn GTGT">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxCheckBox ID="cbPrintOrder" runat="server">
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Chú thích">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxMemo ID="memoChuThich" runat="server" Rows="3" Columns="50">
                                </dx:ASPxMemo>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:ASPxFormLayout>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <div style="float:left">
            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                <Image>
                    <SpriteProperties CssClass="Sprite_Help"></SpriteProperties>
                </Image>
            </dx:ASPxButton>
        </div>
    </FooterContentTemplate>
</dx:ASPxPopupControl>