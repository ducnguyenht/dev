<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="S04b3-DN.ascx.cs" Inherits="WebModule.Accounting.Report.UserControls.S04b3_DN" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

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

.dxeButtonEditSys 
{
    width: 170px;
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
.dxeSpinIncButton,
.dxeSpinDecButton,
.dxeSpinLargeIncButton,
.dxeSpinLargeDecButton
{
	padding: 0px 2px 0px 3px;
	
}
.dxeButtonEditButton,
.dxeCalendarButton,
.dxeSpinIncButton,
.dxeSpinDecButton,
.dxeSpinLargeIncButton,
.dxeSpinLargeDecButton
{
	vertical-align: middle;
	border: 1px solid #7f7f7f;
	cursor: pointer;
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

<dx:ASPxPopupControl ID="PopupControlS04b3Dn" runat="server" 
    AllowDragging="True" AllowResize="True" AutoUpdatePosition="True" 
    ClientInstanceName="PopupControlS04b3Dn" CloseAction="CloseButton" 
    Height="93px" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" 
    ScrollBars="Vertical" Width="650px">
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ReportToolbar ID="ReportToolbar_S04b3DN" runat="server" 
        ReportViewerID="ReportViewer_S04b3DN" ShowDefaultButtons="False">
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
    <dx:ReportViewer ID="ReportViewer_S04b3DN" runat="server" 
        ClientInstanceName="ReportViewer_S04b3DN">
    </dx:ReportViewer>
    <dx:ASPxGridViewExporter ID="GridViewExporter_S04b3DN" runat="server" 
        GridViewID="GridView_S04b3DN" Landscape="True" MaxColumnWidth="1000" 
        PaperKind="A4">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="GridView_S04b3DN" runat="server" Visible="False">
    </dx:ASPxGridView>
    <dx:ASPxHiddenField ID="hS04b3DN_month" runat="server" 
        ClientInstanceName="hS04b3DN_month">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b3DN_year" runat="server" 
        ClientInstanceName="hS04b3DN_year">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b3DN_owner" runat="server" 
        ClientInstanceName="hS04b3DN_owner">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b3DN_asset" runat="server" 
        ClientInstanceName="hS04b3DN_asset">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hS04b3dn" runat="server" ClientInstanceName="hS04b3dn">
    </dx:ASPxHiddenField>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

