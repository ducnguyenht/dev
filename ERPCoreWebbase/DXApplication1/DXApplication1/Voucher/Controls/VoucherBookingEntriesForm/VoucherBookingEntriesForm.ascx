<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VoucherBookingEntriesForm.ascx.cs"
    Inherits="WebModule.Voucher.Controls.VoucherBookingEntriesForm.VoucherBookingEntriesForm" %>
<%@ Register Src="../../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<script type="text/javascript">

    var VoucherBookingEntriesForm = {

        //declare properties
        events: {
            eShown: 'Shown',
            eBookedEntries: 'BookedEntries',
            eClosing: 'Closing'
        },

        actions: {
            tEdit: 'Edit',
            tBookEntries: 'Book',
            tCancel: 'Cancel'
        },

        Show: function (recordId) {
            if (!cpnVoucherBookingEntriesForm.InCallback()) {
                var args = '';
                if (recordId) {
                    args = this.actions.tEdit + '|' + recordId;
                }
                cpnVoucherBookingEntriesForm.PerformCallback(args);
            }
        },

        Cancel: function () {
            var args = this.actions.tCancel;
            if (!cpnVoucherBookingEntriesForm.InCallback()) {
                cpnVoucherBookingEntriesForm.PerformCallback(args);
            }
        },

        BookEntries: function () {
            var args = this.actions.tBookEntries;
            if (!cpnVoucherBookingEntriesForm.InCallback()) {
                cpnVoucherBookingEntriesForm.PerformCallback(args);
            }
        },

        EndCallback: function (args) {
            switch (args.transition) {
                case this.actions.tCancel:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.actions.tBookEntries:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eBookedEntries);
                    }
                    break;
                case this.actions.tEdit:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                default:
                    break;
            }
        }

    };


    function gridviewVoucherBookingEntries_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'AllocateTransaction':
                if (!cpnTransactionAllocationObjects.InCallback()) {
                    popupTransactionAllocationObjects.Show();
                    var args = 'AllocateTransaction|' + e.visibleIndex;
                    cpnTransactionAllocationObjects.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }

    function gridviewGeneralJournal_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'AllocateGeneralJournal':
                if (!cpnTransactionAllocationObjects.InCallback()) {
                    popupTransactionAllocationObjects.Show();
                    var args = 'AllocateGeneralJournal|' + s.GetRowKey(e.visibleIndex);
                    cpnTransactionAllocationObjects.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }

    function gridviewGeneralJournal_EndCallback(s, e) {
        console.log('gridviewGeneralJournal_EndCallback');
        if (s.cpEvent == 'GeneralJournalChanged') {
            console.log('GeneralJournalChanged');
            if (!cpnVoucherBookingEntriesForm.InCallback()) {
                cpnVoucherBookingEntriesForm.PerformCallback('ForceRefresh');
                console.log('ForceRefresh');
            }
            delete s.cpEvent;
        }
    }

    function popupVoucherBookingEntriesForm_btnBookEntries_Click(s, e) {
        VoucherBookingEntriesForm.BookEntries();
    }

    function popupVoucherBookingEntriesForm_btnCancel_Click(s, e) {
        VoucherBookingEntriesForm.Cancel();
    }

    function cpnVoucherBookingEntriesForm_EndCallback(s, e) {
        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            VoucherBookingEntriesForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }
    }
