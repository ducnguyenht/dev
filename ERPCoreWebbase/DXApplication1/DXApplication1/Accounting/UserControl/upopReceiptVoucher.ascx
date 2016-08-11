<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="upopReceiptVoucher.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.upopReceiptVoucher" %>
<script type="text/javascript">
    var CostingEditForm = {

        Show: function (headerText, recordId) {
            if (headerText) {
                popCosting.SetHeaderText(headerText);
            }
            console.log("recordId: " + recordId);
            if (recordId) {
                this._recordId = recordId;
                var args = 'edit';
                args += '|' + recordId;
                popCosting.PerformCallback(args);
            }
            else {
                alert('Original artifact is not valid');
                return;
            }
            popCosting.Show();
            $(CostingEditForm).triggerHandler('shown');
        },

        ApproveCosting: function () {
            ldpnCostingEditForm.Show();
            popCosting.PerformCallback('approveCosting');
        },

        Hide: function () { popCosting.Hide(); },

        btnCancel_Click: function (s, e) {
            CostingEditForm.Hide();
            //Cancel Edit Row
            if (grdTransaction.IsEditing()) {
                grdTransaction.CancelEdit();
            } else if (grdGeneralJournal.IsEditing()) {
                grdGeneralJournal.CancelEdit();
            }
        },

        btnApprove_Click: function (s, e) {
            CostingEditForm.ApproveCosting();
        },

        Closing: function (s, e) {
            $(CostingEditForm).triggerHandler('closing');
        },

        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(CostingEditForm).on('shown', callback);
        },

        //Bind Closing Event
        BindClosingEvent: function (callback) {
            $(CostingEditForm).on('closing', callback);
        },

        Focus: function () {
            var jObject = $(".KeyShortcutCostingEditForm");
            jObject.focus();
        }

    };

    function popCosting_Init(s, e) {
        var jObject = $(".KeyShortcutCostingEditForm");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            CostingEditForm.Hide();
            //Cancel Edit Row
            if (grdTransaction.IsEditing()) {
                grdTransaction.CancelEdit();
            } else if (grdGeneralJournal.IsEditing()) {
                grdGeneralJournal.CancelEdit();
            }
        });
    }

    function popCosting_Shown(s, e) {
        CostingEditForm.Focus(); 
        if (s.GetVisibleRowsOnPage() <= 0) {
            s.AddNewRow();
        }
    }

    function popCosting_EndCallback(s, e) {
        if (s.cpEvent == 'approveComplete') {
            ldpnCostingEditForm.Hide();
            delete s.cpEvent;
        }

        if (s.cpException != undefined) {
            alert(s.cpException);
            delete s.cpException;
        }

        

        if (s.cpIsApprovedCosting != undefined) {
            btnApproveCosting.SetVisible(!s.cpIsApprovedCosting);
            try {
                if (grdGeneralJournal != undefined) {
                    grdGeneralJournal.PerformCallback();
                }
            }
            catch (err) {
                console.log(err);
            }           
            delete s.cpIsApprovedCosting;
        }
    }

    function popCosting_CallbackError(s, e) {
        ldpnCostingEditForm.Hide();
    }

    function grdTransaction_Init(s, e) {
        if (s.GetVisibleRowsOnPage() <= 0) {
            s.AddNewRow();
        } else {
            if (grdTransaction.IsEditing()) {
                grdTransaction.CancelEdit();
            }
        }
        //Using standard shortcut key and standard shortcut behavior
        Utils.AttachStandardShortcutToGridview(s);
    }

    function grdGeneralJournal_Init(s, e) {
        if (s.GetVisibleRowsOnPage() <= 1) {
            s.AddNewRow();
        }
        s.Focus();
        //Using standard shortcut key and standard shortcut behavior
        Utils.AttachStandardShortcutToGridview(s);
    }

    function popCosting_CloseButtonClick(s, e) {
        //Cancel Edit Row
        try {
            if (grdTransaction.IsEditing()) {
                grdTransaction.CancelEdit();
            }
            else if (grdGeneralJournal.IsEditing()) {
                grdGeneralJournal.CancelEdit();
            }
        }
        catch (err) {
        }
    }
</script>
<dx:ASPxLoadingPanel ID="ldpnCostingEditForm" runat="server" ClientInstanceName="ldpnCostingEditForm"
    HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
