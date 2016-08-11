<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="WarehouseInventory.aspx.cs" Inherits="WebModule.WarehouseInventory" %>
<%@ Register src="UserControl/AcceptEntry.ascx" tagname="AcceptEntry" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grdData_EndCallback(s, e) {
        if (s.cpEdit) {
            formAcceptEntry.Show();

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

    function cpHeader_EndCallback(s, e) {
    }

    function cpLine_EndCallback(s, e) {
        if (s.cpCodeExisting) {

        }

    }

    function buttonSave_Click(s, e) {
        if (ASPxClientControl.GetControlCollection().GetByName('txtCode').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtCode').Validate();
            return;
        }

        if (ASPxClientControl.GetControlCollection().GetByName('txtName').GetValue() == null) {
            ASPxClientControl.GetControlCollection().GetByName('txtName').Validate();
            return;
        }

        //cpLine.PerformCallback('validate');

        cpLine.PerformCallback('save');

        cpHeader.PerformCallback('refresh');

    }

    function buttonCancel_Click(s, e) {
        formAcceptEntry.Hide();
    }

    function grdData_CustomButtonClick(s, e) {
        alert(1);
        s.GetRowValues(e.visibleIndex, 'SupplierId', OnGetRowValues);
    }

    function OnGetRowValues(values) {
        formAcceptEntry.Show();
        ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

        var str = 'view|' + values;
        cpLine.PerformCallback(str);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Kiểm kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
</div>
<div style="margin-bottom:10px;">
    <dx:ASPxPageControl ID="pc" ClientInstanceName="pc" runat="server" ActiveTabIndex="2" 
        RenderMode="Lightweight" Width="100%" Height="100%">
        <TabPages>
            <dx:TabPage Name="Personal" 
                Text="Vị trí hàng hóa trong kho">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                    <div style="overflow:auto; height:450px">
                        <asp:XmlDataSource ID="XmlDataSource1" runat="server"></asp:XmlDataSource>
                        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="430px">
                            <Panes>
                                <dx:SplitterPane AutoHeight="True" Size="200px">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <strong>Vị trí<br /> </strong>
                                            <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                                KeyFieldName="OrganizationId" ParentFieldName="ParentOrganizationId">
                                                <SettingsBehavior AllowFocusedNode="True" />
                                                <Columns>
                                                    <dx:TreeListTextColumn Caption="Kho" FieldName="name" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                                    </dx:TreeListTextColumn>
                                                </Columns>
                                                <settingsbehavior allowfocusednode="True" />

<SettingsBehavior AllowFocusedNode="True"></SettingsBehavior>
                                            </dx:ASPxTreeList>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane AutoHeight="True">
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                                            <strong>Danh sách mặt hàng</strong><dx:ASPxGridView ID="grdDataAccept" runat="server" AutoGenerateColumns="False">
                                                <settings showfilterrow="True" />

<Settings ShowFilterRow="True"></Settings>
                                                <Columns>
                                                    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="description" 
                                                        ShowInCustomizationForm="True" VisibleIndex="7">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" 
                                                        ShowInCustomizationForm="True" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                                                        ShowInCustomizationForm="True" VisibleIndex="8">
                                                        <EditButton Visible="True">
    <Image ToolTip="Sửa">
        <SpriteProperties CssClass="Sprite_Edit" />
    </Image>
</EditButton>
<NewButton Visible="True">
    <Image ToolTip="Thêm">
        <SpriteProperties CssClass="Sprite_New" />
    </Image>
</NewButton>
<DeleteButton Visible="True">
    <Image ToolTip="Xóa">
        <SpriteProperties CssClass="Sprite_Delete" />
    </Image>
</DeleteButton>
<ClearFilterButton Visible="True">
    <Image ToolTip="Hủy">
        <SpriteProperties CssClass="Sprite_Clear" />
    </Image>
</ClearFilterButton>
<UpdateButton>
    <Image ToolTip="Cập nhật">
        <SpriteProperties CssClass="Sprite_Apply" />
    </Image>
</UpdateButton>
<CancelButton>
    <Image ToolTip="Bỏ qua">
        <SpriteProperties CssClass="Sprite_Cancel" />
    </Image>
</CancelButton>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="Mã số" FieldName="code" 
                                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" 
                                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="unit" 
                                                        ShowInCustomizationForm="True" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="Số lô" FieldName="lot" 
                                                        ShowInCustomizationForm="True" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption=" SL thực tế" FieldName="realamount" 
                                                        ShowInCustomizationForm="True" VisibleIndex="5">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="SL theo CT" FieldName="recieptamount" 
                                                        ShowInCustomizationForm="True" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings ShowFilterRow="True" />
                                            </dx:ASPxGridView>
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                        </dx:ASPxSplitter>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="Confirmation" Text="Bút toán hàng tồn kho" 
                ClientEnabled="False">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                     <div style="overflow:auto; height:450px">
                    <div style="margin-bottom:10px;">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách hàng hóa nhập kho" 
        Font-Bold="True" Font-Size="Small">            
    </dx:ASPxLabel>
      
</div>
<dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" 
                ClientInstanceName="cpHeader" oncallback="cpHeader_Callback" 
                HideContentOnCallback="True" >
    <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxGridView ID="grdData" Width="100%" runat="server" 
                AutoGenerateColumns="False" OnStartRowEditing="grdData_StartRowEditing">
                <ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}" CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback" />
<ClientSideEvents SelectionChanged="function(s, e) {
	var key = s.GetRowKey(e.visibleIndex);
}"></ClientSideEvents>
                <Columns>
                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="10" 
                        Caption="Thao tác">
                        <EditButton Visible="True" Text="Sửa bút toán">
                        </EditButton>
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                        <HeaderStyle HorizontalAlign="Center" />
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Mã số" ShowInCustomizationForm="True" 
                        VisibleIndex="0" FieldName="code">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Đơn vị tính" ShowInCustomizationForm="True" 
                        VisibleIndex="2" FieldName="unit">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Bút toán" FieldName="entry" 
                        ShowInCustomizationForm="True" VisibleIndex="8">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Chênh lệch" FieldName="difamount" 
                        ShowInCustomizationForm="True" VisibleIndex="7">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SL thực tế" FieldName="realamount" 
                        ShowInCustomizationForm="True" VisibleIndex="6">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SL theo chứng từ" FieldName="recieptamount" 
                        ShowInCustomizationForm="True" VisibleIndex="5">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Thuộc chứng từ" FieldName="reciept" 
                        ShowInCustomizationForm="True" VisibleIndex="4">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Lô" FieldName="lot" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Tên" FieldName="name" 
                        ShowInCustomizationForm="True" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFilterRow="True" />

<Settings ShowFilterRow="True"></Settings>

            </dx:ASPxGridView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
</div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                <ContentCollection>
                            <dx:ContentControl ID="ContentControlFinish" runat="server">
                            <div style="overflow:auto; height:450px">
                                <div class="tabPageContent finishArea">
                                    <p>
                                        Hoàn tất kiểm kho</p>
                                    <dx:ASPxButton ID="btnGoToSchedule" runat="server" 
                                         CssClass="finishAreaButton" HorizontalAlign="Center" UseSubmitBehavior="False" 
                                         Text="Tiếp tục kiểm kho">
                                    </dx:ASPxButton>
                                </div>
                            </div>
                            </dx:ContentControl>
                        </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
     <dx:ASPxPanel ID="dxpError" ClientInstanceName="dxpError" runat="server" CssClass="errorPanel"
                ClientVisible="false">
                <PanelCollection>
                    <dx:PanelContent>
                        Please complete or correct the fields highlighted in red.
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
            <dx:ASPxHiddenField ID="hfRegInfo" ClientInstanceName="hfRegInfo" runat="server"
                SyncWithServer="true" ViewStateMode="Enabled">
            </dx:ASPxHiddenField>
</div>
<div class="buttonsArea">
                <div class="buttons">
                    <table cellpadding="0" cellspacing="0" border="0" class="buttonsTable">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnBack" ClientInstanceName="btnBack" runat="server" 
                                    AutoPostBack="false" ClientVisible="false" CausesValidation="false" 
                                    UseSubmitBehavior="False" Text="Trở về" BackColor="#0066FF" ForeColor="White">
                                    <ClientSideEvents Click="OnBackButtonClick" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" 
                                    AutoPostBack="false" CausesValidation="false" UseSubmitBehavior="true" 
                                    Text="Tiếp theo" BackColor="#0066FF" ForeColor="White">
                                    <ClientSideEvents Click="OnNextButtonClick" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnFinish" ClientInstanceName="btnFinish" runat="server" 
                                    AutoPostBack="false" ClientVisible="false" UseSubmitBehavior="false" 
                                    Text="Hoàn tất" BackColor="#0066FF" ForeColor="White">
                                    <ClientSideEvents Click="OnFinishButtonClick"/>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <uc1:AcceptEntry ID="AcceptEntry" runat="server" />   
</asp:Content>
