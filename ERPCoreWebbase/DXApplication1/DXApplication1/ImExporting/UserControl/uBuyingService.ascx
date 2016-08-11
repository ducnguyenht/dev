<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingService.ascx.cs"
    Inherits="DXApplication1.ImExporting.UserControl.uBuyingService" %>
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
    function formServiceEdit_Init(s, e) {
        grdSameService.SetHeight($("#testheight").height() - 90);
        grdSeviceCategory.SetHeight($("#testheight").height() - 120);
    }

    function formServiceEdit_AfterResizing(s, e) {
        grdSameService.SetHeight($("#testheight").height() - 90);
        grdSeviceCategory.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    }
</script>
<div id="lineContainerService">
    <dx:ASPxCallbackPanel ID="cpLineService" runat="server" Width="100%" ClientInstanceName="cpLineService"
        OnCallback="cpLineService_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formServiceEdit" runat="server" HeaderText="Thông Tin Dịch Vụ - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formServiceEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                    <ClientSideEvents Init="formServiceEdit_Init" AfterResizing="formServiceEdit_AfterResizing">
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
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
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
                                <dx:ASPxPageControl ID="lauoyr22" runat="server" ActiveTabIndex="0" Height="100%"
                                    RenderMode="Classic" Width="100%">
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="loutajsd" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã Dịch Vụ" RequiredMarkDisplayMode="Required" HelpText="Tối đa 128 ký tự">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxFormLayout1235_E2" runat="server" Width="200px">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tên Dịch Vụ" RequiredMarkDisplayMode="Required" HelpText="255 ký tự">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="ASPxFormLayout1235_E1" runat="server" Width="400px">
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="ASPxFormLayout1235_E4" runat="server" Width="200px">
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Hình Ảnh">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                                    Width="300px">
                                                                                </dx:ASPxImage>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="   " ShowCaption="True" HelpText="Hình ảnh cho dịch vụ">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxUploadControl ID="ASPxFormLayout1235_E6" runat="server" UploadMode="Auto"
                                                                                    Width="280px">
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
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                   <dx:ASPxFormLayout ID="wwffPdsdssd22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
															
											 <dx:ASPxGridView ID="grdSeviceCategory" runat="server" AutoGenerateColumns="False"
                                                        OnCellEditorInitialize="grdBuyingProductCategory_CellEditorInitialize" 
                                                        Width="100%" ClientInstanceName="grdSeviceCategory">
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã nhóm dịch vụ" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="150px" FieldName="materialcategorycode">
                                                                <PropertiesComboBox>
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã nhóm dịch vụ" Width="150px" />
                                                                        <dx:ListBoxColumn Caption="Tên nhóm dịch vụ" Width="300px" />
                                                                    </Columns>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên nhóm dịch vụ" ShowInCustomizationForm="True"
                                                                VisibleIndex="1" Width="250px" FieldName="categoryname">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                Width="200px" FieldName="categorydescription">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="3">
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
															<dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm DV" 
																ToolTip="Tạo mới 1 nhóm dịch vụ - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhóm Dịch Vụ">
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
                                        <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="wqwedsdssd22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho dịch vụ">
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
                                        <dx:TabPage Text="Dịch Vụ Tương Đương">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                             <dx:ASPxFormLayout ID="wfffPdsdssd22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
															SupportsDisabledAttribute="True">
															
											       <dx:ASPxGridView ID="grdSameService" runat="server" AutoGenerateColumns="False" OnCellEditorInitialize="grdBuyingProductCategory_CellEditorInitialize"
                                                        Width="100%" ClientInstanceName="grdSameService">
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã dịch vụ" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="150px" FieldName="materialcode">
                                                                <PropertiesComboBox>
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã Nhóm dịch vụ" Width="150px" />
                                                                        <dx:ListBoxColumn Caption="Tên Nhóm dịch vụ" Width="300px" />
                                                                    </Columns>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên dịch vụ" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                Width="250px" FieldName="materialname">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mô Tả" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                FieldName="materialdescription">
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
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Dịch Vụ Tương Đương">
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
<asp:SqlDataSource ID="ManufacturerSDS" runat="server" ConnectionString="Data Source=192.168.1.120;Initial Catalog=ERPCORE;Integrated Security=True"
    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
<asp:SqlDataSource ID="SupplierSDS" runat="server" ConnectionString="Data Source=192.168.1.120;Initial Catalog=ERPCORE;Integrated Security=True">
</asp:SqlDataSource>
<dx:XpoDataSource ID="ProductSupplierXDS" runat="server" TypeName="DAL.Purchasing.ViewProductSupplier"
    OnInserted="ProductSupplierXDS_Inserted">
</dx:XpoDataSource>
