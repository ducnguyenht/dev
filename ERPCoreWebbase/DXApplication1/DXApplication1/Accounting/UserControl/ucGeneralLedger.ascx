<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGeneralLedger.ascx.cs" Inherits="WebModule.Accounting.UserControl.ucGeneralLedger" %>
<dx:ASPxGridView runat="server" ClientInstanceName="GeneralLedger" KeyFieldName="GeneralLedgerId"
    AutoGenerateColumns="False" Width="100%" ID="ASPxGridView1" OnCustomCallback="ASPxGridView1_CustomCallback">
    <TotalSummary>
        <dx:ASPxSummaryItem DisplayFormat="Tổng nợ = {0}" FieldName="Debit" 
            SummaryType="Sum" Tag="Tổng nợ" />
        <dx:ASPxSummaryItem DisplayFormat="Tổng có = {0}" FieldName="Credit" 
            SummaryType="Sum" Tag="Tổng có" />
    </TotalSummary>
    <Columns>
        <dx:GridViewDataDateColumn FieldName="CreatDate" SortIndex="0"  SortOrder="Ascending"
            Caption="Ng&#224;y" VisibleIndex="1">
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn FieldName="TransactionCode" Caption="Mã Bút Toán" 
            VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Description" Caption="Diễn Giải" 
            VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="AccountCode" Caption="Số TK" 
            VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Debit" Caption="Nợ" VisibleIndex="5">
            <PropertiesTextEdit DisplayFormatString="0.0">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Có" VisibleIndex="6" FieldName="Credit">
            <PropertiesTextEdit DisplayFormatString="0.0">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Số Dư" FieldName="Balance" VisibleIndex="7">
            <PropertiesTextEdit DisplayFormatString="0.0">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsBehavior ColumnResizeMode="Control" />
    <Settings ShowFilterRow="True" ShowFooter="True" />

<SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>

<Settings ShowFooter="True" ShowFilterRow="True"></Settings>
</dx:ASPxGridView>
<dx:XpoDataSource ID="TransactionXPO" runat="server" Criteria="[RowSatatus]&gt;0"
    TypeName="NAS.DAL.Accounting.Journal.Transaction">
</dx:XpoDataSource>
<dx:XpoDataSource runat="server" ID="GeneralLedgerXPO" TypeName="NAS.DAL.Accounting.Journal.GeneralLedger">
</dx:XpoDataSource>
<dx:XpoDataSource ID="GeneralJournalXPO" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal"
    Criteria="[TransactionId!Key] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="TransactionId" SessionField="TransactionId" />
    </CriteriaParameters>
</dx:XpoDataSource>

