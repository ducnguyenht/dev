<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="u05_VT.ascx.cs" Inherits="WebModule.Warehouse.Report.UserControls.u05_VT" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<dx:ASPxPopupControl ID="xPopupControl" runat="server" AllowDragging="True" 
    AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="xPopupControl" 
    CloseAction="CloseButton" Modal="True" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" Width="417px" 
    HeaderText="Biên bản kiểm kê" Maximized="True">
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="ReportToolbar" runat="server" ShowDefaultButtons="False" 
        ReportViewerID="ReportViewer">
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
    <br />
    <dx:ASPxGridView ID="xGridView" runat="server" Visible="False">
        <SettingsBehavior AllowSort="False" />
    </dx:ASPxGridView>
    <br />
    <dx:ASPxGridViewExporter ID="xGridViewExporter" runat="server" Landscape="True" 
        OnRenderBrick="xGridViewExporter_RenderBrick">
    </dx:ASPxGridViewExporter>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

    <p>
        &nbsp;</p>



    

    



<dx:ASPxHiddenField ID="hfReportAudit" runat="server" 
    ClientInstanceName="hfReportAudit">
</dx:ASPxHiddenField>




    

    



