<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uCommonDetailInfo.ascx.cs" Inherits="WebModule.UserControls.uCommonDetailInfo" %>

<dx:ASPxPopupControl ID="popupCommonDetailInfo" runat="server" HeaderText="Thông Tin Chi Tiết - ABC"
    Height="600px" Modal="True" Width="900px" ScrollBars="Auto" ClientInstanceName="popupCommonDetailInfo"
    AllowDragging="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
    ShowFooter="true" ShowSizeGrip="False" AllowResize="true" 
    ShowMaximizeButton="True" 
    onwindowcallback="popupCommonDetailInfo_WindowCallback">
    <FooterContentTemplate>
        <div id="Footer" style="display: inline; width: 100%;">
            <div style="display: inline; float: left">
                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server"
                    Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Help" />
                    </Image>
                </dx:ASPxButton>
            </div>
            <div style="display: inline; float: right;">
                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False"
                    ClientInstanceName="buttonCancelDevice" Text="Đóng" Wrap="False" ToolTip="Thoát  - Ctrl + C">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Cancel" />
                    </Image>
                    <ClientSideEvents Click="function(s,e){
                        popupCommonDetailInfo.Hide();
                    }" />
                </dx:ASPxButton>
            </div>
        </div>
    </FooterContentTemplate>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ID="pcCommonDetailInfo" ClientInstanceName="pcCommonDetailInfo" runat="server" ActiveTabIndex="0" Height="100%"
                RenderMode="Classic" Width="100%">
                <TabPages>
                    <dx:TabPage Name="description" Text="Mô tả">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="formCommonDetailInfo" runat="server" Width="100%">
                                    <Items>
                                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem ShowCaption="False">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer>
                                                            <%= this.DetailContent %>
                                                            <%--<div style="font-size:13px">
                                                                <p style="font-size:13px">
                                                                    Năng suất lao động sẽ tăng do các dữ liệu đầu vào chỉ 
                                                                    phải nhập một lần cho mọi giao dịch có liên quan, đồng 
                                                                    thời các báo cáo được thực hiện với tốc độ nhanh hơn, 
                                                                    chính xác hơn. Doanh nghiệp (DN) có khả năng kiểm soát 
                                                                    tốt hơn các hạn mức về tồn kho, công nợ, chi phí, doanh thu, 
                                                                    lợi nhuận… đồng thời có khả năng tối ưu hóa các nguồn lực 
                                                                    như nguyên vật liệu, nhân công, máy móc thi công… vừa đủ để 
                                                                    sản xuất, kinh doanh.
                                                                </p>
                                                                <br />
                                                                <p>
                                                                   Các thông tin của DN được tập trung, đầy đủ, kịp 
                                                                   thời và có khả năng chia sẻ cho mọi đối tượng cần sử dụng 
                                                                   thông tin như khách hàng, đối tác, cổ đông. Khách hàng sẽ 
                                                                   hài lòng hơn do việc giao hàng sẽ được thực hiện chính xác 
                                                                   và đúng hạn. Ứng dụng ERP cũng đồng nghĩa với việc tổ chức 
                                                                   lại các hoạt động của DN theo các quy trình chuyên nghiệp, 
                                                                   phù hợp với các tiêu chuẩn quốc tế, do đó nó nâng cao chất 
                                                                   lượng sản phẩm, tiết kiệm chi phí, tăng lợi nhuận, tăng năng
                                                                   lực cạnh tranh và phát triển thương hiệu của DN.
                                                                </p>
                                                                <br />
                                                                <p>
                                                                   Ứng dụng ERP là công cụ quan trọng để DN nâng cao năng lực cạnh tranh, 
                                                                   đồng thời nó cũng giúp DN tiếp cận tốt hơn với các tiêu chuẩn quốc tế. 
                                                                   Một DN nếu ứng dụng ngay từ khi quy mô còn nhỏ sẽ có thuận lợi là dễ 
                                                                   triển khai và DN sớm đi vào nề nếp. DN nào chậm trễ ứng dụng ERP, 
                                                                   DN đó sẽ tự gây khó khăn cho mình và tạo lợi thế cho đối thủ.
                                                                </p>
                                                                <br />
                                                                <p>
                                                                   Tuy nhiên, ứng dụng ERP không phải dễ, cần hội tụ nhiều điều kiện để có 
                                                                   thể ứng dụng thành công như: nhận thức và quyết tâm cao của ban lãnh đạo 
                                                                   DN; cần xác định đúng đắn mục tiêu, phạm vi và các bước triển khai; lựa chọn giải 
                                                                   pháp phù hợp; lựa chọn đối tác triển khai đúng; phối hợp tốt với đối tác triển khai 
                                                                   trong quá trình thực hiện dự án; sẵn sàng thay đổi các quy trình bất hợp lí hiện hữu 
                                                                   trong DN (đây là việc thường xuyên gặp nhiều sự chống đối nhất); chú trọng công tác 
                                                                   đào tạo cán bộ theo các quy trình mới; chú trọng đào tạo khai thác hệ thống cho cán 
                                                                   bộ mọi cấp; có cán bộ chuyên trách tiếp thu quản trị hệ thống…
                                                                </p>
                                                            </div>--%>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <%--<dx:TabPage Text="Tài liệu đính kèm">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxFormLayout ID="wwffPdsdssd22t2" runat="server" Height="100%" Width="100%">
		                            <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server">
                                                        <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
                                                    </dx:ASPxFileManager>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
	                            </dx:ASPxFormLayout>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>--%>
                </TabPages>
                </dx:ASPxPageControl>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

