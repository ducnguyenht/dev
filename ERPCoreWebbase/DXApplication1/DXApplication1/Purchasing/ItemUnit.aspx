<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ItemUnit.aspx.cs" Inherits="WebModule.Purchasing.ItemUnit" %>
<%@ Register Src="UserControl/uUnitEdit.ascx" TagName="uUnitEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        ///////////////////Unit////////////////////-START
        function grdUnit_CustomButtonClick(s, e) {
            if (e.buttonID == 'AddUnit') {
                clientAction = 'Add';
                cpUnitEdit.PerformCallback(clientAction);
            }
            else if (e.buttonID == 'EditUnit') {
                s.GetRowValues(e.visibleIndex, 'UnitId', UpdateUnitAction);
            }
            else if (e.buttonID == 'DeleteUnit') {
                s.GetRowValues(e.visibleIndex, 'UnitId', DeleteUnitAction);
            }
        }

        function UpdateUnitAction(values) {
            clientAction = 'Edit';
            var params = new Array(clientAction, values);
            cpUnitEdit.PerformCallback(params);
        }

        function DeleteUnitAction(values) {
            var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
            if (confirmMessage == true) {
                clientAction = 'Delete';
                var params = new Array(clientAction, values);
                cpUnitEdit.PerformCallback(params);
            } else
                LoadingPanelCombineMaterial.Hide();
        }

        function btnAddUnitclick(s, e) {
            clientAction = 'Add';
            cpUnitEdit.PerformCallback(clientAction);
        }
        ///////////////////Unit////////////////////-END

        function grdUnit_Init(s, e) {
            //Press F2 to show edit popup
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var key = s.GetRowKey(focusedRowIndex);
                UpdateUnitAction(key);
            });
            //Press Insert to show insert popup
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                clientAction = 'Add';
                cpUnitEdit.PerformCallback('Add');
            });
            //Press Delete to delete record
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var key = s.GetRowKey(focusedRowIndex);
                DeleteUnitAction(key);
            });

            s.GetMainElement().focus();
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div>
        <dx:ASPxGridView ID="grdUnit" runat="server" AutoGenerateColumns="False" KeyFieldName="UnitId"
            Width="100%" KeyboardSupport="true"
            DataSourceID="ProductUnitXDS" ClientInstanceName="grdUnit">
            <ClientSideEvents 
                CustomButtonClick="grdUnit_CustomButtonClick" Init="grdUnit_Init">
            </ClientSideEvents>
            <Columns>
                <dx:GridViewDataTextColumn Caption="UnitId" FieldName="UnitId" ShowInCustomizationForm="True"
                    Visible="False" VisibleIndex="0" Name="UnitId">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Mã Quy Cách" FieldName="Code" Name="Code"
                    ShowInCustomizationForm="True" VisibleIndex="1" Width="15%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tên Quy Cách" FieldName="Name" Name="Name"
                    ShowInCustomizationForm="True" VisibleIndex="2" Width="25%">                              
                </dx:GridViewDataTextColumn>          
                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                    VisibleIndex="3" Width="100px" Visible="false">
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
                <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="Description" Name="Description"
                    ShowInCustomizationForm="True" VisibleIndex="4" Width="200px" Visible="false">                                   
                </dx:GridViewDataTextColumn>               
                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                    VisibleIndex="5" Width="50%" Visible="false">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Document" />
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>           
                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                    ShowInCustomizationForm="True" VisibleIndex="6" Width="10%">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="EditUnit">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
				            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="AddUnit">
                            <Image>
                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="DeleteUnit">
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
                        <dx:ASPxButton ID="btnAddUnit" runat="server" AutoPostBack="false">
                            <Image>
                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				            </Image>
                            <ClientSideEvents Click="btnAddUnitclick" />
                        </dx:ASPxButton>
                    </center>
                </EmptyDataRow>
            </Templates>                  
            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" 
                ColumnResizeMode="Control" ConfirmDelete="True" AllowFocusedRow="True">
            </SettingsBehavior>
            <SettingsPager PageSize="20" ShowEmptyDataRows="True" Mode="ShowPager">
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                ShowFilterRowMenu="True" VerticalScrollableHeight="500" 
                VerticalScrollBarMode="Auto" />
            <SettingsEditing Mode="Inline"></SettingsEditing>
            <SettingsLoadingPanel Text="" ShowImage="false" Mode="Disabled"/>
            <Styles>
                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                </Header>
                <HeaderPanel HorizontalAlign="Center">
                </HeaderPanel>
                <CommandColumn HorizontalAlign="Center" Spacing="10px">
                </CommandColumn>
            </Styles>
        </dx:ASPxGridView>  
        <uc1:uUnitEdit ID="uUnitEdit1" runat="server" />
        <dx:xpodatasource id="ProductUnitXDS" runat="server" 
            typename="NAS.DAL.Nomenclature.Item.Unit">
        </dx:xpodatasource>   
    </div>
</asp:Content>
