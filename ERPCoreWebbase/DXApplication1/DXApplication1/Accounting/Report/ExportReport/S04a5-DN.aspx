<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="S04a5-DN.aspx.cs" Inherits="WebModule.Accounting.Report.ExportReport.S04a5_DN" %>

<%@ Register Src="../UserControls/S04a5-DN.ascx" TagName="S04a5" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/S04a4-DN.ascx" TagName="S04a4" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/S04b3-DN.ascx" TagName="S04b3" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/S04b9-DN.ascx" TagName="S04b9" TagPrefix="uc4" %>
<%@ Register Src="../UserControls/S04b4-DN.ascx" TagName="S04b4" TagPrefix="uc5" %>
<%@ Register src="../UserControls/S04b5-DN.ascx" tagname="S04b5" tagprefix="uc6" %>
<%@ Register src="../UserControls/S04b6-DN.ascx" tagname="S04b6" tagprefix="uc7" %>
<%@ Register src="../UserControls/S04b8-DN.ascx" tagname="S04b8" tagprefix="uc8" %>
<%@ Register src="../UserControls/S04b10-DN.ascx" tagname="S04b10" tagprefix="uc9" %>
<%@ Register src="../UserControls/S12-DN.ascx" tagname="S12" tagprefix="uc10" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 41px;
        }
        .style3
        {
            width: 66px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function buttonClick(s, e) {
            //            hS04a5Asset.Set("asset_id", xComboBox_Currency.GetText());
            //            hS04a5month.Set("month_id", xComboBox_month.GetText());
            //            hS04a5year.Set("year_id", xComboBox_year.GetText());
            //            PopupControlS04a4dn.PerformCallback();
            //            PopupControlS04a4dn.Show();

            PopupControlS12Dn.PerformCallback();
            PopupControlS12Dn.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    Month
                </td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_month" runat="server" ClientInstanceName="xComboBox_month"
                        IncrementalFilteringMode="Contains" ValueType="System.String">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    Year
                </td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_year" runat="server" ClientInstanceName="xComboBox_year"
                        IncrementalFilteringMode="Contains" ValueType="System.String">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    Currency
                </td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_Currency" runat="server" ClientInstanceName="xComboBox_Currency"
                        IncrementalFilteringMode="Contains" ValueType="System.String">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="Export">
                        <ClientSideEvents Click="buttonClick" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <br />
        <uc10:S12 ID="S121" runat="server" />
        <br />
    </div>
    </form>
</body>
</html>
