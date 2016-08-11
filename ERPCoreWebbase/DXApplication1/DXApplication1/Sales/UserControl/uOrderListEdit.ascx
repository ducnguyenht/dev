<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uOrderListEdit.ascx.cs"
    Inherits="ERPCore.Sales.UserControl.uOrderListEdit" %>
<%@ Register Src="../../Accounting/UserControl/uPopupphieumuaban.ascx" TagName="uPopupphieumuaban"
    TagPrefix="uc1" %>
<%@ Register Src="~/Purchasing/Usercontrol/uViewIssueCooperativePrinciples.ascx"
    TagName="uViewIssueCooperativePrinciples" TagPrefix="uc2" %>
<%@ Register Src="~/Warehouse/Command/PopupCommand/EdittingOutputInventoryCommand/uEdittingOutputInventoryCommand.ascx"
    TagName="uEdittingOutputInventoryCommand" TagPrefix="uc3" %>
<%@ Register Src="../../Voucher/Receipt/GUI/ReceiptVoucherEditingForm.ascx" TagName="ReceiptVoucherEditingForm"
    TagPrefix="uc4" %>
<script type="text/javascript">
    
</script>
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
    
    
    .dxgvControl, .dxgvDisabled
    {
        border: 1px Solid #9F9F9F;
        font: 12px Tahoma, Geneva, sans-serif;
        background-color: #F2F2F2;
        color: Black;
        cursor: default;
    }
    .dxgvTable
    {
        -webkit-tap-highlight-color: rgba(0,0,0,0);
    }
    
    .dxgvTable
    {
        background-color: White;
        border-width: 0;
        border-collapse: separate !important;
        overflow: hidden;
        color: Black;
    }
    .dxgvHeader
    {
        cursor: pointer;
        white-space: nowrap;
        padding: 4px 6px 5px;
        border: 1px Solid #9F9F9F;
        background-color: #DCDCDC;
        overflow: hidden;
        font-weight: normal;
        text-align: left;
    }
    
    .dxgvPagerTopPanel, .dxgvPagerBottomPanel
    {
        padding-top: 4px;
        padding-bottom: 4px;
    }
</style>
<div id="lineContainer">
    <dx:ASPxPopupControl ID="formPurchaseEdit" runat="server" HeaderText="Thông tin phiếu bán hàng"
        Height="620px" Modal="True" Width="950px" ClientInstanceName="formPurchaseEdit"
        AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowFooter="True" ShowSizeGrip="False" AllowResize="True" ShowMaximizeButton="True"
        CloseAction="CloseButton" Maximized="True" ScrollBars="Auto" 
        ShowLoadingPanel="False">
        <ClientSideEvents Shown="formPurchaseEdit_Shown" CloseUp="formPurchaseEdit_Close"
            Closing="formPurchaseEdit_Close" Resize="function(s, e) {
	cpLine.PerformCallback();
}" Init="formPurchaseEdit_Init"></ClientSideEvents>
        <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
        <FooterContentTemplate>
            <dx:ASPxCallbackPanel ID="cpCommand" runat="server" ClientInstanceName="cpCommand"
                OnCallback="cpCommand_Callback" Width="100%" ShowLoadingPanel="False">
                <ClientSideEvents EndCallback="cpCommand_EndCallback"></ClientSideEvents>
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" Visible="False" UseSubmitBehavior="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton UseSubmitBehavior="false" ID="buttonCancelDevice" runat="server" AutoPostBack="False"
                                    CssClass="float_right dl mg" ClientInstanceName="buttonCancelDevice" Text="Thoát ra"
                                    Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: left;">
                                <dx:ASPxButton ID="buttonLock" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonLock" Text="" Wrap="False" UseSubmitBehavior="false"
                                    Visible="True" Enabled="false">
                                    <ClientSideEvents Click="buttonImportInventory_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Lock" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonSaveDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu lại" Wrap="False" Visible="true"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Save" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonModify" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonModify" Text="Chỉnh sửa" UseSubmitBehavior="False"
                                    Visible="true">
                                    <ClientSideEvents Click="buttonModify_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Edit" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: left;">
                                <dx:ASPxButton ID="buttonImportInventory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonImportInventory" Text="Xuất kho" Wrap="False" UseSubmitBehavior="false"
                                    Visible="false">
                                    <ClientSideEvents Click="buttonImportInventory_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: left;">
                                <dx:ASPxButton ID="buttonBooking" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonBooking" Text="Hạch toán" UseSubmitBehavior="False"
                                    Visible="false">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Balance"></SpriteProperties>
                                    </Image>

                                    <ClientSideEvents Click="buttonBooking_Click" />
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: left;">
                                <dx:ASPxButton ID="buttonPrint" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonPrint" Text="In Phiếu" UseSubmitBehavior="False" Visible="true">
                                    <ClientSideEvents Click="buttonPrint_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Print" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </FooterContentTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine"
                    OnCallback="cpLine_Callback">
                    <ClientSideEvents EndCallback="cpLine_EndCallback"></ClientSideEvents>
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
                                <Items>
                                    <dx:LayoutItem Caption="Mã số">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" Width="200px"
                                                    MaxLength="30">
                                                    <ClientSideEvents ValueChanged="txtCode_ValueChanged" KeyDown="txtCode_KeyDown">
                                                    </ClientSideEvents>
                                                    <NullTextStyle ForeColor="Silver">
                                                    </NullTextStyle>
                                                    <ValidationSettings ErrorText="" SetFocusOnError="True" ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField ErrorText="Chưa nhập mã số phiếu" IsRequired="True"></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ngày lập">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxDateEdit ID="txtIssuedDate" runat="server" ClientInstanceName="txtIssuedDate"
                                                    Width="150px">
                                                    <ClientSideEvents KeyDown="txtIssuedDate_KeyDown"></ClientSideEvents>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True">
                                                        <RequiredField IsRequired="True" ErrorText="Chưa chọn ng&#224;y lập"></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Khách hàng">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboSupplier" runat="server" CallbackPageSize="20" ClientInstanceName="cboSupplier"
                                                    DataSourceID="BuyerXDS" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                    TextField="Name" TextFormatString="{1}" ValueField="Code" Width="300px">
                                                    <ClientSideEvents KeyDown="cboSupplier_KeyDown" ValueChanged="cboSupplier_ValueChanged">
                                                    </ClientSideEvents>
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã khách hàng" FieldName="Code" Name="Code" Width="150px" />
                                                        <dx:ListBoxColumn Caption="Tên khách hàng" FieldName="Name" Name="Name" Width="300px" />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" ErrorText="Chưa chọn kh&#225;ch h&#224;ng"></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Đối tượng phiếu bán">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <table>
                                                    <tr>
                                                        <td id="popupAnchor">
                                                            <dx:ASPxButton ID="cmdBillActor" runat="server" AutoPostBack="False" ClientInstanceName="cmdBillActor"
                                                                Text="..." UseSubmitBehavior="False" Width="10px">
                                                                <ClientSideEvents Click="cmdBillActor_Click"></ClientSideEvents>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboUser" runat="server" ClientInstanceName="cboUser" DataSourceID="PersonXDS"
                                                    DropDownStyle="DropDown" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                    TextField="Name" TextFormatString="{1}" ValueField="Code" Visible="False" Width="150px">
                                                    <ClientSideEvents KeyDown="cboUser_KeyDown" ValueChanged="cboUser_ValueChanged">
                                                    </ClientSideEvents>
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã nhân viên" FieldName="Code" Name="Code" Width="150px" />
                                                        <dx:ListBoxColumn Caption="Tên nhân viên" FieldName="Name" Name="Name" Width="300px" />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField ErrorText="Chưa chọn nh&#226;n vi&#234;n đề nghị"></RequiredField>
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                                <SettingsItems HorizontalAlign="Right"></SettingsItems>
                            </dx:ASPxFormLayout>
                            <dx:ASPxSplitter ID="ASPxSplitter2" runat="server" Height="600px">
                                <Panes>
                                    <dx:SplitterPane Size="80%">
                                        <ContentCollection>
                                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                <div style="height: 550px; overflow: scroll">
                                                    <dx:ASPxPageControl ID="pagePurchaseEdit" runat="server" ActiveTabIndex="2" ClientInstanceName="pagePurchaseEdit"
                                                        Height="100%" Width="100%" RenderMode="Classic" ShowLoadingPanel="False">
                                                        <TabPages>
                                                            <dx:TabPage Name="tabGeneral" Text="Hàng hóa">
                                                                <ContentCollection>
                                                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdPurchaseEditProduct" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdPurchaseEditProduct" DataSourceID="ProductXDS" KeyboardSupport="True"
                                                                            KeyFieldName="BillItemId" OnCellEditorInitialize="grdPurchaseEditProduct_CellEditorInitialize"
                                                                            OnCommandButtonInitialize="grdPurchaseEditProduct_CommandButtonInitialize" OnCustomCallback="grdPurchaseEditProduct_CustomCallback"
                                                                            OnCustomColumnDisplayText="grdPurchaseEditProduct_CustomColumnDisplayText" OnCustomUnboundColumnData="grdPurchaseEditProduct_CustomUnboundColumnData"
                                                                            OnInitNewRow="grdPurchaseEditProduct_InitNewRow" OnRowDeleting="grdPurchaseEditProduct_RowDeleting"
                                                                            OnRowInserting="grdPurchaseEditProduct_RowInserting" OnRowUpdating="grdPurchaseEditProduct_RowUpdating"
                                                                            OnRowValidating="grdPurchaseEditProduct_RowValidating" OnStartRowEditing="grdPurchaseEditProduct_StartRowEditing"
                                                                            Width="100%">
                                                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True">
                                                                            </SettingsBehavior>
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                            <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="250" VerticalScrollBarMode="Auto">
                                                                            </Settings>
                                                                            <ClientSideEvents EndCallback="grdPurchaseEditProduct_EndCallback" Init="grdPurchaseEditProduct_Init">
                                                                            </ClientSideEvents>
                                                                            <TotalSummary>
                                                                                <dx:ASPxSummaryItem FieldName="TotalPrice" SummaryType="Sum" />
                                                                                <dx:ASPxSummaryItem FieldName="TotalPriceCK" SummaryType="Sum" />
                                                                            </TotalSummary>
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã hàng hóa" FieldName="ItemUnitId!Key" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px">
                                                                                    <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                        TextFormatString="{0} - {1}" ValueField="ItemUnitId"
                                                                                        ValueType="System.Guid" OnItemRequestedByValue="comboItemUnit_ItemRequestedByValue"
                                                                                        OnItemsRequestedByFilterCondition="comboItemUnit_ItemsRequestedByFilterCondition"
                                                                                        DropDownStyle="DropDown">
                                                                                        <Columns>
                                                                                            
