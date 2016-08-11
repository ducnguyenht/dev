<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookAccountEdit2.ascx.cs" Inherits="ERPCore.Accounting.UserControl.BookAccountEdit2" %>
<style type="text/css">

    img
    {
        border-width: 0px;
    }
    
    .dxflHARSys
    {
        text-align: right;
    }
        
    .dxflVATSys
    {
        vertical-align: top;
    }
    td.dxic
    {
        font-size: 0;
    }
    .dxflHACSys
    {
        text-align: center;
    }
    .dxflHACSys > div
    {
        margin-left: auto;
        margin-right: auto;
    }
        
    .dxflHARSys > table
    {
        margin-left: auto;
        margin-right: 0px;
    }
    .mg
    {
        margin-top: 10px;
        margin-bottom: 10px;
    }
    .float_right
    {
        float: right;    
    }
    </style>
    
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1111" runat="server" Width="200px" 
    ClientInstanceName ="cp1" oncallback="ASPxCallbackPanel1_Callback">
<PanelCollection>
<dx:PanelContent ID = "cbpanel" runat = "server" >
<dx:ASPxPopupControl ID="formBookAccountEdit2" runat="server" AllowDragging="True" ClientInstanceName="pop_chdkthuchi"
    HeaderText="Cấu hình định khoản phiếu" Height="311px" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" Width="750px">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" Width="100%" 
                ColCount="4" AlignItemCaptionsInAllGroups="True">
                <Items>
                    <dx:LayoutItem>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout4_E6" runat="server" HorizontalAlign="Center" 
                                    Text="TK nợ" Width="170px">
                                    <Border BorderStyle="None" />
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout4_E9" runat="server" HorizontalAlign="Center" 
                                    Text="TK có" Width="170px">
                                    <Border BorderStyle="None" />
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Right">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout4_E1" runat="server" Text="Tổng tiền phiếu chi" 
                                    Width="170px">
                                    <Border BorderStyle="None" />
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxComboBox ID="ASPxFormLayout4_E3" runat="server">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxComboBox ID="ASPxFormLayout4_E5" runat="server">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Diễn giải" ColSpan="4" ShowCaption="True">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" 
                                    Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <SettingsItems ShowCaption="False" />
                <Border BorderStyle="Solid" BorderWidth="1px" />
            </dx:ASPxFormLayout>
            <dx:ASPxButton ID="ASPxButton1" runat="server" CssClass="float_right mg" Text="Lưu">
                <Image ToolTip="Lưu">
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </dx:ASPxButton>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
</dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>


