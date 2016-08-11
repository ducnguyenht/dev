<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentVoucherEdit.ascx.cs"
    Inherits="ERPCore.PayReceiving.UserControl.PaymentVoucherEdit" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Src="uSplit_p.ascx" TagName="uSplit_p" TagPrefix="uc2" %>
<%@ Register src="../../Accounting/UserControl/uPaymentVoucherCosting.ascx" tagname="uPaymentVoucherCosting" tagprefix="uc3" %>
<script type="text/javascript">

    $(document).ready(function () {

        //Bind eVoucherAmountRowCountChanged event
        $(PaymentVoucherEditForm).on(
            PaymentVoucherEditForm.events.eVoucherAmountRowCountChanged,
            function (event) {
                PaymentVoucherEditForm.LoadCommandButtons();
            }
        );

        PaymentVoucherEditForm.BindSavedEvent(function () {
            PaymentVoucherEditForm.ReloadCommandButton();
        });

        CostingEditForm.BindClosingEvent(function () {
            grdVouchersAmount.Refresh();
            //Delete Edit Rows
            if (grdVouchersAmount.IsEditing()) {
                grdVouchersAmount.CancelEdit();
            }
            //Focus to popup edit form when costing edit form was closed
            PaymentVoucherEditForm.Focus();
        });

    });

    var PaymentVoucherEditForm = {

        //declare properties
        events: {
            eVoucherAmountRowCountChanged: 'voucherAmountRowCountChanged',
            eShown: 'shown',
            eSaved: 'saved',
            eClosing: 'closing'
        },

        currentMode: '',
        isNewRecordSaved: false,
        voucherAmountRowCount: 0,

        SetVoucherAmountRowCount: function (value) {
            this.voucherAmountRowCount = value;
            $(PaymentVoucherEditForm).triggerHandler(this.events.eVoucherAmountRowCountChanged);
        },

        ReloadCommandButton: function () {
            grdVouchersAmount.PerformCallback('getRowCount');
        },

        LoadCommandButtons: function () {
            if (this.currentMode == 'new') {
                //Set visibility for btnCosting button
                if (this.isNewRecordSaved == true && this.voucherAmountRowCount > 0) {
                    btnCosting.SetVisible(true);
                }
                else {
                    btnCosting.SetVisible(false);
                }
            }
            else {
                //Set visibility for btnCosting button
                if (this.voucherAmountRowCount > 0) {
                    btnCosting.SetVisible(true);
                }
                else {
                    btnCosting.SetVisible(false);
                }
            }
        },


        Show: function (headerText, recordId) {
            if (headerText) {
                popPaymentVoucherEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                this.currentMode = 'edit';
                var args = 'edit';
                args += '|' + recordId;
                popPaymentVoucherEdit.PerformCallback(args);
            }
            else {
                this._recordId = null;
                this.currentMode = 'new';
                var args = 'new';
                popPaymentVoucherEdit.PerformCallback(args);
            }
            PaymentVoucherEditForm.ReloadCommandButton();
            popPaymentVoucherEdit.Show();
            $(PaymentVoucherEditForm).triggerHandler(this.events.eShown);
        },
        Save: function () {
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(frmPaymentVoucher.GetMainElement(), null, true);
            if (validated) {
                var args = 'save';
                if (this._recordId) {
                    args += '|' + this._recordId;
                }
                if (popPaymentVoucherEdit.InCallback()) {
                    console.log('popPaymentVoucherEdit: server too busy');
                }
                else {
                    popPaymentVoucherEdit.PerformCallback(args);
                }

            }
            else {
                //pagMunufacturer.SetActiveTabIndex(0);
            }
        },

        Costing: function () {
            var headerText = 'Hạch Toán Phiếu Chi';
            CostingEditForm.Show(headerText, this._recordId);
        },

        Hide: function () { popPaymentVoucherEdit.Hide(); },
        btnSave_Click: function (s, e) {
            PaymentVoucherEditForm.Save();
        },
        btnCancel_Click: function (s, e) {
            PaymentVoucherEditForm.Hide();
            if (grdVouchersAmount.IsEditing()) {
                grdVouchersAmount.CancelEdit();
            }
        },
        EndCallback: function (s, e) {
            
            //DND 1048
            if (s.cpIsDefaultSourceOrg) {
                delete s.cpIsDefaultSourceOrg;
                cbTargetOrganization.SetValue(null);
            }
            //END DND 1048

            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }

            if (s.cpNewRecordId) {
                PaymentVoucherEditForm._recordId = s.cpNewRecordId;
                delete s.cpNewRecordId;
            }

            if (s.cpCallbackArgs) {
                //popPaymentVoucherEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                //When save NEW data successfully
                if (args.isSuccess == true && PaymentVoucherEditForm.currentMode == 'new') {
                    PaymentVoucherEditForm.isNewRecordSaved = true;
                }
                $(PaymentVoucherEditForm).triggerHandler(PaymentVoucherEditForm.events.eSaved, args);
//                delete s.cpCallbackArgs;
                if (args.isSuccess == true && grdVouchersAmount.GetVisibleRowsOnPage() <= 0) {
                    grdVouchersAmount.Focus();
                    grdVouchersAmount.AddNewRow();
                } else {
                    grdVouchersAmount.Focus();
                }
                delete s.cpCallbackArgs;
            }

        },
        Closing: function (s, e) {
            $(PaymentVoucherEditForm).triggerHandler(PaymentVoucherEditForm.events.eClosing);
        },
        //Bind Saved Event
        BindSavedEvent: function (callback) {
            $(PaymentVoucherEditForm).on(this.events.eSaved, callback);
        },
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(PaymentVoucherEditForm).on(this.events.eShown, callback);
        },
        //Bind Closing Event
        BindClosingEvent: function (callback) {
            $(PaymentVoucherEditForm).on(this.events.eClosing, callback);
        },
        ValidateForm: function (s, e) {
            switch (s.name) {
                case '<%= txtCode.ClientID %>':
                    //Validate max length
                    var CODE_MAX_LENGTH = 36;
                    if (!ValidateMaxLength(e.value, CODE_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Mã phiếu chi không được vượt quá 36 kí tự";
                    }
                    if (e.isValid == true) {
                        //Check Code is exist in database
                        if (cpntxtCode.InCallback()) {
                        }
                        else {
                            cpntxtCode.PerformCallback();
                        }
                    }
                    break;
                default:
                    break;
            }
        },
        Focus: function () {
            var jObject = $(".KeyShortcutpopPaymentVoucherEdit");
            jObject.focus();
        }
    };

    var tempExchangeRate;

    function grdVouchersAmount_colForeignCurrencyId_SelectedIndexChanged(s, e) {
        var editorExchangeRate = grdVouchersAmount.GetEditor('ExchangeRate');
        if (editorExchangeRate) {
            var foreignCurrencyName = s.GetSelectedItem().GetColumnText('Name');
            if (foreignCurrencyName == 'VNĐ') {
                tempExchangeRate = editorExchangeRate.GetValue();
                editorExchangeRate.SetValue(1);
                editorExchangeRate.SetVisible(false);
            }
            else {
                editorExchangeRate.SetValue(tempExchangeRate);
                editorExchangeRate.SetVisible(true);
            }
            ReCalculate_ConvertedAmount();
        }
    }

    function ConvertedAmount_Update(s, e) {
        ReCalculate_ConvertedAmount();
    }

    function ReCalculate_ConvertedAmount() {
        var editorExchangeRate = grdVouchersAmount.GetEditor('ExchangeRate');
        var editorCredit = grdVouchersAmount.GetEditor('Credit');
        var editorConvertedAmount = grdVouchersAmount.GetEditor('ConvertedAmount');
        if (editorCredit) {
            var ExchangeRate = editorExchangeRate.GetNumber();
            var Credit = editorCredit.GetNumber();
            if (ExchangeRate > 0 && Credit > 0) {
                editorConvertedAmount.SetValue(ExchangeRate * Credit);
            }
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

    function grdVouchersAmount_EndCallback(s, e) {
            s.FocusEditor(0);
        if (s.cpVisibleRowCount != undefined) {
            var visibleRowCount = parseInt(s.cpVisibleRowCount);
            PaymentVoucherEditForm.SetVoucherAmountRowCount(visibleRowCount);
            delete s.cpVisibleRowCount;
        }
        if (s.cpEvent) {
            if (s.cpEvent == 'rowCountChanged') {
                PaymentVoucherEditForm.ReloadCommandButton();
            }
            delete s.cpEvent;
        }
    }
    function btnCosting_Click(s, e) {
        PaymentVoucherEditForm.Costing();
    }

    function grdVouchersAmount_Init(s, e) {
        if (s.IsEditing()) {
            grdVouchersAmount.CancelEdit();
        }
        //Using standard shortcut key and standard shortcut behavior
        Utils.AttachStandardShortcutToGridview(s);
    }


    function popPaymentVoucherEdit_Init(s, e) {
        var jObject = $(".KeyShortcutpopPaymentVoucherEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
        if (grdVouchersAmount.IsEditing()) {
                grdVouchersAmount.CancelEdit();
            }
            PaymentVoucherEditForm.Hide();
            
        });
        //Press Ctrl+Enter to save general information
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            PaymentVoucherEditForm.Save();
        });
    }
    function popPaymentVoucherEdit_Shown(s, e) {
        //Focus to popup edit form when shown
        txtCode.Focus();
    }
    function popPaymentVoucherEdit_CloseButtonClick(s, e) {
        if (grdVouchersAmount.IsEditing()) {
            grdVouchersAmount.CancelEdit();
        }
    }

    function cpPayViewer_EndCallback(s, e) {
        if (s.cpShowForm) {
            formPayViewer.Show();            
            delete (s.cpShowForm);
        }
    }

    function btnPrint_Click(s, e) {
        hPayViewer.Set("print", "1");
        cpPayViewer.PerformCallback();
    }

    //DND 851
