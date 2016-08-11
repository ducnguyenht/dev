<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Application.aspx.cs" Inherits="WebModule.Authorization.Application.Application" %>

<%@ Register Src="~/Authorization/Usercontrol/uApplication.ascx" TagName="uApplication"
    TagPrefix="uc1" %>
<%@ Register src="UserControl/uResource.ascx" tagname="uResource" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grdApplication_CustomButton(s, e) {
            hdId.Set("id", grdApplication.GetRowKey(grdApplication.GetFocusedRowIndex()));
            popup_application.Show();
            cpResourceLine.PerformCallback("cpResourceLine");            
        }

        function grdApplication_EndCallback(s, e) {
            if (s.cpRefresh == 'refresh') {
                grdApplication.Refresh();
                delete s.cpRefresh;
            }
        }

        function grdSitemap_EndCallback(s, e) {         
        }

        function buttonSave_Click(s, e) {
            cbLine.PerformCallback("save");
            popup_application.Hide();
        }

        function buttonCancel_Click(s, e) {
            popup_application.Hide();
        }


    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục ứng dụng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:XpoDataSource ID="ApplicationXDS" runat="server" 
        TypeName="NAS.DAL.System.Resource.App" Criteria="[RowStatus] &gt;= 1">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="ResourceApplicationXDS" runat="server" 
        TypeName="NAS.DAL.System.Resource.AppComponent" 
        Criteria="[RowStatus] &gt;= 1">
    </dx:XpoDataSource>
    <uc2:uResource ID="uResource1" runat="server" />
    <dx:ASPxGridView ID="grdApplication" runat="server" AutoGenerateColumns="False" 
        Width="100%" ClientInstanceName="grdApplication" DataSourceID="ApplicationXDS" 
        KeyFieldName="AppId" onrowdeleting="grdApplication_RowDeleting" 
        onrowinserting="grdApplication_RowInserting" 
        onrowupdating="grdApplication_RowUpdating" 
        oncustomcallback="grdApplication_CustomCallback" 
        oncelleditorinitialize="grdApplication_CellEditorInitialize" 
        onrowvalidating="grdApplication_RowValidating">

        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
            AllowSelectSingleRowOnly="True" />
        <SettingsEditing Mode="Inline" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
        <SettingsDetail ShowDetailRow="True" />

        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
<ClientSideEvents CustomButtonClick="grdApplication_CustomButton" 
            EndCallback="grdApplication_EndCallback"></ClientSideEvents>
        <Columns>
            <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description"
                VisibleIndex="4" Width="200px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" 
                VisibleIndex="5" Width="100px">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Sử dụng" Value="1" />
                        <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="8"
                Width="100px">
                <EditButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                    </Image>
                </EditButton>
                <NewButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                    </Image>
                </NewButton>
                <DeleteButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                    </Image>
                </DeleteButton>
                <CancelButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                    </Image>
                </CancelButton>
                <UpdateButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                    </Image>
                </UpdateButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="6" Width="50px">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="bUpdateResource" Text="Tài Nguyên">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>

<SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
            AllowSelectSingleRowOnly="True"></SettingsBehavior>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>

<SettingsDetail ShowDetailRow="True"></SettingsDetail>
     

        <Templates>
            <DetailRow>
                <dx:ASPxPageControl Width="100%" ID="ASPxPageControl2" runat="server" ActiveTabIndex="0"
                    RenderMode="Lightweight">
                    <TabPages>
                        <dx:TabPage Text="Sitemap">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxTreeList ID="tlSitemap" runat="server" AutoGenerateColumns="False" Width="100%"
                                        Height="300px" KeyFieldName="ResourceId" 
                                        ParentFieldName="RelationResourceId" DataSourceID="ResourceApplicationXDS" 
                                        OnInit="tlSitemap_Init">
                                        <Columns>
                                            <dx:TreeListTextColumn Caption="ApplicationId" FieldName="ApplicationId" ShowInCustomizationForm="True"
                                                VisibleIndex="0" Visible="False">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Mã Tài Nguyên" FieldName="Code" ShowInCustomizationForm="True"
                                                VisibleIndex="1" Width="150px">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Tên Tài Nguyên" FieldName="ResourceName" ShowInCustomizationForm="True"
                                                VisibleIndex="2">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="ResourceDescription" 
                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                            </dx:TreeListTextColumn>
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
     

<BorderLeft BorderWidth="0px"></BorderLeft>

<BorderRight BorderWidth="0px"></BorderRight>
    </dx:ASPxGridView>
</asp:Content>
