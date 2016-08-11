<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="GrossSplitPurchase.aspx.cs" Inherits="WebModule.Purchasing.GrossSplitPurchase" %>
<%@ Register Src="UserControl/ucReportPopup.ascx" TagName="ucReportPopup" TagPrefix="uc1" %>
<%@ Register Src="UserControl/uPopupphieumuaban.ascx" TagName="uPopupphieumuaban" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function cpHeader_EndCallback(s, e) {
        }

        function cpLine_EndCallback(s, e) {
            if (s.cpCodeExisting) {

            }

        }
        // MainForm Event
        function grdData_EndCallback(s, e) {
            if (s.cpEdit) {
                formSupplierGroupEdit.Show();

                ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
                cboRowStatus.SetValue('A');

                if (s.cpEdit == 'edit') {
                    cpLine.PerformCallback('edit');
                }
                else {
                    cpLine.PerformCallback('new');
                }

                delete (s.cpEdit);
                return;
            }
        }

        function grdData_CustomButtonClick(s, e) {
            s.GetRowValues(e.visibleIndex, 'ProductId', OnGetRowValues);
        }

        function OnGetRowValues(values) {
            formSupplierGroupEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

            var str = 'view|' + values;
            cpLine.PerformCallback(str);
        }

        // EditForm Event 
        function buttonSave_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').Validate();
                return;
            }

            cpLine.PerformCallback('save');
            cpHeader.PerformCallback('refresh');

        }

        function buttonCancel_Click(s, e) {
            formSupplierGroupEdit.Hide();
        }



        function link_click(s, e) {
            popup2.Show();
        }

        function checkboxchietkhau_invi(s, e) {

            roundpanelchietkhau.SetVisible(s.GetChecked());

        }
        function checkboxtiengiam_invi(s, e) {
            roundpaneltiengiam.SetVisible(s.GetChecked());
        }
        function checkboxquatang_invi(s, e) {
            roundpanelquatang.SetVisible(s.GetChecked());
        }
    </script>
    <style type="text/css">
        .style1
        {
            background-color: #FFFFFF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:ucReportPopup ID="ucReportPopup1" runat="server" />
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Gộp tách chứng từ mua hàng" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <div style="margin-bottom: 10px;">
        <dx:ASPxPageControl ID="pc" ClientInstanceName="pc" runat="server" ActiveTabIndex="3"
            RenderMode="Lightweight" Width="100%" Height="500px">
            <TabPages>
                <dx:TabPage Text="Chọn chứng từ nguồn" ClientEnabled="True" Name="Personal">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <div style="margin-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblHeader1" runat="server" Text="Danh sách chứng từ mua hàng" Font-Bold="True"
                                        Font-Size="Small">
                                    </dx:ASPxLabel>
                                </div>
                                <dx:ASPxGridView ID="grdData0" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnInitNewRow="grdData0_InitNewRow" OnRowInserting="grdData0_RowInserting" OnStartRowEditing="grdData0_StartRowEditing">
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback" />
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback"></ClientSideEvents>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                            VisibleIndex="0">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Stt" FieldName="order" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="7" Caption="Thao tác">
                                            <EditButton Text="Chi tiết" Visible="True">
                                            </EditButton>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thành tiền phải trả" FieldName="sum" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tràng thái" FieldName="status" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplier" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày mua hàng" FieldName="date" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                    <Settings ShowFilterRow="True"></Settings>
                                </dx:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Name="Payment" ClientEnabled="False" Text="Chọn mục để chuyển">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <div style="margin-bottom: 10px;">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách chứng từ được chọn"
                                        Font-Bold="True" Font-Size="Small">
                                    </dx:ASPxLabel>
                                </div>
                                <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                                    OnCallback="cpHeader_Callback" HideContentOnCallback="True">
                                    <ClientSideEvents EndCallback="cpHeader_EndCallback" />
                                    <ClientSideEvents EndCallback="cpHeader_EndCallback"></ClientSideEvents>
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="grdData" Width="100%" runat="server" AutoGenerateColumns="False">
                                                <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" />
                                                <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}"></ClientSideEvents>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Nhà cung cấp" ShowInCustomizationForm="True"
                                                        VisibleIndex="2" FieldName="supplier">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Ngày tạo" ShowInCustomizationForm="True" VisibleIndex="1"
                                                        FieldName="date">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã Chứng từ" FieldName="code" ShowInCustomizationForm="True"
                                                        VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowFilterRow="True" />
                                                <Settings ShowFilterRow="True"></Settings>
                                                <SettingsDetail ShowDetailRow="True" />
                                                <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                                                <Templates>
                                                    <DetailRow>
                                                        <dx:ASPxPageControl ID="ASPxPageControl3" runat="server" ActiveTabIndex="1" RenderMode="Lightweight"
                                                            Width="100%">
                                                            <TabPages>
                                                                <dx:TabPage Text="Hàng hóa">
                                                                    <ContentCollection>
                                                                        <dx:ContentControl ID="ContentControl8" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect"
                                                                                Width="100%">
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" VisibleIndex="4">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" VisibleIndex="3">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" VisibleIndex="2">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" VisibleIndex="5">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                                                                                        <ClearFilterButton Visible="True">
                                                                                        </ClearFilterButton>
                                                                                    </dx:GridViewCommandColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" VisibleIndex="1">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewCommandColumn Caption="Tham chiếu" VisibleIndex="6">
                                                                                        <EditButton Text="Chứng từ" Visible="True">
                                                                                        </EditButton>
                                                                                        <ClearFilterButton Visible="True">
                                                                                        </ClearFilterButton>
                                                                                    </dx:GridViewCommandColumn>
                                                                                </Columns>
                                                                                <Settings ShowFilterRow="True" />
                                                                                <Settings ShowFilterRow="True" />
                                                                                <Settings ShowFilterRow="True" />
                                                                                <Settings ShowFilterRow="True" />
                                                                            </dx:ASPxGridView>
                                                                        </dx:ContentControl>
                                                                    </ContentCollection>
                                                                </dx:TabPage>
                                                                <dx:TabPage Text="Dịch vụ">
                                                                    <ContentCollection>
                                                                        <dx:ContentControl ID="ContentControl9" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxGridView ID="ASPxGridView3_dichvu" runat="server" AutoGenerateColumns="False"
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
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                                                                                        SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit14" runat="server" Height="21px" Number="0">
                                                                                                        </dx:ASPxSpinEdit>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                            <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server"
                                                                                                        SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit15" runat="server" Height="21px" Number="0">
                                                                                                        </dx:ASPxSpinEdit>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                            <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server"
                                                                                                        SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px" Visible="false">
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                            <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server"
                                                                                                        SupportsDisabledAttribute="True">
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
                                                                        <dx:ContentControl ID="ContentControl10" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCheckBox ID="cbquatang" runat="server" CheckState="Checked" Text="Quà Tặng"
                                                                                ClientInstanceName="cbquatang" Checked="True">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxRoundPanel ID="ASPxRoundPanel9" HeaderText="" runat="server" View="GroupBox"
                                                                                Width="100%" ClientInstanceName="roundpanelquatang">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent ID="PanelContent7" runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxGridView Caption="Danh mục tặng phẩm" ID="gridview_tangpham" runat="server"
                                                                                            AutoGenerateColumns="False" Width="100%">
                                                                                            <Columns>
                                                                                                <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Caption="Áp dụng" ShowSelectCheckbox="True">
                                                                                                </dx:GridViewCommandColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="STT" FieldName="stt" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="1" Visible="false">
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
                <dx:TabPage ClientEnabled="False" Text="Chọn chứng từ đích" Name="Payment1">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxGridView ID="grdData1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnInitNewRow="grdData0_InitNewRow" OnRowInserting="grdData0_RowInserting" OnStartRowEditing="grdData0_StartRowEditing">
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback" />
                                    <Settings ShowFilterRow="True" />
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback"></ClientSideEvents>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                            VisibleIndex="0">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Stt" FieldName="order" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="7" Caption="Thao tác">
                                            <EditButton Text="Chi tiết" Visible="True">
                                            </EditButton>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thành tiền phải trả" FieldName="sum" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tràng thái" FieldName="status" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplier" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày mua hàng" FieldName="date" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True"></Settings>
                                </dx:ASPxGridView>
                                <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server">
                                    <Items>
                                        <dx:LayoutItem Caption="Tạo mới chứng từ đích">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" CheckState="Unchecked">
                                                    </dx:ASPxCheckBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage ClientEnabled="False" Name="Confirmation" Text="Chứng từ đích">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxFormLayout ID="ASPxFormLayout13" runat="server" Width="100%" Height="53px">
                                    <Items>
                                        <dx:LayoutGroup Caption="Layout Group" ColCount="2" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem Caption="Mã chứng từ" RequiredMarkDisplayMode="Required">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxTextBox11" runat="server" Width="170px">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Nhà cung cấp">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxComboBox3a" runat="server" Width="170px" ValueType="System.String">
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
                                                <dx:LayoutItem Caption="Bộ phận đề nghị">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
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
                                <br />
                                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="100%"
                                    Width="100%" EnableViewState="False">
                                    <TabPages>
                                        <dx:TabPage Text="Hàng hóa">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridView2_hanghoa" runat="server" AutoGenerateColumns="False"
                                                        Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="c1" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="c2" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="c3" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                                <FooterTemplate>
                                                                    Cộng
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="c4" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="c5" ShowInCustomizationForm="True"
                                                                VisibleIndex="8">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="c6" ShowInCustomizationForm="True"
                                                                VisibleIndex="9">
                                                                <PropertiesDateEdit DisplayFormatString="">
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn FieldName="grossamount" ShowInCustomizationForm="True"
                                                                Caption="Số lượng tách" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng ct góc" FieldName="c7" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="c8" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="c9" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
                                                                <FooterTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100%">
                                                                    </dx:ASPxTextBox>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="c11" ShowInCustomizationForm="True"
                                                                VisibleIndex="11">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="12">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                    </Image>
                                                                </DeleteButton>
                                                                <ClearFilterButton Visible="True">
                                                                    <Image ToolTip="Hủy">
                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                    </Image>
                                                                </ClearFilterButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                    </Image>
                                                                </CancelButton>
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
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit3" runat="server" Height="21px" Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit6" runat="server" Height="21px" Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                                                    <Border BorderStyle="None" />
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tổng tiền hàng hóa">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit4" runat="server" Height="21px" Number="0">
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
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridView3_dichvu" runat="server" AutoGenerateColumns="False"
                                                        Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="c1" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="c2" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên dịch vụ" FieldName="c3" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                                <FooterTemplate>
                                                                    <span class="style1">Cộng</span>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="c4" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="gia" ShowInCustomizationForm="True" Caption="Đơn giá"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="c5" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="c6" ShowInCustomizationForm="True"
                                                                VisibleIndex="6">
                                                                <FooterTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="100%">
                                                                    </dx:ASPxTextBox>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="c7" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="8"
                                                                ButtonType="Image">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                    </Image>
                                                                </DeleteButton>
                                                                <ClearFilterButton Visible="True">
                                                                    <Image ToolTip="Hủy">
                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                    </Image>
                                                                </ClearFilterButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                    </Image>
                                                                </CancelButton>
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
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit14" runat="server" Height="21px" Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit15" runat="server" Height="21px" Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                                                                    <Border BorderStyle="None" />
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
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
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ContentControl ID="ContentControl12" runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxCheckBox ID="cbchietkhau" runat="server" CheckState="Checked" Text="Chiết Khấu"
                                                            ClientInstanceName="cbchietkhau" Checked="True">
                                                            <ClientSideEvents CheckedChanged="checkboxchietkhau_invi" />
                                                            <ClientSideEvents CheckedChanged="checkboxchietkhau_invi"></ClientSideEvents>
                                                        </dx:ASPxCheckBox>
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel7" runat="server" HeaderText="Chiết Khấu" View="GroupBox"
                                                            Width="100%" ClientInstanceName="roundpanelchietkhau">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent6" runat="server" SupportsDisabledAttribute="True">
                                                                    <table class="form">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="ASPxLabel63" runat="server" Text="Giá Trị Chiết Khấu">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 109px;">
                                                                                <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                        <dx:ASPxCheckBox ID="cbtiengiam" runat="server" CheckState="Checked" Text="Tiền Giảm"
                                                            ClientInstanceName="cbtiengiam" Checked="True">
                                                            <ClientSideEvents CheckedChanged="checkboxtiengiam_invi" />
                                                            <ClientSideEvents CheckedChanged="checkboxtiengiam_invi"></ClientSideEvents>
                                                        </dx:ASPxCheckBox>
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel8" runat="server" HeaderText="Chiết Khấu" View="GroupBox"
                                                            Width="100%" ClientInstanceName="roundpaneltiengiam">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent77" runat="server" SupportsDisabledAttribute="True">
                                                                    <table class="form">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Giá Trị Tiền Giảm">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 109px;">
                                                                                <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                        <dx:ASPxCheckBox ID="cbquatang" runat="server" CheckState="Checked" Text="Quà Tặng"
                                                            ClientInstanceName="cbquatang" Checked="True">
                                                            <ClientSideEvents CheckedChanged="checkboxquatang_invi" />
                                                            <ClientSideEvents CheckedChanged="checkboxquatang_invi"></ClientSideEvents>
                                                        </dx:ASPxCheckBox>
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel9" runat="server" HeaderText="Quà Tặng" View="GroupBox"
                                                            Width="100%" ClientInstanceName="roundpanelquatang">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent7" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxGridView ID="ASPxGridView_khuyenmai" runat="server" AutoGenerateColumns="False"
                                                                        Width="100%">
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Caption="Thao Tác" Width="100px" ShowSelectCheckbox="True">
                                                                            </dx:GridViewCommandColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Giá Trị" FieldName="GiaTri" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2">
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridView>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                        <dx:ASPxPanel ID="ASPxPanel8" runat="server" Width="100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent88" runat="server" SupportsDisabledAttribute="True">
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxPanel>
                                                    </dx:ContentControl>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Chi tiết">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Width="100%">
                                                    </dx:ASPxHtmlEditor>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                    <Paddings Padding="5px" />
                                    <Border BorderStyle="Solid" BorderWidth="1px" />
                                </dx:ASPxPageControl>
                                <br />
                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2" Width="100%"
                                    SettingsItemCaptions-HorizontalAlign="Right">
                                    <Items>
                                        <dx:LayoutItem Caption="Tổng giá trị phiếu bán hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit21" runat="server" Height="21px" Number="0">
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" Caption="Chú thích">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTXTChuThich" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                                </dx:ASPxFormLayout>
                                <br />
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControlFinish" runat="server">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxFormLayout ID="ASPxFormLayout5" runat="server" Width="100%" Height="16px">
                                    <Items>
                                        <dx:LayoutGroup Caption="Layout Group" ColCount="2" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem Caption="Mã số">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="MS0001">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Nhà cung cấp">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Nhà cung cấp 1">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Ngày lập">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="25-07-2013">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Bộ phận đề nghị">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Phòng kế hoạch">
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
                                <br />
                                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" Height="250px"
                                    Width="100%" EnableViewState="False">
                                    <TabPages>
                                        <dx:TabPage Text="Hàng hóa">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="c1" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="c2" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="c3" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                                <FooterTemplate>
                                                                    Cộng
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="c4" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="c5" ShowInCustomizationForm="True"
                                                                VisibleIndex="8">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="c6" ShowInCustomizationForm="True"
                                                                VisibleIndex="9">
                                                                <PropertiesDateEdit DisplayFormatString="">
                                                                </PropertiesDateEdit>
                                                            </dx:GridViewDataDateColumn>
                                                            <dx:GridViewDataTextColumn FieldName="grossamount" ShowInCustomizationForm="True"
                                                                Caption="Số lượng tách" VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng ct góc" FieldName="c7" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="c8" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="c9" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
                                                                <FooterTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox4" ReadOnly="true" runat="server" Width="100%">
                                                                    </dx:ASPxTextBox>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="c11" ShowInCustomizationForm="True"
                                                                VisibleIndex="11">
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
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Layout Item" ColCount="2" GroupBoxDecoration="None">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Thuế suất GTGT">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                                SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit1" ReadOnly="true" runat="server" Height="21px"
                                                                                    Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                                                SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit5" ReadOnly="true" runat="server" Height="21px"
                                                                                    Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxTextBox3" ReadOnly="true" runat="server" Width="170px">
                                                                                    <Border BorderStyle="None" />
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tổng tiền hàng hóa">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit7" ReadOnly="true" runat="server" Height="21px"
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
                                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="c1" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="c2" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên dịch vụ" FieldName="c3" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                                <FooterTemplate>
                                                                    <span class="style1">Cộng</span>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="c4" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="gia" ShowInCustomizationForm="True" Caption="Đơn giá"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="c5" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="c6" ShowInCustomizationForm="True"
                                                                VisibleIndex="6">
                                                                <FooterTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox5" ReadOnly="true" runat="server" Width="100%">
                                                                    </dx:ASPxTextBox>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="c7" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
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
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout7" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Layout Item" ColCount="2" GroupBoxDecoration="None">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Thuế suất GTGT">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit8" ReadOnly="true" runat="server" Height="21px"
                                                                                    Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit9" ReadOnly="true" runat="server" Height="21px" Number="0">
                                                                                </dx:ASPxSpinEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxTextBox8" ReadOnly="true" runat="server" Width="170px">
                                                                                    <Border BorderStyle="None" />
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit10" ReadOnly="true" runat="server" Height="21px"
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
                                        <dx:TabPage Text="Khuyến mãi">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Chiết Khấu" View="GroupBox"
                                                            Width="100%" ClientInstanceName="roundpanelchietkhau">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                                    <table class="form">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Giá Trị Chiết Khấu">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 109px;">
                                                                                <dx:ASPxTextBox ID="ASPxTextBox9" ReadOnly="true" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="Chiết Khấu" View="GroupBox"
                                                            Width="100%" ClientInstanceName="roundpaneltiengiam">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                                                    <table class="form">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Giá Trị Tiền Giảm">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 109px;">
                                                                                <dx:ASPxTextBox ID="ASPxTextBox10" ReadOnly="true" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                        <dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="Quà Tặng" View="GroupBox"
                                                            Width="100%" ClientInstanceName="roundpanelquatang">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent4" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxGridView ID="ASPxGridView4" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                Caption="Thao Tác" Width="100px" ShowSelectCheckbox="True">
                                                                            </dx:GridViewCommandColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Giá Trị" FieldName="GiaTri" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2">
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridView>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxRoundPanel>
                                                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent ID="PanelContent5" runat="server" SupportsDisabledAttribute="True">
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxPanel>
                                                    </dx:ContentControl>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Chi tiết">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl7" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxHtmlEditor ID="ASPxHtmlEditor2" runat="server" Width="100%">
                                                    </dx:ASPxHtmlEditor>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                    <Paddings Padding="5px" />
                                    <Border BorderStyle="Solid" BorderWidth="1px" />
                                </dx:ASPxPageControl>
                                <dx:ASPxFormLayout ID="ASPxFormLayout8" runat="server" ColCount="2" Width="100%"
                                    SettingsItemCaptions-HorizontalAlign="Right">
                                    <Items>
                                        <dx:LayoutItem Caption="Tổng giá trị phiếu bán hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit2" ReadOnly="true" runat="server" Height="21px" Number="0">
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" Caption="Chú thích">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTXTChuThich1" ReadOnly="true" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                                </dx:ASPxFormLayout>
                                <br />
                                <div style="margin-top: 10px">
                                    <div class="float-right" style="margin-left: 4px">
                                        <dx:ASPxButton ID="ASPxButton8" runat="server" AutoPostBack="False" ClientInstanceName="buttonOrderPrint"
                                            Text="In hóa đơn">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Print" />
                                            </Image>
                                            <ClientSideEvents Click="function(s,e) { 
                                                ERPCore.ShowReportPopupByReportCode('06-VT','Hóa đơn giá trị gia tăng',null); 
                                              }" />
                                        </dx:ASPxButton>
                                    </div>
                                    <%--<div class="float-right" style="margin-left: 4px">
                                        <dx:ASPxButton ID="ASPxButton6" runat="server" AutoPostBack="False" ClientInstanceName="buttonPrint"
                                            Text="In phiếu">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Print" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>--%>
                                    <div class="float-right" style="margin-left: 4px">
                                        <dx:ASPxButton AutoPostBack="false" ID="ASPxButton1" runat="server" ClientInstanceName="buttonOrderPrint"
                                            Text="Hạch toán và kê khai">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Balance" />
                                            </Image>
                                            <ClientSideEvents Click="function(s,e) { 
                                                popmb.Show();
                                              }" />
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </div>
                            </div>
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
    <uc2:uPopupphieumuaban ID="uPopupphieumuaban1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftSubmitContainer" runat="server">
    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="Trợ Giúp">
        <Image ToolTip="Trợ giúp">
            <SpriteProperties CssClass="Sprite_Help" />
        </Image>
    </dx:ASPxButton>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CenterSubmitContainer" runat="server">
    <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp">
        <Image ToolTip="Trợ giúp">
            <SpriteProperties CssClass="Sprite_Help" />
        </Image>
    </dx:ASPxButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightSubmitContainer" runat="server">
    <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
        ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
        <ClientSideEvents Click="OnBackButtonClick" />
        <Image>
            <SpriteProperties CssClass="Sprite_Backward" />
        </Image>
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
        CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
        <ClientSideEvents Click="OnNextButtonClick" />
        <Image>
            <SpriteProperties CssClass="Sprite_Forward" />
        </Image>
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
        ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất">
        <ClientSideEvents Click="OnFinishButtonClick" />
        <Image>
            <SpriteProperties CssClass="Sprite_Finished" />
        </Image>
    </dx:ASPxButton>
</asp:Content>
