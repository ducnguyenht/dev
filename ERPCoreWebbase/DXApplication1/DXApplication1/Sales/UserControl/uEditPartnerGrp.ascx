<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditPartnerGrp.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uEditPartnerGrp" %>
<div id="lineContainerPartnerCategory">
    <dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Modal="True" AllowDragging="True" 
        AllowResize="True" ScrollBars="Auto" 
        ClientInstanceName="formPartnerCategoryEdit" 
        HeaderText="Th&#244;ng Tin Nh&#243;m Cộng T&#225;c Vi&#234;n" ShowFooter="True" 
        Width="900px" Height="600px" ID="formPartnerCategoryEdit">
<ClientSideEvents Shown="formPartnerCategoryEdit_Shown"></ClientSideEvents>
<FooterContentTemplate>
            <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="bPartnerCategoryEditHelp" runat="server" 
                                AutoPostBack="False" ClientInstanceName="bPartnerCategoryEditHelp" 
                                Text="Trợ Giúp">
                                <ClientSideEvents Click="bPartnerCategoryEditHelp_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="bPartnerCategoryEditCancel" runat="server" 
                                AutoPostBack="false" clientinstancename="bPartnerCategoryEditCancel" 
                                Text="Thoát">
                                <ClientSideEvents Click="bPartnerCategoryEditCancel_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="bPartnerCategoryEditSave" runat="server" 
                                clientinstancename="bPartnerCategoryEditSave" Text="Lưu lại" 
                                AutoPostBack="False">
                                <ClientSideEvents Click="bPartnerCategoryEditSave_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        
</FooterContentTemplate>
<ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxCallbackPanel ID="cpPartnerCategoryEdit" runat="server" 
                    ClientInstanceName="cpPartnerCategoryEdit" 
                    OnCallback="cpPartnerCategoryEdit_Callback" Width="100%">
                    <ClientSideEvents CallbackError="cpPartnerCategoryEdit_EndCallback
" />
                    <PanelCollection>
                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="pcPartnerCategory" runat="server" ActiveTabIndex="0" 
                                ClientInstanceName="pcPartnerCategory" Height="100%" Width="100%">
                                <TabPages>
                                    <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Nhóm CTV" HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpPartnerCategoryCode" runat="server" 
                                                                                ClientInstanceName="cpPartnerCategoryCode" 
                                                                                OnCallback="cpPartnerCategoryCode_Callback" Width="200px">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtPartnerCategoryCode" runat="server" 
                                                                                            ClientInstanceName="txtPartnerCategoryCode" MaxLength="128" 
                                                                                            OnValidation="txtPartnerCategoryCode_Validation" Width="200px">
                                                                                            <ClientSideEvents Validation="txtPartnerCategoryCode_Validation" />
                                                                                            <NullTextStyle ForeColor="Silver">
                                                                                            </NullTextStyle>
                                                                                            <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                                <RequiredField ErrorText="Chưa nhập mã nhóm khách hàng" IsRequired="True" />
                                                                                            </ValidationSettings>
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Nhóm CTV" 
                                                                    HelpText="255 ký tự, không cho phép trùng lắp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtPartnerCategoryName" runat="server" 
                                                                                ClientInstanceName="txtPartnerCategoryName" MaxLength="255" 
                                                                                OnValidation="txtPartnerCategoryName_Validation" Width="400px">
                                                                                <NullTextStyle ForeColor="Silver">
                                                                                </NullTextStyle>
                                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                    <RequiredField ErrorText="Chưa nhập tên nhóm CTV" IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Trạng Thái" HelpText="Tự động tạo mới">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboPartnerCategoryRowDevice" runat="server" 
                                                                                ClientInstanceName="cboPartnerCategoryRowDevice" Width="200px">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                                                    <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Hình Ảnh" Visible="False">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxImage ID="ASPxFormLayout1_E1" runat="server" Height="200px" 
                                                                                ImageUrl="~/images/NASID/NASERPLogo.png" Width="300px">
                                                                                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                                                                            </dx:ASPxImage>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                    <CaptionCellStyle CssClass="CaptionStyle">
                                                                    </CaptionCellStyle>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption=" " Visible="False">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxUploadControl ID="ASPxFormLayout1_E2" runat="server" UploadMode="Auto" 
                                                                                Width="300px">
                                                                            </dx:ASPxUploadControl>
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
                                    <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxNavBar ID="nbPartnerCategory" runat="server" AutoCollapse="True" 
                                                    ClientInstanceName="nbPartnerCategory" Height="100%" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxHtmlEditor ID="htmlDescription" runat="server" 
                                                                            ClientInstanceName="htmlDescription" Height="350px" Width="100%">
                                                                            <Settings AllowHtmlView="False" AllowPreview="False" />
                                                                        </dx:ASPxHtmlEditor>
                                                                    </Template>
                                                                </dx:NavBarItem>
                                                            </Items>
                                                        </dx:NavBarGroup>
                                                        <dx:NavBarGroup Expanded="False" Text="Tài Liệu" Visible="False">
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
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

            </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>

</div>
<dx:ASPxHiddenField ID="hPartnerCategoryId" runat="server" 
    ClientInstanceName="hPartnerCategoryId">
</dx:ASPxHiddenField>
