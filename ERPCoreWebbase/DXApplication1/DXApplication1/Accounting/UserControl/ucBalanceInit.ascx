<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucBalanceInit.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.ucBalanceInit" %>
<%@ Register src="../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx" tagname="NASCustomFieldDataGridView" tagprefix="uc1" %>
<%@ Register src="../../Warehouse/UserControl/MessageBox.ascx" tagname="MessageBox" tagprefix="uc2" %>
<script type="text/javascript">

    function AccountList_Init(s, e) {
        //Using standard shortcut key and standard shortcut behavior
        Utils.AttachStandardShortcutToGridview(s);
        shortcut.remove("Insert");
        shortcut.remove("Delete");
    }
    function AccountList_RowClick(s, e) {
        s.SetFocusedRowIndex(e.visibleIndex);
        s.StartEditRow(e.visibleIndex);
    }
    function grdBalanceLine_EndCallback(s, e) {  
        if (s.cpRefreshTree) {
            tgrdAccountBalance.PerformCallback();
            delete (s.cpRefreshTree);
        }

        if (s.cpAccountInvalid) {
            if (!ASPxClientEdit.ValidateEditorsInContainerById('BalanceInitContainer')) {
                e.processOnServer = false;
                return;
            }
            delete (s.cpAccountInvalid);
        }

        if (s.cpCurrencyInvalid) {
            if (!ASPxClientEdit.ValidateEditorsInContainerById('BalanceInitContainer')) {
                e.processOnServer = false;
                return;
            }
            delete (s.cpCurrencyInvalid);
        }

        if (s.cpRefreshSummary) {
            tgrdAccountBalance.PerformCallback();
            delete (s.cpRefreshSummary);
        }

      
    }

    function grdBalanceLine_Init(s, e) {
        //Press F2 to show edit popup
        Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
            var focusedRowIndex = s.GetFocusedRowIndex();
            var key = s.GetRowKey(focusedRowIndex);
            clientAction = 'Edit';
            //UpdateItemAction(key);
            grdBalanceLine.StartEditRow(focusedRowIndex);
        });
        //Press Insert to show insert popup
        Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
            clientAction = 'Add';
            //cpItemEdit.PerformCallback('Add');
            grdBalanceLine.AddNewRow();
        });
        //Press Delete to delete record
        Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
            var focusedRowIndex = s.GetFocusedRowIndex();
            var key = s.GetRowKey(focusedRowIndex);
            grdBalanceLine.DeleteRow(focusedRowIndex);
        });

        s.GetMainElement().focus();
    }
   
    function tgrdAccountBalance_EndCallback(s, e) {
      
    }

    function cpMessageBox_Callback(s, e) {
        alert('test');
        if (s.cpWarning) {
            formMessageBox.Show();
            delete (s.cpWarning);
        }
    }
</script>
<div id="BalanceInitContainer" style="width:100%">
    <dx:ASPxFormLayout ID="ASPxFormLayout3" runat="server">
        <Items>
            <dx:LayoutItem Caption="Chu kỳ kế toán">
                <layoutitemnestedcontrolcollection>
                    <dx:LayoutItemNestedControlContainer runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxComboBox ID="cboAccountPeriod" runat="server" 
                            ClientInstanceName="cboAccountPeriod" EnableCallbackMode="True" 
                            IncrementalFilteringMode="Contains" OnInit="cboAccountPeriod_Init" 
                            OnItemRequestedByValue="cboAccountPeriod_ItemRequestedByValue" 
                            OnItemsRequestedByFilterCondition="cboAccountPeriod_ItemsRequestedByFilterCondition" 
                            TextField="Code" TextFormatString="{0} - {1}" ValueField="AccountingPeriodId" 
                            ValueType="System.Guid" Width="400px">
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã chu kỳ" FieldName="Code" Width="100px" />
                                <dx:ListBoxColumn Caption="Tên chu kỳ" FieldName="Description" Width="100px" />
                                <dx:ListBoxColumn Caption="Từ ngày" FieldName="FromDateTime" Width="100px" />
                                <dx:ListBoxColumn Caption="Đến ngày" FieldName="ToDateTime" Width="100px" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </layoutitemnestedcontrolcollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
<dx:XpoDataSource ID="balanceLineXDS" runat="server" 
    TypeName="NAS.DAL.Accounting.Journal.GeneralJournalBalanceForward" 
    Criteria="">
