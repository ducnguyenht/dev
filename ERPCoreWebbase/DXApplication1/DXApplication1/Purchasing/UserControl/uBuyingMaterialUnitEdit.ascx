<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBuyingMaterialUnitEdit.ascx.cs"
    Inherits="DXApplication1.Purchasing.UserControl.uBuyingMaterialUnitEdit" %>
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
    function buttonSaveMaterialUnit_Click(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcMaterialUnitInfo.GetMainElement(), null, true);
        if (validated) {
            cpLineMaterialUnit.PerformCallback('SaveUnit');
        } else
            pcMaterialUnitInfo.SetActiveTabIndex(0);
    }

    function txtCodeMaterialUnitLostFocus(s, e) {
        var valueCode = txtCodeMaterialUnit.GetText().trim();
        var oldValueCode = '<%= FirstUnitEntity.Code %>';

        if (valueCode != '' && valueCode != oldValueCode)
            if (cpCheckMaterialUnitCode.InCallback())
                console.log("Server's busy");
            else
                cpCheckMaterialUnitCode.PerformCallback("CheckCategoryUnit");
    }

    function validationFormMaterialUnit(s, e) {
        var nameItem = s.name;
        var valueItem = s.GetText().trim();
        var lengthItem = valueItem.length;
        switch (nameItem) {
            case '<%= txtCode.ClientID %>':
                if (valueItem == '') {
                    e.isValid = false;
                    e.errorText = "Bắt buộc nhập Mã đơn vị tính";
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
                    e.errorText = "Bắt buộc nhập Tên đơn vị tính";
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

    function cpLineMaterialUnit_EndCallback(s, e) {
        grdDataMaterialUnit.PerformCallback('Done');
        LoadingPanelCombineMaterial.Hide();
    }

    function cpCheckMaterialUnitCode_Endcallback(s, e) {
    }

    function buttonCancelMaterialUnit_Click(s, e) {
        formMaterialUnitEdit.Hide();
    }
</script>

<dx:ASPxCallbackPanel ID="cpLineMaterialUnit" runat="server" Width="100%" ClientInstanceName="cpLineMaterialUnit"
    OnCallback="cpLineMaterialUnit_Callback" ShowLoadingPanelImage="false" ShowLoadingPanel="false">
    <ClientSideEvents EndCallback="cpLineMaterialUnit_EndCallback" />
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxHiddenField ID="hiddenMode" runat="server">
            </dx:ASPxHiddenField>
            <dx:ASPxPopupControl ID="formMaterialUnitEdit" runat="server" HeaderText="Thông Tin Đơn Vị Tính - "
                Height="600px" Modal="True" Width="900px" ClientInstanceName="formMaterialUnitEdit"
                AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
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
                            <dx:ASPxButton ID="buttonCancelMaterialUnit" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonCancelMaterialUnit" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                <ClientSideEvents Click="buttonCancelMaterialUnit_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonSaveMaterialUnit" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonSaveMaterialUnit" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S"
                                CausesValidation="true">
                                <ClientSideEvents Click="buttonSaveMaterialUnit_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </div>
                </FooterContentTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPageControl ID="pcMaterialUnitInfo" ClientInstanceName="pcMaterialUnitInfo" runat="server" ActiveTabIndex="0" 
                            Height="100%" Width="100%">
                            <TabPages>
                                <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="formCommonEditMaterialUnit" runat="server" 
                                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã đơn vị tính" FieldName="Code" HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpCheckMaterialUnitCode" runat="server" Width="100%" 
                                                                                ClientInstanceName="cpCheckMaterialUnitCode"
                                                                                OnCallback="cpCheckMaterialUnitCode_Callback" ShowLoadingPanel="false">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtCode" runat="server" 
                                                                                            ClientInstanceName="txtCodeMaterialUnit" Width="200px">
                                                                                            <NullTextStyle ForeColor="Silver">
                                                                                            </NullTextStyle>
                                                                                            <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True" SetFocusOnError="false">
                                                                                                <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Mã đơn vị tính" /> 
                                                                                            </ValidationSettings>
                                                                                            <ClientSideEvents Validation="validationFormMaterialUnit" LostFocus="txtCodeMaterialUnitLostFocus" />
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                                <ClientSideEvents EndCallback="cpCheckMaterialUnitCode_Endcallback" />
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên đơn vị tính" FieldName="Name"
                                                                    HelpText="255 ký tự, không cho phép trùng lắp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtName" runat="server" 
                                                                                Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorDisplayMode="ImageWithText" ValidateOnLeave="True"  SetFocusOnError="false">
                                                                                    <RequiredField IsRequired="True" ErrorText="Bắt buộc nhập Tên đơn vị tính" /> 
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents Validation="validationFormMaterialUnit" />
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" 
                                                                    FieldName="RowStatus"
                                                                    HelpText="Tự động tạo mới">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboRowStatus" runat="server" 
                                                                                ClientInstanceName="cboRowStatus" 
                                                                                Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="I" />
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
                                                                <dx:LayoutItem Caption=" " HelpText="Hình ảnh cho ĐVT">
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
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
											<dx:ASPxNavBar ID="navBarMaterialUnitDetail" runat="server" AutoCollapse="True" Height="100%"
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
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
