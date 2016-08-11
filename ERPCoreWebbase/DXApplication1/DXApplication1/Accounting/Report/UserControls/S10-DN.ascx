<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S10-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S10_DN" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<dx:ASPxPopupControl ID="popup_s10_dn" ClientInstanceName="popup_s10_dn" runat="server" Height="501px" Width="860px" Maximized = "True"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ScrollBars="Auto" CloseAction="CloseButton"
    ShowMaximizeButton="true" ShowSizeGrip="False" AllowDragging="true" AllowResize="true" AutoUpdatePosition="true">
<ContentCollection>
    <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server" SupportsDisableAttribute="True">
        <dx:ReportToolbar ID="ReportToolbarS10dn" runat="server" ReportViewerID="ReportViewerS10dn" Width="100%" ShowDefaultButtons="False">
            <Items>
                <dx:ReportToolbarButton ItemKind='Search' />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton ItemKind='PrintReport' />
                <dx:ReportToolbarButton ItemKind='PrintPage' />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton Enabled='False' ItemKind='FirstPage' />
                <dx:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' />
                <dx:ReportToolbarLabel ItemKind='PageLabel' />
                <dx:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
                </dx:ReportToolbarComboBox>
                <dx:ReportToolbarLabel ItemKind='OfLabel' />
                <dx:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
                <dx:ReportToolbarButton ItemKind='NextPage' />
                <dx:ReportToolbarButton ItemKind='LastPage' />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton ItemKind='SaveToDisk' />
                <dx:ReportToolbarButton ItemKind='SaveToWindow' />
                <dx:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
                    <Elements>
                        <dx:ListElement Value='pdf' />
                        <dx:ListElement Value='xls' />
                        <dx:ListElement Value='xlsx' />
                        <dx:ListElement Value='rtf' />
                        <dx:ListElement Value='mht' />
                        <dx:ListElement Value='html' />
                        <dx:ListElement Value='txt' />
                        <dx:ListElement Value='csv' />
                        <dx:ListElement Value='png' />
                    </Elements>
                </dx:ReportToolbarComboBox>
            </Items>
            <Styles>
                <LabelStyle>
                    <Margins MarginLeft='3px' MarginRight='3px' />
                </LabelStyle>
            </Styles>
        </dx:ReportToolbar>
        <dx:ReportViewer ID = "ReportViewerS10" ClientInstanceName="ReportViewerS10" runat="server"></dx:ReportViewer>
        <dx:ASPxGridView ID="xGridView" ClientInstanceName="xGridView" runat="server" Visible="false" ></dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="xGridViewExporter" runat="server" Landscape="true" PaperKind="A4"></dx:ASPxGridViewExporter>
        <dx:ASPxHiddenField ID="hs10dn" runat="server" ClientInstanceName="hs10dn"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs10dnMonth" runat="server" ClientInstanceName="hs10dnMonth"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs10dnYear" runat="server" ClientInstanceName="hs10dnYear"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs10dnAcc" runat="server" ClientInstanceName="hs10dnAcc"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs10dnUnitDim" runat="server" ClientInstanceName="hs10dnUnitDim"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs10dnItem" runat ="server"  ClientInstanceName="hs10dnItem"></dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs10dnInventory" runat="server" ClientInstanceName="hs10dnInventory"></dx:ASPxHiddenField>
        
    </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>