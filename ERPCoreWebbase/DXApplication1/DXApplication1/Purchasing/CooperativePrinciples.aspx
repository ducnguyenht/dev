<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CooperativePrinciples.aspx.cs" Inherits="WebModule.Purchasing.CooperativePrinciples" %>

<%@ Register Src="~/Purchasing/Usercontrol/uEditCooperativePrinciple.ascx" TagName="uEditCooperativePrinciple"
    TagPrefix="uc1" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function click_custombtn(s, e) {
            if (e.buttonID == 'edit_principle' || e.buttonID == 'add_principle')
                popup_editPrinciple.Show();
            if (e.buttonID == "showCommonDetail")
                popupCommonDetailInfo.Show();
        }
    </script>
    <style type="text/css">
       .dxbButton_DevEx
    {
        color: #201f35;
        font: normal 11px Verdana, Geneva, sans-serif;
        vertical-align: middle;
        border: 1px solid #a9acb5;
        padding: 1px;
        cursor: pointer;
    }
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }
    </style>
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="captionFormName">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh Mục Nguyên Tắc" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top;">
                <div class="gridContainer">
                    <dx:ASPxGridView ID="grd_principles" runat="server" AutoGenerateColumns="False" KeyFieldName="principleID"
                        ClientInstanceName="grd_principles" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã Nguyên Tắc" FieldName="principleID" VisibleIndex="0"
                                Width="150px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã Khách Hàng" FieldName="customerID" VisibleIndex="1"
                                Width="150px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên Khách Hàng" FieldName="customerName" VisibleIndex="2"
                                Width="100%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thời Hạn Áp Dụng" FieldName="startDate" VisibleIndex="3"
                                Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thời Hạn Kết Thúc" FieldName="endDate" VisibleIndex="4"
                                Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" VisibleIndex="5"
                                Width="200px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                VisibleIndex="6" Width="60px">
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="showCommonDetail">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="7"
                                Width="100px">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton Text="Sửa" ID="edit_principle">
                                        <Image ToolTip="Sửa">
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton Text="Thêm" ID="add_principle">
                                        <Image ToolTip="Thêm">
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton Text="Xóa" ID="delete_principle">
                                        <Image ToolTip="Xóa">
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="click_custombtn" Init="function(s, e) {
	 var height = Math.max(0, document.documentElement.clientHeight-200);
     grd_principles.SetHeight(height);

}" />
                        <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                        </SettingsPager>
                        <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="True" AllowSelectByRowClick="True"
                            AllowSelectSingleRowOnly="True" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                            VerticalScrollableHeight="1000" VerticalScrollBarMode="Auto" />
                        <Styles>
                            <Header HorizontalAlign="Center" Font-Bold="true" Wrap="True">
                            </Header>
                            <CommandColumn Spacing="10px">
                            </CommandColumn>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="popup_editPrinciple" runat="server" HeaderText="Thông Tin Cấu Hình Nguyên Tắc - "
        Height="600px" Modal="True" Width="900px" ClientInstanceName="popup_editPrinciple"
        AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
        <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc1:uEditCooperativePrinciple ID="uEditCooperativePrinciple1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <uc2:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
