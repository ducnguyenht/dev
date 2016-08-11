<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPriceEstimates.ascx.cs" Inherits="WebModule.Produce.UserControl.uPriceEstimates" %>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" 
    Font-Size="Medium" Height="55px" Text="Ước tính giá thành sản phẩm" 
    Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
    Width="100%">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="ID" 
            VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="Name" 
            VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Chi phí nguyên vật liệu" FieldName="MP" 
            VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Chi phí nhân sự" FieldName="HP" 
            VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Chi phí trang thiết bị" FieldName="EP" 
            VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Giá thành sản phẩm" FieldName="Price" 
            VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" VisibleIndex="6">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
    <Settings VerticalScrollableHeight="600" />
    <Styles>
        <Header HorizontalAlign="Center">
        </Header>
    </Styles>
</dx:ASPxGridView>

