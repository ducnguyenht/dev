<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S08-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S08_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS08Dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS08Dn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl08" runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="S08dnReportToolbar" runat="server" 
        ReportViewerID="S08dnReportViewer" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="S08dnReportViewer" runat="server">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="S08dnASPxGridViewExporter" runat="server" 
        GridViewID="S08dnASPxGridView" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hS08dnOwner" runat="server" 
        ClientInstanceName="hS08dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS08dnFromDate" runat="server" 
        ClientInstanceName="hS08dnFromDate">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS08dnToDate" runat="server" 
        ClientInstanceName="hS08dnToDate">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="S08dnASPxGridView" runat="server" 
        ClientInstanceName="S08dnASPxGridView" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS08dnAsset" runat="server" 
        ClientInstanceName="hS08dnAsset">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS08dnAccount" runat="server" 
        ClientInstanceName="hS08dnAccount">    
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hS08dn" runat="server" 
        ClientInstanceName="hS08dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>