<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uPricePolicyEditting.ascx.cs" Inherits="WebModule.Sales.PricePolicy.uPricePolicyEditting" %>
<%@ Register src="PredicatorSubForm/uBuildingMenuConstruction.ascx" tagname="uBuildingMenuConstruction" tagprefix="uc1" %>
<%@ Register src="~/UserControls/uEvaluantCalculator.ascx" tagname="uEvaluantCalculator" tagprefix="uc2" %>
<script type="text/javascript">
    function popupPricePolicyEditting_CloseButtonClick(s, e) {
        cpPricePolicyEditting.PerformCallback('Close');
    }

    function cpPricePolicyEditting_EndCallback(s, e) {
        if (s.cpIsSaved) {
            delete s.cpIsSaved;
            grdPricePolicy.Refresh();
            alert('Đã cập nhật thông tin chính sách giá');
        }
        LoadingPanelPricePolicy.Hide();
    }

    function pcWizardCreaterPricePolicy_TabClick(s, e) {
        if (!s.GetTabByName('Common').GetEnabled())
            return;
        if (!s.GetTabByName('Condition').GetEnabled())
            return;
        if (!s.GetTabByName('Exception').GetEnabled())
            return;
        if (!s.GetTabByName('ConfigFormula').GetEnabled())
            return;
        if (!s.GetTabByName('PriceReference').GetEnabled())
            return;

        switch (e.tab.name) {
            case 'Common':
                cpPricePolicyEditting.PerformCallback('EdittingClickCommon');
                break;
            case 'Condition':
                cpPricePolicyEditting.PerformCallback('EdittingClickCondition');
                break;
            case 'Exception':
                cpPricePolicyEditting.PerformCallback('EdittingClickException');
                break;
            case 'ConfigFormula':
                cpPricePolicyEditting.PerformCallback('EdittingClickConfigFormula');
                break;
            case 'PriceReference':
                cpPricePolicyEditting.PerformCallback('EdittingClickPriceReference');
                break;
        }
    }
</script>
<dx:ASPxCallbackPanel ID="cpPricePolicyEditting" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    ClientInstanceName="cpPricePolicyEditting" OnCallback="cpPricePolicyEditting_Callback" Width="100%">
    <ClientSideEvents EndCallback="cpPricePolicyEditting_EndCallback" BeginCallback="function(s, e){LoadingPanelPricePolicy.Show();}"/>
