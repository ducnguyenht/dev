<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="CustomFieldListing.aspx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.CustomFieldListing" %>

<%@ Register Src="CustomFieldEditingForm.ascx" TagName="CustomFieldEditingForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(CustomFieldEditingForm).on(
                CustomFieldEditingForm.events.eClosing,
                function (evt) {
                    gridviewCustomField.Refresh();
                }
            );
        });

        function gridviewCustomField_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case 'CustomField_Edit':
                    var recordId = s.GetRowKey(e.visibleIndex);
                    CustomFieldEditingForm.Show(recordId);
                    break;
                case 'CustomField_New':
                    CustomFieldEditingForm.Show();
                    break;
                case 'CustomField_Delete':
                    var confirmMsg = confirm('Bạn có chắc chắn muốn xóa bản ghi này không?');
                    if (confirmMsg == true) {
                        s.DeleteRow(e.visibleIndex);
                    }
                    break;
                case 'CustomField_AttachTo':
                    if (!popupCustomFieldAttachment.InCallback()) {
                        popupCustomFieldAttachment.Show();
                        var recordId = s.GetRowKey(e.visibleIndex);
                        var args = 'AttachTo|' + recordId;
                        popupCustomFieldAttachment.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        function popupCustomFieldAttachment_btnSave_Click(s, e) {
            if (!popupCustomFieldAttachment.InCallback()) {
                popupCustomFieldAttachment.PerformCallback('Save');
            }
        }

        function popupCustomFieldAttachment_btnCancel_Click(s, e) {
            popupCustomFieldAttachment.Hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="padding: 10px">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Medium" Text="Danh sách trường động">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxGridView ID="gridviewCustomField" ClientInstanceName="gridviewCustomField" runat="server" AutoGenerateColumns="False"
        DataSourceID="dsCustomField" KeyFieldName="CustomFieldId" OnCustomColumnDisplayText="gridviewCustomField_CustomColumnDisplayText"
        Width="100%" OnCustomButtonInitialize="gridviewCustomField_CustomButtonInitialize"
        OnRowDeleting="gridviewCustomField_RowDeleting">
        <ClientSideEvents CustomButtonClick="gridviewCustomField_CustomButtonClick" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Tên trường" FieldName="Name" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Loại dữ liệu" FieldName="CustomFieldTypeId.Name"
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Phân loại" FieldName="CustomFieldType" VisibleIndex="3">
                <Settings FilterMode="DisplayText" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="5" Width="100px">
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear" />
                    </Image>
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton Text="Edit" ID="CustomField_Edit">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton Text="New" ID="CustomField_New">
                        <Image ToolTip="Thêm">
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton Text="Delete" ID="CustomField_Delete">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton Text="Attach" ID="CustomField_AttachTo">
                        <Image ToolTip="Kết nối với đối tượng">
                            <SpriteProperties CssClass="Sprite_Attachment" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsPager>
            <PageSizeItemSettings Items="10, 20, 50" Visible="True">
            </PageSizeItemSettings>
        </SettingsPager>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
            </Header>
            <CommandColumn Spacing="4px">
            </CommandColumn>
        </Styles>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="dsCustomField" runat="server" TypeName="NAS.DAL.CMS.ObjectDocument.CustomField">
    </dx:XpoDataSource>
    <dx:ASPxPopupControl ID="popupCustomFieldAttachment" runat="server" AllowDragging="True"
        AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="popupCustomFieldAttachment"
        CloseAction="CloseButton" HeaderText="Đính trường động vào loại đối tượng" Height="480px"
        Maximized="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        RenderMode="Lightweight" ShowMaximizeButton="True" Width="860px" ShowFooter="True"
        OnWindowCallback="popupCustomFieldAttachment_WindowCallback">
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
                            <ClientSideEvents Click="popupCustomFieldAttachment_btnSave_Click" />
                            <Image ToolTip="Lưu lại">
                                <SpriteProperties CssClass="Sprite_Apply" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: left; margin-left: 4px">
                        <!-- Places button here -->
                        <dx:ASPxButton ID="btnCancel" CausesValidation="false" runat="server" Text="Thoát"
                            AutoPostBack="false">
                            <ClientSideEvents Click="popupCustomFieldAttachment_btnCancel_Click" />
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
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <dx:XpoDataSource ID="dsCustomFieldItem" runat="server" Criteria="[CustomFieldId] = ?"
                    TypeName="NAS.DAL.CMS.ObjectDocument.CustomField">
                </dx:XpoDataSource>
                <dx:XpoDataSource ID="dsObjectType" runat="server" Criteria="[RowStatus] = 1s" TypeName="NAS.DAL.CMS.ObjectDocument.ObjectType">
                </dx:XpoDataSource>
                <dx:ASPxFormLayout ID="formlayoutCustomFieldAttachment" runat="server" DataSourceID="dsCustomFieldItem">
                    <Items>
                        <dx:LayoutGroup Caption="Thông tin trường động">
                            <Items>
                                <dx:LayoutItem Caption="Tên trường" FieldName="Name">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblCustomFieldName" runat="server">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Loại dữ liệu" FieldName="CustomFieldTypeId.Name">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblCustomFieldType" runat="server">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Phân loại">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblType" runat="server">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Danh sách loại đối tượng">
                            <Items>
                                <dx:LayoutItem Caption=" " ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="gridviewObjectType" runat="server" AutoGenerateColumns="False"
                                                DataSourceID="dsObjectType" KeyFieldName="ObjectTypeId" Width="100%" OnCommandButtonInitialize="gridviewObjectType_CommandButtonInitialize">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Tên loại đối tượng" FieldName="Description" ShowInCustomizationForm="True"
                                                        VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                        VisibleIndex="0" Width="40px">
                                                        <ClearFilterButton Visible="True">
                                                        </ClearFilterButton>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <SettingsPager>
                                                    <PageSizeItemSettings Items="10, 20, 50" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                <Styles>
                                                    <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
                                                    </Header>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <uc1:CustomFieldEditingForm ID="customFieldEditingForm" runat="server" />
</asp:Content>
