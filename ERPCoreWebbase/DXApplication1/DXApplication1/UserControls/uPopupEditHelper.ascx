<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPopupEditHelper.ascx.cs" Inherits="WebModule.UserControls.uPopupEditHelper" %>
<style type = "text/css">
    #splilterReport_1_CC
    {
            height:100%;
            width:100%;
    }
</style>
<script type="text/javascript">
    function treelistHelper_NodeClick(s, e) {
        popupItemUnitHelper.PerformCallback();
    }

    function popupItemUnitHelper_AfterResizing(s, e) {
        $("#splilterReport_1_CC").css({ height:'100%',width:'100%'});
    }
</script>
<dx:ASPxPopupControl ID="popupItemUnitHelper" 
    ClientInstanceName="popupItemUnitHelper"
    runat="server"
    HeaderText="Màn Hình Trợ Giúp "
    Height="600px" Modal="True" Width="1200px" 
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="false" ShowSizeGrip="False" AllowResize="true" 
    ScrollBars="Auto" ShowMaximizeButton="True" CloseAction="CloseButton" RenderMode="Classic"
    LoadingPanelText="Đang xử lý" 
    onwindowcallback="popupItemUnitHelper_WindowCallback">
    <ClientSideEvents AfterResizing="popupItemUnitHelper_AfterResizing" />
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxSplitter ID="splilterReport" ClientInstanceName="splilterReport" runat="server" Height="100%" Width="100%" ResizingMode="Postponed" 
                Orientation="Horizontal">
                <Panes>
                    <dx:SplitterPane Size="200px">
                        <ContentCollection>
                            <dx:SplitterContentControl>
                                <dx:ASPxTreeList ID="treelistHelper" ClientInstanceName="treelistHelper" 
                                    runat="server" AutoGenerateColumns="False" KeyFieldName="LinkId">
                                    <Columns>
                                        <dx:TreeListTextColumn FieldName="Text" ShowInCustomizationForm="True" 
                                            VisibleIndex="0">
                                        </dx:TreeListTextColumn>
                                    </Columns>
                                    <Settings ShowColumnHeaders="False"/>
                                    <SettingsBehavior AllowFocusedNode="true"  AutoExpandAllNodes="true"/>
                                    <ClientSideEvents FocusedNodeChanged="treelistHelper_NodeClick"/>
                                </dx:ASPxTreeList>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane Size="100%" ScrollBars="Auto">
                        <ContentCollection>
                            <dx:SplitterContentControl>
                                <%--<dx:ASPxHtmlEditor ID="HtmlEditorHelpContent" ClientInstanceName="HtmlEditorHelpContent" runat="server"  Height="100%" Width="100%">
                                    <SettingsImageUpload UploadImageFolder="~/UploadImages/">
                                        <ValidationSettings AllowedFileExtensions=".jpe,.jpeg,.jpg,.gif,.png" MaxFileSize="500000">
                                        </ValidationSettings>
                                    </SettingsImageUpload>
                                    <Settings AllowDesignView="false" AllowHtmlView="false" AllowPreview="true" />
                                </dx:ASPxHtmlEditor>--%>
                                <div id = "ducga" style="width:100%;height:100%">
                                    <%= this.HTMLVALUE %>
                                </div>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>