<dx:ListBoxColumn Caption="Mã Hàng Hóa" FieldName="ItemId.Code" Name="Code" Width="150px" />
                                                                                            
<dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="ItemId.Name" Name="Name" Width="300px" />
                                                                                            
<dx:ListBoxColumn Caption="Nhà sản xuất" FieldName="ItemId.ManufacturerOrgId.Name"
                                                                                                Width="150px" />
                                                                                            
<dx:ListBoxColumn Caption="Đvt" FieldName="UnitId.Name" Name="UnitName" Width="100px" />
                                                                                            
<dx:ListBoxColumn Caption="VAT" FieldName="ItemId.VatPercentage" Width="0px" />
                                                                                            
<dx:ListBoxColumn FieldName="ItemUnitId" Width="0px" />
                                                                                        
</Columns>
                                                                                    
</PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="70px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="Price" Name="Price" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="4" Width="100px">
                                                                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colProductPrice" runat="server" ClientInstanceName="colProductPrice"
                                                                                            DisplayFormatString="0,0" Height="21px" MaxValue="999999999999" MinValue="1"
                                                                                            Number="0" OnInit="colProductPrice_Init" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="TotalPrice" Name="TotalPrice"
                                                                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                                                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colProductSum" runat="server" ClientInstanceName="colProductSum"
                                                                                            DisplayFormatString="0,0" Height="21px" Number="0" OnInit="colProductSum_Init"
                                                                                            Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Comment" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="8" Width="200px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="9" Width="100px">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số lượng " FieldName="Quantity" Name="Quantity"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colProductAmount" runat="server" ClientInstanceName="colProductAmount"
                                                                                            DisplayFormatString="0,0" Height="21px" MaxValue="999999999999" MinValue="1"
                                                                                            Number="0" OnInit="colProductAmount_Init" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemUnitId.ItemId.Name"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Chiết Khấu (%)" FieldName="PromotionInPercentage"
                                                                                    Name="PromotionInPercentage" ShowInCustomizationForm="True" VisibleIndex="6"
                                                                                    Width="70px">
                                                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colProductDiscountPercent" runat="server" ClientInstanceName="colProductDiscountPercent"
                                                                                            DisplayFormatString="0.0" Height="21px" MaxValue="100" Number="0" OnInit="colProductDiscountPercent_Init"
                                                                                            Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="BillId" FieldName="BillId!Key" Name="BillId"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="11" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiền CK" FieldName="PromotionInNumber" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="17" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiền CK" FieldName="TotalPriceCK" ReadOnly="True"
                                                                                    ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="19" Width="0px">
                                                                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                    </PropertiesTextEdit>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="ItemUnitId.ItemId.ManufacturerOrgId.Name"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="21" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tồn kho" Name="Balance" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="15" Width="0px">
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colBalance" runat="server" ClientInstanceName="colBalance" Height="21px"
                                                                                            Number="0" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" VisibleIndex="13"
                                                                                    Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="VAT (%)" FieldName="ItemUnitId.ItemId.VatPercentage"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="7" Width="70px">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True" />
                                                                            <SettingsPager PageSize="50" RenderMode="Classic" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" />
                                                                            <SettingsLoadingPanel Mode="Disabled" ShowImage="False" />

