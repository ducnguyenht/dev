<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="VATReceiptForPrinting.aspx.cs" Inherits="WebModule.Accounting.VATReceiptForPrinting" %>
<%@ Register src="UserControl/ucReportPopup.ascx" tagname="ucReportPopup" tagprefix="uc1" %>
<%@ Register src="~/Sales/UserControl/uBillingVouchers.ascx" tagname="uBillingVouchers" tagprefix="uc2" %>
<%--<%@ Register src="UserControl/uDeclareBillingVoucher.ascx" tagname="uDeclareBillingVoucher" tagprefix="uc3" %>--%>
<%@ Register src="UserControl/uPopupphieumuaban.ascx" tagname="uPopupphieumuaban" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <cc:ContentTitle ID="ContentTitle1" runat="server" CssClass="content-title" 
        Title="Danh sách hóa đơn GTGT" />

    <dx:ASPxGridView ID="grid_dmorder" runat="server" AutoGenerateColumns="False" KeyFieldName="id"
            Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="vatreceiptid" Caption="Mã hóa đơn" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Mã phiếu bán" FieldName="id" 
                    VisibleIndex="1" SortIndex="3"
                    SortOrder="Ascending" Width="100px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="idkh" VisibleIndex="2"
                    SortIndex="2" SortOrder="Ascending" Width="100px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="tenkh" VisibleIndex="3"
                    SortIndex="1" SortOrder="Ascending">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ngày mua" FieldName="ngaymua" VisibleIndex="4"
                    SortIndex="0" SortOrder="Ascending">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Width="100px" Caption="Trạng thái" FieldName="PrintStatus" 
                    VisibleIndex="5">
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewCommandColumn ButtonType="Image" Caption="Tài liệu" 
                    VisibleIndex="6">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton Text="Chi tiết" ID="bt_detail">
                            <Image ToolTip="Tài liệu">
                                <SpriteProperties CssClass="Sprite_Document" />
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="7" 
                    ButtonType="Image">
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="accountAction">
                            <Image ToolTip="Hạch toán và kê khai thuế">
                                <SpriteProperties CssClass="Sprite_Balance" />
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Print">
                            <Image ToolTip="In hóa đơn GTGT">
                                <SpriteProperties CssClass="Sprite_Print" />
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <uc2:uBillingVouchers ID="uBillingVouchers1" runat="server" />
                </DetailRow>
            </Templates>
            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" 
                ShowHeaderFilterButton="true" />
            <SettingsDetail ShowDetailRow="true" ShowDetailButtons="true" />
            <SettingsEditing Mode="Inline" />
            <Styles>
                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                </Header>
                <HeaderPanel HorizontalAlign="Center">
                </HeaderPanel>
                <CommandColumn HorizontalAlign="Center" Spacing="5px">
                </CommandColumn>
            </Styles>
            <BorderLeft BorderWidth="0px" />
            <BorderRight BorderWidth="0px" />
            <ClientSideEvents CustomButtonClick="function(s,e) { 
                    if(e.buttonID == 'Print') {
                        //popup_declareTax.Show();
                        ERPCore.ShowReportPopupByReportCode('01GTKT3-001','Hóa đơn giá trị gia tăng',null);
                        
                    }
                    else if(e.buttonID == 'bt_detail')
	                {
		                alert('Chức năng này đang trong quá trình xây dựng.');
	                }
                    else if(e.buttonID == 'accountAction')
	                {
		                popmb.Show();
	                }

                }"/>
        </dx:ASPxGridView>
    <uc1:ucReportPopup ID="ucReportPopup1" runat="server" />
    <uc4:uPopupphieumuaban ID="uPopupphieumuaban1" runat="server" />
     <%--<dx:ASPxPopupControl ID="popup_declareTax" runat="server" ClientInstanceName="popup_declareTax"
            Height="500px" Width="1000px" ScrollBars="Auto"
            RenderMode="Classic" AllowDragging="True" AllowResize="True" HeaderText="Thao Tác Khai Báo Thuế Cho Hóa Đơn"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowFooter="true">
            <ContentCollection>
                <dx:PopupControlContentControl>
                    <uc3:uDeclareBillingVoucher ID="uDeclareBillingVoucher1" runat="server" />
                </dx:PopupControlContentControl>
            </ContentCollection>
            <FooterContentTemplate>
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                                <div style="float: left; margin-right: 4px">
                                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Help" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                <div style="double: right; margin-left: 4px">
                                    <dx:ASPxButton ID="btnCancel" clientinstancename="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                 <div style="double: right; margin-left: 4px">
                                    <dx:ASPxButton ID="btnApply" clientinstancename="btnApply" runat="server" Text="Lưu lại">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </FooterContentTemplate>
        </dx:ASPxPopupControl>--%>
</asp:Content>
