<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ProcessOfSalesTotal.aspx.cs" Inherits="WebModule.Purchasing.ProcessOfSalesTotal" %>
<%@ Register Src="~/Purchasing/UserControl/uMonitorProcessOfSalesTotalOnDetail.ascx" TagName="uMonitorProcessOfSalesTotalOnDetail"
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
         function grdMonitorProcessOfSalesTotal_btncustom_click(s, e) {
             if (e.buttonID == 'view_detail') {
                 show_uMonitorProcessOfSalesTotalOnDetail();
             }
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uMonitorProcessOfSalesTotalOnDetail ID="uMonitorProcessOfSalesTotalOnDetail1" runat="server"/>
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách Theo Dõi Doanh Số" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>    
    </div>
    <table style="width:100%;">
        <tr>
            <td style="vertical-align:top;">
                <div>
                    <div style="overflow-y: scroll; overflow-x:hidden ;height: 300px">
                        <dx:ASPxGridView ID="grdMonitorProcessOfSalesTotal" ClientInstanceName="grdMonitorProcessOfSalesTotal" runat="server" AutoGenerateColumns="False" Width="100%" 
                            KeyFieldName="CooperativePrincipleId" >
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã HTNT" FieldName="CooperativePrincipleId" >
                                    <EditItemTemplate>
                                        <%# Eval("CooperativePrincipleId") %>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="CustomerId">
                                    <EditItemTemplate>
                                        <%# Eval("CustomerId") %>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên Khách Hàng" FieldName="CustomerName">
                                    <EditItemTemplate>
                                        <%# Eval("CustomerName")%>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Doanh số hiện tại" FieldName="CurrentSalesTotal" >
                                    <EditItemTemplate>
                                        <%# Eval("CurrentSalesTotal")%>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tình trạng" FieldName="Status" >
                                    <EditItemTemplate>
                                        <%# Eval("Status")%>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số ngày cảnh báo trước" FieldName="NumOfDaysAlarm" >
                                    <EditItemTemplate>
                                        <dx:ASPxTextBox ID="txtNumOfDaysAlarm" runat="server" Width="100%">
                                        </dx:ASPxTextBox>
                                    </EditItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Width="100px" Caption="Thao tác">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="view_detail">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Grid" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <EditButton Text="edit" Visible="true">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                        </Image>
                                    </EditButton>
                                    <UpdateButton Text="Lưu lại">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply" />
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton Text="Hủy thao tác">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </CancelButton>
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <ClientSideEvents CustomButtonClick="grdMonitorProcessOfSalesTotal_btncustom_click" />
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
                            <dx:TabPage Text="Tiến Độ Doanh Số Theo Thời Gian">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
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
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>              
            </td>
        </tr>
    </table>
</asp:Content>
