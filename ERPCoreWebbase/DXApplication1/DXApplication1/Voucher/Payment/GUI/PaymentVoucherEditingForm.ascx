<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="PaymentVoucherEditingForm.ascx.cs"
    Inherits="WebModule.Voucher.Payment.GUI.PaymentVoucherEditingForm" %>
<%@ Register Src="../../Controls/GridViewVoucherAllocation/GridViewVoucherAllocation.ascx"
    TagName="GridViewVoucherAllocation" TagPrefix="uc1" %>
<%@ Register Src="../../Controls/VoucherBookingEntriesForm/VoucherBookingEntriesForm.ascx"
    TagName="VoucherBookingEntriesForm" TagPrefix="uc2" %>
<%@ Register Src="../../../Accounting/CurrencyGridLookup/CurrencyGridLookup.ascx"
    TagName="CurrencyGridLookup" TagPrefix="uc3" %>
<script type="text/javascript">

    $(document).ready(function () {
        $(GridViewVoucherAllocation).on(
            GridViewVoucherAllocation.events.eListChanged,
            function (evt) {
                cpnReceiptVoucherEditingForm.PerformCallback('ForceRefresh');
            }
        );

        $(VoucherBookingEntriesForm).on(
            VoucherBookingEntriesForm.events.eBookedEntries,
            function (evt) {
                cpnReceiptVoucherEditingForm.PerformCallback('ForceRefresh');
            }
        );
    });

    var PaymentVoucherEditingForm = {

        //declare properties
        events: {
            eShown: 'shown',
            eSaved: 'saved',
            eClosing: 'closing'
        },

        actions: {
            tEdit: 'Edit',
            tCreate: 'Create',
            tSave: 'Save',
            tCancel: 'Cancel'
        },

        Show: function (recordId) {
            var args = '';
            if (recordId) {
                args = this.actions.tEdit + '|' + recordId;
            }
            else {
                args = this.actions.tCreate;
            }
            if (!cpnReceiptVoucherEditingForm.InCallback()) {
                cpnReceiptVoucherEditingForm.PerformCallback(args);
            }
        },

        CreateFromBill: function (billId) {
            var args = '';
            if (billId) {
                args = this.actions.tCreate + '|' + billId;
            }
            else {
                throw new Error(200, 'BillId cannot be null');
            }
            if (!cpnReceiptVoucherEditingForm.InCallback()) {
                cpnReceiptVoucherEditingForm.PerformCallback(args);
            }
        },

        Save: function () {
            //Validate in ReceiptVoucherEditingForm validation group
            var validated =
                ASPxClientEdit.ValidateEditorsInContainer(null, 'ReceiptVoucherEditingForm');
            if (validated) {
                var args = this.actions.tSave;
                if (!cpnReceiptVoucherEditingForm.InCallback()) {
                    cpnReceiptVoucherEditingForm.PerformCallback(args);
                }
            }
        },

        Cancel: function () {
            var args = this.actions.tCancel;
            if (!cpnReceiptVoucherEditingForm.InCallback()) {
                cpnReceiptVoucherEditingForm.PerformCallback(args);
            }
        },

        EndCallback: function (args) {
            switch (args.transition) {
                case this.actions.tCancel:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.actions.tCreate:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.actions.tEdit:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.actions.tSave:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eSaved);
                    }
                    break;
                default:
                    break;
            }
        }

    };

    function cpnReceiptVoucherEditingForm_EndCallback(s, e) {
        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            PaymentVoucherEditingForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }
    }

    function ReceiptVoucherEditingForm_btnSave_Click(s, e) {
        PaymentVoucherEditingForm.Save();
    }

    function ReceiptVoucherEditingForm_btnCancel_Click(s, e) {
        popupReceiptVoucherEditingForm.Hide();
    }

    function popupReceiptVoucherEditingForm_Closing(s, e) {
        PaymentVoucherEditingForm.Cancel();
    }

    function txtCode_Validation(s, e) {
        //Check Code is exist in database
        if (!cpntxtCode.InCallback()) {
            cpntxtCode.PerformCallback();
        }
    }

    function cboSourceOrganization_SelectedIndexChanged(s, e) {
        var address = s.GetSelectedItem().GetColumnText('Address');
        txtAddress.SetText(address);
    }

    function ConvertedAmount_Update(s, e) {
        ReCalculate_ConvertedAmount();
        CPNumberString.PerformCallback();
    }

    function ReCalculate_ConvertedAmount() {
        var ExchangeRate = spinExchangeRate.GetNumber();
        var Amount = spinAmount.GetNumber();
        if (ExchangeRate > 0 && Amount > 0) {
            spinConvertedAmount.SetValue(ExchangeRate * Amount);
        }
    }

    function ExchangeRate_Validation(s, e) {
        var _exchangeRate = parseFloat(e.value);
        if (_exchangeRate <= 0) {
            e.isValid = false;
            e.errorText = "Tỉ giá phải lớn hơn 0";
        }
    }

    function Amount_Validation(s, e) {
        var _amount = parseFloat(e.value);
        if (_amount <= 0) {
            e.isValid = false;
            e.errorText = "Số tiền chi phải lớn hơn 0";
        }
    }

    function btnBookingEntry_Click(s, e) {
        var voucherId = hiddenField.Get('voucherId');
        console.log(voucherId);
        VoucherBookingEntriesForm.Show(voucherId);
    }

    function gridlookupCurrency_ValueChanged(s, e) {
        if (!panelVoucherAmount.InCallback()) {
            panelVoucherAmount.PerformCallback('CurrencyChanged');
        }
        if (!CPNumberString.InCallback())
            CPNumberString.PerformCallback();
    }
