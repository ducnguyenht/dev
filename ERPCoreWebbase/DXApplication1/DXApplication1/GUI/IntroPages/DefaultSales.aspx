<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DefaultSales.aspx.cs" Inherits="WebModule.GUI.IntroPages.DefaultSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="intro-wrapper">
        <h1 class="intro-title">
            Nhóm chức năng Nghiệp vụ bán</h1>
        <img alt="intro image" class="intro-image float-left" src="../../images/Intro/purchasing.png" />
        <div style="margin-top: 10px;">
            <div class="col1 float-left">
                <div class="intro-paragraph">
                    Nhóm chức năng Bán hàng giúp doanh nghiệp thực hiện đầy đủ các nghiệp vụ: bán hàng
                    hóa (dược phẩm, trang thiết bị y tế...), bán nguyên vật liệu và cung cấp dịch vụ.
                    Bên cạnh đó hệ thống còn giúp cho doanh nghiệp thiết lập chính sách giá cho từng
                    mặt hàng theo từng khách hàng, từng nhóm khách hàng, theo số lượng...
                </div>
                <div class="intro-paragraph">
                    Với chức năng này, doanh nghiệp có thể dễ dàng quản lý các đơn đặt hàng từ phía
                    khách hàng đồng thời theo dõi tiến độ thực hiện đơn hàng để có thể đưa ra các biện
                    pháp phù hợp. Ngoài ra, phân hệ này còn giúp doanh nghiệp theo dõi công nợ phải
                    thu theo hoá đơn, đơn hàng, hợp đồng, khách hàng, nhân viên, đối tác,... công nợ
                    đến hạn thu, công nợ quá hạn thu, tính lãi nợ quá hạn. Qua đó doanh nghiệp dễ dàng
                    quản trị tổng số lượng bán của từng mặt hàng, doanh số theo nhân viên, cửa hàng,
                    chi nhánh, toàn công ty, doanh số theo khách hàng, doanh số theo thị trường, doanh
                    số theo mặt hàng, chi phí, chiết khấu, giảm giá, công nợ,...
                </div>
                <div class="intro-paragraph">
                    Phân hệ Bán hàng được chia làm 4 phần: cấu hình danh mục, chính sách giá, quản lý bán hàng, công nợ.
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="col2 float-left" style="margin-top: 20px;">
                <div class="intro-paragraph">
                    <span class="intro-title">Cấu hình danh mục</span> Là nơi để người sử dụng nhập dữ liệu và tiến hành cấu hình cho hàng hóa, nguyên vật liệu và dịch vụ.</div>
                <div class="intro-paragraph">
                    <span class="intro-title">Chính sách giá</span>
                    Quy định các chương trình khuyến mãi cùng với cách áp dụng các chương trình đó để tạo nên giá bán ứng với từng trường hợp cụ thể. Gồm 3 mục chính: chính sách khuyến mãi, phương pháp tạo giá bán và hiệu chỉnh giá bán.
                </div>
            </div>
            <div class="col2 float-left" style="margin-top: 20px;">
                <div class="intro-paragraph">
                    <span class="intro-title">Quản lý bán hàng</span>
                    <ul>
                        <li>Quản lý các đơn hàng nhận từ khách hàng và xuất hóa đơn bán hàng sau khi so sánh đối chiếu với số lượng thực tế.</li>
                        <li>Theo dõi quá trình giao nhận và thanh toán.</li>
                    </ul>
                </div>
                <div class="intro-paragraph">
                    <span class="intro-title">Công nợ</span>
                    <ul>
                        <li>Quản lý công nợ của từng đối tác để kịp thời lên phương án <br />thanh toán.</li>
                        <li>Quản lý công nợ của từng khách hàng để tiến hành đốc thúc, nhắc nhở.</li>
                    </ul>
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
