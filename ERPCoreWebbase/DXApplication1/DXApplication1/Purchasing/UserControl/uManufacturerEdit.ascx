<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uManufacturerEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uManufacturerEdit" %>
<%@ Register src="../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx" tagname="NASCustomFieldDataGridView" tagprefix="uc1" %>
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

    function formManufacturerEdit_AfterResizing(s, e) {
        //        grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        //        grdManufaturerSupplierForm.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    }

    var ManufacturerEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                popManufacturerEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                var args = 'edit';
                args += '|' + recordId;
                console.log('edit');
                popManufacturerEdit.PerformCallback(args);
            }
            else {
                this._recordId = null;
                var args = 'new';
                console.log('new');
                popManufacturerEdit.PerformCallback(args);
            }
            popManufacturerEdit.Show();
            $(ManufacturerEditForm).triggerHandler('shown');
        },
        Save: function () {
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagMunufacturer.GetMainElement(), null, true);
            if (validated) {
                var args = 'save';
                if (this._recordId) {
                    args += '|' + this._recordId;
                }
                if (popManufacturerEdit.InCallback()) {
                    console.log('popManufacturerEdit: server too busy');
                }
                else {
                    popManufacturerEdit.PerformCallback(args);
                }

            }
            else {
                pagMunufacturer.SetActiveTabIndex(0);
            }
        },
        Hide: function () { popManufacturerEdit.Hide(); },
        btnSave_Click: function (s, e) {
            ManufacturerEditForm.Save();
        },
        btnCancel_Click: function (s, e) {
            ManufacturerEditForm.Hide();
        },
        EndCallback: function (s, e) {
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }

            if (s.cpCallbackArgs) {
                popManufacturerEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManufacturerEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }

        },
        //Bind Saved Event
        BindSavedEvent: function (callback) {
            $(ManufacturerEditForm).on('saved', callback);
        },
        Closing: function (s, e) {

        },
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManufacturerEditForm).on('shown', callback);
        },

        Focus: function () {
            var jObject = $(".KeyShortcutManufacturerEdit");
            jObject.focus();
        },

        ValidateForm: function (s, e) {
            switch (s.name) {
                case '<%= txtCode.ClientID %>':
                    //Validate max length
                    var CODE_MAX_LENGTH = 36;
                    if (!ValidateMaxLength(e.value, CODE_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Mã nhà sản xuất không được vượt quá 36 kí tự";
                    }
                    if (e.isValid == true) {
                        //Check Code is exist in database
                        if (cpntxtCode.InCallback()) {
                        }
                        else {
                            cpntxtCode.PerformCallback();
                        }
                    }
                    break;
                case '<%= txtName.ClientID %>':
                    //Validate max length
                    var NAME_MAX_LENGTH = 255;
                    if (!ValidateMaxLength(e.value, NAME_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Tên nhà sản xuất không được vượt quá 255 kí tự";
                    }
                    break;
                default:
                    break;
            }
        }
    };



    function popManufacturerEdit_CloseUp(s, e) {
        grdDataManufacturer.Focus();
    }

    function popManufacturerEdit_Init(s, e) {
        var jObject = $(".KeyShortcutManufacturerEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            ManufacturerEditForm.Hide();
        });
        //Press Ctrl+Enter to save general information
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            ManufacturerEditForm.Save();
        });
    }

    function popManufacturerEdit_Shown(s, e) {
        //Focus to popup edit form when shown
        //ManufacturerEditForm.Focus();
        txtCode.Focus();
    }

</script>
<div id="lineContainerManufacturer">
    <dx:ASPxCallbackPanel ID="cpnManufacturerEdit" runat="server" Width="100%" ClientInstanceName="cpnManufacturerEdit">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="popManufacturerEdit" runat="server" HeaderText="Thông Tin Nhà Sản Xuất - "
                    CssClass="KeyShortcutManufacturerEdit"
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="popManufacturerEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" CloseAction="CloseButton" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True" OnWindowCallback="popManufacturerEdit_WindowCallback">
                    <ClientSideEvents AfterResizing="formManufacturerEdit_AfterResizing"
                        EndCallback="ManufacturerEditForm.EndCallback" 
                        Init="popManufacturerEdit_Init"
                        Shown="popManufacturerEdit_Shown"
                        CloseUp="popManufacturerEdit_CloseUp">
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
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                    <ClientSideEvents Click="ManufacturerEditForm.btnCancel_Click" />
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                    <ClientSideEvents Click="ManufacturerEditForm.btnSave_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%;" id="testheight">
                                <dx:ASPxPageControl ID="pagMunufacturer" ClientInstanceName="pagMunufacturer" runat="server"
                                    ActiveTabIndex="1" Height="100%" Width="100%">
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông tin chung">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:XpoDataSource ID="dsManufacturer" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.ManufacturerOrg"
                                                        Criteria="[RowStatus] &gt; 0 And [OrganizationId] = ?">
                                                        <CriteriaParameters>
                                                            <asp:Parameter Name="ManufacturerOrgId" />
                                                        </CriteriaParameters>
                                                    </dx:XpoDataSource>
                                                    <dx:ASPxFormLayout ID="frmManufacturerEdit" runat="server" Width="100%" DataSourceID="dsManufacturer">
                                                        <Items>
                                                            <dx:LayoutItem Caption="Mã nhà sản xuất" FieldName="Code" HelpText="Tối đa 36 ký tự gồm chữ, số, gạch nối(- hoặc _)"
                                                                RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxCallbackPanel ID="cpntxtCode" runat="server" ClientInstanceName="cpntxtCode"
                                                                            ShowLoadingPanel="False" Width="200px">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="txtCode" ClientInstanceName="txtCode" runat="server" OnValidation="txtCode_Validation" Width="200px">
                                                                                        <ClientSideEvents Validation="ManufacturerEditForm.ValidateForm"></ClientSideEvents>
                                                                                        <ValidationSettings ErrorText="">
                                                                                            <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
                                                                                            <RegularExpression ErrorText="Mã nhà sản xuất không hợp lệ" ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>"></RequiredField>

<RegularExpression ErrorText="M&#227; nh&#224; sản xuất kh&#244;ng hợp lệ" ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$"></RegularExpression>
                                                                                        </ValidationSettings>
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Tên nhà sản xuất" FieldName="Name" HelpText="Tối đa 255 ký tự"
                                                                RequiredMarkDisplayMode="Required">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTextBox ID="txtName" runat="server" Width="400px">
                                                                            <ClientSideEvents Validation="ManufacturerEditForm.ValidateForm"></ClientSideEvents>
                                                                            <ValidationSettings>
                                                                                <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>"></RequiredField>
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Caption="Trạng thái" FieldName="RowStatus" Visible="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxComboBox ID="cbRowStatus" runat="server" SelectedIndex="0" Width="200px">
                                                                            <Items>
                                                                                <dx:ListEditItem Selected="True" Text="Hoạt động" Value="1" />
                                                                                <dx:ListEditItem Text="Ngưng hoạt động" Value="2" />
                                                                            </Items>
                                                                        </dx:ASPxComboBox>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Cấu hình động">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <br />
                                                    <br />
                                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" 
                                                        ClientInstanceName="panel_manufacturer" Height="223px" Width="100%">
                                                        <PanelCollection>
                                                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                <uc1:NASCustomFieldDataGridView ID="grid_of_Manufacturer" runat="server" />
                                                                <br />
                                                                <br />
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
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
