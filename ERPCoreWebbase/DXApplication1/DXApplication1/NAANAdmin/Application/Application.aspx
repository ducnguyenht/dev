<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Application.aspx.cs" Inherits="WebModule.NAANAdmin.Application.Application" %>

<%@ Register Src="~/NAANAdmin/Application/Usercontrol/uApplication.ascx" TagName="uApplication"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function gvApplication_CustomButton(s, e) {
            if (e.buttonID == 'new' || e.buttonID == 'edit')
                popup_application.Show();
            else if (e.buttonID == '') {

            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uApplication ID="uApplication" runat="server" />
    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục ứng dụng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:ASPxGridView ID="gvApplication" runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Thể loại" FieldName="Type" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" ShowInCustomizationForm="True"
                VisibleIndex="1" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" ReadOnly="True"
                ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="7"
                Width="120px">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit" />
                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="new">
                        <Image ToolTip="Thêm">
                            <SpriteProperties CssClass="Sprite_New" />
                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="delete">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete" />
                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
        <SettingsDetail ShowDetailRow="True" />
        <Templates>
            <DetailRow>
                <dx:ASPxPageControl Width="100%" ID="ASPxPageControl2" runat="server" ActiveTabIndex="0"
                    RenderMode="Lightweight">
                    <TabPages>
                        <dx:TabPage Text="SiteMap">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxTreeList ID="tlSitemap" runat="server" AutoGenerateColumns="False" Width="100%"
                                        Height="300px" KeyFieldName="OrganizationId" OnLoad="tlSitemap_Load" ParentFieldName="ParentOrganizationId">
                                        <Columns>
                                            <dx:TreeListTextColumn Caption="Mã" FieldName="Id" ShowInCustomizationForm="True"
                                                VisibleIndex="0">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Tên" FieldName="Name" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                                VisibleIndex="2">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListCommandColumn Width="100px" ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                VisibleIndex="3">
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
                                            </dx:TreeListCommandColumn>
                                        </Columns>
                                        <Settings ScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                    </dx:ASPxTreeList>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </DetailRow>
        </Templates>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
        <ClientSideEvents CustomButtonClick="gvApplication_CustomButton" />
    </dx:ASPxGridView>
</asp:Content>
