<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountActorComboBox.ascx.cs" Inherits="WebModule.Accounting.AllocationConfigure.Controls.AccountActorComboBox" %>
<dx:ASPxComboBox ID="combo" runat="server" CallbackPageSize="10" 
    EnableCallbackMode="True" IncrementalFilteringMode="Contains" 
    onitemrequestedbyvalue="combo_ItemRequestedByValue" 
    onitemsrequestedbyfiltercondition="combo_ItemsRequestedByFilterCondition" 
    Width="100%" oninit="combo_Init">
    <ItemStyle Wrap="True" />
</dx:ASPxComboBox>