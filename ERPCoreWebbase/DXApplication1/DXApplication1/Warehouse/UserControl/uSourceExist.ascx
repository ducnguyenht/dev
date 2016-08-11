<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uSourceExist.ascx.cs" Inherits="WebModule.Warehouse.UserControl.uSourceExist" %>
<dx:ASPxPopupControl ScrollBars="Auto" CssClass="pcReportViewerPopup" ID="pcReportViewerPopup"
    runat="server" ClientInstanceName="pcReportViewerPopup" HeaderText="Ngưỡng tồn kho hàng hóa"
    MinHeight="550px" MinWidth="860px" Height="550px" RenderMode="Lightweight" Width="860px"
    AllowDragging="true" Modal="true" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
    AllowResize="True" CloseAction="CloseButton">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">


            <dx:ASPxImage ID="ASPxImage1" runat="server" 
                ImageUrl="~/Warehouse/Datasource/NguongTonKhoHangHoa.jpg">
            </dx:ASPxImage>


        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
