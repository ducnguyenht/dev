<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uProductionCommand.ascx.cs"
    Inherits="WebModule.Produce.UserControl.uProductionCommand" %>
<style type="text/css">
    .float_right
    {
        float: right;
    }
    .dxpc-footerContent
    {
        width: 95%;
    }
</style>
<dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Font-Bold="True" Font-Size="Medium"
    Height="55px" Text="Danh sách phiếu sản xuất" Width="300px">
    <Border BorderStyle="None" />
</dx:ASPxTextBox>
<dx:ASPxGridView ID="GrdCommand" runat="server" AutoGenerateColumns="False"
    Width="100%">
    <ClientSideEvents CustomButtonClick="function(s, e) {
	if(e.buttonID == &quot;New&quot;)
		{
			popcc.Show();
		}
}" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã phiếu sản xuất" FieldName="CID" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Mã công đoạn" FieldName="ID" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên công đoạn" FieldName="Name" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ngày" FieldName="Date" VisibleIndex="3">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="4">
            <DeleteButton Visible="True">
                <Image>
                    <SpriteProperties CssClass="Sprite_Delete" />
                </Image>
            </DeleteButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="New" Text="Tạo mới">
                    <Image>
                        <SpriteProperties CssClass="Sprite_New" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
    </Columns>
    <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
    <Settings VerticalScrollableHeight="600" />
    <SettingsDetail ShowDetailRow="True" />
    <Styles>
        <Header HorizontalAlign="Center">
        </Header>
    </Styles>
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="GridDetail_1" runat="server" AutoGenerateColumns="False" 
                Width="100%" onbeforeperformdataselect="GridDetail_1_BeforePerformDataSelect" >
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Từ giờ" FieldName="Start" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đến giờ" FieldName="End" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mã sản phẩm" FieldName="ID" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên sản phẩm" FieldName="Name" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="No" VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Quantity" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="Unit" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption=" " VisibleIndex="8">
                        <EditButton Visible="True">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Edit" />
                            </Image>
                        </EditButton>
                        <CancelButton>
                            <Image>
                                <SpriteProperties CssClass="Sprite_Cancel" />
                            </Image>
                        </CancelButton>
                        <UpdateButton>
                            <Image>
                                <SpriteProperties CssClass="Sprite_Apply" />
                            </Image>
                        </UpdateButton>
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>
                </Columns>
                <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
    <Settings VerticalScrollableHeight="300" />
                <SettingsEditing Mode="Inline" />
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>
<dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="popcc"
    AllowDragging="true" DisappearAfter="200" HeaderText="Tạo phiếu sản xuất" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" RenderMode="Classic" Width="850px" ShowFooter="true"
    AllowResize="true" ScrollBars="Auto" Height="600px" Modal="True">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pctrl" ClientInstanceName="pctrl" runat="server" ActiveTabIndex="1"
                Font-Bold="False" RenderMode="Classic" Width="100%" Height="100%">
                <ClientSideEvents ActiveTabChanged = "function(s,e){
                                                                    var ctab = pctrl.GetTab(pctrl.GetActiveTabIndex());
                                                                    var tabName = ctab.name;
                                                                    btnBack.SetVisible(tabName != 'Personal' && tabName != 'Finish');                                                                
                                                                    btnNext.SetVisible(tabName == 'Personal');                                                                
                                                                    btnExit.SetVisible(tabName == 'Finish');                                                                
                                                                    btnFinish.SetVisible(tabName == 'Confirmation'); 
                                                                   }" />
                <TabPages>
                    <dx:TabPage Text="Chọn công đoạn" Name="Personal">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Font-Bold="True" Font-Size="Small"
                                    Height="40px" Text="Chọn công đoạn sản xuất" Width="200px">
                                    <Border BorderStyle="None" />
                                    <Border BorderStyle="None"></Border>
                                </dx:ASPxTextBox>
                                <dx:ASPxGridView ID="GridCD" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã công đoạn" FieldName="ID" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên công đoạn" FieldName="Name" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày" FieldName="Date" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="PStatus" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                            VisibleIndex="0">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <SettingsPager Mode="EndlessPaging" PageSize="30" RenderMode="Classic">
                                    </SettingsPager>
                                    <Settings VerticalScrollableHeight="350" />
                                    <SettingsDetail ShowDetailRow="True" />
                                    <Styles>
                                        <Header HorizontalAlign="Center">
                                        </Header>
                                    </Styles>
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="GridCD_detail" runat="server" AutoGenerateColumns="False" 
                                                Width="100%" onbeforeperformdataselect="GridCD_detail_BeforePerformDataSelect">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Từ giờ" FieldName="Start" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đến giờ" FieldName="End" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã sản phẩm" FieldName="ID" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên sản phẩm" FieldName="Name" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="No" VisibleIndex="5">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Quantity" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="Unit" VisibleIndex="7">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="8">
                                                        <EditButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                            </Image>
                                                        </EditButton>
                                                        <ClearFilterButton Visible="True">
                                                        </ClearFilterButton>
                                                    </dx:GridViewCommandColumn>
                                                </Columns>
                                                <SettingsPager Mode="EndlessPaging" PageSize="30" RenderMode="Classic">
                                    </SettingsPager>
                                    <Settings VerticalScrollableHeight="200" />
                                                <Styles>
                                                    <Header HorizontalAlign="Center">
                                                    </Header>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </DetailRow>
                                    </Templates>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Phiếu sản xuất" Name="Confirmation" ClientEnabled="False">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="ASPxFormLayout6" runat="server" ColCount="2" Width="100%">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã phiếu" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout6_E1" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày tạo phiếu" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="ASPxFormLayout6_E3" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người tạo phiếu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxFormLayout6_E4" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mã công đoạn" ShowCaption="True">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout6_E2" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên công đoạn">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxFormLayout6_E5" runat="server" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ColSpan="2" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="Gridcm_detail" runat="server" AutoGenerateColumns="False" Width="100%">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Từ giờ" FieldName="Start" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đến giờ" FieldName="End" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã sản phẩm" FieldName="ID" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên sản phẩm" FieldName="Name" ShowInCustomizationForm="True"
                                                                VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="No" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Quantity" ShowInCustomizationForm="True"
                                                                VisibleIndex="6">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="Unit" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="8">
                                                                <EditButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                    </Image>
                                                                </EditButton>
                                                                <CancelButton>
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                    </Image>
                                                                </CancelButton>
                                                                <UpdateButton>
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                    </Image>
                                                                </UpdateButton>
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <SettingsPager Mode="EndlessPaging" PageSize="30">
    </SettingsPager>
    <Settings VerticalScrollableHeight="600" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <Styles>
                                                            <Header HorizontalAlign="Center">
                                                            </Header>
                                                        </Styles>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Finish" Text="Hoàn tất" ClientEnabled="False">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Font-Bold="True" Font-Size="XX-Large"
                                    Height="43px" Width="485px">Tạo phiếu thành công</asp:TextBox>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <FooterContentTemplate>
        <table style="width: 100%">
            <tr>
                <td>
                    <dx:ASPxButton ID="btHelp" ClientInstanceName="btnHelp" runat="server" AutoPostBack="false"
                        ClientVisible="true" CausesValidation="false" UseSubmitBehavior="False" Text="Trợ Giúp"
                        CssClass="float-left">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Help" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnFinish" CssClass="float-right button-right-margin" ClientInstanceName="btnFinish"
                        runat="server" AutoPostBack="false" ClientVisible="false" UseSubmitBehavior="false"
                        Text="Hoàn tất">
                        <ClientSideEvents Click="function(s, e){
                                                                var ctab = pctrl.GetTab(pctrl.GetActiveTabIndex());
                                                                var ntab = pctrl.GetTab(pctrl.GetActiveTabIndex()+1);
                                                                var tabName = ntab.name;
                                                                pctrl.SetActiveTab(ntab);
                                                                btnBack.SetVisible(tabName != 'Personal' && tabName != 'Finish');                                                                
                                                                btnNext.SetVisible(tabName == 'Personal');                                                                
                                                                btnExit.SetVisible(tabName == 'Finish');                                                                
                                                                btnFinish.SetVisible(tabName == 'Confirmation');                                                                
                                                             }" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Finished" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnBack" CssClass="float-right button-right-margin" ClientInstanceName="btnBack"
                        runat="server" AutoPostBack="False" ClientVisible="False" CausesValidation="False"
                        UseSubmitBehavior="False" Text="Trở về">
                        <ClientSideEvents Click="function(s, e){
                                                                var ctab = pctrl.GetTab(pctrl.GetActiveTabIndex());
                                                                var btab = pctrl.GetTab(pctrl.GetActiveTabIndex()-1);
                                                                var tabName = btab.name;
                                                                pctrl.SetActiveTab(btab);
                                                                btnBack.SetVisible(tabName != 'Personal' && tabName != 'Finish');                                                                
                                                                btnNext.SetVisible(tabName == 'Personal');                                                                
                                                                btnFinish.SetVisible(tabName == 'Confirmation');                                                                                                                                
                                                                btnExit.SetVisible(tabName == 'Finish');
                                                             }" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Backward" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnNext" CssClass="float-right button-right-margin" ClientInstanceName="btnNext"
                        runat="server" AutoPostBack="false" CausesValidation="false" UseSubmitBehavior="true"
                        Text="Tiếp theo">
                        <ClientSideEvents Click="function(s, e){
                                                                var ctab = pctrl.GetTab(pctrl.GetActiveTabIndex());
                                                                var ntab = pctrl.GetTab(pctrl.GetActiveTabIndex()+1);
                                                                var tabName = ntab.name;
                                                                pctrl.SetActiveTab(ntab);
                                                                btnBack.SetVisible(tabName != 'Personal' && tabName != 'Finish');                                                                
                                                                btnNext.SetVisible(tabName == 'Personal');                                                                
                                                                btnFinish.SetVisible(tabName == 'Confirmation');                                                                
                                                                btnExit.SetVisible(tabName == 'Finish');                                                                
                                                             }" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Forward" />
                        </Image>
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnExit" CssClass="float-right button-right-margin" ClientInstanceName="btnExit"
                        runat="server" AutoPostBack="false" CausesValidation="false" ClientVisible="False"
                        UseSubmitBehavior="true" Text="Thoát">
                        <ClientSideEvents Click="function(s, e){
                                                                pctrl.SetActiveTab(pctrl.GetTab(0));
                                                                popcc.Hide();
                                                                btnBack.SetVisible(false);                                                                
                                                                btnNext.SetVisible(true);
                                                                btnFinish.SetVisible(false);                                                                
                                                                btnExit.SetVisible(false);                                                                 
                                                             }" />
                        <Image>
                            <SpriteProperties CssClass="Sprite_Cancel" />
                        </Image>
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
    </FooterContentTemplate>
    <ClientSideEvents AfterResizing="function(s,e) { ASPxClientControl.AdjustControls(); }"  />
</dx:ASPxPopupControl>