</dx:XpoDataSource>
<dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Width="100%">
    <Items>
        <dx:LayoutGroup ColCount="2" ShowCaption="False">
            <Items>
                <dx:LayoutItem Caption="Tài khoản">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="cboBalanceInitAccount" runat="server" 
                                ClientInstanceName="cboBalanceInitAccount" EnableCallbackMode="True" 
                                IncrementalFilteringMode="Contains" OnInit="cboBalanceInitAccount_Init" 
                                OnItemRequestedByValue="cboBalanceInitAccount_ItemRequestedByValue" 
                                OnItemsRequestedByFilterCondition="cboBalanceInitAccount_ItemsRequestedByFilterCondition" 
                                TextField="Code" TextFormatString="{0} - {1}" ValueField="AccountId" 
                                ValueType="System.Guid" Width="100%" CallbackPageSize="20" 
                                IncrementalFilteringDelay="0" LoadingPanelDelay="0">
                                <clientsideevents selectedindexchanged="cboBalanceInitAccount_SelectedIndexChanged" />
<ClientSideEvents SelectedIndexChanged="cboBalanceInitAccount_SelectedIndexChanged"></ClientSideEvents>
                                <Columns>
                                    <dx:ListBoxColumn Caption="Tài khoản" FieldName="Code" Width="100px" />
                                    <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" Width="200px" />
                                </Columns>
                                <ValidationSettings SetFocusOnError="True" ErrorText="Chưa chọn tài khoản">
                                    <RequiredField ErrorText="Chưa chọn tài khoản" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa chọn t&#224;i khoản"></RequiredField>
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tiền tệ">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxComboBox ID="cboBalanceInitCurrency" runat="server" 
                                ClientInstanceName="cboBalanceInitCurrency" EnableCallbackMode="True" 
                                IncrementalFilteringMode="Contains" 
                                OnItemRequestedByValue="cboBalanceInitCurrency_ItemRequestedByValue" 
                                OnItemsRequestedByFilterCondition="cboBalanceInitCurrency_ItemsRequestedByFilterCondition" 
                                TextField="Code" TextFormatString="{0} - {1}" ValueField="CurrencyId" 
                                ValueType="System.Guid" Width="100%">
                                <ClientSideEvents SelectedIndexChanged="cboBalanceInitCurrency_SelectedIndexChanged" />
<ClientSideEvents SelectedIndexChanged="cboBalanceInitCurrency_SelectedIndexChanged"></ClientSideEvents>
                                <Columns>
                                    <dx:ListBoxColumn Caption="Mã tiền tệ" FieldName="Code" Width="100px" />
                                    <dx:ListBoxColumn Caption="Tên tiền tệ" FieldName="Name" />
                                </Columns>
                                <ValidationSettings SetFocusOnError="True" ErrorText="Chưa chọn tiền tệ">
                                    <RequiredField ErrorText="Chưa chọn tiền tệ" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa chọn tiền tệ"></RequiredField>
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ColSpan="2" ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxGridView ID="grdBalanceLine" runat="server" AutoGenerateColumns="False" 
                                ClientInstanceName="grdBalanceLine" Width="100%" 
                                DataSourceID="balanceLineXDS" KeyFieldName="GeneralJournalId" 
                                OnCellEditorInitialize="grdBalanceLine_CellEditorInitialize" 
                                OnCustomCallback="grdBalanceLine_CustomCallback" 
                                OnCustomColumnDisplayText="grdBalanceLine_CustomColumnDisplayText" 
                                OnCustomUnboundColumnData="grdBalanceLine_CustomUnboundColumnData" 
                                OnRowDeleting="grdBalanceLine_RowDeleting" 
                                OnRowInserted="grdBalanceLine_RowInserted" 
                                OnRowInserting="grdBalanceLine_RowInserting" 
                                OnRowUpdating="grdBalanceLine_RowUpdating" 
                                OnRowValidating="grdBalanceLine_RowValidating" KeyboardSupport="True" 
                                OnInitNewRow="grdBalanceLine_InitNewRow" 
                                OnStartRowEditing="grdBalanceLine_StartRowEditing">                                
