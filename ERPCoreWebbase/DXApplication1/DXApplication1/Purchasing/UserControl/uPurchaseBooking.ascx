<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uPurchaseBooking.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uPurchaseBooking" %>
<style type = "text/css">
    .float-right
    {
    float:right;
    }
    .float-left
    {
    float:left;
    }
</style>
    <script type="text/javascript">
        function grdBookingDetail_Init(s, e) {
            if (s.GetVisibleRowsOnPage() <= 0) {
                s.AddNewRow();
            } else {
                if (grdBookingDetail.IsEditing()) {
                    grdBookingDetail.CancelEdit();
                }
            }
            Utils.AttachStandardShortcutToGridview(s);
            Utils.AttachShortcutTo(s.GetMainElement(), "Esc", function () {
                if (grdBookingDetail.IsEditing()) {
                    var confirmMessage = confirm("Bạn có chắc chắn muốn thoát không?");
                    if (confirmMessage == true) {                        
                        grdBookingDetail.CancelEdit();
                        s.GetMainElement().focus();                        
                    }
                }
                else {
                    var focusedRowIndex = grdBooking.GetFocusedRowIndex();
                    grdBooking.Focus();
                    grdBooking.CollapseDetailRow(focusedRowIndex);
                }
            });
        }
    </script>
<dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    AppearAfter="200" AllowDragging="True" AllowResize="True" ClientInstanceName="formBooking"
    HeaderText="Hạch toán " ShowMaximizeButton="True" ShowFooter="True" ShowSizeGrip="False"
    Width="850px" Height="600px" ID="formBooking" CloseAction="CloseButton" 
    Modal="True" Maximized="True">
    <ClientSideEvents Shown="formBooking_Show" CloseUp="formBooking_Close" 
        Closing="formBooking_Close" Init="formBooking_Init" 
        Resize="formBooking_Resize" />
    <FooterContentTemplate>
        <dx:ASPxCallbackPanel ID="cpBookingCommand" runat="server" 
            ClientInstanceName="cpBookingCommand" oncallback="cpBookingCommand_Callback" 
            Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" 
                        CssClass="float-left button-left-margin" Text="Trợ Giúp" 
                        UseSubmitBehavior="False" Visible="False" Wrap="False">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="buttonBookingCancel" runat="server" AutoPostBack="False" 
                        ClientInstanceName="buttonBookingCancel" 
                        CssClass="float-right button-right-margin" Text="Thoát" 
                        UseSubmitBehavior="False" Wrap="False">
                        <ClientSideEvents Click="buttonBookingCancel_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" 
                        CssClass="float-right hd button-right-margin" Text="Tiếp theo" 
                        UseSubmitBehavior="False" Visible="False" Wrap="False">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Forward" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="buttonBoookingApprove" runat="server" AutoPostBack="False" 
                        ClientInstanceName="buttonBoookingApprove" 
                        CssClass="float-right button-right-margin" Text="Duyệt" 
                        UseSubmitBehavior="False" Wrap="False">
                        <ClientSideEvents Click="buttonBoookingApprove_Click" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                    </dx:ASPxButton>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxCallbackPanel ID="cpBooking" runat="server" ClientInstanceName="cpBooking"
                Width="100%" OnCallback="cpBooking_Callback">
                <ClientSideEvents EndCallback="cpBooking_EndCallback" />
                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdBooking" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdBooking"
                            DataSourceID="PurchaseInvoiceTransactionXDS" KeyFieldName="TransactionId" OnRowDeleting="grdBooking_RowDeleting"
                            OnRowInserting="grdBooking_RowInserting" OnRowUpdating="grdBooking_RowUpdating"
                            OnRowValidating="grdBooking_RowValidating" Width="100%" 
                            OnCellEditorInitialize="grdBooking_CellEditorInitialize" 
                            OnInit="grdBooking_Init" OnRowInserted="grdBooking_RowInserted" 
                            KeyboardSupport="True" OnInitNewRow="grdBooking_InitNewRow" 
                            OnStartRowEditing="grdBooking_StartRowEditing" 
                            OnHtmlDataCellPrepared="grdBooking_HtmlDataCellPrepared" >
                            <ClientSideEvents Init="grdBooking_Init" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="GeneralJournals.Credit" 
                                    ShowInColumn="Ngày phiếu mua" SummaryType="Sum" 
                                    ShowInGroupFooterColumn="Mã phiếu bán" />
                                <dx:ASPxSummaryItem FieldName="GeneralJournals.Debit" 
                                    ShowInColumn="Mã phiếu mua" SummaryType="Sum" />
                            </TotalSummary>
                            <Columns>
                                <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="3" Width="0px"
                                    FieldName="AccountingPeriodId!Key">
                                    <PropertiesTextEdit EnableFocusedStyle="False">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Ngày bút toán" FieldName="CreateDate" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Width="150px" SortOrder="Descending">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="Code" ShowInCustomizationForm="True" Width="200px"
                                    Caption="Mã bút toán" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Diễn giải" ShowInCustomizationForm="True" VisibleIndex="6"
                                    Width="400px" FieldName="Description">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Ngày phiếu bán" FieldName="IssueDate" ShowInCustomizationForm="True"
                                    VisibleIndex="8" Width="150px" ReadOnly="True">
                                </dx:GridViewDataDateColumn>
<dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" Width="0px" VisibleIndex="0">
    <PropertiesTextEdit EnableFocusedStyle="False">
    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TransactionId" ShowInCustomizationForm="True"
                                    VisibleIndex="1" Width="0px" ReadOnly="True">
                                    <PropertiesTextEdit EnableFocusedStyle="False">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã phiếu bán" ShowInCustomizationForm="True"
                                    VisibleIndex="7" Width="200px" FieldName="PurchaseInvoiceId.Code" 
                                    ReadOnly="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="9" ButtonType="Image" Width="100px">
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
                                    <CancelButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </CancelButton>
                                    <UpdateButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                        </Image>
                                    </UpdateButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="PurchaseInvoiceId!Key" 
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="0px">
                                    <PropertiesTextEdit EnableFocusedStyle="False">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="450" VerticalScrollBarMode="Auto"
                                ShowFooter="True" />
                            <SettingsDetail ShowDetailRow="True" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center">
                                </Header>
                                <CommandColumn Spacing="10px">
                                </CommandColumn>
                            </Styles>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="grdBookingDetail" runat="server" 
                                        AutoGenerateColumns="False" ClientInstanceName="grdBookingDetail" 
                                        DataSourceID="GeneralJournalXDS" KeyFieldName="GeneralJournalId" 
                                        onbeforeperformdataselect="grdBookingDetail_BeforePerformDataSelect" 
                                        OnRowDeleting="grdBookingDetail_RowDeleting" 
                                        OnRowInserting="grdBookingDetail_RowInserting" 
                                        OnRowUpdating="grdBookingDetail_RowUpdating" 
                                        OnRowValidating="grdBookingDetail_RowValidating" Width="100%" 
                                        onrowinserted="grdBookingDetail_RowInserted" 
                                        oncelleditorinitialize="grdBookingDetail_CellEditorInitialize" 
                                        KeyboardSupport="True" oninitnewrow="grdBookingDetail_InitNewRow" 
                                        onstartrowediting="grdBookingDetail_StartRowEditing" 
                                        oncustomcolumndisplaytext="grdBookingDetail_CustomColumnDisplayText">
                                        <ClientSideEvents Init="grdBookingDetail_Init" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem DisplayFormat="Nợ : {0:n}" FieldName="Debit" 
                                                SummaryType="Sum" />
                                            <dx:ASPxSummaryItem DisplayFormat="Có : {0:n}" FieldName="Credit" 
                                                SummaryType="Sum" />
                                        </TotalSummary>
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Tài khoản" FieldName="AccountId!Key" 
                                                VisibleIndex="4" Width="100px">
                                                <PropertiesComboBox TextFormatString="{0} - {1}" ValueField="AccountId" 
                                                    ValueType="System.Guid" EnableCallbackMode="True" CallbackPageSize="10" 
                                                    IncrementalFilteringMode="Contains" TextField="Code">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" Width="150px" />
                                                        <dx:ListBoxColumn Caption="Tài khoản" FieldName="Name" Width="250px" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="GeneralJournalId" ReadOnly="True" 
                                                VisibleIndex="0" Width="0px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nợ " FieldName="Debit" VisibleIndex="6" 
                                                Width="150px" Name="Debit">
                                                <PropertiesTextEdit DisplayFormatString="n">
                                                </PropertiesTextEdit>
                                                <EditItemTemplate>
                                                    <dx:ASPxSpinEdit ID="colBookingDetailDebit" runat="server" 
                                                        ClientInstanceName="colBookingDetailDebit" DisplayFormatString="n" 
                                                        Height="21px" Number="0" oninit="colBookingDetailDebit_Init" Width="100%">
                                                        <ClientSideEvents ValueChanged="colBookingDetailDebit_ValueChanged" />
                                                    </dx:ASPxSpinEdit>
                                                </EditItemTemplate>                                                
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Có" FieldName="Credit" VisibleIndex="7" 
                                                Width="150px" Name="Credit">
                                                <PropertiesTextEdit DisplayFormatString="n">
                                                </PropertiesTextEdit>
                                               <EditItemTemplate>
                                                    <dx:ASPxSpinEdit ID="colBookingDetailCredit" runat="server" 
                                                        ClientInstanceName="colBookingDetailCredit" DisplayFormatString="n" 
                                                        Height="21px" Number="0" oninit="colBookingDetailCredit_Init" Width="100%">
                                                        <ClientSideEvents ValueChanged="colBookingDetailCredit_ValueChanged" />
                                                    </dx:ASPxSpinEdit>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="RowStatus" VisibleIndex="2" Width="0px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
                                                VisibleIndex="5" Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TransactionId!Key" VisibleIndex="1" 
                                                Width="0px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                VisibleIndex="8" Width="100px">
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
                                        </Columns>
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                        <SettingsPager PageSize="50">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings HorizontalScrollBarMode="Auto" ShowFooter="True" 
                                            VerticalScrollableHeight="300" />
                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                            </Header>
                                            <CommandColumn Spacing="10px">
                                            </CommandColumn>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                        </dx:ASPxGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:XpoDataSource runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
    ID="GeneralJournalXDS" 
    Criteria="[TransactionId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter DefaultValue="" Name="newparameter" 
            SessionField="TransactionId" />
    </CriteriaParameters>
</dx:XpoDataSource>


<dx:XpoDataSource ID="PurchaseInvoiceTransactionXDS" runat="server" 
    TypeName="NAS.DAL.Accounting.Journal.PurchaseInvoiceTransaction" 
    Criteria="[PurchaseInvoiceId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="newparameter" SessionField="BillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
