<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingMaterial.ascx.cs"
    Inherits="DXApplication1.Purchasing.UserControl.uBuyingMaterial" %>
<style type="text/css">
    .dxtlHSEC
    {
        width:0px;
        display:none;
    }
</style>
<script type="text/javascript">    
    function buttonSaveMaterial_click(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcMaterial.GetMainElement(), null, true);
        if (validated) {
            cpLineMaterial.PerformCallback('SaveMaterial');
            console.log(validated);
        } else 
            pcMaterial.SetActiveTabIndex(0);
    }

    function buttonEditMaterial_click(s, e) {
        cpLineMaterial.PerformCallback('ActivateForm');
    }


    function updateByLostFocusOnMaterialEdit(s, e) {
        if (formMaterialEdit.cpMode == 'add')
            return;

        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcMaterial.GetMainElement(), null, true);
            if (validated) {
                if (cpCheckMaterialCode.InCallback())
                    console.log("Server's busy");
                else {
                    cpCheckMaterialCode.PerformCallback("updateByLostFocus");
                } 
        }
    }

    function buttonCancelMaterial_click(s, e) {
        formMaterialEdit.Hide();
    }

    function cpLineMaterial_Endcallback(s, e) {
        grdDataMaterial.PerformCallback('Done');
        LoadingPanelCombineMaterial.Hide();
    }

    function cpCheckMaterialCode_Endcallback(s, e) {
    }

    //Validation all items when submit
    function validationFormMaterial(s, e) {
        var nameItem = s.name;
        var valueItem = s.GetText().trim();
        var lengthItem = valueItem.length;
        switch (nameItem)
        {
            case '<%= txtCode.ClientID %>':
                if (valueItem == '') {
                    e.isValid = false;
                    e.errorText = "Bắt buộc nhập Mã nguyên vật liệu";
                }

                break;
                
                if (lengthItem > 128) {
                    e.isValid = false;
                    e.errorText = "Độ dài mã bị vượt quá giới hạn";
                }

                break;
            case '<%= txtName.ClientID %>':
                if (valueItem == '') {
                    e.isValid = false;
                    e.errorText = "Bắt buộc nhập Tên nguyên vật liệu";
                }

                break;

                if (lengthItem > 255) {
                    e.isValid = false;
                    e.errorText = "Độ dài tên bị vượt quá giới hạn";
                }
                break;
            default:
                break;
        }
    }

    function cboCodeCategoryMaterialGrp_selectedIndexChanged(s, e) {
        var itemMaterialGrp = s.GetSelectedItem();

        if (itemMaterialGrp != null) {
            var txtMaterialGrp = itemMaterialGrp.GetColumnText(1);
            var editor = grdMaterialOnCategory.GetEditor('Name');
            editor.SetValue(txtMaterialGrp);
        }
    }

    function cboCodeSupplier_selectedIndexChanged(s, e) {
        var itemSupplier = s.GetSelectedItem();

        if (itemSupplier != null) {
            var txtSupplier = itemSupplier.GetColumnText("Name");
            var editor = grdSupplierOnMaterial.GetEditor('Name');
            editor.SetValue(txtSupplier);
        }
    }

    function cboCodeUnit_SelectedIndexChanged(s, e) {
        var itemUnit = s.GetSelectedItem("Name");
        if (itemUnit != null) {
            var txtUnit = itemUnit.GetColumnText("Name");
            var editor = treelstMaterialUnits.GetEditor('Name');
            editor.SetValue(txtUnit);
        }
    }

    function formMaterialEdit_closing(s, e) {
        grdDataMaterial.PerformCallback('Done');
    }