<ClientSideEvents CustomButtonClick="grdBalanceLine_CustomButtonClick" endcallback="grdBalanceLine_EndCallback" 
                                    Init="grdBalanceLine_Init"></ClientSideEvents>
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Mã bút toán" FieldName="TransactionId.Code" 
                                        ShowInCustomizationForm="True" VisibleIndex="2" UnboundType="String" 
                                        Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="TransactionId.Description" 
                                        ShowInCustomizationForm="True" VisibleIndex="3" UnboundType="String" 
                                        Width="200px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số dư nợ" FieldName="Debit" 
                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="200px">
                                        <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <dx:ASPxSpinEdit ID="colBalanceInitDebit" runat="server" 
                                                ClientInstanceName="colBalanceInitDebit" DisplayFormatString="n0" 
                                                Height="21px" Number="0" Width="100%" oninit="colBalanceInitDebit_Init">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Số dư có" FieldName="Credit" 
                                        ShowInCustomizationForm="True" VisibleIndex="5" Width="200px">
                                        <PropertiesTextEdit DisplayFormatInEditMode="True" DisplayFormatString="n0">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <dx:ASPxSpinEdit ID="colBalanceInitCredit" runat="server" 
                                                ClientInstanceName="colBalanceInitCredit" DisplayFormatString="n0" Height="21px" 
                                                Number="0" Width="100%" oninit="colBalanceInitCredit_Init">
                                                <SpinButtons ShowIncrementButtons="False">
                                                </SpinButtons>
                                            </dx:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Đối tượng" 
                                        ShowInCustomizationForm="True" VisibleIndex="6" Width="70px">
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="dynamicObject">
                                                <Image>
                                                    <SpriteProperties CssClass="Sprite_Allocation" />
<SpriteProperties CssClass="Sprite_Allocation"></SpriteProperties>
                                                </Image>
                                            </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                        ShowInCustomizationForm="True" VisibleIndex="8" Width="100px">
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
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="TransactionId!Key" 
                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="0px" 
                                        ReadOnly="True">
                                        <PropertiesTextEdit Width="0px">
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="AccountId!Key" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="7" 
                                        Width="0px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="CurrencyId!Key" 
                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" 
                                        Width="0px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="RowStatus" ShowInCustomizationForm="True" 
                                        Visible="False" VisibleIndex="9">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="GeneralJournalId" 
                                        ShowInCustomizationForm="True" VisibleIndex="1" Width="0px" 
                                        ReadOnly="True">
                                        <PropertiesTextEdit Width="0px">
                                        </PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" 
                                    ConfirmDelete="True" />
                                <SettingsEditing Mode="Inline" />

<SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>

<SettingsEditing Mode="Inline"></SettingsEditing>

                                <Styles>
                                    <Header Font-Bold="True" HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>

<dx:ASPxGridView ID="AccountList" runat="server" AutoGenerateColumns="False" 
    KeyFieldName="Code" 
    Width="100px" KeyboardSupport="True" ClientInstanceName="AccountList" 
    oncelleditorinitialize="AccountList_CellEditorInitialize" 
    CssClass="KeyShortcut" onrowupdating="AccountList_RowUpdating" 
    onrowvalidating="AccountList_RowValidating" Visible="False">
    <ClientSideEvents Init="AccountList_Init" 
        RowClick="AccountList_RowClick" />
<ClientSideEvents Init="AccountList_Init"></ClientSideEvents>
    <Columns>
        <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="4" 
            ButtonType="Image" Width="10%">
            <EditButton Visible="True">
                <Image ToolTip="Sửa">
                    <SpriteProperties CssClass="Sprite_Edit" />
                </Image>
            </EditButton>
            <CancelButton>
                <Image ToolTip="Bỏ qua">
                    <SpriteProperties CssClass="Sprite_Cancel" />
                </Image>
            </CancelButton>
            <UpdateButton>
                <Image ToolTip="Cập nhật">
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </UpdateButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="Mã TK" FieldName="Code" ReadOnly="True" 
            SortIndex="0" SortOrder="Ascending" VisibleIndex="0" Width="70px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên TK" FieldName="Name" ReadOnly="True" 
            VisibleIndex="1" Width="50%">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn Caption="Số dư ban đầu" FieldName="Balance" 
            VisibleIndex="2" Width="40%">
            <propertiesspinedit displayformatstring="g"></propertiesspinedit>
        </dx:GridViewDataSpinEditColumn>
    </Columns>
    <SettingsBehavior AllowFocusedRow="True"/>

<SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>

    <SettingsPager PageSize="30">
    </SettingsPager>
    <SettingsEditing Mode="Inline" />
    <Settings ShowFilterRow="True" UseFixedTableLayout="True" 
        VerticalScrollableHeight="50" />

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" VerticalScrollableHeight="50" 
        UseFixedTableLayout="True"></Settings>
