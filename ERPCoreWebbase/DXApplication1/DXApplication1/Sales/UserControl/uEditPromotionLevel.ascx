<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditPromotionLevel.ascx.cs" Inherits="WebModule.GUI.Sales.userControl.uEditPromotionLevel" %>
<%@ Register Src="~/Sales/UserControl/uSettingManufacturersList.ascx" TagName="uSettingManufacturersList"
    TagPrefix="uc2" %>
<%@ Register Src="~/Sales/UserControl/uSettingSaleTotalByProductGrp.ascx" TagName="uSettingSaleTotalByProductGrp"
    TagPrefix="uc3" %>
<%@ Register Src="~/Sales/UserControl/uSettingSalesTotalByProduct.ascx" TagName="uSettingSalesTotalByProduct"
    TagPrefix="uc4" %>    
<%@ Register Src="~/Sales/UserControl/uSettingSuppliersList.ascx" TagName="uSettingSuppliersList"
    TagPrefix="uc5" %>    
<%@ Register Src="~/Sales/UserControl/uSettingSalesTotalByValueOfOrder.ascx" TagName="uSettingSalesTotalByValueOfOrder"
    TagPrefix="uc6" %> 
<%@ Register Src="~/Sales/UserControl/uSettingSalesTotal.ascx" TagName="uSettingSalesTotal"
    TagPrefix="uc7" %> 

<script type="text/javascript">

    function show_uEditPromotionLevel(s, e) {
        popup_wizardCreaterPromotionLevel.Show();
    }

    function OnBackClick(s, e) {
        var currentTab = pc_wizardCreaterPromotionLevel.GetTab(pc_wizardCreaterPromotionLevel.GetActiveTabIndex());
        var previousTab = pc_wizardCreaterPromotionLevel.GetTab(pc_wizardCreaterPromotionLevel.GetActiveTabIndex() - 1);
        currentTab.SetEnabled(false);
        previousTab.SetEnabled(true);
        pc_wizardCreaterPromotionLevel.SetActiveTab(previousTab);
    }

    function OnNextClick(s, e) {
        var currentTab = pc_wizardCreaterPromotionLevel.GetTab(pc_wizardCreaterPromotionLevel.GetActiveTabIndex());
        var nextTab = pc_wizardCreaterPromotionLevel.GetTab(pc_wizardCreaterPromotionLevel.GetActiveTabIndex() + 1);
        nextTab.SetEnabled(true);
        currentTab.SetEnabled(false);
        pc_wizardCreaterPromotionLevel.SetActiveTab(nextTab);
    }

    function wizardCreaterPromotionLevel_tabchanged(s, e) {
        btnFinish.SetClientVisible(false);
        if (pc_wizardCreaterPromotionLevel.GetActiveTabIndex() == 2) {
            btnNext.SetClientVisible(false);
            btnFinish.SetClientVisible(true);
        } else {
            btnNext.SetClientVisible(true);
        }

        if (pc_wizardCreaterPromotionLevel.GetActiveTabIndex() == 0)
            btnBack.SetClientVisible(false);
        else
            btnBack.SetClientVisible(true);
    }
</script>

<uc2:uSettingManufacturersList ID="uSettingManufacturersList1" runat="server" />
<uc3:uSettingSaleTotalByProductGrp ID="uSettingSaleTotalByProductGrp" runat="server" />
<uc4:uSettingSalesTotalByProduct ID="uSettingSalesTotalByProduct1" runat="server" />
<uc5:uSettingSuppliersList ID="uSettingSuppliersList1" runat="server" />
<uc6:uSettingSalesTotalByValueOfOrder ID="uSettingSalesTotalByValueOfOrder1" runat="server" />
<uc7:uSettingSalesTotal ID="uSettingSalesTotal1" runat="server" />


