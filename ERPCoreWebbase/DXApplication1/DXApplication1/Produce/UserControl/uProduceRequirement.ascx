<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uProduceRequirement.ascx.cs"
    Inherits="WebModule.Produce.UserControl.uProduceRequirement" %>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Danh sách nhu cầu sản xuất" Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" OnHtmlRowCreated="ASPxGridView1_HtmlRowCreated"
    Width="100%">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã nhu cầu" FieldName="ID" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên nhu cầu" FieldName="Name" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đề xuất bởi" FieldName="RequiredBy" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Des" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="5">
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
    <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
    <SettingsEditing Mode="Inline" />
    <Settings ShowFilterRow="True" VerticalScrollableHeight="600" />
    <SettingsDetail ShowDetailRow="True" />
    <Styles>
        <Header HorizontalAlign="Center">
        </Header>
    </Styles>
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="GridDetail" runat="server" AutoGenerateColumns="False" Width = "100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Mã sản phẩm" FieldName="ID" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên sản phẩm" FieldName="Name" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="Unit" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Quantity" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="TH hoàn thành nhanh nhất" FieldName="ShortestTime" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="TH yêu cầu" FieldName="Time" VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Độ ưu tiên" FieldName="Priority" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="8">
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
                <SettingsPager Mode="EndlessPaging" RenderMode="Classic">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <Settings ShowFilterRow="True" />
                <Styles>
                    <Header HorizontalAlign="Center">
                    </Header>
                </Styles>
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>
