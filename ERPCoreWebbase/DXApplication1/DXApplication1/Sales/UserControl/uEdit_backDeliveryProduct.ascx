<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEdit_backDeliveryProduct.ascx.cs"
    Inherits="WebModule.GUI.usercontrol.uEdit_backDeliveryProduct" %>
<%@ Register Src="uViewConditionReceivePolicy.ascx" TagName="uViewConditionReceivePolicy"
    TagPrefix="uc1" %>
<%@ Register Src="uViewSalesInfo.ascx" TagName="uViewSalesInfo" TagPrefix="uc2" %>
<%@ Register src="~/Sales/userControl/uReturnProductsInfo.ascx" tagname="uReturnProductsInfo" tagprefix="uc3" %>
<style type="text/css">
    .dxgv
    {
        white-space: nowrap;
        text-overflow: ellipsis;
    }
</style>
<script type="text/javascript">
    function gridview_backProduct_custombtn_click(s, e) {
        if (e.buttonID == 'grvBackProduct_show_policy' || e.buttonID == 'grvSelectProduct_show_policy'
             || e.buttonID == 'grvAddProduct_show_policy') {
            popup_showpolicy.Show();
        }

        if (e.buttonID == "show_evidence" || e.buttonID == 'bt_sales_detail') {
            popup_showevidence.Show();
        }
    }

    function selected_rdolst_type(s, e) {
        update_panel.PerformCallback(rdolst_type.GetSelectedIndex());
    }

    function OnBackButtonClick(s, e) {
        var currentTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex());
        var previousTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex() - 1);
        currentTab.SetEnabled(false);
        previousTab.SetEnabled(true);
        pc_wizard.SetActiveTab(previousTab);
    }

    function OnNextButtonClick(s, e) {
        if (pc_wizard.GetActiveTabIndex() == 1 && flg_checkpolicy == 0) {
            alert('Vui lòng thực hiện kiểm tra chính sách');
            return;
        }

        var currentTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex());
        var nextTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex() + 1);
        nextTab.SetEnabled(true);
        currentTab.SetEnabled(false);
        pc_wizard.SetActiveTab(nextTab);
    }

    function pc_wizard_tabchanged(s, e) {
        if (pc_wizard.GetActiveTabIndex() == 3) {
            btnNext.SetClientVisible(false);
            btnFinish.SetClientVisible(true);
        } else {
            btnNext.SetClientVisible(true);
            btnFinish.SetClientVisible(false);
        }

        if (pc_wizard.GetActiveTabIndex() == 0)
            btnBack.SetClientVisible(false);
        else
            btnBack.SetClientVisible(true);
    }


    function selected_rdo_createMethod(s, e) {
        var idx = rdo_createMethod.GetSelectedIndex();
        var promotion_tab = pc_responsibility.GetTab(1);
        var params = new Array(idx, 'n');
        update_paneltab2.PerformCallback(params);
        if (idx == 0)
            promotion_tab.SetVisible(true);
        else
            promotion_tab.SetVisible(false);
    }

    var flg_checkpolicy = 0;

    function btnCheckPolicy_click(s, e) {
        var params = new Array(rdo_createMethod.GetSelectedIndex(), 'c');
        update_paneltab2.PerformCallback(params);
        alert('Tồn tại hàng hóa không được trả! Vui lòng kiểm tra lại');
        flg_checkpolicy = 1;
    }

    function thisPopup_AfterResizing(s, e) {
        popup_editProcessReceiveProduct.AdjustControl();
        pc_wizard.AdjustControl();
    }

