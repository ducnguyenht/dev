<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uManufacturerEdit.ascx.cs"
    Inherits="ERPCore.ImExporting.UserControl.uManufacturerEdit" %>
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
    function formManufacturerEdit_Init(s, e) {
        grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        grmanufacturer0.SetHeight($("#testheight").height() - 120);

        //grdSameBuyingDevice.SetHeight($("#testheight").height() - 90);


    }
    function formManufacturerEdit_AfterResizing(s, e) {
        grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        grmanufacturer0.SetHeight($("#testheight").height() - 120);
        
        ASPxClientControl.AdjustControls();
    } 

</script>
<div id="lineContainerManufacturer">
    <dx:ASPxCallbackPanel ID="cpLineManufacturer" runat="server" Width="100%" ClientInstanceName="cpLineManufacturer"
        OnCallback="cpLineManufacturer_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formManufacturerEdit" runat="server" HeaderText="Thông Tin Nhà Sản Xuất - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formManufacturerEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                    <ClientSideEvents Init="formManufacturerEdit_Init" AfterResizing="formManufacturerEdit_AfterResizing">
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
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="loutajsdfdfd" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Nhà Sản Xuất" RequiredMarkDisplayMode="Required" 
                                                                    HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="ASPxFormLayout1235_E2" runat="server" Width="200px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Nhà Sản Xuất" RequiredMarkDisplayMode="Required" 
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
                                                                <dx:LayoutItem Caption="Hình Ảnh">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                                Width="300px">
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="   " ShowCaption="True" 
                                                                    HelpText="Hình ảnh cho nhà sản xuất">
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
                                    <dx:TabPage Name="tabmanufacturercategory" Text="Trực Thuộc Nhóm">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="qaqdssd22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
															SupportsDisabledAttribute="True">
															
											        <dx:ASPxGridView ID="grdDataManufacturerGroup" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="SupplierId" Width="100%" ClientInstanceName="grdDataManufacturerGroup" >
                                                    <ClientSideEvents EndCallback="grdDataManufacturerGroup_EndCallback" CustomButtonClick="grdDataManufacturerGroup_CustomButtonClick" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm Nhà Sản Xuất" FieldName="Name" Name="Name"
                                                            ShowInCustomizationForm="True" VisibleIndex="2">                                                           
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="5"
                                                            Width="100px" ButtonType="Image">
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
                                                                <Image>
                                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã  Nhóm Nhà Sản Xuất" FieldName="Code" Name="Code"
                                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ManufacturerId" FieldName="SupplierId" ShowInCustomizationForm="True"
                                                            Visible="False" VisibleIndex="0" Name="SupplierId">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" Name="Description"
                                                            ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">                                                        
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Width="100px">
                                                            <PropertiesComboBox>
                                                                <Items>
                                                                    
                                                                    <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                                    
                                                                    <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                                    
                                                                </Items>
                                                                
                                                            </PropertiesComboBox>
                                                            <EditCellStyle HorizontalAlign="Center">
                                                            </EditCellStyle>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataComboBoxColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
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
															<dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm NSX" 
																ToolTip="Tạo mới 1 nhóm NSX - Ctrl + N" Wrap="False">
															</dx:ASPxButton>
														</dx:LayoutItemNestedControlContainer>
													</LayoutItemNestedControlCollection>
												</dx:LayoutItem>
												<dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhóm NSX">
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
                                    <dx:TabPage Text="Hình Thức Phân Phối">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td align="center" class="style26">
                                                            <dx:ASPxRadioButton ID="ASPxRadioButton1" runat="server" Text="Trực Tiếp">
                                                            </dx:ASPxRadioButton>
                                                        </td>
                                                        <td align="center" class="style27">
                                                            <dx:ASPxRadioButton ID="ASPxRadioButton2" runat="server" Text="Gián Tiếp">
                                                            </dx:ASPxRadioButton>
                                                        </td>
                                                        <td align="center">
                                                            <dx:ASPxRadioButton ID="ASPxRadioButton3" runat="server" Text="Trực Tiếp và Gián Tiếp">
                                                            </dx:ASPxRadioButton>
                                                        </td>
                                                    </tr>                                                                                                    
                                                </table>
                                              <dx:ASPxFormLayout ID="qsss22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
															SupportsDisabledAttribute="True">
															
											       <dx:ASPxGridView ID="grmanufacturer0" runat="server" AutoGenerateColumns="False"
                                                                ClientInstanceName="grmanufacturer0" DataSourceID="" KeyFieldName="" Width="100%">
                                                                <ClientSideEvents CustomButtonClick="" EndCallback="" />
                                                                <Columns>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                        VisibleIndex="6" Width="15%">
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
                                                                    <dx:GridViewDataTextColumn Caption="Tên Nhóm Nhà Sản Xuất" FieldName="Name" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" ShowInCustomizationForm="True"
                                                                        VisibleIndex="3" Width="30%">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Mã Nhóm Nhà Sản Xuất" FieldName="Code" ShowInCustomizationForm="True"
                                                                        VisibleIndex="0" Width="10%">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
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
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn hình thức phân phối">
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
                                                <dx:ASPxFormLayout ID="eeess22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
															SupportsDisabledAttribute="True">
															
											      <dx:ASPxNavBar ID="anchsgdhd" runat="server" AutoCollapse="True" Height="100%" 
                                                    Width="100%">
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho nhà sản xuất">
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
