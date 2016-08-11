<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAccount.ascx.cs" Inherits="WebModule.Accounting.UserControl.ucAccount" %>
<script type = "text/javascript">
    function TypeChange() {
        AccountTree.PerformCallback();    
    }
</script>
<dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="1" RenderMode="Lightweight"
    Width="100%">
    <TabPages>
        <dx:TabPage Text="Loại Tài Khoản">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdAccountType" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataSourceID="dSAccountType" OnRowInserting="ASPxGridView1_RowInserting" KeyFieldName="AccountTypeId"
                        OnRowDeleting="grdAccountType_RowDeleting" 
                        OnRowValidating="grdAccountType_RowValidating">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã Phân Loại" ShowInCustomizationForm="True"
                                VisibleIndex="0" FieldName="Code" Width="15%">
                                <PropertiesTextEdit>
                                    <ValidationSettings>
                                        <RequiredField ErrorText="Chưa nhập mã phân loại." IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Diễn giải" ShowInCustomizationForm="True" VisibleIndex="2"
                                FieldName="Description" Width="45%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Thuộc Phân Hệ" FieldName="AccountCategoryId!Key"
                                ShowInCustomizationForm="True" VisibleIndex="3" Width="15%">
                                <PropertiesComboBox DataSourceID="dSAccountCategory" ValueField="AccountCategoryId"
                                    TextField="Code">
                                    <Columns>
                                        <dx:ListBoxColumn Caption="" FieldName="Code" />
                                    </Columns>
                                    <ValidationSettings>
                                        <RequiredField ErrorText="Chưa chọn phân hệ." IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                VisibleIndex="4" Width="10%">
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
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>                                
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Tên phân loại" FieldName="Name" ShowInCustomizationForm="True"
                                VisibleIndex="1" Width="15%">
                                <PropertiesTextEdit>
                                    <ValidationSettings>
                                        <RequiredField ErrorText="Chưa nhập tên phân loại." IsRequired="True" />
                                    </ValidationSettings>
                                </PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsBehavior ConfirmDelete="True"/>
                        <SettingsEditing Mode="Inline" />
                        <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa không?" />
                        <Styles>
                            <Header HorizontalAlign="Center">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Danh Sách Tài Khoản">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
                        <Items>
                            <%--<dx:LayoutItem Caption="Loại tài khoản">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="cbAccountType" runat="server" DataSourceID="dSAccountType" 
                                            OnValueChanged="cbAccountType_ValueChanged" ValueType="System.Guid" ValueField="AccountTypeId"
                                            TextField="Name" TextFormatString="{1}" Width="300px" 
                                            FilterMinLength="1" EnableCallbackMode="True" 
                                            IncrementalFilteringMode="Contains" NullText="Tất cả">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Mã loại" FieldName="Code" />
                                                <dx:ListBoxColumn Caption="Tên loại" FieldName="Name" />
                                            </Columns>
                                            <ClientSideEvents SelectedIndexChanged ="TypeChange" />
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>--%>
                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                            Width="100%" OnNodeInserted="ASPxTreeList1_NodeInserted" ClientInstanceName ="AccountTree" 
                                            OnNodeInserting="ASPxTreeList1_NodeInserting"
                                            KeyFieldName="AccountId" OnCustomCallback="ASPxTreeList1_CustomCallback" 
                                            ParentFieldName="ParentAccountId!Key" 
                                            OnInitNewNode="ASPxTreeList1_InitNewNode" 
                                            OnNodeValidating="ASPxTreeList1_NodeValidating" style="margin-bottom: 0px" 
                                            OnNodeUpdating="ASPxTreeList1_NodeUpdating" 
                                            OnHtmlDataCellPrepared="ASPxTreeList1_HtmlDataCellPrepared">
                                            <Columns>
                                                <dx:TreeListTextColumn Caption="Số TK" ShowInCustomizationForm="True" 
                                                    VisibleIndex="0" FieldName="Code" Width="10%" SortIndex="0" 
                                                    SortOrder="Ascending">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa nhập số tài khoản." IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:TreeListTextColumn>
                                                <dx:TreeListTextColumn Caption="Tên TK" ShowInCustomizationForm="True" 
                                                    VisibleIndex="1" FieldName="Name" Width="20%">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa nhập tên tài khoản." IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:TreeListTextColumn>
                                                <dx:TreeListTextColumn Caption="Cấp" ShowInCustomizationForm="True" 
                                                    VisibleIndex="2" FieldName="Level" Width="10%">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa nhập cấp." IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:TreeListTextColumn>
                                                <dx:TreeListComboBoxColumn Caption="Loại Số Dư" FieldName="BalanceType" 
                                                    ShowInCustomizationForm="True" VisibleIndex="3" Width="10%">
                                                    <PropertiesComboBox>
                                                        <Items>
                                                            <dx:ListEditItem Text="Nợ" Value="1" />
                                                            <dx:ListEditItem Text="Có" Value="-1" />
                                                            <dx:ListEditItem Text="Lưỡng tính" Value="0" />
                                                            <dx:ListEditItem Text="Không có số dư" Value="-2" />
                                                        </Items>
                                                        <ValidationSettings>
                                                            <RequiredField ErrorText="Chưa chọn loại số dư." IsRequired="True" />
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:TreeListComboBoxColumn>
                                                <dx:TreeListComboBoxColumn Caption="Loại Tài Khoản" 
                                                    ShowInCustomizationForm="True" VisibleIndex="4" 
                                                    FieldName="AccountTypeId!Key">
                                                    <PropertiesComboBox DataSourceID="dSAccountType" TextField="Name" 
                                                        TextFormatString="{1}" ValueField="AccountTypeId" ValueType="System.Guid">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="Code" FieldName="Code" />
                                                            <dx:ListBoxColumn Caption="Name" FieldName="Name" />
                                                        </Columns>
                                                    </PropertiesComboBox>
                                                </dx:TreeListComboBoxColumn>
                                                <dx:TreeListTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" 
                                                    VisibleIndex="5" FieldName="Description" Width="40%">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </dx:TreeListTextColumn>
                                                <dx:TreeListCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" 
                                                    VisibleIndex="6" ButtonType="Image" ShowNewButtonInHeader="True" 
                                                    Width="10%">
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
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:TreeListCommandColumn>
                                            </Columns>
                                            <SettingsEditing AllowRecursiveDelete="True" />
                                            <SettingsText ConfirmDelete="Bạn có chắc chắn muốn xóa không?" />
                                        </dx:ASPxTreeList>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<dx:XpoDataSource ID="dSAccountType" runat="server" TypeName="NAS.DAL.Accounting.AccountChart.AccountType">
</dx:XpoDataSource>
<dx:XpoDataSource ID="dSAccountCategory" runat="server" TypeName="NAS.DAL.Accounting.AccountChart.AccountCategory">
</dx:XpoDataSource>
<dx:XpoDataSource ID="AccountXPO" runat="server" 
    TypeName="NAS.DAL.Accounting.AccountChart.Account" 
    Criteria="[RowStatus]&gt;0">
</dx:XpoDataSource>