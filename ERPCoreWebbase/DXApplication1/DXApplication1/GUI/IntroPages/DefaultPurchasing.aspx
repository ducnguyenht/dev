<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="DefaultPurchasing.aspx.cs" Inherits="WebModule.GUI.IntroPages.DefaultPurchasing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="intro-wrapper">
        <h1 class="intro-title">
            Nhóm chức năng Nghiệp vụ mua</h1>
        <img alt="intro image" class="intro-image float-left" src="../../images/Intro/purchasing.png" />
        <div style="margin-top: 10px;">
            <div class="col1 float-left">
                <div class="intro-paragraph">
                    Nhóm chức năng Mua hàng giúp doanh nghiệp thực hiện đầy đủ các nghiệp vụ: mua hàng
                    hóa (dược phẩm, trang thiết bị y tế...), mua dịch vụ (lắp đặt, bảo trì, sửa chữa...),
                    mua nguyên vật liệu để phục vụ sản xuất và mua công cụ dụng cụ để sử dụng.
                </div>
                <div class="intro-paragraph">
                    Bên cạnh đó, hệ thống còn giúp doanh nghiệp lưu trữ và quản lý nhu cầu mua hàng
                    trong nội bộ doanh nghiệp, báo giá từ nhà cung cấp cũng như phiếu mua hàng của doanh
                    nghiệp. Sau khi tiến hành đặt hàng, phần mềm giúp người sử dụng theo dõi tiến độ
                    đơn hàng cũng như quản lý công nợ nhà cung cấp.
                </div>
                <div class="intro-paragraph">
                    Ngoài ra, phần mềm còn giúp doanh nghiệp đánh giá chất lượng nhà cung cấp đồng thời
                    hỗ trợ trong việc ra quyết định chọn nhà cung cấp.
                </div>
                <div class="intro-paragraph">
                    Nghiệp vụ mua được chia làm 5 phần: Cấu hình danh mục, Đặt hàng, Tiến độ, Vấn đề
                    và Phân tích.</div>
            </div>
            <div class="clear"></div>
            <div class="col2 float-left" style="margin-top: 20px;">
                <div class="intro-paragraph">
                    <span class="intro-title">Cấu hình danh mục</span> Là nơi để người sử dụng nhập
                    dữ liệu và tiến hành cấu hình cho hàng hóa, dịch vụ, nguyên vật liệu và công cụ
                    dụng cụ.</div>
                <div class="intro-paragraph">
                    <span class="intro-title">Đặt hàng</span>
                    <ul>
                        <li>Quản lý nhu cầu mua hàng của doanh nghiệp. Nhu cầu được thể hiện qua phiếu yêu cầu
                            từ các phòng ban gửi về bộ phận chịu trách nhiệm mua hàng.</li>
                        <li>Quản lý báo giá từ các nhà cung cấp gửi đến doanh nghiệp.</li>
                        <li>Quản lý phiếu mua hàng của doanh nghiệp gửi đến nhà cung cấp.</li>
                    </ul>
                </div>
            </div>
            <div class="col2 float-left" style="margin-top: 20px;">
                <div class="intro-paragraph">
                    <span class="intro-title">Tiến độ</span>
                    <ul>
                        <li>Theo dõi tiến độ giao hàng của từng đơn hàng, so sánh đối chiếu giữa tình hình giao
                            hàng thực tế với lịch trình đã được quy định trong hợp đồng để kịp thời đưa ra giải
                            pháp nếu phát hiện sai lệch.</li>
                        <li>Theo dõi tiến độ thanh toán của từng đơn hàng.</li>
                        <li>Quản lý công nợ của từng nhà cung cấp để kịp thời lên phương án thanh toán.</li>
                    </ul>
                </div>
                <div class="intro-paragraph">
                    <span class="intro-title">Vấn đề</span>Là nơi chia sẻ và lưu trữ các vấn đề trong
                    quá trình thực hiện nghiệp vụ mua hàng.</div>
                <div class="intro-paragraph">
                    <span class="intro-title">Phân tích</span>Giúp doanh nghiệp so sánh chất lượng giữa
                    các nhà cung cấp dựa trên các tiêu chí giá cả, hạn giao hàng, hạn thanh toán, chính
                    sách khuyến mãi... để từ đó ra quyết định đặt hàng.</div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>
