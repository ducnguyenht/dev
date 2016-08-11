<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ReceiptVoucher.aspx.cs" Inherits="WebModule.Accounting.ReceiptVoucher" %>

<%@ Register Src="UserControl/uDuyetThuChi.ascx" TagName="uDuyetThuChi" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grdData_EndCallback(s, e) {

        }
        function grdata_rowclick(s, e) {
            popduyetthuchi.Show();
            cpthuchi.PerformCallback("thu");
        }
    </script>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Thu Tự Động" Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
                    OnStartRowEditing="grdData_StartRowEditing" KeyFieldName="ProductId" Width="100%"
                    OnCustomCallback="grdData_CustomCallback" OnInitNewRow="grdData_InitNewRow">
                    <ClientSideEvents EndCallback="grdData_EndCallback" RowClick="grdata_rowclick" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mã Phiếu Thu" FieldName="Code" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Người Nộp Tiền" FieldName="Customer" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Địa Chỉ" FieldName="Address" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="8">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="Sơ đồ định khoản" FieldName="sd" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="dg" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="status" ShowInCustomizationForm="True"
                            VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Chứng Từ Gốc" FieldName="Order" ShowInCustomizationForm="True"
                            VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" />
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:uDuyetThuChi ID="uDuyetThuChi1" runat="server" />
                <dx:ASPxButton ID="ASPxButton1" runat="server" CssClass="float_right" Text="Duyệt">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Approve" />
                    </Image>
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
</asp:Content>
