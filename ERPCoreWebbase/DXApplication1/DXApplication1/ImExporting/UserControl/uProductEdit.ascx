<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uProductEdit.ascx.cs"
    Inherits="ERPCore.ImExporting.UserControl.uProductEdit" %>
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
        grdBuyingProductCategory0.SetHeight($("#testheight").height() - 90);
        grdProductSupplier.SetHeight($("#testheight").height() - 120);
        grdBuyingProductCategory.SetHeight($("#testheight").height() - 120);
    }
    function formMaterialEdit_AfterResizing(s, e) {
        grdBuyingProductCategory0.SetHeight($("#testheight").height() - 90);
        grdProductSupplier.SetHeight($("#testheight").height() - 120);
        grdBuyingProductCategory.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    } 
</script>
<div id="lineContainerProduct">
    <dx:aspxcallbackpanel id="cpLineProduct" runat="server" width="100%" clientinstancename="cpLineProduct"
        oncallback="cpLineProduct_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formProductEdit" runat="server" HeaderText="Thông Tin Hàng Hóa - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formProductEdit"
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
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%;" id="testheight">

                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="100%"
                                RenderMode="Classic" Width="100%" 
                                OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ASPxFormLayout1235" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Hàng Hóa" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxFormLayout1235_E2" runat="server" Width="200px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Hàng Hóa" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="255 ký tự">
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
                                                                <dx:LayoutItem Caption="Nhà Sản Xuất" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="Chọn nhà sản xuất">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxFormLayout1235_E3" runat="server" Width="400px">
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
                                                                <dx:LayoutItem ShowCaption="True" Caption="   " 
                                                                    HelpText="Hình ảnh cho hàng hóa">
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
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="aaas22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
															
											     <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Nhóm Hàng Hóa" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px">
                                                            <PropertiesComboBox>
                                                                <Columns>
<dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
<dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
</Columns>
</PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm Hàng Hóa" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="250px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="100px">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Thêm">
                                                                    <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="Xóa">
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
<Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>
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
															<dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm HH" 
																ToolTip="Tạo mới 1 nhóm hàng hóa - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhóm Hàng Hóa">
															</dx:ASPxLabel>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
													<CaptionSettings VerticalAlign="Middle" />
													<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />

<CaptionSettings VerticalAlign="Middle"></CaptionSettings>

<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
												</dx:LayoutItem>
											</Items>
											<SettingsItems VerticalAlign="Middle" />

<SettingsItems VerticalAlign="Middle"></SettingsItems>
										</dx:ASPxFormLayout>                                               
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Nhà Cung Cấp">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                               
                                               <dx:ASPxFormLayout ID="bbbas22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
															SupportsDisabledAttribute="True">
															
											     <dx:ASPxGridView ID="grdProductSupplier" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="grdProductSupplier" DataSourceID="" KeyFieldName="ProductSupplierId"
                                                    OnRowDeleting="grdProductSupplier_RowDeleting" OnRowUpdating="grdProductSupplier_RowUpdating"
                                                    Width="100%">
                                                    <SettingsEditing Mode="Inline" />
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="SupplierCode"
                                                            Name="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                            <PropertiesComboBox CallbackPageSize="20" DropDownStyle="DropDown" EnableCallbackMode="True"
                                                                IncrementalFilteringMode="StartsWith" TextField="Code" TextFormatString="{0} {1}"
                                                                ValueField="Code" Width="400px">
                                                                <Columns>
                                                                    


                                                                    
<dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" Width="150px" />
<dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" Width="300px" />

</Columns>
</PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="SupplierName" Name="SupplierName"
                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="250px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" Name="Description"
                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Width="100px">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Thêm">
                                                                    <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="Xóa">
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ProductSupplierId" FieldName="ProductSupplierId"
                                                            Name="ProductSupplierId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>

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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới NCC" 
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
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhà Cung Cấp">
															</dx:ASPxLabel>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
													<CaptionSettings VerticalAlign="Middle" />
													<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />

<CaptionSettings VerticalAlign="Middle"></CaptionSettings>

<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
												</dx:LayoutItem>
											</Items>
											<SettingsItems VerticalAlign="Middle" />

