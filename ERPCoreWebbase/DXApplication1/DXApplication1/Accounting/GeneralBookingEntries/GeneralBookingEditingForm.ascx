<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralBookingEditingForm.ascx.cs"
    Inherits="WebModule.Accounting.GeneralBookingEntries.GeneralBookingEditingForm" %>
<%@ Register Src="~/ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<script type="text/javascript">
    function PanelGeneralBooking_EndCallback(s, e) {
        if (s.cpRefreshGrid) {

            delete s.cpRefreshGrid;
        }

        if (s.cpBooked) {
            alert("Đã ghi sổ xong !");
            PopupGeneralBooking.Hide();
            delete s.cpBooked;
        }

    }

    function btnGeneralBookingClick(s, e) {
        PanelGeneralBooking.PerformCallback('book');
    }

    function btnGeneralSaveClick(s, e) {
        PanelGeneralBooking.PerformCallback('save');
    }

    function btnGeneralCancelClick(s, e) {
        PanelGeneralBooking.PerformCallback('cancel');
        PopupGeneralBooking.Hide(); 
        cpnGeneralBookingEntries.PerformCallback();
    }

</script>
<dx:ASPxCallbackPanel ID="PanelGeneralBooking" runat="server" ClientInstanceName="PanelGeneralBooking"
    Width="100%" OnCallback="PanelGeneralBooking_Callback">
    <ClientSideEvents EndCallback="PanelGeneralBooking_EndCallback" />
    <ClientSideEvents EndCallback="PanelGeneralBooking_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="PopupGeneralBooking" runat="server" AllowDragging="True"
                AllowResize="True" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ScrollBars="Auto"
                ShowMaximizeButton="True" Width="860px" ClientInstanceName="PopupGeneralBooking"
                ShowFooter="True" HeaderText="Thông tin hạch toán tổng hợp - Thêm mới" 
                Height="520px" Maximized="True">
                <FooterTemplate>
                    <div style="padding: 10px">
                        
                        <div style="float: right">
                            <div style="float: left; margin-left: 4px">
                                <dx:ASPxButton ID="btnSave" runat="server" Text="Lưu lại" AutoPostBack="false">
                                    <ClientSideEvents Click="btnGeneralSaveClick"></ClientSideEvents>
                                    <Image ToolTip="Lưu lại">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <dx:ASPxButton ID="btnCancel" runat="server" Text="Thoát" AutoPostBack="false" UseSubmitBehavior="false">
                                    <ClientSideEvents Click="btnGeneralCancelClick"></ClientSideEvents>
                                    <Image ToolTip="Thoát">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="float: right">
                            <div style="float: right; margin-left: 4px"">
                                <dx:ASPxButton ID="btnBooking" runat="server" Text="Ghi sổ" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="btnGeneralBookingClick"></ClientSideEvents>
                                    <Image ToolTip="Ghi sổ">
                                        <SpriteProperties CssClass="Sprite_Approve" />
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
                        <br />
                        <dx:ASPxFormLayout ID="FormLayoutGeneralBookingEditing" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chung" ColCount="4">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã bút toán" ColSpan="2" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtGeneralBookingCode" runat="server" ClientInstanceName="txtGeneralBookingCode"
                                                        Width="200px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="True" />
                                                            <RequiredField IsRequired="True"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày" ColSpan="2" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxDateEdit ID="txtGeneralBookingDate" runat="server" ClientInstanceName="txtGeneralBookingDate"
                                                        Width="200px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField ErrorText="" IsRequired="True" />
                                                            <RequiredField IsRequired="True" ErrorText=""></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Trạng thái">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtGeneralBookingStatus" runat="server" BackColor="WhiteSmoke"
                                                        ClientInstanceName="txtGeneralBookingStatus" ReadOnly="True" Width="170px">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Số tiền" ColSpan="2" RequiredMarkDisplayMode="Required">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit ID="txtGeneralBookingAmount" ClientInstanceName="txtGeneralBookingAmount"
                                                        runat="server" Height="21px" Number="0" Width="200px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="True" />
                                                            <RequiredField IsRequired="True"></RequiredField>
                                                        </ValidationSettings>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Diễn giải" ColSpan="4">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox ID="txtGeneralBookingDescription" ClientInstanceName="txtGeneralBookingDescription"
                                                        runat="server" Width="100%">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <br />
                        <dx:ASPxFormLayout ID="FormLayoutGeneralBookingDetail" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chi tiết" ColCount="4">
                                    <Items>
                                        <dx:LayoutItem ColSpan="4" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="grdGeneralBookingJournal" runat="server" AutoGenerateColumns="False"
                                                        DataSourceID="gridviewGeneralJournal" KeyFieldName="GeneralJournalId" Width="100%"
                                                        ClientInstanceName="grdGeneralBookingJournal" OnInitNewRow="grdGeneralBookingJournal_InitNewRow"
                                                        OnRowDeleting="grdGeneralBookingJournal_RowDeleting" OnRowInserting="grdGeneralBookingJournal_RowInserting"
                                                        OnRowUpdating="grdGeneralBookingJournal_RowUpdating" OnStartRowEditing="grdGeneralBookingJournal_StartRowEditing"
                                                        OnInit="grdGeneralBookingJournal_Init">
                                                        <TotalSummary>
                                                            <dx:ASPxSummaryItem DisplayFormat="Tổng nợ={0:#,###}" FieldName="Debit" SummaryType="Sum" />
                                                            <dx:ASPxSummaryItem DisplayFormat="Tổng có={0:#,###}" FieldName="Credit" SummaryType="Sum" />
                                                        </TotalSummary>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" Width="200px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Tài khoản" FieldName="AccountId!Key" ShowInCustomizationForm="True"
                                                                VisibleIndex="1" Width="150px">
                                                                <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                    TextField="Code" TextFormatString="{0} - {1}" ValueField="AccountId" ValueType="System.Guid"
                                                                    OnItemRequestedByValue="colAccount_OnItemRequestedByValue" OnItemsRequestedByFilterCondition="colAccount_OnItemsRequestedByFilterCondition">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="Tài khoản" FieldName="Code" Width="150px" />
                                                                        <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" Width="200px" />
                                                                    </Columns>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Nợ" FieldName="Debit" ShowInCustomizationForm="True"
                                                                VisibleIndex="2" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Có" FieldName="Credit" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Width="100px">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                VisibleIndex="6" Width="100px">
                                                                <EditButton Visible="True">
                                                                    <Image ToolTip="Sửa">
                                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                                    </Image>
                                                                </EditButton>
                                                                <NewButton Visible="True">
                                                                    <Image ToolTip="Thêm">
                                                                        <SpriteProperties CssClass="Sprite_New" />
                                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                                    </Image>
                                                                </NewButton>
                                                                <DeleteButton Visible="True">
                                                                    <Image ToolTip="Xóa">
                                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                                    </Image>
                                                                </DeleteButton>
                                                                <CancelButton>
                                                                    <Image ToolTip="Bỏ qua">
                                                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                                    </Image>
                                                                </CancelButton>
                                                                <UpdateButton>
                                                                    <Image ToolTip="Cập nhật">
                                                                        <SpriteProperties CssClass="Sprite_Apply" />
                                                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                                    </Image>
                                                                </UpdateButton>
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Đối tượng phân bổ" ShowInCustomizationForm="True"
                                                                VisibleIndex="4" Width="100px">
                                                                <ClearFilterButton Visible="True">
                                                                </ClearFilterButton>
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="AllocateGeneralJournal" Text="Allocate">
                                                                        <Image ToolTip="Phân bổ">
                                                                            <SpriteProperties CssClass="Sprite_Allocation" />
                                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <SettingsBehavior ConfirmDelete="True" />
                                                        <SettingsEditing Mode="Inline" />
                                                        <Settings ShowFooter="True" />
                                                        <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
                                                        <SettingsEditing Mode="Inline"></SettingsEditing>
                                                        <Settings ShowFooter="True"></Settings>
                                                    </dx:ASPxGridView>
                                                    <dx:XpoDataSource ID="gridviewGeneralJournal" runat="server" Criteria="" 
                                                        TypeName="NAS.DAL.Accounting.Journal.GeneralJournal" DefaultSorting="">
                                                    </dx:XpoDataSource>
                                                    <br />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <br />
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<dx:ASPxCallbackPanel ID="cpnGeneralBookingAllocationObjects" ClientInstanceName="cpnGeneralBookingAllocationObjects"
    runat="server" Width="100%" OnCallback="cpnGeneralBookingAllocationObjects_Callback">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupGeneralBookingAllocationObjects" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="popupGeneralBookingAllocationObjects"
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
