<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04a3-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a3_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS04a3dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS04a3dn" 
    CloseAction="CloseButton" Height="185px" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" Width="704px" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="S04a3dnReportToolbar" runat="server" 
        ReportViewerID="S04a3dnReportViewer" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="S04a3dnReportViewer" ClientInstanceName="S04a3dnReportViewer" runat="server">
    </dx:ReportViewer>
    <br />
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporterS04a3" runat="server" Landscape="True" 
        MaxColumnWidth="1000" PaperKind="A4" GridViewID="ASPxGridViewS04a3">
    </dx:ASPxGridViewExporter>
    <dx:ASPxHiddenField ID="hS04a3dnMonth" runat="server" ClientInstanceName="hS04a3dnMonth">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a3dn" runat="server" ClientInstanceName="hS04a3dn">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a3dnYear" runat="server" ClientInstanceName="hS04a3dnYear">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="ASPxGridViewS04a3" ClientInstanceName="ASPxGridViewS04a3" 
        runat="server" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS04a3dnOwner" runat="server" ClientInstanceName="hS04a3dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a3dnAsset" runat="server" ClientInstanceName="hS04a3dnAsset">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

