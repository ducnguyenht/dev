<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEdittingInputInventoryCommand.ascx.cs"
    Inherits="WebModule.Warehouse.Command.PopupCommand.EdittingInputInventoryCommand.uEdittingInputInventoryCommand" %>
<%@ Register Src="~/ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<%@ Register src="~/Accounting/Journal/Transaction/Control/GridViewBookingEntries.ascx" tagname="GridViewBookingEntries" tagprefix="uc2" %>
<%@ Register src="../AddNewLotsToItem/uAddNewLotsToItem.ascx" tagname="uAddNewLotsToItem" tagprefix="uc3" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<script type="text/javascript">
    var lastItemUnitJournal = null;
    function grdTransaction_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'AllocateTransaction':
                if (!cpnTransactionAllocationObjects.InCallback()) {
                    popupTransactionAllocationObjects.Show();
                    var args = 'AllocateTransaction|' + e.visibleIndex;
                    cpnTransactionAllocationObjects.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }

    /////Handler event from Input InventoryCommand
    var uAddNewLotsToItem1Target = new EventTarget();
    function uAddNewLotsToItem1Target_handle(event) {
        var params = new Array('External', event.OutParam);
        cboLotByItem.PerformCallback(params);
    };
    uAddNewLotsToItem1Target.addListener("UpdatedLotItem", uAddNewLotsToItem1Target_handle);

</script>
<dx:aspxloadingpanel id="ldpnInventoryCommand" runat="server" horizontalalign="Center"
    text="Đang xử lý..." verticalalign="Middle" modal="True">
    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
</dx:aspxloadingpanel>
<dx:aspxcallbackpanel id="cpInventoryCommand" runat="server" showloadingpanel="false"
    showloadingpanelimage="false" width="100%" oncallback="cpInventoryCommand_OnCallback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupInventoryCommand" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="200"
                HeaderText="Thông tin phiếu nhập kho" Height="600px" PopupHorizontalAlign="WindowCenter" 
                PopupVerticalAlign="WindowCenter" Width="1100px" ShowFooter="True" ShowMaximizeButton="True"
                CloseAction="CloseButton" 
                LoadingDivStyle-BackColor="Transparent"
                ModalBackgroundStyle-BackColor="Transparent"
                ShowSizeGrip="False"
                Maximized="true"
                Modal="True">
<LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>

