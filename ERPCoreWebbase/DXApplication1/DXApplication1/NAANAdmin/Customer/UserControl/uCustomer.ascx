<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uCustomer.ascx.cs" Inherits="WebModule.NAANAdmin.Customer.Usercontrol.uCustomer" %>
<style type="text/css">
    .style25
    {
        width: 609px;
    }
    </style>
<dx:ASPxPageControl ID="ASPxPageControl1" runat="server"  
        RenderMode="Lightweight" ActiveTabIndex="0" Width="100%" Height="500px" 
        EnableTabScrolling="True">  
        <TabPages>
            <dx:TabPage Text="Thông tin chung">
                <ContentCollection>
                    <dx:ContentControl>
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                            <Items>
                                <dx:LayoutItem Caption="Mã khách hàng">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Tên khách hàng">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Mã số thuế">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Thể loại">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Người quản trị">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            
            <dx:TabPage Text="Thông tin chi tiết">
                <ContentCollection>
                    <dx:ContentControl>
                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server">
                        </dx:ASPxHtmlEditor>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            </TabPages>
</dx:ASPxPageControl>
<table style="width:100%; margin-top:10px">
                        <tr>
                            <td>
                                <table align="right" style="width:100%;">
                                <tr>
                                    <td align="left" class="style25">
                                        <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                                            Text="Trợ Giúp" >
                                            <Image ToolTip="Trợ giúp">
                                                <SpriteProperties CssClass="Sprite_Help" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonSave" Text="Lưu Lại" 
                                            >
                                            <ClientSideEvents Click="buttonSave_Click" />
                                            <Image ToolTip="Lưu">
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonCancel" Text="Bỏ Qua" 
                                            >
                                            <ClientSideEvents Click="buttonCancel_Click" />
                                            <Image ToolTip="Bỏ qua">
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                    </table>