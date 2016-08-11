<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uDeviceUnit.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.uDeviceUnit" %>
<style type="text/css">
    .dxbButton_DevEx
    {
        color: #201f35;
        font: normal 11px Verdana, Geneva, sans-serif;
        vertical-align: middle;
        border: 1px solid #a9acb5;
        padding: 1px;
        cursor: pointer;
    }
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
        //grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        //grmanufacturer0.SetHeight($("#testheight").height() - 120);
    }
    function formManufacturerEdit_AfterResizing(s, e) {
        // grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        // grmanufacturer0.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    }

    var ManuDeviceUnitEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                formDeviceUnitEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                formDeviceUnitEdit.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                formDeviceUnitEdit.PerformCallback('new');
            }
            formDeviceUnitEdit.Show();
            /////2013-09-21 ERP-580 Khoa.Truong INS START
            $(ManuDeviceUnitEditForm).triggerHandler('shown');
            /////2013-09-22 ERP-580 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-20 ERP-580 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagDeviceUnitEdit.GetMainElement(), null, true);
            if (validated) {
                if (!formDeviceUnitEdit.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    formDeviceUnitEdit.PerformCallback(args);
                }
            }
            else {
                pagDeviceUnitEdit.SetActiveTabIndex(0);
            }
            //formDeviceUnitEdit.Hide();
            /////2013-09-20 ERP-580 Khoa.Truong MOD END
        },
        Hide: function () { formDeviceUnitEdit.Hide(); },

        btnSave_Click: function (s, e) {
            ManuDeviceUnitEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            ManuDeviceUnitEditForm.Hide();
        },

        EndCallback: function (s, e) {
            /////2013-09-21 ERP-580 Khoa.Truong DEL START
            //            if (s.cpCallbackArgs) {
            //                var args = jQuery.parseJSON(s.cpCallbackArgs);
            //                $(ManuDeviceUnitEditForm).triggerHandler('saved', args);
            //            }
            //            delete s.cpCallbackArgs;
            /////2013-09-21 ERP-580 Khoa.Truong DEL END

            /////2013-09-21 ERP-580 Khoa.Truong INS START
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                formDeviceUnitEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManuDeviceUnitEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS END

        },

        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(ManuDeviceUnitEditForm).on('saved', callback);
        },

        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManuDeviceUnitEditForm).on('shown', callback);
        },
        /////2013-09-21 ERP-580 Khoa.Truong INS END

        /////2013-09-21 ERP-580 Khoa.Truong INS START
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
                        e.errorText = "Mã đơn vị tính không được vượt quá 128 kí tự";
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
                        e.errorText = "Tên đơn vị tính không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END

    };
</script>
<dx:ASPxCallbackPanel ID="cpLineMaterialUnit" runat="server" Width="100%" ClientInstanceName="cpLineMaterialUnit">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxHiddenField ID="hiddenMode" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxPopupControl ID="formDeviceUnitEdit" runat="server" HeaderText="Thông tin đơn vị tính - "
                Height="600px" Modal="True" Width="900px" ClientInstanceName="formDeviceUnitEdit"
                AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                OnWindowCallback="popDeviceCategoryEdit_WindowCallback" ShowFooter="true" ShowSizeGrip="False"
                AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                <ClientSideEvents Init="formManufacturerEdit_Init" AfterResizing="formManufacturerEdit_AfterResizing"
                    EndCallback="ManuDeviceUnitEditForm.EndCallback"></ClientSideEvents>
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
                            <dx:ASPxButton ID="buttonCancelMaterialUnit" runat="server" AutoPostBack="False"
                                CssClass="float_right dl mg" ClientInstanceName="buttonCancelMaterialUnit" Text="Thoát"
                                Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                <ClientSideEvents Click="ManuDeviceUnitEditForm.btnCancel_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonSaveMaterialUnit" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonSaveMaterialUnit" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S"
                                CausesValidation="true">
                                <ClientSideEvents Click="ManuDeviceUnitEditForm.btnSave_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </div>
                </FooterContentTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPageControl ID="pagDeviceUnitEdit" ClientInstanceName="pagDeviceUnitEdit"
                            runat="server" ActiveTabIndex="0" Height="100%" Width="100%">
                            <TabPages>
                                <dx:TabPage Name="tabGeneral" Text="Thông tin chung">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="frmlInfoGeneral" runat="server" AlignItemCaptionsInAllGroups="True"
                                                EnableTheming="True" Width="100%">
                                                <Items>
                                                    <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Mã đơn vị tính" FieldName="Code" 
                                                                HelpText="Tối đa 128 ký tự, không trùng lắp" RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                            runat="server" Width="200px">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent>
                                                                                    <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                                        <ClientSideEvents Validation="ManuDeviceUnitEditForm.ValidateForm"></ClientSideEvents>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                            <RequiredField IsRequired="True" ErrorText="Chưa nhập mã đơn vị tính"></RequiredField>
                                                                                        </ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                                <CaptionCellStyle CssClass="CaptionStyle">
                                                                </CaptionCellStyle>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tên đơn vị tính" FieldName="Name" 
                                                                HelpText="Tối đa 255 ký tự">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" MaxLength="255"
                                                                            Width="400px">
                                                                            <ClientSideEvents Validation="ManuDeviceUnitEditForm.ValidateForm"></ClientSideEvents>
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="True">
                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập tên đơn vị tính" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Trạng thái" FieldName="RowStatus" HelpText="Tự động tạo mới">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="cbRowStatus" runat="server" ClientInstanceName="cbRowStatus"
                                                                            Width="200px">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                                <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="I" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <%--<dx:LayoutItem Caption="Hình Ảnh">
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
                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho ĐVT">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" 
                                                                                Width="300px">
                                                                            </dx:ASPxUploadControl>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>--%>
                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="tabDetail" Text="Thông tin chi tiết">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout ID="ASPxFsd22t2" runat="server" Height="100%" Width="100%">
                                                <Items>
                                                    <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxNavBar ID="navbarDetailInfo" runat="server" AutoCollapse="True" Width="100%"
                                                                    Height="100%">
                                                                    <Groups>
                                                                        <dx:NavBarGroup Text="Mô tả" Name="Description">
                                                                            <ContentTemplate>
                                                                                <dx:ASPxHtmlEditor ID="htmleditorDescription" runat="server" Height="350px" Width="100%">
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
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                    ForeColor="#CCCCCC" Text="Cập nhật mô tả và tài liệu ">
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
