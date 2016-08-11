<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CombineProduct.aspx.cs" Inherits="WebModule.Purchasing.CombineProduct" %>
<%@ Register Src="UserControl/uProductEdit.ascx" TagName="uProductEdit" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/uPopupEditHelper.ascx" TagName="uPopupEditHelper" TagPrefix="uc2" %>
<%@ Register src="~/UserControls/uCommonDetailInfo.ascx" tagname="uCommonDetailInfo" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var clientAction = '';
        ///////////////////Item////////////////////-START
        function grdProduct_CustomButtonClick(s, e) {
            var key = s.GetRowKey(e.visibleIndex);

            if (e.buttonID == 'AddItem') {
                clientAction = 'Add';
                cpItemEdit.PerformCallback('Add');
            }
            else if (e.buttonID == 'EditItem') {
                clientAction = 'Edit';
                UpdateItemAction(key);
            }
            else if (e.buttonID == 'DeleteItem') {
                clientAction = 'Delete';
                DeleteItemAction(key);
            }
            else if (e.buttonID == 'showCommonDetail3') {
                s.GetRowValues(e.visibleIndex, 'Description', ShowDetail);
                popupCommonDetailInfo.Show();
            }
        }

        function ShowDetail(values) {
            popupCommonDetailInfo.PerformCallback(values);
        }

        function UpdateItemAction(values) {
            var params = new Array('Edit', values);
            cpItemEdit.PerformCallback(params);
        }

        function DeleteItemAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                var params = new Array('Delete', values);
                cpItemEdit.PerformCallback(params);
            } else
                LoadingPanelCombineMaterial.Hide();
        }

        function btnAddItemclick(s, e) {
            cpItemEdit.PerformCallback('Add');
        }

        ///////////////////Item////////////////////-END

        function cboObjectType_SelectedIndexChanged(s, e) {
            grdProduct.PerformCallback("Refresh");
        }

        function grdProduct_Init(s, e) {
            //Press F2 to show edit popup
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var key = s.GetRowKey(focusedRowIndex);
                clientAction = 'Edit';
                UpdateItemAction(key);
            });
            //Press Insert to show insert popup
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                clientAction = 'Add';
                cpItemEdit.PerformCallback('Add');
            });
            //Press Delete to delete record
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var key = s.GetRowKey(focusedRowIndex);
                DeleteItemAction(key);
            });

            s.GetMainElement().focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:aspxpagecontrol id="pHeaders" runat="server" activetabindex="0" rendermode="Lightweight"
        width="100%" ClientInstanceName="pHeaders">
        <TabPages>
            <dx:TabPage Text="Danh mục đối tượng">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="frmItemGeneral" runat="server" Width="100%">
                            <Items>
                                <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False">
                                    <Items>
                                        <dx:LayoutItem Caption="Phân loại đối tượng">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox ID="cboObjectType" runat="server" 
                                                        ClientInstanceName="cboObjectType" DropDownStyle="DropDown" 
                                                        TextFormatString="{0}" TextField="Description"  ValueField="ObjectTypeId" Width="400px"
                                                        SelectedIndex="5">
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="ObjectTypeId" Caption="ObjectTypeId" Width="100%" Visible="false" />
                                                            <dx:ListBoxColumn FieldName="Name" Caption="Tên loại" Width="100px" Visible="false" />
                                                            <dx:ListBoxColumn FieldName="Description" Caption="Mô tả"  Width="100%" />
                                                        </Columns>
                                                        <ClientSideEvents SelectedIndexChanged="cboObjectType_SelectedIndexChanged" />
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer runat="server" 
                                                    SupportsDisabledAttribute="True">
                                                    <dx:ASPxGridView ID="grdProduct" runat="server" AutoGenerateColumns="False" KeyFieldName="ItemId"
                                                        Width="100%" KeyboardSupport="true"
                                                        ClientInstanceName="grdProduct" DataSourceID="ProductXDS" 
                                                        OnCustomCallback="grdProduct_CustomCallback">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn Caption="ItemId" FieldName="ItemId" ShowInCustomizationForm="True"
                                                                Visible="False" VisibleIndex="0" Name="ItemId">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Mã đối tượng" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                                                VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Tên đối tượng" FieldName="Name" Name="Name" ShowInCustomizationForm="True"
                                                                VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                                                VisibleIndex="3" Visible="false">
                                                                <PropertiesComboBox>
                                                                    <Items>
                                                                        <dx:ListEditItem Text="Sử dụng" Value="1" />
                                                                        <dx:ListEditItem Text="Tạm" Value="0" />
                                                                        <dx:ListEditItem Text="Tạm ngưng" Value="-1" />
                                                                    </Items>
                                                                </PropertiesComboBox>
                                                                <EditCellStyle HorizontalAlign="Center">
                                                                </EditCellStyle>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dx:GridViewDataComboBoxColumn>
                                                            <dx:GridViewDataTextColumn Caption="Nhà sản xuất" 
                                                                FieldName="ManufacturerName"
                                                                ShowInCustomizationForm="True" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Description" 
                                                                FieldName="Description"
                                                                ShowInCustomizationForm="True" Visible="false">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                                VisibleIndex="5" Width="60px" Visible="true">
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="showCommonDetail3">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="6"
                                                                Width="100px" ButtonType="Image">
                                                                <CustomButtons>
                                                                    <dx:GridViewCommandColumnCustomButton ID="EditItem">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
				                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                    <dx:GridViewCommandColumnCustomButton ID="AddItem">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                    <dx:GridViewCommandColumnCustomButton ID="DeleteItem">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
				                                                        </Image>
                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                </CustomButtons>
                                                                <ClearFilterButton Visible="True">
                                                                    <Image>
                                                                        <SpriteProperties CssClass="Sprite_Clear" />
                                                                    </Image>
                                                                </ClearFilterButton>
                                                            </dx:GridViewCommandColumn>
                                                        </Columns>
                                                        <Templates>
                                                            <EmptyDataRow>
                                                                <center>
                                                                    <dx:ASPxButton ID="btnAddItem" runat="server" AutoPostBack="false">
                                                                        <Image AlternateText="Thêm mới">
                                                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                                        </Image>
                                                                        <ClientSideEvents Click="btnAddItemclick" />
                                                                    </dx:ASPxButton>
                                                                </center>
                                                            </EmptyDataRow>
                                                        </Templates>
                                                        <Settings ShowTitlePanel="true" />
                                                        <SettingsPager PageSize="16" ShowEmptyDataRows="True">
                                                        </SettingsPager>
                                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                                                            VerticalScrollableHeight="410" VerticalScrollBarMode="Auto" 
                                                            ShowFilterRowMenu="True" />
                                                        <SettingsLoadingPanel Text="" ShowImage="false" Mode="Disabled"/>

                                                        <Styles>
                                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            </Header>
                                                            <HeaderPanel HorizontalAlign="Center">
                                                            </HeaderPanel>
                                                            <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                                            </CommandColumn>
                                                        </Styles>
                                                        <ClientSideEvents 
                                                            Init="grdProduct_Init"
                                                            CustomButtonClick="grdProduct_CustomButtonClick"/>
                                                    </dx:ASPxGridView>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <TabStyle Cursor="pointer">
        </TabStyle>
    </dx:aspxpagecontrol>
    <uc1:uProductEdit ID="uProductEdit1" runat="server" />
    <uc2:uPopupEditHelper ID="uPopupEditHelper1" runat="server" />
    <uc3:uCommonDetailInfo ID="uCommonDetailInfo1" runat="server" />
    <dx:xpodatasource id="ProductUnitXDS" runat="server" 
        typename="NAS.DAL.Nomenclature.Item.Unit">
    </dx:xpodatasource>
    <dx:XpoDataSource ID="ProductXDS" runat="server" 
        TypeName="NAS.DAL.Nomenclature.Item.Item">
    </dx:XpoDataSource>
    <dx:ASPxLoadingPanel ID="LoadingPanelCombineMaterial" runat="server" ClientInstanceName="LoadingPanelCombineMaterial"
        Modal="True" ShowImage="true" Text="Đang xử lý">
        <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
    </dx:ASPxLoadingPanel>
</asp:Content>
