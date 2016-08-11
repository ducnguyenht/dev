<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="AccountingCycle.aspx.cs" Inherits="ERPCore.Accounting.AccountingCycle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        function treelistACCPeriod_Init(s, e) {
            UtilsForTreeList.AttachStandardShortcutToTreeList(s);
            s.ExpandAll();
        }
        function treelistACCPeriod_EndCallback(s, e) {
            if (s.cpSaved) {
                GridACCPeriodType.Refresh();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pageAccountingCyle" RenderMode="Lightweight" runat="server"
        ActiveTabIndex="0" Width="100%" ContentStyle-HorizontalAlign="Center" ClientInstanceName="pageCurrencyUnit">
        <TabPages>
            <dx:TabPage Text="Chu kỳ kế toán">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataCurrencyUnit" ClientInstanceName="ldpn_grdDataCurrencyUnit"
                            ContainerElementID="grdDataCurrencyUnit" runat="server">
                            <LoadingDivStyle BackColor="Transparent">
                            </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="GridACCPeriodType" runat="server" AutoGenerateColumns="False"
                            DataSourceID="DBPeriodType" KeyFieldName="AccountingPeriodTypeId" Width="100%"
                            ClientInstanceName="GridACCPeriodType" KeyboardSupport="True" OnInit="GridACCPeriodType_Init"
                            OnRowDeleting="GridACCPeriodType_RowDeleting" OnRowInserting="GridACCPeriodType_RowInserting"
                            OnRowUpdating="GridACCPeriodType_RowUpdating1">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="CurrencyTypeId" ReadOnly="True" VisibleIndex="0"
                                    Visible="false" ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên Chu Kỳ Thuế" FieldName="Name" VisibleIndex="1"
                                    Width="40%">
                                    <PropertiesTextEdit>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Tên Chu Kỳ Thuế" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="Description" VisibleIndex="2"
                                    Width="30%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RowStatus" VisibleIndex="3" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn Caption="Chu Kỳ Mặc Định" FieldName="IsDefault" VisibleIndex="4"
                                    ShowInCustomizationForm="True" Width="15%">
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" VisibleIndex="5" ButtonType="Image"
                                    AllowDragDrop="True" Width="15%">
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
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel ID="lblTitleCategory" runat="server" Font-Italic="false" Font-Size="Small"
                                        Text="Danh mục chu kỳ kế toán">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTreeList ID="treelistACCPeriod" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="DBPeriod" KeyFieldName="AccountingPeriodId" ClientInstanceName="treelistACCPeriod"
                                        Width="100%" OnInit="treelistACCPeriod_OnInit" ParentFieldName="ParentAccountingPeriodId!Key"
                                        KeyboardSupport="True" OnNodeDeleting="treelistACCPeriod_NodeDeleting" OnNodeInserting="treelistACCPeriod_NodeInserting"
                                        OnNodeUpdating="treelistACCPeriod_NodeUpdating" EncodeHtml="False" OnNodeValidating="treelistACCPeriod_NodeValidating">
                                        <ClientSideEvents Init="treelistACCPeriod_Init"  EndCallback="treelistACCPeriod_EndCallback"/>
                                        <Columns>
                                            <dx:TreeListTextColumn FieldName="AccountingPeriodId!Key" VisibleIndex="0" Visible="false">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Chu kỳ" FieldName="Code" VisibleIndex="1" Width="25%">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings>
                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Chu Kỳ" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListDateTimeColumn Caption="Từ Ngày" FieldName="FromDateTime" VisibleIndex="2"
                                                Width="15%">
                                            </dx:TreeListDateTimeColumn>
                                            <dx:TreeListDateTimeColumn Caption="Đến Ngày" FieldName="ToDateTime" VisibleIndex="3"
                                                Width="15%">
                                            </dx:TreeListDateTimeColumn>
                                            <dx:TreeListCheckColumn Caption="Mặc Định" FieldName="IsActive" VisibleIndex="4"
                                                Width="15%">
                                            </dx:TreeListCheckColumn>
                                            <dx:TreeListTextColumn Caption="Diễn Giải" Name="Description" FieldName="Description"
                                                 VisibleIndex="5" Width="15%">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListCommandColumn VisibleIndex="6" ButtonType="Image" Caption="Thao Tác"
                                                ShowNewButtonInHeader="True" Width="15%">
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
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                    <BackgroundImage VerticalPosition="center" />
                                                </HeaderStyle>
                                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                </CellStyle>
                                                <GroupFooterCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                </GroupFooterCellStyle>
                                                <FooterCellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                </FooterCellStyle>
                                            </dx:TreeListCommandColumn>
                                        </Columns>
                                        <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" ColumnResizeMode="NextColumn" />
                                    </dx:ASPxTreeList>
                                </DetailRow>
                            </Templates>
                            <SettingsDetail ShowDetailRow="true" />
                            <SettingsPager Position="TopAndBottom">
                                <PageSizeItemSettings Items="10, 20, 50" />
                            </SettingsPager>
                            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                            <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle HorizontalAlign="Center">
        </ContentStyle>
    </dx:ASPxPageControl>
    <dx:XpoDataSource ID="DBPeriodType" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Period.AccountingPeriodType"
        Criteria="[RowStatus] &gt; 0">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="DBPeriod" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Journal.AccountingPeriod"
        Criteria="[AccountingPeriodTypeId] = ? And [RowStatus] &gt; 0 And [OrganizationId] = ?">
        <CriteriaParameters>
            <asp:SessionParameter DefaultValue="" Name="AccountingPeriodTypeId" SessionField="SessionAccountingPeriodTypeId" />
            <asp:SessionParameter DefaultValue="" Name="OrganizationId" SessionField="SessionOrganizationId" />
        </CriteriaParameters>
    </dx:XpoDataSource>
</asp:Content>
