<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uResource.ascx.cs" Inherits="WebModule.Authorization.UserControl.uResource" %>
<script type="text/javascript">
    function grdResource_EndCallback(s, e) {
        if (s.cpRefresh != '') {
            //grdResource.Refresh();
            delete s.cpRefresh;
        }
    }

</script>
    <dx:XpoDataSource ID="ResourceXDS" runat="server" 
    TypeName="NAS.DAL.System.Resource.AppComponent" Criteria="">
</dx:XpoDataSource>
<dx:ASPxHiddenField ID="hdId" runat="server" ClientInstanceName="hdId">
</dx:ASPxHiddenField>


<dx:ASPxPopupControl ID="popup_application" runat="server" AllowDragging="True" 
    AllowResize="True" ClientInstanceName="popup_application" 
    CloseAction="CloseButton" HeaderText="Sơ đồ tài nguyên" Height="584px" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    RenderMode="Lightweight" ScrollBars="Auto" Width="878px">
    <ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxCallbackPanel ID="cpResourceLine" runat="server" 
        ClientInstanceName="cpResourceLine" OnCallback="cpResourceLine_Callback" 
        Width="100%">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxTreeList ID="grdResource" runat="server" AutoGenerateColumns="False" 
                    ClientInstanceName="grdResource" DataSourceID="ResourceXDS" 
                    KeyFieldName="AppComponentId" 
                    OnCellEditorInitialize="grdResource_CellEditorInitialize" 
                    OnNodeDeleting="grdResource_NodeDeleting" 
                    OnNodeInserting="grdResource_NodeInserting" 
                    OnNodeUpdating="grdResource_NodeUpdating" 
                    OnNodeValidating="grdResource_NodeValidating" 
                    ParentFieldName="ParentAppComponentId!Key" Width="100%">
                    <Columns>
                        <dx:TreeListTextColumn FieldName="AppComponentId" 
                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" 
                            Width="0px" ReadOnly="True">
                            <PropertiesTextEdit Width="0px">
                            </PropertiesTextEdit>
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="AppId!Key" ReadOnly="True" 
                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn FieldName="ParentAppComponentId!Key" 
                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Caption="Mã Tài Nguyên" FieldName="Code" 
                            ShowInCustomizationForm="True" VisibleIndex="3" Width="150px">
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Caption="Tên Tài Nguyên" FieldName="Name" 
                            ShowInCustomizationForm="True" VisibleIndex="4">
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="Description" 
                            ShowInCustomizationForm="True" VisibleIndex="5" Width="200px">
                        </dx:TreeListTextColumn>
                        <dx:TreeListComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" 
                            ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                            <PropertiesComboBox>
                                <Items>
                                    <dx:ListEditItem Text="Sử dụng" Value="1" />
                                    <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                                </Items>
                            </PropertiesComboBox>
                        </dx:TreeListComboBoxColumn>
                        <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao Tác" 
                            ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="7" 
                            Width="100px">
                            <EditButton Visible="True">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </EditButton>
                            <NewButton Visible="True">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_New" />
                                </Image>
                            </NewButton>
                            <DeleteButton Visible="True">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </DeleteButton>
                            <UpdateButton>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </UpdateButton>
                            <CancelButton>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </CancelButton>
                        </dx:TreeListCommandColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedNode="True" ExpandCollapseAction="NodeDblClick" />
                    <ClientSideEvents EndCallback="grdResource_EndCallback" />
                </dx:ASPxTreeList>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