<SettingsLoadingPanel Mode="Disabled" ShowImage="False"></SettingsLoadingPanel>

                                                                            <Styles>
                                                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                </Header>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                        <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2" Width="100%">
                                                                            <Items>
                                                                                <dx:LayoutGroup Caption="Chiết khấu hàng hóa" ColCount="2" ColSpan="2">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Chiết khấu (%)">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtProductDiscountAmount" runat="server" ClientInstanceName="txtProductDiscountAmount"
                                                                                                        DisplayFormatString="n2" Height="21px" MaxValue="100" Number="0" Width="80px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ClientSideEvents ValueChanged="txtProductDiscountAmount_ValueChanged" />
                                                                                                        <ClientSideEvents ValueChanged="txtProductDiscountAmount_ValueChanged"></ClientSideEvents>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Tiền CK">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtProductDiscountSum" runat="server" ClientInstanceName="txtProductDiscountSum"
                                                                                                        DisplayFormatString="0,0" Height="21px" MaxValue="999999999999999" Number="0"
                                                                                                        Width="200px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ClientSideEvents ValueChanged="txtProductDiscountSum_ValueChanged" />
                                                                                                        <ClientSideEvents ValueChanged="txtProductDiscountSum_ValueChanged"></ClientSideEvents>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutGroup Caption="Thuế suất hàng hóa" ColCount="2" ColSpan="2">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Thuế GTGT (%)">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtProductTax" runat="server" ClientInstanceName="txtProductTax"
                                                                                                        DisplayFormatString="n2" Height="21px" MaxValue="100" Number="0" Width="80px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ClientSideEvents ValueChanged="txtProductTax_ValueChanged" KeyDown="txtProductTax_KeyDown">
                                                                                                        </ClientSideEvents>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Tiền thuế GTGT">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtProductTaxValue" runat="server" ClientInstanceName="txtProductTaxValue"
                                                                                                        DisplayFormatString="0,0" Height="21px" MaxValue="999999999999999" Number="0"
                                                                                                        ReadOnly="True" Width="200px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutGroup Caption="Tổng giá trị hàng hóa" ColSpan="2">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Tổng tiền thanh toán">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtProductTotal" runat="server" ClientInstanceName="txtProductTotal"
                                                                                                        DisplayFormatString="0,0" Height="21px" Number="0" ReadOnly="True" Width="150px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dx:ASPxSpinEdit>
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
                                                            <dx:TabPage Text="Dịch vụ">
                                                                <ContentCollection>
                                                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdPurchaseEditService" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdPurchaseEditService" DataSourceID="ServiceXDS" KeyboardSupport="True"
                                                                            KeyFieldName="BillItemId" OnCellEditorInitialize="grdPurchaseEditService_CellEditorInitialize"
                                                                            OnCommandButtonInitialize="grdPurchaseEditService_CommandButtonInitialize1" OnCustomUnboundColumnData="grdPurchaseEditService_CustomUnboundColumnData"
                                                                            OnInitNewRow="grdPurchaseEditService_InitNewRow" OnRowDeleting="grdPurchaseEditService_RowDeleting"
                                                                            OnRowInserting="grdPurchaseEditService_RowInserting" OnRowUpdating="grdPurchaseEditService_RowUpdating"
                                                                            OnRowValidating="grdPurchaseEditService_RowValidating" OnStartRowEditing="grdPurchaseEditService_StartRowEditing"
                                                                            Width="100%">
                                                                            <ClientSideEvents EndCallback="grdPurchaseEditService_EndCallback" Init="grdPurchaseEditService_Init" />
                                                                            <ClientSideEvents EndCallback="grdPurchaseEditService_EndCallback" Init="grdPurchaseEditService_Init">
                                                                            </ClientSideEvents>
                                                                            <TotalSummary>
                                                                                <dx:ASPxSummaryItem FieldName="TotalPrice" SummaryType="Sum" />
                                                                                <dx:ASPxSummaryItem FieldName="TotalPriceCK" SummaryType="Sum" />
                                                                            </TotalSummary>
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã Dịch Vụ" FieldName="ItemUnitId!Key" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px">
                                                                                    <PropertiesComboBox DataSourceID="UnitItemServiceXDS" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                        TextFormatString="{0} - {1}" ValueField="ItemUnitId" ValueType="System.Guid"
                                                                                        TextField="ItemId.Code">
                                                                                        <Columns>
                                                                                            
<dx:ListBoxColumn Caption="Mã dịch vụ" FieldName="ItemId.Code" Name="ItemId.Code"
                                                                                                Width="150px" />
                                                                                            
<dx:ListBoxColumn Caption="Tên dịch vụ" FieldName="ItemId.Name" Name="ItemId.Name"
                                                                                                Width="300px" />
                                                                                            
<dx:ListBoxColumn Caption="Đvt" FieldName="UnitId.Name" />
                                                                                            
<dx:ListBoxColumn Caption="Nhà sản xuất" FieldName="ItemId.ManufacturerOrgId.Name"
                                                                                                Width="150px" />
                                                                                            
<dx:ListBoxColumn Caption="VAT" FieldName="ItemId.VatPercentage" />
                                                                                        
</Columns>
                                                                                    
</PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên Dịch Vụ" FieldName="ItemUnitId.ItemId.Name"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="Quantity" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Width="120px">
                                                                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colServiceAmount" runat="server" ClientInstanceName="colServiceAmount"
                                                                                            DisplayFormatString="0,0" Height="21px" NullText="0" Number="0" OnInit="colServiceAmount_Init"
                                                                                            Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Thành Tiền " FieldName="TotalPrice" ReadOnly="True"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colServiceSum" runat="server" ClientInstanceName="colServiceSum"
                                                                                            DisplayFormatString="0,0" Height="21px" Number="0" OnInit="colServiceSum_Init"
                                                                                            Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" Name="Command" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="9" Width="100px">
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
                                                                                    <CancelButton>
                                                                                        <Image ToolTip="Bỏ qua">
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image ToolTip="Cập nhật">
                                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                        <Image ToolTip="Hủy">
                                                                                            <SpriteProperties CssClass="Sprite_Clear" />
                                                                                            <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                                        </Image>
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiền CK" FieldName="TotalPriceCK" ShowInCustomizationForm="True"
                                                                                    UnboundType="Decimal" VisibleIndex="11" Width="0px">
                                                                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                    </PropertiesTextEdit>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="VAT (%)" FieldName="ItemUnitId.ItemId.VatPercentage"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="7" Width="70px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Chiết khấu (%)" FieldName="PromotionInPercentage"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="6" Width="70px">
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colServiceDiscountPercent" runat="server" ClientInstanceName="colServiceDiscountPercent"
                                                                                            DisplayFormatString="0,0" Height="21px" Number="0" OnInit="colServiceDiscountPercent_Init"
                                                                                            Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="Price" Name="Price" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="4" Width="120px">
                                                                                    <PropertiesTextEdit DisplayFormatString="n2">
                                                                                    </PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colServicePrice" runat="server" ClientInstanceName="colServicePrice"
                                                                                            DisplayFormatString="0,0" Height="21px" MaxValue="999999999999" MinValue="1"
                                                                                            Number="0" OnInit="colServicePrice_Init" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="BillId!Key" FieldName="BillId!Key" Name="BillId!Key"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="15" Width="0px">
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colServiceQuantity" runat="server" ClientInstanceName="colServiceQuantity"
                                                                                            DisplayFormatString="0,0" Height="21px" MaxValue="999999999999" MinValue="1"
                                                                                            Number="0" OnInit="colServiceAmount_Init" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="BillItemId" FieldName="BillItemId" Name="BillItemId"
                                                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="13" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Comment" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="8" Width="150px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiền chiết khấu" FieldName="PromotionInNumber"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="12" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="70px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="ItemUnitId.ItemId.ManufacturerOrgId.Name"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="17" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" VisibleIndex="10"
                                                                                    Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsBehavior ConfirmDelete="True" />
                                                                            <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" />
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                            <Settings HorizontalScrollBarMode="Auto" VerticalScrollableHeight="250" VerticalScrollBarMode="Auto">
                                                                            </Settings>
                                                                            <Styles>
                                                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                </Header>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                        <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" ColCount="2" Width="100%">
                                                                            <Items>
                                                                                <dx:LayoutGroup Caption="Chiết khấu dịch vụ" ColCount="2" ColSpan="2">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Chiết khấu (%)">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtServiceDiscountAmount" runat="server" ClientInstanceName="txtServiceDiscountAmount"
                                                                                                        DisplayFormatString="0,0" Height="21px" MaxValue="100" Number="0" Width="80px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ClientSideEvents ValueChanged="txtServiceDiscountAmount_ValueChanged"></ClientSideEvents>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Tiền CK">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtServiceDiscountSum" runat="server" ClientInstanceName="txtServiceDiscountSum"
                                                                                                        DisplayFormatString="0,0" Height="21px" MaxValue="999999999999999" Number="0"
                                                                                                        Width="200px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ClientSideEvents ValueChanged="txtServiceDiscountSum_ValueChanged" />
                                                                                                        <ClientSideEvents ValueChanged="txtServiceDiscountSum_ValueChanged"></ClientSideEvents>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutGroup Caption="Thuế suất dịch vụ" ColCount="2" ColSpan="2">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Thuế GTGT (%)">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtServiceTax" runat="server" ClientInstanceName="txtServiceTax"
                                                                                                        DisplayFormatString="0,0" Height="21px" MaxValue="100" Number="0" Width="80px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                        <ClientSideEvents ValueChanged="txtServiceTax_ValueChanged" />
                                                                                                        <ClientSideEvents ValueChanged="txtServiceTax_ValueChanged"></ClientSideEvents>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                        <dx:LayoutItem Caption="Tiền Thuế GTGT">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtServiceTaxValue" runat="server" ClientInstanceName="txtServiceTaxValue"
                                                                                                        DisplayFormatString="0,0" Height="21px" MaxValue="999999999999999" Number="0"
                                                                                                        ReadOnly="True" Width="200px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutGroup Caption="Tổng giá trị dịch vụ" ColSpan="2">
                                                                                    <Items>
                                                                                        <dx:LayoutItem Caption="Tổng tiền thanh toán ">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxSpinEdit ID="txtServiceTotal" runat="server" ClientInstanceName="txtServiceTotal"
                                                                                                        DisplayFormatString="0,0" Height="21px" Number="0" ReadOnly="True" Width="200px">
                                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                                        </SpinButtons>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                            </Items>
                                                                            <SettingsItems HorizontalAlign="Left" />
                                                                            <SettingsItems HorizontalAlign="Left"></SettingsItems>
                                                                        </dx:ASPxFormLayout>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Tiến độ giao hàng">
                                                                <ContentCollection>
                                                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxFormLayout ID="ASPxFormLayout5" runat="server" Width="100%">
                                                                            <Items>
                                                                                <dx:LayoutGroup Caption="Kế Hoạch">
                                                                                    <Items>
                                                                                        <dx:LayoutItem ShowCaption="False">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxGridView ID="grdDeliveryBillItem" runat="server" AutoGenerateColumns="False"
                                                                                                        ClientInstanceName="grdDeliveryBillItem" DataSourceID="SalesInvoiceInventoryTransactionXDS"
                                                                                                        KeyFieldName="InventoryTransactionId" OnCommandButtonInitialize="grdDeliveryBillItem_CommandButtonInitialize"
                                                                                                        OnRowDeleting="grdDeliveryBillItem_RowDeleting" OnRowInserted="grdDeliveryBillItem_RowInserted"
                                                                                                        OnRowInserting="grdDeliveryBillItem_RowInserting" OnRowUpdating="grdDeliveryBillItem_RowUpdating"
                                                                                                        Width="100%">
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataTextColumn FieldName="InventoryTransactionId" ReadOnly="True" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="4" Visible="false">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Tên đợt giao" FieldName="Code" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="1" Width="300px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataDateColumn FieldName="CreateDate" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="6" Visible="false">
                                                                                                            </dx:GridViewDataDateColumn>
                                                                                                            <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="0" Visible="false">
                                                                                                            </dx:GridViewDataDateColumn>
                                                                                                            <dx:GridViewDataTextColumn Width="100%" Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="2">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" VisibleIndex="8"
                                                                                                                Visible="false">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="3" Width="100px">
                                                                                                                <EditButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </EditButton>
                                                                                                                <NewButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </NewButton>
                                                                                                                <DeleteButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </DeleteButton>
                                                                                                                <CancelButton>
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </CancelButton>
                                                                                                                <UpdateButton>
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </UpdateButton>
                                                                                                                <ClearFilterButton Visible="True">
                                                                                                                </ClearFilterButton>
                                                                                                            </dx:GridViewCommandColumn>
                                                                                                            <dx:GridViewDataTextColumn FieldName="SalesInvoiceId!Key" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="10" Visible="false">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                        </Columns>
                                                                                                        <SettingsBehavior AllowFocusedRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                                                                                        <SettingsPager PageSize="8" ShowEmptyDataRows="True">
                                                                                                        </SettingsPager>
                                                                                                        <SettingsEditing Mode="Inline" />
                                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="160" VerticalScrollBarMode="Visible" />
                                                                                                        <SettingsDetail ShowDetailRow="True" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings VerticalScrollableHeight="160" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible"></Settings>

