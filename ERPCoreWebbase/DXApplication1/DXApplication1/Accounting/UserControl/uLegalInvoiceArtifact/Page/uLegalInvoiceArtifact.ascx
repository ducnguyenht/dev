<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uLegalInvoiceArtifact.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uLegalInvoiceArtifact.uLegalInvoiceArtifact" %>
<style type="text/css">
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }
    .dxtlHSEC
    {
        width: 0px;
        display: none;
    }
</style>
<script type="text/javascript">
    function btn_Cancel_click(s, e) {
        cpLegalInvoiceArtifact.PerformCallback("Cancel");
    }
    function btn_Save_legal_click(s, e) {
        cpLegalInvoiceArtifact.PerformCallback("Save");
    }
    function btn_Edit_legal_click(s, e) {
        cpLegalInvoiceArtifact.PerformCallback("Change_Edit");
    }
    function ItemId_SelectedIndexChanged(s, e) {
        var ItemId = s.GetSelectedItem();
        if (ItemId != null) {
            var Code = "Item|" + ItemId.GetColumnText('Code');
            if (Code != null) {
                Grid_ArtifactDetail.PerformCallback(Code);
            }
        }
    }
    function Grid_ArtifactDetail_UnitId_SelectedIndexChanged(s, e) {
        var Code = "Unit|" + s.GetText();
        Grid_ArtifactDetail.PerformCallback(Code); 
    }
