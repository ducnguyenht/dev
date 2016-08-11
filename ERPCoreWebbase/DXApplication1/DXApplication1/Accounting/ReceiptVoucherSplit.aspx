<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReceiptVoucherSplit.aspx.cs" Inherits="WebModule.Accounting.ReceiptVoucherSplit" %>
<%@ Register src="UserControl/uReceipVoucherSplit.ascx" tagname="uReceipVoucherSplit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
        Font-Size="Medium" Height="35px" Text="Tách Phiếu Thu">
    </dx:ASPxLabel>
    <uc1:uReceipVoucherSplit ID="uReceipVoucherSplit1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftSubmitContainer" runat="server">
    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="Trợ Giúp">
        <Image ToolTip="Trợ giúp">
            <SpriteProperties CssClass="Sprite_Help" />
        </Image>
    </dx:ASPxButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightSubmitContainer" runat="server">
    <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
        ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
        <ClientSideEvents Click="OnBackButtonClick" />
        <Image>
            <SpriteProperties CssClass="Sprite_Backward" />
        </Image>
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
        CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
        <ClientSideEvents Click="OnNextButtonClick" />
        <Image>
            <SpriteProperties CssClass="Sprite_Forward" />
        </Image>
    </dx:ASPxButton>
    <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
        ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất">
        <ClientSideEvents Click="OnFinishButtonClick" />
        <Image>
            <SpriteProperties CssClass="Sprite_Finished" />
        </Image>
    </dx:ASPxButton>
</asp:Content>