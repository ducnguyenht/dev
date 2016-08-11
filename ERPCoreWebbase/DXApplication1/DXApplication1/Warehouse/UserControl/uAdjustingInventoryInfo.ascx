<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uAdjustingInventoryInfo.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uAdjustingInventoryInfo" %>
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" Width="100%">
    <Items>
        <dx:LayoutItem Caption="Mã điều chỉnh">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px" ReadOnly="true">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Phiếu kiểm kho">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="170px" ReadOnly="true">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ngày điều chỉnh">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" ReadOnly="true">
                    </dx:ASPxDateEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Người điều chỉnh">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ReadOnly="true">
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ghi chú">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="100%" ReadOnly="true">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem ColSpan="2" ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách hàng hóa điều chỉnh" Font-Bold="True"
                        Font-Size="Small">
                    </dx:ASPxLabel>

                    <dx:ASPxGridView ID="grvConfirmSummary" Width="100%" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" ShowInCustomizationForm="True" VisibleIndex="0"
                                FieldName="code">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                                <FooterTemplate>
                                    Cộng
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" ShowInCustomizationForm="True" VisibleIndex="2"
                                FieldName="unit">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Lô" FieldName="lot" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="recieptamount" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                                <FooterTemplate>
                                    .........
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                                <FooterTemplate>
                                    .........
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" ShowInCustomizationForm="True"
                                VisibleIndex="7">
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                                <FooterTemplate>
                                    .........
                                </FooterTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewBandColumn Caption="Phẩm chất" VisibleIndex="8">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Còn tốt 100%">
                                        <DataItemTemplate>
                                            .........
                                        </DataItemTemplate>
                                        <FooterTemplate>
                                            .........
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Kém phẩm chất">
                                        <DataItemTemplate>
                                            .........
                                        </DataItemTemplate>
                                        <FooterTemplate>
                                            .........
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Mất phẩm chất">
                                        <DataItemTemplate>
                                            .........
                                        </DataItemTemplate>
                                        <FooterTemplate>
                                            .........
                                        </FooterTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataTextColumn Caption="SL điều chỉnh" ShowInCustomizationForm="True"
                                VisibleIndex="9" >
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" ShowInCustomizationForm="True"
                                VisibleIndex="10" >
                                <DataItemTemplate>
                                    .........
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </dx:LayoutItemNestedControlContainer>
        </LayoutItemNestedControlCollection>
    </dx:LayoutItem>
</Items>
</dx:ASPxFormLayout>