<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Partner.aspx.cs" Inherits="WebModule.GUI.Sales.Partner" %>
<%@ Register Src="~/Sales/UserControl/uEditPartnerGrp.ascx" TagName="uEditPartnerGrp"
    TagPrefix="uc2" %>
<%@ Register src="UserControl/uEditPartner.ascx" tagname="uEditPartner" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function grdPartner_EndCallback(s, e) {
            if (s.cpPartnerEdit) {
                if (s.cpPartnerEdit == 'edit') {
                    hPartnerEditId.Set("id", grdPartner.GetRowKey(grdPartner.GetFocusedRowIndex()));            
                }
                else {            
                    hPartnerEditId.Set("id", '');
                }

                formPartnerEdit.Show();

                delete (s.cpPartnerEdit);
                return;
            }

            if (s.cpRefresh) {
                grdPartner.Refresh();

                delete (s.cpRefresh);
                return;
            }
        }

        function formPartnerEdit_Shown(s, e) {
            pcPartnerEdit.SetActiveTabIndex(0);
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerPartner');

            if (hPartnerEditId.Get("id") == '') {
                cpPartnerEdit.PerformCallback('new');
            }
            else {
                cpPartnerEdit.PerformCallback('edit');
            }
        }

        function grdPartner_CustomButtonClick(s, e) {
            
            //pcPartnerEdit.SetActiveTabIndex(0);
            //ASPxClientEdit.ClearEditorsInContainerById('lineContainerPartner');
            //pcPartnerEdit.SetActiveTabIndex(2);

            hPartnerEditId.Set("id", grdPartner.GetRowKey(grdPartner.GetFocusedRowIndex()));
            formPartnerEdit.Show();
            //pcPartnerEdit.PerformCallback('edit');            
        }

        function bPartnerEditSave_Click(s, e) {
            pcPartnerEdit.SetActiveTabIndex(0);

            if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainerPartner')) {
                e.processOnServer = false;
                return;
            }

            cpPartnerEdit.PerformCallback('save');    
        }

        function cpPartnerEdit_EndCallback(s, e) {
            if (s.cpRefresh) {                
                formPartnerEdit.HideWindow();
                grdPartner.Refresh();

                delete (s.cpRefresh);
                return;
            }
        }

        function bPartnerEditCancel_Click(s, e) {
            formPartnerEdit.HideWindow();
        }

        function bPartnerEditHelp_Click(s, e) {
        }

        function formPartnerEdit_CloseUp(s, e) {
            grdPartnerPartnerCategory.CancelEdit();
        }

        function txtPartnerCode_Validation(s, e) {            
            cpPartnerCode.PerformCallback('txtPartnerCode_Validation');
        }

        //////////////////////////////////////////////////////// PartnerCategory

        function grdPartnerCategory_EndCallback(s, e) {
            if (s.cpPartnerCategoryEdit) {
                if (s.cpPartnerCategoryEdit == 'edit') {
                    hPartnerCategoryId.Set("id", grdPartnerCategory.GetRowKey(grdPartnerCategory.GetFocusedRowIndex()));
                }
                else {
                    hPartnerCategoryId.Set("id", '');
                }

                formPartnerCategoryEdit.Show();

                delete (s.cpPartnerCategoryEdit);
                return;
            }

            if (s.cpRefresh) {
                grdPartnerCategory.Refresh();

                delete (s.cpRefresh);
                return;
            }
        }

        function formPartnerCategoryEdit_Shown(s, e) {
            pcPartnerCategory.SetActiveTabIndex(0);
            ASPxClientEdit.ClearEditorsInContainerById('lineContainerPartnerCategory');

            if (hPartnerCategoryId.Get("id") == '') {
                cpPartnerCategoryEdit.PerformCallback('new');
            }
            else {
                cpPartnerCategoryEdit.PerformCallback('edit');                    
            }
        }

        function grdPartnerCategory_CustomButtonClick(s, e) {
            hPartnerCategoryId.Set("id", grdPartnerCategory.GetRowKey(grdPartnerCategory.GetFocusedRowIndex()));
            formPartnerCategoryEdit.Show();
        }

        function bPartnerCategoryEditSave_Click(s, e) {
            pcPartnerCategory.SetActiveTabIndex(0);

            if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainerPartnerCategory')) {
                e.processOnServer = false;
                return;
            }

            cpPartnerCategoryEdit.PerformCallback('save');            
        }

        function cpPartnerCategoryEdit_EndCallback(s, e) {
            if (s.cpRefresh) {
                formPartnerCategoryEdit.HideWindow();
                grdPartnerCategory.Refresh();

                delete (s.cpRefresh);
                return;
            }                        
        }

        function bPartnerCategoryEditCancel_Click(s, e) {
            formPartnerCategoryEdit.HideWindow();
        }

        function bPartnerCategoryEditHelp_Click(s, e) {
        }

        function txtPartnerCategoryCode_Validation(s, e) {         
            cpPartnerCategoryCode.PerformCallback("txtPartnerCategoryCode_Validation");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="vertical-align: top;">
                <div class="gridContainer">
                   
                    <dx:ASPxPageControl ID="tabs_partner" runat="server" ActiveTabIndex="0" RenderMode="Lightweight"
                        Width="100%">
                        <TabPages>
                            <dx:TabPage Text="Cộng tác viên">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <div>
                                            <dx:ASPxGridView ID="grdPartner" runat="server" AutoGenerateColumns="False"
                                                KeyFieldName="PartnerProperty" Width="100%" 
                                                ClientInstanceName="grdPartner" DataSourceID="PartnerXDS" 
                                                OnInitNewRow="grdPartner_InitNewRow" OnRowDeleting="grdPartner_RowDeleting" 
                                                OnStartRowEditing="grdPartner_StartRowEditing">
                                                <ClientSideEvents CustomButtonClick="grdPartner_CustomButtonClick" 
                                                    EndCallback="grdPartner_EndCallback"></ClientSideEvents>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Mã Cộng Tác Viên" FieldName="Code" VisibleIndex="2"
                                                        SortIndex="1" SortOrder="Ascending" Width="150px">
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên Cộng Tác Viên" FieldName="Name" VisibleIndex="3"
                                                        SortIndex="0" SortOrder="Ascending">
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" 
                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                        <propertiescombobox><Items><dx:ListEditItem Text="Sử dụng" Value="A"></dx:ListEditItem><dx:ListEditItem Text="Tạm ngưng" Value="I"></dx:ListEditItem></Items></propertiescombobox>
                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                        <cellstyle horizontalalign="Center">
                                                        </cellstyle>
                                                    </dx:GridViewDataComboBoxColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" ShowInCustomizationForm="True"
                                                        VisibleIndex="5" Width="60px">
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="showCommonDetail1">
                                                                <Image>
                                                                    <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="7"
                                                        Width="100px" ShowInCustomizationForm="true">
                                                        <editbutton visible="True">
                                                            <image>
<SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                                            </image>
                                                        </editbutton>
                                                        <newbutton visible="True">
                                                            <image>
<SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                                            </image>
                                                        </newbutton>
                                                        <deletebutton visible="True">
                                                            <image>
<SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                                            </image>
                                                        </deletebutton>
                                                        <cancelbutton visible="True">
                                                            <image>
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                            </image>
                                                        </cancelbutton>
                                                        <updatebutton visible="True">
                                                            <image>                                                              
<SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                                            </image>
                                                        </updatebutton>
                                                        <ClearFilterButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Clear" />
                                                            </Image>
                                                        </ClearFilterButton>
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PartnerId" ShowInCustomizationForm="True" 
                                                        Visible="False" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PartnerProperty" 
                                                        ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>                                                
                                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="True" 
                                                    AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                                                <SettingsBehavior ColumnResizeMode="NextColumn"></SettingsBehavior>
                                                <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                                                </SettingsPager>
                                                <SettingsEditing Mode="Inline" />
                                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                    ShowHeaderFilterButton="True" verticalscrollableheight="600" 
                                                    verticalscrollbarmode="Auto">
                                                </Settings>
                                                <Styles>
                                                    <CommandColumn Spacing="10px">
                                                    </CommandColumn>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </div>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Nhóm cộng tác viên">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:ASPxGridView ID="grdPartnerCategory" runat="server" AutoGenerateColumns="False"
                                            KeyFieldName="PartnerCategoryPropertyId" Width="100%" ClientInstanceName="grdPartnerCategory"
                                            DataSourceID="PartnerCategoryXDS" 
                                            OnInitNewRow="grdPartnerCategory_InitNewRow" 
                                            OnRowDeleting="grdPartnerCategory_RowDeleting" 
                                            OnStartRowEditing="grdPartnerCategory_StartRowEditing">
                                            <ClientSideEvents CustomButtonClick="grdPartnerCategory_CustomButtonClick" 
                                                EndCallback="grdPartnerCategory_EndCallback"></ClientSideEvents>
                                            <Columns>
                                                <dx:GridViewDataTextColumn Caption="Mã Nhóm CTV" FieldName="Code" VisibleIndex="2"
                                                    Width="150px">
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="Tên Nhóm CTV" FieldName="Name" 
                                                    VisibleIndex="3">
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="RowStatus" 
                                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
                                                    <PropertiesComboBox>
                                                        <Items>
                                                            
<dx:ListEditItem Text="Sử Dụng" Value="A" />
                                                            
<dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                                        
</Items>
                                                    
</PropertiesComboBox>
                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataComboBoxColumn>
                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                    VisibleIndex="6" Width="100px">
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
                                                    <CancelButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Cancel" />
<SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                                        </Image>
                                                    </CancelButton>
                                                    <UpdateButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Update" />
<SpriteProperties CssClass="Sprite_Update"></SpriteProperties>
                                                        </Image>
                                                    </UpdateButton>
                                                    <ClearFilterButton Visible="True">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Clear" />
                                                        </Image>
                                                    </ClearFilterButton>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" VisibleIndex="5"
                                                    Width="70px">
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton>
                                                            <Image>                                                                
                                                                <SpriteProperties CssClass="Sprite_Document"></SpriteProperties>
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn FieldName="PartnerCategoryPropertyId" 
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" 
                                                    Caption="PartnerCategoryPropertyId">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="PartnerCategoryId" 
                                                    FieldName="PartnerCategoryId" Name="PartnerCategoryId" 
                                                    ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>                                           
                                            <SettingsEditing Mode="Inline" />
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
                                                ShowHeaderFilterButton="True" VerticalScrollableHeight="600" 
                                                VerticalScrollBarMode="Auto" />
                                            <SettingsBehavior AllowFocusedRow="True" 
                                                AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" 
                                                ConfirmDelete="True" />

<SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ColumnResizeMode="NextColumn"></SettingsBehavior>

                                            <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                                            </SettingsPager>

<SettingsEditing Mode="Inline"></SettingsEditing>

<Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" VerticalScrollableHeight="600" VerticalScrollBarMode="Auto"></Settings>

                                            <Styles>
                                                <Header Font-Bold="True" HorizontalAlign="Center">
                                                </Header>
                                                <CommandColumn Spacing="10px">
                                                </CommandColumn>
                                            </Styles>
                                        </dx:ASPxGridView>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <dx:XpoDataSource ID="PartnerCategoryXDS" runat="server" TypeName="DAL.Sale.ViewPartnerCategory">
                </dx:XpoDataSource>
                <dx:XpoDataSource ID="PartnerXDS" runat="server" 
                    TypeName="DAL.Sale.ViewPartner">
                </dx:XpoDataSource>
            </td>
        </tr>
    </table>
    <uc1:uEditPartner ID="uEditPartner1" runat="server" />
    <uc2:uEditPartnerGrp ID="uEditPartnerGrp1" runat="server" />
</asp:Content>
