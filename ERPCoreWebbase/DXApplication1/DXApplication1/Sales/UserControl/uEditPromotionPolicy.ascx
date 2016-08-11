<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditPromotionPolicy.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uEditPromotionPolicy" %>
<%@ Register Src="~/Sales/UserControl/uEditPromotionLevel.ascx" TagName="uEditPromotionLevel"
    TagPrefix="uc1" %>
<uc1:uEditPromotionLevel ID="uEditPromotionLevel1" runat="server" />

<dx:ASPxPopupControl ID="popup_wizardCreaterPromotionPolicy" ClientInstanceName="popup_wizardCreaterPromotionPolicy" runat="server" HeaderText="Hướng dẫn tạo khuyến mãi"
    CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" RenderMode="Classic"
    PopupVerticalAlign="WindowCenter" AllowResize="True" Width="900px" Height="500px" ScrollBars="Auto" ShowFooter="true" 
    AllowDragging="True">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxPageControl ID="pc_wizardCreaterPromotionPolicy" ClientInstanceName="pc_wizardCreaterPromotionPolicy"
            runat="server" RenderMode="Classic" ActiveTabIndex="0" Height="100%" Width="100%" >
                <TabPages>
                    <dx:TabPage Text="Thông tin chung">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxFormLayout ID="form_commoninfo" runat="server" Width="100%">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã chương trình">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="form_commoninfo_id" runat="server" 
                                                        Text="CT0001"
                                                        ReadOnly="true" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên chương trình">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="form_commoninfo_name" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày bắt đầu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="form_commoninfo_from" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày kết thúc">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="form_commoninfo_to" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Trạng thái">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cbo_status" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mô tả">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer  runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="form_commoninfo_description" Width="200px" Height="150px" runat="server">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Các mức khuyến mãi">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxLabel ID="lblTittle_grv_PromotionLevels" runat="server" Font-Bold="True" Font-Size="Small"
                                    Text="Các mức khuyến mãi" Width="170px">
                                </dx:ASPxLabel>
                                <dx:ASPxGridView ID="grv_PromotionLevels" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mức Khuyến Mãi" FieldName="ten" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="diengiai" VisibleIndex="1" Width="200px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="2"
                                            Width="100px">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="edit">
                                                    <Image ToolTip="Edit">
                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="new">
                                                    <Image ToolTip="New">
                                                        <SpriteProperties CssClass="Sprite_New" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="delete">
                                                    <Image ToolTip="Delete">
                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>                                    
                                    </Columns>
                                    <ClientSideEvents CustomButtonClick="show_uEditPromotionLevel" />
                                    <Settings HorizontalScrollBarMode="Hidden" />
                                    <SettingsPager PageSize="10" ShowEmptyDataRows="true"></SettingsPager>
                                    <SettingsBehavior ColumnResizeMode="NextColumn" />
                                    <Styles>
                                        <Header HorizontalAlign="Center" Font-Bold="true">
                                        </Header>
                                        <CommandColumn Spacing="10px">
                                        </CommandColumn>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="btnCancel" clientinstancename="btnCancel" runat="server" Text="Thoát" AutoPostBack="false">
                                <ClientSideEvents Click="OnNextClick" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                         <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="btnApply" clientinstancename="btnApply" runat="server" Text="Lưu chính sách giá">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:ASPxPopupControl>