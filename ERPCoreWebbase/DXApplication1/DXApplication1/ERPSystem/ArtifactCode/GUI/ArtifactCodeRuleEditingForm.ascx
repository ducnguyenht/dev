<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArtifactCodeRuleEditingForm.ascx.cs"
    Inherits="WebModule.ERPSystem.ArtifactCode.GUI.ArtifactCodeRuleEditingForm" %>
<%@ Register Src="CodeRuleDefinitionEditingForm.ascx" TagName="CodeRuleDefinitionEditingForm"
    TagPrefix="uc1" %>
<script type="text/javascript">

    $(document).ready(function () {

        $(CodeRuleDefinitionEditingForm).on(
                CodeRuleDefinitionEditingForm.events.eClosing,
                function (evt) {
                    cpnArtifactCodeRuleEditingForm.PerformCallback('Refresh');
                }
            );

    });

    var ArtifactCodeRuleEditingForm = {

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
            tCancel: 'Cancel'
        },

        Show: function (recordId) {
            var args = '';
            //Set transition
            if (recordId) {
                args = this.transitions.tEdit + '|' + recordId;
            }
            else {
                args = this.transitions.tCreate;
            }

            if (!cpnArtifactCodeRuleEditingForm.InCallback()) {
                cpnArtifactCodeRuleEditingForm.PerformCallback(args);
            }
        },

        Save: function () {
            var validated =
                ASPxClientEdit.ValidateEditorsInContainer(formlayoutArtifactCodeRuleEditingForm.GetMainElement(), null, false);
            if (validated) {
                var args = this.transitions.tSave;
                if (!cpnArtifactCodeRuleEditingForm.InCallback()) {
                    cpnArtifactCodeRuleEditingForm.PerformCallback(args);
                }
            }
        },

        Cancel: function () {
            var args = this.transitions.tCancel;
            if (!cpnArtifactCodeRuleEditingForm.InCallback()) {
                cpnArtifactCodeRuleEditingForm.PerformCallback(args);
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

    function cpnArtifactCodeRuleEditingForm_EndCallback(s, e) {

        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            ArtifactCodeRuleEditingForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }

    }

    function btnSaveArtifactCodeRuleEditingForm_Click(s, e) {
        ArtifactCodeRuleEditingForm.Save();
    }

    function btnCancelArtifactCodeRuleEditingForm_Click(s, e) {
        ArtifactCodeRuleEditingForm.Cancel();
    }

    function popupArtifactCodeRuleEditingForm_Closing(s, e) {
        ArtifactCodeRuleEditingForm.Cancel();
    }

    function treelistCodeRuleData_EndCallback(s, e) {
        if (s.cpCallbackArgs) {
            console.log('start parse json: ' + s.cpCallbackArgs);
            var args = jQuery.parseJSON(s.cpCallbackArgs);

            if (args.command == 'Create') {
                CodeRuleDefinitionEditingForm.Show(args.command, args.nodekey);
            }
            else if (args.command == 'Edit') {
                CodeRuleDefinitionEditingForm.Show(args.command, args.nodekey);
            }

            delete s.cpCallbackArgs;
        }

        if (s.cpEvent) {
            if (s.cpEvent == 'deleted') {
                cpnArtifactCodeRuleEditingForm.PerformCallback('Refresh');
            }
            delete s.cpEvent;
        }

    }

</script>
<dx:ASPxCallbackPanel ID="cpnArtifactCodeRuleEditingForm" runat="server" ClientInstanceName="cpnArtifactCodeRuleEditingForm"
    OnCallback="cpnArtifactCodeRuleEditingForm_Callback">
    <ClientSideEvents EndCallback="cpnArtifactCodeRuleEditingForm_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupArtifactCodeRuleEditingForm" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" HeaderText="Phương pháp tạo mã - Thêm mới"
                Height="480px" Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ShowMaximizeButton="True"
                Width="820px" ShowFooter="True">
                <ClientSideEvents Closing="popupArtifactCodeRuleEditingForm_Closing"></ClientSideEvents>
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
                                    <Image ToolTip="Lưu lại">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                    <ClientSideEvents Click="btnSaveArtifactCodeRuleEditingForm_Click" />
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                    <Image ToolTip="Bỏ qua">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                    <ClientSideEvents Click="btnCancelArtifactCodeRuleEditingForm_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="formlayoutArtifactCodeRuleEditingForm" runat="server" DataSourceID="dsArtifactCodeRule"
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chung">
                                    <Items>
                                        <dx:LayoutItem Caption="Tên phương pháp" FieldName="Name" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtArtifactCodeRuleName" runat="server" Width="170px">
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" IsRequired="true" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Loại chứng từ" FieldName="ArtifactTypeId!Key" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cbbArtifactType" runat="server" DataSourceID="dsArtifactType"
                                                        TextField="Name" ValueField="ArtifactTypeId">
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>"
                                                                IsRequired="true" />
<RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mô tả" FieldName="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtArtifactCodeRuleDescription" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ví dụ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="txtExample" runat="server" Font-Italic="True" 
                                                        ForeColor="#666666">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Chi tiết" Name="Details">
                                    <Items>
                                        <dx:LayoutItem Caption=" " ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTreeList ID="treelistCodeRuleData" 
                                                        ClientInstanceName="treelistCodeRuleData" runat="server" AutoGenerateColumns="False"
                                                        KeyFieldName="CodeRuleDefinitionId" Width="100%" ParentFieldName="ParentCodeRuleDefinitionId"
                                                        OnInitNewNode="treelistCodeRuleData_InitNewNode" 
                                                        OnStartNodeEditing="treelistCodeRuleData_StartNodeEditing" 
                                                        OnNodeDeleting="treelistCodeRuleData_NodeDeleting">
                                                        <Columns>
                                                            <dx:TreeListTextColumn Caption="Thể loại" FieldName="DataType" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Giá trị" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                FieldName="DataValue">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Định dạng" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                FieldName="DataFormat">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                ButtonType="Image" ShowNewButtonInHeader="True" Width="100px">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                    </Image>
                                                                </DeleteButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                    </Image>
                                                                </CancelButton>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dx:TreeListCommandColumn>
                                                        </Columns>
                                                        <Styles>
                                                            <Header HorizontalAlign="Center">
                                                            </Header>
                                                            <CommandCell Spacing="4px">
                                                            </CommandCell>
                                                        </Styles>
                                                        <ClientSideEvents EndCallback="treelistCodeRuleData_EndCallback"></ClientSideEvents>
                                                    </dx:ASPxTreeList>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                            <Border BorderWidth="0px"></Border>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="dsArtifactCodeRule" runat="server" Criteria="[ArtifactCodeRuleId!Key] = ?"
                            TypeName="NAS.DAL.System.ArtifactCode.ArtifactCodeRule">
                            <CriteriaParameters>
                                <asp:Parameter Name="ArtifactCodeRuleId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsArtifactType" runat="server" TypeName="NAS.DAL.System.ArtifactCode.ArtifactType"
                            
                            
                            
                            Criteria="([RowStatus] = -1 Or [RowStatus] &gt; 0s) And [OrganizationId.OrganizationId] = ?" 
                            DefaultSorting="">
                            <CriteriaParameters>
                                <asp:Parameter Name="OrganizationId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc1:CodeRuleDefinitionEditingForm ID="CodeRuleDefinitionEditingForm1" runat="server" />
