<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04a4-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a4_DN" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<dx:ASPxPopupControl ID="PopupControlS04a4dn" runat="server" AllowDragging="True"
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS04a4dn"
    CloseAction="CloseButton" Height="185px" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ScrollBars="Vertical"
    Width="704px" HeaderText="">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ReportToolbar ID="S04a4dnReportToolbar" runat="server" ReportViewerID="S04a4dnReportViewer"
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
            <dx:ReportViewer ID="S04a4dnReportViewer" ClientInstanceName="S04a4dnReportViewer"
                runat="server">
            </dx:ReportViewer>
            <br />
            <dx:ASPxGridView ID="xGridView" ClientInstanceName="xGridView" runat="server" Visible="False" />
            <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" Landscape="True" MaxColumnWidth="1000"
                PaperKind="A4" GridViewID="GridViewExporter" />
            <dx:ASPxHiddenField ID="hS04a4dnMonth" runat="server" ClientInstanceName="hS04a4dnMonth" />
            <dx:ASPxHiddenField ID="hS04a4dn" runat="server" ClientInstanceName="hS04a4dn" />
            <dx:ASPxHiddenField ID="hS04a4dnYear" runat="server" ClientInstanceName="hS04a4dnYear" />
            <dx:ASPxHiddenField ID="hS04a4dnAcc" runat="server" ClientInstanceName="hS04a4dnAcc" />
            <dx:ASPxHiddenField ID="hS04a4dnOwnerOrg" runat="server" ClientInstanceName="hS04a4dnOwnerOrg" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
