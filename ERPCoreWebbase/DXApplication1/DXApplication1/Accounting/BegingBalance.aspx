<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="BegingBalance.aspx.cs" Inherits="WebModule.Accounting.BegingBalance" %>
<%@ Register src="UserControl/ucBalanceInit.ascx" tagname="ucBalanceInit" tagprefix="uc1" %>

<%@ Register src="../ERPSystem/CustomField/GUI/Control/NASCustomFieldDataGridView.ascx" tagname="NASCustomFieldDataGridView" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function tree_callback(s, e) {
            treedata.PerformCallback();
        }

        function grdBalanceLine_CustomButtonClick(s, e) {
            formDynamicObject.Show();
            cpAllocation.PerformCallback('allocation|' + s.GetRowKey(e.visibleIndex));          
        }

        function cboBalanceInitAccount_SelectedIndexChanged(s, e) {
            //ASPxClientEdit.ClearEditorsInContainerById('BalanceInitContainer');
            if (!ASPxClientEdit.ValidateEditorsInContainerById('BalanceInitContainer')) {
                e.processOnServer = false;
                return;
            }
            grdBalanceLine.PerformCallback('selectAccountChange|' + e.GetValue);
        }

        function cboBalanceInitCurrency_SelectedIndexChanged(s, e) {
            if (!ASPxClientEdit.ValidateEditorsInContainerById('BalanceInitContainer')) {
                e.processOnServer = false;
                return;
            }
            //ASPxClientEdit.ClearEditorsInContainerById('BalanceInitContainer');
            grdBalanceLine.PerformCallback('selectCurrencyChange|' + e.GetValue);           
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Nhập Dữ Liệu Ban Đầu" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
        <uc1:ucBalanceInit ID="ucBalanceInit1" runat="server" />
    </div>
</asp:Content>
