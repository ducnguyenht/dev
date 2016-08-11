<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Department.aspx.cs" Inherits="WebModule.NAANAdmin.Authorization.Department" %>

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

        function trlDepartment_OnGetNodeValues(values) {
            var recordId = values[0];
        }
        function trlDepartment_Init(s, e) {
            UtilsForTreeList.AttachStandardShortcutToTreeList(s);
            UtilsForTreeList.RemoveShortcut("Delete");
            UtilsForTreeList.AttachShortcut("Delete", 
                function () {
                    var focusedNodeKey = s.GetFocusedNodeKey();
                    s.DeleteNode(focusedNodeKey);
                    s.Focus();
                },
                {
                    'disable_in_input': true,
                    'target': s.GetMainElement(),
                    'propagate': false
                }
            );

//            UtilsForTreeList.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
//                alert('asdasd');
//                var focusedNodeKey = trlDepartment.GetFocusedNodeKey();
//                trlDepartment.DeleteNode(focusedNodeKey);
//                trlDepartment.Focus();
//            });
//            s.GetMainElement().focus();
        }

        function trlDepartment_EndCallback(s, e) {
            if (s.cpQuestion) {
                var msg = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                if (msg) {
                    trlDepartment.PerformCallback('Delete|' + s.cpQuestion);
                }
                delete (s.cpQuestion);
            }
            if (s.cpRefresh) {
                trlDepartment.PerformCallback();
                delete (s.cpRefresh);
            }
        }
        //        function trlDepartment_BeginCallback(s, e) {
        //            if (s.cpQuestion) {
        //                var msg = confirm("Bạn có chắc chắn muốn xóa không?");
        //                if (msg) {
        //                    trlDepartment.PerformCallback('Delete|' + s.cpQuestion);                    
        //                }
        //                delete (s.cpQuestion);
        //            }
        //            if (s.cpRefresh) {
        //                trlDepartment.PerformCallback();
        //                delete (s.cpRefresh);
        //            }
        //        }

        //        function Code_Client_lostFocus(s, e) {
        //            var txtCode = s.GetText();
        //            if (txtCode.length > 36) {
        //                Code_Client.SetIsValid(false);
        //                Code_Client.SetErrorText("Mã phòng ban không được vượt quá 36 ký tự!");
        //                Code_Client.Focus();
        //            }
        //        }
        //        function Name_Client_lostFocus(s, e) {
        //            var txtName = s.GetText();
        //            if (txtName.length > 36) {
        //                Name_Client.SetIsValid(false);
        //                Name_Client.SetErrorText("Tên phòng ban không được vượt quá 255 ký tự!");
        //                Name_Client.Focus();
        //            }
        //        }
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
                        <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitleSize="16px"
                            Style="display: block;" Title="Danh sách phòng ban" TitleSize="26px" />
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane>
                <ContentCollection>
                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">

                        <dx:ASPxTreeList ID="trlDepartment" runat="server" AutoGenerateColumns="False"
                        KeyFieldName="DepartmentId" ParentFieldName="ParentDepartmentId!Key" 
                            Width="100%" OnNodeInserting="trlDepartment_NodeInserting"
                            DataSourceID="Department_XPO" OnHtmlDataCellPrepared="trlDepartment_HtmlDataCellPrepared"
                            OnNodeValidating="trlDepartment_NodeValidating" OnNodeDeleting="trlDepartment_NodeDeleting"
                            KeyboardSupport="true" ClientInstanceName="trlDepartment" OnCellEditorInitialize="trlDepartment_CellEditorInitialize"
                            OnCustomCallback="trlDepartment_CustomCallback">
                            <ClientSideEvents CustomButtonClick="trlDepartment_CustomButtonClick" EndCallback="trlDepartment_EndCallback" />
                            <Columns>
                                <dx:TreeListTextColumn Caption="Tên phòng ban" FieldName="Name" ShowInCustomizationForm="True"
                                    Width="25%" VisibleIndex="1">
                                    <PropertiesTextEdit ClientInstanceName="Name_Client" MaxLength="255" NullText="Tối đa 255 ký tự">
                                        <ValidationSettings>
                                            <RequiredField ErrorText="Không được để trống!" IsRequired="True" />
                                        </ValidationSettings>
                                        <%--<ClientSideEvents LostFocus="Name_Client_lostFocus" />--%>
                                    </PropertiesTextEdit>
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Mã phòng ban" FieldName="Code" ShowInCustomizationForm="True"
                                    Width="12%" VisibleIndex="0">
                                    <PropertiesTextEdit ClientInstanceName="Code_Client" MaxLength="36" NullText="Tối đa 36 ký tự">
                                        <ValidationSettings>
                                            <RequiredField ErrorText="Không được để trống!" IsRequired="True" />
                                        </ValidationSettings>
                                        <%--<ClientSideEvents LostFocus="Code_Client_lostFocus" />--%>
                                    </PropertiesTextEdit>
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListComboBoxColumn Caption="Phân loại" FieldName="DepartmentTypeId!Key" Name="DpId"
                                    ShowInCustomizationForm="True" Width="15%" VisibleIndex="2" Visible="false">
                                    <PropertiesComboBox DataSourceID="DepartmentType" ValueField="DepartmentTypeId" TextField="Name">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="" FieldName="Name" />
                                        </Columns>
                                    </PropertiesComboBox>
                                </dx:TreeListComboBoxColumn>
                                <dx:TreeListTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                    VisibleIndex="3">
                                    <PropertiesTextEdit MaxLength="1024" NullText="Tối đa 1024 ký tự">
                                    </PropertiesTextEdit>
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    ShowNewButtonInHeader="True" VisibleIndex="4" Width="100px">
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
                            <SettingsBehavior AllowFocusedNode="True" />
                            <SettingsEditing ConfirmDelete="False" />
                            <SettingsText RecursiveDeleteError="Không được xóa vì có phòng ban con." />
                            <Styles>
                                <Header HorizontalAlign="Center">
                                </Header>
                            </Styles>
                            <ClientSideEvents Init="trlDepartment_Init" />
                            <BorderLeft BorderWidth="0px" />
                            <BorderRight BorderWidth="0px" />
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
    <dx:XpoDataSource ID="Department_XPO" runat="server" Criteria="[RowStatus] > 0 And [OrganizationId.OrganizationId] = ?" TypeName="NAS.DAL.Nomenclature.Organization.Department">
        <CriteriaParameters>
            <asp:Parameter Name="OrganizationId" />
        </CriteriaParameters>
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="DepartmentType" runat="server" Criteria="[RowStatus] > 0" TypeName="NAS.DAL.Nomenclature.Organization.DepartmentType">
    </dx:XpoDataSource>
    <dx:ASPxPopupControl ID="pcRolesOfOrganization" runat="server" RenderMode="Lightweight"
        ClientInstanceName="pcRolesOfOrganization" HeaderText="Danh sách vai trò" Height="500px"
        Width="760px" AllowDragging="True" AllowResize="True" AutoUpdatePosition="True"
        CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Size="16px" Text="Danh sách vai trò của"
                    Font-Bold="False" ForeColor="#333333">
                </dx:ASPxLabel>
                &nbsp;<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Size="16px" Text="Phòng Kế toán">
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
