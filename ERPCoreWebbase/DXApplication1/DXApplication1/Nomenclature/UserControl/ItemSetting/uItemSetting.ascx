<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uItemSetting.ascx.cs" Inherits="WebModule.Nomenclature.UserControl.ItemSetting.uItemSetting" %>
<dx:ASPxCallbackPanel ID="cpItemEdit" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    ClientInstanceName="cpItemEdit" Width="100%"  Maximized="True" 
    oncallback="cpItemEdit_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <div id="lineContainerProduct">
                <dx:ASPxPopupControl ID="formProductEdit" ClientInstanceName="formProductEdit" runat="server"
                    CssClass="KeyShortcutformProductEdit" HeaderText="Thông Tin Đối Tượng - " Height="620px"
                    Modal="True" Width="820px" AllowDragging="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True" CloseAction="CloseButton" RenderMode="Classic"
                    LoadingPanelText="FormLoad" Maximized="True">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="bProductEditHelp" CausesValidation="false" AutoPostBack="false"
                                    runat="server" CssClass="float_left dl mg" Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnCancelItem" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnCancelItem" Text="Thoát" Wrap="False" ToolTip="Thoát  - ESC">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnSaveItem" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnSaveItem" Text="Lưu lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + Enter">
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
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%;" id="testheight">
                                <dx:ASPxPageControl ID="pcProduct" runat="server" ActiveTabIndex="0" ClientInstanceName="pcProduct"
                                    Height="100%" LoadingPanelText="TabLoad" ShowLoadingPanel="False" 
                                    Width="100%" >
                                    <TabPages>
                                        <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="frmlyItemEdit" runat="server" Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã" HelpText="Tối đa 36 ký tự, không cho phép trùng lắp."
                                                                        RequiredMarkDisplayMode="Required">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxCallbackPanel ID="cpProductCode" runat="server" ClientInstanceName="cpProductCode"
                                                                                    ShowLoadingPanel="False" Width="200px">
                                                                                    <PanelCollection>
                                                                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                            <dx:ASPxTextBox ID="txtProductCode" runat="server" ClientInstanceName="txtProductCode"
                                                                                                MaxLength="36" Width="200px">
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
                                                                    <dx:LayoutItem Caption="Tên" FieldName="Name" HelpText="Tối đa 255 ký tự." RequiredMarkDisplayMode="Required">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="txtProductName" runat="server" ClientInstanceName="txtProductName"
                                                                                    MaxLength="255" Width="400px">
                                                                                    <ValidationSettings ErrorText="">
                                                                                        <RequiredField ErrorText="Chưa nhập tên hàng hóa" IsRequired="True" />
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
                                                                                    DropDownStyle="DropDown" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                    TextFormatString="{1}" TextField="Name" ValueField="OrganizationId" Width="400px"
                                                                                    DataSourceID="ManufactuerCboXDS">
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn Caption="OrganizationId" FieldName="OrganizationId" Visible="false" />
                                                                                        <dx:ListBoxColumn Caption="Mã nhà sản xuất" FieldName="Code" Width="150px" />
                                                                                        <dx:ListBoxColumn Caption="Tên nhà sản xuất" FieldName="Name" Width="200px" />
                                                                                        <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" Width="200px" />
                                                                                    </Columns>
                                                                                </dx:ASPxComboBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Loại">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxListBox ID="lbType" runat="server" SelectionMode="CheckColumn" Width="100%"
                                                                                    ClientInstanceName="lbType" ValueField="ObjectTypeId" Rows="6" Height="160px"
                                                                                    ShowLoadingPanel="false" DataSourceID="ObjectTypeLbXDS"
                                                                                    ValidationSettings-CausesValidation="true">
                                                                                    <Columns>
                                                                                        <dx:ListBoxColumn FieldName="ObjectTypeId" Caption="ObjectTypeId" Width="100%" Visible="false" />
                                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên loại" Width="100px" Visible="false" />
                                                                                        <dx:ListBoxColumn FieldName="Description" Caption="Mô tả" Width="100%" />
                                                                                    </Columns>
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
                                        <%--<dx:TabPage Name="tabConfigCustomField" Text="Cấu hình động">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
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
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>--%>
                                        <dx:TabPage Text="Nhà Cung Cấp">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="frmSupplierList" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxGridView ID="grdProductSupplier" runat="server" KeyboardSupport="true" AutoGenerateColumns="False"
                                                                            ClientInstanceName="grdProductSupplier" KeyFieldName="ItemSupplierId" Width="100%" DataSourceID="SupplierListXDS">
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã nhà cung cấp" Name="Supplier" FieldName="SupplierOrgId!Key"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                                                    <PropertiesComboBox ClientInstanceName="cboSupplierColumn" DropDownStyle="DropDownList"
                                                                                        IncrementalFilteringMode="Contains" TextField="Code" ValueField="OrganizationId"
                                                                                        CallbackPageSize="10" EnableCallbackMode="true" TextFormatString="{0}" DropDownRows="10">
                                                                                        <Columns>
                                                                                            <dx:ListBoxColumn Caption="OrganizationId" FieldName="OrganizationId" Visible="false" />
                                                                                            <dx:ListBoxColumn Caption="Mã nhà cung cấp" FieldName="Code" Width="150px" />
                                                                                            <dx:ListBoxColumn Caption="Tên nhà cung cấp" FieldName="Name" Width="350px" />
                                                                                            <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                                                                                        </Columns>
                                                                                    </PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên nhà cung cấp" FieldName="SupplierName" ReadOnly="True"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="SupplierDescription" ReadOnly="True"
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Link" Caption="Thao Tác" Name="Action" ShowInCustomizationForm="True"
                                                                                    VisibleIndex="5" Width="100px">
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
                                                                            </Columns>
                                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                                            </SettingsPager>
                                                                            <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="true" />
                                                                            <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa?" EmptyDataRow="Chưa có nhà cung cấp" />
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                                            <SettingsEditing Mode="Inline"></SettingsEditing>
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
                                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTreeList ID="treelstProductUnits" runat="server" AutoGenerateColumns="False"
                                                        KeyboardSupport="true" ClientInstanceName="treelstProductUnits" Height="360px"
                                                        KeyFieldName="ItemUnitId" ParentFieldName="ParentItemUnitId!Key" Width="100%"
                                                        DataSourceID="ItemUnitTreeXDS" 
                                                        AccessKey="G">
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
                                                                </PropertiesComboBox>
                                                            </dx:TreeListComboBoxColumn>
                                                            <dx:TreeListTextColumn Caption="Tên đơn vị tính" FieldName="UnitId.Name" Name="Name"
                                                                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListSpinEditColumn Caption="Bao Gồm" FieldName="NumRequired" Name="NumRequired"
                                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
                                                                <PropertiesSpinEdit DisplayFormatString="g">
                                                                </PropertiesSpinEdit>
                                                            </dx:TreeListSpinEditColumn>
                                                            <dx:TreeListTextColumn Caption="Diễn Giải" Name="Description" ShowInCustomizationForm="True"
                                                                VisibleIndex="7" Visible="true" Width="300px">
                                                                <CellStyle Wrap="True">
                                                                </CellStyle>
                                                                <EditCellTemplate>
                                                                </EditCellTemplate>
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListCommandColumn ButtonType="Link" Name="Action" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                ShowNewButtonInHeader="True" VisibleIndex="7" Width="100px">
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
                                                            <dx:TreeListTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" Visible="False">
                                                            </dx:TreeListTextColumn>
                                                        </Columns>
                                                        <Settings HorizontalScrollBarMode="Auto" ScrollableHeight="360" VerticalScrollBarMode="Auto"
                                                            GridLines="Both" ShowTreeLines="False" />
                                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                                                        <SettingsEditing AllowRecursiveDelete="True" AllowNodeDragDrop="true" />
                                                        <Settings GridLines="Both" HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"
                                                            ScrollableHeight="360"></Settings>
                                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick"></SettingsBehavior>
                                                        <SettingsEditing AllowNodeDragDrop="True" AllowRecursiveDelete="True"></SettingsEditing>
                                                        <Styles>
                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                            </Header>
                                                            <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                            </CommandButton>
                                                        </Styles>
                                                    </dx:ASPxTreeList>
                                                    <dx:ASPxFormLayout ID="frmItemUnit" runat="server" Height="100%" Width="100%">
                                                        <Items>
                                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Thêm mới ĐVT" ToolTip="Tạo mới 1 ĐVT - Ctrl + N"
                                                                            Visible="False" Wrap="False">
                                                                        </dx:ASPxButton>
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
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
                                                        <SettingsItems VerticalAlign="Middle"></SettingsItems>
                                                    </dx:ASPxFormLayout>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <%--DND 1094--%>
                                        <dx:TabPage Text="Thuế Suất">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl5" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridTax" runat="server" AutoGenerateColumns="true" DataSourceID="ItemTaxXDS"
                                                        KeyFieldName="ItemTaxId" Width="100%" ClientInstanceName="ASPxGridTax">
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Phân Loại" Name="txt_TaxId" FieldName="TaxId!Key" ShowInCustomizationForm="true"
                                                                VisibleIndex="0" Width="20%" >
                                                                <PropertiesComboBox DataSourceID="TaxXDS" ValueField="TaxId" TextField="Code" TextFormatString="{0}" 
                                                                 IncrementalFilteringMode="Contains">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn FieldName="Code" Caption="Mã Phân Loại"/>
                                                                        <dx:ListBoxColumn FieldName="Name" Caption="Tên Phân Loại"/>
                                                                        <dx:ListBoxColumn FieldName="Percentage" Caption="Tỉ Lệ"/>
                                                                    </Columns>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên Phân Loại" 
                                                                FieldName="TaxId.Name" Name="Name"
                                                                ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tỷ Lệ Phần Trăm(%)" 
                                                                FieldName="TaxId.Percentage" Name="Percentage"
                                                                ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                                                <PropertiesTextEdit>
                                                                    <Style HorizontalAlign="Right">
                                                                    </Style>
                                                                </PropertiesTextEdit>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn Caption="Thao Tác" ButtonType="Link" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Width="10%">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                    </Image>
                                                                </DeleteButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                    </Image>
                                                                </CancelButton>
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsBehavior ConfirmDelete="true" AllowFocusedRow="true" />
                                                        <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa?" />
                                                    </dx:ASPxGridView>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <%--END DND 1094--%>
                                        <dx:TabPage Text="Thông Tin Chi Tiết">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
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
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:XpoDataSource ID="ManufactuerCboXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.ManufacturerOrg"
    Criteria="[RowStatus] &gt; 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="ObjectTypeLbXDS" runat="server" TypeName="NAS.DAL.CMS.ObjectDocument.ObjectType"
    Criteria="">
