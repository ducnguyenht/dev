<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DepartmentManagement.aspx.cs" Inherits="WebModule.Authorization.DepartmentManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
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
                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" 
                            ParentTitleSize="16px" Style="display: block;" Title="Danh mục phòng ban"
                            TitleSize="26px" />
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane>
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                        
                        <dx:ASPxTreeList ID="trlDepartment" runat="server" AutoGenerateColumns="False" KeyFieldName="DepartmentId"
                            ParentFieldName="ParentDepartmentId" Width="100%" 
                            OnNodeDeleting="trlDepartment_NodeDeleting" 
                            OnNodeInserting="trlDepartment_NodeInserting" 
                            OnNodeUpdating="trlDepartment_NodeUpdating" 
                            DataSourceID="dsDepartmentTree" OnNodeDeleted="trlDepartment_NodeDeleted">
                            <Columns>
                                <dx:TreeListTextColumn Caption="Mã phòng ban" FieldName="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="0">
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Tên phòng ban" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                    VisibleIndex="2" Width="100px">
                                    <DataCellTemplate>
                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" 
                                            SpriteCssClass="Sprite_Info">
                                        </dx:ASPxImage>
                                    </DataCellTemplate>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Email" FieldName="Email" ShowInCustomizationForm="True"
                                    VisibleIndex="3" Width="220px">
                                    <DataCellTemplate>
                                        <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server" CssClass="mailto" 
                                            NavigateUrl='<%# "mailto:" + Eval("Email") %>' Text='<%# Eval("Email") %>'>
                                        </dx:ASPxHyperLink>
                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Số điện thoại" FieldName="PhoneNumber" 
                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn Caption="Trạng thái" FieldName="RowStatus" 
                                    ShowInCustomizationForm="True" VisibleIndex="5">
                                    <EditFormSettings Visible="False" />
                                </dx:TreeListTextColumn>
                                <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                    ShowNewButtonInHeader="True" VisibleIndex="6" Width="100px">
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
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:TreeListCommandColumn>
                            </Columns>
                            <SettingsEditing AllowRecursiveDelete="True" />
                            <BorderLeft BorderWidth="0px" />
                            <BorderRight BorderWidth="0px" />
                            <Settings ShowTreeLines="False" />
                        </dx:ASPxTreeList>

                        <dx:XpoDataSource ID="dsDepartmentTree" runat="server" DefaultSorting="" 
                            TypeName="DAL.Authorization.ViewDepartmentTree" 
                            Criteria="[RowStatus] In ('A'c, 'I'c) And [OrganizationId] = ? And [Language] = ?">
                            <CriteriaParameters>
                                <asp:Parameter Name="AccessingOrganizationId" />
                                <asp:Parameter Name="Language" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>

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
    </asp:Content>

