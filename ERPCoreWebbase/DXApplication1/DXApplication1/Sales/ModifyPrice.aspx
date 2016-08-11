<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="~/Sales/ModifyPrice.aspx.cs" Inherits="DXApplication1.GUI.ModifyPrice" %>
<%@ Register src="~/Sales/UserControl/uModifyFixedPrice.ascx" tagname="uModifyFixedPrice" tagprefix="uc1" %>
<%@ Register src="userControl/uViewModifyFixedPrice.ascx" tagname="uViewModifyFixedPrice" tagprefix="uc2" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    function grvModifyingPrice_custombtn_click(s, e) {
        if (e.buttonID == "showCommonDetail") {
            popupCommonDetailInfo.Show();
            return;
        }
        popup_CreaterModifyFixedPrice.Show();
    }    
</script>
<style type="text/css">
        .float_right
        {
            float: right;
            margin-left: 10px;
    
        }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh Mục Hiệu Chỉnh Giá" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="grvModifyingPrice" runat="server" AutoGenerateColumns="False" KeyFieldName="id"
        Width="100%">
            <ClientSideEvents CustomButtonClick="grvModifyingPrice_custombtn_click"></ClientSideEvents>
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã Hiệu Chỉnh" FieldName="id" VisibleIndex="0"
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngày Hiệu Chỉnh" FieldName="createDate" VisibleIndex="1"
                Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="Hiệu Lực" VisibleIndex="2">
                <Columns>
                    <dx:GridViewDataDateColumn Caption="Từ Ngày" FieldName="ht" VisibleIndex="0" Width="100px">
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn Caption="Đến Ngày" FieldName="hd" VisibleIndex="1" Width="100px">
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataDateColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="diengiai" 
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
	            VisibleIndex="4" Width="60px">
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
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="5"
                Width="100px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit_modifying">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="add_modifying">
                        <Image>
                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="delete_modifying">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <ClearFilterButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Clear" />
<SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                    </Image>
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />     
        <SettingsDetail ShowDetailRow="true" />
        <SettingsBehavior ColumnResizeMode="NextColumn" />

<SettingsBehavior ColumnResizeMode="NextColumn"></SettingsBehavior>

        <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>                   

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>

<SettingsDetail ShowDetailRow="True"></SettingsDetail>

        <Styles>
            <Header HorizontalAlign="Center" Font-Bold="true">
            </Header>
            <CommandColumn Spacing="10px">
            </CommandColumn>
        </Styles>
        <Templates>
            <DetailRow>
                <uc2:uViewModifyFixedPrice ID="uViewModifyFixedPrice1" runat="server" />
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>

    <uc1:uModifyFixedPrice ID="uModifyFixedPrice1" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>

