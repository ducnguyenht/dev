<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="test_promotional_program.ascx.cs" Inherits="DXApplication1.GUI.usercontrol.test_promotional_program" %>
<style type="text/css">
.dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma, Geneva, sans-serif;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
	color: Black;
}
.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px 5px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}

.dxgv 
{
    white-space: nowrap;
    text-overflow: ellipsis;
}

.dxgvCommandColumn
{
	padding: 2px;
}
.dxgvFooter
{
	background-color: #D7D7D7;
	white-space: nowrap;
}

.dxgvPagerTopPanel,
.dxgvPagerBottomPanel
{
	padding-top: 4px;
	padding-bottom: 4px;
}

    .style1
    {
        text-align: Center;
        color: Black;
        border-left-width: 0px;
        border-top-width: 0px;
    }
    .style2
    {
        text-align: Center;
        color: Black;
        border-left-width: 0px;
        border-right-width: 0px;
        border-top-width: 0px;
    }
    .style3
    {
        margin: 0;
        padding: 0;
    }
</style>
<script type="text/javascript">

    function checkboxchietkhau_invi(s, e) {
        gridview_cauhinhhhtest.SetVisible(s.GetChecked());
    }
</script>

<dx:ASPxFormLayout ID="form_testPromotion" runat="server">
    <Items>
        <dx:LayoutGroup Caption="Cấu hình thông tin tham khảo" ColCount="2">
            <Items>
                <dx:LayoutItem Caption="Ngày mua giả định" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxDateEdit ID="txt_date" runat="server">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Mã KH giả định" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="txt_customer" runat="server" Width="170px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ColSpan="2" ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxRadioButton ID="rdo_real" runat="server" 
                                Text="Theo tổng doanh số hiện có">
                            </dx:ASPxRadioButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxRadioButton ID="rdo_customize" runat="server" 
                                Text="Theo tổng doanh số giả định">
                            </dx:ASPxRadioButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                    <HelpTextSettings HorizontalAlign="Left" />
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxSpinEdit ID="txt_custsales" runat="server" Height="21px" Number="0">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:TabbedLayoutGroup ActiveTabIndex="0">
            <Items>
                <dx:LayoutGroup Caption="Thông tin phiếu mua" ShowCaption="False">
                    <Items>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="gridview_hanghoa" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="gridview_hanghoa" Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" 
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" 
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                <FooterTemplate>
                                                    Cộng
                                                </FooterTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" 
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="number" 
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                                <DataItemTemplate>
                                                    <dx:ASPxTextBox ID="txt_price" runat="server" Text='<%# Bind("number") %>' 
                                                        Width="80px">
                                                    </dx:ASPxTextBox>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" 
                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                                <DataItemTemplate>
                                                    <dx:ASPxTextBox ID="txt_price0" runat="server" Text='<%# Bind("priceunit") %>' 
                                                        Width="150px">
                                                    </dx:ASPxTextBox>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" 
                                                ShowInCustomizationForm="True" VisibleIndex="5">
                                                <FooterTemplate>
                                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" ReadOnly="true" 
                                                        Text="7.500.000" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </FooterTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" 
                                                ShowInCustomizationForm="True" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="duedate" 
                                                ShowInCustomizationForm="True" VisibleIndex="7">
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" 
                                                ShowInCustomizationForm="True" VisibleIndex="8">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                ShowInCustomizationForm="True" VisibleIndex="9">
                                                <DeleteButton Visible="True">
                                                    <Image ToolTip="Xóa">
                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                    </Image>
                                                </DeleteButton>
                                                <ClearFilterButton Visible="True">
                                                    <Image ToolTip="Xóa">
                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                    </Image>
                                                </ClearFilterButton>
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="add_product">
                                                        <Image ToolTip="Thêm">
                                                            <SpriteProperties CssClass="Sprite_New" />
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <Settings ShowFooter="True" />
                                        <Styles>
                                            <Header HorizontalAlign="Center">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Cấu hình hàng hóa trên phiếu mua" ShowCaption="False">
                    <Items>
                        <dx:LayoutItem Caption="Cấu hình doanh số cho từng hàng hóa" 
                            ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxCheckBox ID="chk_configOnProduct" runat="server" CheckState="Unchecked" 
                                        ClientInstanceName="cb_configOnProduct" 
                                        Text="Cấu hình doanh số cho từng mặt hàng">
                                        <ClientSideEvents CheckedChanged="checkboxchietkhau_invi" />
                                    </dx:ASPxCheckBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="gridview_cauhinhhhtest" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="gridview_cauhinhhhtest" 
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
                                            <dx:GridViewDataTextColumn Caption="Doanh số theo hàng hóa" 
                                                FieldName="totalsale" ShowInCustomizationForm="True" VisibleIndex="10">
                                                <Settings AllowDragDrop="False" />
                                                <DataItemTemplate>
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" ColCount="2">
                                                        <Items>
                                                            <dx:LayoutItem ColSpan="2" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" 
                                                                        runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxRadioButton ID="rdo_realSaleRow" runat="server" 
                                                                            Text="Doanh số thực tế ">
                                                                        </dx:ASPxRadioButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" 
                                                                        runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxRadioButton ID="rdo_customizeSaleRow" runat="server" 
                                                                            Text="Doanh số giả định" Width="79px">
                                                                        </dx:ASPxRadioButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" 
                                                                        runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxSpinEdit ID="txt_customizeSaleRow" runat="server" Height="21px" 
                                                                            Number="0" Value='<%# Bind("totalsale") %>'>
                                                                        </dx:ASPxSpinEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFilterRow="True" />
                                    </dx:ASPxGridView>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:TabbedLayoutGroup>
    </Items>
</dx:ASPxFormLayout>

<dx:ASPxButton ID="btn_test" runat="server" AutoPostBack="False" 
    Text="Kiểm tra chương trình khuyến mãi" Width="222px">
</dx:ASPxButton>

<br />

