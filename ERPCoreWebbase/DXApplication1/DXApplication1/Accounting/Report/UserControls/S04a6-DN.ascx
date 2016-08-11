<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04a6-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04a6_DN" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<dx:ASPxPopupControl ID="pop_s04a6_dn" ClientInstanceName="pop_s04a6_dn" Height="501px"
    Width="680px" Maximized="true" ShowMaximizeButton="true" ShowSizeGrip="False"
    AllowDragging="true" AllowResize="true" AutoUpdatePosition="true" ScrollBars="Auto"
    CloseAction="CloseButton" runat="server" RenderMode="Lightweight">
    <ContentCollection>
        <dx:PopupControlContentControl ID="popc_s04a6_dn" runat="server" SupportsDisableAttribut="true">
            <dx:ReportToolbar ID="ReportToolbar" runat="server" ReportViewerID="ReportViewers04a6"
                ShowDefaultButtons="False" Width="100%">
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
            <dx:ReportViewer ID="ReportViewers04a6" runat="server" />
            <dx:ASPxGridView ID="xGridView" runat="server" Visible="false" AllowSort="false">
                <Styles>
                    <Header Wrap="False" />
                    <Header Wrap="False">
                    </Header>
                </Styles>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="GridViewExporter" runat="server" Landscape="true" PaperKind="A4"
                OnRenderBrick="GridViewExporter_RenderBrick" />
            <dx:ASPxHiddenField ID="hs04a6dnMonth" runat="server" ClientInstanceName="hs04a6dnMonth" />
            <dx:ASPxHiddenField ID="hs04a6dnYear" runat="server" ClientInstanceName="hs04a6dnYear" />
            <dx:ASPxHiddenField ID="hS04a6dn" runat="server" ClientInstanceName="hS04a6dn" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
