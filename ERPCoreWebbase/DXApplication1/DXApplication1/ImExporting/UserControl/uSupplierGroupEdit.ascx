<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSupplierGroupEdit.ascx.cs"
    Inherits="ERPCore.ImExporting.UserControl.uSupplierGroupEdit" %>
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
<div id="lineContainerSupplierGroup">
    <dx:ASPxCallbackPanel ID="cpLineSupplierGroup" runat="server" Width="100%" ClientInstanceName="cpLineSupplierGroup"
        OnCallback="cpLineSupplierGroup_Callback">
        <ClientSideEvents EndCallback="cpLineSupplierGroup_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formSupplierEdit" runat="server" HeaderText="Thông Tin Nhà Cung Cấp - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formSupplierEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
                    ScrollBars="Auto" ShowMaximizeButton="True"
                    RenderMode="Classic">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
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
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" Height="100%"
                                RenderMode="Classic" Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Nhóm NCC" HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtCodeDevice" runat="server" 
                                                                                ClientInstanceName="txtCodeDevice" 
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
                                                                <dx:LayoutItem Caption="Tên Nhóm NCC" 
                                                                    HelpText="255 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtNameDevice" runat="server" 
                                                                                ClientInstanceName="txtNameDevice" MaxLength="255" Width="400px">
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
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboRowStatusDevice" runat="server" 
                                                                                ClientInstanceName="cboRowStatusDevice" 
                                                                                Width="200px">
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
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" 
                                                                                ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho nhà cung cấp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" 
                                                                                Width="300px">
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
                                    <dx:TabPage Name="tabNhomNhaCungCap" Text="Trực Thuộc Nhóm">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grmanufacturer" runat="server" AutoGenerateColumns="False" ClientInstanceName="grmanufacturer"
                                                    Width="100%">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Xóa" ShowInCustomizationForm="True"
                                                            VisibleIndex="6" Width="3%">
                                                            <DeleteButton Visible="True">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                </Image>
                                                            </DeleteButton>
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                            VisibleIndex="5" Width="6%">
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
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên Nhóm Nhà Cung Cấp" FieldName="Name" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" ShowInCustomizationForm="True"
                                                            VisibleIndex="2" Width="30%">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã Nhóm Nhà Cung Cấp" FieldName="Code" ShowInCustomizationForm="True"
                                                            VisibleIndex="0" Width="10%">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                        ShowHeaderFilterButton="True" VerticalScrollableHeight="360" 
                                                        VerticalScrollBarMode="Auto" />
                                                </dx:ASPxGridView>
                                                  <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"                                                        
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp" 
                                                         Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" AutoCollapse="True" 
                                                    Height="100%" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="350px" 
                                                                            Width="100%">
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
                                                 <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="False" ForeColor="Gray"                                                        
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
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