<SettingsDetail ShowDetailRow="True"></SettingsDetail>

                                                                                                        <Styles>
                                                                                                            <Header Font-Bold="True">
                                                                                                            </Header>
                                                                                                        </Styles>
                                                                                                        <Templates>
                                                                                                            <DetailRow>
                                                                                                                <dx:ASPxGridView ID="grdDeliverySchedule" runat="server" AutoGenerateColumns="False"
                                                                                                                    ClientInstanceName="grdDeliverySchedule" 
                                                                                                                    DataSourceID="InventoryJournalXDS" KeyFieldName="InventoryTransactionId!Key"
                                                                                                                    OnCommandButtonInitialize="grdDeliverySchedule_CommandButtonInitialize" OnCustomColumnDisplayText="grdDeliverySchedule_CustomColumnDisplayText"
                                                                                                                    OnRowDeleting="grdDeliverySchedule_RowDeleting" OnRowInserting="grdDeliverySchedule_RowInserting"
                                                                                                                    OnRowUpdating="grdDeliverySchedule_RowUpdating" Width="100%" OnCellEditorInitialize="grdDeliverySchedule_CellEditorInitialize"
                                                                                                                    OnRowInserted="grdDeliverySchedule_RowInserted" 
                                                                                                                    onbeforeperformdataselect="grdDeliverySchedule_BeforePerformDataSelect">
                                                                                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="160" VerticalScrollBarMode="Visible">
                                                                                                                    </Settings>
                                                                                                                    <TotalSummary>
                                                                                                                        <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" />
                                                                                                                    </TotalSummary>
                                                                                                                    <Columns>
                                                                                                                        <dx:GridViewDataTextColumn Caption="Diễn giải" Width="100%" FieldName="Description" VisibleIndex="6">
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataTextColumn Caption="SL dự kiến" FieldName="Debit" VisibleIndex="2">
                                                                                                                            <PropertiesTextEdit>
                                                                                                                                <ValidationSettings>
                                                                                                                                    <RequiredField IsRequired="true" ErrorText="Chưa nhập 'Số lượng dự kiến'" />
                                                                                                                                </ValidationSettings>
                                                                                                                            </PropertiesTextEdit>
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="7">
                                                                                                                            <EditButton Visible="True">
                                                                                                                                <Image>
                                                                                                                                    <SpriteProperties CssClass="Sprite_Edit" />
                                                                                                                                    <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                                                                </Image>
                                                                                                                            </EditButton>
                                                                                                                            <NewButton Visible="True">
                                                                                                                                <Image>
                                                                                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                                                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                                                                </Image>
                                                                                                                            </NewButton>
                                                                                                                            <DeleteButton Visible="True">
                                                                                                                                <Image>
                                                                                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                                                                </Image>
                                                                                                                            </DeleteButton>
                                                                                                                            <CancelButton Visible="True">
                                                                                                                                <Image>
                                                                                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                                                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                                                                </Image>
                                                                                                                            </CancelButton>
                                                                                                                            <UpdateButton Visible="True">
                                                                                                                                <Image>
                                                                                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                                                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                                                                </Image>
                                                                                                                            </UpdateButton>
                                                                                                                            <ClearFilterButton Visible="True">
                                                                                                                            </ClearFilterButton>
                                                                                                                        </dx:GridViewCommandColumn>
                                                                                                                        <dx:GridViewDataTextColumn FieldName="InventoryTransactionId!Key" VisibleIndex="9"
                                                                                                                            Visible="false">
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataTextColumn FieldName="JournalType" VisibleIndex="11" Visible="false">
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Hàng hóa dịch vụ" FieldName="ItemUnitId!Key"
                                                                                                                            VisibleIndex="0" Width="120px">
                                                                                                                            <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                                                                ValueField="ItemUnitId!Key" ValueType="System.Guid" TextFormatString="{0}" OnItemRequestedByValue="colBillItemRequestedByValue"
                                                                                                                                OnItemsRequestedByFilterCondition="colBillItemsRequestedByFilterCondition" TextField="ItemUnitId.ItemId.Code">
                                                                                                                                <Columns>
                                                                                                                                    <dx:ListBoxColumn Caption="Mã HHDV" FieldName="ItemUnitId.ItemId.Code" />
                                                                                                                                    <dx:ListBoxColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Name" />
                                                                                                                                </Columns>
                                                                                                                            </PropertiesComboBox>
                                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                                        <dx:GridViewDataTextColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Name" VisibleIndex="1"
                                                                                                                            Width="70px">
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataTextColumn FieldName="AccountId!Key" VisibleIndex="13" Visible="false">
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataTextColumn FieldName="InventoryJournalId" VisibleIndex="15" Visible="false">
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Số lô" FieldName="LotId!Key" VisibleIndex="4"
                                                                                                                            Width="100px">
                                                                                                                            <PropertiesComboBox TextField="Code" TextFormatString="{0}" ValueField="LotId" ValueType="System.Guid"
                                                                                                                                EnableCallbackMode="true" IncrementalFilteringMode="Contains" OnItemRequestedByValue="colLotIdOnItemRequestedByValue"
                                                                                                                                OnItemsRequestedByFilterCondition="colLotIdOnItemsRequestedByFilterCondition">
                                                                                                                                <Columns>
                                                                                                                                    <dx:ListBoxColumn Caption="Số lô" FieldName="Code" Width="100px" />
                                                                                                                                    <dx:ListBoxColumn Caption="Hạn sử dụng" FieldName="ExpireDate" Width="100px" />
                                                                                                                                </Columns>
                                                                                                                            </PropertiesComboBox>
                                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Kho" FieldName="InventoryId!Key" VisibleIndex="5"
                                                                                                                            Width="200px">
                                                                                                                            <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                                                                TextField="Name" TextFormatString="{1}" ValueField="InventoryId" ValueType="System.Guid"
                                                                                                                                OnItemRequestedByValue="colInventoryOnItemRequestedByValue" OnItemsRequestedByFilterCondition="colInventoryOnItemsRequestedByFilterCondition">
                                                                                                                                <Columns>
                                                                                                                                    <dx:ListBoxColumn Caption="Mã kho" FieldName="Code" Width="100px" />
                                                                                                                                    <dx:ListBoxColumn Caption="Tên kho" FieldName="Name" Width="200px" />
                                                                                                                                </Columns>
                                                                                                                            </PropertiesComboBox>
                                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                                    </Columns>
                                                                                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                                                    <SettingsPager ShowEmptyDataRows="True">
                                                                                                                    </SettingsPager>
                                                                                                                    <SettingsEditing Mode="Inline" />
                                                                                                                    <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="160" />
                                                                                                                    <Styles>
                                                                                                                        <Header Font-Bold="True">
                                                                                                                        </Header>
                                                                                                                    </Styles>
                                                                                                                </dx:ASPxGridView>
                                                                                                            </DetailRow>
                                                                                                        </Templates>
                                                                                                    </dx:ASPxGridView>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutGroup Caption="Thực giao">
                                                                                    <Items>
                                                                                        <dx:LayoutItem ShowCaption="False">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxGridView ID="grdDeliveryScheduleActual" runat="server" AutoGenerateColumns="False"
                                                                                                        ClientInstanceName="grdDeliveryScheduleActual" KeyFieldName="InventoryJournalId"
                                                                                                        Width="100%" OnCustomCallback="grdDeliveryScheduleActual_CustomCallback">
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="7" Width="300px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataComboBoxColumn Caption="Kho" FieldName="InventoryId.Code" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="6" Width="100px">
                                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="LotId.Code" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="5" Width="100px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="SL thực giao " FieldName="Credit" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="4" Width="100px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataComboBoxColumn Caption="Hàng hóa dịch vụ" FieldName="ItemUnitId.ItemId.Name" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="2" Width="150px">
                                                                                                                <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                                                    TextField="ItemUnitId.ItemId.Code" TextFormatString="{0}" ValueField="ItemUnitId!Key"
                                                                                                                    ValueType="System.Guid">
                                                                                                                    <Columns>
                                                                                                                        
