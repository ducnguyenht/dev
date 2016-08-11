<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPopupUserEditting.ascx.cs"
    Inherits="WebModule.NAANAdmin.Authorization.UserControl.uPopupUserEditting" %>
<script type="text/javascript">
    function cpPersonEdit_EndCallback(s, e) {
        LoadingPanelCombineMaterial.Hide();
        if (clientAction == 'Save')
            grd_ListUser.PerformCallback('Refresh');

    }

    function btnPersonEditEdit_Click(s, e) {
        clientAction = 'ActivateForm';
        cpPersonEdit.PerformCallback(clientAction);
    }

    function btnPersonEditCancel_Click(s, e) {
        popup_PersonEdit.Hide();
    }

    function btnPersonEditSave_Click(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(popup_PersonEdit.GetMainElement(), null, true);
        if (validated) {
            clientAction = 'Save';
            cpPersonEdit.PerformCallback(clientAction);
        }
    }

    function txtPersonCode_LostFocus(s, e) {
        var txtValue = s.GetText();
        if (txtValue.length > 36) {
            txtPersonCode.SetIsValid(false);
            txtPersonCode.SetErrorText("Mã người dùng không được vượt quá 36 ký tự!");
            txtPersonCode.Focus();
        }
        cpPersonCode.PerformCallback();
    }

    function txtPersonName_lostFocus(s, e) {
        var txtValue = s.GetText();
        if (txtValue.length > 255) {
            txtPersonName.SetIsValid(false);
            txtPersonName.SetErrorText("Tên người dùng không được vượt quá 255 ký tự!");
            txtPersonName.Focus();
        }
    }

    var UserEditForm = {
        Focus: function () {
            var jObject = $(".KeyShortcutpopup_PersonEdit");
            jObject.focus();
        },

        Closing: function () {
            grd_ListUser.PerformCallback("Refresh");
            grd_ListUser.Focus();
        }
    };

    function popup_PersonEdit_Init(s, e) {
        txtPersonCode.Focus();
        var jObject = $(".KeyShortcutpopup_PersonEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            popup_PersonEdit.Hide();
        });
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            var validated = ASPxClientEdit.ValidateEditorsInContainer(popup_PersonEdit.GetMainElement(), null, true);
            if (validated) {
                cpPersonEdit.PerformCallback('Save');
            }
        });
    }

    function btnPersonEditHelp_Click(s, e) {
        window.open("https://www.youtube.com/watch?v=JwKf7AVhUTo", "_blank");
    }
