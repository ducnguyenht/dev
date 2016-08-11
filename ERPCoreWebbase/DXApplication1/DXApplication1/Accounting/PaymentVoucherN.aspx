<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PaymentVoucherN.aspx.cs" Inherits="ERPCore.Accounting.PaymentVoucherN" %>

<%@ Register Src="UserControl/PaymentVoucherNEdit.ascx" TagName="PaymentVoucherNEdit"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/upopReceiptVoucher.ascx" TagName="upopReceiptVoucher"
    TagPrefix="uc2" %>
<%@ Register src="UserControl/uChungTuGoc.ascx" tagname="uChungTuGoc" tagprefix="uc3" %>
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
        function bt_click(s, e) {
            switch (e.buttonID) {
                case "Xem":
                    {
                        alert("Hiển thị chứng từ gốc");
                        break;
                    }
                case "btApprove":
                    {                        
                        popphieuthu.Show();
                        cppt.PerformCallback("phiếu chi");
                        break;
                    }
                default: { break; }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Chi" Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
                    KeyFieldName="Code" Width="100%">
                    <ClientSideEvents CustomButtonClick="bt_click" />
                    <Columns>
                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="0">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="Mã Phiếu Chi" FieldName="Code" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Người Nhận Tiền" FieldName="Customer" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Địa Chỉ" FieldName="Address" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="Status" VisibleIndex="5"
                            Width="5%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Sơ đồ định khoản" FieldName="AM" VisibleIndex="6"
                            SortIndex="4" SortOrder="Ascending">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chứng từ gốc" VisibleIndex="7"
                            Width="5%">
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="Xem" Text="Xem">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Document" />
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="8"
                            Width="5%">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btApprove" Text="Duyệt">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <Settings ShowFilterRow="True" />
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsEditing Mode="Inline"></SettingsEditing>
                </dx:ASPxGridView>
                
            </td>
        </tr>
    </table>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                        SupportsDisabledAttribute="True">
                        <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" CssClass="float_right" Text="Duyệt">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Approve" />
                            </Image>
                            <ClientSideEvents Click="function(s, e) {
	                    alert('Duyệt tất cả các chứng từ được chọn');}" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <uc2:upopReceiptVoucher ID="upopReceiptVoucher1" runat="server" />
    <uc3:uChungTuGoc ID="uChungTuGoc1" runat="server" />
</asp:Content>
