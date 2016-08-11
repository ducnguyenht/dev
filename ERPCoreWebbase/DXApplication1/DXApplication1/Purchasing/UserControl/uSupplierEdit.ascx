<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSupplierEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uSupplierEdit" %>
<%@ Register src="../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx" tagname="NASCustomFieldDataGridView" tagprefix="uc1" %>
<style type="text/css">
    .style1
    {
        font-size: small;
    }
</style>
<script type="text/javascript">
    var SupplierEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                popSupplierEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                var args = 'edit';
                args += '|' + recordId;
                popSupplierEdit.PerformCallback(args);
            }
            else {
                this._recordId = null;
                var args = 'new';
                popSupplierEdit.PerformCallback(args);
            }
            popSupplierEdit.Show();
            $(SupplierEditForm).triggerHandler('shown');
        },
        Save: function () {
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagSupplier.GetMainElement(), null, true);
            if (validated) {
                var args = 'save';
                if (this._recordId) {
                    args += '|' + this._recordId;
                }
                if (popSupplierEdit.InCallback()) {
                    console.log('popSupplierEdit: server too busy');
                }
                else {
                    popSupplierEdit.PerformCallback(args);
                }

            }
            //            else {
            //                pagSupplier.SetActiveTabIndex(0);
            //            }
        },

        Focus: function () {
            var jObject = $(".KeyShortcutpopSupplierEdit");
            jObject.focus();
        },

        Hide: function () { popSupplierEdit.Hide(); },
        btnSave_Click: function (s, e) {
            SupplierEditForm.Save();
        },
        btnCancel_Click: function (s, e) {
            SupplierEditForm.Hide();
        },
        EndCallback: function (s, e) {
            //If all data of form is invalid
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }

            if (s.cpCallbackArgs) {
                popSupplierEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(SupplierEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
        },
        //Bind Saved Event
        BindSavedEvent: function (callback) {
            $(SupplierEditForm).on('saved', callback);
        },
        Closing: function (s, e) {

        },
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(SupplierEditForm).on('shown', callback);
        },
        ValidateForm: function (s, e) {
            switch (s.name) {
                case '<%= txtCode.ClientID %>':
                    //Validate max length
                    var CODE_MAX_LENGTH = 36;
                    if (!ValidateMaxLength(e.value, CODE_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Mã nhà cung cấp không được vượt quá 36 kí tự";
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
                        e.errorText = "Tên nhà cung cấp không được vượt quá 255 kí tự";
                    }
                    break;
                // Duc.Vo 10/09/2013 INS-START
                case '<%= txtBankName.ClientID %>':
                    //Validate max length
                    var NAME_MAX_LENGTH = 255;
                    if (!ValidateMaxLength(e.value, NAME_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Tên ngân hàng không được vượt quá 255 kí tự";
                    }
                    break;
                case '<%= txtAccountNumber.ClientID %>':
                    //Validate max length
                    var NAME_MAX_LENGTH = 50;
                    if (!ValidateMaxLength(e.value, NAME_MAX_LENGTH)) {
                        e.isValid = false;
                        e.errorText = "Tài khoản ngân hàng không được vượt quá 50 kí tự";
                    }
                    break;
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

    function popSupplierEdit_Init(s, e) {
        var jObject = $(".KeyShortcutpopSupplierEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            SupplierEditForm.Hide();
        });
        //Press Ctrl+Enter to save general information
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            SupplierEditForm.Save();
        });
    }

    function popSupplierEdit_CloseUp(s, e) {
        grdSupplier.Focus();
    }

//    function popSupplierEdit_Shown(s, e) {
//        //Focus to popup edit form when shown
//        //ManufacturerEditForm.Focus();
//        txtCode.Focus();
//    }

    function lbSupplierType_SelectedIndexChanged(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pagSupplier.GetMainElement(), null, true);
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
<dx:ASPxPopupControl ID="popSupplierEdit" CssClass="KeyShortcutpopSupplierEdit" runat="server" 
    HeaderText="Thông Tin Nhà Cung Cấp"
    Height="600px" Modal="True" Width="900px" ClientInstanceName="popSupplierEdit"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="true" CloseAction="CloseButton" AllowResize="true" ScrollBars="Auto"
    ShowMaximizeButton="True" 
    OnWindowCallback="popSupplierEdit_WindowCallback">
    <ClientSideEvents EndCallback="SupplierEditForm.EndCallback"
        Closing="popSupplierEdit_CloseUp"
        Shown="SupplierEditForm.Focus"
        Init="popSupplierEdit_Init"></ClientSideEvents>
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
                        <ClientSideEvents Click="SupplierEditForm.btnSave_Click" />
                    </dx:ASPxButton>
                </div>
                <div style="float: left; margin-left: 4px;">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" ClientInstanceName="btnCancel"
                        Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                        <ClientSideEvents Click="SupplierEditForm.btnCancel_Click" />
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pagSupplier" ClientInstanceName="pagSupplier" runat="server"
                ActiveTabIndex="0" Height="100%" Width="100%">
                <TabPages>
                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:XpoDataSource ID="dsSupplier" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
                                    Criteria="[RowStatus] &gt; 0 And [OrganizationId] = ?" DefaultSorting="">
                                    <CriteriaParameters>
                                        <asp:Parameter Name="SupplierOrgId" />
                                    </CriteriaParameters>
                                </dx:XpoDataSource>
                                <dx:ASPxFormLayout ID="frmSupplier" runat="server" Width="100%" DataSourceID="dsSupplier">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã nhà cung cấp" FieldName="Code" HelpText="Tối đa 36 ký tự gồm chữ, số, gạch nối(- hoặc _)"
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
                                                                    <ClientSideEvents Validation="SupplierEditForm.ValidateForm"></ClientSideEvents>
                                                                    <ValidationSettings ErrorText="">
                                                                        <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
                                                                        <RegularExpression ErrorText="Mã nhà sản xuất không hợp lệ" ValidationExpression="^[A-Za-z0-9]{1}[A-Za-z0-9_-]{0,35}$" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên nhà cung cấp" FieldName="Name" HelpText="Tối đa 255 ký tự"
                                            RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtName" runat="server" Width="500px">
                                                        <ClientSideEvents Validation="SupplierEditForm.ValidateForm"></ClientSideEvents>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="True" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mã số thuế" FieldName="TaxNumber">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
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
                                                    <dx:ASPxTextBox ID="txtAddress" runat="server" Width="500px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Số tài khoản" FieldName="AccountNumber">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtAccountNumber" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tại ngân hàng" FieldName="BankName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtBankName" runat="server" Width="500px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Trạng thái" FieldName="RowStatus" Visible="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
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
                                                    <dx:ASPxListBox ID="lbSupplierType" runat="server" SelectionMode="CheckColumn" Width="100%"
                                                        ClientInstanceName="lbSupplierType" ValueField="TradingCategoryId" Rows="6" Height="160px"
                                                        ShowLoadingPanel="false" 
                                                        DataSourceID="ObjectSupplierTypeLbXDS" ValidationSettings-CausesValidation="true">
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="TradingCategoryId" Caption="TradingCategoryId" Width="100%" Visible="false" />
                                                            <dx:ListBoxColumn FieldName="Name" Caption="Tên loại" Width="100px" Visible="false"/>
                                                            <dx:ListBoxColumn FieldName="Description" Caption="Tên loại"  Width="100%" />
                                                        </Columns>
                                                        <ClientSideEvents SelectedIndexChanged="lbSupplierType_SelectedIndexChanged" />
                                                        <ValidationSettings CausesValidation="True"></ValidationSettings>
                                                    </dx:ASPxListBox>
                                                    <dx:XpoDataSource ID="ObjectSupplierTypeLbXDS" runat="server" 
                                                        TypeName="NAS.DAL.Nomenclature.Organization.TradingCategory" 
                                                        Criteria="[RowStatus] &gt; 0" DefaultSorting="">
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
                                            <span class="style1" 
                                                style="color: rgb(51, 51, 51); font-family: 'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); display: inline !important; float: none;">
                                            <dx:ASPxLabel ID="xlabel_customer" runat="server" 
                                                Text="Cấu hình thuộc tính cho khách hàng">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <uc1:NASCustomFieldDataGridView ID="grid_Customer" runat="server" />
                                            </span>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                                <br class="style1" />
                                <span class="style1" 
                                    style="color: rgb(51, 51, 51); font-family: 'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); display: inline !important; float: none;">
                                &nbsp;<br />
                                <dx:ASPxCallbackPanel ID="xCallbackPanel_supplier" runat="server" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <span class="style1" 
                                                style="color: rgb(51, 51, 51); font-family: 'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); display: inline !important; float: none;">
                                            <dx:ASPxLabel ID="xlabel_supplier" runat="server" 
                                                Text="Cấu hình thuộc tính cho nhà cung cấp">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <uc1:NASCustomFieldDataGridView ID="grid_Supplier" runat="server" />
                                            </span>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                                <br />
                                <br />
                                <br />
                                </span>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
