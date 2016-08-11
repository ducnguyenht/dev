<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Help.aspx.cs" Inherits="WebModule.Help.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="SideHolder" runat="server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <p>
        <br />
        <h3>
            Hướng dẫn sử dụng bằng video clip</h3>
        <ul>
            <li>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://www.youtube.com/watch?v=jf4Py5Ky0ps" Target="_blank">Phân bổ phiếu thu theo dự án</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://www.youtube.com/watch?v=Wd9WpdObVxw" Target="_blank">Phân bổ phiếu thu theo khách hàng cụ thể</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://www.youtube.com/watch?v=boemBgrEM-0" Target="_blank">Phân bổ phiếu thu theo phiếu bán hàng đã có</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://www.youtube.com/watch?v=IJkoaR44lI4" Target="_blank">Phân bổ phiếu chi nhân viên tạm ứng</asp:HyperLink>
            </li>
            <li>
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="http://www.youtube.com/watch?v=Ft0jPHH3QY0" Target="_blank">Phân bổ phiếu thu nhân viên tạm ứng</asp:HyperLink>
            </li>
        </ul>
        <p>
            &nbsp;</p>
    </p>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LeftSubmitContainer" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CenterSubmitContainer" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightSubmitContainer" runat="server">
</asp:Content>
