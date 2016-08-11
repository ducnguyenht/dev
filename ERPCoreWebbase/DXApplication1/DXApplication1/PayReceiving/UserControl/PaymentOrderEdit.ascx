<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOrderEdit.ascx.cs"
    Inherits="ERPCore.PayReceiving.UserControl.PaymentOrderEdit" %>
<%@ Register Src="../../Accounting/UserControl/upopReceiptVoucher.ascx" TagName="upopReceiptVoucher"
    TagPrefix="uc1" %>
<style type="text/css">
    .pdt
    {
        margin-top: 5px;
    }
    .float_r
    {
        float: right;
    }
    .pd
    {
        padding-left: 10px;
    }
    .hd
    {
        display: none;
    }
</style>
<dx:aspxcallbackpanel id="ASPxCallbackPanel1" runat="server" width="200px" clientinstancename="uncagri">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <dx:ASPxPopupControl ID="formPaymentOrderEdit" runat="server" Width="850px" ClientInstanceName="formPaymentOrderEdit" CloseAction = "CloseButton"
                HeaderText="Nội dung ủy nhiệm chi" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <dx:ASPxFormLayout Width="100%" ID="ASPxFormLayout1" runat="server" ColCount="2">
                            <Items>
                                <dx:LayoutItem Caption="Mã số ủy nhiệm chi">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Mẫu ủy nhiệm chi">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E20" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ngày">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                            <dx:ASPxDateEdit ID="ASPxFormLayout1_E5" runat="server" Width="100px">
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Loại tiền">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E4" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>                                
                                <dx:LayoutGroup ShowCaption="True" HorizontalAlign="Left" Caption="Đơn vị thụ hưởng"
                                    ColCount="2" ColSpan="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Tên đơn vị thụ hưởng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E3" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:EmptyLayoutItem>
                                        </dx:EmptyLayoutItem>
                                        <dx:LayoutItem Caption="Chứng minh thư/Hộ chiếu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E12" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày cấp">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                    <dx:ASPxDateEdit ID="ASPxFormLayout1_E13" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nơi cấp">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E14" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Số điện thoại">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E11" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Số tài khoản">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngân hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E15" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                    <SettingsItemCaptions HorizontalAlign="Right" />
                                </dx:LayoutGroup>
                                <dx:LayoutItem Caption="Số tiền (bằng số)" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                            <dx:ASPxSpinEdit ID="ASPxFormLayout1_E18" runat="server" Height="21px" Number="0">
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Nội dung" ColSpan="2">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E19" runat="server" Height="21px" Width="500px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutGroup Caption="Chi tiết" ColCount="2" ColSpan="2">
                                    <Items>
                                        <dx:LayoutItem ColSpan="2" ShowCaption="False" Width="100%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">                                                                                     
                                                    <dx:ASPxGridView ID="grdDetail" runat="server" AutoGenerateColumns="False" Width="100%">                                                                                                                                                                                             
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Mã phiếu mua" FieldName="MaPhieuMua" 
                                                                VisibleIndex="0" Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ngày" FieldName="Ngay" VisibleIndex="1" 
                                                                Width="10%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Tài khoản nợ" FieldName="TkNo" 
                                                                VisibleIndex="6" Width="15%">
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thanh Toán" FieldName="ThanhToan" 
                                                                VisibleIndex="3" Width="15%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Dư Nợ" FieldName="DuNo" VisibleIndex="2" 
                                                                Width="15%">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                VisibleIndex="7" Width="10%">                                                                                                            
                                                                <EditButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                    </Image>
                                                                </DeleteButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="DienGiai" 
                                                                VisibleIndex="4" Width="25%">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>                                                        
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutItem ColSpan="2" Caption="Layout Item" CaptionCellStyle-CssClass="hd" HorizontalAlign="Right"
                                    ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <table style = "width:100%">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="Hạch Toán" CssClass = "float_r">
                                                        <Image>
                                                                <SpriteProperties CssClass="Sprite_Approve" />
                                                            </Image>
                                                            <ClientSideEvents Click = "function(s,e){popphieuthu.Show();
                cppt.PerformCallback('ủy nhiệm chi');}" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td style = "width:110px">
                                                        <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" CssClass="float_r pd"
                                                            Text="Xác Nhận">
                                                            <ClientSideEvents Click="function(s, e) {
	                                                                        popuncagri.Show();
                                                                                        }" />
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Apply" />
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                    <CaptionCellStyle CssClass="hd">
                                    </CaptionCellStyle>
                                </dx:LayoutItem>
                            </Items>
                            <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                        </dx:ASPxFormLayout>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:aspxcallbackpanel>
<uc1:upopReceiptVoucher ID="upopReceiptVoucher1" runat="server" />
