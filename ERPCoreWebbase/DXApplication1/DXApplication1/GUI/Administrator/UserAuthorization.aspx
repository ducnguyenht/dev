<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserAuthorization.aspx.cs" Inherits="DXApplication1.GUI.Administrator.UserAuthorization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .float-right
        {
            float: right;
        }
        .margin-right-10
        {
            margin-right: 10px;
        }
        table
        {
            /*border-collapse:collapse !important;*/
        }
    </style>
    <script type="text/javascript">
        function cbSelectAllUsers_CheckedChanged(s, e) {
            grdUser.SelectAllRowsOnPage(s.GetChecked());
        }
        function cbSelectAllRoles_CheckedChanged(s, e) {
            grdRolesOfOganization.SelectAllRowsOnPage(s.GetChecked());
        }
        function imgShowRoles_Click(s, e) {
            pcRolesOfOrganization.Show();
        }
        function btnClose_pcRolesOfOrganization_Click(s, e) {
            pcRolesOfOrganization.Hide();
        }
        function pcRolesOfOrganization_CloseUp(s, e) {
            //alert("I'm hided.");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxSplitter ID="ASPxSplitter3" runat="server" Height="100%" Orientation="Vertical"
        Width="100%" SeparatorVisible="False">
        <Border BorderWidth="0px" />
        <Styles>
            <Pane>
                <Paddings Padding="0px"></Paddings>
                <Border BorderWidth="0px" />
            </Pane>
        </Styles>
        <Panes>
            <dx:SplitterPane MaxSize="80px" MinSize="80px" Size="80px">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitle="Tổ chức"
                            ParentTitleSize="16px" Style="display: block;" Title="Phân quyền người dùng"
                            TitleSize="26px" />
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane>
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxSplitter ID="ASPxSplitter2" runat="server" Height="100%" Width="100%">
                            <Panes>
                                <dx:SplitterPane MaxSize="400px" ScrollBars="Auto" Size="220px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxTreeList ID="trlOrganization" runat="server" AutoGenerateColumns="False"
                                                Height="100%" KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId"
                                                Width="100%">
                                                <Columns>
                                                    <dx:TreeListTextColumn Caption="Tổ chức" FieldName="Name" ShowInCustomizationForm="True"
                                                        VisibleIndex="0">
                                                    </dx:TreeListTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowDragDrop="False" AllowFocusedNode="True" AutoExpandAllNodes="True"
                                                    FocusNodeOnExpandButtonClick="False" />
                                                <Border BorderWidth="0px" />
                                            </dx:ASPxTreeList>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCallbackPanel ID="cpContent" runat="server" ClientInstanceName="cpContent"
                                                Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxPanel ID="pnCommands" runat="server" Width="100%">
                                                            <Paddings PaddingBottom="6px" PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" />
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxButton AutoPostBack="false" ID="ASPxButton2" runat="server" Text="Chọn vai trò">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Role" />
                                                                        </Image>
                                                                        <ClientSideEvents Click="imgShowRoles_Click" />
                                                                    </dx:ASPxButton>
                                                               <%--     <dx:ASPxImage ID="imgShowRoles" runat="server" Cursor="pointer"
                                                                        ImageUrl="~/images/NASID/role.png" IsPng="True" ToolTip="Chọn vai trò">
                                                                        <ClientSideEvents Click="imgShowRoles_Click" />
                                                                        <Border BorderColor="#CCCCCC" BorderWidth="1px" />
                                                                    </dx:ASPxImage>--%>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxPanel>
                                                        <dx:ASPxGridView ID="grdUser" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdUser"
                                                            KeyFieldName="OrganizationUserId" Style="margin-left: 0px" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                                    VisibleIndex="0" Width="40px" ButtonType="Image">
                                                                    <ClearFilterButton Visible="True">
                                                                        <Image ToolTip="Xóa">
                                                                            <SpriteProperties CssClass="Sprite_Clear" />
                                                                        </Image>
                                                                    </ClearFilterButton>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <HeaderTemplate>
                                                                        <dx:ASPxCheckBox ID="cbSelectAllUsers" runat="server" ToolTip="Chọn/Bỏ chọn tất cả các hàng trong trang">
                                                                            <ClientSideEvents CheckedChanged="cbSelectAllUsers_CheckedChanged" />
                                                                        </dx:ASPxCheckBox>
                                                                    </HeaderTemplate>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Chức danh" FieldName="JobTitle" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Họ tên" FieldName="FullName" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataCheckColumn Caption="Quản trị viên" FieldName="isAdmin" ReadOnly="True"
                                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                                </dx:GridViewDataCheckColumn>
                                                            </Columns>
                                                            <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                            <SettingsDetail ShowDetailRow="True" />
                                                            <Templates>
                                                                <DetailRow>
                                                                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
                                                                        Width="100%">
                                                                        <TabPages>
                                                                            <dx:TabPage Text="Danh sách vai trò">
                                                                                <ContentCollection>
                                                                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxGridView ID="grdRolesOfUser" runat="server" AutoGenerateColumns="False" KeyFieldName="UserRoleId"
                                                                                            OnBeforePerformDataSelect="grdRolesOfUser_BeforePerformDataSelect" OnLoad="grdRolesOfUser_Load"
                                                                                            Width="100%">
                                                                                            <Columns>
                                                                                                <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                                    Width="60px" ButtonType="Image">
                                                                                                    <DeleteButton Text="Xóa" Visible="True">
                                                                                                        <Image ToolTip="Xóa">
                                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                        </Image>
                                                                                                    </DeleteButton>
                                                                                                </dx:GridViewCommandColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="Tên vai trò" FieldName="Name" ShowInCustomizationForm="True"
                                                                                                    VisibleIndex="1">
                                                                                                </dx:GridViewDataTextColumn>
                                                                                                <dx:GridViewDataTextColumn Caption="Chi tiết" ShowInCustomizationForm="True" 
                                                                                                    VisibleIndex="3" Width="60px">
                                                                                                    <DataItemTemplate>
                                                                                                        <dx:ASPxHyperLink ID="lnkRoleDetails0" runat="server" OnInit="lnkRoleDetails_Init1"
                                                                                                            Text="Xem" Cursor="pointer" ImageUrl="~/images/icon/16x16/ico-details.png" 
                                                                                                            ToolTip="Xem">
                                                                                                            <ClientSideEvents Click="lnkRoleDetails_Click" />
                                                                                                        </dx:ASPxHyperLink>
                                                                                                    </DataItemTemplate>
                                                                                                    <CellStyle HorizontalAlign="Center">
                                                                                                    </CellStyle>
                                                                                                </dx:GridViewDataTextColumn>
                                                                                            </Columns>
                                                                                            <SettingsBehavior ConfirmDelete="True" />
                                                                                        </dx:ASPxGridView>
                                                                                    </dx:ContentControl>
                                                                                </ContentCollection>
                                                                            </dx:TabPage>
                                                                        </TabPages>
                                                                    </dx:ASPxPageControl>
                                                                </DetailRow>
                                                            </Templates>
                                                            <Border BorderWidth="0px" />
                                                            <BorderTop BorderWidth="1px" />
                                                            <BorderBottom BorderWidth="1px" />
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxCallbackPanel>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                            <Styles>
                                <Pane>
                                    <Paddings Padding="0px" />
                                    <BorderLeft BorderWidth="0px" />
                                    <BorderRight BorderWidth="0px" />
                                    <BorderBottom BorderWidth="0px" />
                                </Pane>
                                <Separator>
                                    <Border BorderWidth="1px" />
                                    <BorderBottom BorderWidth="0px" />
                                </Separator>
                            </Styles>
                            <Border BorderWidth="0px" />
                            <BorderBottom BorderWidth="0px" />
                        </dx:ASPxSplitter>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
            </Pane>
        </Styles>
    </dx:ASPxSplitter>
    <dx:ASPxPopupControl ID="pcRolesOfOrganization" runat="server" RenderMode="Lightweight"
        ClientInstanceName="pcRolesOfOrganization" HeaderText="Danh sách vai trò" Height="500px"
        Width="760px" AllowDragging="True" AllowResize="True" AutoUpdatePosition="True"
        CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="16px" Text="Các vai trò của"
                    Font-Bold="False" ForeColor="#333333">
                </dx:ASPxLabel>
                &nbsp;<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Size="16px" Text="Công ty Cổ phần Dược Vật tư y tế Quảng Nam (QUASAPHARCO)">
                </dx:ASPxLabel>
                <br />
                <br />
                <dx:ASPxGridView ID="grdRolesOfOganization" ClientInstanceName="grdRolesOfOganization"
                    runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="OrganizationRoleId">
                    <Columns>
                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                            VisibleIndex="0" Width="40px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="cbSelectAllRoles" runat="server">
                                    <ClientSideEvents CheckedChanged="cbSelectAllRoles_CheckedChanged" />
                                </dx:ASPxCheckBox>
                            </HeaderTemplate>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="Tên vai trò" FieldName="Name" ShowInCustomizationForm="True"
                            VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Chi tiết" ShowInCustomizationForm="True" 
                            VisibleIndex="2" Width="60px">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="lnkRoleDetails" runat="server" Text="Xem" 
                                    OnInit="lnkRoleDetails_Init" Cursor="pointer" 
                                    ImageUrl="~/images/icon/16x16/ico-details.png" ToolTip="Xem">
                                    <ClientSideEvents Click="lnkRoleDetails_Click" />
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center" VerticalAlign="Bottom">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
                <div style="position: absolute; bottom: 0; right: 0; left: 0; padding: 10px; border-top: 1px solid #ccc">
                    <dx:ASPxButton AutoPostBack="false" CssClass="float-right" ID="ASPxButton1" runat="server"
                        Text="Phân quyền">
                        <Image>
                            <SpriteProperties CssClass="Sprite_AssignTo" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton AutoPostBack="false" CssClass="float-right margin-right-10" ID="btnClose_pcRolesOfOrganization"
                        runat="server" Text="Bỏ qua">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                        <ClientSideEvents Click="btnClose_pcRolesOfOrganization_Click" />
                    </dx:ASPxButton>
                </div>
                
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents CloseUp="pcRolesOfOrganization_CloseUp" />
    </dx:ASPxPopupControl>
    <dx:ASPxPopupControl ID="pcRoleDetails" runat="server" HeaderText="Chi tiết vai trò"
                    Height="398px" RenderMode="Lightweight" Width="685px" ClientInstanceName="pcRoleDetails"
                    AllowDragging="True" AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="False" Font-Size="16px" ForeColor="Black"
                                Text="Chi tiết vai trò Quản lý chứng từ">
                            </dx:ASPxLabel>
                            <br />
                            <br />
                            <dx:ASPxGridView ID="grdAccessObjectsOfRole" runat="server" Width="100%" AutoGenerateColumns="False"
                                KeyFieldName="AccessObjectId">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Tên chức năng" FieldName="Name" ShowInCustomizationForm="True"
                                        VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                <Templates>
                                    <DetailRow>
                                        <dx:ASPxGridView ID="grdPermissions" runat="server" AutoGenerateColumns="False" OnBeforePerformDataSelect="grdPermissions_BeforePerformDataSelect"
                                            Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Tên thao tác" FieldName="Name" VisibleIndex="0">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataCheckColumn Caption="Cho phép" FieldName="Authorization" ReadOnly="True"
                                                    VisibleIndex="1">
                                                </dx:GridViewDataCheckColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </DetailRow>
                                </Templates>
                            </dx:ASPxGridView>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
</asp:Content>
