<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Delivery.aspx.cs" Inherits="DXApplication1.GUI.Delivery" %>
<%@ Register Src="~/Purchasing/UserControl/uDeliveryEdit.ascx" TagName="uDeliveryEdit"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            border-width: 0px;
            padding: 3px 15px;
        }
    </style>

     <script type="text/javascript">
         function tabchange(s, e) {
             var str = pagetiendo.GetActiveTabIndex();
             grdatatiendo.PerformCallback(str);   
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">

<div style="margin-bottom: 10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Tiến Độ Đơn Hàng" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>    
</div>
<table style="width:100%;">
<tr>
    <td style="vertical-align:top;">
           <div>
                <div style="overflow-y: scroll; overflow-x:hidden ;height: 300px">
                    <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdBuyingProductCategory"
                        KeyFieldName="MaDonHang" ClientInstanceName="grdatatiendo" 
                        oncustomcallback="grdBuyingProductCategory_CustomCallback">
                        <Columns>
                            <dx:GridViewDataComboBoxColumn Width="10%" Caption="Mã Đơn Hàng" VisibleIndex="0"
                                FieldName="MaDonHang">
                                <PropertiesComboBox>
                                    <Columns>
                                        <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
                                        <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                                        </dx:ListBoxColumn>
                                    </Columns>
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="MaKhachHang" VisibleIndex="1" Width="150px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên Khách Hàng" FieldName="TenKhachHang" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày Mua" VisibleIndex="3" FieldName="NgayMua" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tổng Thành Tiền" VisibleIndex="4" Width="120px"
                                FieldName="TongThanhTien">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Width="100px" Caption="Thao tác" VisibleIndex="5">
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
                                <CancelButton Visible="True">
                                </CancelButton>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                        <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Styles>
                            <Header HorizontalAlign="Center" Font-Bold="true">
                            </Header>   
                            <CommandColumn Spacing="10px">
                            </CommandColumn>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
                <dx:ASPxPageControl runat="server" ActiveTabIndex="0" RenderMode="Lightweight" Width="100%"
                    Height="100%" ID="pagetiendo" ClientInstanceName="pagetiendo">
                    <TabPages>
                        <dx:TabPage Text="Tiến Độ Giao H&#224;ng">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <div style="overflow: scroll; height: 400px;">
                                        <dx:ASPxScheduler runat="server" Start="2013-08-01" Width="100%" ClientIDMode="AutoID"
                                            ID="shedulergiaohang">
                                            <Views>
                                                <DayView>
                                                    <TimeRulers>
                                                        <dx:TimeRuler></dx:TimeRuler>
                                                    </TimeRulers>
                                                </DayView>
                                                <WorkWeekView>
                                                    <TimeRulers>
                                                        <dx:TimeRuler></dx:TimeRuler>
                                                    </TimeRulers>
                                                </WorkWeekView>
                                            </Views>
                                        </dx:ASPxScheduler>
                                    </div>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="tabDetail" Text="Tiến Độ Thanh To&#225;n">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxScheduler runat="server" Start="2013-08-24" Width="100%" ClientIDMode="AutoID"
                                        ID="shedulerthanhtoan">
                                        <Views>
                                            <DayView>
                                                <TimeRulers>
                                                    <dx:TimeRuler></dx:TimeRuler>
                                                </TimeRulers>
                                                <DayViewStyles ScrollAreaHeight="200px">
                                                </DayViewStyles>
                                            </DayView>
                                            <WorkWeekView>
                                                <TimeRulers>
                                                    <dx:TimeRuler></dx:TimeRuler>
                                                </TimeRulers>
                                            </WorkWeekView>
                                        </Views>
                                    </dx:ASPxScheduler>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>        
                    <ClientSideEvents ActiveTabChanged="tabchange"></ClientSideEvents>
    </dx:ASPxPageControl>
        </div>              
    </td>
</tr>
</table>
</asp:Content>
