<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="S04a3-DN.aspx.cs" Inherits="WebModule.Accounting.Report.ExportReport.S04a3_DN" %>

<%@ Register src="../UserControls/S04a3-DN.ascx" tagname="S04a3" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style1
        {
            width: 100%;
        }
        .style4
        {
            width: 61px;
        }
        .style3
        {
            width: 68px;
        }
        </style>
        <script language="javascript" type="text/javascript">
            function buttonClick(s, e) {
                hMonth.Set("month_id", xComboBox_month.GetText());
                hYear.Set("year_id", xComboBox_year.GetText());
                hOwner.Set("owner_id", xComboBox_Owner.GetText());
                hAsset.Set("asset_id", xComboBox_Currency.GetText());
                xPopupControl.PerformCallback();
                xPopupControl.Show();
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
    
        <br />
        <br />
    
        <table class="style1">
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    Owner</td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_Owner" runat="server" 
                        ClientInstanceName="xComboBox_Owner" IncrementalFilteringMode="Contains" 
                        ValueType="System.String">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    Month</td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_month" runat="server" 
                        ClientInstanceName="xComboBox_month" IncrementalFilteringMode="Contains" 
                        ValueType="System.Int32">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    Year</td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_year" runat="server" 
                        ClientInstanceName="xComboBox_year" IncrementalFilteringMode="Contains" 
                        ValueType="System.Int32">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    Currency</td>
                <td>
                    <dx:ASPxComboBox ID="xComboBox_Currency" runat="server" 
                        ClientInstanceName="xComboBox_Currency" IncrementalFilteringMode="Contains" 
                        ValueType="System.String">
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" 
                        Text="Export">
                        <ClientSideEvents Click="buttonClick" />
                    </dx:ASPxButton>
                </td>
            </tr>
            </table>
    
        <br />
        <uc1:S04a3 ID="S04a31" runat="server" />
    
    </div>
    
    </div>
    </form>
</body>
</html>