</script>
<dx:ASPxCallbackPanel ID="cpPersonEdit" runat="server" ShowLoadingPanel="false" ClientInstanceName="cpPersonEdit"
    Width="100%" OnCallback="cpPersonEdit_Callback">
    <ClientSideEvents EndCallback="cpPersonEdit_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server">
            <div id="lineContainerUnit">
                <dx:ASPxPopupControl runat="server" CssClass="KeyShortcutpopup_PersonEdit" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" Modal="True" AllowDragging="True" AllowResize="True"
                    ScrollBars="Auto" ClientInstanceName="popup_PersonEdit" HeaderText="Thông Tin Người Dùng - "
                    ShowMaximizeButton="True" ShowFooter="True" ShowSizeGrip="False" Width="900px"
                    Height="500px" CloseAction="CloseButton" ID="popup_PersonEdit" 
                    Maximized="True">
                    <ClientSideEvents Init="popup_PersonEdit_Init" Closing="UserEditForm.Closing" />
                    <FooterStyle HorizontalAlign="Center" CssClass="footer_bt"></FooterStyle>
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="btnPersonEditHelp" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" ClientInstanceName="btnPersonEditHelp">
                                    <ClientSideEvents Click="btnPersonEditHelp_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right">
                                <dx:ASPxButton ID="btnPersonEditCancel" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnPersonEditCancel" Text="Thoát" Wrap="False" CausesValidation="false">
                                    <ClientSideEvents Click="btnPersonEditCancel_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right">
                                <dx:ASPxButton ID="btnPersonEditSave" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnPersonEditSave" Text="Lưu lại" Wrap="False" CausesValidation="true">
                                    <ClientSideEvents Click="btnPersonEditSave_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <%--<div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnPersonEditSave" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnPersonEditSave" Text="Lưu lại" Wrap="False" CausesValidation="true">
                                    <ClientSideEvents Click="btnPersonEditSave_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>--%>
                            <%--<div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnPersonEditEdit" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnPersonEditEdit" Text="Chỉnh sửa" Wrap="False">
                                    <ClientSideEvents Click="btnPersonEditEdit_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>--%>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            <dx:ASPxFormLayout ID="frmPersonEdit" runat="server" DataSourceID="PersonEdittingXDS"
                                AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                <Items>
                                    <dx:LayoutGroup Caption="Thông tin chung" ShowCaption="true">
                                        <Items>
                                            <dx:LayoutItem Caption="Mã người dùng" HelpText="Tối đa 128 ký tự, , không cho phép trùng lắp"
                                                FieldName="Code" RequiredMarkDisplayMode="Required">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxCallbackPanel ID="cpPersonCode" runat="server" ClientInstanceName="cpPersonCode"
                                                            Width="200px" ShowLoadingPanel="False" OnCallback="cpPersonCode_Callback">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxTextBox ID="txtPersonCode" runat="server" ClientInstanceName="txtPersonCode"
                                                                        Width="200px">
                                                                        <NullTextStyle ForeColor="Silver">
                                                                        </NullTextStyle>
                                                                        <ValidationSettings ErrorText="" SetFocusOnError="false">
                                                                            <RequiredField IsRequired="True" ErrorText="Chưa nhập mã người dùng"></RequiredField>
                                                                            <RegularExpression ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$" ErrorText="Mã người dùng không đúng định dạng" />
                                                                        </ValidationSettings>
                                                                        <ClientSideEvents LostFocus="txtPersonCode_LostFocus" />
                                                                    </dx:ASPxTextBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                                <CaptionCellStyle CssClass="CaptionStyle">
                                                </CaptionCellStyle>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Họ và tên" FieldName="Name" HelpText="255 ký tự">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxTextBox ID="txtPersonName" runat="server" ClientInstanceName="txtPersonName"
                                                            MaxLength="255" Width="400px">
                                                            <ClientSideEvents LostFocus="txtPersonName_lostFocus" />
                                                            <NullTextStyle ForeColor="Silver">
                                                            </NullTextStyle>
                                                            <ValidationSettings ErrorText="" SetFocusOnError="false">
                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập tên người dùng"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Trạng Thái" FieldName="RowStatus" Visible="true">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server">
                                                        <dx:ASPxComboBox ID="cboPersonRowStatus" runat="server" ClientInstanceName="cboPersonRowStatus"
                                                            Width="200px">
                                                            <Items>
                                                                <dx:ListEditItem Text="Sử dụng" Value="1" />
                                                                <dx:ListEditItem Text="Tạm ngưng" Value="-1" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <%--<dx:ASPxButton ID="btnPersonEditSave" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                                            ClientInstanceName="btnPersonEditSave" Text="Lưu lại" Wrap="False" CausesValidation="true">
                                                            <ClientSideEvents Click="btnPersonEditSave_Click" />
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Apply" />
                                                            </Image>
                                                        </dx:ASPxButton>--%>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Thông tin đăng nhập và tổ chức" ShowCaption="true">
                                        <Items>
                                            <dx:LayoutItem Caption="Email đăng nhập">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:XpoDataSource ID="LoginEmailAccountXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.LoginAccount"
                                                            Criteria="[RowStatus] &gt; 0 And [PersonId!Key] = ?">
                                                            <CriteriaParameters>
                                                                <asp:SessionParameter Name="PersonId" SessionField="uPopupUserEditting_PersonId" />
                                                            </CriteriaParameters>
                                                        </dx:XpoDataSource>
                                                        <dx:ASPxGridView ID="grdEmailList" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            DataSourceID="LoginEmailAccountXDS" KeyFieldName="LoginAccountId" OnRowDeleting="grdEmailList_RowDeleting"
                                                            OnRowInserting="grdEmailList_RowInserting" OnRowValidating="grdEmailList_RowValidating"
                                                            KeyboardSupport="true" >
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="LoginAccountId" ReadOnly="True" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn FieldName="RowCreationTimeStamp" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="1">
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn FieldName="Email" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle Wrap="True">
                                                                    </CellStyle>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" Visible="False"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="PersonId!Key" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="AuthenticationProviderId!Key" ShowInCustomizationForm="True"
                                                                    Visible="False" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                    Caption="Thao tác" Width="10%">
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
                                                                    <UpdateButton Visible="true">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                    <CancelButton Visible="true">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dx:GridViewCommandColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="true" ConfirmDelete="true" />
                                                            <SettingsText  EmptyDataRow="Chưa nhập email" ConfirmDelete="Bạn có muốn xóa email này" />
                                                            <SettingsEditing Mode="Inline" />
                                                        </dx:ASPxGridView>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:EmptyLayoutItem>
                                            </dx:EmptyLayoutItem>
                                            <dx:LayoutItem Caption="Thuộc phòng ban" ShowCaption="True">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxTreeList ID="trlDepartment" runat="server" AutoGenerateColumns="False" DataSourceID="DepartmentXDS"
                                                            KeyFieldName="DepartmentId" ParentFieldName="ParentDepartmentId!Key" Width="100%"
                                                            OnSelectionChanged="trlDepartment_SelectionChanged">
                                                            <Columns>
                                                                <dx:TreeListTextColumn Caption="Phòng ban" FieldName="Name" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0">
                                                                </dx:TreeListTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedNode="True" AutoExpandAllNodes="True"
                                                                FocusNodeOnExpandButtonClick="False" ProcessSelectionChangedOnServer="True" />
                                                            <SettingsSelection Enabled="True" />
                                                            <Border BorderWidth="0px" />
                                                        </dx:ASPxTreeList>
                                                        <dx:XpoDataSource ID="DepartmentXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Department"
                                                            Criteria="[RowStatus] &gt; 0">
                                                        </dx:XpoDataSource>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:XpoDataSource ID="PersonEdittingXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Person"
    Criteria="[RowStatus] &gt; 0 And [PersonId] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="PersonId" SessionField="uPopupUserEditting_PersonId" />
    </CriteriaParameters>
</dx:XpoDataSource>
