<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uMaufacturerGroupEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uMaufacturerGroupEdit" %>
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
    var ManufacturerCategoryEditForm = {
        Show: function (headerText, recordId) {
            if (recordId) {
                this._recordId = recordId;
                popManufacturerGroupEdit.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                popManufacturerGroupEdit.PerformCallback('new');
            }
            if (headerText) {
                popManufacturerGroupEdit.SetHeaderText(headerText);
            }
            popManufacturerGroupEdit.Show();

            /////2013-09-20 Khoa.Truong INS START
            $(ManufacturerCategoryEditForm).triggerHandler('shown');
            /////2013-09-20 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-20 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagManufacturerGroupEdit.GetMainElement(), null, true);
            if (validated) {
                if (!popManufacturerGroupEdit.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    popManufacturerGroupEdit.PerformCallback(args);
                }

                ////Will hide when saved
                //popManufacturerGroupEdit.Hide();

            }
            else {
                pagManufacturerGroupEdit.SetActiveTabIndex(0);
            }
            /////2013-09-20 Khoa.Truong MOD END
        },

        Hide: function () { popManufacturerGroupEdit.Hide(); },

        btnSave_Click: function (s, e) {
            ManufacturerCategoryEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            ManufacturerCategoryEditForm.Hide();
        },

        EndCallback: function (s, e) {

            /////2013-09-20 Khoa.Truong DEL START
            //            if (s.cpCallbackArgs) {
            //                var args = jQuery.parseJSON(s.cpCallbackArgs);
            //                $(ManufacturerCategoryEditForm).triggerHandler('saved', args);
            //            }
            //            delete s.cpCallbackArgs;
            /////2013-09-20 Khoa.Truong DEL END

            /////2013-09-20 Khoa.Truong INS START
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                popManufacturerGroupEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManufacturerCategoryEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
            /////2013-09-20 Khoa.Truong INS END
        },

        //Bind Saved Event
        BindSavedEvent: function (callback) {
            $(ManufacturerCategoryEditForm).on('saved', callback);
        },

        /////2013-09-20 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManufacturerCategoryEditForm).on('shown', callback);
        },
        /////2013-09-20 Khoa.Truong INS END

        /////2013-09-20 Khoa.Truong INS START
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
                        e.errorText = "Mã nhóm nhà sản xuất không được vượt quá 128 kí tự";
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
                        e.errorText = "Tên nhóm nhà sản xuất không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-20 Khoa.Truong INS END

    };

</script>
<div id="lineContainerManufacturerGroup">
    <dx:ASPxCallbackPanel ID="cpLineManufacturerGroup" runat="server" Width="100%" ClientInstanceName="cpLineManufacturerGroup"
        OnCallback="cpLineManufacturerGroup_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="popManufacturerGroupEdit" runat="server" HeaderText="Thông Tin Nhóm Nhà Sản Xuất - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="popManufacturerGroupEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True"
                    OnWindowCallback="popManufacturerGroupEdit_WindowCallback" CloseAction="CloseButton">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <LoadingPanelStyle HorizontalAlign="Center" VerticalAlign="Middle">
                    </LoadingPanelStyle>
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
                                    ClientInstanceName="btnCancel" CausesValidation="false" Text="Thoát" Wrap="False"
                                    ToolTip="Thoát  - Ctrl + C">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                    <ClientSideEvents Click="ManufacturerCategoryEditForm.btnCancel_Click" />
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnSave" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                    <ClientSideEvents Click="ManufacturerCategoryEditForm.btnSave_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:XpoDataSource ID="dsManufacturerCategory" runat="server" TypeName="DAL.Purchasing.ViewManufacturerCategory"
                                Criteria="[ManufacturerCategoryId] = ? And [Language] = ?">
                                <CriteriaParameters>
                                    <asp:Parameter Name="ManufacturerCategoryId" />
                                    <asp:Parameter Name="Language" DefaultValue="VN" />
                                </CriteriaParameters>
                            </dx:XpoDataSource>
                            <dx:ASPxPageControl ID="pagManufacturerGroupEdit" ClientInstanceName="pagManufacturerGroupEdit"
                                runat="server" ActiveTabIndex="0" Height="100%" RenderMode="Classic" Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="frmlManufactureCategory" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã nhóm nhà sản xuất" RequiredMarkDisplayMode="Required"
                                                                    HelpText="Tối đa 128 ký tự, không cho phép trùng lắp." FieldName="Code">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                                runat="server" Width="200px">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent>
                                                                                        <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                                            <ClientSideEvents Validation="ManufacturerCategoryEditForm.ValidateForm" />
                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập mã nhóm nhà sản xuất"></RequiredField>
                                                                                            </ValidationSettings>
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên nhóm nhà sản xuất" RequiredMarkDisplayMode="Required"
                                                                    HelpText="Tối đa 255 ký tự" FieldName="Name">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtName" runat="server" Width="400px">
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithText">
                                                                                    <RequiredField IsRequired="True" ErrorText="Chưa nhập tên nhóm nhà sản xuất"></RequiredField>
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents Validation="ManufacturerCategoryEditForm.ValidateForm" />
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng thái" HelpText="Trạng thái của nhóm nhà sản xuất" FieldName="RowStatus">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cbRowStatus" runat="server" Width="200px" SelectedIndex="0">
                                                                                <Items>
                                                                                    <dx:ListEditItem Selected="True" Text="Hoạt động" Value="A" />
                                                                                    <dx:ListEditItem Text="Ngưng hoạt động" Value="I" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Hình ảnh" Visible="False">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                                Width="300px">
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="   " ShowCaption="True" HelpText="Hình ành cho nhà sản xuất"
                                                                    Visible="False">
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
                                                <dx:ASPxFormLayout ID="ddeess22t2" runat="server" Height="100%" Width="100%">
                                                    <Items>
                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
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
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                        ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho nhóm nhà sản xuất">
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
                    <ClientSideEvents EndCallback="ManufacturerCategoryEditForm.EndCallback" />
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
