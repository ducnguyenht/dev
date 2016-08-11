<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CooperativePrinciples.aspx.cs" Inherits="WebModule.GUI.Sales.CooperativePrinciples" %>

<%@ Register src="~/Sales/UserControl/uEditCooperativePrinciple.ascx" tagname="uEditCooperativePrinciple" tagprefix="uc1" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function click_custombtn(s, e) {
            if (e.buttonID == 'edit_principle' || e.buttonID == 'add_principle')
                popup_editPrinciple.Show();
            else if (e.buttonID == 'showCommonDetail')
                popupCommonDetailInfo.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin:10px 0 10px 10px">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh Mục Nguyên Tắc" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>    
</div>
<table style="width:100%;">
<tr>
    <td style="vertical-align:top;">
           <div class="gridContainer">
                <dx:ASPxGridView ID="grd_principles" runat="server" AutoGenerateColumns="False" 
                    KeyFieldName="principleID">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Mã Nguyên Tắc" FieldName="principleID" VisibleIndex="0" Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mã Khách Hàng" FieldName="customerID" VisibleIndex="1" Width="150px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tên Khách Hàng" FieldName="customerName" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thời Hạn Áp Dụng" FieldName="startDate" VisibleIndex="3" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Thời Hạn Kết Thúc" FieldName="endDate" VisibleIndex="4" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tình trạng" FieldName="status" VisibleIndex="5" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="description" VisibleIndex="6" Width="200px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
	                        VisibleIndex="7" Width="60px">
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
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="8" Width="100px">
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
                    <ClientSideEvents CustomButtonClick="click_custombtn" />
                    <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
                    <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"/>
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

<dx:ASPxPopupControl ID="popup_editPrinciple" 
    ClientInstanceName="popup_editPrinciple" runat="server" 
    RenderMode="Classic" HeaderText="Thông tin cấu hình nguyên tắc" 
    Modal="True" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter" Width="800px" Height="600px" ScrollBars="Auto" ShowFooter="true" AllowResize="true" AllowDragging="true">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <uc1:uEditCooperativePrinciple ID="uEditCooperativePrinciple1" runat="server"/>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel4" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <div style="float: left; margin-right: 4px">
                        <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                            Text="Bỏ Qua">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right">
                        <dx:ASPxButton ID="buttonAccept" ClientInstanceName="buttonSave" runat="server" Text="Lưu lại"
                            clientvisible="true">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:ASPxPopupControl>
<uc2:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
