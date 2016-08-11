<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEdittingOutputInventoryCommand.ascx.cs" Inherits="WebModule.Warehouse.Command.PopupCommand.EdittingOutputInventoryCommand.uEdittingOutputInventoryCommand" %>
<%@ Register Src="~/ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<%@ Register src="../../../../Accounting/Journal/Transaction/Control/GridViewBookingEntries.ascx" tagname="GridViewBookingEntries" tagprefix="uc4" %>

<%@ Register src="../AddNewLotsToItem/uAddNewLotsToItem.ascx" tagname="uAddNewLotsToItem" tagprefix="uc2" %>
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

    function grdDetailJournal_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'AllocateJournal':
                if (!cpnTransactionAllocationObjects.InCallback()) {
                    popupTransactionAllocationObjects.Show();
                    var args = 'AllocateJournal|' + s.GetRowKey(e.visibleIndex);
                    cpnTransactionAllocationObjects.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }
</script>
<dx:ASPxLoadingPanel ID="ldpnInventoryCommand" runat="server" HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
</dx:ASPxLoadingPanel>
<dx:ASPxCallbackPanel ID="cpInventoryCommand" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    Width="100%" OnCallback="cpInventoryCommand_OnCallback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupInventoryCommand" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="200"
                HeaderText="Thông tin phiếu xuất kho" Height="600px" PopupHorizontalAlign="WindowCenter" 
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
                                        <SpriteProperties CssClass="Sprite_Apply" />
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
                        <div style="clear: both">
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
                                        <dx:LayoutItem Caption="Mã phiếu xuất kho" FieldName="Code" Width="50%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtCode" runat="server" Font-Bold="True">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã phiếu kho" />
<RequiredField IsRequired="True" ErrorText="Bắt buộc nhập M&#227; phiếu kho"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày tạo" FieldName="IssueDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="txtIssueDate" runat="server">
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
                                        <dx:LayoutItem Caption="Người nhận">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboReciever" 
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
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
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
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblMovingInventoryCommand" Text="N/A" runat="server">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Xuất từ kho" FieldName="RelevantInventoryId!Key" Width="350px">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboInventory" runat="server" CallbackPageSize="10" 
                                                    ValueField="InventoryId"
                                                    TextField="Name"
                                                    DataSourceID="InventoryXDS" EnableCallbackMode="True" Width="350px"
                                                    EnableSynchronization="true" IncrementalFilteringMode="Contains">
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
                                        <dx:LayoutItem Caption="Chi tiết phiếu xuất kho">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSplitter ID="SplitterDetail" ClientInstanceName="SplitterDetailAuditingInventoryCommand" runat="server" Height="400px" Width="100%" AllowResize="true">
                                                        <Styles>
                                                            <Pane>
                                                                <Paddings Padding="0px" />
<Paddings Padding="0px"></Paddings>
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
                                                                            <ClientSideEvents Init="function(s,e){ s.ExpandAllDetailRows(); }" 
                                                                                CustomButtonClick="grdTransaction_CustomButtonClick"
                                                                                EndCallback="function(s, e){
                                                                                    if (s.cpErrorMsg !== null && s.cpErrorMsg !== undefined && s.cpErrorMsg != '') 
                                                                                    {
                                                                                        alert(s.cpErrorMsg);
                                                                                        delete s.cpErrorMsg;
                                                                                    }
                                                                                ;}" />
                                                                            <SettingsEditing Mode="Inline" />
                                                                            <SettingsDetail ShowDetailRow="True" />
