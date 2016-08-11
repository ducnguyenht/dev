<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uViewModifyFixedPrice.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uViewModifyFixedPrice" %>
<dx:ASPxLabel ID="lbl_titlethamkhao" runat="server" Font-Bold="True" 
    Text="Danh mục hàng hóa đã hiệu chỉnh giá" ClientInstanceName="lbl_titlethamkhao">
</dx:ASPxLabel>
<dx:ASPxGridView ID="grv_FixedPrice" runat="server" Width="100%"
    AutoGenerateColumns="False" ClientInstanceName="grid_thamkhaogia" 
    KeyFieldName="productid" Settings-VerticalScrollBarMode="Auto" Settings-HorizontalScrollBarMode="Auto">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" 
            ShowInCustomizationForm="True">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="product_name" 
            ShowInCustomizationForm="True">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Nhóm hàng hóa" FieldName="productgrpid" 
            ShowInCustomizationForm="True" Visible="False">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Nhóm nhà sản xuất" 
            FieldName="manufacturergrpid" ShowInCustomizationForm="True" Visible="False">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manufacturerpid">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Nhóm nhà cung cấp" 
            FieldName="suppliergrppid" ShowInCustomizationForm="True" Visible="False">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplierpid">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="cost" 
            ShowInCustomizationForm="True">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Lợi nhuận" FieldName="profit" 
            ShowInCustomizationForm="True" >
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataMemoColumn Caption="Thuế (%)" FieldName="tax" 
            ShowInCustomizationForm="True">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataMemoColumn>
        <dx:GridViewDataTextColumn Caption="Giá mua" FieldName="income_price" 
            ShowInCustomizationForm="True"> 
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Giá bán tham khảo" FieldName="ref_price" 
            ShowInCustomizationForm="True">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Giá bán hiệu chỉnh" FieldName="fixedprice" 
            ShowInCustomizationForm="True">
            <Settings AllowDragDrop="False"></Settings>
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>