<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S06-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S06_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS06Dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS06Dn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControlS06dn" runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="S06dnReportToolbar" runat="server" 
        ReportViewerID="S06dnReportViewer" ShowDefaultButtons="False" 
        ClientInstanceName="S06dnReportToolbar">
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
<Margins MarginLeft="3px" MarginRight="3px"></Margins>
            </LabelStyle>
        </Styles>
    </dx:ReportToolbar>
    <dx:ReportViewer ID="S06dnReportViewer" runat="server" 
        ClientInstanceName="S06dnReportViewer">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="S06dnASPxGridViewExporter" runat="server" 
        GridViewID="S06dnASPxGridView" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hS06dnOwner" runat="server" 
        ClientInstanceName="hS06dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS06dnMonth" runat="server" 
        ClientInstanceName="hS06dnMonth">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS06dnYear" runat="server" 
        ClientInstanceName="hS06dnYear">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="S06ASPxGridView" runat="server" 
        ClientInstanceName="S06ASPxGridView" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS06dnAsset" runat="server" 
        ClientInstanceName="hS06dnAsset">
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hS06dn" runat="server" 
        ClientInstanceName="hS06dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>