//    function cbVouchesType_BeginCallBack(s, e) {
//        cbVouchesType.PerformCallback(e.VisibleIndex);
//    }
    function cbTargetOrganization_ValueChanged(s, e) {
        s.SetIsValid(true);
        var args = 'cbo_click|' + s.GetText(s.visibleIndex);
        popPaymentVoucherEdit.PerformCallback(args);
    }
    //END DND 851
</script>
<dx:ASPxPopupControl ID="popPaymentVoucherEdit" CssClass="KeyShortcutpopPaymentVoucherEdit" runat="server" HeaderText="Chi Tiết Phiếu Chi"
    CloseAction="CloseButton" Height="541px" Width="784px" ClientInstanceName="popPaymentVoucherEdit"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowFooter="True"
    Modal="True" ShowMaximizeButton="True" OnWindowCallback="popPaymentVoucherEdit_WindowCallback"
    AllowDragging="True" Maximized="true" AllowResize="True">
    <FooterTemplate>
        <div style="padding: 10px;">
            <div style="float: left">
            </div>
            <div style="float: right">
                <div style="float: left;">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnPrint" ClientInstanceName="btnPrint" runat="server" AutoPostBack="False"
                        Text="In">
                        <ClientSideEvents Click="btnPrint_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Print" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; margin-left: 4px">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnCosting" ClientInstanceName="btnCosting" runat="server" AutoPostBack="False"
                        Text="Hạch toán">
                        <ClientSideEvents Click="btnCosting_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; margin-left: 4px">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnCancel" CausesValidation="false" AutoPostBack="false" runat="server"
                        Text="Thoát">
                        <ClientSideEvents Click="PaymentVoucherEditForm.btnCancel_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div style="float: left; margin-left: 4px">
                    <!-- Places button here -->
                </div>
            </div>
            <div style="clear: both">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:XpoDataSource ID="dsPaymentVoucher" runat="server" TypeName="NAS.DAL.Vouches.PaymentVouches"
                Criteria="[VouchesId] = ? And [RowStatus] &gt; 0">
                <CriteriaParameters>
                    <asp:Parameter  Name="VouchesId" />
                </CriteriaParameters>
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="dsPaymentVouchersType" runat="server" Criteria="[RowStatus] &gt; 0s"
                TypeName="NAS.DAL.Vouches.PaymentVouchesType" >
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="dsReceivedOrg" runat="server" Criteria="[RowStatus] &gt; 0s"
                TypeName="NAS.DAL.Nomenclature.Organization.Organization" DefaultSorting="">
            </dx:XpoDataSource>
            <dx:ASPxFormLayout ID="frmPaymentVoucher" runat="server" ClientInstanceName="frmPaymentVoucher"
                DataSourceID="dsPaymentVoucher" Width="100%">
                <Items>
                    <dx:LayoutGroup Caption="Thông tin chung" ColCount="3">
                        <Items>
                            <dx:LayoutItem Caption="Số phiếu chi" HelpText="Tối đa 36 ký tự liền kề không dấu"
                                RequiredMarkDisplayMode="Required" FieldName="Code" Width="20%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxCallbackPanel ID="cpntxtCode" runat="server" ClientInstanceName="cpntxtCode"
                                            ShowLoadingPanel="False" Width="200px">
                                            <PanelCollection>
                                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtCode" runat="server" OnValidation="txtCode_Validation" 
                                                        Width="170px" ClientInstanceName="txtCode">
                                                        <ClientSideEvents Validation="PaymentVoucherEditForm.ValidateForm"></ClientSideEvents>
                                                        <ValidationSettings ErrorText="" ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField ErrorText="Chưa nhập Số phiếu chi" IsRequired="True" />
                                                            <RegularExpression ErrorText="Số phiếu chi không hợp lệ" ValidationExpression="<%$ Resources:Resources, validation_regex_code %>" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxCallbackPanel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Đơn vị nhận tiền" FieldName="TargetOrganizationId!Key" Width="40%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" >
                                        <dx:ASPxComboBox ID="cbTargetOrganization" runat="server" DataSourceID="dsReceivedOrg"
                                            TextFormatString="{0} - {1}" IncrementalFilteringMode="Contains" 
                                            ValueField="OrganizationId" EnableCallbackMode="true" CallbackPageSize="20" 
                                            Height="19px" Width="100%">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Mã đơn vị nhận" FieldName="Code" />
                                                <dx:ListBoxColumn Caption="Tên đơn vị nhận" FieldName="Name" />
                                                <dx:ListBoxColumn Caption="Địa Chỉ" FieldName="Address"/>
                                            </Columns>
                                            <ClientSideEvents  ValueChanged="cbTargetOrganization_ValueChanged" />
                                            <%--<ValidationSettings ErrorText="" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField ErrorText="Chưa chọn đơn vị nhận tiền" IsRequired="True" />
                                            </ValidationSettings>--%>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Địa chỉ" FieldName="Address" Width="40%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxMemo ID="memoAddress" runat="server" MaxLength="128" Width="100%" Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                             <dx:LayoutItem Caption="Ngày chi" RequiredMarkDisplayMode="Required" FieldName="IssuedDate" Width="20%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="dateIssuedDate" runat="server">
                                            <ValidationSettings ErrorText="" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField ErrorText="Chưa chọn ngày chi" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                             <dx:LayoutItem Caption="Người nhận tiền" FieldName="Payee" Width="40%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="txtPayer" runat="server" Width="100%" >
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Lý do chi" FieldName="Description" Width="40%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxMemo ID="memoDescription" runat="server" MaxLength="255" Width="100%" Rows="3">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Phân loại" RequiredMarkDisplayMode="Required" FieldName="VouchesTypeId!Key"
                                HelpText="Phân loại phiếu chi" VerticalAlign="Top" Width="20%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="cbVouchesType" TextFormatString="{1}" runat="server" DataSourceID="dsPaymentVouchersType"
                                             ValueField="VouchesTypeId" EnableCallbackMode="true" CallbackPageSize="20" IncrementalFilteringMode="Contains"
                                             Width="100%">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Phân loại" FieldName="Name" />
                                                <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                                            </Columns>
                                            <%--<ValidationSettings ErrorText="" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField ErrorText="Chưa chọn phân loại phiếu chi" IsRequired="True" />
                                            </ValidationSettings>
                                            <ClientSideEvents BeginCallback="cbVouchesType_BeginCallBack"  />--%>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption=" " ColSpan="2">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxButton ID="btApply" runat="server" AutoPostBack="False" ClientInstanceName="btApply"
                                            Text="Lưu lại">
                                            <ClientSideEvents Click="PaymentVoucherEditForm.btnSave_Click" />
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:TabbedLayoutGroup Width="100%">
                        <Items>
                            <dx:LayoutItem Caption="Chi tiết phiếu chi">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="grdVouchersAmount" ClientInstanceName="grdVouchersAmount" runat="server"
                                            AutoGenerateColumns="False" DataSourceID="dsVouchersAmount" KeyFieldName="VouchesAmountId"
                                            Width="100%" OnRowInserting="grdVouchersAmount_RowInserting" 
                                            OnCustomUnboundColumnData="grdVouchersAmount_CustomUnboundColumnData"
                                            OnCustomColumnDisplayText="grdVouchersAmount_CustomColumnDisplayText" OnRowDeleted="grdVouchersAmount_RowDeleted"
                                            OnRowInserted="grdVouchersAmount_RowInserted" OnRowUpdated="grdVouchersAmount_RowUpdated"
                                            OnRowUpdating="grdVouchersAmount_RowUpdating" 
                                            OnCustomCallback="grdVouchersAmount_CustomCallback" KeyboardSupport="True" 
                                            OnCellEditorInitialize="grdVouchersAmount_CellEditorInitialize">
                                            <ClientSideEvents EndCallback="grdVouchersAmount_EndCallback" 
                                                Init="grdVouchersAmount_Init" />
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem DisplayFormat="Tổng cộng={0:#,###}" FieldName="ConvertedAmount"
                                                    SummaryType="Sum" />
                                            </TotalSummary>
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" ButtonType="Image" VisibleIndex="4"
                                                    Caption="Thao tác">
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
                                                <dx:GridViewDataSpinEditColumn Caption="Số tiền" FieldName="Credit" ShowInCustomizationForm="True"
                                                    VisibleIndex="1">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,###}" DisplayFormatInEditMode="True">
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa nhập số tiền chi" IsRequired="True" />
                                                        </ValidationSettings>
                                                        <Style HorizontalAlign="Right">
                                                            
                                                        </Style>
                                                        <ClientSideEvents NumberChanged="ConvertedAmount_Update" Validation="Amount_Validation" />
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataSpinEditColumn Caption="Tỉ giá" FieldName="ExchangeRate" ShowInCustomizationForm="True"
                                                    VisibleIndex="2">
                                                    <PropertiesSpinEdit DisplayFormatString="{0:#,###}" DisplayFormatInEditMode="True">
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa nhập tỉ giá" IsRequired="True" />
                                                        </ValidationSettings>
                                                        <Style HorizontalAlign="Right">
                                                            
                                                        </Style>
                                                        <ClientSideEvents NumberChanged="ConvertedAmount_Update" Validation="ExchangeRate_Validation" />
                                                    </PropertiesSpinEdit>
                                                </dx:GridViewDataSpinEditColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="Loại tiền tệ" FieldName="ForeignCurrencyId!Key"
                                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                                    <PropertiesComboBox DataSourceID="dsForeignCurrency" EnableCallbackMode="True" TextFormatString="{0}"
                                                        ValueField="ForeignCurrencyId">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Tên loại tiền tệ" FieldName="Name" />
                                                            <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                                                        </Columns>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa chọn loại tiền tệ" IsRequired="True" />
                                                        </ValidationSettings>
                                                        <ClientSideEvents SelectedIndexChanged="grdVouchersAmount_colForeignCurrencyId_SelectedIndexChanged" />
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewDataTextColumn Caption="Số tiền qui đổi" ShowInCustomizationForm="True"
                                                    VisibleIndex="3" FieldName="ConvertedAmount" UnboundType="Decimal" ReadOnly="True">
                                                    <PropertiesTextEdit DisplayFormatString="{0:#,###}" DisplayFormatInEditMode="True">
                                                        <Style HorizontalAlign="Right">
                                                            
                                                        </Style>
                                                    </PropertiesTextEdit>
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="ForeignCurrencyId.Name" ShowInCustomizationForm="True"
                                                    Visible="False" VisibleIndex="6">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <SettingsBehavior ConfirmDelete="True" />
                                            <SettingsPager PageSize="5">
