<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebModule.GUI.Administrator.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="intro-wrapper">
        <h1 class="intro-title">
            Nhóm chức năng Hệ thống</h1>
        <img alt="intro image" class="intro-image float-left" src="../../images/Intro/system.png" />
        <div class="col1 float-left" style="margin-top: 10px;">
            <p class="intro-paragraph">
                Nhóm chức năng Hệ thống cho phép quản trị viên của tổ chức quản lý (tạo mới, xoá,
                sửa) người dùng, các phòng ban, các tổ chức trực thuộc tổ chức này. Với nhóm chức
                năng Hệ thống quản trị viên của tổ chức còn có thể quản lí các vai trò của từng
                phòng ban, từng tổ chức trực thuộc cũng như phân quyền truy xuất cho người dùng
                của tổ chức.
            </p>
            <p class="intro-paragraph">
                Thêm vào đó nhóm chức năng Hệ thống cũng cung cấp chức năng cấu hình máy chủ email
                để phục vụ việc mời quản trị viên và người dùng vào tổ chức.
            </p>
            <p class="intro-paragraph">
                Các chức năng trong nhóm chức năng Hệ thống gồm:</p>
            <p class="intro-paragraph">
                <span class="intro-title">Người dùng</span> Cho phép người quản trị mời thêm người
                dùng mới vào tổ chức qua email, tạm ngưng hoặc kích hoạt một người dùng, và quản
                lý danh sách người sử dụng hiện có.</p>
            <p class="intro-paragraph">
                <span class="intro-title">Phòng ban</span> Giúp quản trị viên quản lý các phòng
                ban của tổ chức theo cấu trúc cây phân cấp cùng các thông tin liên quan của từng
                phòng ban. Chức năng Phòng ban còn giúp quản trị viên có thể quản lý vai trò của
                từng phòng ban một cách dễ dàng.</p>
            <p class="intro-paragraph">
                <span class="intro-title">Tổ chức trực thuộc</span>Giúp quản trị viên quản lý các
                tổ chức trực thuộc tổ chức cùng các phòng ban thuộc tổ chức trực thuộc theo cấu
                trúc cây phân cấp. Quản trị viên cũng có thể quản lí vai trò của tổ chức trực thuộc
                và phòng ban của tổ chức trực thuộc một cách dễ dàng.</p>
            <p class="intro-paragraph">
                <span class="intro-title">Phân quyền người dùng</span>Chức năng này cho phép quản
                trị viên của tổ chức gán các vai trò của tổ chức hoặc các vai trò của phòng ban
                hay vai trò tổ chức trực thuộc cho những người dùng đã được mời vào tổ chức.</p>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
