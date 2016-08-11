<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uOutCommWarehouse.ascx.cs"
    ClientIDMode="AutoID" Inherits="WebModule.Warehouse.UserControl.uOutCommWarehouse" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
            <dx:ASPxHiddenField runat="server" 
    ClientInstanceName="hOutputCommId" ID="hOutputCommId"></dx:ASPxHiddenField>

            <dx:ASPxHiddenField runat="server" 
    ClientInstanceName="hOutputCommPrint" ID="hOutputCommPrint"></dx:ASPxHiddenField>

            <dx:ASPxCallbackPanel ID="cpOutputCommReport" runat="server" 
    ClientInstanceName="cpOutputCommReport" 
    oncallback="cpOutputCommReport_Callback" Width="100%">
                <ClientSideEvents EndCallback="cpOutputCommReport_EndCallback" />
                <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formOutCommViewer" runat="server" AllowDragging="True" 
        AllowResize="True" ClientInstanceName="formOutCommViewer" 
        CloseAction="CloseButton" DragElement="Window" EnableViewState="False" 
        HeaderText="" Height="560px" PopupAnimationType="None" 
        Width="800px" Maximized="True">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <div style="height: 100%; width: 100%; overflow: hidden">
                    <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" 
                        ClientInstanceName="PrintLayoutSplitter" FullscreenMode="True" Height="100%" 
                        Orientation="Vertical" SeparatorVisible="False" Width="100%">
                        <Panes>
                            <dx:SplitterPane MinSize="20px" Name="ToolbarPane" Size="40px">
                                <ContentCollection>
                                    <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                        <dx:ReportToolbar ID="ReportToolbar1" runat="server" 
                                            ReportViewerID="rptOutputCommViewer" ShowDefaultButtons="False">
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
                                        <dx:ReportViewer ID="rptOutputCommViewer" runat="server" BorderColor="Black" 
                                            BorderStyle="Solid" BorderWidth="1px" ClientInstanceName="rptOutputCommViewer">
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
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
                    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>

            <dx:XpoDataSource runat="server" DefaultSorting="" 
    TypeName="NAS.DAL.Invoice.BillItem" ID="BillItemXDS" 
    Criteria="[BillId] = ?">
                <CriteriaParameters>
                    <asp:Parameter Name="BillId" />
                </CriteriaParameters>
</dx:XpoDataSource>

            <dx:ASPxPopupControl runat="server" 
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
    Modal="True" AllowDragging="True" AllowResize="True" 
    ClientInstanceName="formEntryDetail" HeaderText="Phiếu xuất kho" 
    ShowMaximizeButton="True" ShowFooter="True" ShowSizeGrip="False" Width="910px" 
    Height="400px" ID="formEntryDetail" CloseAction="CloseButton">
                <ClientSideEvents Shown="formEntryDetail_Shown" />
<ClientSideEvents Shown="formEntryDetail_Shown"></ClientSideEvents>
                <FooterContentTemplate>
                    <div>
                        <%--<dx:ASPxButton ID="btnHelp" ClientInstanceName = "btnHelp" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                            Text="Trợ giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help"></SpriteProperties>
                            </Image>                            
                        </dx:ASPxButton> --%>                       
                        <dx:ASPxButton ID="btnExit" ClientInstanceName = "btnExit" CssClass="float-right button-right-margin exit"
                            runat="server" AutoPostBack="False" CausesValidation="False"
                            UseSubmitBehavior="False" Text="Thoát">
                            <ClientSideEvents Click = "buttonOutputCommExit_Click" />
                            <Image>                            
                                <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                        <%--<dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" CssClass="float-right button-right-margin"
                            runat="server" AutoPostBack="false" ClientVisible="false" UseSubmitBehavior="false"
                            Text="Hoàn tất">
                            <ClientSideEvents Click="finish"></ClientSideEvents>
                            <Image>
                                <SpriteProperties CssClass="Sprite_Finished"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnRepeat" ClientInstanceName = "btnRepeat" AutoPostBack="false" runat="server" CssClass="afinishAreaButton float-right button-right-margin"
                            HorizontalAlign="Center" Text="Tiếp tục tạo phiếu xuất kho" UseSubmitBehavior="False">
                            <ClientSideEvents Click = "repeat" />
                            <Image>
                                <SpriteProperties CssClass="Sprite_Repeat"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnNext" ClientInstanceName = "btnNext" CssClass="float-right button-right-margin"
                            runat="server" AutoPostBack="false" CausesValidation="false" UseSubmitBehavior="true"
                            Text="Tiếp theo">
                            <ClientSideEvents Click="next"></ClientSideEvents>                            
                            <Image>
                                <SpriteProperties CssClass="Sprite_Forward"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton> 
                        <dx:ASPxButton ID="btnBack" ClientInstanceName = "btnBack" CssClass="float-right button-right-margin"
                            runat="server" AutoPostBack="false" ClientVisible="false" CausesValidation="false"
                            UseSubmitBehavior="False" Text="Trở về">
                            <ClientSideEvents Click="back"></ClientSideEvents>                            
                            <Image>
                                <SpriteProperties CssClass="Sprite_Backward"></SpriteProperties>
                            </Image>
                        </dx:ASPxButton> --%>
                        <dx:ASPxButton ID="btnPrint" ClientInstanceName = "btnPrint" 
                            AutoPostBack="False" runat="server" CssClass="float-right button-right-margin"
                            Text="In">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Print"></SpriteProperties>
                            </Image>
                            <ClientSideEvents Click="buttonOutputCommPrint_Click" />
                        </dx:ASPxButton>                      
                    </div>
                
