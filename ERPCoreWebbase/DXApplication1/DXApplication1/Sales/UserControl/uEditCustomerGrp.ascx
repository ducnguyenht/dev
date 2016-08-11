<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditCustomerGrp.ascx.cs"
    Inherits="WebModule.GUI.Sales.userControl.uEditCustomerGrp" %>
<script type="text/javascript">
    var CustomerCategoryEditForm = {
        Show: function (headerText, recordId) {
            if (recordId) {
                this._recordId = recordId;
                popCustomerCategory.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                popCustomerCategory.PerformCallback('new');
            }
            if (headerText) {
                popCustomerCategory.SetHeaderText(headerText);
            }
            popCustomerCategory.Show();

            /////2013-09-23 ERP-570 Khoa.Truong INS START
            $(CustomerCategoryEditForm).triggerHandler('shown');
            /////2013-09-23 ERP-570 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-23 ERP-570 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagCustomerCategory.GetMainElement(), null, true);
            if (validated) {
                if (!popCustomerCategory.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    popCustomerCategory.PerformCallback(args);
                    //popCustomerCategory.Hide();
                }
            }
            else {
                pagCustomerCategory.SetActiveTabIndex(0);
            }
            /////2013-09-23 ERP-570 Khoa.Truong MOD END
        },
        Hide: function () { popCustomerCategory.Hide(); },

        btnSave_Click: function (s, e) {
            CustomerCategoryEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            CustomerCategoryEditForm.Hide();
        },

        EndCallback: function (s, e) {

            /////2013-09-23 ERP-570 Khoa.Truong DEL START
            //            if (s.cpCallbackArgs) {
            //                var args = jQuery.parseJSON(s.cpCallbackArgs);
            //                $(CustomerCategoryEditForm).triggerHandler('saved', args);
            //            }
            //            delete s.cpCallbackArgs;
            /////2013-09-23 ERP-570 Khoa.Truong DEL END

            /////2013-09-23 ERP-570 Khoa.Truong INS START
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                popCustomerCategory.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(CustomerCategoryEditForm).triggerHandler('saved', args);
                console.log(s.cpCallbackArgs);
                delete s.cpCallbackArgs;
            }
            /////2013-09-23 ERP-570 Khoa.Truong INS END

        },

        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(CustomerCategoryEditForm).on('saved', callback);
        },

        /////2013-09-23 ERP-570 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(CustomerCategoryEditForm).on('shown', callback);
        },
        /////2013-09-23 ERP-570 Khoa.Truong INS END

        /////2013-09-23 ERP-570 Khoa.Truong INS START
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
                        e.errorText = "Mã nhóm khách hàng không được vượt quá 128 kí tự";
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
                        e.errorText = "Tên nhóm khách hàng không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-23 ERP-570 Khoa.Truong INS END

    };
</script>
<dx:aspxpopupcontrol id="popCustomerCategory" clientinstancename="popCustomerCategory"
    runat="server" allowdragging="True" allowresize="True" headertext="Thông Tin Nhóm Khách Hàng"
    modal="True" width="900px" height="500px" scrollbars="Auto" popuphorizontalalign="WindowCenter"
    popupverticalalign="WindowCenter" showfooter="True" 
    onwindowcallback="popCustomerCategory_WindowCallback" 
    ShowMaximizeButton="True">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pagCustomerCategory" ClientInstanceName="pagCustomerCategory" runat="server" ActiveTabIndex="0" Height="100%"
                RenderMode="Classic" Width="100%">
                <TabPages>
                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:XpoDataSource ID="dsCustomerCategory" runat="server" 
                                    Criteria="[CustomerCategoryId!Key] = ? And [Language] = ?" 
                                    TypeName="DAL.Sale.CustomerCategoryProperty">
                                    <CriteriaParameters>
                                        <asp:Parameter DefaultValue="" Name="CustomerCategoryId" />
                                        <asp:Parameter DefaultValue="VN" Name="Language" />
                                    </CriteriaParameters>
                                </dx:XpoDataSource>
                                <dx:ASPxFormLayout ID="frmCustomerCategoryGeneralInfo" runat="server" 
                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%" 
                                    DataSourceID="dsCustomerCategory">
                                    <Items>
                                        <dx:LayoutGroup ShowCaption="False">
                                            <Items>
                                                <dx:LayoutItem Caption="Mã Nhóm Khách Hàng" RequiredMarkDisplayMode="Required" FieldName="CustomerCategoryId.Code">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                runat="server" Width="200px">
                                                                <PanelCollection>
                                                                    <dx:PanelContent>
                                                                        <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập mã nhóm khách hàng"></RequiredField>
                                                                            </ValidationSettings>
                                                                            <ClientSideEvents Validation="CustomerCategoryEditForm.ValidateForm" />
                                                                        </dx:ASPxTextBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                    </CaptionCellStyle>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tên Nhóm Khách Hàng" RequiredMarkDisplayMode="Required" FieldName="Name">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtNameDevice" MaxLength="255" Width="400px">
                                                                <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                    <RequiredField IsRequired="True" ErrorText="Chưa nhập tên nhóm khách hàng"></RequiredField>
                                                                </ValidationSettings>
                                                                <ClientSideEvents Validation="CustomerCategoryEditForm.ValidateForm" />
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Trạng Thái" FieldName="CustomerCategoryId.RowStatus">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="cbRowStatus" runat="server" 
                                                                ClientInstanceName="cboRowStatusDevice" NullText="Tự động tạo mới" 
                                                                Width="200px" SelectedIndex="0">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Hoạt động" Value="A" Selected="True" />
                                                                    <dx:ListEditItem Text="Ngưng hoạt động" Value="I" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Hình Ảnh" Visible="False">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" 
                                                                ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
<Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"></Border>
                                                            </dx:ASPxImage>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                    </CaptionCellStyle>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption=" " Visible="False">
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
                                <div class="quickHelp">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Italic="False" ForeColor="Gray"                                                        
                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp" 
                                            Font-Bold="False" Font-Size="XX-Small">
                                    </dx:ASPxLabel>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxNavBar ID="navbarDetail" runat="server" AutoCollapse="True" 
                                    Height="100%" Width="100%">
                                    <Groups>
                                        <dx:NavBarGroup Name="Description" Text="Mô Tả">
                                            <ContentTemplate>
                                                        <dx:ASPxHtmlEditor ID="htmleditorDescription" runat="server" Height="350px" 
                                                            Width="100%">
                                                            <Settings AllowHtmlView="False" AllowPreview="False" />
                                                        </dx:ASPxHtmlEditor>
                                            </ContentTemplate>
                                        </dx:NavBarGroup>
                                        <dx:NavBarGroup Expanded="False" Text="Tài Liệu" Visible="False">
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
                                    <div class="quickHelp">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="False" ForeColor="Gray"                                                        
                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp" 
                                            Font-Bold="False" Font-Size="XX-Small">
                                    </dx:ASPxLabel>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>   
    </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents EndCallback="CustomerCategoryEditForm.EndCallback"></ClientSideEvents>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                    <spriteproperties cssclass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="btnCancel" clientinstancename="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                <clientsideevents click="CustomerCategoryEditForm.btnCancel_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                    <spriteproperties cssclass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                         <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton AutoPostBack="false" ID="btnApply" clientinstancename="btnApply" runat="server" Text="Lưu lại">
                                <clientsideevents click="CustomerCategoryEditForm.btnSave_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                    <spriteproperties cssclass="Sprite_Apply" />
                                </Image>
                                
                            </dx:ASPxButton>
                        </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:aspxpopupcontrol>
