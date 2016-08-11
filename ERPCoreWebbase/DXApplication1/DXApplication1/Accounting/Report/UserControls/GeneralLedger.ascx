<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralLedger.ascx.cs"
    Inherits="WebModule.Accounting.Report.UserControls.GeneralLedger" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<dx:ASPxPopupControl ID="pop_GeneralLedger" ClientInstanceName="pop_GeneralLedger" Height="501px"  Width="680px"
AllowDragging="true" AllowResize="true" AutoUpdatePosition="true" ScrollBars="Auto" Maximized="true"
 CloseAction="CloseButton" ShowMaximizeButton="true" ShowSizeGrip="False" runat="server" RenderMode="Lightweight">
    <ContentCollection>
        <dx:PopupControlContentControl ID="popcccccc_GeneralLedger" runat="server" SupportsDisableAttribut="true">
            <dx:ReportToolbar ID="ReportToolbarGL" runat="server" ReportViewerID="ReportViewerGLedger"
                ShowDefaultButtons="False" Width="100%" 
                ClientInstanceName="ReportToolbarGL">
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
                    <dx:ReportToolbarTextBox ItemKind="PageCount" />
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
            <dx:ReportViewer ID="ReportViewerGLedger" runat="server" 
                ClientInstanceName="ReportViewerGLedger">
            </dx:ReportViewer>
            <dx:ASPxGridView ID="xGridView" runat="server" Visible="false" AllowSort="False">
                <Styles>
                    <Header Wrap="False">
                    </Header>
                </Styles>
            </dx:ASPxGridView>

            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server"  />

            <dx:ASPxHiddenField ID="GeneralLedgerMonth" runat="server" ClientInstanceName="GeneralLedgerMonth" />
            <dx:ASPxHiddenField ID="GeneralLedgerYear" runat="server" ClientInstanceName="GeneralLedgerYear" />
            <dx:ASPxHiddenField ID="GeneralLedgerAcc" runat="server" ClientInstanceName="GeneralLedgerAcc" />
            <dx:ASPxHiddenField ID="GLedger" runat="server" ClientInstanceName="GLedger" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
