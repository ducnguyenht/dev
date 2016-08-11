<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="PayType.aspx.cs" Inherits="WebModule.ImExporting.Paytype" %>
<%@ Register src="~/ImExporting/UserControl/uEditPayType.ascx" tagname="uEditPayType" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">

.dxpControl_DevEx
{
	font: 11px Verdana, Geneva, sans-serif;
	color: #201f35;
}
.dxpSummary_DevEx
{
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
	padding: 1px 4px;
}
.dxpButton_DevEx
{
	text-decoration: none;
	white-space: nowrap;
	text-align: center;
	vertical-align: middle;
}
.dxpDisabledButton_DevEx
{
	color: #b1b1b8;
	text-decoration: none;
}
.dxpDisabled_DevEx
{
	color: #b1b1b8;
	border-color: #f2f2f4;
	cursor: default;
}
.dxpPageNumber_DevEx
{
	text-decoration: none;
	text-align: center;
	vertical-align: middle;
	padding: 1px 6px 2px;
}
.dxpCurrentPageNumber_DevEx
{
	background-color: #e2eafd;
	text-decoration: none;
	padding: 1px 4px 2px;
	border: 1px solid #c9d7fb;
	white-space: nowrap;
}
    </style>
    <script type="text/javascript">
        function grdData_customBtn_click(s, e) {
            pc_editPayment.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pc_Payment" runat="server" RenderMode="Lightweight" Width="100%">
        <TabPages>
            <dx:TabPage Text="Loại hình thanh toán">
                <ContentCollection>
                    <dx:ContentControl>
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderSupplier" runat="server" Text="Loại Hình Thanh Toán"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>

                        <dx:ASPxGridView runat="server" KeyFieldName="Name" AutoGenerateColumns="False" 
                             Width="100%" ID="grdData"  >
                            <ClientSideEvents CustomButtonClick="grdData_customBtn_click">
                            </ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Name" Name="Name" Width="300px" 
                                    Caption="Tên Loại Hình Thanh Toán" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>            
                                <dx:GridViewDataTextColumn FieldName="Description" Name="Description" 
                                    Caption="Mô tả" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn ButtonType="Image" Width="50px" 
                                    Caption="Thao Tác" VisibleIndex="5">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="edit_payment">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Edit" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="add_payment">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_New" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="delete_payment">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Delete" />
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="RowStatus" Width="50px" 
                                    Caption="Trạng thái" VisibleIndex="4">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Sử dụng" Value="A">
                                            </dx:ListEditItem>
                                            <dx:ListEditItem Text="Tạm ngưng" Value="I">
                                            </dx:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                    <EditCellStyle HorizontalAlign="Center">
                                    </EditCellStyle>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataComboBoxColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                AllowSelectSingleRowOnly="True" ConfirmDelete="True">
                            </SettingsBehavior>
                            <SettingsPager PageSize="50">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline">
                            </SettingsEditing>
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True">
                            </Settings>
                            <Styles>
                                <Header HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True">
                                </Header>
                                <HeaderPanel HorizontalAlign="Center">
                                </HeaderPanel>
                                <CommandColumn Spacing="10px" HorizontalAlign="Center">
                                </CommandColumn>
                            </Styles>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Công đoạn xử lý">
                <ContentCollection>
                    <dx:ContentControl>
                        <dx:ASPxGridView ID="grv_configProcess" runat="server" KeyFieldName="name" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Tên xử lý" FieldName="name" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" Width="300px" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn Caption="Thao tác" ButtonType="Image" VisibleIndex="2">
                                <EditButton Visible="true">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Edit" />
                                    </Image>
                                </EditButton>
                                <NewButton Visible="true">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_New" />
                                    </Image>
                                </NewButton>
                                <DeleteButton Visible="true">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Delete" />
                                    </Image>
                                </DeleteButton>
                                <UpdateButton Visible="true">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </UpdateButton>
                                <CancelButton Visible="true">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </CancelButton>
                            </dx:GridViewCommandColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <dx:ASPxPopupControl ID="pc_editPayment" ClientInstanceName="pc_editPayment" runat="server" RenderMode="Classic" 
        AllowDragging="true" AllowResize="true" Width="800px" Height="600px" ScrollBars="Auto"
        HeaderText="Thông tin loại hình thức thanh toán" ShowFooter="true" Modal="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <uc1:uEditPayType ID="uEditPayTyp1" runat="server" />
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
                                Text="Bỏ Qua">
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

</asp:Content>