<SettingsItems VerticalAlign="Middle"></SettingsItems>
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
															SupportsDisabledAttribute="True">
												<dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                    KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="Mã Đơn Vị Tính" FieldName="code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="100px">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Tên Đơn Vị Tính" FieldName="name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="200px">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Bao Gồm" FieldName="amount" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="80px">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="description" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="300px">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="3"
                                                            Width="120px" Caption="Thao tác">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Thêm">
                                                                    <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="Xóa">
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                        </dx:TreeListCommandColumn>
                                                    </Columns>
                                                    <Styles>
                                                        <Header HorizontalAlign="Center" Font-Bold="true">
                                                        </Header>
                                                        <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                        </CommandButton>
                                                    </Styles>
                                                </dx:ASPxTreeList>
															
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới ĐVT" 
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
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn đơn vị tính">
															</dx:ASPxLabel>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
													<CaptionSettings VerticalAlign="Middle" />
													<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />

<CaptionSettings VerticalAlign="Middle"></CaptionSettings>

<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
												</dx:LayoutItem>
											</Items>
											<SettingsItems VerticalAlign="Middle" />

<SettingsItems VerticalAlign="Middle"></SettingsItems>
										</dx:ASPxFormLayout>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                              <dx:ASPxFormLayout ID="ffdsds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
															SupportsDisabledAttribute="True">
												
												  <dx:ASPxNavBar ID="ASPxNavBar13232" runat="server" AutoCollapse="True" 
                                                    Height="100%" Width="100%" OnItemClick="ASPxNavBar13232_ItemClick">
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
															<dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho hàng hóa">
															</dx:ASPxLabel>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
													<CaptionSettings VerticalAlign="Middle" />
													<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />

<CaptionSettings VerticalAlign="Middle"></CaptionSettings>

<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
												</dx:LayoutItem>
											</Items>
											<SettingsItems VerticalAlign="Middle" />

<SettingsItems VerticalAlign="Middle"></SettingsItems>
										</dx:ASPxFormLayout>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Thông Tin Khác">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="fgggsds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" 
															SupportsDisabledAttribute="True">
												
												<dx:ASPxNavBar ID="navBarOtherInfo" runat="server" RenderMode="Lightweight" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Dược Liệu, Hoạt Chất">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxGridView ID="grdBuyingProductCategory0" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Tên Dươc Liệu, Hoạt Chất" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px">
                                                                                    <PropertiesComboBox>
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                            <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                        </Columns>
                                                                                    </PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Thành Phần" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                                    Width="250px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Chức Năng" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                                    Width="200px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" VisibleIndex="3">
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
                                                                            <SettingsPager PageSize="30">
                                                                            </SettingsPager>
                                                                            <Styles>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                    </Template>
                                                                </dx:NavBarItem>
                                                            </Items>
                                                        </dx:NavBarGroup>
                                                        <dx:NavBarGroup Text="Tiêu Chuẩn Lưu Trữ">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxGridView ID="grdBuyingProductCategory0" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Tiêu Chuẩn Lưu Trữ" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px">
                                                                                    <PropertiesComboBox>                                                                                        
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                            <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                        </Columns>
                                                                                    </PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Thấp Nhất" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="250px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Cao Nhất" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="200px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" VisibleIndex="3">
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
                                                                            <SettingsPager PageSize="30">
                                                                            </SettingsPager>
                                                                            <Styles>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Cập nhật thông tin khác">
															</dx:ASPxLabel>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
													<CaptionSettings VerticalAlign="Middle" />
													<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />

<CaptionSettings VerticalAlign="Middle"></CaptionSettings>

<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
												</dx:LayoutItem>
											</Items>
											<SettingsItems VerticalAlign="Middle" />
<SettingsItems VerticalAlign="Middle"></SettingsItems>
										</dx:ASPxFormLayout>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Hàng Hóa Tương Đương">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                      <dx:ASPxFormLayout ID="ooogggsds" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer runat="server" 
															SupportsDisabledAttribute="True">
												
												         <dx:ASPxGridView ID="grdBuyingProductCategory0" runat="server" AutoGenerateColumns="False"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã Hàng Hóa" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="150px">
                                                            <PropertiesComboBox>
                                                                <Columns>
<dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
<dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
</Columns>
</PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên  Hàng Hóa" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="250px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Width="100px">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Thêm">
                                                                    <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="Xóa">
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
                                                    </SettingsPager>
                                                    <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />

<Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>

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
														<dx:LayoutItemNestedControlContainer runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E221" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn hàng hóa tương đương">
															</dx:ASPxLabel>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
													<CaptionSettings VerticalAlign="Middle" />
													<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
<CaptionSettings VerticalAlign="Middle"></CaptionSettings>
<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
												</dx:LayoutItem>
											</Items>
											<SettingsItems VerticalAlign="Middle" />
<SettingsItems VerticalAlign="Middle"></SettingsItems>
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
    </dx:aspxcallbackpanel>
</div>
