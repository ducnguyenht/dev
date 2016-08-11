<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEdit_backOrderProduct.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.uEdit_backOrderProduct" %>
<%@ Register Src="~/Sales/UserControl/uViewApprovedPromotionSale.ascx" TagName="uViewApprovedPromotionSale"
    TagPrefix="uc3" %>
<%@ Register Src="~/Sales/UserControl/uViewConditionReceivePolicy.ascx" TagName="uViewConditionReceivePolicy"
    TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/uViewSalesInfo.ascx" TagName="uViewSalesInfo"
    TagPrefix="uc2" %>
<style type="text/css">
    .style1
    {
        height: 16px;
    }
</style>
<script type="text/javascript">
    function gridview_backProduct_custombtn_click(s, e) {
        if (e.buttonID == 'show_evidence') {
            popup_showevidence.Show();
        }
    }

    function selected_rdochange(s, e) {
        update_panel.PerformCallback(rdolst_type.GetSelectedIndex());
    }

    function OnBackButtonClick(s, e) {
        var currentTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex());
        var previousTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex() - 1);
        currentTab.SetEnabled(false);
        previousTab.SetEnabled(false);
        pc_wizard.SetActiveTab(previousTab);
    }

    function OnNextButtonClick(s, e) {
        var currentTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex());
        var nextTab = pc_wizard.GetTab(pc_wizard.GetActiveTabIndex() + 1);
        nextTab.SetEnabled(true);
        currentTab.SetEnabled(false);
        pc_wizard.SetActiveTab(nextTab);
    }

    function pc_wizard_tabchanged(s, e) {
        btnFinish.SetClientVisible(false);
        if (pc_wizard.GetActiveTabIndex() == 2) {
            btnNext.SetClientVisible(false);
            btnFinish.SetClientVisible(true);
        } else {
            btnNext.SetClientVisible(true);
        }

        if (pc_wizard.GetActiveTabIndex() == 0)
            btnBack.SetClientVisible(false);
        else
            btnBack.SetClientVisible(true);
    }
