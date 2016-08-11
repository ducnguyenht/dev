<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReceiptSchedule.aspx.cs" Inherits="ERPCore.PayReceiving.ReceiptSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Lịch Nhắc" 
            Font-Bold="True" Font-Size="Small">            
        </dx:ASPxLabel>
    </div>
    <table class="style1">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <dx:ASPxScheduler ID="ASPxScheduler1" runat="server">
            </dx:ASPxScheduler>
        </td>
    </tr>
</table>
</asp:Content>
