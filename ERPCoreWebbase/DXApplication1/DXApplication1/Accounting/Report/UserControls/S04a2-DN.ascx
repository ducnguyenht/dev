<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04a2-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a2_DN" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<dx:ASPxPopupControl ID="PopupControlS04a2Dn" runat="server" AllowDragging="True"
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="PopupControlS04a2Dn"
    CloseAction="CloseButton" Height="180px" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ScrollBars="Vertical"
    Width="680px" HeaderText="">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ReportToolbar ID="ReportToolbar2" runat="server" ShowDefaultButtons="False">
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
            <dx:ReportViewer ID="ReportViewerS04a2" runat="server">
            </dx:ReportViewer>
            <br />
            <dx:ASPxGridView ID="ASPxGridViewS04a2" runat="server" 
                ClientInstanceName="ASPxGridViewS04a2" Visible="False">
                <SettingsBehavior ColumnResizeMode="NextColumn" />
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="ASPxGridViewExporterS04a2" runat="server" Landscape="True"
                MaxColumnWidth="800" PaperKind="A4" GridViewID="ASPxGridViewS04a2">
            </dx:ASPxGridViewExporter>
            <dx:ASPxHiddenField ID="hS04a2dnMonth" runat="server" 
                ClientInstanceName="hS04a2dnMonth">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hS04a2dnYear" runat="server" 
                ClientInstanceName="hS04a2dnYear">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hS04a2dnOwner" runat="server" 
                ClientInstanceName="hS04a2dnOwner">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hS04a2dn" runat="server" ClientInstanceName="hS04a2dn">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hCurrency" runat="server" 
                ClientInstanceName="hS04a2dnCurrency">
            </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
