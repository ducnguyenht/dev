<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uVoucherBookingEntry.ascx.cs" Inherits="WebModule.PayReceiving.UserControl.VoucherBookingEntry.uVoucherBookingEntry" %>
<dx:ASPxLoadingPanel ID="ldpnCostingEditForm" runat="server" HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
</dx:ASPxLoadingPanel>
<dx:ASPxCallbackPanel ID="cpVoucherBookingEntry" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    Width="100%" OnCallback="cpVoucherBookingEntry_OnCallback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl ID="popCosting" runat="server" AllowDragging="True" AllowResize="True"
                AppearAfter="200"
                HeaderText="Hạch toán" Height="600px" PopupHorizontalAlign="WindowCenter" 
                PopupVerticalAlign="WindowCenter" Width="850px" ShowFooter="True" ShowMaximizeButton="True"
                CloseAction="CloseButton" 
                LoadingDivStyle-BackColor="Transparent"
                ModalBackgroundStyle-BackColor="Transparent"
                ShowSizeGrip="False"
                Modal="True" Maximized="false">
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
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                            </div>
                        </div>
                        <div style="float: right">
                            <div style="float: left;">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnApproveCosting" AutoPostBack="false" runat="server" Text="Duyệt"
                                    Wrap="False" ClientInstanceName="btnApproveCosting">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Approve" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnCancel" AutoPostBack="false" runat="server" Text="Thoát" Wrap="False">
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
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="frmCosting" runat="server" DataSourceID="dsOriginArtifact"
                            Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Thông tin chứng từ" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Mã phiếu thu" FieldName="Code" Width="50%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblCode" runat="server" Font-Bold="True">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Phân loại" FieldName="VouchesTypeId.Description" 
                                            Width="50%">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblVoucherType" runat="server" Font-Bold="False">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Ngày thu" FieldName="IssuedDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblIssuesDate" runat="server" Text="">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Đơn vị trả" FieldName="SourceOrganizationId.Name">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblOrganization" runat="server" Text="">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Lý do thu" FieldName="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblDescription" runat="server" Text="">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Địa chỉ" FieldName="Address">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblAddress" runat="server" Text="">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Số tiền quy đổi">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblSumOfDebit" runat="server" Font-Bold="True">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Người trả tiền" FieldName="Payer">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblPayer" runat="server">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Trạng thái">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="lblIsApprovedCosting" Font-Bold="True" runat="server">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                                <dx:TabbedLayoutGroup Width="100%">
                                    <Items>
                                        <dx:LayoutItem Caption="Thông tin định khoản">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView KeyboardSupport="true" ID="grdTransaction" 
                                                        ClientInstanceName="grdTransaction" runat="server"
                                                        AutoGenerateColumns="False" KeyFieldName="VoucherAllocationId"
                                                        Width="100%" DataSourceID="dsVoucherAllocation" 
                                                        OnRowInserting="grdTransaction_RowInserting">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="VoucherAllocationId" ShowInCustomizationForm="True"
                                                                VisibleIndex="0" ReadOnly="True" Visible="false">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã bút toán" FieldName="Code" ShowInCustomizationForm="True" 
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Mã phân bổ" 
                                                                FieldName="AllocationId!Key" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                <PropertiesComboBox DataSourceID="dsAllocation" ValueField="AllocationId" TextField="Code" TextFormatString="{0}">
                                                                    <Columns>
                                                                        <dx:ListBoxColumn Caption="AllocationId" FieldName="AllocationId" Visible="false"/>
                                                                        <dx:ListBoxColumn Caption="Mã phân bổ" FieldName="Code"/>
                                                                        <dx:ListBoxColumn Caption="Tên phân bổ" FieldName="Name"/>
                                                                    </Columns>
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập mã phân bổ" />
                                                                    </ValidationSettings>
                                                                </PropertiesComboBox>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="Số tiền" FieldName="Amount" ShowInCustomizationForm="True"
                                                                VisibleIndex="3">
                                                                <PropertiesSpinEdit>
                                                                    <ValidationSettings>
                                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập số tiền" />
                                                                    </ValidationSettings>
                                                                </PropertiesSpinEdit>
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
                                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsEditing Mode="Inline" />
                                                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                                                        <Styles>
                                                            <Header Font-Bold="True" HorizontalAlign="Center">
                                                            </Header>
                                                            <CommandColumn Spacing="4px">
                                                            </CommandColumn>
                                                        </Styles>
                                                        <Templates>
                                                            <DetailRow>
                                                                <dx:ASPxGridView KeyboardSupport="True" ID="grdDetailEntry" 
                                                                    ClientInstanceName="grdDetailEntry" runat="server"
                                                                    AutoGenerateColumns="False"
                                                                    Width="100%" DataSourceID="dsVoucherAllocationBookingAccount" 
                                                                    KeyFieldName="VoucherAllocationBookingAccountId" 
                                                                    oncelleditorinitialize="grdDetailEntry_CellEditorInitialize"
                                                                    onbeforeperformdataselect="grdDetailEntry_BeforePerformDataSelect" 
                                                                    onrowinserting="grdDetailEntry_RowInserting" 
                                                                    onhtmldatacellprepared="grdDetailEntry_HtmlDataCellPrepared" 
                                                                    onrowdeleting="grdDetailEntry_RowDeleting" 
                                                                    onrowvalidating="grdDetailEntry_RowValidating">
                                                                    <TotalSummary>
                                                                        <dx:ASPxSummaryItem DisplayFormat="Tổng nợ={0:#,###}" FieldName="Debit" SummaryType="Sum" />
                                                                        <dx:ASPxSummaryItem DisplayFormat="Tổng có={0:#,###}" FieldName="Credit" SummaryType="Sum" />
                                                                    </TotalSummary>
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="VoucherAllocationBookingAccountId"
                                                                            VisibleIndex="0" ReadOnly="True" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                         <dx:GridViewDataComboBoxColumn Caption="Tài khoản" Name="AccountId" FieldName="AccountId!Key" VisibleIndex="1">
                                                                            <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                                                LoadDropDownOnDemand="True" CallbackPageSize="10" ValueField="AccountId" ValueType="System.Guid" TextFormatString="{0} - {1}" >
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" />
                                                                                    <dx:ListBoxColumn Caption="Tên tài khoản" FieldName="Name" />
                                                                                    <dx:ListBoxColumn Caption="Mô tả" FieldName="Description" />
                                                                                </Columns>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="true" ErrorText="Chưa chọn tài khoản" />
                                                                                </ValidationSettings>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                        <dx:GridViewDataSpinEditColumn Caption="Có" FieldName="Credit" VisibleIndex="2">
                                                                            <PropertiesSpinEdit DisplayFormatString="g">
                                                                            </PropertiesSpinEdit>
                                                                        </dx:GridViewDataSpinEditColumn>
                                                                        <dx:GridViewDataSpinEditColumn Caption="Nợ" FieldName="Debit" VisibleIndex="3">
                                                                            <PropertiesSpinEdit DisplayFormatString="g">
                                                                            </PropertiesSpinEdit>
                                                                        </dx:GridViewDataSpinEditColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="VoucherAllocationId!Key" VisibleIndex="4" Visible="false">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn Name="CommonOperations" ButtonType="Link" 
			                                                                Caption="Thao tác" VisibleIndex="5">
			                                                                <EditButton Visible="True">
				                                                                <%--<Image ToolTip="Sửa">
					                                                                <SpriteProperties CssClass="Sprite_Edit" />
				                                                                </Image>--%>
			                                                                </EditButton>
			                                                                <NewButton Visible="True">
				                                                                <%--<Image ToolTip="Thêm">
					                                                                <SpriteProperties CssClass="Sprite_New" />
				                                                                </Image>--%>
			                                                                </NewButton>
			                                                                <DeleteButton Visible="True">
				                                                                <%--<Image ToolTip="Xóa">
					                                                                <SpriteProperties CssClass="Sprite_Delete" />
				                                                                </Image>--%>
			                                                                </DeleteButton>
			                                                                <CancelButton Visible="True">
				                                                                <%--<Image ToolTip="Bỏ qua">
					                                                                <SpriteProperties CssClass="Sprite_Cancel" />
				                                                                </Image>--%>
			                                                                </CancelButton>
			                                                                <UpdateButton Visible="True">
				                                                                <%--<Image ToolTip="Cập nhật">
					                                                                <SpriteProperties CssClass="Sprite_Apply" />
				                                                                </Image>--%>
			                                                                </UpdateButton>
			                                                                <ClearFilterButton Visible="True">
				                                                                <%--<Image ToolTip="Hủy">
					                                                                <SpriteProperties CssClass="Sprite_Clear" />
				                                                                </Image>--%>
			                                                                </ClearFilterButton>
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
                        <dx:XpoDataSource ID="dsOriginArtifact" runat="server" TypeName="NAS.DAL.Vouches.ReceiptVouches"
                            Criteria="[VouchesId] = ?">
                            <CriteriaParameters>
                                <asp:Parameter Name="VoucherId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsVoucherAllocation" runat="server" TypeName="NAS.DAL.Vouches.Allocation.VoucherAllocation"
                            Criteria="[VouchesId] = ?">
                            <CriteriaParameters>
                                <asp:Parameter Name="VoucherId"/>
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsVoucherAllocationBookingAccount" runat="server" TypeName="NAS.DAL.Vouches.Allocation.VoucherAllocationBookingAccount"
                            Criteria="[VoucherAllocationId] = ?">
                            <CriteriaParameters>
                                <asp:Parameter Name="VoucherAllocationId"/>
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                        <dx:XpoDataSource ID="dsAllocation" runat="server" TypeName="NAS.DAL.Accounting.Configure.Allocation"
                            Criteria="[RowStatus] &gt; 0s">
                            <CriteriaParameters>
                                <asp:Parameter Name="VoucherId" />
                            </CriteriaParameters>
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>