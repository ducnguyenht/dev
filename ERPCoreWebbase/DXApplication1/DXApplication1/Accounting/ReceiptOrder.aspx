<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ReceiptOrder.aspx.cs" Inherits="WebModule.Accounting.ReceiptOrder" %>

<%@ Register Src="UserControl/uDuyetThuChi.ascx" TagName="uDuyetThuChi" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .float_right
        {
            float: right;
        }
    </style>
    <script type="text/javascript">
        function grdData_EndCallback(s, e) {
            //formPaymentOrderEdit.Show();
        }
        function grdata_rowclick(s, e) {
            popduyetthuchi.Show();
            cpthuchi.PerformCallback("unt");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Ủy Nhiệm Thu Tự Động" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
                    OnStartRowEditing="grdData_StartRowEditing" KeyFieldName="Code" Width="100%"
                    OnCustomCallback="grdData_CustomCallback" OnInitNewRow="grdData_InitNewRow">
                    <ClientSideEvents EndCallback="grdData_EndCallback" RowClick="grdata_rowclick" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mã Số UNT" FieldName="Code" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày Lập" FieldName="Date" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Đơn Vị Trả" FieldName="Sent" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="dg" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="status" ShowInCustomizationForm="True" Caption="Trạng thái"
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sơ đồ định khoản" FieldName="sd" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn Caption="  " ShowSelectCheckbox="True" VisibleIndex="7">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <uc3:uDuyetThuChi ID="uDuyetThuChi1" runat="server" />
                <dx:ASPxButton ID="ASPxButton1" runat="server" CssClass="float_right" Text="Duyệt">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Approve" />
                    </Image>
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</asp:Content>
