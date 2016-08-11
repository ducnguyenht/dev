<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllocationEditingForm.ascx.cs"
    Inherits="WebModule.Accounting.AllocationConfigure.GUI.AllocationEditingForm" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<script type="text/javascript">
    var AllocationEditingForm = {

        //declare properties
        events: {
            eShown: 'shown',
            eSaved: 'saved',
            eClosing: 'closing'
        },

        actions: {
            tEdit: 'Edit',
            tCreate: 'Create',
            tSave: 'Save',
            tCancel: 'Cancel'
        },

        Show: function (recordId) {

            var args = '';
            if (recordId) {
                args = this.actions.tEdit + '|' + recordId;
            }
            else {
                args = this.actions.tCreate;
            }
            if (!cpnAllocationEditingForm.InCallback()) {
                cpnAllocationEditingForm.PerformCallback(args);
            }
        },

        Save: function () {
            var validated =
                ASPxClientEdit.ValidateEditorsInContainer(formlayoutAllocationEditingForm.GetMainElement(), null, false);
            if (validated) {
                var args = this.actions.tSave;
                if (!cpnAllocationEditingForm.InCallback()) {
                    cpnAllocationEditingForm.PerformCallback(args);
                }
            }
        },

        Cancel: function () {
            var args = this.actions.tCancel;
            if (!cpnAllocationEditingForm.InCallback()) {
                cpnAllocationEditingForm.PerformCallback(args);
            }
        },

        EndCallback: function (args) {
            switch (args.transition) {
                case this.actions.tCancel:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.actions.tCreate:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.actions.tEdit:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.actions.tSave:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eSaved);
                    }
                    break;
                default:
                    break;
            }
        }

    };

    function cpnAllocationEditingForm_EndCallback(s, e) {
        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            AllocationEditingForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }

    }

    function btnSave_Click(s, e) {
        AllocationEditingForm.Save();
    }

    function btnCancel_Click(s, e) {
        AllocationEditingForm.Cancel();
    }

    function popupAllocationEditingForm_Closing(s, e) {
        AllocationEditingForm.Cancel();
    }
