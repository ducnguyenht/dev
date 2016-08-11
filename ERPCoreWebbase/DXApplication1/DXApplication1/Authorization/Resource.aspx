<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Resource.aspx.cs" Inherits="WebModule.Authorization.Resource" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdResource_EndCallback(s,e) {
        if (s.cpRefresh != '') {            
            //grdResource.Refresh();
            delete s.cpRefresh;
        }
    }

</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Sơ Đồ Tài Nguyên" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
    <dx:ASPxTreeList ID="grdResource" runat="server" AutoGenerateColumns="False" 
    ClientInstanceName="grdResource" DataSourceID="ResourceXDS" 
    KeyFieldName="AppComponentId" ParentFieldName="ParentAppComponentId!Key" Width="100%" 
        onnodedeleting="grdResource_NodeDeleting" 
        onnodeinserting="grdResource_NodeInserting" 
        onnodeupdating="grdResource_NodeUpdating" 
        oncelleditorinitialize="grdResource_CellEditorInitialize" 
        onnodevalidating="grdResource_NodeValidating">
        <Columns>
            <dx:TreeListTextColumn FieldName="AppComponentId" VisibleIndex="0" Visible="False" 
                Width="0px">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="ParentAppComponentId!Key" 
                VisibleIndex="1" Visible="False">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="Code" VisibleIndex="2" Width="150px" 
                Caption="Mã Tài Nguyên">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="Name" VisibleIndex="3" 
                Caption="Tên Tài Nguyên">
            </dx:TreeListTextColumn>
            <dx:TreeListTextColumn FieldName="Description" VisibleIndex="4" Width="200px" 
                Caption="Diễn Giải">
            </dx:TreeListTextColumn>
            <dx:TreeListComboBoxColumn FieldName="RowStatus" VisibleIndex="5" Width="100px" 
                Caption="Trạng Thái">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Sử dụng" Value="1" />
                        <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                    </Items>
                </PropertiesComboBox>
            </dx:TreeListComboBoxColumn>
            <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao Tác" 
                VisibleIndex="7" Width="100px" ShowNewButtonInHeader="true">
                <EditButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                    </Image>
                </EditButton>
                <NewButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                    </Image>
                </NewButton>
                <DeleteButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                    </Image>
                </DeleteButton>
                <UpdateButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                    </Image>
                </UpdateButton>
                <CancelButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                    </Image>
                </CancelButton>
            </dx:TreeListCommandColumn>
        </Columns>
        <SettingsBehavior AllowFocusedNode="True" ExpandCollapseAction="NodeDblClick" />

<SettingsBehavior AllowFocusedNode="True"></SettingsBehavior>
        <ClientSideEvents EndCallback="grdResource_EndCallback" />
</dx:ASPxTreeList>
<dx:XpoDataSource ID="ResourceXDS" runat="server" 
    TypeName="NAS.DAL.System.Resource.AppComponent" Criteria="[RowStatus] &gt;= 1">
</dx:XpoDataSource>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LeftSubmitContainer" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CenterSubmitContainer" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightSubmitContainer" runat="server">
</asp:Content>
