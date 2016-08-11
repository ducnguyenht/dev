<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uApplication.ascx.cs"
    Inherits="WebModule.NAANAdmin.Application.Usercontrol.uApplication" %>
<dx:ASPxPopupControl ID="popup_edit" runat="server" Width="1000px" Height="550px"
    RenderMode="Lightweight" HeaderText="Thông tin ứng dụng" ClientInstanceName="popup_application"
    AllowResize="True" AllowDragging="true" Modal="True" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowFooter="True">
    <FooterTemplate>
        <div style="padding: 10px;">
            <div class="float-left">
                <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                    <Image ToolTip="Trợ giúp">
                        <SpriteProperties CssClass="Sprite_Help" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div class="float-right">
                <div class="float-left">
                    <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" ClientInstanceName="buttonSave"
                        Text="Lưu Lại">
                        <ClientSideEvents Click="buttonSave_Click" />
                        <Image ToolTip="Lưu">
                            <SpriteProperties CssClass="Sprite_Apply" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div class="float-left button-left-margin">
                    <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                        Text="Bỏ Qua">
                        <ClientSideEvents Click="buttonCancel_Click" />
                        <Image ToolTip="Bỏ qua">
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" RenderMode="Lightweight"
                ActiveTabIndex="1" Width="100%" Height="100%" EnableTabScrolling="True">
                <TabPages>
                    <dx:TabPage Text="Thông tin chung">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã ứng dụng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên ứng dụng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Thể loại">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mô tả" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="ASPxMemo2" runat="server" Height="71px" Width="707px">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="SiteMap">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTreeList ID="tlSitemap" runat="server" AutoGenerateColumns="False" Height="300px"
                                    KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId" Width="980px">
                                    <Columns>
                                        <dx:TreeListTextColumn Caption="Mã" FieldName="Id" ShowInCustomizationForm="True"
                                            VisibleIndex="0">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="Tên" FieldName="Name" ShowInCustomizationForm="True"
                                            VisibleIndex="0">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
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
                                        </dx:TreeListCommandColumn>
                                    </Columns>
                                    <Settings ScrollableHeight="300" VerticalScrollBarMode="Visible" />
                                </dx:ASPxTreeList>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
