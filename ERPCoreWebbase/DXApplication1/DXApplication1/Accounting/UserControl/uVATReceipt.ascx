<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode = "AutoID" CodeBehind="uVATReceipt.ascx.cs" Inherits="WebModule.Accounting.UserControl.uVATReceipt" %>
<dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
    Font-Size="Medium" Height="35px" Text="Danh sách hóa đơn GTGT">
</dx:ASPxLabel>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width = "100%">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Phân loại" FieldName="Type" 
            VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Quyển số" FieldName="QS" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mẫu số" FieldName="MS" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ký hiệu" FieldName="KH" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Từ số" FieldName="From" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đến số" FieldName="To" VisibleIndex="6">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ngày phát hành" FieldName="Date" 
            VisibleIndex="7">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đơn vị sử dụng" FieldName="DV" 
            VisibleIndex="8">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="Status" 
            VisibleIndex="9">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="10">
                <EditButton Visible="True">
                    <Image ToolTip="Sửa">
                        <SpriteProperties CssClass="Sprite_Edit" />
                    </Image>
                </EditButton>
                <NewButton Visible="True">
                    <Image ToolTip="Thêm">
                        <SpriteProperties CssClass="Sprite_New" />
                    </Image>
                </NewButton>
                <DeleteButton>
                    <Image ToolTip="Xóa">
                        <SpriteProperties CssClass="Sprite_Delete" />
                    </Image>
                </DeleteButton>
                <UpdateButton>
                    <Image ToolTip="Cập nhật">
                        <SpriteProperties CssClass="Sprite_Apply" />
                    </Image>
                </UpdateButton>
                <CancelButton>
                    <Image ToolTip="Bỏ qua">
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                </CancelButton>
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear" />
                    </Image>
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
    </Columns>
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
        ShowHeaderFilterButton="True" />
    <SettingsDetail ShowDetailRow="True" />
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                    Width="100%" OnBeforePerformDataSelect = "DetailPerformData">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Số hóa đơn" FieldName = "code" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày sử dụng" FieldName = "date" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName = "status" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>

