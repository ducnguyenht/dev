<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uReportPivot.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uReportPivot" %>
<dx:ASPxHiddenField ID="reportType" runat="server" ClientInstanceName="reportType">
</dx:ASPxHiddenField>
<dx:ASPxPopupControl ScrollBars="Auto" CssClass="pcReportViewerPopup" ID="pcReportViewerPopup"
    runat="server" ClientInstanceName="pcReportViewerPopup" HeaderText="Báo cáo"
    MinHeight="550px" MinWidth="860px" Height="647px" RenderMode="Lightweight" Width="1024px"
    AllowDragging="true" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
    AllowResize="True" CloseAction="CloseButton">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">

        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%" Width="100%" 
                Orientation="Vertical">
            <Panes>
                <dx:SplitterPane ScrollBars="Auto">
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                            <div style="margin-bottom: 10px;">
                                <dx:ASPxLabel ID="lblHeader" runat="server" Text="Bảng" Font-Bold="True"
                                    Font-Size="Small">
                                </dx:ASPxLabel>
                            </div>
                            <dx:ASPxPivotGrid ID="ASPxPivotGrid1" runat="server" ClientIDMode="AutoID" 
                                Width="100%">
                                <Fields>
                                    <dx:PivotGridField ID="fieldKho" Area="RowArea" AreaIndex="1" Caption="Kho" 
                                        FieldName="Kho">
                                    </dx:PivotGridField>
                                    <dx:PivotGridField ID="fieldDVT" Area="ColumnArea" AreaIndex="0" Caption="ĐVT" 
                                        FieldName="DVT">
                                    </dx:PivotGridField>
                                    <dx:PivotGridField ID="fieldSoLuong" Area="DataArea" AreaIndex="0" 
                                        Caption="Số Lượng" FieldName="SoLuong">
                                    </dx:PivotGridField>
                                    <dx:PivotGridField ID="fieldLo" Area="RowArea" AreaIndex="0" Caption="Lô" 
                                        FieldName="Lo">
                                    </dx:PivotGridField>
                                    <dx:PivotGridField ID="fieldTotal" Area="RowArea" AreaIndex="1" 
                                        Caption="Thành tiền" FieldName="Total">
                                    </dx:PivotGridField>
                                </Fields>
                                <ClientSideEvents EndCallback="function(s, e) {
	chart1.PerformCallback();
}" />
                            </dx:ASPxPivotGrid>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane ScrollBars="Auto">
                    <ContentCollection>
                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                <div style="margin-bottom: 10px;">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Biểu đồ" Font-Bold="True"
                                    Font-Size="Small">
                                    </dx:ASPxLabel>
                                </div>
                                <dx:WebChartControl ID="WebChartControl1" runat="server"  ClientInstanceName="chart1"
                                    DataSourceID="ASPxPivotGrid1" Width="819px" Height="250px">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

                                    <legend maxhorizontalpercentage="30"></legend>

<SeriesTemplate argumentdatamember="Arguments" argumentscaletype="Qualitative" 
                                        valuedatamembersserializable="Values"><ViewSerializable>
<dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<dx:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
<PointOptionsSerializable>
<dx:PointOptions></dx:PointOptions>
</PointOptionsSerializable>
</dx:SideBySideBarSeriesLabel>
</LabelSerializable>
<LegendPointOptionsSerializable>
<dx:PointOptions></dx:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>

<CrosshairOptions><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
                                </dx:WebChartControl>
                            </dx:SplitterContentControl>
                
        </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>
</dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
