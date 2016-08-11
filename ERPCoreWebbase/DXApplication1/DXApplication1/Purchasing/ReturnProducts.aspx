<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReturnProducts.aspx.cs" Inherits="WebModule.Purchasing.ReturnProducts" %>
<%@ Register src="UserControl/uEdit_backOrderProduct.ascx" tagname="uEdit_backOrderProduct" tagprefix="uc1" %>
<%@ Register src="UserControl/uReturnProductsInfo.ascx" tagname="uReturnProductsInfo" tagprefix="uc2" %>
<%@ Register src="../Accounting/UserControl/uBringBackProduct.ascx" tagname="uBringBackProduct" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function ShowInfoclick(s, e) {
            popup_showSummaryInfo.Show();
        }
    </script>
    <style type = "text/css">
        .hd
        {
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">   
<table style="width:100%">
<tr>
    <td style="vertical-align:top">
        <div class="gridContainer">
            <dx:ASPxPageControl ID="pagecontrol_backProduct" runat="server" 
                RenderMode="Classic" Width="100%" Height="100%" ActiveTabIndex="0">
                <TabPages>
                    <dx:TabPage Text="Phiếu Trả Hàng">
                        <ContentCollection>
                            <dx:ContentControl>             
                                <dx:ASPxGridView ID="grid_bringbacklist" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="STT" FieldName="sequenceno" VisibleIndex="0"  Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã Phiếu Trả" FieldName="bringbackid" VisibleIndex="1" Width="150px">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <DataItemTemplate>
                                                <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("bringbackid") %>'>
                                                    <ClientSideEvents Click="ShowInfoclick" />
                                                </dx:ASPxHyperLink>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã nhà cung cấp" FieldName="supplierid" VisibleIndex="2" Width="150px">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên nhà cung cấp" FieldName="suppliername" VisibleIndex="3" Width="150px">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày tạo phiếu" FieldName="bringbackdate" VisibleIndex="4" Width="100px">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="reason" VisibleIndex="5">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn Caption="Thao Tác" VisibleIndex="6" ButtonType="Image" Width="65px">
                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                            <CustomButtons>                                    
                                                <dx:GridViewCommandColumnCustomButton ID="Approve" Text="Hạch toán">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Balance" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="delete">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <ClientSideEvents CustomButtonClick="function(s, e) {
                                                    if (e.buttonID == 'edit' || e.buttonID == 'add'){
                                                        popup_detail.Show();
                                                    }
                                                    if (e.buttonID == 'Approve'){
                                                        popup_approvebr.Show();
                                                    }
                                                }"/>
                                    <Settings ShowFilterRow="True" />
                                    <SettingsBehavior ColumnResizeMode="Control"/>
                                    <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Thao Tác Trả">
                        <ContentCollection>
                            <dx:ContentControl Height="100%">
                               
                                <table class="style1">
                                    <tr>
                                        <td>
                                            <uc1:uEdit_backOrderProduct ID="uEdit_backOrderProduct1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                               
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
         </div>
    </td>
</tr>

    <tr>
        <td>            
            <dx:ASPxPopupControl ID="popup_showSummaryInfo" runat="server" AllowDragging="True"
                AllowResize="True" ClientInstanceName="popup_showSummaryInfo" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowFooter="true" RenderMode="Classic" HeaderText="Thông tin trả hàng"
                Width="850px" Height="600px" ScrollBars="Auto" ShowSizeGrip = "False" ShowMaximizeButton = "true">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <uc2:uReturnProductsInfo ID="uReturnProductsInfo1" runat="server" />
                    </dx:PopupControlContentControl>
                </ContentCollection>                
                <FooterContentTemplate>
                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp" CssClass = "float-left button-left-margin">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btncancel" runat="server" AutoPostBack="False" Text="Bỏ qua" CssClass = "float-right button-right-margin">
                        <ClientSideEvents Click = "function(s,e){popup_showSummaryInfo.Hide();}" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnapprove" runat="server" AutoPostBack="False" Text="Hạch toán" CssClass = "float-right button-right-margin">
                        <ClientSideEvents Click = "function(s,e){popup_approvebr.Show();}" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Balance" />
                        </Image>
                    </dx:ASPxButton>
                </FooterContentTemplate>
            </dx:ASPxPopupControl>
        </td>
    </tr>
</table>
<uc3:uBringBackProduct ID="uBringBackProduct1" runat="server" />    
</asp:Content>




