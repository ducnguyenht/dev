<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04b11-DN.ascx.cs" ClientIDMode="AutoID" Inherits="WebModule.Accounting.Report.UserControls.S04b11_DN" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<dx:ASPxPopupControl ID="popup_s04b11_dn" ClientInstanceName="popup_s04b11_dn" runat="server" Height="501px" Maximized="True"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    RenderMode="Lightweight" ScrollBars="Auto" Width="860px" CloseAction="CloseButton" ShowMaximizeButton="True" ShowSizeGrip="False"
    AllowDragging="True" AllowResize="True" AutoUpdatePosition="True">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ReportToolbar ID="ReportToolbar" runat="server" ReportViewerID="ReportViewerS04b11"
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
            <dx:ReportViewer ID="ReportViewerS04b11" ClientInstanceName="ReportViewerS04b11" runat="server">
            </dx:ReportViewer>
            <dx:ASPxGridView ID="xGridView" runat="server" Visible="false" AllowSort="False">
                <Styles>
                    <Header Wrap="False">
                    </Header>
                </Styles>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="xGridViewExporter" runat="server" Landscape="True" 
                PaperKind="A4" OnRenderBrick="xGridViewExporter_RenderBrick">
            </dx:ASPxGridViewExporter>
            <dx:ASPxHiddenField ID="hs04b11dn" runat="server" ClientInstanceName="hs04b11dn">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hs04b11dnMonth" runat="server" ClientInstanceName="hs04b11dnMonth">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hs04b11dnYear" runat="server" ClientInstanceName="hs04b11dnYear">
            </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
