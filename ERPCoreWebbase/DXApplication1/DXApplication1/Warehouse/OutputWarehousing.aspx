<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="OutputWarehousing.aspx.cs" Inherits="WebModule.Purchasing.OutputWarehousing" %>

<%@ Register Src="UserControl/uDetailOutComm.ascx" TagName="uDeatilOutComm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function cpHeader_EndCallback(s, e) {
        }

        function cpLine_EndCallback(s, e) {
            if (s.cpCodeExisting) {

            }

        }
        // MainForm Event
        function grdData_EndCallback(s, e) {
            if (s.cpEdit) {
                formSupplierGroupEdit.Show();

                ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
                cboRowStatus.SetValue('A');

                if (s.cpEdit == 'edit') {
                    cpLine.PerformCallback('edit');
                }
                else {
                    cpLine.PerformCallback('new');
                }

                delete (cpEdit);
                return;
            }
        }

        function grdData_CustomButtonClick(s, e) {
            s.GetRowValues(e.visibleIndex, 'ProductId', OnGetRowValues);
        }

        function OnGetRowValues(values) {
            formSupplierGroupEdit.Show();
            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

            var str = 'view|' + values;
            cpLine.PerformCallback(str);
        }

        // EditForm Event 
        function buttonSave_Click(s, e) {
            if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
                return;
            }

            if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').GetValue() == null) {
                ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').Validate();
                return;
            }

            cpLine.PerformCallback('save');
            cpHeader.PerformCallback('refresh');

        }

        function buttonCancel_Click(s, e) {
            formSupplierGroupEdit.Hide();
        }
        var index = 0;
        function TabChange() {
            index = pctrl.GetActiveTabIndex();
            updateBut();
        }
        function next() {
            pctrl.SetActiveTab(pctrl.GetTab(++index));
        }
        function back() {
            pctrl.SetActiveTab(pctrl.GetTab(--index));
        }
        function finish() {
            pctrl.SetActiveTab(pctrl.GetTab(index = 0));
        }
        function repeat() {
            pctrl.SetActiveTab(pctrl.GetTab(index = 0));
        }
        function updateBut() {
            btnNext.SetVisible(index < 3);
            btnBack.SetVisible(index == 1 || index == 2);
            btnFinish.SetVisible(index == 3);
            btnRepeat.SetVisible(index == 7);
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Xuất Kho" Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <div style="margin-bottom: 10px;">
        <dx:ASPxPageControl ID="pc" ClientInstanceName="pctrl" runat="server" ActiveTabIndex="1"
            RenderMode="Classic" Width="100%" Height="100%">
            <ClientSideEvents ActiveTabChanged="TabChange" />
            <TabPages>
                <dx:TabPage Text="Danh sách phiếu xuất kho" ClientEnabled="True" Name="Personal">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxGridView ID="grdData0" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnInitNewRow="grdData0_InitNewRow" OnStartRowEditing="grdData0_StartRowEditing">
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback" />
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="9"
                                            ButtonType="Image">
                                            <EditButton Text="Chi tiết phiếu" Visible="True">
                                                <Image ToolTip="Chi tiết phiếu">
                                                    <SpriteProperties CssClass="Sprite_Document" />
                                                </Image>
                                            </EditButton>
                                            <NewButton Text="Chi tiết xuất kho">
                                            </NewButton>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Xuất từ kho" FieldName="warehouse" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="status" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày tạo phiếu" FieldName="date" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                            Caption="" VisibleIndex="0">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox runat="server" ID="checkall_1">
                                                </dx:ASPxCheckBox>
                                            </HeaderTemplate>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Người tạo phiếu" FieldName="name" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                </dx:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Name="Payment" ClientEnabled="False" Text="Chọn vị trí hàng hóa xuất kho">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" Theme="Default"
                                    ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="Ngày giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người nhận hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ghi chú" ColSpan="3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="50px" Width="100%">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                                <div style="margin-bottom: 10px;">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Chọn hàng hóa xuất kho" Font-Bold="True"
                                        Font-Size="Small">
                                    </dx:ASPxLabel>
                                </div>
                                <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                                    OnCallback="cpHeader_Callback" HideContentOnCallback="True">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="grdData" Width="100%" runat="server" AutoGenerateColumns="False">
                                                <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                        VisibleIndex="0">
                                                        <ClearFilterButton Visible="True">
                                                        </ClearFilterButton>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server">
                                                            </dx:ASPxCheckBox>
                                                        </HeaderTemplate>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                        VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã số" ShowInCustomizationForm="True" VisibleIndex="1"
                                                        FieldName="code">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="amount" ShowInCustomizationForm="True" Caption="SL xuất theo CT"
                                                        VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="amount" ShowInCustomizationForm="True" Caption="SL xuất thực tế"
                                                        VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lô" ShowInCustomizationForm="True" VisibleIndex="6"
                                                        FieldName="lot">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Hạn sử dụng" ShowInCustomizationForm="True" VisibleIndex="7"
                                                        FieldName="date">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                        VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" ShowInCustomizationForm="True"
                                                        VisibleIndex="8" Width="200px">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowFilterRow="True" />
                                                <SettingsDetail ShowDetailRow="True" />
                                                <Templates>
                                                    <DetailRow>
                                                        <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
                                                            Width="100%">
                                                            <TabPages>
                                                                <dx:TabPage Text="Vị trí">
                                                                    <ContentCollection>
                                                                        <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxGridView ID="grdDataDetail" runat="server" AutoGenerateColumns="False" OnBeforePerformDataSelect="grdDataDetail_BeforePerformDataSelect"
                                                                                Width="100%">
                                                                                <ClientSideEvents SelectionChanged="function(s, e) {
		var key = s.GetRowKey(e.visibleIndex);
        alert(s.GetSelectedRowCount());
    
}" />
                                                                                <Columns>
                                                                                    <%--<dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="4"
                                                                                        ButtonType="Image">
                                                                                        <EditButton Visible="True" Text="Sửa SL xuất thực tế">
                                                                                            <Image ToolTip="Sửa SL xuất thực tế">
                                                                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                                                            </Image>
                                                                                        </EditButton>
                                                                                    </dx:GridViewCommandColumn>--%>
                                                                                    <dx:GridViewDataTextColumn Caption="Vị trí" FieldName="location" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="0">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="SL có trong kho" FieldName="amountinw" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="1">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="SL xuất thực tế" FieldName="realoutamount" ShowInCustomizationForm="True"
                                                                                        VisibleIndex="2">
                                                                                        <DataItemTemplate>
                                                                                            <dx:ASPxSpinEdit runat="server" ID="sltt">
                                                                                            </dx:ASPxSpinEdit>
                                                                                        </DataItemTemplate>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="SL còn lại trong kho" FieldName="amountremain"
                                                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                </Columns>
                                                                                <SettingsEditing Mode="PopupEditForm" />
                                                                                <SettingsPopup>
                                                                                    <EditForm Height="150px" Width="500px" />
                                                                                </SettingsPopup>
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
           
                <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControlFinish" runat="server">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" EnableTheming="True" Theme="Default"
                                    ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="Ngày giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Enabled="False">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px" Enabled="False">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người nhận hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px" Enabled="False">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ghi chú" ColSpan="3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="ASPxMemo2" runat="server" Height="50px" Width="100%" Enabled="False">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="codedepence" ShowInCustomizationForm="True"
                                            Caption="Thuộc chứng từ" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                            VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="description" ShowInCustomizationForm="True"
                                            VisibleIndex="8">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="amountdiff" ShowInCustomizationForm="True"
                                            VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="amountreal" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="amount" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                </dx:ASPxGridView>
                            </div>
                        </dx:ContentControl>
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

    <uc1:uDeatilOutComm ID="uDeatilOutComm" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SubmitContainer" runat="server">
    <div class="float-right button-left-margin">
        <!-- Places button here -->
        <dx:ASPxButton ID="btnRepeat" ClientInstanceName="btnRepeat" runat="server" CssClass="float-right button-right-margin"
            HorizontalAlign="Center" UseSubmitBehavior="False" ClientVisible="false" Text="Tiếp tục nhập kho"
            Wrap="False">
            <ClientSideEvents Click="repeat" />
            <Image>
                <SpriteProperties CssClass="Sprite_Repeat" />
            </Image>
        </dx:ASPxButton>

        <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
            ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất">
            <ClientSideEvents Click="finish" />
            <Image>
                <SpriteProperties CssClass="Sprite_Finished" />
            </Image>
        </dx:ASPxButton>
    </div>

    <div class="float-right button-left-margin">
        <!-- Places button here -->
        <dx:ASPxButton ID="ASPxButton2" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
            CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo">
            <ClientSideEvents Click="next" />
            <Image>
                <SpriteProperties CssClass="Sprite_Forward" />
            </Image>
        </dx:ASPxButton>
    </div>

    <div class="float-right">
        <!-- Places button here -->
        <dx:ASPxButton ID="ASPxButton3" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
            ClientVisible="false" CausesValidation="false" UseSubmitBehavior="False" Text="Trở về">
            <ClientSideEvents Click="back" />
            <Image>
                <SpriteProperties CssClass="Sprite_Backward" />
            </Image>
        </dx:ASPxButton>
    </div>
</asp:Content>