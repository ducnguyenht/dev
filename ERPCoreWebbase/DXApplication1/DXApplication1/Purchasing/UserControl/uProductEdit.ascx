<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uProductEdit.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.uProductEdit" %>
<%@ Register Src="../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
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
    .dxtlHSEC
    {
        width: 0px;
        display: none;
    }
</style>
<script type="text/javascript">
    function btnCancelItem_click(s, e) {
        formProductEdit.Hide();
    }

    function btnSaveItem_click(s, e) {

        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcProduct.GetMainElement(), null, true);
        if (!validated) {
            pcProduct.SetActiveTabIndex(0);
            return;
        }

        if (!checkHaveBlankCharInCode())
            return;

        var rs = checkIsValidFormatCode();

        if (!rs) {
            var r = confirm("Mã đang chứa giá trị đặc biệt, bạn có muốn lưu lại hay không?")
            if (r == true) {
                clientAction = 'Save';
                cpItemEdit.PerformCallback(clientAction);
            } else
                pcProduct.SetActiveTabIndex(0);
            return;
        }

        clientAction = 'Save';
        cpItemEdit.PerformCallback(clientAction);
    }

    function cpItemEdit_EndCallback(s, e) {
        switch (clientAction) {
            case 'Add':
                txtProductCode.Focus();
                break;
            case 'Edit':
                var jObject = $(".KeyShortcutformProductEdit");
                jObject.attr("tabindex", "0");
                jObject.focus();
                break;
            case 'ActivateForm':
                pcProduct.SetActiveTabIndex(0);
                txtProductCode.Focus();
                break;
            case 'Delete':
            case 'Save':
                grdProduct.Refresh();
                //grdProduct.Focus();
                if (s.cpIsSaved) {
                    delete s.cpIsSaved;
                    alert('Đã cập nhật thông tin đối tượng');
                }
                break;
        }
        grdUnitType.Refresh();
        ldpnItemEdit.Hide();
    }

    function loadFocusBaseTabIndex() {
        var curridx = pcProduct.GetActiveTabIndex();

        if (clientAction == 'Add') {
            switch (curridx) {
                case 0:
                    txtProductCode.Focus();
                    break;
                case 1:
                    cboSupplierColumn.Focus();
                    break;
                case 2:
                    cboUnitIdColumn.Focus();
                    break;
                case 3:
                    htmlEditDescription.Focus();
                    break;
            }
        }

        if (clientAction == 'Edit') {
            switch (curridx) {
                case 0:
                    var jObject = $(".KeyShortcutformProductEdit");
                    jObject.focus();
                    break;
                case 1:
                    grdProductSupplier.Focus();
                    break;
                case 2:
                    treelstProductUnits.ExpandAll();
                    treelstProductUnits.GetMainElement().focus();
                    break;
                case 3:
                    htmlEditDescription.Focus();
                    break;
            }
        }

        if (clientAction == 'ActivateForm') {
            switch (curridx) {
                case 0:
                    txtProductCode.Focus();
                    break;
                case 1:
                    grdProductSupplier.Focus();
                    break;
                case 2:
                    treelstProductUnits.ExpandAll();
                    treelstProductUnits.GetMainElement().focus();
                    break;
                case 3:
                    htmlEditDescription.Focus();
                    break;
            }
        }
    }

    function lbType_SelectedIndexChanged(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcProduct.GetMainElement(), null, true);
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

    function UnitIdCbo_SelectedIndexChanged(s, e) {
        var itemUnit = s.GetSelectedItem();
        if (itemUnit != null) {
            var unitName = itemUnit.GetColumnText("Name");
            var editor = treelstProductUnits.GetEditor('Name');
            editor.SetValue(unitName);
            editor = treelstProductUnits.GetEditor('NumRequired');
            editor.SetValue(1);
        }
    }

    function cpProductCode_EndCallback(s, e) {
        LoadingPanelCombineMaterial.Hide();
        lbType.PerformCallback("Refresh");
    }

    function supplierCbo_SelectedIndexChanged(s, e) {
        var supp = s.GetSelectedItem();
        if (supp != null) {

            var name = supp.GetColumnText("Name");
            var editor = grdProductSupplier.GetEditor('SupplierName');
            editor.SetValue(name);

            name = supp.GetColumnText("Description");
            editor = grdProductSupplier.GetEditor('SupplierDescription');
            editor.SetValue(name);
        }
    }

    // Shortcut for popup---START-----
    function formProductEdit_Init(s, e) {
        var jObject = $(".KeyShortcutformProductEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        htmlObject.focus();
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            formProductEdit.Hide();
        });
        //Press Ctrl+Enter to save general information
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pcProduct.GetMainElement(), null, true);
            if (validated) {
                clientAction = 'Save';
                cpItemEdit.PerformCallback('Save');
            } else {
                pcProduct.SetActiveTabIndex(0);
            }
        });

        //Utils.AttachShortcutTo(htmlObject, "Ctrl+F2", function () {
        //    clientAction = 'ActivateForm';
        //    cpItemEdit.PerformCallback(clientAction);
        //    loadFocusBaseTabIndex();
        //});

        Utils.AttachShortcutTo(htmlObject, "Ctrl+H", function () {
            popupItemUnitHelper.Show();
        });

        Utils.AttachShortcutTo(htmlObject, "Shift+Right", function () {
            var curridx = pcProduct.GetActiveTabIndex();
            if (curridx < 3) {
                pcProduct.SetActiveTabIndex(curridx + 1);
                jObject.focus();
                loadFocusBaseTabIndex();
            }
        });

        Utils.AttachShortcutTo(htmlObject, "Shift+Left", function () {
            var curridx = pcProduct.GetActiveTabIndex();
            if (curridx >= 1) {
                pcProduct.SetActiveTabIndex(curridx - 1);
                jObject.focus();
                loadFocusBaseTabIndex();
            }
        });
    }

    function formProductEdit_Closing(s, e) {
        grdProduct.Focus();
    }

    function grdProductSupplier_Init(s, e) {
        Utils.AttachStandardShortcutToGridview(s);
    }

    function treelstProductUnits_Init(s, e) {
        UtilsForTreeList.AttachStandardShortcutToTreeList(s);
        var nodeKeys = s.GetVisibleNodeKeys();
        if (nodeKeys.length == 0)
            return;
        s.GetMainElement().focus();
        s.SetFocusedNodeKey(nodeKeys[0]);
        s.ExpandAll();

    }
    // Shortcut for popup---END-----

    function treelstProductUnits_EndCallback(s, e) {
        var nodeKeys = s.GetVisibleNodeKeys();
        if (nodeKeys.length == 0)
            return;
        s.GetMainElement().focus();
        s.SetFocusedNodeKey(nodeKeys[0]);
    }

    function checkIsValidFormatCode() {
        var rgx = /[!@#$%^&*(){},?~`]/g;
        var rs = txtProductCode.GetText().match(rgx);
        if (rs != null) {
            txtProductCode.SetIsValid(false);
            txtProductCode.SetErrorText('Tồn tại giá trị đặc biệt');
            return false;
        }
        return true;
    }

    function checkHaveBlankCharInCode() {
        var rgx = /[\s\t]/g;
        var rs = txtProductCode.GetText().match(rgx);
        if (rs != null) {
            txtProductCode.SetIsValid(false);
            txtProductCode.SetErrorText('Tồn kí tự trắng');
            return false;
        }
        return true;
    }

    function txtProductCode_LostFocus(s, e) {
        if (checkHaveBlankCharInCode());
        checkIsValidFormatCode();
    }

    //DND 1094
    function TaxId_SelectedIndexChanged(s, e) {
        var taxId = s.GetSelectedItem();
        if (taxId != null) {
            var percentage = taxId.GetColumnText('Percentage');
            var editor = ASPxGridTax.GetEditor('Percentage');
            if (percentage != null) {
                editor.SetValue(percentage);
            }
            var name = taxId.GetColumnText('Name');
            var editor = ASPxGridTax.GetEditor('Name');
            if (name != null) {
                editor.SetValue(name);
            }
        }
    }
    function ASPxGridTax_Init(s, e) {
        Utils.AttachStandardShortcutToGridview(s);
    }
    //END DND 1094
</script>
<dx:aspxcallbackpanel id="cpItemEdit" runat="server" showloadingpanel="false" showloadingpanelimage="false"
    clientinstancename="cpItemEdit" oncallback="cpItemEdit_Callback" width="100%"
    maximized="True">
    <ClientSideEvents BeginCallback="function(s, e){ldpnItemEdit.Show();}" EndCallback="cpItemEdit_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <div id="lineContainerProduct">
                <dx:ASPxLoadingPanel ID="ldpnItemEdit" runat="server" HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
                    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
                </dx:ASPxLoadingPanel>
                <dx:ASPxPopupControl ID="formProductEdit" ClientInstanceName="formProductEdit" runat="server"
                    CssClass="KeyShortcutformProductEdit" HeaderText="Thông Tin Đối Tượng - " Height="620px"
                    Modal="True" Width="820px" AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True" CloseAction="CloseButton" RenderMode="Classic"
                    LoadingPanelText="FormLoad" Maximized="True">
                    <ClientSideEvents Init="formProductEdit_Init" Closing="formProductEdit_Closing" />
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="bProductEditHelp" CausesValidation="false" AutoPostBack="false"
                                    runat="server" CssClass="float_left dl mg" Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                    <ClientSideEvents Click="function(s, e){
                                        popupItemUnitHelper.Show();
                                    }" />
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnCancelItem" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnCancelItem" Text="Thoát" Wrap="False" ToolTip="Thoát  - ESC">
                                    <ClientSideEvents Click="btnCancelItem_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnSaveItem" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnSaveItem" Text="Lưu lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + Enter">
                                    <ClientSideEvents Click="btnSaveItem_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <%--<div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnEditItem" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnEditItem" Text="Chỉnh sửa" Wrap="False" 
                                    ToolTip="Lưu và Đóng - Ctr + F2">
                                    <ClientSideEvents Click="btnEditItem_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>--%>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%;" id="testheight">
                                <dx:ASPxPageControl ID="pcProduct" runat="server" ActiveTabIndex="1" ClientInstanceName="pcProduct"
                                    Height="100%" LoadingPanelText="TabLoad" ShowLoadingPanel="False" 
                                    Width="100%">
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="frmlyItemEdit" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã" HelpText="Tối đa 36 ký tự, không cho phép trùng lắp."
                                                                        RequiredMarkDisplayMode="Required">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxCallbackPanel ID="cpProductCode" runat="server" ClientInstanceName="cpProductCode"
                                                                                    OnCallback="cpProductCode_Callback" ShowLoadingPanel="False" Width="200px">
                                                                                    <ClientSideEvents EndCallback="cpProductCode_EndCallback"></ClientSideEvents>
                                                                                    <PanelCollection>
                                                                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxTextBox ID="txtProductCode" runat="server" ClientInstanceName="txtProductCode"
                                                                                                MaxLength="36" OnValidation="txtProductCode_Validation" Width="200px">
                                                                                                <ClientSideEvents LostFocus="txtProductCode_LostFocus"></ClientSideEvents>
                                                                                                <ValidationSettings ErrorText="">
                                                                                                    <%--<RegularExpression ValidationExpression="^[A-Za-z0-9_-][A-Za-z0-9_-]{0,35}$" 
                                                                                                        ErrorText="Mã hàng hóa không đúng định dạng" />--%>
                                                                                                    <RequiredField IsRequired="True" ErrorText="Chưa nhập m&#227; h&#224;ng h&#243;a">
                                                                                                    </RequiredField>
                                                                                                    <RequiredField ErrorText="Chưa nhập mã hàng hóa" IsRequired="True" />
                                                                                                </ValidationSettings>
                                                                                                <ClientSideEvents LostFocus="txtProductCode_LostFocus" />
                                                                                            </dx:ASPxTextBox>
                                                                                        </dx:PanelContent>
                                                                                    </PanelCollection>
                                                                                    <ClientSideEvents EndCallback="cpProductCode_EndCallback" />
                                                                                </dx:ASPxCallbackPanel>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tên" FieldName="Name" HelpText="Tối đa 255 ký tự." RequiredMarkDisplayMode="Required">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="txtProductName" runat="server" ClientInstanceName="txtProductName"
                                                                                    MaxLength="255" OnValidation="txtProductName_Validation" Width="400px">
                                                                                    <ValidationSettings ErrorText="">
                                                                                        <RequiredField ErrorText="Chưa nhập tên hàng hóa" IsRequired="True" />
                                                                                        <RequiredField IsRequired="True" ErrorText="Chưa nhập t&#234;n h&#224;ng h&#243;a">
                                                                                        </RequiredField>
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Trạng Thái" FieldName="RowStatus" HelpText="Tự động tạo mới"
                                                                        Visible="false">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="cboProductRowStatus" runat="server" ClientInstanceName="cboProductRowStatus"
                                                                                    Width="200px">
                                                                                    <Items>
                                                                                        <dx:ListEditItem Text="Sử dụng" Value="1" />
                                                                                        <dx:ListEditItem Text="Tạm" Value="0" />
                                                                                        <dx:ListEditItem Text="Tạm ngưng" Value="-1" />
                                                                                    </Items>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Nhà sản xuất" FieldName="OrganizationId" HelpText="Chọn nhà sản xuất"
                                                                        RequiredMarkDisplayMode="Required">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="cboProductManufacturer" runat="server" ClientInstanceName="cboProductManufacturer"
                                                                                    DropDownStyle="DropDown" 
                                                                                    EnableCallbackMode="True" 
                                                                                    CallbackPageSize="10" 
                                                                                    IncrementalFilteringMode="Contains"
                                                                                    TextFormatString="{0}" 
                                                                                    TextField="Name" 
                                                                                    ValueField="OrganizationId" 
                                                                                    Width="400px"
                                                                                    OnItemsRequestedByFilterCondition="cboProductManufacturer_OnItemsRequestedByFilterCondition_SQL"
                                                                                    OnItemRequestedByValue="cboProductManufacturer_OnItemRequestedByValue_SQL"
                                                                                    DropDownRows="10">
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn Caption="OrganizationId" FieldName="OrganizationId" Visible="false" />
                                                                                        <dx:ListBoxColumn Caption="Mã nhà sản xuất" FieldName="Code" Width="150px" />
                                                                                        <dx:ListBoxColumn Caption="Tên nhà sản xuất" FieldName="Name" Width="200px" />
                                                                                        <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" Width="200px" />
                                                                                    </Columns>
                                                                                    <%--<ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                        <RequiredField ErrorText="Chưa chọn nhà sản xuất" IsRequired="True" />
                                                                                        <RequiredField IsRequired="True" ErrorText="Chưa chọn nh&#224; sản xuất"></RequiredField>
                                                                                    </ValidationSettings>--%>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Loại">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxListBox ID="lbType" runat="server" SelectionMode="CheckColumn" Width="100%"
                                                                                    ClientInstanceName="lbType" ValueField="ObjectTypeId" Rows="6" Height="190px"
                                                                                    OnCallback="lbType_Callback" ShowLoadingPanel="false" DataSourceID="ObjectTypeLbXDS"
                                                                                    ValidationSettings-CausesValidation="true">
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn FieldName="ObjectTypeId" Caption="ObjectTypeId" Width="100%" Visible="false" />
                                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên loại" Width="100px" Visible="false" />
                                                                                        <dx:ListBoxColumn FieldName="Description" Caption="Mô tả" Width="100%" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents SelectedIndexChanged="lbType_SelectedIndexChanged" />
                                                                                    <ValidationSettings CausesValidation="True">
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxListBox>
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
                                        <dx:TabPage Name="tabConfigCustomField" Text="Cấu hình động">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="TittleProductCustomFieldGrid" runat="server" Text="Cấu hình thuộc tính cho hàng hóa">
                                                    </dx:ASPxLabel>
                                                    <uc1:NASCustomFieldDataGridView ID="NASProductCustomFieldDataGridView" runat="server" />
                                                    <br />
                                                    <dx:ASPxLabel ID="TittleServiceCustomFieldGrid" runat="server" Text="Cấu hình thuộc tính cho dịch vụ">
                                                    </dx:ASPxLabel>
                                                    <uc1:NASCustomFieldDataGridView ID="NASServiceCustomFieldDataGridView" runat="server" />
                                                    <br />
                                                    <dx:ASPxLabel ID="TittleToolCustomFieldGrid" runat="server" Text="Cấu hình thuộc tính cho công cụ dụng cụ">
                                                    </dx:ASPxLabel>
                                                    <uc1:NASCustomFieldDataGridView ID="NASToolCustomFieldDataGridView" runat="server" />
                                                    <br />
                                                    <dx:ASPxLabel ID="TittleMaterialCustomFieldGrid" runat="server" Text="Cấu hình thuộc tính cho nguyên vật liệu">
                                                    </dx:ASPxLabel>
                                                    <uc1:NASCustomFieldDataGridView ID="NASMaterialCustomFieldDataGridView" runat="server" />
                                                    <br />
                                                    <dx:ASPxLabel ID="TittleSelfProductionCustomFieldGrid" runat="server" Text="Cấu hình thuộc tính cho sản phẩm">
                                                    </dx:ASPxLabel>
                                                    <uc1:NASCustomFieldDataGridView ID="NASSelfProductionCustomFieldDataGridView" runat="server" />
                                                    <br />
                                                    <dx:ASPxLabel ID="TittleFixedAssestCustomFieldGrid" runat="server" Text="Cấu hình thuộc tính cho tài sản cố định">
                                                    </dx:ASPxLabel>
                                                    <uc1:NASCustomFieldDataGridView ID="NASFixedAssestCustomFieldDataGridView" runat="server" />
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Nhà Cung Cấp">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="frmSupplierList" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdProductSupplier" runat="server" KeyboardSupport="true" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdProductSupplier" KeyFieldName="ItemSupplierId" Width="100%" DataSourceID="SupplierListXDS"
                                                                            OnRowDeleting="grdProductSupplier_RowDeleting"
                                                                            OnRowInserting="grdProductSupplier_RowInserting" 
                                                                            OnRowUpdating="grdProductSupplier_RowUpdating"
                                                                            OnRowValidating="grdProductSupplier_RowValidating" 
                                                                            OnCommandButtonInitialize="grdProductSupplier_CommandButtonInitialize" 
                                                                            OnRowInserted="grdProductSupplier_RowInserted"
                                                                            OnCellEditorInitialize="grdProductSupplier_CellEditorInitialize">
                                                                            <ClientSideEvents Init="grdProductSupplier_Init" />
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã nhà cung cấp" Name="Supplier" FieldName="SupplierOrgId!Key"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                                                    <PropertiesComboBox ClientInstanceName="cboSupplierColumn" DropDownStyle="DropDownList"
                                                                                        IncrementalFilteringMode="Contains" TextField="Code" ValueField="OrganizationId"
                                                                                        CallbackPageSize="10" EnableCallbackMode="true" TextFormatString="{0}" 
                                                                                        OnItemsRequestedByFilterCondition="cboSupplierColumn_OnItemsRequestedByFilterCondition_SQL"
                                                                                        OnItemRequestedByValue="cboSupplierColumn_OnItemRequestedByValue_SQL" DropDownRows="10">
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="OrganizationId" FieldName="OrganizationId" Visible="false" />
                                                                                            <dx:ListBoxColumn Caption="Mã nhà cung cấp" FieldName="Code" Width="150px" />
                                                                                            <dx:ListBoxColumn Caption="Tên nhà cung cấp" FieldName="Name" Width="350px" />
                                                                                            <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                                                                                        </Columns>
                                                                                        <ClientSideEvents SelectedIndexChanged="supplierCbo_SelectedIndexChanged" />
                                                                                    </PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên nhà cung cấp" FieldName="SupplierName" ReadOnly="True"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="SupplierDescription" ReadOnly="True"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" Name="Action" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="5" Width="100px">
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
                                                                                    <CancelButton>
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
                                                                            </Columns>
                                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True"></SettingsBehavior>
                                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="true" />
                                                                            <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa?" EmptyDataRow="Chưa có nhà cung cấp" />
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
                                                                            <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto"></Settings>
                                                                        </dx:ASPxGridView>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới NCC" ToolTip="Tạo mới 1 NCC - Ctrl + N"
                                                                            Visible="False" Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn Nhà Cung Cấp">
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
                                        <dx:TabPage Text="Đơn Vị Tính">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <%--DND 944--%>
                                                    <div id="grdUnitTypeDIV">
                                                        <dx:ASPxGridView ID="grdUnitType" ClientInstanceName="grdUnitType" 
                                                            runat="server" AutoGenerateColumns="False" DataSourceID="UnitTypeXDS"
                                                            Width="100%"
                                                            SettingsLoadingPanel-Text="Đang xử lý"
                                                            KeyFieldName="ItemUnitTypeConfigId" OnCustomCallback="grdUnitType_CustomCallback"
                                                            OnHtmlRowPrepared="grdUnitType_HtmlRowPrepared">
                                                            <ClientSideEvents EndCallback="function(s,e){ 
                                                                if (s.cpChangedSelection) {
                                                                    delete s.cpChangedSelection;
                                                                    grdUnitType.Refresh();
                                                                    //alert('Đã cập nhật thông tin cấu hình đơn vị tính');
                                                                }
                                                             }"/>
                                                            <Columns>
                                                                <dx:GridViewDataColumn Caption="#" FieldName="IsSelected" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Width="100px">
                                                                    <DataItemTemplate>
                                                                        <center>
                                                                            <dx:ASPxCheckBox ID="ChkIsSelected" Value='<%# Bind("IsSelected") %>' runat="server" oninit="ChkIsSelected_Init">
                                                                            </dx:ASPxCheckBox>
                                                                        </center>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên đơn vị tính" 
                                                                    FieldName="UnitTypeId.Name" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Diễn giải" 
                                                                    FieldName="UnitTypeId.Description" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" Width="100%">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataColumn Caption="Đơn vị chuẩn" FieldName="IsMaster" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" Width="100px">
                                                                    <DataItemTemplate>
                                                                        <center>
                                                                            <dx:ASPxRadioButton ID="rdoIsMater" Value='<%# Bind("IsMaster") %>' ClientVisible='<%# Bind("IsSelected") %>' runat="server" oninit="rdoIsMater_Init">
                                                                            </dx:ASPxRadioButton>
                                                                        </center>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn Caption="Chi tiết" Name="Detail" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" Width="200px">
                                                                    <DataItemTemplate>
                                                                        <center>
                                                                            <dx:ASPxHyperLink ID="hyperlinkDetail" 
                                                                                ClientVisible='<%# Bind("IsSelected") %>' runat="server" 
                                                                                oninit="hyperlinkDetail_Init" Text="Chi tiết">
                                                                            </dx:ASPxHyperLink>
                                                                        </center>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataColumn>
                                                            </Columns>
                                                            <Settings ShowFilterRow="True" />

