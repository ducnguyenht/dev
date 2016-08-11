<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingService.ascx.cs"
    Inherits="DXApplication1.Purchasing.UserControl.uBuyingService" %>
<script type="text/javascript">

    /////2013-09-25 ERP-608 Khoa.Truong INS START
    var BuyingServiceEditForm = {
        Show: function (headerText, recordId) {
            if (recordId) {
                this._recordId = recordId;
                popBuyingService.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                popBuyingService.PerformCallback('new');
            }
            if (headerText) {
                popBuyingService.SetHeaderText(headerText);
            }
            popBuyingService.Show();

            $(BuyingServiceEditForm).triggerHandler('shown');
        },
        Save: function () {
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagBuyingService.GetMainElement(), null, true);
            if (validated) {
                if (!popBuyingService.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    popBuyingService.PerformCallback(args);
                }
            }
            else {
                pagBuyingService.SetActiveTabIndex(0);
            }
        },
        Hide: function () { popBuyingService.Hide(); },

        btnSave_Click: function (s, e) {
            BuyingServiceEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            BuyingServiceEditForm.Hide();
        },

        EndCallback: function (s, e) {
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                popBuyingService.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(BuyingServiceEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
        },

        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(BuyingServiceEditForm).on('saved', callback);
        },

        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(BuyingServiceEditForm).on('shown', callback);
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
                        e.errorText = "Mã dịch vụ không được vượt quá 128 kí tự";
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
                        e.errorText = "Tên dịch vụ không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
    };
    /////2013-09-25 ERP-608 Khoa.Truong INS END

    function formServiceEdit_Init(s, e) {
        //        grdServiceEquivalence.SetHeight($("#testheight").height() - 90);
        //        grdSeviceCategory.SetHeight($("#testheight").height() - 120);
    }

    function formServiceEdit_AfterResizing(s, e) {
        //        grdServiceEquivalence.SetHeight($("#testheight").height() - 90);
        //        grdSeviceCategory.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    }

    function grdSeviceCategory_BuyingServiceCategoryId_SelectedIndexChanged(s, e) {
        var editor = grdSeviceCategory.GetEditor('Name');
        if (editor) {
            editor.SetValue(s.GetSelectedItem().GetColumnText('Name'));
        }
    }

    function grdSeviceSupplier_SupplierId_SelectedIndexChanged(s, e) {
        var editor = grdSeviceSupplier.GetEditor('Name');
        if (editor) {
            editor.SetValue(s.GetSelectedItem().GetColumnText('Name'));
        }
    }

    function grdServiceEquivalence_BuyingServiceId_SelectedIndexChanged(s, e) {
        var editor = grdServiceEquivalence.GetEditor('BuyingServiceName');
        if (editor) {
            editor.SetValue(s.GetSelectedItem().GetColumnText('Name'));
        }
    }

</script>
<dx:ASPxPopupControl ID="popBuyingService" runat="server" HeaderText="Thông Tin Dịch Vụ"
    Height="600px" Modal="True" Width="900px" ClientInstanceName="popBuyingService"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True"
    CloseAction="CloseButton" OnWindowCallback="popBuyingService_WindowCallback">
    <ClientSideEvents Init="formServiceEdit_Init" EndCallback="BuyingServiceEditForm.EndCallback"
        AfterResizing="formServiceEdit_AfterResizing"></ClientSideEvents>
    <FooterTemplate>
        <div style="padding: 10px;">
            <div class="float-left">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnHelpService" UseSubmitBehavior="false" CausesValidation="false"
                        AutoPostBack="false" runat="server" Text="Trợ giúp" ToolTip="Ctrl + H">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="float-right">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton UseSubmitBehavior="true" ID="btnSaveService" runat="server" AutoPostBack="False"
                        Text="Lưu lại" ToolTip="Enter">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Apply" />
                        </Image>
                        <ClientSideEvents Click="BuyingServiceEditForm.btnSave_Click" />
                    </dx:ASPxButton>
                </div>
                <div class="float-left button-left-margin">
                    <dx:ASPxButton ID="btnCancelService" UseSubmitBehavior="false" CausesValidation="false"
                        runat="server" AutoPostBack="False" Text="Thoát" ToolTip="Esc">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                        <ClientSideEvents Click="BuyingServiceEditForm.btnCancel_Click" />
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pagBuyingService" ClientInstanceName="pagBuyingService" runat="server"
                ActiveTabIndex="4" Height="100%" RenderMode="Classic" Width="100%">
                <TabPages>
                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:XpoDataSource ID="dsBuyingServiceProperty" runat="server" DefaultSorting="" TypeName="DAL.Purchasing.BuyingServiceProperty"
                                    Criteria="[Language] = ? And [BuyingServiceId.BuyingServiceId] = ?">
                                    <CriteriaParameters>
                                        <asp:Parameter DefaultValue="VN" Name="Language" />
                                        <asp:Parameter DefaultValue="" Name="BuyingServiceId" />
                                    </CriteriaParameters>
                                </dx:XpoDataSource>
                                <dx:ASPxFormLayout ID="frmBuyingService" runat="server" Width="100%" DataSourceID="dsBuyingServiceProperty">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã dịch vụ" FieldName="BuyingServiceId.Code" HelpText="Tối đa 128 ký tự"
                                            RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                        runat="server" Width="200px">
                                                        <PanelCollection>
                                                            <dx:PanelContent>
                                                                <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                    <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                        <RequiredField IsRequired="True" ErrorText="Chưa nhập mã dịch vụ"></RequiredField>
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents Validation="BuyingServiceEditForm.ValidateForm" />
                                                                </dx:ASPxTextBox>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên dịch vụ" FieldName="Name" HelpText="Tối đa 255 ký tự"
                                            RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtName" runat="server" Width="400px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                            <RequiredField IsRequired="True" ErrorText="Chưa nhập tên dịch vụ"></RequiredField>
                                                        </ValidationSettings>
                                                        <ClientSideEvents Validation="BuyingServiceEditForm.ValidateForm" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Trạng thái" FieldName="BuyingServiceId.RowStatus">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cbRowStatus" runat="server" SelectedIndex="0" Width="200px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="True" Text="Hoạt động" Value="A" />
                                                            <dx:ListEditItem Text="Ngưng hoạt động" Value="I" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Hình Ảnh" Visible="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                        Width="300px">
                                                    </dx:ASPxImage>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="" HelpText="Hình ảnh cho dịch vụ" ShowCaption="True" Visible="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxUploadControl ID="ASPxFormLayout1235_E6" runat="server" UploadMode="Auto"
                                                        Width="280px">
                                                    </dx:ASPxUploadControl>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Trực Thuộc Nhóm">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="wwffPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                    <Items>
                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:XpoDataSource ID="dsBuyingServiceCategoryProperty" runat="server" DefaultSorting=""
                                                        TypeName="DAL.Purchasing.BuyingServiceCategoryProperty" Criteria="[Language] = ? And [BuyingServiceCategoryId.RowStatus] = 'A'">
                                                        <CriteriaParameters>
                                                            <asp:Parameter DefaultValue="VN" Name="Language" />
                                                        </CriteriaParameters>
                                                    </dx:XpoDataSource>
                                                    <dx:ASPxGridView ID="grdSeviceCategory" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" ClientInstanceName="grdSeviceCategory" KeyFieldName="BuyingServiceCategoryId"
                                                        OnRowDeleting="grdSeviceCategory_RowDeleting" OnRowInserting="grdSeviceCategory_RowInserting"
                                                        OnRowValidating="grdSeviceCategory_RowValidating">
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã nhóm dịch vụ" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="150px" FieldName="BuyingServiceCategoryId">
                                                                <PropertiesComboBox EnableCallbackMode="True" DataSourceID="dsBuyingServiceCategoryProperty"
                                                                    IncrementalFilteringMode="Contains" LoadDropDownOnDemand="True" TextFormatString="{0}"
                                                                    ValueField="BuyingServiceCategoryId!Key" ValueType="System.Guid">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã nhóm dịch vụ" FieldName="BuyingServiceCategoryId.Code" />
                                                                        <dx:ListBoxColumn Caption="Tên nhóm dịch vụ" FieldName="Name" />
                                                                    </Columns>
                                                                    <Buttons>
                                                                        <dx:EditButton>
                                                                            <Image ToolTip="Thêm mới nhóm dịch vụ">
                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                            </Image>
                                                                        </dx:EditButton>
                                                                    </Buttons>
                                                                    <ValidationSettings>
                                                                        <RequiredField ErrorText="Chưa chọn nhóm dịch vụ" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents SelectedIndexChanged="grdSeviceCategory_BuyingServiceCategoryId_SelectedIndexChanged" />
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên nhóm dịch vụ" ShowInCustomizationForm="True"
                                                                VisibleIndex="1" Width="250px" FieldName="Name">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="4" Width="100px">
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
                                                        <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>
                                                        <SettingsEditing Mode="Inline" />
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
                                        <dx:LayoutItem ShowCaption="False" Height="20px" VerticalAlign="Middle">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                        ForeColor="#CCCCCC" Text="Chọn Nhóm Dịch Vụ">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                            <CaptionSettings VerticalAlign="Middle" />
                                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                        </dx:LayoutItem>
                                    </Items>
                                    <SettingsItems VerticalAlign="Middle" />
                                    <SettingsItems VerticalAlign="Middle"></SettingsItems>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Nhà cung cấp">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:XpoDataSource ID="dsSupplierProperty" runat="server" Criteria="[Language] = ? And [SupplierId.RowStatus] = 'A'"
                                    DefaultSorting="" TypeName="DAL.Purchasing.SupplierProperty">
                                    <CriteriaParameters>
                                        <asp:Parameter DefaultValue="VN" Name="Language" />
                                    </CriteriaParameters>
                                </dx:XpoDataSource>
                                <dx:ASPxFormLayout ID="frmBuyingServiceSupplier" runat="server" Height="100%" Width="100%">
                                    <Items>
                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="grdSeviceSupplier" runat="server" AutoGenerateColumns="False"
                                                        ClientInstanceName="grdSeviceSupplier" Width="100%" OnRowDeleting="grdSeviceSupplier_RowDeleting"
                                                        OnRowInserting="grdSeviceSupplier_RowInserting" OnRowValidating="grdSeviceSupplier_RowValidating"
                                                        KeyFieldName="SupplierId">
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã nhà cung cấp" FieldName="SupplierId" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="150px">
                                                                <PropertiesComboBox EnableCallbackMode="True" DataSourceID="dsSupplierProperty" IncrementalFilteringMode="Contains"
                                                                    ValueField="SupplierId!Key" ValueType="System.Guid" TextFormatString="{0}" LoadDropDownOnDemand="True">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã nhà cung cấp" FieldName="SupplierId.Code" />
                                                                        <dx:ListBoxColumn Caption="Tên nhà cung cấp" FieldName="Name" />
                                                                    </Columns>
                                                                    <Buttons>
                                                                        <dx:EditButton>
                                                                            <Image ToolTip="Thêm mới nhà cung cấp">
                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                            </Image>
                                                                        </dx:EditButton>
                                                                    </Buttons>
                                                                    <ValidationSettings>
                                                                        <RequiredField ErrorText="Chưa chọn nhà cung cấp" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents SelectedIndexChanged="grdSeviceSupplier_SupplierId_SelectedIndexChanged" />
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên nhà cung cấp" FieldName="Name" ShowInCustomizationForm="True"
                                                                VisibleIndex="1" Width="250px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="4" Width="100px">
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
                                                            <Header Font-Bold="True" HorizontalAlign="Center">
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
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout2_E2" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                        ForeColor="#CCCCCC" Text="Chọn nhà cung cấp">
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
                                <dx:ASPxFormLayout ID="wqwedsdssd22t2" runat="server" Height="100%" Width="100%">
                                    <Items>
                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxNavBar ID="navbarDetailInfo" runat="server" AutoCollapse="True" Height="100%"
                                                        Width="100%">
                                                        <Groups>
                                                            <dx:NavBarGroup Text="Mô Tả" Name="Description">
                                                                <ContentTemplate>
                                                                    <dx:ASPxHtmlEditor ID="htmleditorDescription" runat="server" Height="350px" Width="100%">
                                                                        <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                    </dx:ASPxHtmlEditor>
                                                                </ContentTemplate>
                                                            </dx:NavBarGroup>
                                                            <dx:NavBarGroup Expanded="False" Text="Tài Liệu" Visible="False">
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
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                        ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho dịch vụ">
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
                    <dx:TabPage Text="Dịch Vụ Tương Đương">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                <dx:XpoDataSource ID="dsBuyingServiceEquivalence" runat="server" 
                                    Criteria="[Language] = ? And [BuyingServiceId.RowStatus] = 'A' And [BuyingServiceId.BuyingServiceId] <> ?"
                                    TypeName="DAL.Purchasing.BuyingServiceProperty" DefaultSorting="">
                                    <CriteriaParameters>
                                        <asp:Parameter DefaultValue="VN" Name="Language" />
                                        <asp:Parameter DefaultValue="" Name="ForBuyingServiceId" />
                                    </CriteriaParameters>
                                </dx:XpoDataSource>
                                <dx:ASPxFormLayout ID="wfffPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                    <Items>
                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="grdServiceEquivalence" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" ClientInstanceName="grdServiceEquivalence"
                                                        KeyFieldName="EquivalentBuyingServicetId" 
                                                        OnRowDeleting="grdServiceEquivalence_RowDeleting" 
                                                        OnRowInserting="grdServiceEquivalence_RowInserting" 
                                                        OnRowUpdating="grdServiceEquivalence_RowUpdating" 
                                                        OnRowValidating="grdServiceEquivalence_RowValidating">
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã dịch vụ" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="150px" FieldName="EquivalentBuyingServicetId">
                                                                <PropertiesComboBox EnableCallbackMode="True" DataSourceID="dsBuyingServiceEquivalence" IncrementalFilteringMode="Contains"
                                                                    ValueField="BuyingServiceId!Key" ValueType="System.Guid" TextFormatString="{0}" LoadDropDownOnDemand="True">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã dịch vụ" FieldName="BuyingServiceId.Code" />
                                                                        <dx:ListBoxColumn Caption="Tên dịch vụ" FieldName="Name" />
                                                                    </Columns>
                                                                    <ValidationSettings>
                                                                        <RequiredField ErrorText="Chưa chọn dịch vụ tương đương" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents SelectedIndexChanged="grdServiceEquivalence_BuyingServiceId_SelectedIndexChanged" />
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên dịch vụ" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                FieldName="BuyingServiceName">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mô tả" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                FieldName="Description">
                                                                <PropertiesTextEdit>
                                                                    <ValidationSettings>
                                                                        <RequiredField ErrorText="Chưa nhập mô tả dịch vụ tương đương" 
                                                                            IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="4" Width="100px">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                    </Image>
                                                                </DeleteButton>
                                                                <ClearFilterButton Visible="True">
                                                                    <Image ToolTip="Hủy">
                                                                        <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                    </Image>
                                                                </ClearFilterButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                    </Image>
                                                                </CancelButton>
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <SettingsPager PageSize="50" RenderMode="Classic" ShowEmptyDataRows="True">
                                                        </SettingsPager>
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
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                        ForeColor="#CCCCCC" Text="Chọn Dịch Vụ Tương Đương">
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
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
