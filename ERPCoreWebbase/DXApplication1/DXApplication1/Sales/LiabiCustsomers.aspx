<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="LiabiCustsomers.aspx.cs" Inherits="DXApplication1.GUI.LiabiCustsomers" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<%@ Register Src="~/Sales/UserControl/uViewSalesInfo.ascx" TagName="uViewSalesInfo" TagPrefix="uc1" %>
<%@ Register Src="~/Accounting/UserControl/ReceiptVoucherView.ascx" TagName="ReceiptVoucherView" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title></title>
    <script type="text/javascript">
        function click_visible(s, e) {
            gridview_master.SetVisible(1);
            btn_printall.SetVisible(1);
        }

        function click_custom_griddetail(s, e) {
            popup_report.Show();
        }

        function click_custom_printall(s, e) {
            popup_reportprintall.Show();
        }

        function click_link_viewSale(s, e) {
            popup_showevidence.Show();
        }

        function click_link_viewReceiptVoucher(s, e) {
            popup_showeReceiptVoucher.Show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div>
        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" 
            Font-Size="Medium" Height="32px" Text="Báo cáo công nợ" Width="210px">
            <Border BorderStyle="None" />
        </dx:ASPxTextBox>
        <br />
        <dx:ASPxFormLayout ID="formlayout_condition" runat="server">
            <Items>
                <dx:LayoutItem Caption="Ngày bắt đầu">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxDateEdit ID="txt_startdate" runat="server">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày kết thúc">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxDateEdit ID="txt_enddate" runat="server">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxButton ID="btn_view" runat="server" Text="Xem công nợ" AutoPostBack="false">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Grid" />
                                </Image>
                                <ClientSideEvents Click="click_visible" />
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:ASPxFormLayout>
        <dx:ASPxGridView ID="gridview_master" KeyFieldName="customerid" runat="server" 
            AutoGenerateColumns="False" ClientInstanceName="gridview_master" ClientVisible="False"
            Width="100%" onhtmlrowcreated="gridview_master_HtmlRowCreated">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Mã khách" FieldName="customerid" 
                    VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tên khách" FieldName="customername" 
                    VisibleIndex="1">
                    <FooterTemplate>
                        Tổng cộng:
                    </FooterTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewBandColumn Caption="Số dư đầu kì" VisibleIndex="2">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Dư nợ đầu kì" FieldName="firstdebt1" 
                            VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <FooterTemplate>
                                <div style="float: right; margin-left: 4px">
                                    2.360.000.000
                                </div>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Dư có đầu kì" FieldName="firstdebt2" VisibleIndex="1">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <FooterTemplate>
                                <div style="float: right; margin-left: 4px">
                                    0
                                </div>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Phát sinh" VisibleIndex="3">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Phát sinh nợ" FieldName="issue" 
                            VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <FooterTemplate>
                                <div style="float: right; margin-left: 4px">
                                    65.000.000
                                </div>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Phát sinh có" FieldName="payment" 
                            VisibleIndex="1">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <FooterTemplate>
                                <div style="float: right; margin-left: 4px">
                                    1.500.000.000
                                </div>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="Số dư cuối kì" VisibleIndex="4">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Dư nợ cuối kì" FieldName="lastdebt1" 
                            VisibleIndex="0">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <FooterTemplate>
                                <div style="float: right; margin-left: 4px">
                                    835.000.000
                                </div>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Dư có cuối kì" FieldName="lastdebt2" VisibleIndex="1">
                            <CellStyle HorizontalAlign="Right">
                            </CellStyle>
                            <FooterTemplate>
                                <div style="float: right; margin-left: 4px">
                                    0
                                </div>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:GridViewBandColumn>
                <dx:GridViewCommandColumn ButtonType="Button" VisibleIndex="8" Caption="Thao tác">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="print" Text="In công nợ">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <ClientSideEvents CustomButtonClick="click_custom_griddetail" />    
            <Settings ShowFilterRow="True" />
            <SettingsDetail ExportMode="All" ShowDetailRow="True" />
            <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectSingleRowOnly="true" />
            <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
            <Settings ShowFooter="True" />
            <Templates>
                <DetailRow>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxFormLayout ID="form_top"  runat="server">
                            <Items>
                                <dx:LayoutItem Caption="Số dư nợ đầu kì" FieldName="form_top_E1" HorizontalAlign="Right">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="form_top_E1" runat="server" >
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    </div>
                    <dx:ASPxGridView ID="gridview_detail" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewBandColumn Caption="Chứng từ" VisibleIndex="0">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Ngày" FieldName="date" 
                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số" VisibleIndex="1" FieldName="evidenceid">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataTextColumn Caption="Diễn giải" VisibleIndex="1" FieldName="description">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Phát sinh nợ" FieldName="issue" 
                                VisibleIndex="2" CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="link_viewSale" ClientInstanceName="link_viewSale"  Text='<%# Eval("issue") %>' runat="server" 
                                    ClientSideEvents-Click="click_link_viewSale"/>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Phát sinh có" FieldName="payment" 
                                VisibleIndex="3" CellStyle-HorizontalAlign="Right">
                                <CellStyle HorizontalAlign="Right">
                                </CellStyle>
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="link_viewReceiptVoucher" ClientInstanceName="link_viewSale"  Text='<%# Eval("payment") %>' runat="server" 
                                    ClientSideEvents-Click="click_link_viewReceiptVoucher"/>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxFormLayout ID="form_bottom" runat="server">
                        <Items>
                            <dx:LayoutItem Caption="Tổng phát sinh nợ" FieldName="form_bottom_E1" HorizontalAlign="Right">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="form_bottom_E1" runat="server" >
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tổng phát sinh có" FieldName="form_bottom_E2" HorizontalAlign="Right">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="form_bottom_E2" runat="server" >
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Số dư nợ cuối kì" FieldName="form_bottom_E3" HorizontalAlign="Right">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="form_bottom_E3" runat="server" >
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                    </div>
                    <br />
                </DetailRow>
            </Templates>
        </dx:ASPxGridView>

        <dx:ASPxButton ID="btn_printall" AutoPostBack="false" runat="server" Text="In toàn bộ danh sách" 
            ClientInstanceName="btn_printall" ClientVisible="False"
            Width="159px">
            <ClientSideEvents Click="click_custom_printall" />
        </dx:ASPxButton>        
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" 
            RenderMode="Lightweight" ClientInstanceName="popup_report" AllowDragging="True" AllowResize="True" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Maximized="False" ScrollBars="Vertical" HeaderText="Công nợ chi tiết" Width="750px" Height="900px">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ReportToolbar ID="ReportToolbar1" runat="server" 
                            ReportViewerID="ReportViewer1" ShowDefaultButtons="False">
                            <Items>
                                <dx:ReportToolbarButton ItemKind="Search" />
                                <dx:ReportToolbarSeparator />
                                <dx:ReportToolbarButton ItemKind="PrintReport" />
                                <dx:ReportToolbarButton ItemKind="PrintPage" />
                                <dx:ReportToolbarSeparator />
                                <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                <dx:ReportToolbarLabel ItemKind="PageLabel" />
                                <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                </dx:ReportToolbarComboBox>
                                <dx:ReportToolbarLabel ItemKind="OfLabel" />
                                <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                <dx:ReportToolbarButton ItemKind="NextPage" />
                                <dx:ReportToolbarButton ItemKind="LastPage" />
                                <dx:ReportToolbarSeparator />
                                <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                    <Elements>
                                        <dx:ListElement Value="pdf" />
                                        <dx:ListElement Value="xls" />
                                        <dx:ListElement Value="xlsx" />
                                        <dx:ListElement Value="rtf" />
                                        <dx:ListElement Value="mht" />
                                        <dx:ListElement Value="html" />
                                        <dx:ListElement Value="txt" />
                                        <dx:ListElement Value="csv" />
                                        <dx:ListElement Value="png" />
                                    </Elements>
                                </dx:ReportToolbarComboBox>
                            </Items>
                            <Styles>
                                <LabelStyle>
                                <Margins MarginLeft="3px" MarginRight="3px" />
                                </LabelStyle>
                            </Styles>
                        </dx:ReportToolbar>
                        <dx:ReportViewer ID="ReportViewer1" runat="server" 
                            Report="<%# new BienBanDoiChieuCongNo() %>" 
                            ReportName="BienBanDoiChieuCongNo">
                        </dx:ReportViewer>
                        <br/>
                    </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="ASPxPopupControl2" runat="server" ClientInstanceName="popup_reportprintall"
            RenderMode="Lightweight" AllowDragging="True" AllowResize="True" 
            HeaderText="Danh sách công nợ" Height="900px" Modal="True" 
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
            ScrollBars="Vertical" Width="775px">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ReportToolbar ID="ReportToolbar2" runat="server" ShowDefaultButtons="False">
                        <Items>
                            <dx:ReportToolbarButton ItemKind="Search" />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton ItemKind="PrintReport" />
                            <dx:ReportToolbarButton ItemKind="PrintPage" />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                            <dx:ReportToolbarLabel ItemKind="PageLabel" />
                            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                            </dx:ReportToolbarComboBox>
                            <dx:ReportToolbarLabel ItemKind="OfLabel" />
                            <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                            <dx:ReportToolbarButton ItemKind="NextPage" />
                            <dx:ReportToolbarButton ItemKind="LastPage" />
                            <dx:ReportToolbarSeparator />
                            <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                            <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                <Elements>
                                    <dx:ListElement Value="pdf" />
                                    <dx:ListElement Value="xls" />
                                    <dx:ListElement Value="xlsx" />
                                    <dx:ListElement Value="rtf" />
                                    <dx:ListElement Value="mht" />
                                    <dx:ListElement Value="html" />
                                    <dx:ListElement Value="txt" />
                                    <dx:ListElement Value="csv" />
                                    <dx:ListElement Value="png" />
                                </Elements>
                            </dx:ReportToolbarComboBox>
                    </Items>
                    <Styles>
                        <LabelStyle>
                        <Margins MarginLeft="3px" MarginRight="3px" />
                        <Margins MarginLeft="3px" MarginRight="3px"></Margins>
                        </LabelStyle>
                    </Styles>
                </dx:ReportToolbar>
                <dx:ReportViewer ID="ReportViewer2" runat="server" 
                        Report="<%# new DXApplication1.GUI.Sales.Report.CongNo_kh() %>" 
                        ReportName="DXApplication1.GUI.Sales.Report.CongNo_kh">
                </dx:ReportViewer>
                </dx:PopupControlContentControl>
        </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
    <dx:aspxpopupcontrol id="popup_showevidence" runat="server" clientinstancename="popup_showevidence"
        allowdragging="True" allowresize="True" headertext="Thông tin chứng từ liên quan"
        modal="True" popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
        rendermode="Lightweight">
        <ContentCollection>
            <dx:PopupControlContentControl  runat="server"   >
                <uc1:uViewSalesInfo ID="uViewSalesInfo1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:aspxpopupcontrol>
    <dx:aspxpopupcontrol id="popup_showeReceiptVoucher" runat="server" clientinstancename="popup_showeReceiptVoucher"
        allowdragging="True" allowresize="false" headertext="Thông tin chứng từ liên quan"
        modal="True" popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
        rendermode="Lightweight" Width="750px" Height="520px" ScrollBars="Auto">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server"   >
                <uc2:ReceiptVoucherView ID="ReceiptVoucherView1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:aspxpopupcontrol>
</asp:Content>