<SettingsLoadingPanel Text="Đang xử l&#253;"></SettingsLoadingPanel>
                                                        </dx:ASPxGridView>
                                                    </div>
                                                    <%--END DND 944--%>
                                                    <dx:ASPxPopupControl ID="popZoneTreelstProductUnits" 
                                                        ClientInstanceName="popZoneTreelstProductUnits" runat="server"
                                                        CssClass="KeyShortcutformProductEdit" HeaderText="Cấu hình chi tiết đơn vị tính" Height="500px"
                                                        Modal="True" Width="1208px" AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                                                        PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                                                        ScrollBars="Auto" ShowMaximizeButton="True" CloseAction="CloseButton" RenderMode="Classic"
                                                        LoadingPanelText="FormLoad" Maximized="false" 
                                                        OnWindowCallback="popZoneTreelstProductUnits_WindowCallback">
                                                            <ClientSideEvents EndCallback="function(s, e){ s.Show(); }" />
                                                            <ContentCollection>
                                                                <dx:PopupControlContentControl>
                                                                    <dx:ASPxTreeList ID="treelstProductUnits" runat="server" AutoGenerateColumns="False"
                                                                        ClientVisible="true"
                                                                        KeyboardSupport="true" ClientInstanceName="treelstProductUnits" Height="360px"
                                                                        KeyFieldName="ItemUnitId" ParentFieldName="ParentItemUnitId!Key" OnCellEditorInitialize="grdProductUnit_CellEditorInitialize"
                                                                        OnInit="grdProductUnit_Init" OnInitNewNode="grdProductUnit_InitNewNode"
                                                                        OnNodeInserting="grdProductUnit_NodeInserting" OnNodeValidating="grdProductUnit_NodeValidating"
                                                                        OnStartNodeEditing="grdProductUnit_StartNodeEditing" Width="100%" OnHtmlDataCellPrepared="grdProductUnit_HtmlDataCellPrepared"
                                                                        DataSourceID="ItemUnitTreeXDS" OnNodeInserted="treelstProductUnits_NodeInserted"
                                                                        AccessKey="G" OnHtmlRowPrepared="treelstProductUnits_HtmlRowPrepared" 
                                                                        OnNodeDeleting="treelstProductUnits_NodeDeleting" 
                                                                        OnCustomCallback="treelstProductUnits_CustomCallback">
                                                                        <Settings GridLines="Both" HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"
                                                                            ScrollableHeight="360"></Settings>
                                                                        <ClientSideEvents Init="treelstProductUnits_Init" 
                                                                            NodeCollapsing="function(s, e){
                                                                                e.cancel = true;
                                                                                s.ExpandAll();
                                                                            }"
                                                                            EndCallback="treelstProductUnits_EndCallback">
                                                                        </ClientSideEvents>
                                                                        <Columns>
                                                                            <dx:TreeListTextColumn FieldName="ItemUnitId" ShowInCustomizationForm="True" Visible="False"
                                                                                VisibleIndex="1">
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" Visible="False"
                                                                                VisibleIndex="1">
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListComboBoxColumn Caption="Mã đơn vị tính" FieldName="UnitId.Code" Name="Code"
                                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                                <PropertiesComboBox DropDownStyle="DropDown" DataSourceID="UnitCboXDS" ClientInstanceName="cboUnitIdColumn"
                                                                                    IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{1}" ValueField="Code"
                                                                                    EnableCallbackMode="true">
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn Caption="UnitId" FieldName="UnitId" Visible="false" />
                                                                                        <dx:ListBoxColumn Caption="Mã đơn vị tính" FieldName="Code" Width="150px" />
                                                                                        <dx:ListBoxColumn Caption="Tên đơn vị tính" FieldName="Name" Width="200px" />
                                                                                    </Columns>
                                                                                    <ClientSideEvents SelectedIndexChanged="UnitIdCbo_SelectedIndexChanged" />
                                                                                </PropertiesComboBox>
                                                                            </dx:TreeListComboBoxColumn>
                                                                            <dx:TreeListTextColumn Caption="Tên đơn vị tính" FieldName="UnitId.Name" Name="Name"
                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListSpinEditColumn Caption="Bao Gồm" FieldName="NumRequired" Name="NumRequired"
                                                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
<PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                                                            </dx:TreeListSpinEditColumn>
                                                                            <dx:TreeListDataColumn Caption="Đơn vị chuẩn" FieldName="IsDefault" ShowInCustomizationForm="True"
                                                                                VisibleIndex="7" Width="100px">
                                                                                <DataCellTemplate>
                                                                                    <center>
                                                                                        <dx:ASPxRadioButton ID="rdoIsDefault" Value='<%# Bind("IsDefault") %>' runat="server" oninit="rdoIsDefault_Init">
                                                                                        </dx:ASPxRadioButton>
                                                                                    </center>
                                                                                </DataCellTemplate>
                                                                                <EditCellTemplate>
                                                                                </EditCellTemplate>
                                                                            </dx:TreeListDataColumn>
                                                                            <dx:TreeListTextColumn Caption="Hệ số quy đổi" FieldName="Coefficient" ShowInCustomizationForm="True"
                                                                                VisibleIndex="8" Width="100px" ReadOnly="true">
                                                                                <CellStyle Wrap="True">
                                                                                </CellStyle>
                                                                                <EditCellTemplate>
                                                                                </EditCellTemplate>
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListTextColumn Caption="Diễn Giải" Name="Description" ShowInCustomizationForm="True"
                                                                                VisibleIndex="9" Visible="true" Width="300px">
                                                                                <CellStyle Wrap="True">
                                                                                </CellStyle>
                                                                                <EditCellTemplate>
                                                                                </EditCellTemplate>
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListCommandColumn ButtonType="Image" Name="Action" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                                ShowNewButtonInHeader="True" VisibleIndex="9" Width="100px">
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
                                                                            </dx:TreeListCommandColumn>
                                                                            <dx:TreeListTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" Visible="False">
                                                                            </dx:TreeListTextColumn>
                                                                        </Columns>
                                                                        <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="360" VerticalScrollBarMode="Auto"
                                                                            GridLines="Both" ShowTreeLines="False" />
                                                                        <SettingsBehavior AutoExpandAllNodes="True" 
                                                                            FocusNodeOnExpandButtonClick="False" FocusNodeOnLoad="False" />
                                                                        <SettingsPager RenderMode="Classic">
                                                                        </SettingsPager>
                                                                        <SettingsLoadingPanel Text="Đang xử lý" />
                                                                        <SettingsEditing AllowRecursiveDelete="true" AllowNodeDragDrop="true" />
                                                                        <SettingsText LoadingPanelText="Đang xử lý" />
                                                                        <Styles>
                                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                                            </Header>
                                                                            <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                                            </CommandButton>
                                                                        </Styles>
                                                                        <ClientSideEvents Init="treelstProductUnits_Init" />
                                                                    </dx:ASPxTreeList>
                                                                </dx:PopupControlContentControl>
                                                            </ContentCollection>
                                                        </dx:ASPxPopupControl>
                                                    <dx:ASPxFormLayout ID="frmItemUnit" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới ĐVT" ToolTip="Tạo mới 1 ĐVT - Ctrl + N"
                                                                            Visible="False" Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn đơn vị tính">
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
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <%--DND 1094--%>
                                        <dx:TabPage Text="Thuế Suất">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridTax" runat="server" AutoGenerateColumns="true" DataSourceID="ItemTaxXDS"
                                                        KeyFieldName="ItemTaxId" Width="100%" ClientInstanceName="ASPxGridTax" OnRowInserting="ASPxGridTax_RowInserting"
                                                        OnCellEditorInitialize="ASPxGridTax_CellEditorInitialize" OnRowDeleting="ASPxGridTax_RowDeleting">
                                                        <ClientSideEvents Init="ASPxGridTax_Init" />
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Phân Loại" Name="txt_TaxId" FieldName="TaxId!Key"
                                                                ShowInCustomizationForm="true" VisibleIndex="0" Width="20%">
                                                                <PropertiesComboBox DataSourceID="TaxXDS" ValueField="TaxId" TextField="Code" TextFormatString="{0}"
                                                                    IncrementalFilteringMode="Contains">
                                                                    <ClientSideEvents SelectedIndexChanged="TaxId_SelectedIndexChanged" />
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="Code" Caption="Mã Phân Loại" />
                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên Phân Loại" />
                                                                        <dx:ListBoxColumn FieldName="Percentage" Caption="Tỉ Lệ" />
                                                                    </Columns>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên Phân Loại" FieldName="TaxId.Name" Name="Name"
                                                                ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tỷ Lệ Phần Trăm(%)" FieldName="TaxId.Percentage"
                                                                Name="Percentage" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                                <PropertiesTextEdit>
                                                                    <Style HorizontalAlign="Right">
                                                                        
                                                                    </Style>
                                                                </PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn Caption="Thao Tác" ButtonType="Image" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Width="10%">
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
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="true" />
                                                        <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa?" />
                                                    </dx:ASPxGridView>
                                                    <dx:XpoDataSource ID="TaxXDS" runat="server" 
                                                        TypeName="NAS.DAL.Accounting.Tax.Tax" 
                                                        Criteria="[RowStatus] &gt; 0 And [TaxTypeId.RowStatus] &gt; 0">
                                                    </dx:XpoDataSource>
                                                    <dx:XpoDataSource ID="ItemTaxXDS" runat="server" 
                                                        TypeName="NAS.DAL.Nomenclature.Item.ItemTax" 
                                                        Criteria="[ItemId!Key] = ? And [RowStatus] &gt; 0">
                                                        <CriteriaParameters>
                                                            <asp:SessionParameter Name="ItemId" SessionField="ItemId" />
                                                        </CriteriaParameters>
                                                    </dx:XpoDataSource>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <%--END DND 1094--%>
                                        <dx:TabPage Text="Thông Tin Chi Tiết">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ffdsds" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxNavBar ID="nbProduct" runat="server" AutoCollapse="True" ClientInstanceName="nbProduct"
                                                                            Height="100%" Width="100%">
                                                                            <Groups>
                                                                                <dx:NavBarGroup Text="Mô Tả">
                                                                                    <ContentTemplate>
                                                                                        <dx:ASPxHtmlEditor ID="htmlEditDescription" runat="server" ClientInstanceName="htmlEditDescription"
                                                                                            Height="350px" Width="100%">
                                                                                            <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                                        </dx:ASPxHtmlEditor>
                                                                                    </ContentTemplate>
                                                                                </dx:NavBarGroup>
                                                                            </Groups>
                                                                        </dx:ASPxNavBar>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Nhập mô tả cho hàng hóa">
                                                                        </dx:ASPxLabel>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                                <CaptionSettings VerticalAlign="Middle" />
                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                <CaptionSettings VerticalAlign="Middle"></CaptionSettings>
                                                                <Border BorderWidth="1px" BorderColor="#CCCCCC" BorderStyle="Solid"></Border>
                                                            </dx:LayoutItem>
                                                        </Items>
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
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:aspxcallbackpanel>
<%--<dx:xpodatasource id="ManufactuerCboXDS" runat="server" typename="NAS.DAL.Nomenclature.Organization.ManufacturerOrg"
    criteria="[RowStatus] &gt; 0">
