<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditCustomer.ascx.cs"
    Inherits="WebModule.GUI.usercontrol.uEditCustomer" %>
<%@ Register src="../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx" tagname="NASCustomFieldDataGridView" tagprefix="uc1" %>
<script type="text/javascript">
    var CustomerEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                popCustomerEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                var args = 'edit';
                args += '|' + recordId;
                popCustomerEdit.PerformCallback(args);
            }
            else {
                this._recordId = null;
                var args = 'new';
                popCustomerEdit.PerformCallback(args);
            }
            popCustomerEdit.Show();
            $(CustomerEditForm).triggerHandler('shown');
        },
        Save: function () {
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagCustomer.GetMainElement(), null, true);
            if (validated) {
                var args = 'save';
                if (this._recordId) {
                    args += '|' + this._recordId;
                }
                if (popCustomerEdit.InCallback()) {
                    console.log('popCustomerEdit: server too busy');
                }
                else {
                    popCustomerEdit.PerformCallback(args);
                }

            }
            //            else {
            //                pagCustomer.SetActiveTabIndex(0);
            //            }
        },
        Hide: function () { popCustomerEdit.Hide(); },
        btnSave_Click: function (s, e) {
            CustomerEditForm.Save();
        },
        btnCancel_Click: function (s, e) {
            CustomerEditForm.Hide();
        },
        EndCallback: function (s, e) {
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }

            if (s.cpCallbackArgs) {
                popCustomerEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(CustomerEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }

        },
        //Bind Saved Event
        BindSavedEvent: function (callback) {
            $(CustomerEditForm).on('saved', callback);
        },
        Closing: function (s, e) {

        },

        Focus: function () {
            var jObject = $(".KeyShortcutPopCustomerEdit");
            jObject.focus();
        },

        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(CustomerEditForm).on('shown', callback);
        },
        ValidateForm: function (s, e) {
            switch (s.name) {
                case '<%= txtCode.ClientID %>':
                    //Validate max length
                    var CODE_MAX_LENGTH = 36;
                    if (!ValidateMaxLength(e.value, CODE_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Mã khách hàng không được vượt quá 36 kí tự";
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
                        e.errorText = "Tên khách hàng không được vượt quá 255 kí tự";
                    }
                    break;
                // Duc.Vo 10/09/2013 INS-START 
                case '<%= txtAddress.ClientID %>':
                    //Validate max length
                    var NAME_MAX_LENGTH = 255;
                    if (!ValidateMaxLength(e.value, NAME_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Địa chỉ không được vượt quá 255 kí tự";
                    }
                    break;
                // Duc.Vo 10/09/2013 INS-END  
                default:
                    break;
            }
        }
    };

    function popCustomerEdit_Init(s, e) {
        var jObject = $(".KeyShortcutPopCustomerEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            CustomerEditForm.Hide();
        });
        //Press Ctrl+Enter to save general information
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            CustomerEditForm.Save();
        });
    }

    function popCustomerEdit_Shown(s, e) {
        //Focus to popup edit form when shown
        //ManufacturerEditForm.Focus();
        txtCode.Focus();
    }

    function popCustomerEdit_CloseUp(s, e) {
        grdCustomer.Focus();
    }

    function lbCustomerType_SelectedIndexChanged(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pagCustomer.GetMainElement(), null, true);
        if (!validated) {
            var currItem = s.GetItem(e.index);
            var arr = new Array();
            arr[0] = currItem;
            if (e.isSelected) {
                s.UnselectItems(arr);
            } else {
                s.SelectItems(arr);
            }
        }
    }
