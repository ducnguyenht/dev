<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptVoucherView.ascx.cs" Inherits="WebModule.Accounting.UserControl.ReceiptVoucherView" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        height: 14px;
    }
    .style3
    {
        height: 21px;
    }
    .style26
    {
        width: 120px;
    }
    .style27
    {
        height: 14px;
        width: 120px;
    }
    .style28
    {
        height: 21px;
        width: 120px;
    }
    .style29
    {
        width: 112px;
    }
    .style30
    {
        width: 134px;
    }
    .style31
    {
        width: 532px;
    }
    .style32
    {
        width: 187px;
    }

    </style>


                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                    Height="467px" RenderMode="Lightweight" Width="100%" >
                    <TabPages>
                        <dx:TabPage Text="Chi Tiết">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <table cellpadding="0" cellspacing="0" class="style1">
                                                    <tr>                                                            
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Mã Phiếu Thu">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style29" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td class="style29" colspan="2">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Quyển Số">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style29" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Tài Khoản Nợ">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td class="style29" colspan="2">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Ngày Nộp">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style29" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Tài Khoản Có">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td class="style29" colspan="2">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Người Nộp Tiền">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="4">
                                                            <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Địa Chỉ">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="4">
                                                            <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style27">
                                                        </td>
                                                        <td class="style2" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Lý Do Nộp">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="4">
                                                            <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style28">
                                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Số Tiền Nộp">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style3" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style3">
                                                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Loại Tiền">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style3">
                                                            <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="Số Tiền Qui Đổi">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Tỉ Giá ">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26">
                                                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Diễn Giải">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="4">
                                                            <dx:ASPxLabel ID="ASPxLabel30" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Chứng Từ Gốc">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                    <table cellpadding="0" cellspacing="0" class="style1">
                                        <tr>
                                            <td>
                                                <table class="style1">
                                                    <tr>
                                                        <td class="style30">
                                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Mã Chứng Từ Gốc">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style32">
                                                            <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text=".........">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Xem Chứng Từ Gốc">
                                                            </dx:ASPxHyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="370px">
                                                    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                </dx:ASPxFileManager>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>