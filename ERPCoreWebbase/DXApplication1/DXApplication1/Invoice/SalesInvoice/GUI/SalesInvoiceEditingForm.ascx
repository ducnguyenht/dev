<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="SalesInvoiceEditingForm.ascx.cs"
    Inherits="WebModule.Invoice.SalesInvoice.GUI.SalesInvoiceEditingForm" %>
<%@ Register Src="../../Control/BillTotalSummary/BillTotalSummary.ascx" TagName="BillTotalSummary"
    TagPrefix="uc1" %>
<%@ Register Src="../Control/BillDetails/BillDetails.ascx" TagName="BillDetails"
    TagPrefix="uc2" %>
<%@ Register Src="../Control/BookingEntriesForm/BookingEntriesForm.ascx" TagName="BookingEntriesForm"
    TagPrefix="uc3" %>
<%@ Register Src="../../../Voucher/Receipt/GUI/ReceiptVoucherEditingForm.ascx" TagName="ReceiptVoucherEditingForm"
    TagPrefix="uc4" %>
<%@ Register Src="../../../Accounting/UserControl/DeclareVat.ascx" TagName="DeclareVat"
    TagPrefix="uc6" %>
<%@ Register Src="../../Control/TradingItemExtraInformation/TradingItemExtraInformation.ascx"
    TagName="TradingItemExtraInformation" TagPrefix="uc7" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Src="../../../Sales/UserControl/ReportViewer.ascx" TagName="ReportViewer"
    TagPrefix="uc5" %>
<%@ Register Src="../../../Warehouse/Command/PopupCommand/EdittingOutputInventoryCommand/uEdittingOutputInventoryCommand.ascx"
    TagName="uEdittingOutputInventoryCommand" TagPrefix="uc7" %>
