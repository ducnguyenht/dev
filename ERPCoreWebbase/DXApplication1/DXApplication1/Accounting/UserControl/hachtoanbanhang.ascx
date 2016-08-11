<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hachtoanbanhang.ascx.cs"
    Inherits="SamNgocLinh.UserControl.WebUserControl1" %>
<style type="text/css">
    .float_right
    {
        float: right;    
    }
    .mg
    {
        margin-top: 10px;
        margin-bottom: 10px;
    }
    img
    {
        border-width: 0px;
    }
    
    .dxflVATSys
    {
        vertical-align: top;
    }
    .dxflHARSys
    {
        text-align: right;
    }
    .dxeTrackBar_DevEx, .dxeIRadioButton_DevEx, .dxeButtonEdit_DevEx, .dxeTextBox_DevEx, .dxeRadioButtonList_DevEx, .dxeCheckBoxList_DevEx, .dxeMemo_DevEx, .dxeListBox_DevEx, .dxeCalendar_DevEx, .dxeColorTable_DevEx
    {
        -webkit-tap-highlight-color: rgba(0,0,0,0);
    }
    
    .dxeTextBox_DevEx, .dxeButtonEdit_DevEx, .dxeIRadioButton_DevEx, .dxeRadioButtonList_DevEx, .dxeCheckBoxList_DevEx
    {
        cursor: default;
    }
    
    
    .dxeTextBox_DevEx, .dxeMemo_DevEx
    {
        background-color: white;
        border-top: 1px solid #9da0aa;
        border-right: 1px solid #c2c4cb;
        border-bottom: 1px solid #d9dae0;
        border-left: 1px solid #c2c4cb;
    }
    td.dxic
    {
        font-size: 0;
    }
    .dxeTextBox_DevEx .dxeEditArea_DevEx
    {
        background-color: white;
    }
    
    .dxeEditArea_DevEx.dxeEditAreaSys
    {
        font: 11px Verdana, Geneva, sans-serif;
        height: 13px;
        margin-bottom: 1px;
        border-top: 1px solid #9da0aa;
        border-right: 1px solid #c2c4cb;
        border-bottom: 1px solid #d9dae0;
        border-left: 1px solid #c2c4cb;
    }
    .dxeMemoEditArea_DevEx, input.dxeEditArea_DevEx
    {
        outline: none;
    }
    
    .dxflHARSys > table, .dxflHARSys > div
    {
        margin-left: auto;
        margin-right: 0px;
    }
    .dxflHACSys
    {
        text-align: center;
    }
    .dxflHACSys > table, .dxflHACSys > div
    {
        margin-left: auto;
        margin-right: auto;
    }
    .dxeBase_DevEx
    {
        font: 11px Verdana, Geneva, sans-serif;
    }
</style>
<dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" AllowDragging="True" ClientInstanceName="pop_chdk"
    HeaderText="Cấu hình định khoản" Height="401px" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" Width="750px">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="2" 
                Width="100%">
                <TabPages>
                    <dx:TabPage Text="Hàng hóa">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
                                    Width="100%" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="      " ShowCaption="True" Width="100px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E3" runat="server" HorizontalAlign="Center" 
                                                        Text="Số TK" Width="170px" Height="17px">
                                                        <Border BorderStyle="None" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" 
                                            ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" HorizontalAlign="Center" 
                                                        Text="TK nợ" Width="40px">
                                                        <Border BorderStyle="None" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" 
                                            ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" HorizontalAlign="Center" 
                                                        Text="TK có" Width="40px">
                                                        <Border BorderStyle="None" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="170px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer Width="170px" runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E10" runat="server" 
                                                        Text="Tổng tiền trước CK (chưa thuế) (1)" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                            <CaptionSettings HorizontalAlign="Right" />
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" 
                                            ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer Width="170px" runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" 
                                            ShowCaption="False" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E7" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="170px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout1_E12" runat="server" 
                                                        Text="Thuế trên tổng tiền trước CK (2)">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E5" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E8" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="170px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout1_E11" runat="server" 
                                                        Text="Tổng tiền trước CK (có thuế) (3) = (1) + (2)">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E6" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout1_E9" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Diễn giải" ColSpan="3" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True" Width="200px">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout1_E13" runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                    <SettingsItems ShowCaption="False" />
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Dịch vụ">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" AlignItemCaptionsInAllGroups="True"
                                    Width="100%">
                                    <Items>
                                        <dx:LayoutGroup Caption="Hàng hóa" ColCount="3" ShowCaption="False">
                                            <Items>
                                                <dx:LayoutItem Caption="      " ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E46" runat="server" HorizontalAlign="Center"
                                                                Text="Số TK" Width="170px">
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E47" runat="server" HorizontalAlign="Center"
                                                                Text="TK nợ" Width="40px">
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E48" runat="server" HorizontalAlign="Center"
                                                                Text="TK có" Width="40px">
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tổng tiền trước CK (chưa thuế) (1)" HorizontalAlign="Right"
                                                    ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E49" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <CaptionSettings HorizontalAlign="Right" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" ShowCaption="False"
                                                    Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E50" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E51" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Thuế trên tổng tiền trước CK (2)" HorizontalAlign="Right"
                                                    ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E52" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E53" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E54" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tổng tiền trước CK (có thuế) (3) = (1) + (2)" HorizontalAlign="Right"
                                                    ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E55" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E56" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E57" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Diễn giải" ColSpan="3" ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E58" runat="server" Width="100%">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                            <SettingsItemCaptions HorizontalAlign="Right" />
                                            <SettingsItems ShowCaption="False" />
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Khuyến mãi">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" AlignItemCaptionsInAllGroups="True"
                                    Width="100%">
                                    <Items>
                                        <dx:LayoutGroup Caption="Hàng hóa" ColCount="3" ShowCaption="False">
                                            <Items>
                                                <dx:LayoutItem Caption="      " ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E68" runat="server" HorizontalAlign="Center"
                                                                Text="Số TK" Width="170px">
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E69" runat="server" HorizontalAlign="Center"
                                                                Text="TK nợ" Width="40px">
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer32" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E70" runat="server" HorizontalAlign="Center"
                                                                Text="TK có" Width="40px">
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tổng giá trị khuyến mãi(chưa thuế) (1)" HorizontalAlign="Right"
                                                    ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E71" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                    <CaptionSettings HorizontalAlign="Right" />
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" ShowCaption="False"
                                                    Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer34" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E72" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer35" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E73" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Thuế trên tổng giá trị khuyến mãi (2)" HorizontalAlign="Right"
                                                    ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer36" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E74" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer37" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E75" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer38" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E76" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Tổng tiền giá trị khuyến mãi (có thuế) (3) = (1) + (2)" HorizontalAlign="Right"
                                                    ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer39" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E77" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer40" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E78" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Layout Item" HorizontalAlign="Center" Width="40px">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer41" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxCheckBox ID="ASPxFormLayout1_E79" runat="server" CheckState="Unchecked">
                                                                <CheckBoxStyle HorizontalAlign="Center" />
                                                                <Border BorderStyle="None"></Border>
                                                            </dx:ASPxCheckBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Diễn giải" ColSpan="3" ShowCaption="True">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer42" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E80" runat="server" Width="100%">
                                                            </dx:ASPxTextBox>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                            <SettingsItemCaptions HorizontalAlign="Right" />
                                            <SettingsItems ShowCaption="False" />
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
            <dx:ASPxButton ID="ASPxButton1" runat="server" CssClass="float_right mg" Text="Lưu">
                <Image ToolTip="Lưu">
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </dx:ASPxButton>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