</dx:XpoDataSource>
<dx:XpoDataSource ID="ItemUnitTreeXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Item.ItemUnit"
    
    Criteria="[ItemId!Key] = ? And [RowStatus] &gt; 0 And [UnitId.RowStatus] &gt; 0 And [ItemId] Is Not Null And [UnitId] Is Not Null">
    <CriteriaParameters>
        <asp:SessionParameter Name="ItemId" SessionField="ItemId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="SupplierListXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Item.ItemSupplier"
    Criteria="[ItemId!Key] = ?">
    <CriteriaParameters>
        <asp:SessionParameter Name="ItemId" SessionField="ItemId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:XpoDataSource ID="UnitCboXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Item.Unit"
    Criteria="[RowStatus] &gt; 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="TaxXDS" runat="server" 
    TypeName="NAS.DAL.Accounting.Tax.Tax" DefaultSorting="" 
    Criteria="[RowStatus] &gt; 0 And [TaxTypeId.RowStatus] &gt; 0">
</dx:XpoDataSource>
<dx:XpoDataSource ID="ItemTaxXDS" runat="server" 
    TypeName="NAS.DAL.Nomenclature.Item.ItemTax" DefaultSorting="" 
    Criteria="[ItemId!Key] = ? And [RowStatus] &gt; 0">
    <CriteriaParameters>
        <asp:SessionParameter Name="ItemId" 
            SessionField="ItemId" />
    </CriteriaParameters>
</dx:XpoDataSource>
