<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SelectSuppliers.aspx.cs" Inherits="WebModule.ImExporting.SelectSuppliers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxHiddenField ClientInstanceName="hfDic" ID="hfDic" runat="server">
    </dx:ASPxHiddenField>
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Chọn nhà cung cấp" 
            Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxCallbackPanel ID="cpeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
        HideContentOnCallback="True" >
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                    OnInitNewRow="grdData_InitNewRow" OnRowDeleting="grdData_RowDeleting" 
                    OnStartRowEditing="grdData_StartRowEditing"  Width="100%">                    
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="key" ShowInCustomizationForm="True" 
                            Visible="False" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="1">
                            <EditButton Visible="True">
                                <Image ToolTip="Sửa">
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </EditButton>
                            <NewButton Visible="True">
                                <Image ToolTip="Thêm">
                                    <SpriteProperties CssClass="Sprite_New" />
                                </Image>
                            </NewButton>
                            <DeleteButton Visible="True">
                                <Image ToolTip="Xóa">
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </DeleteButton>
                            <ClearFilterButton Visible="True">
                                <Image ToolTip="Hủy">
                                    <SpriteProperties CssClass="Sprite_Clear" />
                                </Image>
                            </ClearFilterButton>
                            <UpdateButton>
                                <Image ToolTip="Cập nhật">
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </UpdateButton>
                            <CancelButton>
                                <Image ToolTip="Bỏ qua">
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </CancelButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="Tổng Số lượng yêu cầu" FieldName="amount" 
                            ShowInCustomizationForm="True" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="productunit" 
                            ShowInCustomizationForm="True" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="name" 
                            ShowInCustomizationForm="True" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="code" 
                            ShowInCustomizationForm="True" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                        AllowSelectSingleRowOnly="True" ConfirmDelete="True" />
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                    <SettingsEditing Mode="Inline" />
                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                    <SettingsDetail ShowDetailRow="True" />
                    <Styles>
                        <CommandColumn HorizontalAlign="Center" Spacing="10px">
                        </CommandColumn>
                        <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                        </Header>
                        <HeaderPanel HorizontalAlign="Center">
                        </HeaderPanel>
                    </Styles>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="grdDataDetail" runat="server" AutoGenerateColumns="False" 
                                EnableTheming="True" OnInitNewRow="grdDataDetail_InitNewRow" 
                                OnRowDeleting="grdDataDetail_RowDeleting" OnStartRowEditing="grdDataDetail_StartRowEditing" 
                                Theme="Default" Width="100%" 
                                onbeforeperformdataselect="grdDataDetail_BeforePerformDataSelect" 
                                ondatabinding="grdDataDetail_DataBinding">                              
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Số lượng" VisibleIndex="12">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn Caption="Chọn" ShowSelectCheckbox="True" 
                                        VisibleIndex="11">
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                        VisibleIndex="13">
                                        <EditButton Visible="True">
                                            <Image ToolTip="Sửa">
                                                <SpriteProperties CssClass="Sprite_Edit" />
                                            </Image>
                                        </EditButton>
                                        <NewButton Visible="True">
                                            <Image ToolTip="Thêm">
                                                <SpriteProperties CssClass="Sprite_New" />
                                            </Image>
                                        </NewButton>
                                        <DeleteButton Visible="True">
                                            <Image ToolTip="Xóa">
                                                <SpriteProperties CssClass="Sprite_Delete" />
                                            </Image>
                                        </DeleteButton>
                                        <ClearFilterButton Visible="True">
                                            <Image ToolTip="Hủy">
                                                <SpriteProperties CssClass="Sprite_Clear" />
                                            </Image>
                                        </ClearFilterButton>
                                        <UpdateButton>
                                            <Image ToolTip="Cập nhật">
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </UpdateButton>
                                        <CancelButton>
                                            <Image ToolTip="Bỏ qua">
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </CancelButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="Hiệu Lực Tới" FieldName="timeeffect" 
                                        VisibleIndex="10">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thời Gian Thanh Toán DK" FieldName="dpayment" 
                                        VisibleIndex="9">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tỉ Giá" FieldName="Exchange" 
                                        VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Loại Tiền Tệ" FieldName="Currency" 
                                        VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành Tiền Quy Đổi" FieldName="AmountA" 
                                        VisibleIndex="7">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thời Gian Giao Hàng DK" FieldName="dday" 
                                        VisibleIndex="8">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="sum" 
                                        VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="costs" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Chiết khấu" FieldName="discount" 
                                        VisibleIndex="2">
                                        <CellStyle Wrap="True">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="price" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="name" 
                                        VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                    ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                                <SettingsPager PageSize="50">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline" />
                                <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                <Styles>
                                    <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                    </CommandColumn>
                                    <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                        Wrap="True">
                                    </Header>
                                    <HeaderPanel HorizontalAlign="Center">
                                    </HeaderPanel>
                                </Styles>
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <div id="clientContainer">
    </div>
</asp:Content>
