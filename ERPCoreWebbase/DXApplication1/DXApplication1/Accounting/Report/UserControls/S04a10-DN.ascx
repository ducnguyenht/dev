<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04a10-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a10_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlS04a10Dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS04a10Dn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="">ContentRightL
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
    <dx:ReportViewer ID="S04a10dnReportViewer" runat="server">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="S04a10dnASPxGridViewExporter" runat="server" 
        GridViewID="S04a10dnASPxGridView" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hS04a10dnOwner" runat="server" 
        ClientInstanceName="hS04a10dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a10dnMonth" runat="server" 
        ClientInstanceName="hS04a10dnMonth">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a10dnYear" runat="server" 
        ClientInstanceName="hS04a10dnYear">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="S04a10dnASPxGridView" runat="server" 
        ClientInstanceName="S04a10dnASPxGridView" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS04a10dnAccount" runat="server" 
        ClientInstanceName="hS04a10dnAccount">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04a10dnAsset" runat="server" 
        ClientInstanceName="hS04a10dnAsset">
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hS04a10dn" runat="server" 
        ClientInstanceName="hS04a10dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>