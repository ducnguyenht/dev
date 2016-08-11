<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S07a-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S07a_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS07aDn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS07aDn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="">
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl07a" runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="S07adnReportToolbar" runat="server" 
        ReportViewerID="S07adnReportViewer" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="S07adnReportViewer" runat="server">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="S07adnASPxGridViewExporter" runat="server" 
        GridViewID="S07adnASPxGridView" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hS07adnOwner" runat="server" 
        ClientInstanceName="hS07adnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS07adnFromDate" runat="server" 
        ClientInstanceName="hS07adnFromDate">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS07adnToDate" runat="server" 
        ClientInstanceName="hS07adnToDate">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="S07adnASPxGridView" runat="server" 
        ClientInstanceName="S07adnASPxGridView" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS07adnAsset" runat="server" 
        ClientInstanceName="hS07adnAsset">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS07adnAccount" runat="server" 
        ClientInstanceName="hS07adnAccount">    
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hS07adn" runat="server" 
        ClientInstanceName="hS07adn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
