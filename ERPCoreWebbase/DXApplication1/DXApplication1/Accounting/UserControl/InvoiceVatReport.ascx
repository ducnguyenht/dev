<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceVatReport.ascx.cs" Inherits="WebModule.Accounting.UserControl.InvoiceVatReport" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<script type="text/javascript">
    function formInvoiceVatReport_EndCallback(s, e) {
        if (s.cpShowReport) {
            formInvoiceVatReport.Show();
            delete (s.cpShowReport);
        }
    }
</script>
<dx:ASPxPopupControl ID="formInvoiceVatReport" runat="server" 
    ClientInstanceName="formInvoiceVatReport" CloseAction="CloseButton" 
    HeaderText="Bảng kê " Height="363px" RenderMode="Lightweight" 
    Width="710px" onwindowcallback="formInvoiceVatReport_WindowCallback" 
    Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter">
    <ClientSideEvents EndCallback="formInvoiceVatReport_EndCallback" />
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="reportToolbar" runat="server" 
        ReportViewerID="reportViewer" ShowDefaultButtons="False">
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
                <elements>
                    <dx:ListElement Value="pdf" />
                    <dx:ListElement Value="xls" />
                    <dx:ListElement Value="xlsx" />
                    <dx:ListElement Value="rtf" />
                    <dx:ListElement Value="mht" />
                    <dx:ListElement Value="html" />
                    <dx:ListElement Value="txt" />
                    <dx:ListElement Value="csv" />
                    <dx:ListElement Value="png" />
                </elements>
            </dx:ReportToolbarComboBox>
        </Items>
        <styles>
            <LabelStyle>
            <margins marginleft="3px" marginright="3px" />
<Margins MarginLeft="3px" MarginRight="3px"></Margins>
            </LabelStyle>
        </styles>
    </dx:ReportToolbar>
    <dx:ReportViewer ID="reportViewer" runat="server" ClientInstanceName="reportViewer">
    </dx:ReportViewer>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxHiddenField ID="ReportHiddenField" runat="server" 
    ClientInstanceName="ReportHiddenField">
</dx:ASPxHiddenField>


