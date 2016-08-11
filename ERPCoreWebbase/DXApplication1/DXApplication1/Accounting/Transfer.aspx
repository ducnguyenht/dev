<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Transfer.aspx.cs" Inherits="WebModule.Accounting.Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .float-right
        {
            float: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Cấu Hình Kết Chuyển" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False" Width="100%">
                <Items>
                    <dx:LayoutItem ShowCaption="True" Caption="Hệ Thống Tài Khoản Đích">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxComboBox ID="ASPxFormLayout1_E1" runat="server" Width="250px" SelectedIndex="0">
                                    <Items>
                                        <dx:ListEditItem Text="Hệ Thống Tài Khoản Công Ty Mẹ" Value="DSTK01" Selected="True" />
                                        <dx:ListEditItem Text="Hệ Thống Tài Khoản Đại Lý 1" Value="DSTK02" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <%--            <dx:LayoutGroup GroupBoxDecoration="None" ShowCaption="False" Width="100%">
                <Items>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>--%>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False"
        KeyFieldName="STT">
        <Columns>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                VisibleIndex="7">
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
                <UpdateButton>
                    <Image>
                        <SpriteProperties CssClass="Sprite_Apply" />
                    </Image>
                </UpdateButton>
                <CancelButton>
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                </CancelButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="STT" FieldName="STT" ShowInCustomizationForm="True"
                VisibleIndex="0" Visible="false">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Width="250px" Caption="Hệ Thống Tài Khoản Nguồn" ShowInCustomizationForm="True"
                VisibleIndex="2">
                <EditFormSettings Visible="False" />
                <DataItemTemplate>
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" Width="250px">
                        <Items>
                            <dx:ListEditItem Text="Hệ Thống Tài Khoản Công Ty Mẹ" Value="DSTK01" />
                        </Items>
                        <Items>
                            <dx:ListEditItem Text="Hệ Thống Tài Khoản Đại Lý 1" Value="DSTK02" Selected="true" />
                        </Items>
                    </dx:ASPxComboBox>
                </DataItemTemplate>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewBandColumn Caption="Tài Khoản Nguồn" ShowInCustomizationForm="True" VisibleIndex="3">
                <Columns>
                    <dx:GridViewDataComboBoxColumn Caption="Số Tài Khoản" ShowInCustomizationForm="True"
                        VisibleIndex="0" Width="70px">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" ValueType="System.String" Width="70px">
                                <Items>
                                    <dx:ListEditItem Text="111" Value="DSTK111" Selected="true" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="1111" Value="DSTK1111" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="1112" Value="DSTK1112" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="1112.1" Value="DSTK1112.1" />
                                </Items>
                            </dx:ASPxComboBox>
                        </DataItemTemplate>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataDropDownEditColumn Caption="Phát Sinh" ShowInCustomizationForm="True"
                        VisibleIndex="1" Width="50px">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" ValueType="System.String" Width="50px">
                                <Items>
                                    <dx:ListEditItem Text="Nợ" Value="No" Selected="true" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="Có" Value="Co" />
                                </Items>
                            </dx:ASPxComboBox>
                        </DataItemTemplate>
                    </dx:GridViewDataDropDownEditColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="Tài Khoản Đích" ShowInCustomizationForm="True" VisibleIndex="4">
                <Columns>
                    <dx:GridViewDataComboBoxColumn Caption="Số Tài Khoản" ShowInCustomizationForm="True"
                        VisibleIndex="0" Width="70px">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" ValueType="System.String" Width="70px">
                                <Items>
                                    <dx:ListEditItem Text="111" Value="DSTK111" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="1111" Value="DSTK1111" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="1112" Value="DSTK1112" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="1111.1" Value="DSTK111.1" Selected="true" />
                                </Items>
                            </dx:ASPxComboBox>
                        </DataItemTemplate>
                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataComboBoxColumn Caption="Phát Sinh" ShowInCustomizationForm="True"
                        VisibleIndex="1" Width="50px">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <dx:ASPxComboBox ID="ASPxComboBox5" runat="server" ValueType="System.String" Width="50px">
                                <Items>
                                    <dx:ListEditItem Text="Nợ" Value="No" Selected="true" />
                                </Items>
                                <Items>
                                    <dx:ListEditItem Text="Có" Value="Co" />
                                </Items>
                            </dx:ASPxComboBox>
                        </DataItemTemplate>
                    </dx:GridViewDataComboBoxColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="DienGiai" ShowInCustomizationForm="True"
                VisibleIndex="1" Width="280px">
                <PropertiesTextEdit>
                    <Style Wrap="True">                        
                    </Style>
                </PropertiesTextEdit>
                <CellStyle Wrap="True">
                </CellStyle>
            </dx:GridViewDataTextColumn>
        </Columns>
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridView>
    <div style = "margin-top:10px;">
        <dx:ASPxButton ID="btn_cancel" runat="server" Text="Bỏ qua" CssClass="float-right  button-right-margin">
            <Image>
                <SpriteProperties CssClass="Sprite_Cancel" />
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btn_save" runat="server" Text="Lưu lại" CssClass="float-right button-right-margin">
            <Image>
                <SpriteProperties CssClass="Sprite_Apply" />
            </Image>
        </dx:ASPxButton>
    </div>
</asp:Content>
