<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uViewIssueCooperativePrinciples.ascx.cs" Inherits="WebModule.Purchasing.UserControl.uViewIssueCooperativePrinciples" %>
<%@ Register src="~/Purchasing/Usercontrol/uEditCooperativePrinciple.ascx" tagname="uEditCooperativePrinciple" tagprefix="uc1" %>
<script type="text/javascript">
    function click_link_showConfig(s, e) {
        popup_editPrinciple.Show();
    }
    </script>
<dx:ASPxPageControl ID="pc_cooperatePrinciple" ClientInstanceName="pc_cooperatePrinciple" 
    runat="server" RenderMode="Lightweight" Width="100%">
    <TabPages>
        <dx:TabPage Text="Công nợ">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxLabel ID="lbl_status_deb" runat="server" Text="Tình trạng công nợ:" Font-Bold="true">
                    </dx:ASPxLabel>
                    <dx:ASPxGridView ID="grv_debtstatus"  runat="server" KeyFieldName="principleid" 
                        OnHtmlDataCellPrepared="grv_debtstatus_HtmlDataCellPrepared" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã nguyên tắc" FieldName="principleid" VisibleIndex="0">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="link_showConfig1" runat="server"
                                        ToolTip="Hiện thông tin hợp tác nguyên tắc" Text='<%# Eval("principleid") %>'>
                                        <ClientSideEvents Click="click_link_showConfig" />
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate>
                                <FooterTemplate>
                                    Tổng cộng
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                                                                                        
                            <dx:GridViewBandColumn Caption="Thời gian áp dụng" 
                                ShowInCustomizationForm="True" VisibleIndex="1">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="fromdate" ShowInCustomizationForm="True" 
                                        VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đến ngày" FieldName="todate" ShowInCustomizationForm="True" 
                                        VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataTextColumn Caption="Hạn ngạch nợ" FieldName="limmitdebt" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Công nợ hiện tại" FieldName="currentdebt" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="status" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <br />
                    <dx:ASPxLabel ID="lbl_grv_debt" runat="server" Text="Danh sách phiếu bán trả chậm" Font-Bold="true">
                    </dx:ASPxLabel>
                    <dx:ASPxGridView ID="grv_debt" runat="server" KeyFieldName="saleid" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã phiếu mua" FieldName="saleid" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã nguyên tắc" FieldName="principleid" VisibleIndex="1">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="link_showConfig1" runat="server" 
                                        ToolTip="Hiện thông tin hợp tác nguyên tắc" Text='<%# Eval("principleid") %>'>
                                        <ClientSideEvents Click="click_link_showConfig" />
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate>
                                <FooterTemplate>
                                    Tổng cộng
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày mua" FieldName="saledate" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số ngày quá hạn" FieldName="numberoverdate" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tổng giá trị phiếu" FieldName="totalsale" VisibleIndex="4">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Lãi suất phạt (%)" FieldName="ratecharge"
                                Width="50px" VisibleIndex="5">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số tiền phạt" FieldName="charge" VisibleIndex="6">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                                <FooterTemplate>
                                    <dx:ASPxTextBox ID="txt_sumcharge_debt" 
                                        Text="8.000.000" runat="server" ReadOnly="true"></dx:ASPxTextBox>
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn Caption="HTNT liên quan" VisibleIndex="7" ButtonType="Image" Visible="false">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="view_cooperativePrinciple">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Document" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFooter="true" />
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Doanh số">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxLabel ID="lbl_grv_SalePrinciple" runat="server" AssociatedControlID="grv_SalePrinciple" Text="Tiến độ doanh số" Font-Bold="true">
                    </dx:ASPxLabel>
                    <dx:ASPxGridView ID="grv_SalePrinciple" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnHtmlDataCellPrepared="grv_SalePrinciple_HtmlDataCellPrepared" >
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã nguyên tắc" FieldName="principleid"
                                ShowInCustomizationForm="True" VisibleIndex="0">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="link_showConfig1" runat="server" 
                                        ToolTip="Hiện thông tin hợp tác nguyên tắc" Text='<%# Eval("principleid") %>'>
                                        <ClientSideEvents Click="click_link_showConfig" />
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate>
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
                                    <dx:ASPxTextBox ID="txt_sumcharge_SalePrinciple" Width="200px"
                                        Text="4.000.000" runat="server" ReadOnly="true"></dx:ASPxTextBox>
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFooter="true" />
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<dx:ASPxPopupControl ID="popup_editPrinciple" 
        ClientInstanceName="popup_editPrinciple" runat="server" 
        RenderMode="Lightweight" HeaderText="Thông tin cấu hình nguyên tắc" 
        Modal="True" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Width="800px" Height="500px">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc1:uEditCooperativePrinciple ID="uEditCooperativePrinciple1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
</dx:ASPxPopupControl>