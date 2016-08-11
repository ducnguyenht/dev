<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="UserManagement.aspx.cs" Inherits="WebModule.Branch.UserManagement" %>

<%@ Register Src="~/Sales/UserControl/chitiet_user.ascx" TagName="chitiet_user" TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/invite_newuser.ascx" TagName="invite_newuser" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        table, tr, td, ul, li, div
        {
            border-collapse: collapse;
        }
    </style>
    <script type="text/javascript">
        function Adjust_pagUser() {
            pagUser.SetHeight(ERPCore.GetContentPane().GetClientHeight() - $("#<%= titlePage.ClientID %>").outerHeight(true) - 15);
        }
        function Adjust_spltUserManagement() {
            spltUserManagement.SetWidth(pagUser.GetWidth() - 27);
            spltUserManagement.SetHeight(pagUser.GetHeight() - 50);
        }
        function pagUserManagement_Init(s, e) {
            //Adjust_pagUser();
        }
        function pagUserManagement_ActiveTabChanged(s, e) {
            ASPxClientControl.AdjustControls();
            if (e.tab.index == 0) {
                btnInviteUser.SetVisible(true);
                ERPCore.SubmitContainer_Expand();
                Adjust_pagUser();
            }
            else {
                btnInviteUser.SetVisible(false);
                ERPCore.SubmitContainer_Collapse();
                Adjust_pagUser();
                Adjust_spltUserManagement();
            }
        }
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    
    <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitleSize="16px"
        Style="display: block;" Title="Người dùng" TitleSize="26px" />
    <div id="div_UserManagement" style="padding: 0 10px;">
        <dx:ASPxPageControl ClientInstanceName="pagUser" ID="ASPxPageControl1" runat="server"
            ActiveTabIndex="0" Width="100%" RenderMode="Lightweight">
            <TabPages>
                <dx:TabPage Text="Mời người dùng">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <uc2:invite_newuser ID="invite_newuser1" runat="server" />
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="Danh mục người dùng">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxSplitter ID="ASPxSplitter2" Width="100%" ClientInstanceName="spltUserManagement" runat="server" Height="100%">
                                <Panes>
                                    <dx:SplitterPane Size="200px" MaxSize="300px" ScrollBars="Auto">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
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
                                    <dx:SplitterPane ScrollBars="Auto">
                                        <ContentCollection>
                                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                Danh mục người dùng
                                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="Username"
                                                    Width="100%">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
                                                    if (e.buttonID == 'view')
                                                        popup_detail.Show();
                                                }" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="6">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="change_status" Text="Kích hoạt/Tạm ngưng">
                                                                    <Image ToolTip="Duyệt">
                                                                        <SpriteProperties CssClass="Sprite_Approve" />
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="delete" Text="Xóa">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="reset_pwd" Text="Khôi phục mật khẩu">
                                                                    <Image ToolTip="Khôi phục mật khẩu">
                                                                        <SpriteProperties CssClass="Sprite_Refresh" />
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="view" Text="Xem chi tiết">
                                                                    <Image ToolTip="Xem chi tiết">
                                                                        <SpriteProperties CssClass="Sprite_Info" />
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Họ tên" FieldName="Fullname" ShowInCustomizationForm="True"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Email" FieldName="Username" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Phòng ban" FieldName="Role" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="Status" ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Chức danh" FieldName="Grade" ShowInCustomizationForm="True"
                                                            VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Lần truy xuất gần nhất" FieldName="AccessTime"
                                                            ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <BorderLeft BorderWidth="0px" />
                                                    <BorderRight BorderWidth="0px" />
                                                </dx:ASPxGridView>
                                            </dx:SplitterContentControl>
                                        </ContentCollection>
                                    </dx:SplitterPane>
                                </Panes>
                                <Styles>
                                    <Pane>
                                        <Paddings Padding="0" />
                                    </Pane>
                                </Styles>
                            </dx:ASPxSplitter>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
            <ClientSideEvents Init="pagUserManagement_Init" ActiveTabChanged="pagUserManagement_ActiveTabChanged" />
            <Paddings PaddingBottom="0" />
        </dx:ASPxPageControl>
 </div>
    <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="popup_detail"
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
    <dx:ASPxButton ID="ASPxButton1" ClientInstanceName="btnInviteUser" runat="server"
        Text="Mời">
    </dx:ASPxButton>
</asp:Content>
