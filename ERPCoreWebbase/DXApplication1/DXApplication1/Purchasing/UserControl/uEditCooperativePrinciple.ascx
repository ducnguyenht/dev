<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uEditCooperativePrinciple.ascx.cs"
    Inherits="WebModule.Purchasing.UserControl.uEditCooperativePrinciple" %>
<dx:ASPxPageControl ID="pc_configPrinciples" runat="server" ActiveTabIndex="2" Width="100%"
    Height="100%" onactivetabchanged="pc_configPrinciples_ActiveTabChanged">
    <TabPages>
        <dx:TabPage Text="Thông tin chung">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="form_commoninfo" runat="server">
                        <Items>
                            <dx:LayoutItem Caption="Mã nguyên tắc" RequiredMarkDisplayMode="Required">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxTextBox ID="form_commoninfo_E1" runat="server" Width="170px">
                                        </dx:ASPxTextBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Nhà cung cấp" RequiredMarkDisplayMode="Required">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="form_commoninfo_E2" runat="server">
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Thời gian áp dụng" RequiredMarkDisplayMode="Required">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="form_commoninfo_E3" runat="server">
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Thời gian kết thúc">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="form_commoninfo_E4" runat="server">
                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ghi chú">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxMemo ID="form_commoninfo_E5" runat="server" Height="71px" Width="170px">
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                    <div class="quickHelp">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Italic="False" ForeColor="Gray"
                            Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                            Font-Bold="False" Font-Size="XX-Small">
                        </dx:ASPxLabel>
                    </div>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nguyên tắc công nợ">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="form_debt" runat="server">
                        <Items>
                            <dx:LayoutItem Caption="Thời hạn nợ (ngày)">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E1" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Lãi suất phạt">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E2" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Hạn ngạch nợ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E3" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Ký quĩ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxSpinEdit ID="form_debt_E4" runat="server" Height="21px" Number="0">
                                        </dx:ASPxSpinEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                    <div class="quickHelp">
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="False" ForeColor="Gray"
                            Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                            Font-Bold="False" Font-Size="XX-Small">
                        </dx:ASPxLabel>
                    </div>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nguyên tắc doanh số">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grv_SalePrinciple" runat="server" AutoGenerateColumns="False"
                        Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Chỉ tiêu doanh số (VNĐ)" FieldName="objectiveAmount"
                                ShowInCustomizationForm="True" VisibleIndex="0">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txt_objectiveAmount" runat="server" Height="21px" Text='<%# Bind("objectiveAmount") %>'>
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewBandColumn Caption="Thời hạn tính doanh số" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="fromDate" ShowInCustomizationForm="True"
                                        VisibleIndex="0">
                                        <DataItemTemplate>
                                            <dx:ASPxDateEdit ID="txt_fromdate" runat="server">
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
                            <dx:GridViewDataTextColumn Caption="Tỉ lệ phạt trên DS (%)" FieldName="rateCharge"
                                ShowInCustomizationForm="True" VisibleIndex="2" Width="50px">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txt_ratecharge" runat="server" Height="21px" Width="45px" Text='<%# Bind("rateCharge") %>'>
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
                    <div class="quickHelp">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Italic="False" ForeColor="Gray"
                            Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                            Font-Bold="False" Font-Size="XX-Small">
                        </dx:ASPxLabel>
                    </div>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Thông Tin Chi Tiết">
            <ContentCollection>
                <dx:ContentControl>
                    <dx:ASPxNavBar ID="anchdsdsdsdgdhd" runat="server" AutoCollapse="True" Height="100%"
                        Width="100%">
                        <Groups>
                            <dx:NavBarGroup Text="Mô Tả">
                                <Items>
                                    <dx:NavBarItem>
                                        <Template>
                                            <dx:ASPxHtmlEditor ID="ASPxHtmlEditor43dsds33" runat="server" Height="350px" Width="100%">
                                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                                <Settings AllowHtmlView="False" AllowPreview="False" />
                                            </dx:ASPxHtmlEditor>
                                        </Template>
                                    </dx:NavBarItem>
                                </Items>
                            </dx:NavBarGroup>
                            <dx:NavBarGroup Expanded="False" Text="Tài Liệu">
                                <Items>
                                    <dx:NavBarItem>
                                        <Template>
                                            <dx:ASPxFileManager ID="ASPxFileManager4341" runat="server" Height="350px">
                                                <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                            </dx:ASPxFileManager>
                                        </Template>
                                    </dx:NavBarItem>
                                </Items>
                            </dx:NavBarGroup>
                        </Groups>
                    </dx:ASPxNavBar>
                    <div class="quickHelp">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"
                            Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                            Font-Bold="False" Font-Size="XX-Small">
                        </dx:ASPxLabel>
                    </div>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
