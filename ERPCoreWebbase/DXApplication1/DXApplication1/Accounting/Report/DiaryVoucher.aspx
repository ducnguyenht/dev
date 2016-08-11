<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DiaryVoucher.aspx.cs" Inherits="WebModule.Accounting.Report.GeneralJournal" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Src="UserControls/S04a1-DN.ascx" TagName="S04a1" TagPrefix="uc1" %>
<%@ Register Src="UserControls/S04a2-DN.ascx" TagName="S04a2" TagPrefix="uc2" %>
<%@ Register Src="UserControls/S04a3-DN.ascx" TagName="S04a3" TagPrefix="uc3" %>
<%@ Register Src="UserControls/S04a4-DN.ascx" TagName="S04a4" TagPrefix="uc8" %>
<%@ Register Src="UserControls/S04a5-DN.ascx" TagName="S04a5" TagPrefix="uc6" %>
<%@ Register Src="UserControls/S04a6-DN.ascx" TagName="S04a6" TagPrefix="uc7" %>
<%@ Register Src="UserControls/S04a8-DN.ascx" TagName="S04a8" TagPrefix="uc10" %>
<%@ Register Src="UserControls/S04a7-DN.ascx" TagName="S04a7" TagPrefix="uc11" %>
<%@ Register Src="UserControls/S04a9-DN.ascx" TagName="S04a9" TagPrefix="uc12" %>
<%@ Register Src="UserControls/S04a10-DN.ascx" TagName="S04a10" TagPrefix="uc13" %>
<%@ Register Src="UserControls/S04b1-DN.ascx" TagName="S04b1" TagPrefix="uc4" %>
<%@ Register Src="UserControls/S04b2-DN.ascx" TagName="S04b2" TagPrefix="uc5" %>
<%@ Register Src="UserControls/S04b11-DN.ascx" TagName="S04b11" TagPrefix="uc9" %>
<%@ Register Src="UserControls/S04b3-DN.ascx" TagName="S04b3" TagPrefix="uc14" %>
<%@ Register Src="UserControls/S04b9-DN.ascx" TagName="S04b9" TagPrefix="uc15" %>
<%@ Register Src="UserControls/GeneralLedger.ascx" TagName="GeneralLedger" TagPrefix="uc16" %>
<%@ Register Src="UserControls/S06-DN.ascx" TagName="S06" TagPrefix="uc17" %>
<%@ Register Src="UserControls/B01-DN.ascx" TagName="B01" TagPrefix="uc18" %>
<%@ Register Src="UserControls/S04b4-DN.ascx" TagName="S04b4" TagPrefix="uc19" %>
<%@ Register Src="UserControls/S04b5-DN.ascx" TagName="S04b5" TagPrefix="uc20" %>
<%@ Register Src="UserControls/S04b6-DN.ascx" TagName="S04b6" TagPrefix="uc21" %>
<%@ Register Src="UserControls/S07-DN.ascx" TagName="S07" TagPrefix="uc22" %>
<%@ Register Src="UserControls/S07a-DN.ascx" TagName="S07a" TagPrefix="uc23" %>
<%@ Register Src="UserControls/S04b8-DN.ascx" TagName="S04b8" TagPrefix="uc24" %>
<%@ Register Src="UserControls/S08-DN.ascx" TagName="S08" TagPrefix="uc25" %>
<%@ Register Src="UserControls/S04b10-DN.ascx" TagName="S04b10" TagPrefix="uc26" %>
<%@ Register Src="UserControls/S11-DN.ascx" TagName="S11" TagPrefix="uc27" %>
<%@ Register Src="UserControls/B02-DN.ascx" TagName="B02" TagPrefix="uc28" %>
<%@ Register Src="UserControls/S10-DN.ascx" TagName="S10" TagPrefix="uc27" %>
<%@ Register Src="UserControls/S12-DN.ascx" TagName="S12" TagPrefix="uc29" %>
<%@ Register Src="UserControls/B09-DN.ascx" TagName="B09" TagPrefix="uc30" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        function gvData_CustomButtonClick(s, e) {
            if (e.buttonID == 'View') {
                gvData.GetRowValues(gvData.GetFocusedRowIndex(), 'reportid', OnGetRowValues);
            }
        }

        function OnGetRowValues(values) {
            if (cboAccountPeriod.GetText() == '') {
                alert("Chưa chọn kỳ kế toán !");
                return;
            }

            if (cboAccount.GetText() == '' && (values == 'S05-DN' || values == 'S10-DN')) {
                alert("Chưa chọn tài khoản báo cáo !");
                return;
            }

            if (values == 'S04a4-DN' && (cboAccount.GetText() == ''
                    && cboAccount.GetText() != '311'
                    && cboAccount.GetText() != '315'
                    && cboAccount.GetText() != '341'
                    && cboAccount.GetText() != '342'
                    && cboAccount.GetText() != '343')) {
                alert("Chưa chọn 1 trong các tài khoản sau: 311 | 315 |341 | 342 | 343");
                return;
            }

            if (values == 'S04a9-DN' &&
                    (cboAccount.GetText() == ''
                    && cboAccount.GetText() != '211'
                    && cboAccount.GetText() != '212'
                    && cboAccount.GetText() != '213'
                    && cboAccount.GetText() != '217')) {
                alert("Chưa chọn 1 trong các tài khoản sau : 211 | 212 | 213 | 217");
                return;
            }

            if (values == 'S04a10-DN' &&
                    (cboAccount.GetText() == ''
                    && cboAccount.GetText() != '121' && cboAccount.GetText() != '128'
                    && cboAccount.GetText() != '129' && cboAccount.GetText() != '136'
                    && cboAccount.GetText() != '138' && cboAccount.GetText() != '139'
                    && cboAccount.GetText() != '141' && cboAccount.GetText() != '144'
                    && cboAccount.GetText() != '161' && cboAccount.GetText() != '221'
                    && cboAccount.GetText() != '222' && cboAccount.GetText() != '223'
                    && cboAccount.GetText() != '228' && cboAccount.GetText() != '229'
                    && cboAccount.GetText() != '243' && cboAccount.GetText() != '244'
                    && cboAccount.GetText() != '333' && cboAccount.GetText() != '336'
                    && cboAccount.GetText() != '338' && cboAccount.GetText() != '344'
                    && cboAccount.GetText() != '347' && cboAccount.GetText() != '411'
                    && cboAccount.GetText() != '412' && cboAccount.GetText() != '413'
                    && cboAccount.GetText() != '414' && cboAccount.GetText() != '415'
                    && cboAccount.GetText() != '418' && cboAccount.GetText() != '419'
                    && cboAccount.GetText() != '421' && cboAccount.GetText() != '431'
                    && cboAccount.GetText() != '441' && cboAccount.GetText() != '461'
                    && cboAccount.GetText() != '466' && cboAccount.GetText() != '466'
                )) {
                alert("Chưa chọn 1 trong các tài khoản sau : 121 | 128 | 129 | 136...");
                return;
            }

            if (values == 'S04b6-DN' &&
                    (cboAccount.GetText() == ''
                    && cboAccount.GetText() != '142'
                    && cboAccount.GetText() != '242'
                    && cboAccount.GetText() != '335')) {
                alert("Chưa chọn 1 trong các tài khoản sau : 142 | 242 | 335");
                return;
            }

            if (values == 'S04b8-DN' &&
                    (cboAccount.GetText() == ''
                    && cboAccount.GetText() != '155'
                    && cboAccount.GetText() != '156'
                    && cboAccount.GetText() != '158')) {
                alert("Chưa chọn 1 trong các tài khoản sau : 155 | 156 | 158");
                return;
            }

            if (cboAccount.GetText() == '' && values == 'S10-DN') { alert("Chưa chọn số tài khoản!"); return; }
            if (cboItem.GetText() == '' && values == 'S10-DN') { alert("Chưa chọn hàng hóa!"); return; }
            if (cboInventory.GetText() == '' && values == 'S10-DN') { alert("Chưa chọn kho!"); return; }
            if (cbo_UnitDim.GetText() == '' && values == 'S10-DN') { alert("Chưa chọn đơn vị tính!"); return; }

            if (values == 'S11-DN' &&
                    (cboAccount.GetText() == '')) {
                alert("Chưa chọn tài khoản báo cáo.");
                return;
            }

            if (cboItem.GetText() == '' && values == 'S12-DN') {
                alert("Chưa chọn hàng hóa báo cáo !");
                return;
            }

            if (values == 'S07a-DN' && cboAccount.GetText().indexOf('111') == -1) {
                alert("Chưa chọn tài khoản tiền mặt !");
                return;
            }

            if (values == 'S08-DN' && cboAccount.GetText().indexOf('112') == -1) {
                alert("Chưa chọn tài khoản tiền gửi ngân hàng !");
                return;
            }


            hReportViewer.Set("report", values);

            var myDate = new Date(txtFromDate.GetValue());
            var myDateTo = new Date(txtToDate.GetValue());

            var myAccount = cboAccount.GetText();

            //
            hS04a1dn.Clear();
            hS04a2dn.Clear();
            hS04a3dn.Clear();
            hS04a4dn.Clear();
            hS04a6dn.Clear();
            hS04a7dn.Clear();
            hS04a8dn.Clear();
            hS04a9dn.Clear();
            hS04a10dn.Clear();

            hS04b1dn.Clear();
            hS04b2dn.Clear();
            hS04b6dn.Clear();
            hS04b8dn.Clear();
            hS04b10dn.Clear();
            hs04b11dn.Clear();

            hS11dn.Clear();
            hS12dn.Clear();
            GLedger.Clear();
            hS06dn.Clear();
            hS07dn.Clear();
            hS07adn.Clear();
            hS08dn.Clear();
            hB01dn.Clear();
            hB02dn.Clear();
            hB09dn.Clear();
            hs10dn.Clear();


            if (values == 'S04a1-DN') {
                hS04a1dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a1dnYear.Set("year_id", myDate.getFullYear());
                hS04a1dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04a1dnAsset.Set("asset_id", "VND");
                hS04a1dn.Set("show", true);
            }

            //
            if (values == 'S04a2-DN') {
                hS04a2dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a2dnYear.Set("year_id", myDate.getFullYear());
                hS04a2dnOwner.Set("owner_id", "");
                hS04a2dnCurrency.Set("currency_id", "VND");
                hS04a2dn.Set("show", true);
            }

            if (values == 'S04a3-DN') {
                hS04a3dn.Set("show", true);
                hS04a3dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a3dnYear.Set("year_id", myDate.getFullYear());
                hS04a3dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04a3dnAsset.Set("asset_id", "VND");
            }

            if (values == 'S04a4-DN') {
                hS04a4dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a4dnYear.Set("year_id", myDate.getFullYear());
                hS04a4dnAcc.Set("account_id", myAccount);
                hS04a4dnOwnerOrg.Set("owner_id", "Null");
                hS04a4dn.Set("show", true);
            }

            if (values == 'S04a5-DN') {
                hS04a5month.Set("month_id", myDate.getMonth() + 1);
                hS04a5year.Set("year_id", myDate.getFullYear());
                hS04a5dn.Set("show", true);
            }

            if (values == 'S04a6-DN') {
                hs04a6dnMonth.Set("month_Name", myDate.getMonth() + 1);
                hs04a6dnYear.Set("year_Name", myDate.getFullYear());
                hS04a6dn.Set("show", true);
            }

            if (values == 'S04a7-DN') {
                hS04a7dnFromDate.Set("fromDate", myDate);
                hS04a7dnToDate.Set("toDate", myDateTo);
                hS04a7dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04a7dnAsset.Set("asset_id", "VND");
                hS04a7dn.Set("show", true);
            }


            if (values == 'S04a8-DN') {
                hS04a8dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a8dnYear.Set("year_id", myDate.getFullYear());
                hS04a8dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04a8dnAsset.Set("asset_id", "VND");
                hS04a8dn.Set("show", true);
            }

            if (values == 'S04a9-DN') {
                hS04a9dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a9dnYear.Set("year_id", myDate.getFullYear());
                hS04a9dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04a9dnAsset.Set("asset_id", "VND");
                hS04a9dnAccount.Set("account_id", myAccount);
                hS04a9dn.Set("show", true);
            }

            if (values == 'S04a10-DN') {
                hS04a10dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04a10dnYear.Set("year_id", myDate.getFullYear());
                hS04a10dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04a10dnAsset.Set("asset_id", "VND");
                hS04a10dnAccount.Set("account_id", myAccount);
                hS04a10dn.Set("show", true);
            }

            if (values == 'S04b1-DN') {
                hS04b1dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04b1dnYear.Set("year_id", myDate.getFullYear());
                hS04b1dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b1dnAsset.Set("asset_id", "VND");
                hS04b1dn.Set("show", true);
            }

            if (values == 'S04b2-DN') {
                hS04b2dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS04b2dnYear.Set("year_id", myDate.getFullYear());
                hS04b2dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b2dnAsset.Set("asset_id", "VND");
                hS04b2dnAccount.Set("account_id", myAccount);
                hS04b2dn.Set("show", true);
            }

            if (values == 'S04b3-DN') {
                hS04b3DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b3DN_year.Set("year_id", myDate.getFullYear());
                hS04b3DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b3DN_asset.Set("asset_id", "VND");
                hS04b3dn.Set("show", true);
            }

            if (values == 'S04b4-DN') {
                hS04b4DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b4DN_year.Set("year_id", myDate.getFullYear());
                hS04b4DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b4DN_asset.Set("asset_id", "VND");
                hS04b4dn.Set("show", true);
            }

            if (values == 'S04b5-DN') {
                hS04b5DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b5DN_year.Set("year_id", myDate.getFullYear());
                hS04b5DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b5DN_asset.Set("asset_id", "VND");
                hS04b5dn.Set("show", true);
            }

            if (values == 'S04b6-DN') {
                hS04b6DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b6DN_year.Set("year_id", myDate.getFullYear());
                hS04b6DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b6DN_asset.Set("asset_id", "VND");
                hS04b6dnAccount.Set("account_id", myAccount);
                hS04b6dn.Set("show", true);
            }

            if (values == 'S04b8-DN') {
                hS04b8DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b8DN_year.Set("year_id", myDate.getFullYear());
                hS04b8DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b8DN_asset.Set("asset_id", "VND");
                hS04b8dnAccount.Set("account_id", myAccount);
                hS04b8dn.Set("show", true);
            }

            if (values == 'S04b9-DN') {
                hS04b9DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b9DN_year.Set("year_id", myDate.getFullYear());
                hS04b9DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b9DN_asset.Set("asset_id", "VND");
                hS04b9dn.Set("show", true);
            }

            if (values == 'S04b10-DN') {
                hS04b10DN_month.Set("month_id", myDate.getMonth() + 1);
                hS04b10DN_year.Set("year_id", myDate.getFullYear());
                hS04b10DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS04b10DN_asset.Set("asset_id", "VND");
                hS04b10dn.Set("show", true);
            }

            if (values == 'S04b11-DN') {
                hs04b11dnMonth.Set("month_Name", myDate.getMonth() + 1);
                hs04b11dnYear.Set("year_Name", myDate.getFullYear());
                hs04b11dn.Set("show", true);
            }

            if (values == 'S05-DN') {
                GeneralLedgerMonth.Set("month_id", myDate.getMonth() + 1);
                GeneralLedgerYear.Set("year_id", myDate.getFullYear());
                GeneralLedgerAcc.Set("account_id", myAccount);
                GLedger.Set("show", true);
            }

            if (values == 'S06-DN') {
                hS06dnMonth.Set("month_id", myDate.getMonth() + 1);
                hS06dnYear.Set("year_id", myDate.getFullYear());
                hS06dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS06dnAsset.Set("asset_id", "VND");
                hS06dn.Set("show", true);
            }

            if (values == 'S07-DN') {
                hS07dnFromDate.Set("fromDate", myDate);
                hS07dnToDate.Set("toDate", myDateTo);
                hS07dnCurrencyName.Set("currency_id", 'VNĐ');
                hS07dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS07dnAsset.Set("asset_id", "VND");
                hS07dn.Set("show", true);
            }

            if (values == 'S07a-DN') {
                hS07adnFromDate.Set("fromDate", myDate);
                hS07adnToDate.Set("toDate", myDateTo);
                hS07adnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS07adnAsset.Set("asset_id", "VND");
                hS07adnAccount.Set("account_id", myAccount);
                hS07adn.Set("show", true);
            }

            if (values == 'S08-DN') {
                hS08dnFromDate.Set("fromDate", myDate);
                hS08dnToDate.Set("toDate", myDateTo);
                hS08dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hS08dnAsset.Set("asset_id", "VND");
                hS08dnAccount.Set("account_id", myAccount);
                hS08dn.Set("show", true);
            }

            if (values == 'S10-DN') {
                hs10dnMonth.Set("month_id", myDate.getMonth() + 1);
                hs10dnYear.Set("year_id", myDate.getFullYear());
                hs10dnAcc.Set("account_id", myAccount);
                hs10dnUnitDim.Set("unit_id", cbo_UnitDim.GetText());
                hs10dnInventory.Set("Inventory_id", cboInventory.GetText());
                hs10dnItem.Set("Item_id", cboItem.GetText());
                hs10dn.Set("show", true);
            }

            if (values == 'S11-DN') {
                hS11DN_month.Set("month_id", myDate.getMonth() + 1);
                hS11DN_year.Set("year_id", myDate.getFullYear());
                hS11DN_owner.Set("owner_id", "Sâm Ngọc Linh");
                hS11DN_asset.Set("asset_id", "VND");
                hS11dnAccount.Set("account_id", myAccount);
                hS11dn.Set("show", true);
            }

            if (values == 'S12-DN') {
                hS12DN_month.Set("month_id", myDate.getMonth() + 1);
                hS12DN_year.Set("year_id", myDate.getFullYear());
                hS12DN_asset.Set("asset_id", "VND");
                hs12dnItem.Set("Item_id", cboItem.GetText());
                hS12dn.Set("show", true);
            }

            if (values == 'B01-DN') {
                hB01dnMonth.Set("month_id", myDate.getMonth() + 1);
                hB01dnYear.Set("year_id", myDate.getFullYear());
                hB01dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hB01dnAsset.Set("asset_id", "VND");
                hB01dn.Set("show", true);
            }

            if (values == 'B02-DN') {
                hB02dnFromDate.Set("fromDate", myDate);
                hB02dnToDate.Set("toDate", myDateTo);
                hB02dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hB02dnAsset.Set("asset_id", "VND");
                hB02dnTransfered.Set("transfered", chkTransfered.GetChecked());
                hB02dn.Set("show", true);
            }

            if (values == 'B03-DN') {
                hB09dnFromDate.Set("fromDate", myDate);
                hB09dnToDate.Set("toDate", myDateTo);
                hB09dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hB09dnAsset.Set("asset_id", "VND");
                hB09dn.Set("show", true);
            }

            if (values == 'B09-DN') {
                hB09dnFromDate.Set("fromDate", myDate);
                hB09dnToDate.Set("toDate", myDateTo);
                hB09dnOwner.Set("owner_id", "Sâm Ngọc Linh");
                hB09dnAsset.Set("asset_id", "VND");
                hB09dn.Set("show", true);
            }

            cpReportViewer.PerformCallback();
        }

        function cboAccountingPeriod_ValueChanged(s, e) {
            cpHeader.PerformCallback("cboAccountingPeriodChanged");
        }

        function cpHeader_EndCallback(s, e) {
        }

        function link_click(s, e) {
            popup2.Show();
        }

        function link_addproduct(s, e) {
            if (e.buttonID == 'add_product') {
                //formReportViewer.Show();
            }
        }

        function checkboxchietkhau_invi(s, e) {

            roundpanelchietkhau.SetVisible(s.GetChecked());

        }
        function checkboxtiengiam_invi(s, e) {
            roundpaneltiengiam.SetVisible(s.GetChecked());
        }
        function checkboxquatang_invi(s, e) {
            roundpanelquatang.SetVisible(s.GetChecked());
        }

        function btnback_click(s, e) {
            pc.SetActiveTabIndex(pc.GetActiveTabIndex() - 1);
        }

        function btnnext_click(s, e) {
            var nextTab = pc.GetTab(pc.GetActiveTabIndex() + 1);
            nextTab.SetEnabled(true);
            pc.SetActiveTab(nextTab);
        }
        function OnCallbackComplete(s, e) {
            popup_orderdetail.Show();
        }

        function OnGetName(values) {
            cppop.PerformCallback(values);

        }
        function popup_orderdetail_EndCallback(s, e) {
            //alert(33);
        }

        function formReportViewer_EndCallback(s, e) {
            if (s.cpShowReport) {
                switch (s.cpShowReport) {
                    case "S04a1-DN":
                        PopupControlS04a1Dn.Show();
                        break;
                    case "S04a2-DN":
                        PopupControlS04a2Dn.Show();
                        break;
                    case "S04a3-DN":
                        PopupControlS04a3dn.Show();
                        break;
                    case "S04a4-DN":
                        PopupControlS04a4dn.Show();
                        break;
                    case "S04a5-DN":
                        PopupControlS04a5Dn.Show();
                        break;
                    case "S04a6-DN":
                        pop_s04a6_dn.Show();
                        break;
                    case "S04a7-DN":
                        PopupControlS04a7Dn.Show();
                        break;
                    case "S04a8-DN":
                        PopupControlS04a8Dn.Show();
                        break;
                    case "S04a9-DN":
                        PopupControlS04a9Dn.Show();
                        break;
                    case "S04a10-DN":
                        PopupControlS04a10Dn.Show();
                        break;
                    case "S04b1-DN":
                        PopupControlS04b1dn.Show();
                        break;
                    case "S04b2-DN":
                        PopupControlS04b2dn.Show();
                        break;
                    case "S04b3-DN":
                        PopupControlS04b3Dn.Show();
                        break;
                    case "S04b4-DN":
                        PopupControlS04b4Dn.Show();
                        break;
                    case "S04b5-DN":
                        PopupControlS04b5Dn.Show();
                        break;
                    case "S04b6-DN":
                        PopupControlS04b6Dn.Show();
                        break;
                    case "S04b8-DN":
                        PopupControlS04b8Dn.Show();
                        break;
                    case "S04b9-DN":
                        PopupControlS04b9Dn.Show();
                        break;
                    case "S04b10-DN":
                        PopupControlS04b10Dn.Show();
                        break;
                    case "S04b11-DN":
                        popup_s04b11_dn.Show();
                        break;
                    case "S05-DN":
                        pop_GeneralLedger.Show();
                        break;
                    case "S06-DN":
                        PopupControlS06Dn.Show();
                        break;
                    case "S07-DN":
                        PopupControlS07Dn.Show();
                        break;
                    case "S07a-DN":
                        PopupControlS07aDn.Show();
                        break;
                    case "S08-DN":
                        PopupControlS08Dn.Show();
                        break;
                    case "S10-DN":
                        popup_s10_dn.Show();
                        break;
                    case "S11-DN":
                        PopupControlS11Dn.Show();
                        break;
                    case "S12-DN":
                        PopupControlS12Dn.Show();
                        break;

                    case "B01-DN":
                        PopupControlB01Dn.Show();
                        break;
                    case "B02-DN":
                        PopupControlB02Dn.Show();
                        break;
                    case "B03-DN":
                        PopupControlB09Dn.Show();
                        break;
                }
                delete (s.cpShowReport);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách nhật ký - chứng từ" Font-Bold="True"
        Font-Size="Medium" Height="45px">
    </dx:ASPxLabel>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" ClientInstanceName="cpHeader"
        Width="100%" OnCallback="cpHeader_Callback">
        <ClientSideEvents EndCallback="cpHeader_EndCallback" />
        <ClientSideEvents EndCallback="cpHeader_EndCallback"></ClientSideEvents>
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" Theme="Default"
                    ColCount="3" Width="60%">
                    <Items>
                        <dx:LayoutItem Caption="Chọn chu kỳ kế toán">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxComboBox ID="cboAccountPeriod" runat="server" ClientInstanceName="cboAccountPeriod"
                                        OnItemRequestedByValue="cboAccountPeriod_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cboAccountPeriod_ItemsRequestedByFilterCondition"
                                        EnableCallbackMode="True" IncrementalFilteringMode="Contains" OnSelectedIndexChanged="cboAccountPeriod_SelectedIndexChanged"
                                        TextField="Code" TextFormatString="{0}" ValueField="AccountingPeriodId" ValueType="System.Guid"
                                        OnInit="cboAccountPeriod_Init">
                                        <ClientSideEvents ValueChanged="cboAccountingPeriod_ValueChanged"></ClientSideEvents>
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã chu kỳ KT " FieldName="Code" Name="150" />
                                            <dx:ListBoxColumn Caption="Tên chu kỳ KT" FieldName="Description" Name="300" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Từ ngày">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxDateEdit ID="txtFromDate" runat="server" ClientInstanceName="txtFromDate">
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đến ngày">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxDateEdit ID="txtToDate" runat="server" ClientInstanceName="txtToDate">
                                    </dx:ASPxDateEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Số tài khoản">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxComboBox ID="cboAccount" runat="server" ClientInstanceName="cboAccount" EnableCallbackMode="True"
                                        IncrementalFilteringMode="Contains" OnItemRequestedByValue="cboAccount_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="cboAccount_ItemsRequestedByFilterCondition"
                                        TextField="Code" TextFormatString="{0}" ValueField="AccountId" ValueType="System.Guid">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã tài khoản" FieldName="Code" Width="150px" />
                                            <dx:ListBoxColumn Caption="Tài khoàn" FieldName="Name" Width="300px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hàng hóa">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxComboBox ID="cboItem" runat="server" ClientInstanceName="cboItem" OnItemRequestedByValue="cboItem_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="cboItem_ItemsRequestedByFilterCondition" EnableCallbackMode="True"
                                        IncrementalFilteringMode="Contains" TextField="Name" TextFormatString="{1}" ValueField="ItemId"
                                        ValueType="System.Guid">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã hàng hóa" FieldName="Code" Width="150px" />
                                            <dx:ListBoxColumn Caption="Tên hàng hóa" FieldName="Name" Width="300px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Kho">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxComboBox ID="cboInventory" ClientInstanceName="cboInventory" runat="server"
                                        OnItemRequestedByValue="cboInventory_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cboInventory_ItemsRequestedByFilterCondition"
                                        EnableCallbackMode="True" IncrementalFilteringMode="Contains" TextField="Name"
                                        TextFormatString="{1}" ValueField="InventoryId" ValueType="System.Guid">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã kho" FieldName="Code" Width="150px" />
                                            <dx:ListBoxColumn Caption="Tên kho" FieldName="Name" Width="300px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxCheckBox ID="chkTransfered" runat="server" ClientInstanceName="chkTransfered"
                                        Text="Đã kết chuyển (B02DN)">
                                    </dx:ASPxCheckBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đơn vị tính">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxComboBox ID="cbo_UnitDim" runat="server" ClientInstanceName="cbo_UnitDim"
                                        EnableCallbackMode="True" IncrementalFilteringMode="Contains" OnItemRequestedByValue="cbo_UnitDim_ItemRequestedByValue"
                                        OnItemsRequestedByFilterCondition="cbo_UnitDim_ItemsRequestedByFilterCondition"
                                        TextField="Code" TextFormatString="{0}" ValueField="UnitDimId" ValueType="System.Int32">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Loại" FieldName="Code" Width="150px" />
                                            <dx:ListBoxColumn Caption="Tên" FieldName="Name" Width="300px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
                <dx:ASPxGridView ID="gvData" runat="server" AutoGenerateColumns="False" Width="100%"
                    KeyFieldName="reportid">
                    <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                    </Settings>
                    <ClientSideEvents CustomButtonClick="gvData_CustomButtonClick
"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="3">
                            <EditButton Text="Xem">
                            </EditButton>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="View" Text="Xem">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="Ký hiệu" VisibleIndex="2" FieldName="reportid"
                            Width="15%">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tên sổ" VisibleIndex="1" FieldName="name">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" />
                    <SettingsPager PageSize="100" ShowEmptyDataRows="True" Mode="ShowAllRecords">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                    <Styles>
                        <Header HorizontalAlign="Center">
                        </Header>
                    </Styles>
                </dx:ASPxGridView>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxHiddenField ID="hReportViewer" runat="server" ClientInstanceName="hReportViewer">
    </dx:ASPxHiddenField>
    <dx:ASPxGridViewExporter ID="gvDataExporter" runat="server" Landscape="True" MaxColumnWidth="2000">
        <Styles>
            <Header BackColor="White" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridViewExporter>
    <dx:ASPxCallbackPanel ID="cpReportViewer" runat="server" ClientInstanceName="cpReportViewer"
        OnCallback="cpReportViewer_Callback" Width="200px">
        <ClientSideEvents EndCallback="formReportViewer_EndCallback" />
        <ClientSideEvents EndCallback="formReportViewer_EndCallback"></ClientSideEvents>
        <PanelCollection>
            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                <uc1:S04a1 ID="S04a12" runat="server" />
                <uc3:S04a3 ID="S04a31" runat="server" />
                <uc2:S04a2 ID="S04a21" runat="server" />
                <uc8:S04a4 ID="S04a41" runat="server" />
                <uc6:S04a5 ID="S04a51" runat="server" />
                <uc7:S04a6 ID="S04a61" runat="server" />
                <uc11:S04a7 ID="S04a71" runat="server" />
                <uc10:S04a8 ID="S04a81" runat="server" />
                <uc12:S04a9 ID="S04a91" runat="server" />
                <uc13:S04a10 ID="S04a101" runat="server" />
                <uc4:S04b1 ID="S04b11" runat="server" />
                <uc5:S04b2 ID="S04b21" runat="server" />
                <uc14:S04b3 ID="S04b31" runat="server" />
                <uc19:S04b4 ID="S04b41" runat="server" />
                <uc20:S04b5 ID="S04b51" runat="server" />
                <uc21:S04b6 ID="S04b61" runat="server" />
                <uc24:S04b8 ID="S04b81" runat="server" />
                <uc15:S04b9 ID="S04b92" runat="server" />
                <uc26:S04b10 ID="S04b101" runat="server" />
                <uc27:S11 ID="S111" runat="server" />
                <uc29:S12 ID="S121" runat="server" />
                <uc16:GeneralLedger ID="GeneralLedger1" runat="server" />
                <uc17:S06 ID="S061" runat="server" />
                <uc18:B01 ID="B011" runat="server" />
                <uc22:S07 ID="S071" runat="server" />
                <uc23:S07a ID="S07a1" runat="server" />
                <uc25:S08 ID="S081" runat="server" />
                <uc27:S10 ID="S101" runat="server" />
                <uc28:B02 ID="B021" runat="server" />
                <uc30:B09 ID="B091" runat="server" />
                <uc9:S04b11 ID="S04b111" runat="server" />
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
