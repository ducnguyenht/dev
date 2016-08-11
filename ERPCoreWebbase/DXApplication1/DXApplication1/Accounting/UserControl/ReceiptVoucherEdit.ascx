<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptVoucherEdit.ascx.cs" Inherits="ERPCore.Accounting.UserControl.ReceiptVoucherEdit" %>
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

<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px">
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formReceiptVoucherEdit" runat="server" 
        ClientInstanceName="formReceiptVoucherEdit" HeaderText="Chi Tiết Phiếu Thu" 
        Height="541px" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" Width="800px">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <table class="style1">
                    <tr>
                        <td>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                                Height="467px" RenderMode="Lightweight" Width="100%" 
                                OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                                <TabPages>
                                    <dx:TabPage Text="Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
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
                                                                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Sơ Đồ Định Khoản">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Quyển Số">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Tài Khoản Nợ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Ngày Nộp">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="style29" colspan="2">
                                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Tài Khoản Có">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="ASPxComboBox4" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Người Nộp Tiền">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="600px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Địa Chỉ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="600px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Lý Do Nộp">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="600px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Số Tiền Nộp">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="style3" colspan="2">
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit1" runat="server" Height="21px" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="style3">
                                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Loại Tiền">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="style3">
                                                                        <dx:ASPxComboBox ID="ASPxComboBox6" runat="server">
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
                                                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit2" runat="server" Height="21px" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Tỉ Giá ">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="170px">
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
                                                                        <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Width="600px">
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
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Chứng Từ Gốc">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="right" style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" class="style31">
                                        &nbsp;</td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" class="style1">
                                            <tr>
                                                <td align="right">
                                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Duyệt">
                                                    </dx:ASPxButton>
                                                </td>
                                                <td align="right">
                                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Bỏ Qua">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>