</script>
<dx:ASPxCallbackPanel ID="cpnReceiptVoucherEditingForm" ClientInstanceName="cpnReceiptVoucherEditingForm"
    runat="server" Width="100%" OnCallback="cpnReceiptVoucherEditingForm_Callback">
    <ClientSideEvents EndCallback="cpnReceiptVoucherEditingForm_EndCallback" />
<ClientSideEvents EndCallback="cpnReceiptVoucherEditingForm_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxHiddenField ID="hiddenField" ClientInstanceName="hiddenField" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxPopupControl ID="popupReceiptVoucherEditingForm" ClientInstanceName="popupReceiptVoucherEditingForm" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" Height="480px"
                Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                RenderMode="Lightweight" ShowMaximizeButton="True" Width="860px" ShowFooter="True">
                <ClientSideEvents Closing="popupReceiptVoucherEditingForm_Closing" />
                <FooterTemplate>
                    <div style="padding: 10px;">
                        <div style="float: left">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnHelp" runat="server" Text="Trợ giúp" AutoPostBack="false">
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
                                <dx:ASPxButton ID="btnBookingEntry" runat="server" Text="Ghi Sổ" AutoPostBack="false">
                                    <ClientSideEvents Click="btnBookingEntry_Click" />
                                    <Image ToolTip="Hạch toán">
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnSave" runat="server" Text="Lưu lại" AutoPostBack="false">
                                    <ClientSideEvents Click="ReceiptVoucherEditingForm_btnSave_Click" />
                                    <Image ToolTip="Lưu lại">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                    <ClientSideEvents Click="ReceiptVoucherEditingForm_btnCancel_Click" />
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
<ClientSideEvents Closing="popupReceiptVoucherEditingForm_Closing"></ClientSideEvents>

                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="dsReceiptVoucher" runat="server" Criteria="[VouchesId] = ?"
                            TypeName="NAS.DAL.Vouches.PaymentVouches" DefaultSorting="">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsVoucherAmount" runat="server" Criteria="[VouchesId!Key] = ?"
                            TypeName="NAS.DAL.Vouches.VouchesAmount">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsForeignCurrency" runat="server" Criteria="[RowStatus] = 1s"
                            TypeName="NAS.DAL.Vouches.ForeignCurrency">
                        </dx:XpoDataSource>
                        <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" RenderMode="Classic"
                            Width="100%">
                            <TabPages>
                                <dx:TabPage Text="Thông tin chung">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="formlayoutReceiptVoucherEditingForm" runat="server" DataSourceID="dsReceiptVoucher"
                                                Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Thông tin chung" ColCount="4">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Số phiếu chi" ColSpan="2" FieldName="Code" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxCallbackPanel ID="cpntxtCode" runat="server" ClientInstanceName="cpntxtCode"
                                                                            ShowLoadingPanel="False" Width="100%">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" OnValidation="txtCode_Validation"
                                                                                        Width="170px">
                                                                                        <ClientSideEvents Validation="txtCode_Validation" />
