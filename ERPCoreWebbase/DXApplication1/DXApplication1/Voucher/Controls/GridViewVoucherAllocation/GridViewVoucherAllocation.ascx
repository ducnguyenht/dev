<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewVoucherAllocation.ascx.cs"
    Inherits="WebModule.Voucher.Controls.GridViewVoucherAllocation.GridViewVoucherAllocation" %>
<%@ Register Src="../../../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx"
    TagName="NASCustomFieldDataGridView" TagPrefix="uc1" %>
<script type="text/javascript">
    var GridViewVoucherAllocation = {
        events: { eListChanged: 'GridViewVoucherAllocationChanged' }
    };

    function gridVoucherAllocation_EndCallback(s, e) {
        if (s.cpEvent == 'ListChanged') {
            $(GridViewVoucherAllocation).triggerHandler(GridViewVoucherAllocation.events.eListChanged);
            console.log('ListChanged was triggered');
            delete s.cpEvent;
        }
    }

    function gridviewAllocation_CustomButtonClick(s, e) {
        switch (e.buttonID) {
            case 'Allocate':
                if (!cpnAllocationObjects.InCallback()) {
                    popupAllocationObjects.Show();
                    var args = 'Allocate|' + e.visibleIndex;
                    cpnAllocationObjects.PerformCallback(args);
                }
                break;
            default:
                break;
        }
    }
</script>
<dx:ASPxGridView ID="gridviewAllocation" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridviewAllocation"
    DataSourceID="dsVoucherTransaction" KeyFieldName="TransactionId" OnRowDeleting="gridviewAllocation_RowDeleting"
    OnRowInserting="gridviewAllocation_RowInserting" OnRowUpdating="gridviewAllocation_RowUpdating"
    Width="100%" OnRowValidating="gridviewAllocation_RowValidating" 
    OnInitNewRow="gridviewAllocation_InitNewRow" 
    oncustomcolumndisplaytext="gridviewAllocation_CustomColumnDisplayText">
    <ClientSideEvents EndCallback="gridVoucherAllocation_EndCallback" CustomButtonClick="gridviewAllocation_CustomButtonClick" />
    <TotalSummary>
        <dx:ASPxSummaryItem DisplayFormat="Tổng cộng={0:#,###}" FieldName="Amount" SummaryType="Sum" />
    </TotalSummary>
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã phân bổ" FieldName="Code" VisibleIndex="0"
            Width="120px">
            <PropertiesTextEdit>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
            </PropertiesTextEdit>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataSpinEditColumn Caption="Số tiền" FieldName="Amount" VisibleIndex="3"
            Width="120px">
            <PropertiesSpinEdit DisplayFormatInEditMode="true" DisplayFormatString="#,###" NumberFormat="Custom"
                Style-HorizontalAlign="Right">
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
                <Style HorizontalAlign="Right"></Style>
            </PropertiesSpinEdit>
        </dx:GridViewDataSpinEditColumn>
        <dx:GridViewDataDateColumn Caption="Ngày" FieldName="IssueDate" VisibleIndex="2"
            Width="120px" Visible="False">
            <PropertiesDateEdit>
                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                    <RequiredField IsRequired="true" />
                </ValidationSettings>
            </PropertiesDateEdit>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="Mục đích" FieldName="Description" VisibleIndex="1">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="6"
            Width="100px">
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
            <UpdateButton>
                <Image ToolTip="Cập nhật">
                    <SpriteProperties CssClass="Sprite_Apply" />
                </Image>
            </UpdateButton>
            <CancelButton>
                <Image ToolTip="Bỏ qua">
                    <SpriteProperties CssClass="Sprite_Cancel" />
                </Image>
            </CancelButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn ButtonType="Image" Caption="Phân bổ" VisibleIndex="4"
            Width="100px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton Text="Allocate" ID="Allocate">
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
    <Settings ShowFooter="True" />
    <Styles>
        <Footer Font-Bold="True">
        </Footer>
        <Header Font-Bold="True" Wrap="True" HorizontalAlign="Center">
        </Header>
        <CommandColumn Spacing="4px">
        </CommandColumn>
        <Cell Wrap="True">
        </Cell>
    </Styles>
</dx:ASPxGridView>
<dx:XpoDataSource ID="dsVoucherTransaction" runat="server">
</dx:XpoDataSource>
<dx:ASPxCallbackPanel ID="cpnAllocationObjects" ClientInstanceName="cpnAllocationObjects"
    runat="server" Width="100%" OnCallback="cpnAllocationObjects_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popupAllocationObjects" runat="server" AllowDragging="True"
                AllowResize="True" AutoUpdatePosition="True" ClientInstanceName="popupAllocationObjects"
                CloseAction="CloseButton" HeaderText="Thông tin đối tượng phân bổ" Height="480px"
                Modal="True" OnWindowCallback="popupAllocationObjects_WindowCallback" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" RenderMode="Lightweight" ShowMaximizeButton="True"
                Width="600px">
                <ClientSideEvents Closing="function(s, e) { gridviewAllocation.Refresh(); }" />
                <ModalBackgroundStyle BackColor="Transparent">
                </ModalBackgroundStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <uc1:NASCustomFieldDataGridView ID="customFieldDataGridView" runat="server" OnDataUpdated="customFieldDataGridView_DataUpdated" />
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
