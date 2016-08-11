<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uModifyFixedPrice.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uModifyFixedPrice" %>
<script type="text/javascript">
    function OnBackClick(s, e) {
        var currentTab = pc_createModifyingFixedPrice.GetTab(pc_createModifyingFixedPrice.GetActiveTabIndex());
        var previousTab = pc_createModifyingFixedPrice.GetTab(pc_createModifyingFixedPrice.GetActiveTabIndex() - 1);
        currentTab.SetEnabled(false);
        previousTab.SetEnabled(true);
        pc_createModifyingFixedPrice.SetActiveTab(previousTab);
    }

    function OnNextClick(s, e) {
        var currentTab = pc_createModifyingFixedPrice.GetTab(pc_createModifyingFixedPrice.GetActiveTabIndex());
        var nextTab = pc_createModifyingFixedPrice.GetTab(pc_createModifyingFixedPrice.GetActiveTabIndex() + 1);
        nextTab.SetEnabled(true);
        currentTab.SetEnabled(false);
        pc_createModifyingFixedPrice.SetActiveTab(nextTab);
    }

    function wizardCreateModifyingFixedPrice_tabchanged(s, e) {
        btnFinish.SetClientVisible(false);
        if (pc_createModifyingFixedPrice.GetActiveTabIndex() == 3) {
            btnNext.SetClientVisible(false);
            btnFinish.SetClientVisible(true);
        } else {
            btnNext.SetClientVisible(true);
        }

        if (pc_createModifyingFixedPrice.GetActiveTabIndex() == 0)
            btnBack.SetClientVisible(false);
        else
            btnBack.SetClientVisible(true);
    }
