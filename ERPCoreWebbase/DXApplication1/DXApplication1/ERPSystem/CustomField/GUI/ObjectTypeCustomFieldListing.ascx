<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="ObjectTypeCustomFieldListing.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.ObjectTypeCustomFieldListing" %>
<%@ Register Src="CustomFieldEditingForm.ascx" TagName="CustomFieldEditingForm" TagPrefix="uc1" %>
<script type="text/javascript">

    $(document).ready(function () {
        $(CustomFieldEditingForm).on(
                CustomFieldEditingForm.events.eClosing,
                function (evt) {
                    cpnObjectTypeEditForm.PerformCallback(ObjectTypeEditForm.transitions.tRefresh);
                }
            );
    });

    var ObjectTypeEditForm = {

        //declare properties
        events: {
            eShown: 'shown',
            eClosing: 'closing'
        },

        transitions: {
            tEdit: 'Edit',
            tCancel: 'Cancel',
            tRefresh: 'Refresh'
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

            if (!cpnObjectTypeEditForm.InCallback()) {
                cpnObjectTypeEditForm.PerformCallback(args);
            }
        },

        Cancel: function () {
            //            var args = this.transitions.tCancel;
            //            if (!cpnObjectTypeEditForm.InCallback()) {
            //                cpnObjectTypeEditForm.PerformCallback(args);
            //            }
            popupObjectTypeEditing.Hide();
        },

        EndCallback: function (args) {
            switch (args.transition) {
                case this.transitions.tCancel:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eClosing);
                    }
                    break;
                case this.transitions.tEdit:
                    if (args.success) {
                        $(this).triggerHandler(this.events.eShown);
                    }
                    break;
                default:
                    break;
            }
        }
    };

    function imgEmptyDataNewCommand_Click(s, e) {
        CustomFieldEditingForm.Show();
    }

    function gridviewCustomField_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'CustomField_Edit':
                s.GetRowValues(e.visibleIndex, 'CustomFieldId!Key', gridviewCustomField_OnGetRowValues);
                break;
            case 'CustomField_New':
                CustomFieldEditingForm.Show();
                break;
            case 'CustomField_Delete':
                var confirmMsg = confirm('Bạn có chắc chắn muốn xóa bản ghi này không?');
                if (confirmMsg == true) {
                    s.DeleteRow(e.visibleIndex);
                    //                    var recordId = s.GetRowKey(e.visibleIndex);
                    //                    var args = 'Delete|' + recordId;
                    //                    gridviewCustomField.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }

    function gridviewCustomField_OnGetRowValues(values) {
        CustomFieldEditingForm.Show(values);
    }

    function cpnObjectTypeEditForm_EndCallback(s, e) {
        if (s.cpCallbackArgs) {
            var args = jQuery.parseJSON(s.cpCallbackArgs);
            ObjectTypeEditForm.EndCallback(args);
            delete s.cpCallbackArgs;
        }
    }

    function ObjectTypeEditingForm_btnCancel_Click(s, e) {
        ObjectTypeEditForm.Cancel();
    }

    function gridviewCustomField_colCustomFieldId_SelectedIndexChanged(s, e) {
        var comboxCustomFieldSelected = s.GetSelectedItem().GetColumnText('CustomFieldTypeId.Name');
        gridviewCustomField.GetEditor('CustomFieldId.CustomFieldTypeId.Name').SetText(comboxCustomFieldSelected);
    }

</script>
<dx:ASPxCallbackPanel ID="cpnObjectTypeEditForm" runat="server" ClientInstanceName="cpnObjectTypeEditForm"
    Width="100%" OnCallback="cpnObjectTypeEditForm_Callback">
    <ClientSideEvents EndCallback="cpnObjectTypeEditForm_EndCallback" />
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupObjectTypeEditing" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" Height="480px"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                RenderMode="Lightweight" Width="860px" HeaderText="Thông tin trường động liên kết"
                ShowFooter="True" ShowMaximizeButton="True" Maximized="true" ClientInstanceName="popupObjectTypeEditing">
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
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                    <ClientSideEvents Click="ObjectTypeEditingForm_btnCancel_Click" />
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
                        <dx:ASPxFormLayout ID="formlayoutObjectTypeEditing" runat="server" DataSourceID="dsObjectType"
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chung">
                                    <Items>
                                        <dx:LayoutItem Caption="Tên loại" FieldName="Name">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblName" runat="server">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mô tả" FieldName="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblDesciption" runat="server">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Danh sách trường dữ liệu">
                                    <Items>
                                        <dx:LayoutItem Caption=" " ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="gridviewCustomField" ClientInstanceName="gridviewCustomField"
                                                        runat="server" AutoGenerateColumns="False" DataSourceID="dsObjectTypeCustomFields"
                                                        KeyFieldName="ObjectTypeCustomFieldId" Width="100%" OnRowDeleting="gridviewCustomField_RowDeleting"
                                                        OnCustomButtonInitialize="gridviewCustomField_CustomButtonInitialize" OnCommandButtonInitialize="gridviewCustomField_CommandButtonInitialize"
                                                        OnRowInserting="gridviewCustomField_RowInserting" OnRowValidating="gridviewCustomField_RowValidating">
                                                        <ClientSideEvents CustomButtonClick="gridviewCustomField_CustomButtonClick" />
                                                        <Columns>
                                                            <dx:GridViewDataComboBoxColumn Caption="Tên trường" FieldName="CustomFieldId!Key"
                                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                                                <PropertiesComboBox CallbackPageSize="10" DataSourceID="dsCustomField" EnableCallbackMode="True"
                                                                    ValueField="CustomFieldId" TextFormatString="{0}" IncrementalFilteringMode="Contains" ValueType="System.Guid">
                                                                    <ClientSideEvents SelectedIndexChanged="gridviewCustomField_colCustomFieldId_SelectedIndexChanged" />
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Tên trường" FieldName="Name" />
                                                                        <dx:ListBoxColumn Caption="Loại dữ liệu" FieldName="CustomFieldTypeId.Name" />
                                                                    </Columns>
                                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic"> 
                                                                        <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                                                                    </ValidationSettings>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Loại dữ liệu" FieldName="CustomFieldId.CustomFieldTypeId.Name"
                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn Visible="true" ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="2" Width="100px">
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
                                                                <%--<CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="CustomField_Edit">
                                                                        <Image ToolTip="Sửa">
                                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                    <dx:GridViewCommandColumnCustomButton ID="CustomField_New">
                                                                        <Image ToolTip="Thêm">
                                                                            <SpriteProperties CssClass="Sprite_New" />
                                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                    <dx:GridViewCommandColumnCustomButton ID="CustomField_Delete">
                                                                        <Image ToolTip="Xóa">
                                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>--%>
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                                                        <Styles>
                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                            </Header>
                                                        </Styles>
                                                        <%--<Templates>
                                                            <EmptyDataRow>
                                                                <dx:ASPxImage Visible="false" ID="imgEmptyDataNewCommand" Cursor="pointer" runat="server"
                                                                    SpriteCssClass="Sprite_New" ShowLoadingImage="true">
                                                                    <ClientSideEvents Click="imgEmptyDataNewCommand_Click" />
                                                                </dx:ASPxImage>
                                                                <br />
                                                                <dx:ASPxLabel ID="lblGridviewEmptyText" runat="server" Text="No data to display">
                                                                </dx:ASPxLabel>
                                                            </EmptyDataRow>
                                                        </Templates>--%>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="dsObjectType" runat="server" 
                            TypeName="NAS.DAL.CMS.ObjectDocument.ObjectType" DefaultSorting="">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsObjectTypeCustomFields" runat="server" 
                            TypeName="NAS.DAL.CMS.ObjectDocument.ObjectTypeCustomField">
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsCustomField" runat="server" 
                            TypeName="NAS.DAL.CMS.ObjectDocument.CustomField" DefaultSorting="">
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc1:CustomFieldEditingForm ID="CustomFieldEditingForm1" runat="server" />
