<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeSingleSelectionListControl.ascx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeSingleSelectionListControl.NASCustomFieldTypeSingleSelectionListControl" %>
<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeSingleSelectionListControl" runat="server" 
    Width="100%" oncallback="cpnNASCustomFieldTypeSingleSelectionListControl_Callback" ShowLoadingPanel="false">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkSingleSelectionListDataViewing" runat="server" 
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxComboBox ID="cboSingleSelectionList" runat="server" 
                    TextField="StringValue" ValueField="CustomFieldDataId">

                    <ClientSideEvents GotFocus="function(s, e) {
	                    s.ShowDropDown();
                    }" />

                </dx:ASPxComboBox>
            </div>
            <div style="float: right">
                <dx:ASPxImage Cursor="pointer" ID="imgRemove" runat="server" ShowLoadingImage="true" 
                    SpriteCssClass="Sprite_Remove" ToolTip="Xóa">
                </dx:ASPxImage>
            </div>
            <div style="clear: both; visibility: hidden; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>