<ClientSideEvents Validation="txtCode_Validation"></ClientSideEvents>

                                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="ReceiptVoucherEditingForm">
                                                                                            <RegularExpression ErrorText="Mã không hợp lệ" ValidationExpression="<%$ Resources:Resources, validation_regex_code %>" />
                                                                                            <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
<RegularExpression ErrorText="M&#227; kh&#244;ng hợp lệ" ValidationExpression="<%$ Resources:Resources, validation_regex_code %>"></RegularExpression>

<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Ngày chi" ColSpan="2" FieldName="IssuedDate" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxDateEdit ID="txtIssueDate" runat="server">
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="ReceiptVoucherEditingForm">
                                                                                <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>"></RequiredField>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxDateEdit>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Đơn vị nhận tiền" ColSpan="2" FieldName="TargetOrganizationId!Key">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="cboSourceOrganization" runat="server" CallbackPageSize="10"
                                                                            EnableCallbackMode="True" IncrementalFilteringMode="Contains" OnItemRequestedByValue="cboSourceOrganization_ItemRequestedByValue"
                                                                            OnItemsRequestedByFilterCondition="cboSourceOrganization_ItemsRequestedByFilterCondition"
                                                                            TextFormatString="{0} - {1}" ValueField="OrganizationId" ValueType="System.Guid"
                                                                            Width="100%">
                                                                            <ClientSideEvents SelectedIndexChanged="cboSourceOrganization_SelectedIndexChanged" />
<ClientSideEvents SelectedIndexChanged="cboSourceOrganization_SelectedIndexChanged"></ClientSideEvents>
                                                                            <Columns>
                                                                                <dx:ListBoxColumn Caption="Mã đơn vị nhận tiền" FieldName="Code" />
                                                                                <dx:ListBoxColumn Caption="Tên đơn vị nhận tiền" FieldName="Name" />
                                                                                <dx:ListBoxColumn Caption="Địa chỉ" FieldName="Address" />
                                                                            </Columns>
                                                                            <ItemStyle Wrap="True" />
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Người nhận tiền" ColSpan="2" FieldName="Payer">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txtPayer" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Địa chỉ" ColSpan="4" FieldName="Address">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txtAddress" runat="server" ClientInstanceName="txtAddress" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Lý do chi" ColSpan="4" FieldName="Description">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txtDescription" runat="server" Width="100%">
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                                <Paddings PaddingBottom="0px" />

<Paddings PaddingBottom="0px"></Paddings>
                                            </dx:ASPxFormLayout>
                                            <dx:ASPxCallbackPanel ID="panelVoucherAmount" ClientInstanceName="panelVoucherAmount"
                                                runat="server" Width="100%" OnCallback="panelVoucherAmount_Callback" 
                                                ShowLoadingPanel="False">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxFormLayout ID="formlayoutVoucherAmount" runat="server" Width="100%" DataSourceID="dsVoucherAmount">
                                                            <Items>
                                                                <dx:LayoutGroup Caption="Thông tin số tiền chi" ColCount="4">
                                                                    <Items>
                                                                        <dx:LayoutItem Caption="Số tiền" RequiredMarkDisplayMode="Required" FieldName="Credit"
                                                                            HorizontalAlign="Right">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxSpinEdit ID="spinAmount" ClientInstanceName="spinAmount" runat="server" Height="21px"
                                                                                        Number="0" DisplayFormatString="#,###" HorizontalAlign="Right">
