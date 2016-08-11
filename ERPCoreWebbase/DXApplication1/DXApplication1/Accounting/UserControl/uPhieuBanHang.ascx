<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uPhieuBanHang.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uPhieuBanHang" %>
<%@ Register Src="uPopupphieumuaban.ascx" TagName="uPopupphieumuaban" TagPrefix="uc1" %>
<script type="text/javascript">
    function grdHeader_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'Xem': { alert("Hiển thị file chứng từ gốc"); break; }
            case 'btApprove':
                {
                    popmb.Show();
                    cppmb.PerformCallback("mua");
                    break;
                }
        }
    }
</script>
<style type = "text/css">
       .hd
       {
           display:none;
       }
</style>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Danh sách phiếu bán" Width="259px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
    <ClientSideEvents CustomButtonClick="grdHeader_CustomButtonClick" />
    <Columns>
        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="0">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <HeaderTemplate>
                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                </dx:ASPxCheckBox>
            </HeaderTemplate>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="Mã chứng từ" FieldName="c1" VisibleIndex="1"
            SortIndex="0" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên khách hàng" FieldName="c3" VisibleIndex="2"
            SortIndex="2" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="Ngày bán" FieldName="c4" VisibleIndex="2" SortIndex="3"
            SortOrder="Ascending">
            <PropertiesDateEdit DisplayFormatString="">
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Sơ đồ định khoản" FieldName="sd" VisibleIndex="4"
            SortIndex="4" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="c2" VisibleIndex="5" SortIndex="1"
            SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="c5" VisibleIndex="6" SortIndex="5"
            SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn VisibleIndex="7" ButtonType="Image" Caption="Chứng từ gốc">
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
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxButton ID="bt_approve_1" runat="server" CssClass="float-right button-right-margin" Text="Duyệt">
                        <ClientSideEvents Click="function(s, e) {
	alert('Duyệt tất cả các chứng từ được chọn');
}" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Approve" />
                        </Image>
                    </dx:ASPxButton>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>
<uc1:uPopupphieumuaban ID="uPopupphieumuaban1" runat="server" />
