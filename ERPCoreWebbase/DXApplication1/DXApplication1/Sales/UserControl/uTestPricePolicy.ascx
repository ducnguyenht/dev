<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uTestPricePolicy.ascx.cs" Inherits="WebModule.GUI.usercontrol.uTestPricePolicy" %>
<%--<%@ Register Src="~/Sales/UserControl/uSettingTestSalesTotal.ascx" TagName="uSettingTestSalesTotal"
    TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/uSettingTestSalesTotalByProduct.ascx" TagName="uSettingTestSalesTotalByProduct"
    TagPrefix="uc2" %> 
<%@ Register Src="~/Sales/UserControl/uSettingTestSaleTotalByProductGrp.ascx" TagName="uSettingTestSaleTotalByProductGrp"
    TagPrefix="uc3" %> 
<%@ Register Src="~/Sales/UserControl/uSettingTestSalesTotalByValueOfOrder.ascx" TagName="uSettingTestSalesTotalByValueOfOrder"
    TagPrefix="uc4" %> --%>
<%--<script type="text/javascript">

    function checkbox_invi(s, e) {
        grv_configproducts.SetVisible(s.GetChecked());
    }

    function init_grv() {
        grv_configproducts.SetVisible(1);
        grid_thamkhaogia.SetVisible(0);
        lbl_titlethamkhao.SetVisible(0);
    }

    function grid_thamkhaogia_callback(s, e) {
        grid_thamkhaogia.PerformCallback("hhb");
        grid_thamkhaogia.SetVisible(1);
        lbl_titlethamkhao.SetVisible(1);
    }

    function btn_test_click(s, e) {
        popup_referencePrice.Show();
    }
</script>--%>

<%--<uc1:uSettingTestSalesTotal ID="uSettingTestSalesTotal1" runat="server" />
<uc2:uSettingTestSalesTotalByProduct ID="uSettingTestSalesTotalByProduct1" runat="server" />
<uc3:uSettingTestSaleTotalByProductGrp ID="uSettingTestSaleTotalByProductGrp1" runat="server" />
<uc4:uSettingTestSalesTotalByValueOfOrder ID="uSettingTestSalesTotalByValueOfOrder1" runat="server" />

<dx:ASPxRoundPanel runat="server" HeaderText="Cấu hình các thông tin giả lập"
    Height="100%" View="GroupBox" Width="100%">
    <PanelCollection>
        <dx:PanelContent runat="server">
            <dx:ASPxPanel runat="server" Height="100%" ScrollBars="Auto" Width="100%">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxNavBar ID="Navbar_testprice" runat="server" Width="100%">
                            <Groups>
                                <dx:NavBarGroup Expanded="False" Text="Khách Hàng">
                                    <ContentTemplate>
                                        <dx:ASPxFormLayout ID="form_grp1" runat="server" ColCount="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="True" Caption="Khách hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="form_grp1_cbo_grp" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </ContentTemplate>
                                </dx:NavBarGroup>
                                <dx:NavBarGroup Expanded="False" Text="Doanh số">
                                    <ContentTemplate>
                                        <dx:ASPxFormLayout ID="form_doanhso" runat="server" ColCount="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="false" Caption="">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Giả lập doanh số theo">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink1" runat="server" NavigateUrl=""
                                                                Text="Hàng hóa">
                                                                <ClientSideEvents Click="show_uSettingTestSalesTotalByProduct" />
                                                            </dx:ASPxHyperLink>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Và">
                                                            </dx:ASPxLabel>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="Giả lập doanh số theo">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink2" runat="server" NavigateUrl=""
                                                                Text="Nhóm hàng hóa">
                                                                <ClientSideEvents Click="show_uSettingTestSaleTotalByProductGrp" />
                                                            </dx:ASPxHyperLink>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Và">
                                                            </dx:ASPxLabel>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Tổng giá trị của phiếu bán">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink5" runat="server" NavigateUrl=""
                                                                Text="Phiếu bán">
                                                                <ClientSideEvents Click="show_settingTestSalesTotalByValueOfOrder" />
                                                            </dx:ASPxHyperLink>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Và">
                                                            </dx:ASPxLabel>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Giả lập khách hàng đạt tổng">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink6" runat="server" NavigateUrl=""
                                                                Text="Doanh số">
                                                                <ClientSideEvents Click="show_settingTestSalesTotal" />
                                                            </dx:ASPxHyperLink>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </ContentTemplate>
                                </dx:NavBarGroup>
                                <dx:NavBarGroup Expanded="False" Text="Hình thức thanh toán">
                                    <ContentTemplate>
                                        <dx:ASPxFormLayout ID="form_paymenttype" runat="server">
                                            <Items>
                                                <dx:LayoutItem Caption="Hình thức giả lập">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="cbo_selectPaymentType" runat="server">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Chuyển khoản" Value="0" />
                                                                    <dx:ListEditItem Text="Tiền mặt" Value="1" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </ContentTemplate>
                                </dx:NavBarGroup>
                            </Groups>
                        </dx:ASPxNavBar>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
            
<dx:ASPxButton ID="btn_test" runat="server" AutoPostBack="False" 
    Text="Xem giá tham khảo" Width="153px">
    <ClientSideEvents Click="btn_test_click" />
</dx:ASPxButton>
<br />
<dx:aspxpopupcontrol id="popup_referencePrice" clientinstancename="popup_referencePrice" 
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Thông tin giá bán tham khảo" 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="900px" height="400px" ScrollBars="Auto">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">--%>
<dx:ASPxLabel ID="lbl_titlethamkhao" runat="server" Font-Bold="True" 
    Text="Giá tham khảo theo từng mặt hàng" ClientInstanceName="lbl_titlethamkhao">
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
    </Columns>
</dx:ASPxGridView>
    <%--    </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <div style="float: left; margin-right: 4px">
                        <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                            Text="Thoát">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right">
                        <dx:ASPxButton ID="buttonAccept" ClientInstanceName="buttonSave" runat="server" Text="Lưu lại"
                            clientvisible="true">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:aspxpopupcontrol>--%>