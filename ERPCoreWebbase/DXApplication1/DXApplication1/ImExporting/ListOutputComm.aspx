<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ListOutputComm.aspx.cs" Inherits="WebModule.ImExporting.ListOutputComm" %>

<%@ Register Src="~/Warehouse/UserControl/uOutCommWarehouse.ascx" TagName="uOutCommWarehouse"
    TagPrefix="uc1" %>
<%@ Register Src="~/Accounting/UserControl/uInventoryVoucher_ex.ascx" TagName="uInventoryVoucher_ex"
    TagPrefix="uc2" %>
<%@ Register Src="~/Warehouse/UserControl/uOutCommWarehouseInfo.ascx" TagName="uOutCommWarehouseInfo"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        // MainForm Event
        function grdData_EndCallback(s, e) {
            // formEntryDetail.Show();

        }
        function grdData_CustomButtonClick(s, e) {
            if (e.buttonID == "btnApprove") {
                poppxk.Show();
            }
            if (e.buttonID == "btnEdit") {
                formEntryDetail.Show();
                formEntryDetail.SetHeaderText("Chỉnh sửa phiếu xuất kho");
            }
            if (e.buttonID == "btnNew") {
                formEntryDetail.Show();
                formEntryDetail.SetHeaderText("Tạo mới phiếu xuất kho");
            }
        }

        function ShowInfoclick(s, e) {
            popup_outCommInfo.Show();
        }
    </script>
    <style type="text/css">
        .hd
        {
            display: none;
        }
        .dxgvControl, .dxgvDisabled
        {
            border: 1px Solid #9F9F9F;
            font: 12px Tahoma, Geneva, sans-serif;
            background-color: #F2F2F2;
            color: Black;
            cursor: default;
        }
        .dxgvTable
        {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }
        
        .dxgvTable
        {
            background-color: White;
            border-width: 0;
            border-collapse: separate !important;
            overflow: hidden;
            color: Black;
        }
        .dxgvHeader
        {
            cursor: pointer;
            white-space: nowrap;
            padding: 4px 6px 5px;
            border: 1px Solid #9F9F9F;
            background-color: #DCDCDC;
            overflow: hidden;
            font-weight: normal;
            text-align: left;
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
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách phiếu xuất kho" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
        HideContentOnCallback="True">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdData">
                    <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback">
                    </ClientSideEvents>
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Hạch toán" VisibleIndex="5" ButtonType="Image"
                            Width="60px">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btnApprove" Text="Hạch toán" Image-ToolTip="Hạch toán">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Balance" />
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn Caption="Thao t&#225;c" VisibleIndex="6" ButtonType="Image">
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
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="code" Caption="Mã phiếu xuất" VisibleIndex="0">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("code") %>'>
                                    <ClientSideEvents Click="ShowInfoclick" />
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="status" Caption="Trạng thái" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Loại chứng từ" FieldName="type" ShowInCustomizationForm="True"
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="date" Caption="Ng&#224;y tạo phiếu" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="name" Caption="Người tạo phiếu" VisibleIndex="1">
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
    <uc2:uInventoryVoucher_ex ID="uInventoryVoucher_ex1" runat="server" />
    <uc1:uOutCommWarehouse ID="uOutCommWarehouse" runat="server" />
    <dx:ASPxPopupControl ID="popup_outCommInfo" runat="server" HeaderText="Thông Tin Phiếu Xuất Kho" Height="600px" ScrollBars="Auto"
        Modal="True" RenderMode="Classic" Width="900px" ClientInstanceName="popup_outCommInfo"
        AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        LoadingPanelDelay="1000" ShowSizeGrip="False" ShowMaximizeButton="true" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc3:uOutCommWarehouseInfo ID="uOutCommWarehouseInfo1" runat="server" />
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
