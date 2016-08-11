<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ListInputComm.aspx.cs" Inherits="WebModule.Warehouse.ListInputComm" %>
<%@ Register src="UserControl/uInputCommWarehouse.ascx" tagname="uInputCommWarehouse" tagprefix="uc1" %>
<%@ Register src="UserControl/uWarehouseBooking.ascx" tagname="uWarehouseBooking" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        /////////////////////////////////////////////////////////////////// key xu ly


        function formBooking_Init() {
            //        ASPxClientUtils.DetachEventFromElement(document, 'keydown', function (evt) {
            //            if (evt.keyCode == 27) {
            //                formBooking.Hide();
            //            }
            //        });

            //        ASPxClientUtils.AttachEventToElement(document, 'keydown', function (evt) {
            //            if (evt.keyCode == 27) {
            //                formBooking.Hide();
            //            }
            //        });
        }

        function grdData_Init(s, e) {
            RemoveKeyboardNavigationTo(grdData, 'header');
            AddKeyboardNavigationTo(grdData, 'header');
        }

        function grdBooking_Init(s, e) {
            RemoveKeyboardNavigationTo(grdBooking, 'line');
            AddKeyboardNavigationTo(grdBooking, 'line');
        }

        function grdBookingDetail_Init(s, e) {
            RemoveKeyboardNavigationTo(grdBookingDetail, 'line');
            AddKeyboardNavigationTo(grdBookingDetail, 'line');
        }


        function AddKeyboardNavigationTo(grid, type) {
            ASPxClientUtils.AttachEventToElement(grid.GetMainElement(), 'keydown', function (evt) {
                return OnDocumentKeyDown(evt, grid, type);
            });

            grid.GetMainElement().focus();
        }

        function RemoveKeyboardNavigationTo(grid, type) {
            ASPxClientUtils.DetachEventFromElement(grid.GetMainElement(), 'keydown', function (evt) {
                return OnDocumentKeyDown(evt, grid, type);
            });

            grid.GetMainElement().focus();
        }

        function OnDocumentKeyDown(evt, grid, type) {
            var currentIndex = grid.GetFocusedRowIndex();
            if (typeof (event) != "undefined" && event != null)
                evt = event;

            if (evt.ctrlKey) {
                //Enter
                if (evt.keyCode == 13) {
                    if (type == 'line') {
                        grid.UpdateEdit();
                    }
                }
            }
            else {
                if (evt.keyCode == 27) //Esc
                {
                    if (type == 'header') {
                    }
                    else {
                        grid.CancelEdit();
                    }
                }
                if (evt.keyCode == 113) //F2
                {
                    if (type == 'header') {
                        hWarehouseBookingId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                        formBooking.Show();
                    }
                    else {
                        if (grid.kbdHelper == ASPxKbdHelper.active) {
                            grid.StartEditRow(currentIndex);
                        }
                    }
                }
                if (evt.keyCode == 45) //Insert
                {
                    if (type == 'header') {
                        showFormEditing('new');
                    }
                    else {
                        if (grid.kbdHelper == ASPxKbdHelper.active) {
                            grid.AddNewRow();
                        }
                    }
                }

                if (evt.keyCode == 46) //Del
                {
                    if (grid.kbdHelper == ASPxKbdHelper.active) {
                        grid.DeleteRow(currentIndex);
                    }
                }
            }

        }


        ////////////////////////////////////////////////////////////////////// form main 

        function grdData_EndCallback(s, e) {
            if (s.cpEdit) {
                hInputCommId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                formEntryDetail.Show();
            }
            else {
                formBooking.Show();
            }
        }

        function grdData_CustomButtonClick(s, e) {
            if (e.buttonID == "btApprove") {
                hInputCommId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                hWarehouseBookingId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                formBooking.Show();
            }
        }

        /////////////////////////////////////////////////////////////// form hach toan

        function formBooking_Resize(s, e) {
            cpBooking.PerformCallback();
        }

        function formBooking_Show(s, e) {
            cpBooking.PerformCallback("view");
        }

        function buttonBooking_Click(s, e) {
            formBooking.Show();
        }

        function buttonBoookingApprove_Click(s, e) {
            cpBooking.PerformCallback('booking');
        }

        function buttonBoookingPrint_Click(s, e) {
            hInputCommPrint.Set("print", "account");
            cpInpuCommReport.PerformCallback();
        }

        function buttonBookingCancel_Click(s, e) {
            grdBooking.CancelEdit();
            formBooking.Hide();
        }

        function cpBooking_EndCallback(s, e) {
            if (s.cpEnable) {
                cpBookingCommand.PerformCallback();
                delete (s.cpEnable);
            }

            if (s.cpBooked) {
                formBooking.Hide();
                delete (s.cpBooked);
            }

            if (s.cpUnbooked) {
                if (s.cpUnbooked == '1') {
                    alert("Bút toán có tài khoản xuất hiện nhiều lần !");
                }
                else {
                    alert("Tổng nợ phải bằng tổng có !");

                }
                delete (s.cpUnbooked);
            }
        }

        function formBooking_Close(s, e) {
            grdBooking.CancelEdit();
            grdData.GetMainElement().focus();
        }

      

        ////////////////////////////////////////////////////////////////// form in lenh nhap kho

        function buttonInpuCommPrint_Click(s, e) {            
            hInputCommPrint.Set("print", "inventory");
            cpInpuCommReport.PerformCallback();
        }

        function buttonInpuCommExit_Click(s, e) {
            formEntryDetail.Hide();
        }

        function cpInpuCommLine_EndCallback(s, e) {

        }

        function cpInpuCommReport_EndCallback(s, e) {
            if (s.cpShowForm) {
                formInpuCommReport.Show();
                cpInpuCommLine.PerformCallback();
                delete (s.cpShowForm);
            }
        }

        function formEntryDetail_Shown(s, e) {
            cpInpuCommLine.PerformCallback("view");
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div class="captionFormName">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh Sách Phiếu Nhập Kho" Font-Bold="True"
            Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" DataSourceID="ImportCommandXDS"
        KeyFieldName="InventoryTransactionId" Width="100%" ClientInstanceName="grdData"
        KeyboardSupport="True" OnRowUpdating="grdData_RowUpdating" OnStartRowEditing="grdData_StartRowEditing">
        <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback" />
        <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback"
            Init="grdData_Init"></ClientSideEvents>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="InventoryTransactionId" ReadOnly="True" VisibleIndex="0"
                Width="0px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mã phiếu nhập kho" FieldName="Code" VisibleIndex="1"
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày tạo" FieldName="CreateDate" VisibleIndex="2"
                Width="100px">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Hạch toán" VisibleIndex="4"
                Width="70px">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btApprove">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Balance" />
                            <SpriteProperties CssClass="Sprite_Balance"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="Thao tác" VisibleIndex="5" Width="70px" ButtonType="Image">
                <EditButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Edit" />
                    </Image>
                </EditButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control" />
        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="Control"></SettingsBehavior>
        <SettingsPager PageSize="50" ShowEmptyDataRows="True">
        </SettingsPager>
        <Settings ShowFilterRow="True" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="True"></Settings>
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="ImportCommandXDS" runat="server" TypeName="NAS.DAL.Inventory.Journal.PurchaseInvoiceInventoryTransaction">
    </dx:XpoDataSource>

    <uc1:uInputCommWarehouse ID="uInputCommWarehouse1" runat="server" />
    <uc2:uWarehouseBooking ID="uWarehouseBooking1" runat="server" />    
</asp:Content>