</script>
<dx:ASPxCallbackPanel ID="cpnVoucherBookingEntriesForm" runat="server" ClientInstanceName="cpnVoucherBookingEntriesForm"
    Width="100%" OnCallback="cpnVoucherBookingEntriesForm_Callback">
    <ClientSideEvents EndCallback="cpnVoucherBookingEntriesForm_EndCallback" />
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupVoucherBookingEntriesForm" runat="server" RenderMode="Lightweight"
                AllowDragging="True" AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton"
                Height="480px" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowMaximizeButton="True" Width="860px" ShowFooter="True">
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
                                <dx:ASPxButton ID="btnBookEntries" runat="server" Text="Ghi sổ" AutoPostBack="false">
                                    <ClientSideEvents Click="popupVoucherBookingEntriesForm_btnBookEntries_Click" />
                                    <Image ToolTip="Ghi sổ">
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                    <ClientSideEvents Click="popupVoucherBookingEntriesForm_btnCancel_Click" />
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
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="gridviewVoucherBookingEntries" runat="server" AutoGenerateColumns="False"
                            DataSourceID="dsVoucherTransaction" KeyFieldName="TransactionId" Width="100%"
                            OnDataBinding="gridviewVoucherBookingEntries_DataBinding" 
                            ClientInstanceName="gridviewVoucherBookingEntries"
                            OnCustomButtonInitialize="gridviewVoucherBookingEntries_CustomButtonInitialize" 
                            OnCustomColumnDisplayText="gridviewVoucherBookingEntries_CustomColumnDisplayText">
                            <ClientSideEvents CustomButtonClick="gridviewVoucherBookingEntries_CustomButtonClick" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã bút toán" FieldName="Code" VisibleIndex="0"
                                    Width="120px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Số tiền" FieldName="Amount" VisibleIndex="3"
                                    Width="120px">
                                    <PropertiesSpinEdit DisplayFormatString="#,###" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" VisibleIndex="2"
                                    Width="120px">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="Mục đích" FieldName="Description" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Phân bổ" VisibleIndex="4"
                                    Width="100px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton Text="Allocate" ID="AllocateTransaction">
                                            <Image ToolTip="Phân bổ">
                                                <SpriteProperties CssClass="Sprite_Allocation" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Name="DynamicObjectList" Caption="Đối tượng phân bổ" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsEditing Mode="Inline" />
                            <SettingsDetail ShowDetailRow="True" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
                                </Header>
                                <CommandColumn Spacing="4px">
                                </CommandColumn>
                                <Cell Wrap="True"></Cell>
                            </Styles>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="gridviewGeneralJournal" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="dsGeneralJournal" KeyFieldName="GeneralJournalId" OnBeforePerformDataSelect="gridviewGeneralJournal_BeforePerformDataSelect"
                                        Width="100%" OnRowDeleting="gridviewGeneralJournal_RowDeleting" OnRowInserting="gridviewGeneralJournal_RowInserting"
                                        OnRowUpdating="gridviewGeneralJournal_RowUpdating" OnCellEditorInitialize="gridviewGeneralJournal_CellEditorInitialize"
                                        OnCustomColumnDisplayText="gridviewGeneralJournal_CustomColumnDisplayText" 
                                        oncommandbuttoninitialize="gridviewGeneralJournal_CommandButtonInitialize" 
                                        oncustombuttoninitialize="gridviewGeneralJournal_CustomButtonInitialize">
                                        <ClientSideEvents CustomButtonClick="gridviewGeneralJournal_CustomButtonClick" EndCallback="gridviewGeneralJournal_EndCallback" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem DisplayFormat="Tổng nợ={0:#,###}" FieldName="Debit" SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="Tổng có={0:#,###}" FieldName="Credit" SummaryType="Sum" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Tài khoản" FieldName="AccountId!Key" VisibleIndex="1"
                                                Width="170px">
                                                <PropertiesComboBox EnableCallbackMode="true" CallbackPageSize="10" TextField="Code"
                                                    ValueField="AccountId" ValueType="System.Guid" IncrementalFilteringMode="Contains"
                                                    LoadDropDownOnDemand="True" TextFormatString="{0} - {1}">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" />
                                                        <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                    </ValidationSettings>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Có" FieldName="Credit" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="120px">
                                                <PropertiesTextEdit DisplayFormatString="#,###">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nợ" FieldName="Debit" ShowInCustomizationForm="True"
                                                VisibleIndex="2" Width="120px">
                                                <PropertiesTextEdit DisplayFormatString="#,###">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                                VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Phân bổ" ShowInCustomizationForm="True"
                                                VisibleIndex="4" Width="100px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="AllocateGeneralJournal" Text="Allocate">
                                                        <Image ToolTip="Phân bổ">
                                                            <SpriteProperties CssClass="Sprite_Allocation" />
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Name="DynamicObjectList" Caption="Đối tượng phân bổ" VisibleIndex="5">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="6"
                                                Width="100px">
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
                                                <UpdateButton Visible="True">
                                                    <Image ToolTip="Cập nhật">
                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                    </Image>
                                                </UpdateButton>
                                                <CancelButton Visible="True">
                                                    <Image ToolTip="Bỏ qua">
                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                    </Image>
                                                </CancelButton>
                                            </dx:GridViewCommandColumn>
                                        </Columns>
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <Settings ShowFooter="True" />
                                        <SettingsEditing Mode="Inline" />
                                        <Styles>
                                            <Header Font-Bold="True" Wrap="True" HorizontalAlign="Center">
                                            </Header>
                                            <Footer Font-Bold="True">
                                            </Footer>
                                            <CommandColumn Spacing="4px">
                                            </CommandColumn>
                                            <Cell Wrap="True"></Cell>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                        </dx:ASPxGridView>
                        <dx:XpoDataSource ID="dsVoucherTransaction" runat="server" DefaultSorting="" TypeName="">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsGeneralJournal" runat="server" Criteria="[TransactionId!Key] = ?"
                            TypeName="NAS.DAL.Accounting.Journal.GeneralJournal">
                        </dx:XpoDataSource>
                        <br />
                        <dx:ASPxCallbackPanel ID="cpnTransactionAllocationObjects" ClientInstanceName="cpnTransactionAllocationObjects"
                            runat="server" Width="100%" OnCallback="cpnTransactionAllocationObjects_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxPopupControl ID="popupTransactionAllocationObjects" runat="server" AllowDragging="True"
                                        AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="popupTransactionAllocationObjects"
                                        CloseAction="CloseButton" HeaderText="Thông tin đối tượng phân bổ" Height="480px"
                                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        RenderMode="Lightweight" ShowMaximizeButton="True" Width="600px">
                                        <ClientSideEvents Closing="function(s, e) { gridviewVoucherBookingEntries.Refresh(); }" />
                                        <ModalBackgroundStyle BackColor="Transparent">
                                        </ModalBackgroundStyle>
                                        <ContentCollection>
                                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <uc1:NASCustomFieldDataGridView ID="transactionCustomFieldDataGridView" runat="server" />
                                            </dx:PopupControlContentControl>
                                        </ContentCollection>
                                    </dx:ASPxPopupControl>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
