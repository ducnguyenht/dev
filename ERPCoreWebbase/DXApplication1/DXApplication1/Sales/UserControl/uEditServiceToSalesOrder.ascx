<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uEditServiceToSalesOrder.ascx.cs" Inherits="WebModule.GUI.usercontrol.uEditServiceToSalesOrder" %>
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
                <dx:LayoutItem Caption="Mã dịch vụ">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="cbo_dsdichvu" runat="server" IncrementalFilteringMode="Contains" TextField="productname">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="serviceid" Caption="Mã dịch vụ" />
                                    <dx:ListBoxColumn FieldName="servicename" Caption="Tên tên dịch vụ" />
                                    <dx:ListBoxColumn FieldName="servicegrpid" Caption="Nhóm dịch vụ" />
                                </Columns>
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tên dịch vụ">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_servicename" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Đơn vị tính">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_unit" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxSpinEdit ID="txt_number" runat="server" Height="21px" Number="0">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Đơn giá">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxLabel ID="lbl_priceunit" runat="server" Text="ASPxLabel">
                            </dx:ASPxLabel>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi chú">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
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