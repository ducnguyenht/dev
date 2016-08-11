<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ReceiptVoucher.aspx.cs" Inherits="WebModule.PayReceiving.ReceiptVoucher" %>

<%@ Register Src="UserControl/ReceiptVoucherEdit.ascx" TagName="ReceiptVoucherEdit"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            /////START: Adjust layout when window resized
            ERPCore.WindowResize_Adjust = function (s, e) {

            };
            /////END: Adjust layout when window resized

            ///START: Bind events to ManufacturerEditForm
            //Raise when saved
            ReceiptVoucherEditForm.BindSavedEvent(function (event, args) {
                if (args.isSuccess == true) {
                    grdReceiptVouches.Refresh();
                }
            });

            //Raise when popup shown
            ReceiptVoucherEditForm.BindShownEvent(function (event) {
                ldpn_grdReceiptVouches.Hide();
            });

            //Raise when popup shown
            ReceiptVoucherEditForm.BindClosingEvent(function (event) {
                grdReceiptVouches.Refresh();
                grdReceiptVouches.GetMainElement().focus();
            });
            ///END: Bind events to ManufacturerEditForm
        });

        //gridview custom button click event handler
        function grdReceiptVouches_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                //When custom button ID is Add                 
                case "grdReceiptVouches_Add":
                    //Show loading panel for preventing multi-click
                    ldpn_grdReceiptVouches.Show();
                    var headerText = 'Thông Tin Phiếu Thu - Thêm Mới';
                    ReceiptVoucherEditForm.Show(headerText);
                    break;
                //When custom button ID is Edit                 
                case "grdReceiptVouches_Edit":
                    //Show loading panel for preventing multi-click
                    ldpn_grdReceiptVouches.Show();
                    //Get data of ManufacturerId, Code column in clicked row
                    s.GetRowValues(e.visibleIndex, 'VouchesId;Code', grdReceiptVouches_OnGetRowValues);
                    break;
                //When custom button ID is Delete          
                case "grdReceiptVouches_Delete":
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'delete';
                        args += '|' + s.GetRowKey(e.visibleIndex);
                        grdReceiptVouches.PerformCallback(args);
                    }
                    break;
                default:
                    break;
            }
        }

        //GetRowValues callback of grdDataManufacturer gridview
        function grdReceiptVouches_OnGetRowValues(values) {
            var recordId = values[0];
            var headerText = 'Thông Tin Phiếu Thu - ' + values[1];
            ReceiptVoucherEditForm.Show(headerText, recordId);
        }

        //EndCallback Handler of grdDataManufacturer gridview
        function grdReceiptVouches_EndCallback(s, e) {
            //Refresh the gridview when data is deleted
            if (s.cpEvent == 'deleted') {
                grdReceiptVouches.Refresh();
            }
            delete s.cpEvent;
        }

        function grdReceiptVouches_Init(s, e) {
            //Press F2 to show edit popup
            Utils.AttachShortcutTo(s.GetMainElement(), "F2", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                //Show loading panel for preventing multi-click
                ldpn_grdReceiptVouches.Show();
                //Get data of VouchesId, Code column in clicked row
                s.GetRowValues(focusedRowIndex, 'VouchesId;Code', grdReceiptVouches_OnGetRowValues);
            });
            //Press Insert to show insert popup
            Utils.AttachShortcutTo(s.GetMainElement(), "Insert", function () {
                //Show loading panel for preventing multi-click
                ldpn_grdReceiptVouches.Show();
                var headerText = 'Thông Tin Phiếu Thu - Thêm Mới';
                ReceiptVoucherEditForm.Show(headerText);
            });
            //Press Delete to delete record
            Utils.AttachShortcutTo(s.GetMainElement(), "Delete", function () {
                var focusedRowIndex = s.GetFocusedRowIndex();
                var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                if (confirmMessage == true) {
                    var args = 'delete';
                    args += '|' + s.GetRowKey(focusedRowIndex);
                    grdReceiptVouches.PerformCallback(args);
                }
            });

            s.GetMainElement().focus();

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
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding: 10px">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Phiếu Thu" Font-Bold="True" Font-Size="Medium">
        </dx:ASPxLabel>
    </div>
    <dx:XpoDataSource ID="dsReceiptVouches" runat="server" TypeName="NAS.DAL.Vouches.ReceiptVouches"
        Criteria="[RowStatus] > 0">
    </dx:XpoDataSource>
    <dx:ASPxLoadingPanel Modal="true" ID="ldpn_grdReceiptVouches" ClientInstanceName="ldpn_grdReceiptVouches"
        ContainerElementID="grdReceiptVouches" runat="server">
        <LoadingDivStyle BackColor="Transparent">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
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
    <dx:ASPxGridView ID="grdReceiptVouches" KeyboardSupport="True" runat="server" AutoGenerateColumns="False"
        ClientInstanceName="grdReceiptVouches" KeyFieldName="VouchesId" Width="100%"
        DataSourceID="dsReceiptVouches" OnCustomCallback="grdReceiptVouches_CustomCallback"
        OnCustomColumnDisplayText="grdReceiptVouches_CustomColumnDisplayText">
        <ClientSideEvents CustomButtonClick="grdReceiptVouches_CustomButtonClick" EndCallback="grdReceiptVouches_EndCallback"
            Init="grdReceiptVouches_Init" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Số phiếu thu" FieldName="Code" VisibleIndex="0" Width="10%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Phân loại" FieldName="VouchesTypeId.Description"
                VisibleIndex="1" Width="10%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Số tiền" FieldName="SumOfDebit" VisibleIndex="2" Width="18%">
                <PropertiesTextEdit DisplayFormatString="{0:#,###}">
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày thu" FieldName="IssuedDate" VisibleIndex="3" Width="12%">
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
            <dx:GridViewDataTextColumn Caption="Người trả tiền" FieldName="Payer" VisibleIndex="6" Width="10%">
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
            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="EntryBookingStatus" VisibleIndex="8" Width="10%">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="9" Width="12%">
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                    </Image>
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="grdReceiptVouches_Edit">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="grdReceiptVouches_Add">
                        <Image ToolTip="Thêm">
                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="grdReceiptVouches_Delete">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="NextColumn" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
        </Settings>
        <Templates>
            <EmptyDataRow>
                <div style="margin: 0 auto">
                    <dx:ASPxImage ID="ASPxImage1" runat="server" Cursor="pointer" SpriteCssClass="Sprite_New">
                        <ClientSideEvents Click="function(s, e) {
                    //Show loading panel for preventing multi-click
                    ldpn_grdReceiptVouches.Show();
                    var headerText = 'Thông Tin Phiếu Thu - Thêm Mới';
                    ReceiptVoucherEditForm.Show(headerText);
}" />
                    </dx:ASPxImage>
                    <br />
                </div>
            </EmptyDataRow>
        </Templates>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center">
            </Header>
            <CommandColumn Spacing="4px">
            </CommandColumn>
        </Styles>
        <BorderLeft BorderWidth="0px"></BorderLeft>
        <BorderRight BorderWidth="0px"></BorderRight>
    </dx:ASPxGridView>
    <uc1:ReceiptVoucherEdit ID="ReceiptVoucherEdit1" runat="server" />
</asp:Content>
