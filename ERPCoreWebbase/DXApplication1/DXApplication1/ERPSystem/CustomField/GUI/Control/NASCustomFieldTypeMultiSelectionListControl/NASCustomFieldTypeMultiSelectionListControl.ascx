<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NASCustomFieldTypeMultiSelectionListControl.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeMultiSelectionListControl.NASCustomFieldTypeMultiSelectionListControl" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
 
<dx:ASPxCallbackPanel ID="cpnNASCustomFieldTypeMultiSelectionListControl" runat="server"
    Width="100%" OnCallback="cpnNASCustomFieldTypeMultiSelectionListControl_Callback"
    ShowLoadingPanel="false">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div style="float: left">
                <dx:ASPxHyperLink ID="hyperlinkMultiSelectionListDataViewing" runat="server" Cursor="pointer">
                </dx:ASPxHyperLink>
            </div>
            <div style="float: left">
                <dx:ASPxGridLookup ID="grdlookupItemMenu" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="CustomFieldDataId" TextFormatString="{3}">
                    <ClientSideEvents GotFocus="function(s,e) { s.ShowDropDown(); }" />
                    <GridViewProperties>
                        <Settings ShowStatusBar="Visible" ShowColumnHeaders="false" />
                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="false" AllowSelectByRowClick="True" />
                        <Templates>
                            <StatusBar>
                                <table class="OptionsTable" style="float: right">
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnApply" runat="server" AutoPostBack="false" Text="Cập nhật"
                                                OnInit="btnApply_Init">
                                                <ClientSideEvents Click="" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </StatusBar>
                        </Templates>
                    </GridViewProperties>
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" />
                        <dx:GridViewDataTextColumn FieldName="CustomFieldDataId" ReadOnly="True" ShowInCustomizationForm="True"
                            VisibleIndex="1" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Order" ShowInCustomizationForm="True" VisibleIndex="2"
                            Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="CustomFieldId!Key" ShowInCustomizationForm="True"
                            VisibleIndex="3" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="StringValue" ShowInCustomizationForm="True"
                            VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridLookup>
            </div>
            <div style="clear: both; visibility: hidden; display: none">
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
