<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="S04a5-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a5_DN" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<dx:ASPxPopupControl ID="PopupControlS04a5Dn" runat="server" 
    AllowDragging="True" AllowResize="True"
    AutoUpdatePosition="True" ClientInstanceName="PopupControlS04a5Dn" CloseAction="CloseButton"
    Height="93px" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ScrollBars="Vertical"
    Width="650px" HeaderText="">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ReportToolbar ID="ReportToolbar" runat="server" ReportViewerID="ReportViewer"
                ShowDefaultButtons="False">
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
            <dx:ReportViewer ID="ReportViewer" runat="server">
            </dx:ReportViewer>
            <dx:ASPxGridView ID="xGridView" runat="server" Visible="False">
                <SettingsBehavior AllowSort="False" />
                <SettingsPager Visible="False">
                </SettingsPager>
            </dx:ASPxGridView>
            <br />
            <dx:ASPxGridViewExporter ID="xGridViewExporter" runat="server" Landscape="True" 
                PaperKind="A4" OnRenderBrick="xGridViewExporter_RenderBrick">
            </dx:ASPxGridViewExporter>
            <dx:ASPxHiddenField ID="hS04a5Asset" runat="server" 
                ClientInstanceName="hS04a5Asset">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hS04a5month" runat="server" 
                ClientInstanceName="hS04a5month">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hS04a5year" runat="server" 
                ClientInstanceName="hS04a5year">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hS04a5dn" runat="server" 
                ClientInstanceName="hS04a5dn">
            </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
