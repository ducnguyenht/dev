<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uFinishedProductEdit.ascx.cs"
    Inherits="WebModule.Produce.Config.UserControl.uProductEdit" %>
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
        grdatauFinishedProductSame.SetHeight($("#testheight").height() - 90);
        ASPxGridView3.SetHeight($("#testheight").height() - 120);
        treelistFinishedProductUnit.SetHeight($("#testheight").height() - 120);
        grdatauFinishedProductGroup.SetHeight($("#testheight").height() - 120);
    }
    function formMaterialEdit_AfterResizing(s, e) {        
        grdatauFinishedProductSame.SetHeight($("#testheight").height() - 90);
        ASPxGridView3.SetHeight($("#testheight").height() - 120);
        treelistFinishedProductUnit.SetHeight($("#testheight").height() - 120);
        grdatauFinishedProductGroup.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    } 

</script>
<div id="lineContainerFinishedProduct">
    <dx:ASPxCallbackPanel ID="cbpanelUserFinishedProduct" runat="server" Width="1000px" ClientInstanceName="cbpanelUserFinishedProduct"
        OnCallback="cbpanelUserFinishedProduct_Callback">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                 <dx:ASPxPopupControl ID="formFinishedProductEdit" runat="server" HeaderText="Thông Tin Thành Phẩm - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formFinishedProductEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" 
                     ShowMaximizeButton="True">
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
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                         <div style="height: 100%;" id="testheight">
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" RenderMode="Classic" Height="100%"
                                Width="100%" ActiveTabIndex="1">
                                <TabPages>
                                    <dx:TabPage Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ASPxFormLdsdayout1235" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Thành Phẩm" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxFormLayout1235_E2" runat="server" Width="200px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Thành Phẩm" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="255 ký tự, không cho phép trùng lắp">
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
                                                                    HelpText="Hình ành cho thành phẩm">
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
                                    <dx:TabPage Text="Trực Thuộc Nhóm">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                               <dx:ASPxFormLayout ID="wfdfddsds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
						
						   <dx:ASPxGridView ID="grdatauFinishedProductGroup" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" ClientInstanceName="grdatauFinishedProductGroup">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Nhóm Thành Phẩm" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                    <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm Thành Phẩm" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="250px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="2"
                                                            Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="100px">
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
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                                </Image>
                                                            </ClearFilterButton>
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
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="50" RenderMode="Classic" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                    <Styles>
                                                        <Header HorizontalAlign="Center" Font-Bold="true">
                                                        </Header>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>               
						
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới thành phẩm" 
																ToolTip="Tạo mới 1 thành phẩm - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn nhóm thành phẩm">
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
                                    <dx:TabPage Text="Đơn Vị Tính">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                               
                                              <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
															SupportsDisabledAttribute="True">
															
						  <dx:ASPxTreeList ID="treelistFinishedProductUnit" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId" Width="100%" ClientInstanceName="treelistFinishedProductUnit">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="Mã Đơn Vị Tính" FieldName="FinishedProductUnitID" ShowInCustomizationForm="True"
                                                            VisibleIndex="0">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Tên Đơn Vị Tính" FieldName="FinishedProductUnit" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Bao Gồm" FieldName="FinishedProductUnitAmount" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="FinishedProductUnitDescription" ShowInCustomizationForm="True"
                                                            VisibleIndex="3">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
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
                                                        </dx:TreeListCommandColumn>
                                                    </Columns>
                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                    </Styles>
                                                </dx:ASPxTreeList>      
						
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới ĐVT" 
																ToolTip="Tạo mới 1 ĐVT - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn đơn vị tính">
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
                                    <dx:TabPage Text="Cấu thành">
                                        <contentcollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="rrfddsds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
															SupportsDisabledAttribute="True">
						
						    <dx:ASPxGridView ID="ASPxGridView3" runat="server" AutoGenerateColumns="False" 
                                                    OnDetailRowExpandedChanged="ASPxGridView3_DetailRowExpandedChanged" 
                                                    Width="100%" ClientInstanceName="ASPxGridView3">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã đơn vị tính" FieldName="ID" 
                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên đơn vị tính" FieldName="Name" 
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsDetail ShowDetailRow="True" ShowDetailButtons = "true" />
                                                    <templates>
                                                        <detailrow>
                                                            <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                                                                Width="100%">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Mã thành phần" FieldName="ID" 
                                                                        VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Thành phần" FieldName="Tp" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Bao gồm" FieldName="bg" VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="dg" VisibleIndex="3">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                                        VisibleIndex="4">
                                                                        <editbutton visible="True">
                                                                            <image>
                                                                                <spriteproperties cssclass="Sprite_Edit" />
                                                                            </image>
                                                                        </editbutton>
                                                                        <newbutton visible="True">
                                                                            <image>
                                                                                <spriteproperties cssclass="Sprite_New" />
                                                                            </image>
                                                                        </newbutton>
                                                                        <deletebutton visible="True">
                                                                            <image>
                                                                                <spriteproperties cssclass="Sprite_Delete" />
                                                                            </image>
                                                                        </deletebutton>
                                                                        <clearfilterbutton visible="True">
                                                                        </clearfilterbutton>
                                                                    </dx:GridViewCommandColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </detailrow>
                                                    </templates>
                                                </dx:ASPxGridView>
						
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới CCDC" 
																ToolTip="Tạo mới 1 CCDC - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Cáu thành thành phẩm">
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
                                        </contentcollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ssfdfd" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
															SupportsDisabledAttribute="True">
						
						   <dx:ASPxNavBar ID="ASPxNavBdsar13232" runat="server" AutoCollapse="True" 
                                                    Height="100%" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor43433" runat="server" Height="350px" 
                                                                            Width="100%">
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu thành phẩm">
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
                                    <dx:TabPage Text="Thông Tin Khác">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                              <dx:ASPxFormLayout ID="sssdfd" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
															SupportsDisabledAttribute="True">
						
						    <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" RenderMode="Lightweight" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Dữ Liệu Hoạt Chất" Expanded="False">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxGridView ID="grduoclieuhoatchat" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="4">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên Dược Liệu Hoạt Chất" VisibleIndex="0">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Thành Phần" VisibleIndex="1">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Chức Năng" VisibleIndex="2">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn Giải" VisibleIndex="3">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                                            <Styles>
                                                                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                </Header>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                    </Template>
                                                                </dx:NavBarItem>
                                                            </Items>
                                                        </dx:NavBarGroup>
                                                        <dx:NavBarGroup Expanded="False" Text="Tiêu Chuẩn Lưu Trữ">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                                                            <Columns>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="4">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delêt" />
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Lưu Trữ" VisibleIndex="0">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Thấp Nhất" VisibleIndex="1">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Cao Nhất" VisibleIndex="2">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn Giải" VisibleIndex="3">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <Styles>
                                                                                <Header Font-Bold="True" Font-Italic="False" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                </Header>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Thông tin khác cho thành phẩm">
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
                                    <dx:TabPage Text="Thành Phẩm Tương Đương">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                               <dx:ASPxFormLayout ID="sdssdfd" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" 
															SupportsDisabledAttribute="True">
						
						    <dx:ASPxGridView ID="grdatauFinishedProductSame" runat="server" 
                                                    AutoGenerateColumns="False" Width="100%" ClientInstanceName="grdatauFinishedProductSame">

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>

                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                            ShowInCustomizationForm="True" VisibleIndex="3">
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
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã Thành Phẩm" 
                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Thành Phẩm" 
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" ShowInCustomizationForm="True" 
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                        ShowHeaderFilterButton="True" VerticalScrollableHeight="360" 
                                                        VerticalScrollBarMode="Auto" />

                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                    </Styles>
                                                </dx:ASPxGridView>              
                                                
						   
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>												
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn thành phẩm tương đương">
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
