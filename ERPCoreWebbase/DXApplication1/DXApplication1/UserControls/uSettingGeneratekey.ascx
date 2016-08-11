<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uSettingGeneratekey.ascx.cs" Inherits="WebModule.UserControls.uSettingGeneratekey" %>
<dx:ASPxPopupControl ID="popupSettingGeneratekey" runat="server" HeaderText="Cấu Hình Mã Tự Động"
    Height="600px" Modal="True" Width="900px" ScrollBars="Auto" ClientInstanceName="popupSettingGeneratekey"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" CloseAction="CloseButton"
    ShowMaximizeButton="True">
    <FooterContentTemplate>
        <div id="Footer" style="display: inline; width: 100%;">
            <div style="display: inline; float: left">
                <dx:ASPxButton ID="btnHelpPopupSettingGeneratekey" AutoPostBack="false" runat="server"
                    Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Help" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="btnCancelPopupSettingGeneratekey" runat="server" AutoPostBack="False"
                    ClientInstanceName="btnCancelPopupSettingGeneratekey" Text="Đóng" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                    <ClientSideEvents Click="function(s,e){
                        popupSettingGeneratekey.Hide();
                    }" />
                </dx:ASPxButton>
            </div>
        </div>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                <Items>
                    <dx:LayoutGroup ColCount="2" ShowCaption="False">
                        <Items>
                            <dx:LayoutItem Caption="Mẫu" Width="100px">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="cboType" runat="server" SelectedIndex="1">
                                            <Items>
                                                <dx:ListEditItem Text="Mặc định" Value="0" />
                                                <dx:ListEditItem Text="Tự tạo" Value="1" Selected="True" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ghi chú">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="txtNote" runat="server" Width="320px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutGroup ShowCaption="False">
                        <Items>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Vị trí" FieldName="position" ShowInCustomizationForm="True" 
                                                    VisibleIndex="0">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Luật" FieldName="name" ShowInCustomizationForm="True" 
                                                    VisibleIndex="1">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Định dạng" FieldName="format" ShowInCustomizationForm="True" 
                                                    VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Giá trị" FieldName="value" ShowInCustomizationForm="True" 
                                                    VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Qui luật lặp"  FieldName="repeat_rule"
                                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="4" Caption="Di chuyển" ButtonType="Image">
                                                    <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton>
                                                            <Image ToolTip="Di chuyển lên">
                                                                <SpriteProperties CssClass="Sprite_MoveUp" />
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                        <dx:GridViewCommandColumnCustomButton>
                                                            <Image ToolTip="Di chuyển xuống">
                                                                <SpriteProperties CssClass="Sprite_MoveDown" />
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                            </dx:GridViewCommandColumn>
                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="5" Caption="Thao tác" ButtonType="Image">
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
                                                    <DeleteButton Visible="True">
                                                        <Image ToolTip="Xóa">
                                                            <SpriteProperties CssClass="Sprite_Delete" />
                                                        </Image>
                                                    </DeleteButton>
                                                    <ClearFilterButton Visible="True">
                                                        <Image ToolTip="Hủy">
                                                            <SpriteProperties CssClass="Sprite_Clear" />
                                                        </Image>
                                                    </ClearFilterButton>
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
                                            </dx:GridViewCommandColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ví dụ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="txtExample" runat="server" Width="170px" 
                                            Text="QUA012013" ReadOnly="True">
                                        </dx:ASPxTextBox>
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

