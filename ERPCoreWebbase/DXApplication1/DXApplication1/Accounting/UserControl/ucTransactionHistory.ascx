<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucTransactionHistory.ascx.cs" Inherits="WebModule.Accounting.UserControl.ucTransactionHistory" %>

<dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
    Width="100%" KeyFieldName = "ID"
    ondetailrowexpandedchanged="ASPxGridView2_DetailRowExpandedChanged">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Ngày" FieldName="CreatDate" 
            VisibleIndex="1" SortIndex="0" SortOrder="Descending">
            <PropertiesTextEdit Width="100%">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mã Bút Toán" FieldName="Code" 
            VisibleIndex="2">
            <PropertiesTextEdit Width="100%">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" 
            VisibleIndex="3">
            <PropertiesTextEdit Width="100%">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tổng tiền" FieldName="Total" 
            VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
    </Columns>
    <Settings ShowFilterRow="True" />
    <SettingsDetail ShowDetailRow="True" />
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="grd_Journal" runat="server" AutoGenerateColumns="False" 
                DataSourceID="GeneralJournalXPO" KeyFieldName="GeneralJournalId" 
                OnBeforePerformDataSelect="ASPxGridView2_BeforePerformDataSelect" Width="100%">
                <Settings ShowFooter="true" />
                <TotalSummary>
                    <dx:ASPxSummaryItem DisplayFormat="Tổng Có : {0}" FieldName="Credit" 
                        ShowInColumn="Có" SummaryType="Sum" Tag="Có" />
                    <dx:ASPxSummaryItem DisplayFormat="Tổng Nợ : {0}" FieldName="Debit" 
                        ShowInColumn="Nợ" SummaryType="Sum" Tag="Nợ" />
                </TotalSummary>
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Có" FieldName="Credit" VisibleIndex="4">
                        <PropertiesTextEdit DisplayFormatString="0.0" NullDisplayText="0" NullText="0">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Số TK" FieldName="AccountId.Code" 
                        VisibleIndex="2">
                        <PropertiesTextEdit Width="100%">
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Nợ" FieldName="Debit" VisibleIndex="3">
                        <PropertiesTextEdit DisplayFormatString="0.0" NullDisplayText="0" NullText="0">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" 
                        VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="Control" />
                <SettingsEditing Mode="Inline" />
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>

<dx:XpoDataSource ID="TransactionXPO" runat="server" 
    Criteria="[RowSatatus]&gt;0" TypeName="NAS.DAL.Accounting.Journal.Transaction">
</dx:XpoDataSource>
<dx:XpoDataSource runat="server" ID="GeneralLedgerXPO" 
    TypeName="NAS.DAL.Accounting.Journal.GeneralLedger">
</dx:XpoDataSource>
<dx:XpoDataSource ID="GeneralJournalXPO" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
    Criteria="[TransactionId!Key] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="TransactionId" SessionField="TransactionId" />
    </CriteriaParameters>
</dx:XpoDataSource>

