<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ListOutputComm.aspx.cs" Inherits="WebModule.Warehouse.ListOutputComm" %>

<%@ Register Src="UserControl/uListOutCommBooking.ascx" TagName="uListOutCommBooking"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/uInputCommWarehouse.ascx" TagName="uInputCommWarehouse"
    TagPrefix="uc2" %>
<%@ Register Src="UserControl/uOutCommWarehouse.ascx" TagName="uOutCommWarehouse"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        ///////////////////////////////////////////////////////////////////

        function formBooking_Init() {
            //            ASPxClientUtils.DetachEventFromElement(document, 'keydown', function (evt) {
            //                if (evt.keyCode == 27) {
            //                    formBooking.Hide();
            //                }
            //            });

            //            ASPxClientUtils.AttachEventToElement(document, 'keydown', function (evt) {
            //                if (evt.keyCode == 27) {
            //                    formBooking.Hide();
            //                }
            //            });
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
           // formBooking.Show();            
        }

        function grdData_CustomButtonClick(s, e) {
            if (e.buttonID == "btnApprove") {
                hWarehouseBookingId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                hOutputCommId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                formBooking.Show();
            }
            if (e.buttonID == "btnEdit") {
                hOutputCommId.Set("id", grdData.GetRowKey(grdData.GetFocusedRowIndex()));
                formEntryDetail.Show();

                //cpLine.PerformCallback("view");
                
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

        function buttonBookingCancel_Click(s, e) {
            grdBooking.CancelEdit();
            formBooking.Hide();
        }

        function buttonBookingPrint_Click(s, e) {            
            hOutputCommPrint.Set("print", "account");
            cpOutputCommReport.PerformCallback();
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

        //form print
        
        ///////////////////////////////////////////////////////////////////// form in lenh xuat

        function buttonOutputCommPrint_Click(s, e) {
            hOutputCommPrint.Set("print", "inventory");
            cpOutputCommReport.PerformCallback();
        }

        function buttonOutputCommExit_Click(s, e) {
            formEntryDetail.Hide();
        }

        function cpOutputCommLine_EndCallback(s, e) {

        }

        function cpOutputCommReport_EndCallback(s, e) {            
            if (s.cpShowForm) {                
                formOutCommViewer.Show();
                cpOutputCommLine.PerformCallback();
                delete (s.cpShowForm);
            }
    
        }

        function formEntryDetail_Shown(s, e) {
            cpLine.PerformCallback("view");
        }

        function cpLine_EndCallback(s, e) {
        }

        Date.prototype.ddmmyyyy = function () {

            var yyyy = this.getFullYear().toString();
            var mm = (this.getMonth() + 1).toString(); 
            var dd = this.getDate().toString();

            return (dd[1] ? dd : "0" + dd[0]) + '/' + (mm[1] ? mm : "0" + mm[0]) + '/' + yyyy;
        }; 

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách phiếu xuất kho" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>
    <dx:ASPxCallbackPanel ID="cpHeader" runat="server" Width="100%" ClientInstanceName="cpHeader"
        HideContentOnCallback="True">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="grdData"
                    DataSourceID="SalesInvoiceInventoryTransactionXDS" KeyFieldName="InventoryTransactionId"
                    ClientInstanceName="grdData" KeyboardSupport="True">
                    <ClientSideEvents CustomButtonClick="grdData_CustomButtonClick" EndCallback="grdData_EndCallback"
                        Init="grdData_Init"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewCommandColumn Caption="Hạch toán" VisibleIndex="5" ButtonType="Image"
                            Width="70px">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btnApprove" Text="Hạch toán" Image-ToolTip="Hạch toán">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Balance"></SpriteProperties>
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="Code" Caption="Mã phiếu xuất" VisibleIndex="0"
                            Width="150px">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="codelink" runat="server" Text='<%# Eval("code") %>'>
                                    <ClientSideEvents Click="grdData_CustomButtonClick" />
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Description" Caption="Diễn giải" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ngày tạo phiếu" FieldName="CreateDate" ShowInCustomizationForm="True"
                            VisibleIndex="1" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                            VisibleIndex="7" Width="70px">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="btnEdit">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Edit" />
                                    </Image>
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>                            
                            <ClearFilterButton Visible="True">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                        ColumnResizeMode="NextColumn"></SettingsBehavior>
                    <SettingsPager PageSize="50" ShowEmptyDataRows="true">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                    </Settings>
                    <Styles>
                        <Header HorizontalAlign="Center" Font-Bold="true" Wrap="True">
                        </Header>
                        <CommandColumn Spacing="10px">
                        </CommandColumn>
                    </Styles>
                </dx:ASPxGridView>
                <br />
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <uc1:uListOutCommBooking ID="uListOutCommBooking1" runat="server" />
    <dx:XpoDataSource ID="SalesInvoiceInventoryTransactionXDS" runat="server" TypeName="NAS.DAL.Inventory.Journal.SalesInvoiceInventoryTransaction">
    </dx:XpoDataSource>
    <uc3:uOutCommWarehouse ID="uOutCommWarehouse1" runat="server" />
</asp:Content>
