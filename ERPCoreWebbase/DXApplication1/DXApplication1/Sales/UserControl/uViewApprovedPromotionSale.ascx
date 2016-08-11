<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uViewApprovedPromotionSale.ascx.cs" Inherits="WebModule.GUI.usercontrol.uViewApprovedPromotionSale" %>
 <style type="text/css">

.dxeBase
{
	font: 12px Tahoma, Geneva, sans-serif;
}
.dxeTrackBar, 
.dxeIRadioButton, 
.dxeButtonEdit, 
.dxeTextBox, 
.dxeRadioButtonList, 
.dxeCheckBoxList, 
.dxeMemo, 
.dxeListBox, 
.dxeCalendar, 
.dxeColorTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxeTextBox,
.dxeButtonEdit,
.dxeIRadioButton,
.dxeRadioButtonList,
.dxeCheckBoxList
{
    cursor: default;
}

.dxeTextBox,
.dxeMemo
{
	background-color: white;
	border: 1px solid #9f9f9f;
}

td.dxic
{
	font-size: 0;
}

.dxeTextBox .dxeEditArea
{
	background-color: white;
}

.dxeEditArea
{
	font: 12px Tahoma, Geneva, sans-serif;
	border: 1px solid #A0A0A0;
}
     .style1
     {
         border-width: 0;
         background-image: url('mvwres://DevExpress.Web.v13.1,%20Version=13.1.5.0,%20Culture=neutral,%20PublicKeyToken=b88d1754d700e49a/DevExpress.Web.Images.sprite.png');
     }
 </style>
 <script type="text/javascript">
</script>
<dx:ASPxPanel ID="panel_viewPromotion" ClientInstanceName="panel_viewPromotion" runat="server" Height="190px" Width="100%" ScrollBars="Vertical">
    <PanelCollection>
        <dx:PanelContent>
<dx:ASPxPageControl ID="PageControl_Viewapprovekm" runat="server" 
    RenderMode="Classic" ActiveTabIndex="0" Width="100%" ClientInstanceName="PageControl_Viewapprovekm">
    <TabPages>
        <dx:TabPage Text="Thông tin chính sách K.M">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxFormLayout ID="form_policyInfoCommon" runat="server">
                        <Items>
                            <dx:LayoutItem Caption="Mã khuyến mãi">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="form_policyInfoCommon_E1" runat="server" Text="KM0001">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Mức khuyến mãi">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="form_policyInfoCommon_E2" runat="server" Text="KM0001_1">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Thời hạn áp dụng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="form_policyInfoCommon_E3" runat="server" Text="01/06/2012 - 01/07/2012">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
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
                                                                <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="productid" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="productname"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="productunitid" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số Lo" FieldName="lotid" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="3" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" FieldName="duedate" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="3" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewBandColumn Caption="Điều Kiện Tặng Hàng" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="Mua" FieldName="condition_buy"
                                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn 
                                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                                            <DataItemTemplate>
                                                                                ->
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give"
                                                                            ShowInCustomizationForm="True" VisibleIndex="2">
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
        <dx:TabPage Text="Quyền lợi đã nhận">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel7" runat="server" View="Standard" ShowHeader="false" HeaderText="Chiết khấu"
                        Width="100%" ClientInstanceName="roundpanelchietkhau">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent9" runat="server" 
                                SupportsDisabledAttribute="True">
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
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel9" runat="server" View="Standard" ShowHeader="false" HeaderText="Quà tặng"
                        Width="100%" ClientInstanceName="roundpanelquatang">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent7" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="lbl_titlegrv_tangpham" Text="Danh mục tặng phẩm" runat="server" AssociatedControlID="gridview_tangpham" Font-Bold="true"/>
                                <dx:ASPxGridView ID="gridview_tangpham" runat="server"
                                    AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                            Caption="Áp dụng" ShowSelectCheckbox="True" Visible="false">
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
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="GiaTri" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành tiền" FieldName="ThanhTien"
                                            VisibleIndex="6">
                                            <DataItemTemplate>
                                            .........
                                            </DataItemTemplate>
                                            <FooterTemplate>
                                                <dx:ASPxTextBox ID="txtTotal" Text="........." runat="server" ReadOnly="true">
                                                </dx:ASPxTextBox>
                                            </FooterTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Sô Lô" FieldName="lotid" ShowInCustomizationForm="True"
                                            VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                            VisibleIndex="8">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                            VisibleIndex="9">
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
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxPanel>
        
               

 