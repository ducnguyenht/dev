<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOrderConfirm.ascx.cs" Inherits="WebModule.Accounting.UserControl.PaymentOrderConfirm" %>
<%@ Register assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<style type="text/css">
        .pdt
        {
            margin-top:-10px !important;

        }
        .float_r
        {
            float:right;
        }
        #UNC_Agri1_ASPxFormLayout1_ReportParametersPanel1_rootContainer * div,.dxflItem_DevEx div div
        {
            width:150px;
            margin-bottom:4px !important;
            margin-right:4px !important;
            display:inline-table;
        }
       
    </style>
    
<dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="200px" ClientInstanceName ="uncagri">
    <PanelCollection>
<dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
    <dx:ASPxPopupControl ID="formPaymentOrderConfirm" runat="server" Width="600px" 
        ClientInstanceName="popuncagri" HeaderText="Xác nhận ủy nhiệm chi" 
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                    <Items>
                        <dx:LayoutItem Caption="Layout Item" Height="572px" ShowCaption="False" 
                            Width="806px">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                    SupportsDisabledAttribute="True">
                                    
                                    <dx:ReportViewer ID="ReportViewer1" runat="server" 
                                        Report="<%# new WebModule.Accounting.Report.UNC_Agribank() %>" 
                                        ReportName="WebModule.Accounting.Report.UNC_Agribank" CssClass="pdt">
                                    </dx:ReportViewer>
                                    
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                            <BackgroundImage ImageUrl="~/images/agribank.png" Repeat="NoRepeat" />
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Layout Item" ShowCaption="False">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                    SupportsDisabledAttribute="True">
                                    <dx:ReportToolbar ID="ReportToolbar1" runat='server' ShowDefaultButtons='False' 
                                        ReportViewerID="ReportViewer1">
                                        <Items>
                                            <dx:ReportToolbarButton ItemKind='Search' />
                                            <dx:ReportToolbarSeparator />
                                            <dx:ReportToolbarButton ItemKind='PrintReport' />
                                            <dx:ReportToolbarButton ItemKind='PrintPage' />
                                            <dx:ReportToolbarSeparator />
                                            <dx:ReportToolbarButton Enabled='False' ItemKind='FirstPage' />
                                            <dx:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' />
                                            <dx:ReportToolbarLabel ItemKind='PageLabel' />
                                            <dx:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
                                            </dx:ReportToolbarComboBox>
                                            <dx:ReportToolbarLabel ItemKind='OfLabel' />
                                            <dx:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
                                            <dx:ReportToolbarButton ItemKind='NextPage' />
                                            <dx:ReportToolbarButton ItemKind='LastPage' />
                                            <dx:ReportToolbarSeparator />
                                            <dx:ReportToolbarButton ItemKind='SaveToDisk' />
                                            <dx:ReportToolbarButton ItemKind='SaveToWindow' />
                                            <dx:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
                                                <Elements>
                                                    <dx:ListElement Value='pdf' />
                                                    <dx:ListElement Value='xls' />
                                                    <dx:ListElement Value='xlsx' />
                                                    <dx:ListElement Value='rtf' />
                                                    <dx:ListElement Value='mht' />
                                                    <dx:ListElement Value='html' />
                                                    <dx:ListElement Value='txt' />
                                                    <dx:ListElement Value='csv' />
                                                    <dx:ListElement Value='png' />
                                                </Elements>
                                            </dx:ReportToolbarComboBox>
                                        </Items>
                                        <Styles>
                                            <LabelStyle>
                                                <Margins MarginLeft='3px' MarginRight='3px' />
                                            </LabelStyle>
                                        </Styles>
                                    </dx:ReportToolbar>                                   
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
        </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
