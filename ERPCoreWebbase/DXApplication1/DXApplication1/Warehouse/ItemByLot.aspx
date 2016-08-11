<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ItemByLot.aspx.cs" Inherits="WebModule.Warehouse.ItemByLot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function AddKeyboardNavigationTo(grid, type) {
            ASPxClientUtils.AttachEventToElement(grid.GetMainElement(), 'keydown', function (evt) {
                return OnDocumentKeyDown(evt, grid, type);
            });

            grid.GetMainElement().focus();
        }

        function RemoveKeyboardNavigationTo(grid, type) {
            ASPxClientUtils.DetachEventFromElement(grid.GetMainElement(), 'keydown', function (evt) {
                return OnDocumentKeyDown(evt, grid, type);
            });

            grid.GetMainElement().focus();
        }

        function OnDocumentKeyDown(evt, grid, type) {
            var currentIndex = grid.GetFocusedRowIndex();

            if (typeof (event) != "undefined" && event != null)
                evt = event;

            if (evt.ctrlKey) {
                //Enter
                if (evt.keyCode == 13) {
                    if (type == 'line') {
                        grid.UpdateEdit();
                    }
                }
            }
            else {
                if (evt.keyCode == 27) //Esc
                {
                    if (type == 'header') {
                    }
                    else {
                        grid.CancelEdit();
                    }
                }
                if (evt.keyCode == 113) //F2
                {
                    if (type == 'header') {
                        showFormEditing('edit');
                    }
                    else {
                        if (grid.kbdHelper == ASPxKbdHelper.active) {
                            grid.StartEditRow(currentIndex);
                        }
                    }
                }
                if (evt.keyCode == 45) //Insert
                {
                    if (type == 'header') {
                        showFormEditing('new');
                    }
                    else {
                        if (grid.kbdHelper == ASPxKbdHelper.active) {
                            grid.AddNewRow();
                        }
                    }
                }
                if (evt.keyCode == 46) //Del
                {
                    if (confirm("Xóa số lô này ?")) {
                        if (grid.kbdHelper == ASPxKbdHelper.active) {
                            grid.DeleteRow(currentIndex);
                        }
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////

        function grdItem_Init(s,e) {
            RemoveKeyboardNavigationTo(grdItem, 'line');
            AddKeyboardNavigationTo(grdItem, 'line');
            grdItem.PerformCallback("Refresh");
        }
        function ItemId_SelectedIndexChanged(s, e) {
            var ItemId = s.GetSelectedItem();
            if (ItemId != null) {
                var Name = ItemId.GetColumnText('Name');
                var editor = grdItem.GetEditor('Item_Name');
                if (Name != null) {
                    editor.SetValue(Name);
                }
                var name = ItemId.GetColumnText('ManufacturerOrgId.Name');
                var editor = grdItem.GetEditor('Manu_Name');
                if (name != null) {
                    editor.SetValue(name);
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="captionFormName">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Số lô theo hàng hóa" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxGridView ID="grdItem" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdItem"
        DataSourceID="DBLot" KeyFieldName="LotId" OnCellEditorInitialize="grdItem_CellEditorInitialize"
        KeyboardSupport="True" OnRowDeleting="grdItem_RowDeleting" OnRowValidating="grdItem_RowValidating"
        Width="100%"
        OnRowUpdating="grdItem_RowUpdating" onrowinserted="grdItem_RowInserted" 
        onrowinserting="grdItem_RowInserting">
        <ClientSideEvents Init="grdItem_Init" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Số lô" FieldName="Code" VisibleIndex="0" Width="10%">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorTextPosition="Bottom">
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditCellStyle VerticalAlign="Middle">
                </EditCellStyle>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Hạn dùng" FieldName="ExpireDate" VisibleIndex="1"
                Width="10%">
                <PropertiesDateEdit>
                    <ValidationSettings ErrorTextPosition="Bottom">
                        <RequiredField ErrorText="Chưa chọn hạn dùng" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesDateEdit>
                <EditCellStyle>
                    <Paddings PaddingTop="15px" />
                </EditCellStyle>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Mã hàng hóa" FieldName="ItemId!Key" VisibleIndex="2"
                Width="20%" ShowInCustomizationForm="True">
                <PropertiesComboBox EnableCallbackMode="True" DataSourceID="ItemXDS" IncrementalFilteringMode="Contains"
                    TextField="Code" TextFormatString="{0}" ValueField="ItemId">
                    <Columns>
                        <dx:ListBoxColumn FieldName="ItemId" Visible="false" />
                        <dx:ListBoxColumn Caption="Mã hàng hóa" FieldName="Code" Width="150px" />
                        <dx:ListBoxColumn Caption="Tên hàng hóa" FieldName="Name" Width="200px" />
                        <dx:ListBoxColumn Caption="Nhà sản xuất" FieldName="ManufacturerOrgId.Name" />
                    </Columns>
                    <ClientSideEvents SelectedIndexChanged="ItemId_SelectedIndexChanged" />
                    <ValidationSettings>
                        <RequiredField ErrorText="Chưa chọn hàng hóa !" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ItemId.Name" VisibleIndex="3" Name="Item_Name"
                Width="20%">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Nhà sản xuất" VisibleIndex="5" Width="20%" FieldName="ItemId.ManufacturerOrgId.Name" Name="Manu_Name"> 
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="6"
                Width="10%">
                <EditButton Visible="true">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Edit" />
                    </Image>
                </EditButton>
                <NewButton Visible="true">
                    <Image>
                        <SpriteProperties CssClass="Sprite_New" />
                    </Image>
                </NewButton>
                <DeleteButton Visible="true">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Delete" />
                    </Image>
                </DeleteButton>
                <CancelButton Visible="true">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                </CancelButton>
                <UpdateButton Visible="true">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Apply" />
                    </Image>
                </UpdateButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle">
                </CellStyle>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" ColumnResizeMode="NextColumn" />
        <SettingsPager PageSize="20" Position="Bottom">
            <PageSizeItemSettings Visible="true" Items="10, 20, 50">
            </PageSizeItemSettings>
        </SettingsPager>
        <SettingsEditing Mode="Inline" />
        <Settings ShowFilterRow="True" />
        <SettingsText ConfirmDelete="Bạn Có Chắc Xóa Số Lô Này Không?" />
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="ItemXDS" runat="server" TypeName="NAS.DAL.Nomenclature.Item.Item"
        Criteria="[RowStatus] > 0">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="DBLot" runat="server" TypeName="NAS.DAL.Inventory.Lot.Lot"
        Criteria="[RowStatus] > 0">
    </dx:XpoDataSource>
</asp:Content>
