<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CurrencyGridLookup.ascx.cs" Inherits="WebModule.Accounting.CurrencyGridLookup.CurrencyGridLookup" %>
<%@ Register assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>

<dx:ASPxGridLookup ID="gridlookupCurrency" runat="server" 
    AutoGenerateColumns="False" DataSourceID="dsCurrecy" 
    KeyFieldName="CurrencyId" IncrementalFilteringMode="StartsWith" 
    TextFormatString="{0}">
<GridViewProperties>
<SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>
</GridViewProperties>
    <Columns>
        <dx:GridViewDataTextColumn Caption="Mã tiền tệ" FieldName="Code" SortIndex="0" 
            SortOrder="Ascending" VisibleIndex="2">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Tên tiền tệ" FieldName="Name" 
            VisibleIndex="8">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Loại tiền tệ" 
            FieldName="CurrencyTypeId.Name" GroupIndex="0" SortIndex="0" 
            SortOrder="Ascending" VisibleIndex="10">
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridLookup>
<dx:XpoDataSource ID="dsCurrecy" runat="server" Criteria="[RowStatus] = 1s And [CurrencyTypeId] Is Not Null" 
    ServerMode="True" TypeName="NAS.DAL.Accounting.Currency.Currency">
</dx:XpoDataSource>

