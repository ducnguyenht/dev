<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeDateTimeControl.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeDateTimeControl.NASCustomFieldTypeDateTimeControl" %>

<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeDateTimeControl" runat="server" 
    Width="100%" oncallback="cpnNASCustomFieldTypeDateTimeControl_Callback" 
    oninit="cpnNASCustomFieldTypeDateTimeControl_Init">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkDateTimeDataViewing" runat="server" 
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxDateEdit ID="calendar" runat="server">
                </dx:ASPxDateEdit>
            </div>
            <div style="clear: both;">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
