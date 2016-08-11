<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DefaultWarehouse.aspx.cs" Inherits="WebModule.GUI.IntroPages.DefaultWarehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="intro-wrapper">
        <h1 class="intro-title">
            Nhóm chức năng Quản lý kho</h1>
        <img alt="intro image" class="intro-image float-left" src="../../images/Intro/warehouse.png" />
        <div style="margin-top: 10px;">
            <div class="col1 float-left" style="padding-right:0 !important">
                <div class="intro-paragraph">
                    Giúp doanh nghiệp thực hiện tất cả nghiệp vụ nhập kho, xuất kho, chuyển kho nội
                    bộ, tính giá xuất kho, kiểm kho, bút toán...Cho phép thực hiện tính giá xuất kho
                    theo nhiều phương pháp: FIFO, LIFO, FEFO, bình quân gia quyền, đích danh... Phân
                    hệ Kho bãi được chia làm 2 phần: Cấu hình danh mục và Nghiệp vụ quản lý kho.
                </div>
                <div class="intro-paragraph">
                    <span class="intro-title">Cấu hình danh mục</span> Là nơi để người sử dụng nhập dữ liệu và tiến hành cấu hình cho kho bãi.</div>
                <div class="intro-paragraph">
                    <span class="intro-title">Nghiệp vụ quản lý kho</span>
                    <ul>
                        <li>Cho phép nhập kho hàng hóa theo đơn vị tính này và xuất ra theo đơn vị tính khác. Ví dụ: Nhập kho theo đơn vị thùng nhưng khi xuất ra bán tính theo đơn vị hộp, với tỉ lệ 1 thùng = 10 hộp.</li>
                        <li>Giúp kiểm soát được việc xuất kho đúng định mức quy định và tiết kiệm thời gian khi lập Phiếu xuất kho.</li>
                        <li>Quản lý phiếu nhập kho và  phiếu xuất kho.</li>
                        <li>Cho phép kế toán kho, thủ kho tiếp nhận những yêu cầu nhập kho, xuất kho của kế toán bán hàng, kế toán mua hàng giúp tiết kiệm thời gian không phải <br />nhập liệu lại.</li>
                        <li>Cho phép kế toán kho và thủ kho thực hiện kiểm kê kho nhằm phát hiện chênh lệch giữa sổ kế toán và thực tế. Nếu phát hiện mất mát hoặc thừa thì kế toán có thể tiến hành bút toán.</li>
                        <li>Chức năng tồn kho thông minh sẽ nhắc nhở người sử dụng đặt hàng và nhập hàng khi số lượng trong kho xuống dưới mức tối thiểu cho phép.</li>
                    </ul>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
