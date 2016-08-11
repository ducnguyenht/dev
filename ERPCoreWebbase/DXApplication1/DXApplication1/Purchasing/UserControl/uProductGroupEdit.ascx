<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uProductGroupEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uProductGroupEdit" %>
<style type="text/css">
    .dxbButton_DevEx
    {
        color: #201f35;
        font: normal 11px Verdana, Geneva, sans-serif;
        vertical-align: middle;
        border: 1px solid #a9acb5;
        padding: 1px;
        cursor: pointer;
    }
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
<div id="lineContainerProductGroup">
                <dx:ASPxPopupControl runat="server" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        Modal="True" AllowDragging="True" AllowResize="True" ScrollBars="Auto" 
        ClientInstanceName="formProductGroupEdit" 
        HeaderText="Th&#244;ng Tin Nh&#243;m H&#224;ng H&#243;a - " 
        ShowMaximizeButton="True" ShowFooter="True" ShowSizeGrip="False" Width="900px" 
        Height="600px" ID="formProductGroupEdit">
<FooterStyle HorizontalAlign="Center" CssClass="footer_bt"></FooterStyle>
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
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    
</FooterContentTemplate>
<ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxCallbackPanel ID="cpProductGroupEdit" runat="server" 
                                ClientInstanceName="cpProductGroupEdit" 
                                OnCallback="cpProductGroupEdit_Callback" Width="100%">
                                <PanelCollection>
                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxPageControl ID="pcProductGroup" runat="server" ActiveTabIndex="0" 
                                            ClientInstanceName="pcProductGroup" Height="100%" Width="100%">
                                            <TabPages>
                                                <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                                    <ContentCollection>
                                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                                                AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                                <Items>
                                                                    <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False">
                                                                        <Items>
                                                                            <dx:LayoutItem Caption="Mã Nhóm Hàng Hóa" HelpText="Tối đa 128 ký tự">
                                                                                <LayoutItemNestedControlCollection>
                                                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                        SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtProductGroupEditCode" runat="server" 
                                                                                            ClientInstanceName="txtProductGroupEditCode" 
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
                                                                            <dx:LayoutItem Caption="Tên Nhóm Hàng Hóa" 
                                                                                HelpText="255 ký tự, không cho phép trùng lắp">
                                                                                <LayoutItemNestedControlCollection>
                                                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                        SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtProductGroupEditName" runat="server" 
                                                                                            ClientInstanceName="txtProductGroupEditName" MaxLength="255" 
                                                                                            OnValidation="txtProductGroupEditName_Validation" Width="400px">
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
                                                                                        <dx:ASPxComboBox ID="cboProductGroupEditRowStatus" runat="server" 
                                                                                            ClientInstanceName="cboProductGroupEditRowStatus" Width="200px">
                                                                                            <Items>
                                                                                                <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                                                                <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                                                            </Items>
                                                                                        </dx:ASPxComboBox>
                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                </LayoutItemNestedControlCollection>
                                                                            </dx:LayoutItem>
                                                                            <dx:LayoutItem Caption="Hình Ảnh" Visible="False">
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
                                                                            <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho nhóm hàng hóa" 
                                                                                Visible="False">
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
                                                <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                                    <ContentCollection>
                                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxFormLayout ID="fxxsds" runat="server" Height="100%" Width="100%">
                                                                <Items>
                                                                    <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                SupportsDisabledAttribute="True">
                                                                                <dx:ASPxNavBar ID="nbProductGroupEdit" runat="server" AutoCollapse="True" 
                                                                                    ClientInstanceName="nbProductGroupEdit" Height="100%" Width="100%">
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
                                                                    <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle" 
                                                                        Visible="False">
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
                                            </TabPages>
                                        </dx:ASPxPageControl>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>

                        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

</div>
<dx:ASPxHiddenField ID="hEditProductGroupId" runat="server" 
    ClientInstanceName="hEditProductGroupId">
</dx:ASPxHiddenField>

