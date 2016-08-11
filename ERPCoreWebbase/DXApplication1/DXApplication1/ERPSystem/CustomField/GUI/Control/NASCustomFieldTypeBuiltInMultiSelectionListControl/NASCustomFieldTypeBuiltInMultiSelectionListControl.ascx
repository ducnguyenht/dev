<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeBuiltInMultiSelectionListControl.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeBuiltInMultiSelectionListControl.NASCustomFieldTypeBuiltInMultiSelectionListControl" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeBuiltInMultiSelectionListControl"
    runat="server" Width="100%" OnCallback="cpnNASCustomFieldTypeBuiltInMultiSelectionListControl_Callback"
    ShowLoadingPanel="false">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkBuiltInMultiSelectionListDataViewing" runat="server"
                    Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxGridLookup ID="grdlookupItemMenu" runat="server" AutoGenerateColumns="False"
                    Width="100%" DataSourceID="datasource" SelectionMode="Multiple">
                    <ClientSideEvents GotFocus="function(s,e) { s.ShowDropDown(); }" />
                    <GridViewProperties>
                        <Settings ShowColumnHeaders="true" ShowStatusBar="Visible" ShowFilterRow="True" />
                        <SettingsBehavior AllowSelectByRowClick="True" />
                        <Templates>
                            <StatusBar>
                                <dx:ASPxButton ID="btnApply" runat="server" AutoPostBack="false" Text="Cập nhật"
                                    OnInit="btnApply_Init">
                                </dx:ASPxButton>
                            </StatusBar>
                        </Templates>
                    </GridViewProperties>
                    <GridViewStyles>
                        <Cell Wrap="True">
                        </Cell>
                    </GridViewStyles>
                    <ClientSideEvents GotFocus="function(s,e) { s.ShowDropDown(); }"></ClientSideEvents>
                </dx:ASPxGridLookup>
            </div>
            <div style="clear: both; visibility: hidden; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:XpoDataSource ID="datasource" runat="server">
</dx:XpoDataSource>
