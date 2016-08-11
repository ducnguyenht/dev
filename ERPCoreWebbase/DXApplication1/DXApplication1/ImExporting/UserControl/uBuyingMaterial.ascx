<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingMaterial.ascx.cs"
    Inherits="DXApplication1.ImExporting.UserControl.uBuyingMaterial" %>
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

</script>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLineMaterial" runat="server" Width="100%" ClientInstanceName="cpLineMaterial"
        OnCallback="cpLineMaterial_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formMaterialEdit" runat="server" HeaderText="Thông Tin Nguyên Vật Liệu -"
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formMaterialEdit"
                    AllowDragging="True" RenderMode="Classic" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True">
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
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
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
                                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" Height="100%"
                                    Width="100%" EnableTabScrolling="True" RenderMode="Classic">
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
                                                        EnableTheming="True" Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã Nguyên Vật Liệu" HelpText="Tối đa 128 ký tự">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="txtCodeDevice" runat="server" ClientInstanceName="txtCodeDevice"
                                                                                    Width="200px">
                                                                                    <NullTextStyle ForeColor="Silver">
                                                                                    </NullTextStyle>
                                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                        <RequiredField ErrorText="Chưa nhập mã công cụ dụng cụ" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                                        </CaptionCellStyle>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Tên  Nguyên Vật Liệu" HelpText="255 ký tự">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="txtNameDevice" runat="server" ClientInstanceName="txtNameDevice"
                                                                                    MaxLength="255" NullText="255 ký tự" Width="400px">
                                                                                    <NullTextStyle ForeColor="Silver">
                                                                                    </NullTextStyle>
                                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                        <RequiredField ErrorText="Chưa nhập tên công cụ dụng cụ" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="cboRowStatusDevice" runat="server" ClientInstanceName="cboRowStatusDevice"
                                                                                    Width="200px">
                                                                                    <Items>
                                                                                        <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                                        <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                                                    </Items>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Nhà Sản Xuất" HelpText="Chọn Nhà Sản Xuất">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxComboBox ID="cboManufacturer" runat="server" CallbackPageSize="20" ClientInstanceName="cboManufacturer"
                                                                                    DropDownHeight="200px" DropDownWidth="450px" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                    TextField="Name" TextFormatString="{1};{0}" ValueField="Code" Width="400px">
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn Caption="Mã Nhà Sản Xuất" FieldName="Code" Name="Code" Width="150px" />
                                                                                        <dx:ListBoxColumn Caption="Tên Nhà Sản Xuất" FieldName="Name" Name="Name" Width="300px" />
                                                                                    </Columns>
                                                                                    <ValidationSettings SetFocusOnError="True">
                                                                                        <RequiredField ErrorText="Chưa chọn nhà sản xuất !" IsRequired="True" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                                        </CaptionCellStyle>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Hình Ảnh">
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
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPxFsd22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdMaterialCategory" runat="server" AutoGenerateColumns="False"
                                                                            OnCellEditorInitialize="grdBuyingProductCategory_CellEditorInitialize" Width="100%"
                                                                            KeyFieldName="code" ClientInstanceName="grdMaterialCategory">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã nhóm nguyên vật liệu" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="0" Width="150px" FieldName="code">
                                                                                    <PropertiesComboBox>
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="Mã nhóm nguyên vật liệu" Width="150px" />
                                                                                            <dx:ListBoxColumn Caption="Tên nhóm nguyên vật liệu" Width="300px" />
                                                                                        </Columns>
                                                                                    </PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên nhóm nguyên vật liệu" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="1" Width="250px" FieldName="name">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                                    FieldName="note">
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
                                                                                        <Image Height="25px" Url="~/images/cancel.png" Width="25px">
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
                                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                                                VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
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
                                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxFormLayout2_E22" runat="server" Text="Thêm mới nhóm NVL" ToolTip="Tạo mới 1 nhóm NVL - Ctrl + N"
                                                                            Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn Nhóm Nguyên Vật Liệu">
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
                                                    <dx:ASPxFormLayout ID="ASPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdProductSupplier" runat="server" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdProductSupplier" OnCellEditorInitialize="grdProductSupplier_CellEditorInitialize"
                                                                            OnRowDeleting="grdProductSupplier_RowDeleting" OnRowUpdating="grdProductSupplier_RowUpdating"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="suppliercode"
                                                                                    Name="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
                                                                                    <PropertiesComboBox CallbackPageSize="20" DropDownStyle="DropDown" EnableCallbackMode="True"
                                                                                        IncrementalFilteringMode="StartsWith" TextField="Code" TextFormatString="{0} {1}"
                                                                                        ValueField="Code" Width="400px">
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" Width="150px" />
                                                                                            <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" Width="300px" />
                                                                                        </Columns>
                                                                                    </PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="suppliername" Name="SupplierName"
                                                                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Width="250px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="description" Name="Description"
                                                                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="5" Width="100px">
                                                                                    <EditButton Visible="True">
                                                                                        <Image SpriteProperties-CssClass="Sprite_Edit">
                                                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image SpriteProperties-CssClass="Sprite_New">
                                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image SpriteProperties-CssClass="Sprite_Delete">
                                                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image SpriteProperties-CssClass="Sprite_Cancel">
                                                                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton Visible="True">
                                                                                        <Image SpriteProperties-CssClass="Sprite_Update">
                                                                                            <SpriteProperties CssClass="Sprite_Update"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <ClearFilterButton Visible="True">
                                                                                    </ClearFilterButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn Caption="ProductSupplierId" FieldName="ProductSupplierId"
                                                                                    Name="ProductSupplierId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
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
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới NCC" ToolTip="Tạo mới 1 CCDC - Ctrl + N"
                                                                            Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn Nhà Cung Cấp">
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
                                                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ssSPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                            Height="100%">
                                                                            <Columns>
                                                                                <dx:TreeListTextColumn Caption="Mã Đơn Vị Tính" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                                    Width="10%">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="Tên Đơn Vị Tính" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                                    Width="20%">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="Thành Phần" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                                    Width="10%">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                                    Width="20%">
                                                                                </dx:TreeListTextColumn>
                                                                                <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                                    Width="10%" Caption="Thao Tác">
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
                                                                                            <SpriteProperties CssClass="Sprite_Update" />
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                    <CancelButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                </dx:TreeListCommandColumn>
                                                                            </Columns>
                                                                            <Styles>
                                                                                <Header HorizontalAlign="Center" Font-Bold="true">
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
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Thêm mới ĐVT" ToolTip="Tạo mới 1 ĐVT - Ctrl + N"
                                                                            Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Chọn Đơn Vị Tính">
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
                                        <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" AutoCollapse="True" Height="100%"
                                                                            Width="100%">
                                                                            <Groups>
                                                                                <dx:NavBarGroup Text="Mô Tả">
                                                                                    <Items>
                                                                                        <dx:NavBarItem>
                                                                                            <Template>
                                                                                                <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="350px" Width="100%">
                                                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                                                </dx:ASPxHtmlEditor>
                                                                                            </Template>
                                                                                        </dx:NavBarItem>
                                                                                    </Items>
                                                                                </dx:NavBarGroup>
                                                                                <dx:NavBarGroup Expanded="False" Text="Tài Liệu">
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
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                            ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho NVL">
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
                                        <dx:TabPage Text="Nguyên Vật Liệu Tương Đương">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl6" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="ee3SPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdBuyingProductCategory0" runat="server" AutoGenerateColumns="False"
                                                                            OnCellEditorInitialize="grdBuyingProductCategory_CellEditorInitialize" Width="100%"
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
