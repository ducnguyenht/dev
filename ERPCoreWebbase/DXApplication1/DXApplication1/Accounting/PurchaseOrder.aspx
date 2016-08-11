<%@ Page Title="" Language="C#" ClientIDMode="AutoID" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PurchaseOrder.aspx.cs" Inherits="WebModule.Accounting.Phieumua" %>

<%@ Register Src="UserControl/uPopupphieumuaban.ascx" TagName="uPopupphieumuaban"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
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

        function grdHeader_EndCallback(s, e) {

        }
        function grdRow_Click(s, e) {

        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .pd
        {
            padding-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh Mục Phiếu Mua" Font-Bold="True"
            Font-Size="Medium" Height="45px">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
        <ClientSideEvents CustomButtonClick="grdHeader_CustomButtonClick" EndCallback="grdHeader_EndCallback"
            RowClick="grdRow_Click" />
        <Columns>
            <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" VisibleIndex="0"> 
                <HeaderTemplate>
                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                </dx:ASPxCheckBox>
            </HeaderTemplate>                               
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Mã Chứng Từ" FieldName="c1" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên Khách Hàng" FieldName="c3" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày Mua" FieldName="c4" VisibleIndex="3">
                <PropertiesDateEdit DisplayFormatString="">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Sơ Đồ Định Khoản" FieldName="sd" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="c2" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="c5" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn VisibleIndex="7" ButtonType="Image" Caption="Chứng Từ Gốc">
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
                                                            <SettingsBehavior ColumnResizeMode="Control" />
        <SettingsBehavior AllowFocusedRow="True" />
        <Settings ShowFilterRow="True" />
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                        SupportsDisabledAttribute="True">
                        <dx:ASPxButton ID="bt_duyet_2" runat="server" CssClass="float-right" Text="Duyệt">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Approve" />
                            </Image>
                            <ClientSideEvents Click="function(s, e) {
	                    alert('Duyệt tất cả các chứng từ được chọn');}" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <uc1:uPopupphieumuaban ID="uPopupphieumuaban1" runat="server" />
</asp:Content>
