<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uBusinessPerson.ascx.cs" Inherits="WebModule.Member.UserControl.uBusinessPerson" %>
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
                                                    <dx:LayoutItem Caption="Mã doanh nhân" HelpText="Tối đa 128 ký tự">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtMaDoanhNghiep" runat="server" ClientInstanceName="txtMaDoanhNghiep"
                                                                    Width="200px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập mã doanh nhân" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                        <CaptionCellStyle CssClass="CaptionStyle">
                                                        </CaptionCellStyle>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tên doanh nhân" HelpText="255 ký tự, không cho phép trùng lắp">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtTenDoanhNhan" runat="server" ClientInstanceName="txtTenDoanhNhan"
                                                                    MaxLength="255" NullText="255 ký tự, không cho phép trùng lắp" Width="400px">
                                                                    <NullTextStyle ForeColor="Silver">
                                                                    </NullTextStyle>
                                                                    <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                        <RequiredField ErrorText="Chưa nhập tên doanh nhân" IsRequired="True" />
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
                        <dx:TabPage Text="Đại diện cho doanh nghiệp">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFsd22t2" runat="server" Height="100%" Width="100%">
                                        <Items>
                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxGridView ID="grdDaiDienChoDoanhNghiep" runat="server" AutoGenerateColumns="False"
                                                            Width="100%" ClientInstanceName="grdDaiDienChoDoanhNghiep">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Chức vụ" ShowInCustomizationForm="True" VisibleIndex="5"
                                                                    Width="250px" FieldName="chucvu" Name="chucVu">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="9" Width="100px">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btNewDDDN">
                                                                            <Image>
                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                            </Image>
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btEditDDDN">
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
                                                                <dx:GridViewDataTextColumn Caption="STT" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                    Width="5%" FieldName="stt" Name="STT">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên doanh nghiệp" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2" FieldName="tendn" Name="tenDN">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã doanh nghiệp" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1" FieldName="madn" Name="maDN">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mô tả" ShowInCustomizationForm="True" 
                                                                    VisibleIndex="7" FieldName="mota" Name="moTa">
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
                        <dx:TabPage Text="Thông Tin Chi Tiết" Name="tabDetail">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl9" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Height="100%" Width="100%">
                                        <Items>
                                            <dx:LayoutItem HorizontalAlign="Right" ShowCaption="False">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
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
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
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
