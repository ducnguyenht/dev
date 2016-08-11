<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uMaterialEdit.ascx.cs"
    Inherits="WebModule.Produce.Config.UserControl.uMaterialEdit" %>
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
    .style8
    {
        height: 18px;
    }
</style>
<script type="text/javascript">
    function formMaterialEdit_Init(s, e) {        
        grdatMaterialSame.SetHeight($("#testheight").height() - 90);
        grdataMaterialSupplier.SetHeight($("#testheight").height() - 120);
        grdataUserMaterialGroup.SetHeight($("#testheight").height() - 120);
        grdataMaterialMaterialUnit.SetHeight($("#testheight").height() - 120);
    }
    function formMaterialEdit_AfterResizing(s, e) {
        grdatMaterialSame.SetHeight($("#testheight").height() - 90);
        grdataMaterialSupplier.SetHeight($("#testheight").height() - 120);
        grdataUserMaterialGroup.SetHeight($("#testheight").height() - 120);
        grdataMaterialMaterialUnit.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    } 

</script>
<div id="lineContainerMaterial">
    <dx:ASPxCallbackPanel ID="cbpanelUserMaterial" runat="server" Width="200px" ClientInstanceName="cbpanelUserMaterial"
        OnCallback="cbpanelUserMaterial_Callback">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formMaterialEdit" runat="server" HeaderText="Thông Tin Nguyên Vật Liệu -"
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formMaterialEdit"
                    AllowDragging="True" RenderMode="Classic" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True">
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
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" Width="100%"
                                Height="100%">
                                <TabPages>
                                    <dx:TabPage Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ASPxFfdfLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
                                                    EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Nguyên Vật Liệu" HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtCodeDevice" runat="server" 
                                                                                ClientInstanceName="txtCodeDevice" Width="200px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                    <RequiredField ErrorText="Chưa nhập mã công cụ dụng cụ" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên  Nguyên Vật Liệu" 
                                                                    HelpText="255 ký tự, không cho phép trùng lắp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtNameDevice" runat="server" ClientInstanceName="txtNameDevice"
                                                                                MaxLength="255" Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                    <RequiredField ErrorText="Chưa nhập tên công cụ dụng cụ" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboRowStatusDevice" runat="server" 
                                                                                ClientInstanceName="cboRowStatusDevice" Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Hình Ảnh">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                                Width="300px">
                                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho NVL">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" Width="300px">
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
                                               <dx:ASPxFormLayout ID="fdddsssf" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
							 <dx:ASPxGridView ID="grdataUserMaterialGroup" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" ClientInstanceName="grdataUserMaterialGroup">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã nhóm nguyên vật liệu" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px" FieldName="materialcategorycode">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Mã nhóm nguyên vật liệu" Width="150px" />
                                                                    <dx:ListBoxColumn Caption="Tên nhóm nguyên vật liệu" Width="300px" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên nhóm nguyên vật liệu" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="250px" FieldName="categoryname">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="100px">
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
                                                            <CancelButton Visible="True">
                                                                <Image Height="25px" Url="~/images/cancel.png" Width="25px">
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Update" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="50" ShowEmptyDataRows="True" RenderMode="Classic">
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                        VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
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
															<dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm NVL" 
																ToolTip="Tạo mới 1 nhóm NVL - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn nhóm NVL">
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
                                    <dx:TabPage Text="Nhà Cung Cấp">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                
                                              <dx:ASPxFormLayout ID="hhdsssf" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
															SupportsDisabledAttribute="True">
						   
						    
							<dx:ASPxGridView ID="grdataMaterialSupplier" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" ClientInstanceName="grdataMaterialSupplier">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="3">
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
                                                        <dx:GridViewDataTextColumn Caption="Mã Nhà Cung Cấp" FieldName="SupplierID" ShowInCustomizationForm="True"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="SupplierName" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="SupplierDescription" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                    </Styles>
                                                </dx:ASPxGridView>
						   
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới NCC" 
																ToolTip="Tạo mới 1 NCC - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn nhà cung cấp">
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
                                            <dx:ASPxFormLayout ID="bbdsssf" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
															SupportsDisabledAttribute="True">
						   
							<dx:ASPxGridView ID="grdataMaterialMaterialUnit" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" ClientInstanceName="grdataMaterialMaterialUnit">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
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
                                                        <dx:GridViewDataTextColumn Caption="Mã Đơn Vị Tính" FieldName="MaterialMaterialUnitID"
                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Đơn Vị Tính" FieldName="MaterialMaterialName"
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Thành Phần" FieldName="MaterialMaterialUnitComponent"
                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="MaterialMaterialUnitDescription"
                                                            ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                    <Styles>
                                                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                        </Header>
                                                    </Styles>
                                                </dx:ASPxGridView>
						   
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới ĐVT" 
																ToolTip="Tạo mới 1 ĐVT - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn ĐVT">
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
                                    <dx:TabPage Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
															SupportsDisabledAttribute="True">						   						  
											
						       <dx:ASPxNavBar ID="ASdsPxNavdsBar1" runat="server" AutoCollapse="True" Height="100%"
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho NVL">
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
                                    <dx:TabPage Text="Nguyên Vật Liệu Tương Đương">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                              <dx:ASPxFormLayout ID="eedsssf" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
															SupportsDisabledAttribute="True">						   						  
											
						     <dx:ASPxGridView ID="grdatMaterialSame" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" ClientInstanceName="grdatMaterialSame">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4">
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
                                                        <dx:GridViewDataTextColumn Caption="Mã Nguyên Vật Liệu" FieldName="MaterialSameID"
                                                            ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nguyên Vật Liệu" FieldName="MaterialSameName"
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" FieldName="MaterialSameManufacturer"
                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MaterialSameDescription" ShowInCustomizationForm="True"
                                                            VisibleIndex="3">
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn NVL tương đương">
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
