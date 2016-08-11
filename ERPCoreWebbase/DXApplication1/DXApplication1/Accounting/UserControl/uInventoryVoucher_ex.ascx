<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uInventoryVoucher_ex.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uInventoryVoucher_ex" %>
<%@ Register Assembly="DevExpress.XtraReports.v13.2.Web, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<script type="text/javascript">
    function printReport() {
        popup_Report.Show();
    }
</script>
<dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" RenderMode="Classic" Width="850px"
    Height="600px" HeaderText="Hạch toán" ClientInstanceName="poppxk" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" ShowMaximizeButton="true">
    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
    <FooterContentTemplate>
        <div>
            <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float-left button-left-margin"
                Text="Trợ Giúp" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Help" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton4" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin"
                Text="In piếu xuất kho" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Print" />
                </Image>
                <ClientSideEvents Click="printReport" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton2" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin hd"
                Text="Tiếp theo" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Forward" />
                </Image>
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="false" runat="server" CssClass="float-right button-right-margin"
                Text="Duyệt" Wrap="False">
                <Image>
                    <SpriteProperties CssClass="Sprite_Approve" />
                </Image>
            </dx:ASPxButton>
        </div>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True" >
            <div>
                <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Width="100%">
                    <Items>
                        <dx:LayoutGroup Caption="Thông tin phiếu xuất kho" ColCount="2" 
                            GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem Caption="Mã chứng từ">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E1" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Ngày xuất">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxDateEdit ID="ASPxFormLayout1_E2" runat="server">
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Người tạo">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E3" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Lý do">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="ASPxFormLayout1_E4" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Tổng tiền phiếu xuất kho">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout1_E5" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Hạch toán" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem Caption="Sơ đồ định khoản">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxComboBox ID="ASPxFormLayout2_E2" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Diễn giải" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="ASPxFormLayout2_E1" runat="server" Width="100%">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxGridView ID="ASPxGridView_ive" runat="server" 
                                                AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="STT" FieldName="No" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0" Width="40px">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mục" FieldName="Item" 
                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                        <CellStyle HorizontalAlign="Left">
                                                        </CellStyle>
                                                        <FooterTemplate>
                                                            <dx:ASPxTextBox ID="tong" runat="server" Text="Xuất kho bán hàng" Width="100%">
                                                            </dx:ASPxTextBox>
                                                        </FooterTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TK nợ" FieldName="TKno" 
                                                        ShowInCustomizationForm="True" VisibleIndex="2" Width="80px">
                                                        <FooterTemplate>
                                                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" 
                                                                Width="100%">
                                                            </dx:ASPxComboBox>
                                                        </FooterTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TK có" FieldName="TKco" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3" Width="80px">
                                                        <FooterTemplate>
                                                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" NullText="156" 
                                                                ValueType="System.String" Width="100%">
                                                            </dx:ASPxComboBox>
                                                        </FooterTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                        ShowInCustomizationForm="True" VisibleIndex="10">
                                                        <EditButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Edit" />
                                                            </Image>
                                                        </EditButton>
                                                        <NewButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_New" />
                                                            </Image>
                                                        </NewButton>
                                                        <DeleteButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Delete" />
                                                            </Image>
                                                        </DeleteButton>
                                                        <CancelButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                                            </Image>
                                                        </CancelButton>
                                                        <UpdateButton Visible="True">
                                                            <Image>
                                                                <SpriteProperties CssClass="Sprite_Apply" />
                                                            </Image>
                                                        </UpdateButton>
                                                        <ClearFilterButton Visible="True">
                                                        </ClearFilterButton>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="Note" 
                                                        ShowInCustomizationForm="True" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Giá trị" FieldName="Value" 
                                                        ShowInCustomizationForm="True" VisibleIndex="4" Width="80px">
                                                        <CellStyle HorizontalAlign = "Right"></CellStyle>
                                                        <FooterTemplate>                                                            
                                                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%" Text = "2.200.000" HorizontalAlign = "Right">
                                                        </dx:ASPxTextBox>
                                                        </FooterTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsEditing Mode="Inline" />
                                                <Settings ShowFooter="True" />
                                                <Styles>
                                                    <Header Font-Bold="True" HorizontalAlign="Center">
                                                    </Header>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </div>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="popup_report" runat="server" CloseAction="CloseButton" ClientInstanceName="popup_Report"
    AllowDragging="True" AllowResize="True" PopupAnimationType="None" PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter" EnableViewState="False" HeaderText="" Height="600px"
    Width="850px" DragElement="Window">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <div style="height: 100%; width: 100%; overflow: hidden">
                <dx:ASPxSplitter ID="PrintLayoutSplitter" runat="server" FullscreenMode="True" Width="100%"
                    Height="100%" Orientation="Vertical" SeparatorVisible="false" ClientInstanceName="PrintLayoutSplitter">
                    <Panes>
                        <dx:SplitterPane Size="40" Name="ToolbarPane" MinSize="20">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                    <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" ReportViewer="<%# ReportViewer2 %>"
                                        ReportViewerID="ReportViewer2">
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
                                    <dx:ReportViewer ID="ReportViewer2" runat="server" Border-BorderStyle="Solid" Border-BorderColor="Black"
                                        Border-BorderWidth="1px" Report="<%# new WebModule.Warehouse.Report._02_VT() %>"
                                        ReportName="WebModule.Warehouse.Report._02_VT">
                                        <Border BorderWidth="1px" BorderColor="Black" BorderStyle="Solid"></Border>
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
</dx:ASPxPopupControl>
