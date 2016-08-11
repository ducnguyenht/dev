<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S12-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S12_DN" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<style type="text/css">

.dxeBase
{
	font: 12px Tahoma, Geneva, sans-serif;
}
.dxeTrackBar, 
.dxeIRadioButton, 
.dxeButtonEdit, 
.dxeTextBox, 
.dxeRadioButtonList, 
.dxeCheckBoxList, 
.dxeMemo, 
.dxeListBox, 
.dxeCalendar, 
.dxeColorTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxeTextBox,
.dxeButtonEdit,
.dxeIRadioButton,
.dxeRadioButtonList,
.dxeCheckBoxList
{
    cursor: default;
}

.dxeButtonEdit
{
	background-color: white;
	border: 1px solid #9F9F9F;
}

.dxeTextBoxSys,
.dxeButtonEditSys 
{
    width: 170px;
}

    input[type="text"].dxeEditArea,
    input[type="password"].dxeEditArea
    {
        padding-top: 1px;
        padding-bottom: 1px;
        margin-top: -1px;
        margin-bottom: -1px;
    }
    input[type="text"].dxeEditArea, /*Bootstrap correction*/
input[type="password"].dxeEditArea /*Bootstrap correction*/
{
    height: 14px;
    margin-bottom: 0px;
}
.dxeEditArea,
input[type="text"].dxeEditArea, /*Bootstrap correction*/
input[type="password"].dxeEditArea /*Bootstrap correction*/
{
    font: 12px Tahoma, Geneva, sans-serif;
}
.dxeMemoEditAreaSys, /*Bootstrap correction*/
input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/
input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
{
    display: block;
    -webkit-box-shadow: none;
    -moz-box-shadow: none;
    box-shadow: none;
    -webkit-transition: none;
    -moz-transition: none;
    -o-transition: none;
    transition: none;
	-webkit-border-radius: 0px;
    -moz-border-radius: 0px;
    border-radius: 0px;
}
.dxeEditAreaSys,
.dxeMemoEditAreaSys, /*Bootstrap correction*/
input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/
input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
{
    line-height: normal;
}
.dxeEditAreaSys,
input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/
input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
{
    padding: 0px 1px 0px 0px; /* B146658 */
}

.dxeButtonEdit .dxeEditArea
{
	background-color: white;
}

.dxeEditArea
{
	border: 1px solid #A0A0A0;
}
.dxeEditAreaSys 
{
    height: 14px;
    border: 0px!important;
    background-position: 0 0; /* iOS Safari */
}
.dxeButtonEditButton,
.dxeSpinLargeIncButton,
.dxeSpinLargeDecButton
{
	
}
.dxeButtonEditButton
{
    padding: 0px 2px 1px 3px;    
}
.dxeButtonEditButton,
.dxeCalendarButton,
.dxeSpinIncButton,
.dxeSpinDecButton,
.dxeSpinLargeIncButton,
.dxeSpinLargeDecButton,
.dxeColorEditButton
{
	vertical-align: middle;
	border: 1px solid #7f7f7f;
	cursor: pointer;
	font: normal 11px Tahoma, Geneva, sans-serif;
	text-align: center;
	white-space: nowrap;
} 
.dxeTextBox,
.dxeMemo
{
	background-color: white;
	border: 1px solid #9f9f9f;
}

.dxeTextBoxSys, 
.dxeMemoSys 
{
    border-collapse:separate!important;
}

.dxeTextBox .dxeEditArea
{
	background-color: white;
}
</style>


<dx:ASPxPopupControl ID="PopupControlS12Dn" runat="server" Width="650px" 
    AllowDragging="True" AllowResize="True" AutoUpdatePosition="True" 
    ClientInstanceName="PopupControlS12Dn" CloseAction="CloseButton" 
    Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" ScrollBars="Vertical">
    <ContentCollection>
<dx:PopupControlContentControl runat="server">
    <dx:ReportToolbar ID="ReportToolbar_S12DN" runat="server" 
        ReportViewerID="ReportViewer_S11DN" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="ReportViewer_S12DN" runat="server" 
        ClientInstanceName="ReportViewer_S12DN" ImagesEmbeddingMode="Url">
    </dx:ReportViewer>
    <br />
    <dx:ASPxGridViewExporter ID="GridViewExporter_S12DN" runat="server" 
        GridViewID="GridView_S12DN" Landscape="True" MaxColumnWidth="1000" 
        PaperKind="A4">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="GridView_S12DN" runat="server" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS12DN_month" runat="server" 
        ClientInstanceName="hS12DN_month">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS12DN_year" runat="server" 
        ClientInstanceName="hS12DN_year">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS12DN_owner" runat="server" 
        ClientInstanceName="hS12DN_owner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS12DN_asset" runat="server" 
        ClientInstanceName="hS12DN_asset">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS12dn" runat="server" ClientInstanceName="hS12dn">
    </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="hs12dnItem" runat="server" 
        ClientInstanceName="hs12dnItem">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>


