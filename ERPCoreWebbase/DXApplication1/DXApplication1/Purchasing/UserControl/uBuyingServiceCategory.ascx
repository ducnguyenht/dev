<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingServiceCategory.ascx.cs"
    Inherits="DXApplication1.Purchasing.UserControl.uBuyingServiceCategory" %>
<script type="text/javascript">
    /////2013-09-25 ERP-607 Khoa.Truong INS START
    var BuyingServiceCategoryEditForm = {
        Show: function (headerText, recordId) {
            if (recordId) {
                this._recordId = recordId;
                popBuyingServiceCategory.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                popBuyingServiceCategory.PerformCallback('new');
            }
            if (headerText) {
                popBuyingServiceCategory.SetHeaderText(headerText);
            }
            popBuyingServiceCategory.Show();

            $(BuyingServiceCategoryEditForm).triggerHandler('shown');
        },
        Save: function () {
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagBuyingServiceCategory.GetMainElement(), null, true);
            if (validated) {
                if (!popBuyingServiceCategory.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    popBuyingServiceCategory.PerformCallback(args);
                }
            }
            else {
                pagBuyingServiceCategory.SetActiveTabIndex(0);
            }
        },
        Hide: function () { popBuyingServiceCategory.Hide(); },

        btnSave_Click: function (s, e) {
            BuyingServiceCategoryEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            BuyingServiceCategoryEditForm.Hide();
        },

        EndCallback: function (s, e) {
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                popBuyingServiceCategory.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(BuyingServiceCategoryEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
        },

        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(BuyingServiceCategoryEditForm).on('saved', callback);
        },

        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(BuyingServiceCategoryEditForm).on('shown', callback);
        },
        //Custom validation
        ValidateForm: function (s, e) {
            switch (s.name) {
                case '<%= txtCode.ClientID %>':
                    var code = e.value;
                    if (code) {
                        code = code.trim();
                    }
                    else {
                        return;
                    }
                    //Validate max length
                    var CODE_MAX_LENGTH = 128;
                    if (code.length > CODE_MAX_LENGTH) {
                        e.isValid = false;
                        e.errorText = "Mã nhóm dịch vụ không được vượt quá 128 kí tự";
                    }
                    if (e.isValid == true) {
                        //Check Code is exist in database
                        if (cpntxtCode.InCallback()) {
                            console.log('cpntxtCode: server too busy');
                        }
                        else {
                            cpntxtCode.PerformCallback();
                        }
                    }
                    break;
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
                        e.errorText = "Tên nhóm dịch vụ không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
    };
    /////2013-09-25 ERP-607 Khoa.Truong INS END

    function formMaterialEdit_AfterResizing(s, e) {
        ASPxClientControl.AdjustControls();
    }
</script>
<dx:ASPxPopupControl ID="popBuyingServiceCategory" runat="server" HeaderText="Thông Tin Nhóm Dịch Vụ - "
    Height="600px" Modal="True" Width="900px" ClientInstanceName="popBuyingServiceCategory"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="true" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True"
    OnWindowCallback="popBuyingServiceCategory_WindowCallback">
    <ClientSideEvents EndCallback="BuyingServiceCategoryEditForm.EndCallback" AfterResizing="formMaterialEdit_AfterResizing"></ClientSideEvents>
    <FooterTemplate>
        <div style="padding: 10px;">
            <div class="float-left">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnHelpServiceCategory" UseSubmitBehavior="false" CausesValidation="false" AutoPostBack="false"
                        runat="server" Text="Trợ giúp" ToolTip="Ctrl + H">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="float-right">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton UseSubmitBehavior="true" ID="btnSaveServiceCategory" runat="server"
                        AutoPostBack="False" Text="Lưu lại" ToolTip="Enter">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Apply" />
                        </Image>
                        <ClientSideEvents Click="BuyingServiceCategoryEditForm.btnSave_Click" />
                    </dx:ASPxButton>
                </div>
                <div class="float-left button-left-margin">
                    <dx:ASPxButton ID="btnCancelServiceCategory" UseSubmitBehavior="false" CausesValidation="false" runat="server"
                        AutoPostBack="False" Text="Thoát" ToolTip="Esc">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                        <ClientSideEvents Click="BuyingServiceCategoryEditForm.btnCancel_Click" />
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:XpoDataSource ID="dsBuyingServiceCategoryProperty" runat="server" 
                DefaultSorting="" TypeName="DAL.Purchasing.BuyingServiceCategoryProperty" 
                Criteria="[Language] = ? And [BuyingServiceCategoryId.BuyingServiceCategoryId] = ?">
                <CriteriaParameters>
                    <asp:Parameter DefaultValue="VN" Name="Language" />
                    <asp:Parameter DefaultValue="" Name="BuyingServiceCategoryId" />
                </CriteriaParameters>
            </dx:XpoDataSource>
            <dx:ASPxPageControl ID="pagBuyingServiceCategory" ClientInstanceName="pagBuyingServiceCategory"
                RenderMode="Classic" runat="server" ActiveTabIndex="0" Height="100%" Width="100%">
                <TabPages>
                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="frmBuyingServiceCategory" runat="server" Width="100%" 
                                    DataSourceID="dsBuyingServiceCategoryProperty">
                                    <Items>
                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem Caption="Mã dịch vụ" RequiredMarkDisplayMode="Required" FieldName="BuyingServiceCategoryId.Code" HelpText="Tối đa 128 ký tự">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                runat="server" Width="200px">
                                                                <PanelCollection>
                                                                    <dx:PanelContent>
                                                                        <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập mã nhóm dịch vụ"></RequiredField>
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents Validation="BuyingServiceCategoryEditForm.ValidateForm" />
                                                                        </dx:ASPxTextBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tên dịch vụ" RequiredMarkDisplayMode="Required" FieldName="Name" HelpText="Tối đa 255 ký tự">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="txtName" runat="server" Width="400px">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                    <RequiredField IsRequired="True" ErrorText="Chưa nhập tên nhóm dịch vụ"></RequiredField>
                                                                </ValidationSettings>
                                                                <ClientSideEvents Validation="BuyingServiceCategoryEditForm.ValidateForm" />
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Trạng Thái" FieldName="BuyingServiceCategoryId.RowStatus">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="cbRowStatus" runat="server" Width="200px" 
                                                                SelectedIndex="0">
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="Hoạt động" Value="A" />
                                                                    <dx:ListEditItem Text="Ngưng hoạt động" Value="I" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Visible="false" Caption="Hình Ảnh">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                Width="300px">
                                                            </dx:ASPxImage>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="" Visible="false" ShowCaption="True" HelpText="Hình ảnh cho nhóm dịch vụ">
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
                    <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="wqwedsddssd22t2" runat="server" Height="100%" Width="100%">
                                    <Items>
                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxNavBar ID="navbarDetail" runat="server" AutoCollapse="True" Height="100%"
                                                        Width="100%">
                                                        <Groups>
                                                            <dx:NavBarGroup Name="Description" Text="Mô Tả">
                                                                <ContentTemplate>
                                                                    <dx:ASPxHtmlEditor ID="htmleditorDescription" runat="server" Height="350px" Width="100%">
                                                                        <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                    </dx:ASPxHtmlEditor>
                                                                </ContentTemplate>
                                                            </dx:NavBarGroup>
                                                            <dx:NavBarGroup Visible="false" Expanded="False" Text="Tài Liệu">
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
                                                    <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                        ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho nhóm dịch vụ">
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
