<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uProductionCommandExecution.ascx.cs"
    Inherits="WebModule.Produce.UserControl.uProductionCommandExecution" %>
<%@ Register Src="uEditCommandExecution.ascx" TagName="uEditCommandExecution" TagPrefix="uc1" %>
<script type="text/javascript">
    function click_custombtn(s, e) {
        if (e.buttonID == 'edit_command' || e.buttonID == 'add_command')
            popup_editcommand.Show();
    }
</script>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Thực thi phiếu sản xuất" Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã phiếu sản xuất" FieldName="ID" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ngày thực thi" FieldName="Date" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Từ giờ thực tế" FieldName="RStart" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Đến giờ thực tế" FieldName="REnd" VisibleIndex="6">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Số lượng kế hoạch" FieldName="RQ" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Sản lượng thực tế" FieldName="Sl" VisibleIndex="7">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" VisibleIndex="8">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="11">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton Text="Sửa" ID="edit_command">
                    <Image ToolTip="Sửa">
                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
                <dx:GridViewCommandColumnCustomButton Text="Thêm" ID="add_command">
                    <Image ToolTip="Thêm">
                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
                <dx:GridViewCommandColumnCustomButton Text="Xóa" ID="delete_command">
                    <Image ToolTip="Xóa">
                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
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
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="Mã phiếu thực thi" FieldName="IDExecution" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mã Sản Phẩm" FieldName="ProductID" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Thành Tiền" FieldName="ExecutionValue" VisibleIndex="9">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
                <Settings ShowFilterRow="True" VerticalScrollableHeight="600" />
    <ClientSideEvents CustomButtonClick="click_custombtn" />
    <SettingsEditing Mode="Inline" />
    <Settings ShowFilterRow="True" />
    <BorderLeft BorderWidth="0px" />
    <BorderRight BorderWidth="0px" />
</dx:ASPxGridView>
<dx:ASPxPopupControl ID="popup_editcommand" ClientInstanceName="popup_editcommand"
    runat="server" AllowDragging="True" AllowResize="True" HeaderText="Thực thi phiếu sản xuất"
    Modal="True" PopupHorizontalAlign="WindowCenter" ShowSizeGrip = "False" PopupVerticalAlign="WindowCenter" RenderMode = "Classic"
    Width="850px" Height="600px" ShowFooter="true" ScrollBars="Auto">
    <FooterContentTemplate>
        <div style="width: 100%; vertical-align: middle">
            <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp"
                CssClass="float-left button-left-margin">
                <Image>
                    <SpriteProperties CssClass="Sprite_Help" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                CssClass="float-right button-right-margin" Text="Thoát">
                <Image>
                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="buttonAccept" ClientInstanceName="buttonSave" runat="server" Text="Lưu lại"
                CssClass="float-right button-right-margin" ClientVisible="true">
                <Image>
                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                </Image>
            </dx:ASPxButton>
        </div>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <uc1:uEditCommandExecution ID="uEditCommandExecution1" runat="server" />
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
