<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CurrencyUnit.aspx.cs"
    Inherits="WebModule.Accounting.CurrencyUnit" %>
    <%@ Register src="UserControl/uCurrency.ascx" tagname="uCurrency" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function TLCurrency_Init(s, e) {
            UtilsForTreeList.AttachStandardShortcutToTreeList(s);
            s.ExpandAll();
        }
        function treelistCurrency_EndCallback(s, e) {
            if (s.cpSaved) {
                GridCurrencyUnit.Refresh();
            }
        }
        function pageCurrencyUnit_TabClick(s, e) {
            Grid_CurrencyType.Refresh();
            GridCurrencyUnit.Refresh();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pageCurrencyUnit" RenderMode="Lightweight" runat="server"
        ActiveTabIndex="0" Width="100%" ContentStyle-HorizontalAlign="Center" 
        ClientInstanceName="pageCurrencyUnit" EnableHierarchyRecreation="True">
        <ClientSideEvents TabClick="pageCurrencyUnit_TabClick" />
        <TabPages>
            <dx:TabPage Text="Đơn Vị Tiền Tệ">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataCurrencyUnit" ClientInstanceName="ldpn_grdDataCurrencyUnit"
                            ContainerElementID="grdDataCurrencyUnit" runat="server">
                            <LoadingDivStyle BackColor="Transparent">
                            </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <dx:ASPxGridView ID="GridCurrencyUnit" runat="server" AutoGenerateColumns="False"
                            DataSourceID="DBCurrencyType" KeyFieldName="CurrencyTypeId" Width="100%" ClientInstanceName="GridCurrencyUnit"
                            KeyboardSupport="True" OnRowInserting="GridCurrencyUnit_RowInserting" OnRowDeleting="GridCurrencyUnit_RowDeleting"
                            OnRowUpdating="GridCurrencyUnit_RowUpdating" OnInit="GridCurrencyUnit_Init">
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="CurrencyTypeId" ReadOnly="True" VisibleIndex="0"
                                    Visible="false" ShowInCustomizationForm="True">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên Tiền Tệ" FieldName="Name" VisibleIndex="1"
                                    Width="40%">
                                    <PropertiesTextEdit>
                                        <ValidationSettings Display="Dynamic">
                                            <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Tên tiền tệ" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="Description" VisibleIndex="2"
                                    Width="40%">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataCheckColumn Caption="Sử Dụng Chính" FieldName="IsMaster" VisibleIndex="3"
                                    ShowInCustomizationForm="True" Width="10%">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" VisibleIndex="4" ButtonType="Image"
                                    AllowDragDrop="True" Width="10%">
                                    <EditButton Visible="True">
                                        <Image ToolTip="Sửa">
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image ToolTip="Thêm">
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image ToolTip="Xóa">
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <UpdateButton>
                                        <Image ToolTip="Cập nhật">
                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton>
                                        <Image ToolTip="Bỏ qua">
                                            <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                        </Image>
                                    </CancelButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                    </CellStyle>
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxLabel ID="lblTitleCategory" runat="server" Font-Italic="false" Font-Size="Small"
                                        Text="Danh mục đơn vị tiền tệ">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTreeList ID="treelistCurrency" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="DBCurrency" KeyFieldName="CurrencyId" ClientInstanceName="TLCurrency"
                                        Width="100%" OnInit="TLCurrency_OnInit" OnNodeInserting="TLCurrency_NodeInserting"
                                        ParentFieldName="ParentCurrencyId!Key" KeyboardSupport="True" OnNodeUpdating="treelistCurrency_NodeUpdating"
                                        OnNodeValidating="treelistCurrency_NodeValidating" OnCellEditorInitialize="treelistCurrency_CellEditorInitialize"
                                        OnHtmlDataCellPrepared="treelistCurrency_HtmlDataCellPrepared" OnNodeDeleting="treelistCurrency_NodeDeleting">
                                        <ClientSideEvents Init="TLCurrency_Init" EndCallback="treelistCurrency_EndCallback" />
                                        <Columns>
                                            <dx:TreeListTextColumn FieldName="CurrencyId" VisibleIndex="0" Visible="false">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Mã Đơn Vị Tiền Tệ" FieldName="Code" VisibleIndex="1"
                                                Width="15%">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings Display="Dynamic">
                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Mã Đơn Vị Tiền Tệ" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Tên" FieldName="Name" VisibleIndex="2" Width="15%">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings Display="Dynamic">
                                                        <RequiredField IsRequired="true" ErrorText="Bắt buộc nhập Tên" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListSpinEditColumn Caption="Bao gồm" FieldName="NumRequired" VisibleIndex="3"
                                                Width="15%">
                                                <PropertiesSpinEdit NumberType="Float" DecimalPlaces="3" MinValue="0">
                                                    <ValidationSettings ValidateOnLeave="False" Display="Dynamic">
                                                        <RequiredField ErrorText="Chưa Nhập Số" IsRequired="True" />
                                                    </ValidationSettings>
                                                </PropertiesSpinEdit>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </dx:TreeListSpinEditColumn>
                                            <dx:TreeListTextColumn Caption="Hệ Số Quy Đổi" FieldName="Coefficient" VisibleIndex="4"
                                                ReadOnly="true" Width="15%">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="Diễn Giải" Name="Description" FieldName="Description"
                                                ReadOnly="true" VisibleIndex="5" Width="22%">
                                                <EditCellTemplate>
                                                </EditCellTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListCheckColumn Caption="Đơn Vị Mặc Định" FieldName="IsDefault" VisibleIndex="6"
                                                Width="8%">
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                                                </CellStyle>
                                            </dx:TreeListCheckColumn>
                                            <dx:TreeListCommandColumn VisibleIndex="7" ButtonType="Image" Caption="Thao Tác"
                                                ShowNewButtonInHeader="True" Width="10%">
                                                <EditButton Visible="True">
                                                    <Image ToolTip="Sửa">
                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                    </Image>
                                                </EditButton>
                                                <NewButton Visible="True">
                                                    <Image ToolTip="Thêm">
                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                    </Image>
                                                </NewButton>
                                                <DeleteButton Visible="True">
                                                    <Image ToolTip="Xóa">
                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                    </Image>
                                                </DeleteButton>
                                                <UpdateButton>
                                                    <Image ToolTip="Cập nhật">
                                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                    </Image>
                                                </UpdateButton>
                                                <CancelButton>
                                                    <Image ToolTip="Bỏ qua">
                                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                    </Image>
                                                </CancelButton>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </dx:TreeListCommandColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowPager">
                                        </SettingsPager>
                                        <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" ColumnResizeMode="NextColumn"
                                            AllowFocusedNode="True" />
                                    </dx:ASPxTreeList>
                                </DetailRow>
                            </Templates>
                            <SettingsPager PageSize="20" Position="Bottom">
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                            </SettingsPager>
                            <SettingsDetail ShowDetailRow="true" />
                            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="NextColumn" />
                            <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Tỷ Giá">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdDataExc" ClientInstanceName="ldpn_grdDataExc"
                            ContainerElementID="grdDataExc" runat="server">
                            <LoadingDivStyle BackColor="Transparent">
                            </LoadingDivStyle>
                        </dx:ASPxLoadingPanel>
                        <uc1:uCurrency ID="uCurrency1" runat="server" />
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle HorizontalAlign="Center">
        </ContentStyle>
    </dx:ASPxPageControl>
    <dx:XpoDataSource ID="DBCurrencyType" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Currency.CurrencyType"
        Criteria="[RowStatus] &gt; 0">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="DBCurrency" runat="server" DefaultSorting="" TypeName="NAS.DAL.Accounting.Currency.Currency"
        Criteria="[CurrencyTypeId] = ? And [RowStatus] &gt; 0">
        <CriteriaParameters>
            <asp:SessionParameter DefaultValue="" Name="CurrencyTypeId" SessionField="SessionCurrencyTypeId" />
        </CriteriaParameters>
    </dx:XpoDataSource>
</asp:Content>
