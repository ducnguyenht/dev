<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SourceExistMaterial.aspx.cs" Inherits="WebModule.Warehouse.SourceExistMaterial" %>
<%@ Register src="~/Purchasing/UserControl/uBuyingMaterial.ascx" tagname="uWarehouse" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách nguyên vật liệu" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
    <dx:ASPxGridView ID="gr_data" runat="server" AutoGenerateColumns="False" 
        oninitnewrow="gr_data_InitNewRow">
        <ClientSideEvents EndCallback="function(s, e) {
	if (s.cpNew) {

    }

}" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" 
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngưỡng thấp nhất" FieldName="sourcemin" 
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngưỡng an toàn" FieldName="sourcesafe" 
                VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Chu kỳ" FieldName="cycle" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên NVL" FieldName="name" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã NVL" FieldName="code" VisibleIndex="0">
                <PropertiesTextEdit DisplayFormatString="{0}">
                </PropertiesTextEdit>
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" 
                        Text='<%# Eval("code") %>' Cursor="pointer" >
                        <ClientSideEvents Click="function(s, e) {
	formMaterialEdit.Show();
}" />
                        </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="7" 
                ButtonType="Image">
                <EditButton Visible="True">
                    <Image ToolTip="Sửa">
                        <SpriteProperties CssClass="Sprite_Edit" />
                    </Image>
                </EditButton>
                <NewButton Visible="True">
                    <Image ToolTip="Biểu đồ">
                        <SpriteProperties CssClass="Sprite_Chart" />
                    </Image>
                </NewButton>
                <CancelButton Visible = "true">
                    <Image ToolTip = "Thoát">
                        <SpriteProperties CssClass = "Sprite_Cancel" />
                    </Image>
                </CancelButton>
                <UpdateButton Visible = "true">
                    <Image ToolTip = "Lưu">
                        <SpriteProperties CssClass = "Sprite_Apply" />
                    </Image>
                </UpdateButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="PopupEditForm" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowHeaderFilterButton="True" />
            <SettingsText PopupEditFormCaption = "Chỉnh sửa" />
        <SettingsPopup>
            <EditForm Height="200px" Width="600px" />
        </SettingsPopup>
    </dx:ASPxGridView>
        <div id="clientContainer">       
    <uc1:uWarehouse ID="uWarehouse" runat="server" />       
</div>
</asp:Content>

