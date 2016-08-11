<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uCreateConditionToReceiveReturnProduct.ascx.cs"
    Inherits="WebModule.GUI.usercontrol.uCreateConditionToReceiveReturnProduct" %>
<%@ Register Src="~/Sales/UserControl/uSettingManufacturersList.ascx" TagName="uSettingManufacturersList"
    TagPrefix="uc1" %>
<%@ Register Src="~/Sales/UserControl/uSettingSuppliersList.ascx" TagName="uSettingSuppliersList"
    TagPrefix="uc2" %>    
<script type="text/javascript">
    function btnback_click(s, e) {
        var currentTab = pc.GetTab(pc.GetActiveTabIndex());
        var previousTab = pc.GetTab(pc.GetActiveTabIndex() - 1);
        currentTab.SetEnabled(false);
        previousTab.SetEnabled(true);
        pc.SetActiveTab(previousTab);
    }

    function btnnext_click(s, e) {
        var currentTab = pc.GetTab(pc.GetActiveTabIndex());
        var nextTab = pc.GetTab(pc.GetActiveTabIndex() + 1);
        nextTab.SetEnabled(true);
        currentTab.SetEnabled(false);
        pc.SetActiveTab(nextTab);
    }

    function pc_tabchanged(s, e) {
        btnFinish.SetClientVisible(false);
        if (pc.GetActiveTabIndex() == 3) {
            btnNext.SetClientVisible(false);
            btnFinish.SetClientVisible(true);
        } else {
            btnNext.SetClientVisible(true);
        }

        if (pc.GetActiveTabIndex() == 0)
            btnBack.SetClientVisible(false);
        else
            btnBack.SetClientVisible(true);
    }
