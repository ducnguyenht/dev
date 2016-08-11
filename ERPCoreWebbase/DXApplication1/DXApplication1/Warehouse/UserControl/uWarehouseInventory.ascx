<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uWarehouseInventory.ascx.cs"
    Inherits="WebModule.uWarehouseInventory" %>
<%@ Register Src="~/Accounting/UserControl/ucReportPopup.ascx" TagName="ucReportPopup"
    TagPrefix="uc1" %>
<script type="text/javascript">
    var index = 0;
    $(document).ready(function () {
        pctrl.SetActiveTab(pctrl.GetTab(index));
        updateBut();
    });

    function TabChange() {
        index = pctrl.GetActiveTabIndex();
        updateBut();
        btnPrint.SetVisible(index == 2);
    }

    function next() {
        pctrl.SetActiveTab(pctrl.GetTab(++index));
    }

    function back() {
        pctrl.SetActiveTab(pctrl.GetTab(--index));
    }

    function finish() {
        pctrl.SetActiveTab(pctrl.GetTab(index = 0));
        exit();
    }

    function exit() {
        pctrl.SetActiveTab(pctrl.GetTab(index = 0));
        formEntryDetail.Hide();
    }

    function updateBut() {
        btnNext.SetVisible(index < 2);
        btnBack.SetVisible(index == 1);
        btnFinish.SetVisible(index == 2);
        btnExit1.SetVisible(index < 2);
    }
