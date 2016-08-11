<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ListInputComm.aspx.cs" Inherits="WebModule.ImExporting.ListInputComm" %>

<%@ Register Src="~/Warehouse/UserControl/uInputCommWarehouse.ascx" TagName="uInputCommWarehouse"
    TagPrefix="uc1" %>
<%@ Register Src="~/Accounting/UserControl/uInventoryVoucher_im.ascx" TagName="uInventoryVoucher_im"
    TagPrefix="uc2" %>
<%@ Register Src="~/Warehouse/UserControl/uInCommWarehouseInfo.ascx" TagName="uInCommWarehouseInfo"
    TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        // MainForm Event
        function grdData_EndCallback(s, e) {
            //formEntryDetail.Show();

        }

        function grdData_CustomButtonClick(s, e) {
            if (e.buttonID == "btApprove") {
                poppnk.Show();
            }
            if (e.buttonID == "btnEdit") {
                formEntryDetail.Show();
                formEntryDetail.SetHeaderText("Chỉnh sửa phiếu nhập kho");
            }
            if (e.buttonID == "btnNew") {
                formEntryDetail.Show();
                formEntryDetail.SetHeaderText("Tạo mới phiếu nhập kho");
            }
        }

        function ShowInfoclick() {
            popup_inputCommInfo.Show();
        }
    </script>
    <style type="text/css">
        .hd
        {
            display: none;
        }
        
        
        .style1
        {
            color: Black;
            border-left-width: 0px;
            border-top-width: 0px;
        }
        .style2
        {
            text-align: Center;
            color: Black;
            border-left-width: 0px;
            border-right-width: 0px;
            border-top-width: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="captionFormName">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh Sách Phiếu Nhập Kho" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top;">
                <div class="gridContainer">
                    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                        OnCallback="cpHeader_Callback" HideContentOnCallback="True">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdData">
                                    <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback">
                                    </ClientSideEvents>
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Hạch Toán" VisibleIndex="5" ButtonType="Image"
                                            Width="60px">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btApprove" Text="Hạch Toán" Image-ToolTip="Hạch toán">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Balance" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn Caption="Thao Tác" VisibleIndex="6" ButtonType="Image"
                                            Width="100px">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnEdit" Text="Sửa" Image-ToolTip="Sửa">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnNew" Text="Tạo mới" Image-ToolTip="Tạo mới">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_New" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="btnDelete" Text="Xóa" Image-ToolTip="Xóa">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="code" Caption="Mã Phiếu Nhập" VisibleIndex="0"
                                            Width="150px">
                                            <DataItemTemplate>
                                                <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("code") %>'>
                                                    <ClientSideEvents Click="ShowInfoclick" />
                                                </dx:ASPxHyperLink>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="type" Caption="Loại chứng từ" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="status" ShowInCustomizationForm="True" Width="100px"
                                            Caption="Trạng Thái" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="date" Caption="Ngày Tạo Phiếu" VisibleIndex="2"
                                            Width="150px">
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="name" Caption="Người Tạo Phiếu" VisibleIndex="1"
                                            Width="150px">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                    <SettingsBehavior AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true"
                                        ColumnResizeMode="NextColumn" />
                                    <SettingsPager PageSize="22" ShowEmptyDataRows="true">
                                    </SettingsPager>
                                    <Styles>
                                        <Header HorizontalAlign="Center" Font-Bold="true" Wrap="True">
                                        </Header>
                                        <CommandColumn Spacing="10px">
                                        </CommandColumn>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:uInventoryVoucher_im ID="uInventoryVoucher_im1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <uc1:uInputCommWarehouse ID="uInputCommWarehouse" runat="server" />
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="popup_inputCommInfo" runat="server" HeaderText="Thông Tin Phiếu Nhập Kho" Height="600px" ScrollBars="Auto"
        Modal="True" RenderMode="Classic" Width="900px" ClientInstanceName="popup_inputCommInfo"
        AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        LoadingPanelDelay="1000" ShowSizeGrip="False" ShowMaximizeButton="true" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc3:uInCommWarehouseInfo ID="uInCommWarehouseInfo1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterTemplate>
            <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                            <div style="float: left; margin-right: 4px">
                                <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </FooterTemplate>
    </dx:ASPxPopupControl>

</asp:Content>
