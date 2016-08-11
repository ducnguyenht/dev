<%@ Control Language="C#" ClientIDMode ="AutoID"  AutoEventWireup="true" CodeBehind="uBringBackProduct.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uBringBackProduct" %>
<dx:ASPxPopupControl ID="bringback" runat="server" HeaderText="Hạch toán hàng trả"
    Height="600px" Modal="True" RenderMode="Classic" Width="850px" ShowFooter="true"
    ShowMaximizeButton="true" ShowSizeGrip="False" 
    ClientInstanceName="popup_approvebr" PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter">
    <FooterContentTemplate>
            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                Text="Trợ Giúp" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Help" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton4" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin"
                Text="Bỏ qua" Wrap="False">                
                <Image>
                    <SpriteProperties CssClass="Sprite_Cancel" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton2" AutoPostBack="false" runat="server" CssClass="float-right hd button-right-margin"
                Text="Tiếp theo" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Forward" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin"
                Text="Duyệt" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Approve" />
                </Image>
            </dx:ASPxButton>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                <Items>
                    <dx:LayoutGroup Caption="Thông tin" ColCount="2" GroupBoxDecoration="HeadingLine">
                        <Items>
                            <dx:LayoutItem Caption="Mã phiếu trả hàng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="ASPxFormLayout1_E1" runat="server">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem>
                            </dx:EmptyLayoutItem>
                            <dx:LayoutItem Caption="Ngày tạo">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Người tạo">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" Width="170px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tổng tiền">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E4" runat="server" Width="170px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup Caption="Hạch toán" GroupBoxDecoration="HeadingLine">
                        <Items>
                            <dx:LayoutItem Caption="Diễn giải">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="ASPxFormLayout1_E5" runat="server" Width="100%">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                            Settings-ShowFooter="true">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="STT" FieldName="No" ShowInCustomizationForm="True"
                                                    VisibleIndex="0" Width="35px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Mục" FieldName="Item" ShowInCustomizationForm="True"
                                                    VisibleIndex="1">
                                                    <FooterTemplate>
                                                        <dx:ASPxTextBox runat="server" ID="item" Text="Tổng tiền phiếu trả hàng" Width="100%">
                                                        </dx:ASPxTextBox>
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="TK nợ" FieldName="TKno" ShowInCustomizationForm="True"
                                                    VisibleIndex="2" Width="70px">
                                                    <FooterTemplate>
                                                        <dx:ASPxComboBox runat="server" ID="tkno" Width="100%">
                                                        </dx:ASPxComboBox>
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="TK có" FieldName="TKco" ShowInCustomizationForm="True"
                                                    VisibleIndex="3" Width="70px">
                                                    <FooterTemplate>
                                                        <dx:ASPxComboBox runat="server" ID="tkno" Width="100%" NullText = "000">
                                                        </dx:ASPxComboBox>
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Giá trị" FieldName="Amount" ShowInCustomizationForm="True"
                                                    VisibleIndex="4" Width="100px">
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                    <FooterTemplate>
                                                        <dx:ASPxTextBox runat="server" ID="amount" Text="3.000.000" Width="100%" HorizontalAlign = "Right">
                                                        </dx:ASPxTextBox>
                                                    </FooterTemplate>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" ShowInCustomizationForm="True"
                                                    VisibleIndex="5">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                    VisibleIndex="6" Width="80px">
                                                    <EditButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                        </Image>
                                                    </EditButton>
                                                    <NewButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New" />
                                                        </Image>
                                                    </NewButton>
                                                    <DeleteButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                        </Image>
                                                    </DeleteButton>
                                                    <CancelButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                        </Image>
                                                    </CancelButton>
                                                    <UpdateButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                        </Image>
                                                    </UpdateButton>
                                                    <ClearFilterButton Visible="True">
                                                    </ClearFilterButton>
                                                </dx:GridViewCommandColumn>
                                            </Columns>
                                            <SettingsBehavior ColumnResizeMode="Control" />
                                            <Settings ShowFooter="True"></Settings>
                                            <Styles>
                                                <Header HorizontalAlign="Center">
                                                </Header>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
