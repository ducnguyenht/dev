<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uVerifyingInventoryInfo.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uVerifyingInventoryInfo" %>
<dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
    <Items>
        <dx:LayoutItem Caption="Mã kiểm kho">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px" ReadOnly="true">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ngày kiểm kho">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ReadOnly="true">
                    </dx:ASPxDateEdit>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Trưởng ban">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" ReadOnly="true">
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Nhân viên 1">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxComboBox ID="ASPxComboBox5" runat="server" ReadOnly="true">
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Nhân viên 2">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxComboBox ID="ASPxComboBox6" runat="server" ReadOnly="true">
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Vị trí kho">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="lblPosition"  Text="........." runat="server" ReadOnly="true">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>
<div style="margin-bottom: 10px;">
    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Danh sách hàng hóa tồn kho" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>
</div>

<dx:ASPxGridView ID="grvConfirmData" Width="100%" runat="server" AutoGenerateColumns="False">
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
        <dx:GridViewBandColumn Caption="Phẩm chất" HeaderStyle-HorizontalAlign="Center" VisibleIndex="8">
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
        <dx:GridViewDataTextColumn Caption="Điều chỉnh" FieldName="entry" ShowInCustomizationForm="True"
            VisibleIndex="8" Visible="false">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsEditing Mode="PopupEditForm" />
    <Settings ShowFooter="true"/>
    <SettingsPopup>
        <EditForm Height="200px" Width="600px" />
    </SettingsPopup>
</dx:ASPxGridView>