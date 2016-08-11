<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Business.aspx.cs" Inherits="WebModule.Member.Business" %>
<%@ Register Src="UserControl/uBusiness.ascx" TagName="uBusiness" TagPrefix="uc1" %>

<%@ Register Src="UserControl/uBusinessGroup.ascx" TagName="uBusinessGroup" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function customCLick(s, e) {

        if (e.buttonID == 'btNewDN' || e.buttonID == 'btEditDN') {
            formEditDN.Show();
        }
    }

    function customCLick1(s, e) {
        if (e.buttonID == 'btNewNhomDN' || e.buttonID == 'btEditNhomDN') {
            formEditNhomDN.Show();
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
    Width="100%">
    <TabPages>
        <dx:TabPage Text="Doanh nghiệp">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdDoanhNghiep" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                VisibleIndex="6" Width="100px">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btNewDN">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="btEditDN">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="mota" ShowInCustomizationForm="True"
                                VisibleIndex="4" Width="150px" Name="moTa">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="trangthai" ShowInCustomizationForm="True"
                                VisibleIndex="3" Width="100px" Name="trangThai">
                                <EditCellStyle HorizontalAlign="Center" />
                                <EditCellStyle HorizontalAlign="Center">
                                </EditCellStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên doanh nghiệp" FieldName="tendn" ShowInCustomizationForm="True"
                                VisibleIndex="1" Name="tenDN">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã doanh nghiệp" FieldName="madn" ShowInCustomizationForm="True"
                                VisibleIndex="0" Width="150px" Name="maDN">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            ColumnResizeMode="Control" ConfirmDelete="True" />
                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            ColumnResizeMode="Control" ConfirmDelete="True" />
                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                        </SettingsPager>
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                        <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                            </Header>
                            <HeaderPanel HorizontalAlign="Center">
                            </HeaderPanel>
                            <CommandColumn HorizontalAlign="Center" Spacing="10px">
                            </CommandColumn>
                        </Styles>
                        <ClientSideEvents CustomButtonClick="customCLick" />
                    </dx:ASPxGridView>
                    <dx:ASPxPopupControl ID="formEditDN" runat="server" HeaderText="Thông tin doanh nghiệp"
                                        Height="600px" Modal="True" Width="900px" ClientInstanceName="formEditDN" AllowDragging="True"
                                        RenderMode="Classic" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        ShowFooter="true" ShowSizeGrip="False" AllowResize="true" ScrollBars="Auto" ShowMaximizeButton="True">
                        <FooterContentTemplate>
                            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                                Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float-right button-rigth-margin"
                                ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                <ClientSideEvents Click="buttonCancelDevice_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float-right button-rigth-margin"
                                ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                <ClientSideEvents Click="buttonSaveDevice_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </FooterContentTemplate>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <uc1:uBusiness ID="uBusiness" runat="server" />
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nhóm doanh nghiệp">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdNhomDoanhNghiep" runat="server" AutoGenerateColumns="False"
                        KeyFieldName="SupplierId" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Tên Nhóm doanh nghiệp" FieldName="tennhomdn"
                                Name="tenNhomDN" ShowInCustomizationForm="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                VisibleIndex="5" Width="100px">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btNewNhomDN">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton ID="btEditNhomDN">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton>
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Mã Nhóm doanh nghiệp" FieldName="manhomdn" Name="maNhomDN"
                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="mota" Name="moTa" ShowInCustomizationForm="True"
                                VisibleIndex="3" Width="200px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="trangthai" ShowInCustomizationForm="True"
                                VisibleIndex="2" Width="100px" Name="trangThai">
                                <EditCellStyle HorizontalAlign="Center">
                                </EditCellStyle>
                                <CellStyle HorizontalAlign="Center">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            ColumnResizeMode="Control" ConfirmDelete="True" />
                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                            ColumnResizeMode="Control" ConfirmDelete="True" />
                        <SettingsPager PageSize="22" ShowEmptyDataRows="True">
                        </SettingsPager>
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                        <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                            </Header>
                            <HeaderPanel HorizontalAlign="Center">
                            </HeaderPanel>
                            <CommandColumn HorizontalAlign="Center" Spacing="10px">
                            </CommandColumn>
                        </Styles>
                        <ClientSideEvents CustomButtonClick="customCLick1" />
                    </dx:ASPxGridView>
                    <dx:ASPxPopupControl ID="formEditNhomDN" runat="server" HeaderText="Thông tin nhóm doanh nghiệp"
                        Height="600px" Modal="True" Width="900px" ClientInstanceName="formEditNhomDN"
                        AllowDragging="True" RenderMode="Classic" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                        ScrollBars="Auto" ShowMaximizeButton="True">
                        <FooterContentTemplate>
                            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                                Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float-right button-rigth-margin"
                                ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                                <ClientSideEvents Click="buttonCancelDevice_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float-right button-rigth-margin"
                                ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False" ToolTip="Lưu và Đóng - Ctr + S">
                                <ClientSideEvents Click="buttonSaveDevice_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </FooterContentTemplate>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <uc2:uBusinessGroup ID="uBusinessGroup" runat="server" />
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    
                    
                    
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
</asp:Content>
