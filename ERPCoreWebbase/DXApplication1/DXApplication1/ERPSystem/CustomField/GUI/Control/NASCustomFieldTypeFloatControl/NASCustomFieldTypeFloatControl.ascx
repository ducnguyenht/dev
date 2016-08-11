<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeFloatControl.ascx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeFloatControl.NASCustomFieldTypeFloatControl" %>
<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeFloatControl" runat="server" 
    Width="100%" oncallback="cpnNASCustomFieldTypeFloatControl_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkFloatDataViewing" runat="server" 
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxSpinEdit ID="txtFloatValueEditing" runat="server" AutoPostBack="false" 
                    Width="170px" DecimalPlaces="2" ToolTip="Nhấn Enter để lưu">
                </dx:ASPxSpinEdit>
            </div>
            <div style="clear: both; visibility: hidden; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>