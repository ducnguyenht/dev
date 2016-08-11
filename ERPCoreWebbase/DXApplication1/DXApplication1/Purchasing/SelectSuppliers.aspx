<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="SelectSuppliers.aspx.cs" Inherits="DXApplication1.Purchasing.approve" %>
<%@ Register src="UserControl/uManufacturerEdit.ascx" tagname="uManufacturerEdit" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<dx:ASPxHiddenField ClientInstanceName="hfDic" ID="hfDic" runat="server">
</dx:ASPxHiddenField>
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Chọn Nhà Cung Cấp" 
        Font-Bold="True" Font-Size="Small">
    </dx:ASPxLabel>
</div>
<table style="width:100%">
<tr>
    <td style="vertical-align:top">
        <div class="gridContainer">
            <dx:ASPxCallbackPanel ID="cpeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
                HideContentOnCallback="True" >
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" 
                            OnInitNewRow="grdData_InitNewRow" OnRowDeleting="grdData_RowDeleting" 
                            OnStartRowEditing="grdData_StartRowEditing"  Width="100%" 
                            ClientInstanceName="grdData">

        <SettingsEditing Mode="Inline"></SettingsEditing>

        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True"></Settings>

        <SettingsDetail ShowDetailRow="True"></SettingsDetail>

                            <ClientSideEvents Init="function(s, e) {
	 var height = Math.max(0, document.documentElement.clientHeight-200);
     grdDataDevice.SetHeight(height);
}" />
<ClientSideEvents Init="function(s, e) {
	 var height = Math.max(0, document.documentElement.clientHeight-200);
     grdDataDevice.SetHeight(height);
}"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="key" ShowInCustomizationForm="True" 
                                    Visible="False" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>                              
                                <dx:GridViewDataTextColumn Caption="Tổng Số Lượng YC" FieldName="amount" 
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="120px">
                                    <CellStyle HorizontalAlign="Right"></CellStyle>                                    
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ĐVT" FieldName="productunit" 
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">                                    
                                    <CellStyle HorizontalAlign="Center"></CellStyle>    
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="name" 
                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                    <DataItemTemplate>
	                                    <div class="wrapColumn">
		                                    <%# Eval("name")%>
	                                    </div>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="code" 
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="150px">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                AllowSelectSingleRowOnly="True" ConfirmDelete="True" ColumnResizeMode="Control" />

<SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" ColumnResizeMode="Control" ConfirmDelete="True"></SettingsBehavior>

                            <SettingsPager PageSize="22" Mode="EndlessPaging">
                            </SettingsPager>
                            <SettingsEditing Mode="Inline" />
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                                VerticalScrollableHeight="1000" />
                            <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />

                            <Styles>
                                <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                </CommandColumn>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                                <HeaderPanel HorizontalAlign="Center">
                                </HeaderPanel>
                            </Styles>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="grdDataDetail" runat="server" AutoGenerateColumns="False" 
                                        EnableTheming="True" OnInitNewRow="grdDataDetail_InitNewRow" 
                                        OnRowDeleting="grdDataDetail_RowDeleting" OnStartRowEditing="grdDataDetail_StartRowEditing" 
                                        Width="100%" 
                                        onbeforeperformdataselect="grdDataDetail_BeforePerformDataSelect" 
                                        KeyFieldName="name">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="Số lượng" VisibleIndex="2" Width="100px" 
                                                FieldName="quantity">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewCommandColumn Caption="Chọn" ShowSelectCheckbox="True" 
                                                VisibleIndex="0" Width="50px">
                                                <ClearFilterButton Visible="True">
                                                </ClearFilterButton>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                VisibleIndex="13" Width="60px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="edit_supplier">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_Edit" />
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="add_supplier">
                                                        <Image>
                                                            <SpriteProperties CssClass="Sprite_New" />
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>                                            
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataDateColumn Caption="Hiệu Lực Tới" FieldName="timeeffect" 
                                                VisibleIndex="12" Width="100px">
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="Ngày TT Dự Kiến" 
                                                FieldName="dpayment" VisibleIndex="11" Width="100px">
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Tỉ Giá" FieldName="Exchange" 
                                                VisibleIndex="8" Visible="False">                                                
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Loại Tiền Tệ" FieldName="Currency" 
                                                VisibleIndex="7" Visible="False">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành Tiền Quy Đổi" FieldName="AmountA" 
                                                VisibleIndex="9" Visible="False">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataDateColumn Caption="Ngày Giao Dự Kiến" FieldName="dday" 
                                                VisibleIndex="10" Width="100px">
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="Ghi chú người đề xuất" FieldName="note" 
                                                VisibleIndex="12" Width="250px" >
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                                <CellStyle Wrap="True">
                                                </CellStyle>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn Caption="Ghi chú người duyệt" FieldName="" 
                                                VisibleIndex="12" Width="250px" >
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                                <CellStyle Wrap="True">
                                                </CellStyle>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn Caption="Thành tiền" FieldName="sum" 
                                                VisibleIndex="6" Width="120px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="costs" VisibleIndex="5" Width="100px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Chiết khấu (%)" FieldName="discount" 
                                                VisibleIndex="4" Width="60px">                                                
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Đơn giá" FieldName="price" VisibleIndex="3" 
                                                Width="100px">
                                                <CellStyle HorizontalAlign="Right"></CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="Tên Nhà Cung Cấp" FieldName="name" 
                                                VisibleIndex="1" Width="300px">
                                                <DataItemTemplate>
	                                                <div class="wrapColumn">
		                                                <%# Eval("name")%>
	                                                </div>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" 
                                            ConfirmDelete="True" ColumnResizeMode="Control"  />
                                        <SettingsPager PageSize="10" ShowEmptyDataRows="true">
                                        </SettingsPager>
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                                            HorizontalScrollBarMode="Auto" />
                                        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" />
                                        <SettingsEditing Mode="Inline" />
                                        <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                        <Styles>
                                            <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                            </CommandColumn>
                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" 
                                                Wrap="True">
                                            </Header>
                                            <HeaderPanel HorizontalAlign="Center">
                                            </HeaderPanel>
                                        </Styles>
                                    </dx:ASPxGridView>
                                    <br />
                                    <div style="float:right;width:100%">
                                        <dx:ASPxButton ID="btnApprove" Text="Duyệt" runat="server">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Approve" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </div>
                                </DetailRow>
                            </Templates>
                        </dx:ASPxGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
         </div>
    </td>
</tr>
<tr>
    <td>
    
    </td>
</tr>
<tr>
    <td>               
    
    </td>
</tr>
</table>

</asp:Content>
