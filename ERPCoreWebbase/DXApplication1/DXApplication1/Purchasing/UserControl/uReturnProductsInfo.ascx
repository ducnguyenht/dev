<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uReturnProductsInfo.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.uReturnProductsInfo" %>
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
    <Items>
        <dx:LayoutItem Caption="Mã phiếu trả hàng">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_finish_E1" runat="server" Enabled="False" 
                        Text="TH00001" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ngày tạo phiếu">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxDateEdit ID="form_finish_E2" runat="server" Date="2013-08-10" 
                        Enabled="False">
                    </dx:ASPxDateEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Người tạo">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="form_finish_E3" runat="server" Enabled="False" 
                        Text="Nguyễn Văn A" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>
<br />
<dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách hàng trả" Font-Bold="True"
    Font-Size="Small">
</dx:ASPxLabel>
<dx:ASPxGridView ID="gridview_backProduct" runat="server" AutoGenerateColumns="False"
    Width="100%">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
            VisibleIndex="0" Width="150px">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
            VisibleIndex="1">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
            VisibleIndex="2" Width="100px">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="unitid" ShowInCustomizationForm="True"
            VisibleIndex="3" Width="60px">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Số lượng trả" FieldName="numberofback" ShowInCustomizationForm="True"
            VisibleIndex="4" Width="100px">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
            <CellStyle HorizontalAlign="Right">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Hạn sản xuất" FieldName="duedate" ShowInCustomizationForm="True"
            VisibleIndex="5" Width="100px">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="unitprice" ShowInCustomizationForm="True"
            VisibleIndex="8" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True"
            VisibleIndex="9" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Lý do" FieldName="reason" ShowInCustomizationForm="True"
            VisibleIndex="10">
            <HeaderStyle Font-Bold="true" HorizontalAlign="Center" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tình trạng" FieldName="status" ShowInCustomizationForm="True"
            VisibleIndex="11" Visible="false">
            <DataItemTemplate>
                .........
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>
    <Settings ShowFilterRow="True" />
    <SettingsPager PageSize="10" ShowEmptyDataRows="true">
    </SettingsPager>
    <SettingsBehavior ColumnResizeMode="Control" />
    <Settings ShowFilterRow="True"></Settings>
</dx:ASPxGridView>
