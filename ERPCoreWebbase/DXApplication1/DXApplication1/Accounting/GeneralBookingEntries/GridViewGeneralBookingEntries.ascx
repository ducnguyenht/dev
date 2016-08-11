<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewGeneralBookingEntries.ascx.cs"
    Inherits="WebModule.Accounting.GeneralBookingEntries.GridViewGeneralBookingEntries" %>
<%@ Register Src="~/ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<%@ Register src="GeneralBookingEditingForm.ascx" tagname="GeneralBookingEditingForm" tagprefix="uc2" %>
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
        <% if(ClientInstanceName != null && ClientInstanceName.Trim().Length > 0) { %>
            window['<%= ClientInstanceName %>'] = nasObj;
        <% } else { %>
            window['<%= ClientID %>'] = nasObj;
        <% } %>
        nasObj.GridViewDataChanged.AddHandler(<%= GridViewDataChanged %>);
    })();

    function t(s, e) {
        if(s.cpDataChanged) {
            if(!gridBookingEntries.InCallback()) { 
                gridBookingEntries.Refresh();
            }
        }
    }
    
//    function gridBookingEntries_CustomButtonClick(s,e){
//        switch(e.buttonID){
//            case 'buttonNew':
//                //
//                break;
//            default:
//                break;
//        }
//    }

</script>
<dx:ASPxGridView ID="gridBookingEntries" runat="server" AutoGenerateColumns="False"
    KeyFieldName="TransactionId" Width="100%" OnCustomButtonInitialize="gridBookingEntries_CustomButtonInitialize"
    OnCustomCallback="gridBookingEntries_CustomCallback" 
    OnRowDeleting="gridBookingEntries_RowDeleting" OnRowUpdating="gridBookingEntries_RowUpdating"
    OnStartRowEditing="gridBookingEntries_StartRowEditing" 
    oncustomcolumndisplaytext="gridBookingEntries_CustomColumnDisplayText" 
    onrowinserting="gridBookingEntries_RowInserting" 
    oninitnewrow="gridBookingEntries_InitNewRow">
    <SettingsEditing Mode="Inline"></SettingsEditing>
    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
        ShowHeaderFilterButton="True"></Settings>
    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
    <ClientSideEvents CustomButtonClick="gridBookingEntries_CustomButtonClick" />
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã bút toán" FieldName="Code" VisibleIndex="0"
            Width="120px">
            <PropertiesTextEdit>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn Caption="Số tiền" FieldName="Amount" VisibleIndex="3"
            Width="120px">
            <PropertiesSpinEdit DisplayFormatString="N0">
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" VisibleIndex="2"
            Width="120px">
            <PropertiesDateEdit>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
            VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Đối tượng phân bổ" VisibleIndex="5"
            Width="100px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton Text="Allocate" ID="AllocateTransaction">
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
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="Book" Text="Book">
                    <Image ToolTip="Ghi sổ">
                        <SpriteProperties CssClass="Sprite_Approve" />
                    </Image>
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="BookingStatus" 
            VisibleIndex="4" Width="100px">
            <PropertiesComboBox>
                <Items>
                    <dx:ListEditItem Text="(Tất cả)" Value="" />
                    <dx:ListEditItem Text="Chưa ghi sổ" Value="Chưa ghi sổ" />
                    <dx:ListEditItem Text="Đã ghi sổ" Value="Đã ghi sổ" />
                </Items>
            </PropertiesComboBox>
            <EditFormSettings Visible="False" />
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
        </dx:GridViewDataComboBoxColumn>
    </Columns>
    <SettingsBehavior ConfirmDelete="True" />
    <Styles>
        <Header Font-Bold="True" HorizontalAlign="Center">
        </Header>
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
                OnStartRowEditing="gridviewGeneralJournal_StartRowEditing" OnInitNewRow="gridviewGeneralJournal_InitNewRow">
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
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Đối tượng phân bổ" ShowInCustomizationForm="True"
                        VisibleIndex="4" Width="100px">
                        <CustomButtons>
                            <dx:GridViewCommandColumnCustomButton ID="AllocateGeneralJournal" Text="Allocate">
                                <Image ToolTip="Phân bổ">
                                    <SpriteProperties CssClass="Sprite_Allocation" />
                                </Image>
                            </dx:GridViewCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="5"
                        Width="100px" Visible="False">
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
                </Columns>
                <SettingsBehavior ConfirmDelete="True" />
                <Settings ShowFooter="True" />
                <SettingsEditing Mode="Inline" />
                <Styles>
                    <Header Font-Bold="True" HorizontalAlign="Center">
                    </Header>
                    <Footer Font-Bold="True">
                    </Footer>
                    <CommandColumn Spacing="4px">
                    </CommandColumn>
                </Styles>
            </dx:ASPxGridView>
        </DetailRow>
    </Templates>
    <BorderLeft BorderWidth="0px" />
    <BorderRight BorderWidth="0px" />
</dx:ASPxGridView>
<dx:XpoDataSource ID="dsGeneralJournal" runat="server" Criteria="[TransactionId!Key] = ?"
    TypeName="NAS.DAL.Accounting.Journal.GeneralJournal">
</dx:XpoDataSource>
<dx:ASPxCallbackPanel ID="cpnAllocationObjects" ClientInstanceName="cpnAllocationObjects"
    runat="server" Width="100%" OnCallback="cpnAllocationObjects_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupAllocationObjects" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="popupAllocationObjects"
                CloseAction="CloseButton" HeaderText="Thông tin đối tượng phân bổ" Height="480px"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
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
<uc2:GeneralBookingEditingForm ID="GeneralBookingEditingForm1" runat="server" />
