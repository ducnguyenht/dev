<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="ucReportPopup.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.ucReportPopup" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<script type="text/javascript">
    function pcReportViewerPopup_AfterResizing(s, e) {
        PrintLayoutSplitter.AdjustControl();
    }
    function pcReportViewerPopup_Adjust() {
        //ERPCore.SetAdjustSizeForPopupControl(pcReportViewerPopup);
    }
    $(document).ready(function () {
        pcReportViewerPopup_Adjust();
        ASPxClientUtils.AttachEventToElement(window, "resize", pcReportViewerPopup_Adjust);
    });
</script> 
<dx:ASPxHiddenField ID="rpvHf" runat="server" ClientInstanceName="rpvHf">
</dx:ASPxHiddenField>
<dx:ASPxPopupControl ScrollBars="Auto" AutoUpdatePosition="true" 
    CssClass="pcReportViewerPopup" ID="pcReportViewerPopup"
    runat="server" ClientInstanceName="pcReportViewerPopup" HeaderText="Báo cáo"
    Height="600px" RenderMode="Lightweight" Width="900px"
    AllowDragging="true" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
    AllowResize="True" CloseAction="CloseButton" ShowMaximizeButton="True">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <div style="height: 100%; width: 100%; overflow: hidden">
                <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" FullscreenMode="True" Width="100%"
                    Height="100%" Orientation="Vertical" SeparatorVisible="false" ClientInstanceName="PrintLayoutSplitter">
                    <Panes>
                        <dx:SplitterPane Size="40" Name="ToolbarPane" MinSize="20">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                    <dx:ReportToolbar ID="rptbReportToolbarPopup" runat="server" ClientInstanceName="rptbReportToolbarPopup"
                                        ReportViewerID="rpvReportViewerPopup" ShowDefaultButtons="False">
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
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane ScrollBars="Auto">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                    <dx:ReportViewer ID="rpvReportViewerPopup" runat="server" ClientInstanceName="rpvReportViewerPopup"
                                        OnCacheReportDocument="rpvReportViewerPopup_CacheReportDocument" OnRestoreReportDocumentFromCache="rpvReportViewerPopup_RestoreReportDocumentFromCache"
                                        BorderWidth="1px" OnUnload="rpvReportViewerPopup_Unload">
                                        <Border BorderWidth="1px" />
                                    </dx:ReportViewer>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <Styles>
                        <Pane HorizontalAlign="Center">
                            <Paddings Padding="0" />
                            <Border BorderWidth="0" />
                        </Pane>
                    </Styles>
                </dx:ASPxSplitter>
            </div>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents AfterResizing="pcReportViewerPopup_AfterResizing" />
</dx:ASPxPopupControl>
