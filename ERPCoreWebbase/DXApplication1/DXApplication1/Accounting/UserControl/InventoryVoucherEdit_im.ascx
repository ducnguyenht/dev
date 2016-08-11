<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InventoryVoucherEdit_im.ascx.cs" Inherits="ERPCore.Accounting.UserControl.InventoryVoucherEdit_im" %>
<%@ Register src="uInventoryVoucher_im.ascx" tagname="uInventoryVoucher_im" tagprefix="uc1" %>

<script TKco = "text/javascript">
    function csbt_click(s, e) {
        switch (e.buttonID) {
            case 'Xem': { alert("Hiển thị file chứng từ gốc"); break; }
            case 'btApprove':
                {
                    poppnk.Show();
                    break;
                }
        }
    }
    function bt_ap_click(s, e) {
        alert("Duyệt tất cả các chứng từ được chọn");
    }
</script>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" 
    Font-Size="Medium" Height="55px" Text="Danh sách phiếu nhập kho" 
    Width="259px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
    Width="100%">
    <ClientSideEvents CustomButtonClick="csbt_click" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã chứng từ" FieldName="c1" VisibleIndex="1" 
            SortIndex="0" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="c2" 
            VisibleIndex="2" SortIndex="1" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mục đích" FieldName="c3" 
            VisibleIndex="3" SortIndex="2" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="Sơ đồ định khoản" FieldName="c4" VisibleIndex="6" 
            SortIndex="3" SortOrder="Ascending">
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Ngày xuất" FieldName="c5" VisibleIndex="4" 
            SortIndex="4" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tổng tiền" FieldName="c6" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="c7" VisibleIndex="7">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn VisibleIndex="8" Caption="Chứng từ gốc" 
            ButtonType="Image">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="Xem" Text="Xem">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Document" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn ShowInCustomizationForm="True" 
            ShowSelectCheckbox="True" VisibleIndex="0">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <HeaderTemplate>
                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                </dx:ASPxCheckBox>
            </HeaderTemplate>
        </dx:GridViewCommandColumn>
         <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="8">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btApprove" Text="Duyệt">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
    </Columns>
    <Settings ShowFilterRow="True" />
    <Styles>
        <Header HorizontalAlign="Center">
        </Header>
    </Styles>
</dx:ASPxGridView>

<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
    <Items>
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxButton ID="bt_approve" runat="server" CssClass="float-right button-right-margin" 
                        Text="Duyệt">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                        <ClientSideEvents Click = "bt_ap_click" /> 
                    </dx:ASPxButton>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>



<uc1:uInventoryVoucher_im ID="uInventoryVoucher_im1" runat="server" />




