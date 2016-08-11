using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.XtraReports.UI;
using DevExpress.Web.ASPxClasses;
using System.Collections;
using WebModule.PayReceiving.Report;
using WebModule.Warehouse.Report;
using report.NewFolder1;
using report;

namespace WebModule.Accounting.Report
{
    public class ReportMappingConstant
    {
        public static ArrayList PayReceivingDS()
        {
            return new ArrayList                     
            {
                    new { key="1234", reportid = "S03a1-DN", name = "Sổ Nhật ký thu tiền"
                    },
                    new { key="123", reportid = "S03a2-DN", name = "Sổ Nhật ký chi tiền"
                    },
                    new { key="1234", reportid = "S07-DN", name = "Sổ quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S07a-DN", name = "Sổ kế toán chi tiết quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S08-DN", name = "Sổ tiền gửi ngân hàng"
                    },
                    new { key="1234", reportid = "S31-DN", name = "Sổ chi tiết thanh toán với người mua(người bán)"
                    },
                    new { key="1234", reportid = "S32-DN", name = "Sổ chi tiết thanh toán với người mua(người bán) bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S33-DN", name = "Sổ theo dõi thanh toán bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S34-DN", name = "Sổ chi tiết tiền vay"
                    }
            };
        }
        public static ArrayList getWarehouseDS()
        {
            return new ArrayList {
                    new { key="1234", reportid = "01-VT", name = "Phiếu Nhập Kho"
                    },
                    new { key="1234", reportid = "02-VT", name = "Phiếu Xuất Kho"
                    },
                    new { key="1234", reportid = "03-VT", name = "Biên Bản Kiểm Nghiệm Vật Tư, Công Cụ, Sản Phẩm, Hàng Hóa"
                    },
                    new { key="1234", reportid = "04-VT", name = "Phiếu Báo Vật Tư Còn Lại Cuối Kỳ"
                    },
                    new { key="1234", reportid = "05-VT", name = "Biên Bản Kiểm Kê Vật Tư, Công Cụ, Sản Phẩm, Hàng Hóa"
                    },
            /////////////04/09/2013 Duc.Vo Delete_code Issue:ERP-376----------------------------------------START
                    //new { key="1234", reportid = "06-VT", name = "Bảng Kê Mua Hàng"
                    //},
                    //new { key="1234", reportid = "07-VT", name = "Bảng Phân Bổ Nguyên Liệu, Vật Liệu, Công Cụ, Dụng Cụ"
                    //}
            /////////////04/09/2013 Duc.Vo Delete_code Issue:ERP-376----------------------------------------END
                    //new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    //},
                    //new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa (Mẫu chuẩn)"
                    //},
                    //new { key="1234", reportid = "S11-DN-SNL", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa (Mẫu Sâm Ngọc Linh)"
                    //},
                    //new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    //}                    
            };
        }

