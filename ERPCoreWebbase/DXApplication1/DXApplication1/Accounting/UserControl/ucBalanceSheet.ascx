<%@ Control Language="C#" ClientIDMode = "AutoID" AutoEventWireup="true" CodeBehind="ucBalanceSheet.ascx.cs" Inherits="WebModule.Accounting.UserControl.ucBalanceSheet" %>
<script type = "text/javascript">    
    $(document).ready(function () {
       // cbGet.PerformCallback();
    });
    $(document).load(function () {
      //  cbGet.PerformCallback();
    });   
    function setValue(s, e) {
      $("#divAssetTotal .dxeEditAreaSys").attr({"value":e.result.split("||")[0]});    
      $("#divLiabilityTotal .dxeEditAreaSys").attr({"value":e.result.split("||")[1]});
      $("#divEquityTotal .dxeEditAreaSys").attr({ "value": e.result.split("||")[2] });
      $("#divDebitTotal .dxeEditAreaSys").attr({ "value": e.result.split("||")[3] });
    }
</script>
<dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" KeyFieldName="AccountId" 
    ParentFieldName="ParentAccountId" Width="100%">
    <Columns>
        <dx:TreeListTextColumn Caption="Tài sản" FieldName="Code" VisibleIndex="0" 
            Width="250px" SortIndex="0" SortOrder="Ascending">
            <FooterCellTemplate>                
                <dx:ASPxLabel ID="ASPxLabel111" runat="server" Text="Tổng tài sản" Font-Bold="True" ForeColor = "Blue">
                </dx:ASPxLabel>
            </FooterCellTemplate>
        </dx:TreeListTextColumn>
        <dx:TreeListTextColumn Caption=" " VisibleIndex="1" FieldName="Name">
        </dx:TreeListTextColumn>
        <dx:TreeListSpinEditColumn Caption="Nợ" VisibleIndex="2" Width="120px" 
            FieldName="Balance">
            <PropertiesSpinEdit DisplayFormatString="#,#;-#,#;0" NumberFormat="Custom" 
                NullDisplayText="0" NullText="0">
            </PropertiesSpinEdit>
            <footercelltemplate>
                <div ID="divAssetTotal">
                    <dx:ASPxTextBox ID="AssetTotal" ClientEnabled = "False" runat="server" ClientInstanceName="AssetTotal"  DisplayFormatString="#,#;-#,#;0"
                        ForeColor="Blue" HorizontalAlign="Right" Width="100%" 
                        ReadOnly="True">
                    </dx:ASPxTextBox>
                </div>
            </footercelltemplate>
        </dx:TreeListSpinEditColumn>
        <dx:TreeListSpinEditColumn Caption="Có" VisibleIndex="3" Width="120px">
            <propertiesspinedit displayformatstring="0," numberformat="Custom"></propertiesspinedit>
        </dx:TreeListSpinEditColumn>
    </Columns>
    <Settings ShowFooter="True" />

<Settings ShowFooter="True"></Settings>

    <Styles>
        <Header Font-Bold="True" Font-Size="Small">
        </Header>
    </Styles>
</dx:ASPxTreeList>
<br/>

<dx:ASPxTreeList ID="ASPxTreeList2" runat="server" AutoGenerateColumns="False" KeyFieldName="AccountId" 
    ParentFieldName="ParentAccountId" Width="100%">
    <Columns>
        <dx:TreeListTextColumn Caption="Nợ Phải Trả" VisibleIndex="0" FieldName="Code" 
            Width="250px" SortIndex="0" SortOrder="Ascending">
            <FooterCellTemplate>                
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Tổng Nợ Phải Trả" Font-Bold="True" >
                </dx:ASPxLabel> 
            </FooterCellTemplate>
        </dx:TreeListTextColumn>
        <dx:TreeListTextColumn Caption=" " FieldName="Name" VisibleIndex="1" 
            ShowInCustomizationForm="True">
        </dx:TreeListTextColumn>
        <dx:TreeListSpinEditColumn Caption="Có" VisibleIndex="3" Width="120px" 
            FieldName="Balance">
            <PropertiesSpinEdit DisplayFormatString="#,#;-#,#;0" NumberFormat="Custom">
            </PropertiesSpinEdit>
            <FooterCellTemplate>
            <div id = "divLiabilityTotal">
                <dx:ASPxTextBox ID="LiabilityTotal" runat="server"  ClientInstanceName = "LiabilityTotal"
                    DisplayFormatString="#,#;-#,#;0" HorizontalAlign="Right" Width="100%" ClientEnabled = "False" ReadOnly="True">
                </dx:ASPxTextBox>
                </div>
            </FooterCellTemplate>
        </dx:TreeListSpinEditColumn>
        <dx:TreeListSpinEditColumn Caption="Nợ" VisibleIndex="2" 
            Width="120px">
            <PropertiesSpinEdit DisplayFormatString="#,#;-#,#;0" NumberFormat="Custom">
            </PropertiesSpinEdit>
        </dx:TreeListSpinEditColumn>
    </Columns>
    <Settings ShowFooter="True" />

