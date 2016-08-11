﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04b6-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04b6_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS04b6Dn" runat="server" 
    AllowDragging="True" AllowResize="True" AutoUpdatePosition="True" 
    ClientInstanceName="PopupControlS04b6Dn" CloseAction="CloseButton" 
    Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" 
    ScrollBars="Vertical" Width="650px">
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="ReportToolbar_S04b6DN" runat="server" 
        ReportViewerID="ReportViewer_S04b6DN" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="ReportViewer_S04b6DN" runat="server" 
        ClientInstanceName="ReportViewer_S04b6DN">
    </dx:ReportViewer>
    <dx:ASPxGridViewExporter ID="GridViewExporter_S04b6DN" runat="server" 
        GridViewID="GridView_S04b6DN" Landscape="True" MaxColumnWidth="1000" 
        PaperKind="A4">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="GridView_S04b6DN" runat="server" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS04b6DN_month" runat="server" 
        ClientInstanceName="hS04b6DN_month">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b6DN_year" runat="server" 
        ClientInstanceName="hS04b6DN_year">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b6DN_owner" runat="server" 
        ClientInstanceName="hS04b6DN_owner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b6DN_asset" runat="server" 
        ClientInstanceName="hS04b6DN_asset">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b6dn" runat="server" ClientInstanceName="hS04b6dn">
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hS04b6dnAccount" runat="server" ClientInstanceName="hS04b6dnAccount">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