</dx:ASPxLoadingPanel>
<dx:ASPxPopupControl ID="popCosting" runat="server" AllowDragging="True" AllowResize="True"
    AppearAfter="200" CssClass="KeyShortcutCostingEditForm" ClientInstanceName="popCosting"
    HeaderText="Hạch toán" Height="600px" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" Width="850px" ShowFooter="True" ShowMaximizeButton="True"
    OnWindowCallback="popCosting_WindowCallback" CloseAction="CloseButton" 
    Modal="True" Maximized="True">
    <ClientSideEvents 
        Closing="CostingEditForm.Closing" 
        Init="popCosting_Init" 
        Shown="popCosting_Shown"
        EndCallback="popCosting_EndCallback"
        CallbackError="popCosting_CallbackError" 
        CloseButtonClick="popCosting_CloseButtonClick" />
    <FooterTemplate>
        <div style="padding: 10px;">
            <div style="float: left">
                <div style="float: left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" Text="Trợ Giúp"
                        Wrap="False">
                        <Image>
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
                    <dx:ASPxButton ID="btnApproveCosting" AutoPostBack="false" runat="server" Text="Duyệt"
                        Wrap="False" ClientInstanceName="btnApproveCosting" OnLoad="btnApproveCosting_Load">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                        <ClientSideEvents Click="CostingEditForm.btnApprove_Click" />
                    </dx:ASPxButton>
                </div>
                <div style="float: left; margin-left: 4px">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnCancel" AutoPostBack="false" runat="server" Text="Thoát" Wrap="False">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                        <ClientSideEvents Click="CostingEditForm.btnCancel_Click" />
                    </dx:ASPxButton>
                </div>
            </div>
            <div style="clear: both">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="frmCosting" runat="server" DataSourceID="dsOriginArtifact"
                Width="100%">
                <Items>
                    <dx:LayoutGroup Caption="Thông tin chứng từ" ColCount="2">
                        <Items>
                            <dx:LayoutItem Caption="Mã phiếu thu" FieldName="Code" Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblCode" runat="server" Font-Bold="True">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Phân loại" FieldName="VouchesTypeId.Description" 
                                Width="50%">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblVoucherType" runat="server" Font-Bold="False">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ngày thu" FieldName="IssuedDate">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblIssuesDate" runat="server" Text="">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Đơn vị trả" FieldName="SourceOrganizationId.Name">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblOrganization" runat="server" Text="">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Lý do thu" FieldName="Description">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblDescription" runat="server" Text="">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Địa chỉ" FieldName="Address">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblAddress" runat="server" Text="">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Số tiền quy đổi">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblSumOfDebit" runat="server" Font-Bold="True">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Người trả tiền" FieldName="Payer">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblPayer" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Trạng thái">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="lblIsApprovedCosting" Font-Bold="True" runat="server">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:TabbedLayoutGroup Width="100%">
                        <Items>
                            <dx:LayoutItem Caption="Thông tin định khoản">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView KeyboardSupport="true" ID="grdTransaction" 
                                            ClientInstanceName="grdTransaction" runat="server"
                                            AutoGenerateColumns="False" DataSourceID="dsTransaction" KeyFieldName="TransactionId"
                                            Width="100%" OnRowInserting="grdTransaction_RowInserting" OnRowUpdating="grdTransaction_RowUpdating"
                                            OnRowValidating="grdTransaction_RowValidating" 
                                            OnCellEditorInitialize="grdTransaction_CellEditorInitialize" 
                                            OnRowUpdated="grdTransaction_RowUpdated" 
                                            OnRowInserted="grdTransaction_RowInserted" 
                                            OnInitNewRow="grdTransaction_InitNewRow">
                                            <ClientSideEvents Init="grdTransaction_Init" />
                                            <Columns>
                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                    VisibleIndex="4" Name="CommonOperations">
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
                                                    <ClearFilterButton>
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
                                                <dx:GridViewDataTextColumn Caption="Mã bút toán" Width="170px" FieldName="Code" ShowInCustomizationForm="True"
                                                    VisibleIndex="0">
                                                    <PropertiesTextEdit MaxLength="36">
                                                        <ValidationSettings ErrorText="" ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField ErrorText="Chưa nhập mã bút toán" IsRequired="True" />
                                                            <RegularExpression ErrorText="Mã không đúng định dạng" ValidationExpression="<%$ Resources:Resources, validation_regex_code %>" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <HeaderCaptionTemplate>
                                                        <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" SpriteCssClass="Sprite_Caption"
                                                            ToolTip="Tối đa 36 kí tự liền kề không dấu">
                                                        </dx:ASPxImage>
                                                        <dx:ASPxLabel ID="ASPxLabel1" Font-Bold="true" runat="server" Text="Mã bút toán">
                                                        </dx:ASPxLabel>
                                                    </HeaderCaptionTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                                    VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" ShowInCustomizationForm="True"
                                                    VisibleIndex="1" Width="170px">
                                                    <PropertiesDateEdit>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa chọn ngày" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesDateEdit>
                                                </dx:GridViewDataDateColumn>
                                            </Columns>
                                            <SettingsEditing Mode="Inline" />
                                            <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                            <Styles>
                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                </Header>
                                                <CommandColumn Spacing="4px">
                                                </CommandColumn>
                                            </Styles>
                                            <Templates>
                                                <DetailRow>
                                                    <dx:ASPxGridView KeyboardSupport="True" ID="grdGeneralJournal" 
                                                        ClientInstanceName="grdGeneralJournal" runat="server"
                                                        AutoGenerateColumns="False" DataSourceID="dsGeneralJournal" KeyFieldName="GeneralJournalId"
                                                        Width="100%" OnBeforePerformDataSelect="grdGeneralJournal_BeforePerformDataSelect"
                                                        OnRowInserting="grdGeneralJournal_RowInserting" OnRowValidating="grdGeneralJournal_RowValidating"
                                                        OnLoad="grdGeneralJournal_Load" 
                                                        OnRowUpdating="grdGeneralJournal_RowUpdating" 
                                                        oncelleditorinitialize="grdGeneralJournal_CellEditorInitialize" 
                                                        oncustomcolumndisplaytext="grdGeneralJournal_CustomColumnDisplayText">
                                                        <ClientSideEvents Init="grdGeneralJournal_Init" />
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem DisplayFormat="Tổng nợ={0:#,###}" FieldName="Debit" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem DisplayFormat="Tổng có={0:#,###}" FieldName="Credit" SummaryType="Sum" />
                                                        </TotalSummary>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn Name="CommonOperations" ButtonType="Image" 
                                                                Caption="Thao tác" VisibleIndex="4">
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
                                                                <CancelButton Visible="True">
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                    </Image>
                                                                </CancelButton>
                                                                <UpdateButton Visible="True">
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                    </Image>
                                                                </UpdateButton>
                                                                <ClearFilterButton Visible="True">
                                                                    <Image ToolTip="Hủy">
                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                    </Image>
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Tài khoản" FieldName="AccountId!Key" VisibleIndex="1">
                                                                <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                    LoadDropDownOnDemand="True" CallbackPageSize="10" ValueField="AccountId" ValueType="System.Guid" TextFormatString="{0} - {1}">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" />
                                                                        <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" />
                                                                        <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                                                                    </Columns>
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Chưa chọn tài khoản" />
                                                                    </ValidationSettings>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Có" FieldName="Credit" VisibleIndex="3">
                                                                <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:#,###}"
                                                                    NumberFormat="Custom">
                                                                    <Style HorizontalAlign="Right">
                                                                        
                                                                    </Style>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Nợ" FieldName="Debit" VisibleIndex="2">
                                                                <PropertiesSpinEdit DisplayFormatInEditMode="True" DisplayFormatString="{0:#,###}"
                                                                    NumberFormat="Custom">
                                                                    <Style HorizontalAlign="Right">
                                                                        
                                                                    </Style>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description"
                                                                VisibleIndex="0">
                                                                <PropertiesTextEdit ClientInstanceName="Description">
                                                                </PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="Inline" />
                                                        <Settings ShowFooter="True" />
                                                        <Styles>
                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                            </Header>
                                                            <Footer Font-Bold="True">
                                                            </Footer>
                                                            <CommandColumn Spacing="4px">
                                                            </CommandColumn>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                </DetailRow>
                                            </Templates>
                                        </dx:ASPxGridView>
                                        <dx:XpoDataSource ID="dsTransaction" runat="server" TypeName="NAS.DAL.Accounting.Journal.ReceiptVouchesTransaction"
                                            Criteria="[ReceiptVouchesId.VouchesId] = ?" DefaultSorting="">
                                            <CriteriaParameters>
                                                <asp:Parameter Name="VoucherId" />
                                            </CriteriaParameters>
                                        </dx:XpoDataSource>
                                        <dx:XpoDataSource ID="dsGeneralJournal" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
                                            Criteria="[TransactionId.TransactionId] = ?" DefaultSorting="">
                                            <CriteriaParameters>
                                                <asp:Parameter Name="TransactionId" />
                                            </CriteriaParameters>
                                        </dx:XpoDataSource>
                                        <dx:XpoDataSource ID="dsAccount" runat="server" Criteria="[RowStatus] &gt; 0s" 
                                            TypeName="NAS.DAL.Accounting.AccountChart.Account" DefaultSorting="">
                                        </dx:XpoDataSource>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:TabbedLayoutGroup>
                </Items>
            </dx:ASPxFormLayout>
            <dx:XpoDataSource ID="dsOriginArtifact" runat="server" TypeName="NAS.DAL.Vouches.ReceiptVouches"
                Criteria="[VouchesId] = ?">
                <CriteriaParameters>
                    <asp:Parameter Name="VoucherId" />
                </CriteriaParameters>
            </dx:XpoDataSource>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
