<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCombobox.aspx.cs" Inherits="WebModule.TestCombobox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="grdGeneralJournal" runat="server" AutoGenerateColumns="False"
            DataSourceID="dsGeneralJournal" KeyFieldName="GeneralJournalId" OnCellEditorInitialize="grdGeneralJournal_CellEditorInitialize">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0">
                    <EditButton Visible="True">
                    </EditButton>
                    <NewButton Visible="True">
                    </NewButton>
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataComboBoxColumn FieldName="AccountId!Key" VisibleIndex="1">
                    <PropertiesComboBox IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}"
                        ValueField="AccountId" ValueType="System.Guid" EnableCallbackMode="True" IncrementalFilteringDelay="30">
                        <Columns>
                            <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" />
                            <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" />
                            <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                        </Columns>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="Credit" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Debit" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RowStatus" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TransactionId!Key" VisibleIndex="6">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <dx:XpoDataSource ID="dsGeneralJournal" runat="server" TypeName="NAS.DAL.Accounting.Journal.GeneralJournal">
        </dx:XpoDataSource>
        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" IncrementalFilteringMode="Contains"
            TextField="Name" ValueField="ItemId" ValueType="System.Guid" 
            EnableCallbackMode="True" CallbackPageSize="10"
            TextFormatString="{0} - {1}">
            <Columns>
                <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" />
                <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" />
                <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
            </Columns>
        </dx:ASPxComboBox>
    </div>
    </form>
</body>
</html>