<dx:ASPxPopupControl ID="popup_wizardCreaterPromotionLevel" ClientInstanceName="popup_wizardCreaterPromotionLevel" runat="server" HeaderText="Hướng dẫn tạo khuyến mãi"
    CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" RenderMode="Classic"
    PopupVerticalAlign="WindowCenter" AllowResize="True" Width="1000px" Height="600px" ScrollBars="Auto" ShowFooter="true" 
    AllowDragging="True">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxPageControl ID="pc_wizardCreaterPromotionLevel" ClientInstanceName="pc_wizardCreaterPromotionLevel"
            runat="server" RenderMode="Classic" ActiveTabIndex="0" Height="100%" Width="100%" >
                <TabPages>
                    <dx:TabPage Text="Điều Kiện" ClientEnabled="false">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="Bước 1: Chọn yếu tố"
                                    Height="350" View="GroupBox" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxPanel runat="server" Height="250" Width="100%" ScrollBars="Vertical">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxNavBar ID="ASPxNavBar4" runat="server" Width="100%">
                                                            <Groups>
                                                                <dx:NavBarGroup Expanded="false" Text="Nhà sản xuất">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox17" runat="server">
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
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox19" runat="server">
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
                                                                            <dx:ASPxLabel ID="ASPxLabel46" runat="server" Style="color: Red" Text="Hàng hóa">
                                                                            </dx:ASPxLabel>
                                                                            <br />
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox24" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel47" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel48" runat="server" Style="color: Red" Text="Nhóm hàng hóa">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </dx:NavBarGroup>
                                                                <dx:NavBarGroup Expanded="False" Text="Hình thức thanh toán">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox29" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Áp dụng cho hình thức">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel58" runat="server" Style="color: Red" Text="Thanh toán">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </dx:NavBarGroup>
                                                                <dx:NavBarGroup Expanded="False" Text="Doanh số">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox29" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Áp dụng cho doanh số theo">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel58" runat="server" Style="color: Red" Text="Hàng hóa">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Áp dụng cho doanh số theo">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Style="color: Red" Text="Nhóm hàng hóa">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox2" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Áp dụng cho doanh số theo">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Style="color: Red" Text="Phiếu bán">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox3" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Áp dụng cho">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Style="color: Red" Text="Tổng doanh số">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </dx:NavBarGroup>
                                                            </Groups>
                                                        </dx:ASPxNavBar>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
                                            <dx:ASPxRoundPanel runat="server" Width="100%" HeaderText="Bước 2: Chọn dữ liệu yếu tố"
                                                View="GroupBox">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxPanel runat="server" Width="100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server" >
                                                                    <dx:ASPxLabel ID="lbl_loaitrue1" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxHyperLink Style="color: Blue" ID="hplink1" runat="server" NavigateUrl=""
                                                                        Text="Nhà sản xuất">
                                                                        <ClientSideEvents Click="show_settingManufacturersList" />
                                                                    </dx:ASPxHyperLink>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="lbl_loaitru2" runat="server" Text="Và">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="lbl_loaitru3" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxHyperLink Style="color: Blue" ID="hplink2" runat="server" NavigateUrl=""
                                                                        Text="Nhà cung cấp">
                                                                        <ClientSideEvents Click="show_settingSuppliersList" />
                                                                    </dx:ASPxHyperLink>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="Và">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Áp dụng cho khách hàng đạt điều kiện doanh số theo">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink1" runat="server" NavigateUrl=""
                                                                        Text="Hàng hóa">
                                                                        <ClientSideEvents Click="show_uSettingSalesTotalByProduct" />
                                                                    </dx:ASPxHyperLink>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Và">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="Áp dụng cho khách hàng đạt điều kiện doanh số theo">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink2" runat="server" NavigateUrl=""
                                                                        Text="Nhóm hàng hóa">
                                                                        <ClientSideEvents Click="show_uSettingSaleTotalByProductGrp" />
                                                                    </dx:ASPxHyperLink>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Và">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Áp dụng cho khách hàng đạt điều kiện doanh số theo">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink5" runat="server" NavigateUrl=""
                                                                        Text="Phiếu bán">
                                                                        <ClientSideEvents Click="show_settingSalesTotalByValueOfOrder" />
                                                                    </dx:ASPxHyperLink>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Và">
                                                                    </dx:ASPxLabel>
                                                                    <br />
                                                                    <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="Áp dụng cho khách hàng đạt tổng">
                                                                    </dx:ASPxLabel>
                                                                    <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink6" runat="server" NavigateUrl=""
                                                                        Text="Doanh số">
                                                                        <ClientSideEvents Click="show_settingSalesTotal" />
                                                                    </dx:ASPxHyperLink>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxPanel>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxRoundPanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Loại Trừ" ClientEnabled="False">
                        <ContentCollection>
                            <dx:ContentControl runat="server">
                                <dx:ASPxRoundPanel runat="server" HeaderText="Bước 1: Chọn yếu tố"
                                    Height="50%" View="GroupBox" Width="100%">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxPanel runat="server" Height="250" ScrollBars="Auto" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent4" runat="server">
                                                        <dx:ASPxNavBar ID="Navbar_exception" runat="server" Width="100%">
                                                            <Groups>
                                                                <dx:NavBarGroup Expanded="false" Text="Nhà sản xuất">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox17" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel34" runat="server" Style="color: Red" Text="Nhà sản xuất">
                                                                            </dx:ASPxLabel>
                                                                            <br />
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox18" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="Loại Trừ hàng hóa">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel36" runat="server" Style="color: Red" Text="Nhóm nhà sản xuất">
                                                                            </dx:ASPxLabel>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </dx:NavBarGroup>
                                                                <dx:NavBarGroup Expanded="False" Text="Nhà Cung Cấp">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox19" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel38" runat="server" Style="color: Red" Text="Nhà cung cấp">
                                                                            </dx:ASPxLabel>
                                                                            <br />
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox20" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Loại trừ hàng hóa">
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
                                                                            <dx:ASPxLabel ID="ASPxLabel42" runat="server" Style="color: Red" Text="Khách hàng">
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
                                                                <dx:NavBarGroup Expanded="False" Text="Hình thức thanh toán">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <dx:ASPxCheckBox ID="ASPxCheckBox29" runat="server">
                                                                            </dx:ASPxCheckBox>
                                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" Text="Loại trừ">
                                                                            </dx:ASPxLabel>
                                                                            <dx:ASPxLabel ID="ASPxLabel58" runat="server" Style="color: Red" Text="Hình thức thanh toán">
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
                                <dx:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" Width="100%" HeaderText="Bước 2: Chọn dữ liệu yếu tố"
                                    View="GroupBox">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:ASPxPanel runat="server" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent9" runat="server" >
                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink3" runat="server" NavigateUrl=""
                                                            Text="nhà sản xuất">
                                                            <ClientSideEvents Click="show_settingManufacturersList" />
                                                        </dx:ASPxHyperLink>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Và">
                                                        </dx:ASPxLabel>
                                                        <br />
                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxHyperLink Style="color: Blue" ID="ASPxHyperLink4" runat="server" NavigateUrl=""
                                                            Text="nhà cung cấp">
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
                    <dx:TabPage Text="Quyền Lợi" ClientEnabled="False">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxPanel runat="server" Width="100%" Height="350px" ScrollBars="Vertical">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <dx:ASPxCheckBox ID="cbchietkhau" runat="server" CheckState="Checked" Text="Chiết Khấu"
                                                ClientInstanceName="cbchietkhau" Checked="True">
                                            </dx:ASPxCheckBox>
                                            <dx:ASPxRoundPanel ID="ASPxRoundPanel7" runat="server" ShowHeader="false" View="GroupBox"
                                                Width="100%" ClientInstanceName="roundpanelchietkhau">
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent6" runat="server" SupportsDisabledAttribute="True">
                                                        <table class="form">
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxRadioButton ID="ASPxRadioButton1" runat="server" GroupName="type_select"
                                                                        Text="Tiền giảm theo phiếu" Width="100%" Wrap="False">
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td style="width: 109px;">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxRadioButton ID="ASPxRadioButton2" runat="server" GroupName="type_select"
                                                                        Text="Tỉ lệ chiết khấu  theo phiếu(%)" Width="100%">
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td style="width: 109px;">
                                                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxRadioButton ID="ASPxRadioButton3" runat="server" GroupName="type_select"
                                                                        Text="Chiết khấu trên từng hàng hóa" Width="100%">
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td style="width: 109px;">
                                                                    <dx:ASPxGridView ID="grid_hanghoakhuyenmai" runat="server" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <dx:GridViewDataTextColumn Caption="STT" FieldName="sequenceno" ShowInCustomizationForm="True"
                                                                                VisibleIndex="0" Visible="false">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                                                VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                                                VisibleIndex="2">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="productunitid" ShowInCustomizationForm="True"
                                                                                VisibleIndex="3">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="Chiết khấu (%)" FieldName="discountrate" ShowInCustomizationForm="True"
                                                                                VisibleIndex="4">
                                                                                <DataItemTemplate>
                                                                                    <dx:ASPxTextBox ID="ASPxTextBox8" runat="server" Text='<%# Bind("discountrate") %>'
                                                                                        Width="50px">
                                                                                    </dx:ASPxTextBox>
                                                                                </DataItemTemplate>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewBandColumn Caption="Điều kiện tặng hàng" ShowInCustomizationForm="True"
                                                                                VisibleIndex="5" Visible="false">
                                                                                <Columns>
                                                                                    <dx:GridViewDataTextColumn Caption="Mua" FieldName="condition_buy" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="0">
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Text='<%# Bind("condition_buy") %>'
                                                                                                Width="50px">
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                        <DataItemTemplate>
                                                                                            ->
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="0">
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Text='<%# Bind("condition_give") %>'
                                                                                                Width="50px">
                                                                                            </dx:ASPxTextBox>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                </Columns>
                                                                            </dx:GridViewBandColumn>
                                                                        </Columns>
                                                                    </dx:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                            <dx:ASPxCheckBox ID="cbquatang" runat="server" CheckState="Checked" Text="Quà Tặng"
                                                ClientInstanceName="cbquatang" Checked="True">
                                            </dx:ASPxCheckBox>
                                            <dx:ASPxRoundPanel runat="server" View="GroupBox" Width="100%"
                                                ClientInstanceName="roundpanelquatang" ShowHeader="false">
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent7" runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ASPxGridView Caption="Danh mục tặng phẩm theo là hàng hóa" ID="gridview_hanghoatang"
                                                            runat="server" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="STT" FieldName="sequenceno" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="productid" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="productname" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="productunitid" ShowInCustomizationForm="True"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lotid" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewBandColumn Caption="Điều kiện tặng hàng" ShowInCustomizationForm="True"
                                                                    VisibleIndex="5">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="Mua" FieldName="condition_buy" ShowInCustomizationForm="True"
                                                                            VisibleIndex="0">
                                                                            <DataItemTemplate>
                                                                                <dx:ASPxTextBox ID="ASPxTextBox6" runat="server" Text='<%# Bind("condition_buy") %>'
                                                                                    Width="50px">
                                                                                </dx:ASPxTextBox>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="1">
                                                                            <DataItemTemplate>
                                                                                ->
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Tặng" FieldName="condition_give" ShowInCustomizationForm="True"
                                                                            VisibleIndex="2">
                                                                            <DataItemTemplate>
                                                                                <dx:ASPxTextBox ID="ASPxTextBox7" runat="server" Text='<%# Bind("condition_give") %>'
                                                                                    Width="50px">
                                                                                </dx:ASPxTextBox>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                </dx:GridViewBandColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                        <br />
                                                        <dx:ASPxGridView Caption="Danh mục tặng phẩm khác" ID="grv_bonusProduct" runat="server"
                                                            AutoGenerateColumns="False" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="Tên Quà Tặng" FieldName="TenQuaTang" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Giá Trị" FieldName="GiaTri" ShowInCustomizationForm="True"
                                                                    VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                                                    VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Caption="Thành tiền" FieldName="ThanhTien"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MoTa" ShowInCustomizationForm="True"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" VisibleIndex="5"
                                                                    Caption="Thao Tác" Width="100px">
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
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="wizardCreaterPromotionLevel_tabchanged" />
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <dx:ASPxPanel runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server">
                        <div style="float: left; margin-right: 4px">
                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="btnFinish" clientinstancename="btnFinish" runat="server" Text="Lưu mức khuyến mãi"
                                clientvisible="false">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Finished" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right; margin-left: 4px">
                            <dx:ASPxButton ID="btnNext" clientinstancename="btnNext" runat="server" Text="Tiếp" AutoPostBack="false">
                                <ClientSideEvents Click="OnNextClick" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Forward" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="float: right">
                            <dx:ASPxButton ID="btnBack" clientinstancename="btnBack" runat="server" Text="Lùi Lại" AutoPostBack="False"
                                clientvisible="false">
                                <ClientSideEvents Click="OnBackClick" />
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