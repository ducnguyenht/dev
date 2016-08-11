<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingServiceCategory.ascx.cs"
    Inherits="DXApplication1.ImExporting.UserControl.uBuyingServiceCategory" %>
<style type="text/css">
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }
</style>
<script type="text/javascript">
    function formMaterialEdit_Init(s, e) {
    }

    function formMaterialEdit_AfterResizing(s, e) {
        ASPxClientControl.AdjustControls();
    }
</script>
<div id="lineContainerServiceCategory">
    <dx:ASPxCallbackPanel ID="cpLineServiceCategory" runat="server" Width="100%" ClientInstanceName="cpLineServiceCategory"
        OnCallback="cpLineServiceCategory_Callback">
        <ClientSideEvents EndCallback="cpLineServiceCategory_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                  <dx:ASPxPopupControl ID="formServiceCategoryEdit" runat="server" HeaderText="Thông Tin Nhóm Dịch Vụ - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formServiceCategoryEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                     <ClientSideEvents Init="formMaterialEdit_Init" AfterResizing="formMaterialEdit_AfterResizing">
                    </ClientSideEvents>
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False" 
                                    ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" 
                                    ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                         <div style="height: 100%;" id="testheight">
                            <dx:ASPxPageControl ID="dsdfdfrfdfd" RenderMode="Classic" runat="server" 
                                ActiveTabIndex="0" Height="100%"
                                Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="loutajsd" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Dịch Vụ" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxFormLayout1235_E2" runat="server" Width="200px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Dịch Vụ" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="255 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxFormLayout1235_E1" runat="server" Width="400px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxFormLayout1235_E4" runat="server" Width="200px">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Hình Ảnh">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" 
                                                                                ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="   " ShowCaption="True" 
                                                                    HelpText="Hình ảnh cho nhóm dịch vụ">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1235_E6" runat="server" 
                                                                                UploadMode="Auto" Width="280px">
                                                                            </dx:ASPxUploadControl>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
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
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="wqwedsddssd22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
															
											     <dx:ASPxNavBar ID="ASPxNavBar13232" runat="server" AutoCollapse="True" Height="100%"
                                                        Width="100%">
                                                        <Groups>
                                                            <dx:NavBarGroup Text="Mô Tả">
                                                                <Items>
                                                                    <dx:NavBarItem>
                                                                        <Template>
                                                                            <dx:ASPxHtmlEditor ID="ASPxHtmlEditor43433" runat="server" Height="350px" Width="100%">
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
                                                                            <dx:ASPxFileManager ID="ASPxFileManager4341" runat="server" Height="350px">
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho nhóm dịch vụ">
															</dx:ASPxLabel>
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
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
