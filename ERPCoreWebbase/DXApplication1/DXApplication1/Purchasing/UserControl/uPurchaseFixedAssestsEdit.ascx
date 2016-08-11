<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPurchaseFixedAssestsEdit.ascx.cs" Inherits="WebModule.Purchasing.UserControl.uPurchaseFixedAssestsEdit" %>
<%@ Register Src="../../Accounting/UserControl/uPopupphieumuaban.ascx" TagName="uPopupphieumuaban"
    TagPrefix="uc1" %>
<%@ Register Src="~/Purchasing/Usercontrol/uViewIssueCooperativePrinciples.ascx"
    TagName="uViewIssueCooperativePrinciples" TagPrefix="uc2" %>
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
</style>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formPurchaseEdit" runat="server" HeaderText="Thông Tin Phiếu Mua Tài Sản Cố Định - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formPurchaseEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonPrint" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonPrint" Text="In">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Print" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="ASPxButton_HT" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonPrint" Text="Hạch toán">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Approve"></SpriteProperties>
                                    </Image>
                                    <ClientSideEvents Click="function(s, e) {
	                                                                                            popmb.Show();
                                                                                               }" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
                                <Items>
                                    <dx:LayoutItem Caption="Mã Số">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" NullText="Tối đa 128 ký tự"
                                                    Width="200px">
                                                    <NullTextStyle ForeColor="Silver">
                                                    </NullTextStyle>
                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                        <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Nhân Viên Đề Nghị">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboRowStatus0" runat="server" ClientInstanceName="cboRowStatus"
                                                    Width="200px">
                                                    <Items>
                                                        <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ngày Lập">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="200px">
                                                </dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Nhà Cung Cấp">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboRowStatus" runat="server" ClientInstanceName="cboRowStatus"
                                                    Width="200px">
                                                    <Items>
                                                        <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng Giá Trị " ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxTXTChuThich" runat="server" NullText="Không quá 128 kí tự"
                                                    Width="80%">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="70%"
                                Width="100%" ScrollButtonStyle-HorizontalAlign="Right" 
                                EnableTabScrolling="True">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="TSCĐ">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdBuyingProductCategory3" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Số" ShowInCustomizationForm="True" VisibleIndex="1"
                                                            Width="10%">
                                                            <PropertiesComboBox>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="3"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Đơn Giá" ShowInCustomizationForm="True" VisibleIndex="5"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Thành Tiền" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="12" Width="10%">
                                                            <EditButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                            <CancelButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Số Lượng " ShowInCustomizationForm="True" VisibleIndex="4"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TSCĐ" ShowInCustomizationForm="True" VisibleIndex="2"
                                                            Width="30%">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Thuế GTGT">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit10" runat="server" Height="21px" Number="0" Width="80px">
                                                                    </dx:ASPxSpinEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Tiền Thuế GTGT">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit22" runat="server" Height="21px" Number="0" Width="150px">
                                                                    </dx:ASPxSpinEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Tổng Tiền TSCĐ">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit23" runat="server" Height="21px" Number="0" Width="150px">
                                                                    </dx:ASPxSpinEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Dịch Vụ">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Dịch Vụ" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="10%">
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Dịch Vụ" ShowInCustomizationForm="True" VisibleIndex="2"
                                                            Width="30%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="3"
                                                            Width="5%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Số Lượng" ShowInCustomizationForm="True" VisibleIndex="4"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành Tiền " VisibleIndex="5"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="7"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="8" Width="10%">
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
                                                    <SettingsPager PageSize="30">
                                                    </SettingsPager>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" ColCount="2">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Thuế GTGT">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit27" runat="server" Height="21px" Number="0" Width="80px">
                                                                    </dx:ASPxSpinEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Tiền Thuế GTGT">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit28" runat="server" Height="21px" Number="0" Width="150px">
                                                                    </dx:ASPxSpinEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Tổng Tiền Dịch Vụ">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit29" runat="server" Height="21px" Number="0" Width="150px">
                                                                    </dx:ASPxSpinEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Khuyến Mãi">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                            <table class="dxflInternalEditorTable_DevEx" width="100%">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <table class="style68">
                                                                            <tr>
                                                                                <td class="style72">
                                                                                    <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" CheckState="Unchecked" Text="Chiết Khấu">
                                                                                    </dx:ASPxCheckBox>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style72" rowspan="3" valign="top">
                                                                                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" Width="100%">
                                                                                        <Items>
                                                                                            <dx:ListEditItem Text="Tiền Giảm Theo Phiếu" Value="Tiền Giảm Theo Phiếu" />
                                                                                            <dx:ListEditItem Text="Tỉ lệ CK Theo Phiếu (%)" Value="Tỉ lệ Chiết Khấu (%)" />
                                                                                            <dx:ListEditItem Text="CK Theo Từng TSCĐ" Value="CK Theo Từng" />
                                                                                        </Items>
                                                                                        <Border BorderStyle="Solid"></Border>
                                                                                    </dx:ASPxRadioButtonList>
                                                                                </td>
                                                                                <td>
                                                                                    <dx:ASPxSpinEdit ID="ASPxSpinEdit33" runat="server" Height="21px" Number="0" Width="150px">
                                                                                    </dx:ASPxSpinEdit>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="style68" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td class="style71">
                                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit34" runat="server" Height="21px" Number="0" Width="50px">
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="ASPxSpinEdit35" runat="server" Height="21px" Number="0" Width="150px">
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <dx:ASPxGridView ID="grdProductSupplier" runat="server" AutoGenerateColumns="False"
                                                                                        ClientInstanceName="grdProductSupplier" KeyFieldName="ProductSupplierId"
                                                                                        Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Số" FieldName="SupplierCode" Name="SupplierCode"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                                                                                                <PropertiesComboBox CallbackPageSize="20" DropDownStyle="DropDown" EnableCallbackMode="True"
                                                                                                    IncrementalFilteringMode="StartsWith" TextField="Code" TextFormatString="{0} {1}"
                                                                                                    ValueField="Code" Width="400px">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" Width="150px">
                                                                                                        </dx:ListBoxColumn>
                                                                                                        <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" Width="300px">
                                                                                                        </dx:ListBoxColumn>
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="TSCĐ" FieldName="SupplierName" Name="SupplierName"
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="30%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" VisibleIndex="8"
                                                                                                Width="5%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" ShowInCustomizationForm="True" VisibleIndex="9"
                                                                                                Width="10%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Description" Name="Description"
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="5%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                Visible="False" VisibleIndex="11" Width="10%">
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
                                                                                            <dx:GridViewDataTextColumn Caption="ProductSupplierId" FieldName="ProductSupplierId"
                                                                                                Name="ProductSupplierId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Giá" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Chiết Khấu (%)" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="7">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng" ShowInCustomizationForm="True" VisibleIndex="5"
                                                                                                Width="10%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="10"
                                                                                                Width="10%">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsPager PageSize="30">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="Inline" />
                                                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                        <Styles>
                                                                                            <CommandColumn Spacing="10px">
                                                                                            </CommandColumn>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table cellpadding="0" cellspacing="0" class="controlContain" width="100%">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    &nbsp;
                                                                                    <table class="style68">
                                                                                        <tr>
                                                                                            <td class="style70">
                                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox2" runat="server" CheckState="Unchecked" Text="Quà Tặng">
                                                                                                </dx:ASPxCheckBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxGridView ID="grdProductSupplier0" runat="server" AutoGenerateColumns="False"
                                                                                                    ClientInstanceName="grdProductSupplier" KeyFieldName="ProductSupplierId"
                                                                                                    Width="100%">
                                                                                                    <Columns>
                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Số" FieldName="SupplierCode" Name="SupplierCode"
                                                                                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="10%">
                                                                                                            <PropertiesComboBox CallbackPageSize="20" DropDownStyle="DropDown" EnableCallbackMode="True"
                                                                                                                IncrementalFilteringMode="StartsWith" TextField="Code" TextFormatString="{0} {1}"
                                                                                                                ValueField="Code" Width="400px">
                                                                                                                <Columns>
                                                                                                                    <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" Width="150px">
                                                                                                                    </dx:ListBoxColumn>
                                                                                                                    <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" Width="300px">
                                                                                                                    </dx:ListBoxColumn>
                                                                                                                </Columns>
                                                                                                            </PropertiesComboBox>
                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="TSCĐ" FieldName="SupplierName" Name="SupplierName"
                                                                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="30%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Giá Trị" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" VisibleIndex="7"
                                                                                                            Width="5%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Hạn Sử Dụng" ShowInCustomizationForm="True" VisibleIndex="8"
                                                                                                            Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Description" Name="Description"
                                                                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="5%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                            Visible="False" VisibleIndex="10" Width="10%">
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
                                                                                                        <dx:GridViewDataTextColumn Caption="ProductSupplierId" FieldName="ProductSupplierId"
                                                                                                            Name="ProductSupplierId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Số Lượng" ShowInCustomizationForm="True" VisibleIndex="5"
                                                                                                            Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="9"
                                                                                                            Width="10%">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                    <SettingsPager PageSize="30">
                                                                                                    </SettingsPager>
                                                                                                    <SettingsEditing Mode="Inline" />
                                                                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                                    <Styles>
                                                                                                        <CommandColumn Spacing="10px">
                                                                                                        </CommandColumn>
                                                                                                    </Styles>
                                                                                                </dx:ASPxGridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxCallbackPanel>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Tiến Độ Giao Hàng">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdBuyingProductCategory4" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã TSCĐ" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="10%">
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="ĐVT" ShowInCustomizationForm="True" VisibleIndex="4"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="11"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="13" Width="10%">
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
                                                        <dx:GridViewDataTextColumn Caption="Số Lô" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SL Giao Thực Tế" ShowInCustomizationForm="True"
                                                            VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SL Giao Dự Kiến" ShowInCustomizationForm="True"
                                                            VisibleIndex="8" Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên TSCĐ" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="30%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ngày Giao Dự Kiến" ShowInCustomizationForm="True"
                                                            VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ngày Giao Thực Tế" ShowInCustomizationForm="True"
                                                            VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="tabDetail" Text="Tiến Độ Thanh Toán">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdBuyingProductCategory5" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Ngày Thanh Toán" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Caption="Số Tiền Thanh Toán" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="10%">
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="7"
                                                            Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Chi Tiết" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="11" Width="10%">
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
                                                        <dx:GridViewDataTextColumn Caption="Hình Thức Thanh Toán" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="30%">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                 <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) Trường hợp công nợ vượt quá hạn ngạch thì sẽ được đánh dấu màu vàng | (*) Các mức doanh số không đạt sẽ được đánh dấu màu vàng"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Hợp Tác Nguyên Tắc">
                                        <ContentCollection>
                                            <dx:ContentControl>
                                                <dx:ASPxPanel ID="panel_cooperativePrinciples" ClientInstanceName="panel_cooperativePrinciples"
                                                    runat="server" Width="100%" Height="300px" ScrollBars="Vertical">
                                                    <PanelCollection>
                                                        <dx:PanelContent>
                                                            <uc2:uViewIssueCooperativePrinciples ID="uViewIssueCooperativePrinciples1" runat="server" />
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) Trường hợp công nợ vượt quá hạn ngạch thì sẽ được đánh dấu màu vàng | (*) Các mức doanh số không đạt sẽ được đánh dấu màu vàng"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="300px" Width="100%">
                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                    <Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                                </dx:ASPxHtmlEditor>
                                                 <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                                <Paddings Padding="5px"></Paddings>
                                <ScrollButtonStyle HorizontalAlign="Right">
                                </ScrollButtonStyle>
                            </dx:ASPxPageControl>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
<uc1:uPopupphieumuaban ID="uPopupphieumuaban1" runat="server" />
