<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CustomerType.aspx.cs" Inherits="WebModule.NAANAdmin.Customer.CustomerType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục loại khách hàng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:ASPxGridView ID="gvCustomerType" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Tên" FieldName="Name" VisibleIndex="0" 
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                VisibleIndex="1" Width="250px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" 
                ReadOnly="True" VisibleIndex="2" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                VisibleIndex="3" Width="120px">
                <editbutton visible="True">
                    <image tooltip="Sửa">
                        <spriteproperties cssclass="Sprite_Edit" />
                    </image>
                </editbutton>
                <newbutton visible="True">
                    <image tooltip="Thêm">
                        <spriteproperties cssclass="Sprite_New" />
                    </image>
                </newbutton>
                <deletebutton visible="True">
                    <image tooltip="Xóa">
                        <spriteproperties cssclass="Sprite_Delete" />
                    </image>
                </deletebutton>
                <clearfilterbutton visible="True">
                </clearfilterbutton>
            </dx:GridViewCommandColumn>
        </Columns>
        <settingsediting mode="PopupEditForm" />
        <settings showfilterrow="True" showfilterrowmenu="True" 
            showheaderfilterbutton="True" />
        <settingspopup>
            <editform height="150px" width="500px" />
            <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
        </settingspopup>
    </dx:ASPxGridView>
</asp:Content>