</script>
<dx:ASPxCallbackPanel ID="cpnAllocationEditingForm" runat="server" ClientInstanceName="cpnAllocationEditingForm"
    Width="100%" OnCallback="cpnAllocationEditingForm_Callback">
    <ClientSideEvents EndCallback="cpnAllocationEditingForm_EndCallback" />
    <ClientSideEvents EndCallback="cpnAllocationEditingForm_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupAllocationEditingForm" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" Height="480px"
                Maximized="True" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                RenderMode="Lightweight" ShowFooter="True" ShowMaximizeButton="True" Width="860px">
                <ClientSideEvents Closing="popupAllocationEditingForm_Closing" />
                <ClientSideEvents Closing="popupAllocationEditingForm_Closing"></ClientSideEvents>
                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <FooterTemplate>
                    <div style="padding: 10px;">
                        <div style="float: left">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnHelp" runat="server" CausesValidation="false" Text="Trợ giúp"
                                    AutoPostBack="false">
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
                                    <ClientSideEvents Click="btnSave_Click" />
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" CausesValidation="false"
                                    AutoPostBack="false">
                                    <Image ToolTip="Bỏ qua">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                    <ClientSideEvents Click="btnCancel_Click" />
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="formlayoutAllocationEditingForm" ClientInstanceName="formlayoutAllocationEditingForm"
                            runat="server" DataSourceID="dsAllocation" Width="100%" ColCount="2">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chung" Width="50%">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã phân bổ" FieldName="Code" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtCode" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên phân bổ" FieldName="Name" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtName" runat="server" Width="170px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Loại phân bổ" FieldName="AllocationTypeId!Key" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cboAllocationType" runat="server" DataSourceID="dsAllocationType"
                                                        EnableCallbackMode="True" IncrementalFilteringMode="Contains" TextField="Name"
                                                        ValueType="System.Guid" ValueField="AllocationTypeId">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mô tả" FieldName="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtDescription" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Thông tin loại đối tượng phân bổ" Width="50%">
                                    <Items>
                                        <dx:LayoutItem Caption="Loại đối tượng" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cbIsMasterAccountActorType" runat="server" DataSourceID="dsAccountActorType"
                                                        TextField="Name" ValueField="AccountActorTypeId" ValueType="System.Guid">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Loại đối tượng liên quan">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridLookup ID="gridlookupAccountActorType" runat="server" AutoGenerateColumns="False"
                                                        DataSourceID="dsAccountActorType" KeyFieldName="AccountActorTypeId" SelectionMode="Multiple"
                                                        Width="100%" IncrementalFilteringMode="Contains">
                                                        <GridViewProperties>
                                                            <SettingsBehavior AllowSelectByRowClick="True" />
                                                        </GridViewProperties>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                                VisibleIndex="2">
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Caption="Loại đối tượng" FieldName="Name" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridLookup>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Cấu hình định khoản" ColSpan="2">
                                    <Items>
                                        <dx:LayoutItem Caption=" " ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="gridviewAllocationAccountTemplate" runat="server" AutoGenerateColumns="False"
                                                        DataSourceID="dsAllocationAccountTemplate" KeyFieldName="AllocationAccountTemplateId"
                                                        Width="100%" OnCustomColumnDisplayText="gridviewAllocationAccountTemplate_CustomColumnDisplayText"
                                                        OnInitNewRow="gridviewAllocationAccountTemplate_InitNewRow" OnRowInserting="gridviewAllocationAccountTemplate_RowInserting"
                                                        OnCellEditorInitialize="gridviewAllocationAccountTemplate_CellEditorInitialize">
                                                        <Columns>
                                                            <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="3"
                                                                Width="100px" ButtonType="Image">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                    </Image>
                                                                </DeleteButton>
                                                                <ClearFilterButton Visible="True">
                                                                    <Image ToolTip="Hủy">
                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                    </Image>
                                                                </ClearFilterButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                    </Image>
                                                                </UpdateButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                    </Image>
                                                                </CancelButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="CardSite" ShowInCustomizationForm="True" VisibleIndex="1"
                                                                Caption="Phát sinh" Width="100px">
                                                                <EditItemTemplate>
                                                                    <dx:ASPxRadioButtonList SelectedIndex="0" ID="ASPxRadioButtonList1" runat="server" ItemSpacing="4px"
                                                                        RepeatDirection="Horizontal" Value='<%# Bind("CardSite") %>' ValueType="System.Char">
                                                                        <Items>
                                                                            <dx:ListEditItem Text="Nợ" Value="D" />
                                                                            <dx:ListEditItem Text="Có" Value="C" />
                                                                        </Items>
                                                                        <Border BorderWidth="0px" />
                                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                            <RequiredField IsRequired="True" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                                        </ValidationSettings>
                                                                    </dx:ASPxRadioButtonList>
                                                                </EditItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="Description" ShowInCustomizationForm="True"
                                                                VisibleIndex="2" Caption="Diễn giải">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Tài khoản" FieldName="AccountId!Key" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="20%">
                                                                <PropertiesComboBox EnableCallbackMode="true" CallbackPageSize="10" TextField="Code"
                                                                    ValueField="AccountId" ValueType="System.Guid" IncrementalFilteringMode="StartsWith"
                                                                    LoadDropDownOnDemand="True">
                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                                    </ValidationSettings>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                        </Columns>
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <Settings ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                        <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <Settings ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>
                                                        <Styles>
                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                            </Header>
                                                            <CommandColumn Spacing="4px">
                                                            </CommandColumn>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="dsAllocation" runat="server" TypeName="NAS.DAL.Accounting.Configure.Allocation">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsAllocationType" runat="server" TypeName="NAS.DAL.Accounting.Configure.AllocationType">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsAllocationAccountTemplate" runat="server" TypeName="NAS.DAL.Accounting.Configure.AllocationAccountTemplate">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsAccountActorType" runat="server" TypeName="NAS.DAL.Staging.Accounting.Journal.AccountActorType"
                            DefaultSorting="">
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
