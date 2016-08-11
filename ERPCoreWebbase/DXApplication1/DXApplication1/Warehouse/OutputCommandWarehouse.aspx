<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="OutputCommandWarehouse.aspx.cs" Inherits="WebModule.Purchasing.Warehousing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Tạo Phiếu Xuất Kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
<div style="margin-bottom:10px;">
    <dx:ASPxPageControl ID="pc" ClientInstanceName="pc" runat="server" ActiveTabIndex="2" 
        RenderMode="Lightweight" Width="100%" Height="100%">
        <TabPages>
            <dx:TabPage Name="Personal" Text="Chọn chứng từ">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                     <div style="overflow:auto; height:450px">
                    <div style="margin-bottom:10px;">
                  <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" EnableTheming="True" 
                      Theme="Default">
                      <Items>
                          <dx:LayoutItem Caption="Chọn chứng từ">
                              <LayoutItemNestedControlCollection>
                                  <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" 
                                      SupportsDisabledAttribute="True">
                                      <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="Phiếu bán hàng" Value="C" />
                            <dx:ListEditItem Text="Chuyển kho nội bộ" Value="Chuyển kho nội bộ" />
                            <dx:ListEditItem Text="Hàng trả về" Value="Hàng trả về" />
                            <dx:ListEditItem Text="Hàng ký gửi" Value="Hàng ký gửi" />
                        </Items>
                        </dx:ASPxComboBox>
                                  </dx:LayoutItemNestedControlContainer>
                              </LayoutItemNestedControlCollection>
                          </dx:LayoutItem>
                      </Items>
                  </dx:ASPxFormLayout>
                  
</div>
<dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grdData" Width="100%" runat="server" AutoGenerateColumns="False">
                <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" />
                <Columns>
                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" 
                        ShowSelectCheckbox="True" VisibleIndex="0">
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Mã số" ShowInCustomizationForm="True" 
                        VisibleIndex="1" FieldName="code">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Ngày bán hàng" ShowInCustomizationForm="True" 
                        VisibleIndex="3" FieldName="date">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Khách hàng" FieldName="suppliername" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" 
                        ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFilterRow="True" />
                <SettingsDetail ShowDetailRow="True" />
                <Templates>
                    <DetailRow>
                        <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" 
                            RenderMode="Lightweight">
                            <TabPages>
                                <dx:TabPage Text="Danh sách hàng hóa">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="grdDataDetail" runat="server" AutoGenerateColumns="False" 
                                                OnBeforePerformDataSelect="grdDataDetail_BeforePerformDataSelect">
                                                <ClientSideEvents SelectionChanged="function(s, e) {
		var key = s.GetRowKey(e.visibleIndex);
        //alert(s.GetSelectedRowCount());
    
}" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Mã" FieldName="code" 
                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn Caption="#" ShowInCustomizationForm="True" 
                                                        ShowSelectCheckbox="True" VisibleIndex="0">
                                                        <ClearFilterButton Visible="True">
                                                        </ClearFilterButton>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" 
                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" 
                                                        ShowInCustomizationForm="True" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" 
                                                        ShowInCustomizationForm="True" VisibleIndex="5">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="description" 
                                                        ShowInCustomizationForm="True" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </DetailRow>
                </Templates>

            </dx:ASPxGridView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
</div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage ClientEnabled="False" Name="Confirmation" 
                Text="Xác nhận phiếu xuất kho">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                    <div style="overflow:auto; height:450px">
                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" 
                            Theme="Aqua">
                            <Items>
                                <dx:LayoutItem Caption="Người tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ngày tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Mã số">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            
                                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    <div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Danh sách mặt hàng xuất kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
                        <dx:ASPxGridView ID="grdDataAccept" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                    ShowInCustomizationForm="True" VisibleIndex="6">
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
<dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True" Caption="Thuộc chứng từ" 
                                    VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" 
                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" 
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" 
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" 
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" 
                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                <ContentCollection>
                            <dx:ContentControl ID="ContentControlFinish" runat="server">
                             <div style="overflow:auto; height:450px">
                            <p>Phiếu xuất kho đã được tạo.</p>
                            <div style="margin-bottom:10px;">
                        <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" EnableTheming="True" 
                            Theme="Aqua">
                            <Items>
                                <dx:LayoutItem Caption="Người tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Enabled="False" 
                                                Text="Nhân viên 1" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ngày tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" Enabled="False" 
                                                Text="27-07-2013" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Mã số">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Text="MS0001" Width="170px" 
                                                Enabled="False">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                        <br />
    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Danh sách mặt hàng xuất kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                    ShowInCustomizationForm="True" VisibleIndex="6">
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
<dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True" Caption="Thuộc chứng từ" 
                                    VisibleIndex="5"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" 
                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" 
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" 
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" 
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="amount" 
                                    ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                                  <dx:ASPxButton ID="btnGoToSchedule" runat="server" 
                                         CssClass="finishAreaButton" HorizontalAlign="Center" 
                                     UseSubmitBehavior="False" Text="Tiếp tục tạo phiếu xuất kho">
                                    </dx:ASPxButton>

                            </div>                            </dx:ContentControl>
                        </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
     <dx:ASPxPanel ID="dxpError" ClientInstanceName="dxpError" runat="server" CssClass="errorPanel"
                ClientVisible="false">
                <PanelCollection>
                    <dx:PanelContent>
                        Please complete or correct the fields highlighted in red.
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
            <dx:ASPxHiddenField ID="hfRegInfo" ClientInstanceName="hfRegInfo" runat="server"
                SyncWithServer="true" ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
</div>
<div class="buttonsArea">
                <div class="buttons">
                    <table cellpadding="0" cellspacing="0" border="0" class="buttonsTable">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" 
                                    AutoPostBack="false" ClientVisible="false" CausesValidation="false" 
                                    UseSubmitBehavior="False" Text="Trở về" BackColor="Blue" ForeColor="White">
                                    <ClientSideEvents Click="OnBackButtonClick" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" 
                                    AutoPostBack="false" CausesValidation="false" UseSubmitBehavior="true" 
                                    Text="Tiếp theo" BackColor="Blue" ForeColor="White">
                                    <ClientSideEvents Click="OnNextButtonClick" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" 
                                    AutoPostBack="false" ClientVisible="false" UseSubmitBehavior="false" 
                                    Text="Hoàn tất" BackColor="Blue" ForeColor="White">
                                    <ClientSideEvents Click="OnFinishButtonClick"/>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
</asp:Content>

