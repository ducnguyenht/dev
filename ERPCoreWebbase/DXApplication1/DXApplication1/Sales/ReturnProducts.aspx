<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ReturnProducts.aspx.cs" Inherits="WebModule.GUI.Sales.ReturnProducts" %>

<%@ Register Src="~/Sales/UserControl/uEdit_backDeliveryProduct.ascx" TagName="uEdit_backDeliveryProduct"
    TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/uReturnProductsInfo.ascx" TagName="uReturnProductsInfo"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grid_return_custombtn_click(s, e) {
            if (e.buttonID == 'add_return' || e.buttonID == 'edit_return')
                popup_editProcessReceiveProduct.Show();
        }

        function ShowInfoclick(s, e) {
            popup_showSummaryInfo.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="11pt" Text="Danh mục phiếu trả hàng">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="grid_return" runat="server" AutoGenerateColumns="False" KeyFieldName="id"
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="id" VisibleIndex="1" SortIndex="3"
                SortOrder="Ascending" Width="100px">
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("id") %>'>
                        <ClientSideEvents Click="ShowInfoclick" />
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã khách hàng" FieldName="idkh" VisibleIndex="2"
                SortIndex="2" SortOrder="Ascending" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="tenkh" VisibleIndex="3"
                SortIndex="1" SortOrder="Ascending">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngày trả" FieldName="ngaytra" VisibleIndex="4"
                SortIndex="0" SortOrder="Ascending">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi tiết" VisibleIndex="5">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton Text="Chi tiết" ID="bt_detail">
                        <Image ToolTip="Tài liệu">
                            <SpriteProperties CssClass="Sprite_Info" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="5">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit_return">
                        <Image ToolTip="Chỉnh sửa">
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="add_return">
                        <Image ToolTip="Thêm mới">
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="delete_return">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="grid_return_custombtn_click" />
        <SettingsPager PageSize="22" ShowEmptyDataRows="true">
        </SettingsPager>
        <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectSingleRowOnly="true" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowHeaderFilterButton="true" />
        <Styles>
            <CommandColumn Spacing="10px">
            </CommandColumn>
        </Styles>
        <SettingsEditing Mode="Inline" />
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
            </Header>
            <HeaderPanel HorizontalAlign="Center">
            </HeaderPanel>
            <CommandColumn HorizontalAlign="Center" Spacing="5px">
            </CommandColumn>
        </Styles>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
    </dx:ASPxGridView>
    <uc1:uEdit_backDeliveryProduct ID="uEdit_backDeliveryProduct1" runat="server" />
    <dx:ASPxPopupControl ID="popup_showSummaryInfo" ClientInstanceName="popup_showSummaryInfo"
        ShowFooter="true" runat="server" AllowDragging="True" AllowResize="True" HeaderText="Thông tin phiếu trả hàng"
        Modal="True" RenderMode="Classic" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
        MinHeight="100px" MinWidth="850px" Height="600px" Width="1200px" ScrollBars="Auto"
        ShowSizeGrip="False">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc2:uReturnProductsInfo ID="uReturnProductsInfo1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterContentTemplate>
            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp"
                CssClass="float-left button-left-margin">
                <Image>
                    <SpriteProperties CssClass="Sprite_Help" />
                </Image>
            </dx:ASPxButton>
        </FooterContentTemplate>
    </dx:ASPxPopupControl>
</asp:Content>
