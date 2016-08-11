<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uWarehouse.ascx.cs"
    Inherits="WebModule.Warehouse.UserControl.uWarehouse" %>
<script type="text/javascript">
    function formManufacturerEdit_AfterResizing(s, e) {
        // grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        // grmanufacturer0.SetHeight($("#testheight").height() - 120);
        ASPxClientControl.AdjustControls();
    }

    var ManuWarehouseEditForm = {
        events: {
            eShown: 'shown',
            eSaved: 'saved',
            eClosing: 'closing'
        },

        CurrentAction: '',
        Show: function (headerText, recordId) {
            if (headerText) {
                formWarehouseEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                cpLineWarehouse.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;

                formWarehouseEdit.PerformCallback('new');
                formWarehouseEdit.Show();
                formWarehouseEdit.SetHeaderText("Thêm mới kho");
                CurrentAction = "Show";
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS START
            $(ManuWarehouseEditForm).triggerHandler('shown');
            /////2013-09-22 ERP-580 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-20 ERP-580 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagWarehouseEdit.GetMainElement(), null, true);
            if (validated) {
                if (!formWarehouseEdit.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    formWarehouseEdit.PerformCallback(args);
                }
            }
            else {
                pagWarehouseEdit.SetActiveTabIndex(0);
            }
            //formWarehouseEdit.Hide();
            /////2013-09-20 ERP-580 Khoa.Truong MOD END
        },
        Hide: function () {
            formWarehouseEdit.Hide();
            $(ManuWarehouseEditForm).triggerHandler(ManuWarehouseEditForm.events.eClosing);
        },

        btnSave_Click: function (s, e) {
            ManuWarehouseEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            var args = 'cancel';
            if (this._recordId) {
                args += '|' + this._recordId;
            }
            formWarehouseEdit.PerformCallback(args);
            ManuWarehouseEditForm.Hide();
        },

        EndCallback: function (s, e) {
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                ManuWarehouseEditForm.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManuWarehouseEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS END
            treelstToolUnits.PerformCallback("Refresh");

            if (CurrentAction == "Show") {
                txtCode.Focus();
                CurrentAction = "";
            }
        },

        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(ManuWarehouseEditForm).on('saved', callback);
        },


        //Bind Saved Event method
        BindClosingEvent: function (callback) {
            $(ManuWarehouseEditForm).on('closing', callback);
        },
        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManuWarehouseEditForm).on('shown', callback);
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
                        e.errorText = "Mã kho không được vượt quá 128 kí tự";
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
                        e.errorText = "Tên kho không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END

    };

    /////2013-09-21 ERP-580 Duc.Vo INS START
//    function cboCodeWarehouseCategory_SelectedIndexChanged(s, e) {
//        var itemWarehouseGrp = s.GetSelectedItem("Name");
//        if (itemWarehouseGrp != null) {
//            var txtWarehouseGrp = itemWarehouseGrp.GetColumnText("Name");
//            var editor = grdataBuyingToolCategory.GetEditor('Name');
//            editor.SetValue(txtWarehouseGrp);
//        }
//    }

