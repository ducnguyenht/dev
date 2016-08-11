<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPopupOrder.ascx.cs" Inherits="WebModule.Sales.UserControl.PopupOrder.ucPopupOrder" %>
<dx:ASPxLoadingPanel ID="ldpnPopupOrder" runat="server" HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
    <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
</dx:ASPxLoadingPanel>
<dx:ASPxCallbackPanel ID="cpPopupOrder" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    Width="100%">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPopupControl runat="server" HeaderText="Thông tin phiếu bán hàng" Height="620px"
                Modal="True" Width="950px" ClientInstanceName="formPurchaseEdit" AllowDragging="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowFooter="True"
                ShowSizeGrip="False" AllowResize="True" ShowMaximizeButton="True" CloseAction="CloseButton"
                Maximized="True" ScrollBars="Auto" ShowLoadingPanel="False" ShowOnPageLoad="True">
                <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                <FooterContentTemplate>
                    <div id="Footer" style="display: inline; width: 100%;">
                        <div style="display: inline; float: left">
                            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                Text="Trợ Giúp" Wrap="False" Visible="False" UseSubmitBehavior="False">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Help" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton UseSubmitBehavior="false" ID="buttonCancelDevice" runat="server" AutoPostBack="False"
                                CssClass="float_right dl mg" ClientInstanceName="buttonCancelDevice" Text="Thoát ra"
                                Wrap="False">
                                <ClientSideEvents Click="buttonCancelDevice_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: left;">
                            <dx:ASPxButton ID="buttonLock" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonLock" Text="" Wrap="False" UseSubmitBehavior="false"
                                Visible="True" Enabled="false">
                                <ClientSideEvents Click="buttonImportInventory_Click"></ClientSideEvents>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Lock" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonSaveDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonSaveDevice" Text="Lưu lại" Wrap="False" Visible="true"
                                UseSubmitBehavior="False">
                                <ClientSideEvents Click="buttonSaveDevice_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Save" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: right;">
                            <dx:ASPxButton ID="buttonModify" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonModify" Text="Chỉnh sửa" UseSubmitBehavior="False"
                                Visible="true">
                                <ClientSideEvents Click="buttonModify_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: left;">
                            <dx:ASPxButton ID="buttonImportInventory" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonImportInventory" Text="Xuất kho" Wrap="False" UseSubmitBehavior="false"
                                Visible="false">
                                <ClientSideEvents Click="buttonImportInventory_Click"></ClientSideEvents>
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: left;">
                            <dx:ASPxButton ID="buttonBooking" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonBooking" Text="Hạch toán" UseSubmitBehavior="False"
                                Visible="false">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Balance"></SpriteProperties>
                                </Image>
                                <ClientSideEvents Click="buttonBooking_Click" />
                            </dx:ASPxButton>
                        </div>
                        <div style="display: inline; float: left;">
                            <dx:ASPxButton ID="buttonPrint" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                ClientInstanceName="buttonPrint" Text="In Phiếu" UseSubmitBehavior="False" Visible="true">
                                <ClientSideEvents Click="buttonPrint_Click" />
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Print" />
                                </Image>
                            </dx:ASPxButton>
                        </div>
                    </div>
                </FooterContentTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
                            <Items>
                                <dx:LayoutItem Caption="Mã số (*)">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ngày lập (*)">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Khách hàng (*)">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxComboBox ID="cboSupplier" runat="server" CallbackPageSize="20" ClientInstanceName="cboSupplier"
                                                DataSourceID="BuyerXDS" EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                TextField="Name" TextFormatString="{1}" ValueField="Code" Width="300px">
                                                <ClientSideEvents KeyDown="cboSupplier_KeyDown" ValueChanged="cboSupplier_ValueChanged">
                                                </ClientSideEvents>
                                                <Columns>
                                                    <dx:ListBoxColumn Caption="Mã khách hàng" FieldName="Code" Name="Code" Width="150px" />
                                                    <dx:ListBoxColumn Caption="Tên khách hàng" FieldName="Name" Name="Name" Width="300px" />
                                                </Columns>
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="True" ErrorText="Chưa chọn kh&#225;ch h&#224;ng"></RequiredField>
                                                </ValidationSettings>
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Đối tượng phiếu bán">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxButton ID="cmdBillActor" runat="server" AutoPostBack="False" 
                                                ClientInstanceName="cmdBillActor" Text="..." UseSubmitBehavior="False" 
                                                Width="10px">
                                                <ClientSideEvents Click="cmdBillActor_Click" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                        <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Tổng giá trị phiếu hàng" ColCount="4">
                                    <Items>
                                        <dx:LayoutItem Caption="Tổng tiền hàng hóa">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E1" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tổng tiền dịch vụ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E2" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tổng tiền chiết khấu">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E3" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tổng tiền thuế suất">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E4" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Tổng giá trị">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E5" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Đã thanh toán">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E6" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Còn lại">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel ID="ASPxFormLayout4_E7" runat="server" Text="00">
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                            <SettingsItems HorizontalAlign="Left" />
                            <SettingsItems HorizontalAlign="Left"></SettingsItems>
                        </dx:ASPxFormLayout>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:XpoDataSource ID="BuyerXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Organization.Organization"
                Criteria="[RowStatus] &gt; 0">
                <CriteriaParameters>
                    <asp:SessionParameter Name="newparameter" SessionField="SaleBillId" />
                    <asp:Parameter DefaultValue="5817b239-e150-4c8e-a313-eaa8bd6944c4" Name="ProductObjectTypeId" />
                </CriteriaParameters>
            </dx:XpoDataSource>
            <dx:XpoDataSource ID="BillActorXDS" runat="server" Criteria="[BillId] = ?" TypeName="NAS.DAL.Invoice.BillActor">
    <CriteriaParameters>
        <asp:SessionParameter Name="SaleBillId" SessionField="SaleBillId" />
    </CriteriaParameters>