<ClientSideEvents BeginCallback="function(s, e){LoadingPanelPricePolicy.Show();}" 
        EndCallback="cpPricePolicyEditting_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:aspxpopupcontrol id="popupPricePolicyEditting" 
                clientinstancename="popupPricePolicyEditting" runat="server" headertext="Hướng dẫn tạo điều kiện giá bán"
                width="1000px" height="600px" closeaction="CloseButton" modal="True" popuphorizontalalign="WindowCenter" 
                popupverticalalign="WindowCenter" allowresize="True" ShowFooter="true" ScrollBars="Auto"
                ShowMaximizeButton="true"
                Maximized="false" ShowSizeGrip="False"
                allowdragging="True">
                <ClientSideEvents CloseButtonClick="popupPricePolicyEditting_CloseButtonClick" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" >
                        <dx:ASPxPageControl ID="pcWizardCreaterPricePolicy" ClientInstanceName="pcWizardCreaterPricePolicy"
                            runat="server" RenderMode="Classic"
                            ActiveTabIndex="4" Height="100%" Width="100%">
                            <ClientSideEvents TabClick="pcWizardCreaterPricePolicy_TabClick" />
                            <TabPages>
                                <dx:TabPage Name="Common" Text="Thông tin chung">
                                    <ContentCollection>
                                        <dx:ContentControl>
                                            <dx:ASPxFormLayout ID="form_commoninfo" runat="server" Width="100%">
                                                <Items>
                                                    <dx:LayoutItem Caption="Mã phương pháp" FieldName="Code" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtPricePolicyCode" runat="server" MaxLength="36"
                                                                    Width="170px">
                                                                    <ValidationSettings ErrorText="" ValidationGroup="groupCommon">
                                                                        <RequiredField ErrorText="Chưa nhập mã phương pháp" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tên phương pháp" FieldName="Name" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtPricePolicyName" runat="server" Width="170px" MaxLength="255">
                                                                    <ValidationSettings ErrorText="" ValidationGroup="groupCommon">
                                                                        <RequiredField ErrorText="Chưa nhập tên phương pháp" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Ngày bắt đầu" FieldName="ValidFrom" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="txtValidFrom" runat="server" ValidationGroup="groupCommon">
                                                                    <ValidationSettings ErrorText="">
                                                                        <RequiredField ErrorText="Chưa nhập ngày bắt đầu" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Ngày kết thúc" FieldName="ValidTo" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="txtValidTo" runat="server">
                                                                    <ValidationSettings ErrorText="" ValidationGroup="groupCommon">
                                                                        <RequiredField ErrorText="Chưa nhập ngày kết thúc" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Phân loại" FieldName="PricePolicyTypeId" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="cboPricePolicyType" runat="server" 
                                                                    DataSourceID="PricePolicyTypeLstXDS" ValueField="PricePolicyTypeId" TextField="Name">
                                                                    <ValidationSettings ErrorText="" ValidationGroup="groupCommon">
                                                                        <RequiredField ErrorText="Chưa chọn phân loại" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                </dx:ASPxComboBox>
                                                                <dx:XpoDataSource ID="PricePolicyTypeLstXDS" runat="server" 
                                                                    Criteria="[RowStatus] &gt; 0" 
                                                                    TypeName="NAS.DAL.Sales.Price.PricePolicyType" DefaultSorting="">
                                                                </dx:XpoDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Trạng thái" FieldName="RowStatus" RequiredMarkDisplayMode="Required">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxComboBox ID="cboRowStatus" runat="server">
                                                                    <ValidationSettings ErrorText="" ValidationGroup="groupCommon">
                                                                        <RequiredField ErrorText="Chưa chọn trạng thái" IsRequired="True" />
                                                                    </ValidationSettings>
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Sử dụng" Value="1"/>
                                                                        <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                                                                    </Items>
                                                                </dx:ASPxComboBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Mô tả" FieldName="Description">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxMemo ID="txtDescription" Width="100%" Height="150px" runat="server">
                                                                </dx:ASPxMemo>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="Condition" Text="Điều Kiện">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:ASPxRoundPanel runat="server" HeaderText="Bước 1: Chọn yếu tố"
                                                Height="350" View="GroupBox" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxPanel runat="server" Height="250" Width="100%" ScrollBars="Vertical">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxNavBar ID="navBarCondition" runat="server" Width="100%">
                                                                        <Groups>
                                                                            <dx:NavBarGroup Expanded="true" Text="Nhà sản xuất">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <dx:ASPxCheckBox ID="chkApplyManufacturer" runat="server" Cursor="pointer">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e)
                                                                                            {
                                                                                                cpPricePolicyEditting.PerformCallback('UpdateMenu');
                                                                                            }" />
                                                                                        </dx:ASPxCheckBox>
                                                                                        <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                                        </dx:ASPxLabel>
                                                                                        <dx:ASPxLabel ID="ASPxLabel34" runat="server" Style="color: Red" Text="Nhà sản xuất">
                                                                                        </dx:ASPxLabel>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </dx:NavBarGroup>
                                                                            <dx:NavBarGroup Expanded="true" Text="Nhà Cung Cấp">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <dx:ASPxCheckBox ID="chkApplySupplier" runat="server" Cursor="pointer">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e)
                                                                                            {
                                                                                                cpPricePolicyEditting.PerformCallback('UpdateMenu');
                                                                                            }" />
                                                                                        </dx:ASPxCheckBox>
                                                                                        <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Áp dụng cho hàng hóa thuộc">
                                                                                        </dx:ASPxLabel>
                                                                                        <dx:ASPxLabel ID="ASPxLabel38" runat="server" Style="color: Red" Text="Nhà cung cấp">
                                                                                        </dx:ASPxLabel>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </dx:NavBarGroup>
                                                                            <dx:NavBarGroup Expanded="true" Text="Hàng Hóa">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <dx:ASPxCheckBox ID="chkApplyItemUnit" runat="server" Cursor="pointer">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e)
                                                                                            {
                                                                                                cpPricePolicyEditting.PerformCallback('UpdateMenu');
                                                                                            }" />
                                                                                        </dx:ASPxCheckBox>
                                                                                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Áp dụng cho">
                                                                                        </dx:ASPxLabel>
                                                                                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Style="color: Red" Text="Hàng hóa">
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
                                            <dx:ASPxRoundPanel runat="server" Width="100%" HeaderText="Bước 2: Chọn dữ liệu yếu tố"
                                                View="GroupBox">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxPanel runat="server" Width="100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server" >
                                                                    <uc1:uBuildingMenuConstruction ID="uBuildingMenuConstruction1" runat="server" />
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxPanel>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="Exception" Text="Loại Trừ">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <dx:ASPxRoundPanel runat="server" HeaderText="Bước 1: Chọn yếu tố loại trừ"
                                                Height="350" View="GroupBox" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxPanel runat="server" Height="250" Width="100%" ScrollBars="Vertical">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxNavBar ID="navBarException" runat="server" Width="100%">
                                                                        <Groups>
                                                                            <dx:NavBarGroup Expanded="true" Text="Nhà sản xuất">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <dx:ASPxCheckBox ID="chkApplyManufacturerException" runat="server" Cursor="pointer">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e)
                                                                                            {
                                                                                                cpPricePolicyEditting.PerformCallback('UpdateExceptionMenu');
                                                                                            }" />
                                                                                        </dx:ASPxCheckBox>
                                                                                        <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                                        </dx:ASPxLabel>
                                                                                        <dx:ASPxLabel ID="ASPxLabel34" runat="server" Style="color: Red" Text="Nhà sản xuất">
                                                                                        </dx:ASPxLabel>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </dx:NavBarGroup>
                                                                            <dx:NavBarGroup Expanded="true" Text="Nhà Cung Cấp">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <dx:ASPxCheckBox ID="chkApplySupplierException" runat="server" Cursor="pointer">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e)
                                                                                            {
                                                                                                cpPricePolicyEditting.PerformCallback('UpdateExceptionMenu');
                                                                                            }" />
                                                                                        </dx:ASPxCheckBox>
                                                                                        <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="Loại trừ hàng hóa thuộc">
                                                                                        </dx:ASPxLabel>
                                                                                        <dx:ASPxLabel ID="ASPxLabel38" runat="server" Style="color: Red" Text="Nhà cung cấp">
                                                                                        </dx:ASPxLabel>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </dx:NavBarGroup>
                                                                            <dx:NavBarGroup Expanded="true" Text="Hàng Hóa">
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <dx:ASPxCheckBox ID="chkApplyItemUnitException" runat="server" Cursor="pointer">
                                                                                            <ClientSideEvents CheckedChanged="function(s, e)
                                                                                            {
                                                                                                cpPricePolicyEditting.PerformCallback('UpdateExceptionMenu');
                                                                                            }" />
                                                                                        </dx:ASPxCheckBox>
                                                                                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Loại trừ hàng hóa">
                                                                                        </dx:ASPxLabel>
                                                                                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Style="color: Red" Text="Hàng hóa">
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
                                            <dx:ASPxRoundPanel runat="server" Width="100%" HeaderText="Bước 2: Chọn dữ liệu yếu tố"
                                                View="GroupBox">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxPanel runat="server" Width="100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server" >
                                                                    <uc1:uBuildingMenuConstruction ID="uBuildingMenuConstruction2" runat="server" />
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxPanel>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxRoundPanel>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="ConfigFormula" Text="Cấu hình giá">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <uc2:uEvaluantCalculator ID="uEvaluantCalculator1" runat="server" />
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Name="PriceReference" Text="Tham khảo giá bán">
	                                <ContentCollection>
		                                <dx:ContentControl ID="ContentControl_thamkhaogia" runat="server">
                                            <dx:ASPxSplitter ID="splilterReport" ClientInstanceName="splilterReport" 
                                                runat="server" Height="600px" Width="100%" ResizingMode="Postponed" 
                                                Orientation="Vertical" AllowResize="False" SeparatorSize="1px" 
                                                SeparatorVisible="False" ShowSeparatorImage="False" >
                                                <Panes>
                                                    <dx:SplitterPane Name="TopSplitterPane" Size="240px" MaxSize="240px" MinSize="240px">
                                                        <ContentCollection>
                                                            <dx:SplitterContentControl>
                                                                Chọn các mặt hàng để kiểm tra chính sách:
			                                                    <dx:ASPxGridView ID="grdItemUnitListSelectionTesting" ClientInstanceName="grdItemUnitListSelectionTesting" runat="server" AutoGenerateColumns="False"
                                                                    Width="100%"
                                                                    KeyFieldName="COGSId">
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn Caption="Chọn" ShowInCustomizationForm="True" 
                                                                            ShowSelectCheckbox="True" VisibleIndex="0">
                                                                            <FilterTemplate>
                                                                                <center>
                                                                                    <dx:ASPxCheckBox ID="chkCOGSPriceAll" runat="server" AutoPostBack="false">
                                                                                        <ClientSideEvents CheckedChanged="function(s, e){
                                                                                            if (s.GetChecked())
                                                                                                grdItemUnitListSelectionTesting.SelectRows();
                                                                                            else
                                                                                                grdItemUnitListSelectionTesting.UnselectRows();
                                                                                        }" />
                                                                                    </dx:ASPxCheckBox>
                                                                                </center>
                                                                            </FilterTemplate>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Mã hàng hóa" 
                                                                            FieldName="ItemUnitId.ItemId.Code" ShowInCustomizationForm="True"
                                                                            VisibleIndex="1">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Tên hàng hóa" 
                                                                            FieldName="ItemUnitId.ItemId.Name" ShowInCustomizationForm="True"
                                                                            VisibleIndex="2">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" 
                                                                            FieldName="ItemUnitId.UnitId.Code" ShowInCustomizationForm="True"
                                                                            VisibleIndex="3">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Kho" FieldName="InventoryId.Name" ShowInCustomizationForm="True"
                                                                            VisibleIndex="4">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Ngày cập nhật giá" FieldName="InventoryTransactionId.IssueDate" ShowInCustomizationForm="True"
                                                                            VisibleIndex="5" Width="155px">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Giá vốn" FieldName="COGSPrice" ShowInCustomizationForm="True"
                                                                            VisibleIndex="6" Width="130px">
                                                                            <PropertiesTextEdit DisplayFormatString="{0:c0}">
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="COGSId" ShowInCustomizationForm="True"
                                                                            Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                    <SettingsPager PageSize="15">
                                                                    </SettingsPager>
                                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                                        ShowFilterRowMenuLikeItem="True" VerticalScrollableHeight="300" />
                                                                </dx:ASPxGridView>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                    <dx:SplitterPane Name="MiddleSplitterPane" Size="50px" MaxSize="50px" MinSize="50px">
                                                        <ContentCollection>
                                                            <dx:SplitterContentControl>
                                                                <center>
                                                                    <dx:ASPxButton ID="buttonView" ClientInstanceName="buttonView" runat="server" AutoPostBack="False" Text="Tham khảo giá" CausesValidation="false">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Help" />
                                                                        </Image>
                                                                        <ClientSideEvents Click="function(s, e){
                                                                            grdItemUnitListReviewPolicy.PerformCallback('Refresh');
                                                                        }" />
                                                                    </dx:ASPxButton>
                                                                </center>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                    <dx:SplitterPane Name="BottomSplitterPane" Size="240px">
                                                        <ContentCollection>
                                                            <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                                Thông tin giá tham khảo của các mặt hàng đã chọn:
			                                                    <dx:ASPxGridView ID="grdItemUnitListReviewPolicy" 
                                                                    ClientInstanceName="grdItemUnitListReviewPolicy" runat="server" AutoGenerateColumns="False"
                                                                    Width="100%"
                                                                    KeyFieldName="COGSId"
                                                                    OnCustomCallback="grdItemUnitListReviewPolicy_CustomCallback" 
                                                                    OnInit="grdItemUnitListReviewPolicy_Init" 
                                                                    OnCustomUnboundColumnData="grdItemUnitListReviewPolicy_CustomUnboundColumnData">
                                                                    <Columns>
                                                                    </Columns>
                                                                    <SettingsBehavior AllowFocusedRow="True"/>
                                                                    <SettingsPager PageSize="15" ShowEmptyDataRows="false">
                                                                    </SettingsPager>
                                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                                        ShowFilterRowMenuLikeItem="True"/>
                                                                </dx:ASPxGridView>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                </Panes>
                                                <border borderstyle="None" borderwidth="1px"></border>
                                            </dx:ASPxSplitter>
		                                </dx:ContentControl>
	                                </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                        <asp:PlaceHolder runat="server" ID="PlaceHolderSubForm" />
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <FooterContentTemplate>
                    <div id="Footer" style="display: inline; width: 100%;">
                        <div style="display: inline; float: left">
                            <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" Text="Trợ Giúp" CausesValidation="false">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonFinish" clientinstancename="buttonFinish" runat="server" Text="Lưu chính sách giá" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){
                                        cpPricePolicyEditting.PerformCallback('Save');
                                    }" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Finished" />
                            </Image>
                        </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonNext" clientinstancename="buttonNext" runat="server" Text="Tiếp" AutoPostBack="false" CausesValidation="true">
                                <ClientSideEvents Click="function(s, e){
                                    var tabName = (pcWizardCreaterPricePolicy.GetActiveTab()).name;
                                    var validated = ASPxClientEdit.ValidateGroup('group' + tabName);
                                    if (!validated){
                                        return;
                                    }
                                    cpPricePolicyEditting.PerformCallback('Next');
                                }" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Forward" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonBack" clientinstancename="buttonBack" runat="server" Text="Lùi Lại" AutoPostBack="False" CausesValidation="false">
                                <ClientSideEvents Click="function(s, e){
                                    cpPricePolicyEditting.PerformCallback('Back');
                                }" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Backward" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonSave" clientinstancename="buttonSave" runat="server" Text="Lưu lại" AutoPostBack="False" CausesValidation="false">
                                <ClientSideEvents Click="function(s, e){
                                    var idx =  pcWizardCreaterPricePolicy.GetActiveTabIndex();
                                    if (idx == 0)
                                        cpPricePolicyEditting.PerformCallback('SaveCommonInfo');
                                    else
                                        cpPricePolicyEditting.PerformCallback('SaveConfigPrice');
                                }" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </div>
                </FooterContentTemplate>
                <ModalBackgroundStyle BackColor="White" Opacity="0"></ModalBackgroundStyle>
            </dx:aspxpopupcontrol>
            <dx:ASPxLoadingPanel ID="LoadingPanelPricePolicy" runat="server" ClientInstanceName="LoadingPanelPricePolicy"
                Modal="True" ShowImage="true" Text="Đang xử lý">
                <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
                </dx:ASPxLoadingPanel>
            </dx:PanelContent>
        </PanelCollection>
</dx:ASPxCallbackPanel>