<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="TaxReport.aspx.cs" Inherits="WebModule.Accounting.Report.TaxReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register src="../UserControl/InvoicelVatList.ascx" tagname="InvoicelVatList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        var reportTitle = "";
        function link_click(s, e) {
            popup2.Show();
        }

        function link_addproduct(s, e) {
            if (e.buttonID == 'add_product')
                popup_editproduct.Show();
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
        
        function OnGetValue(values) {
            document.getElementById("hf").value = values;
            if (values == '01-1/GTGT' || values == '01-2/GTGT') {
                ShowInvoiceVatList(values, "");            
            }
        }

        function OnGetName(values) {        
            reportTitle = values;
        }
        function popup_orderdetail_EndCallback(s, e) {            
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" 
        Theme="Default" ColCount="3" Width="60%">
        <Items>
            <dx:LayoutItem Caption="Chọn chu kỳ kế toán">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Từ ngày">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxDateEdit ID="ASPxFormLayout1_E1" runat="server">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Đến ngày">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxDateEdit ID="ASPxFormLayout1_E2" runat="server">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Số tài khoản">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxComboBox ID="ASPxFormLayout1_E3" runat="server">
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Hệ thống tài khoản">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách báo cáo thuế" Font-Bold="True"
        Font-Size="Medium" Height="45px">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="gvData" runat="server" AutoGenerateColumns="False" OnStartRowEditing="gvData_StartRowEditing"
        Width="70%">
        <ClientSideEvents CustomButtonClick="function(s, e) {
                s.GetRowValues(e.visibleIndex, 'name', OnGetName);                
                s.GetRowValues(e.visibleIndex, 'reportid', OnGetValue);
	           
            }"></ClientSideEvents>
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
            <dx:GridViewDataTextColumn Caption="Ký hiệu" VisibleIndex="2" 
                FieldName="reportid" Width="15%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên sổ" VisibleIndex="1" FieldName="name">
                <CellStyle Wrap="True">
                </CellStyle>
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowHeaderFilterBlankItems="False" ShowHeaderFilterButton="True" />
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridView>
    <asp:HiddenField ID="hf" runat="server" />
    <uc1:InvoicelVatList ID="InvoicelVatList1" runat="server" />
</asp:Content>