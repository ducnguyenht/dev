<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uAuditingInventoryCommand.ascx.cs"
    Inherits="WebModule.Warehouse.Command.PopupCommand.AuditingInventoryCommand.uAuditingInventoryCommand" %>

<%@ Register src="../EdittingOutputInventoryCommand/uEdittingOutputInventoryCommand.ascx" tagname="uEdittingOutputInventoryCommand" tagprefix="uc1" %>



<%@ Register src="../EdittingInputInventoryCommand/uEdittingInputInventoryCommand.ascx" tagname="uEdittingInputInventoryCommand" tagprefix="uc2" %>



<%@ Register src="../../../Report/UserControls/u05_VT.ascx" tagname="u05_VT" tagprefix="uc3" %>



<script type="text/javascript">
    function btnInventoryCommandActor_Click(s, e) {
        formInventoryCommandActor.ShowAtElementByID('btnInventoryCommandActor');
        formInventoryCommandActor.PerformCallback();
    }

    function btnPrintCommand_Click(s, e) {
        //xPopupControl.Show();
        report_panel.PerformCallback("print");
        
    }

    function grdDetailJournal_FocusedRowChanged(s, e) {
        if (grdDetailJournal.GetFocusedRowIndex() >= 0) {
            var id = grdDetailJournal.GetRowKey(grdDetailJournal.GetFocusedRowIndex());
            cpProperty.PerformCallback('properties|' + id);
        }
    }

    function grdDetailJournal_EndCallback(s, e) {
        if (s.cpEnableProperties) {
            cpProperty.PerformCallback('status|' + s.cpEnableProperties);
            delete (s.cpEnableProperties);
        }

        if (s.cpRefreshProperties) {
            cpProperty.PerformCallback('modify|' + s.cpRefreshProperties);
            delete (s.cpRefreshProperties);
        }
    }

    function grdDetailJournal_CustomButtonClick(s, e) {

    }

    var keyValue;
    function suggestionClick(s, e) {
        //callbackSuggestion.SetContentHtml("");
        popupSuggestion.ShowAtElement(s);
        keyValue = e;
    }

    function popupSuggestion_Shown(s, e) {
        callbackSuggestion.PerformCallback('open|' + keyValue);
    }

    function popupSuggestion_Closing(s, e) {
        callbackSuggestion.PerformCallback('close|' + keyValue);
    }

    function callbackSuggestion_EndCallback(s, e) {
        if (s.cpRefreshGrid) {
            grdDetailJournal.Refresh();
            delete (s.cpRefreshGrid);
        }
    }

    function report_panel_EndCallback(s, e) {
        if (s.cpShowReport) {
            xPopupControl.Show();
            delete (s.cpShowReport);
        }
    }

</script>
<dx:aspxloadingpanel id="ldpnInventoryCommandCheck" runat="server" horizontalalign="Center"
    text="Đang xử lý..." verticalalign="Middle" modal="True">
    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
</dx:aspxloadingpanel>
<dx:aspxcallbackpanel id="cpInventoryCommandCheck" clientinstancename="cpInventoryCommandCheck"
    runat="server" showloadingpanel="False" showloadingpanelimage="False" width="100%"
    oncallback="cpAuditInventoryCommand_OnCallback">
    <ClientSideEvents EndCallback="cpInventoryCommandCheck_EndCallback" />
<ClientSideEvents EndCallback="cpInventoryCommandCheck_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupInventoryCommandCheck" runat="server" 
                AllowDragging="True" AllowResize="True"
                AppearAfter="200"
                HeaderText="Thông tin biên bản kiểm kho" Height="600px" PopupHorizontalAlign="WindowCenter" 
                PopupVerticalAlign="WindowCenter" Width="1100px" ShowFooter="True" ShowMaximizeButton="True"
                CloseAction="CloseButton" 
                ScrollBars="Auto"
                LoadingDivStyle-BackColor="Transparent"
                ModalBackgroundStyle-BackColor="Transparent"
                ShowSizeGrip="False"
                Maximized="true"
                Modal="True" ShowLoadingPanel="False" ShowLoadingPanelImage="False">   
<ClientSideEvents AfterResizing="function(s, e){
                    SplitterDetailAuditingInventoryCommand.AdjustControl();
                }"></ClientSideEvents>
<LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
<ModalBackgroundStyle BackColor="Transparent"></ModalBackgroundStyle>
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
                            <div style="float: left;margin-left: 4px;">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnModifyInCommand" AutoPostBack="false" runat="server" Text="Điều chỉnh nhập"
                                    Wrap="False" CausesValidation="true" oninit="btnModifyInCommand_Init">                                     
                                       <ClientSideEvents Click="btnPrintCommand_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left;margin-left: 4px;">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnModifyOutCommand" AutoPostBack="false" runat="server" Text="Điều chỉnh xuất"
                                    Wrap="False" CausesValidation="true" onload="btnModifyOutCommand_Load" 
                                    onprerender="btnModifyOutCommand_PreRender" 
                                    oninit="btnModifyOutCommand_Init">                                     
                                       <ClientSideEvents Click="btnPrintCommand_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px;">
                                <!-- Places button here -->
                               <dx:ASPxButton ID="btnPrintCommand" AutoPostBack="false" runat="server" Text="In Phiếu"
                                    Wrap="False" oninit="btnPrintCommand_Init">
                                       <ClientSideEvents Click="btnPrintCommand_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Print" />
                                    </Image>
                                </dx:ASPxButton>                          
                            </div>
                        </div>
                        <div style="float: right">                           
                            <div style="float: left;">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnSaveAuditCommand" ClientInstanceName="btnSaveCommand" AutoPostBack="false" runat="server" Text="Lưu lại"
                                    Wrap="False" CausesValidation="true">
                                    <ClientSideEvents Click="btnSaveAuditCommand_Click"></ClientSideEvents>
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCloseAuditCommandPopup" AutoPostBack="false" runat="server" 
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
                </FooterContentTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div style="height: 100%; width: 100%; overflow: hidden">
                        <dx:ASPxFormLayout ID="frmMaster" runat="server"
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chứng từ" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã biên bản" FieldName="Code" Width="50%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtCode" runat="server" Font-Bold="True" 
                                                        ClientInstanceName="txtCode" Width="200px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">                                                            
<RequiredField IsRequired="True" ErrorText="Chưa nhập mã biên bản !"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tên biên bản">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" 
                                                        Font-Bold="True" Width="400px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">                                                            
