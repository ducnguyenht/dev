<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uWarehouse.ascx.cs" Inherits="WebModule.Produce.UserControl.uWarehouse" %>
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
        //grdSameBuyingDevice.SetHeight($("#testheight").height() - 90);
        //grdProductSupplier.SetHeight($("#testheight").height() - 120);
        grdBuyingProductCategory.SetHeight($("#testheight").height() - 120);
    }
    function formMaterialEdit_AfterResizing(s, e) {
        //grdSameBuyingDevice.SetHeight($("#testheight").height() - 90);
        //grdProductSupplier.SetHeight($("#testheight").height() - 120);
        grdBuyingProductCategory.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();

    } 

</script>
<div id="lineContainer"> 
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine" oncallback="cpLine_Callback">
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formMaterialEdit" runat="server" 
            HeaderText="Thông tin kho bãi" Height="600px" Modal="True"  
            Width="900px" ClientInstanceName="formMaterialEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000" 
            CloseAction="CloseButton" ScrollBars="Auto" ShowFooter="True" 
            ShowMaximizeButton="True" ShowSizeGrip="False">
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
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        Height="100%" Width="100%">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl11" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                            AlignItemCaptionsInAllGroups="True" ClientInstanceName="ASPxFormLayout1" 
                                            EnableTheming="True" Height="100%" Width="100%">
                                            <Items>
                                                <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Mã Kho" HelpText="Tối đa 128 ký tự">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
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
                                                        <dx:LayoutItem Caption="Tên Kho" HelpText="255 ký tự, không cho phép trùng lắp">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="txtNameDevice" runat="server" 
                                                                        ClientInstanceName="txtNameDevice" MaxLength="255" Width="400px">
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
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
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
                                                        <dx:LayoutItem Caption="Thể Loại Kho" HelpText="Chọn thể loại kho">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="cboManufacturer" runat="server" CallbackPageSize="20" 
                                                                        ClientInstanceName="cboManufacturer" DropDownHeight="200px" 
                                                                        DropDownWidth="450px" EnableCallbackMode="True" 
                                                                        IncrementalFilteringMode="Contains" 
                                                                        OnItemRequestedByValue="cboManufacturer_ItemRequestedByValue" 
                                                                        OnItemsRequestedByFilterCondition="cboManufacturer_ItemsRequestedByFilterCondition" 
                                                                        TextField="Name" TextFormatString="{1};{0}" ValueField="Code" Width="400px">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="Mã Nhà Sản Xuất" FieldName="Code" Name="Code" 
                                                                                Width="150px" />
                                                                            <dx:ListBoxColumn Caption="Tên Nhà Sản Xuất" FieldName="Name" Name="Name" 
                                                                                Width="300px" />
                                                                        </Columns>
                                                                        <ValidationSettings SetFocusOnError="True">
                                                                            <RequiredField ErrorText="Chưa chọn nhà sản xuất !" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                            <CaptionCellStyle CssClass="CaptionStyle">
                                                            </CaptionCellStyle>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Hình Ảnh">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" 
                                                                        ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </dx:ASPxImage>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                            <CaptionCellStyle CssClass="CaptionStyle">
                                                            </CaptionCellStyle>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho CCDC">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" 
                                                                        Width="300px">
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
                                        <dx:ASPxFormLayout ID="ccdasds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
												
								<dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" 
                                                                    AutoGenerateColumns="False" 
                                                                    OnCellEditorInitialize="grdBuyingProductCategory_CellEditorInitialize" 
                                                                    Width="100%" ClientInstanceName="grdBuyingProductCategory">
                                                                    <Columns>
                                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Nhóm" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="150px" 
                                                                            FieldName="materialcategorycode">
                                                                            <PropertiesComboBox>
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn Caption="Mã nhóm nguyên vật liệu" Width="150px" />
                                                                                    <dx:ListBoxColumn Caption="Tên nhóm nguyên vật liệu" Width="300px" />
                                                                                </Columns>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="250px" 
                                                                            FieldName="categoryname">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="3">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
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
                                                                    <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                    </SettingsPager>
                                                                    <SettingsEditing Mode="Inline" />
                                                                    <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                    <Styles>
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
															<dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm kho" 
																ToolTip="Tạo mới 1 nhóm kho - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhóm Kho">
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
                            <dx:TabPage Text="Cấu hình lưu trữ">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="dxsasds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
															SupportsDisabledAttribute="True">
												
								<dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                                        Width="100%" KeyFieldName="OrganizationId" 
                                                        ParentFieldName="ParentOrganizationId">
                                                        <Columns>
                                                            <dx:TreeListTextColumn Caption="Mã" FieldName="code" ShowInCustomizationForm="True" 
                                                                VisibleIndex="0" Width="100px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True" 
                                                                VisibleIndex="1" Width="300px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Bao gồm" FieldName="amount" ShowInCustomizationForm="True" 
                                                                VisibleIndex="2" Width="100px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="Description" ShowInCustomizationForm="True" 
                                                                VisibleIndex="3" Width="300px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" 
                                                                VisibleIndex="3" Width="100px" Caption="Thao tác">
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
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                    </Image>
                                                                </CancelButton>
                                                            </dx:TreeListCommandColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedNode="True" />
                                                        <Styles>
                                                            <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                            </CommandButton>
                                                        </Styles>
                                                    </dx:ASPxTreeList>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới CHLT" 
																ToolTip="Tạo mới 1 cấu hình lưu trữ - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn cấu hình lưu trữ">
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
                                                   <dx:ASPxFormLayout ID="ASPdsdsxF22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
															SupportsDisabledAttribute="True">
															
													 <dx:ASPxNavBar ID="ASPxNfdfar1" runat="server" AutoCollapse="True" Width="100%" Height="100%">
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho kho bãi">
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
<asp:SqlDataSource ID="ManufacturerSDS" runat="server" 
    ConnectionString="Data Source=192.168.1.120;Initial Catalog=ERPCORE;Integrated Security=True" 
    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
<asp:SqlDataSource ID="SupplierSDS" runat="server" 
    ConnectionString="Data Source=192.168.1.120;Initial Catalog=ERPCORE;Integrated Security=True">
</asp:SqlDataSource>