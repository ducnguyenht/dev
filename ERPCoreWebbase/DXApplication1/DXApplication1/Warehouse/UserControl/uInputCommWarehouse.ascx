<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uInputCommWarehouse.ascx.cs"
    ClientIDMode="AutoID" Inherits="WebModule.Warehouse.UserControl.uInputCommWarehouse" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
       <style type = "text/css">
        .float-right
        {
            float:right;
        }
        .float-left
        {
            float:left;
        }
    </style>
<dx:ASPxPopupControl runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    Modal="True" AllowDragging="True" AllowResize="True" ClientInstanceName="formEntryDetail"
    HeaderText="Phiếu nhập kho" ShowMaximizeButton="True" ShowFooter="True" ShowSizeGrip="False"
    Width="950px" Height="400px" ID="formEntryDetail">
    <ClientSideEvents Shown="formEntryDetail_Shown" />
    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
    <FooterContentTemplate>
        <dx:ASPxButton ID="btnHelp" ClientInstanceName="btnHelp" AutoPostBack="False" runat="server"
            CssClass="float-right dl mg" Text="Trợ giúp" UseSubmitBehavior="False"
            Visible="False">
            <Image>
                <SpriteProperties CssClass="Sprite_Help"></SpriteProperties>
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="buttonInpuCommExit" ClientInstanceName="buttonInpuCommExit" CssClass="float-right dl mg"
            runat="server" AutoPostBack="False" UseSubmitBehavior="False" Text="Thoát">
            <ClientSideEvents Click="buttonInpuCommExit_Click" />
            <Image>
                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" CssClass="float-right dl mg"
            runat="server" AutoPostBack="False" ClientVisible="False" UseSubmitBehavior="False"
            Text="Hoàn tất" Visible="False">
            <Image>
                <SpriteProperties CssClass="Sprite_Finished"></SpriteProperties>
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnRepeat" ClientInstanceName="btnRepeat" AutoPostBack="False"
            runat="server" CssClass="afinishAreaButton float-right button-right-margin" HorizontalAlign="Center"
            Text="Tiếp tục tạo phiếu nhập kho" UseSubmitBehavior="False" Visible="False">
            <Image>
                <SpriteProperties CssClass="Sprite_Repeat"></SpriteProperties>
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" CssClass="float-right button-right-margin"
            runat="server" AutoPostBack="False" CausesValidation="False" UseSubmitBehavior="False"
            Text="Tiếp theo" Visible="False">
            <Image>
                <SpriteProperties CssClass="Sprite_Forward"></SpriteProperties>
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" CssClass="float-right button-right-margin"
            runat="server" AutoPostBack="False" ClientVisible="False" CausesValidation="False"
            UseSubmitBehavior="False" Text="Trở về" Visible="False">
            <Image>
                <SpriteProperties CssClass="Sprite_Backward"></SpriteProperties>
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="buttonInpuCommPrint" ClientInstanceName="buttonInpuCommPrint"
            AutoPostBack="False" runat="server" CssClass="float-right dl mg"
            Text="In Phiếu" UseSubmitBehavior="False" Visible="true">
             <Image>
                <SpriteProperties CssClass="Sprite_Print" />
            </Image>
            <ClientSideEvents Click="buttonInpuCommPrint_Click" />
        </dx:ASPxButton>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxCallbackPanel ID="cpInpuCommLine" runat="server" Width="100%" ClientInstanceName="cpInpuCommLine"
                OnCallback="cpInpuCommLine_Callback">
                <ClientSideEvents EndCallback="cpInpuCommLine_EndCallback" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent12" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxPageControl ID="pc" runat="server" ActiveTabIndex="0" ClientInstanceName="pctrl"
                            Height="100%" Width="100%">
                            <TabPages>
                                <dx:TabPage ClientEnabled="False" Name="Finish" Text="">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                            <p>
                                                Thông tin phiếu nhập kho được tạo.</p>
                                            <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" ColCount="2">
                                                <Items>
                                                    <dx:LayoutItem Caption="Mã phiếu nhập" ColSpan="2">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtInputCommWarehouseCode" runat="server" ClientInstanceName="txtInputCommWarehouseCode"
                                                                    Width="170px">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Ngày tạo" ColSpan="2">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="txtInputCommWarehouseCreateDate" runat="server" ClientInstanceName="txtInputCommWarehouseCreateDate">
                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Diễn giải" ColSpan="2">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxTextBox ID="txtInputCommWarehouseDescription" runat="server" ClientInstanceName="txtInputCommWarehouseDescription"
                                                                    Width="400px">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:ASPxFormLayout>
                                            <br />
                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Small" Text="Danh sách mặt hàng nhập kho">
                                            </dx:ASPxLabel>
                                            <dx:ASPxGridView ID="grdInputCommLine" runat="server" AutoGenerateColumns="False"
                                                ClientInstanceName="grdInputCommLine" DataSourceID="BillItemXDS" KeyFieldName="BillItemId"
                                                Width="100%" 
                                                OnCellEditorInitialize="grdInputCommLine_CellEditorInitialize" 
                                                OnRowUpdating="grdInputCommLine_RowUpdating" 
                                                OnRowValidating="grdInputCommLine_RowValidating" 
                                                OnStartRowEditing="grdInputCommLine_StartRowEditing" 
                                                OnCustomColumnDisplayText="grdInputCommLine_CustomColumnDisplayText" 
                                                OnCustomErrorText="grdInputCommLine_CustomErrorText" 
                                                OnParseValue="grdInputCommLine_ParseValue">
                                                <ClientSideEvents EndCallback="cpInpuCommLine_EndCallback" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn FieldName="Quantity" ShowInCustomizationForm="True" VisibleIndex="7"
                                                        Caption="Số lượng" Width="50px" ReadOnly="True">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataComboBoxColumn Caption="Số lô" FieldName="LotId!Key" 
                                                        ShowInCustomizationForm="True" VisibleIndex="9" Width="100px">
                                                        <PropertiesComboBox DropDownStyle="DropDown" EnableCallbackMode="True" 
                                                            IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{0}" 
                                                            ValueField="LotId" ValueType="System.Guid" 
                                                            OnItemRequestedByValue="cboLot_ItemRequestedByValue"
                                                            OnItemsRequestedByFilterCondition="cboLot_ItemsRequestedByFilterCondition">
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="Số lô" FieldName="Code" Width="150px" />
                                                                <dx:ListBoxColumn Caption="Hạn sử dụng" FieldName="ExpireDate" Width="200px" />
                                                            </Columns>
                                                        </PropertiesComboBox>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ItemUnitId.ItemId.Code" ShowInCustomizationForm="True"
                                                        VisibleIndex="0" Caption="Mã hàng hóa" Width="100px" ReadOnly="True">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ItemUnitId.UnitId.Name" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Caption="Đvt" Width="60px" ReadOnly="True">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ItemUnitId.ItemId.Name" ShowInCustomizationForm="True"
                                                        VisibleIndex="1" Caption="Tên hàng hóa" Width="170px" ReadOnly="True">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                        ShowInCustomizationForm="True" VisibleIndex="12" Width="70px">
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
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lượng thực tế" 
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="14" 
                                                        Width="120px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ItemUnitId!Key" 
                                                        ShowInCustomizationForm="True" VisibleIndex="16" Width="0px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn Caption="Hạn dùng" FieldName="LotId.ExpireDate" 
                                                        ShowInCustomizationForm="True" VisibleIndex="11" Width="100px">
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn Caption="Nhà sản xuất" 
                                                        FieldName="ItemUnitId.ItemId.ManufacturerOrgId.Name" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="200px">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                                    ColumnResizeMode="NextColumn" />
                                                <SettingsPager ShowEmptyDataRows="True">
                                                </SettingsPager>
                                                <SettingsEditing Mode="Inline" />
                                                <Settings HorizontalScrollBarMode="Auto" ShowFilterRowMenu="True"
                                                    ShowHeaderFilterButton="True" />
                                            </dx:ASPxGridView>
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
<dx:ASPxCallbackPanel ID="cpInpuCommReport" runat="server" ClientInstanceName="cpInpuCommReport"
    OnCallback="cpInpuCommReport_Callback" Width="100%">
    <ClientSideEvents EndCallback="cpInpuCommReport_EndCallback" />
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="formInpuCommReport" runat="server" AllowDragging="True"
                AllowResize="True" ClientInstanceName="formInpuCommReport" CloseAction="CloseButton"
                DragElement="Window" HeaderText="" Maximized="True" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="800px">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" ClientInstanceName="PrintLayoutSplitter"
                            FullscreenMode="True" Height="100%" Orientation="Vertical" SeparatorVisible="False"
                            Width="100%">
                            <Panes>
                                <dx:SplitterPane MinSize="20px" Name="ToolbarPane" Size="40px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ReportToolbar ID="tlbInpuCommViewer" runat="server" ClientInstanceName="tlbInpuCommViewer"
                                                ReportViewerID="rptInpuCommViewer" ShowDefaultButtons="False">
                                                <Items>
                                                    <dx:ReportToolbarButton ItemKind="Search" />
                                                    <dx:ReportToolbarSeparator />
                                                    <dx:ReportToolbarButton ItemKind="PrintReport" />
                                                    <dx:ReportToolbarButton ItemKind="PrintPage" />
                                                    <dx:ReportToolbarSeparator />
                                                    <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                                    <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                                    <dx:ReportToolbarLabel ItemKind="PageLabel" />
                                                    <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                                    </dx:ReportToolbarComboBox>
                                                    <dx:ReportToolbarLabel ItemKind="OfLabel" />
                                                    <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                                    <dx:ReportToolbarButton ItemKind="NextPage" />
                                                    <dx:ReportToolbarButton ItemKind="LastPage" />
                                                    <dx:ReportToolbarSeparator />
                                                    <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                                    <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                                    <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                                        <Elements>
                                                            <dx:ListElement Value="pdf" />
                                                            <dx:ListElement Value="xls" />
                                                            <dx:ListElement Value="xlsx" />
                                                            <dx:ListElement Value="rtf" />
                                                            <dx:ListElement Value="mht" />
                                                            <dx:ListElement Value="html" />
                                                            <dx:ListElement Value="txt" />
                                                            <dx:ListElement Value="csv" />
                                                            <dx:ListElement Value="png" />
                                                        </Elements>
                                                    </dx:ReportToolbarComboBox>
                                                </Items>
                                                <Styles>
                                                    <LabelStyle>
                                                        <Margins MarginLeft="3px" MarginRight="3px" />
                                                    </LabelStyle>
                                                </Styles>
                                            </dx:ReportToolbar>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane ScrollBars="Auto">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <dx:ReportViewer ID="rptInpuCommViewer" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" ClientInstanceName="rptInpuCommViewer">
                                                <Border BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                            </dx:ReportViewer>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                            <Styles>
                                <Pane HorizontalAlign="Center">
                                    <Paddings Padding="0px" />
                                    <Border BorderWidth="0px" />
                                </Pane>
                            </Styles>
                        </dx:ASPxSplitter>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:XpoDataSource ID="BillItemXDS" runat="server" TypeName="NAS.DAL.Invoice.BillItem"
    Criteria="[BillId] = ?">
    <CriteriaParameters>
        <asp:Parameter Name="BillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
<dx:ASPxHiddenField ID="hInputCommId" runat="server" ClientInstanceName="hInputCommId">
</dx:ASPxHiddenField>
<dx:ASPxHiddenField ID="hInputCommPrint" runat="server" ClientInstanceName="hInputCommPrint">
</dx:ASPxHiddenField>


<dx:ASPxGridView ID="grdBooking" runat="server" AutoGenerateColumns="False">
    <Columns>
        <dx:GridViewDataTextColumn FieldName="Dc" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Account" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Amount" VisibleIndex="2">
            <PropertiesTextEdit DisplayFormatString="#,#">
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsPager Mode="ShowAllRecords">
    </SettingsPager>
    <Settings ShowColumnHeaders="False" />
    <Styles>
        <Cell>
            <Border BorderStyle="None" />
        </Cell>
    </Styles>
</dx:ASPxGridView>
<dx:ASPxGridViewExporter ID="gvDataExporter" runat="server" 
    GridViewID="grdBooking">
</dx:ASPxGridViewExporter>




