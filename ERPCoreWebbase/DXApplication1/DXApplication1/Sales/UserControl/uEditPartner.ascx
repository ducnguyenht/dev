<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uEditPartner.ascx.cs" Inherits="WebModule.GUI.usercontrol.uEditPartner" %>
<script type="text/javascript">
    
</script>
<div id="lineContainerPartner">
    <dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" 
        PopupVerticalAlign="WindowCenter" Modal="True" DisappearAfter="2000" 
        CloseAction="CloseButton" AllowDragging="True" AllowResize="True" 
        ScrollBars="Auto" ClientInstanceName="formPartnerEdit" 
        HeaderText="Th&#244;ng tin cộng t&#225;c vi&#234;n" LoadingPanelText="Nạp" 
        ShowFooter="True" ShowLoadingPanel="False" Width="900px" Height="600px" 
        ID="formPartnerEdit" OnWindowCallback="formPartnerEdit_WindowCallback">
<ClientSideEvents CloseButtonClick="formPartnerEdit_CloseUp" 
            CloseUp="formPartnerEdit_CloseUp" Shown="formPartnerEdit_Shown"></ClientSideEvents>
<FooterContentTemplate>
            <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="bPartnerEditHelp" runat="server" AutoPostBack="False" 
                                Text="Trợ Giúp" ClientInstanceName="bPartnerEditHelp">
                                <ClientSideEvents Click="bPartnerEditHelp_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="bPartnerEditCancel" runat="server" AutoPostBack="false" 
                                clientinstancename="bPartnerEditCancel" Text="Thoát">
                                <ClientSideEvents Click="bPartnerEditCancel_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="bPartnerEditSave" runat="server" clientinstancename="bPartnerEditSave" 
                                Text="Lưu lại" AutoPostBack="False">
                                <ClientSideEvents Click="bPartnerEditSave_Click" />
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
                <dx:ASPxCallbackPanel ID="cpPartnerEdit" runat="server" 
                    ClientInstanceName="cpPartnerEdit" LoadingPanelText="Đang nạp" 
                    OnCallback="cpPartnerEdit_Callback" ShowLoadingPanel="False" Width="100%">
                    <ClientSideEvents EndCallback="cpPartnerEdit_EndCallback" />
                    <LoadingDivStyle BackColor="White" Opacity="100">
                    </LoadingDivStyle>
                    <PanelCollection>
                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxPageControl ID="pcPartnerEdit" runat="server" ActiveTabIndex="0" 
                                ClientInstanceName="pcPartnerEdit" Height="100%" Width="100%">
                                <TabPages>
                                    <dx:TabPage Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout ID="ASPxFormLayout11" runat="server" 
                                                    AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã  CTV" HelpText="Tối đa 128 ký tự">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxCallbackPanel ID="cpPartnerCode" runat="server" 
                                                                                ClientInstanceName="cpPartnerCode" OnCallback="cpPartnerCode_Callback" 
                                                                                Width="200px">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txtPartnerCode" runat="server" 
                                                                                            ClientInstanceName="txtPartnerCode" MaxLength="128" 
                                                                                            OnValidation="txtPartnerCode_Validation" Width="200px">
                                                                                            <ClientSideEvents Validation="txtPartnerCode_Validation" />
                                                                                            <NullTextStyle ForeColor="Silver">
                                                                                            </NullTextStyle>
                                                                                            <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                                                <RequiredField ErrorText="Chưa nhập mã cộng tác viên" IsRequired="True" />
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
                                                                <dx:LayoutItem Caption="Tên CTV" HelpText="255 ký tự, không cho phép trùng lắp">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="txtPartnerName" runat="server" 
                                                                                ClientInstanceName="txtPartnerName" MaxLength="255" 
                                                                                OnValidation="txtPartnerName_Validation" Width="400px">
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
                                                                            <dx:ASPxComboBox ID="cboPartnerRowStatus" runat="server" 
                                                                                ClientInstanceName="cboPartnerRowStatus" Width="200px">
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
                                    <dx:TabPage Text="Trực Thuộc Nhóm">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="grdPartnerPartnerCategory" runat="server" 
                                                    AutoGenerateColumns="False" ClientInstanceName="grdPartnerPartnerCategory" 
                                                    KeyFieldName="PartnerPartnerCategoryId" 
                                                    OnCellEditorInitialize="grdPartnerCategory_CellEditorInitialize" 
                                                    OnRowDeleting="grdPartnerCategory_RowDeleting" 
                                                    OnRowInserting="grdPartnerCategory_RowInserting" 
                                                    OnRowUpdating="grdPartnerCategory_RowUpdating" 
                                                    OnRowValidating="grdPartnerCategory_RowValidating" Width="100%">
                                                    <Columns>
                                                        <dx:GridViewDataComboBoxColumn Caption="Mã nhóm cộng tác viên" FieldName="Code" 
                                                            ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                            <PropertiesComboBox DropDownStyle="DropDown" 
                                                                IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{0}" 
                                                                ValueField="Code"
                                                                OnItemRequestedByValue="cboPartnerCategory_ItemRequestedByValue" 
                                                                OnItemsRequestedByFilterCondition="cboPartnerCategory_ItemsRequestedByFilterCondition" >
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="Mã nhóm CTV" FieldName="Code" Name="Code" 
                                                                        Width="150px" />
                                                                    <dx:ListBoxColumn Caption="Tên nhóm CTV" FieldName="Name" Width="200px" />
                                                                    <dx:ListBoxColumn Caption="Description" FieldName="Description" 
                                                                        Name="Description" Width="0px" />
                                                                </Columns>
                                                            </PropertiesComboBox>
                                                        </dx:GridViewDataComboBoxColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên nhóm cộng tác viên" FieldName="Name" 
                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                                                            ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                            ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
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
                                                            <CancelButton>
                                                                <Image ToolTip="Bỏ qua">
                                                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                                                </Image>
                                                            </CancelButton>
                                                            <UpdateButton>
                                                                <Image ToolTip="Cập nhật">
                                                                    <SpriteProperties CssClass="Sprite_Apply" />
                                                                </Image>
                                                            </UpdateButton>
                                                            <ClearFilterButton Visible="True">
                                                                <Image ToolTip="Hủy">
                                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                                </Image>
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PartnerId" ShowInCustomizationForm="True" 
                                                            Visible="False" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="PartnerCategoryPropertyId" 
                                                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
                                                        AllowSelectSingleRowOnly="True" />
                                                    <SettingsPager PageSize="50">
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="Inline" />
                                                    <Settings VerticalScrollableHeight="360" VerticalScrollBarMode="Auto" />
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Thông Tin Chi Tiết">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxNavBar ID="nbPartnerEdit" runat="server" AutoCollapse="True" 
                                                    ClientInstanceName="nbPartnerEdit" Height="100%" Width="100%">
                                                    <Groups>
                                                        <dx:NavBarGroup Text="Mô Tả">
                                                            <Items>
                                                                <dx:NavBarItem>
                                                                    <Template>
                                                                        <dx:ASPxHtmlEditor ID="htmlDescription" runat="server" 
                                                                            ClientInstanceName="htmlDescription" Height="350px" Width="100%">
                                                                            <Settings AllowHtmlView="False" AllowPreview="False" />
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
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="False" 
                                                        Font-Italic="False" Font-Size="XX-Small" ForeColor="Gray" 
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp">
                                                    </dx:ASPxLabel>
                                                </div>
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
<dx:ASPxHiddenField ID="hPartnerEditId" runat="server" 
    ClientInstanceName="hPartnerEditId">
</dx:ASPxHiddenField>

