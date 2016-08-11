<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uBuildingMenuConstruction.ascx.cs" ClientIDMode="AutoID" Inherits="WebModule.Sales.PricePolicy.PredicatorSubForm.uBuildingMenuConstruction" %>
<script type="text/javascript">
</script>
<%@ Register src="~/Sales/PricePolicy/PredicatorSubForm/SupplierList/uSettingSuppliersList.ascx" tagname="uSettingSuppliersList" tagprefix="ucSupplier" %>
<%@ Register src="~/Sales/PricePolicy/PredicatorSubForm/ManufacturerList/uSettingManufacturersList.ascx" tagname="uSettingManufacturersList" tagprefix="ucManufacturer" %>
<%@ Register src="~/Sales/PricePolicy/PredicatorSubForm/ItemUnitList/uSettingItemUnitList.ascx" tagname="uSettingItemUnitList" tagprefix="ucSettingItemUnitList" %>
 <dx:ASPxCallbackPanel ID="cpBuildingMenuConstruction" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    Width="100%">
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <ucSupplier:uSettingSuppliersList ID="uSettingSuppliersList1" runat="server" />
            <ucManufacturer:uSettingManufacturersList ID="uSettingManufacturersList1" runat="server" />
            <ucSettingItemUnitList:uSettingItemUnitList ID="uSettingItemUnitList1" runat="server" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>