<RequiredField IsRequired="True" ErrorText="Chưa nhập tên biên bản !"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Thời điểm" FieldName="IssueDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="txtIssuedDate" runat="server" 
                                                        ClientInstanceName="txtIssuedDate">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" SetFocusOnError="True">
                                                            <RequiredField ErrorText="Chưa chọn thời điểm kiểm kê" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa chọn thời điểm kiểm k&#234;"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Mục đích" FieldName="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxMemo ID="txtDescription" runat="server" 
                                                        ClientInstanceName="txtDescription" Height="50px" Width="400px">
                                                    </dx:ASPxMemo>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ban kiểm kho">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                    <table>
                                                        <tr>
                                                            <td id="popupActor">
                                                                <dx:ASPxButton ID="btnInventoryCommandActor" runat="server" 
                                                                    AutoPostBack="False" ClientInstanceName="btnInventoryCommandActor" Text="..." 
                                                                    UseSubmitBehavior="False" Width="10px">                                                                   
                                                                    <ClientSideEvents Click="btnInventoryCommandActor_Click"></ClientSideEvents>
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Kho" Name="Inventory">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cboInventory" runat="server" CallbackPageSize="10" 
                                                        ClientInstanceName="cboInventory" EnableCallbackMode="True" 
                                                        IncrementalFilteringMode="Contains" 
                                                        OnItemRequestedByValue="cboInventory_ItemRequestedByValue" 
                                                        OnItemsRequestedByFilterCondition="cboInventory_ItemsRequestedByFilterCondition" 
                                                        TextField="Code" TextFormatString="{0}-{1}" ValueField="InventoryId" 
                                                        ValueType="System.Guid" Width="400px"
                                                        OnInit="cboInventory_Init" ShowLoadingPanel="False" 
                                                        ShowLoadingPanelImage="False">
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="Code" Caption="Mã kho" Width="100px" />
                                                            <dx:ListBoxColumn Caption="Tên kho" FieldName="Name" Width="150px" />
                                                            <dx:ListBoxColumn Caption="Địa chỉ" FieldName="Address" />
                                                        </Columns>
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">                                                            
                                                            <RequiredField IsRequired="True" ErrorText="Chưa chọn kho kiểm kê !"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:TabbedLayoutGroup Width="100%">
                                    <Items>
                                        <dx:LayoutItem Caption="Danh mục hàng hóa">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSplitter ID="SplitterDetail" ClientInstanceName="SplitterDetailAuditingInventoryCommand" runat="server" Height="400px" Width="100%" AllowResize="true">
                                                        <Panes>
                                                            <dx:SplitterPane Size="78%" ShowCollapseForwardButton="True" AllowResize="True">
                                                                <ContentCollection>
                                                                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                                                        <dx:ASPxGridView KeyboardSupport="True" ID="grdDetailJournal" 
                                                                            runat="server"
                                                                            AutoGenerateColumns="False"
                                                                            Width="100%"
                                                                            KeyFieldName="InventoryAuditItemUnitId" DataSourceID="InventoryAuditItemUnitXDS" 
                                                                            OnRowDeleting="grdDetailJournal_RowDeleting" 
                                                                            OnRowInserting="grdDetailJournal_RowInserting" 
                                                                            OnRowUpdating="grdDetailJournal_RowUpdating" 
                                                                            OnRowValidating="grdDetailJournal_RowValidating" 
                                                                            ClientInstanceName="grdDetailJournal" 
                                                                            OnCellEditorInitialize="grdDetailJournal_CellEditorInitialize" 
                                                                            OnRowInserted="grdDetailJournal_RowInserted" 
                                                                            OnInitNewRow="grdDetailJournal_InitNewRow" 
                                                                            OnStartRowEditing="grdDetailJournal_StartRowEditing" 
                                                                            OnCommandButtonInitialize="grdDetailJournal_CommandButtonInitialize" 
                                                                            OnParseValue="grdDetailJournal_ParseValue" 
                                                                            OnCustomUnboundColumnData="grdDetailJournal_CustomUnboundColumnData">

                                                                            <ClientSideEvents FocusedRowChanged="grdDetailJournal_FocusedRowChanged" 
                                                                                EndCallback="grdDetailJournal_EndCallback" 
                                                                                CustomButtonClick="grdDetailJournal_CustomButtonClick">
                                                                            </ClientSideEvents>
                                                                            <Columns>
                                                                                <dx:GridViewDataComboBoxColumn Caption="Mã hàng hóa" FieldName="ItemUnitId!Key" 
	                                                                                ShowInCustomizationForm="True" VisibleIndex="0" Width="150px">
	                                                                                <PropertiesComboBox CallbackPageSize="10" EnableCallbackMode="True" 
		                                                                                IncrementalFilteringMode="Contains" TextFormatString="{0} - {1}" 
		                                                                                ValueField="ItemUnitId" ValueType="System.Guid" textfield="Code"
                                                                                        OnItemRequestedByValue="comboItemUnit_ItemRequestedByValue"
                                                                                        OnItemsRequestedByFilterCondition="comboItemUnit_ItemsRequestedByFilterCondition"><Columns><dx:ListBoxColumn Caption="Mã Hàng Hóa" FieldName="ItemId.Code" 
				                                                                                Width="150px" /><dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="ItemId.Name" 
				                                                                                Width="300px" /><dx:ListBoxColumn Caption="Nhà sản xuất" 
				                                                                                FieldName="ItemId.ManufacturerOrgId.Name" Width="150px" /><dx:ListBoxColumn Caption="Đvt" FieldName="UnitId.Name" 
				                                                                                Width="100px" /><dx:ListBoxColumn FieldName="ItemUnitId" Width="0px" /></Columns><ValidationSettings><RequiredField IsRequired="true" ErrorText="Bắt buộc nhập mã hàng hóa" /></ValidationSettings></PropertiesComboBox>
                                                                                </dx:GridViewDataComboBoxColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" 
                                                                                    FieldName="ItemUnitId.ItemId.Name" VisibleIndex="24" Width="0px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="ItemUnitId.UnitId.Name" 
                                                                                    VisibleIndex="1" Width="50px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số lượng theo chứng từ" FieldName="BookingAmount" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="1" Width="80px">
                                                                                    <PropertiesTextEdit DisplayFormatString="n0" Width="100%"><ValidationSettings><RequiredField ErrorText="Bắt buộc số lượng" IsRequired="True" /></ValidationSettings></PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colBookingAmount" runat="server" 
                                                                                            ClientInstanceName="colBookingAmount" Height="21px"
                                                                                            oninit="colBookingAmount_Init" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle Wrap="True" />
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="Số lượng thực tế" 
                                                                                    FieldName="RealAmount" ShowInCustomizationForm="True" VisibleIndex="2" 
                                                                                    Width="80px">
                                                                                    <PropertiesTextEdit DisplayFormatString="n0" Width="100%"><ValidationSettings><RequiredField ErrorText="Bắt buộc Số lượng" IsRequired="True" /></ValidationSettings></PropertiesTextEdit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colRealAmount" runat="server" 
                                                                                            ClientInstanceName="colRealAmount" Height="21px" 
                                                                                            oninit="colRealAmount_Init" Width="100%" />
                                                                                    </EditItemTemplate>
                                                                                    <HeaderStyle Wrap="True" />
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="13">
                                                                                    <EditButton Visible="True">
                                                                                        <Image>
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                                        </Image>
                                                                                    </EditButton>
                                                                                    <NewButton Visible="True">
                                                                                        <Image>
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                                        </Image>
                                                                                    </NewButton>
                                                                                    <DeleteButton Visible="True">
                                                                                        <Image>
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                                        </Image>
                                                                                    </DeleteButton>
                                                                                    <CancelButton>
                                                                                        <Image>
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                                        </Image>
                                                                                    </CancelButton>
                                                                                    <UpdateButton>
                                                                                        <Image>
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                                        </Image>
                                                                                    </UpdateButton>
                                                                                </dx:GridViewCommandColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="InventoryAuditArtifactId!Key" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="22" 
                                                                                    Width="0px">
                                                                                    <PropertiesTextEdit Width="0px"></PropertiesTextEdit>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="RowStatus" ReadOnly="True" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="20" Width="0px">
                                                                                    <PropertiesTextEdit Width="0px"></PropertiesTextEdit>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn FieldName="InventoryAuditItemUnitId" ReadOnly="True" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="16" Width="0px">
                                                                                    <PropertiesTextEdit Width="0px"></PropertiesTextEdit>
                                                                                </dx:GridViewDataTextColumn>
                                                                                 <dx:GridViewDataColumn Caption="Đề xuất" VisibleIndex="11" Width="40px" 
                                                                                    Name="dexuat">
                                                                                    <DataItemTemplate>                                                                                        
                                                                                        <a href="javascript:void(0);" onclick="suggestionClick(this, '<%# Container.KeyValue %>')">
                                                                                            <img id="img" runat="server" alt='' src='<%# GetImageName(Container.KeyValue) %>'/>
                                                                                        </a></DataItemTemplate>
                                                                                     <CellStyle HorizontalAlign="Center">
                                                                                     </CellStyle>
                                                                                </dx:GridViewDataColumn>
                                                                                <dx:GridViewDataSpinEditColumn Caption="Số lượng MPC" Name="LossQuantity" 
                                                                                    ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="7" 
                                                                                    FieldName="LossQuantity">
                                                                                    <propertiesspinedit displayformatstring="g"></propertiesspinedit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colLossQuantity" runat="server" 
                                                                                            ClientInstanceName="colLossQuantity" MaxValue="999999999999" Number="0" 
                                                                                            Width="100%" OnInit="colLossQuantity_Init">
                                                                                        </dx:ASPxSpinEdit>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataSpinEditColumn>
                                                                                <dx:GridViewDataSpinEditColumn Caption="Điều chỉnh MPC" Name="LossProcess" 
                                                                                    ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="8" 
                                                                                    FieldName="LossProcess">
                                                                                    <propertiesspinedit displayformatstring="g"></propertiesspinedit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colLossProcess" runat="server" 
                                                                                            ClientInstanceName="colLossProcess" MinValue="-999999999999" Number="0" 
                                                                                            Width="100%" OnInit="colLossProcess_Init">
                                                                                        </dx:ASPxSpinEdit>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataSpinEditColumn>
                                                                                <dx:GridViewDataSpinEditColumn Caption="Số lượng KPC" Name="LessQuantity" 
                                                                                    ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="5" 
                                                                                    FieldName="LessQuantity">
                                                                                    <propertiesspinedit displayformatstring="g"></propertiesspinedit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colLessQuantity" runat="server" 
                                                                                            ClientInstanceName="colLessQuantity" MaxValue="999999999999" Number="0" 
                                                                                            Width="100%" OnInit="colLessQuantity_Init">
                                                                                        </dx:ASPxSpinEdit>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataSpinEditColumn>
                                                                                <dx:GridViewDataSpinEditColumn Caption="Điều chỉnh KPC" Name="LessProcess" 
                                                                                    ShowInCustomizationForm="True" UnboundType="Decimal" VisibleIndex="6" 
                                                                                    FieldName="LessProcess">
                                                                                    <propertiesspinedit displayformatstring="g"></propertiesspinedit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colLessProcess" runat="server" 
                                                                                            ClientInstanceName="colLessProcess" MinValue="-999999999999" Number="0" 
                                                                                            Width="100%" OnInit="colLessProcess_Init">
                                                                                        </dx:ASPxSpinEdit>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataSpinEditColumn>
                                                                                <dx:GridViewDataSpinEditColumn Caption="Điều chỉnh CL" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="4" 
                                                                                    FieldName="ProcessingBiasAmount">
                                                                                    <propertiesspinedit displayformatstring="g"></propertiesspinedit>
                                                                                    <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colBalanceProcess" runat="server" 
                                                                                            ClientInstanceName="colBalanceProcess" MaxValue="999999999999" 
                                                                                            MinValue="-999999999999" Number="0"
                                                                                            OnInit="colBalanceProcess_Init" Width="100%">
                                                                                        </dx:ASPxSpinEdit>
                                                                                    </EditItemTemplate>
                                                                                </dx:GridViewDataSpinEditColumn>
                                                                                <dx:GridViewDataSpinEditColumn Caption="Số lượng CL" Name="Balance" 
                                                                                    ShowInCustomizationForm="True" VisibleIndex="3" FieldName="Balance" 
                                                                                    UnboundType="Decimal" ReadOnly="true">
                                                                                     <propertiesspinedit displayformatstring="g"></propertiesspinedit>
                                                                                     <EditItemTemplate>
                                                                                        <dx:ASPxSpinEdit ID="colBalance" ClientInstanceName="colBalance" runat="server"
                                                                                         OnInit="colBalance_Init" Width="100%" ReadOnly="true">
                                                                                        </dx:ASPxSpinEdit>
                                                                                    </EditItemTemplate>                                                                                    
                                                                                </dx:GridViewDataSpinEditColumn>
                                                                            </Columns>

