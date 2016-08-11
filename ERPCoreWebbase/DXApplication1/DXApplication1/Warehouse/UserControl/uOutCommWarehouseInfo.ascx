<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uOutCommWarehouseInfo.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uOutCommWarehouseInfo" %>

        <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" EnableTheming="True" Theme="Aqua"
            ColCount="2">
            <Items>
                <dx:LayoutItem Caption="Mã phiếu xuất">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Enabled="False" Text="MS0001" Width="170px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Người tạo">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Enabled="False" Text="Nhân viên 1"
                                Width="170px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày tạo">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Text="27-07-2013" Width="170px"
                                Enabled="False">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Thủ kho">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="ASPxComboBox5" runat="server">
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Kho">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="ASPxComboBox6" runat="server">
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:ASPxFormLayout>
        <br />
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách mặt hàng xuất kho" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    <dx:ASPxGridView ID="grvSummaryData" runat="server" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True"
                Caption="Thuộc chứng từ" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="code" ShowInCustomizationForm="True"
                VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="name" ShowInCustomizationForm="True"
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Số lượng theo CT" FieldName="amount" ShowInCustomizationForm="True"
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Số lượng thực tế"  ShowInCustomizationForm="True"
                VisibleIndex="5">
                <DataItemTemplate>
                    .........
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Chênh lệch" ShowInCustomizationForm="True"
                VisibleIndex="6">
                <DataItemTemplate>
                    .........
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
        <SettingsPager PageSize="10" ShowEmptyDataRows="true"></SettingsPager>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
        </Settings>
    </dx:ASPxGridView>