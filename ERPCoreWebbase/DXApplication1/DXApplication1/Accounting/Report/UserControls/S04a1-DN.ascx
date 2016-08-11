<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04a1-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a1_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS04a1Dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS04a1Dn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="S04a1dnReportToolbar" runat="server" 
        ReportViewerID="S04a1dnReportViewer" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="S04a1dnReportViewer" runat="server">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="S04a1dnASPxGridViewExporter" runat="server" 
        GridViewID="S04a11dnASPxGridView" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hS04a1dnOwner" runat="server" 
        ClientInstanceName="hS04a1dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a1dnMonth" runat="server" 
        ClientInstanceName="hS04a1dnMonth">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a1dnYear" runat="server" 
        ClientInstanceName="hS04a1dnYear">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="S04a11dnASPxGridView" runat="server" 
        ClientInstanceName="S04a11dnASPxGridView" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS04a1dnAsset" runat="server" 
        ClientInstanceName="hS04a1dnAsset">
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hS04a1dn" runat="server" 
        ClientInstanceName="hS04a1dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