<ModalBackgroundStyle BackColor="Transparent"></ModalBackgroundStyle>
                <FooterTemplate>
                    <div style="padding: 10px;vertical-align:middle">
                        <div style="float: left;vertical-align:middle">
                            <div style="float: left">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" Text="Trợ Giúp"
                                    Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="float: right;vertical-align:middle">
                            <div style="float: left;">
                                <div style="float: left;"> 
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="btnPrint" CausesValidation="false" UseSubmitBehavior="false" runat="server"
                                        Text="In" AutoPostBack="false" Width="120px">
                                        <Image ToolTip="In phiếu">
                                            <SpriteProperties CssClass="Sprite_Print" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                <div style="float: left;">
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="btnBookingEntries" AutoPostBack="false" runat="server" Text="Hạch toán"
                                        Wrap="False" CausesValidation="true">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Approve" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                <div style="float: left;">
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="btnSaveCommand" AutoPostBack="false" runat="server" Text="Lưu lại"
                                        Wrap="False" CausesValidation="true">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Approve" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                                <div style="float: left; margin-left: 4px">
                                    <!-- Places button here -->
                                    <dx:ASPxButton ID="btnCloseCommandPopup" AutoPostBack="false" runat="server" 
                                        Text="Thoát" Wrap="False">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                        </Image>
                                    </dx:ASPxButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="frmCosting" runat="server" DataSourceID="InventoryCommandXDS"
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chứng từ" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã phiếu nhập kho" FieldName="Code" Width="50%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtCode" runat="server" Font-Bold="True">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã phiếu kho" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày tạo" FieldName="IssueDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="txtIssueDate" runat="server" 
                                                        PopupHorizontalAlign="RightSides">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mục đích" FieldName="Description" RowSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="txtDescription" runat="server" Text="" Width="350px" Height="35px">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người giao">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboShipper" 
                                                    runat="server" 
                                                    EnableCallbackMode="True" 
                                                    CallbackPageSize="10"
                                                    DropDownStyle="DropDown"
                                                    IncrementalFilteringMode="Contains" 
                                                    TextFormatString="{1}" 
                                                    ValueField="PersonId" ValueType="System.Guid"
                                                    EnableSynchronization="true"
                                                    OnItemRequestedByValue="colPersonOnItemRequestedByValue"
                                                    OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition" 
                                                    Width="300px">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã nhân sự" FieldName="Code" />
                                                        <dx:ListBoxColumn Caption="Tên nhân sự" FieldName="Name" />
                                                    </Columns>
                                                </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Thủ kho" VerticalAlign="Bottom">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboStoreKeeper" 
                                                    runat="server" 
                                                    EnableCallbackMode="True" 
                                                    CallbackPageSize="10"
                                                    DropDownStyle="DropDown"
                                                    IncrementalFilteringMode="Contains" TextFormatString="{1}" 
                                                    ValueField="PersonId" ValueType="System.Guid"
                                                    EnableSynchronization="true"
                                                    OnItemRequestedByValue="colPersonOnItemRequestedByValue"
                                                    OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition" 
                                                    Width="300px">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã nhân sự" FieldName="Code" />
                                                        <dx:ListBoxColumn Caption="Tên nhân sự" FieldName="Name" />
                                                    </Columns>
                                                </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người lập phiếu" VerticalAlign="Bottom">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboCreator" 
                                                    runat="server" 
                                                    EnableCallbackMode="True" 
                                                    CallbackPageSize="10"
                                                    DropDownStyle="DropDown"
                                                    IncrementalFilteringMode="Contains" TextFormatString="{1}" 
                                                    ValueField="PersonId" ValueType="System.Guid"
                                                    EnableSynchronization="true"
                                                    OnItemRequestedByValue="colPersonOnItemRequestedByValue"
                                                    OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition" 
                                                    Width="300px">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã nhân sự" FieldName="Code" />
                                                        <dx:ListBoxColumn Caption="Tên nhân sự" FieldName="Name" />
                                                    </Columns>
                                                </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Phiếu chuyển kho" Name="ParentInventoryCommand">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="lblMovingInventoryCommand" Text="N/A" runat="server">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Nhập vào kho" FieldName="RelevantInventoryId!Key">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboInventory" runat="server" CallbackPageSize="10" 
                                                    ValueField="InventoryId"
                                                    TextField="Name"
                                                    DataSourceID="InventoryXDS" EnableCallbackMode="True" 
                                                    EnableSynchronization="true" IncrementalFilteringMode="Contains" 
                                                    PopupHorizontalAlign="RightSides" Width="350px">
                                                    <ReadOnlyStyle BackColor="ButtonFace" Cursor="default">
                                                    </ReadOnlyStyle>
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="Mã kho" FieldName="Code" />
                                                        <dx:ListBoxColumn Caption="Tên kho" FieldName="Name" />
                                                    </Columns>
                                                </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:TabbedLayoutGroup Width="100%">
                                    <Items>
                                        <dx:LayoutItem Caption="Chi tiết phiếu nhập kho">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSplitter ID="SplitterDetail" ClientInstanceName="SplitterDetailAuditingInventoryCommand" runat="server" Height="400px" Width="100%" AllowResize="true">
                                                        <Styles>
                                                            <Pane>
                                                                <Paddings Padding="0px" />
                                                            </Pane>
                                                        </Styles>
                                                        <Panes>
                                                            <dx:SplitterPane Size="85%" ShowCollapseForwardButton="True" AllowResize="True">
                                                                <ContentCollection>
                                                                    <dx:SplitterContentControl ID="SplitterContentControl3" runat="server">
                                                                <dx:ASPxGridView KeyboardSupport="true" ID="grdTransaction" ClientInstanceName="grdTransaction"
                                                                    runat="server"
                                                                    AutoGenerateColumns="False" KeyFieldName="InventoryTransactionId"
                                                                    Width="100%" OnRowDeleting="grdTransaction_RowDeleting" 
                                                                    OnRowInserting="grdTransaction_RowInserting"
                                                                    OnRowUpdating="grdTransaction_RowUpdating" 
                                                                    DataSourceID="InventoryTransactionXDS" 
                                                                    OnCellEditorInitialize="grdTransaction_CellEditorInitialize" 
                                                                    OnCommandButtonInitialize="grdTransaction_CommandButtonInitialize" 
                                                                    OnInitNewRow="grdTransaction_InitNewRow">
                                                                    <ClientSideEvents Init="function(s,e){ s.ExpandAllDetailRows(); }" CustomButtonClick="grdTransaction_CustomButtonClick" />
                                                                    <SettingsEditing Mode="Inline" />
                                                                    <SettingsDetail AllowOnlyOneMasterRowExpanded="false" ShowDetailRow="True" />
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="Thẻ nhập kho" FieldName="Code" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="0" Width="125px" ReadOnly="true">
                                                                            <PropertiesTextEdit Width="120px" 
                                                                                ReadOnlyStyle-BackColor="ButtonFace"
                                                                                ReadOnlyStyle-Cursor="default">
