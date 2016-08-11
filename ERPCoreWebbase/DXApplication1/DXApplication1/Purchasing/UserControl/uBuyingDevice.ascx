<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingDevice.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.uBuyingDevice" %>
<style type="text/css">
    .dxtlHSEC
    {
        width:0px;
        display:none;
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

    var ManuDeviceEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                formDeviceEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                cpLineDevice.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                //formDeviceEdit.SetHeaderText("Thêm Mới Công Cụ Dụng Cụ");
                formDeviceEdit.PerformCallback('new');
                formDeviceEdit.Show();
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS START
            $(ManuDeviceEditForm).triggerHandler('shown');
            /////2013-09-22 ERP-580 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-20 ERP-580 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagDeviceEdit.GetMainElement(), null, true);
            if (validated) {
                if (!formDeviceEdit.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    formDeviceEdit.PerformCallback(args);
                }
            }
            else {
                pagDeviceEdit.SetActiveTabIndex(0);
            }
            //formDeviceEdit.Hide();
            /////2013-09-20 ERP-580 Khoa.Truong MOD END
        },
        Hide: function () { formDeviceEdit.Hide(); },

        btnSave_Click: function (s, e) {
            ManuDeviceEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            var args = 'cancel';
            if (this._recordId) {
                args += '|' + this._recordId;
            }
            formDeviceEdit.PerformCallback(args);
            ManuDeviceEditForm.Hide();
        },

        EndCallback: function (s, e) {

            /////2013-09-21 ERP-580 Khoa.Truong DEL START
//            if (s.cpCallbackArgs) {
//                var args = jQuery.parseJSON(s.cpCallbackArgs);
//                $(ManuDeviceEditForm).triggerHandler('saved', args);
//            }
//            delete s.cpCallbackArgs;
            /////2013-09-21 ERP-580 Khoa.Truong DEL START

            /////2013-09-21 ERP-580 Khoa.Truong INS START
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                ManuDeviceEditForm.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManuDeviceEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS END

        },

        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(ManuDeviceEditForm).on('saved', callback);
        },

        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManuDeviceEditForm).on('shown', callback);
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
                        e.errorText = "Mã công cụ dụng cụ không được vượt quá 128 kí tự";
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
                        e.errorText = "Tên công cụ dụng cụ không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END

    };


    /////2013-09-21 ERP-580 Duc.Vo INS START
    function cboCodeDeviceCategory_SelectedIndexChanged(s, e) {
        var itemDeviceGrp = s.GetSelectedItem("Name");
        if (itemDeviceGrp != null) {
            var txtDeviceGrp = itemDeviceGrp.GetColumnText("Name");
            var editor = grdataBuyingToolCategory.GetEditor('Name');
            editor.SetValue(txtDeviceGrp);
        }
    }

    function cboCodeSupplier_SelectedIndexChanged(s, e) {
        var itemSupplier = s.GetSelectedItem("Name");
        if (itemSupplier != null) {
            var txtSupplier = itemSupplier.GetColumnText("Name");
            var editor = grdataSupplier.GetEditor('Name');
            editor.SetValue(txtSupplier);
        }
    }

    function cboCodeUnit_SelectedIndexChanged(s, e) {
        var itemUnit = s.GetSelectedItem("Name");
        if (itemUnit != null) {
            var txtUnit = itemUnit.GetColumnText("Name");
            var editor = treelstToolUnits.GetEditor('Name');
            editor.SetValue(txtUnit);
        }
    }
    /////2013-09-21 ERP-580 Duc.Vo INS END