<dx:ListBoxColumn Caption="Mã HHDV" FieldName="ItemUnitId.ItemId.Code" />
                                                                                                                        
<dx:ListBoxColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Name" />
                                                                                                                    
</Columns>
                                                                                                                
</PropertiesComboBox>
                                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Đvt" FieldName="ItemUnitId.UnitId.Code" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="3" Width="70px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Tên đợt giao" FieldName="InventoryTransactionId.Code" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="1" Width="150px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Ngày" FieldName="InventoryTransactionId.IssueDate" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="0" Width="100px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                        </Columns>
                                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                                                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                                                        <SettingsPager ShowEmptyDataRows="True">
                                                                                                        </SettingsPager>
                                                                                                        <SettingsEditing Mode="Inline" />
                                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="160" VerticalScrollBarMode="Visible" />
                                                                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                                        <Settings VerticalScrollableHeight="160" HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible">
                                                                                                        </Settings>
                                                                                                        <Styles>
                                                                                                            <Header Font-Bold="True">
                                                                                                            </Header>
                                                                                                        </Styles>
                                                                                                    </dx:ASPxGridView>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxButton ID="bOutputInventoryCommand" runat="server" HorizontalAlign="Right"
                                                                                                Text="Tạo phiếu xuất kho" Wrap="False" AutoPostBack="False" 
                                                                                                UseSubmitBehavior="False" OnLoad="bOutputInventoryCommand_Load">
                                                                                            </dx:ASPxButton>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                            </Items>
                                                                        </dx:ASPxFormLayout>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                            <dx:TabPage Text="Tiến độ thanh toán">
                                                                <ContentCollection>
                                                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server" Width="100%">
                                                                            <Items>
                                                                                <dx:LayoutGroup Caption="Kế hoạch">
                                                                                    <Items>
                                                                                        <dx:LayoutItem ShowCaption="False">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxGridView ID="grdPaymentSchedule" runat="server" AutoGenerateColumns="False"
                                                                                                        ClientInstanceName="grdPaymentSchedule" DataSourceID="SaleInvoiceTransactionXDS"
                                                                                                        KeyFieldName="TransactionId" OnCommandButtonInitialize="grdPaymentSchedule_CommandButtonInitialize"
                                                                                                        OnRowDeleting="grdPaymentSchedule_RowDeleting" OnRowInserted="grdPaymentSchedule_RowInserted"
                                                                                                        OnRowInserting="grdPaymentSchedule_RowInserting" OnRowUpdating="grdPaymentSchedule_RowUpdating"
                                                                                                        Width="100%">
                                                                                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                                                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="150" VerticalScrollBarMode="Visible">
                                                                                                        </Settings>
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Description" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="3" Width="300px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Số tiền thanh toán " FieldName="Amount" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="2" Width="200px">
                                                                                                                <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                                                </PropertiesTextEdit>
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataDateColumn Caption="Ngày thanh toán dự kiến" FieldName="IssueDate"
                                                                                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
                                                                                                            </dx:GridViewDataDateColumn>
                                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="4" Width="100px">
                                                                                                                <EditButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </EditButton>
                                                                                                                <NewButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </NewButton>
                                                                                                                <DeleteButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </DeleteButton>
                                                                                                                <CancelButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </CancelButton>
                                                                                                                <UpdateButton Visible="True">
                                                                                                                    <Image>
                                                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                                                    </Image>
                                                                                                                </UpdateButton>
                                                                                                                <ClearFilterButton Visible="True">
                                                                                                                </ClearFilterButton>
                                                                                                            </dx:GridViewCommandColumn>
                                                                                                            <dx:GridViewDataTextColumn FieldName="TransactionId" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="6" Visible="false">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" VisibleIndex="7"
                                                                                                                Visible="false">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Width="100%" Caption="Tên đợt thanh toán" FieldName="Code" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="1">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                        </Columns>
                                                                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                                                                        <SettingsPager ShowEmptyDataRows="True">
                                                                                                        </SettingsPager>
                                                                                                        <SettingsEditing Mode="Inline" />
                                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="150"
                                                                                                            ShowFooter="True" />
                                                                                                    </dx:ASPxGridView>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutGroup Caption="Thực thu">
                                                                                    <Items>
                                                                                        <dx:LayoutItem ShowCaption="False">
                                                                                            <LayoutItemNestedControlCollection>
                                                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                                    <dx:ASPxGridView ID="grdPaymentScheduleActual" runat="server" AutoGenerateColumns="False"
                                                                                                        ClientInstanceName="grdPaymentScheduleActual" OnCommandButtonInitialize="grdPaymentSchedule_CommandButtonInitialize"
                                                                                                        OnRowDeleting="grdPaymentSchedule_RowDeleting" OnRowInserted="grdPaymentSchedule_RowInserted"
                                                                                                        OnRowInserting="grdPaymentSchedule_RowInserting" OnRowUpdating="grdPaymentSchedule_RowUpdating"
                                                                                                        Width="100%" OnCustomCallback="grdPaymentScheduleActual_CustomCallback">
                                                                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                                                                        <SettingsEditing Mode="Inline" />
                                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" VerticalScrollableHeight="150"
                                                                                                            ShowFooter="True" />
                                                                                                        <ClientSideEvents EndCallback="grdPaymentScheduleActual_EndCallback" />
                                                                                                        <ClientSideEvents EndCallback="grdPaymentScheduleActual_EndCallback"></ClientSideEvents>
                                                                                                        <TotalSummary>
                                                                                                            <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" DisplayFormat="Đã thanh toán : {0:0,0}" />
                                                                                                        </TotalSummary>
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Description" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="3" Width="300px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Chứng từ gốc" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="2" Width="200px">
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                            <dx:GridViewDataDateColumn Caption="Ngày " FieldName="CreateDate" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="0" Width="300px">
                                                                                                            </dx:GridViewDataDateColumn>
                                                                                                            <dx:GridViewDataTextColumn Caption="Số tiền" FieldName="Debit" ShowInCustomizationForm="True"
                                                                                                                VisibleIndex="1" Width="200px">
                                                                                                                <PropertiesTextEdit DisplayFormatString="0,0">
                                                                                                                </PropertiesTextEdit>
                                                                                                            </dx:GridViewDataTextColumn>
                                                                                                        </Columns>
                                                                                                        <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                                                                                        <SettingsPager ShowEmptyDataRows="True">
                                                                                                        </SettingsPager>
                                                                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                                                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="150" VerticalScrollBarMode="Visible">
                                                                                                        </Settings>
                                                                                                    </dx:ASPxGridView>
                                                                                                </dx:LayoutItemNestedControlContainer>
                                                                                            </LayoutItemNestedControlCollection>
                                                                                        </dx:LayoutItem>
                                                                                    </Items>
                                                                                </dx:LayoutGroup>
                                                                                <dx:LayoutItem ShowCaption="False">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxButton ID="bPayment" ClientInstanceName="bPayment" runat="server" Text="Tạo phiếu thu"
                                                                                                UseSubmitBehavior="False" AutoPostBack="false">
                                                                                                <ClientSideEvents Click="bPayment_Click"></ClientSideEvents>
                                                                                            </dx:ASPxButton>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                            </Items>
                                                                        </dx:ASPxFormLayout>
                                                                    </dx:ContentControl>
                                                                </ContentCollection>
                                                            </dx:TabPage>
                                                        </TabPages>
                                                        <Paddings Padding="0px" />
                                                        <Paddings Padding="0px"></Paddings>
                                                        <ScrollButtonStyle HorizontalAlign="Right">
                                                        </ScrollButtonStyle>
                                                    </dx:ASPxPageControl>
                                                    <div>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                    <dx:SplitterPane Size="20%" Visible="False">
                                        <ContentCollection>
                                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdItemProperty" runat="server" ClientInstanceName="grdItemProperty"
                                                    AutoGenerateColumns="False">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Thuộc tính" FieldName="Code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                </Panes>
                            </dx:ASPxSplitter>
                            <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" Width="100%">
                                <Items>
                                    <dx:LayoutGroup Caption="Tổng giá trị phiếu hàng" ColCount="4">
                                        <Items>
                                            <dx:LayoutItem Caption="Tổng tiền hàng hóa">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtSumProduct" runat="server" ClientInstanceName="txtSumProduct"
                                                            DisplayFormatString="0,0" Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtSumService" runat="server" ClientInstanceName="txtSumService"
                                                            DisplayFormatString="0,0" Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tổng tiền chiết khấu">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtSumDiscount" runat="server" ClientInstanceName="txtSumDiscount"
                                                            DisplayFormatString="0,0" Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tổng tiền thuế suất">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtSumTax" runat="server" ClientInstanceName="txtSumTax" DisplayFormatString="0,0"
                                                            Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tổng giá trị">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtAmount" runat="server" ClientInstanceName="txtAmount" DisplayFormatString="0,0"
                                                            Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Đã thanh toán">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtPaid" runat="server" ClientInstanceName="txtPaid" DisplayFormatString="0,0"
                                                            Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Còn lại">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxSpinEdit ID="txtCredit" runat="server" ClientInstanceName="txtCredit" DisplayFormatString="0,0"
                                                            Height="21px" Number="0" ReadOnly="True" Width="100px">
                                                            <SpinButtons ShowIncrementButtons="False">
                                                            </SpinButtons>
                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                                <SettingsItems HorizontalAlign="Left" />
                                <SettingsItems HorizontalAlign="Left"></SettingsItems>
                            </dx:ASPxFormLayout>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>