<SettingsBehavior ConfirmDelete="True" ColumnResizeMode="Control" AllowFocusedRow="True"></SettingsBehavior>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFooter="True"></Settings>

<SettingsText ConfirmDelete="C&#243; chắc chắn muốn x&#243;a th&#244;ng tin n&#224;y?"></SettingsText>

<SettingsLoadingPanel ShowImage="False"></SettingsLoadingPanel>

                                                                            <Styles>
                                                                                <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
                                                                                </Header>
                                                                                <Footer Font-Bold="True">
                                                                                </Footer>
                                                                                <CommandColumn Spacing="4px">
                                                                                </CommandColumn>
                                                                            </Styles>
                                                                        </dx:ASPxGridView>
                                                                    </dx:SplitterContentControl>
                                                                </ContentCollection>
                                                            </dx:SplitterPane>
                                                            <dx:SplitterPane MinSize="260px" Size="22%" ShowCollapseForwardButton="True" AllowResize="True">
                                                                <ContentCollection>
                                                                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                                                        <dx:ASPxCallbackPanel ID="cpProperty" runat="server" 
                                                                            ClientInstanceName="cpProperty" OnCallback="cpProperty_Callback" Width="100%">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxFormLayout ID="frmDetailOfLine" runat="server" Height="100%" 
                                                                                        Width="95%" OnInit="frmDetailOfLine_Init">
                                                                                        <Items>
                                                                                            <dx:LayoutGroup Caption="Chi tiết kiểm kho">
                                                                                                <Items>
                                                                                                    <dx:LayoutItem Caption="Nhà sản xuất">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblManufacturer" runat="server" 
                                                                                                                    ClientInstanceName="lblManufacturer">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Đơn giá">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblPrice" runat="server" ClientInstanceName="lblPrice">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="SL chênh lệch">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblBalance" runat="server" ClientInstanceName="lblBalance">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Xử lý" Visible="False">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxSpinEdit ID="txtBalanceProcess" runat="server" 
                                                                                                                    ClientInstanceName="txtBalanceProcess" MinValue="-999999999999" Number="0" 
                                                                                                                    OnInit="txtBalanceProcess_Init" Width="120px" MaxValue="999999999999">
                                                                                                                </dx:ASPxSpinEdit>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="SL kém phẩm chất" Visible="False">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxSpinEdit ID="txtLessQuanlity" runat="server" 
                                                                                                                    ClientInstanceName="txtLessQuanlity" Number="0" Width="120px" 
                                                                                                                    OnInit="txtLessQuanlity_Init" MaxValue="999999999999">
                                                                                                                </dx:ASPxSpinEdit>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Xử lý " Visible="False">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxSpinEdit ID="txtLossProcess" runat="server" 
                                                                                                                    ClientInstanceName="txtLossProcess" Number="0" Width="120px" 
                                                                                                                    OnInit="txtLossProcess_Init" MinValue="-999999999999">
                                                                                                                </dx:ASPxSpinEdit>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="SL mất phẩm chất" Visible="False">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxSpinEdit ID="txtLossQuanlity" runat="server" 
                                                                                                                    ClientInstanceName="txtLossQuanlity" Number="0" Width="120px" 
                                                                                                                    OnInit="txtLossQuanlity_Init" MaxValue="999999999999">
                                                                                                                </dx:ASPxSpinEdit>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Xử lý " Visible="False">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxSpinEdit ID="txtLessProcess" runat="server" 
                                                                                                                    ClientInstanceName="txtLessProcess" MinValue="-999999999999" Number="0" 
                                                                                                                    OnInit="txtLessProcess_Init" Width="120px">
                                                                                                                </dx:ASPxSpinEdit>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Tổng SL cần điều chỉnh">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblTotalModify" runat="server" 
                                                                                                                    ClientInstanceName="lblTotalModify">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Thành tiền sổ sách">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblBookAmount" runat="server" 
                                                                                                                    ClientInstanceName="lblBookAmount">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Thành tiền thực tế">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblRealAmount" runat="server" 
                                                                                                                    ClientInstanceName="lblRealAmount">
                                                                                                                </dx:ASPxLabel>
                                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                                        </LayoutItemNestedControlCollection>
                                                                                                    </dx:LayoutItem>
                                                                                                    <dx:LayoutItem Caption="Thành tiền chênh lệch">
                                                                                                        <LayoutItemNestedControlCollection>
                                                                                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                                                                                SupportsDisabledAttribute="True">
                                                                                                                <dx:ASPxLabel ID="lblBalanceAmount" runat="server" 
                                                                                                                    ClientInstanceName="lblBalanceAmount">
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
                                                        <Styles>
                                                            <Pane>
