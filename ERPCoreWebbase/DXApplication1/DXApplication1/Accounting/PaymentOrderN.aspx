<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PaymentOrderN.aspx.cs" Inherits="ERPCore.Accounting.PaymentOrderN" %>

<%@ Register Src="UserControl/upopReceiptVoucher.ascx" TagName="upopReceiptVoucher"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .float_right
        {
            float:right;
        }
        .hd
        {
            display:none;
        }
    </style>
    <script type="text/javascript">
        function but_click(s, e) {
            if (e.buttonID == "Approve") {
                popphieuthu.Show();
                cppt.PerformCallback("ủy nhiệm chi");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Ủy Nhiệm Chi" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table class="style1" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td>
                            <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData" 
                                KeyFieldName="Code" Width="100%" >                                
                                <ClientSideEvents CustomButtonClick = "but_click"/>
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="  " ShowSelectCheckbox="True" VisibleIndex="0">
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="Mã Số UNC" FieldName="Code" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Ngày Lập" FieldName="Date" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn Vị Trả" FieldName="Sent" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="6">                                        
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="Approve" Text="Duyệt">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Approve" />
                                                </Image>                                                
                                            </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn Vị Nhận" FieldName="Receive" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFilterRow="True" />
                            </dx:ASPxGridView>
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxButton ID="Approve_All" runat="server" CssClass="float_right" Text="Duyệt" AutoPostBack = "false">
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc2:upopReceiptVoucher ID="upopReceiptVoucher1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