</script>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLineMaterial" runat="server" Width="100%" ClientInstanceName="cpLineMaterial"
        OnCallback="cpLineMaterial_Callback" ShowLoadingPanel="false">
        <ClientSideEvents EndCallback="cpLineMaterial_Endcallback" />
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxHiddenField ID="hiddenMode" ClientInstanceName="hiddenMode" runat="server">
                </dx:ASPxHiddenField>
                <dx:ASPxPopupControl ID="formMaterialEdit" runat="server" HeaderText="Thông Tin Nguyên Vật Liệu -"
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formMaterialEdit"
                    AllowDragging="True" RenderMode="Classic" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True" CloseAction="CloseButton">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="btnHelp" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelMaterial" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelMaterial" Text="Thoát" 
                                    CausesValidation="false"
                                    Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="buttonCancelMaterial_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonSaveMaterial" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveMaterial" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="buttonSaveMaterial_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonEditMaterial" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonEditMaterial" Text="Chỉnh sửa" Wrap="False">
                                    <ClientSideEvents Click="buttonEditMaterial_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="pcMaterial" ClientInstanceName="pcMaterial" 
                                runat="server" ActiveTabIndex="2" Height="100%"
                                Width="100%" EnableTabScrolling="True" RenderMode="Classic">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="FormCommonMaterialEdit" ClientInstanceName="FormCommonMaterialEdit" runat="server" AlignItemCaptionsInAllGroups="True"
                                                    EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã nguyên vật liệu" 
                                                                    FieldName="Code"
                                                                    RequiredMarkDisplayMode="Required"
                                                                    HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpCheckMaterialCode" runat="server" Width="100%" ClientInstanceName="cpCheckMaterialCode"
                                                                                OnCallback="cpCheckMaterialCode_Callback" ShowLoadingPanel="false">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtCode" ClientInstanceName="txtMaterialCode" runat="server" MaxLength="128"
                                                                                        Width="200px">
                                                                                        <NullTextStyle ForeColor="Silver">
                                                                                        </NullTextStyle>
                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="false">
                                                                                            <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Mã nguyên vật liệu" />
                                                                                        </ValidationSettings>
                                                                                        <ClientSideEvents Validation="validationFormMaterial" LostFocus="updateByLostFocusOnMaterialEdit"  />
                                                                                    </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                                <ClientSideEvents EndCallback="cpCheckMaterialCode_Endcallback" />
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên nguyên vật liệu" 
                                                                    FieldName="Name"
                                                                    RequiredMarkDisplayMode="Required"
                                                                    HelpText="255 ký tự, không cho phép trùng lắp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtName" ClientInstanceName="txtName" runat="server"
                                                                                MaxLength="255" NullText="255 ký tự, không cho phép trùng lắp" Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="false">
                                                                                    <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Tên nguyên vật liệu" />
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents Validation="validationFormMaterial" LostFocus="updateByLostFocusOnMaterialEdit" />
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng thái" 
                                                                    FieldName="RowStatus">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboRowStatus" runat="server"
                                                                                Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="I" />
                                                                                </Items>
                                                                                <ClientSideEvents LostFocus="updateByLostFocusOnMaterialEdit" />
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Nhà sản xuất" 
                                                                    FieldName="ManufacturerId"
                                                                    HelpText="Chọn nhà sản xuất">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboManufacturer" runat="server" 
                                                                                DropDownHeight="200px" DropDownWidth="450px" EnableCallbackMode="True"
                                                                                CallbackPageSize="20"
                                                                                IncrementalFilteringMode="Contains"
                                                                                TextField="Name" TextFormatString="{0};{1}" ValueField="ManufacturerId" 
                                                                                Width="400px" DataSourceID="cboManufacturerXDS">
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn Caption="Mã nhà sản xuất" FieldName="Code" Name="Code" Width="150px" />
                                                                                    <dx:ListBoxColumn Caption="Tên nhà sản xuất" FieldName="Name" Name="Name" Width="300px" />
                                                                                    <dx:ListBoxColumn Caption="Key" FieldName="ManufacturerId" Name="ManufacturerId" Width="150px" Visible="false"/>
                                                                                </Columns>
                                                                                <ValidationSettings SetFocusOnError="false">
                                                                                    <RequiredField ErrorText="Bắt buộc chọn Nhà sản xuất" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents LostFocus="updateByLostFocusOnMaterialEdit" />
                                                                            </dx:ASPxComboBox>
                                                                            <dx:XpoDataSource ID="cboManufacturerXDS" runat="server" 
                                                                                TypeName="DAL.Purchasing.ViewManufacturer">
                                                                            </dx:XpoDataSource>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <%--<dx:LayoutItem Caption="Hình Ảnh">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                                Width="300px">
                                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho nguyên vật liệu">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" Width="300px">
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
                                    <dx:TabPage Text="Trực thuộc nhóm">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdMaterialOnCategory" runat="server" 
                                                    AutoGenerateColumns="False" Width="100%"
                                                    ClientInstanceName="grdMaterialOnCategory"
                                                    KeyFieldName="Code" 
                                                    OnRowInserting="grdMaterialOnCategory_RowInserting"
                                                    OnRowDeleting="grdMaterialOnCategory_RowDeleting" 
                                                    OnRowValidating="grdMaterialOnCategory_RowValidating" 
                                                    OnCommandButtonInitialize="grdMaterialOnCategory_CommandButtonInitialize">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="Code" VisibleIndex="0" Caption="Mã nhóm NVL" Width="300px">
                                                            <EditItemTemplate>
                                                                <dx:ASPxComboBox ID="cboCodeCategory"
                                                                    runat="server" 
                                                                    oninit="cboCodeCategory_Init" 
                                                                    Width="100%"
                                                                    TextField="Name" ValueField="Code">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                        <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                    </Columns>
                                                                    <ClientSideEvents SelectedIndexChanged="cboCodeCategoryMaterialGrp_selectedIndexChanged" />
                                                                </dx:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên nhóm NVL" FieldName="Name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1" Width="60%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Name="action" Caption="Thao tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="100px">
                                                            <NewButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                            <UpdateButton>
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="MaterialId" FieldName="MaterialId" ShowInCustomizationForm="True"
                                                            VisibleIndex="3" Visible="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BuyingMaterialCategoryId" FieldName="BuyingMaterialCategoryId" ShowInCustomizationForm="True"
                                                            VisibleIndex="4" Visible="false">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MaterialBuyingMaterialCategoryId" FieldName="MaterialBuyingMaterialCategoryId" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Visible="false">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                        ShowHeaderFilterButton="True" VerticalScrollableHeight="360" 
                                                        VerticalScrollBarMode="Auto"/>
                                                    <SettingsEditing Mode="Inline" />
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Nhà cung cấp">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdSupplierOnMaterial" KeyFieldName="Code" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="grdSupplierOnMaterial"
                                                    OnRowInserting="grdSupplierOnMaterial_RowInserting"
                                                    OnRowDeleting="grdSupplierOnMaterial_RowDeleting"
                                                    OnCommandButtonInitialize="grdMaterialOnCategory_CommandButtonInitialize"
                                                    Width="100%" OnRowValidating="grdSupplierOnMaterial_RowValidating">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã NCC" FieldName="Code" VisibleIndex="0" Width="150px">
                                                            <EditItemTemplate>
                                                                <dx:ASPxComboBox ID="cboCodeSupplier" runat="server" 
                                                                    oninit="cboCodeSupplier_Init" 
                                                                    Width="100%"
                                                                    TextField="Name" ValueField="Code">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                        <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                    </Columns>
                                                                    <ClientSideEvents SelectedIndexChanged="cboCodeSupplier_selectedIndexChanged" />
                                                                </dx:ASPxComboBox>
                                                            </EditItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên nhà cung cấp" FieldName="Name" Name="Name"
                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Name="action" Caption="Thao tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Width="100px">
                                                            <NewButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="true">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                            <UpdateButton>
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <CancelButton>
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="MaterialSupplierId"
                                                            Name="MaterialSupplierId" ShowInCustomizationForm="True" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="MaterialId"
                                                            Name="MaterialId" ShowInCustomizationForm="True" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SupplierId"
                                                            Name="SupplierId" ShowInCustomizationForm="True" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                        VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                    <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Styles>
                                                        <Header HorizontalAlign="Center" Font-Bold="true">
                                                        </Header>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Đơn vị tính">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="formMaterialUnit" runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutItem ShowCaption="false">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxTreeList ID="treelstMaterialUnits" ClientInstanceName="treelstMaterialUnits"
                                                                        KeyFieldName="MaterialMaterialUnitHierachyId" ParentFieldName="ParentMaterialMaterialUnitId"
                                                                        runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        OnNodeDeleting="treelstMaterialUnits_NodeDeleting" 
                                                                        OnNodeInserting="treelstMaterialUnits_NodeInserting" 
                                                                        OnNodeUpdating="treelstMaterialUnits_NodeUpdating" 
                                                                        OnNodeValidating="treelstMaterialUnits_NodeValidating" 
                                                                        OnCellEditorInitialize="treelstMaterialUnits_CellEditorInitialize" 
                                                                        OnHtmlDataCellPrepared="treelstMaterialUnits_HtmlDataCellPrepared" 
                                                                        OnCommandColumnButtonInitialize="treelstMaterialUnits_CommandColumnButtonInitialize">
                                                                        <Columns>
                                                                            <dx:TreeListTextColumn Caption="Mã đơn vị tính" FieldName="Code" Name="Code" VisibleIndex="0">
                                                                                <EditCellTemplate>
                                                                                    <dx:ASPxComboBox ID="cboCodeUnit" runat="server"
                                                                                        OnInit="cboCodeUnit_Init"
                                                                                        Width="100%"
                                                                                        TextField="Name" 
                                                                                        ValueField="Code">
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="MaterialUnitId" FieldName="MaterialUnitId" Width="150px" 
                                                                                                Visible="false"/>
                                                                                            <dx:ListBoxColumn Caption="MaterialUnitPropertyId" FieldName="MaterialUnitPropertyId" Width="150px"
                                                                                                 Visible="false"/>
                                                                                            <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                                                            <dx:ListBoxColumn Caption="Tên" FieldName="Name" />
                                                                                        </Columns>
                                                                                        <ClientSideEvents SelectedIndexChanged="cboCodeUnit_SelectedIndexChanged" />
                                                                                    </dx:ASPxComboBox>
                                                                                </EditCellTemplate>
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListTextColumn Caption="Tên đơn vị tính" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListSpinEditColumn Caption="Số lượng" FieldName="NumRequired" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                                                            </dx:TreeListSpinEditColumn>
                                                                            <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="Description" ShowInCustomizationForm="True" VisibleIndex="3" Visible="true" Width="300px">
                                                                                <CellStyle Wrap="True">
                                                                                </CellStyle>
                                                                                <EditCellTemplate></EditCellTemplate>
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                Width="10%" Name="action" Caption="Thao Tác" ShowNewButtonInHeader="true">
                                                                                <EditButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                                    </Image>
                                                                                </EditButton>
                                                                                <NewButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                                    </Image>
                                                                                </NewButton>
                                                                                <DeleteButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                                    </Image>
                                                                                </DeleteButton>
                                                                                <UpdateButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                                    </Image>
                                                                                </UpdateButton>
                                                                                <CancelButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                    </Image>
                                                                                </CancelButton>
                                                                            </dx:TreeListCommandColumn>
                                                                            <dx:TreeListTextColumn FieldName="MaterialMaterialUnitId" ShowInCustomizationForm="True" Visible="false">
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListTextColumn FieldName="ParentMaterialMaterialUnitId" ShowInCustomizationForm="True" Visible="false">
                                                                            </dx:TreeListTextColumn>
                                                                            <dx:TreeListTextColumn FieldName="MaterialMaterialUnitHierachyId" ShowInCustomizationForm="True" Visible="false">
                                                                            </dx:TreeListTextColumn>
                                                                        </Columns>
                                                                        <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="360" 
                                                                        VerticalScrollBarMode="Auto" />
                                                                        <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" 
                                                                            ExpandCollapseAction="NodeDblClick" />
                                                                        <Styles>
                                                                            <Header HorizontalAlign="Center" Font-Bold="true">
                                                                            </Header>
                                                                            <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                                            </CommandButton>
                                                                        </Styles>
                                                                        <Settings ShowFooter="true" />
                                                                        <SettingsEditing AllowRecursiveDelete="true" />
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
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxNavBar ID="navBarMaterialDetail" runat="server" AutoCollapse="True" Height="100%"
                                                    Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <ContentTemplate>
                                                                <dx:ASPxHtmlEditor ID="htmlEditDescription" runat="server" Height="350px" Width="100%" Enabled="false">
                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                    <ClientSideEvents LostFocus="updateByLostFocus" />
                                                                </dx:ASPxHtmlEditor>
                                                            </ContentTemplate>
                                                        </dx:NavBarGroup>
                                                        <%--<dx:NavBarGroup Expanded="False" Text="Tài Liệu">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="350px">
                                                                            <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                                        </dx:ASPxFileManager>
                                                                    </Template>
                                                                </dx:NavBarItem>
                                                            </Items>
                                                        </dx:NavBarGroup>--%>
                                                    </Groups>
                                                </dx:ASPxNavBar>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <%--<dx:TabPage Text="Nguyên Vật Liệu Tương Đương">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ee3SPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                                    <Items>
                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxGridView ID="grdBuyingProductCategory0" runat="server" AutoGenerateColumns="False" 
                                                                        Width="100%"
                                                                        ClientInstanceName="grdBuyingProductCategory0">
                                                                        <Columns>
                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã nguyên vật liệu" ShowInCustomizationForm="True"
                                                                                VisibleIndex="0" Width="150px" FieldName="materialcode">
                                                                                <PropertiesComboBox>
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                        <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                    </Columns>
                                                                                </PropertiesComboBox>
                                                                            </dx:GridViewDataComboBoxColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Tên  nguyên vật liệu" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1" Width="250px" FieldName="materialname">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2" Width="200px" FieldName="manufacturername">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Mô Tả" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                                FieldName="materialdescription">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                VisibleIndex="4" Width="100px">
                                                                                <EditButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                                    </Image>
                                                                                </EditButton>
                                                                                <NewButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                                    </Image>
                                                                                </NewButton>
                                                                                <DeleteButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                                    </Image>
                                                                                </DeleteButton>
                                                                                <CancelButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                    </Image>
                                                                                </CancelButton>
                                                                                <UpdateButton Visible="True">
                                                                                    <Image>
                                                                                        <SpriteProperties CssClass="Sprite_Update" />
                                                                                    </Image>
                                                                                </UpdateButton>
                                                                                <ClearFilterButton Visible="True">
                                                                                </ClearFilterButton>
                                                                            </dx:GridViewCommandColumn>
                                                                        </Columns>
                                                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                                            VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                        <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                                                                        </SettingsPager>
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
                                                        <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                        ForeColor="#CCCCCC" Text="Chọn NVL tương đương">
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
                                    </dx:TabPage>--%>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <ClientSideEvents Closing="formMaterialEdit_closing" />
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
