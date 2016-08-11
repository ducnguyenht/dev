<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uMonitorProcessOfSalesTotalOnDetail.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uMonitorProcessOfSalesTotalOnDetail" %>
<script type="text/javascript">
    function show_uMonitorProcessOfSalesTotalOnDetail(s, e) {
        popup_monitorProcessOfSalesTotalOnDetail.Show();
    }

    function grv_SalePrinciple_custombtn_click(s, e) {
        if (e.buttonID == 'payment') {
            popup_giveCashmonitorProcessOfSalesTotalOnDetail.Show();
        }
    }
</script>
<dx:aspxpopupcontrol id="popup_monitorProcessOfSalesTotalOnDetail" clientinstancename="popup_monitorProcessOfSalesTotalOnDetail" 
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Chi Tiết Tiến Độ Doanh Số - " 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="900px" height="650px" ScrollBars="Auto">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxLabel ID="lbl_grv_SalePrinciple" runat="server" AssociatedControlID="grv_SalePrinciple" Text="Tiến độ doanh số" Font-Bold="true">
            </dx:ASPxLabel>
            <dx:ASPxGridView ID="grv_SalePrinciple" runat="server" AutoGenerateColumns="False" Width="100%"
                OnHtmlDataCellPrepared="grv_SalePrinciple_HtmlDataCellPrepared" >
                <ClientSideEvents CustomButtonClick="grv_SalePrinciple_custombtn_click" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="STT" FieldName="seq"
                        ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Chỉ tiêu DS (VNĐ)" FieldName="objectiveAmount"
                        ShowInCustomizationForm="True" VisibleIndex="1">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <FooterTemplate>Tổng cộng</FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="DS thực (VNĐ)" FieldName="realAmount"
                        ShowInCustomizationForm="True" VisibleIndex="2">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewBandColumn Caption="Thời hạn tính doanh số" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="fromDate" ShowInCustomizationForm="True" 
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đến ngày" FieldName="toDate" ShowInCustomizationForm="True" 
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:GridViewBandColumn>
                    <dx:GridViewDataTextColumn Caption="Tỉ lệ phạt DS chưa đạt (%)" FieldName="rateCharge"
                        ShowInCustomizationForm="True" VisibleIndex="4" Width="50px">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tiền phạt" FieldName="charge"
                        ShowInCustomizationForm="True" VisibleIndex="5">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <FooterTemplate>
                            <dx:ASPxTextBox ID="txt_sumcharge_SalePrinciple" 
                                Text="4.000.000" runat="server" ReadOnly="true"></dx:ASPxTextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tiền trả" FieldName="recharge"
                        ShowInCustomizationForm="True" VisibleIndex="5">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <FooterTemplate>
                            <dx:ASPxTextBox ID="txt_recharge"
                                Text="3.000.000" runat="server" ReadOnly="true"></dx:ASPxTextBox>
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn Caption="Thanh toán" ButtonType="Image">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="payment">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_AssignTo" />
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
                <Settings ShowFooter="true" />
            </dx:ASPxGridView>
            <br />
            <dx:ASPxScheduler ID="ASPxScheduler1" runat="server" ClientIDMode="AutoID" 
                Start="2013-08-24" Width="100%">
                <Views>
                    <DayView>
                        <TimeRulers>
                            <dx:TimeRuler />
                        </TimeRulers>
                        <DayViewStyles ScrollAreaHeight="320px">
                        </DayViewStyles>
                    </DayView>
                    <WorkWeekView>
                        <TimeRulers>
                            <dx:TimeRuler />
                        </TimeRulers>
                    </WorkWeekView>
                </Views>
            </dx:ASPxScheduler>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server">
                    <div style="float: left; margin-right: 4px">
                        <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                            Text="Thoát">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:aspxpopupcontrol>

<dx:aspxpopupcontrol id="popup_giveCashmonitorProcessOfSalesTotalOnDetail" clientinstancename="popup_giveCashmonitorProcessOfSalesTotalOnDetail" 
    runat="server" rendermode="Classic" ShowFooter="false"
    closeaction="CloseButton" headertext="Thanh toán khoản phạt - " 
    modal="True" AllowDragging="true" AllowResize="false"
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="900px" height="160px" ScrollBars="Auto">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Text="Thanh toán tiền phạt">
            </dx:ASPxLabel>
            <dx:ASPxGridView ID="grv_giveCash" runat="server" KeyFieldName="customerid" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Ngày" FieldName="date" ShowInCustomizationForm="True"
                        VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="customerid" ShowInCustomizationForm="True"
                        VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="customername" ShowInCustomizationForm="True"
                        VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Nợ đầu kì" FieldName="firstdebt" ShowInCustomizationForm="True"
                        VisibleIndex="3">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Phát sinh" FieldName="issue" ShowInCustomizationForm="True"
                        VisibleIndex="4">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đã thanh toán" FieldName="payment" ShowInCustomizationForm="True"
                        VisibleIndex="5">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtPayment" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Nợ cuối kì" FieldName="lastdebt" ShowInCustomizationForm="True"
                        VisibleIndex="6" Visible="false">
                        <CellStyle HorizontalAlign="Right">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="7" Caption="Thao tác">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="accept">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:aspxpopupcontrol>