<ClientSideEvents CustomButtonClick="grdTransaction_CustomButtonClick" EndCallback="function(s, e){
                                                                                    if (s.cpErrorMsg !== null &amp;&amp; s.cpErrorMsg !== undefined &amp;&amp; s.cpErrorMsg != &#39;&#39;) 
                                                                                    {
                                                                                        alert(s.cpErrorMsg);
                                                                                        delete s.cpErrorMsg;
                                                                                    }
                                                                                ;}" Init="function(s,e){ s.ExpandAllDetailRows(); }"></ClientSideEvents>
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="Mã thẻ xuất" FieldName="Code" ShowInCustomizationForm="True" 
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
<SpriteProperties CssClass="Sprite_Allocation"></SpriteProperties>
                                                                                            </Image>
                                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                                    </CustomButtons>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewCommandColumn Name="MasterAction" Caption="Thao tác" VisibleIndex="4" ButtonType="Image" Width="120px">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Edit" />
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_New" />
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
                                                                                            <SpriteProperties CssClass="Sprite_Delete" />
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
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
                                                                                </dx:GridViewCommandColumn>
                                                                            </Columns>

<SettingsEditing Mode="Inline"></SettingsEditing>

<SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                                                                            <Styles>
                                                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                </Header>
                                                                                <CommandColumn Spacing="4px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                            <Templates>
                                                                                <DetailRow>
                                                                                    <dx:ASPxGridView KeyboardSupport="True" ID="grdDetailJournal"
                                                                                        runat="server"
                                                                                        AutoGenerateColumns="False"
                                                                                        Width="100%"
                                                                                        KeyFieldName="InventoryJournalId" DataSourceID="InventoryJournalXDS" 
                                                                                        onbeforeperformdataselect="grdDetailJournal_BeforePerformDataSelect" 
                                                                                        oninit="grdDetailJournal_Init" 
                                                                                        onrowdeleting="grdDetailJournal_RowDeleting" 
                                                                                        onrowinserting="grdDetailJournal_RowInserting" 
                                                                                        oncelleditorinitialize="grdDetailJournal_CellEditorInitialize" 
                                                                                        oncustomcolumndisplaytext="grdDetailJournal_CustomColumnDisplayText" 
                                                                                        onrowupdating="grdDetailJournal_RowUpdating" 
                                                                                        onrowvalidating="grdDetailJournal_RowValidating">
                                                                                        <ClientSideEvents CustomButtonClick="grdDetailJournal_CustomButtonClick" 
                                                                                            RowClick="grdDetailJournal_RowClick" />
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã hàng hóa" FieldName="ItemUnitId!Key" 
	                                                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="120px">
	                                                                                            <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True" 
		                                                                                            IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}"
		                                                                                            ValueField="ItemUnitId" ValueType="System.Guid">
		                                                                                            <Columns>
			                                                                                            <dx:ListBoxColumn Caption="Mã Hàng Hóa" FieldName="ItemId.Code" Name="Code" 
				                                                                                            Width="150px" />
			                                                                                            <dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="ItemId.Name" Name="Name" 
				                                                                                            Width="300px" />
			                                                                                            <dx:ListBoxColumn Caption="Nhà sản xuất" 
				                                                                                            FieldName="ItemId.ManufacturerOrgId.Name" Width="150px" />
			                                                                                            <dx:ListBoxColumn Caption="Đvt" FieldName="UnitId.Name" Name="UnitName" 
				                                                                                            Width="100px" />
		                                                                                            </Columns>
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
                                                                                                    ReadOnlyStyle-Cursor="default">
                                                                                                    <ReadOnlyStyle BackColor="ButtonFace" Cursor="default">
                                                                                                    </ReadOnlyStyle>
                                                                                                </PropertiesTextEdit>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataSpinEditColumn Caption="Số lượng theo chứng từ" FieldName="PlanCredit" VisibleIndex="3" Width="90px">
                                                                                                <HeaderStyle Wrap="True"/>
                                                                                                <PropertiesSpinEdit Width="100%" DisplayFormatString="F2">
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesSpinEdit>
                                                                                            </dx:GridViewDataSpinEditColumn>
                                                                                            <dx:GridViewDataSpinEditColumn Caption="Số lượng thực tế" FieldName="Credit" VisibleIndex="4" Width="90px">
                                                                                                <HeaderStyle Wrap="True"/>
                                                                                                <PropertiesSpinEdit Width="100%" DisplayFormatString="F2">
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesSpinEdit>
                                                                                            </dx:GridViewDataSpinEditColumn>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã lô" FieldName="LotId!Key" VisibleIndex="5"
                                                                                                ShowInCustomizationForm="True" Width="80px" Settings-AllowSort="False">
                                                                                                <HeaderTemplate>
                                                                                                    <div>
                                                                                                        <div style="display:inline-block;vertical-align:middle;">
                                                                                                            Số lô
                                                                                                        </div>
                                                                                                        <div style="display:inline-block;vertical-align:middle;float:right">
                                                                                                            <dx:ASPxButton ID="btnAddLot" runat="server" 
                                                                                                                Image-SpriteProperties-CssClass="Sprite_New"
                                                                                                                FocusRectPaddings-Padding="0px"
                                                                                                                FocusRectBorder-BorderColor="Transparent" AutoPostBack="false"
                                                                                                                FocusRectBorder-BorderWidth="0px" onload="btnAddLot_Load">
                                                                                                            </dx:ASPxButton>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </HeaderTemplate>
	                                                                                            <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True"
                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0}" 
		                                                                                            ValueField="LotId" ValueType="System.Guid" Width="80px">
                                                                                                    <Columns>
                                                                                                        <dx:ListBoxColumn Caption="Mã lô" FieldName="Code"
				                                                                                            Width="100px" />
			                                                                                            <dx:ListBoxColumn Caption="Hạn dùng" FieldName="ExpireDate"
				                                                                                            Width="68px" />
		                                                                                            </Columns>
                                                                                                    <ValidationSettings>
                                                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã lô" />
                                                                                                    </ValidationSettings>
                                                                                                </PropertiesComboBox>
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataDateColumn Caption="Hạn sử dụng" FieldName="LotId.ExpireDate" Name="ExpireDate" VisibleIndex="6" ReadOnly="true" Width="75px">
                                                                                                <PropertiesDateEdit DisplayFormatString="d"
                                                                                                    ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                    ReadOnlyStyle-Cursor="default">
                                                                                                    <ReadOnlyStyle BackColor="ButtonFace" Cursor="default">
                                                                                                    </ReadOnlyStyle>
                                                                                                </PropertiesDateEdit>
                                                                                            </dx:GridViewDataDateColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" VisibleIndex="7" >
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
                                                                                        <SettingsDetail AllowOnlyOneMasterRowExpanded="false" />
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
                                                                                        <SettingsLoadingPanel Mode="Disabled"/>
                                                                                        <SettingsBehavior ConfirmDelete="true" />
                                                                                        <SettingsText ConfirmDelete="Có chắc chắn muốn xóa thông tin này?" />
                                                                                    </dx:ASPxGridView>
                                                                                    <dx:XpoDataSource ID="InventoryJournalXDS" runat="server" 
                                                                                        TypeName="NAS.DAL.Inventory.Journal.InventoryJournal" 
                                                                                        Criteria="[JournalType] = 'A' 
                                                                                            And [InventoryTransactionId!Key] = ? 
                                                                                            And [Credit] &lt;&gt; 0.0 And [Debit] = 0.0
                                                                                            And ( [RowStatus] = 1 Or [RowStatus] = 4 )" 
                                                                                        oninit="InventoryJournalXDS_Init">
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
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblManufacturer" runat="server" 
                                                                                                                    ClientInstanceName="lblManufacturer">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Đơn giá">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblPrice" runat="server" ClientInstanceName="lblPrice">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="SL tồn">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
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
                                                                                    <%--<uc3:uViewBalanceInfo ID="uViewBalanceInfo1" runat="server" />--%>
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
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommandItemTransaction">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="InventoryCommandXDS" runat="server" 
                            Criteria="[InventoryCommandId] = ?" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommand">
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
                                <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                    <uc4:GridViewBookingEntries ID="GridViewBookingEntries1" runat="server" />
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                        <dx:ASPxPopupControl ID="PopupInventoryCommandActor" runat="server"
                            PopupElementID="popupAnchor"
                            Height="268px" Width="650px" RenderMode="Classic"
                            HeaderText="Thông tin đối tượng phiếu nhập kho">
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdInventoryCommandActor" DataSourceID="InventoryCommandActorXDS" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="InventoryCommandActorId"
                                        Width="100%">
                                        <ClientSideEvents RowClick="function(s, e){
                                                                        s.StartEditRow(e.visibleIndex);}" />
