<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPlanningDetail.ascx.cs"
    Inherits="WebModule.Produce.UserControl.uPlanningDetail" %>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Hoạch định chi tiết" Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" KeyFieldName="ID" runat="server" AutoGenerateColumns="False"
    OnHtmlRowCreated="ASPxGridView1_HtmlRowCreated" Width="100%" >
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã sản phẩm" FieldName="ID" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên sản phẩm" FieldName="Name" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Số lượng yêu cầu" FieldName="Quantity" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="Unit" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Độ ưu tiên" FieldName="Priority" VisibleIndex="6">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
    <Settings ShowFilterRow="True" VerticalScrollableHeight="600" />
    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
    <Styles>
        <Header HorizontalAlign="Center">
        </Header>
    </Styles>
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="GridDetail1" KeyFieldName="ID" runat="server" AutoGenerateColumns="False"
                SettingsDetail-ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" OnHtmlRowCreated="GridDetail1_HtmlRowCreated"
                Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="No" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Số lượng ( /1 lô )" FieldName="Quantity" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="3">
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
                <Settings ShowFilterRow="True" VerticalScrollableHeight="400" />
                <Templates>
                    <DetailRow>
                        <dx:ASPxGridView ID="GridDetail2" runat="server" AutoGenerateColumns="False" Width="100%"
                            KeyFieldName="ID">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã công đoạn" FieldName="ID" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên công đoạn" FieldName="Name" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewBandColumn Caption="Thời lượng" VisibleIndex="2">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Thời lượng" FieldName="Time" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị thời lượng" FieldName="Type" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Thời điểm có thể bắt đầu" VisibleIndex="5">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Giờ" FieldName="Start_h" VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày" FieldName="Start_d" VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="Thời điểm kết thúc dự kiến" VisibleIndex="8">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Giờ" FieldName="End_h" VisibleIndex="9">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày" FieldName="End_d" VisibleIndex="10">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                            </Columns>
                            <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
                <Settings ShowFilterRow="True" VerticalScrollableHeight="200" />
                            <Styles>
                                <Header HorizontalAlign="Center">
                                </Header>
                            </Styles>
                        </dx:ASPxGridView>
                    </DetailRow>
                </Templates>
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>
<!--

-->
