<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="S04b11-DN.aspx.cs" Inherits="WebModule.Accounting.Report.ExportReport.S04b11_DN" %>

<%@ Register Src="~/Accounting/Report/UserControls/S04b11-DN.ascx" TagName="S04b11"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function buttonClick(s, e) {
            hs04b11dnYear.Set("year_Name", cbo_year.GetText());
            hs04b11dnMonth.Set("month_Name", cbo_month.GetText());
            hs04b11dn.Set("show", true);
            popup_s04b11_dn.PerformCallback();
            popup_s04b11_dn.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxCallbackPanel ID="cp_s04b11_dn" runat="server" Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3">
                        <Items>
                            <dx:LayoutItem Caption="Tháng">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="cbo_month" ClientInstanceName="cbo_month" runat="server" DataSourceID="DBMonth">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Tháng" FieldName="Name" />
                                            </Columns>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="Năm">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxComboBox ID="cbo_year" ClientInstanceName="cbo_year" runat="server" DataSourceID="DBYear">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="Năm" FieldName="Name" />
                                            </Columns>
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem ColSpan="1" Caption=" " HorizontalAlign="Center" VerticalAlign="Middle">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxButton ID="btn_Report" runat="server" Text="Report" EnableTheming="True"
                                            HorizontalAlign="Center" VerticalAlign="Middle">
                                            <ClientSideEvents Click="buttonClick" />
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                    <uc1:S04b11 ID="S04b111" runat="server" />
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <dx:XpoDataSource ID="DBMonth" runat="server" TypeName="NAS.DAL.System.ShareDim.MonthDim">
        </dx:XpoDataSource>
        <dx:XpoDataSource ID="DBYear" runat="server" TypeName="NAS.DAL.System.ShareDim.YearDim">
        </dx:XpoDataSource>
    </div>
    </form>
</body>
</html>
