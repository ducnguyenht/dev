<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uAuthorization.ascx.cs"
    Inherits="WebModule.NAANAdmin.Authorization.UserControl.uAuthorization" %>
<script type="text/javascript">
    function popup_authorization_AfterResizing(s, e) {
        ASPxClientControl.AdjustControls();
        Adjust_splt_Authorization_User();
        Adjust_Authorization_trlDepartment();
    }
    function Adjust_splt_Authorization_User() {
        splt_Authorization_User.SetHeight(pag_Authorization.GetHeight() - 50);
        splt_Authorization_User.SetWidth(pag_Authorization.GetWidth() - 24);
    }
    function Adjust_Authorization_trlDepartment() {
        //trlDepartment.SetHeight(pag_Authorization.GetHeight() - 50);
        trlDepartment.SetWidth(pag_Authorization.GetWidth() - 24);
    }
    function popup_authorization_Show(s, e) {
        Adjust_splt_Authorization_User();
        Adjust_Authorization_trlDepartment();
    }
</script>
<dx:ASPxPopupControl ID="popup_edit" runat="server" MinHeight="515px" MinWidth="725px" Width="800px" Height="540px"
    HeaderText="Phân quyền cho tổ chức và người sử dụng" ClientInstanceName="popup_authorization"
    AllowResize="True" RenderMode="Lightweight" AllowDragging="true" Modal="True" PopupHorizontalAlign="WindowCenter"
    ShowFooter="true" ScrollBars="Auto" ShowMaximizeButton="true" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="ASPxPageControl1" ClientInstanceName="pag_Authorization" runat="server" RenderMode="Lightweight" ActiveTabIndex="0"
                Width="100%" Height="100%" EnableTabScrolling="True">
                <TabPages>
                    <dx:TabPage Text="Tổ chức">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxTreeList ID="trlDepartment" ClientInstanceName="trlDepartment" runat="server" AutoGenerateColumns="False" KeyFieldName="OrganizationId"
                                    ParentFieldName="ParentOrganizationId" Width="100%" Height="340px">
                                    <Columns>
                                        <dx:TreeListCheckColumn Caption="Được quyền" FieldName="isAlow" ShowInCustomizationForm="True"
                                            VisibleIndex="0" Width="100px">
                                            <HeaderCaptionTemplate>
                                                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Text="Được quyền">
                                                </dx:ASPxCheckBox>
                                            </HeaderCaptionTemplate>
                                        </dx:TreeListCheckColumn>
                                        <dx:TreeListTextColumn Caption="Mã số" FieldName="id" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="Tên tổ chức" FieldName="name" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="Thể loại" FieldName="type" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="Người quản trị" FieldName="admin" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="Trạng thái" FieldName="status" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:TreeListTextColumn>
                                    </Columns>
                                    <Settings VerticalScrollBarMode="Auto" ScrollableHeight="340" />
                                </dx:ASPxTreeList>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Người sử dụng">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxSplitter ID="ASPxSplitter2" ClientInstanceName="splt_Authorization_User" runat="server" Height="100%" Width="100%">
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
                                                    <h2>Danh mục người dùng</h2>
                                                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="Username"
                                                        Width="100%">
                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
                                                    if (e.buttonID == 'view')
                                                        popup_detail.Show();
                                                }" />
                                                        <Columns>
                                                            <dx:GridViewDataCheckColumn Caption="Dược" FieldName="isAlow" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                                <HeaderTemplate>
                                                                    <dx:ASPxCheckBox ID="ASPxCheckBox2" runat="server" Text="Được quyền">
                                                                    </dx:ASPxCheckBox>
                                                                </HeaderTemplate>
                                                            </dx:GridViewDataCheckColumn>
                                                            <dx:GridViewDataTextColumn Caption="Họ tên" FieldName="Fullname" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Email" FieldName="Username" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Phòng ban" FieldName="Role" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="Status" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Chức danh" FieldName="Grade" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
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
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterTemplate>
        <div style="padding: 10px;">
            <div class="float-left">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                        <Image ToolTip="Trợ giúp">
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="float-right">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" ClientInstanceName="buttonSave"
                        Text="Lưu Lại">
                        <ClientSideEvents Click="buttonSave_Click" />
                        <Image ToolTip="Lưu">
                            <SpriteProperties CssClass="Sprite_Apply" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div class="float-left button-left-margin">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                        Text="Bỏ Qua">
                        <ClientSideEvents Click="buttonCancel_Click" />
                        <Image ToolTip="Bỏ qua">
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
    <ClientSideEvents AfterResizing="popup_authorization_AfterResizing" Shown="popup_authorization_Show" />
</dx:ASPxPopupControl>
