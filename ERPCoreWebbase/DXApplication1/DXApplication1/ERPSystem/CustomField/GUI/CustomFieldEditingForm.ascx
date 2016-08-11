<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="CustomFieldEditingForm.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.CustomFieldEditingForm" %>

<script type="text/javascript">


    var CustomFieldEditingForm = {

        //declare properties
        events: {
            eShown: 'shown',
            eClosing: 'closing'
        },

        transitions: {
            tCreate: 'Create',
            tEdit: 'Edit',
            tCancel: 'Cancel',
            tSave: 'Save',
            tAccept: 'Accept'
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

            if (!cpnCustomFieldEditingForm.InCallback()) {
                cpnCustomFieldEditingForm.PerformCallback(args);
            }
        },

        Save: function () {
            var args = this.transitions.tSave;
            if (!cpnCustomFieldEditingForm.InCallback()) {
                cpnCustomFieldEditingForm.PerformCallback(args);
            }
        },

        Cancel: function () {
            var args = this.transitions.tCancel;
            if (!cpnCustomFieldEditingForm.InCallback()) {
                cpnCustomFieldEditingForm.PerformCallback(args);
            }
        },

        EndCallback: function (args) {
            switch (args.transition) {
                case this.transitions.tCancel:
                    console.log('args.success: ' + args.success);
                    if (args.success) {
                        console.log('triggle closing event');
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.transitions.tAccept:
                    console.log('args.success: ' + args.success);
                    if (args.success) {
                        console.log('triggle closing event');
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.transitions.tEdit:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                case this.transitions.tCreate:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                default:
                    break;
            }
        }
    }; 

    function cpnObjectTypeEditForm_EndCallback(s, e) {
        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            CustomFieldEditingForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }
    }


    function pagCustomFieldEditingForm_ActiveTabChanged(s, e) {
        var args = 'TabChanged';
        if (!cpnCustomFieldEditingForm.InCallback()) {
            cpnCustomFieldEditingForm.PerformCallback(args);
        }
    }
    function CustomFieldEditingForm_btnSave_Click(s, e) {
        CustomFieldEditingForm.Save();
    }
    function CustomFieldEditingForm_btnCancel_Click(s, e) {
        CustomFieldEditingForm.Cancel();
    }
    function popupCustomFieldEditingForm_Closing(s, e) {
        CustomFieldEditingForm.Cancel();
    }
    function CustomFieldEditingForm_btnBackward_Click(s, e) {
        var args = 'Back';
        if (!cpnCustomFieldEditingForm.InCallback()) {
            cpnCustomFieldEditingForm.PerformCallback(args);
        }
    }
    function CustomFieldEditingForm_btnForward_Click(s, e) {
        var args = 'Next';
        if (!cpnCustomFieldEditingForm.InCallback()) {
            cpnCustomFieldEditingForm.PerformCallback(args);
        }
    }
    function CustomFieldEditingForm_btnFinish_Click(s, e) {
        var args = 'Accept';
        if (!cpnCustomFieldEditingForm.InCallback()) {
            cpnCustomFieldEditingForm.PerformCallback(args);
        }
    }
    function CustomFieldEditingForm_cbbCustomFieldType_SelectedIndexChanged(s, e) {
        var args = 'DataTypeChanged';
        if (!cpnCustomFieldEditingForm.InCallback()) {
            cpnCustomFieldEditingForm.PerformCallback(args);
        }
    }
</script>

<dx:ASPxCallbackPanel ID="cpnCustomFieldEditingForm" runat="server" ClientInstanceName="cpnCustomFieldEditingForm"
    Width="100%" oncallback="cpnCustomFieldEditingForm_Callback">
    <ClientSideEvents EndCallback="cpnObjectTypeEditForm_EndCallback" />
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupCustomFieldEditingForm" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" HeaderText="Cấu hình trường dữ liệu"
                Height="480px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                RenderMode="Lightweight" ShowFooter="True" ShowMaximizeButton="True" Maximized="true" Width="860px">
                <ClientSideEvents Closing="popupCustomFieldEditingForm_Closing" />
                <FooterTemplate>
                    <div style="padding: 10px;">
                        <div style="float: left">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnHelp" runat="server" Text="Trợ giúp"  AutoPostBack="false">
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
                                    <ClientSideEvents Click="CustomFieldEditingForm_btnSave_Click" />
                                    <Image ToolTip="Lưu lại">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnBackward" runat="server" Text="Lùi lại" AutoPostBack="false">
                                    <ClientSideEvents Click="CustomFieldEditingForm_btnBackward_Click" />
                                    <Image ToolTip="Lùi lại">
                                        <SpriteProperties CssClass="Sprite_Backward" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnForward" runat="server" Text="Tiếp theo" AutoPostBack="false">
                                    <ClientSideEvents Click="CustomFieldEditingForm_btnForward_Click" />
                                    <Image ToolTip="Tiếp theo">
                                        <SpriteProperties CssClass="Sprite_Forward" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnFinish" runat="server" Text="Hoàn tất" AutoPostBack="false">
                                    <ClientSideEvents Click="CustomFieldEditingForm_btnFinish_Click" />
                                    <Image ToolTip="Hoàn tất">
                                        <SpriteProperties CssClass="Sprite_Finished" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" CausesValidation="false" runat="server" Text="Thoát" AutoPostBack="false">
                                    <ClientSideEvents Click="CustomFieldEditingForm_btnCancel_Click" />
                                    <Image ToolTip="Thoát">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPageControl ID="pagCustomFieldEditingForm" runat="server" 
                            ActiveTabIndex="1" RenderMode="Classic"
                            Width="100%">
                            <ClientSideEvents ActiveTabChanged="pagCustomFieldEditingForm_ActiveTabChanged" />
                            <TabPages>
                                <dx:TabPage Text="Thông tin chung">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxFormLayout Width="100%" ID="formlayoutGeneralInfo" runat="server" 
                                                DataSourceID="dsCustomField">
                                                <Items>
                                                    <dx:LayoutItem Caption="Tên trường" RequiredMarkDisplayMode="Required" 
                                                        FieldName="Name">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtCustomFieldName" runat="server" Width="170px">
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Fill %>" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Loại dữ liệu" RequiredMarkDisplayMode="Required" 
                                                        FieldName="CustomFieldTypeId!Key" Width="100%">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="cbbCustomFieldType" runat="server" 
                                                                    DataSourceID="dsCustomFieldType" IncrementalFilteringMode="Contains" 
                                                                    TextFormatString="{0}" ValueField="CustomFieldTypeId">
                                                                    <ClientSideEvents SelectedIndexChanged="CustomFieldEditingForm_cbbCustomFieldType_SelectedIndexChanged" />
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Loại dữ liệu" FieldName="Name" Width="170px" />
                                                                        <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" Width="300px" />
                                                                    </Columns>
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Dữ liệu">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="gridviewListData" runat="server" 
                                                AutoGenerateColumns="False" Width="100%" DataSourceID="dsCustomFieldData" 
                                                KeyFieldName="CustomFieldDataId" OnRowInserting="gridviewListData_RowInserting">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Dữ liệu" ShowInCustomizationForm="True" 
                                                        VisibleIndex="0" Name="DataValue">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="100px">
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
                                                </Columns>
                                                <SettingsPager>
                                                    <PageSizeItemSettings Items="10, 20, 50" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="Inline" />
                                                <Styles>
                                                    <Header Font-Bold="True" HorizontalAlign="Center">
                                                    </Header>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                        <dx:XpoDataSource ID="dsCustomField" runat="server" 
                            TypeName="NAS.DAL.CMS.ObjectDocument.CustomField">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsCustomFieldType" runat="server" 
                            TypeName="NAS.DAL.CMS.ObjectDocument.CustomFieldType" DefaultSorting="Name">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsCustomFieldData" runat="server" 
                            TypeName="NAS.DAL.CMS.ObjectDocument.CustomFieldDataString" 
                            DefaultSorting="" Criteria="[CustomFieldDataId] = ?">
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
