<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ConditionReceiveReturn.aspx.cs" Inherits="WebModule.GUI.Sales.ConditionReceiveReturn" %>

<%@ Register Src="~/Sales/UserControl/uCreateConditionToReceiveReturnProduct.ascx"
    TagName="uCreateConditionToReceiveReturnProduct" TagPrefix="uc1" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grv_configConditionReceive_clickrow(s, e) {

        }        

        function cust_click(s, e) {
            if (e.buttonID == 'new') {
                popup_editrecord.Show();
            }
            else if (e.buttonID == 'edit') {
                popup_editrecord.Show();
            }

            else if (e.buttonID == 'showCommonDetail') {
                popupCommonDetailInfo.Show();
            }
        }

        function popup_editrecord_resize(s, e) {
            pc.AdjustControl();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh mục điều kiện nhận hàng trả"  
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
    <dx:aspxgridview id="grv_configConditionReceive" runat="server" autogeneratecolumns="False" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã chính sách" FieldName="id" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên chính sách" FieldName="name"  VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="Hiệu lực" VisibleIndex="2">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Từ" FieldName="to" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đến" FieldName="from" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" VisibleIndex="3">
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
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="5" Width="6%" 
                ButtonType="Image">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit">
                        <Image ToolTip="Edit">
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="new">
                        <Image ToolTip="New">
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="GridViewCommandColumnCustomButton1">
                        <Image ToolTip="New">
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents RowClick="grv_configConditionReceive_clickrow" />
        <Settings ShowFilterRow="True" />
        <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
        <Styles>
            <CommandColumn Spacing="10px">
            </CommandColumn>
        </Styles>
        <ClientSideEvents CustomButtonClick="cust_click" />
    </dx:aspxgridview>   
    <uc1:uCreateConditionToReceiveReturnProduct ID="uCreateConditionToReceiveReturnProduct1" runat="server" />
    <uc2:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
</asp:Content>