</script>
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine"
    OnCallback="cpLine_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent12" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="formEntryDetail" runat="server" HeaderText="Kiểm kho" Height="600px" ScrollBars="Auto"
                Modal="True" RenderMode="Classic" Width="1200px" ClientInstanceName="formEntryDetail"
                AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                LoadingPanelDelay="1000" ShowFooter="true" ShowSizeGrip="False" ShowMaximizeButton="true">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxPageControl ID="pc" ClientInstanceName="pctrl" runat="server" ActiveTabIndex="0"
                                RenderMode="Classic" Width="100%" Height="100%">
                                <ClientSideEvents ActiveTabChanged="TabChange" />
                                <TabPages>
                                    <dx:TabPage Name="Personal" Text="Hàng trong kho">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                <div style="overflow: auto; height: 450px">
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Mã kiểm kho">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Ngày kiểm kho">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                                                        </dx:ASPxDateEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Trưởng ban">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Nhân viên 1">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Nhân viên 2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="430px">
                                                        <Panes>
                                                            <dx:SplitterPane AutoHeight="True" Size="200px">
                                                                <ContentCollection>
                                                                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                                        <strong>Vị trí<br />
                                                                        </strong>
                                                                        <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" KeyFieldName="OrganizationId"
                                                                            ParentFieldName="ParentOrganizationId">
                                                                            <SettingsBehavior AllowFocusedNode="True" />
                                                                            <Columns>
                                                                                <dx:TreeListTextColumn Caption="Kho" FieldName="name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0">
                                                                                </dx:TreeListTextColumn>
                                                                            </Columns>
                                                                            <SettingsBehavior AllowFocusedNode="True"></SettingsBehavior>
                                                                        </dx:ASPxTreeList>
                                                                    </dx:SplitterContentControl>
                                                                </ContentCollection>
                                                            </dx:SplitterPane>
                                                            <dx:SplitterPane AutoHeight="True">
                                                                <ContentCollection>
                                                                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                                        <strong>Danh sách mặt hàng</strong>
                                                                        <dx:ASPxGridView ID="grdDataAccept" runat="server"
                                                                            AutoGenerateColumns="False" Width="100%">
                                                                            <Settings ShowFilterRow="True"></Settings>
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="description" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="8">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="7" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="4">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption=" SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="6" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="SL theo CT" FieldName="recieptamount" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="5" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
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
                                                                            </Columns>
                                                                            <SettingsEditing Mode="PopupEditForm" />
                                                                            <Settings ShowFilterRow="True" />
                                                                            <SettingsPopup>
                                                                                <EditForm Height="150px" Width="600px" />
                                                                            </SettingsPopup>
                                                                        </dx:ASPxGridView>
                                                                    </dx:SplitterContentControl>
                                                                </ContentCollection>
                                                            </dx:SplitterPane>
                                                        </Panes>
                                                    </dx:ASPxSplitter>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="Second Step" Text="Thông tin kiểm">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <div style="overflow: auto; height: 450px">
                                                    
                                                    <div style="margin-bottom: 10px;">
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách hàng hóa tồn kho" Font-Bold="True"
                                                            Font-Size="Small">
                                                        </dx:ASPxLabel>
                                                    </div>
                                                    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                                                        OnCallback="cpHeader_Callback" HideContentOnCallback="True">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxGridView ID="grdData" Width="100%" runat="server" AutoGenerateColumns="False"
                                                                    OnStartRowEditing="grdData_StartRowEditing">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                            FieldName="code">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                                            VisibleIndex="1">
                                                                            <FooterTemplate>
                                                                                Cộng
                                                                            </FooterTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                            FieldName="unit">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                            VisibleIndex="3">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" ShowInCustomizationForm="True"
                                                                            VisibleIndex="4">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="recieptamount" ShowInCustomizationForm="True"
                                                                            VisibleIndex="5">
                                                                            <FooterTemplate>
                                                                                .........
                                                                            </FooterTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                                                            VisibleIndex="6">
                                                                            <DataItemTemplate>
                                                                                <dx:ASPxTextBox ID = "RealAmount" runat = "server"></dx:ASPxTextBox>
                                                                            </DataItemTemplate>
                                                                            <FooterTemplate>
                                                                                .........
                                                                            </FooterTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" ShowInCustomizationForm="True"
                                                                            VisibleIndex="7" Visible="false">
                                                                            <FooterTemplate>
                                                                                .........
                                                                            </FooterTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewBandColumn Caption="Kiểm nghiệm/Phẩm chất" HeaderStyle-HorizontalAlign="Center" VisibleIndex="8">
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="Phương pháp" Width="100px">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxComboBox ID="cboVerifyingType" runat="server" Width="100%">
                                                                                            <Items>
                                                                                                <dx:ListEditItem Value="0" Text="Toàn bộ" />
                                                                                                <dx:ListEditItem Value="1" Text="Xác suất" />
                                                                                            </Items>
                                                                                        </dx:ASPxComboBox>
                                                                                    </DataItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Còn tốt 100%">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxTextBox ID = "goodnum" runat = "server"></dx:ASPxTextBox>
                                                                                    </DataItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        .........
                                                                                    </FooterTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Kém phẩm chất">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxTextBox ID = "mediumnum" runat = "server"></dx:ASPxTextBox>
                                                                                    </DataItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        .........
                                                                                    </FooterTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Mất phẩm chất">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxTextBox ID = "badnum" runat = "server"></dx:ASPxTextBox>
                                                                                    </DataItemTemplate>
                                                                                    <FooterTemplate>
                                                                                        .........
                                                                                    </FooterTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Đề xuất điều chỉnh" FieldName="entry" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="9">
                                                                                    <DataItemTemplate>
                                                                                        <dx:ASPxTextBox ID="txtRaise" runat="server" Width="100%"></dx:ASPxTextBox>
                                                                                    </DataItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                        </dx:GridViewBandColumn>
                                                                    </Columns>
                                                                    <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                                                    <SettingsPager PageSize="10" ShowEmptyDataRows="true"></SettingsPager>
                                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                                                                    </Settings>
                                                                </dx:ASPxGridView>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <%--<dx:TabPage Name="Confirmation" Text="Tạo phiếu xuất/nhập kho" ClientEnabled="False">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <div style="overflow: auto; height: 450px">
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Phiếu Xuất Kho" ColCount="3" GroupBoxDecoration="HeadingLine">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã phiếu">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxFormLayout2_E1" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="NV tạo">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout2_E2" runat="server">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Ngày tạo">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxDateEdit ID="ASPxFormLayout2_E3" runat="server">
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Kho">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout2_E7" runat="server">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Thủ kho">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout2_E9" runat="server">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:EmptyLayoutItem>
                                                                    </dx:EmptyLayoutItem>
                                                                    <dx:LayoutItem ColSpan="3" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxGridView ID="Gr_xk" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="ID" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="Name" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="1">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Số Lô" FieldName="SLo" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="2">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="Unit" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="3">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="Amount" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="4">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Thuộc Chứng Từ" FieldName="CT" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="5">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="6" ButtonType="Image">
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
                                                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                                                </dx:ASPxGridView>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                </Items>
                                                            </dx:LayoutGroup>
                                                            <dx:LayoutGroup Caption="Phiếu Nhập Kho" ColCount="3" GroupBoxDecoration="HeadingLine">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã phiếu">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxFormLayout2_E4" runat="server" Width="170px">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="NV tạo">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout2_E5" runat="server">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Ngày tạo">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxDateEdit ID="ASPxFormLayout2_E6" runat="server">
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Kho">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout2_E8" runat="server">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Thủ kho">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout2_E10" runat="server">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:EmptyLayoutItem>
                                                                    </dx:EmptyLayoutItem>
                                                                    <dx:LayoutItem ColSpan="3" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxGridView ID="Gr_nk" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="ID" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="Name" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="1">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Số Lô" FieldName="SLo" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="2">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="Unit" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="3">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="Amount" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="4">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Thuộc Chứng Từ" FieldName="CT" ShowInCustomizationForm="True"
                                                                                            VisibleIndex="5">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="6" ButtonType="Image">
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
                                                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                                                </dx:ASPxGridView>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                </Items>
                                                            </dx:LayoutGroup>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>--%>
                                    <%--<dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControlFinish" runat="server">
                                                <div style="overflow: auto; height: 450px">
                                                    <div class="tabPageContent finishArea">
                                                        <p>
                                                            Hoàn tất kiểm kho</p>
                                                    </div>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>--%>
                                    <dx:TabPage Text="Hoàn tất">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Mã kiểm kho">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px" ReadOnly="true">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Ngày kiểm kho">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ReadOnly="true">
                                                                    </dx:ASPxDateEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Trưởng ban">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" ReadOnly="true">
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Nhân viên 1">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="ASPxComboBox5" runat="server" ReadOnly="true">
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Nhân viên 2">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="ASPxComboBox6" runat="server" ReadOnly="true">
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                                <div style="margin-bottom: 10px;">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Danh sách hàng hóa tồn kho" Font-Bold="True"
                                                        Font-Size="Small">
                                                    </dx:ASPxLabel>
                                                </div>

                                                <dx:ASPxGridView ID="grvConfirmData" Width="100%" runat="server" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" ShowInCustomizationForm="True" VisibleIndex="0"
                                                            FieldName="code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                            <FooterTemplate>
                                                                Cộng
                                                            </FooterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" ShowInCustomizationForm="True" VisibleIndex="2"
                                                            FieldName="unit">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Lô" FieldName="lot" ShowInCustomizationForm="True"
                                                            VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="recieptamount" ShowInCustomizationForm="True"
                                                            VisibleIndex="5">
                                                            <DataItemTemplate>
                                                                .........
                                                            </DataItemTemplate>
                                                            <FooterTemplate>
                                                                .........
                                                            </FooterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                                            VisibleIndex="6">
                                                            <DataItemTemplate>
                                                                .........
                                                            </DataItemTemplate>
                                                            <FooterTemplate>
                                                                .........
                                                            </FooterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" ShowInCustomizationForm="True"
                                                            VisibleIndex="7">
                                                            <DataItemTemplate>
                                                                .........
                                                            </DataItemTemplate>
                                                            <FooterTemplate>
                                                                .........
                                                            </FooterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewBandColumn Caption="Kiểm nghiệm/Phẩm chất" VisibleIndex="8">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Phương pháp" Width="100px">
                                                                        <DataItemTemplate>
                                                                            ..........
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Còn tốt 100%">
                                                                    <DataItemTemplate>
                                                                        .........
                                                                    </DataItemTemplate>
                                                                    <FooterTemplate>
                                                                        .........
                                                                    </FooterTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Kém phẩm chất">
                                                                    <DataItemTemplate>
                                                                        .........
                                                                    </DataItemTemplate>
                                                                    <FooterTemplate>
                                                                        .........
                                                                    </FooterTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mất phẩm chất">
                                                                    <DataItemTemplate>
                                                                        .........
                                                                    </DataItemTemplate>
                                                                    <FooterTemplate>
                                                                        .........
                                                                    </FooterTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Đề xuất điều chỉnh" FieldName="entry" ShowInCustomizationForm="True"
                                                            VisibleIndex="8">
                                                            <DataItemTemplate>
                                                                .........
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsEditing Mode="PopupEditForm" />
                                                    <Settings ShowFooter="true"/>
                                                    <SettingsPopup>
                                                        <EditForm Height="200px" Width="600px" />
                                                    </SettingsPopup>
                                                </dx:ASPxGridView>
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
                        <%--<div class="buttonsArea">
                            <div class="buttons" align="right">
                                <table cellpadding="0" cellspacing="0" border="0" class="buttonsTable">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
                                                ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
                                                <ClientSideEvents Click="OnBackButtonClick" />
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Backward" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
                                                CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
                                                <ClientSideEvents Click="OnNextButtonClick" />
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Forward" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
                                                ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất">
                                                <ClientSideEvents Click="OnFinishButtonClick" />
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Finished" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>--%>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <FooterContentTemplate>
                    <div id="footer">
                        <%--<dx:ASPxButton ID="btnRepeat" runat="server" HorizontalAlign="Center" 
                            AutoPostBack="false" CssClass="float-right button-right-margin"
                            UseSubmitBehavior="true" Text="Tiếp tục kiểm kho" Visible="False">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Repeat" />
                            </Image>
                        </dx:ASPxButton>--%>
                        <dx:ASPxButton ID="btnExit" ClientInstanceName="btnExit1" runat="server" AutoPostBack="false"
                            ClientVisible="false" UseSubmitBehavior="false" Text="Thoát" CssClass="float-right button-right-margin">
                            <ClientSideEvents Click="exit" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
                            ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất" CssClass="float-right button-right-margin">
                            <ClientSideEvents Click="finish" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Finished" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
                            CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo" CssClass="float-right button-right-margin">
                            <ClientSideEvents Click="next" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Forward" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
                            ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về"
                            CssClass="float-right button-right-margin">
                            <ClientSideEvents Click="back" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Backward" />
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnPrint" ClientInstanceName="btnPrint" runat="server" AutoPostBack="false"
                            ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="In phiếu kiểm"
                            CssClass="float-right button-right-margin">
                            <ClientSideEvents Click="function(s,e){
                                    ERPCore.ShowReportPopupByReportCode('05-VT','Thông tin phiếu kiểm kho',null);
                                }" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Print" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </FooterContentTemplate>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc1:ucReportPopup ID="ucReportPopup1" runat="server" />
