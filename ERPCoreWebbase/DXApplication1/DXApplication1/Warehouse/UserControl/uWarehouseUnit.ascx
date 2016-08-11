<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uWarehouseUnit.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uWarehouseUnit" %>
<script type="text/javascript">

    function formManufacturerEdit_Init(s, e) {
        //grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        //grmanufacturer0.SetHeight($("#testheight").height() - 120);
    }
    function formManufacturerEdit_AfterResizing(s, e) {
        // grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        // grmanufacturer0.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    }

    var ManuWarehouseUnitEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                formWarehouseUnitEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                formWarehouseUnitEdit.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                formWarehouseUnitEdit.PerformCallback('new');
            }
            formWarehouseUnitEdit.Show();
            /////2013-09-21 ERP-580 Khoa.Truong INS START
            $(ManuWarehouseUnitEditForm).triggerHandler('shown');

            /////2013-09-22 ERP-580 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-20 ERP-580 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagWarehouseUnitEdit.GetMainElement(), null, true);
            if (validated) {
                if (!formWarehouseUnitEdit.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    formWarehouseUnitEdit.PerformCallback(args);
                }
            }
            else {
                pagWarehouseUnitEdit.SetActiveTabIndex(0);
            }
            //formWarehouseUnitEdit.Hide();
            /////2013-09-20 ERP-580 Khoa.Truong MOD END
        },
        Hide: function () { formWarehouseUnitEdit.Hide(); },

        btnSave_Click: function (s, e) {
            ManuWarehouseUnitEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            ManuWarehouseUnitEditForm.Hide();
        },

        EndCallback: function (s, e) {
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                formWarehouseUnitEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManuWarehouseUnitEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS END

        },
        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(ManuWarehouseUnitEditForm).on('saved', callback);
        },
        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManuWarehouseUnitEditForm).on('shown', callback);
        },
        /////2013-09-21 ERP-580 Khoa.Truong INS END


        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Custom validation
        ValidateForm: function (s, e) {
            switch (s.name) {
                case '<%= txtName.ClientID %>':
                    var name = e.value;
                    if (name) {
                        name = name.trim();
                    }
                    else {
                        return;
                    }
                    //Validate max length
                    var NAME_MAX_LENGTH = 255;
                    if (name.length > NAME_MAX_LENGTH) {
                        e.isValid = false;
                        e.errorText = "Tên đơn vị lưu trữ không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END


    };
</script>
<div id="lineContainerWarehouseUnit"> 
<dx:ASPxCallbackPanel ID="cpLineWarehouseUnit" runat="server" Width="100%" 
        ClientInstanceName="cpLineWarehouseUnit" oncallback="cpLineWarehouseUnit_Callback">
 
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formWarehouseUnitEdit" runat="server" 
            HeaderText="Thông tin đơn vị lưu trữ -" Height="600px" Modal="True"  
            Width="900px" ClientInstanceName="formWarehouseUnitEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" OnWindowCallback="popWarehouseUnitEdit_WindowCallback" 
            ShowFooter="True" ScrollBars="Auto" ShowMaximizeButton="True" 
            ShowSizeGrip="False">
            <ClientSideEvents 
                        Init="formManufacturerEdit_Init" 
                        AfterResizing="formManufacturerEdit_AfterResizing"
                        EndCallback="ManuWarehouseUnitEditForm.EndCallback">
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
                                <dx:ASPxButton ID="buttonCancelWarehouse" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelWarehouse" Text="Thoát" Wrap="False" 
                                    ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="ManuWarehouseUnitEditForm.btnCancel_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptWarehouse" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveWarehouse" Text="Lưu Lại" Wrap="False" 
                                    ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="ManuWarehouseUnitEditForm.btnSave_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
                    <dx:ASPxPageControl ID="pagWarehouseUnitEdit" ClientInstanceName="pagWarehouseUnitEdit" runat="server" ActiveTabIndex="0" 
                        Height="100%" Width="100%" 
                        EnableTabScrolling="True">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Thông tin chung">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="frmlInfoGeneral" runat="server" DataSourceID="dsInventoryUnit" 
                                            AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                            <Items>
                                                <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                    <Border BorderStyle="None" />
                                                    <Items>
                                                         <dx:LayoutItem Caption="Tên đơn vị lưu trữ" FieldName="Name" 
                                                            HelpText="255 ký tự">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="txtName" runat="server" 
                                                                        ClientInstanceName="txtName" MaxLength="255" Width="400px">
                                                                        <ClientSideEvents Validation="ManuWarehouseUnitEditForm.ValidateForm"></ClientSideEvents>
                                                                        <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                            <RequiredField ErrorText="Chưa nhập tên đơn vị lưu trữ" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Trạng thái" HelpText="Tự động tạo mới" Visible="false">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="cbRowStatus" runat="server" 
                                                                        ClientInstanceName="cbRowStatus" 
                                                                        Width="200px">
                                                                        <Items>
                                                                             <dx:ListEditItem Selected="True" Text="Hoạt động" Value="1" />
                                                                             <dx:ListEditItem Text="Ngưng hoạt động" Value="2" />
                                                                        </Items>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Hình ảnh" Visible="false">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
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
                                                        <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho đơn vị lưu trữ" Visible="false">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
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
                                            <Border BorderStyle="None" />
                                        </dx:ASPxFormLayout>
                                        <dx:XpoDataSource ID="dsInventoryUnit" runat="server" 
                                            TypeName="NAS.DAL.Nomenclature.Inventory.InventoryUnit" 
                                            Criteria="[RowStatus] &gt; 0 And [InventoryUnitId] = ?">
                                            <CriteriaParameters>
                                                <asp:Parameter Name="InventoryUnitId" />
                                            </CriteriaParameters>
                                        </dx:XpoDataSource>
                                    </dx:ContentControl>                                   
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="tabDetail" Text="Thông tin chi tiết" Visible="false">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                   <dx:ASPxFormLayout ID="ASPxFsd22t2" runat="server" Height="100%" 
											Width="100%">
											<Items>
												<dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
													<LayoutItemNestedControlCollection>
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
															SupportsDisabledAttribute="True">
                                           		
													 <dx:ASPxNavBar ID="navbarDetailInfo" runat="server" AutoCollapse="True" Width="100%" Height="100%">
                                                        <Groups>
                                                        <dx:NavBarGroup Text="Mô tả" Name="Description">
                                                            <ContentTemplate>
                                                                <dx:ASPxHtmlEditor ID="htmleditorDescription" runat="server" Height="350px" 
                                                                    Width="100%">
                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                </dx:ASPxHtmlEditor>
                                                            </ContentTemplate>
                                                        </dx:NavBarGroup>
                                                        <dx:NavBarGroup Expanded="False" Text="Tài liệu" Visible="False">
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
														<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
															SupportsDisabledAttribute="True">
															<dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
																Font-Size="X-Small" ForeColor="#CCCCCC" Text="Cập nhật mô tả và tài liệu ">
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
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
</div>