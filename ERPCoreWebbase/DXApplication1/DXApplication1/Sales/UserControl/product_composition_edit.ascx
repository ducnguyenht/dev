<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="product_composition_edit.ascx.cs" Inherits="DXApplication1.GUI.usercontrol.product_composition_edit" %>
<style type="text/css">
    .style25
    {
        width: 609px;
    }
    </style>
<dx:ASPxPopupControl ID="popup_edit" runat="server" Width="900px" Height="600px" ScrollBars="Auto"
    RenderMode="Classic" HeaderText="Thông Tin Cấu Thành Sản Phẩm" ClientInstanceName="popup_editproduct" 
    AllowResize="True" AllowDragging="true" ShowFooter="true"
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server"  
                    RenderMode="Classic" ActiveTabIndex="0" Width="100%" Height="100%" 
                    EnableTabScrolling="True">  
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                                        <Items>
                                            <dx:LayoutItem Caption="Mã Sản Phẩm">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                            <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                <RequiredField ErrorText="Chưa nhập mã sản phẩm" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tên Sản Phẩm">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                                            <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                <RequiredField ErrorText="Chưa nhập tên sản phẩm" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nhà Sản Xuất">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                                                            <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                <RequiredField ErrorText="Chưa nhập nhà sản xuất" IsRequired="True" />
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Mô Tả">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" Height="100px" 
                                                            MaxLength="1000" Width="400px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Hình Ảnh">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxImage ID="ASPxImage1" runat="server" Height="200px" 
                                                            ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                        </dx:ASPxImage>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                                <CaptionCellStyle CssClass="CaptionStyle">
                                                </CaptionCellStyle>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption=" ">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" UploadMode="Auto" 
                                                            Width="300px">
                                                        </dx:ASPxUploadControl>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                    <div class="quickHelp">
                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Italic="False" ForeColor="Gray"                                                        
                                            Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp" 
                                                Font-Bold="False" Font-Size="XX-Small">
                                        </dx:ASPxLabel>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Trực Thuộc Nhóm">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="grid_productgrp" runat="server" 
                                        AutoGenerateColumns="False" KeyFieldName="productgrp_id" Settings-VerticalScrollBarMode="Auto" Settings-HorizontalScrollBarMode="Auto"
                                        Width="100%">
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Mã số" FieldName="productgrp_id"
                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="DT0001" Value="DT0001" />
                                                        <dx:ListEditItem Text="DT0002" Value="DT0002" />
                                                        <dx:ListEditItem Text="DT0003" Value="DT0003" />
                                                        <dx:ListEditItem Text="DT0004" Value="DT0004" />
                                                        <dx:ListEditItem Text="DT0005" Value="DT0005" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên nhóm sản phẩm"  FieldName="productgrp_name"
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="250px">
                                                <EditItemTemplate>
                                                    <%# Eval("productgrp_name")%>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô tả" ShowInCustomizationForm="True" FieldName="description"
                                                VisibleIndex="2" Width="100%">
                                                <EditItemTemplate>
                                                    <%# Eval("description")%>
                                                </EditItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" FieldName="note"
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
                                        <SettingsEditing Mode="Inline" />
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsBehavior ColumnResizeMode="NextColumn" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                         <Styles>
                                            <Header HorizontalAlign="Center" Font-Bold="true">
                                            </Header>             
                                            <CommandColumn Spacing="10px">
                                            </CommandColumn>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <dx:ASPxButton ID="ASPxButton4" runat="server" Font-Bold="True" 
                                        Text="Thêm nhóm sản phẩm" Width="191px">
                                    </dx:ASPxButton>
                        
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Nhà Cung Cấp">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="grid_manufacturer" runat="server" 
                                        AutoGenerateColumns="False" Width="100%" 
                                        Settings-VerticalScrollBarMode="Auto" Settings-HorizontalScrollBarMode="Auto">
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Mã nhà cung cấp" FieldName="supplierid"
                                                Name="SupplierCode" ShowInCustomizationForm="True" 
                                                VisibleIndex="1" Width="150px">
                                                <PropertiesComboBox CallbackPageSize="20" DropDownStyle="DropDown" 
                                                    EnableCallbackMode="True" IncrementalFilteringMode="StartsWith" 
                                                    TextField="Code" TextFormatString="{0} {1}" ValueField="Code" Width="400px">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" 
                                                            Width="150px" />
                                                        <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" 
                                                            Width="300px" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="suppliername" 
                                                Name="SupplierName" ReadOnly="True" ShowInCustomizationForm="True" 
                                                VisibleIndex="2" Width="250px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" 
                                                Name="Description" ReadOnly="True" ShowInCustomizationForm="True" 
                                                VisibleIndex="3" Width="100%">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
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
                                            <dx:GridViewDataTextColumn Caption="ProductSupplierId" 
                                                FieldName="ProductSupplierId" Name="ProductSupplierId" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsBehavior ColumnResizeMode="NextColumn" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                         <Styles>
                                            <Header HorizontalAlign="Center" Font-Bold="true">
                                            </Header>             
                                            <CommandColumn Spacing="10px">
                                            </CommandColumn>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Font-Bold="True" 
                                        Text="Thêm Nhà sản xuất" Width="191px">
                                    </dx:ASPxButton>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Đơn Vị Tính">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%" Height="400px" ScrollBars="Vertical">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                                    Width="100%" KeyFieldName="OrganizationId" 
                                                    ParentFieldName="ParentOrganizationId">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="Mã Đơn Vị Tính"  ShowInCustomizationForm="True" 
                                                            VisibleIndex="0" Width="90px" FieldName="code">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Tên Đơn Vị Tính" ShowInCustomizationForm="True" 
                                                            VisibleIndex="1" Width="250px" FieldName="name">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Bao Gồm" ShowInCustomizationForm="True" 
                                                            VisibleIndex="2" Width="90px" FieldName="amount">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" 
                                                            VisibleIndex="3" Width="250px" FieldName="description">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" 
                                                            VisibleIndex="3" Width="120px" Caption="Thao tác">
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
                                                    <Styles>
                                                        <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                        </CommandButton>
                                                    </Styles>
                                                </dx:ASPxTreeList>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Font-Bold="True" 
                                        Text="Thêm Đơn vị tính" Width="191px">
                                    </dx:ASPxButton>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Danh Mục Thành Phần">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="grdProductSupplier0" runat="server" 
                                        AutoGenerateColumns="False" Width="100%"
                                        Settings-VerticalScrollBarMode="Auto" Settings-HorizontalScrollBarMode="Auto">
                                        <Columns>
                                            <dx:GridViewDataComboBoxColumn Caption="Mã thành phần" FieldName="SupplierCode" 
                                                Name="SupplierCode" ShowInCustomizationForm="True" VisibleIndex="1" 
                                                Width="150px">
                                                <PropertiesComboBox CallbackPageSize="20" DropDownStyle="DropDown" 
                                                    EnableCallbackMode="True" IncrementalFilteringMode="StartsWith" 
                                                    TextField="Code" TextFormatString="{0} {1}" ValueField="Code" Width="400px">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã Nhà Cung Cấp" FieldName="Code" Name="Code" 
                                                            Width="150px" />
                                                        <dx:ListBoxColumn Caption="Tên Nhà Cung Cấp" FieldName="Name" Name="Name" 
                                                            Width="300px" />
                                                    </Columns>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên thành phần" FieldName="SupplierName" 
                                                Name="SupplierName" ReadOnly="True" ShowInCustomizationForm="True" 
                                                VisibleIndex="2" Width="100%">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="Description" 
                                                Name="Description" ReadOnly="True" ShowInCustomizationForm="True" 
                                                VisibleIndex="3" Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                ShowInCustomizationForm="True" VisibleIndex="6" Width="100px">
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
                                            <dx:GridViewDataTextColumn Caption="ProductSupplierId" 
                                                FieldName="ProductSupplierId" Name="ProductSupplierId" 
                                                ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số lượng" ShowInCustomizationForm="True" 
                                                VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsBehavior ColumnResizeMode="NextColumn" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                         <Styles>
                                            <Header HorizontalAlign="Center" Font-Bold="true">
                                            </Header>             
                                            <CommandColumn Spacing="10px">
                                            </CommandColumn>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <dx:ASPxButton ID="ASPxButton5" runat="server" Font-Bold="True" 
                                        Text="Thêm thành phần" Width="191px">
                                    </dx:ASPxButton>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Thông Tin Chi Tiết">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxNavBar ID="anchdsdsdsdgdhd" runat="server" AutoCollapse="True" Height="100%"
                                    Width="100%">
                                    <Groups>
                                        <dx:NavBarGroup Text="Mô Tả">
                                            <Items>
                                                <dx:NavBarItem>
                                                    <Template>
                                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor43dsds33" runat="server" Height="350px" Width="100%">
                                                            <Settings AllowHtmlView="False" AllowPreview="False" />
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
                                                        <dx:ASPxFileManager ID="ASPxFileManager4341" runat="server" Height="350px">
                                                            <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                            <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                        </dx:ASPxFileManager>
                                                    </Template>
                                                </dx:NavBarItem>
                                            </Items>
                                        </dx:NavBarGroup>
                                    </Groups>
                                </dx:ASPxNavBar>
                                <div class="quickHelp">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"
                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                        Font-Bold="False" Font-Size="XX-Small">
                                    </dx:ASPxLabel>
                                </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterTemplate>
        <div style="padding: 10px;">
            <div class="float-left">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                </div>
            </div>
            <div class="float-right">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnApply" clientinstancename="btnApply" runat="server" Text="Lưu lại">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                </div>
                <div class="float-left button-left-margin">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnCancel" clientinstancename="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                <ClientSideEvents Click="OnNextClick" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </FooterTemplate>
</dx:ASPxPopupControl>   