<dx:XpoDataSource ID="ProductXDS" runat="server" TypeName="NAS.DAL.Invoice.BillItem"
    Criteria="[BillId!Key] = ? And [ItemUnitId.ItemId.ItemCustomTypes][[ObjectTypeId.ObjectTypeId] = ?] And [RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="SaleBillId" />
        <asp:Parameter DefaultValue="5817b239-e150-4c8e-a313-eaa8bd6944c4" Name="ProductObjectTypeId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="BillItemXDS" runat="server" TypeName="NAS.DAL.Invoice.BillItem"
    Criteria="[BillId!Key] = ? And [RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="SaleBillId" />
        <asp:Parameter DefaultValue="5817b239-e150-4c8e-a313-eaa8bd6944c4" Name="ProductObjectTypeId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="GeneralJournalXDS" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
    Criteria="[TransactionId.Code] = ? And [RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="Code" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="GeneralJournalActualXDS" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
    Criteria="[TransactionId.Code] = ? And [RowStatus] &gt; 0 And [JournalType] = 'A'">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="Code" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="InventoryJournalXDS" runat="server" TypeName="NAS.DAL.Inventory.Journal.InventoryJournal"
    Criteria="[InventoryTransactionId] = ? And [Debit] &gt; 0 And [JournalType] = 'P'">
    <CriteriaParameters>
        <asp:SessionParameter Name="InventoryTransactionId" SessionField="InventoryTransactionId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="InventoryJournalActualXDS" runat="server" TypeName="NAS.DAL.Inventory.Journal.InventoryJournal"
    Criteria="[InventoryTransactionId] = ? And [Debit] &gt; 0 And [JournalType] = 'A'">
    <CriteriaParameters>
        <asp:SessionParameter Name="InventoryTransactionId" SessionField="InventoryTransactionId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="SaleInvoiceTransactionXDS" runat="server" TypeName="NAS.DAL.Accounting.Journal.SaleInvoiceTransaction"
    Criteria="[SalesInvoiceId] = ? And [GeneralJournals][[JournalType] = 'P']">
    <CriteriaParameters>
        <asp:SessionParameter Name="SaleBillId" SessionField="SaleBillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="SaleInvoiceTransactionActualXDS" runat="server" TypeName="NAS.DAL.Accounting.Journal.SaleInvoiceTransaction"
    Criteria="[SalesInvoiceId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="SaleBillId" SessionField="SaleBillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="SalesInvoiceInventoryTransactionActualXDS" runat="server" TypeName="NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction"
    Criteria="[SalesInvoiceId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="SalesInvoiceId" SessionField="SaleBillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="SalesInvoiceInventoryTransactionXDS" runat="server" TypeName="NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction"
    Criteria="[SalesInvoiceId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="SalesInvoiceId" SessionField="SaleBillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="BuyerXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
    Criteria="[RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="SaleBillId" />
        <asp:Parameter DefaultValue="5817b239-e150-4c8e-a313-eaa8bd6944c4" Name="ProductObjectTypeId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="UserXDS" runat="server" Criteria="[RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="SaleBillId" />
        <asp:Parameter DefaultValue="5817b239-e150-4c8e-a313-eaa8bd6944c4" Name="ProductObjectTypeId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Person"
    ID="PersonXDS" Criteria="[RowStatus] &gt; 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="UnitItemServiceXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Item.ItemUnit"
    Criteria="[ItemId.ItemCustomTypes][[ObjectTypeId.ObjectTypeId] = ?] And [ItemId.RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:Parameter DefaultValue="bebab7e7-8294-4eb0-81df-b5e33acbfe76" Name="newparameter" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="ServiceXDS" runat="server" TypeName="NAS.DAL.Invoice.BillItem"
    Criteria="[BillId!Key] = ? And [ItemUnitId.ItemId.ItemCustomTypes][[ObjectTypeId.ObjectTypeId] = ?] And [RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="SaleBillId" />
        <asp:Parameter DefaultValue="bebab7e7-8294-4eb0-81df-b5e33acbfe76" Name="newparameter" />
    </CriteriaParameters>
</dx:XpoDataSource>
<uc1:uPopupphieumuaban ID="uPopupphieumuaban1" runat="server" />
<uc3:uEdittingOutputInventoryCommand ID="uEdittingOutputInventoryCommand1" 
    runat="server" SharedClientEvent="SharedClientEvent" />
<dx:ASPxHiddenField ID="hPurchaseEditId" runat="server" ClientInstanceName="hPurchaseEditId">
</dx:ASPxHiddenField>
<dx:XpoDataSource ID="BillActorXDS" runat="server" Criteria="[BillId] = ?" TypeName="NAS.DAL.Invoice.BillActor">
    <CriteriaParameters>
        <asp:SessionParameter Name="SaleBillId" SessionField="SaleBillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:ASPxPopupControl ID="formBillActor" runat="server" Height="268px" RenderMode="Lightweight"
    Width="633px" ClientInstanceName="formBillActor" PopupElementID="bBillActor"
    HeaderText="" OnWindowCallback="formBillActor_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grdBillActor" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdBillActor"
                DataSourceID="BillActorXDS" KeyFieldName="BillActorId" OnRowUpdating="grdBillActor_RowUpdating"
                Width="100%" OnCustomColumnDisplayText="grdBillActor_CustomColumnDisplayText">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="BillActorId" ShowInCustomizationForm="True"
                        VisibleIndex="3" ReadOnly="True" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Loại đối tượng" FieldName="BillActorTypeId!Key"
                        ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="200px">
                        <PropertiesComboBox TextField="Description" TextFormatString="{1}" ValueField="BillActorTypeId"
                            ValueType="System.Guid" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                            OnItemRequestedByValue="colBillActorTypeItemRequestedByValue" OnItemsRequestedByFilterCondition="colBillActorTypeItemsRequestedByFilterCondition">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Name" />
                                <dx:ListBoxColumn FieldName="Description" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="OrganizationId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="4" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Tên đối tượng" FieldName="PersonId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="1" Width="300px">
                        <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                            TextField="Name" TextFormatString="{0}-{1}" ValueField="PersonId" ValueType="System.Guid"
                            OnItemRequestedByValue="colPersonOnItemRequestedByValue" OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition">
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã đối tượng" FieldName="Code" />
                                <dx:ListBoxColumn Caption="Tên đối tượng" FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="BillId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="5" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                        VisibleIndex="2" Width="70px">
                        <EditButton Visible="True">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Edit" />
                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                            </Image>
                        </EditButton>
                        <CancelButton>
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel" />
                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                            </Image>
                        </CancelButton>
                        <UpdateButton>
                            <Image>
                                <SpriteProperties CssClass="Sprite_Apply" />
                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                            </Image>
                        </UpdateButton>
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                </Columns>
                <SettingsBehavior AllowFocusedRow="True" />
                <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                <SettingsPager ShowEmptyDataRows="True">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                <SettingsEditing Mode="Inline"></SettingsEditing>
                <Settings VerticalScrollBarMode="Visible"></Settings>
            </dx:ASPxGridView>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<uc4:ReceiptVoucherEditingForm ID="ReceiptVoucherEditingForm1" runat="server" />
