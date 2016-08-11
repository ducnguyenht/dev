<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uConfigAccountMachine.ascx.cs" Inherits="WebModule.Accounting.UserControl.uConfigAccountMachine" %>
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px">
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" 
        HeaderText="Cấu hình định khoản" RenderMode="Lightweight" Width="650px">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>

