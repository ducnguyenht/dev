<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uViewConditionReceivePolicy.ascx.cs" Inherits="WebModule.GUI.usercontrol.uViewConditionReceivePolicy" %>
<dx:ASPxFormLayout ID="form_chinhsach" runat="server">
    <Items>
        <dx:LayoutItem Caption="Mã hàng hóa">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="lbl_productid" runat="server" Text="SP0001">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Tên hàng hóa">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="lbl_productname" runat="server" Text="Hàng hóa 1">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID="form_chinhsach_E1" runat="server" Font-Bold="True" 
                        Text="Danh mục chính sách ảnh hưởng đến hàng hóa">
                    </dx:ASPxLabel>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem ShowCaption="False">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grv_chinhsach" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã chính sách" FieldName="policyid" 
                                ShowInCustomizationForm="True" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên chính sách" FieldName="policyname" 
                                ShowInCustomizationForm="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewBandColumn Caption="Hiệu lực từ" ShowInCustomizationForm="True" 
                                VisibleIndex="2">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Từ" FieldName="from" 
                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đến" FieldName="to" 
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataTextColumn Caption="Thời gian trả lại (ngày)" FieldName="numofday" 
                                ShowInCustomizationForm="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn giá nhận" FieldName="priceunit"
                                ShowInCustomizationForm="True" VisibleIndex="4"
                            >
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Trách nhiệm" FieldName="reponsibility" 
                                ShowInCustomizationForm="True" VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>
</dx:ASPxFormLayout>

