<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04b2-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04b2_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS04b2dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS04b2dn" 
    CloseAction="CloseButton" Height="185px" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" Width="704px" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlS04b2dnaaa" runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="S04b2dnReportToolbar" runat="server" 
        ReportViewerID="S04b2dnReportViewer" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="S04b2dnReportViewer" ClientInstanceName="S04b2dnReportViewer" runat="server">
    </dx:ReportViewer>
    <br />
    <dx:ASPxGridView ID="S04b2dnASPxGridView1" ClientInstanceName="S04b2dnASPxGridView1" runat="server" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="S04b2dnGridViewExporter" runat="server" Landscape="True" 
        MaxColumnWidth="1000" PaperKind="A4" GridViewID="S04b2dnASPxGridView1">
    </dx:ASPxGridViewExporter>
    <dx:ASPxHiddenField ID="hS04b2dnMonth" runat="server" ClientInstanceName="hS04b2dnMonth">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b2dn" runat="server" ClientInstanceName="hS04b2dn">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b2dnYear" runat="server" ClientInstanceName="hS04b2dnYear">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b2dnAccount" runat="server" 
        ClientInstanceName="hS04b2dnAccount">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b2dnOwner" runat="server" ClientInstanceName="hS04b2dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b2dnAsset" runat="server" ClientInstanceName="hS04b2dnAsset">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>