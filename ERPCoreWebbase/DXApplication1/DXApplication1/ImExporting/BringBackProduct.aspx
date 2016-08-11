<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="BringBackProduct.aspx.cs" Inherits="WebModule.ImExporting.BringBackProduct" %>
<%@ Register src="~/ImExporting/UserControl/uEdit_backOrderProduct.ascx" tagname="uedit_backorderproduct" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPopupControl ID="popup_detail" runat="server" AllowDragging="True" 
        AllowResize="True" ClientInstanceName="popup_detail" Modal="True" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
        RenderMode="Lightweight" HeaderText="Thông tin trả hàng">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc1:uEdit_backOrderProduct ID="uEdit_backOrderProduct" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="11pt" 
        Text="Danh mục trả hàng">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="grid_bringbacklist" runat="server" AutoGenerateColumns="False">
        <Columns>
            <dx:GridViewDataTextColumn Caption="STT" FieldName="sequenceno" VisibleIndex="0"  Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã trả hàng" FieldName="bringbackid" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã phiếu mua" FieldName="orderid" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Lý do" FieldName="reason" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngày trả hàng" FieldName="bringbackdate" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <%--            <dx:GridViewBandColumn Caption="Chứng từ kèm theo" VisibleIndex="3">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Mã chứng từ" FieldName="voucherid" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>--%>
            <dx:GridViewCommandColumn Caption="File chứng từ" VisibleIndex="5" ButtonType="Image">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="down">
                        <Image ToolTip="Tải xuống">
                            <SpriteProperties CssClass="Sprite_Download" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <%--                </Columns>
            </dx:GridViewBandColumn>--%>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="6" ButtonType="Image">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="add">
                        <Image>
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="delete">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="function(s, e) {
                        if (e.buttonID == 'edit' || e.buttonID == 'add')
                            popup_detail.Show();
                 }"/>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
</asp:Content>
