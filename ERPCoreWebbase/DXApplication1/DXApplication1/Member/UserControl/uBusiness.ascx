<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uBusiness.ascx.cs" Inherits="WebModule.Member.UserControl.uBusiness" %>
<dx:ASPxCallbackPanel ID="cpLineMaterial" runat="server" Width="100%" Height="100%" ClientInstanceName="cpLineMaterial">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" Height="100%"
                    Width="100%" EnableTabScrolling="True">
                    <TabPages>
                        <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" AlignItemCaptionsInAllGroups="True"
                                        EnableTheming="True" Width="100%" Height="100%">
                                        <Items>
                                            <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                <Items>
                                                    <dx:LayoutItem Caption="Mã doanh nghiệp" HelpText="Tối đa 128 ký tự">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtMaDoanhNghiep" runat="server" ClientInstanceName="txtMaDoanhNghiep"
                                                                    Width="200px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập mã doanh nghiệp" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                        </CaptionCellStyle>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tên doanh nghiệp" HelpText="255 ký tự, không cho phép trùng lắp">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtTenDoanhNghiep" runat="server" ClientInstanceName="txtTenDoanhNghiep"
                                                                    MaxLength="255" NullText="255 ký tự, không cho phép trùng lắp" Width="400px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập tên doanh nghiệp" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Mô tả">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxMemo ID="txtMota" runat="server" ClientInstanceName="txtMota" Height="71px"
                                                                    Width="440px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập mô tả" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxMemo>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                        </CaptionCellStyle>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Danh sách đại diện">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFsd22t2" runat="server" Height="100%" Width="100%">
                                        <Items>
                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxGridView ID="grdDanhSachDaiDien" runat="server" AutoGenerateColumns="False"
                                                            Width="100%" ClientInstanceName="grdDanhSachDaiDien">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Chức vụ" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                    Width="250px" FieldName="chucvu" Name="chucVu">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số điện thoại" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3" FieldName="sdt" Name="soDT">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="7" Width="100px">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btNewDSDD">
                                                                            <Image>
                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                            </Image>
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btEditDSDD">
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
                                                                <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="0" FieldName="stt" Name="STT">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Email" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="5" FieldName="email" Name="eMail">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Họ và tên" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="1" FieldName="hoten" Name="hoTen">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="True">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Inline" />
                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                                VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                            <Styles>
                                                                <Header HorizontalAlign="Center" Font-Bold="true">
                                                                </Header>
                                                                <CommandColumn Spacing="10px">
                                                                </CommandColumn>
                                                            </Styles>
                                                        </dx:ASPxGridView>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False" Width="110px">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                                <CaptionSettings VerticalAlign="Middle" />
                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                            </dx:LayoutItem>
                                        </Items>
                                        <SettingsItems VerticalAlign="Middle" />
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Lĩnh vực hoạt động">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl9" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPdsdssd22t2" runat="server" Height="100%" Width="100%">
                                        <Items>
                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxGridView ID="grdLinhVucHoatDong" runat="server" AutoGenerateColumns="False"
                                                            ClientInstanceName="grdLinhVucHoatDong" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="mota" Name="moTa"
                                                                    ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5" 
                                                                    Width="250px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="7" Width="100px">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btNewLVHD">
                                                                            <Image>
                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                            </Image>
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btEditLVHD">
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
                                                                <dx:GridViewDataTextColumn Caption="Mã nhóm ngành nghề" FieldName="mannn"
                                                                    Name="maNNN" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                    Width="5%" FieldName="stt" Name="STT">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên Nhóm ngành nghề" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" FieldName="tennnn" Name="tenNNN">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                                VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                            <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Inline" />
                                                            <Styles>
                                                                <Header HorizontalAlign="Center" Font-Bold="true">
                                                                </Header>
                                                                <CommandColumn Spacing="10px">
                                                                </CommandColumn>
                                                            </Styles>
                                                        </dx:ASPxGridView>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                        <SettingsItems VerticalAlign="Middle" />
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="100%" Width="100%">
                                        <Items>
                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" AutoCollapse="True" Height="100%"
                                                            Width="100%">
                                                            <Groups>
                                                                <dx:NavBarGroup Text="Mô Tả">
                                                                    <Items>
                                                                        <dx:NavBarItem>
                                                                            <Template>
                                                                                <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="350px" Width="100%">
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
                                                                                <dx:ASPxFileManager ID="ASPxFileManager1" runat="server" Height="350px">
                                                                                    <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                                                </dx:ASPxFileManager>
                                                                            </Template>
                                                                        </dx:NavBarItem>
                                                                    </Items>
                                                                </dx:NavBarGroup>
                                                            </Groups>
                                                        </dx:ASPxNavBar>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Height="20px" ShowCaption="False" VerticalAlign="Middle">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                                <CaptionSettings VerticalAlign="Middle" />
                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                            </dx:LayoutItem>
                                        </Items>
                                        <SettingsItems VerticalAlign="Middle" />
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
