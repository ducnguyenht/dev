<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Organization.aspx.cs" Inherits="WebModule.NAANAdmin.Authorization.Organization" %>

<%@ Register Src="UserControl/uPopupOrganization.ascx" TagName="uPopupOrganization"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/uPopupUserCreating.ascx" TagName="uPopupUserCreating"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function tList_Organization_Init(s, e) {
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
            s.GetMainElement().focus();
        }

        function lostFocus(s, e) {
            var txtValue = s.GetText().replace(/\-|\.|\_/g, "");
            if (txtValue.length > 13 || txtValue.length < 10) {
                TaxNumber_Client.SetIsValid(false);
                TaxNumber_Client.SetErrorText("Mã số gồm 10 hoặc 13 số!");
            }
        }

        function tList_Organization_BeginCallback(s, e) {
            if (e.command != 'UpdateEdit')
                return;
            if (TaxNumber_Client.GetIsValid()) {
                var txtValue = TaxNumber_Client.GetText().replace(/\-|\./g, "");
                if (txtValue.length > 13 || txtValue.length < 10) {
                    var r = confirm('Mã số thuế chưa đúng định dạng! Bạn có muốn lưu hay không?');
                    if (r == true) {
                        TaxNumber_Client.SetIsValid(true);
                    }
                }
            }
        }

        function tList_Organization_EndCallback(s, e) {
            if (s.cpQuestion) {
                var msg = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                if (msg) {
                    tList_Organization.PerformCallback("Delete|" + s.cpQuestion);
                }
                delete (s.cpQuestion);
            }
            if (s.cpRefresh) {
                tList_Organization.PerformCallback();
                delete (s.cpRefresh);
            }
        }


        // 
        function tList_Organization_CustomCommand(s, e) {
            //console.log(e.buttonID);
            switch (e.buttonID) {
                case 'New':
                case 'Edit':
                    var params = [e.buttonID, e.nodeKey];
                    cpPopupOrganization.PerformCallback(params);
                    break;
                default:
                    break;
            }
        }


        $(document).ready(function () {
            OrganizationClass.BindClosingEvent(function (event) {
                //grdReceiptVouches.Refresh();
                tList_Organization.PerformCallback();
            });

        });
        function tList_Organization_Add(s, e) {
            cpPopupOrganization.PerformCallback('NewNodeFirst');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLoadingPanel ID="LoadingPanelCombineMaterial" runat="server" ClientInstanceName="LoadingPanelCombineMaterial"
        Modal="True" ShowImage="true" Text="Đang xử lý">
        <LoadingDivStyle BackColor="Transparent">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <dx:ASPxSplitter ID="ASPxSplitter3" runat="server" Height="100%" SeparatorVisible="False"
        Width="100%">
        <Panes>
            <dx:SplitterPane>
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <dx:ASPxTreeList ID="tList_Organization" runat="server" AutoGenerateColumns="False"
                            ClientInstanceName="tList_Organization" DataSourceID="dS_Organization" KeyboardSupport="True"
                            KeyFieldName="OrganizationId" OnNodeDeleting="tList_Organization_NodeDeleting"
                            OnNodeInserting="tList_Organization_NodeInserting" OnNodeValidating="tList_Organization_NodeValidating"
                            ParentFieldName="ParentOrganizationId!Key" OnCellEditorInitialize="tList_Organization_CellEditorInitialize"
                            OnCustomCallback="tList_Organization_CustomCallback">
                            <Columns>
                                <dx:TreeListTextColumn Caption="Mã tổ chức" FieldName="Code" Name="code" ShowInCustomizationForm="True"
                                    VisibleIndex="0" Width="12%">
                                    <PropertiesTextEdit ClientInstanceName="Code_Client" MaxLength="36" NullText="Tối đa 36 ký tự">
                                        <ValidationSettings>
                                            <RequiredField ErrorText="Không được để trống!" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Tên tổ chức" FieldName="Name" Name="name" ShowInCustomizationForm="True"
                                    VisibleIndex="1" Width="12%">
                                    <PropertiesTextEdit ClientInstanceName="Name_Client" MaxLength="255" NullText="Tối đa 255 ký tự">
                                        <ValidationSettings>
                                            <RequiredField ErrorText="Không được để trống!" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Địa chỉ" FieldName="Address" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="20%">
                                    <PropertiesTextEdit MaxLength="255" NullText="Tối đa 255 ký tự">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Mã số thuế" FieldName="TaxNumber" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="13%">
                                    <PropertiesTextEdit ClientInstanceName="TaxNumber_Client" NullText="Từ 10 đến 13 ký tự">
                                        <ClientSideEvents LostFocus="lostFocus" />
                                        <ValidationSettings>
                                            <%--<RegularExpression ErrorText="Mã số thuế chưa hợp lệ." 
                                                            ValidationExpression="^\w*\W*\$\^\\\-?\w*\W*\$\^\\\-?\w*\W*\$\^\\\-?\w*\W*\$\^\\\-?\w*\W*\$\^\\*$|^\w*\W*\$\^\\\.?\w*\W*\$\^\\\.?\w*\W*\$\^\\\.?\w*\W*\$\^\\\.?\w*\W*\$\^\\$" />--%>
                                            <%--"^\d*\-?\d*\-?\d*\-?\d*\-?\d*$|^\d*\.?\d*\.?\d*\.?\d*\.?\d*$"--%>
                                            <RequiredField ErrorText="Không được để trống!" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Mô tả" FieldName="Description" Name="description"
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="33%">
                                    <PropertiesTextEdit MaxLength="1024" NullText="Tối đa 1024 ký tự">
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle Wrap="True">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    VisibleIndex="5" Width="10%">
                                    <NewButton>
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
                                    <CustomButtons>
                                        <dx:TreeListCommandColumnCustomButton ID="New">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_New" />
                                            </Image>
                                        </dx:TreeListCommandColumnCustomButton>
                                        <dx:TreeListCommandColumnCustomButton ID="Edit">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Edit" />
                                            </Image>
                                        </dx:TreeListCommandColumnCustomButton>
                                    </CustomButtons>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                    <HeaderCaptionTemplate>
                                        <dx:ASPxImage ID="tList_Organization_Add" runat="server" ShowLoadingImage="true"
                                            ImageUrl="~/images/icon/Actions/AddFile_16x16.png" ClientSideEvents-Click="tList_Organization_Add">
                                            <ClientSideEvents Click="tList_Organization_Add" />
                                        </dx:ASPxImage>
                                    </HeaderCaptionTemplate>
                                </dx:TreeListCommandColumn>
                            </Columns>
                            <Settings ShowRoot="false"/>
                            <SettingsBehavior AllowFocusedNode="True" />
                            <Settings GridLines="Both" SuppressOuterGridLines="true" />
                            <SettingsEditing ConfirmDelete="False" />
                            <SettingsText RecursiveDeleteError="Bạn không thể xóa vì có tổ chức con." />
                            <SettingsEditing ConfirmDelete="False"></SettingsEditing>
                            <ClientSideEvents BeginCallback="tList_Organization_BeginCallback" EndCallback="tList_Organization_EndCallback"
                                Init="tList_Organization_Init" CustomButtonClick="tList_Organization_CustomCommand">
                            </ClientSideEvents>
                        </dx:ASPxTreeList>
                        <br />
                        <!-- POPUP: uPopupOrganization Create/Edit -->
                        <uc1:uPopupOrganization ID="uPopupOrganization1" runat="server" />
                        <!-- END - POPUP: uPopupOrganization Create/Edit -->
                        <!-- POPUP: Person Create/Edit -->
                        <uc2:uPopupUserCreating ID="uPopupUserCreating1" runat="server" />
                        <!-- END - POPUP: Person Create/Edit -->
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
        <Styles>
            <Pane>
                <Paddings Padding="0px" />
                <Border BorderWidth="0px" />
                <Paddings Padding="0px"></Paddings>
                <Border BorderWidth="0px"></Border>
            </Pane>
        </Styles>
        <Border BorderWidth="0px" />
        <Border BorderWidth="0px"></Border>
    </dx:ASPxSplitter>
    <dx:XpoDataSource ID="dS_Organization" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
        Criteria="[RowStatus] > 0 And [OrganizationTypeId] = ?">
        <CriteriaParameters>
            <asp:Parameter Name="OrganizationTypeId" />
        </CriteriaParameters>
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="OrganizationType" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.OrganizationType">
    </dx:XpoDataSource>
</asp:Content>
