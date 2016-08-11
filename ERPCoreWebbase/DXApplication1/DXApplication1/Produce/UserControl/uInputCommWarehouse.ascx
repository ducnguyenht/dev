<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uInputCommWarehouse.ascx.cs"
    ClientIDMode="AutoID" Inherits="WebModule.Produce.UserControl.uInputCommWarehouse" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<style type="text/css">
    .float_right
    {
        float: right;
    }
</style>
<script type="text/javascript">
    function Print() {
        popup_orderdetail.Show();
        popup_orderdetail.SetHeaderText("Phiếu Nhập Kho");
    }
</script>
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine"
    OnCallback="cpLine_Callback">
    <ClientSideEvents EndCallback="cpLine_EndCallback" />
    <ClientSideEvents EndCallback="cpLine_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent ID="PanelContent12" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="formEntryDetail" runat="server" HeaderText="Tạo phiếu nhập kho"
                Height="400px" Modal="True" RenderMode="Lightweight" Width="910px" ClientInstanceName="formEntryDetail"
                AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                LoadingPanelDelay="1000">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeader" runat="server" Text="Tạo Phiếu Nhập Kho" Font-Bold="True"
                                Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxPageControl ID="pc" ClientInstanceName="pc" runat="server" ActiveTabIndex="0"
                                RenderMode="Lightweight" Width="100%" Height="100%">
                                <TabPages>
                                    <dx:TabPage Name="Personal" Text="Chọn chứng từ">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <div style="overflow: auto; height: 450px">
                                                    <div style="margin-bottom: 10px;">
                                                        <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" EnableTheming="True" Theme="Default">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Chọn loại chứng từ">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" SelectedIndex="0">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
    rppurchase.SetVisible(false);
}" />
                                                                                <Items>
                                                                                    <dx:ListEditItem Selected="True" Text="Phiếu mua hàng" Value="purchase" />
                                                                                    <dx:ListEditItem Text="Chuyển kho nội bộ" Value="movewarehouse" />
                                                                                    <dx:ListEditItem Text="Hàng trả về" Value="productreturn" />
                                                                                    <dx:ListEditItem Text="Hàng ký gửi" Value="productcheck" />
                                                                                    <dx:ListEditItem Text="Điều chỉnh tồn kho" Value="productcheck" />
                                                                                    <dx:ListEditItem Text="Nhập kho sản xuất" Value="productcheck" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                        </dx:ASPxFormLayout>
                                                    </div>
                                                    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                                                        OnCallback="cpHeader_Callback" HideContentOnCallback="True">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxRoundPanel ID="rppurchase" Visible="false" ClientInstanceName="rppurchase"
                                                                    runat="server" Width="200px">
                                                                    <PanelCollection>
                                                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                        </dx:PanelContent>
                                                                    </PanelCollection>
                                                                </dx:ASPxRoundPanel>
                                                                <dx:ASPxGridView ID="grdData" Width="100%" runat="server" AutoGenerateColumns="False">
                                                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" />
                                                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}"></ClientSideEvents>
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                                            VisibleIndex="0">
                                                                            <ClearFilterButton Visible="True">
                                                                            </ClearFilterButton>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <HeaderTemplate>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                            </HeaderTemplate>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Mã phiếu mua" ShowInCustomizationForm="True"
                                                                            VisibleIndex="1" FieldName="code">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Ngày mua hàng" ShowInCustomizationForm="True"
                                                                            VisibleIndex="3" FieldName="date">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="suppliername" ShowInCustomizationForm="True"
                                                                            VisibleIndex="2">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" ShowInCustomizationForm="True"
                                                                            VisibleIndex="4" Width="200px">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <Settings ShowFilterRow="True" />
                                                                    <SettingsDetail ShowDetailRow="True" />
                                                                    <Settings ShowFilterRow="True"></Settings>
                                                                    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                                                                    <Templates>
                                                                        <DetailRow>
                                                                            <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="1" RenderMode="Lightweight"
                                                                                Width="100%">
                                                                                <TabPages>
                                                                                    <dx:TabPage Text="Danh sách hàng hóa">
                                                                                        <ContentCollection>
                                                                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxGridView ID="grdDataDetail" runat="server" AutoGenerateColumns="False" OnBeforePerformDataSelect="grdDataDetail_BeforePerformDataSelect"
                                                                                                    Width="100%">
                                                                                                    <ClientSideEvents SelectionChanged="function(s, e) {
		var key = s.GetRowKey(e.visibleIndex);
    
}" />
                                                                                                    <Columns>
                                                                                                        <dx:GridViewDataTextColumn Caption="Mã" FieldName="code" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="1">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewCommandColumn Caption="#" ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                                                                            VisibleIndex="0">
                                                                                                            <ClearFilterButton Visible="True">
                                                                                                            </ClearFilterButton>
                                                                                                        </dx:GridViewCommandColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="2">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="3">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="4">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="5">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="description" ShowInCustomizationForm="True"
                                                                                                            VisibleIndex="6">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                </dx:ASPxGridView>
                                                                                            </dx:ContentControl>
                                                                                        </ContentCollection>
                                                                                    </dx:TabPage>
                                                                                    <dx:TabPage Text="Danh sách hàng khuyến mãi">
                                                                                        <ContentCollection>
                                                                                            <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxCheckBox ID="cbchietkhau" runat="server" CheckState="Checked" Text="Chiết Khấu"
                                                                                                    ClientInstanceName="cbchietkhau" Checked="True">
                                                                                                </dx:ASPxCheckBox>
                                                                                                <dx:ASPxRoundPanel ID="ASPxRoundPanel7" runat="server" HeaderText="" View="GroupBox"
                                                                                                    Width="100%" ClientInstanceName="roundpanelchietkhau">
                                                                                                    <PanelCollection>
                                                                                                        <dx:PanelContent ID="PanelContent6" runat="server" SupportsDisabledAttribute="True">
                                                                                                            <table class="form">
                                                                                                                <tr>
                                                                                                                    <td style="width: 109px;">
                                                                                                                        <dx:ASPxGridView ID="grid_hanghoachietsuat" Caption="Danh mục hàng hóa" runat="server"
                                                                                                                            AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_hanghoachietsuat_BeforePerformDataSelect">
                                                                                                                            <Columns>
                                                                                                                                <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                                                    Caption="Áp dụng" Width="100px" ShowSelectCheckbox="True">
                                                                                                                                </dx:GridViewCommandColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="STT" FieldName="sequenceno" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="1" Visible="false">
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="2">
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="3">
                                                                                                                                    <FooterTemplate>
                                                                                                                                        Tổng tiền chiết khấu
                                                                                                                                    </FooterTemplate>
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="productunitid" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="4">
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="5">
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="DonGia" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="6">
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn Caption="Chiết khấu (%)" FieldName="discountrate" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="7">
                                                                                                                                    <DataItemTemplate>
                                                                                                                                        <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Text='<%# Bind("discountrate") %>'
                                                                                                                                            Width="50px">
                                                                                                                                        </dx:ASPxTextBox>
                                                                                                                                    </DataItemTemplate>
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Tiền chiết khấu"
                                                                                                                                    FieldName="discountcash" VisibleIndex="8">
                                                                                                                                    <FooterTemplate>
                                                                                                                                        <dx:ASPxTextBox ID="txt_sumdiscountcash" runat="server" ReadOnly="true" Width="100%"
                                                                                                                                            Text="150.000">
                                                                                                                                        </dx:ASPxTextBox>
                                                                                                                                    </FooterTemplate>
                                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                                <dx:GridViewBandColumn Caption="Điều kiện tặng hàng" ShowInCustomizationForm="True"
                                                                                                                                    VisibleIndex="9" Visible="false">
                                                                                                                                    <Columns>
                                                                                                                                        <dx:GridViewDataTextColumn Caption="Mua" FieldName="condition_buy" ShowInCustomizationForm="True"
                                                                                                                                            VisibleIndex="0">
                                                                                                                                            <DataItemTemplate>
                                                                                                                                                <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Text='<%# Bind("condition_buy") %>'
                                                                                                                                                    Width="50px">
                                                                                                                                                </dx:ASPxTextBox>
                                                                                                                                            </DataItemTemplate>
                                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                                                                            <DataItemTemplate>
                                                                                                                                                ->
                                                                                                                                            </DataItemTemplate>
                                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                                        <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give" ShowInCustomizationForm="True"
                                                                                                                                            VisibleIndex="0">
                                                                                                                                            <DataItemTemplate>
                                                                                                                                                <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Text='<%# Bind("condition_give") %>'
                                                                                                                                                    Width="50px">
                                                                                                                                                </dx:ASPxTextBox>
                                                                                                                                            </DataItemTemplate>
                                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                                    </Columns>
                                                                                                                                </dx:GridViewBandColumn>
                                                                                                                                <dx:GridViewCommandColumn Caption="Duyệt C.K" ButtonType="Image" VisibleIndex="10">
                                                                                                                                    <CustomButtons>
                                                                                                                                        <dx:GridViewCommandColumnCustomButton ID="Inquiry">
                                                                                                                                            <Image ToolTip="Duyệt chiết khấu">
                                                                                                                                                <SpriteProperties CssClass="Sprite_Approve" />
                                                                                                                                            </Image>
                                                                                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                                                                                    </CustomButtons>
                                                                                                                                </dx:GridViewCommandColumn>
                                                                                                                            </Columns>
                                                                                                                            <Settings ShowFooter="True" />
                                                                                                                        </dx:ASPxGridView>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </dx:PanelContent>
                                                                                                    </PanelCollection>
                                                                                                </dx:ASPxRoundPanel>
                                                                                                <dx:ASPxCheckBox ID="cbquatang" runat="server" CheckState="Checked" Text="Quà Tặng"
                                                                                                    ClientInstanceName="cbquatang" Checked="True">
                                                                                                </dx:ASPxCheckBox>
                                                                                                <dx:ASPxRoundPanel ID="ASPxRoundPanel9" HeaderText="" runat="server" View="GroupBox"
                                                                                                    Width="100%" ClientInstanceName="roundpanelquatang">
                                                                                                    <PanelCollection>
                                                                                                        <dx:PanelContent ID="PanelContent7" runat="server" SupportsDisabledAttribute="True">
                                                                                                            <dx:ASPxGridView Caption="Danh mục tặng phẩm" ID="gridview_tangpham" runat="server"
                                                                                                                AutoGenerateColumns="False" Width="100%" OnBeforePerformDataSelect="gridview_tangpham_BeforePerformDataSelect">
                                                                                                                <Columns>
                                                                                                                    <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                                        Caption="Áp dụng" ShowSelectCheckbox="True">
                                                                                                                    </dx:GridViewCommandColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="STT" FieldName="stt" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="1">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Mã quà tặng" FieldName="MaQuaTang" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="2">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="3">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="DonViTinh" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="4">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Số lượng tặng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="5">
                                                                                                                        <DataItemTemplate>
                                                                                                                            <dx:ASPxTextBox ID="txt_number" runat="server" Text='<%# Bind("SoLuong") %>' Width="50px">
                                                                                                                            </dx:ASPxTextBox>
                                                                                                                        </DataItemTemplate>
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Giá Trị" FieldName="GiaTri" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="6">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Phân loại" FieldName="PhanLoai" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="7">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành tiền" FieldName="ThanhTien"
                                                                                                                        VisibleIndex="8">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                                                                                                        VisibleIndex="9">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewCommandColumn Caption="Duyệt K.M" ButtonType="Image" VisibleIndex="10"
                                                                                                                        Visible="false">
                                                                                                                        <CustomButtons>
                                                                                                                            <dx:GridViewCommandColumnCustomButton ID="GridViewCommandColumnCustomButton1">
                                                                                                                                <Image ToolTip="Duyệt chiết khấu">
                                                                                                                                    <SpriteProperties CssClass="Sprite_Approve" />
                                                                                                                                </Image>
                                                                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                                                                        </CustomButtons>
                                                                                                                    </dx:GridViewCommandColumn>
                                                                                                                </Columns>
                                                                                                            </dx:ASPxGridView>
                                                                                                        </dx:PanelContent>
                                                                                                    </PanelCollection>
                                                                                                </dx:ASPxRoundPanel>
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
                                                    </dx:ASPxCallbackPanel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage ClientEnabled="False" Name="Confirmation" Text="Xác nhận phiếu nhập kho">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                <div style="overflow: auto; height: 450px">
                                                    <div style="margin-bottom: 10px;">
                                                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" Theme="Aqua"
                                                            AlignItemCaptionsInAllGroups="True" ColCount="2">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã phiếu nhập">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text="MS0001" Width="170px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Người tạo">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Ngày tạo">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Date="11/02/2009 09:23:00" EditFormat="Custom">
                                                                                <TimeSectionProperties>
                                                                                    <TimeEditProperties EditFormatString="hh:mm tt">
                                                                                    </TimeEditProperties>
                                                                                </TimeSectionProperties>
                                                                            </dx:ASPxDateEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Thủ kho">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Kho">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox4" runat="server">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                        </dx:ASPxFormLayout>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Danh sách mặt hàng nhập kho" Font-Bold="True"
                                                            Font-Size="Small">
                                                        </dx:ASPxLabel>
                                                    </div>
                                                    <dx:ASPxGridView ID="grdDataAccept" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <Settings ShowFilterRow="True"></Settings>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="6">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                    </Image>
                                                                </DeleteButton>
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True"
                                                                Caption="Thuộc chứng từ" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="code" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="name" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="PopupEditForm" />
                                                        <Settings ShowFilterRow="True" />
                                                        <SettingsPopup>
                                                            <EditForm Height="150px" Width="500px" />
                                                            <EditForm Width="500px" Height="150px"></EditForm>
                                                        </SettingsPopup>
                                                    </dx:ASPxGridView>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControlFinish" runat="server">
                                                <div style="overflow: auto; height: 450px">
                                                    <p>
                                                        Phiếu nhập kho đã được tạo.</p>
                                                    <div style="margin-bottom: 10px;">
                                                        <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" EnableTheming="True" Theme="Aqua"
                                                            ColCount="2">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã phiếu nhập">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Enabled="False" Text="MS0001" Width="170px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Người tạo">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Enabled="False" Text="Nhân viên 1"
                                                                                Width="170px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Ngày tạo">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Text="27-07-2013" Width="170px"
                                                                                Enabled="False">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Thủ kho">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox5" runat="server">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Kho">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox6" runat="server">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                        </dx:ASPxFormLayout>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách mặt hàng nhập kho" Font-Bold="True"
                                                            Font-Size="Small">
                                                        </dx:ASPxLabel>
                                                    </div>
                                                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True"
                                                                Caption="Thuộc chứng từ" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="code" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="name" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </div>
                                                <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" ColCount="2" Width="100%">
                                                    <Items>
                                                        <dx:LayoutItem ShowCaption="False" Width="50%">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" CssClass="float_right"
                                                                        Text="In">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Print" />
                                                                            <SpriteProperties CssClass="Sprite_Print"></SpriteProperties>
                                                                        </Image>
                                                                        <ClientSideEvents Click="Print" />
                                                                    </dx:ASPxButton>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ShowCaption="False" Width="50%" CaptionSettings-Location="Right">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxButton ID="btnGoToSchedule" AutoPostBack="false" runat="server" CssClass="afinishAreaButton"
                                                                        HorizontalAlign="Center" Text="Tiếp tục tạo phiếu nhập kho" UseSubmitBehavior="False">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Repeat" />
                                                                            <SpriteProperties CssClass="Sprite_Repeat"></SpriteProperties>
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                            <CaptionSettings Location="Right"></CaptionSettings>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                            <dx:ASPxPanel ID="dxpError" ClientInstanceName="dxpError" runat="server" CssClass="errorPanel"
                                ClientVisible="false">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        Please complete or correct the fields highlighted in red.
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel>
                            <dx:ASPxHiddenField ID="hfRegInfo" ClientInstanceName="hfRegInfo" runat="server"
                                SyncWithServer="true" ViewStateMode="Enabled">
                            </dx:ASPxHiddenField>
                        </div>
                        <div class="buttonsArea">
                            <div class="buttons" align="right">
                                <table cellpadding="0" cellspacing="0" border="0" class="buttonsTable">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
                                                ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
                                                <ClientSideEvents Click="OnBackButtonClick"></ClientSideEvents>
                                                <ClientSideEvents Click="OnBackButtonClick" />
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Backward" />
                                                    <SpriteProperties CssClass="Sprite_Backward"></SpriteProperties>
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
                                                CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
                                                <ClientSideEvents Click="OnNextButtonClick"></ClientSideEvents>
                                                <ClientSideEvents Click="OnNextButtonClick" />
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Forward" />
                                                    <SpriteProperties CssClass="Sprite_Forward"></SpriteProperties>
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
                                                ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất">
                                                <ClientSideEvents Click="OnFinishButtonClick" />
                                                <ClientSideEvents Click="OnFinishButtonClick"></ClientSideEvents>
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Finished" />
                                                    <SpriteProperties CssClass="Sprite_Finished"></SpriteProperties>
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:ASPxPopupControl ID="popup_report" runat="server" CloseAction="CloseButton" ClientInstanceName="popup_orderdetail"
    AllowDragging="True" AllowResize="True" PopupAnimationType="None" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" EnableViewState="False" HeaderText="" Height="560px"
    Width="800px" DragElement="Window">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
            <div style="height: 100%; width: 100%; overflow: hidden">
                <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" FullscreenMode="True" Width="100%"
                    Height="100%" Orientation="Vertical" SeparatorVisible="false" ClientInstanceName="PrintLayoutSplitter">
                    <Panes>
                        <dx:SplitterPane Size="40" Name="ToolbarPane" MinSize="20">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                    <dx:ReportToolbar ID="ReportToolbar1" runat="server" ReportViewerID="ReportViewer1"
                                        ShowDefaultButtons="False">
                                        <Items>
                                            <dx:ReportToolbarButton ItemKind="Search" />
                                            <dx:ReportToolbarSeparator />
                                            <dx:ReportToolbarButton ItemKind="PrintReport" />
                                            <dx:ReportToolbarButton ItemKind="PrintPage" />
                                            <dx:ReportToolbarSeparator />
                                            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                            <dx:ReportToolbarLabel ItemKind="PageLabel" />
                                            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                            </dx:ReportToolbarComboBox>
                                            <dx:ReportToolbarLabel ItemKind="OfLabel" />
                                            <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                            <dx:ReportToolbarButton ItemKind="NextPage" />
                                            <dx:ReportToolbarButton ItemKind="LastPage" />
                                            <dx:ReportToolbarSeparator />
                                            <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                            <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                                <Elements>
                                                    <dx:ListElement Value="pdf" />
                                                    <dx:ListElement Value="xls" />
                                                    <dx:ListElement Value="xlsx" />
                                                    <dx:ListElement Value="rtf" />
                                                    <dx:ListElement Value="mht" />
                                                    <dx:ListElement Value="html" />
                                                    <dx:ListElement Value="txt" />
                                                    <dx:ListElement Value="csv" />
                                                    <dx:ListElement Value="png" />
                                                </Elements>
                                            </dx:ReportToolbarComboBox>
                                        </Items>
                                        <Styles>
                                            <LabelStyle>
                                                <Margins MarginLeft="3px" MarginRight="3px" />
                                            </LabelStyle>
                                        </Styles>
                                    </dx:ReportToolbar>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane ScrollBars="Auto">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                    <dx:ReportViewer ID="ReportViewer1" runat="server" Border-BorderStyle="Solid" Border-BorderWidth="1px"
                                        Border-BorderColor="Black" Report="<%# new WebModule.Warehouse.Report._01_VT() %>"
                                        ReportName="WebModule.Warehouse.Report._01_VT">
                                        <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></Border>
                                    </dx:ReportViewer>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <Styles>
                        <Pane HorizontalAlign="Center">
                            <Paddings Padding="0" />
                            <Border BorderWidth="0" />
                        </Pane>
                    </Styles>
                </dx:ASPxSplitter>
            </div>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents EndCallback="popup_orderdetail_EndCallback" />
</dx:ASPxPopupControl>
