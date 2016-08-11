<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="CodeRuleDefinitionEditingForm.ascx.cs"
    Inherits="WebModule.ERPSystem.ArtifactCode.GUI.CodeRuleDefinitionEditingForm" %>
<script type="text/javascript">
    var CodeRuleDefinitionEditingForm = {

        //declare properties
        events: {
            eShown: 'shown',
            eSaved: 'saved',
            eClosing: 'closing'
        },

        transitions: {
            tEdit: 'Edit',
            tCreate: 'Create',
            tSave: 'Save',
            tCancel: 'Cancel',
            tChangeDataType: 'ChangeDataType'
        },

        Show: function (mode, recordId) {
            var args = '';
            if (mode == 'Create') {
                args = this.transitions.tCreate + '|' + recordId;
            }
            else if (mode == 'Edit') {
                args = this.transitions.tEdit + '|' + recordId;
            }
            if (!cpnCodeRuleDefinitionEditingForm.InCallback()) {
                cpnCodeRuleDefinitionEditingForm.PerformCallback(args);
            }
        },

        Save: function () {
            var validated =
                ASPxClientEdit.ValidateEditorsInContainer(formlayoutCodeRuleDefinitionEditingForm.GetMainElement(), null, false);
            if (validated) {
                var args = this.transitions.tSave;
                if (!cpnCodeRuleDefinitionEditingForm.InCallback()) {
                    cpnCodeRuleDefinitionEditingForm.PerformCallback(args);
                }
            }
        },

        Cancel: function () {
            var args = this.transitions.tCancel;
            if (!cpnCodeRuleDefinitionEditingForm.InCallback()) {
                cpnCodeRuleDefinitionEditingForm.PerformCallback(args);
            }
        },

        EndCallback: function (args) {
            switch (args.transition) {
                case this.transitions.tCancel:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.transitions.tCreate:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.transitions.tEdit:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.transitions.tSave:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eSaved);
                    }
                    break;
                default:
                    break;
            }
        }

    };

    function cpnCodeRuleDefinitionEditingForm_EndCallback(s, e) {

        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            CodeRuleDefinitionEditingForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }

    }

    function btnSaveCodeRuleDefinitionEditingForm_Click(s, e) {
        CodeRuleDefinitionEditingForm.Save();
    }

    function btnCancelCodeRuleDefinitionEditingForm_Click(s, e) {
        CodeRuleDefinitionEditingForm.Cancel();
    }

    function popupCodeRuleDefinitionEditingForm_Closing(s, e) {
        CodeRuleDefinitionEditingForm.Cancel();
    }

    function cbCodeRuleDataType_SelectedIndexChanged(s, e) {
        if (!cpnCodeRuleDefinitionEditingForm.InCallback()) {
            var args = CodeRuleDefinitionEditingForm.transitions.tChangeDataType;
            cpnCodeRuleDefinitionEditingForm.PerformCallback(args);
        }
    }

</script>
<dx:ASPxCallbackPanel ID="cpnCodeRuleDefinitionEditingForm" ClientInstanceName="cpnCodeRuleDefinitionEditingForm"
    runat="server" OnCallback="cpnCodeRuleDefinitionEditingForm_Callback">
    <ClientSideEvents EndCallback="cpnCodeRuleDefinitionEditingForm_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupCodeRuleDefinitionEditingForm" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" RenderMode="Lightweight"
                HeaderText="Cấu hình định nghĩa phương pháp tạo mã" Height="480px" ShowMaximizeButton="True"
                Width="820px" ShowFooter="True">
                <ClientSideEvents Closing="popupCodeRuleDefinitionEditingForm_Closing"></ClientSideEvents>
                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <FooterTemplate>
                    <div style="padding: 10px;">
                        <div style="float: left">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnHelp" runat="server" Text="Trợ giúp" AutoPostBack="false">
                                    <Image ToolTip="Trợ giúp">
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                            </div>
                        </div>
                        <div style="float: right">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnSave" runat="server" Text="Lưu lại" AutoPostBack="false">
                                    <ClientSideEvents Click="btnSaveCodeRuleDefinitionEditingForm_Click" />
                                    <Image ToolTip="Lưu lại">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                    <ClientSideEvents Click="btnCancelCodeRuleDefinitionEditingForm_Click" />
                                    <Image ToolTip="Bỏ qua">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="formlayoutCodeRuleDefinitionEditingForm" ClientInstanceName="formlayoutCodeRuleDefinitionEditingForm"
                            runat="server" DataSourceID="dsCodeRuleData">
                            <Items>
                                <dx:LayoutItem Caption="Thể loại" RequiredMarkDisplayMode="Required">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="cbCodeRuleDataType" runat="server" DataSourceID="dsCodeRuleDataType"
                                                TextField="Name" ValueField="CodeRuleDataTypeId">
                                                <ClientSideEvents SelectedIndexChanged="cbCodeRuleDataType_SelectedIndexChanged" />
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Name="ValueData" Caption="Giá trị" 
                                    RequiredMarkDisplayMode="Required" FieldName="StringValue">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="txtStringValue" runat="server" Width="170px">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Name="BeginNumber" Caption="Số bắt đầu" 
                                    RequiredMarkDisplayMode="Required" FieldName="BeginNumberValue">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="spinBeginNumberValue" runat="server" Height="21px" Number="0">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Name="Step" Caption="Số bước tăng" 
                                    RequiredMarkDisplayMode="Required" FieldName="Step">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="spinStep" runat="server" Height="21px" Number="0">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Name="EndNumber" Caption="Số kết thúc" 
                                    RequiredMarkDisplayMode="Optional" FieldName="EndNumberValue">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="spinEndNumberValue" runat="server" Height="21px" Number="0">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Định dạng" RequiredMarkDisplayMode="Required" 
                                    FieldName="CodeRuleDataFormatId!Key">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="cbCodeRuleDataFormat" runat="server" DataSourceID="dsCodeRuleDataFormat"
                                                TextField="Name" ValueField="CodeRuleDataFormatId">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Name="RepeaterType" Caption="Qui luật lặp" 
                                    RequiredMarkDisplayMode="Required" FieldName="RuleRepeaterTypeId!Key">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="cbRuleRepeaterType" runat="server" DataSourceID="dsRuleRepeaterType"
                                                TextField="Name" ValueField="RuleRepeaterTypeId">
                                                <ValidationSettings>
                                                    <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" IsRequired="true" />
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="dsCodeRuleDataType" runat="server" TypeName="NAS.DAL.System.ArtifactCode.CodeRuleDataType">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsCodeRuleDataFormat" runat="server" TypeName="NAS.DAL.System.ArtifactCode.CodeRuleDataFormat">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsRuleRepeaterType" runat="server" TypeName="NAS.DAL.System.ArtifactCode.RuleRepeaterType">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsCodeRuleData" runat="server">
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
