<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="InputWarehousing.aspx.cs" Inherits="DXApplication1.Purchasing.WarehouseTesting" %>

<%@ Register Src="UserControl/DetailInputCommWahoure.ascx" TagName="DetailInputCommWahoure"
    TagPrefix="uc1" %>
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

        function grdData_custombtn_click(s, e) {
            grdData_verifyProduct.PerformCallback(e.visibleIndex);
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
    <style type="text/css">
        .edInline
        {
            display: inline-table;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Nhập kho" Font-Bold="True" Font-Size="Medium"
            Height="35px">
        </dx:ASPxLabel>
    </div>
    <div style="margin-bottom: 10px;">
        <dx:ASPxPageControl ID="pctrl" ClientInstanceName="pctrl" runat="server" ActiveTabIndex="2"
            RenderMode="Classic" Width="100%" Height="100%" 
            onactivetabchanged="pctrl_ActiveTabChanged">
            <ClientSideEvents ActiveTabChanged="TabChange" />
            <TabPages>
                <dx:TabPage Text="Danh sách phiếu nhập kho" ClientEnabled="True" Name="Personal">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxGridView ID="grdData0" runat="server" AutoGenerateColumns="False" Width="100%"
                                    OnInitNewRow="grdData0_InitNewRow" OnRowInserting="grdData0_RowInserting" OnStartRowEditing="grdData0_StartRowEditing">
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback" />
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Chi tiết" ShowInCustomizationForm="True" VisibleIndex="9"
                                            ButtonType="Image">
                                            <EditButton Text="Chi tiết phiếu" Visible="True">
                                                <Image ToolTip="Chi tiết phiếu">
                                                    <SpriteProperties CssClass="Sprite_Document" />
                                                </Image>
                                            </EditButton>
                                            <NewButton Text="Chi tiết nhập kho">
                                            </NewButton>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="status" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Nhập vào kho" FieldName="warehouse" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ngày tạo phiếu" FieldName="date" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Người tạo phiếu" FieldName="name" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewCommandColumn Caption="" ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                            VisibleIndex="0">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox runat="server" ID="checkall">
                                                </dx:ASPxCheckBox>
                                            </HeaderTemplate>
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                </dx:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage ClientEnabled="False" Name="Confirmation" Text="Xếp hàng hóa vào kho">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" Theme="Default"
                                    ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="Ngày giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người nhận hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="ASPxComboBox2" runat="server">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ghi chú" ColSpan="3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="50px" Width="100%">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                                <asp:XmlDataSource ID="XmlDataSource1" runat="server"></asp:XmlDataSource>
                                <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="430px">
                                    <Panes>
                                        <dx:SplitterPane AutoHeight="True" Size="200px">
                                            <ContentCollection>
                                                <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <strong>Vị trí<br />
                                                    </strong>
                                                    <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" KeyFieldName="OrganizationId"
                                                        ParentFieldName="ParentOrganizationId">
                                                        <Columns>
                                                            <dx:TreeListTextColumn Caption="Kho" FieldName="name" ShowInCustomizationForm="True"
                                                                VisibleIndex="0">
                                                            </dx:TreeListTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedNode="True" />
                                                    </dx:ASPxTreeList>
                                                </dx:SplitterContentControl>
                                            </ContentCollection>
                                        </dx:SplitterPane>
                                        <dx:SplitterPane AutoHeight="True">
                                            <ContentCollection>
                                                <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                                    <strong>Danh sách mặt hàng nhập kho<br />
                                                    </strong>
                                                    <dx:ASPxGridView ID="grdDataAccept" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <dx:GridViewCommandColumn Caption="Thao tác" ShowInCustomizationForm="True" VisibleIndex="10"
                                                                ButtonType="Image">
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
                                                            <dx:GridViewCommandColumn Caption="" ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                                VisibleIndex="0">
                                                                <HeaderTemplate>
                                                                    <dx:ASPxCheckBox runat="server" ID="checkall_3">
                                                                    </dx:ASPxCheckBox>
                                                                </HeaderTemplate>
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" ShowInCustomizationForm="True"
                                                                VisibleIndex="7">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Hạn sử dụng" FieldName="date" ShowInCustomizationForm="True"
                                                                VisibleIndex="8">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption=" SL xếp kho" FieldName="sortamount" ShowInCustomizationForm="True"
                                                                VisibleIndex="6">
                                                                <DataItemTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" 
                                                                        Text='<%# Bind("sortamount") %>' Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                                                VisibleIndex="5">
                                                                <DataItemTemplate>
                                                                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" 
                                                                        Text='<%# Bind("realamount") %>' Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="PopupEditForm" />
                                                        <Settings ShowFilterRow="True" />
                                                        <SettingsPopup>
                                                            <EditForm Height="150px" Width="600px" />
                                                        </SettingsPopup>
                                                    </dx:ASPxGridView>
                                                    <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" EnableTheming="True" Theme="Default">
                                                        <Items>
                                                            <dx:LayoutItem Caption=" ">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                        SupportsDisabledAttribute="True">
                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </dx:SplitterContentControl>
                                            </ContentCollection>
                                        </dx:SplitterPane>
                                    </Panes>
                                </dx:ASPxSplitter>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControlFinish" runat="server">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" EnableTheming="True" Theme="Default"
                                    ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="Ngày giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px" Enabled="False">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người giao hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px" Enabled="False">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người nhận hàng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px" Enabled="False">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ghi chú" ColSpan="3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="ASPxMemo2" runat="server" Height="50px" Width="100%" Enabled="False">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:ASPxFormLayout>
                                <dx:ASPxGridView ID="ASPxGridView1" Width="100%" runat="server" AutoGenerateColumns="False">
                                    <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã số" ShowInCustomizationForm="True" VisibleIndex="0"
                                            FieldName="code">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Đơn vị tính" ShowInCustomizationForm="True" VisibleIndex="2"
                                            FieldName="unit">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="recieptamount" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <%--<dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Caption="Số Lô" FieldName="lot" ShowInCustomizationForm="True"
                                            VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Hạn sử dụng" FieldName="date" ShowInCustomizationForm="True"
                                            VisibleIndex="5">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Vị trí kho" FieldName="position" ShowInCustomizationForm="True"
                                            VisibleIndex="6">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" ShowInCustomizationForm="True"
                                            VisibleIndex="7">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="description" ShowInCustomizationForm="True"
                                            VisibleIndex="8">
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
    <div class="buttonsArea" style="width: 100%; height: 35px;">
        <dx:ASPxButton ID="btnRepeat" ClientInstanceName="btnRepeat" runat="server" CssClass="float-right button-right-margin"
            HorizontalAlign="Center" UseSubmitBehavior="False" ClientVisible="false" Text="Tiếp tục nhập kho"
            Wrap="False">
            <ClientSideEvents Click="repeat" />
            <Image>
                <SpriteProperties CssClass="Sprite_Repeat" />
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" AutoPostBack="false"
            ClientVisible="false" UseSubmitBehavior="false" Text="Hoàn tất" CssClass="float-right button-right-margin">
            <ClientSideEvents Click="finish" />
            <Image>
                <SpriteProperties CssClass="Sprite_Finished" />
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" AutoPostBack="false"
            ClientVisible="true" CausesValidation="false" UseSubmitBehavior="true" Text="Tiếp theo"
            CssClass="float-right button-right-margin">
            <ClientSideEvents Click="next" />
            <Image>
                <SpriteProperties CssClass="Sprite_Forward" />
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" AutoPostBack="false"
            ClientVisible="false" CssClass="float-right button-right-margin" CausesValidation="false"
            UseSubmitBehavior="False" Text="Trở về">
            <ClientSideEvents Click="back" />
            <Image>
                <SpriteProperties CssClass="Sprite_Backward" />
            </Image>
        </dx:ASPxButton>
    </div>
    <uc1:DetailInputCommWahoure ID="DetailInputCommWahoure" runat="server" />
</asp:Content>
