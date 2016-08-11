<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uHumanRequirement.ascx.cs"
    Inherits="WebModule.Produce.UserControl.uHumanRequirement" %>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Danh sách nhu cầu nhân sự" Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%"
    OnHtmlRowCreated="ASPxGridView1_HtmlRowCreated">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Cấp bậc" FieldName="Degree" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Quantity" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Thời lượng (giờ)" FieldName="Time" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Chi phí ước tính" FieldName="Total" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="7">
            <EditButton Visible="True">
                <Image>
                    <SpriteProperties CssClass="Sprite_Edit" />
                </Image>
            </EditButton>
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
            <dx:ASPxGridView ID="GridDetail" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Tên nhu cầu sản xuất" FieldName="PRName" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên sản phẩm" FieldName="PName" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
                <Settings ShowFilterRow="True" VerticalScrollableHeight="200" />
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>