</dx:xpodatasource>--%>
<dx:xpodatasource id="ObjectTypeLbXDS" runat="server" typename="NAS.DAL.CMS.ObjectDocument.ObjectType"
    criteria="">
</dx:xpodatasource>
<dx:xpodatasource id="ItemUnitTreeXDS" runat="server" typename="NAS.DAL.Nomenclature.Item.ItemUnit"
    criteria="[ItemId!Key] = ? And ([RowStatus] = 1 Or [RowStatus] = -1) And ([UnitId.RowStatus] = 1 Or [UnitId.RowStatus] = -1) And [ItemId] Is Not Null And [UnitId] Is Not Null And [UnitId.UnitTypeId.Code] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="ItemId" SessionField="ItemId" />
        <asp:SessionParameter Name="UnitTypeCode" SessionField="UnitTypeCode" />
    </CriteriaParameters>
</dx:xpodatasource>
<dx:xpodatasource id="SupplierListXDS" runat="server" typename="NAS.DAL.Nomenclature.Item.ItemSupplier"
    criteria="[ItemId!Key] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="ItemId" SessionField="ItemId" />
    </CriteriaParameters>
</dx:xpodatasource>
<dx:xpodatasource id="UnitCboXDS" runat="server" typename="NAS.DAL.Nomenclature.Item.Unit"
    criteria="[RowStatus] &gt; 0">
</dx:xpodatasource>
<%--DND--%>
<dx:xpodatasource id="UnitTypeXDS" runat="server" typename="NAS.DAL.Nomenclature.Item.ItemUnitTypeConfig"
    criteria="[ItemId] = ? And ([RowStatus] = -1 Or [RowStatus] = 1)">
    <CriteriaParameters>
        <asp:SessionParameter Name="ItemId" SessionField="ItemId" />
    </CriteriaParameters>
</dx:xpodatasource>
<%--END DND--%>