<Paddings Padding="0px"></Paddings>
                                                            </Pane>
                                                        </Styles>
                                                    </dx:ASPxSplitter>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:TabbedLayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        </div>                       
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:aspxcallbackpanel>
<dx:xpodatasource id="InventoryAuditItemUnitXDS" runat="server" typename="NAS.DAL.Inventory.Audit.InventoryAuditItemUnit"
    criteria="[InventoryAuditArtifactId] = ?">
</dx:xpodatasource>
<dx:ASPxPopupControl ID="popupSuggestion" runat="server" 
    ClientInstanceName="popupSuggestion" Height="120px" RenderMode="Lightweight" 
    Width="600px" HeaderText="Đề xuất">
    <ClientSideEvents Closing="popupSuggestion_Closing" 
        Shown="popupSuggestion_Shown" CloseUp="popupSuggestion_Closing" />
    <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxCallbackPanel ID="callbackSuggestion" runat="server" 
        ClientInstanceName="callbackSuggestion" 
        OnCallback="callbackSuggestion_Callback" Width="100%">
        <ClientSideEvents EndCallback="callbackSuggestion_EndCallback" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxHtmlEditor ID="txtSuggestion" runat="server" Width="100%" 
                    ClientInstanceName="txtSuggestion" OnInit="txtSuggestion_Init">
                </dx:ASPxHtmlEditor>          
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
<dx:aspxpopupcontrol id="formInventoryCommandActor" runat="server" height="268px"
    rendermode="Lightweight" width="633px" clientinstancename="formInventoryCommandActor"
    popupelementid="bBillActor" headertext="Ban kiểm kê" onwindowcallback="formInventoryCommandActor_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grdInventoryCommandActor" runat="server" 
                AutoGenerateColumns="False" ClientInstanceName="grdInventoryCommandActor"
                DataSourceID="InventoryCommandActorXDS" 
                KeyFieldName="InventoryCommandActorId" OnRowUpdating="grdInventoryCommandActor_RowUpdating"
                Width="100%" OnRowDeleting="grdInventoryCommandActor_RowDeleting" 
                OnRowInserting="grdInventoryCommandActor_RowInserting">
                <Columns>
                    <dx:GridViewDataComboBoxColumn Caption="Chức vụ" 
                        FieldName="InventoryCommandActorTypeId!Key" ReadOnly="True" 
                        ShowInCustomizationForm="True" VisibleIndex="1" Width="200px">
                        <PropertiesComboBox TextField="Description" TextFormatString="{0}" 
                            ValueField="InventoryCommandActorTypeId" ValueType="System.Guid"
                            OnItemRequestedByValue="colInventoryCommandOnItemRequestedByValue" 
                            OnItemsRequestedByFilterCondition="colInventoryCommandOnItemsRequestedByFilterCondition">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Description" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Tên đối tượng" FieldName="PersonId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="2" Width="300px">
                        <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                            TextField="Name" TextFormatString="{0}-{1}" ValueField="PersonId" ValueType="System.Guid"
                            OnItemRequestedByValue="colPersonOnItemRequestedByValue" OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition">
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã đối tượng" FieldName="Code" />
                                <dx:ListBoxColumn Caption="Tên đối tượng" FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                        VisibleIndex="3" Width="70px">
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
                <Settings HorizontalScrollBarMode="Visible" VerticalScrollBarMode="Visible" />
                <SettingsEditing Mode="Inline"></SettingsEditing>
                <Settings VerticalScrollBarMode="Visible"></Settings>
                <SettingsLoadingPanel Mode="Disabled" ShowImage="False" />
            </dx:ASPxGridView>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:aspxpopupcontrol>
<dx:xpodatasource id="InventoryCommandActorXDS" runat="server" typename="NAS.DAL.Inventory.Command.InventoryCommandActor"
    criteria="[InventoryCommandId] = ?">
</dx:xpodatasource>
<dx:ASPxCallbackPanel ID="cpInventoryAction" runat="server" 
    ClientInstanceName="cpInventoryAction" oncallback="cpInventoryAction_Callback" 
    Width="100%">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <uc2:uEdittingInputInventoryCommand ID="uEdittingInputInventoryCommand1" 
        runat="server" />
            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px">
                <PanelCollection>
                    <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            <uc1:uEdittingOutputInventoryCommand ID="uEdittingOutputInventoryCommand1" 
        runat="server" />
        </dx:PanelContent>    
    </PanelCollection>
</dx:ASPxCallbackPanel>
    

<dx:ASPxCallbackPanel ID="report_panel" runat="server" 
    ClientInstanceName="report_panel" oncallback="report_panel_Callback" 
    Width="100%">
    <ClientSideEvents EndCallback="report_panel_EndCallback" />
    <PanelCollection>
<dx:PanelContent runat="server" SupportsDisabledAttribute="True">
    <uc3:u05_VT ID="u05_VT1" runat="server" />
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>

    

