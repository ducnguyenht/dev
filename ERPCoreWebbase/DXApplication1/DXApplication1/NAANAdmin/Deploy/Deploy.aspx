<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Deploy.aspx.cs" Inherits="WebModule.NAANAdmin.Deploy.Deploy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục ứng dụng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:ASPxGridView ID="gvApplication" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <ClientSideEvents CustomButtonClick="gvApplication_CustomButton
" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Thể loại" FieldName="Type" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" 
                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" 
                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                VisibleIndex="7" Width="120px">
                <clearfilterbutton visible="True">
                </clearfilterbutton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit">
                        <Image ToolTip="Triển khai">
                            <SpriteProperties CssClass="Sprite_Publish" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
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
