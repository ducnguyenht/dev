<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uUnitEdit.ascx.cs" Inherits="WebModule.Purchasing.UserControl.uUnitEdit" %>
<script type="text/javascript">
    function cpUnitEdit_EndCallback(s, e) {
        switch (clientAction) {
            case 'Add':
                txtUnitEditCode.Focus();
                break;
            case 'Edit':
                var jObject = $(".KeyShortcutformUnitEdit");
                jObject.attr("tabindex", "0");
                jObject.focus();
                break;
            case 'ActivateForm':
                pcUnit.SetActiveTabIndex(0);
                txtUnitEditCode.Focus();
                break;
            case 'Delete':
            case 'Save':
                grdUnit.Refresh();
                grdUnit.Focus();
                break;
        }
    }

    function btnUnitEdit_Click(s, e) {
        clientAction = 'ActivateForm';
        cpUnitEdit.PerformCallback(clientAction);
    }

    function btnUnitCancel_Click(s, e) {
        formUnitEdit.Hide();
    }

    function btnUnitSave_Click(s, e) {
        var validated = ASPxClientEdit.ValidateEditorsInContainer(pcUnit.GetMainElement(), null, true);
        if (validated) {
            clientAction = 'Save';
            cpUnitEdit.PerformCallback(clientAction);
        } else {
            pcUnit.SetActiveTabIndex(0);
        }
    }

    function formUnitEdit_closing(s, e) {
        grdUnit.Refresh();
        grdUnit.Focus();
    }

    // Shortcut for popup---START-----
    function formUnitEdit_Init(s, e) {
        var jObject = $(".KeyShortcutformUnitEdit");
        jObject.attr("tabindex", "0");
        var htmlObject = jObject.get(0);
        htmlObject.focus();
        //Press Esc to close popup
        Utils.AttachShortcutTo(htmlObject, "Esc", function () {
            formUnitEdit.Hide();
        });
        //Press Ctrl+Enter to save general information
        Utils.AttachShortcutTo(htmlObject, "Ctrl+Enter", function () {
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pcUnit.GetMainElement(), null, true);
            if (validated) {
                clientAction = 'Save';
                cpUnitEdit.PerformCallback('Save');
            } else {
                pcUnit.SetActiveTabIndex(0);
            }
        });

        Utils.AttachShortcutTo(htmlObject, "Ctrl+F2", function () {
            clientAction = 'ActivateForm';
            cpUnitEdit.PerformCallback(clientAction);
            txtUnitEditCode.Focus();
        });

        Utils.AttachShortcutTo(htmlObject, "Shift+Right", function () {
            var curridx = pcUnit.GetActiveTabIndex();
            if (curridx < 1) {
                pcUnit.SetActiveTabIndex(curridx + 1);
                jObject.focus();
            }
        });

        Utils.AttachShortcutTo(htmlObject, "Shift+Left", function () {
            var curridx = pcUnit.GetActiveTabIndex();
            if (curridx >= 1) {
                pcUnit.SetActiveTabIndex(curridx - 1);
                jObject.focus();
            }
        });
    }
    // Shortcut for popup---END-----
</script>
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
<dx:ASPxCallbackPanel ID="cpUnitEdit" runat="server" ShowLoadingPanel="false"
    ClientInstanceName="cpUnitEdit" OnCallback="cpUnitEdit_Callback" Width="100%">
    <ClientSideEvents EndCallback="cpUnitEdit_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <div id="lineContainerUnit">
                <dx:ASPxPopupControl runat="server" 
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                    Modal="True" AllowDragging="True" AllowResize="True" ScrollBars="Auto" 
                    ClientInstanceName="formUnitEdit"
                    CssClass="KeyShortcutformUnitEdit"
                    HeaderText="Thông Tin Quy Cách - " ShowMaximizeButton="True" 
                    ShowFooter="True" ShowSizeGrip="False" Width="900px" Height="600px" 
                    ID="formUnitEdit">
                    <ClientSideEvents Closing="formUnitEdit_closing" Init="formUnitEdit_Init"/>
                    <FooterStyle HorizontalAlign="Center" CssClass="footer_bt"></FooterStyle>
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="bUnitEditHelp" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" ClientInstanceName="bUnitEditHelp">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnUnitCancel" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnUnitCancel" Text="Thoát" Wrap="False" CausesValidation="false"
                                    ToolTip="Thoát  - ESC">
                                    <ClientSideEvents Click="btnUnitCancel_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnUnitEdit" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnUnitEdit" Text="Chỉnh sửa" Wrap="False" ToolTip="Thoát  - Ctrl + F2">
                                    <ClientSideEvents Click="btnUnitEdit_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="btnUnitSave" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="btnUnitSave" Text="Lưu lại" Wrap="False" CausesValidation="true" ToolTip="Thoát  - Ctrl + Enter">
                                    <ClientSideEvents Click="btnUnitSave_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>     
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="pcUnit" runat="server" ActiveTabIndex="0" 
                                ClientInstanceName="pcUnit" Height="100%" Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="frmlytUnitInfo" runat="server" DataSourceID="UnitEdittingXDS"
                                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Quy Cách" HelpText="Tối đa 36 ký tự" FieldName="Code" RequiredMarkDisplayMode="Required">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpUnitEditCode" runat="server" 
                                                                                ClientInstanceName="cpUnitEditCode" Width="200px" 
                                                                                OnCallback="cpUnitEditCode_Callback" ShowLoadingPanel="False">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtUnitEditCode" runat="server" MaxLength="36"
                                                                                            ClientInstanceName="txtUnitEditCode" OnValidation="txtUnitEditCode_Validation" 
                                                                                            Width="200px">
                                                                                            <NullTextStyle ForeColor="Silver">
                                                                                            </NullTextStyle>
                                                                                            <ValidationSettings ErrorText="" SetFocusOnError="false"
                                                                                            ValidationGroup="groupEditUnit">
                                                                                                <RequiredField IsRequired="True" ErrorText="Chưa nhập mã quy cách"></RequiredField>
                                                                                            </ValidationSettings>
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Quy Cách" FieldName="Name"
                                                                    HelpText="255 ký tự, không cho phép trùng lắp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtUnitEditName" runat="server" 
                                                                                ClientInstanceName="txtUnitEditName" MaxLength="255" 
                                                                                OnValidation="txtUnitEditName_Validation" Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorText="" SetFocusOnError="false" ValidationGroup="groupEditUnit">
                                                                                    <RequiredField IsRequired="True" ErrorText="Chưa nhập tên quy cách"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" FieldName="RowStatus" HelpText="Tự động tạo mới" Visible="false" >
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboUnitEditRowStatus" runat="server" 
                                                                                ClientInstanceName="cboUnitEditRowStatus" Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Sử dụng" Value="1"/>
                                                                                    <dx:ListEditItem Text="Tạm" Value="0" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng" Value="-1" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
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
                                                <dx:ASPxNavBar ID="nbUnitEdit" runat="server" AutoCollapse="True" 
                                                    ClientInstanceName="nbUnitEdit" Height="100%" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <ContentTemplate>
                                                                <dx:ASPxHtmlEditor ID="htmlEditDescription" runat="server" Height="350px" Width="100%">
                                                                    <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                </dx:ASPxHtmlEditor>
                                                            </ContentTemplate>
                                                        </dx:NavBarGroup>
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
            </div>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:XpoDataSource ID="UnitEdittingXDS" runat="server" 
    TypeName="NAS.DAL.Nomenclature.Item.Unit">
</dx:XpoDataSource>



