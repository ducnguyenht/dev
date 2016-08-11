<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="NASCustomFieldTypeIntegerControl.ascx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.NASCustomFieldTypeIntegerControl" %>
<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeIntegerControl" runat="server" 
    Width="100%" oncallback="cpnNASCustomFieldTypeIntegerControl_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkIntegerDataViewing" runat="server" 
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxSpinEdit ID="txtIntegerValueEditing" runat="server" 
                    AutoPostBack="false" Width="170px" NumberType="Integer" 
                    ToolTip="Nhấn Enter để lưu">
                </dx:ASPxSpinEdit>
            </div>
            <div style="clear: both; visibility: hidden; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>