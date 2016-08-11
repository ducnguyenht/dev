<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeclareVatTest.aspx.cs" Inherits="WebModule.Test.DeclareVatTest" %>

<%@ Register src="../Accounting/UserControl/InvoicelVatList.ascx" tagname="InvoicelVatList" tagprefix="uc1" %>
<%@ Register src="../Accounting/UserControl/DeclareVat.ascx" tagname="DeclareVat" tagprefix="uc2" %>

<%@ Register src="../Accounting/UserControl/InvoiceVatReport.ascx" tagname="InvoiceVatReport" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Chọn hóa đơn">
        </dx:ASPxLabel>
        <dx:ASPxComboBox ID="cboBill" runat="server" ClientInstanceName="cboBill" 
            onitemrequestedbyvalue="cboBill_ItemRequestedByValue" 
            onitemsrequestedbyfiltercondition="cboBill_ItemsRequestedByFilterCondition" 
            Width="400px" EnableCallbackMode="True" 
            IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{0}" 
            ValueField="BillId" ValueType="System.Guid">
            <Columns>
                <dx:ListBoxColumn Caption="Số hóa đơn" FieldName="Code" />
                <dx:ListBoxColumn Caption="Ngày tạo" FieldName="IssuedDate" />
            </Columns>
        </dx:ASPxComboBox>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Kê khai thuế" 
            AutoPostBack="False">
            <ClientSideEvents Click="function(s, e) {
	DeclareVatFromBill(cboBill.GetValue());
}" />
        </dx:ASPxButton>
        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="In báo cáo" 
            AutoPostBack="False">
            <ClientSideEvents Click="function(s, e) {
	formInvoiceVatList.Show();
}" />
        </dx:ASPxButton>
        
        <uc2:DeclareVat ID="DeclareVat1" runat="server" />
        
    </div>
  
    <uc1:InvoicelVatList ID="InvoicelVatList1" runat="server" />
  
    </form>
</body>
</html>
