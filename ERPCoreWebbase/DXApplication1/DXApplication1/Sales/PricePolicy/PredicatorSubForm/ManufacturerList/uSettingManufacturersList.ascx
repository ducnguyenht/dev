<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uSettingManufacturersList.ascx.cs" Inherits="WebModule.Sales.PricePolicy.PredicatorSubForm.ManufacturerList.uSettingManufacturersList" %>
<dx:ASPxCallbackPanel ID="cpSettingManufacturerList" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    OnCallback="cpSettingManufacturerList_Callback" Width="100%">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
<dx:aspxpopupcontrol id="popup_settingManufacturersList"
    runat="server" rendermode="Classic" ShowFooter="true"
    closeaction="CloseButton" headertext="Hàng hóa thuộc nhà sản xuất" 
    modal="True" AllowDragging="true" AllowResize="true" 
    popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
    width="800px" height="515px" ScrollBars="Auto" ShowSizeGrip="False">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pcSettingManufacturerList" 
                runat="server" RenderMode="Classic"
                ActiveTabIndex="0" Height="100%" Width="100%">
                <TabPages>
                    <dx:TabPage Text="Thao tác chọn">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxGridView ID="grdManufacturerList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" DataSourceID="ManufacturerListXDS" 
                                    KeyFieldName="OrganizationId">
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Chọn" ShowInCustomizationForm="True" 
                                            ShowSelectCheckbox="True" VisibleIndex="0">
                                            <FilterTemplate>
                                                <center>
                                                    <dx:ASPxCheckBox ID="ChkSelectedManufacturerAll" runat="server" 
                                                        AutoPostBack="false" oninit="ChkSelectedManufacturerAll_Init">
                                                    </dx:ASPxCheckBox>
                                                </center>
                                                </FilterTemplate>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã" FieldName="Code" ShowInCustomizationForm="True" 
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên nhà sản xuất" FieldName="Name" ShowInCustomizationForm="True" 
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <SettingsPager PageSize="10">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                        ShowFilterRowMenuLikeItem="True" VerticalScrollableHeight="300" />
                                </dx:ASPxGridView>
                                <dx:XpoDataSource ID="ManufacturerListXDS" runat="server" 
                                    TypeName="NAS.DAL.Nomenclature.Organization.ManufacturerOrg" 
                                    Criteria="[RowStatus] &gt; 0">
                                </dx:XpoDataSource>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Thông tin đã chọn">
                        <ContentCollection>
                            <dx:ContentControl>
                                Số lượng nhà sản xuất được chọn: <%= this.grdManufacturerList.Selection.Count %>
                                <dx:ASPxGridView ID="grdManufacturerListPreview" runat="server" AutoGenerateColumns="False"
                                    Width="100%"
                                    KeyFieldName="OrganizationId">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã" FieldName="Code" ShowInCustomizationForm="True" 
                                            VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên nhà sản xuất" FieldName="Name" ShowInCustomizationForm="True" 
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
                <dx:ASPxButton ID="buttonFinishManufacturerSetting" runat="server" Text="Chọn" AutoPostBack="false">
                <Image>
                    <SpriteProperties CssClass="Sprite_Forward" />
                </Image>
            </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonNextManufacturerSetting" runat="server" Text="Tiếp" AutoPostBack="false">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Forward" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonBackManufacturerSetting" runat="server" Text="Chọn lại" AutoPostBack="False">
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
