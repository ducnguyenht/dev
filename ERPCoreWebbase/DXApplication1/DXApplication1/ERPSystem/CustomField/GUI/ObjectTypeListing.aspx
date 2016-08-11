<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ObjectTypeListing.aspx.cs" Inherits="WebModule.ERPSystem.CustomField.GUI.ObjectTypeListing" %>

<%@ Register Src="ObjectTypeCustomFieldListing.ascx" TagName="ObjectTypeCustomFieldListing"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function gridviewObjectType_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case 'ObjectType_Edit':
                    var recordId = s.GetRowKey(e.visibleIndex);
                    ObjectTypeEditForm.Show(recordId);
                    break;
                default:
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Medium" Text="Danh sách loại đối tượng">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxGridView ID="gridviewObjectType" runat="server" AutoGenerateColumns="False"
        DataSourceID="dsObjectType" KeyFieldName="ObjectTypeId" Width="100%">
        <ClientSideEvents CustomButtonClick="gridviewObjectType_CustomButtonClick" />
        <ClientSideEvents CustomButtonClick="gridviewObjectType_CustomButtonClick"></ClientSideEvents>
        <Columns>
            <dx:GridViewDataTextColumn Caption="Tên loại đối tượng" FieldName="Description" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="3">
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear" />
                        <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                    </Image>
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="ObjectType_Edit" Text="Sửa">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit" />
                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"></Settings>
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center">
            </Header>
            <CommandColumn Spacing="4px">
            </CommandColumn>
        </Styles>
        <Templates>
            <EmptyDataRow>
                <dx:ASPxImage ID="imgEmptyDataNewCommand" Cursor="pointer" runat="server" SpriteCssClass="Sprite_New"
                    ShowLoadingImage="true">
                    <ClientSideEvents Click="" />
                </dx:ASPxImage>
                <br />
                <dx:ASPxLabel ID="lblGridviewEmptyText" runat="server" Text="No data to display">
                </dx:ASPxLabel>
            </EmptyDataRow>
        </Templates>
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="dsObjectType" runat="server" TypeName="NAS.DAL.CMS.ObjectDocument.ObjectType"
        Criteria="[RowStatus] = 1s">
    </dx:XpoDataSource>
    <uc1:ObjectTypeCustomFieldListing ID="ObjectTypeCustomFieldListing1" runat="server" />
</asp:Content>
