<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="UserManagement.aspx.cs" Inherits="WebModule.NAANAdmin.Authorization.UserManagement" %>

<%@ Register Src="~/Sales/UserControl/chitiet_user.ascx" TagName="chitiet_user" TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/invite_newuser.ascx" TagName="invite_newuser"
    TagPrefix="uc2" %>
<%@ Register Src="~/NAANAdmin/Authorization/UserControl/uPopupUserEditting.ascx"
    TagName="uPopupUserEditting" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        table, tr, td, ul, li, div
        {
            border-collapse: collapse;
        }
        .float_center
        {
            margin-left: 50%;
        }
    </style>
    <script type="text/javascript">
        var clientAction = '';
        function Adjust_spltUserManagement() {
            spltUserManagement.SetWidth(pagUser.GetWidth() - 27);
            spltUserManagement.SetHeight(ERPCore.GetContentPane().GetClientHeight() - $("#<%= titlePage.ClientID %>").outerHeight(true) - 74);
        }

        function pagUserManagement_ActiveTabChanged(s, e) {
            ASPxClientControl.AdjustControls();
            if (e.tab.index == 0) {
                btnInviteUser.SetVisible(true);
                ERPCore.SubmitContainer_Expand();
            }
            else {
                btnInviteUser.SetVisible(false);
                ERPCore.SubmitContainer_Collapse();
                Adjust_spltUserManagement();
            }
        }

        function grd_ListUser_EndCallback(s, e) {
            if (s.cpRefresh) {
                grd_ListUser.Refresh();
            }
            //            if (s.cpListUser) {
            //                if (s.cpListUser == 'note') {
            //                    var key = s.GetRowKey(e.visibleIndex);
            //                    var message = confirm("Người dùng này hiện đang là nhân viên. Bạn có chắc chắn muốn xóa không?");
            //                    if (message) {
            //                        var params = new Array('Delete', key,"old");
            //                        grd_ListUser.PerformCallback(params);
            //                    } else
            //                        LoadingPanelCombineMaterial.Hide();                    
            //                }
            //            }
            LoadingPanelCombineMaterial.Hide();
        }

        function personcbo_SelectedIndexChanged(s, e) {
            var person = s.GetSelectedItem("Name");
            if (person != null) {
                var name = person.GetColumnText("Name");
                var editor = grd_ListUser.GetEditor('PersonName');
                editor.SetValue(name);

                var des = supp.GetColumnText("Description");
                editor = grd_ListUser.GetEditor('Description');
                editor.SetValue(des);
            }
        }

        function grd_ListUser_CustomButtonClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);
            LoadingPanelCombineMaterial.Show();
            if (e.buttonID == 'AddPerson') {
                var selectedDepartment = trlDepartmentMenu.GetFocusedNodeKey();
                var params = new Array('Add', selectedDepartment);
                clientAction = 'Add';
                cpPersonEdit.PerformCallback(params);
            }
            else if (e.buttonID == 'EditPerson') {
                UpdatePersonAction(key);
            }
            else if (e.buttonID == 'DeletePerson') {
                DeletePersonAction(key);
            }
        }

        function UpdatePersonAction(values) {
            clientAction = 'Edit';
            var params = new Array(clientAction, values);
            cpPersonEdit.PerformCallback(params);
        }

        function DeletePersonAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                clientAction = 'Delete';
                var params = new Array(clientAction, values);
                grd_ListUser.PerformCallback(params);
            } else
                LoadingPanelCombineMaterial.Hide();
        }

        function btnAddPersonclick(s, e) {
            cpPersonEdit.PerformCallback('Add');
        }

        //        function trlDepartmentMenu_OnGetNodeValues(values) {
        //            var recordId = values[0];
        //        }


        function grd_ListUser_OnGetRowValues(values) {
            var recordId = values[0];
        }
        function grd_ListUser_Init(s, e) {
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                LoadingPanelCombineMaterial.Show();
                var selectedDepartment = trlDepartmentMenu.GetFocusedNodeKey();
                var params = new Array('Add', selectedDepartment);
                cpPersonEdit.PerformCallback(params);
            });
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                if (idxFocus < 0)
                    return;
                var idxFocus = s.GetFocusedRowIndex();
                var key = s.GetRowKey(idxFocus);
                LoadingPanelCombineMaterial.Show();
                UpdatePersonAction(key);
            });
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var idxFocus = s.GetFocusedRowIndex();
                if (idxFocus < 0)
                    return;
                LoadingPanelCombineMaterial.Show();
                var key = s.GetRowKey(idxFocus);
                DeletePersonAction(key);
            });
            s.GetMainElement().focus();
        }

        function trlDepartmentMenu_FocusedNodeChanged(s, e) {
            LoadingPanelCombineMaterial.Show();
            grd_ListUser.PerformCallback("Refresh");
        }
        function trlDepartmentMenu_NodeClick(s, e) {
            LoadingPanelCombineMaterial.Show();
            grd_ListUser.PerformCallback('Refresh');
        }
        function trlDepartmentMenu_SelectionChanged(s, e) {

            LoadingPanelCombineMaterial.Show();
            grd_ListUser.PerformCallback('Refresh');
        }
        function trlDepartmentMenu_Init(s, e) {
            grd_ListUser.PerformCallback('Refresh');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLoadingPanel ID="LoadingPanelCombineMaterial" runat="server" ClientInstanceName="LoadingPanelCombineMaterial"
        Modal="True" ShowImage="true" Text="Đang xử lý">
        <LoadingDivStyle BackColor="Transparent">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <uc3:uPopupUserEditting ID="uPopupUserEditting1" runat="server" />
    <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitleSize="16px"
        Style="display: block;" Title="Người dùng" TitleSize="26px" />
    <dx:ASPxPageControl ClientInstanceName="pagUser" ID="ASPxPageControl1" runat="server"
        ActiveTabIndex="1" Width="97%" RenderMode="Lightweight" Height="478px">
        <TabPages>
            <dx:TabPage Text="Mời người dùng" Visible="false">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <uc2:invite_newuser ID="invite_newuser1" runat="server" />
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Danh mục người dùng">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="Department" runat="server" Criteria="[RowStatus] > 0 And [OrganizationId.OrganizationId] = ?"
                            TypeName="NAS.DAL.Nomenclature.Organization.Department">
                            <CriteriaParameters>
                                <asp:Parameter Name="OrganizationId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:ASPxSplitter ID="ASPxSplitter2" Width="100%" ClientInstanceName="spltUserManagement"
                            runat="server" Height="478px">
                            <Panes>
                                <dx:SplitterPane Size="200px" MaxSize="300px" ScrollBars="Auto">
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxTreeList ID="trlDepartmentMenu" ClientInstanceName="trlDepartmentMenu" runat="server"
                                                AutoGenerateColumns="False" Height="100%" KeyFieldName="DepartmentId" ParentFieldName="ParentDepartmentId!Key"
                                                Width="100%" DataSourceID="Department" KeyboardSupport="true" >
                                                <ClientSideEvents NodeClick="trlDepartmentMenu_NodeClick" FocusedNodeChanged="trlDepartmentMenu_FocusedNodeChanged"
                                                    Init="trlDepartmentMenu_Init"/>
                                                <Border BorderWidth="0px"></Border>
                                                <Columns>
                                                    <dx:TreeListTextColumn Name="tl_Name" Caption="Phòng ban" FieldName="Name" ShowInCustomizationForm="True"
                                                        VisibleIndex="0">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:TreeListTextColumn>
                                                </Columns>
                                                <SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True" AllowDragDrop="False"
                                                    FocusNodeOnExpandButtonClick="true" />
                                                <SettingsEditing Mode="EditFormAndDisplayNode" />
                                                <Border BorderWidth="0px" />
                                            </dx:ASPxTreeList>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane ScrollBars="Auto">
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="grd_ListUser" runat="server" AutoGenerateColumns="False" KeyFieldName="PersonId"
                                                Width="100%" ClientInstanceName="grd_ListUser" OnCustomCallback="grd_ListUser_CustomCallback"
                                                KeyboardSupport="true">
                                                <ClientSideEvents EndCallback="grd_ListUser_EndCallback" CustomButtonClick="grd_ListUser_CustomButtonClick"
                                                    Init="grd_ListUser_Init" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" FieldName="PersonId"
                                                        ReadOnly="True" Visible="False" Width="1%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã người dùng" FieldName="Code" ShowInCustomizationForm="True"
                                                        VisibleIndex="1" Width="30%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle Wrap="True">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Họ và tên" ShowInCustomizationForm="True" VisibleIndex="2"
                                                        FieldName="Name" Width="45%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle Wrap="True">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True"
                                                        VisibleIndex="3" Visible="false" Width="45%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle Wrap="True">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn FieldName="RowCreationTimeStamp" ShowInCustomizationForm="True"
                                                        Visible="False" VisibleIndex="4" Width="1%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Width="15%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <PropertiesComboBox>
                                                            <Items>
                                                                <dx:ListEditItem Text="Đang hoạt động" Value="1" />
                                                                <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                                                            </Items>
                                                        </PropertiesComboBox>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewCommandColumn Caption="Thao tác" ButtonType="Image" Width="10%">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="EditPerson">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="AddPerson">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="DeletePerson">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="true" ConfirmDelete="true" />
                                                <SettingsEditing Mode="Inline" />
                                                <Settings ShowFilterBar="Visible" ShowFilterRow="True" ShowFilterRowMenu="True" />
                                                <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa?" FilterBarCreateFilter="Tạo bộ lọc"
                                                    EmptyDataRow="Không có dữ liệu" />
                                                <Styles>
                                                    <CommandColumn Spacing="2px">
                                                    </CommandColumn>
                                                </Styles>
                                                <BorderLeft BorderWidth="0px" />
                                                <BorderTop BorderWidth="0px" />
                                                <BorderRight BorderWidth="0px" />
                                                <Templates>
                                                    <EmptyDataRow>
                                                        <div style="width: 100%;">
                                                            <dx:ASPxButton ID="btnNew" AutoPostBack="false" runat="server" CssClass="float_center"
                                                                Image-SpriteProperties-CssClass="Sprite_New" ClientInstanceName="btnNew" BackgroundImage-Repeat="NoRepeat">
                                                                <ClientSideEvents Click="function(s, e){                                                                    
                                                                    var selectedDepartment = trlDepartmentMenu.GetFocusedNodeKey();
                                                                    var params = new Array('Add', selectedDepartment);
                                                                    cpPersonEdit.PerformCallback(params);
                                                                }" />
                                                            </dx:ASPxButton>
                                                        </div>
                                                    </EmptyDataRow>
                                                </Templates>
                                            </dx:ASPxGridView>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                            <Styles>
                                <Pane>
                                    <Paddings Padding="0px"></Paddings>
                                </Pane>
                            </Styles>
                        </dx:ASPxSplitter>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ClientSideEvents ActiveTabChanged="pagUserManagement_ActiveTabChanged" />
        <Paddings PaddingBottom="0" />
        <ContentStyle>
            <BorderLeft BorderWidth="0px" />
            <BorderRight BorderWidth="0px" />
            <BorderBottom BorderWidth="0px" />
        </ContentStyle>
    </dx:ASPxPageControl>
    <dx:ASPxPopupControl ID="popup_detail" runat="server" ClientInstanceName="popup_detail"
        RenderMode="Lightweight" AllowDragging="True" AllowResize="True" HeaderText="Thông tin người dùng"
        Modal="True" Width="500px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc1:chitiet_user ID="chitiet_user1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CenterSubmitContainer">
    <dx:ASPxButton ID="btnInviteUser" ClientInstanceName="btnInviteUser" runat="server"
        Text="Mời" AutoPostBack="False" Visible="false">
        <ClientSideEvents Click="function(s, e) {            
            cp_InviteUser.PerformCallback('insertUserInvite');
        }" />
    </dx:ASPxButton>
</asp:Content>
