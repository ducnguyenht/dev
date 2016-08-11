<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="WarehouseStorage.aspx.cs" Inherits="WebModule.Warehouse.WarehouseStorage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
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
        .footer_bt
        {
            height: 45px;
        }
    </style>
    <script type="text/javascript">
        function formMaterialEdit_Init(s, e) {
            
            ASPxHtmlEditor3.SetHeight($("#testheight").height() - 200);
            ASPxFileManager1.SetHeight($("#testheight").height() - 200);
            ASPxdsNavfdfBar1.SetHeight($("#testheight").height() - 100);

            ASPxClientControl.AdjustControls();
        }
        function formMaterialEdit_AfterResizing(s, e) {
            ASPxHtmlEditor3.SetHeight($("#testheight").height() - 200);
            ASPxFileManager1.SetHeight($("#testheight").height() - 200);
            ASPxdsNavfdfBar1.SetHeight($("#testheight").height() - 100);

            ASPxClientControl.AdjustControls();
        }

        function buttonCancelDevice_Click(s, e) {
        }
        function buttonSaveDevice_Click(s, e) {
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="captionFormName">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh Mục Tiêu Chuẩn" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top;">
                <div class="gridContainer">
                    <dx:ASPxGridView ID="grid_dmorder" runat="server" AutoGenerateColumns="False" KeyFieldName="id"
                        ClientInstanceName="popup_orderdetail" Width="100%" OnInitNewRow="grid_dmorder_InitNewRow"
                        OnStartRowEditing="grid_dmorder_StartRowEditing">
                        <ClientSideEvents EndCallback="function(s, e) {
	        popup_orderdetail.Show();
	        ASPxClientControl.AdjustControls();
            ASPxFileManager1.SetHeight($(&quot;#testheight&quot;).height() - 200);
}" />
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã Tiêu Chuẩn" FieldName="code" VisibleIndex="1"
                                SortIndex="3" SortOrder="Ascending" Width="150px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên Tiêu Chuẩn" FieldName="name" VisibleIndex="2"
                                SortIndex="2" SortOrder="Ascending" Width="100%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" VisibleIndex="3"
                                SortIndex="3" SortOrder="Ascending" Width="200px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="status" VisibleIndex="4"
                                SortIndex="1" SortOrder="Ascending" Width="100px">
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="5"
                                Width="100px">
                                <EditButton Text="Tùy chỉnh" Visible="True">
                                    <Image ToolTip="Sửa">
                                        <SpriteProperties CssClass="Sprite_Edit" />
                                    </Image>
                                </EditButton>
                                <NewButton Text="Tạo mới" Visible="True">
                                    <Image ToolTip="Thêm">
                                        <SpriteProperties CssClass="Sprite_New" />
                                    </Image>
                                </NewButton>
                                <DeleteButton Text="Xóa" Visible="True">
                                    <Image ToolTip="Xóa">
                                        <SpriteProperties CssClass="Sprite_Delete" />
                                    </Image>
                                </DeleteButton>
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                        <SettingsBehavior ColumnResizeMode="Control" AllowFocusedRow="true" AllowSelectByRowClick="true"
                            AllowSelectSingleRowOnly="true" />
                        <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                        </SettingsPager>
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                            <CommandColumn Spacing="10px">
                            </CommandColumn>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="popup_orderdetail" runat="server" HeaderText="Thông Tin Lưu Trữ - "
        Height="600px" Modal="True" Width="900px" ClientInstanceName="popup_orderdetail"
        AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
        ScrollBars="Auto" ShowMaximizeButton="True"
        EnableViewState="False" CloseAction="CloseButton" ShowShadow="False">
        <ClientSideEvents Init="formMaterialEdit_Init" AfterResizing="formMaterialEdit_AfterResizing">
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
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="100%"
                        Width="100%" RenderMode="Classic">
                        <TabPages>
                            <dx:TabPage Text="Thông Tin Chung">
                                <ContentCollection>
                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="fdF2dsddsdormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
                                            EnableTheming="True" Height="100%" Width="100%">
                                            <Items>
                                                <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Mã Lưu Trữ" HelpText="Tối đa 128 ký tự">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="txtCodeDevice" runat="server" ClientInstanceName="txtCodeDevice"
                                                                        NullText="Tối đa 128 ký tự" Width="200px">
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
                                                        <dx:LayoutItem Caption="Tên Lưu Trữ" HelpText="255 ký tự, không cho phép trùng lắp">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="txtNameDevice" runat="server" ClientInstanceName="txtNameDevice"
                                                                        MaxLength="255" Width="400px">
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
                                                                        NullText="Tự động tạo mới" Width="200px">
                                                                        <Items>
                                                                            <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                            <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                                        </Items>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Hình Ảnh">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxImage ID="A32SPxFormLayout1_E1" runat="server" Height="200px" ImageUrl="~/images/NASID/NASERPLogo.png"
                                                                        Width="300px">
                                                                        <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </dx:ASPxImage>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                            <CaptionCellStyle CssClass="CaptionStyle">
                                                            </CaptionCellStyle>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho thông tin lưu trữ">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxUploadControl ID="fdF2dsddsdormLayout1_E1" runat="server" UploadMode="Auto"
                                                                        Width="280px">
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
                            <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="wwwsfdfasds" runat="server" Height="100%" Width="100%">
                                            <Items>
                                                <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxNavBar ID="ASPxdsNavfdfBar1" runat="server" AutoCollapse="True" Height="100%"
                                                                Width="100%" ClientInstanceName="ASPxdsNavfdfBar1" AllowSelectItem="True" EnableClientSideAPI="True">
                                                                <Groups>
                                                                    <dx:NavBarGroup Text="Mô Tả">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="350px" Width="100%"
                                                                                        ClientInstanceName="ASPxHtmlEditor3">
                                                                                        <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                                    </dx:ASPxHtmlEditor>
                                                                                </Template>
                                                                            </dx:NavBarItem>
                                                                        </Items>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Text="Tài Liệu" Expanded="False">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" 
                                                                                        ClientInstanceName="ASPxFileManager1" Height="350px">
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
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Italic="True" Font-Size="X-Small"
                                                                ForeColor="#CCCCCC" Text="Mô tả và tài liệu cho tiêu chuẩn lưu trữ">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <CaptionSettings VerticalAlign="Middle" />
                                                    <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                </dx:LayoutItem>
                                            </Items>
                                            <SettingsItems VerticalAlign="Middle" />
                                            <SettingsItemHelpTexts Position="Bottom" />
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
</asp:Content>
