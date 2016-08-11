<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentVoucherView.ascx.cs" Inherits="WebModule.Accounting.UserControl.PaymentVoucherView" %>
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
    .style31
    {
        width: 532px;
    }
    .style30
    {
        width: 134px;
    }
    .style32
    {
        width: 187px;
    }


    .style26
    {
        width: 120px;
    }
    .style29
    {
        width: 112px;
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
    

</style>


<dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
    Height="467px" RenderMode="Lightweight" Width="100%">
    <TabPages>
        <dx:TabPage Text="Chi Tiết">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0" class="style1">
                                                <tr>
                                                    <td class="style26">
                                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Mã Phiếu Chi">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="ASPxComboBox7" runat="server" ReadOnly="true">
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="style29" colspan="2">
                                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Sơ Đồ Định Khoản">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="ASPxComboBox8" runat="server" ReadOnly="true">
                                                        </dx:ASPxComboBox>
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
                                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Quyển Số">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="style29" colspan="2">
                                                        <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="Tài Khoản Nợ">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="ASPxComboBox9" runat="server" ReadOnly="true">
                                                        </dx:ASPxComboBox>
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
                                                        <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="Ngày Nhận">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ReadOnly="true">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="style29" colspan="2">
                                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Tài Khoản Có">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="ASPxComboBox10" runat="server" ReadOnly="true">
                                                        </dx:ASPxComboBox>
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
                                                        <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="Người Nhận Tiền">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td colspan="4">
                                                        <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Width="600px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                                                        <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="Địa Chỉ">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td colspan="4">
                                                        <dx:ASPxTextBox ID="ASPxTextBox9" runat="server" Width="600px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                                                        <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="Lý Do Nộp">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td colspan="4">
                                                        <dx:ASPxTextBox ID="ASPxTextBox10" runat="server" Width="600px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                                                        <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="Số Tiền Nhận">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="style3" colspan="2">
                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit3" runat="server" Height="21px" Number="0" ReadOnly="true">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="style3">
                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Loại Tiền">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="style3">
                                                        <dx:ASPxComboBox ID="ASPxComboBox6" runat="server" ReadOnly="true">
                                                        </dx:ASPxComboBox>
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
                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit2" runat="server" Height="21px" Number="0" ReadOnly="true">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Tỉ Giá ">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="170px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                                                        <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="600px" ReadOnly="true">
                                                        </dx:ASPxTextBox>
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
                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
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
                                            <dx:ASPxComboBox ID="ASPxComboBox5" runat="server">
                                            </dx:ASPxComboBox>
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
                  