</dx:ASPxGridView>



    <dx:ASPxCallbackPanel ID="cpMessageBox" runat="server" 
        ClientInstanceName="cpMessageBox" Width="100%">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <uc2:MessageBox ID="MessageBox1" runat="server" />
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>



<dx:ASPxCallbackPanel ID="cpAllocation" runat="server" 
    ClientInstanceName="cpAllocation" oncallback="cpAllocation_Callback" 
    Width="500px">
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formDynamicObject" runat="server" 
                    ClientInstanceName="formDynamicObject" CloseAction="CloseButton" HeaderText="Đối tượng số dư ban đầu" 
                    Height="372px" Modal="True" PopupHorizontalAlign="WindowCenter" 
                    PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" Width="500px">
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                            <uc1:NASCustomFieldDataGridView ID="NASCustomFieldDataGridView1" 
                                runat="server" />
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
</dx:ASPxCallbackPanel>




                                        <dx:ASPxTreeList runat="server" 
    KeyFieldName="AccountId" ParentFieldName="ParentAccountId" 
    AutoGenerateColumns="False" ClientInstanceName="tgrdAccountBalance" 
    Width="95%" ID="tgrdAccountBalance" style="margin-bottom: 0px" 
    OnCustomCallback="tgrdAccountBalance_CustomCallback" Height="400px" 
    onhtmlrowprepared="tgrdAccountBalance_HtmlRowPrepared"><Columns>
<dx:TreeListTextColumn FieldName="AccountId" Width="0px" VisibleIndex="0" Visible="False">
</dx:TreeListTextColumn>
<dx:TreeListTextColumn FieldName="Code" Width="100px" Caption="Tài khoản" VisibleIndex="1" SortIndex="0" SortOrder="Ascending">
<PropertiesTextEdit>
<ValidationSettings>
<RequiredField IsRequired="True" ErrorText="Chưa nhập số tài khoản."></RequiredField>
</ValidationSettings>
</PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</dx:TreeListTextColumn>
<dx:TreeListTextColumn FieldName="Name" Width="200px" Caption="Tên tài khoản" VisibleIndex="2">
<PropertiesTextEdit>
<ValidationSettings>
<RequiredField IsRequired="True" ErrorText="Chưa nhập tên tài khoản."></RequiredField>
</ValidationSettings>
</PropertiesTextEdit>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
</dx:TreeListTextColumn>
<dx:TreeListTextColumn FieldName="Level" Width="50px" Caption="Cấp" VisibleIndex="3">
    <PropertiesTextEdit>
        <ValidationSettings>
            <RequiredField ErrorText="Chưa nhập cấp." IsRequired="True" />
        </ValidationSettings>
    </PropertiesTextEdit>
    <HeaderStyle HorizontalAlign="Center" />
</dx:TreeListTextColumn>
                                                <dx:TreeListTextColumn Caption="Số dư nợ" FieldName="Debit" VisibleIndex="4" 
                                                    Width="200px">
                                                    <PropertiesTextEdit DisplayFormatString="n0">
                                                    </PropertiesTextEdit>
                                                </dx:TreeListTextColumn>
                                                <dx:TreeListTextColumn FieldName="Credit" VisibleIndex="5" Caption="Số dư có" 
                                                    Width="200px">
                                                    <PropertiesTextEdit DisplayFormatString="n0">
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:TreeListTextColumn>
                                                <dx:TreeListTextColumn FieldName="ParentAccountId" Visible="False" 
                                                    VisibleIndex="6">
                                                </dx:TreeListTextColumn>
</Columns>

                                            <Settings ScrollableHeight="400" VerticalScrollBarMode="Visible" 
                                                ShowFooter="True" />
                                            <SettingsLoadingPanel ShowImage="False" Enabled="False" />

<SettingsEditing AllowRecursiveDelete="True"></SettingsEditing>

<SettingsText ConfirmDelete="Bạn c&#243; chắc chắn muốn x&#243;a kh&#244;ng?"></SettingsText>
                                            <Styles>
                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                </Header>
                                                <Footer Font-Bold="True">
                                                </Footer>
                                            </Styles>
                                            <ClientSideEvents EndCallback="tgrdAccountBalance_EndCallback" />
</dx:ASPxTreeList>
</div>

                                    







                                    




    