<Settings ShowFooter="True"></Settings>

    <Styles>
        <Header Font-Bold="True" Font-Size="Small">
        </Header>
    </Styles>
</dx:ASPxTreeList>
<dx:ASPxTreeList ID="ASPxTreeList3" runat="server" AutoGenerateColumns="False" KeyFieldName="AccountId" 
    ParentFieldName="ParentAccountId" Width="100%">
    <Columns>
        <dx:TreeListTextColumn Caption="Vốn Chủ Sở Hữu" VisibleIndex="0" 
            FieldName="Code" Width="250px" SortIndex="0" SortOrder="Ascending">
            <FooterCellTemplate>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Tổng Vốn Chủ Sở Hữu" Font-Bold="True">
                </dx:ASPxLabel>   
                <br />             
                <br />    
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Nguồn Vốn" Font-Bold="True" ForeColor = "Red">
                </dx:ASPxLabel>
            </FooterCellTemplate>
        </dx:TreeListTextColumn>
        <dx:TreeListTextColumn Caption=" " FieldName="Name" VisibleIndex="1">
        </dx:TreeListTextColumn>
        <dx:TreeListSpinEditColumn Caption="Có" FieldName="Balance" VisibleIndex="3" 
            Width="120px">
            <PropertiesSpinEdit DisplayFormatString="#,#;-#,#;0" NumberFormat="Custom">
            </PropertiesSpinEdit>
            <FooterCellTemplate>
            <div id = "divEquityTotal">
                <dx:ASPxTextBox ID="EquityTotal" ReadOnly="True" ClientEnabled = "False" ClientInstanceName = "EquityTotal" HorizontalAlign="Right" DisplayFormatString="#,#;-#,#;0" runat="server"  Width="100%">
                </dx:ASPxTextBox>
                </div>
                <br />
                <div id = "divDebitTotal">
                <dx:ASPxTextBox ID="DebitTotal" ReadOnly="True" ClientEnabled = "False" ClientInstanceName = "DebitTotal" HorizontalAlign="Right" DisplayFormatString="#,#;-#,#;0" runat="server" ForeColor="Red" 
                    Width="100%">
                </dx:ASPxTextBox> 
                </div>
            </FooterCellTemplate>
        </dx:TreeListSpinEditColumn>
        <dx:TreeListSpinEditColumn Caption="Nợ" VisibleIndex="2" Width="120px">
            <PropertiesSpinEdit DisplayFormatString="#,#;-#,#;0" NumberFormat="Custom">
            </PropertiesSpinEdit>
        </dx:TreeListSpinEditColumn>
    </Columns>
    <Settings ShowFooter="True" />

<Settings ShowFooter="True"></Settings>

    <Styles>
        <Header Font-Bold="True" Font-Size="Small">
        </Header>
    </Styles>
</dx:ASPxTreeList>
<dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName = "cbGet" 
    oncallback="ASPxCallback1_Callback">
    <ClientSideEvents CallbackComplete="setValue" />
</dx:ASPxCallback>
<dx:XpoDataSource ID="AssetAccountXPO" runat="server" 
    Criteria="[AccountTypeId.AccountCategoryId.Code] = 'ASSET' And [RowStatus] &gt; 0" 
    TypeName="NAS.DAL.Accounting.AccountChart.Account">
</dx:XpoDataSource>
<dx:XpoDataSource ID="LiabilityAccountXPO" runat="server" 
    Criteria="[AccountTypeId.AccountCategoryId.Code] = 'LIABILITY' And [RowStatus] &gt; 0" 
    TypeName="NAS.DAL.Accounting.AccountChart.Account">
</dx:XpoDataSource>
<dx:XpoDataSource ID="EquityAccountXPO" runat="server" 
    Criteria="[AccountTypeId.AccountCategoryId.Code] = 'EQUITY' And [RowStatus] &gt; 0" 
    TypeName="NAS.DAL.Accounting.AccountChart.Account">
</dx:XpoDataSource>

