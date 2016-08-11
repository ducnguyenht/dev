<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProdctPriceInventCaculation.ascx.cs" Inherits="WebModule.Accounting.UserControl.ProdctPriceInventCaculation" %>
<dx:ASPxFormLayout ID="form_config" runat="server">
    <Items>
        <dx:LayoutItem Caption="Phương pháp tính giá tồn kho">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxComboBox ID="cbo_caculation" runat="server">
                        <Items>
                            <dx:ListEditItem Text="FIFO" Value="FIFO" />
                            <dx:ListEditItem Text="LIFO" Value="LIFO" />
                            <dx:ListEditItem Text="FEFO" Value="FEFO" />
                            <dx:ListEditItem Text="BQGQ" Value="BQGQ" />
                            <dx:ListEditItem Text="Đích danh" Value="dinhdanh" />
                        </Items>
                    </dx:ASPxComboBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxButton ID="btn_save" runat="server" Text="Lưu lại">
                    </dx:ASPxButton>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>

