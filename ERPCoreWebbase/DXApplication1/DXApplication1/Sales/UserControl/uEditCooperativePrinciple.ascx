<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEditCooperativePrinciple.ascx.cs" Inherits="WebModule.GUI.usercontrol.uEditCooperativePrinciple" %>
<dx:ASPxPageControl ID="pc_configPrinciples" runat="server" ActiveTabIndex="0" 
    RenderMode="Classic" Width="100%" Height="100%">
    <TabPages>
        <dx:TabPage Text="Thông tin chung">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="form_commoninfo" runat="server">
                        <Items>
                            <dx:LayoutItem Caption="Mã nguyên tắc">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="form_commoninfo_E1" runat="server" Width="170px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Khách hàng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="form_commoninfo_E2" runat="server">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Thời gian áp dụng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="form_commoninfo_E3" runat="server">
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Thời gian kết thúc">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="form_commoninfo_E4" runat="server">
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Tình trạng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="form_commoninfo_E5" runat="server">
                                            <Items>
                                                <dx:ListEditItem Value="0" Text="Đang hoạt động" />
                                                <dx:ListEditItem Value="1" Text="Tạm ngưng" />
                                            </Items>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ghi chú">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxMemo ID="form_commoninfo_E6" runat="server" Height="71px" Width="170px">
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nguyên tắc công nợ">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="form_debt" runat="server">
                        <Items>
                            <dx:LayoutItem Caption="Thời hạn nợ (ngày)">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E1" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Lãi suất phạt">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E2" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Hạn ngạch nợ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E3" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ký quĩ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E4" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nguyên tắc doanh số">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grv_SalePrinciple" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Chỉ tiêu doanh số (VNĐ)" FieldName="objectiveAmount"
                                ShowInCustomizationForm="True" VisibleIndex="0">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txt_objectiveAmount" runat="server" Height="21px" Text='<%# Bind("objectiveAmount") %>' >
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewBandColumn Caption="Thời hạn tính doanh số" 
                                ShowInCustomizationForm="True" VisibleIndex="1">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="fromDate" ShowInCustomizationForm="True" 
                                        VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxDateEdit ID="txt_fromdate" runat="server" >
                                            </dx:ASPxDateEdit>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đến ngày" FieldName="toDate" ShowInCustomizationForm="True" 
                                        VisibleIndex="1">
                                        <DataItemTemplate>
                                            <dx:ASPxDateEdit ID="txt_todate" runat="server">
                                            </dx:ASPxDateEdit>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataTextColumn Caption="Tỉ lệ phạt DS chưa đạt (%)" FieldName="rateCharge"
                                ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txt_ratecharge" runat="server" Height="21px" Width="45px" Text='<%# Bind("rateCharge") %>' >
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="3">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton Text="Sửa" ID="edit_totalsale">
                                        <Image ToolTip="Sửa">
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton Text="Thêm" ID="add_totalsale">
                                        <Image ToolTip="Thêm">
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                    <dx:GridViewCommandColumnCustomButton Text="Xóa" ID="delete_totalsale">
                                        <Image ToolTip="Xóa">
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Chi tiết">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Width="100%">
                    </dx:ASPxHtmlEditor>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Thư viện tài liệu">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server"  Width="100%">
                        <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                    </dx:ASPxFileManager>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>