//    function cboCodeUnit_SelectedIndexChanged(s, e) {
//        var itemUnit = s.GetSelectedItem("Name");
//        if (itemUnit != null) {
//            var txtUnit = itemUnit.GetColumnText("Name");
//            var editor = treelstToolUnits.GetEditor('Name');
//            editor.SetValue(txtUnit);
//        }
//    }
    /////2013-09-21 ERP-580 Duc.Vo INS END
    ////2013-30-10 ERP 850 duy.do INS Start
    function ASPxTreeList1_SelectedIndexChanged(s, e) {
        var itemUnit = s.GetSelectedItem();
        if (itemUnit != null) {
            var unitName = itemUnit.GetColumnText("InventoryId");
            var editor = treelstProductUnits.GetEditor('InventoryId');
            editor.SetValue(unitName);

        }
    }
    function treelstToolUnits_Init(s, e) {
        UtilsForTreeList.AttachStandardShortcutToTreeList(s);
    }
    function formManufacturerEdit_Init(s, e) {
        var jObject = $(".keyboarShortcut");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        htmlObject.focus();
        //ESC popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            formWarehouseEdit.Hide();
        });
        //button Tro Giup
        Utils.AttachShortcutTo(htmlObject, "Ctrl+H", function () {
            //popupItemUnitHelper.Show();
        });

        Utils.AttachShortcutTo(htmlObject, "Shift+Right", function () {
            var curridx = pagWarehouseEdit.GetActiveTabIndex();
            if (curridx < 3) {
                pagWarehouseEdit.SetActiveTabIndex(curridx + 1);
                treelstToolUnits.FocusEditor("Name_InventoryUnitId");
                treelstToolUnits.GetMainElement().focus();
            }
        });

        Utils.AttachShortcutTo(htmlObject, "Shift+Left", function () {
            var curridx = pagWarehouseEdit.GetActiveTabIndex();
            if (curridx >= 1) {
                pagWarehouseEdit.SetActiveTabIndex(curridx - 1);
                jObject.focus();
                txtCode.Focus();
            }
        });

        //press Ctrl + Enter
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagWarehouseEdit.GetMainElement(), null, true);
            if (validated) {
                ManuWarehouseEditForm.Save();
            } else {
                pagWarehouseEdit.SetActiveTabIndex(0);
            }
        });
    }

    function cpLineWarehouse_EndCallback(s, e) {
        switch (clientAction) {
            case 'Add':
                break;
            case 'Edit':
                txtCode.Focus();
                break;
        }
    }

    ////2013-30-10 ERP 850 duy.do INS END