<ClientSideEvents RowClick="function(s, e){
                                                                        s.StartEditRow(e.visibleIndex);}"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="InventoryCommandActorId" 
                                                ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="True" 
                                                Width="0px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Loại đối tượng" 
                                                FieldName="InventoryCommandActorTypeId.Description" ReadOnly="True" ShowInCustomizationForm="True" 
                                                VisibleIndex="0" Width="200px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Tên đối tượng" FieldName="PersonId!Key" 
                                                ShowInCustomizationForm="True" VisibleIndex="1" Width="300px">
                                                <PropertiesComboBox EnableCallbackMode="True" 
                                                    IncrementalFilteringMode="Contains" TextField="Name" TextFormatString="{1}" 
                                                    ValueField="PersonId" ValueType="System.Guid"
                                                    OnItemRequestedByValue="colPersonOnItemRequestedByValue"
                                                    OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition">
                                                    <Columns>
                                                        


<dx:ListBoxColumn Caption="Mã đối tượng" FieldName="Code" />
                                                        


<dx:ListBoxColumn Caption="Tên đối tượng" FieldName="Name" />
                                                        


</Columns>
                                                        


</PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="InventoryCommandId!Key" ShowInCustomizationForm="True" 
                                                VisibleIndex="5" Width="0px">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                ShowInCustomizationForm="True" VisibleIndex="2" Width="70px">
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
                                        <SettingsBehavior AllowFocusedRow="True" />

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

                                        <SettingsPager ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings VerticalScrollBarMode="Visible" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings VerticalScrollBarMode="Visible"></Settings>
                                    </dx:ASPxGridView>
                                    <dx:XpoDataSource ID="InventoryCommandActorXDS" runat="server" Criteria="[InventoryCommandId] = ?" 
                                        TypeName="NAS.DAL.Inventory.Command.InventoryCommandActor" 
                                        DefaultSorting="">
                                        <CriteriaParameters>
                                            <asp:Parameter Name="InventoryCommandId" />
                                        </CriteriaParameters>
                                    </dx:XpoDataSource>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                        <dx:ASPxPopupControl ID="popupOutputCommReport" runat="server" AllowDragging="True"
                            AllowResize="True" ClientInstanceName="formOutputCommReport" CloseAction="CloseButton"
                            ShowMaximizeButton="True"
                            DragElement="Window" HeaderText="" Maximized="True" Modal="True"
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
                                                        <dx:ReportToolbar ID="tlbOutputCommViewer" runat="server"
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
<Margins MarginLeft="3px" MarginRight="3px"></Margins>
                                                                </LabelStyle>
                                                            </Styles>
                                                        </dx:ReportToolbar>
                                                    </dx:SplitterContentControl>
                                                </ContentCollection>
                                            </dx:SplitterPane>
                                            <dx:SplitterPane ScrollBars="Auto">
                                                <ContentCollection>
                                                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                        <dx:ReportViewer ID="rptOutputCommViewer" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                            BorderWidth="1px" ClientInstanceName="rptOutputCommViewer">
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
                                    <dx:ASPxGridView ID="grdFinancialJournal" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="AccountCode" VisibleIndex="0" Caption="Tài khoản">
                                             <PropertiesTextEdit InvalidStyle-HorizontalAlign="Left" >
<InvalidStyle HorizontalAlign="Left"></InvalidStyle>
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Debit" VisibleIndex="1" Caption="Nợ" >
                                                <PropertiesTextEdit DisplayFormatString="#,#">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Credit" VisibleIndex="2" Caption="Có" >
                                                <PropertiesTextEdit DisplayFormatString="#,#">
                                                </PropertiesTextEdit>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <Settings ShowColumnHeaders="true" GridLines="Horizontal" />
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
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc2:uAddNewLotsToItem ID="uAddNewLotsToItem1" runat="server" />