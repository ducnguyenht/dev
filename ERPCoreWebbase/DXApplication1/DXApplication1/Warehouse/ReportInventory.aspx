<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReportInventory.aspx.cs" Inherits="WebModule.Warehouse.ReportInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grid_CustomButtonClick(s, e) {
        chart1.PerformCallback();

    }

 </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
                            <Panes>
                                <dx:SplitterPane AutoHeight="True" Size="200px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                                KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId">
                                                <Columns>
                                                    <dx:TreeListTextColumn Caption="Kho" FieldName="name" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                                    </dx:TreeListTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedNode="True" />
                                            </dx:ASPxTreeList>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane AutoHeight="True">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            B<strong>iểu đồ danh sách mặt hàng trong kho<br /> </strong>
                                            <dx:ASPxPivotGrid ID="ASPxPivotGrid12" runat="server" ClientIDMode="AutoID" 
                                                Width="780px">
                                                <Fields>
                                                    <dx:PivotGridField ID="fieldHangHoa" Area="RowArea" AreaIndex="0" 
                                                        Caption="Hàng hóa" FieldName="HangHoa">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldDVT" Area="ColumnArea" AreaIndex="0" 
                                                        Caption="Đơn vị tính" FieldName="DVT">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldSoLuong" Area="DataArea" AreaIndex="0" 
                                                        Caption="Số lượng" FieldName="SoLuong">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="FieldVitri" Area="RowArea" AreaIndex="1" 
                                                        Caption="Vị trí" FieldName="Vitri">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldHansudung" Area="RowArea" AreaIndex="0" 
                                                        Caption="Hạn sử dụng" FieldName="HanSuDung">
                                                    </dx:PivotGridField>
                                                </Fields>
                                                <ClientSideEvents EndCallback="function(s, e) {
	                                                    grid_CustomButtonClick(s,e);
                                        }" />
                                                <Groups>
                                                    <dx:PivotGridWebGroup ShowNewValues="True" />
                                                </Groups>
                                            </dx:ASPxPivotGrid>
                                            <dx:WebChartControl ID="WebChartControl1" runat="server" 
                                                DataSourceID="ASPxPivotGrid12" Width="780px" ClientInstanceName="chart1" 
                                                Height="300px">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

                                                <legend maxhorizontalpercentage="30"></legend>

<SeriesTemplate argumentdatamember="Arguments" argumentscaletype="Qualitative" valuedatamembersserializable="Values"><ViewSerializable>
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
</asp:Content>
