<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CustomerService.aspx.cs" Inherits="WebModule.NAANAdmin.Customer.CustomerService" %>
<%@ Register src="~/NAANAdmin/Customer/Usercontrol/uCustomerService.ascx" tagname="uCustomerService" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function gvApplication_Custombutton(s, e) {
        if (e.buttonID == 'new' || e.buttonID == 'edit')
            popup_customerservice.Show();
        else if (e.buttonID == '') {

        }
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
     <dx:ASPxPopupControl ID="popup_edit" runat="server" Width="1000px" Height="400px"
        RenderMode="Lightweight" HeaderText="Thông tin khách hàng" ClientInstanceName="popup_customerservice" AllowResize="True" AllowDragging="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <uc1:uCustomerService ID="uCustomerService" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Medium"
        Height="54px" Text="Danh mục khách hàng" Width="303px">
        <Border BorderStyle="None" />
    </dx:ASPxTextBox>
    <dx:ASPxGridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" 
        Width="100%">
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
            
        </Columns>
        <settingsediting mode="PopupEditForm" />
        <settings showfilterrow="True" showfilterrowmenu="True" 
            showheaderfilterbutton="True" />
        <SettingsDetail ShowDetailRow="True" />
        <settingspopup>
            <editform height="150px" width="500px" />
            <CustomizationWindow HorizontalAlign="Center" VerticalAlign="Middle" />
        </settingspopup>
        <Templates>
            <DetailRow>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                    RenderMode="Lightweight" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Danh sách ứng dụng">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="gvApplication" runat="server" AutoGenerateColumns="False" 
                                        Width="100%" 
                                        OnBeforePerformDataSelect="gvApplication_BeforePerformDataSelect">
                                        <ClientSideEvents CustomButtonClick="gvApplication_Custombutton
" />
                                        <Columns>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                ShowInCustomizationForm="True" VisibleIndex="5">
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
                                            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" 
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" 
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="FromDate" 
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Ngày hết hạn" FieldName="ExpireDate" 
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Số người truy cập" FieldName="Visistor" 
                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>
</asp:Content>