<ReadOnlyStyle Cursor="default" BackColor="Control"></ReadOnlyStyle>

                                                                                <ValidationSettings>              
                                                                                    <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã giao dịch" />
                                                                                </ValidationSettings>
                                                                            </PropertiesTextEdit>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataDateColumn Caption="Ngày phát sinh" FieldName="IssueDate" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="1" Width="125px">
                                                                            <PropertiesDateEdit Width="120px">
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Ngày phát sinh" />
                                                                                </ValidationSettings>
                                                                            </PropertiesDateEdit>
                                                                        </dx:GridViewDataDateColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Đối tượng phân bổ" ShowInCustomizationForm="True"
                                                                            VisibleIndex="3" Width="100px">
                                                                            <CustomButtons>
                                                                                <dx:GridViewCommandColumnCustomButton ID="AllocateTransaction" Text="Allocate">
                                                                                    <Image ToolTip="Phân bổ">
                                                                                        <SpriteProperties CssClass="Sprite_Allocation" />
                                                                                    </Image>
                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                            </CustomButtons>
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewCommandColumn Name="MasterAction" Caption="Thao tác" VisibleIndex="4" ButtonType="Image" Width="100px">
                                                                            <EditButton Visible="True">
                                                                                <Image>
                                                                                    <SpriteProperties CssClass="Sprite_Edit" />
                                                                                </Image>
                                                                            </EditButton>
                                                                            <NewButton Visible="True">
                                                                                <Image>
                                                                                    <SpriteProperties CssClass="Sprite_New" />
                                                                                </Image>
                                                                            </NewButton>
                                                                            <DeleteButton Visible="True">
                                                                                <Image>
                                                                                    <SpriteProperties CssClass="Sprite_Delete" />
                                                                                </Image>
                                                                            </DeleteButton>
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
                                                                    </Columns>
                                                                    <Styles>
                                                                        <Header Font-Bold="True" HorizontalAlign="Center">
                                                                        </Header>
                                                                        <CommandColumn Spacing="4px">
                                                                        </CommandColumn>
                                                                    </Styles>
                                                                    <Templates>
                                                                        <DetailRow>
                                                                            <dx:ASPxGridView KeyboardSupport="True" 
                                                                                ID="grdDetailJournal" 
                                                                                runat="server"
                                                                                AutoGenerateColumns="False"
                                                                                Width="100%"
                                                                                KeyFieldName="InventoryJournalId" 
                                                                                DataSourceID="InventoryJournalXDS" 
                                                                                ViewStateMode="Disabled"
                                                                                EnableRowsCache="false"
                                                                                onbeforeperformdataselect="grdDetailJournal_BeforePerformDataSelect" 
                                                                                oninit="grdDetailJournal_Init" 
                                                                                onrowdeleting="grdDetailJournal_RowDeleting" 
                                                                                onrowinserting="grdDetailJournal_RowInserting" 
                                                                                oncelleditorinitialize="grdDetailJournal_CellEditorInitialize" 
                                                                                oncustomcolumndisplaytext="grdDetailJournal_CustomColumnDisplayText" 
                                                                                onrowupdating="grdDetailJournal_RowUpdating" 
                                                                                onrowvalidating="grdDetailJournal_RowValidating" 
                                                                                oncommandbuttoninitialize="grdDetailJournal_CommandButtonInitialize">
                                                                                <ClientSideEvents RowClick="grdDetailJournal_RowClick"/>
                                                                                <Columns>
                                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã hàng hóa" FieldName="ItemUnitId!Key" 
                                                                                        VisibleIndex="0" Width="120px">
	                                                                                    <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True" 
		                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" 
		                                                                                    ValueField="ItemUnitId" TextField="ItemUnitId.ItemId.Code" ValueType="System.Guid"
                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                            ReadOnlyStyle-Cursor="default">
		                                                                                    <Columns>
			                                                                                    <dx:ListBoxColumn Caption="Mã Hàng Hóa" FieldName="ItemId.Code" Name="Code" 
				                                                                                    Width="150px" />
			                                                                                    <dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="ItemId.Name" Name="Name" 
				                                                                                    Width="300px" />
			                                                                                    <dx:ListBoxColumn Caption="Nhà sản xuất" 
				                                                                                    FieldName="ItemId.ManufacturerOrgId.Name" Width="150px" />
			                                                                                    <dx:ListBoxColumn Caption="Đvt" FieldName="UnitId.Name" Name="UnitName" 
				                                                                                    Width="100px" />
			                                                                                    <dx:ListBoxColumn Caption="VAT" FieldName="ItemId.VatPercentage" />
			                                                                                    <dx:ListBoxColumn FieldName="ItemUnitId" Width="0px" />
		                                                                                    </Columns>
                                                                                            <ReadOnlyStyle BackColor="Control" Cursor="default">
                                                                                            </ReadOnlyStyle>
                                                                                            <ValidationSettings>
                                                                                                <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập mã hàng hóa" />
                                                                                            </ValidationSettings>
	                                                                                    </PropertiesComboBox>
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemUnitId.ItemId.Name" VisibleIndex="1" ReadOnly="true" Width="100px">
                                                                                        <HeaderStyle Wrap="True"/>
                                                                                        <CellStyle Wrap="True">
                                                                                        </CellStyle>
                                                                                        <PropertiesTextEdit 
                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                            ReadOnlyStyle-Cursor="default">
                                                                                            <ReadOnlyStyle BackColor="ButtonFace" Cursor="default">
                                                                                            </ReadOnlyStyle>
                                                                                        </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ItemUnitId.UnitId.Name" VisibleIndex="2" Width="70px">
                                                                                        <PropertiesTextEdit 
                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                            ReadOnlyStyle-Cursor="default"
                                                                                            Width="60px">            
                                                                                            <ReadOnlyStyle BackColor="Control" Cursor="default">
                                                                                            </ReadOnlyStyle>
                                                                                        </PropertiesTextEdit>
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewDataSpinEditColumn Caption="Số lượng theo chứng từ" FieldName="PlanCredit" VisibleIndex="3" Width="90px">
                                                                                        <PropertiesSpinEdit Width="100%" 
                                                                                            DisplayFormatString="F2"
                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                            ReadOnlyStyle-Cursor="default" >
                                                                                            <ReadOnlyStyle BackColor="Control" Cursor="default">
                                                                                            </ReadOnlyStyle>
                                                                                            <ValidationSettings >
                                                                                                <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                            </ValidationSettings>
                                                                                        </PropertiesSpinEdit>
                                                                                        <HeaderStyle Wrap="True"/>
                                                                                    </dx:GridViewDataSpinEditColumn>
                                                                                    <dx:GridViewDataSpinEditColumn Caption="Số lượng thực tế" FieldName="Credit" VisibleIndex="4" Width="90px">
                                                                                        <PropertiesSpinEdit Width="100%" 
                                                                                            DisplayFormatString="F2">
                                                                                            <ValidationSettings>
                                                                                                <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                            </ValidationSettings>
                                                                                        </PropertiesSpinEdit>
                                                                                        <HeaderStyle Wrap="True"/>
                                                                                    </dx:GridViewDataSpinEditColumn>
                                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã lô" FieldName="LotId!Key"
                                                                                        VisibleIndex="5" Width="80px" Settings-AllowSort="False">
                                                                                        <Settings AllowSort="False"/>
                                                                                        <HeaderTemplate>
                                                                                            <div>
                                                                                                <div style="display:inline-block;vertical-align:middle;">
                                                                                                    Số lô
                                                                                                </div>
                                                                                                <div style="display:inline-block;vertical-align:middle;float:right">
                                                                                                    <dx:ASPxButton ID="btnAddLot" runat="server" 
                                                                                                        Image-SpriteProperties-CssClass="Sprite_New"
                                                                                                        FocusRectPaddings-Padding="0px"
                                                                                                        FocusRectBorder-BorderColor="Transparent"
                                                                                                        FocusRectBorder-BorderWidth="0px"
                                                                                                        AutoPostBack="false"
                                                                                                        onload="btnAddLot_Load">
                                                                                                    </dx:ASPxButton>
                                                                                                </div>
                                                                                            </div>
                                                                                        </HeaderTemplate>
                                                                                        <EditItemTemplate>
                                                                                            <dx:ASPxComboBox ID="cboLotByItem" runat="server"
                                                                                                CallbackPageSize="10" 
                                                                                                ClientInstanceName="cboLotByItem"
                                                                                                EnableCallbackMode="True"
                                                                                                IncrementalFilteringMode="Contains" 
                                                                                                TextFormatString="{0}"
                                                                                                Value='<%# Bind("[LotId!Key]") %>' 
                                                                                                ValueType="System.Guid"
		                                                                                        ValueField="LotId"
                                                                                                ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                ReadOnlyStyle-Cursor="default"
                                                                                                OnCallback="comboLots_OnCallback"
                                                                                                OnItemsRequestedByFilterCondition="comboLots_ItemsRequestedByFilterCondition"
                                                                                                OnItemRequestedByValue="comboLots_ItemRequestedByValue" 
                                                                                                OnLoad="cboLotByItem_Load"
                                                                                                Width="100%">
                                                                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                                                                    <RequiredField IsRequired="true" ErrorText="Chưa nhập số lô"/>
                                                                                                </ValidationSettings>
                                                                                                <Columns>
                                                                                                    <dx:ListBoxColumn Caption="Mã lô" FieldName="Code"
				                                                                                        Width="100px" />
			                                                                                        <dx:ListBoxColumn Caption="Hạn dùng" FieldName="ExpireDate" Name="ExpireDate"
				                                                                                        Width="68px" />
		                                                                                        </Columns>
                                                                                                <ReadOnlyStyle BackColor="Control" Cursor="default">
                                                                                                </ReadOnlyStyle>
                                                                                            </dx:ASPxComboBox>
                                                                                        </EditItemTemplate>
                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                    <dx:GridViewDataDateColumn Caption="Hạn dùng" 
                                                                                        FieldName="LotId.ExpireDate" 
                                                                                        Name="ExpireDate" 
                                                                                        VisibleIndex="6" ReadOnly="true" Width="75px">
                                                                                        <PropertiesDateEdit DisplayFormatString="d"
                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                            ReadOnlyStyle-Cursor="default" Width="95px">
                                                                                            <ReadOnlyStyle BackColor="ButtonFace" Cursor="default">
                                                                                            </ReadOnlyStyle>
                                                                                        </PropertiesDateEdit>
                                                                                    </dx:GridViewDataDateColumn>
                                                                                    <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" VisibleIndex="7">
                                                                                    </dx:GridViewDataTextColumn>
                                                                                    <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="8" ButtonType="Image" Width="95px">
                                                                                        <EditButton Visible="True">
                                                                                            <Image>
                                                                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                                                            </Image>
                                                                                        </EditButton>
                                                                                        <NewButton Visible="True">
                                                                                            <Image>
                                                                                                <SpriteProperties CssClass="Sprite_New" />
                                                                                            </Image>
                                                                                        </NewButton>
                                                                                        <DeleteButton Visible="True">
                                                                                            <Image>
                                                                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                                                            </Image>
                                                                                        </DeleteButton>
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
                                                                                </Columns>
                                                                                <SettingsEditing Mode="Inline" />
                                                                                <Settings ShowFooter="True" />
                                                                                <Styles>
                                                                                    <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                    </Header>
                                                                                    <Footer Font-Bold="True">
                                                                                    </Footer>
                                                                                    <CommandColumn Spacing="4px">
                                                                                    </CommandColumn>
                                                                                </Styles>
                                                                                <SettingsBehavior ConfirmDelete="true" />
                                                                                <SettingsText ConfirmDelete="Có chắc chắn muốn xóa thông tin này?" />
                                                                            </dx:ASPxGridView>
                                                                            <dx:XpoDataSource ID="InventoryJournalXDS" runat="server" 
                                                                                TypeName="NAS.DAL.Inventory.Journal.InventoryJournal" 
                                                                                Criteria="[JournalType] = 'A' 
                                                                                            And [InventoryTransactionId!Key] = ? 
                                                                                            And [Credit] &lt;&gt; 0.0 
                                                                                            And [Debit] = 0.0 
                                                                                            And ( [RowStatus] = 1 Or [RowStatus] = 4 )" 
                                                                                DefaultSorting="" oninit="InventoryJournalXDS_Init">
                                                                                <CriteriaParameters>
                                                                                    <asp:Parameter Name="InventoryTransactionId" />
                                                                                </CriteriaParameters>
                                                                            </dx:XpoDataSource>
                                                                        </DetailRow>
                                                                    </Templates>
                                                                </dx:ASPxGridView>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                    <dx:SplitterPane MinSize="260px" Size="15%" ShowCollapseForwardButton="True" AllowResize="True">
                                                        <ContentCollection>
                                                            <dx:SplitterContentControl ID="SplitterContentControl4" runat="server">
                                                                <dx:ASPxCallbackPanel ID="cpItemUnitBalanceDetail" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
                                                                    Width="100%" OnCallback="cpItemUnitBalanceDetail_OnCallback">
                                                                    <PanelCollection>
                                                                        <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxFormLayout ID="frmDetailOfLine" runat="server" Height="100%" 
                                                                                Width="95%">
                                                                                <Items>
                                                                                    <dx:LayoutGroup Caption="Chi tiết tồn kho">
                                                                                        <Items>
                                                                                            <dx:LayoutItem Caption="Nhà sản xuất">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
                                                                                                        SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxLabel ID="lblManufacturer" runat="server" 
                                                                                                            ClientInstanceName="lblManufacturer">
                                                                                                        </dx:ASPxLabel>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                            <dx:LayoutItem Caption="Đơn giá">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
                                                                                                        SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxLabel ID="lblPrice" runat="server" ClientInstanceName="lblPrice" >
                                                                                                        </dx:ASPxLabel>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                            <dx:LayoutItem Caption="SL tồn">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" 
                                                                                                        SupportsDisabledAttribute="True">
                                                                                                        <dx:ASPxLabel ID="lblBalance" runat="server" ClientInstanceName="lblBalance">
                                                                                                        </dx:ASPxLabel>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                        </Items>
                                                                                    </dx:LayoutGroup>
                                                                                </Items>
                                                                            </dx:ASPxFormLayout>
                                                                        </dx:PanelContent>
                                                                    </PanelCollection>
                                                                </dx:ASPxCallbackPanel>
                                                            </dx:SplitterContentControl>
                                                        </ContentCollection>
                                                    </dx:SplitterPane>
                                                </Panes>
                                            </dx:ASPxSplitter>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:TabbedLayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="InventoryTransactionXDS" runat="server"
                            Criteria="[InventoryCommandId] = ? And ( [RowStatus] = 1 Or [RowStatus] = 4 )" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommandItemTransaction" 
                            DefaultSorting="">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="InventoryCommandXDS" runat="server" 
                            Criteria="[InventoryCommandId] = ?" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommand" DefaultSorting="">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="InventoryXDS" runat="server" Criteria="[RowStatus] = 1s" 
                            TypeName="NAS.DAL.Nomenclature.Inventory.Inventory" DefaultSorting="">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:ASPxCallbackPanel ID="cpnTransactionAllocationObjects" ClientInstanceName="cpnTransactionAllocationObjects"
                            runat="server" Width="100%" OnCallback="cpnTransactionAllocationObjects_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxPopupControl ID="popupTransactionAllocationObjects" runat="server" AllowDragging="True"
                                        AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="popupTransactionAllocationObjects"
                                        CloseAction="CloseButton" HeaderText="Thông tin đối tượng phân bổ" Height="480px"
                                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                                        RenderMode="Lightweight" ShowMaximizeButton="True" Width="600px">
                                        <ModalBackgroundStyle BackColor="Transparent">
                                        </ModalBackgroundStyle>
                                        <ContentCollection>
                                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <uc1:NASCustomFieldDataGridView ID="NASCustomFieldDataGridView" runat="server" />
                                            </dx:PopupControlContentControl>
                                        </ContentCollection>
                                    </dx:ASPxPopupControl>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                        <dx:ASPxPopupControl ID="popupBookingEntries" runat="server" 
                            AllowDragging="True"
                            AllowResize="True"
                            ShowFooter="True"
                            CloseAction="CloseButton" HeaderText="Thông tin hạch toán" Height="480px"
                            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            ShowSizeGrip="False"
                            RenderMode="Classic" ShowMaximizeButton="true" Width="860px">
                            <ModalBackgroundStyle BackColor="Transparent">
                            </ModalBackgroundStyle>
                            <FooterContentTemplate>
                                <div style="padding: 10px;">
                                    <div style="float: left">
                                        <div style="float: left">
                                            <!-- Places button here -->
                                            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" Text="Trợ Giúp"
                                                Wrap="False">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Help" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </div>
                                        <div style="float: left; margin-left: 4px">
                                            <!-- Places button here -->
                                        </div>
                                    </div>
                                    <div style="float: right">
                                        <div style="float: left;">
                                            <!-- Places button here -->
                                            <dx:ASPxButton ID="btnPrint" CausesValidation="false" UseSubmitBehavior="false" runat="server"
                                                Text="In" AutoPostBack="false" Width="120px">
                                                <Image ToolTip="In phiếu">
                                                    <SpriteProperties CssClass="Sprite_Print" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </div>
                                        <div style="float: left;">
                                            <!-- Places button here -->
                                            <dx:ASPxButton ID="btnBookedEntries" AutoPostBack="false" runat="server" Text="Ghi sổ"
                                                Wrap="False" CausesValidation="true" onload="btnBookedEntries_Load">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Approve" />
                                                </Image>
                                            </dx:ASPxButton>
                                        </div>
                                    </div>
                                    <div style="clear: both">
                                    </div>
                                </div>
                            </FooterContentTemplate>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server" SupportsDisabledAttribute="True">
                                    <uc2:GridViewBookingEntries ID="GridViewBookingEntries1" runat="server" />
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:ASPxPopupControl ID="popupInputCommReport" runat="server" AllowDragging="True"
                AllowResize="True" ClientInstanceName="formInpuCommReport" CloseAction="CloseButton"
                DragElement="Window" HeaderText="" Maximized="True" Modal="True"
                ShowMaximizeButton="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Width="800px">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" ClientInstanceName="PrintLayoutSplitter"
                            FullscreenMode="True" Height="100%" Orientation="Vertical" SeparatorVisible="False"
                            Width="100%">
                            <Panes>
                                <dx:SplitterPane MinSize="20px" Name="ToolbarPane" Size="40px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
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
                                        <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
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
                        <dx:ASPxGridView ID="grdFinancialJournal" runat="server" AutoGenerateColumns="False" 
                            Width="100%">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="AccountCode" VisibleIndex="0" Caption="Tài khoản">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Debit" VisibleIndex="1" Caption="Nợ">
                                    <PropertiesTextEdit DisplayFormatString="#,#">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Credit" VisibleIndex="2" Caption="Có">
                                    <PropertiesTextEdit DisplayFormatString="#,#">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <Settings ShowColumnHeaders="true" GridLines="Horizontal"/>
                        <Styles>
                            <Table>
                                <Border BorderStyle="None" />
                            </Table>
                            <Cell>
                                <Border BorderStyle="None" />
                            </Cell>
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="gvDataExporter" runat="server" 
                       GridViewID="grdFinancialJournal" OnRenderBrick="gvDataExporter_RenderBrick">
                    </dx:ASPxGridViewExporter>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:aspxcallbackpanel>
<uc3:uAddNewLotsToItem ID="uAddNewLotsToItem1" runat="server" ObjectHandler="uAddNewLotsToItem1Target" EventHandler="UpdatedLotItem"/>
