<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Main.Master" CodeBehind="Default.aspx.cs" Inherits="WebModule._Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxSplitter ID="ContentSplitter" runat="server" Orientation="Vertical" Height="100%" Width="100%" ClientInstanceName="ContentSplitter">
    <ClientSideEvents PaneResized="OutlookController.ContentSplitterPaneResized" />
    <Styles>
        <Pane>
            <Border BorderWidth="0px" />
            <Paddings Padding="0px" />
        </Pane>
    </Styles>
    <Panes>
        <dx:SplitterPane Name="MessageList">
			<PaneStyle CssClass="mlist"></PaneStyle>
            <ContentCollection>
                <dx:SplitterContentControl>
                    <dx:ASPxCallbackPanel ID="MessageListPanel" runat="server" RenderMode="Div" Height="100%">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
								<%-- DXCOMMENT: Configure GridView that displays mail messages --%>
                                <dx:ASPxGridView ID="MessagesGrid" runat="server" ClientInstanceName="MessagesGrid"
                                    DataSourceID="MessagesDataSource" KeyFieldName="ID" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="HasAttachment" Width="40px">
                                            <Settings AllowGroup="False" />
                                            <HeaderCaptionTemplate>
                                                <div class="attachcap"></div>
                                            </HeaderCaptionTemplate>
                                            <DataItemTemplate>
                                                <dx:ASPxImage ID="AttachmentImg" runat="server" ImageUrl="../images/attach.png" Visible='<%# Eval("HasAttachment") %>' />
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" SortingImageSpacing="5px" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="From" Width="200px" />
                                        <dx:GridViewDataColumn FieldName="Subject" />
                                        <dx:GridViewDataDateColumn FieldName="Date" Width="200px" GroupIndex="0" SortOrder="Descending">
                                            <PropertiesDateEdit DisplayFormatString="g" />
                                        </dx:GridViewDataDateColumn>
                                    </Columns>
                                    <SettingsPager Mode="ShowAllRecords" />
                                    <Settings ShowGroupPanel="True" VerticalScrollBarMode="Visible" VerticalScrollableHeight="0"
                                        ShowGroupedColumns="True" GridLines="Vertical" />
                                    <SettingsBehavior AllowFocusedRow="true" AllowClientEventsOnLoad="false" AutoExpandAllGroups="true"
                                        EnableRowHotTrack="True" ColumnResizeMode="NextColumn" />
                                    <Paddings Padding="0px" />
                                    <Border BorderWidth="0px" />
                                    <Styles>
                                        <Row Cursor="pointer" />
                                    </Styles>
                                    <ClientSideEvents FocusedRowChanged="OutlookController.MessagesGridRowChanged" EndCallback="OutlookController.MessagesGridEndCallback" />
                                </dx:ASPxGridView>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
        <dx:SplitterPane Name="MessageBody" ScrollBars="Auto">
			<PaneStyle CssClass="mbody"></PaneStyle>
            <Separator>
                <SeparatorStyle>
                    <Border BorderWidth="1px" />
                    <BorderLeft BorderWidth="0px" />
                    <BorderRight BorderWidth="0px" />
                </SeparatorStyle>
            </Separator>
            <ContentCollection>
                <dx:SplitterContentControl>
                    <div id="messageBody" class="message"></div>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
    </Panes>
</dx:ASPxSplitter>
<%-- DXCOMMENT: Configure a data source that contains mail messages --%>
<asp:SqlDataSource ID="MessagesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DataConnectionString %>"
    ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Messages]">
</asp:SqlDataSource>
</asp:Content>