<PageSizeItemSettings Items="10, 20, 50" Visible="True"></PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="Inline" />
                                            <Settings ShowFooter="True" />
                                            <SettingsPager>
                                                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                                            </SettingsPager>
                                            <Styles>
                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                </Header>
                                                <Footer Font-Bold="True">
                                                </Footer>
                                                <CommandColumn Spacing="4px">
                                                </CommandColumn>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:TabbedLayoutGroup>
                </Items>
            </dx:ASPxFormLayout>
            <dx:XpoDataSource ID="dsVouchersAmount" runat="server" TypeName="NAS.DAL.Vouches.VouchesAmount"
                Criteria="[VouchesId.VouchesId] = ?">
                <CriteriaParameters>
                    <asp:Parameter Name="VouchesId" />
                </CriteriaParameters>
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="dsForeignCurrency" runat="server" Criteria="[RowStatus] &gt; 0s"
                TypeName="NAS.DAL.Vouches.ForeignCurrency">
            </dx:XpoDataSource>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Closing="PaymentVoucherEditForm.Closing" 
        EndCallback="PaymentVoucherEditForm.EndCallback" 
        Init="popPaymentVoucherEdit_Init" Shown="popPaymentVoucherEdit_Shown" 
        CloseButtonClick="popPaymentVoucherEdit_CloseButtonClick" />
