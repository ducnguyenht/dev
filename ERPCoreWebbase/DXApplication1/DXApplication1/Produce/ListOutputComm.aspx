<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ListOutputComm.aspx.cs" Inherits="WebModule.Produce.ListOutputComm" %>

<%@ Register Src="UserControl/uOutCommWarehouse.ascx" TagName="uOutCommWarehouse"
    TagPrefix="uc1" %>
<%@ Register Src="../Accounting/UserControl/uInventoryVoucher_ex.ascx" TagName="uInventoryVoucher_ex"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        // MainForm Event
        function grdData_EndCallback(s, e) {
            formEntryDetail.Show();

        }
        function grdData_CustomButtonClick(s, e) {
            if (e.buttonID == "btApprove") {
                poppxk.Show();
            }
        }
    </script>
    <style type="text/css">
        .hdb
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
        OnCallback="cpHeader_Callback" HideContentOnCallback="True">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdData"
                    OnInitNewRow="grdData_InitNewRow" OnStartRowEditing="grdData_StartRowEditing">
                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback"></ClientSideEvents>
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
                        <dx:GridViewCommandColumn Caption="Thao t&#225;c" VisibleIndex="5" ButtonType="Image">
                            <EditButton Visible="True">
                                <Image ToolTip="Sửa">
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </EditButton>
                            <NewButton Visible="True">
                                <Image ToolTip="Thêm">
                                    <SpriteProperties CssClass="Sprite_New" />
                                </Image>
                            </NewButton>
                            <DeleteButton Visible="True">
                                <Image ToolTip="Xóa">
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </DeleteButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="code" Caption="Mã phiếu xuất" VisibleIndex="0">
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
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                        AllowSelectSingleRowOnly="True" />
                    <Settings ShowFilterRow="True" />
                    <Settings ShowFilterRow="True"></Settings>
                </dx:ASPxGridView>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <uc2:uInventoryVoucher_ex ID="uInventoryVoucher_ex1" runat="server" />
    <uc1:uOutCommWarehouse ID="uOutCommWarehouse" runat="server" />
</asp:Content>
