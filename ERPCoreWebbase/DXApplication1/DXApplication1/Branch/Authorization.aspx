<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Authorization.aspx.cs" Inherits="WebModule.Branch.Authorization" %>
<%@ Register src="~/NAANAdmin/Authorization/Usercontrol/uAuthorization.ascx" tagname="uAuthorization" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function tlSitemap_Custombutton(s, e) {
        if (e.buttonID == 'authorization')
            popup_authorization.Show();
        else if (e.buttonID == '') {

        }
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">

    <uc1:uAuthorization ID="uAuthorization" runat="server" />

    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục ứng dụng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:ASPxGridView ID="gvApplication" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <ClientSideEvents CustomButtonClick="gvApplication_CustomButton
" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Thể loại" FieldName="Type" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" 
                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" 
                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
            </dx:GridViewDataTextColumn>
        </Columns>
        <settingsediting mode="PopupEditForm" />
        <settings showfilterrow="True" showfilterrowmenu="True" 
            showheaderfilterbutton="True" />
        <SettingsDetail ShowDetailRow="True" />
        <settingspopup>
            <editform height="150px" width="500px" />
            <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
        </settingspopup>
        <Templates>
            <DetailRow>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" RenderMode="Lightweight" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="SiteMap">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxTreeList ID="tlSitemap" runat="server" AutoGenerateColumns="False" Height="300px"
                                        KeyFieldName="OrganizationId" OnLoad="tlSitemap_Load" ParentFieldName="ParentOrganizationId" Width="100%">
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
                                            <dx:TreeListCommandColumn CellStyle-HorizontalAlign="Center" ButtonType="Image" Caption="Phân quyền" ShowInCustomizationForm="True"
                                                VisibleIndex="3" Width="100px">
                                                <CustomButtons>
                                                    <dx:TreeListCommandColumnCustomButton ID="authorization">
                                                        <Image ToolTip="Phân quyền">
                                                            <SpriteProperties CssClass="Sprite_AssignTo" />
                                                        </Image>
                                                    </dx:TreeListCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:TreeListCommandColumn>
                                        </Columns>
                                        <Settings ScrollableHeight="300" VerticalScrollBarMode="Auto" />
                                        <ClientSideEvents CustomButtonClick="tlSitemap_Custombutton" />
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
    </dx:ASPxGridView>
</asp:Content>
