<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSupplierEdit.ascx.cs"
    Inherits="ERPCore.ImExporting.UserControl.uSupplierEdit" %>
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
<div id="lineContainerSupplier">
    <dx:aspxcallbackpanel id="cpLineSupplier" runat="server" width="100%" clientinstancename="cpLineSupplier"
        oncallback="cpLineSupplier_Callback">
    <ClientSideEvents EndCallback="cpLineSupplier_EndCallback" />
<ClientSideEvents EndCallback="cpLineSupplier_EndCallback"></ClientSideEvents>
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
           <dx:ASPxPopupControl ID="formSupplierGroupEdit" runat="server" HeaderText="Thông Tin Nhóm Nhà Cung Cấp - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formSupplierGroupEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
               ScrollBars="Auto" ShowMaximizeButton="True">
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
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        Height="100%" Width="100%">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="dsdsdsd" runat="server" Width="100%">
                                            <Items>
                                                <dx:LayoutGroup ShowCaption="False">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Mã Nhà Cung Cấp" RequiredMarkDisplayMode="Required" 
                                                            HelpText="Tối đa 128 ký tự">
                                                            <layoutitemnestedcontrolcollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxFormLayout1235_E2" runat="server" Width="200px">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </layoutitemnestedcontrolcollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Tên Nhà Cung Cấp" RequiredMarkDisplayMode="Required" 
                                                            HelpText="255 ký tự">
                                                            <layoutitemnestedcontrolcollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="ASPxFormLayout1235_E1" runat="server" Width="400px">
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </layoutitemnestedcontrolcollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                            <layoutitemnestedcontrolcollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="ASPxFormLayout1235_E4" runat="server" Width="200px">
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </layoutitemnestedcontrolcollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Hình Ảnh">
                                                            <layoutitemnestedcontrolcollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxImage ID="ASPxFormLayout1235_E5" runat="server" Height="200px" 
                                                                        ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                    </dx:ASPxImage>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </layoutitemnestedcontrolcollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="   " ShowCaption="True" 
                                                            HelpText="Hình ảnh cho nhóm nhà cung cấp">
                                                            <layoutitemnestedcontrolcollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxUploadControl ID="ASPxFormLayout1235_E6" runat="server" 
                                                                        UploadMode="Auto" Width="280px">
                                                                    </dx:ASPxUploadControl>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </layoutitemnestedcontrolcollection>
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
                                           <dx:ASPxNavBar ID="anchdsdsgdhd" runat="server" AutoCollapse="True" Height="100%" 
                                                        Width="100%">
                                                        <groups>
                                                            <dx:NavBarGroup Text="Mô Tả">
                                                                <Items>
                                                                    <dx:NavBarItem>
                                                                        <template>
                                                                            <dx:ASPxHtmlEditor ID="ASPxHtmlEditor43dsds33" runat="server" Height="350px" 
                                                                                Width="100%">
                                                                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                            <Settings AllowHtmlView="False" AllowPreview="False" /></dx:ASPxHtmlEditor>
                                                                        </template>
                                                                    </dx:NavBarItem>
                                                                </Items>
                                                            </dx:NavBarGroup>
                                                            <dx:NavBarGroup Expanded="False" Text="Tài Liệu">
                                                                <Items>
                                                                    <dx:NavBarItem>
                                                                        <template>
                                                                            <dx:ASPxFileManager ID="ASPxFileManager4341" runat="server" Height="350px">
                                                                                <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                                            <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" /></dx:ASPxFileManager>
                                                                        </template>
                                                                    </dx:NavBarItem>
                                                                </Items>
                                                            </dx:NavBarGroup>
                                                        </groups>
                                                    </dx:ASPxNavBar>
                                         <div class="quickHelp">                                                  
                                                    <dx:ASPxLabel ID="ASPxLabel3e" runat="server" Font-Italic="False" ForeColor="Gray"
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
</dx:aspxcallbackpanel>
</div>
