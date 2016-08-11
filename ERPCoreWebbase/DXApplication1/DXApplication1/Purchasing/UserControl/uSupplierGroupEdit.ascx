<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSupplierGroupEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uSupplierGroupEdit" %>
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

<script type="text/javascript">
    function buttonSaveCategory_Click(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcSupplierCategory.GetMainElement(), null, true);
        if (validated) {
            cpLineSupplierGroup.PerformCallback('SaveCategory');
        } else
            pcSupplierCategory.SetActiveTabIndex(0);
    }

    function txtCodeSupplierCategoryLostFocus(s, e) {
        var valueCode = txtCodeSupplierCategory.GetText().trim();
        var oldValueCode = '<%= FirstCategoryEntity.Code %>';
        if (valueCode != '' && valueCode != oldValueCode)
            if (cpCheckSupplierCategoryCode.InCallback())
                console.log("Server's busy");
            else
                cpCheckSupplierCategoryCode.PerformCallback("CheckSupplierCategory");
        }

    function validationFormSupplierCategory(s, e) {
        var nameItem = s.name;
        var valueItem = s.GetText().trim();
        var lengthItem = valueItem.length;
        switch (nameItem) {
            case '<%= txtCodeCategory.ClientID %>':
                if (lengthItem > 128) {
                    e.isValid = false;
                    e.errorText = "Độ dài mã bị vượt quá giới hạn";
                }

                break;
            case '<%= txtNameCategory.ClientID %>':
                if (lengthItem > 255) {
                    e.isValid = false;
                    e.errorText = "Độ dài tên bị vượt quá giới hạn";
                }
                break;
            default:
                break;
        }
    }

    function buttonCancelCategory_Click(s, e) {
        formSupplierEdit.Hide();
    }

    function cpLineSupplierGroup_EndCallback(s, e) {
        grdDataSupplierGroup.PerformCallback('Done');
        LoadingPanelCombineSupplier.Hide();
    }
</script>
<div id="lineContainerSupplierGroup">
    <dx:ASPxCallbackPanel ID="cpLineSupplierGroup" runat="server" Width="100%" ClientInstanceName="cpLineSupplierGroup"
        OnCallback="cpLineSupplierGroup_Callback" ShowLoadingPanel="false">
        <ClientSideEvents EndCallback="cpLineSupplierGroup_EndCallback" />
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxHiddenField ID="hiddenModeCategory" runat="server">
                </dx:ASPxHiddenField>
                <dx:ASPxPopupControl ID="formSupplierGroupEdit" runat="server" HeaderText="Thông Tin Nhóm Nhà Cung Cấp - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formSupplierGroupEdit"
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
                                <dx:ASPxButton ID="buttonCancelCategory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelCategory" Text="Thoát" Wrap="False" CausesValidation="false">
                                    <ClientSideEvents Click="buttonCancelCategory_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptCategory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonAcceptCategory" Text="Lưu Lại" Wrap="False" CausesValidation="true">
                                    <ClientSideEvents Click="buttonSaveCategory_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="pcSupplierCategory" ClientInstanceName="pcSupplierCategory" runat="server" ActiveTabIndex="0" Height="100%"
                                RenderMode="Classic" Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="formEditCommonCategory" runat="server" 
                                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã nhóm NCC" FieldName="Code" RequiredMarkDisplayMode="Required">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpCheckSupplierCategoryCode" runat="server" Width="100%" 
                                                                            ClientInstanceName="cpCheckSupplierCategoryCode"
                                                                            OnCallback="cpCheckSupplierCategoryCode_Callback" ShowLoadingPanel="false">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtCodeCategory" runat="server" 
                                                                                            ClientInstanceName="txtCodeSupplierCategory" NullText="Tối đa 128 ký tự" 
                                                                                            Width="200px">
                                                                                            <NullTextStyle ForeColor="Silver">
                                                                                            </NullTextStyle>
                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="false">
                                                                                                <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Mã nhóm NCC" /> 
                                                                                            </ValidationSettings>
                                                                                            <ClientSideEvents Validation="validationFormSupplierCategory" LostFocus="txtCodeSupplierCategoryLostFocus" />
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Nhóm NCC" FieldName="Name" RequiredMarkDisplayMode="Required">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtNameCategory" runat="server" 
                                                                                ClientInstanceName="txtNameCategory" MaxLength="255" 
                                                                                NullText="255 ký tự, không cho phép trùng lắp" Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Tên nhóm NCC" /> 
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" FieldName="RowStatus">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboRowStatusCategory" runat="server" 
                                                                                ClientInstanceName="cboRowStatusCategory" NullText="Tự động tạo mới" 
                                                                                Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <%--<dx:LayoutItem Caption="Hình Ảnh">
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
                                                                <dx:LayoutItem Caption=" ">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" 
                                                                                Width="300px">
                                                                            </dx:ASPxUploadControl>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>--%>
                                                            </Items>
                                                        </dx:LayoutGroup>
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
                                    <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxNavBar ID="navBarSupplierCategoryDetail" runat="server" AutoCollapse="True" 
                                                    Height="100%" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <ContentTemplate>
                                                                <dx:ASPxHtmlEditor ID="htmlEditDescription" runat="server" Height="350px" 
                                                                    Width="100%">
                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                </dx:ASPxHtmlEditor>
                                                            </ContentTemplate>
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