</dx:XpoDataSource>
            <dx:ASPxPopupControl ID="formBillActor" runat="server" Height="268px" RenderMode="Lightweight"
    Width="633px" ClientInstanceName="formBillActor" PopupElementID="bBillActor"
    HeaderText="" OnWindowCallback="formBillActor_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grdBillActor" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdBillActor"
                DataSourceID="BillActorXDS" KeyFieldName="BillActorId" OnRowUpdating="grdBillActor_RowUpdating"
                Width="100%" OnCustomColumnDisplayText="grdBillActor_CustomColumnDisplayText">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="BillActorId" ShowInCustomizationForm="True"
                        VisibleIndex="3" ReadOnly="True" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Loại đối tượng" FieldName="BillActorTypeId!Key"
                        ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Width="200px">
                        <PropertiesComboBox TextField="Description" TextFormatString="{1}" ValueField="BillActorTypeId"
                            ValueType="System.Guid" EnableCallbackMode="true" IncrementalFilteringMode="Contains"
                            OnItemRequestedByValue="colBillActorTypeItemRequestedByValue" OnItemsRequestedByFilterCondition="colBillActorTypeItemsRequestedByFilterCondition">
                            <Columns>
                                <dx:ListBoxColumn FieldName="Name" />
                                <dx:ListBoxColumn FieldName="Description" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="OrganizationId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="4" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Tên đối tượng" FieldName="PersonId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="1" Width="300px">
                        <PropertiesComboBox EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                            TextField="Name" TextFormatString="{0}-{1}" ValueField="PersonId" ValueType="System.Guid"
                            OnItemRequestedByValue="colPersonOnItemRequestedByValue" OnItemsRequestedByFilterCondition="colPersonOnItemsRequestedByFilterCondition">
                            <Columns>
                                <dx:ListBoxColumn Caption="Mã đối tượng" FieldName="Code" />
                                <dx:ListBoxColumn Caption="Tên đối tượng" FieldName="Name" />
                            </Columns>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="BillId!Key" ShowInCustomizationForm="True"
                        VisibleIndex="5" Width="0px">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                        VisibleIndex="2" Width="70px">
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
            </dx:ASPxGridView>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
