<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ListEntry.aspx.cs" Inherits="WebModule.Warehouse.UserControl.ListEntry" %>
<%@ Register src="UserControl/uEntryDetail.ascx" tagname="uEntryDetail" tagprefix="uc1" %>
<%@ Register src="UserControl/uAdjustingHistory.ascx" tagname="uAdjustingHistory" tagprefix="uc2" %>

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

    function ShowInfoclick(s, e) {
        formEntryDetailHistory.Show();
    }

    
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
<div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách điều chỉnh hàng tồn kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
<dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">

    <dx:ASPxGridView runat="server" AutoGenerateColumns="False" ID="grdDataAccept" Width="100%" Settings-HorizontalScrollBarMode="Auto"
        onstartrowediting="grdDataAccept_StartRowEditing1">
        <ClientSideEvents EndCallback="grdData_EndCallback" 
            CustomButtonClick="grdData_CustomButtonClick" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="code" ShowInCustomizationForm="True" 
                Caption="Mã số">
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("code") %>'>
                        <ClientSideEvents Click="ShowInfoclick" />
                    </dx:ASPxHyperLink>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="name" ShowInCustomizationForm="True" 
                Caption="Tên">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="unit" ShowInCustomizationForm="True" 
                Caption="Đơn vị tính">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="codedepence" 
                ShowInCustomizationForm="True" Caption="Thuộc chứng từ">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="position" 
                ShowInCustomizationForm="True" Caption="Vị trí kho">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="amount" ShowInCustomizationForm="True" 
                Caption="SL theo chứng từ">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="amountreal" 
                ShowInCustomizationForm="True" Caption="SL thực tế">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="amountdiff" 
                ShowInCustomizationForm="True" Caption="Chênh lệch">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="Phẩm chất" HeaderStyle-HorizontalAlign="Center">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="Kém phẩm chất">
                        <DataItemTemplate>
                            .........
                        </DataItemTemplate>
                        <FooterTemplate>
                            .........
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Mất phẩm chất">
                        <DataItemTemplate>
                            .........
                        </DataItemTemplate>
                        <FooterTemplate>
                            .........
                        </FooterTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewDataTextColumn 
                ShowInCustomizationForm="True" Caption="Đề xuất điều chỉnh">
                <DataItemTemplate>
                    .........
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="adjusteddate" ShowInCustomizationForm="True" 
                Caption="Ngày điều chỉnh">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="status" 
                ShowInCustomizationForm="True" Caption="Trạng thái">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Caption="Thao tác" ButtonType = "Image" Width="70px">
                <EditButton Text="Điều chỉnh" Visible="True" >
                    <Image>
                        <SpriteProperties CssClass = "Sprite_Edit"/>
                    </Image>
                </EditButton>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="true" AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />
        <SettingsPager PageSize="22" ShowEmptyDataRows="true"></SettingsPager>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
        </Settings>
    </dx:ASPxGridView>
       </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
<uc1:uEntryDetail ID="uEntryDetail" runat="server" /> 
<uc2:uAdjustingHistory ID="uAdjustingHistory1" runat="server" /> 
</asp:Content>
