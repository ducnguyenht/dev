<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="UnFinishedProduct.aspx.cs" Inherits="WebModule.Produce.Config.UnFinishedProduct" %>

<%@ Register Src="UserControl/uUnFinishedProductEdit.ascx" TagName="uUnFinishedProductEdit"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/uUnFinishedProductGroupEdit.ascx" TagName="uUnFinishedProductGroupEdit"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl/uUnFinishedProductUnitEdit.ascx" TagName="uUnFinishedProductUnitEdit"
    TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        // UnFinishedProduct
        // MainForm Event
        function grDataUnFinishedProduct_EndCallback(s, e) {
            if (s.cpEditUnFinishedProduct) {
                formUnFinishedProductEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnFinishedProduct');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditUnFinishedProduct == 'edit') {
                    cbpanelUserUnFinishedProduct.PerformCallback('edit');
                }
                else {
                    cbpanelUserUnFinishedProduct.PerformCallback('new');
                }
                delete (s.cpEditUnFinishedProduct);
                return;
            }
        }
        function btnCancelUnFinishedProduct_Click(s, e) {
            formUnFinishedProductEdit.Hide();
        }
        // UnFinishedProduct

        // UnFinishedProductGroup
        function grDataUnFinishedProductGroup_EndCallback(s, e) {
            if (s.cpEditUnFinishedProductGroup) {
                formUnFinishedProductGroupEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnFinishedProductGroup');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditUnFinishedProductGroup == 'edit') {
                    cbpanelUserUnFinishedProductGroup.PerformCallback('edit');
                }
                else {
                    cbpanelUserUnFinishedProductGroup.PerformCallback('new');
                }
                delete (s.cpEditUnFinishedProductGroup);
                return;
            }
        }
        function btnCancelUnFinishedProductGroup_Click(s, e) {
            formUnFinishedProductGroupEdit.Hide();
        }
        // UnFinishedProductGroup
        // UnFinishedProductUnit
        function grdataUnFinishedProductUnit_EndCallback(s, e) {
            if (s.cpEditUnFinishedProductUnit) {
                formUnFinishedProductUnitEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerUnFinishedProductUnit');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditUnFinishedProductUnit == 'edit') {
                    cbpanelUserUnFinishedProductUnit.PerformCallback('edit');
                }
                else {
                    cbpanelUserUnFinishedProductUnit.PerformCallback('new');
                }
                delete (s.cpEditUnFinishedProductUnit);
                return;
            }
        }
        function btnCancelUnFinishedProductUnit_Click(s, e) {
            formUnFinishedProductUnitEdit.Hide();
        }
        function buttonCancelDevice_Click(s, e) {
        }
        function buttonSaveDevice_Click(s, e) {
        }

        //FinishedProductUnit
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc3:uUnFinishedProductUnitEdit ID="uUnFinishedProductUnitEdit1" runat="server" />
    <uc2:uUnFinishedProductGroupEdit ID="uUnFinishedProductGroupEdit1" runat="server" />
    <uc1:uUnFinishedProductEdit ID="uUnFinishedProductEdit1" runat="server" />
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" RenderMode="Lightweight"
        ActiveTabIndex="0" Width="100%">
        <TabPages>
            <dx:TabPage Text="Sản Phẩm Dở Dang">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderUnFinishedProduct" runat="server" Text="Danh Mục Sản Phẩm Dở Dang"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelUnFinishedProduct" runat="server" Width="100%" ClientInstanceName="cbpanelUnFinishedProduct">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataUnFinishedProduct" runat="server" Width="100%" ClientInstanceName="grdataUnFinishedProduct"
                                        AutoGenerateColumns="False" KeyFieldName="UnFinishedProductID" OnInitNewRow="grdataUnFinishedProduct_InitNewRow"
                                        OnStartRowEditing="grdataUnFinishedProduct_StartRowEditing">
                                        <ClientSideEvents EndCallback="grDataUnFinishedProduct_EndCallback" />
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
                                            <dx:GridViewDataTextColumn Caption="Mã Sản Phẩm Dở Dang" FieldName="UnFinishedProductID"
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Sản Phẩm Dở Dang" FieldName="UnFinishedProduct" ShowInCustomizationForm="True"
                                                VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                VisibleIndex="2">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                        </Columns>
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                            </Header>
                                            </Styles>
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Nhóm Sản Phẩm Dở Dang">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lbHeaderUnFinishedProductGroup" runat="server" Text="Nhóm Hàng Hóa"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelUnFinishedProductGroup" runat="server" Width="100%"
                            ClientInstanceName="cbpanelUnFinishedProductGroup">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataUnFinishedProductGroup" runat="server" ClientInstanceName="grdataUnFinishedProductGroup"
                                        Width="100%" AutoGenerateColumns="False" KeyFieldName="UnFinishedProductGroupID"
                                        OnInitNewRow="grdataUnFinishedProductGroup_InitNewRow" OnStartRowEditing="grdataUnFinishedProductGroup_StartRowEditing">
                                        <ClientSideEvents EndCallback=" grDataUnFinishedProductGroup_EndCallback" />
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
                                            <dx:GridViewDataTextColumn Caption="Mã Nhóm Hàng Hóa" FieldName="UnFinishedProductGroupID"
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Hàng Hóa" FieldName="UnFinishedProductGroup"
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="UnFinishedProductGroupRowStatus"
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="UnFinishedProductGroupDescription"
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Đơn Vị Tính">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lbheaderUnFinishedProductUnit" runat="server" Text="Đơn Vị Tính"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelUnFinishedProductUnit" runat="server" Width="100%"
                            ClientInstanceName="cbpanelUnFinishedProductUnit">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataUnFinishedProductUnit" runat="server" ClientInstanceName="grdataUnFinishedProductUnit"
                                        Width="100%" AutoGenerateColumns="False" KeyFieldName="UnFinishedProductUnitID"
                                        OnInitNewRow="grdataUnFinishedProductUnit_InitNewRow" OnStartRowEditing="grdataUnFinishedProductUnit_StartRowEditing">
                                        <ClientSideEvents EndCallback=" grdataUnFinishedProductUnit_EndCallback" />
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
                                            <dx:GridViewDataTextColumn Caption="Mã Đơn Vị Tính" FieldName="UnFinishedProductUnitID"
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Đơn Vị Tính" FieldName="UnFinishedProductUnit"
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="UnFinishedProductUnitRowStatus"
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="UnFinishedProductUnitDescription"
                                                ShowInCustomizationForm="True" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                        <Styles>
                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridView>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
