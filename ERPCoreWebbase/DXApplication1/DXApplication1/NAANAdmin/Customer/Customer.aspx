<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="WebModule.NAANAdmin.Customer.Customer" %>
<%@ Register src="~/NAANAdmin/Customer/Usercontrol/uCustomer.ascx" tagname="uCustomer" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function gvCustomer_CustomButton(s, e) {
        if (e.buttonID == 'new' || e.buttonID == 'edit')
            popup_customer.Show();
        else if (e.buttonID == '') {
            
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
     <dx:ASPxPopupControl ID="popup_edit" runat="server" Width="1000px" Height="400px"
        RenderMode="Lightweight" HeaderText="Thông tin khách hàng" ClientInstanceName="popup_customer" AllowResize="True" AllowDragging="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc1:uCustomer ID="uCustomer" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục khách hàng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:ASPxGridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <ClientSideEvents CustomButtonClick="gvCustomer_CustomButton
" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Thể loại" FieldName="Type" 
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Người quản trị" FieldName="Admin" 
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã thuế" FieldName="Taxcode" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="Name" 
                ShowInCustomizationForm="True" VisibleIndex="1" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thông tin liên hệ" 
                VisibleIndex="5">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton>
                        <Image ToolTip="Thông tin chi tiết">
                            <SpriteProperties CssClass="Sprite_Document" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" 
                ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                VisibleIndex="7" Width="120px">
                <clearfilterbutton visible="True">
                </clearfilterbutton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="edit">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="new">
                        <Image ToolTip="Thêm">
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="delete">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <settingsediting mode="PopupEditForm" />
        <settings showfilterrow="True" showfilterrowmenu="True" 
            showheaderfilterbutton="True" />
        <settingspopup>
            <editform height="150px" width="500px" />
            <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
        </settingspopup>
    </dx:ASPxGridView>
</asp:Content>
