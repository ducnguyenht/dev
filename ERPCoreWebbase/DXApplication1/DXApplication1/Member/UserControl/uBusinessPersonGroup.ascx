<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBusinessPersonGroup.ascx.cs" Inherits="WebModule.Member.UserControl.uBusinessPersonGroup" %>
<dx:ASPxCallbackPanel ID="cpLineMaterial" runat="server" Width="100%" Height="100%" ClientInstanceName="cpLineMaterial">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" RenderMode="Classic" ActiveTabIndex="0" Height="100%"
                    Width="100%" EnableTabScrolling="True">
                    <TabPages>
                        <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
                                        EnableTheming="True" Width="100%" >
                                        <Items>
                                            <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                <Items>
                                                    <dx:LayoutItem Caption="Mã nhóm doanh nhân" HelpText="Tối đa 128 ký tự">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtMaNhomDoanhNhan" runat="server" ClientInstanceName="txtMaNhomDoanhNhan"
                                                                    Width="200px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập mã nhóm doanh nhân" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                        </CaptionCellStyle>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tên nhóm doanh nhân" HelpText="255 ký tự, không cho phép trùng lắp">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtTenNhomDoanhNhan" runat="server" ClientInstanceName="txtTenNhomDoanhNhan"
                                                                    MaxLength="255" NullText="255 ký tự, không cho phép trùng lắp" Width="400px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập tên nhóm doanh nhân" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Mô tả">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxMemo ID="txtMota" runat="server" ClientInstanceName="txtMota" Height="71px"
                                                                    Width="440px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập mô tả" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                        </CaptionCellStyle>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="100%" Width="100%">
                                        <Items>
                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" AutoCollapse="True" Height="100%"
                                                            Width="100%">
                                                            <Groups>
                                                                <dx:NavBarGroup Text="Mô Tả">
                                                                    <Items>
                                                                        <dx:NavBarItem>
                                                                            <Template>
                                                                                <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="350px" Width="100%">
                                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                                </dx:ASPxHtmlEditor>
                                                                            </Template>
                                                                        </dx:NavBarItem>
                                                                    </Items>
                                                                </dx:NavBarGroup>
                                                                <dx:NavBarGroup Expanded="False" Text="Tài Liệu">
                                                                    <Items>
                                                                        <dx:NavBarItem>
                                                                            <Template>
                                                                                <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="350px">
                                                                                    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                                                </dx:ASPxFileManager>
                                                                            </Template>
                                                                        </dx:NavBarItem>
                                                                    </Items>
                                                                </dx:NavBarGroup>
                                                            </Groups>
                                                        </dx:ASPxNavBar>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                                <CaptionSettings VerticalAlign="Middle" />
                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                            </dx:LayoutItem>
                                        </Items>
                                        <SettingsItems VerticalAlign="Middle" />
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>