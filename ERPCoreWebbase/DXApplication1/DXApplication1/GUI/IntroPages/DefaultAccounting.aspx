<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DefaultAccounting.aspx.cs" Inherits="WebModule.GUI.IntroPages.DefaultAccounting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="intro-wrapper">
        <h1 class="intro-title">
            Nhóm chức năng Kế toán</h1>
        <img alt="intro image" class="intro-image float-left" src="../../images/Intro/accounting.png" />
        <div style="margin-top: 86px;">
            <div class="col1 float-left" style="padding-right: 0 !important">
                <div class="intro-paragraph">
                    Chức năng kế toán cho phép người sử dụng tạo cấu hình tài khoản theo đặc thù công
                    ty mình. Tuỳ theo nhu cầu sử dụng chức năng mà kế toán có thể tạo ra các sơ đồ kế
                    toán tự động, kết chuyển tự động.
                </div>
                <div class="intro-paragraph">
                    Ứng với mỗi nghiệp vụ được cấu hình sẵn, kế toán chỉ việc kiểm tra, duyệt lại các
                    bút toán. Ngoài ra, tổ chức phân quyền cho người có thẩm quyền được hạch toán các
                    bút toán cá biệt khác so với cấu hình.
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
