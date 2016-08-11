<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeBuiltInSingleSelectionListControl.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInSingleSelectionListControl.NASCustomFieldTypeBuiltInSingleSelectionListControl" %>
<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeBuiltInSingleSelectionListControl"
    runat="server" Width="100%" OnCallback="cpnNASCustomFieldTypeBuiltInSingleSelectionListControl_Callback"
    ShowLoadingPanel="false">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkBuiltInSingleSelectionListDataViewing" runat="server"
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxComboBox ID="cboBuiltInSingleSelectionList" runat="server" EnableCallbackMode="true"
                    CallbackPageSize="10" OnItemRequestedByValue="cboBuiltInSingleSelectionList_ItemRequestedByValue"
                    OnItemsRequestedByFilterCondition="cboBuiltInSingleSelectionList_ItemsRequestedByFilterCondition"
                    IncrementalFilteringMode="Contains" 
                    OnInit="cboBuiltInSingleSelectionList_Init">
                    <ClientSideEvents GotFocus="function(s, e) {
	                        s.ShowDropDown();
                        }" />
                    <ItemStyle Wrap="True" />
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
