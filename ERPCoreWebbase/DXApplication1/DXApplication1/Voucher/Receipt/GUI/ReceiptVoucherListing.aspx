<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="ReceiptVoucherListing.aspx.cs"
    Inherits="WebModule.Voucher.Receipt.GUI.ReceiptVoucherListing" %>

<%@ Register Src="ReceiptVoucherEditingForm.ascx" TagName="ReceiptVoucherEditingForm"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(ReceiptVoucherEditingForm).on(
                ReceiptVoucherEditingForm.events.eClosing,
                function (evt) {
                    gridviewReceiptVoucher.Refresh();
                }
            );
        });

        //gridview custom button click event handler
        function gridviewReceiptVoucher_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add                      
                case "gridviewReceiptVoucher_Add":
                    ReceiptVoucherEditingForm.Show();
                    break;
                //When custom button ID is Edit    
                case "gridviewReceiptVoucher_Edit":
                    var recordId = s.GetRowKey(e.visibleIndex);
                    ReceiptVoucherEditingForm.Show(recordId);
                    break;
                //When custom button ID is Delete               
                case "gridviewReceiptVoucher_Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        gridviewReceiptVoucher.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        //EndCallback Handler of grdDataManufacturer gridview
        function gridviewReceiptVoucher_EndCallback(s, e) {
            //Refresh the gridview when data is deleted
            if (s.cpEvent == 'deleted') {
                gridviewReceiptVoucher.Refresh();
            }
            delete s.cpEvent;
        }

        var moreInfoContentArgs;
        function ShowMoreInfo(atElement, headerText, rowKey, infoFieldName) {
            cpMoreInfoContent.SetContentHtml("");
            popMoreInfo.SetHeaderText(headerText);
            popMoreInfo.ShowAtElement(atElement);
            moreInfoContentArgs = rowKey + '|' + infoFieldName;
        }

        function popMoreInfo_Shown(s, e) {
            cpMoreInfoContent.PerformCallback(moreInfoContentArgs);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Segoe UI"
            Font-Size="Large" Text="Phiếu thu">
        </dx:ASPxLabel>
    </div>
    <div>
        <dx:ASPxPopupControl ID="popMoreInfo" runat="server" ClientInstanceName="popMoreInfo"
            RenderMode="Lightweight" ShowLoadingPanel="False">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <dx:ASPxCallbackPanel ID="cpMoreInfoContent" runat="server" ClientInstanceName="cpMoreInfoContent"
                        Width="100%" OnCallback="cpInfoContent_Callback">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <dx:ASPxLabel ID="lblMoreInfoContent" runat="server" Text="">
                                </dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popMoreInfo_Shown" />
        </dx:ASPxPopupControl>
        <dx:XpoDataSource ID="dsReceiptVoucher" runat="server" TypeName="NAS.DAL.Vouches.ReceiptVouches"
            Criteria="[RowStatus] > 0">
        </dx:XpoDataSource>
        <dx:ASPxGridView ID="gridviewReceiptVoucher" KeyboardSupport="True" runat="server"
            AutoGenerateColumns="False" ClientInstanceName="gridviewReceiptVoucher" KeyFieldName="VouchesId"
            Width="100%" DataSourceID="dsReceiptVoucher" OnCustomCallback="gridviewReceiptVoucher_CustomCallback"
            OnCustomColumnDisplayText="gridviewReceiptVoucher_CustomColumnDisplayText">
            <ClientSideEvents CustomButtonClick="gridviewReceiptVoucher_CustomButtonClick" EndCallback="gridviewReceiptVoucher_EndCallback" />
<ClientSideEvents CustomButtonClick="gridviewReceiptVoucher_CustomButtonClick" 
                EndCallback="gridviewReceiptVoucher_EndCallback"></ClientSideEvents>
            <Columns>
                <dx:GridViewDataTextColumn Caption="Số phiếu thu" FieldName="Code" VisibleIndex="0"
                    Width="10%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Phân loại" FieldName="VouchesTypeId.Description"
                    VisibleIndex="1" Width="10%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Số tiền" FieldName="SumOfDebit" VisibleIndex="2"
                    Width="18%">
                    <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                    </PropertiesTextEdit>
                    <CellStyle HorizontalAlign="Right">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="Ngày thu" FieldName="IssuedDate" VisibleIndex="3"
                    Width="12%">
                    <PropertiesDateEdit DisplayFormatString="{0:d}">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Lý do thu" FieldName="Description" VisibleIndex="4"
                    Width="10%">
                    <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="ShowMoreInfo(this, 'Lý do thu' ,'<%# Container.KeyValue %>', 'Description')">
                            <dx:ASPxImage ID="ASPxImage3" runat="server" ShowLoadingImage="true" SpriteCssClass="Sprite_Info"
                                ToolTip="Xem chi tiết">
                            </dx:ASPxImage>
                        </a>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Đơn vị trả tiền" FieldName="SourceOrganizationId.Name"
                    VisibleIndex="5" Width="18%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Người trả tiền" FieldName="Payer" VisibleIndex="6"
                    Width="10%">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="Address" VisibleIndex="7"
                    Width="10%" Visible="False">
                    <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="ShowMoreInfo(this, 'Địa chỉ' ,'<%# Container.KeyValue %>', 'Address')">
                            <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" SpriteCssClass="Sprite_Info"
                                ToolTip="Xem chi tiết">
                            </dx:ASPxImage>
                        </a>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="RowStatus" VisibleIndex="8"
                    Width="10%">
                    <cellstyle horizontalalign="Center">
                    </cellstyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="9"
                    Width="12%">
                    <ClearFilterButton Visible="True">
                        <Image ToolTip="Hủy">
                            <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                        </Image>
                    </ClearFilterButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton Text="Edit" ID="gridviewReceiptVoucher_Edit">
                            <Image ToolTip="Sửa">
                                <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton Text="New" ID="gridviewReceiptVoucher_Add">
                            <Image ToolTip="Thêm">
                                <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton Text="Delete" ID="gridviewReceiptVoucher_Delete">
                            <Image ToolTip="Xóa">
                                <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                            </Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <SettingsBehavior ColumnResizeMode="NextColumn" />

<SettingsBehavior ColumnResizeMode="NextColumn"></SettingsBehavior>

            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
            </Settings>
            <Templates>
                <EmptyDataRow>
                    <div style="margin: 0 auto">
                        <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                            <ClientSideEvents Click="function(s, e) {
                    ReceiptVoucherEditingForm.Show();
}" />
                        </dx:ASPxImage>
                        <br />
                    </div>
                </EmptyDataRow>
            </Templates>
            <BorderLeft BorderWidth="0px" />
            <BorderRight BorderWidth="0px" />
            <Styles>
                <Header Font-Bold="True" HorizontalAlign="Center" wrap="True">
                </Header>
                <cell wrap="True">
                </cell>
                <CommandColumn Spacing="4px">
                </CommandColumn>
            </Styles>
            <BorderLeft BorderWidth="0px"></BorderLeft>
            <BorderRight BorderWidth="0px"></BorderRight>
        </dx:ASPxGridView>
        <uc1:ReceiptVoucherEditingForm ID="ReceiptVoucherEditingForm1" runat="server" />
    </div>
</asp:Content>
