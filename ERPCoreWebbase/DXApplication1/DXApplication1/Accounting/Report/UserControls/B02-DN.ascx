<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="B02-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.B02_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlB02Dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlB02Dn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControlB02dn" runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="B02dnReportToolbar" runat="server" 
        ReportViewerID="B02dnReportViewer" ShowDefaultButtons="False" 
        ClientInstanceName="B02dnReportToolbar">
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
    <dx:ReportViewer ID="B02dnReportViewer" runat="server" 
        ClientInstanceName="B02dnReportViewer">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="B02dnASPxGridViewExporter" runat="server" 
        GridViewID="B02dnASPxGridView" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hB02dnOwner" runat="server" 
        ClientInstanceName="hB02dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hB02dnFromDate" runat="server" 
        ClientInstanceName="hB02dnFromDate">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hB02dnToDate" runat="server" 
        ClientInstanceName="hB02dnToDate">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="SB02dnASPxGridView" runat="server" 
        ClientInstanceName="SB02dnASPxGridView" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hB02dnAsset" runat="server" 
        ClientInstanceName="hB02dnAsset">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hB02dnTransfered" runat="server" 
        ClientInstanceName="hB02dnTransfered">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hB02dn" runat="server" 
        ClientInstanceName="hB02dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
