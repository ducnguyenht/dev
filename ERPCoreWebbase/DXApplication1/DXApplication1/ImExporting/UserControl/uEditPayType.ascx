<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditPayType.ascx.cs" Inherits="WebModule.ImExporting.UserControl.uEditPayType" %>
<dx:ASPxPageControl ID="pc_editPayType" runat="server" RenderMode="Classic" Width="100%"
    ActiveTabIndex="0">
    <TabPages>
        <dx:TabPage Text="Thông tin chung">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxFormLayout ID="form_editCommonInfo" runat="server" Width="100%">
                        <Items>
                            <dx:LayoutItem Caption="Tên loại hình thanh toán">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxTextBox ID="txt_name" runat="server">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Mô tả">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxMemo ID="txt_des" runat="server" Width="100%">
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Trạng thái">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>
                                        <dx:ASPxComboBox ID="cbo_status" runat="server">
                                            <Items>
                                                <dx:ListEditItem Value="0" Text="Đang hoạt động" />
                                                <dx:ListEditItem Value="1" Text="Ngưng sử dụng" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Quy trình thanh toán">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxGridView ID="grv_PaymentProcess" runat="server" KeyFieldName="id" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Bước" FieldName="seq" Width="30px" >
                                </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Bước xử lý" FieldName="id" Width="200px">
                                <DataItemTemplate>
                                    <dx:ASPxComboBox ID="cbo_processID" runat="server" Width="100%" ValueField="id">
                                        <Items>
                                            <dx:ListEditItem  Value="0" Text="Ký hợp đồng"/>
                                            <dx:ListEditItem  Value="1" Text="Chuyển tiền"/>
                                            <dx:ListEditItem  Value="2" Text="Chyển hàng"/>
                                            <dx:ListEditItem  Value="3" Text="Chứng từ về"/>
                                            <dx:ListEditItem  Value="4" Text="Hàng về"/>
                                            <dx:ListEditItem  Value="5" Text="Kê khai và làm thủ tục hải quan"/>
                                            <dx:ListEditItem  Value="6" Text="Lấy hàng"/>
                                        </Items>
                                    </dx:ASPxComboBox>
                                </DataItemTemplate>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" Width="300px" >
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note">
                                <DataItemTemplate>
                                    <dx:ASPxMemo ID="txt_note" Width="100%" runat="server">
                                    </dx:ASPxMemo>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn Caption="Thao tác" ButtonType="Image">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton>
                                        <Image ToolTip="Thêm xử lý">
                                            <SpriteProperties CssClass="Sprite_New" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton>
                                        <Image ToolTip="Xóa xử lý">
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