</script>
<dx:ASPxPopupControl ID="popup_CreaterModifyFixedPrice" ClientInstanceName="popup_CreaterModifyFixedPrice" runat="server" HeaderText="Thao Tác Hiệu Chỉnh Giá Bán"
    CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" RenderMode="Classic"
    PopupVerticalAlign="WindowCenter" AllowResize="True" Width="1100px" Height="600px" ScrollBars="Auto" ShowFooter="true" 
    AllowDragging="True">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxPageControl ID="pc_createModifyingFixedPrice" 
                ClientInstanceName="pc_createModifyingFixedPrice" runat="server" RenderMode="Classic"
                Width="100%" Height="100%" ActiveTabIndex="0">
                <ClientSideEvents ActiveTabChanged="wizardCreateModifyingFixedPrice_tabchanged" />
                <TabPages>
                    <dx:TabPage Text="Thông tin chung">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxFormLayout ID="form_ModifyFixedPrice_common" runat="server">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã hiệu chỉnh" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtModifyingID" runat="server">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày hiệu chỉnh" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit ID="txtCreatedDate" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày hiệu lực" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit ID="txtAffectedDate" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày kết thúc" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit ID="txtDueDate" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ghi chú" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox ID="txtNote" runat="server">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Chọn Hàng Hóa" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl runat="server" >
                                <dx:ASPxGridView ID="grd_pricepolicy" runat="server" AutoGenerateColumns="False" 
                                    KeyFieldName="price_methodid" 
                                    onhtmlrowcreated="grd_pricepolicy_HtmlRowCreated">
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Chọn" ShowSelectCheckbox="True" Width="50px">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="price_methodid" Caption="Mã PP Tính Giá" Width="150px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="price_methodname" Caption="Tên PP Tính Giá">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewBandColumn Caption="Hiệu lực">
                                            <Columns>
                                                <dx:GridViewDataTextColumn FieldName="From" Caption="Từ Ngày" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="To" Caption="Đến Ngày" Width="100px">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="status">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mô tả" Width="200px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn Caption="Chi Tiết" 
                                            ButtonType="Image" Width="60px">
                                            <ClearFilterButton>
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Clear" />
                                                </Image>
                                            </ClearFilterButton>
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton>
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>                                    
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxLabel ID="lbl_titlethamkhao" runat="server" Font-Bold="True" 
                                                Text="Giá tham khảo theo từng mặt hàng" ClientInstanceName="lbl_titlethamkhao">
                                                </dx:ASPxLabel>

                                            <dx:ASPxGridView ID="grv_referencePrice" runat="server" Width="100%"
                                                AutoGenerateColumns="False" ClientInstanceName="grid_thamkhaogia" 
                                                KeyFieldName="productid">
                                                <Columns>
                                                    <dx:GridViewCommandColumn Caption="Chọn" ShowSelectCheckbox="True" Width="50px">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="product_name" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhóm hàng hóa" FieldName="productgrpid" 
                                                        ShowInCustomizationForm="True" Visible="False">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhóm nhà sản xuất" 
                                                        FieldName="manufacturergrpid" ShowInCustomizationForm="True" Visible="False">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manufacturerpid">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhóm nhà cung cấp" 
                                                        FieldName="suppliergrppid" ShowInCustomizationForm="True" Visible="False">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplierpid">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="cost" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Lợi nhuận" FieldName="profit" 
                                                        ShowInCustomizationForm="True" >
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataMemoColumn Caption="Thuế (%)" FieldName="tax" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataMemoColumn>
                                                    <dx:GridViewDataTextColumn Caption="Giá mua" FieldName="income_price" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Giá bán tham khảo" FieldName="ref_price" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Giá bán hiệu chỉnh cũ" FieldName="fixedprice" 
                                                        ShowInCustomizationForm="True">
                                                        <Settings AllowDragDrop="False" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"/>
                                    <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="false" />
                                    <SettingsDetail ShowDetailButtons="true" ShowDetailRow="true" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ColumnResizeMode="NextColumn"></SettingsBehavior>
                                    <SettingsPager PageSize="10" ShowEmptyDataRows="true"></SettingsPager>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>
                                    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                                    <Styles>
                                        <Header HorizontalAlign="Center" Font-Bold="true">
                                        </Header>             
                                        <CommandColumn Spacing="10px">
                                        </CommandColumn>
                                    </Styles>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Hiệu Chỉnh Giá Bán" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxLabel ID="lbl_titlethamkhao" runat="server" Font-Bold="True" 
                                    Text="Danh mục hàng hóa cần hiệu chỉnh giá" ClientInstanceName="lbl_titlethamkhao">
                                </dx:ASPxLabel>
                                <dx:ASPxGridView ID="grv_FixedPrice" runat="server" Width="100%"
                                    AutoGenerateColumns="False" ClientInstanceName="grid_thamkhaogia" 
                                    KeyFieldName="productid">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" 
                                            ShowInCustomizationForm="True">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="product_name" 
                                            ShowInCustomizationForm="True">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhóm hàng hóa" FieldName="productgrpid" 
                                            ShowInCustomizationForm="True" Visible="False">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhóm nhà sản xuất" 
                                            FieldName="manufacturergrpid" ShowInCustomizationForm="True" Visible="False">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manufacturerpid">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhóm nhà cung cấp" 
                                            FieldName="suppliergrppid" ShowInCustomizationForm="True" Visible="False">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplierpid">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="cost" 
                                            ShowInCustomizationForm="True">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Lợi nhuận" FieldName="profit" 
                                            ShowInCustomizationForm="True" >
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataMemoColumn Caption="Thuế (%)" FieldName="tax" 
                                            ShowInCustomizationForm="True">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataMemoColumn>
                                        <dx:GridViewDataTextColumn Caption="Giá mua" FieldName="income_price" 
                                            ShowInCustomizationForm="True"> 
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Giá bán tham khảo" FieldName="ref_price" 
                                            ShowInCustomizationForm="True">
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Giá bán hiệu chỉnh" FieldName="fixedprice" 
                                            ShowInCustomizationForm="True">
                                            <DataItemTemplate>
                                                <dx:ASPxTextBox ID="txtFixedPrice" runat="server" Width="100%">
                                                </dx:ASPxTextBox>
                                            </DataItemTemplate>
                                            <Settings AllowDragDrop="False"></Settings>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Hoàn Tất" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxLabel ID="lblFinish" Text="Thao tác hiệu chỉnh đã hoàn tất" runat="server" Font-Bold="true" Font-Size="Large">
                                </dx:ASPxLabel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="wizardCreateModifyingFixedPrice_tabchanged"></ClientSideEvents>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterTemplate>
        <div style="padding: 10px;">
            <div class="float-left">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
            <div class="float-right">
                <div class="float-left">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnBack" clientinstancename="btnBack" runat="server" Text="Lùi Lại" AutoPostBack="False"
                        clientvisible="false">
                        <ClientSideEvents Click="OnBackClick" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Backward" />
                        </Image>
                    </dx:ASPxButton>
                </div>
                <div class="float-left button-left-margin">
                    <!-- Places button here -->
                    <dx:ASPxButton ID="btnNext" clientinstancename="btnNext" runat="server" Text="Tiếp" AutoPostBack="false">
                        <ClientSideEvents Click="OnNextClick" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Forward" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnFinish" clientinstancename="btnFinish" runat="server" Text="Lưu hiệu chỉnh"
                        clientvisible="false">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Finished" />
                        </Image>
                    </dx:ASPxButton>
                </div>
            </div>
        </div>
    </FooterTemplate>
</dx:ASPxPopupControl>
