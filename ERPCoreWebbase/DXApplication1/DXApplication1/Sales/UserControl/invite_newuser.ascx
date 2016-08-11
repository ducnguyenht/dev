<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="invite_newuser.ascx.cs"
    Inherits="DXApplication1.GUI.invite_newuser" %>
<script type="text/javascript">
    function pnInviteSingleUser_Adjust() {
        //$("#pnInviteSingleUser").height(pcInviteSingleUser.GetHeight()-84);
    }
    $(document).ready(function () {
//        pnInviteSingleUser_Adjust();
//        ASPxClientUtils.AttachEventToElement(window, "resize", pnInviteSingleUser_Adjust);
    });

</script>
<dx:ASPxCallbackPanel ID="cp_InviteUser" ClientInstanceName="cp_InviteUser" runat="server" Width="100%" 
    oncallback="cp_InviteUser_Callback">
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        ClientInstanceName="pcInviteSingleUser" EnableTabScrolling="True" 
        RenderMode="Lightweight" Width="100%">
        <TabPages>
            <dx:TabPage Text="Mời qua email">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div ID="pnInviteSingleUser" style="overflow: auto; width: 100%">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                AlignItemCaptionsInAllGroups="True">
                                <Items>
                                    <dx:LayoutGroup Caption="Thông tin người dùng">
                                        <Items>
                                            <dx:LayoutItem Caption="Họ và tên" RequiredMarkDisplayMode="Required">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="tbx_UserInvite" runat="server" Width="170px">
                                                            <ValidationSettings>
                                                                <RequiredField ErrorText="Chưa nhập họ và tên!" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Email" RequiredMarkDisplayMode="Required">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="tbx_Email" runat="server" Width="170px">
                                                            <ValidationSettings  EnableCustomValidation="True" ErrorDisplayMode="Text" ErrorTextPosition="Bottom" SetFocusOnError="true">
                                                                <ErrorFrameStyle Font-Size="Smaller"/>
                                                                <RegularExpression ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" 
                                                                    ErrorText="Email không hợp lệ!"/>
                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập địa chỉ Email!"/>
                                                            </ValidationSettings>                                                            
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Chọn phòng ban">
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxCheckBox ID="ckb_Invite" runat="server" CheckState="Unchecked" 
                                                            Text="Mời làm quản trị viên">
                                                        </dx:ASPxCheckBox>
                                                        <dx:ASPxTreeList ID="trlDepartment" runat="server" AutoGenerateColumns="False" 
                                                            DataSourceID="ds_Department" KeyFieldName="DepartmentId" 
                                                            ParentFieldName="ParentDepartmentId!Key" Width="100%">
                                                            <Columns>
                                                                <dx:TreeListTextColumn Caption="Phòng ban" FieldName="Name" 
                                                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                                                </dx:TreeListTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowDragDrop="False" AllowFocusedNode="True" 
                                                                AutoExpandAllNodes="True" FocusNodeOnExpandButtonClick="False" />
                                                            <Border BorderWidth="0px" />
                                                        </dx:ASPxTreeList>
                                                        <dx:XpoDataSource ID="ds_Department" runat="server" 
                                                            TypeName="NAS.DAL.Nomenclature.Organization.Department">
                                                        </dx:XpoDataSource>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutGroup Caption="Nội dung thư mời">
                                        <Items>
                                            <dx:LayoutItem ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxHtmlEditor ID="contentMail" runat="server" Height="200px">
                                                        </dx:ASPxHtmlEditor>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                        </div>
                        <div class="clear">
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Mời theo danh sách file" Visible="False">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPanel ID="ASPxPanel2" runat="server" Height="100%" Width="100%">
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbl_file" runat="server" AssociatedControlID="txt_hoten" 
                                                    Text="File danh sách:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txt_file" runat="server" Width="400px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="btn_selectfile" runat="server" Text="Chọn file">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Tải mẫu file">
                                                </dx:ASPxHyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle>
            <BorderLeft BorderWidth="0px" />
            <BorderRight BorderWidth="0px" />
            <BorderBottom BorderWidth="0px" />
        </ContentStyle>        
    </dx:ASPxPageControl>
        </dx:PanelContent>        
</PanelCollection>    
</dx:ASPxCallbackPanel>