</script>
<uc1:uSettingManufacturersList ID="uSettingManufacturersList1" runat="server" />
<uc2:uSettingSuppliersList ID="uSettingSuppliersList1" runat="server" />
<dx:aspxpopupcontrol id="popup_editrecord" clientinstancename="popup_editrecord"
        runat="server" allowdragging="True" allowresize="True" headertext="Hướng dẫn tạo chính sách trả hàng"
        modal="True" rendermode="Classic" popupverticalalign="WindowCenter" popuphorizontalalign="WindowCenter"
        minheight="100px" minwidth="100px" height="600px" width="1100px" ScrollBars="Auto" ShowFooter="true">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <dx:aspxpagecontrol id="pc" runat="server" rendermode="Classic" activetabindex="0"
                    height="100%" width="100%" clientinstancename="pc" ClientSideEvents-ActiveTabChanged="pc_tabchanged">
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung" ClientEnabled="true">
                            <ContentCollection>
                                <dx:ContentControl Height="100%">
                                    <dx:ASPxFormLayout ID="form_commoninfo" runat="server">
                                        <Items>
                                            <dx:LayoutItem Caption="Mã chính sách">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="txt_common_id" runat="server" Width="170px" ReadOnly="false">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tên chính sách">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="txt_common_name" runat="server" Width="170px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Hiệu lực từ">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxDateEdit ID="txt_common_from" runat="server">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Hiệu lực đến">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxDateEdit ID="txt_common_to" runat="server">
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Mô tả">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                        SupportsDisabledAttribute="True">
                                                        <dx:ASPxTextBox ID="txt_common_description" runat="server" Height="23px" 
                                                            Width="383px">
                                                        </dx:ASPxTextBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:ASPxFormLayout>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Điều Kiện" ClientEnabled="false">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl11" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Bước 1: Chọn yếu tố"
                                        Height="158px" View="GroupBox" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel1" runat="server" Height="225px" ScrollBars="Auto" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxNavBar ID="ASPxNavBar4" runat="server" Width="100%">
                                                                <Groups>
                                                                    <dx:NavBarGroup Expanded="false" Text="Nhà sản xuất">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox17" runat="server" Checked="True">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel34" runat="server" Style="color: Red" Text="Nhà sản xuất">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox18" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel36" runat="server" Style="color: Red" Text="Nhóm nhà sản xuất">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Nhà Cung Cấp">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox19" runat="server" Checked="True">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel38" runat="server" Style="color: Red" Text="Nhà cung cấp">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox20" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel40" runat="server" Style="color: Red" Text="Nhóm nhà cung cấp">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Khách Hàng">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox21" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="Áp dụng cho">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel42" runat="server" Style="color: Red" Text="Khách hàng">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox22" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="Áp dụng cho">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel44" runat="server" Style="color: Red" Text="Nhóm khách hàng">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Hàng Hóa">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox23" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Áp dụng cho">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel46" runat="server" Style="color: Red" Text="hàng hóa">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox24" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel47" runat="server" Text="Áp dụng cho ">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel48" runat="server" Style="color: Red" Text="Nhóm hàng hóa">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Text="Thanh Toán" Expanded="False">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox29" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Áp dụng cho">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel58" runat="server" Style="color: Red" Text="Hình thức thanh toán">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Text="Thời hạn trả lại" Expanded="False">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox29" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Áp dụng theo">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel58" runat="server" Style="color: Red" Text="Thời gian">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                </Groups>
                                                            </dx:ASPxNavBar>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>
                                    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" HeaderText="Bước 2: Chọn dữ liệu yếu tố"
                                        View="GroupBox">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel2" runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent4" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel61" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink1" runat="server" NavigateUrl=""
                                                                Text="Nhà sản xuất">
                                                                <ClientSideEvents Click="show_settingManufacturersList" />
                                                            </dx:ASPxHyperLink>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Và">
                                                            </dx:ASPxLabel>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink2" runat="server" NavigateUrl=""
                                                                Text="Nhà cung cấp">
                                                                <ClientSideEvents Click="show_settingSuppliersList" />
                                                            </dx:ASPxHyperLink>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Loại Trừ" ClientEnabled="False">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="Bước 1: Chọn yếu tố"
                                        Height="158px" View="GroupBox" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent5" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel3" runat="server" Height="225px" ScrollBars="Auto" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent9" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Width="100%">
                                                                <Groups>
                                                                    <dx:NavBarGroup Expanded="false" Text="Nhà sản xuất">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox17" runat="server" Checked="True">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel34" runat="server" Style="color: Red" Text="Nhà sản xuất">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox18" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="Loại Trừ hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel36" runat="server" Style="color: Red" Text="Nhóm nhà sản xuất">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Nhà Cung Cấp">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox19" runat="server" Checked="True">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel38" runat="server" Style="color: Red" Text="Nhà cung cấp">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox20" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel40" runat="server" Style="color: Red" Text="Nhóm nhà cung cấp">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Khách Hàng">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox21" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="Loại trừ">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel42" runat="server" Style="color: Red" Text="khách hàng">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox22" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="Loại trừ">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel44" runat="server" Style="color: Red" Text="Nhóm khách hàng">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Hàng Hóa">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox23" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Loại trừ">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel46" runat="server" Style="color: Red" Text="Hàng hóa">
                                                                                </dx:ASPxLabel>
                                                                                <br />
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox24" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel47" runat="server" Text="Loại trừ">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel48" runat="server" Style="color: Red" Text="Nhóm hàng hóa">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Expanded="False" Text="Thanh Toán">
                                                                        <ContentTemplate>
                                                                            <div>
                                                                                <dx:ASPxCheckBox ID="ASPxCheckBox29" runat="server">
                                                                                </dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Loại trừ">
                                                                                </dx:ASPxLabel>
                                                                                <dx:ASPxLabel ID="ASPxLabel58" runat="server" Style="color: Red" Text="hình thức thanh toán">
                                                                                </dx:ASPxLabel>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </dx:NavBarGroup>
                                                                </Groups>
                                                            </dx:ASPxNavBar>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>
                                    <dx:ASPxRoundPanel ID="ASPxRoundPanel6" runat="server" Width="100%" HeaderText="Bước 2: Chọn dữ liệu yếu tố"
                                        View="GroupBox">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent10" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel6" runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink3" runat="server" NavigateUrl=""
                                                                Text="Nhà sản xuất">
                                                                <ClientSideEvents Click="show_settingManufacturersList" />
                                                            </dx:ASPxHyperLink>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Và">
                                                            </dx:ASPxLabel>
                                                            <br />
                                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink4" runat="server" NavigateUrl=""
                                                                Text="Nhà cung cấp">
                                                                <ClientSideEvents Click="show_settingSuppliersList" />
                                                            </dx:ASPxHyperLink>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Cấu hình trách nhiệm" ClientEnabled="false">
                            <ContentCollection>           
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxPanel runat="server" Width="100%" Height="500px" ScrollBars="Vertical">
                                        <PanelCollection>
                                            <dx:PanelContent>
                                                <dx:ASPxFormLayout ID="form_group" runat="server">
                                                    <Items>
                                                        <dx:LayoutItem Caption="Gom nhóm theo">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxComboBox ID="cbo_group" runat="server">
                                                                        <ClientSideEvents 
                                                                                SelectedIndexChanged="function(s) { grv_responsibility.PerformCallback(s.GetValue()) }"></ClientSideEvents>
                                                                        <Items>
                                                                            <dx:ListEditItem Text="Nhóm hàng hóa" Value="productgrpid" />
                                                                            <dx:ListEditItem Text="Nhóm nhà sản xuất" Value="manufacturergrpid" />
                                                                            <dx:ListEditItem Text="Nhóm nhà cung cấp" Value="suppliergrppid" />
                                                                        </Items>
                                                                        <ClientSideEvents SelectedIndexChanged="function(s) { grv_responsibility.PerformCallback(s.GetValue()) }"/>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                                <dx:ASPxLabel ID="ASPxLabel62" runat="server" Font-Bold="True" 
                                                    Text="Cấu hình theo sản phẩm">
                                                </dx:ASPxLabel>
                                                <dx:ASPxGridView ID="grv_responsibility" ClientInstanceName="grv_responsibility" runat="server" AutoGenerateColumns="False"
                                                    OnCustomCallback="grv_responsibility_CustomCallback" 
                                                    OnHtmlRowPrepared="grv_responsibility_HtmlRowPrepared">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True" 
                                                            VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname"
                                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Nhóm hàng hóa" FieldName="productgrpid" 
                                                            ShowInCustomizationForm="True" VisibleIndex="2" Visible="false">
                                                            <Settings AllowDragDrop="False"></Settings>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhóm nhà sản xuất" FieldName="manufacturergrpid" 
                                                            ShowInCustomizationForm="True" VisibleIndex="3" Visible="false">
                                                            <Settings AllowDragDrop="False"></Settings>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhà sản xuất" FieldName="manufacturerpid" 
                                                            ShowInCustomizationForm="True" VisibleIndex="4" Visible="false">
                                                            <Settings AllowDragDrop="False"></Settings>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhóm nhà cung cấp" FieldName="suppliergrppid" 
                                                            ShowInCustomizationForm="True" VisibleIndex="5" Visible="false">
                                                            <Settings AllowDragDrop="False"></Settings>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Nhà cung cấp" FieldName="supplierpid" 
                                                            ShowInCustomizationForm="True" VisibleIndex="6" Visible="false">
                                                            <Settings AllowDragDrop="False"></Settings>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="productunit" ShowInCustomizationForm="True" 
                                                            VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Thời hạn trả lại (ngày)" FieldName="numofdate" ShowInCustomizationForm="True" 
                                                            VisibleIndex="8">
                                                            <DataItemTemplate>
                                                                <dx:ASPxSpinEdit ID="spin_numofdate" runat="server" Width="50px" Number="10">
                                                                </dx:ASPxSpinEdit>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Giá thu hồi" ShowInCustomizationForm="True" 
                                                            VisibleIndex="9">
                                                            <DataItemTemplate>
                                                                <dx:ASPxTextBox ID="txt_returnPrice" runat="server">
                                                                </dx:ASPxTextBox>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="reponsibility" ShowInCustomizationForm="True" 
                                                            VisibleIndex="10">
                                                            <DataItemTemplate>
                                                                <dx:ASPxMemo ID="txt_trachnhiem" runat="server" 
                                                                    Text='<%# Bind("reponsibility") %>' Width="250px" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" ShowInCustomizationForm="True" 
                                                            VisibleIndex="11">
                                                            <DataItemTemplate>
                                                                <dx:ASPxMemo ID="txt_mota" runat="server" Text='<%# Bind("description") %>'  Width="250px" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <GroupRowContent>
                                                            <dx:ASPxLabel ID="lbl_groupname" Text="Tên nhóm modify ở đây" runat="server" 
                                                                Font-Bold="True" />
                                                            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server">
                                                                <Items>
                                                                    <dx:LayoutGroup Caption="Cài đặt trách nhiệm theo nhóm" ColCount="3">
                                                                        <Items>
                                                                            <dx:LayoutItem Caption="Tỉ lệ giá nhận (So với giá ban đầu)">
                                                                                <LayoutItemNestedControlCollection>
                                                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                        SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxTextBox ID="txt_returnPrice" runat="server"  >
                                                                                        </dx:ASPxTextBox>
                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                </LayoutItemNestedControlCollection>
                                                                            </dx:LayoutItem>
                                                                            <dx:LayoutItem Caption="Nội dung trách nhiệm">
                                                                                <LayoutItemNestedControlCollection>
                                                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                        SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxMemo ID="txt_responsibility" runat="server" Width="250px" >
                                                                                        </dx:ASPxMemo>
                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                </LayoutItemNestedControlCollection>
                                                                            </dx:LayoutItem>
                                                                            <dx:LayoutItem ShowCaption="False">
                                                                                <LayoutItemNestedControlCollection>
                                                                                    <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                        SupportsDisabledAttribute="True">
                                                                                        <dx:ASPxButton ID="ASPxFormLayout2_E4" runat="server" Text="Áp dụng toàn nhóm" 
                                                                                            Width="141px">
                                                                                        </dx:ASPxButton>
                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                </LayoutItemNestedControlCollection>
                                                                            </dx:LayoutItem>
                                                                        </Items>
                                                                    </dx:LayoutGroup>
                                                                </Items>
                                                            </dx:ASPxFormLayout> 
                                                        </GroupRowContent>
                                                    </Templates>
                                                </dx:ASPxGridView>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxPanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:aspxpagecontrol>

        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel  runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <div style="float:left">
                        <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help" />
                            </Image>
                        </dx:ASPxButton>
                    </div>
                    <div style="float: right; margin-left: 4px">
                        <dx:ASPxButton ID="btnFinish" clientinstancename="btnFinish" runat="server" Text="Lưu chính sách"
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
    <ClientSideEvents AfterResizing="popup_editrecord_resize" />
</dx:aspxpopupcontrol>
