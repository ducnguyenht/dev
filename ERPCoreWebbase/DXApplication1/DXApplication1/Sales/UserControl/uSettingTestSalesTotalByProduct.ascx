<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSettingTestSalesTotalByProduct.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uSettingTestSalesTotalByProduct" %>
<script type="text/javascript">
    function show_uSettingTestSalesTotalByProduct(s, e) {
        popup_settingTestSalesTotalByProduct.Show();
    }
</script>
<dx:ASPxPopupControl ID="popup_settingTestSalesTotalByProduct" runat="server" CloseAction="CloseButton" Modal="true"
    ClientInstanceName="popup_settingTestSalesTotalByProduct" AllowDragging="True" AllowResize="True" 
    PopupAnimationType="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    EnableViewState="False" HeaderText="Giả lập doanh số theo hàng hóa" Width="850px" Height="400px" ScrollBars="Auto" ShowFooter="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grv_settingSalesTotalByProduct" ClientInstanceName="grv_settingSalesTotalByProduct" 
                runat="server" AutoGenerateColumns="False" KeyFieldName="SupplierId" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="Code" 
                        Name="Code" Width="90px" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="Name" 
                        Name="Name" Width="150px" VisibleIndex="1">                                       
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" 
                        Name="Description" Width="150px" VisibleIndex="2">                                             
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="Sales" Width="100px"
                        Name="Name" VisibleIndex="3">  
                        <DataItemTemplate>
                            <dx:ASPxDateEdit ID="txtFrom" runat="server" Width="90px"></dx:ASPxDateEdit>
                        </DataItemTemplate>                                     
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đến ngày" FieldName="Sales" Width="100px"
                        Name="Name" VisibleIndex="4">  
                        <DataItemTemplate>
                            <dx:ASPxDateEdit ID="txtTo" runat="server" Width="90px"></dx:ASPxDateEdit>
                        </DataItemTemplate>                                     
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Doanh số đã đạt" FieldName="Sales" 
                        Name="Name" VisibleIndex="5">  
                        <DataItemTemplate>
                            <dx:ASPxTextBox ID="txtSales" runat="server"></dx:ASPxTextBox>
                        </DataItemTemplate>                                     
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior AllowSelectByRowClick="True" ConfirmDelete="True" ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsPager PageSize="10" ShowEmptyDataRows="True">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>
                <Styles>
                    <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                    </Header>
                    <HeaderPanel HorizontalAlign="Center">
                    </HeaderPanel>
                    <CommandColumn HorizontalAlign="Center" Spacing="10px">
                    </CommandColumn>
                </Styles>
            </dx:ASPxGridView>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" ClientInstanceName="buttonCancel"
                                Text="Thoát">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right">
                            <dx:ASPxButton ID="buttonAccept" ClientInstanceName="buttonSave" runat="server" Text="Lưu lại"
                                clientvisible="true">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:ASPxPopupControl>
