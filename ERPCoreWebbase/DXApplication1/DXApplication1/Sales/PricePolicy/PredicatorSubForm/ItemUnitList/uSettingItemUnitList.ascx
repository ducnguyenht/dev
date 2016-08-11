<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSettingItemUnitList.ascx.cs" Inherits="WebModule.Sales.PricePolicy.PredicatorSubForm.ItemUnitList.uSettingItemUnitList" %>
<dx:ASPxCallbackPanel ID="cpSettingItemUnitList" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    OnCallback="cpSettingItemUnitList_Callback" Width="100%">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
<dx:aspxpopupcontrol id="popup_settingItemUnitList" 
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Danh Mục Hàng Hóa Áp Dụng" 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="800px" height="515px" ScrollBars="Auto" ShowSizeGrip="False">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pcSettingItemUnitList" 
                runat="server" RenderMode="Classic"
                ActiveTabIndex="0" Height="100%" Width="100%">
                <TabPages>
                    <dx:TabPage Text="Thao tác chọn">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxGridView ID="grdItemUnitList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" DataSourceID="ItemUnitListXDS" 
                                    KeyFieldName="ItemUnitId">
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Chọn" ShowInCustomizationForm="True" 
                                            ShowSelectCheckbox="True" VisibleIndex="0">
                                            <FilterTemplate>
                                                <center>
                                                    <dx:ASPxCheckBox ID="chkItemUnitAll" runat="server" AutoPostBack="false" 
                                                        oninit="chkItemUnitAll_Init">
                                                    </dx:ASPxCheckBox>
                                                </center>
                                            </FilterTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã" FieldName="ItemId.Code" ShowInCustomizationForm="True" 
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemId.Name" ShowInCustomizationForm="True" 
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="UnitId.Name" ShowInCustomizationForm="True" 
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsPager PageSize="10">
                                                </SettingsPager>
                                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                    ShowFilterRowMenuLikeItem="True" VerticalScrollableHeight="300" />
                                </dx:ASPxGridView>
                                <dx:XpoDataSource ID="ItemUnitListXDS" runat="server" 
                                    TypeName="NAS.DAL.Nomenclature.Item.ItemUnit" 
                                    
                                    Criteria="[RowStatus] &gt; 0 And [ItemId] Is Not Null And [UnitId] Is Not Null">
                                </dx:XpoDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Thông tin đã chọn">
                        <ContentCollection>
                            <dx:ContentControl>
                                Số lượng hàng hóa được chọn: <%= this.grdItemUnitList.Selection.Count %>
                                <dx:ASPxGridView ID="grdItemUnitListPreview" runat="server" AutoGenerateColumns="False"
                                    Width="100%"
                                    KeyFieldName="ItemUnitId">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã" FieldName="ItemCode" ShowInCustomizationForm="True" 
                                            VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemName" ShowInCustomizationForm="True" 
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="UnitName" ShowInCustomizationForm="True" 
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsPager PageSize="10">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                        ShowFilterRowMenuLikeItem="True" VerticalScrollableHeight="300" />
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <div id="Footer" style="display: inline; width: 100%;">
            <div style="display: inline; float: left">
                <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Help" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonFinishItemUnitSetting" runat="server" Text="Chọn" AutoPostBack="false">
                <Image>
                    <SpriteProperties CssClass="Sprite_Forward" />
                </Image>
            </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonNextItemUnitSetting" runat="server" Text="Tiếp" AutoPostBack="false">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Forward" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonBackItemUnitSetting" runat="server" Text="Chọn lại" AutoPostBack="False">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Backward" />
                    </Image>
                </dx:ASPxButton>
            </div>
        </div>
    </FooterContentTemplate>
    <ModalBackgroundStyle BackColor="White" Opacity="0"></ModalBackgroundStyle>
</dx:aspxpopupcontrol>
</dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