<script type="text/javascript">
    $(document).ready(function () {
        $(ReceiptVoucherEditingForm).on(
            ReceiptVoucherEditingForm.events.eClosing,
            function (evt) {
                billDetails.RefreshPayment();
                billTotalSummary.Refresh();
            }
        );
    });

    var InvoiceEditingFormClass = function () {

        this.Closing = new NASClientEvent();

        this.RaiseClosing = function()
        {
            this.Closing.FireEvent(this, null);
        };

        this.Show = function(recordId)
        {
            var args = '';
            if (recordId) {
                args = 'Edit|' + recordId;
            }
            else {
                args = 'Create';
            }
            if (!panelInvoiceEditingForm.InCallback()) {
                panelInvoiceEditingForm.PerformCallback(args);
                billDetails.ResetActiveTabIndex();
            }
        };

        this.Save = function () {
            //Validate in ReceiptVoucherEditingForm validation group
            var validated =
                ASPxClientEdit.ValidateEditorsInContainer(null, 'GeneralInfo');
            if (validated) {
                var args = 'Save';
                if (!panelInvoiceEditingForm.InCallback()) {
                    panelInvoiceEditingForm.PerformCallback(args);
                }
            }
        };

        this.Cancel = function () {
            var args = 'Cancel';
            if (!panelInvoiceEditingForm.InCallback()) {
                panelInvoiceEditingForm.PerformCallback(args);
            }
        };

    }; 

    function bookingEntriesForm_Closing()
    {
        var args = 'Refresh';
        if (!panelInvoiceEditingForm.InCallback()) {
            panelInvoiceEditingForm.PerformCallback(args);
        }
    }

    var nasObj = new InvoiceEditingFormClass();
    window['<%= _ClientInstanceName %>'] = nasObj;

    <% if(Closing != null && Closing.Trim().Length > 0) { %>
        nasObj.Closing.AddHandler(<%= Closing %>);
    <% } %>

    function panelInvoiceEditingForm_EndCallback(s, e)
    {
        if(s.cpEvent == 'Closing')
        {
            var tempObj = window['<%= _ClientInstanceName %>'];
            tempObj.RaiseClosing();
            delete s.cpEvent;
        }
    }

    function popupInvoiceEditingForm_AfterResizing(s, e) {
        splitter.AdjustControl();
    }

    function btnShowBookingEntries_Click(s, e)
    {
        //console.log(hfBillId.Get('BillId'));
        bookingEntriesForm.Show(hfBillId.Get('BillId'));
    }

    function btnDeclareTax_Click(s, e)
    {
        console.log(hfBillId.Get('BillId'));
        DeclareVatFromBill(hfBillId.Get('BillId'));
    }

    function btnPrint_Click(s, e)
    {
        hReportBillId.Set("id", hfBillId.Get('BillId'));
        cpReportViewer.PerformCallback();
    }

    function btnCreateVoucher_Click(s, e)
    {
        ReceiptVoucherEditingForm.CreateFromBill(hfBillId.Get('BillId'));
    }

    function btnCreateInventoryCommand_Click(s, e)
    {
    }

    function btnSave_Click(s, e)
    {
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.Save();
    }

    function btnCancel_Click(s, e)
    {
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.Cancel();
    }

    function popupInvoiceEditingForm_Closing(s, e)
    {
        var tempObj = window['<%= _ClientInstanceName %>'];
        tempObj.Cancel();
    }


    function billDetails_BillItemClientSideEvents_SelectedTradingItemIndexChanged(s, e)
    {
        var itemId = billDetails.GridViewBillItems.GetComboBoxItem().GetValue();
        var unitId = billDetails.GridViewBillItems.GetComboBoxUnit().GetValue();
        tradingItemExtraInformation.Show(itemId, unitId);
    }

    function billDetails_BillItemClientSideEvents_SelectedTradingUnitIndexChanged(s, e)
    {
        var itemId = billDetails.GridViewBillItems.GetComboBoxItem().GetValue();
        var unitId = billDetails.GridViewBillItems.GetComboBoxUnit().GetValue();
        tradingItemExtraInformation.Show(itemId, unitId);
    }

    function billDetails_BillItemClientSideEvents_StartRowEditing(s, e)
    {
        var itemId = billDetails.GridViewBillItems.GetComboBoxItem().GetValue();
        var unitId = billDetails.GridViewBillItems.GetComboBoxUnit().GetValue();
        tradingItemExtraInformation.Show(itemId, unitId);
    }

    function billDetails_BillItemClientSideEvents_FocusedRowChanged(s, e)
    {
        var focusedRowIndex = s.GetFocusedRowIndex();
        var itemFieldName = billDetails.GridViewBillItems.FieldNames.Item;
        var unitFieldName = billDetails.GridViewBillItems.FieldNames.Unit;
        billDetails.GridViewBillItems
            .GetGridView()
            .GetRowValues(focusedRowIndex, itemFieldName + ';' + unitFieldName, 
                GridViewBillItems_OnGetRowValues);
    }

    function GridViewBillItems_OnGetRowValues(values)
    {
        var itemId = values[0];
        var unitId = values[1];
        tradingItemExtraInformation.Show(itemId, unitId);
    }

    /////Handler event from Input InventoryCommand
    var target = new EventTarget();
    function handleEvent(event) { 
        billDetails.RefreshDeliverySchedule();
    };
    target.addListener("CompleteInventoryCommand", handleEvent);

    function btnPrint_Click(s, e) {       
        hReportBillId.Set("id", hfBillId.Get('BillId'));        
        formReportViewer.PerformCallback();
    }

    function txtCode_Validation(s, e) {
        //Check Code is exist in database
        if (!cpntxtCode.InCallback()) {
            cpntxtCode.PerformCallback();
        }
    }
    
    function formReportViewer_EndCallback(s, e) {
        if (s.cpShowReport) {
            formReportViewer.Show();
            delete(s.cpShowReport);
        }
    }