<ClientSideEvents NumberChanged="ConvertedAmount_Update" Validation="Amount_Validation"></ClientSideEvents>

                                                                                        <ValidationSettings ValidationGroup="ReceiptVoucherEditingForm" Display="Dynamic"
                                                                                            ErrorDisplayMode="ImageWithTooltip">
                                                                                            <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <ClientSideEvents NumberChanged="ConvertedAmount_Update" Validation="Amount_Validation" />
                                                                                    </dx:ASPxSpinEdit>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Loại tiền tệ" RequiredMarkDisplayMode="Required" FieldName="ForeignCurrencyId!Key">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <uc3:CurrencyGridLookup ID="gridlookupCurrency" runat="server"
                                                                                        ValidationSettings-Display="Dynamic" ValidationSettings-ErrorDisplayMode="ImageWithTooltip"
                                                                                        ValidationSettings-ValidationGroup="ReceiptVoucherEditingForm"
                                                                                        ValidationSettings-RequiredField-IsRequired="true"
                                                                                        ValidationSettings-RequiredField-ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>"
                                                                                        ClientSideEvents-ValueChanged="gridlookupCurrency_ValueChanged">
                                                                                    </uc3:CurrencyGridLookup>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Name="ExchangeRate" Caption="Tỉ giá" RequiredMarkDisplayMode="Required" FieldName="ExchangeRate"
                                                                            HorizontalAlign="Right">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxSpinEdit ID="spinExchangeRate" ClientInstanceName="spinExchangeRate" runat="server"
                                                                                        Height="21px" Number="0" DisplayFormatString="#,###" HorizontalAlign="Right">
<ClientSideEvents NumberChanged="ConvertedAmount_Update" Validation="ExchangeRate_Validation"></ClientSideEvents>

                                                                                        <ValidationSettings ValidationGroup="ReceiptVoucherEditingForm" Display="Dynamic"
                                                                                            ErrorDisplayMode="ImageWithTooltip">
                                                                                            <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                        <ClientSideEvents NumberChanged="ConvertedAmount_Update" Validation="ExchangeRate_Validation" />
                                                                                    </dx:ASPxSpinEdit>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Name="ConvertedAmount" Caption="Số tiền qui đổi" FieldName="VouchesId.SumOfCredit">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxSpinEdit ClientInstanceName="spinConvertedAmount" ID="spinConvertedAmount"
                                                                                        runat="server" Height="21px" Number="0" DisplayFormatString="#,###" ReadOnly="True"
                                                                                        HorizontalAlign="Right">
                                                                                        <SpinButtons ShowIncrementButtons="False">
                                                                                        </SpinButtons>
                                                                                        <ReadOnlyStyle BackColor="#EEEEEE" Cursor="default">
                                                                                        </ReadOnlyStyle>
                                                                                    </dx:ASPxSpinEdit>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption=" " ColSpan="4">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxCallbackPanel ID="CPNumberString" ClientInstanceName="CPNumberString" runat="server" Width="100%" 
                                                                                        OnCallback="CPNumberString_Callback" ShowLoadingPanel="false">
                                                                                       
                                                                                        <PanelCollection>
                                                                                            <dx:PanelContent>
                                                                                                <dx:ASPxLabel EncodeHtml="false" ID="lbl_text_Number" runat="server" Text="">
                                                                                                </dx:ASPxLabel>
                                                                                            </dx:PanelContent>
                                                                                        </PanelCollection>
                                                                                    </dx:ASPxCallbackPanel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                    </Items>
                                                                </dx:LayoutGroup>
                                                            </Items>
                                                            <Paddings PaddingBottom="0px" />

<Paddings PaddingBottom="0px"></Paddings>
                                                        </dx:ASPxFormLayout>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxCallbackPanel>
                                            <dx:ASPxFormLayout ID="formlayoutExtendedInfomation" runat="server" Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Thông tin phân bổ">
                                                        <Items>
                                                            <dx:LayoutItem Caption=" " ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <uc1:GridViewVoucherAllocation ID="gridviewReceiptVoucherAllocation" runat="server" />
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
                                <dx:TabPage Text="Thông tin mở rộng">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc2:VoucherBookingEntriesForm ID="voucherBookingEntriesForm" runat="server" />
