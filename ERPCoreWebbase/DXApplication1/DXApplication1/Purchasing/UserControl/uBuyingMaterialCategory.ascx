<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingMaterialCategory.ascx.cs"
    Inherits="DXApplication1.Purchasing.UserControl.uBuyingMaterialCategory" %>
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
    function buttonSaveMaterialCategory_Click(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcMaterialCategory.GetMainElement(), null, true);
        if (validated) {
            cpLineMaterialCategory.PerformCallback('SaveCategory');
        } else
            pcMaterialCategory.SetActiveTabIndex(0);
    }

    function updateByLostFocus(s, e) {
        if (formMaterialCategoryEdit.cpMode == 'add')
            return;
        var valueCode = txtCodeCategoryMaterial.GetText().trim();
        if (valueCode != '')
            if (cpCheckMaterialCategoryCode.InCallback())
                console.log("Server's busy");
            else
                cpCheckMaterialCategoryCode.PerformCallback("updateByLostFocus");
    }

    function validationFormMaterialCategory(s, e) {
        var nameItem = s.name;
        var valueItem = s.GetText().trim();
        var lengthItem = valueItem.length;
        switch (nameItem) {
            case '<%= txtCode.ClientID %>':
                if (valueItem == '') {
                    e.isValid = false;
                    e.errorText = "Bắt buộc nhập Mã nhóm nguyên vật liệu";
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
                    e.errorText = "Bắt buộc nhập Tên nhóm nguyên vật liệu";
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

    function buttonCancelMaterialCategory_Click(s, e) {
        formMaterialCategoryEdit.Hide();
    }

    function cpLineMaterialCategory_EndCallback(s, e) {
        grdDataMaterialCategory.PerformCallback('Done');
        LoadingPanelCombineMaterial.Hide();
    }

    function cpCheckMaterialCategoryCode_Endcallback(s, e) {
    }

    function buttonEditMaterialCategory_click(s, e) 
    {
        cpLineMaterialCategory.PerformCallback('ActivateForm');
    }

    function formMaterialCategoryEdit_closing(s, e) {
        grdDataMaterialCategory.PerformCallback('Done');
    }
    
</script>
<div id="lineContainerMaterialCategory">
    <dx:ASPxCallbackPanel ID="cpLineMaterialCategory" runat="server" Width="100%" ClientInstanceName="cpLineMaterialCategory"
        OnCallback="cpLineMaterialCategory_Callback" ShowLoadingPanelImage="false" ShowLoadingPanel="false">
        <ClientSideEvents EndCallback="cpLineMaterialCategory_EndCallback" />
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxHiddenField ID="hiddenModeCategory" runat="server">
                </dx:ASPxHiddenField>
                <dx:ASPxPopupControl ID="formMaterialCategoryEdit" ClientInstanceName="formMaterialCategoryEdit" runat="server" HeaderText="Thông Tin Công Cụ Dụng Cụ - "
                    Height="600px" Modal="True" Width="900px"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
                    ScrollBars="Auto" ShowMaximizeButton="True" 
                    CloseAction="CloseButton">
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
                                <dx:ASPxButton ID="buttonCancelMaterialCategory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelMaterialCategory" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="buttonCancelMaterialCategory_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonSaveMaterialCategory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveMaterialCategory" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="buttonSaveMaterialCategory_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonEditMaterialCategory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonEditMaterialCategory" Text="Chỉnh sửa" Wrap="False">
                                    <ClientSideEvents Click="buttonEditMaterialCategory_click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="pcMaterialCategory" ClientInstanceName="pcMaterialCategory" runat="server" ActiveTabIndex="0" Height="100%"
                                Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="FormCommonMaterialCategoryEdit" runat="server" AlignItemCaptionsInAllGroups="True"
                                                    EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã nhóm nguyên vật liệu" HelpText="Tối đa 128 ký tự" 
                                                                    FieldName="Code" 
                                                                    RequiredMarkDisplayMode="Required">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpCheckMaterialCategoryCode" runat="server" Width="100%" 
                                                                                ClientInstanceName="cpCheckMaterialCategoryCode"
                                                                                OnCallback="cpCheckMaterialCategoryCode_Callback" ShowLoadingPanel="false">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtCode" ClientInstanceName="txtCodeCategoryMaterial" runat="server"
                                                                                            NullText="Tối đa 128 ký tự" Width="200px">
                                                                                            <NullTextStyle ForeColor="Silver">
                                                                                            </NullTextStyle>
                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="false">
                                                                                                <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Mã nhóm nguyên vật liệu" /> 
                                                                                            </ValidationSettings>
                                                                                            <ClientSideEvents Validation="validationFormMaterialCategory" LostFocus="updateByLostFocus" />
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                                <ClientSideEvents EndCallback="cpCheckMaterialCategoryCode_Endcallback" />
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên nhóm nguyên vật liệu" HelpText="255 ký tự, không cho phép trùng lắp" 
                                                                    FieldName="Name" 
                                                                    RequiredMarkDisplayMode="Required">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtName" runat="server"
                                                                                MaxLength="255" Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="false">
                                                                                    <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Tên nhóm nguyên vật liệu" /> 
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents Validation="validationFormMaterialCategory" LostFocus="updateByLostFocus" />
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới" FieldName="RowStatus"
                                                                    RequiredMarkDisplayMode="Required">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboRowStatus" runat="server"
                                                                                Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="I" />
                                                                                </Items>
                                                                                <ClientSideEvents Validation="validationFormMaterialCategory" LostFocus="updateByLostFocus" />
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
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
                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho CCDC">
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
                                    <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxNavBar ID="navBarMaterialCategoryDetail" runat="server" AutoCollapse="True" Height="100%"
                                                    Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <ContentTemplate>
                                                                <dx:ASPxHtmlEditor ID="htmlEditDescription" runat="server" Height="350px" 
                                                                    Width="100%">
                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
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
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <ClientSideEvents Closing="formMaterialCategoryEdit_closing" />
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