</script>
<dx:ASPxPopupControl ID="popCustomerEdit" runat="server" HeaderText="Thông Tin Khách Hàng"
    Height="600px" Modal="True" Width="900px" ClientInstanceName="popCustomerEdit"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="true" CloseAction="CloseButton" AllowResize="true" ScrollBars="Auto"
    ShowMaximizeButton="True" 
    CssClass="KeyShortcutPopCustomerEdit"
    OnWindowCallback="popCustomerEdit_WindowCallback">
    <ClientSideEvents EndCallback="CustomerEditForm.EndCallback"
        Init="popCustomerEdit_Init" CloseUp="popCustomerEdit_CloseUp" Shown="popCustomerEdit_Shown"
        ></ClientSideEvents>
    <FooterTemplate>
        <div style="padding: 10px;">
            <div style="float: left">
                <div style="float: left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnHelp" AutoPostBack="false" runat="server" Text="Trợ Giúp" Wrap="False"
                        ToolTip="Trợ Giúp - Ctrl + H">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div style="float: right">
                <div style="float: left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnApply" runat="server" AutoPostBack="False" Text="Lưu Lại" Wrap="False"
                        ToolTip="Lưu và Đóng - Ctr + S">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Apply" />
                        </Image>
                        <ClientSideEvents Click="CustomerEditForm.btnSave_Click" />
                    </dx:ASPxButton>
                </div>
                <div style="float: left; margin-left: 4px;">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" ClientInstanceName="btnCancel"
                        Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                        <ClientSideEvents Click="CustomerEditForm.btnCancel_Click" />
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pagCustomer" ClientInstanceName="pagCustomer" runat="server"
                ActiveTabIndex="1" Height="100%" Width="100%">
                <TabPages>
                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:XpoDataSource ID="dsCustomer" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
                                    Criteria="[RowStatus] &gt; 0 And [OrganizationId] = ?">
                                    <CriteriaParameters>
                                        <asp:Parameter Name="CustomerOrgId" />
                                    </CriteriaParameters>
                                </dx:XpoDataSource>
                                <dx:ASPxFormLayout ID="frmCustomer" runat="server" Width="100%" DataSourceID="dsCustomer">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã khách hàng" FieldName="Code" HelpText="Tối đa 36 ký tự gồm chữ, số, gạch nối(- hoặc _)"
                                            RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxCallbackPanel ID="cpntxtCode" runat="server" ClientInstanceName="cpntxtCode"
                                                        ShowLoadingPanel="False" Width="200px">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtCode" ClientInstanceName="txtCode" runat="server" OnValidation="txtCode_Validation" 
                                                                    Width="170px">
                                                                    <ClientSideEvents Validation="CustomerEditForm.ValidateForm"></ClientSideEvents>
                                                                    <ValidationSettings ErrorText="">
                                                                        <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
                                                                        <RegularExpression ErrorText="Mã khách hàng không hợp lệ" ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên khách hàng" FieldName="Name" HelpText="Tối đa 255 ký tự"
                                            RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtName" runat="server" Width="400px">
                                                        <ClientSideEvents Validation="CustomerEditForm.ValidateForm"></ClientSideEvents>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mã số thuế" FieldName="TaxNumber">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtTaxNumber" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Địa chỉ" FieldName="Address">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtAddress" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Trạng thái" FieldName="RowStatus" Visible="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cbRowStatus" runat="server" SelectedIndex="0" 
                                                        Width="200px">
                                                        <Items>
                                                            <dx:ListEditItem Selected="True" Text="Hoạt động" Value="1" />
                                                            <dx:ListEditItem Text="Ngưng hoạt động" Value="2" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Loại">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxListBox ID="lbCustomerType" runat="server" SelectionMode="CheckColumn" Width="100%"
                                                        ClientInstanceName="lbCustomerType" ValueField="TradingCategoryId" Rows="6" Height="160px"
                                                        ShowLoadingPanel="false" 
                                                        DataSourceID="ObjectCustomerTypeLbXDS" ValidationSettings-CausesValidation="true">
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="TradingCategoryId" Caption="TradingCategoryId" Width="100%" Visible="false" />
                                                            <dx:ListBoxColumn FieldName="Name" Caption="Tên loại" Width="100px" Visible="false"/>
                                                            <dx:ListBoxColumn FieldName="Description" Caption="Tên loại"  Width="100%" />
                                                        </Columns>
                                                        <ClientSideEvents SelectedIndexChanged="lbCustomerType_SelectedIndexChanged" />
                                                        <ValidationSettings CausesValidation="True"></ValidationSettings>
                                                    </dx:ASPxListBox>
                                                    <dx:XpoDataSource ID="ObjectCustomerTypeLbXDS" runat="server" 
                                                        TypeName="NAS.DAL.Nomenclature.Organization.TradingCategory" 
                                                        Criteria="[RowStatus] &gt; 0">
                                                    </dx:XpoDataSource>
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
                                <dx:ASPxCallbackPanel ID="xCallbackPanel_customer" runat="server" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                Text="Cấu hình thuộc tính cho khách hàng">
                                            </dx:ASPxLabel>
                                            <br />
                                            <uc1:NASCustomFieldDataGridView ID="grid_customer" runat="server" />
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                                <br />
                                <dx:ASPxCallbackPanel ID="xCallbackPanel_supplier" runat="server" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                Text="Cấu hình thuộc tính cho nhà cung cấp">
                                            </dx:ASPxLabel>
                                            <br />
                                            <uc1:NASCustomFieldDataGridView ID="grid_supplier" runat="server" />
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
