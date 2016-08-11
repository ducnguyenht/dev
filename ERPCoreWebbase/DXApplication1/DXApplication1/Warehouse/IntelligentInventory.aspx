<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="IntelligentInventory.aspx.cs" Inherits="WebModule.Warehouse.IntelligentInventory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">

    // MainForm Event
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formEntryDetail.Show();

            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');
            cboRowStatus.SetValue('A');

            if (s.cpEdit == 'edit') {
                cpLine.PerformCallback('edit');
            }
            else {
                cpLine.PerformCallback('new');
            }

            delete (cpEdit);
            return;
        }
    }

    function grdData_CustomButtonClick(s, e) {
        s.GetRowValues(e.visibleIndex, 'ProductId', OnGetRowValues);
    }

    function OnGetRowValues(values) {
        formEntryDetail.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }

    // EditForm Event 
    function buttonSave_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('cboManufacturer').Validate();
            return;
        }

        cpLine.PerformCallback('save');
        cpHeader.PerformCallback('refresh');

    }

    function buttonCancel_Click(s, e) {
        formEntryDetail.Hide();
    }

    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    Danh sách hàng hóa tồn kho</div>
<dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            
    <dx:ASPxGridView runat="server" AutoGenerateColumns="False" ID="grdDataAccept" 
        onstartrowediting="grdDataAccept_StartRowEditing1">
        <ClientSideEvents EndCallback="grdData_EndCallback" 
            CustomButtonClick="grdData_CustomButtonClick" />
        <Columns>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="9">
                <EditButton Text="Xem chi tiết" Visible="True">
                </EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="codedepence" 
                ShowInCustomizationForm="True" Caption="Thuộc chứng từ" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="code" ShowInCustomizationForm="True" 
                Caption="M&#227; số" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="name" ShowInCustomizationForm="True" 
                Caption="T&#234;n" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="unit" ShowInCustomizationForm="True" 
                Caption="Đơn vị t&#237;nh" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="lot" ShowInCustomizationForm="True" 
                Caption="Số l&#244;" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="description" 
                ShowInCustomizationForm="True" Caption="Ghi ch&#250;" VisibleIndex="8">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="amountdiff" 
                ShowInCustomizationForm="True" Caption="Ch&#234;nh lệch" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="amountreal" 
                ShowInCustomizationForm="True" Caption="SL thực tế" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="amount" ShowInCustomizationForm="True" 
                Caption="SL theo chứng từ" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFilterRow="True">
        </Settings>
    </dx:ASPxGridView>
            
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="403px">
                <Panes>

                    <dx:SplitterPane>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" 
                Theme="Default">
            <Items>
                <dx:LayoutItem Caption="SL tồn kho cao nhất">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="SL tồn kho thấp nhất">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="SL tồn kho cần bổ sung">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="170px">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
            </dx:ASPxFormLayout>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane>
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/warehousechar.jpg">
            </dx:ASPxImage>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>
    
       </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
</asp:Content>

