<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="FinishedProduct.aspx.cs" Inherits="WebModule.Produce.Config.FinishedProduct" %>

<%@ Register Src="UserControl/uFinishedProductEdit.ascx" TagName="uFinishedProductEdit"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/uFinishedProductGroupEdit.ascx" TagName="uFinishedProductGroupEdit"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl/uFinishedProductUnitEdit.ascx" TagName="uFinishedProductUnitEdit"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        //FInishedProduct
        // MainForm Event
        function grDataFinishedProduct_EndCallback(s, e) {
            if (s.cpEditFinishedProduct) {
                formFinishedProductEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerFinishedProduct');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditFinishedProduct == 'edit') {
                    cbpanelUserFinishedProduct.PerformCallback('edit');
                }
                else {
                    cbpanelUserFinishedProduct.PerformCallback('new');
                }
                delete (s.cpEditFinishedProduct);
                return;
            }
        }
        function btnCancelFinishedProduct_Click(s, e) {
            formFinishedProductEdit.Hide();
        }
        //FinishedProduct

        //FinishedProductGroup
        function grDataFinishedProductGroup_EndCallback(s, e) {
            if (s.cpEditFinishedProductGroup) {
                formFinishedProductGroupEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerFinishedProductGroup');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditFinishedProductGroup == 'edit') {
                    cbpanelUserFinishedProductGroup.PerformCallback('edit');
                }
                else {
                    cbpanelUserFinishedProductGroup.PerformCallback('new');
                }
                delete (s.cpEditFinishedProductGroup);
                return;
            }
        }
        function btnCancelFinishedProductGroup_Click(s, e) {
            formFinishedProductGroupEdit.Hide();
        }
        //FinishedProductGroup
        //FinishedProductUnit
        function grdataFinishedProductUnit_EndCallback(s, e) {
            if (s.cpEditFinishedProductUnit) {
                formFinishedProductUnitEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerFinishedProductUnit');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditFinishedProductUnit == 'edit') {
                    cbpanelUserFinishedProductUnit.PerformCallback('edit');
                }
                else {
                    cbpanelUserFinishedProductUnit.PerformCallback('new');
                }
                delete (s.cpEditFinishedProductUnit);
                return;
            }
        }
        function btnCancelFinishedProductUnit_Click(s, e) {
            formFinishedProductUnitEdit.Hide();
        }
        //FinishedProductUnit

        function buttonCancelDevice_Click(s, e) {
        }

        function buttonSaveDevice_Click(s, e) {
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <uc3:uFinishedProductUnitEdit ID="uFinishedProductUnitEdit1" runat="server" />
    <uc2:uFinishedProductGroupEdit ID="uFinishedProductGroupEdit1" runat="server" />
    <uc1:uFinishedProductEdit ID="uFinishedProductEdit1" runat="server" />
    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" RenderMode="Lightweight"
        ActiveTabIndex="0" Width="100%">
        <TabPages>
            <dx:TabPage Text="Thành Phẩm">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lblHeaderFinishedProduct" runat="server" Text="Danh Mục Thành Phẩm"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelFinishedProduct" runat="server" Width="100%" ClientInstanceName="cbpanelFinishedProduct">
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataFinishedProduct" runat="server" Width="100%" ClientInstanceName="grdataFinishedProduct"
                                        AutoGenerateColumns="False" KeyFieldName="FinishedProductID" OnInitNewRow="grdataFinishedProduct_InitNewRow"
                                        OnStartRowEditing="grdataFinishedProduct_StartRowEditing">
                                        <ClientSideEvents EndCallback="grDataFinishedProduct_EndCallback" />
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
                                            <dx:GridViewDataTextColumn Caption="Mã Thành Phẩm" FieldName="FinishedProductID"
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành Phẩm" FieldName="FinishedProduct" ShowInCustomizationForm="True"
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
            <dx:TabPage Text="Nhóm Hàng Hóa">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lbHeaderFinishedProductGroup" runat="server" Text="Nhóm Hàng Hóa"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelFinishedProductGroup" runat="server" Width="100%"
                            ClientInstanceName="cbpanelFinishedProductGroup">
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataFinishedProductGroup" runat="server" ClientInstanceName="grdataFinishedProductGroup"
                                        Width="100%" AutoGenerateColumns="False" KeyFieldName="FinishedProductGroupID"
                                        OnInitNewRow="grdataFinishedProductGroup_InitNewRow" OnStartRowEditing="grdataFinishedProductGroup_StartRowEditing">
                                        <ClientSideEvents EndCallback=" grDataFinishedProductGroup_EndCallback" />
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
                                            <dx:GridViewDataTextColumn Caption="Mã Nhóm Hàng Hóa" FieldName="FinishedProductGroupID"
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhóm Hàng Hóa" FieldName="FinishedProductGroup"
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="FinishedProductGroupRowStatus"
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="FinishedProductGroupDescription"
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
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="margin-bottom: 10px;">
                            <dx:ASPxLabel ID="lbheaderFinishedProductUnit" runat="server" Text="Đơn Vị Tính"
                                Font-Bold="True" Font-Size="Small">
                            </dx:ASPxLabel>
                        </div>
                        <dx:ASPxCallbackPanel ID="cbpanelFinishedProductUnit" runat="server" Width="100%"
                            ClientInstanceName="cbpanelFinishedProductUnit">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="grdataFinishedProductUnit" runat="server" ClientInstanceName="grdataFinishedProductUnit"
                                        Width="100%" AutoGenerateColumns="False" KeyFieldName="FinishedProductUnitID"
                                        OnInitNewRow="grdataFinishedProductUnit_InitNewRow" OnStartRowEditing="grdataFinishedProductUnit_StartRowEditing">
                                        <ClientSideEvents EndCallback=" grdataFinishedProductUnit_EndCallback" />
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
                                            <dx:GridViewDataTextColumn Caption="Mã Đơn Vị Tính" FieldName="FinishedProductUnitID"
                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Đơn Vị Tính" FieldName="FinishedProductUnit"
                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="FinishedProductUnitRowStatus"
                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                <PropertiesComboBox>
                                                    <Items>
                                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                    </Items>
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="FinishedProductUnitDescription"
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
