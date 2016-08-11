<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Account.aspx.cs" Inherits="ERPCore.Accounting.Account" %>

<%@ Register Src="UserControl/AccountEdit.ascx" TagName="AccountEdit" TagPrefix="uc1" %>
<%@ Register src="UserControl/ucAccount.ascx" tagname="ucAccount" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        // MainForm Event
//        function grdData_EndCallback(s, e) {
//            if (s.cpEdit) {
//                if (s.cpEdit == 'edit') {
//                    formAccountEdit.Show();

//                    ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
//                    cpLine.PerformCallback('edit');
//                }
//                delete (s.cpEdit);
//                return;
//            }
//        }

//        function grdData_CustomButtonClick(s, e) {
//            formAccountEdit.Show();
//            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
//            cpLine.PerformCallback('edit');
//        }

//        function OnGetRowValues(values) {

//        }

//        // EditForm Event 
//        function buttonSave_Click(s, e) {
//            if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
//                ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
//                return;
//            }

//            if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
//                ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
//                return;
//            }

//            if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').GetValue() == null) {
//                ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').Validate();
//                return;
//            }

//            cpLine.PerformCallback('save');
//            cpHeader.PerformCallback('refresh');

//        }

//        function buttonCancel_Click(s, e) {
//            formAccountEdit.Hide();
//        }

//    
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Hệ thống tài khoản" Font-Bold="True"
        Font-Size="Medium">
    </dx:ASPxLabel>

    <%--<dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
        KeyFieldName="ProductId" Width="100%" OnHtmlRowCreated="grdData_HtmlRowCreated">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã" FieldName="Code" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên Hệ Thống Tài Khoản" FieldName="Name" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="Note" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                VisibleIndex="4">
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
                <CancelButton>
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                </CancelButton>
                <UpdateButton>
                    <Image>
                        <SpriteProperties CssClass="Sprite_Apply" />
                    </Image>
                </UpdateButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="Mặc Định" VisibleIndex="3" ButtonType="Image"
                ShowSelectCheckbox="True">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Khoá" VisibleIndex="6">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Lock" Text="Khóa">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Lock" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsBehavior AllowSelectSingleRowOnly="True" />
        <SettingsEditing Mode="Inline" />
        <SettingsDetail ShowDetailRow="True" />
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
        </Styles>
        <Templates>
            <DetailRow>
                <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx" style="padding-bottom: 10px;
                    width: 100%;">
                    <tr>
                        <td valign="top" width="800px">
                            <dx:ASPxTreeList ID="ASPxTreeList_httk" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId" Width="100%">
                                <Columns>
                                    <dx:TreeListTextColumn Caption="Loại Tài Khoản" FieldName="LoaiTaiKhoan" ShowInCustomizationForm="True"
                                        VisibleIndex="0">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn Caption="Số Tài Khoản" FieldName="SoTaiKhoan" ShowInCustomizationForm="True"
                                        VisibleIndex="1">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn Caption="Cấp" FieldName="Cap" ShowInCustomizationForm="True"
                                        VisibleIndex="2">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn Caption="Tên Tài Khoản" FieldName="TenTaiKhoan" ShowInCustomizationForm="True"
                                        VisibleIndex="3">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                        VisibleIndex="4">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                        VisibleIndex="5">
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
                                    </dx:TreeListCommandColumn>
                                </Columns>
                            </dx:ASPxTreeList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxUploadControl ID="ASPxUploadControl1_ct" runat="server" UploadMode="Auto"
                                Width="280px" NullText="Chọn tập tin để Upload">
                            </dx:ASPxUploadControl>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1_ul" runat="server" Text="Upload">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="ASPxButton2_dl" runat="server" Text="Tải Mẫu">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>--%>
    <%--<uc1:AccountEdit ID="AccountEdit1" runat="server" />--%>
    <uc2:ucAccount ID="ucAccount1" runat="server" />
</asp:Content>
