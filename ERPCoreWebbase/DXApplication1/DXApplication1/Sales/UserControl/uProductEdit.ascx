<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uProductEdit.ascx.cs"
    Inherits="ERPCore.Sale.UserControl.uProductEdit" %>
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
        //grdSalingProductCategory0.SetHeight($("#testheight").height() - 90);
        grdProductSupplier.SetHeight($("#testheight").height() - 120);
        grdSalingProductCategory.SetHeight($("#testheight").height() - 120);
    }
    function formMaterialEdit_AfterResizing(s, e) {
        //grdSalingProductCategory0.SetHeight($("#testheight").height() - 90);
        grdProductSupplier.SetHeight($("#testheight").height() - 120);
        grdSalingProductCategory.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    } 
</script>
<div id="lineContainerProduct">
  
                <dx:ASPxPopupControl ID="formProductEdit" runat="server" HeaderText="Thông Tin Hàng Hóa - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formProductEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
                    ScrollBars="Auto" ShowMaximizeButton="True" CloseAction="CloseButton" 
                    LoadingPanelText="FormLoad">
                    <ClientSideEvents 
                        CloseButtonClick="formProductEdit_CloseUp" 
                        CloseUp="formProductEdit_CloseUp" Shown="formProductEdit_Shown">
                    </ClientSideEvents>
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="bProductEditHelp" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="bProductEditCancel" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="bProductEditCancelbuttonCancelDevice" Text="Thoát" Wrap="False" 
                                    ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="bProductEditCancel_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="bProductEditSave" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="bProductEditSave" Text="Lưu Lại" Wrap="False" 
                                    ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="bProductEditSave_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%;" id="testheight">

                                <dx:ASPxCallbackPanel ID="cpProductEdit" runat="server" 
                                    ClientInstanceName="cpProductEdit" OnCallback="cpProductEdit_Callback" 
                                    Width="100%">
                                    <ClientSideEvents EndCallback="cpProductEdit_EndCallback" />
                                    <PanelCollection>
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxPageControl ID="pcProduct" runat="server" ActiveTabIndex="0" 
                                                ClientInstanceName="pcProduct" Height="100%" LoadingPanelText="TabLoad" 
                                                ShowLoadingPanel="False" Width="100%">
                                                <TabPages>
                                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="ASPxFormLayout1235" runat="server" Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False">
                                                                            <Items>
                                                                                <dx:LayoutItem Caption="Mã Hàng Hóa" HelpText="Tối đa 128 ký tự" 
                                                                                    RequiredMarkDisplayMode="Required">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxCallbackPanel ID="cpProductCode" runat="server" 
                                                                                                ClientInstanceName="cpProductCode" OnCallback="cpProductCode_Callback" 
                                                                                                ShowLoadingPanel="False" Width="200px">
                                                                                                <PanelCollection>
                                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxTextBox ID="txtProductCode" runat="server" 
                                                                                                            ClientInstanceName="txtProductCode" MaxLength="128" 
                                                                                                            OnValidation="txtProductCode_Validation" Width="200px">
                                                                                                            <ClientSideEvents Validation="txtProductCode_Validation" />
                                                                                                            <ValidationSettings ErrorText="">
                                                                                                                <RequiredField ErrorText="Chưa nhập mã hàng hóa" IsRequired="True" />
                                                                                                            </ValidationSettings>
                                                                                                        </dx:ASPxTextBox>
                                                                                                    </dx:PanelContent>
                                                                                                </PanelCollection>
                                                                                            </dx:ASPxCallbackPanel>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Tên Hàng Hóa" 
                                                                                    HelpText="255 ký tự, không cho phép trùng lắp" 
                                                                                    RequiredMarkDisplayMode="Required">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxTextBox ID="txtProductName" runat="server" 
                                                                                                ClientInstanceName="txtProductName" MaxLength="255" 
                                                                                                OnValidation="txtProductName_Validation" Width="400px">
                                                                                                <ValidationSettings ErrorText="">
                                                                                                    <RequiredField ErrorText="Chưa nhập tên hàng hóa" IsRequired="True" />
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxTextBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxComboBox ID="cboProductRowStatus" runat="server" 
                                                                                                ClientInstanceName="cboProductRowStatus" Width="200px">
                                                                                                <Items>
                                                                                                    <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                                                                    <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                                                                </Items>
                                                                                            </dx:ASPxComboBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Nhà Sản Xuất" HelpText="Chọn nhà sản xuất" 
                                                                                    RequiredMarkDisplayMode="Required">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxComboBox ID="cboProductManufacturer" runat="server" 
                                                                                                ClientInstanceName="cboProductManufacturer" DropDownStyle="DropDown" 
                                                                                                EnableCallbackMode="True" IncrementalFilteringMode="Contains" 
                                                                                                OnItemRequestedByValue="cboProductManufacturer_ItemRequestedByValue1" 
                                                                                                OnItemsRequestedByFilterCondition="cboProductManufacturer_ItemsRequestedByFilterCondition1" 
                                                                                                OnValidation="cboProductManufacturer_Validation" TextField="Name" 
                                                                                                TextFormatString="{1}" ValueField="Code" Width="400px">
                                                                                                <Columns>
                                                                                                    <dx:ListBoxColumn Caption="Mã nhà sản xuất" FieldName="Code" Width="150px" />
                                                                                                    <dx:ListBoxColumn Caption="Tên nhà sản xuất" FieldName="Name" Width="200px" />
                                                                                                </Columns>
                                                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                                    <RequiredField ErrorText="Chưa chọn nhà sản xuất" IsRequired="True" />
                                                                                                </ValidationSettings>
                                                                                            </dx:ASPxComboBox>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="Hình Ảnh" Visible="False">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" 
                                                                                                ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                                            </dx:ASPxImage>
                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                    </LayoutItemNestedControlCollection>
                                                                                </dx:LayoutItem>
                                                                                <dx:LayoutItem Caption="   " HelpText="Hình ảnh cho hàng hóa" 
                                                                                    ShowCaption="True" Visible="False">
                                                                                    <LayoutItemNestedControlCollection>
                                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                            SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1235_E6" runat="server" 
                                                                                                UploadMode="Auto" Width="280px">
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
                                                    <dx:TabPage Text="Trực Thuộc Nhóm">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="aaas22t2" runat="server" Height="100%" Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxGridView ID="grdSalingProductCategory" runat="server" 
                                                                                        AutoGenerateColumns="False" ClientInstanceName="grdSalingProductCategory" 
                                                                                        KeyFieldName="Code" 
                                                                                        OnCellEditorInitialize="grdSalingProductCategory_CellEditorInitialize1" 
                                                                                        OnRowDeleting="grdSalingProductCategory_RowDeleting" 
                                                                                        OnRowInserting="grdSalingProductCategory_RowInserting" 
                                                                                        OnRowUpdating="grdSalingProductCategory_RowUpdating" 
                                                                                        OnRowValidating="grdSalingProductCategory_RowValidating" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Nhóm Hàng Hóa" FieldName="Code" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                                                                                <PropertiesComboBox DropDownStyle="DropDown" 
                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0}" ValueField="Code"
                                                                                                    OnItemRequestedByValue="SalingProductCategoryItemRequestedByValue" 
                                                                                                    OnItemsRequestedByFilterCondition="SalingProductCategoryItemsRequestedByFilterCondition">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" FieldName="Code" Name="Code" 
                                                                                                            Width="150px" />
                                                                                                        <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" FieldName="Name" Name="Name" 
                                                                                                            Width="300px" />
                                                                                                        <dx:ListBoxColumn Caption="Description" FieldName="Description" 
                                                                                                            Name="Description" Width="0px" />
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Hàng Hóa" FieldName="Name" 
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="Description" 
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="3" Width="100px">
                                                                                                <EditButton Visible="True">
                                                                                                    <Image ToolTip="Sửa">
                                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <NewButton Visible="True">
                                                                                                    <Image ToolTip="Thêm">
                                                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                                                    </Image>
                                                                                                </NewButton>
                                                                                                <DeleteButton Visible="True">
                                                                                                    <Image ToolTip="Xóa">
                                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Bỏ qua">
                                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Cập nhật">
                                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <ClearFilterButton Visible="True">
                                                                                                    <Image ToolTip="Hủy">
                                                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                                                    </Image>
                                                                                                </ClearFilterButton>
                                                                                            </dx:GridViewCommandColumn>
                                                                                            <dx:GridViewDataTextColumn FieldName="ProductSalingProductCategory" 
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn FieldName="ProductId" ShowInCustomizationForm="True" 
                                                                                                Visible="False" VisibleIndex="4">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                                                                            AllowSelectSingleRowOnly="True" />
                                                                                        <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="Inline" />
                                                                                        <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                            </Header>
                                                                                            <CommandColumn Spacing="10px">
                                                                                            </CommandColumn>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm HH" 
                                                                                        ToolTip="Tạo mới 1 nhóm hàng hóa - Ctrl + N" Visible="False" Wrap="False">
                                                                                    </dx:ASPxButton>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" 
                                                                                        Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhóm Hàng Hóa">
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
                                                    <dx:TabPage Text="Nhà Cung Cấp">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="bbbas22t2" runat="server" Height="100%" Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxGridView ID="grdProductSupplier" runat="server" 
                                                                                        AutoGenerateColumns="False" ClientInstanceName="grdProductSupplier" 
                                                                                        KeyFieldName="Code" 
                                                                                        OnCellEditorInitialize="grdProductSupplier_CellEditorInitialize" 
                                                                                        OnRowDeleting="grdProductSupplier_RowDeleting" 
                                                                                        OnRowInserting="grdProductSupplier_RowInserting" 
                                                                                        OnRowUpdating="grdProductSupplier_RowUpdating" 
                                                                                        OnRowValidating="grdProductSupplier_RowValidating" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" 
                                                                                                Name="Code" ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                                                                <PropertiesComboBox DropDownStyle="DropDown" 
                                                                                                    IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{0}" 
                                                                                                    ValueField="Code"
                                                                                                    OnItemRequestedByValue="ProductSupplierItemRequestedByValue" 
                                                                                                    OnItemsRequestedByFilterCondition="ProductSupplierItemsRequestedByFilterCondition">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" />
                                                                                                        <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" />
                                                                                                        <dx:ListBoxColumn Caption="Description" FieldName="Description" 
                                                                                                            Name="Description" Width="0px" />
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="SupplierId" FieldName="SupplierId" 
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="ProductSupplierId" 
                                                                                                FieldName="ProductSupplierId" ShowInCustomizationForm="True" Visible="False" 
                                                                                                VisibleIndex="1">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
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
                                                                                                <CancelButton>
                                                                                                    <Image>
                                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <UpdateButton>
                                                                                                    <Image>
                                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <ClearFilterButton Visible="True">
                                                                                                </ClearFilterButton>
                                                                                            </dx:GridViewCommandColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" 
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" 
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                                        </SettingsPager>
                                                                                        <SettingsEditing Mode="Inline" />
                                                                                        <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                                    </dx:ASPxGridView>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới NCC" 
                                                                                        ToolTip="Tạo mới 1 NCC - Ctrl + N" Visible="False" Wrap="False">
                                                                                    </dx:ASPxButton>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" 
                                                                                        Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn Nhà Cung Cấp">
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
                                                    <dx:TabPage Text="Đơn Vị Tính">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Height="100%" 
                                                                    Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTreeList ID="grdProductUnit" runat="server" AutoGenerateColumns="False" 
                                                                                        ClientInstanceName="grdProductUnit" Height="360px" 
                                                                                        KeyFieldName="ProductProductUnitId" 
                                                                                        OnCellEditorInitialize="grdProductUnit_CellEditorInitialize" 
                                                                                        OnInit="grdProductUnit_Init" OnInitNewNode="grdProductUnit_InitNewNode" 
                                                                                        OnNodeCollapsing="grdProductUnit_NodeCollapsing" 
                                                                                        OnNodeDeleting="grdProductUnit_NodeDeleting" 
                                                                                        OnNodeInserting="grdProductUnit_NodeInserting" 
                                                                                        OnNodeUpdating="grdProductUnit_NodeUpdating" 
                                                                                        OnNodeValidating="grdProductUnit_NodeValidating" 
                                                                                        OnStartNodeEditing="grdProductUnit_StartNodeEditing" 
                                                                                        ParentFieldName="ParentProductProductUnit" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:TreeListTextColumn Caption="ProductUnitId" FieldName="ProductUnitId" 
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListTextColumn FieldName="ProductProductUnitId" 
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListTextColumn Caption="ParentProductProductUnit" 
                                                                                                FieldName="ParentProductProductUnit" ShowInCustomizationForm="True" 
                                                                                                Visible="False" VisibleIndex="2">
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListTextColumn FieldName="ProductUnitPropertyId" 
                                                                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListComboBoxColumn Caption="Mã Đơn Vị Tính" FieldName="Code" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="150px">
                                                                                                <PropertiesComboBox DropDownStyle="DropDown" 
                                                                                                    IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{0}" 
                                                                                                    ValueField="Code"
                                                                                                    OnItemRequestedByValue="ProductUnitItemRequestedByValue" 
                                                                                                    OnItemsRequestedByFilterCondition="ProductUnitItemsRequestedByFilterCondition">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Đơn Vị Tính" FieldName="Code" Name="Code" 
                                                                                                            Width="150px" />
                                                                                                        <dx:ListBoxColumn Caption="Tên Đơn Vị Tính" FieldName="Name" Name="Name" 
                                                                                                            Width="200px" />
                                                                                                        <dx:ListBoxColumn FieldName="Description" Name="Description" Width="0px" />
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:TreeListComboBoxColumn>
                                                                                            <dx:TreeListTextColumn Caption="Tên Đơn Vị Tính" FieldName="Name" 
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="250px">
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListTextColumn Caption="Bao Gồm" FieldName="NumRequired" 
                                                                                                Name="NumRequired" ShowInCustomizationForm="True" VisibleIndex="6" 
                                                                                                Width="100px">
                                                                                                <EditCellTemplate>
                                                                                                    <dx:ASPxSpinEdit ID="colNumRequired" runat="server" 
                                                                                                        ClientInstanceName="colNumRequired" Height="21px" MaxValue="999999999" 
                                                                                                        MinValue="1" Number="0" oninit="colNumRequired_Init" Width="60px">
                                                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True">
                                                                                                        </ValidationSettings>
                                                                                                    </dx:ASPxSpinEdit>
                                                                                                </EditCellTemplate>
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="Description" 
                                                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="7" Width="200px">
                                                                                            </dx:TreeListTextColumn>
                                                                                            <dx:TreeListCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                                                                ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="7" 
                                                                                                Width="100px">
                                                                                                <EditButton Visible="True">
                                                                                                    <Image ToolTip="Sửa">
                                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <NewButton Visible="True">
                                                                                                    <Image ToolTip="Thêm">
                                                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                                                    </Image>
                                                                                                </NewButton>
                                                                                                <DeleteButton Visible="True">
                                                                                                    <Image ToolTip="Xóa">
                                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Cập nhật">
                                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Bỏ qua">
                                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                            </dx:TreeListCommandColumn>
                                                                                        </Columns>
                                                                                        <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="360" 
                                                                                            VerticalScrollBarMode="Auto" />
                                                                                        <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" 
                                                                                            ExpandCollapseAction="NodeDblClick" />
                                                                                        <SettingsEditing AllowRecursiveDelete="True" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                            </Header>
                                                                                            <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                                                            </CommandButton>
                                                                                        </Styles>
                                                                                    </dx:ASPxTreeList>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới ĐVT" 
                                                                                        ToolTip="Tạo mới 1 ĐVT - Ctrl + N" Visible="False" Wrap="False">
                                                                                    </dx:ASPxButton>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" 
                                                                                        Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn đơn vị tính">
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
                                                    <dx:TabPage Text="Thông Tin Chi Tiết">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="ffdsds" runat="server" Height="100%" Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxNavBar ID="nbProduct" runat="server" AutoCollapse="True" 
                                                                                        ClientInstanceName="nbProduct" Height="100%" Width="100%">
                                                                                        <Groups>
                                                                                            <dx:NavBarGroup Text="Mô Tả">
                                                                                                <Items>
                                                                                                    <dx:NavBarItem>
                                                                                                        <Template>
                                                                                                            <dx:ASPxHtmlEditor ID="htmlDescription" runat="server" 
                                                                                                                ClientInstanceName="htmlDescription" Height="350px" Width="100%">
                                                                                                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                                                            </dx:ASPxHtmlEditor>
                                                                                                        </Template>
                                                                                                    </dx:NavBarItem>
                                                                                                </Items>
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
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="True" 
                                                                                        Font-Size="X-Small" ForeColor="#CCCCCC" Text="Nhập mô tả cho hàng hóa">
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
                                                    <dx:TabPage Text="Thông Tin Khác">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="fgggsds" runat="server" Height="100%" Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxNavBar ID="nbOtherInfo" runat="server" ClientInstanceName="nbOtherInfo" 
                                                                                        RenderMode="Lightweight" Width="100%">
                                                                                        <Groups>
                                                                                            <dx:NavBarGroup Text="Dược Liệu, Hoạt Chất">
                                                                                                <Items>
                                                                                                    <dx:NavBarItem>
                                                                                                        <Template>
                                                                                                            <dx:ASPxGridView ID="grdActiveElement" runat="server" 
                                                                                                                AutoGenerateColumns="False" ClientInstanceName="grdActiveElement" 
                                                                                                                KeyFieldName="Code" 
                                                                                                                oncelleditorinitialize="grdActiveElement_CellEditorInitialize" 
                                                                                                                onrowdeleting="grdActiveElement_RowDeleting" 
                                                                                                                onrowinserting="grdActiveElement_RowInserting" 
                                                                                                                onrowupdating="grdActiveElement_RowUpdating" 
                                                                                                                onrowvalidating="grdActiveElement_RowValidating" Width="100%">
                                                                                                                <Columns>
                                                                                                                    <dx:GridViewDataComboBoxColumn Caption="Tên Dược Liệu, Hoạt Chất" 
                                                                                                                        FieldName="Name" ReadOnly="True" VisibleIndex="1">
                                                                                                                        <PropertiesComboBox>
                                                                                                                            <Columns>
                                                                                                                                <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                                                <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                                            </Columns>
                                                                                                                        </PropertiesComboBox>
                                                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Thành Phần" FieldName="Component" 
                                                                                                                        ReadOnly="True" VisibleIndex="2" Width="200px">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Chức Năng" FieldName="ActiveFunction" 
                                                                                                                        ReadOnly="True" VisibleIndex="3" Width="200px">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Description" 
                                                                                                                        ReadOnly="True" VisibleIndex="4" Width="200px">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                                        VisibleIndex="5" Width="100px">
                                                                                                                        <EditButton Visible="True">
                                                                                                                            <Image ToolTip="Sửa">
                                                                                                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                                                                                            </Image>
                                                                                                                        </EditButton>
                                                                                                                        <NewButton Visible="True">
                                                                                                                            <Image ToolTip="Thêm">
                                                                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                                                                            </Image>
                                                                                                                        </NewButton>
                                                                                                                        <DeleteButton Visible="True">
                                                                                                                            <Image ToolTip="Xóa">
                                                                                                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                                            </Image>
                                                                                                                        </DeleteButton>
                                                                                                                        <ClearFilterButton Visible="True">
                                                                                                                            <Image ToolTip="Hủy">
                                                                                                                                <SpriteProperties CssClass="Sprite_Clear" />
                                                                                                                            </Image>
                                                                                                                        </ClearFilterButton>
                                                                                                                        <UpdateButton>
                                                                                                                            <Image ToolTip="Cập nhật">
                                                                                                                                <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                                            </Image>
                                                                                                                        </UpdateButton>
                                                                                                                        <CancelButton>
                                                                                                                            <Image ToolTip="Bỏ qua">
                                                                                                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                                            </Image>
                                                                                                                        </CancelButton>
                                                                                                                    </dx:GridViewCommandColumn>
                                                                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã Dược Liệu, Hoạt Chất" 
                                                                                                                        FieldName="Code" VisibleIndex="0">
                                                                                                                        <PropertiesComboBox DropDownStyle="DropDown" 
                                                                                                                            IncrementalFilteringMode="Contains" 
                                                                                                                            OnItemRequestedByValue="ActiveItemRequestedByValue" 
                                                                                                                            OnItemsRequestedByFilterCondition="ActiveItemsRequestedByFilterCondition" 
                                                                                                                            TextField="Code" TextFormatString="{0}" ValueField="Code">
                                                                                                                            <Columns>
                                                                                                                                <dx:ListBoxColumn Caption="Mã Dược Liệu Hoạt Chất" FieldName="Code" Name="Code" 
                                                                                                                                    Width="150px" />
                                                                                                                                <dx:ListBoxColumn Caption="Tên Dược Liệu, Hoạt Chất" FieldName="Name" 
                                                                                                                                    Name="Name" Width="200px" />
                                                                                                                                <dx:ListBoxColumn FieldName="Component" Name="Component" Width="0px" />
                                                                                                                                <dx:ListBoxColumn FieldName="Description" Name="Description" Width="0px" />
                                                                                                                                <dx:ListBoxColumn Caption="ActiveFunction" FieldName="ActiveFunction" 
                                                                                                                                    Name="ActiveFunction" Width="0px" />
                                                                                                                            </Columns>
                                                                                                                        </PropertiesComboBox>
                                                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                                                </Columns>
                                                                                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                                                                                                    AllowSelectSingleRowOnly="True" />
                                                                                                                <SettingsPager PageSize="30">
                                                                                                                </SettingsPager>
                                                                                                                <SettingsEditing Mode="Inline" />
                                                                                                                <Styles>
                                                                                                                    <CommandColumn Spacing="10px">
                                                                                                                    </CommandColumn>
                                                                                                                </Styles>
                                                                                                            </dx:ASPxGridView>
                                                                                                        </Template>
                                                                                                    </dx:NavBarItem>
                                                                                                </Items>
                                                                                            </dx:NavBarGroup>
                                                                                            <dx:NavBarGroup Text="Tiêu Chuẩn Lưu Trữ" Visible="False">
                                                                                                <Items>
                                                                                                    <dx:NavBarItem>
                                                                                                        <Template>
                                                                                                            <dx:ASPxGridView ID="grdSalingProductCategory0" runat="server" 
                                                                                                                AutoGenerateColumns="False" Width="100%">
                                                                                                                <Columns>
                                                                                                                    <dx:GridViewDataComboBoxColumn Caption="Tiêu Chuẩn Lưu Trữ" 
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                                                                                                        <PropertiesComboBox>
                                                                                                                            <Columns>
                                                                                                                                <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                                                <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                                            </Columns>
                                                                                                                        </PropertiesComboBox>
                                                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Thấp Nhất" 
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Tiêu Chuẩn Cao Nhất" 
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="200px">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewDataTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" 
                                                                                                                        VisibleIndex="3">
                                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                                                                        <EditButton Visible="True">
                                                                                                                            <Image ToolTip="Sửa">
                                                                                                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                                                                                            </Image>
                                                                                                                        </EditButton>
                                                                                                                        <NewButton Visible="True">
                                                                                                                            <Image ToolTip="Thêm">
                                                                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                                                                            </Image>
                                                                                                                        </NewButton>
                                                                                                                        <DeleteButton Visible="True">
                                                                                                                            <Image ToolTip="Xóa">
                                                                                                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                                            </Image>
                                                                                                                        </DeleteButton>
                                                                                                                        <ClearFilterButton Visible="True">
                                                                                                                            <Image ToolTip="Hủy">
                                                                                                                                <SpriteProperties CssClass="Sprite_Clear" />
                                                                                                                            </Image>
                                                                                                                        </ClearFilterButton>
                                                                                                                        <UpdateButton>
                                                                                                                            <Image ToolTip="Cập nhật">
                                                                                                                                <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                                            </Image>
                                                                                                                        </UpdateButton>
                                                                                                                        <CancelButton>
                                                                                                                            <Image ToolTip="Bỏ qua">
                                                                                                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                                            </Image>
                                                                                                                        </CancelButton>
                                                                                                                    </dx:GridViewCommandColumn>
                                                                                                                </Columns>
                                                                                                                <SettingsPager PageSize="30">
                                                                                                                </SettingsPager>
                                                                                                                <Styles>
                                                                                                                    <CommandColumn Spacing="10px">
                                                                                                                    </CommandColumn>
                                                                                                                </Styles>
                                                                                                            </dx:ASPxGridView>
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
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Italic="True" 
                                                                                        Font-Size="X-Small" ForeColor="#CCCCCC" Text="Cập nhật thông itn khác">
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
                                                    <dx:TabPage Text="Hàng Hóa Tương Đương" Visible="False">
                                                        <ContentCollection>
                                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxFormLayout ID="ooogggsds" runat="server" Height="100%" Width="100%">
                                                                    <Items>
                                                                        <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxGridView ID="grdSalingProductCategory0" runat="server" 
                                                                                        AutoGenerateColumns="False" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Hàng Hóa" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                                                                                <PropertiesComboBox>
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                                                                        <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                                                                                    </Columns>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên  Hàng Hóa" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="200px">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Mô Tả" ShowInCustomizationForm="True" 
                                                                                                VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                                                ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                                                                <EditButton Visible="True">
                                                                                                    <Image ToolTip="Sửa">
                                                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                                                    </Image>
                                                                                                </EditButton>
                                                                                                <NewButton Visible="True">
                                                                                                    <Image ToolTip="Thêm">
                                                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                                                    </Image>
                                                                                                </NewButton>
                                                                                                <DeleteButton Visible="True">
                                                                                                    <Image ToolTip="Xóa">
                                                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                                                    </Image>
                                                                                                </DeleteButton>
                                                                                                <CancelButton>
                                                                                                    <Image ToolTip="Bỏ qua">
                                                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                                    </Image>
                                                                                                </CancelButton>
                                                                                                <UpdateButton>
                                                                                                    <Image ToolTip="Cập nhật">
                                                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                                                    </Image>
                                                                                                </UpdateButton>
                                                                                                <ClearFilterButton Visible="True">
                                                                                                    <Image ToolTip="Hủy">
                                                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                                                    </Image>
                                                                                                </ClearFilterButton>
                                                                                            </dx:GridViewCommandColumn>
                                                                                        </Columns>
                                                                                        <SettingsPager PageSize="30" RenderMode="Classic" ShowEmptyDataRows="True">
                                                                                        </SettingsPager>
                                                                                        <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center">
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
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel ID="ASPxFormLayout2_E221" runat="server" Font-Italic="True" 
                                                                                        Font-Size="X-Small" ForeColor="#CCCCCC" Text="Chọn hàng hóa tương đương">
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
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
       
</div>

<dx:ASPxHiddenField ID="hProductEditId" runat="server" 
    ClientInstanceName="hProductEditId">
</dx:ASPxHiddenField>


<dx:XpoDataSource ID="ProductSalingProductCategoryXDS" runat="server" 
    TypeName="DAL.Purchasing.ViewProductProductUnit">
</dx:XpoDataSource>



