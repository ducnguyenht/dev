<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uSplit.ascx.cs" Inherits="WebModule.PayReceiving.UserControl.uSplit" %>
<style type="text/css">
.float_right
{
    float:right;
}
</style>
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="PopupSplit" runat="server" CloseAction="CloseButton" DragElement="Window"
                HeaderText="Tách phiếu thu" RenderMode="Lightweight" Width="650px" 
                AllowDragging="True" ClientInstanceName="popsplit_v" 
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" 
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Phiếu Thu Nguồn" ColCount="2" 
                                    GroupBoxDecoration="HeadingLine" ColSpan="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã phiếu thu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E1" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tổng tiền phiếu thu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit ID="ASPxFormLayout1_E2" runat="server" Height="21px" Number="0">
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutGroup Caption="Phiếu Thu Đích" GroupBoxDecoration="HeadingLine" 
                                    ColSpan="2">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Mã Phiếu Thu Đích" FieldName="Code" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đối Tượng Nộp Tiền" FieldName="NN" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                                <FooterTemplate>
                                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Tổng">
                                                                    </dx:ASPxLabel>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã Chứng Từ" FieldName="ID" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                                <FooterTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="Des" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="5">
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
                                                        <SettingsEditing Mode="PopupEditForm" />
                                                        <Settings ShowFilterRow="True" ShowFooter="True" />
                                                        <SettingsPopup>
                                                            <EditForm ShowHeader="false"  HorizontalAlign="WindowCenter" MinWidth="600px" 
                                                                VerticalAlign="WindowCenter" />
                                                        </SettingsPopup>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:LayoutItem ShowCaption="False" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxButton ID="ASPxFormLayout1_E5" runat="server" CssClass="float_right" 
                                                Text="Đồng ý">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" Width="100px">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxButton ID="ASPxFormLayout1_E4" runat="server" CssClass="float_right" 
                                                Text="Bỏ qua">
                                                <ClientSideEvents Click="function(s, e) {
	popsplit_v.Hide();
}" />
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
