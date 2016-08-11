<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountingEntrylst.ascx.cs" Inherits="WebModule.Accounting.UserControl.AccountingEntrylst" %>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" 
    AutoGenerateColumns="False" KeyFieldName="STT">
    <Columns>
        <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="DienGiai" 
            ShowInCustomizationForm="True" VisibleIndex="0">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="Hệ Thống Tài Khoản Nguồn" ShowInCustomizationForm="True"
            VisibleIndex="1" Width="150px">
            <EditFormSettings Visible="False" />
            <DataItemTemplate>
                <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" Width="150px">
                    <Items>
                        <dx:ListEditItem Text="Danh sách TK Lưu Động" Value="DSTK02" />
                    </Items>
                    <Items>
                        <dx:ListEditItem Text="Danh sách TK Cố Định" Value="DSTK013" />
                    </Items>
                </dx:ASPxComboBox>
            </DataItemTemplate>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewBandColumn Caption="Tài Khoản Nguồn" ShowInCustomizationForm="True" 
            VisibleIndex="2">
            <Columns>
                <dx:GridViewDataComboBoxColumn Caption="Số Hiệu Tài Khoản" ShowInCustomizationForm="True"
                    VisibleIndex="0" Width="150px">
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" ValueType="System.String" Width="150px">
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
                                <dx:ListEditItem Text="Nợ" Value="No" />
                            </Items>
                            <Items>
                                <dx:ListEditItem Text="Có" Value="Co" />
                            </Items>
                        </dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataDropDownEditColumn>
            </Columns>
        </dx:GridViewBandColumn>
        <dx:GridViewBandColumn Caption="Tài Khoản Đích" ShowInCustomizationForm="True" 
            VisibleIndex="3">
            <Columns>
                <dx:GridViewDataComboBoxColumn Caption="Số Hiệu Tài Khoản" ShowInCustomizationForm="True"
                    VisibleIndex="0" Width="150px">
                    <EditFormSettings Visible="False" />
                    <DataItemTemplate>
                        <dx:ASPxComboBox ID="ASPxComboBox4" runat="server" ValueType="System.String" Width="150px">
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
                                <dx:ListEditItem Text="1112.1" Value="DSTK1112.1" />
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
                                <dx:ListEditItem Text="Nợ" Value="No" />
                            </Items>
                            <Items>
                                <dx:ListEditItem Text="Có" Value="Co" />
                            </Items>
                        </dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
        </dx:GridViewBandColumn>
        <dx:GridViewDataTextColumn Caption="Số tiền" FieldName="SoTien" 
            ShowInCustomizationForm="True" VisibleIndex="4">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" 
            ShowInCustomizationForm="True" VisibleIndex="5">
        </dx:GridViewDataTextColumn>
<%--        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
            ShowInCustomizationForm="True" VisibleIndex="7">
            <EditButton Visible="True">
                <Image Height="25px" Url="~/images/edit.png" Width="25px">
                </Image>
            </EditButton>
            <NewButton Visible="True">
                <Image Height="25px" Url="~/images/add.png" Width="25px">
                </Image>
            </NewButton>
            <DeleteButton Visible="True">
                <Image Height="25px" Url="~/images/delete.png" Width="25px">
                </Image>
            </DeleteButton>
        </dx:GridViewCommandColumn>--%>
    </Columns>
    <Settings ShowTitlePanel="True" />
    <SettingsText Title="Các bút toán kết chuyển phát sinh" />
</dx:ASPxGridView>