        public static ArrayList getProduceReportDS()
        {
            return new ArrayList {
                    new { key="1234", reportid = "07-VT", name = "Bảng Phân Bổ Nguyên Liệu, Vật Liệu, Công Cụ, Dụng Cụ"
                    }
            /////////////04/09/2013 Duc.Vo Delete_code Issue:ERP-376----------------------------------------START
                    //new { key="1234", reportid = "06-VT", name = "Bảng Kê Mua Hàng"
                    //},
                    //new { key="1234", reportid = "07-VT", name = "Bảng Phân Bổ Nguyên Liệu, Vật Liệu, Công Cụ, Dụng Cụ"
                    //}
            /////////////04/09/2013 Duc.Vo Delete_code Issue:ERP-376----------------------------------------END
                    //new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    //},
                    //new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa (Mẫu chuẩn)"
                    //},
                    //new { key="1234", reportid = "S11-DN-SNL", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa (Mẫu Sâm Ngọc Linh)"
                    //},
                    //new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    //}                    
            };
        }
        public static ArrayList getWarehouseRPDS()
        {
            return new ArrayList {                    
                    new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa (Mẫu chuẩn)"
                    },
                    new { key="1234", reportid = "S11-DN-SNL", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa (Mẫu Sâm Ngọc Linh)"
                    },
                    new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    }                    
            };
        }
        public static ArrayList getCurrencyDS()
        {
            return new ArrayList {
                    new { key="1234", reportid = "01-TT", name = "Phiếu thu"
                    },
                    new { key="1234", reportid = "02-TT", name = "Phiếu Chi"
                    },
                    new { key="1234", reportid = "03-TT", name = "Giấy đề nghị tạm ứng"
                    },
                    new { key="1234", reportid = "04-TT", name = "Giấy thanh toán tiền tạm ứng"
                    },
                    new { key="1234", reportid = "05-TT", name = "Giấy đề nghị thanh toán"
                    },
                    new { key="1234", reportid = "06-TT", name = "Biên lai thu tiền"
                    },
                    new { key="1234", reportid = "07-TT", name = "Bảng kê vàng, bạc, kim khí quý, đá quý"
                    },
                    new { key="1234", reportid = "08a-TT", name = "Bảng kiểm kê quỹ (dùng cho VND)"
                    },
                    new { key="1234", reportid = "08b-TT", name = "Bảng kiểm kê quỹ (dùng cho ngoại tệ, vàng, bạc, kim khí quý, đá quý)"
                    },
                    new { key="1234", reportid = "09-TT", name = "Bảng kê chi tiền"
                    }
            };
        }

        public static ArrayList getDiaryGeneralDS()
        {
            return new ArrayList { 
                    new { key="1234", reportid = "S03a-DN", name = "Sổ Nhật ký chung"
                    },
                    new { key="1234", reportid = "S03a1-DN", name = "Sổ Nhật ký thu tiền"
                    },
                    new { key="123", reportid = "S03a2-DN", name = "Sổ Nhật ký chi tiền"
                    },
                    new { key="1234", reportid = "S03a3-DN", name = "Sổ Nhật ký mua hàng"
                    },
                    new { key="1234", reportid = "S03a4-DN", name = "Sổ Nhật ký bán hàng"
                    },
                    new { key="1234", reportid = "S03b-DN", name = "Sổ Cái(dùng cho hình thức nhật ký chung)"
                    },
                    new { key="1234", reportid = "S06-DN", name = "Bản cân đối phát sinh"
                    },
                    new { key="1234", reportid = "S07-DN", name = "Sổ quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S07a-DN", name = "Sổ kế toán chi tiết quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S08-DN", name = "Sổ tiền gửi ngân hàng"
                    },
                    new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    },
                    new { key="1234", reportid = "S21-DN", name = "Sổ tài sản cố định"
                    },
                    new { key="1234", reportid = "S22-DN", name = "Sổ theo dõi TSCĐ và công cụ, dụng cụ tại nơi sử dụng"
                    },
                    new { key="1234", reportid = "S23-DN", name = "Thẻ tài sản cố định"
                    },
                    new { key="1234", reportid = "S31-DN", name = "Sổ chi tiết thanh toán với người mua(người bán)"
                    },
                    new { key="1234", reportid = "S32-DN", name = "Sổ chi tiết thanh toán với người mua(người bán) bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S33-DN", name = "Sổ theo dõi thanh toán bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S34-DN", name = "Sổ chi tiết tiền vay"
                    },
                    new { key="1234", reportid = "S35-DN", name = "Sổ chi tiết bán hàng"
                    },
                    new { key="1234", reportid = "S36-DN", name = "Sổ chi phí sản xuất, kinh doanh"
                    },
                    new { key="1234", reportid = "S37-DN", name = "Thẻ tính giá thành sản phẩm, dịch vụ"
                    },
                    new { key="1234", reportid = "S38-DN", name = "Sổ chi tiết các tài khoản"
                    },
                    new { key="1234", reportid = "S41-DN", name = "Sổ kế toán chi tiết theo dõi các khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S42-DN", name = "Sổ theo dõi phân bổ các khoản chênh lệch phát sinh khi mua khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S43-DN", name = "Sổ chi tiết phát hành cổ phiếu"
                    },
                    new { key="1234", reportid = "S44-DN", name = "Sổ chi tiết cổ phiếu quỹ"
                    },
                    new { key="1234", reportid = "S45-DN", name = "Sổ chi tiết đầu tư chứng khoán"
                    },
                    new { key="1234", reportid = "S51-DN", name = "Sổ theo dõi chi tiết nguồn vốn kinh doanh"
                    },
                    new { key="1234", reportid = "S52-DN", name = "Sổ chi phí đầu tư xây dựng"
                    },
                    new { key="1234", reportid = "S61-DN", name = "Sổ theo dõi thuế GTGT"
                    },
                    new { key="1234", reportid = "S62-DN", name = "Sổ chi tiết thuế GTGT được hoàn lại"
                    },
                    new { key="1234", reportid = "S63-DN", name = "Sổ chi tiết thuế GTGT được miễn giảm"
                    },
                    new { key="1234", reportid = "B01-DN", name = "BẢNG CÂN ĐỐI KẾ TOÁN"
                    },
                    new { key="1234", reportid = "B02-DN", name = "Báo cáo hoạt động kinh doanh"
                    },
                    new { key="1234", reportid = "B03-DN-GT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp gián tiếp"
                    },
                    new { key="1234", reportid = "B03-DN-TT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp trực tiếp"
                    },
                };
        }

        public static ArrayList getDiaryLedgerDS()
        {
            return new ArrayList { 
                    new { key="1234", reportid = "S01-DN", name = "Sổ Nhật ký - Sổ Cái"
                    },
                    new { key="1234", reportid = "S07-DN", name = "Sổ quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S07a-DN", name = "Sổ kế toán chi tiết quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S08-DN", name = "Sổ tiền gửi ngân hàng"
                    },
                    new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    },
                    new { key="1234", reportid = "S21-DN", name = "Sổ tài sản cố định"
                    },
                    new { key="1234", reportid = "S22-DN", name = "Sổ theo dõi TSCĐ và công cụ, dụng cụ tại nơi sử dụng"
                    },
                    new { key="1234", reportid = "S23-DN", name = "Thẻ tài sản cố định"
                    },
                    new { key="1234", reportid = "S31-DN", name = "Sổ chi tiết thanh toán với người mua(người bán)"
                    },
                    new { key="1234", reportid = "S32-DN", name = "Sổ chi tiết thanh toán với người mua(người bán) bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S33-DN", name = "Sổ theo dõi thanh toán bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S34-DN", name = "Sổ chi tiết tiền vay"
                    },
                    new { key="1234", reportid = "S35-DN", name = "Sổ chi tiết bán hàng"
                    },
                    new { key="1234", reportid = "S36-DN", name = "Sổ chi phí sản xuất, kinh doanh"
                    },
                    new { key="1234", reportid = "S37-DN", name = "Thẻ tính giá thành sản phẩm, dịch vụ"
                    },
                    new { key="1234", reportid = "S38-DN", name = "Sổ chi tiết các tài khoản"
                    },
                    new { key="1234", reportid = "S41-DN", name = "Sổ kế toán chi tiết theo dõi các khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S42-DN", name = "Sổ theo dõi phân bổ các khoản chênh lệch phát sinh khi mua khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S43-DN", name = "Sổ chi tiết phát hành cổ phiếu"
                    },
                    new { key="1234", reportid = "S44-DN", name = "Sổ chi tiết cổ phiếu quỹ"
                    },
                    new { key="1234", reportid = "S45-DN", name = "Sổ chi tiết đầu tư chứng khoán"
                    },
                    new { key="1234", reportid = "S51-DN", name = "Sổ theo dõi chi tiết nguồn vốn kinh doanh"
                    },
                    new { key="1234", reportid = "S52-DN", name = "Sổ chi phí đầu tư xây dựng"
                    },
                    new { key="1234", reportid = "S61-DN", name = "Sổ theo dõi thuế GTGT"
                    },
                    new { key="1234", reportid = "S62-DN", name = "Sổ chi tiết thuế GTGT được hoàn lại"
                    },
                    new { key="1234", reportid = "S63-DN", name = "Sổ chi tiết thuế GTGT được miễn giảm"
                    },
                    new { key="1234", reportid = "B01-DN", name = "BẢNG CÂN ĐỐI KẾ TOÁN"
                    },
                    new { key="1234", reportid = "B02-DN", name = "Báo cáo hoạt động kinh doanh"
                    },
                    new { key="1234", reportid = "B03-DN-GT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp gián tiếp"
                    },
                    new { key="1234", reportid = "B03-DN-TT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp trực tiếp"
                    },

                };
        }

        public static ArrayList getVoucherLedgerDS()
        {
            return new ArrayList { 
                    new { key="1234", reportid = "S02a-DN", name = "Chứng từ ghi sổ"
                    },
                    new { key="1234", reportid = "S02b-DN", name = "Sổ đăng ký Chứng từ ghi sổ"
                    },
                    new { key="1234", reportid = "S02c1-DN", name = "Sổ Cái(Dùng cho hình thức chứng từ ghi sổ)"
                    },
                    new { key="1234", reportid = "S02c2-DN", name = "Sổ Cái(Dùng cho hình thức chứng từ ghi sổ)"
                    },
                    new { key="1234", reportid = "S06-DN", name = "Bản cân đối phát sinh"
                    },
                    new { key="1234", reportid = "S07-DN", name = "Sổ quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S07a-DN", name = "Sổ kế toán chi tiết quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S08-DN", name = "Sổ tiền gửi ngân hàng"
                    },
                    new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    },
                    new { key="1234", reportid = "S21-DN", name = "Sổ tài sản cố định"
                    },
                    new { key="1234", reportid = "S22-DN", name = "Sổ theo dõi TSCĐ và công cụ, dụng cụ tại nơi sử dụng"
                    },
                    new { key="1234", reportid = "S23-DN", name = "Thẻ tài sản cố định"
                    },
                    new { key="1234", reportid = "S31-DN", name = "Sổ chi tiết thanh toán với người mua(người bán)"
                    },
                    new { key="1234", reportid = "S32-DN", name = "Sổ chi tiết thanh toán với người mua(người bán) bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S33-DN", name = "Sổ theo dõi thanh toán bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S34-DN", name = "Sổ chi tiết tiền vay"
                    },
                    new { key="1234", reportid = "S35-DN", name = "Sổ chi tiết bán hàng"
                    },
                    new { key="1234", reportid = "S36-DN", name = "Sổ chi phí sản xuất, kinh doanh"
                    },
                    new { key="1234", reportid = "S37-DN", name = "Thẻ tính giá thành sản phẩm, dịch vụ"
                    },
                    new { key="1234", reportid = "S38-DN", name = "Sổ chi tiết các tài khoản"
                    },
                    new { key="1234", reportid = "S41-DN", name = "Sổ kế toán chi tiết theo dõi các khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S42-DN", name = "Sổ theo dõi phân bổ các khoản chênh lệch phát sinh khi mua khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S43-DN", name = "Sổ chi tiết phát hành cổ phiếu"
                    },
                    new { key="1234", reportid = "S44-DN", name = "Sổ chi tiết cổ phiếu quỹ"
                    },
                    new { key="1234", reportid = "S45-DN", name = "Sổ chi tiết đầu tư chứng khoán"
                    },
                    new { key="1234", reportid = "S51-DN", name = "Sổ theo dõi chi tiết nguồn vốn kinh doanh"
                    },
                    new { key="1234", reportid = "S52-DN", name = "Sổ chi phí đầu tư xây dựng"
                    },
                    new { key="1234", reportid = "S61-DN", name = "Sổ theo dõi thuế GTGT"
                    },
                    new { key="1234", reportid = "S62-DN", name = "Sổ chi tiết thuế GTGT được hoàn lại"
                    },
                    new { key="1234", reportid = "S63-DN", name = "Sổ chi tiết thuế GTGT được miễn giảm"
                    },
                    new { key="1234", reportid = "B01-DN", name = "BẢNG CÂN ĐỐI KẾ TOÁN"
                    },
                    new { key="1234", reportid = "B02-DN", name = "Báo cáo hoạt động kinh doanh"
                    },
                    new { key="1234", reportid = "B03-DN-GT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp gián tiếp"
                    },
                    new { key="1234", reportid = "B03-DN-TT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp trực tiếp"
                    },
                };
        }
        public static ArrayList getTaxReportDS()
        {
            //2013-08-29 ERP-398 Khoa.Truong INS START
            return new ArrayList { 
                    //new { key="1234", reportid = "S61-DN", name = "Sổ theo dõi thuế GTGT"
                    //},
                    //new { key="1234", reportid = "S62-DN", name = "Sổ chi tiết thuế GTGT được hoàn lại"
                    //},
                    //new { key="1234", reportid = "S63-DN", name = "Sổ chi tiết thuế GTGT được miễn giảm"
                    //},    
                    //new { key="1234", reportid = "02-KK-TNCN", name = "Tờ khai khấu trừ thuế thu nhập cá nhân"
                    //},
                    //new { key="1234", reportid = "03-1A-TNDN", name = "Phụ lục 03-1A Kết quả hoạt động sản xuất kinh doanh"
                    //},
                    //new { key="1234", reportid = "03-1B-TNDN", name = "Phụ lục 03-1B Kết quả hoạt động sản xuất kinh doanh"
                    //},
                    //new { key="1234", reportid = "03-1C-TNDN", name = "Phụ lục 03-1C Kết quả hoạt động sản xuất kinh doanh"
                    //},
                    //new { key="1234", reportid = "03-2A-TNDN", name = "Phụ lục 03-2A Chuyển lỗ từ hoạt động SXKD"
                    //},
                    //new { key="1234", reportid = "03-2B-TNDN", name = "Phụ lục 03-2B Chuyển lỗ từ quyền sử dụng đất và quyền thuê đất"
                    //},
                    //new { key="1234", reportid = "03-3A-TNDN", name = "Phụ lục 03-3A Thuế thu nhập doanh nghiệp được ưu đãi"
                    //},
                    //new { key="1234", reportid = "03-3B-TNDN", name = "Phụ lục 03-3B Thuế thu nhập doanh nghiệp được ưu đãi"
                    //},
                    //new { key="1234", reportid = "03-3C-TNDN", name = "Phụ lục 03-3C Thuế thu nhập doanh nghiệp được ưu đãi"
                    //},
                    //new { key="1234", reportid = "03-4-TNDN", name = "Phụ lục 03-4 Thuế thu nhập doanh nghiệp đã nộp ở nước ngoài được trừ trong kỳ tính thuế"
                    //},
                    //new { key="1234", reportid = "03-5-TNDN", name = "Phụ lục 03-5 Thuế TNDN đối với hoạt động chuyển nhượng bất động sản"
                    //},
                    //new { key="1234", reportid = "03-6-TNDN", name = "Phụ lục 03-6 Báo cáo trích, sử dụng quỹ khoa học và công nghệ"
                    //},
                    //new { key="1234", reportid = "03-TNDN", name = "Tờ khai quyết toán thuế thu nhập doanh nghiệp (mẫu số 03/TNDN)"
                    //},
                    //new { key="1234", reportid = "05-KK-TNCN", name = "Tờ khai quyết toán thuế thu nhập cá nhân"
                    //},
                    //new { key="1234", reportid = "05A-BK-TNCN", name = "Bảng kê thu nhập chịu thuế và thuế thu nhập cá nhân đã khấu trừ đối với thu nhập từ tiền lương, tiền công của cá nhân cư trú không ký hợp đồng lao động hoặc có hợp đồng lao động dưới 3 tháng và cá nhân không cư trú"
                    //},
                    //new { key="1234", reportid = "05B-BK-TNCN", name = "Bảng kê thu nhập chịu thuế và thuế thu nhập cá nhân đã khấu trừ đối với thu nhập từ tiền lương, tiền công của cá nhân cư trú không ký hợp đồng lao động hoặc có hợp đồng lao động dưới 3 tháng và cá nhân không cư trú"
                    //},
                    //new { key="1234", reportid = "09-KK-TNCN", name = "Tờ khai quyết toán thuế thu nhập cá nhân"
                    //},
                    //new { key="1234", reportid = "09A-PL-TNCN", name = "Phụ lục thu nhập từ tiền lương, tiền công"
                    //},
                    //new { key="1234", reportid = "09B-PL-TNCN", name = "Phụ lục thu nhập từ kinh doanh"
                    //},
                    //new { key="1234", reportid = "09C-PL-TNCN", name = "Phụ lục giảm trừ gia cảnh cho người phụ thuộc"
                    //},
                    //new { key="1234", reportid = "05-PL-TNDN", name = "Phụ lục tính nộp thuế TNDN của doanh nghiệp có các cơ sở sản xuất hạch toán phụ thuộc"
                    //},
                    //new { key="1234", reportid = "S11-DN", name = "Số tổng hợp nhập xuất tồn"
                    //},
                    //new { key="1234", reportid = "01A-TNDN", name = "Tờ khai thuế thu nhập doanh nghiệp tạm tính"
                    //},
                    new { key="1234", reportid = "01-1/GTGT", name = "Bảng kê hoá đơn, chứng từ hàng hoá, dịch vụ bán ra"
                    },
                    new { key="1234", reportid = "01-2/GTGT", name = "Bảng kê hoá đơn, chứng từ hàng hoá, dịch vụ mua vào"
                    },
                    //new { key="1234", reportid = "BC26/AC", name = "Báo cáo tình hình sử dụng hóa đơn (BC26/AC)"
                    //},            
                    //new { key="1234", reportid = "GCN-01-QLT", name = "Phụ lục 1- GNC/CC Thông tin về giao dịch liên kết"
                    //},            
                    
                };
            //2013-08-29 ERP-398 Khoa.Truong INS END
        }
        public static ArrayList getDiaryVoucherDS()
        {
            return new ArrayList { 
                    //new { key="1234", reportid = "S04-DN", name = "Nhật ký - Chứng từ, các loại nhật ký - Chứng từ, Bảng kê"
                    //},
                    //new { key="1234", reportid = "S04a-DN", name = "Nhật ký - Chứng từ từ số 1 đến số 10"
                    //},
                    new { key="1234", reportid = "S04a1-DN", name = "Nhật ký chứng từ số 1"
                    },
                    new { key="1234", reportid = "S04a2-DN", name = "Nhật ký chứng từ số 2"
                    },
                    new { key="1234", reportid = "S04a3-DN", name = "Nhật ký chứng từ số 3"
                    },
                    new { key="1234", reportid = "S04a4-DN", name = "Nhật ký chứng từ số 4"
                    },
                    new { key="1234", reportid = "S04a5-DN", name = "Nhật ký chứng từ số 5"
                    },
                    new { key="1234", reportid = "S04a6-DN", name = "Nhật ký chứng từ số 6"
                    },
                    new { key="1234", reportid = "S04a7-DN", name = "Nhật ký chứng từ số 7"
                    },
                    new { key="1234", reportid = "S04a8-DN", name = "Nhật ký chứng từ số 8"
                    },
                    new { key="1234", reportid = "S04a9-DN", name = "Nhật ký chứng từ số 9"
                    },
                    new { key="1234", reportid = "S04a10-DN", name = "Nhật ký chứng từ số 10"
                    },
                    new { key="1234", reportid = "S04b-DN", name = "Bảng kê từ số 1 đến số 11"
                    },
                    new { key="1234", reportid = "S04b1-DN", name = "Bảng kê số 1"
                    },
                    new { key="1234", reportid = "S04b2-DN", name = "Bảng kê số 2"
                    },
                    new { key="1234", reportid = "S04b3-DN", name = "Bảng kê số 3"
                    },
                    new { key="1234", reportid = "S04b4-DN", name = "Bảng kê số 4"
                    },
                    new { key="1234", reportid = "S04b5-DN", name = "Bảng kê số 5"
                    },
                    new { key="1234", reportid = "S04b6-DN", name = "Bảng kê số 6"
                    },
                    new { key="1234", reportid = "S04b8-DN", name = "Bảng kê số 8"
                    },
                    new { key="1234", reportid = "S04b9-DN", name = "Bảng kê số 9"
                    },
                    new { key="1234", reportid = "S04b10-DN", name = "Bảng kê số 10"
                    },
                    new { key="1234", reportid = "S04b11-DN", name="Bảng kê số 11"
                    },
                    new { key="1234", reportid = "S05-DN", name = "Sổ Cái(Dùng cho hình thức Nhật ký - Chứng từ)"
                    },
                    new { key="1234", reportid = "S06-DN", name = "Bảng cân đối số phát sinh"
                    },
                    new { key="1234", reportid = "S07-DN", name = "Sổ quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S07a-DN", name = "Sổ kế toán chi tiết quỹ tiền mặt"
                    },
                    new { key="1234", reportid = "S08-DN", name = "Sổ tiền gửi ngân hàng"
                    },
                    new { key="1234", reportid = "S10-DN", name = "Sổ chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S11-DN", name = "Bảng tổng hợp chi tiết vật liệu, dụng cụ, sản phẩm, hàng hóa"
                    },
                    new { key="1234", reportid = "S12-DN", name = "Thẻ kho(Sổ kho)"
                    },
                    new { key="1234", reportid = "S21-DN", name = "Sổ tài sản cố định"
                    },
                    new { key="1234", reportid = "S22-DN", name = "Sổ theo dõi TSCĐ và công cụ, dụng cụ tại nơi sử dụng"
                    },
                    new { key="1234", reportid = "S23-DN", name = "Thẻ tài sản cố định"
                    },
                    new { key="1234", reportid = "S31-DN", name = "Sổ chi tiết thanh toán với người mua(người bán)"
                    },
                    new { key="1234", reportid = "S32-DN", name = "Sổ chi tiết thanh toán với người mua(người bán) bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S33-DN", name = "Sổ theo dõi thanh toán bằng ngoại tệ"
                    },
                    new { key="1234", reportid = "S34-DN", name = "Sổ chi tiết tiền vay"
                    },
                    new { key="1234", reportid = "S35-DN", name = "Sổ chi tiết bán hàng"
                    },
                    new { key="1234", reportid = "S36-DN", name = "Sổ chi phí sản xuất, kinh doanh"
                    },
                    new { key="1234", reportid = "S37-DN", name = "Thẻ tính giá thành sản phẩm, dịch vụ"
                    },
                    new { key="1234", reportid = "S38-DN", name = "Sổ chi tiết các tài khoản"
                    },
                    new { key="1234", reportid = "S41-DN", name = "Sổ kế toán chi tiết theo dõi các khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S42-DN", name = "Sổ theo dõi phân bổ các khoản chênh lệch phát sinh khi mua khoản đầu tư vào công ty liên kết"
                    },
                    new { key="1234", reportid = "S43-DN", name = "Sổ chi tiết phát hành cổ phiếu"
                    },
                    new { key="1234", reportid = "S44-DN", name = "Sổ chi tiết cổ phiếu quỹ"
                    },
                    new { key="1234", reportid = "S45-DN", name = "Sổ chi tiết đầu tư chứng khoán"
                    },
                    new { key="1234", reportid = "S51-DN", name = "Sổ theo dõi chi tiết nguồn vốn kinh doanh"
                    },
                    new { key="1234", reportid = "S52-DN", name = "Sổ chi phí đầu tư xây dựng"
                    },
                    new { key="1234", reportid = "S61-DN", name = "Sổ theo dõi thuế GTGT"
                    },
                    new { key="1234", reportid = "S62-DN", name = "Sổ chi tiết thuế GTGT được hoàn lại"
                    },
                    new { key="1234", reportid = "S63-DN", name = "Sổ chi tiết thuế GTGT được miễn giảm"
                    },
                    new { key="1234", reportid = "B01-DN", name = "Bảng cân đối kế toán"
                    },
                    new { key="1234", reportid = "B02-DN", name = "Báo cáo hoạt động kinh doanh"
                    },
                    new { key="1234", reportid = "B03-DN", name = "Bản thuyết minh báo cáo tài chính"
                    },
                    new { key="1234", reportid = "B03-DN-GT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp gián tiếp"
                    },
                    new { key="1234", reportid = "B03-DN-TT", name = "Báo cáo lưu chuyển tiền tệ theo phương pháp trực tiếp"
                    },
                };
        }

        public static XtraReport getReportInstance(string reportName)
        {
            switch (reportName)
            {
                case "01-VT":
                    return new _01_VT();
                case "02-VT":
                    return new _02_VT();
                case "03-VT":
                    return new _03_VT();
                case "04-VT":
                    return new _04_VT();
                case "05-VT":
                    return new _05_VT();
                case "06-VT":
                    return new Mau06_VT();
                case "07-VT":
                    return new Mau07_VT();
                case "01-TT":
                    return new _01_TT();
                case "02-TT":
                    return new _02_TT();
                case "03-TT":
                    return new _03_TT();
                case "04-TT":
                    return new _04_TT();
                case "05-TT":
                    return new _05_TT();
                case "06-TT":
                    return new _06_TT();
                case "07-TT":
                    return new _07_TT();
                case "08a-TT":
                    return new _08a_TT();
                case "08b-TT":
                    return new _08b_TT();
                case "09-TT":
                    return new _09_TT();
                case "S01-DN":
                    return new MauS01_DN();
                case "S02b-DN":
                    return new S02b_DN();
                case "S02c1-DN":
                    return new S02c1_DN();
                case "S02c2-DN":
                    return new S02c2_DN2();
                case "S03a-DN":
                    return new S03a_DN();
                case "S03a2-DN":
                    return new S03a2_DN();
                case "S03a3-DN":
                    return new S03a3_DN();
                case "S03a4-DN":
                    return new S03a4_DN();
                case "S03b-DN":
                    return new S03b_DN();
                case "S06-DN":
                    return new S06_DN();
                case "S07-DN":
                    return new S07_DN();
                case "S07a-DN":
                    return new S07a_DN();
                case "S08-DN":
                    return new S08_DN();
                case "S10-DN":
                    return new S10_DN();
                case "S11-DN":
                    return new S11_DN();
                case "S11-DN-SNL":
                    return new MauS11_DN();
                case "S12-DN":
                    return new S12_DN();
                case "S21-DN":
                    return new S21_DN();
                case "S22-DN":
                    return new S22_DN();
                case "S23-DN":
                    return new S23_DN();
                case "S31-DN":
                    return new S31_DN();
                case "S32-DN":
                    return new S32_DN();
                case "S33-DN":
                    return new S33_DN();
                case "S34-DN":
                    return new S34_DN();
                case "S35-DN":
                    return new S35_DN();
                case "S36-DN":
                    return new S36_DN();
                case "S37-DN":
                    return new S37_DN();
                case "S38-DN":
                    return new S38_DN();
                case "S41-DN":
                    return new S41_DN();
                case "S42-DN":
                    return new S42_DN();
                case "S43-DN":
                    return new S43_DN();
                case "S44-DN":
                    return new MauSoS44_DN();
                case "S45-DN":
                    return new MauSoS45_DN();
                case "S51-DN":
                    return new MauSoS51_DN();
                case "S52-DN":
                    return new MauSoS52_DN();
                case "S61-DN":
                    return new MauSoS61_DN();
                case "S62-DN":
                    return new S62_DN();
                case "S63-DN":
                    return new S62_DN_MG();
                case "B01-DN":
                    return new B01_DN();
                case "B02-DN":
                    return new B02_DN();
                case "B03-DN-GT":
                    return new B03_DN_gt();
                case "BO3-DN-TT":
                    return new B03_DN_tt();
                case "01GTKT3-001":
                    return new _01GTKT3_001();
                case "01GTKT3-Direct":
                    return new _01GTKT3_Direct();
                case "02GTTT3":
                    return new WebModule.GUI.Sales.Report.Rpt_02GTTT3();
                //2013-08-29 ERP-398 Khoa.Truong INS START
                case "02-KK-TNCN":
                    return new Mau02_KK_TNCN();
                case "03-1A-TNDN":
                    return new Mau03_1A_TNDN();
                case "03-1B-TNDN":
                    return new Mau03_1B_TNDN();
                case "03-1C-TNDN":
                    return new Mau03_1B_TNDN();
                case "03-2A-TNDN":
                    return new Mau03_2A_TNDN();
                case "03-2B-TNDN":
                    return new Mau03_2B_TNDN();
                case "03-3A-TNDN":
                    return new Mau03_3A_TNDN();
                case "03-3B-TNDN":
                    return new Mau03_3B_TNDN();
                case "03-3C-TNDN":
                    return new Mau03_3C_TNDN();
                case "03-4-TNDN":
                    return new Mau03_4_TNDN();
                case "03-5-TNDN":
                    return new Mau03_5_TNDN();
                case "03-6-TNDN":
                    return new Mau03_6_TNDN();
                case "03-TNDN":
                    return new Mau03_TNDN();
                case "05-KK-TNCN":
                    return new Mau05_KK_TNCN();
                case "05A-BK-TNCN":
                    return new Mau05A_BK_TNCN();
                case "05B-BK-TNCN":
                    return new Mau05B_BK_TNCN();
                case "09-KK-TNCN":
                    return new Mau09_KK_TNCN();
                case "09A-PL-TNCN":
                    return new Mau09A_PL_TNCN();
                case "09B-PL-TNCN":
                    return new Mau09B_PL_TNCN();
                case "09C-PL-TNCN":
                    return new Mau09C_PL_TNCN();
                case "05-PL-TNDN":
                    return new MauPL_05_TNDN();
                case "01A-TNDN":
                    return new MauSo01A_TNDN();
                case "01-1/GTGT":
                    return new BangKeBanRa();
                case "01-2/GTGT":
                    return new BangKeMuaVao();
                case "BC26/AC":
                    return new BC26_AC();
                case "GCN-01-QLT":
                    return new MauGCN_01_QLT();
                case "BC-THDSX":
                    return new TongHopDoanhSoXuat();
                case "BC-LCHH":
                    return new BaoCaoLuanChuyenHangHoa();
                case "S04a1-DN":
                    return new S04a1_DN();
                case "S04a2-DN":
                    return new S04a2_DN();
                case "S04a3-DN":
                    return new S04a3_DN();
                case "S04a4-DN":
                    return new S04a4_DN();
                case "S04a5-DN":
                    return new S04a5_DN();
                case "S04a6-DN":
                    return new S04a6_DN();
                case "S04a7-DN":
                    return new S04a7_DN();
                case "S04a8-DN":
                    return new S04a8_DN();
                case "S04a9-DN":
                    return new S04a9_DN();
                case "S04a10-DN":
                    return new S04a10_DN();
                case "S04b1-DN":
                    return new S04b1_DN();
                case "S04b2-DN":
                    return new S04b2_DN();
                case "S04b3-DN":
                    return new S04b3_DN();
                case "S04b4-DN":
                    return new S04b4_DN();
                case "S04b5-DN":
                    return new S04b5_DN();
                case "S04b6-DN":
                    return new S04b6_DN();
                case "S04b11-DN":
                    return new S04b11_DN();
                //2013-08-29 ERP-398 Khoa.Truong INS END
                default:
                    return null;

            }
        }
    }
}