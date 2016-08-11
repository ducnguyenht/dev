<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReportDevice.aspx.cs" Inherits="WebModule.Warehouse.ReportDevice" %>
<%@ Register src="UserControl/uReportPivot.ascx" tagname="uReportPivot" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function grid_CustomButtonClick(s, e) {
        if (e.buttonID != 'popPivot') return;
        //rowVisibleIndex = e.visibleIndex;
        s.GetRowValues(e.visibleIndex, 'name', ShowPopup);

    }

    function ShowPopup(productName) {
        pcReportViewerPopup.Show();
        pcReportViewerPopup.SetHeaderText("Báo cáo tồn công cụ dụng cụ: " + productName);
        reportType.Set('ReportType', "ReportDevice");
        pcReportViewerPopup.PerformCallback();
    }
    
 </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh sách công cụ dụng cụ" 
        Font-Bold="True" Font-Size="Small">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="gr_data" runat="server" AutoGenerateColumns="False" 
        oninitnewrow="ASPxGridView1_InitNewRow" 
        onstartrowediting="ASPxGridView1_StartRowEditing" Width="100%">
        <ClientSideEvents CustomButtonClick="function(s, e) {
	grid_CustomButtonClick(s, e);
}" />
        <Columns>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                VisibleIndex="3">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="popPivot">
                        <Image ToolTip="Bảng">
                            <SpriteProperties CssClass="Sprite_Grid" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="description" 
                VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên CCDC" FieldName="name" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã CCDC" FieldName="code" 
                VisibleIndex="0">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowHeaderFilterButton="True" />
    </dx:ASPxGridView>
    <div id="clientContainer">       
    <uc1:uReportPivot ID="uReportPivot" runat="server" />       
</div>

</asp:Content>

