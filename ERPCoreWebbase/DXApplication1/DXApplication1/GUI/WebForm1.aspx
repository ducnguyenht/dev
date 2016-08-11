<%@ Page Title="" Language="C#" MasterPageFile="~/GUI/Main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DXApplication1.GUI.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
        Height="100%" RenderMode="Lightweight" Width="100%">
        <TabPages>
            <dx:TabPage>
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" 
                            EnableTabScrolling="True" Height="92%" RenderMode="Lightweight" Width="100%">
                            <TabPages>
                                <dx:TabPage Text="Mời qua email">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxPanel ID="ASPxPanel1" runat="server" Height="500px" ScrollBars="Auto" 
                                                Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                                            AlignItemCaptionsInAllGroups="True">
                                                            <Items>
                                                                <dx:LayoutGroup Caption="Thông tin người dùng">
                                                                    <Items>
                                                                        <dx:LayoutItem Caption="Họ và tên" RequiredMarkDisplayMode="Required">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Email" RequiredMarkDisplayMode="Required">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                    </Items>
                                                                </dx:LayoutGroup>
                                                                <dx:LayoutGroup Caption="Chọn tổ chức">
                                                                    <Items>
                                                                        <dx:LayoutItem ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" CheckState="Unchecked" 
                                                                                        Text="Mời làm quản trị viên">
                                                                                    </dx:ASPxCheckBox>
                                                                                    <dx:ASPxTreeList ID="trlOrganization" runat="server" 
                                                                                        AutoGenerateColumns="False" KeyFieldName="OrganizationId" 
                                                                                        ParentFieldName="ParentOrganizationId" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:TreeListTextColumn Caption="Tổ chức" FieldName="Name" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            </dx:TreeListTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowDragDrop="False" AllowFocusedNode="True" 
                                                                                            AutoExpandAllNodes="True" FocusNodeOnExpandButtonClick="False" />
                                                                                        <Border BorderWidth="0px" />
                                                                                    </dx:ASPxTreeList>
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
                                                                                    <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" ActiveView="Html" 
                                                                                        Height="200px">
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
                                                                            <dx:ASPxButton ID="ASPxFormLayout1_E3" runat="server" Text="Mời">
                                                                            </dx:ASPxButton>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                        </dx:ASPxFormLayout>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Mời theo danh sách file">
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
                        </dx:ASPxPageControl>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
