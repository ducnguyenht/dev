<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Material.aspx.cs" Inherits="WebModule.Produce.Config.Material" %>

<%@ Register Src="UserControl/uMaterialEdit.ascx" TagName="uMaterialEdit" TagPrefix="uc1" %>
<%@ Register Src="UserControl/uMaterialCategory.ascx" TagName="uMaterialCategory"
    TagPrefix="uc2" %>
<%@ Register src="UserControl/uMaterialUnit.ascx" tagname="uMaterialUnit" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        //Material
        function grDataMaterial_EndCallback(s, e) {
            if (s.cpEditMaterial) {
                formMaterialEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterial');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditMaterial == 'edit') {
                    cbpanelUserMaterial.PerformCallback('edit');
                }
                else {
                    cbpanelUserMaterial.PerformCallback('new');
                }
                delete (s.cpEditMaterial);
                return;
            }
        }
        function btnCancelMaterial_Click(s, e) {
            formMaterialEdit.Hide();
        }
        //Material

        //MaterialCategory
        function grDataMaterialCategory_EndCallback(s, e) {
            if (s.cpEditMaterialCategory) {
                formMaterialCategoryEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialCategory');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditMaterialCategory == 'edit') {
                    cbpanelUserMaterialCategory.PerformCallback('edit');
                }
                else {
                    cbpanelUserMaterialCategory.PerformCallback('new');
                }
                delete (s.cpEditMaterialCategory);
                return;
            }
        }
        function btnCancelMaterialCategory_Click(s, e) {
            formMaterialCategoryEdit.Hide();
        }
        //MaterialCategory
        //MaterialUnit
        function grDataMaterialUnit_EndCallback(s, e) {
            if (s.cpEditMaterialUnit) {
                formMaterialUnitEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerMaterialUnit');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditMaterialUnit == 'edit') {
                    cbpanelUserMaterialUnit.PerformCallback('edit');
                }
                else {
                    cbpanelUserMaterialUnit.PerformCallback('new');
                }
                delete (s.cpEditMaterialUnit);
                return;
            }
        }
        function btnCancelMaterialUnit_Click(s, e) {
            formMaterialUnitEdit.Hide();
        }
        function buttonCancelDevice_Click(s, e) {
        }
        function buttonSaveDevice_Click(s, e) {
        }

        //MaterialUnit
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc2:uMaterialCategory ID="uMaterialCategory1" runat="server" />
    <uc1:uMaterialEdit ID="uMaterialEdit1" runat="server" />
    <uc3:uMaterialUnit ID="uMaterialUnit1" runat="server" />
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" RenderMode="Lightweight"
        ActiveTabIndex="0" Width="100%">
        <TabPages>
            <dx:TabPage Text="Nguyên Vật Liệu">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderMaterial" runat="server" Text="Danh Mục Nguyên Vật Liệu"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelMaterial" runat="server" Width="100%" ClientInstanceName="cbpanelMaterial">
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataMaterial" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdataMaterial"
                                        Width="100%" OnInitNewRow="grdataMaterial_InitNewRow">
                                        <ClientSideEvents EndCallback="grDataMaterial_EndCallback" />
                                        <ClientSideEvents EndCallback="grDataMaterial_EndCallback"></ClientSideEvents>
                                        <Columns>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                VisibleIndex="5">
                                                <EditButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Edit" />
                                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                    </Image>
                                                </EditButton>
                                                <NewButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_New" />
                                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                    </Image>
                                                </NewButton>
                                                <DeleteButton Visible="True">
                                                    <Image>
                                                        <SpriteProperties CssClass="Sprite_Delete" />
                                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                    </Image>
                                                </DeleteButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="Mã Nguyên Vật Liệu" FieldName="MaterialID" ShowInCustomizationForm="True"
                                                VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Nguyên Vật Liệu" FieldName="Material" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Nhà Sản Xuất" FieldName="MaterialManufacturer"
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="MaterialRowStatus"
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MaterialDescription" ShowInCustomizationForm="True"
                                                VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                                        </Settings>
                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                            </Header>
                                            <PreviewRow Font-Bold="False">
                                            </PreviewRow>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Nhóm Nguyên Vật Liệu">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderMaterialCategory" runat="server" Text="Danh mục Nhóm Nguyên Vật Liệu"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxGridView ID="grdataMaterialCategory" runat="server" AutoGenerateColumns="False"
                            Width="100%" OnInitNewRow="grdataMaterialCategory_InitNewRow">
                            <ClientSideEvents EndCallback="grDataMaterialCategory_EndCallback" />
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                    VisibleIndex="4">
                                    <EditButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </DeleteButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Nhóm NVL" FieldName="MaterialCategoryID"
                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên Nhóm NVL" FieldName="MaterialCategoryName"
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="MaterialCategoryRowStatus"
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Sử dụng" Value="A" />
                                            <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MaterialCategoryDescription"
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                            </Styles>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Đơn Vị Tính">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lbHeaderMaterialUnit" runat="server" Text="Danh Mục Đơn Vị Tính"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxGridView ID="grdataMaterialUnit" runat="server" AutoGenerateColumns="False" Width="100%"
                            OnInitNewRow="grdataMaterialUnit_InitNewRow">
                            <ClientSideEvents EndCallback="grDataMaterialUnit_EndCallback" />
                            <Columns>
                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                    VisibleIndex="4">
                                    <EditButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Edit" />
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New" />
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete" />
                                        </Image>
                                    </DeleteButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã " FieldName="MaterialUnitID"
                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên " FieldName="MaterialUnitName"
                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="MaterialUnitRowStatus"
                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Sử dụng" Value="A" />
                                            <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="MaterialUnitDescription"
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                            </Styles>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