</dx:ASPxPopupControl>
<uc3:uPaymentVoucherCosting ID="uPaymentVoucherCosting1" runat="server" />
<uc2:uSplit_p ID="uSplit_p" runat="server" />
<dx:ASPxCallbackPanel ID="cpPayViewer" runat="server" 
    ClientInstanceName="cpPayViewer" 
    Width="100%">
    <ClientSideEvents EndCallback="cpPayViewer_EndCallback" />
    <PanelCollection>
<dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formPayViewer" runat="server" AllowDragging="True" 
        AllowResize="True" ClientInstanceName="formPayViewer" 
        CloseAction="CloseButton" DragElement="Window" HeaderText="" Maximized="True" 
        Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Width="800px">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" 
                    ClientInstanceName="PrintLayoutSplitter" FullscreenMode="True" Height="100%" 
                    Orientation="Vertical" SeparatorVisible="False" Width="100%">
                    <Panes>
                        <dx:SplitterPane MinSize="20px" Name="ToolbarPane" Size="40px">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ReportToolbar ID="tlbPayViewer" runat="server" 
                                        ClientInstanceName="tlbPayViewer" ReportViewerID="rptPayViewer" 
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
                                <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ReportViewer ID="rptPayViewer" runat="server" BorderColor="Black" 
                                        BorderStyle="Solid" BorderWidth="1px" ClientInstanceName="rptPayViewer">
                                        <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    </dx:ReportViewer>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <Styles>
                        <Pane HorizontalAlign="Center">
                            <Paddings Padding="0px" />
                            <Border BorderWidth="0px" />
                        </Pane>
                    </Styles>
                </dx:ASPxSplitter>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
<dx:ASPxHiddenField ID="hPayViewer" runat="server" 
    ClientInstanceName="hPayViewer">
</dx:ASPxHiddenField>
<dx:ASPxGridView ID="grdBooking" runat="server" AutoGenerateColumns="False" 
    ClientInstanceName="grdBooking">
    <Columns>
        <dx:GridViewDataTextColumn FieldName="Dc" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Account" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Amount" VisibleIndex="2">
            <PropertiesTextEdit DisplayFormatString="#,#">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager Mode="ShowAllRecords">
    </SettingsPager>
    <Settings ShowColumnHeaders="False" />
    <Styles>
        <Cell>
            <Border BorderStyle="None" />
        </Cell>
    </Styles>
</dx:ASPxGridView>
<dx:ASPxGridViewExporter ID="gvDataExporter" runat="server" 
    GridViewID="grdBooking">
</dx:ASPxGridViewExporter>