</FooterContentTemplate>
<ContentCollection>
<dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxCallbackPanel ID="cpLine" runat="server" ClientInstanceName="cpLine" 
                            OnCallback="cpLine_Callback" Width="100%">
                            <ClientSideEvents EndCallback="cpLine_EndCallback" />
<ClientSideEvents EndCallback="cpLine_EndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxPageControl ID="pctrl" runat="server" ActiveTabIndex="0" 
                                        ClientInstanceName="pctrl" Height="100%" Width="100%">
                                        <TabPages>
                                            <dx:TabPage ClientEnabled="False" Name="Finish" Text="">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                        <div style="overflow: auto; height: 450px">
                                                            <p>
                                                                Phiếu xuất kho đã được tạo.</p>
                                                            <div style="margin-bottom: 10px;">
                                                                <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server" ColCount="2" 
                                                                    EnableTheming="True" Theme="Aqua">
                                                                    <Items>
                                                                        <dx:LayoutItem Caption="Mã phiếu xuất" ColSpan="2">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="txtOutputCommWarehouseCode" runat="server" Width="200px">
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Ngày tạo" ColSpan="2">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="txtOutputCommWarehouseCreateDate" runat="server" 
                                                                                        Width="200px">
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                        <dx:LayoutItem Caption="Diễn giải" ColSpan="2">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxTextBox ID="txtOutputCommWarehouseDescription" runat="server" 
                                                                                        ClientInstanceName="txtOutputCommWarehouseDescription" Width="400px">
                                                                                    </dx:ASPxTextBox>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>
                                                                    </Items>
                                                                </dx:ASPxFormLayout>
                                                                <br />
                                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Small" 
                                                                    Text="Danh sách mặt hàng xuất kho">
                                                                </dx:ASPxLabel>
                                                            </div>
                                                            <dx:ASPxGridView ID="grdSalesInvoice" runat="server" 
                                                                AutoGenerateColumns="False" DataSourceID="BillItemXDS" Width="100%" 
                                                                ClientInstanceName="grdSalesInvoice" KeyFieldName="ItemUnitId!Key" 
                                                                OnCellEditorInitialize="grdSalesInvoice_CellEditorInitialize" 
                                                                OnParseValue="grdSalesInvoice_ParseValue" 
                                                                OnRowUpdating="grdSalesInvoice_RowUpdating" 
                                                                OnRowValidating="grdSalesInvoice_RowValidating" 
                                                                OnStartRowEditing="grdSalesInvoice_StartRowEditing">                                                               
<ClientSideEvents EndCallback="grdData_EndCallback"></ClientSideEvents>
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Mã hàng hóa" 
                                                                        FieldName="ItemUnitId.ItemId.Code" ShowInCustomizationForm="True" 
                                                                        VisibleIndex="0" Width="120px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Tên hàng hóa" 
                                                                        FieldName="ItemUnitId.ItemId.Name" ShowInCustomizationForm="True" 
                                                                        VisibleIndex="1" Width="170px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ĐVT" 
                                                                        FieldName="ItemUnitId.UnitId.Name" ShowInCustomizationForm="True" 
                                                                        VisibleIndex="3" Width="60px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="Quantity" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="80px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataComboBoxColumn Caption="Số Lô" FieldName="LotId!Key" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                                                                        <PropertiesComboBox EnableCallbackMode="True" 
                                                                            IncrementalFilteringMode="Contains" TextField="Code" TextFormatString="{0}" 
                                                                            ValueField="LotId" ValueType="System.Guid"
                                                                            OnItemRequestedByValue="cboLot_ItemRequestedByValue"
                                                                            OnItemsRequestedByFilterCondition="cboLot_ItemsRequestedByFilterCondition">
                                                                            <Columns>
                                                                                <dx:ListBoxColumn Caption="Số lô" FieldName="Code" Width="150px" />
                                                                                <dx:ListBoxColumn Caption="Hạn sử dụng" FieldName="ExpireDate" Width="100px" />
                                                                            </Columns>
                                                                        </PropertiesComboBox>
                                                                    </dx:GridViewDataComboBoxColumn>
                                                                    <dx:GridViewDataDateColumn Caption="Hạn sử dụng" ReadOnly="True" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="6" Width="100px" 
                                                                        FieldName="LotId.ExpireDate">
                                                                        <PropertiesDateEdit DisplayFormatString="">
                                                                        </PropertiesDateEdit>
                                                                    </dx:GridViewDataDateColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="8" Width="70px">
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
                                                                    <dx:GridViewDataTextColumn FieldName="BillItemId" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="10" Width="0px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Nhà sản xuất" 
                                                                        FieldName="ItemUnitId.ItemId.ManufacturerOrgId.Name" 
                                                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <settingspager showemptydatarows="True">
                                                                </settingspager>
                                                                <settingsediting mode="Inline" />
                                                                <settings horizontalscrollbarmode="Auto" verticalscrollbarmode="Auto" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings HorizontalScrollBarMode="Auto" VerticalScrollBarMode="Auto"></Settings>
                                                            </dx:ASPxGridView>
                                                        </div>
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


                            <dx:ASPxHiddenField runat="server" 
    ClientInstanceName="hfRegInfo" ID="hfRegInfo" ViewStateMode="Enabled"></dx:ASPxHiddenField>


                        
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






                        
<%--<dx:ASPxButton ID="btnHelp" ClientInstanceName = "btnHelp" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                            Text="Trợ giúp">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Help"></SpriteProperties>
                            </Image>                            
                        </dx:ASPxButton> --%>