<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEdit_backOrderProduct.ascx.cs" Inherits="WebModule.ImExporting.UserControl.uEdit_backOrderProduct" %>
<style type="text/css">
    .style25
    {
    }
    .style31
    {
    }
    .style33
    {
        width: 98px;
    }
    .style37
    {
        width: 98px;
        height: 13px;
    }
    .style38
    {
        height: 13px;
    }
    .style39
    {
        height: 17px;
    }
    .style41
    {
        width: 254px;
        height: 13px;
    }
    .style42
    {
        width: 135px;
    }
    .style43
    {
    }
    .style44
    {
        width: 575px;
    }
    .style45
    {
        width: 135px;
        height: 13px;
    }
</style>
<dx:ASPxPageControl ID="ASPxPageControl_detail" runat="server" 
    RenderMode="Lightweight" ActiveTabIndex="0">
    <TabPages>
        <dx:TabPage Text="Thông tin chung">
            <ContentCollection>
                
<dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
        <Items>
            <dx:LayoutGroup ShowCaption="False" ColCount="2">
                <Items>
                    <dx:LayoutItem Caption="Mã trả hàng" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="txt_backid" runat="server" Width="170px">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Mã phiếu mua">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btn_inquiry" runat="server" ToolTip="Tìm phiếu mua" 
                                    Width="30px">
                                    <Image Url="~/images/icon/16x16/inquiry.png" Width="15px">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutGroup Caption="Chứng từ kèm theo" Visible="false">
                        <Items>
                            <dx:LayoutItem Caption="Mã chứng từ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDropDownEdit ID="ASPxFormLayout1_E1" runat="server">
                                        </dx:ASPxDropDownEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="File đính kèm">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxUploadControl ID="ASPxFormLayout1_E3" runat="server" UploadMode="Auto" 
                                            Width="280px">
                                        </dx:ASPxUploadControl>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutItem Caption="Lý do" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxFormLayout1_E4" runat="server" Width="170px">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ngày trả" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxDateEdit ID="ASPxFormLayout1_E5" runat="server">
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
                </dx:ContentControl>
                
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Danh mục hàng trả">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="gridview_backProduct" runat="server" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="STT" FieldName="sequencenno" ShowInCustomizationForm="True" 
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True" 
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True" 
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unitid" ShowInCustomizationForm="True" 
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lượng trả" FieldName="numberofback"
                                ShowInCustomizationForm="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="unitprice" ShowInCustomizationForm="True" 
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="total" ShowInCustomizationForm="True" 
                                VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Lý do" FieldName="reason" ShowInCustomizationForm="True" 
                                VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="note" ShowInCustomizationForm="True" 
                                VisibleIndex="7">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="8" ButtonType="Image">
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
                        </Columns>
                        <Settings ShowFilterRow="True" />
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Chi tiết">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server">
                    </dx:ASPxHtmlEditor>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Chứng từ liên quan">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
                        <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                    </dx:ASPxFileManager>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<table>
    <tr>
        <td align="left" class="style44">
            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                Text="Trợ Giúp" >
                <Image ToolTip="Trợ giúp">
                    <SpriteProperties CssClass="Sprite_Help" />
                </Image>
            </dx:ASPxButton>
        </td>
        <td align="right">
            <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                ClientInstanceName="buttonSave" Text="Lưu Lại" >
                <ClientSideEvents Click="buttonSave_Click" />
                <Image ToolTip="Lưu">
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </dx:ASPxButton>
        </td>
        <td align="right">
            <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                ClientInstanceName="buttonCancel" Text="Bỏ Qua" >
                <ClientSideEvents Click="buttonCancel_Click" />
                <Image ToolTip="Bỏ qua">
                    <SpriteProperties CssClass="Sprite_Cancel" />
                </Image>
            </dx:ASPxButton>
        </td>
    </tr>
</table>