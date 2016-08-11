<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PaymentOrder.aspx.cs" Inherits="ERPCore.PayReceiving.PaymentOrder" %>
<%@ Register src="UserControl/PaymentOrderEdit.ascx" tagname="PaymentOrderEdit" tagprefix="uc1" %>
<%@ Register src="UserControl/PaymentOrderConfirm.ascx" tagname="PaymentOrderConfirm" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

    <script type="text/javascript">
        function grdData_EndCallback(s, e) {
            formPaymentOrderEdit.Show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Ủy Nhiệm Chi" 
        Font-Bold="True" Font-Size="Small" Height="35px" Width="134px">            
    </dx:ASPxLabel>
</div>
<table class="style1" cellpadding="0" cellspacing="0">
    <tr>
        <td>
<dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
    ClientInstanceName="grdData"
    onstartrowediting="grdData_StartRowEditing" KeyFieldName="Code" 
    Width="100%" oncustomcallback="grdData_CustomCallback" 
        oninitnewrow="grdData_InitNewRow">
    <ClientSideEvents EndCallback="grdData_EndCallback" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã Số UNC" FieldName="Code" 
            VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ngày Lập" 
            FieldName="Date" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đơn Vị Trả" FieldName="Sent" 
            VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
            VisibleIndex="5">
            <EditButton Visible="True">
                <Image>
                    <SpriteProperties CssClass = "Sprite_Edit" />
                </Image>
            </EditButton>
            <newbutton visible="True">
                <Image>
                    <SpriteProperties CssClass = "Sprite_New" />
                </Image>
            </newbutton>
            <DeleteButton Visible="True">
                <Image>
                    <SpriteProperties CssClass = "Sprite_Delete" />
                </Image>
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" 
            VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đơn Vị Nhận" FieldName="Receive" 
            VisibleIndex="3">
        </dx:GridViewDataTextColumn>
    </Columns>
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
        ShowHeaderFilterButton="True" />
</dx:ASPxGridView>

            </td>
    </tr>
    <tr>
        <td>
         
            
         
        </td>
    </tr>
</table>
<uc1:PaymentOrderEdit ID="PaymentOrderEdit2" runat="server" />
<uc2:PaymentOrderConfirm ID="PaymentOrderConfirm1" runat="server" />
            
</asp:Content>
