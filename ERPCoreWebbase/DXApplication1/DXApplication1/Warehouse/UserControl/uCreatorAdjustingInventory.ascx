<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uCreatorAdjustingInventory.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uCreatorAdjustingInventory" %>
<%@ Register src="~/Warehouse/UserControl/uAdjustingInventoryInfo.ascx" tagname="uAdjustingInventoryInfo" tagprefix="uc1" %>
<%@ Register src="~/Warehouse/UserControl/uVerifyingInventoryInfo.ascx" tagname="uVerifyingInventoryInfo" tagprefix="uc2" %>
<script type="text/javascript">
    function btnback_click(s, e) {
        var currentTab = pc_CreatorAdjustingInventory.GetTab(pc_CreatorAdjustingInventory.GetActiveTabIndex());
        var previousTab = pc_CreatorAdjustingInventory.GetTab(pc_CreatorAdjustingInventory.GetActiveTabIndex() - 1);
        currentTab.SetEnabled(false);
        previousTab.SetEnabled(true);
        pc_CreatorAdjustingInventory.SetActiveTab(previousTab);
    }

    function btnnext_click(s, e) {
        var currentTab = pc_CreatorAdjustingInventory.GetTab(pc_CreatorAdjustingInventory.GetActiveTabIndex());
        var nextTab = pc_CreatorAdjustingInventory.GetTab(pc_CreatorAdjustingInventory.GetActiveTabIndex() + 1);
        nextTab.SetEnabled(true);
        currentTab.SetEnabled(false);
        pc_CreatorAdjustingInventory.SetActiveTab(nextTab);
    }

    function pc_tabchanged(s, e) {
        btnFinish.SetClientVisible(false);
        if (pc_CreatorAdjustingInventory.GetActiveTabIndex() == 2) {
            btnNext.SetClientVisible(false);
            btnFinish.SetClientVisible(true);
        } else {
            btnNext.SetClientVisible(true);
        }

        if (pc_CreatorAdjustingInventory.GetActiveTabIndex() == 0)
            btnBack.SetClientVisible(false);
        else
            btnBack.SetClientVisible(true);
    }

    function ShowInfoclick(s, e) {
        popup_VerifyingInventoryInfo.Show();
    }
</script>
<dx:ASPxPopupControl ID="popup_CreatorAdjustingInventory" runat="server" CloseAction="CloseButton"
    ClientInstanceName="popup_CreatorAdjustingInventory" AllowDragging="True" AllowResize="True" 
    PopupAnimationType="None" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    EnableViewState="False" HeaderText="Kiểm Tra Chương Trình Khuyến Mãi" Width="1200px" Height="600px" ScrollBars="Auto" ShowFooter="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pc_CreatorAdjustingInventory" ClientInstanceName="pc_CreatorAdjustingInventory" 
                runat="server" RenderMode="Classic" Width="100%" ActiveTabIndex="0">
                <ClientSideEvents ActiveTabChanged="pc_tabchanged" />
                <TabPages>
                    <dx:TabPage Text="Chọn chứng từ">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxGridView ID="grvListOfEvidence" runat="server" 
                                    KeyFieldName="code" AutoGenerateColumns="False" Width = "100%">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" Caption="Chọn">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã kiểm kho" FieldName="code">
                                            <DataItemTemplate>
                                                <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("code") %>'>
                                                    <ClientSideEvents Click="ShowInfoclick" />
                                                </dx:ASPxHyperLink>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày kiểm kho" FieldName="date">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Trưởng ban" FieldName="admin">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhân viên 1" FieldName="staff1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhân viên 2" FieldName="staff2">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                        ShowHeaderFilterButton="True" />
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Thông tin điều chỉnh" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl>
                                <dx:ASPxFormLayout ID="form_adjust" runat="server" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã điều chỉnh">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày điều chỉnh">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ReadOnly="true">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người điều chỉnh">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" ReadOnly="true">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ghi chú">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%" ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Danh sách hàng hóa tồn kho" Font-Bold="True"
                                                        Font-Size="Small">
                                                    </dx:ASPxLabel>
                                                    <dx:ASPxGridView ID="grvConfirmData" Width="100%" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" ShowInCustomizationForm="True" VisibleIndex="0"
                                                                FieldName="code">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                                <FooterTemplate>
                                                                    Cộng
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" ShowInCustomizationForm="True" VisibleIndex="2"
                                                                FieldName="unit">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="recieptamount" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                                <DataItemTemplate>
                                                                    .........
                                                                </DataItemTemplate>
                                                                <FooterTemplate>
                                                                    .........
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                                                VisibleIndex="6">
                                                                <DataItemTemplate>
                                                                    .........
                                                                </DataItemTemplate>
                                                                <FooterTemplate>
                                                                    .........
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
                                                                <DataItemTemplate>
                                                                    .........
                                                                </DataItemTemplate>
                                                                <FooterTemplate>
                                                                    .........
                                                                </FooterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewBandColumn Caption="Phẩm chất" VisibleIndex="8">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Còn tốt 100%">
                                                                        <DataItemTemplate>
                                                                            .........
                                                                        </DataItemTemplate>
                                                                        <FooterTemplate>
                                                                            .........
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Kém phẩm chất">
                                                                        <DataItemTemplate>
                                                                            .........
                                                                        </DataItemTemplate>
                                                                        <FooterTemplate>
                                                                            .........
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Mất phẩm chất">
                                                                        <DataItemTemplate>
                                                                            .........
                                                                        </DataItemTemplate>
                                                                        <FooterTemplate>
                                                                            .........
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                            </dx:GridViewBandColumn>
                                                            <dx:GridViewDataTextColumn Caption="SL điều chỉnh" ShowInCustomizationForm="True"
                                                                VisibleIndex="9" >
                                                                <DataItemTemplate>
                                                                    <dx:ASPxTextBox ID="txtAjustNumber" runat="server" Width="100%"></dx:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Ghi chú" ShowInCustomizationForm="True"
                                                                VisibleIndex="10" >
                                                                <DataItemTemplate>
                                                                    <dx:ASPxTextBox ID="txtNote" runat="server" Width="100%"></dx:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Hoàn tất" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <uc1:uAdjustingInventoryInfo ID="uAdjustingInventoryInfo1" runat="server" /> 
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="pc_tabchanged"></ClientSideEvents>
            </dx:ASPxPageControl>
</dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel ID="ASPxPanel1"  runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                    <div style="float:left">
                        <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="btnFinish" clientinstancename="btnFinish" runat="server" Text="Lưu phiếu điều chỉnh"
                            clientvisible="false">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Finished" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="btnNext" clientinstancename="btnNext" runat="server" Text="Tiếp" AutoPostBack="False">
                            <ClientSideEvents Click="btnnext_click"></ClientSideEvents>
                            <Image>
                                <SpriteProperties CssClass="Sprite_Forward" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right">
                        <dx:ASPxButton ID="btnBack" clientinstancename="btnBack" runat="server" Enabled="true" Text="Lùi Lại" AutoPostBack="False">
                            <ClientSideEvents Click="btnback_click" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Backward" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxPanel>
    </FooterContentTemplate>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popup_VerifyingInventoryInfo" runat="server" HeaderText="Thông Tin Kiểm Kho" Height="400px" ScrollBars="Auto"
    Modal="True" RenderMode="Classic" Width="1000px" ClientInstanceName="popup_VerifyingInventoryInfo"
    AllowResize="True" AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    LoadingPanelDelay="1000" ShowSizeGrip="False" ShowMaximizeButton="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
            <uc2:uVerifyingInventoryInfo ID="uVerifyingInventoryInfo1" runat="server" /> 
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