</script>
<div id="lineContainerDevice">
    <dx:ASPxCallbackPanel ID="cpLineDevice" runat="server" Width="100%" ClientInstanceName="cpLineDevice"
        OnCallback="cpLineDevice_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formDeviceEdit" runat="server" HeaderText="Thông tin công cụ dụng cụ - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formDeviceEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True"
                    EnableViewState="False" OnWindowCallback="popDeviceEdit_WindowCallback" CloseAction="CloseButton"
                    ShowShadow="False">
                    <ClientSideEvents Init="formManufacturerEdit_Init" AfterResizing="formManufacturerEdit_AfterResizing"
                        EndCallback="ManuDeviceEditForm.EndCallback"></ClientSideEvents>
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
                                    <ClientSideEvents Click="ManuDeviceEditForm.btnCancel_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="ManuDeviceEditForm.btnSave_Click" />
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
                                <dx:ASPxPageControl ID="pagDeviceEdit" ClientInstanceName="pagDeviceEdit" runat="server"
                                    ActiveTabIndex="0" Width="100%" Height="100%" EnableTabScrolling="True">
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông tin chung">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                   <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx" style="width: 100%;">
                                                        <tr>
                                                            <td valign="top">
                                                                <dx:XpoDataSource ID="cboManufactureXPO" runat="server" TypeName="DAL.Purchasing.ViewManufacturer"
                                                                    DefaultSorting="">
                                                                </dx:XpoDataSource>
                                                                <dx:ASPxFormLayout ID="frmlInfoGeneral" runat="server" EnableTheming="True" Width="100%"
                                                                    AlignItemCaptionsInAllGroups="True" Height="100%" ClientInstanceName="ASPxFormLayout1">
                                                                    <Items>
                                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                                            <Items>
                                                                                <dx:LayoutItem Caption="Mã công cụ dụng cụ" FieldName="Code" CaptionCellStyle-CssClass="CaptionStyle"
                                                                                    HelpText="Tối đa 128 ký tự, không cho trùng lắp" RequiredMarkDisplayMode="Required">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                                                runat="server" Width="200px">
                                                                                                <PanelCollection>
                                                                                                    <dx:PanelContent>
                                                                                                        <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" 
                                                                                                            OnValidation="txtCode_Validation" OnTextChanged="txtCode_TextChanged">
                                                                                                            <ClientSideEvents Validation="ManuDeviceEditForm.ValidateForm" ></ClientSideEvents>
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập mã công cụ dụng cụ"></RequiredField>
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
                                                                                <dx:LayoutItem Caption="Tên công cụ dụng cụ" FieldName="Name" HelpText="255 ký tự">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" MaxLength="255"
                                                                                                Width="400px">
                                                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                                    <RequiredField ErrorText="Chưa nhập tên công cụ dụng cụ" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa nhập t&#234;n c&#244;ng cụ dụng cụ"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                                <ClientSideEvents Validation="ManuDeviceEditForm.ValidateForm"></ClientSideEvents>
                                                                                            </dx:ASPxTextBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Trạng thái" HelpText="Tự động tạo mới" FieldName="RowStatus">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxComboBox ID="cbRowStatus" runat="server" Width="200px" SelectedIndex="0">
                                                                                                <Items>
                                                                                                    <dx:ListEditItem Selected="True" Text="Hoạt động" Value="A" />
                                                                                                    <dx:ListEditItem Text="Ngưng hoạt động" Value="I" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Nhà sản xuất" CaptionCellStyle-CssClass="captionStyle" HelpText="Chọn nhà sản xuất"
                                                                                    FieldName="ManufacturerId">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxComboBox ID="cboManufacturer" runat="server" CallbackPageSize="20" ClientInstanceName="cboManufacturer"
                                                                                                DropDownHeight="200px" DropDownWidth="450px" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                                TextField="Name" TextFormatString="{0};{1}" ValueField="ManufacturerId" Width="400px"
                                                                                                DataSourceID="cboManufactureXPO">
                                                                                                <Columns>
                                                                                                    <dx:ListBoxColumn Caption="Mã nhà sản xuất" FieldName="Code" Name="Code" Width="150px" />
                                                                                                    <dx:ListBoxColumn Caption="Tên nhà sản xuất" FieldName="Name" Name="Name" Width="300px" />
                                                                                                    <dx:ListBoxColumn Caption="Key" FieldName="ManufacturerId" Name="ManufacturerId"
                                                                                                        Width="150px" Visible="false" />
                                                                                                </Columns>
                                                                                                <ValidationSettings SetFocusOnError="True">
                                                                                                    <RequiredField ErrorText="Chưa chọn nhà sản xuất" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa chọn nh&#224; sản xuất"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxComboBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                                    </CaptionCellStyle>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Hình ảnh" CaptionCellStyle-CssClass="captionStyle" Visible="False">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                                                Width="300px">
                                                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                                                <Border BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid"></Border>
                                                                                            </dx:ASPxImage>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                                    </CaptionCellStyle>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho CCDC" Visible="false">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" Width="300px">
                                                                                            </dx:ASPxUploadControl>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                            </Items>
                                                                        </dx:LayoutGroup>
                                                                    </Items>
                                                                </dx:ASPxFormLayout>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Trực thuộc nhóm">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdataBuyingToolCategory" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdataBuyingToolCategory" KeyFieldName="Code" Width="100%"
                                                                            OnRowDeleting="grdataBuyingToolCategory_RowDeleting" OnRowValidating="grdataBuyingToolCategory_RowValidating"
                                                                            OnRowInserting="grdataBuyingToolCategory_RowInserting">

