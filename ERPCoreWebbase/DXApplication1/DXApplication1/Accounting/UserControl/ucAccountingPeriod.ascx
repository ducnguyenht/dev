<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAccountingPeriod.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.ucAccountingPeriod" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<script>
    function AccountingPeriodTypeChanged() {
        cp_Grid1.PerformCallback(cb_type.lastSuccessValue);
    }
</script>
<dx:ASPxLabel ID="ASPxLabel1" runat="server" AssociatedControlID="ASPxLabel1" Text="Chu kì kế toán">
</dx:ASPxLabel>
<br />
<dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"
    Width="100%">
    <TabPages>
        <dx:TabPage Name="Chu kì kế toán" Text="Chu kì kế toán">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="AccountingPeriod"
                        KeyFieldName="AccountingPeriodId" OnCustomColumnDisplayText="ASPxGridView1_CustomColumnDisplayText"
                        Width="100%" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
                        OnRowInserted="ASPxGridView1_RowInserted" OnRowInserting="ASPxGridView1_RowInserting"
                        OnRowUpdating="ASPxGridView1_RowUpdating" OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData"
                        OnStartRowEditing="ASPxGridView1_StartRowEditing" 
                        OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize" 
                        OnRowDeleting="ASPxGridView1_RowDeleting">
                        <SettingsEditing Mode="Inline"></SettingsEditing>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã chu kì" FieldName="Code" ShowInCustomizationForm="True"
                                VisibleIndex="0" Width="150px">
                                <PropertiesTextEdit>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewBandColumn Caption="Thời gian" ShowInCustomizationForm="True" VisibleIndex="3">
                                <Columns>
                                    <dx:GridViewDataDateColumn Caption="Từ ngày" FieldName="FromDateTime" ShowInCustomizationForm="True"
                                        VisibleIndex="0" Width="70px">
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataDateColumn Caption="Đến ngày" FieldName="ToDateTime" ShowInCustomizationForm="True"
                                        VisibleIndex="2" Width="70px">
                                    </dx:GridViewDataDateColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Thể loại" FieldName="AccountingPeriodTypeId!Key"
                                ShowInCustomizationForm="True" VisibleIndex="2">
                                <PropertiesComboBox DataSourceID="XPOAccountingPeriodType" TextField="Name" TextFormatString="{1}: {2}"
                                    ValueField="AccountingPeriodTypeId" ValueType="System.Guid" EnableCallbackMode="True"
                                    ClientInstanceName="cb_type" DisplayFormatInEditMode="True">
                                    <ClientSideEvents SelectedIndexChanged="AccountingPeriodTypeChanged" />
                                    <Columns>
                                        <dx:ListBoxColumn Caption="ID" FieldName="AccountingPeriodTypeId" Width="0px" />
                                        <dx:ListBoxColumn Caption="Thể loại" FieldName="Name" />
                                        <dx:ListBoxColumn Caption="Diễn giải" FieldName="Description" />
                                    </Columns>
                                    <ValidationSettings>
                                        <RequiredField IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataCheckColumn Caption="Kích hoạt" FieldName="IsActive" ShowInCustomizationForm="True"
                                VisibleIndex="5" Width="60px">
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewDataTextColumn Caption="Chu kì trực thuộc" Name="under" ShowInCustomizationForm="True"
                                VisibleIndex="4" Width="170px">
                                <PropertiesTextEdit Width="100%">
                                </PropertiesTextEdit>
                                <EditItemTemplate>
                                    <dx:ASPxCallbackPanel ID="cp_Grid1" runat="server" ClientInstanceName="cp_Grid1"
                                        Width="100%" OnCallback="cp_Grid1_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridLookup ID="GridUnderPeriod" runat="server" AutoGenerateColumns="False"
                                                    ClientInstanceName="GridUnderPeriod" DataSourceID="XPOAccountingPeriodLookup"
                                                    KeyFieldName="AccountingPeriodId" SelectionMode="Multiple" 
                                                    TextFormatString="{0}" Width="100%">
                                                    <GridViewProperties>
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                    </GridViewProperties>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Caption="Chọn" ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                                            VisibleIndex="0">
                                                            <ClearFilterButton Visible="True">
                                                            </ClearFilterButton>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="Mã chu kì" FieldName="Code" ShowInCustomizationForm="True"
                                                            VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                                            VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridLookup>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </EditItemTemplate>
                                <CellStyle Wrap="True">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="6"
                                Width="100px" ButtonType="Image" AllowDragDrop="True">
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
                        </Columns>
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Name="Loại chu kì" Text="Loại chu kì">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="XPOAccountingPeriodType"
                        KeyFieldName="AccountingPeriodTypeId" Width="100%" OnRowDeleting="ASPxGridView2_RowDeleting"
                        OnRowInserted="ASPxGridView2_RowInserted" OnRowInserting="ASPxGridView2_RowInserting"
                        OnRowUpdated="ASPxGridView2_RowUpdated" 
                        OnRowUpdating="ASPxGridView2_RowUpdating" 
                        OnStartRowEditing="ASPxGridView2_StartRowEditing">
                        <SettingsEditing Mode="Inline"></SettingsEditing>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Tên thể loại" FieldName="Name" ShowInCustomizationForm="True"
                                VisibleIndex="0" Width="200px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn Caption="Chu kì nhỏ nhất" FieldName="IsDefault" ShowInCustomizationForm="True"
                                VisibleIndex="5" Width="70px">
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewCommandColumn Caption="Thao Tác" VisibleIndex="6" ButtonType="Image"
                                AllowDragDrop="True" Width="100px">
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
                                <UpdateButton>
                                    <Image ToolTip="Cập nhật">
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                    </Image>
                                </UpdateButton>
                                <CancelButton>
                                    <Image ToolTip="Bỏ qua">
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                    </Image>
                                </CancelButton>
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <SettingsEditing Mode="Inline" />
                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<dx:XpoDataSource ID="AccountingPeriod" runat="server" TypeName="NAS.DAL.Accounting.Journal.AccountingPeriod"
    Criteria="[Code] &lt;&gt; 'NAAN_DEFAULT' And [RowStatus] &gt;= 0s">
</dx:XpoDataSource>
<dx:XpoDataSource ID="XPOAccountingPeriodLookup" runat="server" Criteria="[RowStatus] &gt; 0s And [IsActive] = True And [Code] &lt;&gt; 'NAAN_DEFAULT' And [AccountingPeriodTypeId.IsDefault] = True"
    TypeName="NAS.DAL.Accounting.Journal.AccountingPeriod">
</dx:XpoDataSource>
<dx:XpoDataSource ID="XPOAccountingPeriodType" runat="server" TypeName="NAS.DAL.Accounting.Period.AccountingPeriodType">
</dx:XpoDataSource>
<dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="cp1" OnCallback="ASPxCallback1_Callback">
</dx:ASPxCallback>
