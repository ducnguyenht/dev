<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uChungTuGoc_2.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uChungTuGoc_2" %>
<script type="text/javascript">
    function bt_click_1(s, e) {
        alert("Hiển thị file chứng từ gốc");
    }
</script>
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px" ClientInstanceName="cbCTG">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" HeaderText="Chứng từ liên quan" Height = "600px"
                RenderMode="Classic" Width="850px" ClientInstanceName="popctg" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" ShowMaximizeButton = "true" ShowFooter = "true" ShowSizeGrip = "False">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
                            Height="55px" Text="Danh sách chứng từ liên quan" Width="300px">
                            <Border BorderStyle="None"></Border>
                        </dx:ASPxTextBox>
                        <dx:ASPxGridView ID="ASPxGridView_ctg" runat="server" AutoGenerateColumns="False"
                            Width="100%">
                            <ClientSideEvents CustomButtonClick="bt_click_1"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Mã chứng từ" FieldName="ID" ShowInCustomizationForm="True"
                                    VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên chứng từ" FieldName="Name" ShowInCustomizationForm="True"
                                    VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số hiệu" FieldName="Sign" ShowInCustomizationForm="True"
                                    VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Thanh tiền" FieldName="Total" ShowInCustomizationForm="True"
                                    VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Còn nợ" FieldName="CN" ShowInCustomizationForm="True"
                                    VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Tài liệu đính kèm" ShowInCustomizationForm="True"
                                    VisibleIndex="5">
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="CTG">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Thuế (%)" FieldName="Tax" ShowInCustomizationForm="True"
                                    VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="7">
                                    <EditButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <CancelButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                        </Image>
                                    </CancelButton>
                                    <UpdateButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                        </Image>
                                    </UpdateButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <Settings ShowFilterRow="True"></Settings>
                        </dx:ASPxGridView>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