</script>
<dx:ASPxCallbackPanel ID="panelInvoiceEditingForm" ClientInstanceName="panelInvoiceEditingForm"
    runat="server" Width="100%" OnCallback="panelInvoiceEditingForm_Callback">
    <ClientSideEvents EndCallback="panelInvoiceEditingForm_EndCallback" />
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupInvoiceEditingForm" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" Height="520px"
                Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                RenderMode="Lightweight" ScrollBars="Auto" ShowFooter="True" ShowMaximizeButton="True"
                Width="860px">
                <ClientSideEvents AfterResizing="popupInvoiceEditingForm_AfterResizing" 
                    Closing="popupInvoiceEditingForm_Closing" />
                <FooterTemplate>
                    <div style="padding: 10px;">
                        <div style="float: left">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnHelp" CausesValidation="false" UseSubmitBehavior="false" runat="server"
                                    Text="Trợ giúp" AutoPostBack="false">
                                    <Image ToolTip="Trợ giúp">
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                            </div>
                        </div>
                        <div style="float: right">
                            <div style="float: left;">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCreateInventoryCommand" CausesValidation="false" UseSubmitBehavior="false"
                                    runat="server" Text="Tạo phiếu xuất kho" AutoPostBack="false">
                                    <ClientSideEvents Click="btnCreateInventoryCommand_Click" />
                                    <Image ToolTip="Tạo phiếu xuất kho">
                                        <SpriteProperties CssClass="Sprite_New" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCreateVoucher" CausesValidation="false" UseSubmitBehavior="false"
                                    runat="server" Text="Tạo phiếu thu" AutoPostBack="false">
                                    <ClientSideEvents Click="btnCreateVoucher_Click" />
                                    <Image ToolTip="Tạo phiếu chi">
                                        <SpriteProperties CssClass="Sprite_New" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnShowBookingEntries" CausesValidation="false" UseSubmitBehavior="false"
                                    runat="server" Text="Hạch toán" AutoPostBack="false">
                                    <ClientSideEvents Click="btnShowBookingEntries_Click" />
                                    <Image ToolTip="Hạch toán">
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnDeclareTax" CausesValidation="false" UseSubmitBehavior="false"
                                    runat="server" Text="Kê khai thuế" AutoPostBack="false">
                                    <ClientSideEvents Click="btnDeclareTax_Click" />
                                    <Image ToolTip="Kê khai thuế">
                                        <SpriteProperties CssClass="Sprite_Tax" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnPrint" CausesValidation="false" UseSubmitBehavior="false" runat="server"
                                    Text="In" AutoPostBack="false">
                                    <ClientSideEvents Click="btnPrint_Click" />
                                    <Image ToolTip="In phiếu">
                                        <SpriteProperties CssClass="Sprite_Print" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnSave" runat="server" Text="Lưu lại" AutoPostBack="false">
                                    <ClientSideEvents Click="btnSave_Click" />
                                    <Image ToolTip="Lưu lại">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" CausesValidation="false" UseSubmitBehavior="false"
                                    runat="server" Text="Thoát" AutoPostBack="false">
                                    <ClientSideEvents Click="btnCancel_Click" />
                                    <Image ToolTip="Thoát">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div style="height: 100%; width: 100%; overflow: hidden">
                            <dx:ASPxSplitter ID="ASPxSplitter1" ClientInstanceName="splitter" runat="server"
                                Height="100%" Orientation="Vertical" ResizingMode="Postponed" SeparatorVisible="False"
                                Width="100%">
                                <Panes>
                                    <dx:SplitterPane>
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxSplitter ID="ASPxSplitter2" ClientInstanceName="PurchaseInvoiceEditingForm_ContentSplitter"
                                                    runat="server" Height="100%" Orientation="Horizontal" ResizingMode="Postponed"
                                                    Width="100%" ShowCollapseForwardButton="True">
                                                    <Panes>
                                                        <dx:SplitterPane ScrollBars="Auto">
                                                            <ContentCollection>
                                                                <dx:SplitterContentControl ID="SplitterContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxFormLayout ID="formlayoutInvoiceEditingForm" ClientInstanceName="formlayoutInvoiceEditingForm"
                                                                        runat="server" Width="100%">
                                                                        <Items>
                                                                            <dx:LayoutGroup Caption="Thông tin chung" ColCount="3">
                                                                                <Items>
                                                                                    <dx:LayoutItem Caption="Số phiếu bán" RequiredMarkDisplayMode="Required">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxCallbackPanel ID="cpntxtCode" runat="server" ClientInstanceName="cpntxtCode"
                                                                                                    ShowLoadingPanel="False" Width="100%">
                                                                                                    <PanelCollection>
                                                                                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                            <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" OnValidation="txtCode_Validation"
                                                                                                                Width="170px">
                                                                                                                <ClientSideEvents Validation="txtCode_Validation" />
                                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ValidationGroup="GeneralInfo">
                                                                                                                    <RegularExpression ErrorText="Mã không hợp lệ" ValidationExpression="<%$ Resources:Resources, validation_regex_code %>" />
                                                                                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
                                                                                                                </ValidationSettings>
                                                                                                            </dx:ASPxTextBox>
                                                                                                        </dx:PanelContent>
                                                                                                    </PanelCollection>
                                                                                                </dx:ASPxCallbackPanel>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Ngày" RequiredMarkDisplayMode="Required">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxDateEdit ID="txtIssuedDate" runat="server">
                                                                                                    <ValidationSettings ValidationGroup="GeneralInfo" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxDateEdit>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Khách hàng" RequiredMarkDisplayMode="Required">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxComboBox ID="comboOrganization" runat="server" CallbackPageSize="10" IncrementalFilteringMode="Contains"
                                                                                                    EnableCallbackMode="true" OnItemRequestedByValue="comboOrganization_ItemRequestedByValue"
                                                                                                    OnItemsRequestedByFilterCondition="comboOrganization_ItemsRequestedByFilterCondition"
                                                                                                    TextFormatString="{0} - {1}" ValueField="OrganizationId" ValueType="System.Guid">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã khách hàng" FieldName="Code" />
                                                                                                        <dx:ListBoxColumn Caption="Tên khách hàng" FieldName="Name" />
                                                                                                    </Columns>
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                    <ValidationSettings ValidationGroup="GeneralInfo" Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                                                                        <RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>">
                                                                                                        </RequiredField>
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxComboBox>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Người lập phiếu">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxComboBox ID="comboCreator" EnableCallbackMode="true" CallbackPageSize="10"
                                                                                                    runat="server" OnItemRequestedByValue="comboPerson_ItemRequestedByValue" OnItemsRequestedByFilterCondition="comboPerson_ItemsRequestedByFilterCondition"
                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" ValueField="PersonId"
                                                                                                    ValueType="System.Guid">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn FieldName="Code" Caption="Mã nhân viên" />
                                                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên nhân viên" />
                                                                                                    </Columns>
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                </dx:ASPxComboBox>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Người bán hàng">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxComboBox ID="comboSales" EnableCallbackMode="true" CallbackPageSize="10"
                                                                                                    runat="server" OnItemRequestedByValue="comboPerson_ItemRequestedByValue" OnItemsRequestedByFilterCondition="comboPerson_ItemsRequestedByFilterCondition"
                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" ValueField="PersonId"
                                                                                                    ValueType="System.Guid">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn FieldName="Code" Caption="Mã nhân viên" />
                                                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên nhân viên" />
                                                                                                    </Columns>
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                </dx:ASPxComboBox>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Kế toán trưởng">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxComboBox ID="comboChiefAccountant" EnableCallbackMode="true" CallbackPageSize="10"
                                                                                                    runat="server" OnItemRequestedByValue="comboPerson_ItemRequestedByValue" OnItemsRequestedByFilterCondition="comboPerson_ItemsRequestedByFilterCondition"
                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" ValueField="PersonId"
                                                                                                    ValueType="System.Guid">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn FieldName="Code" Caption="Mã nhân viên" />
                                                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên nhân viên" />
                                                                                                    </Columns>
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                </dx:ASPxComboBox>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                    <dx:LayoutItem Caption="Giám đốc">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <dx:ASPxComboBox ID="comboDirector" EnableCallbackMode="true" CallbackPageSize="10"
                                                                                                    runat="server" OnItemRequestedByValue="comboPerson_ItemRequestedByValue" OnItemsRequestedByFilterCondition="comboPerson_ItemsRequestedByFilterCondition"
                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" ValueField="PersonId"
                                                                                                    ValueType="System.Guid">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn FieldName="Code" Caption="Mã nhân viên" />
                                                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên nhân viên" />
                                                                                                    </Columns>
                                                                                                    <ItemStyle Wrap="True" />
                                                                                                </dx:ASPxComboBox>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                </Items>
                                                                            </dx:LayoutGroup>
                                                                            <dx:LayoutGroup Caption="Thông tin chi tiết">
                                                                                <Items>
                                                                                    <dx:LayoutItem Caption=" " ShowCaption="False">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                                                SupportsDisabledAttribute="True">
                                                                                                <uc2:BillDetails ID="billDetails" runat="server" BillItemsUpdated="function() { billTotalSummary.Refresh(); }"
                                                                                                    ClientInstanceName="billDetails" BillItemClientSideEvents-SelectedTradingItemIndexChanged="billDetails_BillItemClientSideEvents_SelectedTradingItemIndexChanged"
                                                                                                    BillItemClientSideEvents-SelectedTradingUnitIndexChanged="billDetails_BillItemClientSideEvents_SelectedTradingUnitIndexChanged"
                                                                                                    BillItemClientSideEvents-StartRowEditing="billDetails_BillItemClientSideEvents_StartRowEditing"
                                                                                                    BillItemClientSideEvents-GridViewClientSideEvents-FocusedRowChanged="billDetails_BillItemClientSideEvents_FocusedRowChanged" />
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>
                                                                                </Items>
                                                                            </dx:LayoutGroup>
                                                                        </Items>
                                                                    </dx:ASPxFormLayout>
                                                                </dx:SplitterContentControl>
                                                            </ContentCollection>
                                                        </dx:SplitterPane>
                                                        <dx:SplitterPane Size="240" MaxSize="260" ScrollBars="Auto">
                                                            <ContentCollection>
                                                                <dx:SplitterContentControl ID="SplitterContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                                    <uc7:TradingItemExtraInformation ID="tradingItemExtraInformation" runat="server"
                                                                        ClientInstanceName="tradingItemExtraInformation" />
                                                                </dx:SplitterContentControl>
                                                            </ContentCollection>
                                                        </dx:SplitterPane>
                                                    </Panes>
                                                    <Styles>
                                                        <Pane>
                                                            <Paddings Padding="0" />
                                                            <Paddings Padding="0px"></Paddings>
                                                        </Pane>
                                                    </Styles>
                                                </dx:ASPxSplitter>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                    <dx:SplitterPane AutoHeight="true" Size="104">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <uc1:BillTotalSummary ID="billTotalSummary" runat="server" ClientInstanceName="billTotalSummary"
                                                    PromotionInfoClosing="function(s, e) { billDetails.RefreshBillItems(); }" />
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                </Panes>
                                <Styles>
                                    <Pane>
                                        <Paddings Padding="0" />
                                        <Paddings Padding="0px"></Paddings>
                                    </Pane>
                                </Styles>
                            </dx:ASPxSplitter>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:ASPxHiddenField ID="hfBillId" ClientInstanceName="hfBillId" runat="server">
            </dx:ASPxHiddenField>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc3:BookingEntriesForm ID="bookingEntriesForm" runat="server" Closing="bookingEntriesForm_Closing"
    ClientInstanceName="bookingEntriesForm" />
<uc4:ReceiptVoucherEditingForm ID="receiptVoucherEditingForm" runat="server" />
<uc6:DeclareVat ID="declareVat" runat="server" />
<uc5:ReportViewer ID="ReportViewer1" runat="server" />
<uc7:uEdittingOutputInventoryCommand ID="uEdittingOutputInventoryCommand1" runat="server"
    SharedClientEvent="CompleteInventoryCommand" />
