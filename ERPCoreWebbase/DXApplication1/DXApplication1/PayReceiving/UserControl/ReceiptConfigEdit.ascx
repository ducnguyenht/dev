<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReceiptConfigEdit.ascx.cs" Inherits="ERPCore.PayReceiving.UserControl.ReceiptConfigEdit" %>
<style type="text/css">
    .style5
    {
        height: 16px;
    }
    .style7
    {        
        height: 16px;
    }
    .style11
    {
        height: 16px;
    }
    .style12
    {
        width: 483px;
    }
    .style21
    {
        height: 16px;
        width: 356px;
    }
    .style22
    {
        height: 16px;
        width: 38px;
    }
    .style25
    {
        width: 589px;
    }
</style>

<div id="lineContainer"> 
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine" oncallback="cpLine_Callback">
    <ClientSideEvents EndCallback="cpLine_EndCallback" />
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formReceiptConfigEdit" runat="server" 
            HeaderText="Cập Nhật Cấu Hình Phân Loại Thu" Height="617px" Modal="True" 
            RenderMode="Lightweight"  
            Width="850px" ClientInstanceName="formReceiptConfigEdit" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        Height="520px" RenderMode="Lightweight" Width="100%">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx" 
                                                    style="height: 470px; width: 100%;">
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                Text="Mã Số" >
                                                            </dx:ASPxLabel>
                                                            <span style="color:Red">&nbsp;* </span>
                                                        </td>
                                                        <td class="style12">
                                                            <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" 
                                                                NullText="Tối đa 128 ký tự" 
                                                                 Width="200px" 
                                                                MaxLength="128">
                                                                <NullTextStyle ForeColor="Silver">
                                                                </NullTextStyle>
                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                    <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td class="style12">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Trạng Thái" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="2">
                                                            <dx:ASPxComboBox ID="cboRowStatus" runat="server" NullText="Tự động thêm mới" 
                                                                Width="200px" ClientInstanceName="cboRowStatus">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Đang hoạt động" Value="A" />
                                                                    <dx:ListEditItem Text="Ngưng hoạt động" Value="&quot;I&quot;" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                Text="Tên Thể Loại" >
                                                            </dx:ASPxLabel>
                                                            <span style="color:Red">&nbsp;* </span>
                                                        </td>
                                                        <td class="style5" colspan="2">
                                                            <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" 
                                                                NullText="255 ký tự, không cho phép trùng lắp" 
                                                                 Width="400px" 
                                                                MaxLength="255">
                                                                <NullTextStyle ForeColor="Silver">
                                                                </NullTextStyle>
                                                                <ValidationSettings SetFocusOnError="True" ErrorText="">
                                                                    <RequiredField ErrorText="Chưa nhập T6en Nhá Cung Cấp" IsRequired="True" />
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                        </td>
                                                        <td class="style11" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                                Text="<%$ Resources:Resources, uItemEdit_ASPxLabel3_Text %>" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style5" colspan="2">
                                                            <dx:ASPxTextBox ID="txtDescription" runat="server" 
                                                                ClientInstanceName="txtDescription" Height="140px" 
                                                                NullText="1000 ký tự"  
                                                                Width="400px" MaxLength="1000">
                                                                <NullTextStyle ForeColor="Silver">
                                                                </NullTextStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td class="style5" colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                        </td>
                                                        <td class="style5" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7" colspan="3">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5" colspan="3">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5" colspan="3">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5" colspan="3">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7" colspan="3">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5" colspan="3">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7" colspan="3">
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Italic="True" 
                                                                ForeColor="Gray" Text="<%$ Resources:Resources, uItemEdit_ASPxLabel6_Text %>" 
                                                                >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                    <table style="width:100%; margin-top:10px">
                        <tr>
                            <td>
                                <table align="right" style="width:100%;">
                                <tr>
                                    <td align="left" class="style25">
                                        <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                                            Text="Trợ Giúp"  Wrap="False">
                                            <Image ToolTip="Trợ giúp">
                                                <SpriteProperties CssClass="Sprite_Help" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonSave" Text="Lưu Lại" 
                                             Wrap="False">
                                            <ClientSideEvents Click="buttonSave_Click" />
                                            <Image ToolTip="Lưu">
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonCancel" Text="Bỏ Qua" 
                                             Wrap="False">
                                            <ClientSideEvents Click="buttonCancel_Click" />
                                            <Image ToolTip="Bỏ qua">
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </dx:ASPxButton>
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
</div>
          

            
        