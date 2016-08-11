<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="BankListing.aspx.cs" Inherits="WebModule.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <br />
    &nbsp;&nbsp;&nbsp;
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
        Font-Names="Segoe UI" Font-Size="Large" Text="Danh mục ngân hàng">
    </dx:ASPxLabel>
    <br />
    <br />
    <dx:XpoDataSource ID="dsBanking" runat="server" 
        TypeName="NAS.DAL.Nomenclature.Bank.Bank">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="dsBankBranch" runat="server" Criteria="[BankId!Key] = ?" 
        TypeName="NAS.DAL.Nomenclature.Bank.BankBranch">
    </dx:XpoDataSource>
    <dx:ASPxGridView ID="gridviewBanking" runat="server" 
        AutoGenerateColumns="False" ClientInstanceName="gridviewBanking" 
        DataSourceID="dsBanking" KeyboardSupport="True" KeyFieldName="BankId" 
        Width="100%" onrowdeleting="gridviewBanking_RowDeleting" 
        onrowinserting="gridviewBanking_RowInserting" 
        onrowupdating="gridviewBanking_RowUpdating" 
        onrowvalidating="gridviewBanking_RowValidating">
        <Columns>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="8" 
                ButtonType="Image">
                <EditButton Visible="True">
                    <Image ToolTip="Sửa">
                        <SpriteProperties CssClass="Sprite_Edit" />
                    </Image>
                </EditButton>
                <NewButton Visible="True">
                    <Image ToolTip="Thêm">
                        <SpriteProperties CssClass="Sprite_New" />
                    </Image>
                </NewButton>
                <DeleteButton Visible="True">
                    <Image ToolTip="Xóa">
                        <SpriteProperties CssClass="Sprite_Delete" />
                    </Image>
                </DeleteButton>
                <CancelButton>
                    <Image ToolTip="Bỏ qua">
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                </CancelButton>
                <UpdateButton>
                    <Image ToolTip="Cập nhật">
                        <SpriteProperties CssClass="Sprite_Apply" />
                    </Image>
                </UpdateButton>
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear" />
                    </Image>
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="BankId" ReadOnly="True" Visible="False" 
                VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã số Ngân hàng" FieldName="Code" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
                VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên Ngân hàng" FieldName="Name" 
                VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SwiffCode" FieldName="SwiffCode" 
                VisibleIndex="7">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="NextColumn" ConfirmDelete="True" />
        <SettingsEditing Mode="Inline" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowHeaderFilterButton="True" />
        <SettingsText ConfirmDelete="Xóa ngân hàng này đồng thời sẽ xóa các chi nhánh của nó. Bạn có muốn xóa ?" />
        <SettingsDetail ShowDetailRow="True" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="gridviewBankBranch" runat="server" 
                    AutoGenerateColumns="False" DataSourceID="dsBankBranch" 
                    KeyFieldName="BankBranchId" onrowdeleting="gridviewBankBranch_RowDeleting" 
                    onrowinserting="gridviewBankBranch_RowInserting" 
                    onrowupdating="gridviewBankBranch_RowUpdating" KeyboardSupport="True" 
                    onbeforeperformdataselect="gridviewBankBranch_BeforePerformDataSelect" 
                    oninitnewrow="gridviewBankBranch_InitNewRow" 
                    onrowvalidating="gridviewBankBranch_RowValidating" Width="100%">
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                            VisibleIndex="7">
                            <EditButton Visible="True">
                                <Image ToolTip="Sửa">
                                    <SpriteProperties CssClass="Sprite_Edit" />
                                </Image>
                            </EditButton>
                            <NewButton Visible="True">
                                <Image ToolTip="Thêm">
                                    <SpriteProperties CssClass="Sprite_New" />
                                </Image>
                            </NewButton>
                            <DeleteButton Visible="True">
                                <Image ToolTip="Xóa">
                                    <SpriteProperties CssClass="Sprite_Delete" />
                                </Image>
                            </DeleteButton>
                            <CancelButton>
                                <Image ToolTip="Bỏ qua">
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </CancelButton>
                            <UpdateButton>
                                <Image ToolTip="Cập nhật">
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </UpdateButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="BankBranchId" ReadOnly="True" 
                            Visible="False" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="Address" 
                            VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Code" 
                            VisibleIndex="1" Caption="Mã số chi nhánh">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
                            VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tên chi nhánh" FieldName="Name" 
                            VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Điện thoại/Fax" FieldName="PhoneFax" 
                            VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BankId!Key" 
                            VisibleIndex="6" ReadOnly="True" Visible="False">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior ConfirmDelete="True" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsText ConfirmDelete="Bạn có muốn xóa chi nhánh ngân hàng này ?" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
    </dx:ASPxGridView>
</asp:Content>

