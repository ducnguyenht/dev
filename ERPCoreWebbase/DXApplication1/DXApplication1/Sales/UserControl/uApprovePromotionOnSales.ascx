<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uApprovePromotionOnSales.ascx.cs" Inherits="WebModule.GUI.usercontrol.uApprovePromotionOnSales" %>
 <script type="text/javascript">
     function btnrework_click(s, e) {
         var currentTab = PageControl_approvekm.GetTab(PageControl_approvekm.GetActiveTabIndex());
         currentTab.SetEnabled(false);
         PageControl_approvekm.SetActiveTabIndex(PageControl_approvekm.GetActiveTabIndex() - 1);
         btnrework.SetVisible(false);
         btnfinish.SetVisible(true);
     }

     function btnfinish_click(s, e) {
         var nextTab = PageControl_approvekm.GetTab(1);
         var currentTab = PageControl_approvekm.GetTab(0);
         nextTab.SetEnabled(true);
         currentTab.SetEnabled(false);
         PageControl_approvekm.SetActiveTab(nextTab);
         btnrework.SetVisible(true);
         btnfinish.SetVisible(false);
     }

    </script>
            <dx:ASPxPageControl ID="PageControl_khuyenmai" runat="server" 
                RenderMode="Classic" ActiveTabIndex="0" Width="100%" Height="100%" ClientInstanceName="PageControl_approvekm">
                <TabPages>
                    <dx:TabPage Text="Duyệt khuyến mãi">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxLabel ID="lbl_title_khuyenmai" runat="server" 
                                    Text="Danh mục khuyến mãi có thể áp dụng" Font-Bold="True" />
                                <dx:ASPxPanel ID="panel_approvePromotion" ClientInstanceName="panel_approvePromotion" runat="server" ScrollBars="Auto" Height="230px" Border-BorderStyle="None" >
                                    <PanelCollection>
                                        <dx:PanelContent>
                                <dx:ASPxGridView ID="gridview_applykm" KeyFieldName="id" runat="server" AutoGenerateColumns="False"
                                    Width="100%" OnHtmlRowCreated="gridview_applykm_HtmlRowCreated">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" 
                                            ShowSelectCheckbox="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã khuyến mãi" 
                                            FieldName="id" ShowInCustomizationForm="True" VisibleIndex="1" 
                                            SortIndex="3" SortOrder="Ascending">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên khuyến mãi" 
                                            FieldName="name" ShowInCustomizationForm="True" VisibleIndex="2" 
                                            SortIndex="3" SortOrder="Ascending">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tổng giá trị K.M" FieldName="TotalBonus"
                                            ShowInCustomizationForm="True" VisibleIndex="3" 
                                            SortIndex="3" SortOrder="Ascending">
                                            <CellStyle HorizontalAlign="Right">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewBandColumn Caption="Hiệu lực" ShowInCustomizationForm="True" 
                                            VisibleIndex="4">
                                            <Columns>
                                                <dx:GridViewDataDateColumn Caption="Từ" FieldName="from" 
                                                    ShowInCustomizationForm="True" VisibleIndex="0" SortIndex="1" 
                                                    SortOrder="Ascending">
                                                    <PropertiesDateEdit DisplayFormatString="">
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                                <dx:GridViewDataDateColumn Caption="Đến" FieldName="to" 
                                                    ShowInCustomizationForm="True" VisibleIndex="1" SortIndex="0" 
                                                    SortOrder="Ascending">
                                                    <PropertiesDateEdit DisplayFormatString="">
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="description" 
                                            ShowInCustomizationForm="True" VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                    <SettingsDetail ShowDetailRow="True" />
                                    <Styles>
                                        <Header HorizontalAlign="Center">
                                        </Header>
                                    </Styles>
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxPageControl ID="tab_stepkhuyenmai" runat="server" 
                                                RenderMode="Lightweight" Width="100%" ActiveTabIndex="0">
                                                <TabPages>
                                                    <dx:TabPage Text="Mức khuyến mãi hiện tại">
                                                        <ContentCollection>
                                                            <dx:ContentControl>
                                                                <dx:ASPxNavBar ID="navbar_info" runat="server" RenderMode="Lightweight" 
                                                                    Width="100%">
                                                                    <Groups>
                                                                        <dx:NavBarGroup Text="Điều kiện áp dụng">
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
                                                                        <dx:NavBarGroup Text="Quyền lợi" ShowExpandButton="True">
                                                                            <ContentTemplate>
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
                                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                                                                    SupportsDisabledAttribute="True">
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
                                                                                                                VisibleIndex="4" Visible="false">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewBandColumn Caption="Điều kiện tặng hàng" 
                                                                                                                ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                                                <Columns>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Mua" FieldName="condition_buy"
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                                                        <%--<DataItemTemplate>
                                                                                                                            <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" 
                                                                                                                                Text='<%# Bind("condition_buy") %>' Width="50px">
                                                                                                                            </dx:ASPxTextBox>
                                                                                                                        </DataItemTemplate>--%>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn 
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                                        <DataItemTemplate>
                                                                                                                            ->
                                                                                                                        </DataItemTemplate>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give"
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                                        <%--<DataItemTemplate>
                                                                                                                            <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" 
                                                                                                                                Text='<%# Bind("condition_give") %>' Width="50px">
                                                                                                                            </dx:ASPxTextBox>
                                                                                                                        </DataItemTemplate>--%>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                </Columns>
                                                                                                            </dx:GridViewBandColumn>
                                                                                                        </Columns>
                                                                                                    </dx:ASPxGridView>
                                                                                                    <dx:ASPxLabel ID="lbl_title_khuyenmai3" runat="server" 
                                                                                                        Text="Danh mục tặng phẩm kèm theo" Font-Bold="True" />
                                                                                                    <dx:ASPxGridView ID="gridview_hanghoabonus" runat="server" AutoGenerateColumns="False">
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
                                                                                                                <CellStyle HorizontalAlign="Right"></CellStyle>
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
                                                                            </ContentTemplate>
                                                                        </dx:NavBarGroup>
                                                                    </Groups>
                                                                </dx:ASPxNavBar>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Text="Mức khuyến mãi kế tiếp">
                                                        <ContentCollection>
                                                            <dx:ContentControl>
                                                                <dx:ASPxNavBar ID="navi_nextinfo" runat="server" RenderMode="Lightweight" Width="862px">
                                                                    <Groups>
                                                                        <dx:NavBarGroup Text="Điều kiện áp dụng">
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
                                                                                                    <br />
                                                                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" ForeColor="#CC0000" Text="và">
                                                                                                    </dx:ASPxLabel>
                                                                                                    <br />
                                                                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" ForeColor="#0000CC" Font-Underline="true"
                                                                                                        Text="Doanh số mã hàng SP00001 từ 2.000.000 VNĐ trở lên">
                                                                                                    </dx:ASPxLabel>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:ASPxFormLayout>
                                                                            </ContentTemplate>
                                                                        </dx:NavBarGroup>
                                                                        <dx:NavBarGroup Text="Quyền lợi">
                                                                            <ContentTemplate>
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
                                                                                        <dx:LayoutItem ShowCaption="False">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                                                                    SupportsDisabledAttribute="True">
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
                                                                                                                        <%--<DataItemTemplate>
                                                                                                                            <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" 
                                                                                                                                Text='<%# Bind("condition_buy") %>' Width="50px">
                                                                                                                            </dx:ASPxTextBox>
                                                                                                                        </DataItemTemplate>--%>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn 
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                                        <DataItemTemplate>
                                                                                                                            ->
                                                                                                                        </DataItemTemplate>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give"
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                                        <%--<DataItemTemplate>
                                                                                                                            <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" 
                                                                                                                                Text='<%# Bind("condition_give") %>' Width="50px">
                                                                                                                            </dx:ASPxTextBox>
                                                                                                                        </DataItemTemplate>--%>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                </Columns>
                                                                                                            </dx:GridViewBandColumn>
                                                                                                        </Columns>
                                                                                                    </dx:ASPxGridView>
                                                                                                    <dx:ASPxLabel ID="lbl_title_khuyenmai3" runat="server" 
                                                                                                        Text="Danh mục tặng phẩm kèm theo" Font-Bold="True" />
                                                                                                    <dx:ASPxGridView Caption="Danh mục tặng phẩm khác" ID="gridview_hanghoabonus" runat="server" AutoGenerateColumns="False">
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
                                                                            </ContentTemplate>
                                                                        </dx:NavBarGroup>
                                                                    </Groups>
                                                                </dx:ASPxNavBar>
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                </TabPages>
                                            </dx:ASPxPageControl>
                                        </DetailRow>
                                    </Templates>
                                </dx:ASPxGridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                    <Border BorderStyle="None"></Border>
                                </dx:ASPxPanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Quyền lợi đạt được" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxCheckBox ID="cbchietkhau" runat="server" CheckState="Checked" Text="Chiết Khấu"
                                    ClientInstanceName="cbchietkhau" Checked="True">
                                </dx:ASPxCheckBox>
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel7" runat="server" View="Standard" ShowHeader="false"
                                    Width="100%" ClientInstanceName="roundpanelchietkhau">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent6" runat="server" SupportsDisabledAttribute="True">
                                            <table class="form">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lbl_tongtienck" runat="server" AssociatedControlID="txt_tongtienck"
                                                            Text="Tổng tiền C.K trên phiếu">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txt_tongtienck" ReadOnly="true" runat="server" Width="170px" Text="150.000">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>
                                <dx:ASPxCheckBox ID="cbquatang" runat="server" CheckState="Checked" Text="Quà Tặng"
                                    ClientInstanceName="cbquatang" Checked="True">
                                </dx:ASPxCheckBox>
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel9" runat="server" View="Standard" ShowHeader="false"
                                    Width="100%" ClientInstanceName="roundpanelquatang">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent7" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lbl_titlegrv_tangpham" Text="Danh mục tặng phẩm" runat="server" AssociatedControlID="gridview_tangpham" Font-Bold="true"/>
                                            <dx:ASPxGridView ID="gridview_tangpham" runat="server"
                                                AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                        Caption="Áp dụng" ShowSelectCheckbox="True">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã quà tặng" FieldName="MaQuaTang" ShowInCustomizationForm="True"
                                                        VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                                        VisibleIndex="2">
                                                        <FooterTemplate>
                                                            Cộng
                                                        </FooterTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="DonViTinh" ShowInCustomizationForm="True"
                                                        VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lượng tặng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                                        VisibleIndex="4">
                                                        <DataItemTemplate>
                                                            <dx:ASPxTextBox ID="txt_number" runat="server" Text='<%# Bind("SoLuong") %>' Width="50px">
                                                            </dx:ASPxTextBox>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="GiaTri" ShowInCustomizationForm="True"
                                                        VisibleIndex="5">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số Lô" FieldName="lotid" ShowInCustomizationForm="True" 
                                                        VisibleIndex="6" Visible="true">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" FieldName="duedate" ShowInCustomizationForm="True" 
                                                        VisibleIndex="7" Visible="true">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành tiền" FieldName="ThanhTien"
                                                        VisibleIndex="9">
                                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                                        <DataItemTemplate>
                                                            .........
                                                        </DataItemTemplate>
                                                        <FooterTemplate>
                                                            <dx:ASPxTextBox ID="txtTotal" runat="server" Text="........."></dx:ASPxTextBox>
                                                        </FooterTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                                        VisibleIndex="10">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowFooter="true" />
                                            </dx:ASPxGridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        <%--</dx:PanelContent>
    </PanelCollection>
</dx:ASPxPanel>--%>
<table>
    <tr>
        <td>
            <dx:ASPxButton ID="btnfinish" runat="server" Text="Chấp nhận khuyến mãi" 
                AutoPostBack="false" ClientInstanceName="btnfinish">
                <ClientSideEvents Click="btnfinish_click" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnrework" runat="server" Text="Duyệt lại" AutoPostBack="false" ClientInstanceName="btnrework" ClientVisible="false">
                <ClientSideEvents Click="btnrework_click" />
            </dx:ASPxButton>
        </td>
    </tr>
</table>
 


 