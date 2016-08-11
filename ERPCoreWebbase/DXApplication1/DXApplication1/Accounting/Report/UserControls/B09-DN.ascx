<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="B09-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.B09_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="PopupControlB09Dn" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlB09Dn" 
    CloseAction="CloseButton" Maximized="True" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Vertical" HeaderText="" Width="488px">
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControlB09dn" runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="B09dnReportToolbar" runat="server" 
        ReportViewerID="B09dnReportViewer" ShowDefaultButtons="False" 
        ClientInstanceName="B09dnReportToolbar">
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
    <dx:ReportViewer ID="B09dnReportViewer" runat="server" 
        ClientInstanceName="B09dnReportViewer">
    </dx:ReportViewer>
    <br />
    <br />
    <br />
    <dx:ASPxGridViewExporter ID="B09DNT1DataGridViewExporter" runat="server" 
        GridViewID="B09DNT1Data" Landscape="True">
    </dx:ASPxGridViewExporter>
    <br />
    <dx:ASPxHiddenField ID="hB09dnOwner" runat="server" 
        ClientInstanceName="hB09dnOwner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hB09dnFromDate" runat="server" 
        ClientInstanceName="hB09dnFromDate">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hB09dnToDate" runat="server" 
        ClientInstanceName="hB09dnToDate">
    </dx:ASPxHiddenField>
    <dx:ASPxGridView ID="B09DNT1Data" runat="server" 
        ClientInstanceName="B09DNT1Data" Visible="False" 
        AutoGenerateColumns="False" Width="100%">
        <SettingsBehavior ColumnResizeMode="NextColumn" />
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Settings GridLines="None" ShowColumnHeaders="False" />
        <BorderRight BorderStyle="None" />
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hB09dnAsset" runat="server" 
        ClientInstanceName="hB09dnAsset">
    </dx:ASPxHiddenField>    
    <dx:ASPxHiddenField ID="hB09dn" runat="server" 
        ClientInstanceName="hB09dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>