<SettingsEditing Mode="Inline"></SettingsEditing>

                                                                            <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã nhóm CCDC" FieldName="Code" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px">
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxComboBox ID="cboCodeCategory" runat="server" OnInit="cboCodeCategory_Init"
                                                                                            Width="100%" TextField="Name" ValueField="Code">
                                                                                            <Columns>
                                                                                                <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                                                <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                            </Columns>
                                                                                            <ClientSideEvents SelectedIndexChanged="cboCodeDeviceCategory_SelectedIndexChanged" />
                                                                                        </dx:ASPxComboBox>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên nhóm" FieldName="Name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="250px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="100px">
                                                                                    <EditButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="ToolId" FieldName="ToolId" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="BuyingToolCategoryId" FieldName="BuyingToolCategoryId"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="ToolBuyingToolCategoryId" FieldName="ToolBuyingToolCategoryId"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="5" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
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
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới CCDC" ToolTip="Tạo mới 1 CCDC - Ctrl + N"
                                                                            Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn nhóm công cụ dụng cụ">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                                <CaptionSettings VerticalAlign="Middle" />
                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                <CaptionSettings VerticalAlign="Middle"></CaptionSettings>
                                                                <Border BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid"></Border>
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
                                                <dx:ContentControl ID="ContentControl21" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPxF22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdataSupplier" KeyFieldName="Code" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdataSupplier" OnRowInserting="grdataSupplier_RowInserting"
                                                                            OnRowDeleting="grdataSupplier_RowDeleting" Width="100%" OnRowValidating="grdataSupplier_RowValidating">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã nhhà cung cấp" FieldName="Code" Name="Code"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxComboBox ID="cboCodeSupplier" runat="server" OnInit="cboCodeSupplier_Init"
                                                                                            Width="100%" TextField="Name" ValueField="Code">
                                                                                            <Columns>
                                                                                                <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                                                <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                            </Columns>
                                                                                            <ClientSideEvents SelectedIndexChanged="cboCodeSupplier_SelectedIndexChanged" />
                                                                                        </dx:ASPxComboBox>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên nhà cung cấp" FieldName="Name" Name="Name"
                                                                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="250px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="5" Width="100px">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="MaterialSupplierId" Name="MaterialSupplierId"
                                                                                    ShowInCustomizationForm="True" Visible="False">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="MaterialId" Name="MaterialId" ShowInCustomizationForm="True"
                                                                                    Visible="False">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="SupplierId" Name="SupplierId" ShowInCustomizationForm="True"
                                                                                    Visible="False">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                            <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>
                                                                            <Styles>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="105px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới NCC" ToolTip="Tạo mới 1 nhà cung cấp - Ctrl + N"
                                                                            Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn nhóm nhà cung cấp">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                                <CaptionSettings VerticalAlign="Middle" />
                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                <CaptionSettings VerticalAlign="Middle"></CaptionSettings>
                                                                <Border BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid"></Border>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                        <SettingsItems VerticalAlign="Middle" />
                                                        <SettingsItems VerticalAlign="Middle"></SettingsItems>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Đơn vị tính">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="formMaterialUnit" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem ShowCaption="false">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxTreeList ID="treelstToolUnits" ClientInstanceName="treelstToolUnits" KeyFieldName="ToolToolUnitHierachyId"
                                                                            ParentFieldName="ParentToolToolUnitId" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%" OnNodeDeleting="treelstToolUnits_NodeDeleting" OnNodeInserting="treelstToolUnits_NodeInserting"
                                                                            OnNodeUpdating="treelstToolUnits_NodeUpdating" OnNodeValidating="treelstToolUnits_NodeValidating"
                                                                            OnCellEditorInitialize="treelstToolUnits_CellEditorInitialize" 
                                                                            OnHtmlDataCellPrepared="treelstToolUnits_HtmlDataCellPrepared">
                                                                            <Columns>
                                                                                <dx:TreeListTextColumn Caption="Mã đơn vị tính" FieldName="Code" Name="Code" VisibleIndex="0"
                                                                                    Width="150px">
                                                                                    <EditCellTemplate>
                                                                                        <dx:ASPxComboBox ID="cboCodeUnit" runat="server" OnInit="cboCodeUnit_Init" Width="100%"
                                                                                            TextField="Name" ValueField="Code">
                                                                                            <Columns>
                                                                                                <dx:ListBoxColumn Caption="MaterialUnitId" FieldName="MaterialUnitId" Width="150px"
                                                                                                    Visible="false" />
                                                                                                <dx:ListBoxColumn Caption="MaterialUnitPropertyId" FieldName="MaterialUnitPropertyId"
                                                                                                    Width="150px" Visible="false" />
                                                                                                <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                                                <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                            </Columns>
                                                                                            <ClientSideEvents SelectedIndexChanged="cboCodeUnit_SelectedIndexChanged" />
                                                                                        </dx:ASPxComboBox>
                                                                                    </EditCellTemplate>
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="Tên đơn vị tính" FieldName="Name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="150px">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListSpinEditColumn Caption="Bao gồm" FieldName="NumRequired" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="100px">
                                                                                    <PropertiesSpinEdit DisplayFormatString="g">
                                                                                    </PropertiesSpinEdit>
                                                                                </dx:TreeListSpinEditColumn>
                                                                                <dx:TreeListTextColumn Caption="Diễn giải" FieldName="Description" Width="300px" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Visible="true">
                                                                                    <CellStyle Wrap="True">
                                                                                    </CellStyle>
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                    Width="10%" Caption="Thao tác" ShowNewButtonInHeader="false">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <UpdateButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Apply" />
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                </dx:TreeListCommandColumn>
                                                                                <dx:TreeListTextColumn Caption="ToolUnitPropertyId" FieldName="ToolUnitPropertyId"
                                                                                    ShowInCustomizationForm="True" Visible="false">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="ToolId" FieldName="MaterialId" ShowInCustomizationForm="True"
                                                                                    Visible="false">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="ToolToolUnitId" FieldName="ToolToolUnitId" ShowInCustomizationForm="True"
                                                                                    Visible="false">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="ParentToolToolUnitId" FieldName="ParentToolToolUnitId"
                                                                                    ShowInCustomizationForm="True" Visible="false">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="ToolToolUnitHierachyId" FieldName="ToolToolUnitHierachyId"
                                                                                    ShowInCustomizationForm="True" Visible="false">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="ToolUnitId" FieldName="ToolUnitId" ShowInCustomizationForm="True"
                                                                                    Visible="false">
                                                                                </dx:TreeListTextColumn>
                                                                            </Columns>
                                                                            <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                            <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" ExpandCollapseAction="NodeDblClick" />