</script>
<div id="lineContainerWarehouse">
    <dx:ASPxCallbackPanel ID="cpLineWarehouse" runat="server" Width="100%" ClientInstanceName="cpLineWarehouse"
        OnCallback="cpLineWarehouse_Callback">
        <ClientSideEvents EndCallback="cpLineWarehouse_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formWarehouseEdit" runat="server" HeaderText="Thông tin kho - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formWarehouseEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True"
                    EnableViewState="False" OnWindowCallback="popWarehouseEdit_WindowCallback" CloseAction="CloseButton"
                    ShowShadow="False" CssClass="keyboarShortcut">
                    <ClientSideEvents Init="formManufacturerEdit_Init" AfterResizing="formManufacturerEdit_AfterResizing"
                        EndCallback="ManuWarehouseEditForm.EndCallback" CloseUp="ManuWarehouseEditForm.Hide">
                    </ClientSideEvents>
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterTemplate>
                        <div style="padding: 10px;">
                            <div style="float: left">
                                <div style="float: left">
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                        Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Help" />
                                        </Image>
                                        <ClientSideEvents Click="function(s, e){
                                        popupItemUnitHelper.Show();
                                        }" />
                                    </dx:ASPxButton>
                                </div>
                                <div style="float: left; margin-left: 4px">
                                    <!-- Places button here -->
                                </div>
                            </div>
                            <div style="float: right">
                                <div style="float: left">
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="buttonAcceptWarehouse" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                        ClientInstanceName="buttonSaveWarehouse" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                        <ClientSideEvents Click="ManuWarehouseEditForm.btnSave_Click" />
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                <div style="float: left; margin-left: 4px">
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="buttonCancelWarehouse" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                        ClientInstanceName="buttonCancelWarehouse" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                        <ClientSideEvents Click="ManuWarehouseEditForm.btnCancel_Click" />
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                            </div>
                            <div style="clear: both">
                            </div>
                        </div>
                    </FooterTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%;" id="testheight">
                                <dx:ASPxPageControl ID="pagWarehouseEdit" ClientInstanceName="pagWarehouseEdit" runat="server"
                                    ActiveTabIndex="1" Width="100%" Height="100%" LoadingPanelText="TabLoad" ShowLoadingPanel="false">
                                    <ClientSideEvents ActiveTabChanged="function (s,e){ treelstToolUnits.FocusEditor('Name_InventoryUnitId');}" />
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông tin chung">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                    <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx" style="width: 100%;">
                                                        <tr>
                                                            <td valign="top">
                                                                <dx:ASPxFormLayout ID="frmlInfoGeneral" runat="server" EnableTheming="True" Width="100%"
                                                                    AlignItemCaptionsInAllGroups="True" Height="100%" ClientInstanceName="ASPxFormLayout1"
                                                                    DataSourceID="dsInventory">
                                                                    <Items>
                                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                                            <Items>
                                                                                <dx:LayoutItem Caption="Mã kho" FieldName="Code" CaptionCellStyle-CssClass="CaptionStyle"
                                                                                    HelpText="Tối đa 36 ký tự gồm chữ, số, gạch nối(- hoặc _)" RequiredMarkDisplayMode="Required">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                                                runat="server" Width="200px">
                                                                                                <PanelCollection>
                                                                                                    <dx:PanelContent>
                                                                                                        <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                                                            <ClientSideEvents Validation="ManuWarehouseEditForm.ValidateForm"></ClientSideEvents>
                                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập mã kho"></RequiredField>
                                                                                                                <RegularExpression ErrorText="Mã phiếu thu không hợp lệ" ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$" />
                                                                                                                <RegularExpression ErrorText="M&#227; phiếu thu kh&#244;ng hợp lệ" ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$">
                                                                                                                </RegularExpression>
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
                                                                                <dx:LayoutItem Caption="Tên kho" FieldName="Name" HelpText="Tối đa 255 ký tự">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" MaxLength="255"
                                                                                                Width="400px">
                                                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                                    <RequiredField ErrorText="Chưa nhập tên kho" IsRequired="True" />
                                                                                                    <RequiredField IsRequired="True" ErrorText="Chưa nhập t&#234;n kho"></RequiredField>
                                                                                                </ValidationSettings>
                                                                                                <ClientSideEvents Validation="ManuWarehouseEditForm.ValidateForm"></ClientSideEvents>
                                                                                            </dx:ASPxTextBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Địa chỉ" FieldName="Address">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxMemo ID="memoAddress" runat="server" Rows="3" Width="50%">
                                                                                            </dx:ASPxMemo>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Trạng thái" CaptionCellStyle-CssClass="captionStyle" Visible="False"
                                                                                    FieldName="RowStatus" HelpText="Tự động tạo mới">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxComboBox ID="cbRowStatus" runat="server" SelectedIndex="0" Width="200px">
                                                                                                <Items>
                                                                                                    <dx:ListEditItem Selected="True" Text="Hoạt động" Value="1" />
                                                                                                    <dx:ListEditItem Text="Ngưng hoạt động" Value="2" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                    <CaptionCellStyle CssClass="captionStyle">
                                                                                    </CaptionCellStyle>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Hình ảnh" Visible="false">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                                                            SupportsDisabledAttribute="True">
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
                                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho kho" Visible="False">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
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
                                                    <dx:XpoDataSource ID="dsInventory" runat="server" Criteria="[RowStatus] &gt; 0 And [InventoryId] = ?"
                                                        TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
                                                        <CriteriaParameters>
                                                            <asp:Parameter Name="InventoryId" />
                                                        </CriteriaParameters>
                                                    </dx:XpoDataSource>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <%--<dx:TabPage Text="Trực thuộc thể loại kho" Visible="false">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdataWarehouseCategory" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdataWarehouseCategory" KeyFieldName="Code" Width="100%"
                                                                            OnRowDeleting="grdataWarehouseCategory_RowDeleting" OnRowValidating="grdataWarehouseCategory_RowValidating"
                                                                            OnRowInserting="grdataWarehouseCategory_RowInserting">
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                            <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã thể loại kho" FieldName="Code" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px">
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxComboBox ID="cboCodeCategory" runat="server" OnInit="cboCodeCategory_Init"
                                                                                            Width="100%" TextField="Name" ValueField="Code">
                                                                                            <Columns>
                                                                                                <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                                                <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                            </Columns>
                                                                                            <ClientSideEvents SelectedIndexChanged="cboCodeWarehouseCategory_SelectedIndexChanged" />
                                                                                        </dx:ASPxComboBox>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên thể loại kho" FieldName="Name" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="250px">
                                                                                    <EditFormSettings Visible="False" />
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="100px">
                                                                                    <EditButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="WarehouseId" FieldName="WarehouseId" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="WarehouseCategoryId" FieldName="WarehouseCategoryId"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Visible="false">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="WarehouseWarehouseCategoryId" FieldName="WarehouseWarehouseCategoryId"
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
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới thể loại kho"
                                                                            ToolTip="Tạo mới 1 thể loại kho - Ctrl + N" Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn thể loại kho">
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
                                        </dx:TabPage>--%>
                                        <dx:TabPage Text="Đơn vị lưu trữ">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="formMaterialUnit" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem ShowCaption="false">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer>
                                                                        <dx:ASPxTreeList ID="treelstToolUnits" ClientInstanceName="treelstToolUnits" KeyFieldName="InventoryId"
                                                                            ParentFieldName="ParentInventoryId!Key" runat="server" AutoGenerateColumns="False"
                                                                            DataSourceID="dsInventoryForUnit" Width="100%" OnHtmlDataCellPrepared="treelstToolUnits_HtmlDataCellPrepared"
                                                                            OnNodeInserting="treelstToolUnits_NodeInserting" OnCellEditorInitialize="treelstToolUnits_CellEditorInitialize"
                                                                            KeyboardSupport="true" OnNodeValidating="treelstToolUnits_NodeValidating">
                                                                            <ClientSideEvents Init="treelstToolUnits_Init" />
                                                                            <Columns>
                                                                                <dx:TreeListComboBoxColumn Caption="Tên đơn vị lưu trữ" Name="Name_InventoryUnitId"
                                                                                    FieldName="InventoryUnitId!Key" VisibleIndex="1" Width="20%">
                                                                                    <PropertiesComboBox DataSourceID="dsInventoryUnit" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                        ValueField="InventoryUnitId" ValueType="System.Guid" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="Tên đơn vị lưu trữ" FieldName="Name" />
                                                                                        </Columns>
                                                                                        <%--<ValidationSettings>
                                                                                            <RequiredField ErrorText="Chưa chọn đơn vị lưu trữ." IsRequired="True" />
                                                                                        </ValidationSettings>--%>
                                                                                    </PropertiesComboBox>
                                                                                </dx:TreeListComboBoxColumn>
                                                                                <dx:TreeListSpinEditColumn Caption="Bao gồm" Name="NumRequired" FieldName="NumRequired" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="2" Width="25%">
                                                                                    <PropertiesSpinEdit DisplayFormatString="g">
                                                                                    </PropertiesSpinEdit>
                                                                                </dx:TreeListSpinEditColumn>
                                                                                <dx:TreeListTextColumn Caption="Diễn giải" Name="Description" Width="45%" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="3" Visible="true">
                                                                                    <CellStyle Wrap="True">
                                                                                    </CellStyle>
                                                                                    <EditCellTemplate>
                                                                                    </EditCellTemplate>
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                    Width="10%" Caption="Thao tác" ShowNewButtonInHeader="True">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <UpdateButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                </dx:TreeListCommandColumn>
                                                                            </Columns>
                                                                            <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                            <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" ExpandCollapseAction="NodeDblClick" />
                                                                            <SettingsEditing AllowNodeDragDrop="True" AllowRecursiveDelete="True" />
                                                                            <Styles>
                                                                                <Header HorizontalAlign="Center" Font-Bold="true">
                                                                                </Header>
                                                                                <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                                                </CommandButton>
                                                                            </Styles>
                                                                            <Settings ShowFooter="true" />
                                                                            <ClientSideEvents Init="treelstToolUnits_Init"></ClientSideEvents>
                                                                        </dx:ASPxTreeList>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                    <dx:XpoDataSource ID="dsInventoryUnit" runat="server" Criteria="RowStatus &gt; 0"
                                                        TypeName="NAS.DAL.Nomenclature.Inventory.InventoryUnit">
                                                    </dx:XpoDataSource>
                                                    <dx:XpoDataSource ID="dsInventoryForUnit" runat="server" Criteria="[RowStatus] =  0 And [ParentInventoryId] Is Not Null"
                                                        TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
                                                        <CriteriaParameters>
                                                            <asp:Parameter Name="InventoryId" />
                                                        </CriteriaParameters>
                                                    </dx:XpoDataSource>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <%--<dx:TabPage Name="tabDetail" Text="Thông tin chi tiết" Visible="false">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPdsdsxF22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server"
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
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
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
                                        <dx:TabPage Text="Kho tương đương" Visible="false">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASsdssdsF22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdSameBuyingWarehouse" runat="server" AutoGenerateColumns="False"
                                                                            Width="100%" ClientInstanceName="grdSameBuyingWarehouse">
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
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server"
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
                                        </dx:TabPage>--%>
                                    </TabPages>
                                    <ClientSideEvents ActiveTabChanged="function (s,e){ treelstToolUnits.FocusEditor(&#39;Name_InventoryUnitId&#39;);}">
                                    </ClientSideEvents>
                                </dx:ASPxPageControl>
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
