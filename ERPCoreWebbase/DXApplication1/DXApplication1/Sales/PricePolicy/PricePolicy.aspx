<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="PricePolicy.aspx.cs" Inherits="WebModule.Sales.PricePolicy.PricePolicyPage" %>
<%@ Register src="~/Sales/PricePolicy/uPricePolicyEditting.ascx" tagname="uPricePolicyEditting" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <title></title>
    <script type="text/javascript">
        function grdPricePolicy_CustomButtonClick(s, e) {
            if (e.buttonID == 'New' )
                cpPricePolicyEditting.PerformCallback("New");
            else if (e.buttonID == 'Edit')
            {
                var key = s.GetRowKey(e.visibleIndex);
                var params = new Array('Edit', key);
                cpPricePolicyEditting.PerformCallback(params);
            }
        }

        function btnAddPricePolicyclick(s, e) {
            cpPricePolicyEditting.PerformCallback("New");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="gridContainer">
        <dx:aspxpagecontrol id="ASPxPageControl_chinhsach" runat="server" rendermode="Lightweight"
            activetabindex="0" width="100%" height="100%">  
            <TabPages>
                <dx:TabPage Text="Phương Pháp Tính Giá Bán">                            
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl_danhmuccs" runat="server" >                                 
                            <dx:ASPxGridView ID="grdPricePolicy" ClientInstanceName="grdPricePolicy" 
                                runat="server" AutoGenerateColumns="False" 
                                KeyFieldName="PricePolicyId"
                                OnCustomButtonCallback="grdPricePolicy_CustomButtonCallback" 
                                DataSourceID="PricePolicyXDS">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="Name" ShowInCustomizationForm="True" 
                                        VisibleIndex="0" Width="20%">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Ngày bắt đầu" FieldName="ValidFrom" ShowInCustomizationForm="True" 
                                        VisibleIndex="1" Width="15%">
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataDateColumn Caption="Ngày kết thúc" FieldName="ValidTo" ShowInCustomizationForm="True" 
                                        VisibleIndex="2" Width="15%">
                                    </dx:GridViewDataDateColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Phân loại" 
                                        FieldName="PricePolicyTypeId!Key" ShowInCustomizationForm="True" 
                                        VisibleIndex="3" Width="20%">
                                        <PropertiesComboBox DataSourceID="PricePolicyTypeXDS" ValueField="PricePolicyTypeId" 
                                            TextField="Name" Width="100%">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Trạng thái" FieldName="RowStatus" ShowInCustomizationForm="True"
                                        VisibleIndex="4" Visible="true" Width="15%">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dx:ListEditItem Text="Sử dụng" Value="1" />
                                                <dx:ListEditItem Text="Tạm ngưng" Value="2" />
                                            </Items>
                                        </PropertiesComboBox>
                                        <EditCellStyle HorizontalAlign="Center">
                                        </EditCellStyle>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
		                                ShowInCustomizationForm="True" VisibleIndex="6" Width="15%">
		                                <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="Edit">
				                                <Image>
				                                <SpriteProperties CssClass="Sprite_Edit" />
			                                </Image>
			                                </dx:GridViewCommandColumnCustomButton>
			                                <dx:GridViewCommandColumnCustomButton ID="New">
				                                <Image>
				                                <SpriteProperties CssClass="Sprite_New" />
			                                </Image>
			                                </dx:GridViewCommandColumnCustomButton>
		                                </CustomButtons>
		                                <CustomButtons>
			                                <dx:GridViewCommandColumnCustomButton ID="Delete">
				                                <Image>
					                                <SpriteProperties CssClass="Sprite_Delete" />
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
                                            <dx:ASPxButton ID="btnAddPricePolicy" runat="server" AutoPostBack="false">
                                                <Image AlternateText="Thêm mới">
                                                    <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
				                                </Image>
                                                <ClientSideEvents Click="btnAddPricePolicyclick" />
                                            </dx:ASPxButton>
                                        </center>
                                    </EmptyDataRow>
                                </Templates>
                                <ClientSideEvents CustomButtonClick="grdPricePolicy_CustomButtonClick" />
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"/>
                                <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
                                <SettingsDetail ShowDetailRow="false" />
                                <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
                                    <Styles>
                                    <Header HorizontalAlign="Center" Font-Bold="true">
                                    </Header>             
                                    <CommandColumn Spacing="10px">
                                    </CommandColumn>
                                </Styles>
                            </dx:ASPxGridView>
                            <dx:XpoDataSource ID="PricePolicyXDS" runat="server" 
                                Criteria="[RowStatus] &gt; 0" DefaultSorting="" 
                                TypeName="NAS.DAL.Sales.Price.PricePolicy">
                            </dx:XpoDataSource>
                            <dx:XpoDataSource ID="PricePolicyTypeXDS" runat="server" 
                                Criteria="[RowStatus] &gt; 0" DefaultSorting="" 
                                TypeName="NAS.DAL.Sales.Price.PricePolicyType">
                            </dx:XpoDataSource>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="Phương Pháp Làm Tròn Giá" Visible="false">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl_pplamtron" runat="server">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" 
                                AlignItemCaptionsInAllGroups="True">
                                <Items>
                                    <dx:LayoutItem Caption="Phương pháp làm tròn">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                <dx:ASPxComboBox ID="ASPxFormLayout1_E1" runat="server" Width="250px">
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                        <CaptionSettings HorizontalAlign="Right" />
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Giá trị làm tròn" 
                                        HelpText="Làm tròn lên/xuống đến [giá trị làm tròn]">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                    >
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="250px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                        <CaptionSettings HorizontalAlign="Right" />
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Ví dụ">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                    >
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Số chưa làm tròn">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                    >
                                                <dx:ASPxTextBox ID="ASPxFormLayout1_E2" runat="server" Width="250px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                        <CaptionSettings HorizontalAlign="Right" />
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Số đã làm tròn">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer runat="server">                                                         
                                                <dx:ASPxLabel ID="ASPxFormLayout1_E3" runat="server" RightToLeft="False" 
                                                    Width="250px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                        <CaptionSettings HorizontalAlign="Right" />
                                    </dx:LayoutItem>
                                </Items>
                                <SettingsItems HorizontalAlign="Right" ShowCaption="True" />
                            </dx:ASPxFormLayout>
                            <br />
                            <dx:ASPxButton ID="ASPxButton11" runat="server" Text="Lưu" CssClass = "style1" Image-SpriteProperties-CssClass="Sprite_Apply">
                            <Image>
                                <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                            </Image>
                            </dx:ASPxButton>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:aspxpagecontrol>
    </div>    
    <dx:aspxpopupcontrol id="popup_editPriceForProduct" clientinstancename="popup_editPriceForProduct"
        runat="server" rendermode="Lightweight" allowdragging="True" allowresize="True"
        width="600px" popuphorizontalalign="WindowCenter" popupverticalalign="WindowCenter"
        headertext="Cấu hình giá trên hàng hóa" modal="True">
                <ContentCollection>
            <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                <%--<uc2:uEdit_priceForProduct ID="uEdit_priceForProduct1" runat="server" />--%>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:aspxpopupcontrol>
    <uc1:uPricePolicyEditting ID="uPricePolicyEditting1" runat="server" />
</asp:Content>
