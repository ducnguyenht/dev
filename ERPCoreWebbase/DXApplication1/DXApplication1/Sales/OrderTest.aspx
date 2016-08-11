<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="OrderTest.aspx.cs" Inherits="WebModule.GUI.Sales.OrderTest" %>
<%@ Register src="UserControl/PopupOrder/ucPopupOrder.ascx" tagname="ucPopupOrder" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <script type="text/javascript">
        function cmdBillActor_Click(s, e) {
            formBillActor.ShowAtElementByID('popupAnchor');
            formBillActor.PerformCallback();
        }
    </script>
    <uc1:ucPopupOrder ID="ucPopupOrder1" runat="server" />
</asp:Content>
