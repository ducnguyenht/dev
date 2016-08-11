<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeStringControl.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeStringControl.NASCustomFieldTypeStringControl" %>

<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeStringControl" runat="server" 
    Width="100%" oncallback="cpnNASCustomFieldTypeStringControl_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkStringDataViewing" runat="server" 
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxTextBox ID="txtStringValueEditing" ToolTip="Nhấn Enter để lưu" runat="server" Width="170px">
                </dx:ASPxTextBox>
            </div>
            <div style="clear: both; visibility: hidden; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
