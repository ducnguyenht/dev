<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ProduceReport.aspx.cs" Inherits="WebModule.Produce.Report.ProduceReport" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register src="../../Accounting/UserControl/uPopReportViewer.ascx" tagname="uPopReportViewer" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function OnGetValue(values) {
            document.getElementById("hf").value = values[0];
            popup_orderdetail.Show();
            popup_orderdetail.SetHeaderText(values[1]);
            popup_orderdetail.PerformCallback();            
        }
    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách báo cáo sản xuất"
        Font-Bold="True" Font-Size="Medium" Height="45px">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="gvData" runat="server" AutoGenerateColumns="False" 
        Width="70%">
        <ClientSideEvents CustomButtonClick="function(s, e) {
                
                s.GetRowValues(e.visibleIndex, 'reportid;name', OnGetValue);
	           
            }"></ClientSideEvents>
        <Columns>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="3">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="View" Text="Xem">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Ký hiệu" VisibleIndex="2" 
                FieldName="reportid" Width="15%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên sổ" VisibleIndex="1" FieldName="name">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"
            ShowHeaderFilterButton="True" />
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridView>
    <asp:HiddenField ID="hf" runat="server" />

    <dx:ASPxPopupControl ID="popup_report" runat="server" CloseAction="CloseButton" ClientInstanceName="popup_orderdetail"
        AllowDragging="True" AllowResize="True" PopupAnimationType="None" PopupHorizontalAlign="WindowCenter"
        PopupVerticalAlign="WindowCenter" EnableViewState="False" HeaderText="" Height="617px"
        Width="1000px" DragElement="Window">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <div style="height: 100%; width: 100%; overflow: hidden">
                    <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" FullscreenMode="True" Width="100%"
                        Height="100%" Orientation="Vertical" SeparatorVisible="false" ClientInstanceName="PrintLayoutSplitter">
                        <Panes>
                            <dx:SplitterPane Size="40" Name="ToolbarPane" MinSize="20">
                                <ContentCollection>
                                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                        <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer1 %>">
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
                                        <dx:ReportViewer ID="ReportViewer1" runat="server" ClientInstanceName="viewer" OnCacheReportDocument="ReportViewer1_CacheReportDocument"
                                            OnRestoreReportDocumentFromCache="ReportViewer1_RestoreReportDocumentFromCache"
                                            OnUnload="ReportViewer1_Unload" Border-BorderStyle="Solid" Border-BorderWidth="1"
                                            Border-BorderColor="Black">
                                            <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
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
        <ClientSideEvents EndCallback="popup_orderdetail_EndCallback" />
    </dx:ASPxPopupControl>
</asp:Content>

