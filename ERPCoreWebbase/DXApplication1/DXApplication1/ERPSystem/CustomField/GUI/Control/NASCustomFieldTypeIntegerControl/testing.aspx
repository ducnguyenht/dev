<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testing.aspx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldTypeIntegerControl.testing" %>

<%@ Register src="NASCustomFieldTypeIntegerControl.ascx" tagname="NASCustomFieldTypeIntegerControl" tagprefix="uc1" %>

<%@ Register src="../NASCustomFieldTypeSingleSelectionListControl/NASCustomFieldTypeSingleSelectionListControl.ascx" tagname="NASCustomFieldTypeSingleSelectionListControl" tagprefix="uc2" %>

<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../../../../scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="../../../../../scripts/shortcut.js" type="text/javascript"></script>
    <script src="../../../../../scripts/script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var flg = 0;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxCallbackPanel ID="cpDemo" ClientInstanceName="cpDemo" runat="server" Width="200px" 
            oncallback="cpDemo_Callback">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    
                    <dx:ASPxGridLookup ID="grdLookupDemo" runat="server" 
                        AutoGenerateColumns="False" DataSourceID="grdLookupDemoXDS" 
                        KeyFieldName="ObjectTypeId">
                        <ClientSideEvents TextChanged="function(s, e){
                            alert('TextChanged');
                            flg = 1;
                        }" LostFocus="function(s, e){
                            if (flg == 0)
                                alert('ButtonClick');
                        }" />
                        <GridViewProperties>
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="false" />
                            <Templates>
                            <StatusBar>
                                <table class="OptionsTable" style="float: right">
                                    <tr>
                                        <td onclick="return _aspxCancelBubble(event)">
                                            <dx:ASPxButton ID="close" runat="server" AutoPostBack="false" Text="apply" ClientSideEvents-Click="function(s,e)" />
                                        </td>
                                    </tr>
                                 </table>
                            </StatusBar>
                        </Templates>
                         <Settings ShowFilterRow="True" ShowStatusBar="Visible" />
                        </GridViewProperties>
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" />
                            <%--<dx:GridViewDataTextColumn FieldName="ObjectTypeId" ReadOnly="True" 
                                ShowInCustomizationForm="True" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>--%>
                            <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" 
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Description" 
                                ShowInCustomizationForm="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="RowCreationTimeStamp" 
                                ShowInCustomizationForm="True" VisibleIndex="3">
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" 
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridLookup>
                    <dx:XpoDataSource ID="grdLookupDemoXDS" runat="server" DefaultSorting="" 
                        TypeName="NAS.DAL.CMS.ObjectDocument.ObjectType">
                    </dx:XpoDataSource>
                    
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

        <%--<uc2:NASCustomFieldTypeSingleSelectionListControl ID="NASCustomFieldTypeSingleSelectionListControl1" 
            runat="server" />--%>

    </div>
    </form>
</body>
</html>
