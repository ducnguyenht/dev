<%@ Page Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" MasterPageFile="~/Site.master"
    CodeBehind="PromotionPolicy.aspx.cs" Inherits="DXApplication1.GUI.PromotionPolicy" %>
<%@ Register Src="~/Sales/UserControl/uEditPromotionPolicy.ascx" TagName="uEditPromotionPolicy"
    TagPrefix="uc1" %> 
<%@ Register Src="~/Sales/UserControl/uViewPromotionLevelInfo.ascx" TagName="uViewPromotionLevelInfo"
    TagPrefix="uc2" %> 
<%@ Register Src="~/Sales/UserControl/uCreateVirtualSalesTestingPromotion.ascx" TagName="uCreateVirtualSalesTestingPromotion"
    TagPrefix="uc3" %> 
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title></title>
    <script type="text/javascript">
        function show_PromotionLevelInfo(s, e) {
            popup_showPromotionLevelInfo.Show();
        }

        function grv_promotionPolicy_custombtn_click(s, e) {
            if (e.buttonID == 'edit_promotion' || e.buttonID == 'new_promotion')
                popup_wizardCreaterPromotionPolicy.Show();
            else if (e.buttonID == 'test_promotion')
                popup_testPromotion.Show();
            else if (e.buttonID == 'showCommonDetail1')
                popupCommonDetailInfo.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <uc1:uEditPromotionPolicy ID="uEditPromotionPolicy1" runat="server" />
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh Mục Chương Trình Khuyến Mãi" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>    
    <dx:ASPxGridView ID="grv_promotionPolicy" runat="server" AutoGenerateColumns="False" KeyFieldName="ma"
        Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã chương trình" FieldName="ma"
                VisibleIndex="0" SortIndex="0" SortOrder="Ascending" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên Chương Trình Khuyến Mãi" FieldName="ten"
                VisibleIndex="1" SortIndex="3" SortOrder="Ascending" Width="100%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="Hiệu Lực" VisibleIndex="2">
                <Columns>
                    <dx:GridViewDataDateColumn Caption="Từ Ngày" FieldName="hltu" VisibleIndex="0" SortIndex="1"
                        SortOrder="Ascending" Width="100px">
                        <PropertiesDateEdit DisplayFormatString="">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="Đến Ngày" FieldName="hlden" VisibleIndex="1" SortIndex="0"
                        SortOrder="Ascending" Width="100px">
                        <PropertiesDateEdit DisplayFormatString="">
                        </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="diengiai" VisibleIndex="3" Width="150px">
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="trangthai" VisibleIndex="4"
                SortIndex="2" SortOrder="Ascending" Width="150px">
                <CellStyle HorizontalAlign="Left"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
	            VisibleIndex="5" Width="60px">
	            <ClearFilterButton Visible="True">
	            </ClearFilterButton>
	            <CustomButtons>
		            <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
			            <Image>
				            <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
			            </Image>
		            </dx:GridViewCommandColumnCustomButton>
	            </CustomButtons>
            </dx:GridViewCommandColumn> 
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="6"
                Width="150px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit_promotion">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="new_promotion">
                        <Image>
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="delete_promotion">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="test_promotion">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Repeat" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="grv_promotionPolicy_custombtn_click" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="true" />
        <SettingsBehavior ColumnResizeMode="NextColumn" />
        <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
        <Styles>
            <Header HorizontalAlign="Center" Font-Bold="true">
            </Header>
            <CommandColumn Spacing="10px">
            </CommandColumn>
        </Styles>
        <Templates>
            <DetailRow>
                <dx:ASPxLabel ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Small"
                    Text="Các mức khuyến mãi" Width="170px">
                </dx:ASPxLabel>
                <dx:ASPxGridView ID="grv_subPromotion" runat="server" AutoGenerateColumns="False" Width="100%"
                    OnBeforePerformDataSelect="grv_subPromotion_BeforePerformDataSelect">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mức Khuyến Mãi" FieldName="ten" VisibleIndex="0">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="link_ten" runat="server"
                                    ToolTip="Hiện thông tin mức khuyến mãi" Text='<%# Eval("ten") %>'>
                                    <ClientSideEvents Click="show_PromotionLevelInfo" />
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="diengiai" VisibleIndex="1" Width="200px">
                        </dx:GridViewDataTextColumn>                                
                    </Columns>
                    <Settings HorizontalScrollBarMode="Hidden" />
                    <SettingsPager PageSize="10" ShowEmptyDataRows="true"></SettingsPager>
                    <SettingsBehavior ColumnResizeMode="NextColumn" />
                    <Styles>
                        <Header HorizontalAlign="Center" Font-Bold="true">
                        </Header>
                        <CommandColumn Spacing="10px">
                        </CommandColumn>
                    </Styles>
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>

    <dx:ASPxPopupControl ID="popup_showPromotionLevelInfo" ClientInstanceName="popup_showPromotionLevelInfo" runat="server" HeaderText="Thông Tin Mức Khuyến Mãi"
        CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" RenderMode="Classic"
        PopupVerticalAlign="WindowCenter" AllowResize="True" Width="1000px" Height="600px" ScrollBars="Auto"
        AllowDragging="True">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <uc2:uViewPromotionLevelInfo ID="uViewPromotionLevelInfo1" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <uc3:uCreateVirtualSalesTestingPromotion ID="uCreateVirtualSalesTestingPromotion1" runat="server" />
    <uc4:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