<Settings ShowFooter="True" HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto" ScrollableHeight="360"></Settings>

<SettingsBehavior AutoExpandAllNodes="True" AllowFocusedNode="True" ExpandCollapseAction="NodeDblClick"></SettingsBehavior>

                                                                            <Styles>
                                                                                <Header HorizontalAlign="Center" Font-Bold="true">
                                                                                </Header>
                                                                                <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                                                </CommandButton>
                                                                            </Styles>
                                                                            <Settings ShowFooter="true" />
                                                                        </dx:ASPxTreeList>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Name="tabDetail" Text="Thông tin chi tiết">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPdsdsxF22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxNavBar ID="navbarDetailInfo" runat="server" AutoCollapse="True" Width="100%"
                                                                            Height="100%">
                                                                            <Groups>
                                                                                <dx:NavBarGroup Text="Mô Tả" Name="Description">
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
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Cập nhật file tài liệu và mô tả cho CCDC ">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                                <CaptionSettings VerticalAlign="Middle" />
                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                <CaptionSettings VerticalAlign="Middle"></CaptionSettings>
                                                                <Border BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid"></Border>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                        <SettingsItems VerticalAlign="Middle" />
                                                        <SettingsItems VerticalAlign="Middle"></SettingsItems>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Công cụ dụng cụ tương đương" Visible="false">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASsdssdsF22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdSameBuyingDevice" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%" ClientInstanceName="grdSameBuyingDevice">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã công cụ dụng cụ" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px" FieldName="materialcode">
                                                                                    <PropertiesComboBox>
                                                                                        <Columns>
                                                                                            
<dx:ListBoxColumn Caption="Mã nhóm hàng hóa" Width="150px" />
                                                                                            
<dx:ListBoxColumn Caption="Tên nhóm hàng hóa" Width="300px" />
                                                                                        
</Columns>
                                                                                    
</PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên công cụ dụng cụ" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="250px" FieldName="materialname">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Nhà sản xuất" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="200px" FieldName="manufacturername">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Mô tả" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                                    FieldName="materialdescription">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="4" Width="100px">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
                                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Update" />
                                                                                            <SpriteProperties CssClass="Sprite_Update"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                            </Columns>
                                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <Settings VerticalScrollableHeight="370" VerticalScrollBarMode="Auto"></Settings>
                                                                            <Styles>
                                                                                <CommandColumn Spacing="10px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn CCDC tương đương">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                                <CaptionSettings VerticalAlign="Middle" />
                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                <CaptionSettings VerticalAlign="Middle"></CaptionSettings>
                                                                <Border BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid"></Border>
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
    </dx:ASPxCallbackPanel>
</div>
