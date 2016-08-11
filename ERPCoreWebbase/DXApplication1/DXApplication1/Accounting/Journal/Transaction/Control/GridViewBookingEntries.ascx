<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewBookingEntries.ascx.cs"
    Inherits="WebModule.Accounting.Journal.Transaction.Control.GridViewBookingEntries" %>
<%@ Register Src="~/ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<script type="text/javascript">

    var GridViewBookingEntries = function () {
        this.GridViewDataChanged = new NASClientEvent();
        this.RaiseGridViewDataChanged = function()
        {
            this.GridViewDataChanged.FireEvent(this, null);
        };
    };

    (function () {
        var nasObj = new GridViewBookingEntries();
        window['<%= _ClientInstanceName %>'] = nasObj;

        <% if(GridViewDataChanged != null && GridViewDataChanged.Trim().Length > 0) { %>
            nasObj.GridViewDataChanged.AddHandler(<%= GridViewDataChanged %>);
        <% } %>
    })();
    
</script>
<dx:ASPxGridView ID="gridBookingEntries" runat="server" AutoGenerateColumns="False"
    KeyFieldName="TransactionId" Width="100%" 
    OnCustomButtonInitialize="gridBookingEntries_CustomButtonInitialize" 
    oncustomcolumndisplaytext="gridBookingEntries_CustomColumnDisplayText">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã bút toán" FieldName="Code" VisibleIndex="0"
            Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn Caption="Số tiền" FieldName="Amount" VisibleIndex="3"
            Width="120px">
            <PropertiesSpinEdit DisplayFormatString="g">
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" VisibleIndex="2"
            Width="120px">
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Mục đích" FieldName="Description" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Phân bổ" VisibleIndex="4"
            Width="100px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton Text="Allocate" ID="AllocateTransaction">
                    <Image ToolTip="Phân bổ">
                        <SpriteProperties CssClass="Sprite_Allocation" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Name="DynamicObjectList" Caption="Đối tượng phân bổ" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
    </Columns>
    <SettingsEditing Mode="Inline" />
    <SettingsDetail ShowDetailRow="True" />
    <Styles>
        <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
        </Header>
        <Cell Wrap="True">
        </Cell>
        <CommandColumn Spacing="4px">
        </CommandColumn>
    </Styles>
    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="gridviewGeneralJournal" runat="server" AutoGenerateColumns="False"
                DataSourceID="dsGeneralJournal" KeyFieldName="GeneralJournalId" OnBeforePerformDataSelect="gridviewGeneralJournal_BeforePerformDataSelect"
                Width="100%" OnRowDeleting="gridviewGeneralJournal_RowDeleting" OnRowInserting="gridviewGeneralJournal_RowInserting"
                OnRowUpdating="gridviewGeneralJournal_RowUpdating" OnCellEditorInitialize="gridviewGeneralJournal_CellEditorInitialize"
                OnCustomColumnDisplayText="gridviewGeneralJournal_CustomColumnDisplayText" OnInit="gridviewGeneralJournal_Init"
                OnCommandButtonInitialize="gridviewGeneralJournal_CommandButtonInitialize" OnCustomButtonInitialize="gridviewGeneralJournal_CustomButtonInitialize"
                OnInitNewRow="gridviewGeneralJournal_InitNewRow" OnStartRowEditing="gridviewGeneralJournal_StartRowEditing">
                <TotalSummary>
                    <dx:ASPxSummaryItem DisplayFormat="Tổng nợ={0:#,###}" FieldName="Debit" SummaryType="Sum" />
                    <dx:ASPxSummaryItem DisplayFormat="Tổng có={0:#,###}" FieldName="Credit" SummaryType="Sum" />
                </TotalSummary>
                <Columns>
                    <dx:GridViewDataComboBoxColumn Caption="Tài khoản" FieldName="AccountId!Key" VisibleIndex="1"
                        Width="170px">
                        <PropertiesComboBox EnableCallbackMode="true" CallbackPageSize="10" TextField="Code"
                            ValueField="AccountId" ValueType="System.Guid" IncrementalFilteringMode="Contains"
                            LoadDropDownOnDemand="True" TextFormatString="{0} - {1}">
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" />
                                <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" />
                            </Columns>
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="true" ErrorText="<%$ Resources:MessageResource, Msg_Required_Select %>" />
                            </ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn Caption="Có" FieldName="Credit" ShowInCustomizationForm="True"
                        VisibleIndex="3" Width="120px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Nợ" FieldName="Debit" ShowInCustomizationForm="True"
                        VisibleIndex="2" Width="120px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                        VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Phân bổ" ShowInCustomizationForm="True"
                        VisibleIndex="4" Width="100px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="AllocateGeneralJournal" Text="Allocate">
                                <Image ToolTip="Phân bổ">
                                    <SpriteProperties CssClass="Sprite_Allocation" />
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                        VisibleIndex="6" Width="100px">
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
                        <UpdateButton Visible="True">
                            <Image ToolTip="Cập nhật">
                                <SpriteProperties CssClass="Sprite_Apply" />
                            </Image>
                        </UpdateButton>
                        <CancelButton Visible="True">
                            <Image ToolTip="Bỏ qua">
                                <SpriteProperties CssClass="Sprite_Cancel" />
                            </Image>
                        </CancelButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Đối tượng phân bổ" Name="DynamicObjectList" 
                        VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ConfirmDelete="True" />
                <Settings ShowFooter="True" />
                <SettingsEditing Mode="Inline" />
                <Styles>
                    <Header Font-Bold="True" HorizontalAlign="Center" Wrap="True">
                    </Header>
                    <Cell Wrap="True">
                    </Cell>
                    <Footer Font-Bold="True">
                    </Footer>
                    <CommandColumn Spacing="4px">
                    </CommandColumn>
                </Styles>
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
</dx:ASPxGridView>
<dx:XpoDataSource ID="dsGeneralJournal" runat="server" Criteria="[TransactionId!Key] = ?"
    TypeName="NAS.DAL.Accounting.Journal.GeneralJournal">
</dx:XpoDataSource>
<dx:ASPxCallbackPanel ID="cpnAllocationObjects" ClientInstanceName="cpnAllocationObjects"
    runat="server" Width="100%" OnCallback="cpnAllocationObjects_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupAllocationObjects" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" CloseAction="CloseButton" HeaderText="Thông tin đối tượng phân bổ"
                Height="480px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                RenderMode="Lightweight" ShowMaximizeButton="True" Width="600px">
                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <uc1:NASCustomFieldDataGridView ID="customFieldDataGridView" runat="server" />
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
