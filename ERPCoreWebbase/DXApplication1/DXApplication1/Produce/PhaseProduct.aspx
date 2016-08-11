<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PhaseProduct.aspx.cs" Inherits="WebModule.Produce.PhaseProduct" %>

<%@ Register Src="UserControl/PhaseProductEdit.ascx" TagName="PhaseProductEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">

        function grdData_EndCallback(s, e) {
            formPhaseProductEdit.Show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeaderMaterial" runat="server" Text="Công Đoạn Của Sản Phẩm"
        Font-Bold="True" Font-Size="Small">
    </dx:ASPxLabel>
    <table class="style1">
        <tr>
            <td>
                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdData"
                    ClientInstanceName="grdData" OnInitNewRow="grdData_InitNewRow1" OnStartRowEditing="grdData_StartRowEditing"
                    KeyFieldName="Code">
                    <ClientSideEvents EndCallback="grdData_EndCallback" />
                    <ClientSideEvents EndCallback="grdData_EndCallback"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataComboBoxColumn Width="150px" Caption="Mã Sản Phẩm" VisibleIndex="0"
                            FieldName="Code0">
                            <PropertiesComboBox>
                                <Columns>
                                    <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a"></dx:ListBoxColumn>
                                    <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                                    </dx:ListBoxColumn>
                                </Columns>
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn Caption="Tên Sản Phẩm" VisibleIndex="1" FieldName="Name0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ĐVT" VisibleIndex="2" FieldName="Unit">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Mã Công Đoạn" FieldName="Code" VisibleIndex="3"
                            Width="250px" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tên Công Đoạn" FieldName="Name" VisibleIndex="4"
                            Width="200px" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Width="100px" Caption="Thao T&#225;c"
                            VisibleIndex="5" Visible="False">
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
                            <CancelButton Visible="True">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Cancel" />
                                </Image>
                            </CancelButton>
                            <UpdateButton Visible="True">
                                <Image>
                                    <SpriteProperties CssClass="Sprite_Apply" />
                                </Image>
                            </UpdateButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                    <SettingsDetail ShowDetailRow="True" />
                    <SettingsDetail ShowDetailRow="True"></SettingsDetail>
                    <Styles>
                        <CommandColumn Spacing="10px">
                        </CommandColumn>
                    </Styles>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="grdData0" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
                                KeyFieldName="Code" OnBeforePerformDataSelect="grdData0_BeforePerformDataSelect"
                                OnInitNewRow="grdData0_InitNewRow" OnStartRowEditing="grdData0_StartRowEditing"
                                Width="100%">
                                <ClientSideEvents EndCallback="grdData_EndCallback" />
                                <Columns>
                                    <dx:GridViewDataComboBoxColumn Caption="Mã Sản Phẩm" FieldName="Code0" Visible="False"
                                        VisibleIndex="0" Width="150px">
                                        <PropertiesComboBox>
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Mã Nhóm Hàng Hóa" Width="150px" />
                                                <dx:ListBoxColumn Caption="Tên Nhóm Hàng Hóa" Width="300px" />
                                            </Columns>
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên Sản Phẩm" FieldName="Name0" Visible="False"
                                        VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="Unit" Visible="False" VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Mã Công Đoạn" FieldName="Code" VisibleIndex="3"
                                        Width="250px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Tên Công Đoạn" FieldName="Name" VisibleIndex="4"
                                        Width="200px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="5"
                                        Width="100px">
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
                                        <CancelButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </CancelButton>
                                        <UpdateButton Visible="True">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </UpdateButton>
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                </Columns>
                                <SettingsPager PageSize="30">
                                </SettingsPager>
                                <SettingsEditing Mode="Inline" />
                                <Styles>
                                    <CommandColumn Spacing="10px">
                                    </CommandColumn>
                                </Styles>
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
