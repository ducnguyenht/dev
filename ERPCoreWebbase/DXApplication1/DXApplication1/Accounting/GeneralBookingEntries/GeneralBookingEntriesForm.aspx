<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GeneralBookingEntriesForm.aspx.cs"
    Inherits="WebModule.Accounting.GeneralBookingEntries.GeneralBookingEntriesForm" %>

<%@ Register Src="GridViewGeneralBookingEntries.ascx" TagName="GridViewGeneralBookingEntries"
    TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
    // <![CDATA[
        var textSeparator = "; ";
        function OnListBoxSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState();
            UpdateText();
        }
        function UpdateSelectAllItemState() {
            IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
        }
        function IsAllSelected() {
            var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
            return checkListBox.GetSelectedItems().length == selectedDataItemCount;
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            checkComboBox.SetText(GetSelectedItemsText(selectedItems));

            cpnGeneralBookingEntries.PerformCallback();
        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts
        }
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                    texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
    // ]]>        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Large" Text="Hạch toán tổng hợp">
        </dx:ASPxLabel>
    </div>
    <div>
        <table style="border: 0; padding:0px 4px 2px 4px" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div>
                        <dx:ASPxDropDownEdit NullText="Chọn thể loại" ClientInstanceName="checkComboBox"
                            ID="comboTransactionType" Width="210px" runat="server" AnimationType="None">
                            <DropDownWindowStyle BackColor="#EDEDED" />
                            <DropDownWindowTemplate>
                                <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                    runat="server" Height="250px" Rows="10">
                                    <Border BorderStyle="None" />
                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                    <Items>
                                        <dx:ListEditItem Text="(Chọn tất cả)" />
                                        <dx:ListEditItem Text="Bút toán tổng hợp" Value="1" />
                                        <dx:ListEditItem Text="Bút toán phiếu mua hàng" Value="2" />
                                        <dx:ListEditItem Text="Bút toán phiếu bán hàng" Value="3" />
                                        <dx:ListEditItem Text="Bút toán phiếu thu" Value="4" />
                                        <dx:ListEditItem Text="Bút toán phiếu chi" Value="5" />
                                        <dx:ListEditItem Text="Bút toán phiếu nhập kho" Value="6" />
                                        <dx:ListEditItem Text="Bút toán phiếu xuất kho" Value="7" />
                                        <dx:ListEditItem Text="Bút toán phiếu chuyển kho" Value="8" />
                                    </Items>
                                    <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                </dx:ASPxListBox>
                            </DropDownWindowTemplate>
                            <ClientSideEvents TextChanged="SynchronizeListBoxValues" 
                                DropDown="SynchronizeListBoxValues" />
                        </dx:ASPxDropDownEdit>
                    </div>
                </td>
                <td>
                    <div style="margin: 4px">
                        <dx:ASPxButton ID="btnFilter" runat="server" AutoPostBack="False" CausesValidation="False"
                            Text="Lọc" Visible="False">
                            <ClientSideEvents Click="function(s ,e) {  
                                if(!cpnGeneralBookingEntries.InCallback())
                                {
                                    cpnGeneralBookingEntries.PerformCallback();
                                }
                            }" />
                            <Image Url="~/images/icon/Filter/Filter_16x16.png">
                            </Image>
                        </dx:ASPxButton>
                    </div>
                </td>
            </tr>
        </table>
        <dx:ASPxCallbackPanel ClientInstanceName="cpnGeneralBookingEntries" ID="cpnGeneralBookingEntries"
            runat="server" Width="100%" OnCallback="cpnGeneralBookingEntries_Callback">
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                    <uc1:GridViewGeneralBookingEntries ID="gridGeneralBookingEntries" runat="server" />
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
</asp:Content>
