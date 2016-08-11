<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="NASCustomFieldDataGridView.ascx.cs"
    Inherits="WebModule.ERPSystem.CustomField.GUI.Control.NASCustomFieldDataGridView" %>
<dx:ASPxGridView ID="gridviewObjectCustmoField" runat="server" AutoGenerateColumns="False"
    DataSourceID="dsObjectCustomField" KeyFieldName="ObjectCustomFieldId" Width="100%"
    OnInit="gridviewObjectCustmoField_Init" ViewStateMode="Enabled" 
    onpageindexchanged="gridviewObjectCustmoField_PageIndexChanged">
    <ClientSideEvents EndCallback="function(s, e) { 
        if(s.cpEvent == 'Refresh') {  
            s.PerformCallback('Refresh');
            delete s.cpEvent; 
        } 
        else if(s.cpEvent == 'ForceRefresh') {
            s.PerformCallback();
            delete s.cpEvent; 
        }
     }" />
    <Columns>
        <dx:GridViewDataTextColumn FieldName="ObjectTypeCustomFieldId.CustomFieldId.Name"
            VisibleIndex="0" Width="50%">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="ObjectCustomFieldId" ReadOnly="True" VisibleIndex="1"
            Width="50%">
            <CellStyle Wrap="True">
            </CellStyle>
        </dx:GridViewDataTextColumn>
    </Columns>
    <Settings ShowColumnHeaders="False" />
    <Settings ShowColumnHeaders="False"></Settings>
</dx:ASPxGridView>
<dx:XpoDataSource ID="dsObjectCustomField" runat="server" TypeName="NAS.DAL.CMS.ObjectDocument.ObjectCustomField"
    Criteria="[ObjectId.ObjectId] = ?">
    <CriteriaParameters>
        <asp:Parameter Name="ObjectId" />
    </CriteriaParameters>
</dx:XpoDataSource>
