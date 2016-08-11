<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DbConnectionConfiguration.aspx.cs" Inherits="WebModule.NAANAdmin.SystemConfig.DbConnectionConfiguration" %>

<%@ Register Src="UserControls/ucDbConnection.ascx" TagName="ucDbConnection" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(DbConfig).on('inprocess', function () {
                ldpnInProcess.Show();
            });
            $(DbConfig).on('endprocess', function () {
                ldpnInProcess.Hide();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLoadingPanel ID="ldpnInProcess" runat="server" 
        ClientInstanceName="ldpnInProcess" HorizontalAlign="Center" Modal="True" 
        Text="Đang xử lý" VerticalAlign="Middle">
    </dx:ASPxLoadingPanel>
    <uc1:ucDbConnection ID="ucDbConnection1" runat="server" />
    </asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="CenterSubmitContainer">
    
    <dx:ASPxButton CausesValidation="false" UseSubmitBehavior="false" AutoPostBack="false"
        ID="btnReset" ClientInstanceName="btnResetDbConfig" runat="server" Text="Khôi phục">
        <Image>
            <SpriteProperties CssClass="Sprite_Refresh" />
        </Image>
        <ClientSideEvents Click="DbConfig.Reset" />
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnTestConnection" UseSubmitBehavior="false" AutoPostBack="false" 
        ClientInstanceName="btnTestConnection" runat="server" Text="Kiểm tra kết nối">
        <Image>
            <SpriteProperties CssClass="Sprite_Database" />
        </Image>
        <ClientSideEvents Click="DbConfig.CheckConnection" />
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnSave" ClientInstanceName="btnSaveDbConfig" AutoPostBack="false"
         runat="server" Text="Lưu cấu hình">
        <Image>
            <SpriteProperties CssClass="Sprite_Apply" />
        </Image>
        <ClientSideEvents Click="DbConfig.Save" />
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnPopulate" ClientInstanceName="btnPopulate" AutoPostBack="false"
         runat="server" Text="Cập nhật dữ liệu đầu">
        <ClientSideEvents Click="DbConfig.Populate" />
    </dx:ASPxButton>
</asp:Content>