</script>
<table width="100%">
    <tr>
        <td>
            <div class="buttonsArea">
                <div class="buttons" align="right">
                    <table cellpadding="0" cellspacing="0" border="0" class="buttonsTable">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
                                    ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
                                    <ClientSideEvents Click="OnBackButtonClick"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Backward" />
                                    </Image>
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
                                    CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
                                    <ClientSideEvents Click="OnNextButtonClick"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Forward" />
                                    </Image>
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
                                    ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất">
                                    <ClientSideEvents Click="OnFinishButtonClick"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Finished" />
                                    </Image>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td valign="top">
            <div style="width: 100%; height: 670px; overflow-y: auto; overflow-x: hidden;">
                <dx:ASPxPageControl ID="ASPxPageControl_detail" ClientInstanceName="pc_wizard" runat="server"
                    RenderMode="Lightweight" ActiveTabIndex="0" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Chọn hàng trả">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxPanel runat="server" Width="100%" Height="100%">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" KeyFieldName="SupplierId"
                                                    Width="100%" OnHtmlRowCreated="grdData_HtmlRowCreated">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Caption="" ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                            VisibleIndex="0" Width="60px">
                                                            <HeaderCaptionTemplate>
                                                                <dx:ASPxCheckBox runat="server" ID="checkallmaster">
                                                                </dx:ASPxCheckBox>
                                                            </HeaderCaptionTemplate>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="1" Name="SupplierId">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã Phiếu Mua" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhà Cung Cấp" FieldName="Supplier" Name="Supplier"
                                                            ShowInCustomizationForm="True" VisibleIndex="3" Width="100%">
                                                            <DataItemTemplate>
                                                                <div class="wrapColumn">
                                                                    <%# Eval("Supplier")%>
                                                                </div>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhân viên Đề Nghị" FieldName="Department" Name="Description"
                                                            ShowInCustomizationForm="True" VisibleIndex="5" Width="250px">
                                                            <DataItemTemplate>
                                                                <div class="wrapColumn">
                                                                    <%# Eval("Department")%>
                                                                </div>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn Caption="Ngày Mua" FieldName="Date" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="100px">
                                                            <PropertiesDateEdit DisplayFormatString="">
                                                            </PropertiesDateEdit>
                                                            <EditCellStyle HorizontalAlign="Center">
                                                            </EditCellStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataDateColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" ConfirmDelete="True"
                                                        ColumnResizeMode="Control" />
                                                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" ConfirmDelete="True">
                                                    </SettingsBehavior>
                                                    <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
                                                    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                        <HeaderPanel HorizontalAlign="Center">
                                                        </HeaderPanel>
                                                        <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                    <Templates>
                                                        <DetailRow>
                                                            <dx:ASPxGridView ID="grv_hanghoa" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                KeyFieldName="productid">
                                                                <Columns>
                                                                    <dx:GridViewCommandColumn Caption="" ShowSelectCheckbox="True" VisibleIndex="0" Width="60px">
                                                                        <HeaderCaptionTemplate>
                                                                            <dx:ASPxCheckBox runat="server" ID="checkalldetail">
                                                                            </dx:ASPxCheckBox>
                                                                        </HeaderCaptionTemplate>
                                                                    </dx:GridViewCommandColumn>
                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã Hàng Hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                                        VisibleIndex="1" Width="150px">
                                                                    </dx:GridViewDataComboBoxColumn>
                                                                    <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2" Width="300px">
                                                                        <DataItemTemplate>
                                                                            <div class="wrapColumn">
                                                                                <%# Eval("productname")%>
                                                                            </div>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataComboBoxColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unit" ShowInCustomizationForm="True"
                                                                        VisibleIndex="3" Width="60px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Lượng Đặt" FieldName="number" ShowInCustomizationForm="True"
                                                                        VisibleIndex="4" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Lượng Tồn Kho" FieldName="numberinventory"
                                                                        ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Lượng Trả" FieldName="returnNumber" ShowInCustomizationForm="True"
                                                                        VisibleIndex="6" Width="100px">
                                                                        <DataItemTemplate>
                                                                            <dx:ASPxTextBox ID="txt_number" runat="server" Text='<%# Bind("returnNumber") %>' />
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Đơn Giá" FieldName="priceunit" ShowInCustomizationForm="True"
                                                                        VisibleIndex="7" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Thành Tiền" FieldName="total" ShowInCustomizationForm="True"
                                                                        Visible="false" VisibleIndex="8" Width="120px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Lô" FieldName="lotid" ShowInCustomizationForm="True"
                                                                        VisibleIndex="9" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" FieldName="duedate" ShowInCustomizationForm="True"
                                                                        VisibleIndex="10" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Lý do" FieldName="reason" ShowInCustomizationForm="True"
                                                                        VisibleIndex="11" Width="200px">
                                                                        <DataItemTemplate>
                                                                            <div class="wrapColumn">
                                                                                <%# Eval("reason")%>
                                                                            </div>
                                                                            <dx:ASPxComboBox ID="cbo_reason" runat="server" ValueField="reason" ValueType="System.String"
                                                                                IncrementalFilteringMode="Contains" Value='<%# Eval("reason") %>' Width="120px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Value="Hỏng" Text="Hỏng" />
                                                                                    <dx:ListEditItem Value="Hết hạn sử dụng" Text="Hết hạn sử dụng" />
                                                                                    <dx:ListEditItem Value="Hết nhu cầu" Text="Hết nhu cầu" />
                                                                                    <dx:ListEditItem Value="Lý do khác" Text="Lý do khác" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="note" ShowInCustomizationForm="True"
                                                                        VisibleIndex="12" Width="200px">
                                                                        <DataItemTemplate>
                                                                            <dx:ASPxMemo ID="txt_note" runat="server" Width="200px">
                                                                            </dx:ASPxMemo>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsBehavior ColumnResizeMode="Control" />
                                                                <SettingsPager Mode="ShowPager" PageSize="10" ShowEmptyDataRows="true">
                                                                </SettingsPager>
                                                                <Settings HorizontalScrollBarMode="Visible" />
                                                                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" />
                                                                <Styles>
                                                                    <CommandColumn Spacing="10px">
                                                                    </CommandColumn>
                                                                </Styles>
                                                            </dx:ASPxGridView>
                                                        </DetailRow>
                                                    </Templates>
                                                </dx:ASPxGridView>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Danh mục hàng trả" ClientEnabled="false">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="gridview_backProduct" runat="server" AutoGenerateColumns="False"
                                        OnHtmlRowPrepared="gridview_backProduct_HtmlRowPrepared" Width="100%">
                                        <ClientSideEvents CustomButtonClick="gridview_backProduct_custombtn_click"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Width="150px">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                                <DataItemTemplate>
                                                    <div class="wrapColumn">
                                                        <%# Eval("productname")%>
                                                    </div>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unitid" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="60px">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số lượng trả" FieldName="numberofback" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                <CellStyle HorizontalAlign="Right">
                                                </CellStyle>
                                                <DataItemTemplate>
                                                    <dx:ASPxTextBox ID="txt_numberofback" Text='<%# Bind("numberofback") %>' runat="server" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Hạn sản xuất" FieldName="duedate" ShowInCustomizationForm="True"
                                                VisibleIndex="5" Width="100px">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Xem" ShowInCustomizationForm="True" ButtonType="Image"
                                                VisibleIndex="6" Width="50px">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="show_evidence">
                                                        <Image Url="~/images/icon/16x16/inquiry.png" ToolTip="Xem chứng từ liên quan" />
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn Caption="C.S liên quan" ShowInCustomizationForm="True"
                                                ButtonType="Image" VisibleIndex="7" Width="50px" Visible="false">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="show_policy">
                                                        <Image Url="~/images/icon/16x16/inquiry.png" ToolTip="Xem chính sách liên quan" />
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="unitprice" ShowInCustomizationForm="True"
                                                VisibleIndex="8" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
                                                VisibleIndex="9" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Lý do" FieldName="reason" ShowInCustomizationForm="True"
                                                VisibleIndex="10">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tình trạng" FieldName="status" ShowInCustomizationForm="True"
                                                VisibleIndex="11" Visible="false">
                                                <DataItemTemplate>
                                                    <dx:ASPxComboBox ID="cbo_status" runat="server" ValueField="reason" ValueType="System.String"
                                                        IncremetnalFilteringMode="Contains" Value='<%# Eval("status") %>' Width="70px">
                                                        <Items>
                                                            <dx:ListEditItem Value="Đồng ý" Text="Đồng ý" />
                                                            <dx:ListEditItem Value="Từ chối" Text="Từ chối" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="12"
                                                ButtonType="Image" Width="65px">
                                                <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
                                                <DeleteButton Visible="True">
                                                    <Image SpriteProperties-CssClass="Sprite_Delete">
                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                    </Image>
                                                </DeleteButton>
                                                <ClearFilterButton Visible="True">
                                                </ClearFilterButton>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <ClientSideEvents CustomButtonClick="gridview_backProduct_custombtn_click" />
                                        <Settings ShowFilterRow="True" />
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                                        </SettingsPager>
                                        <SettingsBehavior ColumnResizeMode="Control" />
                                        <Settings ShowFilterRow="True"></Settings>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Hoàn tất" ClientEnabled="False">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True"
                                    Width="100%">
                                    <table style="width: 100%; height: 600px; vertical-align: top;">
                                        <tr>
                                            <td style="height: 5px;">
                                                <dx:ASPxLabel ID="lbl_titleform_finish" runat="server" AssociatedControlID="form_finish"
                                                    Text="Mã phiếu trả hàng đã được tạo">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <dx:ASPxFormLayout ID="form_finish" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Mã phiếu trả hàng">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="form_finish_E1" runat="server" Enabled="False" Text="TH00001"
                                                                        Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Ngày tạo phiếu">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxDateEdit ID="form_finish_E2" runat="server" Date="2013-08-10" Enabled="False">
                                                                    </dx:ASPxDateEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Người tạo">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="form_finish_E3" runat="server" Enabled="False" Text="Nguyễn Văn A"
                                                                        Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="pc_wizard_tabchanged"></ClientSideEvents>
                </dx:ASPxPageControl>
            </div>
        </td>
    </tr>
</table>
<dx:ASPxPopupControl ID="popup_showevidence" runat="server" ClientInstanceName="popup_showevidence"
    AllowDragging="True" AllowResize="True" HeaderText="Thông tin phiếu đặt hàng"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    RenderMode="Classic" Height="600px" Width="900px" ScrollBars="Auto" ShowMaximizeButton="True">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <uc2:uViewSalesInfo ID="uViewSalesInfo1" runat="server" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