</script>
<dx:ASPxPopupControl ID="popup_editProcessReceiveProduct" ClientInstanceName="popup_editProcessReceiveProduct"
    ShowFooter="true" runat="server" AllowDragging="True" AllowResize="True" HeaderText="Thao tác tạo phiếu trả hàng"
    Modal="True" RenderMode="Classic" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
    MinHeight="100px" MinWidth="860px" Height="500px" Width="1150px" ScrollBars="Auto"
    ClientSideEvents-AfterResizing="thisPopup_AfterResizing">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="ASPxPageControl_detail" ClientInstanceName="pc_wizard" runat="server"
                RenderMode="Classic" ActiveTabIndex="3" Width="100%" Height="100%">
                <TabPages>
                    <dx:TabPage Text="Thông tin chung">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxFormLayout ID="form_typeofinput" runat="server" Width="100%">
                                    <Items>
                                        <dx:LayoutGroup Caption="Thông tin chung" ShowCaption="False" Width="100%" ColCount="1">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="true" Caption="Mã khách hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="cbo_customerid" runat="server" ValueType="System.String" Width="150px">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="true" Caption="Tên khách hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="txt_customername" ReadOnly="true" runat="server" ValueType="System.String"
                                                                Width="150px">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="true" Caption="Người trả hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="txt_delegatePerson" ReadOnly="true" runat="server" ValueType="System.String"
                                                                Width="150px">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="true" Caption="Nguyên nhân trả hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="cbo_reason" ReadOnly="true" runat="server" ValueType="System.String"
                                                                Width="150px">
                                                                <Items>
                                                                    <dx:ListEditItem Value="0" Text="0" Selected="true" />
                                                                    <dx:ListEditItem Value="1" Text="Hết nhu cầu" />
                                                                    <dx:ListEditItem Value="2" Text="Hàng hỏng" />
                                                                    <dx:ListEditItem Value="3" Text="Hàng hết hạn sử dụng" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="true" Caption="Ngày trả hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer  runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxDateEdit ID="txt_dateReturn" runat="server" Width="150px">
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="true" Caption="Hình thức trả hàng">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxRadioButtonList ID="rdo_createMethod" ClientInstanceName="rdo_createMethod"
                                                                runat="server">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Theo phiếu bán hàng" Value="1" Selected="true" />
                                                                    <dx:ListEditItem Text="Không theo phiếu bán hàng" Value="2" />
                                                                </Items>
                                                                <ClientSideEvents SelectedIndexChanged="selected_rdo_createMethod" />
                                                            </dx:ASPxRadioButtonList>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem ShowCaption="true" Caption="Hình thức thanh toán">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxRadioButtonList ID="rdolst_type" ClientInstanceName="rdolst_type" runat="server">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Cấn trừ công nợ" Value="1" Selected="true" />
                                                                    <dx:ListEditItem Text="Thanh toán tiền mặt" Value="2" />
                                                                    <dx:ListEditItem Text="Nhận lại hàng mới" Value="3" />
                                                                </Items>
                                                                <ClientSideEvents SelectedIndexChanged="selected_rdolst_type" />
                                                            </dx:ASPxRadioButtonList>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Tạo danh mục hàng trả lại" ClientEnabled="False">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxCallbackPanel ID="update_paneltab2" ClientInstanceName="update_paneltab2"
                                    runat="server" Width="100%" OnCallback="update_paneltab2_callback">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxPanel ID="paneltab2_grp1" ClientInstanceName="paneltab2_grp1" runat="server"
                                                Height="320px" Width="100%" ScrollBars="Vertical" Visible="true">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True" Text="Chọn danh mục hàng hóa trả lại">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxGridView ID="grid_dmorder" runat="server" AutoGenerateColumns="False" OnHtmlRowCreated="grid_dmorder_HtmlRowCreated"
                                                            KeyFieldName="id" OnHtmlDataCellPrepared="grid_dmorder_HtmlDataCellPrepared"
                                                            Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ButtonType="Link" Caption="Chọn" VisibleIndex="0" ShowSelectCheckbox="True"
                                                                    Width="50px">
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã số phiếu bán" FieldName="id" VisibleIndex="1"
                                                                    Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="idkh" VisibleIndex="2"
                                                                    Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="tenkh" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Ngày mua" FieldName="ngaymua" VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thông tin chứng từ" VisibleIndex="5">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="bt_sales_detail">
                                                                            <Image ToolTip="Chi tiết chứng từ">
                                                                                <SpriteProperties CssClass="Sprite_Info" />
                                                                            </Image>
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                </dx:GridViewCommandColumn>
                                                            </Columns>
                                                            <ClientSideEvents CustomButtonClick="gridview_backProduct_custombtn_click" />
                                                            <Templates>
                                                                <DetailRow>
                                                                    <dx:ASPxGridView ID="grd_selectproduct" ClientInstanceName="grd_selectproduct" runat="server"
                                                                        AutoGenerateColumns="False" Width="100%" KeyFieldName="productid">
                                                                        <Columns>
                                                                            <dx:GridViewCommandColumn ButtonType="Link" Caption="Chọn" VisibleIndex="0" ShowSelectCheckbox="True">
                                                                            </dx:GridViewCommandColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                                                VisibleIndex="0">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1">
                                                                                <FooterTemplate>
                                                                                    Cộng
                                                                                </FooterTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" ShowInCustomizationForm="True"
                                                                                Visible="true" VisibleIndex="3">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Số lượng mua" FieldName="number" ShowInCustomizationForm="True"
                                                                                VisibleIndex="4">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Số lượng trả"  ShowInCustomizationForm="True"
                                                                                VisibleIndex="5">
                                                                                <DataItemTemplate>
                                                                                    <dx:ASPxTextBox ID="txt_number" runat="server" Width="100%">
                                                                                    </dx:ASPxTextBox>
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                                                                VisibleIndex="6" Visible="false">
                                                                                <PropertiesDateEdit DisplayFormatString="">
                                                                                </PropertiesDateEdit>
                                                                            </dx:GridViewDataDateColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Tình trạng hàng" ShowInCustomizationForm="True"
                                                                                VisibleIndex="7" Visible="true" Width="160px">
                                                                                <DataItemTemplate>
                                                                                    <dx:ASPxComboBox ID="cbo_status" runat="server" ValueField="reason" ValueType="System.String"
                                                                                        IncrementalFilteringMode="Contains" Width="150px">
                                                                                        <Items>
                                                                                            <dx:ListEditItem Value="0" Text="Hỏng" />
                                                                                            <dx:ListEditItem Value="1" Text="Hết hạn sử dụng" />
                                                                                            <dx:ListEditItem Value="2" Text="Bình thường" />
                                                                                            <dx:ListEditItem Value="3" Text="Tốt" />
                                                                                            <dx:ListEditItem Value="4" Text="Hàng hóa không đúng" />
                                                                                        </Items>
                                                                                    </dx:ASPxComboBox>
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Quyết định" ShowInCustomizationForm="True" VisibleIndex="8"
                                                                                Visible="true">
                                                                                <DataItemTemplate>
                                                                                    <dx:ASPxComboBox ID="cbo_status" runat="server" ValueField="reason" ValueType="System.String"
                                                                                        IncrementalFilteringMode="Contains" Width="70px">
                                                                                        <Items>
                                                                                            <dx:ListEditItem Value="Đồng ý" Text="Đồng ý" />
                                                                                            <dx:ListEditItem Value="Từ chối" Text="Từ chối" />
                                                                                        </Items>
                                                                                    </dx:ASPxComboBox>
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True" Width="160px"
                                                                                VisibleIndex="9">
                                                                                <DataItemTemplate>
                                                                                    <dx:ASPxMemo ID="txt_note" runat="server" Width="100%">
                                                                                    </dx:ASPxMemo>
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                        </Columns>
                                                                        <SettingsDetail ShowDetailRow="True" ShowDetailButtons="true" />
                                                                        <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" />
                                                                        <Templates>
                                                                            <DetailRow>
                                                                                <uc1:uViewConditionReceivePolicy ID="uViewConditionReceivePolicy3" runat="server" />
                                                                            </DetailRow>
                                                                        </Templates>
                                                                        <ClientSideEvents CustomButtonClick="gridview_backProduct_custombtn_click" />
                                                                        <Settings ShowFooter="True"></Settings>
                                                                    </dx:ASPxGridView>
                                                                </DetailRow>
                                                            </Templates>
                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="true" />
                                                            <SettingsEditing Mode="Inline" />
                                                            <SettingsDetail ShowDetailButtons="true" ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                            <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectSingleRowOnly="false" />
                                                            <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                                                            </SettingsPager>
                                                            <Styles>
                                                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                </Header>
                                                                <HeaderPanel HorizontalAlign="Center">
                                                                </HeaderPanel>
                                                            </Styles>
                                                            <BorderLeft BorderWidth="0px" />
                                                            <BorderRight BorderWidth="0px" />
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                            <dx:ASPxPanel ID="paneltab2_grp2" ClientInstanceName="paneltab2_grp2" runat="server"
                                                Height="320px" Width="100%" ScrollBars="Vertical" Visible="false">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" Text="Nhập danh mục hàng hóa trả lại">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxGridView ID="grd_addproduct" ClientInstanceName="grd_addproduct" runat="server"
                                                            AutoGenerateColumns="False" Width="100%" KeyFieldName="productid">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Width="100px">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxComboBox ID="cbo_dshanghoa" runat="server" ValueField="productid" TextField="productid"
                                                                            ValueType="System.String" IncrementalFilteringMode="Contains" Value='<%# Eval("productid") %>'
                                                                            Width="100px">
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
                                                                    VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxTextBox ID="txt_price" runat="server" Text='<%# Bind("lotid") %>' Width="80px">
                                                                        </dx:ASPxTextBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="priceunit" ShowInCustomizationForm="True"
                                                                    Visible="true" VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng mua" FieldName="number" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng trả" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxTextBox ID="txt_number" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                                                    Visible="false" VisibleIndex="7">
                                                                    <PropertiesDateEdit DisplayFormatString="">
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tình trạng hàng" ShowInCustomizationForm="True"
                                                                    VisibleIndex="8" Visible="true" Width="150px">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxComboBox ID="cbo_status" runat="server" ValueField="reason" ValueType="System.String"
                                                                            IncrementalFilteringMode="Contains" Width="150px">
                                                                            <Items>
                                                                                <dx:ListEditItem Value="0" Text="Hỏng" />
                                                                                <dx:ListEditItem Value="1" Text="Hết hạn sử dụng" />
                                                                                <dx:ListEditItem Value="2" Text="Bình thường" />
                                                                                <dx:ListEditItem Value="3" Text="Tốt" />
                                                                                <dx:ListEditItem Value="4" Text="Hàng hóa không đúng" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Quyết định" ShowInCustomizationForm="True" VisibleIndex="9"
                                                                    Visible="true" Width="70px">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxComboBox ID="cbo_status" runat="server" ValueField="reason" ValueType="System.String"
                                                                            IncrementalFilteringMode="Contains" Width="70px">
                                                                            <Items>
                                                                                <dx:ListEditItem Value="Đồng ý" Text="Đồng ý" />
                                                                                <dx:ListEditItem Value="Từ chối" Text="Từ chối" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True"
                                                                    VisibleIndex="10">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxMemo ID="txt_note" runat="server" Width="100%">
                                                                        </dx:ASPxMemo>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="11">
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
                                                            <Templates>
                                                                <DetailRow>
                                                                    <uc1:uViewConditionReceivePolicy ID="uViewConditionReceivePolicy3" runat="server" />
                                                                </DetailRow>
                                                            </Templates>
                                                            <ClientSideEvents CustomButtonClick="gridview_backProduct_custombtn_click" />
                                                            <SettingsDetail ShowDetailRow="True" ShowDetailButtons="true" />
                                                            <Settings ShowFooter="True"></Settings>
                                                            <Styles>
                                                                <Header HorizontalAlign="Center">
                                                                </Header>
                                                            </Styles>
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                            <br />
                                            <div style="float: left">
                                                <dx:ASPxButton ID="btnCheckPolicy" runat="server" Width="200px" Text="Kiểm tra chính sách"
                                                    AutoPostBack="false" ClientSideEvents-Click="btnCheckPolicy_click">
                                                    <ClientSideEvents Click="btnCheckPolicy_click"></ClientSideEvents>
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                            <div style="float: right">
                                                <dx:ASPxLabel ID="lbl_remark1" runat="server" Text="(*) Các Phiếu bán/hàng hóa không được trả sẽ được đánh dấu màu vàng"
                                                    ForeColor="Red" Font-Italic="true">
                                                </dx:ASPxLabel>
                                                <br />
                                                <dx:ASPxLabel ID="lbl_remark2" runat="server" Text="(*) Chỉ chọn được tối đa một phiếu bán hàng"
                                                    ForeColor="Red" Font-Italic="true">
                                                </dx:ASPxLabel>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Trách nhiệm bên thu hồi" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxPageControl ID="pc_responsibility" ClientInstanceName="pc_responsibility" runat="server" RenderMode="Classic" Width="100%"
                                    Height="100%">
                                    <TabPages>
                                        <dx:TabPage Text="Đơn giá thu hồi">
                                            <ContentCollection>
                                                <dx:ContentControl>
                                                    <dx:ASPxGridView ID="grd_returnhanghoa" ClientInstanceName="grd_returnhanghoa" runat="server"
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
                                                                    <dx:ASPxTextBox ID="txt_number" runat="server" Text='<%# Bind("number") %>' Width="100%">
                                                                    </dx:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn giá ban đầu" FieldName="priceunit" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn giá thu hồi" FieldName="priceunit" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                                <DataItemTemplate>
                                                                    <dx:ASPxTextBox ID="txt_price" runat="server" Text='<%# Bind("priceunit") %>' Width="100%">
                                                                    </dx:ASPxTextBox>
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
                                                                    <dx:ASPxTextBox ID="txt_note" runat="server" Text='<%# Bind("note") %>' Width="100%">
                                                                    </dx:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Settings ShowFooter="True" />
                                                        <Styles>
                                                            <Header HorizontalAlign="Center">
                                                            </Header>
                                                        </Styles>
                                                    </dx:ASPxGridView>

                                                    <dx:ASPxFormLayout runat="server">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Tổng G.T hàng thu hồi theo giá ban đầu (A)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_totalOfReturn" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tổng G.T hàng thu hồi theo giá nhận (B)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Phí phát sinh trên hàng hóa thu hồi (A - B)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Khuyến mãi thu hồi">
                                            <ContentCollection>
                                                <dx:ContentControl>
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
                                                                    <dx:ASPxTextBox ID="txt_number" runat="server" Width="50px">
                                                                    </dx:ASPxTextBox>
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
                                                                    ..........
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thành tiền thu hồi"  ShowInCustomizationForm="True"
                                                                VisibleIndex="9">
                                                                <FooterTemplate>
                                                                    ..........
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
                                                            <dx:LayoutItem Caption="Tiền C.K thu hồi">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="form_thuhoikm_E1" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Chi phí quà tặng thu hồi (Tổng giá trị tặng - Tổng tiền thu hồi)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="form_thuhoikm_E2" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tổng giá trị K.M thu hồi (Tiền C.K thu hồi + Chi phí quà tặng thu hồi)">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="form_thuhoikm_E3" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Dịch vụ phát sinh">
                                            <ContentCollection>
                                                <dx:ContentControl>
                                                    <dx:ASPxGridView ID="grv_serviceIssue" runat="server" AutoGenerateColumns="False"
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

                                                    <dx:ASPxFormLayout runat="server" Width="100%">
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
                                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px" Visible="false">
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
                                                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                                                </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Hoàn tất" ClientEnabled="False">
                        <ContentCollection>
                            <dx:ContentControl>
                                
                                <uc3:uReturnProductsInfo ID="uReturnProductsInfo1" runat="server" />
                                
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="pc_wizard_tabchanged" />
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
<ClientSideEvents AfterResizing="thisPopup_AfterResizing"></ClientSideEvents>
    <FooterContentTemplate>
        <div style="width: 100%">
            <div style="float: left">
                <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Help" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="float: right; padding-left: 4px;">
                <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
                    ClientVisible="false" UseSubmitBehavior="false" Text="Lưu phiếu trả hàng">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Finished" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="float: right; padding-left: 4px">
                <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
                    CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
                    <ClientSideEvents Click="OnNextButtonClick"></ClientSideEvents>
                    <Image>
                        <SpriteProperties CssClass="Sprite_Forward" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="float: right; padding-left: 4px">
                <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
                    ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
                    <ClientSideEvents Click="OnBackButtonClick"></ClientSideEvents>
                    <Image>
                        <SpriteProperties CssClass="Sprite_Backward" />
                    </Image>
                </dx:ASPxButton>
            </div>
        </div>
    </FooterContentTemplate>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="popup_showpolicy" ClientInstanceName="popup_showpolicy"
    runat="server" AllowDragging="True" AllowResize="True" HeaderText="Thông tin chính sách liên quan"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    RenderMode="Lightweight">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <uc1:uViewConditionReceivePolicy ID="uViewConditionReceivePolicy1" runat="server" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="popup_showevidence" runat="server" ClientInstanceName="popup_showevidence"
    AllowDragging="True" AllowResize="True" HeaderText="Thông tin chứng từ liên quan"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" Width="800px" Height="600px">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <uc2:uViewSalesInfo ID="uViewSalesInfo1" runat="server" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
