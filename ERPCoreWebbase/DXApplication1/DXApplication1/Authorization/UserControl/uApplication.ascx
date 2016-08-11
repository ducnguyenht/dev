<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uApplication.ascx.cs"
    Inherits="WebModule.Authorization.Application.Usercontrol.uApplication" %>
<dx:ASPxCallbackPanel ID="cpLine" runat="server" ClientInstanceName="cpLine" 
    oncallback="cpLine_Callback" Width="200px">
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="popup_edit" runat="server" AllowDragging="True" 
        AllowResize="True" ClientInstanceName="popup_application" 
        CloseAction="CloseButton" HeaderText="Thông tin ứng dụng" Height="600px" 
        Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ShowFooter="True" 
        ShowMaximizeButton="True" Width="900px">
        <FooterTemplate>
            <div style="padding: 10px;">
                <div class="float-left">
                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                        Text="Trợ Giúp">
                        <Image ToolTip="Trợ giúp">
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div class="float-right">
                    <div class="float-left">
                        <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                            ClientInstanceName="buttonSave" Text="Lưu Lại">
                            <ClientSideEvents Click="buttonSave_Click" />
                            <Image ToolTip="Lưu">
                                <SpriteProperties CssClass="Sprite_Apply" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div class="float-left button-left-margin">
                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                            ClientInstanceName="buttonCancel" Text="Bỏ Qua">
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
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" 
                    EnableTabScrolling="True" Height="100%" RenderMode="Lightweight" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung" Visible="False">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
                                        <Items>
                                            <dx:LayoutItem Caption="Mã ứng dụng">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tên ứng dụng">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Thể loại">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Mô tả" ColSpan="2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
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
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxTreeList ID="grdSitemap" runat="server" AutoGenerateColumns="False" 
                                        DataSourceID="ResourceXDS" Height="300px" KeyFieldName="AppComponentId" 
                                        OnHtmlRowPrepared="grdSitemap_HtmlRowPrepared" 
                                        ParentFieldName="ParentAppComponentId!Key" Width="900px">
                                        <Columns>
                                            <dx:TreeListTextColumn Caption="Mã Tài Nguyên" FieldName="Code" 
                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Tên Tài Nguyên" FieldName="Name" 
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="Description" 
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="200px">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListCheckColumn Caption="Chọn" ShowInCustomizationForm="True" 
                                                VisibleIndex="2" Width="50px">
                                                <DataCellTemplate>
                                                    <dx:ASPxCheckBox ID="chkSelect" runat="server" ClientInstanceName="chkSelect" 
                                                        oninit="chkSelect_Init">
                                                    </dx:ASPxCheckBox>
                                                </DataCellTemplate>
                                            </dx:TreeListCheckColumn>
                                            <dx:TreeListTextColumn FieldName="AppComponentId" ShowInCustomizationForm="True" 
                                                Visible="False" VisibleIndex="3">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn FieldName="ParentAppComponentId!Key" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                            </dx:TreeListTextColumn>
                                        </Columns>
                                        <Settings ScrollableHeight="300" VerticalScrollBarMode="Visible" />
                                        <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
                                        <SettingsSelection AllowSelectAll="True" Recursive="True" />
                                        <ClientSideEvents EndCallback="grdSitemap_EndCallback" />
                                    </dx:ASPxTreeList>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>

<dx:ASPxCallback ID="cbLine" runat="server" ClientInstanceName="cbLine" 
    oncallback="cbLine_Callback">
</dx:ASPxCallback>
<dx:XpoDataSource ID="ResourceXDS" runat="server" 
    TypeName="NAS.DAL.System.Resource.AppComponent">
</dx:XpoDataSource>

<dx:ASPxCallback ID="cb" runat="server" ClientInstanceName="cb" 
    oncallback="cb_Callback1">
</dx:ASPxCallback>
<dx:ASPxHiddenField ID="hdId" runat="server" ClientInstanceName="hdId">
</dx:ASPxHiddenField>


