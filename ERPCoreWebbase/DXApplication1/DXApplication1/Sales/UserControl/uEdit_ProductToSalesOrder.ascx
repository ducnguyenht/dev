<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uEdit_ProductToSalesOrder.ascx.cs" Inherits="WebModule.GUI.usercontrol.uEdit_ProductToSalesOrder" %>
<style type="text/css">
    .style25
    {
    }
    .style31
    {
    }
    .style33
    {
        width: 98px;
    }
    .style37
    {
        width: 98px;
        height: 13px;
    }
    .style38
    {
        height: 13px;
    }
    .style39
    {
        height: 17px;
    }
    .style41
    {
        width: 254px;
        height: 13px;
    }
    .style42
    {
        width: 135px;
    }
    .style43
    {
    }
    .style44
    {
        width: 575px;
    }
    .style45
    {
        width: 135px;
        height: 13px;
    }
</style>
<dx:ASPxFormLayout ID="txt_productname" runat="server">
    <Items>
        <dx:LayoutGroup ShowCaption="False">
            <Items>
                <dx:LayoutItem Caption="Mã hàng hóa">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="cbo_dshanghoa" runat="server" IncrementalFilteringMode="Contains" TextField="productname">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="productid" Caption="Mã hàng hóa" />
                                    <dx:ListBoxColumn FieldName="productname" Caption="Tên hàng hóa" />
                                    <dx:ListBoxColumn FieldName="productgrpid" Caption="Nhóm hàng hóa" />
                                </Columns>
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tên hàng hóa">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_productname" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Đơn vị tính">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_unit" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxSpinEdit ID="txt_number" runat="server" Height="21px" Number="0">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Đơn giá">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_priceunit" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số lô">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="cbo_dslot" runat="server" IncrementalFilteringMode="Contains" TextField="lotid">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="lotid" Caption="Mã lô" />
                                    <dx:ListBoxColumn FieldName="lotname" Caption="Tên lô" />
                                </Columns>
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Hạn sử dụng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_duedate" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi chú">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="txt_note" runat="server" Height="16px" Width="388px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>

<table>
    <tr>
        <td align="left" class="style44">
            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                Text="Trợ Giúp" >
                <Image>
                    <SpriteProperties CssClass="Sprite_Help" />
                </Image>
            </dx:ASPxButton>
        </td>
        <td align="right">
            <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                ClientInstanceName="buttonSave" Text="Lưu Lại" >
                <Image>
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </dx:ASPxButton>
        </td>
        <td align="right">
            <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                ClientInstanceName="buttonCancel" Text="Bỏ Qua" >
                <Image>
                    <SpriteProperties CssClass="Sprite_Cancel" />
                </Image>
            </dx:ASPxButton>
        </td>
    </tr>
</table>