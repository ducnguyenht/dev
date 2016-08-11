<%@ Control Language="C#" ClientIDMode ="AutoID" AutoEventWireup="true" CodeBehind="uDetail.ascx.cs" Inherits="WebModule.GUI.usercontrol.uDetail" %>
<dx:ASPxPageControl ID="PC_Detail" runat="server" ActiveTabIndex="1" 
    RenderMode="Lightweight" Width="100%">
    <TabPages>
        <dx:TabPage Text="Văn bản">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxHtmlEditor ID="txtHTML" runat="server">
                    </dx:ASPxHtmlEditor>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Tập tin đính kèm">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFileManager ID="FileManager" runat="server">
                        <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                    </dx:ASPxFileManager>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>

