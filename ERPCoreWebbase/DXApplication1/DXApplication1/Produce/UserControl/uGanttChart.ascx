<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uGanttChart.ascx.cs" Inherits="WebModule.Produce.UserControl.uGanttChart" %>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" 
    Font-Size="Medium" Height="55px" Text="Biểu đồ sản xuất" Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:WebChartControl ID="WebChartControl1" runat="server" 
    CrosshairEnabled="True" Height="417px" SideBySideEqualBarWidth="True" 
    Width="685px">
    <diagramserializable>
        <dx:GanttDiagram>
            <axisx visibleinpanesserializable="-1">
                <range alwaysshowzerolevel="True" sidemarginsenabled="True" />
<Range AlwaysShowZeroLevel="True" SideMarginsEnabled="True"></Range>
            </axisx>
            <axisy visibleinpanesserializable="-1">
                <range alwaysshowzerolevel="True" sidemarginsenabled="True" />
<Range AlwaysShowZeroLevel="True" SideMarginsEnabled="True"></Range>
            </axisy>
        </dx:GanttDiagram>
    </diagramserializable>
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

    <seriesserializable>
        <dx:Series Name="Thời lượng" ValueScaleType="DateTime" 
            SynchronizePointOptions="False">
            <points>
                <dx:SeriesPoint ArgumentSerializable="Công đoạn 1" 
                    DateTimeValues="08/07/2013 08:30:00.000;08/09/2013 00:00:00.000" 
                    SeriesPointID="0">
                </dx:SeriesPoint>
                <dx:SeriesPoint ArgumentSerializable="Công đoạn 2" 
                    DateTimeValues="08/09/2013 00:00:00.000;08/10/2013 00:00:00.000" 
                    SeriesPointID="1">
                </dx:SeriesPoint>
                <dx:SeriesPoint ArgumentSerializable="Công đoạn 3" 
                    DateTimeValues="08/10/2013 00:00:00.000;08/11/2013 00:00:00.000" 
                    SeriesPointID="2">
                </dx:SeriesPoint>
            </points>
            <viewserializable>
                <dx:OverlappedGanttSeriesView>
                </dx:OverlappedGanttSeriesView>
            </viewserializable>
            <labelserializable>
                <dx:RangeBarSeriesLabel>
                    <fillstyle>
                        <optionsserializable>
                            <dx:SolidFillOptions />
                        </optionsserializable>
                    </fillstyle>
                    <pointoptionsserializable>
                        <dx:RangeBarPointOptions>
                        </dx:RangeBarPointOptions>
                    </pointoptionsserializable>
                </dx:RangeBarSeriesLabel>
            </labelserializable>
            <legendpointoptionsserializable>
                <dx:RangeBarPointOptions>
                </dx:RangeBarPointOptions>
            </legendpointoptionsserializable>
        </dx:Series>
    </seriesserializable>
    <seriestemplate>
        <viewserializable>
            <dx:OverlappedGanttSeriesView>
            </dx:OverlappedGanttSeriesView>
        </viewserializable>
        <labelserializable>
            <dx:RangeBarSeriesLabel>
                <fillstyle>
                    <optionsserializable>
                        <dx:SolidFillOptions />
                    </optionsserializable>
                </fillstyle>
                <pointoptionsserializable>
                    <dx:RangeBarPointOptions>
                    </dx:RangeBarPointOptions>
                </pointoptionsserializable>
            </dx:RangeBarSeriesLabel>
        </labelserializable>
        <legendpointoptionsserializable>
            <dx:RangeBarPointOptions>
            </dx:RangeBarPointOptions>
        </legendpointoptionsserializable>
    </seriestemplate>

<CrosshairOptions><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
</dx:WebChartControl>

