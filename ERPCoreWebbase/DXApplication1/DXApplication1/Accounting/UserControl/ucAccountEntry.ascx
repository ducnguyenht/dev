<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAccountEntry.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.ucAccountEntry" %>
<script type="text/javascript">
    function FirstCreate() {
        popupBE.Show();
    }

    function Approve() {
        var credit = (double)($(".dxgv:contains('Tổng Có :')").html().split(" : ")[1]);
        var debit = (double)($(".dxgv:contains('Tổng Nợ :')").html().split(" : ")[1]);
    }
    function BookingGrid_Init(s, e) {
        Utils.AttachStandardShortcutToGridview(s);
        s.GetMainElement().focus();
    }
    function grd_Journal_Init(s, e) {
        CP1.PerformCallback();
        Utils.AttachStandardShortcutToGridview(s);
        s.GetMainElement().focus();
        Utils.AttachShortcutTo(s.GetMainElement(), "Esc", function () {
            if (grd_Journal.IsEditing()) {
                var confirmMessage = confirm("Bạn có chắc chắn muốn thoát không?");
                if (confirmMessage == true) {
                    grd_Journal.CancelEdit();
                    //                    BookingGrid.GetMainElement().focus();
                    //                    BookingGrid.CollapseDetailRow(BookingGrid.focusedRowIndex);
                }
            }
            else {
                BookingGrid.Focus();
                BookingGrid.CollapseDetailRow(BookingGrid.focusedRowIndex);
            }
        });
    }    
</script>
<style type="text/css">
    #ASPxGridView1_EmptyRow_btn_new_first_0
    {
        margin: auto;
    }
    .dxpc-footerContent
    {
        width: 97%;
    }
    .float-right
    {
        margin: 5px 0px 0px 5px;
    }
</style>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="BookingEntryXPO"
    ClientInstanceName="BookingGrid" KeyFieldName="BookingEntryId" 
    Width="100%" OnRowInserted="ASPxGridView1_RowInserted"
    OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleted="ASPxGridView1_RowDeleted"
    OnRowDeleting="ASPxGridView1_RowDeleting" KeyboardSupport="True" 
    onrowvalidating="ASPxGridView1_RowValidating">
    <ClientSideEvents Init="BookingGrid_Init"></ClientSideEvents>
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã Bút Toán" FieldName="Code" VisibleIndex="1">
            <PropertiesTextEdit>
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" VisibleIndex="0"
            SortIndex="0" SortOrder="Descending">
            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                <ValidationSettings>
                    <RequiredField IsRequired="True" />
                </ValidationSettings>
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="5">
            <EditButton Visible="True">
                <Image>
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
    </Columns>
    <SettingsBehavior ColumnResizeMode="Control" />
    <SettingsEditing Mode="Inline" />
    <SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="True" ConfirmDelete="True">
    </SettingsBehavior>
    <SettingsEditing Mode="Inline"></SettingsEditing>
    <Settings ShowFilterRow="True"></Settings>
    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
    <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa không?" />
    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="grd_Journal" runat="server" AutoGenerateColumns="False" DataSourceID="GeneralJournalXPO"
                KeyFieldName="GeneralJournalId" Width="100%" OnBeforePerformDataSelect="ASPxGridView3_BeforePerformDataSelect"
                ClientInstanceName="grd_Journal" OnRowInserting="ASPxGridView3_RowInserting"
                OnRowInserted="ASPxGridView3_RowInserted" OnRowValidating="ASPxGridView3_RowValidating"
                KeyboardSupport="True" OnInitNewRow="grd_Journal_InitNewRow" OnRowDeleting="grd_Journal_RowDeleting"
                OnStartRowEditing="grd_Journal_StartRowEditing" 
                oncelleditorinitialize="grd_Journal_CellEditorInitialize" 
                onhtmldatacellprepared="grd_Journal_HtmlDataCellPrepared">
                <Settings ShowFooter="true" />
                <ClientSideEvents Init="grd_Journal_Init" />
                <TotalSummary>
                    <dx:ASPxSummaryItem FieldName="Credit" SummaryType="Sum" ShowInColumn="Có" Tag="Có"
                        DisplayFormat="Tổng Có : {0}" />
                    <dx:ASPxSummaryItem FieldName="Debit" SummaryType="Sum" ShowInColumn="Nợ" Tag="Nợ"
                        DisplayFormat="Tổng Nợ : {0}" />
                </TotalSummary>
                <Columns>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="5">
                        <EditButton Visible="True">
                            <Image>
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
                    <dx:GridViewDataTextColumn Caption="Có" FieldName="Credit" VisibleIndex="4">
                        <PropertiesTextEdit NullDisplayText="0" NullText="0" DisplayFormatString="0.0">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Số TK" VisibleIndex="2" FieldName="AccountId!Key">
                        <PropertiesComboBox TextField="Code" TextFormatString="{0} - {1}"
                            ValueField="AccountId" ValueType="System.Guid" EnableCallbackMode="True" 
                            IncrementalFilteringMode="Contains">
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã" FieldName="Code"></dx:ListBoxColumn>
                                <dx:ListBoxColumn Caption="Tên TK" FieldName="Name"></dx:ListBoxColumn>
                            </Columns>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn Caption="Nợ" FieldName="Debit" VisibleIndex="3" SortIndex="0"
                        SortOrder="Descending">
                        <PropertiesTextEdit NullDisplayText="0" NullText="0" DisplayFormatString="0.0">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="Control" />
                <SettingsEditing Mode="Inline" />
            </dx:ASPxGridView>
            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="CP1" runat="server"
                Width="100%" OnCallback="ASPxCallbackPanel1_Callback">
                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxButton ID="bt_Approve" runat="server" CssClass="float-right" OnClick="ASPxButton1_Click"
                            Text="Duyệt" Visible="False">
                            <Image>
                                <SpriteProperties CheckedCssClass="Sprite_Appove" />
                            </Image>
                        </dx:ASPxButton>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>
<dx:XpoDataSource ID="AccountingPeriodXPO" runat="server" Criteria="[RowStatus] &gt; 0"
    TypeName="NAS.DAL.Accounting.Journal.AccountingPeriod">
</dx:XpoDataSource>
<dx:XpoDataSource ID="BookingEntryXPO" runat="server" TypeName="NAS.DAL.Accounting.Entry.BookingEntry">
</dx:XpoDataSource>
<dx:XpoDataSource ID="GeneralJournalXPO" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
    Criteria="[TransactionId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="TransactionId" SessionField="TransactionId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="AccountXPO" runat="server" Criteria="[RowStatus] &gt; 0" TypeName="NAS.DAL.Accounting.AccountChart.Account">
</dx:XpoDataSource>
