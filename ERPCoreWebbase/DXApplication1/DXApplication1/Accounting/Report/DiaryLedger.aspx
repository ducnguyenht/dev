<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DiaryLedger.aspx.cs" Inherits="WebModule.Accounting.Report.DiaryLedger" %>

<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

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
            popup_orderdetail.Show();
            //viewer.Refresh();
            popup_orderdetail.PerformCallback();

        }
        function OnGetName(values) {
            cppop.PerformCallback(values);

        }
        function popup_orderdetail_EndCallback(s, e) {
            //alert(33);

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
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách nhật ký sổ cái" Font-Bold="True"
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
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowHeaderFilterButton="True" />
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="cppop"
        OnCallback="ASPxCallbackPanel1_Callback" Width="200px">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="popup_report" runat="server" CloseAction="CloseButton" ClientInstanceName="popup_orderdetail"
                    AllowDragging="True" AllowResize="True" PopupAnimationType="None" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" EnableViewState="False" HeaderText="" Height="617px"
                    Width="1000px" OnWindowCallback="popup_report_WindowCallback" DragElement="Window">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <div style="height: 100%; width: 100%; overflow: hidden">
                                <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" FullscreenMode="True" Width="100%"
                                    Height="100%" Orientation="Vertical" SeparatorVisible="false" ClientInstanceName="PrintLayoutSplitter">
                                    <Panes>
                                        <dx:SplitterPane Size="40" Name="ToolbarPane" MinSize="20">
                                            <ContentCollection>
                                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                                    <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer1 %>">
                                                        <Items>
                                                            <dx:ReportToolbarButton ItemKind="Search" />
                                                            <dx:ReportToolbarSeparator />
                                                            <dx:ReportToolbarButton ItemKind="PrintReport" />
                                                            <dx:ReportToolbarButton ItemKind="PrintPage" />
                                                            <dx:ReportToolbarSeparator />
                                                            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                                                            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                                                            <dx:ReportToolbarLabel ItemKind="PageLabel" />
                                                            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                                                            </dx:ReportToolbarComboBox>
                                                            <dx:ReportToolbarLabel ItemKind="OfLabel" />
                                                            <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                                                            <dx:ReportToolbarButton ItemKind="NextPage" />
                                                            <dx:ReportToolbarButton ItemKind="LastPage" />
                                                            <dx:ReportToolbarSeparator />
                                                            <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                                                            <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                                                            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                                                                <Elements>
                                                                    <dx:ListElement Value="pdf" />
                                                                    <dx:ListElement Value="xls" />
                                                                    <dx:ListElement Value="xlsx" />
                                                                    <dx:ListElement Value="rtf" />
                                                                    <dx:ListElement Value="mht" />
                                                                    <dx:ListElement Value="html" />
                                                                    <dx:ListElement Value="txt" />
                                                                    <dx:ListElement Value="csv" />
                                                                    <dx:ListElement Value="png" />
                                                                </Elements>
                                                            </dx:ReportToolbarComboBox>
                                                        </Items>
                                                        <Styles>
                                                            <LabelStyle>
                                                                <Margins MarginLeft="3px" MarginRight="3px" />
                                                            </LabelStyle>
                                                        </Styles>
                                                    </dx:ReportToolbar>
                                                </dx:SplitterContentControl>
                                            </ContentCollection>
                                        </dx:SplitterPane>
                                        <dx:SplitterPane ScrollBars="Auto">
                                            <ContentCollection>
                                                <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                                    <dx:ReportViewer ID="ReportViewer1" runat="server" ClientInstanceName="viewer" OnCacheReportDocument="ReportViewer1_CacheReportDocument"
                                                        OnRestoreReportDocumentFromCache="ReportViewer1_RestoreReportDocumentFromCache"
                                                        OnUnload="ReportViewer1_Unload" Border-BorderStyle="Solid" Border-BorderWidth="1"
                                                        Border-BorderColor="Black">
                                                    </dx:ReportViewer>
                                                </dx:SplitterContentControl>
                                            </ContentCollection>
                                        </dx:SplitterPane>
                                    </Panes>
                                    <Styles>
                                        <Pane HorizontalAlign="Center">
                                            <Paddings Padding="0" />
                                            <Border BorderWidth="0" />
                                        </Pane>
                                    </Styles>
                                </dx:ASPxSplitter>
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                    <ClientSideEvents EndCallback="popup_orderdetail_EndCallback" />
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="Callback1"
        OnCallback="ASPxCallback1_Callback">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" />
    </dx:ASPxCallback>
    <asp:HiddenField ID="hf" runat="server" />
</asp:Content>