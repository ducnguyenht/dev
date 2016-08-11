<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uConfigEntry.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uConfigEntry" %>
<style type="text/css">
    .style1
    {
        width: 522px;
    }
    </style>
<script type="text/javascript">
    function OnBodyKeyPress(event) {
        if (event.keyCode == 13
        )
            grdata_detail.AddNewRow();
        else {
            if (event.keyCode == 17) {
                grdata_detail.UpdateEdit();
            }
        }
    }
</script>
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px" ClientInstanceName="cp_ce"
    OnCallback="ASPxCallbackPanel1_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" RenderMode="Lightweight"
                ClientInstanceName="pop_configentry" CloseAction="CloseButton" HeaderText="Cấu Hình Định Khoản"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                Width="874px" Height="281px">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <body>
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" 
                            Width="100%">
                                <Items>
                                    <dx:LayoutItem Caption="Phân loại">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="ASPxFormLayout1_E2" runat="server">
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Hệ thống tài khoản">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="ASPxFormLayout1_E3" runat="server">
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Diễn giải" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%">
                                                    <FocusedStyle Wrap="True">
                                                    </FocusedStyle>
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxGridView ID="grdata_detail" runat="server" AutoGenerateColumns="False" Width="100%"
                                KeyFieldName="SoTaiKhoan" ClientInstanceName="grdata_detail" SettingsEditing-Mode="Inline">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="STT" FieldName="No" 
                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="5%">
                                        <Settings AllowAutoFilter="False" AllowAutoFilterTextInputTimer="False" 
                                            AllowHeaderFilter="False" ShowFilterRowMenu="False" 
                                            ShowFilterRowMenuLikeItem="False" ShowInFilterControl="False" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Mục" FieldName="Item" 
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Số Tài Khoản" FieldName="SoTaiKhoan" 
                                        VisibleIndex="2" Width="15%">
                                        <DataItemTemplate>
                                            <dx:ASPxComboBox ID="ASPxComboBox1" Value='<%# Eval("SoTaiKhoan") %>' runat="server"
                                                ValueType="System.String">
                                                <Items>
                                                    <dx:ListEditItem Text="131" Value="131" />
                                                </Items>
                                                <Items>
                                                    <dx:ListEditItem Text="111" Value="111" />
                                                </Items>
                                                <Items>
                                                    <dx:ListEditItem Text="112" Value="112" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </DataItemTemplate>
                                        <PropertiesComboBox>
                                            <Items>
                                                <dx:ListEditItem Text="131" Value="131" />
                                                <dx:ListEditItem Text="111" Value="111" />
                                                <dx:ListEditItem Text="112" Value="112" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Phát sinh" FieldName="NoCo" 
                                        VisibleIndex="3" Width="15%">
                                        <DataItemTemplate>
                                            <dx:ASPxComboBox ID="ASPxComboBox2" Value='<%# Bind("NoCo") %>' runat="server" ValueType="System.String">
                                                <Items>
                                                    <dx:ListEditItem Text="Nợ" Value="No" />
                                                </Items>
                                                <Items>
                                                    <dx:ListEditItem Text="Có" Value="Co" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </DataItemTemplate>
                                        <PropertiesComboBox>
                                            <Items>
                                                <dx:ListEditItem Text="Nợ" Value="No" />
                                                <dx:ListEditItem Text="Có" Value="Co" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                        VisibleIndex="4" Width="12%">
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
                                    </dx:GridViewCommandColumn>
                                </Columns>

<SettingsEditing Mode="Inline"></SettingsEditing>

                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                <Styles>
                                    <Header Font-Bold="True" HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                            <table style="width: 100%">
                                <tr>
                                    <td class="style1">
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Trợ Giúp">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Help" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="Lưu Lại">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right" width = "90px">
                                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Bỏ Qua" AutoPostBack="False"
                                            ClientInstanceName="btncancelconfigentry">
                                            <ClientSideEvents Click=" btncancelconfigentry_click" />
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </body>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