</script>
<dx:ASPxCallbackPanel ID="cpLegalInvoiceArtifact" runat="server" ShowLoadingPanel="false"
    ShowLoadingPanelImage="false" ClientInstanceName="cpLegalInvoiceArtifact" Width="100%"
    maximized="True" OnCallback="cpLegalInvoiceArtifact_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxSplitter ID="ASPxSplitter1" runat="server">
                <Panes>
                    <dx:SplitterPane>
                        <ContentCollection>
                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxPopupControl ID="PopupLegalInvoiceArtifact" runat="server" AllowDragging="True"
                                    AllowResize="True" ClientInstanceName="formProductEdit" CloseAction="CloseButton"
                                    CssClass="KeyShortcutformProductEdit" HeaderText="Mã Hóa Đơn - " Height="620px"
                                    LoadingPanelText="FormLoad" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter"
                                    PopupVerticalAlign="WindowCenter" ScrollBars="Auto" ShowFooter="True" ShowMaximizeButton="True"
                                    ShowSizeGrip="False" Width="860px">
                                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                                    <FooterContentTemplate>
                                        <div id="Footer" style="display: inline; width: 100%;">
                                            <%--<div style="display: inline; float: left">
                                                <dx:ASPxButton ID="bProductEditHelp" runat="server" AutoPostBack="false" CausesValidation="false"
                                                    CssClass="float_left dl mg" Text="Trợ Giúp" ToolTip="Trợ Giúp - Ctrl + H" Wrap="False">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Help" />
                                                    </Image>
                                                    <ClientSideEvents Click="" />
                                                </dx:ASPxButton>
                                            </div>--%>
                                            <div style="display: inline; float: right;">
                                                <dx:ASPxButton ID="btn_Cancel_legal" runat="server" AutoPostBack="False" ClientInstanceName="btn_Cancel_legal"
                                                    CssClass="float_right dl mg" Text="Thoát" ToolTip="Thoát - ESC" Wrap="False">
                                                    <ClientSideEvents Click="btn_Cancel_click" />
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                            <div style="display: inline; float: right;">
                                                <dx:ASPxButton ID="btn_Save_legal" runat="server" AutoPostBack="False" ClientInstanceName="btn_Save_legal"
                                                    CssClass="float_right dl mg" Text="Lưu lại" ToolTip="Lưu và Đóng - Ctr + Enter"
                                                    Wrap="False">
                                                    <ClientSideEvents Click="btn_Save_legal_click" />
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                            <div style="display: inline; float: right;">
                                                <dx:ASPxButton ID="btn_Edit_legal" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                                    ClientInstanceName="btn_Edit_legal" Text="Chỉnh sửa" Wrap="False" ToolTip="Chỉnh sủa - Ctr + F2">
                                                    <ClientSideEvents Click="btn_Edit_legal_click" />
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                        </div>
                                    </FooterContentTemplate>
                                    <ContentCollection>
                                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="Form_Hóa_Đơn" runat="server" AlignItemCaptionsInAllGroups="True"
                                                Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Hóa Đơn Giá Trị Gia Tăng" ColCount="6" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Cấu Hình Hóa Đơn" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="cbo_Code_ArtifactType" runat="server" Width="100%" DataSourceID="DBLegalInvoiceArtifactType"
                                                                            IncrementalFilteringMode="Contains" TextFormatString="{0}">
                                                                            <Columns>
                                                                                <dx:ListBoxColumn Caption="Mã Phân Loại" FieldName="Code" Width="120px" />
                                                                                <dx:ListBoxColumn Caption="Tên Phân Loại" FieldName="Name" Width="500px" />
                                                                            </Columns>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Mã Hóa Đơn" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_Code_Artifact" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Ngày Tạo" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxDateEdit ID="txt_IssuedDate_Artifact" runat="server" Width="50%">
                                                                        </dx:ASPxDateEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Mẩu Số" RequiredMarkDisplayMode="Required" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_Template_Identifier" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Số Sêri" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_Series_Identifier" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Số hóa đơn GTGT" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_Number_Identifier" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                            <dx:ASPxFormLayout ID="Form_Thông_Tin_Bên_Bán" runat="server" AlignItemCaptionsInAllGroups="True"
                                                Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Thông Tin Bên Bán" ColCount="6" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Đơn Vị Bán Hàng" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="cbo_Code_sell_Organization" runat="server" Width="100%" DataSourceID="DBOrganization"
                                                                            IncrementalFilteringMode="Contains" TextFormatString="{0}">
                                                                            <Columns>
                                                                                <dx:ListBoxColumn Caption="Mã Đơn Vị" FieldName="Code" />
                                                                                <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                <dx:ListBoxColumn Caption="Địa Chỉ" FieldName="Address" />
                                                                            </Columns>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:EmptyLayoutItem ColSpan="4">
                                                            </dx:EmptyLayoutItem>
                                                            <dx:LayoutItem Caption="Số Tài Khoản" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_AccountNumber_sell_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Mã Số Thuế" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_TaxCode_sell_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Điện Thoại" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_PhoneFax_sell_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Địa Chỉ" ColSpan="6" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxMemo ID="memo_Address_sell_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxMemo>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                            <dx:ASPxFormLayout ID="Form_Thông_Tin_Bên_Mua" runat="server" AlignItemCaptionsInAllGroups="True"
                                                Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Thông Tin Bên Mua" ColCount="6" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Đơn Vị Bên Mua" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="cbo_Code_buy_Organization" runat="server" Width="100%" DataSourceID="DBOrganization"
                                                                            IncrementalFilteringMode="Contains" TextFormatString="{0}">
                                                                            <Columns>
                                                                                <dx:ListBoxColumn Caption="Mã Đơn Vị" FieldName="Code" />
                                                                                <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                <dx:ListBoxColumn Caption="Địa Chỉ" FieldName="Address" />
                                                                            </Columns>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Họ Tên Người Mua" RequiredMarkDisplayMode="Required" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_Name_buy_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Điện Thoại" ColSpan="2" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_TelephoneFax_buy_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Số Tài Khoản" RequiredMarkDisplayMode="Required" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_AccountNumber_buy_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Mã Số Thuế" RequiredMarkDisplayMode="Required" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_TaxCode_buy_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Hình Thức Thanh Toán" RequiredMarkDisplayMode="Required"
                                                                ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txt_Description_buy_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Địa Chỉ" ColSpan="6" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxMemo ID="memo_Address_buy_OrgActor" runat="server" Width="100%">
                                                                        </dx:ASPxMemo>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                            <dx:ASPxGridView ID="Grid_ArtifactDetail" runat="server" AutoGenerateColumns="False"
                                                DataSourceID="DBLegalInvoiceArtifactDetail" KeyFieldName="LegalInvoiceArtifactDetailId"
                                                Width="100%" OnRowDeleting="Grid_ArtifactDetail_RowDeleting" OnRowInserting="Grid_ArtifactDetail_RowInserting"
                                                OnRowUpdating="Grid_ArtifactDetail_RowUpdating" OnCustomCallback="Grid_ArtifactDetail_CustomCallback">
                                                <Columns>
                                                    <dx:GridViewDataComboBoxColumn Caption="Mã Hàng Hóa" FieldName="ItemId!Key"
                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="25%" SortIndex="0" 
                                                        SortOrder="Descending">
                                                        <PropertiesComboBox EnableCallbackMode="True" DataSourceID="DBItem" IncrementalFilteringMode="Contains"
                                                            TextField="Code" TextFormatString="{0}" ValueField="ItemId">
                                                            <Columns>
                                                                <dx:ListBoxColumn FieldName="ItemId" Visible="false" />
                                                                <dx:ListBoxColumn Caption="Mã hàng hóa" FieldName="Code" Width="150px" />
                                                                <dx:ListBoxColumn Caption="Tên hàng hóa" FieldName="Name" Width="200px" />
                                                            </Columns>
                                                            <ClientSideEvents SelectedIndexChanged="ItemId_SelectedIndexChanged" />
                                                            <%--<ValidationSettings>
                                                                <RequiredField ErrorText="Chưa chọn hàng hóa !" IsRequired="True" />
                                                            </ValidationSettings>--%>
                                                        </PropertiesComboBox>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="Giá" FieldName="Price" ShowInCustomizationForm="True"
                                                        VisibleIndex="1" Width="10%">
                                                        <PropertiesSpinEdit DisplayFormatString="g">
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <CellStyle HorizontalAlign="Right">
                                                        </CellStyle>
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="Amount" ShowInCustomizationForm="True"
                                                        VisibleIndex="2" Width="10%">
                                                        <PropertiesSpinEdit DisplayFormatString="g">
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <CellStyle HorizontalAlign="Right">
                                                        </CellStyle>
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="UnitId!Key" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Width="10%">
                                                        <PropertiesComboBox EnableCallbackMode="True" DataSourceID="DBUnit" IncrementalFilteringMode="Contains"
                                                            TextField="Name" TextFormatString="{0}" ValueField="UnitId">
                                                            <ClientSideEvents SelectedIndexChanged="Grid_ArtifactDetail_UnitId_SelectedIndexChanged" />
                                                            <Columns>
                                                                <dx:ListBoxColumn FieldName="UnitId" Visible="false" />
                                                                <dx:ListBoxColumn Caption="Mã Đơn Vị" FieldName="Code" Width="150px" />
                                                                <dx:ListBoxColumn Caption="Tên Đơn Vị" FieldName="Name" Width="200px" />
                                                            </Columns>
                                                            <%--<ValidationSettings>
                                                                <RequiredField ErrorText="Chưa chọn hàng hóa !" IsRequired="True" />
                                                            </ValidationSettings>--%>
                                                        </PropertiesComboBox>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="Thành Giá" FieldName="Total" ShowInCustomizationForm="True"
                                                        VisibleIndex="6" Width="15%">
                                                        <PropertiesSpinEdit DisplayFormatString="g">
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <CellStyle HorizontalAlign="Right">
                                                        </CellStyle>
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewCommandColumn Caption="Thao Tác" ButtonType="Image" ShowInCustomizationForm="True"
                                                        VisibleIndex="7" Width="10%">
                                                        <EditButton Visible="True">
                                                            <Image ToolTip="Sửa">
                                                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                            </Image>
                                                        </EditButton>
                                                        <NewButton Visible="True">
                                                            <Image ToolTip="Thêm">
                                                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                            </Image>
                                                        </NewButton>
                                                        <DeleteButton Visible="True">
                                                            <Image ToolTip="Xóa">
                                                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                            </Image>
                                                        </DeleteButton>
                                                        <UpdateButton>
                                                            <Image ToolTip="Cập nhật">
                                                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                            </Image>
                                                        </UpdateButton>
                                                        <CancelButton>
                                                            <Image ToolTip="Bỏ qua">
                                                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                            </Image>
                                                        </CancelButton>
                                                        <ClearFilterButton Visible="True">
                                                        </ClearFilterButton>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </CellStyle>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <SettingsText ConfirmDelete="Bạn Có Chắc Xóa Không?" />
                                                <SettingsPager PageSize="20" Position="Bottom">
                                                    <PageSizeItemSettings Visible="true" Items="10, 20, 50">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <Settings ShowFilterRow="True" ShowFooter="True" />
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" DisplayFormat="Tổng Cộng : {0:n0}" />
                                                </TotalSummary>
                                                <GroupSummary>
                                                    <dx:ASPxSummaryItem SummaryType="Count" />
                                                </GroupSummary>
                                            </dx:ASPxGridView>
                                            <dx:ASPxFormLayout ID="Form_Thanh_Toán" runat="server" AlignItemCaptionsInAllGroups="True"
                                                Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Thanh Toán" ColCount="4" GroupBoxDecoration="HeadingLine">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Thuế Suất GTG %" ColSpan="1">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txt_ItemTaxInPercentage_ArtifactDetail" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Thuế Suất GTGT" ColSpan="1">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txt_thuesuatGTGT" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tổng Cộng Tiền Thanh Toán" ColSpan="2">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txt_total_ArtifactDetail" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Số Tiền Viết Bằng Chữ" ColSpan="4">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                                        <dx:ASPxTextBox ID="txt_tienbangchu" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                            <dx:ASPxFormLayout ID="ASPxFormLayout5" runat="server" AlignItemCaptionsInAllGroups="True">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Đơn Vị Tham Gia" ColCount="4" GroupBoxDecoration="HeadingLine">
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:XpoDataSource ID="DBLegalInvoiceArtifactDetail" runat="server" TypeName="NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactDetail"
    Criteria="[RowStatus] &gt; 0s">
</dx:XpoDataSource>
<dx:XpoDataSource ID="DBOrganization" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
    Criteria="[RowStatus] > 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="DBItem" runat="server" TypeName="NAS.DAL.Nomenclature.Item.Item"
    Criteria="[RowStatus] > 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="DBLegalInvoiceArtifactType" runat="server" TypeName="NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifactType"
    Criteria="[RowStatus] > 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="DBUnit" runat="server" TypeName="NAS.DAL.Nomenclature.Item.Unit"
    Criteria="[RowStatus] > 0 And [UnitId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="unitid" SessionField="UnitId" />
    </CriteriaParameters>
</dx:XpoDataSource>
