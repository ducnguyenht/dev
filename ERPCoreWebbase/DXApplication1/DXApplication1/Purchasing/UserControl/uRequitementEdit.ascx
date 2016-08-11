<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uRequitementEdit.ascx.cs"
    Inherits="ERPCore.Purchasing.UserControl.uRequitementEdit" %>
<style type="text/css">
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }
</style>
<div id="lineContainer">
    <dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formRequitementEdit" runat="server" HeaderText="Thông Tin Phiếu Yêu Cầu - "
                    Height="600px" Modal="True" Width="900px" ClientInstanceName="formRequitementEdit"
                    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
                                <Items>
                                    <dx:LayoutItem Caption="Mã Số">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" NullText="Tối đa 128 ký tự"
                                                    Width="200px">
                                                    <NullTextStyle ForeColor="Silver">
                                                    </NullTextStyle>
                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                        <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Nhân Viên Yêu Cầu">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboRowStatus0" runat="server" ClientInstanceName="cboRowStatus"
                                                    NullText="Tự động tạo mới" Width="200px">
                                                    <Items>
                                                        <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ngày Yêu Cầu">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="200px">
                                                </dx:ASPxDateEdit>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Mục Đích">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboRowStatus1" runat="server" ClientInstanceName="cboRowStatus"
                                                    NullText="Tự động tạo mới" Width="200px">
                                                    <Items>
                                                        <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="txtCode0" runat="server" ClientInstanceName="txtCode" Height="16px"
                                                    NullText="Tối đa 128 ký tự" Width="565px">
                                                    <NullTextStyle ForeColor="Silver">
                                                    </NullTextStyle>
                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                        <RequiredField ErrorText="Chưa nhập Mã Nhà Cung Cấp" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxPageControl runat="server" ActiveTabIndex="0" RenderMode="Classic" Width="100%"
                                Height="75%" ID="ASPxPageControl1">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="H&#224;ng H&#243;a">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdBuyingProductCategory">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số"
                                                            VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                                                                    </dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT"
                                                            VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần "
                                                            VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;"
                                                            VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%"
                                                            Caption="Thao T&#225;c" VisibleIndex="7">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Th&#234;m">
                                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="X&#243;a">
                                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="H&#224;ng H&#243;a"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>                                                
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Nguy&#234;n Vật Liệu">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdBuyingProductCategory0">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số"
                                                            VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                                                                    </dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT"
                                                            VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần "
                                                            VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;"
                                                            VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%"
                                                            Caption="Thao T&#225;c" VisibleIndex="7">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Th&#234;m">
                                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="X&#243;a">
                                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="Nguyên Vật Liệu"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Dịch Vụ">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdBuyingProductCategory1">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số"
                                                            VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                                                                    </dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT"
                                                            VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần "
                                                            VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;"
                                                            VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%"
                                                            Caption="Thao T&#225;c" VisibleIndex="7">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Th&#234;m">
                                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="X&#243;a">
                                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="Dịch Vụ"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="C&#244;ng Cụ Dụng Cụ">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdBuyingProductCategory2">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn ShowInCustomizationForm="True" Width="10%" Caption="M&#227; Số"
                                                            VisibleIndex="1">
                                                            <PropertiesComboBox>
                                                                <Columns>
                                                                    <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
                                                                    <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                                                                    </dx:ListBoxColumn>
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="ĐVT"
                                                            VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Thời Hạn Cần "
                                                            VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Ghi Ch&#250;"
                                                            VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" Width="10%"
                                                            Caption="Thao T&#225;c" VisibleIndex="7">
                                                            <EditButton Visible="True">
                                                                <Image ToolTip="Sửa">
                                                                    <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                </Image>
                                                            </EditButton>
                                                            <NewButton Visible="True">
                                                                <Image ToolTip="Th&#234;m">
                                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                </Image>
                                                            </NewButton>
                                                            <DeleteButton Visible="True">
                                                                <Image ToolTip="X&#243;a">
                                                                    <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                </Image>
                                                            </DeleteButton>
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="10%" Caption="Số Lượng Y&#234;u Cầu"
                                                            VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="30%" Caption="C&#244;ng Cụ"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Width="5%" Caption="STT"
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager Mode="ShowAllRecords" PageSize="30">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline"></SettingsEditing>
                                                    <Styles>
                                                        <CommandColumn Spacing="10px">
                                                        </CommandColumn>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</div>
