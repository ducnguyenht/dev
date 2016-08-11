<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEdittingMovingInventoryCommand.ascx.cs" Inherits="WebModule.Warehouse.Command.PopupCommand.EdittingMovingInventoryCommand.uEdittingMovingInventoryCommand" %>
<%@ Register Src="~/ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<%@ Register src="../AddNewLotsToItem/uAddNewLotsToItem.ascx" tagname="uAddNewLotsToItem" tagprefix="uc2" %>
<script type="text/javascript">
    var lastItemUnitJournal = null;
    function grdOutputTransaction_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'OutputAllocateTransaction':
                if (!cpnTransactionAllocationObjects.InCallback()) {
                    popupTransactionAllocationObjects.Show();
                    var args = 'OutputAllocateTransaction|' + e.visibleIndex;
                    cpnTransactionAllocationObjects.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }

    function grdInputTransaction_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'InputAllocateTransaction':
                if (!cpnTransactionAllocationObjects.InCallback()) {
                    popupTransactionAllocationObjects.Show();
                    var args = 'InputAllocateTransaction|' + e.visibleIndex;
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
                HeaderText="Thông tin phiếu chuyển kho" Height="600px" PopupHorizontalAlign="WindowCenter" 
                PopupVerticalAlign="WindowCenter" Width="1100px" ShowFooter="True" ShowMaximizeButton="True"
                CloseAction="CloseButton" 
                LoadingDivStyle-BackColor="Transparent"
                ModalBackgroundStyle-BackColor="Transparent"
                ShowSizeGrip="False"
                Maximized="true"
                Modal="True">
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
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="frmMovingInventoryCommand" runat="server" DataSourceID="MovingInventoryCommandXDS"
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin phiếu chuyển kho" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã phiếu chuyển kho" FieldName="Code" Width="50%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtMovingInventoryCommandCode" runat="server" Font-Bold="True">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã phiếu chuyển kho" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày tạo" FieldName="IssueDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="txtMovingInventoryCommandIssueDate" runat="server">
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mục đích" FieldName="Description" RowSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="txtMovingInventoryCommandDescription" runat="server" Text="" Width="350px" Height="35px">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Giám đốc">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="cboMovingDirector" 
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
                                                <dx:ASPxComboBox ID="cboMovingStoreKeeper" 
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
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:TabbedLayoutGroup Width="100%" ActiveTabIndex="1">
                                    <Items>
                                        <dx:LayoutItem Caption="Phiếu xuất kho">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="frmOutputInventoryCommand" runat="server" DataSourceID="OutputInventoryCommandXDS"
                                                        Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Thông tin phiếu xuất kho" ColCount="2">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã phiếu xuất kho" FieldName="Code" Width="50%">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="txtOutputInventoryCommandCode" runat="server" Font-Bold="True">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã phiếu kho" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Ngày tạo" FieldName="IssueDate">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxDateEdit ID="txtOutputInventoryCommandIssueDate" runat="server">
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Mục đích" FieldName="Description" RowSpan="2">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxMemo ID="txtOutputInventoryCommandDescription" runat="server" Text="" Width="350px" Height="35px">
                                                                                </dx:ASPxMemo>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Giám đốc">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboOutputDirector" 
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
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboOutputStoreKeeper" 
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
                                                                    <dx:LayoutItem Caption="Xuất từ kho" FieldName="RelevantInventoryId!Key" Width="350px">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboFromInventory" 
                                                                                runat="server" 
                                                                                CallbackPageSize="10" 
                                                                                Width="350px"
                                                                                ValueField="InventoryId"
                                                                                TextField="Name"
                                                                                DataSourceID="FromInventoryXDS" EnableCallbackMode="True" 
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
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
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
                                                                                                    <dx:ASPxGridView KeyboardSupport="true" ID="grdOutputTransaction" ClientInstanceName="grdOuputTransaction"
                                                                                                        runat="server"
                                                                                                        AutoGenerateColumns="False" KeyFieldName="InventoryTransactionId"
                                                                                                        Width="100%"
                                                                                                        OnRowDeleting="grdOutputTransaction_RowDeleting" 
                                                                                                        OnRowInserting="grdOutputTransaction_RowInserting"
                                                                                                        OnRowUpdating="grdOutputTransaction_RowUpdating"
                                                                                                        DataSourceID="OutputInventoryTransactionXDS" 
                                                                                                        OnCellEditorInitialize="grdOutputTransaction_CellEditorInitialize">
                                                                                                        <ClientSideEvents Init="function(s,e){ s.ExpandAllDetailRows(); }" CustomButtonClick="grdOutputTransaction_CustomButtonClick" />
                                                                                                        <SettingsEditing Mode="Inline" />
                                                                                                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                                                                                        <Columns>
                                                                                                            <dx:GridViewDataTextColumn Caption="Mã thẻ xuất" FieldName="Code" ShowInCustomizationForm="True" 
                                                                                                                VisibleIndex="0" Width="125px" ReadOnly="true">
                                                                                                                <PropertiesTextEdit Width="120px"
                                                                                                                    ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                                    ReadOnlyStyle-Cursor="default">
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
                                                                                                                    <dx:GridViewCommandColumnCustomButton ID="OutputAllocateTransaction" Text="Allocate">
                                                                                                                        <Image ToolTip="Phân bổ">
                                                                                                                            <SpriteProperties CssClass="Sprite_Allocation" />
                                                                                                                        </Image>
                                                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                                                </CustomButtons>
                                                                                                            </dx:GridViewCommandColumn>
                                                                                                            <dx:GridViewCommandColumn Name="MasterAction" Caption="Thao tác" VisibleIndex="5" ButtonType="Image" Width="100px">
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
                                                                                                        </Columns>
                                                                                                        <Styles>
                                                                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                                                                            </Header>
                                                                                                            <CommandColumn Spacing="4px">
                                                                                                            </CommandColumn>
                                                                                                        </Styles>
                                                                                                        <Templates>
                                                                                                            <DetailRow>
                                                                                                                <dx:ASPxGridView KeyboardSupport="True" ID="grdOutputDetailJournal" 
                                                                                                                    runat="server"
                                                                                                                    AutoGenerateColumns="False"
                                                                                                                    Width="100%"
                                                                                                                    DataSourceID="OutputInventoryJournalXDS"
                                                                                                                    KeyFieldName="InventoryJournalId"
                                                                                                                    onbeforeperformdataselect="grdOutputDetailJournal_BeforePerformDataSelect" 
                                                                                                                    oninit="grdOutputDetailJournal_Init" 
                                                                                                                    onrowdeleting="grdOutputDetailJournal_RowDeleting" 
                                                                                                                    onrowinserting="grdOutputDetailJournal_RowInserting" 
                                                                                                                    oncelleditorinitialize="grdOutputDetailJournal_CellEditorInitialize" 
                                                                                                                    oncustomcolumndisplaytext="grdOutputDetailJournal_CustomColumnDisplayText" 
                                                                                                                    onrowupdating="grdOutputDetailJournal_RowUpdating" 
                                                                                                                    onrowvalidating="grdOutputDetailJournal_RowValidating">
                                                                                                                    <ClientSideEvents RowClick="grdOutputDetailJournal_RowClick"/>
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
                                                                                                                            <PropertiesTextEdit ReadOnlyStyle-BackColor="ButtonFace" 
                                                                                                                                ReadOnlyStyle-Cursor="default"></PropertiesTextEdit>
                                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                                        <dx:GridViewDataSpinEditColumn Caption="Số lượng theo chứng từ" FieldName="PlanCredit" VisibleIndex="3" Width="90px">
                                                                                                                            <HeaderStyle Wrap="True"/>
                                                                                                                            <CellStyle Wrap="True">
                                                                                                                            </CellStyle>
                                                                                                                            <PropertiesSpinEdit Width="100%" DisplayFormatString="F2">
                                                                                                                                <ValidationSettings>
                                                                                                                                    <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                                                                </ValidationSettings>
                                                                                                                            </PropertiesSpinEdit>
                                                                                                                        </dx:GridViewDataSpinEditColumn>
                                                                                                                        <dx:GridViewDataSpinEditColumn Caption="Số lượng thực tế" FieldName="Credit" VisibleIndex="4" Width="90px">
                                                                                                                            <HeaderStyle Wrap="True"/>
                                                                                                                            <CellStyle Wrap="True">
                                                                                                                            </CellStyle>
                                                                                                                            <PropertiesSpinEdit Width="100%" DisplayFormatString="F2">
                                                                                                                                <ValidationSettings>
                                                                                                                                    <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                                                                </ValidationSettings>
                                                                                                                            </PropertiesSpinEdit>
                                                                                                                        </dx:GridViewDataSpinEditColumn>
                                                                                                                        <dx:GridViewDataComboBoxColumn Caption="Mã lô" FieldName="LotId!Key" VisibleIndex="5"
                                                                                                                            ShowInCustomizationForm="True" Width="90px" Settings-AllowSort="False">
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
	                                                                                                                        <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True"
                                                                                                                                IncrementalFilteringMode="Contains" TextFormatString="{0}" 
		                                                                                                                        ValueField="LotId" ValueType="System.Guid">
                                                                                                                                <Columns>
                                                                                                                                    <dx:ListBoxColumn Caption="Mã lô" FieldName="Code"
				                                                                                                                        Width="100px" />
			                                                                                                                        <dx:ListBoxColumn Caption="Hạn dùng" FieldName="ExpireDate" 
				                                                                                                                        Width="68" />
		                                                                                                                        </Columns>
                                                                                                                                <ValidationSettings>
                                                                                                                                    <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã lô" />
                                                                                                                                </ValidationSettings>
                                                                                                                            </PropertiesComboBox>
                                                                                                                        </dx:GridViewDataComboBoxColumn>
                                                                                                                        <dx:GridViewDataDateColumn Caption="Hạn dùng" FieldName="LotId.ExpireDate" Name="ExpireDate" VisibleIndex="6" ReadOnly="true" Width="75px">
                                                                                                                            <PropertiesDateEdit DisplayFormatString="d"
                                                                                                                                ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                                                ReadOnlyStyle-Cursor="default">
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
                                                                                                                    <SettingsLoadingPanel Mode="Disabled" />
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
                                                                                                            </DetailRow>
                                                                                                        </Templates>
                                                                                                    </dx:ASPxGridView>
                                                                                                </dx:SplitterContentControl>
                                                                                            </ContentCollection>
                                                                                        </dx:SplitterPane>
                                                                                        <dx:SplitterPane MinSize="260px" Size="15%" ShowCollapseForwardButton="True" AllowResize="True">
                                                                                            <ContentCollection>
                                                                                                <dx:SplitterContentControl ID="SplitterContentControl4" runat="server">
                                                                                                    <dx:ASPxCallbackPanel ID="cpOutputItemUnitBalanceDetail" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
                                                                                                        Width="100%" OnCallback="cpOutputItemUnitBalanceDetail_OnCallback">
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
                                                                                                                                            <dx:ASPxLabel ID="lblOutputManufacturer" runat="server">
                                                                                                                                            </dx:ASPxLabel>
                                                                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                                                                    </LayoutItemNestedControlCollection>
                                                                                                                                </dx:LayoutItem>
                                                                                                                                <dx:LayoutItem Caption="Đơn giá">
                                                                                                                                    <LayoutItemNestedControlCollection>
                                                                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
                                                                                                                                            SupportsDisabledAttribute="True">
                                                                                                                                            <dx:ASPxLabel ID="lblOutputPrice" runat="server">
                                                                                                                                            </dx:ASPxLabel>
                                                                                                                                        </dx:LayoutItemNestedControlContainer>
                                                                                                                                    </LayoutItemNestedControlCollection>
                                                                                                                                </dx:LayoutItem>
                                                                                                                                <dx:LayoutItem Caption="SL tồn">
                                                                                                                                    <LayoutItemNestedControlCollection>
                                                                                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
                                                                                                                                            SupportsDisabledAttribute="True">
                                                                                                                                            <dx:ASPxLabel ID="lblOutputBalance" runat="server">
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
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Phiếu nhập kho">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxFormLayout ID="frmInputInventoryCommand" runat="server" DataSourceID="InputInventoryCommandXDS"
                                                        Width="100%">
                                                        <Items>
                                                            <dx:LayoutGroup Caption="Thông tin phiếu nhập kho" ColCount="2">
                                                                <Items>
                                                                    <dx:LayoutItem Caption="Mã phiếu nhập kho" FieldName="Code" Width="50%">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxTextBox ID="txtInputInventoryCommandCode" runat="server" Font-Bold="True">
                                                                                    <ValidationSettings>
                                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã phiếu kho" />
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Ngày tạo" FieldName="IssueDate">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxDateEdit ID="txtInputInventoryCommandIssueDate" runat="server">
                                                                                </dx:ASPxDateEdit>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Mục đích" FieldName="Description" RowSpan="2">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxMemo ID="txtInputInventoryCommandDescription" runat="server" Text="" Width="350px" Height="35px">
                                                                                </dx:ASPxMemo>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                    <dx:LayoutItem Caption="Giám đốc">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboInputDirector" 
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
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboInputStoreKeeper" 
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
                                                                    <%--<dx:LayoutItem Caption="Nhân sự">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td id="popupInputAnchor">
                                                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False"
                                                                                                Text="..." UseSubmitBehavior="False" Width="10px">
                                                                                            </dx:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>--%>
                                                                    <dx:LayoutItem Caption="Nhập vào kho" FieldName="RelevantInventoryId!Key" Width="350px">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cboToInventory" runat="server" CallbackPageSize="10" 
                                                                                ValueField="InventoryId"
                                                                                TextField="Name"
                                                                                DataSourceID="ToInventoryXDS" EnableCallbackMode="True" Width="350px"
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
                                                                    <dx:LayoutItem Caption="Chi tiết phiếu nhập kho">
                                                                        <LayoutItemNestedControlCollection>
                                                                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                                                <dx:ASPxGridView KeyboardSupport="true" ID="grdInputTransaction" ClientInstanceName="grdInputTransaction"
                                                                                    runat="server"
                                                                                    AutoGenerateColumns="False" KeyFieldName="InventoryTransactionId"
                                                                                    Width="100%"
                                                                                    DataSourceID="InputInventoryTransactionXDS"
                                                                                    OnRowDeleting="grdInputTransaction_RowDeleting" 
                                                                                    OnRowInserting="grdInputTransaction_RowInserting"
                                                                                    OnRowUpdating="grdInputTransaction_RowUpdating" 
                                                                                    OnCellEditorInitialize="grdInputTransaction_CellEditorInitialize" >
                                                                                    <ClientSideEvents Init="function(s,e){ s.ExpandAllDetailRows(); }" CustomButtonClick="grdInputTransaction_CustomButtonClick" />
                                                                                    <SettingsEditing Mode="Inline" />
                                                                                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Mã thẻ nhập" FieldName="Code" ShowInCustomizationForm="True" 
                                                                                            VisibleIndex="0" Width="125px" ReadOnly="true">
                                                                                            <PropertiesTextEdit Width="120px"
                                                                                                ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                ReadOnlyStyle-Cursor="default">
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
                                                                                                <dx:GridViewCommandColumnCustomButton ID="InputAllocateTransaction" Text="Allocate">
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
                                                                                            <dx:ASPxGridView KeyboardSupport="True" ID="grdInputDetailJournal" 
                                                                                                runat="server"
                                                                                                AutoGenerateColumns="False"
                                                                                                Width="100%"
                                                                                                onbeforeperformdataselect="grdInputDetailJournal_BeforePerformDataSelect" 
                                                                                                oninit="grdInputDetailJournal_Init" 
                                                                                                onrowdeleting="grdInputDetailJournal_RowDeleting" 
                                                                                                onrowinserting="grdInputDetailJournal_RowInserting" 
                                                                                                oncelleditorinitialize="grdInputDetailJournal_CellEditorInitialize" 
                                                                                                oncustomcolumndisplaytext="grdInputDetailJournal_CustomColumnDisplayText" 
                                                                                                onrowupdating="grdInputDetailJournal_RowUpdating" 
                                                                                                onrowvalidating="grdInputDetailJournal_RowValidating"
                                                                                                KeyFieldName="InventoryJournalId" DataSourceID="InputInventoryJournalXDS">
                                                                                                <ClientSideEvents RowClick="function(s, e){
                                                                                                    s.StartEditRow(e.visibleIndex);   
                                                                                                }"/>
                                                                                                <Columns>
                                                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã hàng hóa" FieldName="ItemUnitId!Key" 
	                                                                                                    ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="true" Width="120px">
	                                                                                                    <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True"
		                                                                                                    IncrementalFilteringMode="Contains" TextFormatString="{0}" 
                                                                                                            ReadOnlyStyle-BackColor="ButtonFace" 
                                                                                                            ReadOnlyStyle-Cursor="default"
		                                                                                                    ValueField="ItemUnitId" TextField="ItemUnitId.ItemId.Code" ValueType="System.Guid">
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
                                                                                                            <ValidationSettings>
                                                                                                                <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập mã hàng hóa" />
                                                                                                            </ValidationSettings>
	                                                                                                    </PropertiesComboBox>
                                                                                                    </dx:GridViewDataComboBoxColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="Tên hàng hóa" 
                                                                                                        FieldName="ItemUnitId.ItemId.Name" VisibleIndex="1" ReadOnly="true" Width="100px">
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
                                                                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ItemUnitId.UnitId.Name"
                                                                                                        VisibleIndex="1" 
                                                                                                        Width="70px"
                                                                                                        ReadOnly="true">
                                                                                                        <PropertiesTextEdit ReadOnlyStyle-BackColor="ButtonFace" 
                                                                                                        ReadOnlyStyle-Cursor="default">
                                                                                                        </PropertiesTextEdit>
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataSpinEditColumn Caption="Số lượng theo chứng từ" 
                                                                                                        FieldName="PlanCredit" VisibleIndex="2" ReadOnly="true" Width="90px">
                                                                                                        <PropertiesSpinEdit Width="100%" 
                                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                            ReadOnlyStyle-Cursor="default" DisplayFormatString="F2">
                                                                                                            <ValidationSettings>
                                                                                                                <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesSpinEdit>
                                                                                                        <HeaderStyle Wrap="True"/>
                                                                                                    </dx:GridViewDataSpinEditColumn>
                                                                                                    <dx:GridViewDataSpinEditColumn Caption="Số lượng thực tế" FieldName="Credit" VisibleIndex="3" Width="90px">
                                                                                                        <PropertiesSpinEdit Width="100%" DisplayFormatString="F2">
                                                                                                            <ValidationSettings>
                                                                                                                <RequiredField IsRequired="true" ErrorText="Bắt buộc Số lượng" />
                                                                                                            </ValidationSettings>
                                                                                                        </PropertiesSpinEdit>
                                                                                                        <HeaderStyle Wrap="True"/>
                                                                                                    </dx:GridViewDataSpinEditColumn>
                                                                                                    <dx:GridViewDataComboBoxColumn Caption="Mã lô" FieldName="LotId!Key" VisibleIndex="4"
                                                                                                        ShowInCustomizationForm="True" Width="90px" ReadOnly="true" Settings-AllowSort="False">
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
                                                                                                        <PropertiesComboBox EnableSynchronization="False" IncrementalFilteringMode="Contains" TextFormatString="{0}" 
		                                                                                                    ValueField="LotId" ValueType="System.Guid"
                                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                            ReadOnlyStyle-Cursor="default"
                                                                                                            CallbackPageSize="10" 
                                                                                                            EnableCallbackMode="True">
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
                                                                                                    <dx:GridViewDataDateColumn Caption="Hạn dùng" FieldName="LotId.ExpireDate" VisibleIndex="5" ReadOnly="true" Width="75px">
                                                                                                        <PropertiesDateEdit DisplayFormatString="d"
                                                                                                            ReadOnlyStyle-BackColor="ButtonFace"
                                                                                                            ReadOnlyStyle-Cursor="default">
                                                                                                            <ReadOnlyStyle BackColor="ButtonFace" Cursor="default">
                                                                                                            </ReadOnlyStyle>
                                                                                                        </PropertiesDateEdit>
                                                                                                    </dx:GridViewDataDateColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" VisibleIndex="6">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="7" ButtonType="Image" Width="95px">
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
                                                                                        </DetailRow>
                                                                                    </Templates>
                                                                                </dx:ASPxGridView>
                                                                            </dx:LayoutItemNestedControlContainer>
                                                                        </LayoutItemNestedControlCollection>
                                                                    </dx:LayoutItem>
                                                                </Items>
                                                            </dx:TabbedLayoutGroup>
                                                        </Items>
                                                    </dx:ASPxFormLayout>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:TabbedLayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:XpoDataSource ID="InputInventoryTransactionXDS" runat="server"
                            Criteria="[InventoryCommandId] = ? And ( [RowStatus] = 1 Or [RowStatus] = 4 )" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommandItemTransaction">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="OutputInventoryTransactionXDS" runat="server"
                            Criteria="[InventoryCommandId] = ? And ( [RowStatus] = 1 Or [RowStatus] = 4 )" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommandItemTransaction">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="InputInventoryJournalXDS" runat="server" 
                            TypeName="NAS.DAL.Inventory.Journal.InventoryJournal"
                            Criteria="[JournalType] = 'A' 
                                        And [InventoryTransactionId!Key] = ? 
                                        And [Credit] &lt;&gt; 0.0 And [Debit] = 0.0 
                                        And ( [RowStatus] = 1 Or [RowStatus] = 4 )">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryTransactionId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="OutputInventoryJournalXDS" runat="server" 
                            TypeName="NAS.DAL.Inventory.Journal.InventoryJournal"
                            Criteria="[JournalType] = 'A' 
                                      And [InventoryTransactionId!Key] = ? 
                                      And [Credit] &lt;&gt; 0.0 And [Debit] = 0.0 
                                      And ( [RowStatus] = 1 Or [RowStatus] = 4 )">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryTransactionId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="MovingInventoryCommandXDS" runat="server" 
                            Criteria="[InventoryCommandId] = ?" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommand">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="InputInventoryCommandXDS" runat="server" 
                            Criteria="[InventoryCommandId] = ?" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommand">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="OutputInventoryCommandXDS" runat="server" 
                            Criteria="[InventoryCommandId] = ?" 
                            TypeName="NAS.DAL.Inventory.Command.InventoryCommand">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="FromInventoryXDS" runat="server" Criteria="[RowStatus] = 1s" 
                            TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
                            <CriteriaParameters>
                                <asp:Parameter Name="InventoryCommandId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="ToInventoryXDS" runat="server" Criteria="[RowStatus] = 1s" 
                            TypeName="NAS.DAL.Nomenclature.Inventory.Inventory">
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
                        <dx:ASPxPopupControl ID="PopupMovingInventoryCommandActor" runat="server"
                            PopupElementID="popupMovingAnchor"
                            Height="268px" Width="650px" RenderMode="Classic"
                            HeaderText="Thông tin đối tượng phiếu chuyển kho">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdMovingInventoryCommandActor" DataSourceID="MovingInventoryCommandActorXDS" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="InventoryCommandActorId"
                                        Width="100%">
                                        <ClientSideEvents RowClick="function(s, e){
                                                                        s.StartEditRow(e.visibleIndex);}" />
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
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsPager ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings VerticalScrollBarMode="Visible" />
                                    </dx:ASPxGridView>
                                    <dx:XpoDataSource ID="MovingInventoryCommandActorXDS" runat="server" Criteria="[InventoryCommandId] = ?" 
                                        TypeName="NAS.DAL.Inventory.Command.InventoryCommandActor">
                                        <CriteriaParameters>
                                            <asp:Parameter Name="InventoryCommandId" />
                                        </CriteriaParameters>
                                    </dx:XpoDataSource>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                        <dx:ASPxPopupControl ID="PopupOutputInventoryCommandActor" runat="server"
                            PopupElementID="popupOutputAnchor"
                            Height="268px" Width="650px" RenderMode="Classic"
                            HeaderText="Thông tin đối tượng phiếu chuyển kho">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdOutputInventoryCommandActor" DataSourceID="OutputInventoryCommandActorXDS" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="InventoryCommandActorId"
                                        Width="100%">
                                        <ClientSideEvents RowClick="function(s, e){
                                                                        s.StartEditRow(e.visibleIndex);}" />
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
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsPager ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings VerticalScrollBarMode="Visible" />
                                    </dx:ASPxGridView>
                                    <dx:XpoDataSource ID="OutputInventoryCommandActorXDS" runat="server" Criteria="[InventoryCommandId] = ?" 
                                        TypeName="NAS.DAL.Inventory.Command.InventoryCommandActor">
                                        <CriteriaParameters>
                                            <asp:Parameter Name="InventoryCommandId" />
                                        </CriteriaParameters>
                                    </dx:XpoDataSource>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                        <dx:ASPxPopupControl ID="PopupInputInventoryCommandActor" runat="server"
                            PopupElementID="popupInputAnchor"
                            Height="268px" Width="650px" RenderMode="Classic"
                            HeaderText="Thông tin đối tượng phiếu chuyển kho">
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdInputInventoryCommandActor" DataSourceID="InputInventoryCommandActorXDS" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="InventoryCommandActorId"
                                        Width="100%">
                                        <ClientSideEvents RowClick="function(s, e){
                                                                        s.StartEditRow(e.visibleIndex);}" />
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
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsPager ShowEmptyDataRows="True">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings VerticalScrollBarMode="Visible" />
                                    </dx:ASPxGridView>
                                    <dx:XpoDataSource ID="InputInventoryCommandActorXDS" runat="server" Criteria="[InventoryCommandId] = ?" 
                                        TypeName="NAS.DAL.Inventory.Command.InventoryCommandActor">
                                        <CriteriaParameters>
                                            <asp:Parameter Name="InventoryCommandId" />
                                        </CriteriaParameters>
                                    </dx:XpoDataSource>
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