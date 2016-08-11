<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Department.aspx.cs" Inherits="WebModule.Branch.Department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function trlDepartment_CustomButtonClick(s, e) {
            if (e.buttonID == 'EditRoles') {
                pcRolesOfOrganization.Show();
            }
        }
        function pcRolesOfOrganization_CloseUp(s, e) {
        }
        function grdRolesOfOganization_CustomButtonClick(s, e) {
            if (e.buttonID == 'EditPermissions') {
                pcRoleDetails.Show();
            }
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
            <dx:SplitterPane MinSize="40px" Size="80px" AutoHeight="true">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" 
                            ParentTitleSize="16px" Style="display: block;" Title="Các chi nhánh trực thuộc Công ty CP TM Dược Sâm Ngọc Linh Quảng Nam"
                            TitleSize="26px" />
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane>
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxTreeList ID="trlDepartment" runat="server" AutoGenerateColumns="False" KeyFieldName="OrganizationId"
                            ParentFieldName="ParentOrganizationId" Width="100%">
                            <Columns>
<dx:TreeListTextColumn FieldName="OrganizationId" ShowInCustomizationForm="True" Caption="Mã chi nhánh" 
                                    VisibleIndex="0"></dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Tên chi nhánh" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="100px">
                                    <DataCellTemplate>
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_Info">
                                        </dx:ASPxImage>
                                    </DataCellTemplate>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Địa chỉ" FieldName="Address" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="100px">
                                    <DataCellTemplate>
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_Info">
                                        </dx:ASPxImage>
                                    </DataCellTemplate>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Mã số thuế" FieldName="TaxCode" 
                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Email" FieldName="Email" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Width="220px">
                                    <DataCellTemplate>
                                        <dx:ASPxHyperLink CssClass="mailto" ID="ASPxHyperLink2" runat="server" NavigateUrl='<%# "mailto:" + Eval("Email") %>'
                                            Text='<%# Eval("Email") %>'>
                                        </dx:ASPxHyperLink>
                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Số điện thoại" FieldName="PhoneNo" ShowInCustomizationForm="True"
                                    VisibleIndex="6">
                                </dx:TreeListTextColumn>
                                <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    ShowNewButtonInHeader="True" VisibleIndex="7" Width="100px">
                                    <EditButton Visible="True">
                                        <Image ToolTip="Sửa">
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image ToolTip="Thêm">
                                            <SpriteProperties CssClass="Sprite_New" />
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image ToolTip="Xóa">
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </DeleteButton>
                                    <UpdateButton>
                                        <Image ToolTip="Cập nhật">
                                            <SpriteProperties CssClass="Sprite_Apply" />
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton Text="Bỏ qua">
                                        <Image ToolTip="Bỏ qua">
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </CancelButton>
                                    <%--<CustomButtons>
                                        <dx:TreeListCommandColumnCustomButton ID="EditRoles">
                                            <Image AlternateText="Vai trò" ToolTip="Vai trò">
                                                <SpriteProperties CssClass="Sprite_Role" />
                                            </Image>
                                        </dx:TreeListCommandColumnCustomButton>
                                    </CustomButtons>--%>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:TreeListCommandColumn>
                            </Columns>
                            <BorderLeft BorderWidth="0px" />
                            <BorderRight BorderWidth="0px" />
                            <ClientSideEvents CustomButtonClick="trlDepartment_CustomButtonClick" />
                        </dx:ASPxTreeList>
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
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="16px" Text="Danh sách vai trò của"
                    Font-Bold="False" ForeColor="#333333">
                </dx:ASPxLabel>
                &nbsp;<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Size="16px" 
                    Text="Phòng Kế toán">
                </dx:ASPxLabel>
                <br />
                <br />
                <dx:ASPxGridView ID="grdRolesOfOganization" ClientInstanceName="grdRolesOfOganization"
                    runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="OrganizationRoleId">
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="1"
                            ButtonType="Image" Width="100px">
                            <EditButton Visible="True">
                                <Image ToolTip="Sửa">
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </EditButton>
                            <NewButton Visible="True">
                                <Image ToolTip="Thêm">
                                    <SpriteProperties CssClass="Sprite_New" />
                                </Image>
                            </NewButton>
                            <DeleteButton Visible="True">
                                <Image ToolTip="Xóa">
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </DeleteButton>
                            <ClearFilterButton Visible="True">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Clear" />
                                </Image>
                            </ClearFilterButton>
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="EditPermissions" Text="Quyền truy xuất">
                                    <Image AlternateText="Quyền truy xuất" ToolTip="Quyền truy xuất">
                                        <SpriteProperties CssClass="Sprite_Permission" />
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="Name" ShowInCustomizationForm="True" Caption="T&#234;n vai tr&#242;"
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents CustomButtonClick="grdRolesOfOganization_CustomButtonClick" />
                </dx:ASPxGridView>
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
                    Text="Chi tiết vai trò Cầu kế toán">
                </dx:ASPxLabel>
                <br />
                <br />
                <dx:ASPxGridView ID="grdAccessObjectsOfRole" runat="server" Width="100%" AutoGenerateColumns="False"
                    KeyFieldName="AccessObjectId">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Tên chức năng" FieldName="Name" ShowInCustomizationForm="True"
                            VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                            VisibleIndex="0" Width="100px">
                            <EditButton Visible="True">
                                <Image ToolTip="Sửa">
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </EditButton>
                            <NewButton Visible="True">
                                <Image ToolTip="Thêm">
                                    <SpriteProperties CssClass="Sprite_New" />
                                </Image>
                            </NewButton>
                            <DeleteButton Visible="True">
                                <Image ToolTip="Xóa">
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </DeleteButton>
                            <UpdateButton>
                                <Image ToolTip="Cập nhật">
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </UpdateButton>
                            <CancelButton>
                                <Image ToolTip="Bỏ qua">
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </CancelButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
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
