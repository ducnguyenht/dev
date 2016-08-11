<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="OrderList.aspx.cs" Inherits="ERPCore.Sales.OrderList" %>

<%@ Register Src="UserControl/uOrderListEdit.ascx" TagName="uOrderListEdit" TagPrefix="uc1" %>
<%@ Register src="UserControl/ReportViewer.ascx" tagname="ReportViewer" tagprefix="uc2" %>
<%@ Register src="../Purchasing/UserControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <style type="text/css">
        .float_right
        {
            float: right;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(ReceiptVoucherEditingForm).on(
                ReceiptVoucherEditingForm.events.eClosing,
                function (evt) {
                    grdPaymentScheduleActual.PerformCallback('Refresh');                    
                }
            );
            });


        function grdPaymentScheduleActual_EndCallback(s, e) {
            if (s.cpRefresh) {
                //grdPaymentScheduleActual.Refresh();
                txtPaid.SetText(s.cpRefresh);
                txtCredit.SetText(parseFloat(txtAmount.GetText()) - parseFloat(txtPaid.GetText()));
                delete (s.cpRefresh)
            }
        }

        var gridPerformingCallback = false;
        var sLastFocusedEditorName = null;

        ////////////////////////////////////////////////////

        function grdBooking_Init() {
            RemoveKeyboardNavigationTo(grdBooking, 'line');
            AddKeyboardNavigationTo(grdBooking, 'line');
        }

        function grdBookingDetail_Init() {
            RemoveKeyboardNavigationTo(grdBookingDetail, 'line');
            AddKeyboardNavigationTo(grdBookingDetail, 'line');
        }

        function formBooking_Init() {
        }

        function formPurchaseEdit_Init() {
        }

        function showFormEditing(mode) {
            hPurchaseEditId.Clear();                                    
            if (mode == 'edit') {
                if (grdPurchase.GetFocusedRowIndex() < 0) {
                    return;
                }                
                hPurchaseEditId.Set("id", grdPurchase.GetRowKey(grdPurchase.GetFocusedRowIndex()));
            }
            else {
                hPurchaseEditId.Set("id", '');
            }

            formPurchaseEdit.Show();
        }


        ////////////////////////////////////////////////////

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
                        showFormEditing('edit');
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


        function txtCode_KeyDown(s, e) {
            if (e.htmlEvent.keyCode == 13) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);                
                ASPxClientControl.GetControlCollection().GetByName('cboSupplier').Focus();
            }
        }

        function cboUser_KeyDown(s, e) {
            if (e.htmlEvent.keyCode == 13) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);                
                var editor = grdPurchaseEditProduct.GetEditor('ItemUnitId!Key');
                editor.Focus();
            }
        }

        function txtIssuedDate_KeyDown(s, e) {
            if (e.htmlEvent.keyCode == 13) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                ASPxClientControl.GetControlCollection().GetByName('cboUser').Focus();
            }
        }

        function cboSupplier_KeyDown(s, e) {
            if (e.htmlEvent.keyCode == 13) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                ASPxClientControl.GetControlCollection().GetByName('txtIssuedDate').Focus();                
            }
        }


        ///////////////////////////////////////////////////////////////

        // Main 

        function ToolBar_ItemClick(s, e) {
            if (grdPurchase.GetFocusedRowIndex() < 0) {
                alert('Chưa chọn phiếu bán hàng !');
                return;
            }

            switch (e.item.name) {
                case 'new':
                    showFormEditing('new');
                    break;
                case 'edit':
                    showFormEditing('edit');
                    break;
                case 'copy':
                    if (grdPurchase.GetFocusedRowIndex() >= 0) {
                        if (confirm('Copy 1 phiếu bán hàng mới từ phiếu này ?')) {
                            grdPurchase.PerformCallback("copy|" + grdPurchase.GetRowKey(grdPurchase.GetFocusedRowIndex()));
                        }
                    }
                    break;
                case 'delete':
                    if (grdPurchase.GetFocusedRowIndex() >= 0) {
                        if (confirm('Xóa phiếu bán hàng này ?')) {
                            grdPurchase.DeleteRow(grdPurchase.GetFocusedRowIndex());
                        }
                    }

                    break;
            }
        }


        function grdPurchase_EndCallback(s, e) {
            hPurchaseEditId.Clear();

            if (s.cpPurchaseEdit) {
                showFormEditing(s.cpPurchaseEdit);
                delete (s.cpPurchaseEdit);
                return;
            }

            if (s.cpUndelete) {
                alert('Không xóa được vì phiếu bán hàng này đã nhập kho !');
                delete (s.cpUndelete);
            }

            if (s.cpCopyFailed) {
                alert('Phiếu bán hàng bị trùng mã !');
                delete (cpCopyFailed);
            }
            if (s.cpRefresh) {
                grdPurchase.Refresh();
                delete (s.cpRefresh);
            }
        }


        function grdPurchase_CustomButtonClick(s, e) {
        
        }

        function cmdBillActor_Click(s, e) {
            formBillActor.ShowAtElementByID('popupAnchor');
            formBillActor.PerformCallback();
        }
        

        ///////////////////////////////////////////////////////////////

        // Popup CRUD

        function bPayment_Click(s, e) {            
            ReceiptVoucherEditingForm.CreateFromBill(hPurchaseEditId.Get('id'));
        }

        function buttonSave_Click(s, e) {

        }

        function buttonCancel_Click(s, e) {

        }

   


        function formPurchaseEdit_Shown(s, e) {
            pagePurchaseEdit.SetActiveTabIndex(0);
            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

            if (hPurchaseEditId.Get("id") == '') {
                cpLine.PerformCallback('new');
            }
            else {
                cpLine.PerformCallback('load');
            }
        }

        function cpLine_EndCallback(s, e) {
            if (s.cpRefresh) {
                formPurchaseEdit.Hide();
                grdPurchase.Refresh();

                delete (s.cpRefresh);
                return;
            }

            if (s.cpSaving) {
                if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainer')) {
                    e.processOnServer = false;
                    return;
                }
                cpLine.PerformCallback('save');

                delete (s.cpSaving);
            }

            if (s.cpEnable) {
                if (s.cpEnable != 'true') {
                    hReportBillId.Set("id", s.cpEnable);

                    var editor = grdPurchaseEditProduct.GetEditor('ItemUnitId!Key');
                    if (editor != null) {
                        editor.Focus();
                    }
                }

                cpCommand.PerformCallback();
                delete (s.cpEnable);
            }

            if (s.cpCode) {
                if (s.cpCode == 'valid') {
                    //cpLine.PerformCallback('save');
                }
                else {
                    txtCode.errorText = 'Mã phiếu bán hàng đã tồn tại';
                    txtCode.SetIsValid(false);
                }

                delete (s.cpCode);
            }

            if (s.cpDisable) {
                grdPurchaseEditService.Refresh();
                delete (s.cpDisable);
            }

            if (s.cpUser) {
                //cpLine.PerformCallback('save');
                delete (s.cpUser);
            }

            if (s.cpSupplier) {
                //cpLine.PerformCallback('save');
                delete (s.cpSupplier);
            }

            if (s.cpRecoverFocus) {                
                switch (sLastFocusedEditorName) {
                    case 'txtCode':
                        ASPxClientControl.GetControlCollection().GetByName('cboSupplier').Focus();
                        break;
                    case 'cboSupplier':
                        ASPxClientControl.GetControlCollection().GetByName('txtIssuedDate').Focus();
                        break;
                    case 'txtIssuedDate':
                        ASPxClientControl.GetControlCollection().GetByName('cboUser').Focus();
                        break;
                    case 'cboUser':
                        var editor = grdPurchaseEditProduct.GetEditor('ItemUnitId!Key');
                        editor.Focus();

                        sLastFocusedEditorName = "grdPurchaseEditProduct";
                        break;
                    default:
                        break;
                }

                delete (s.cpRecoverFocus);
            }

            if (s.cpAaccountPeriod) {

                if (s.cpAaccountPeriod == 'valid') {
                    //cpLine.PerformCallback('save');
                }
                else {
                    txtIssuedDate.errorText = 'Ngày lập phải nằm trong chu kỳ kế toán';
                    txtIssuedDate.SetIsValid(false);
                }

                delete (s.cpAaccountPeriod)
            }

            if (s.cpReport) {

                //hReportBillId.Set("id", s.cpReport);

                
                //alert(hReportBillId.Get("id"));
                formReportViewer.Show();                
                delete (s.cpReport);
            }
        }

        function cpCommand_EndCallback(s, e) {
        }

        function buttonSaveDevice_Click(s, e) {
            if (!ASPxClientEdit.ValidateEditorsInContainerById('lineContainer')) {
                e.processOnServer = false;
                return;
            }

            cpLine.PerformCallback('save');
        }

        function buttonCancelDevice_Click(s, e) {
            ASPxClientEdit.ClearEditorsInContainerById('lineContainer');

            grdPurchaseEditProduct.CancelEdit();
            grdPurchaseEditService.CancelEdit();

            formPurchaseEdit.Hide();
            //grdPurchase.Refresh();
        }


        function buttonModify_Click(s, e) {
            cpLine.PerformCallback('edit');
        }


        function buttonImportInventory_Click(s, e) {
            cpLine.PerformCallback('inventoryexport');
        }

        function txtAmount_ValueChanged(s, e) {
        }



        /////////////////////////////////////////////////////////////// form edit

        function formPurchaseEdit_Resize(s, e) {
            cpLine.PerformCallback();
        }


        /////////////////////////////////////////////////////////////// form hach toan

        function formBooking_Resize(s, e) {
            cpBooking.PerformCallback();
        }

        function formBooking_Show(s, e) {
//            ASPxClientUtils.DetachEventFromElement(document, "keydown",
//                function (evt) {
//                    return OnDocumentKeyDown(evt, gridPurchase, type);
//                });

            

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

        function cpBooking_EndCallback(s, e) {
            if (s.cpEnable) {
                cpBookingCommand.PerformCallback();
                delete (s.cpEnable);
            }
            if (s.cpSuccess) {
                cpBookingCommand.PerformCallback();
                delete (s.cpSuccess);
                alert("Duyệt bút toán thành công ");
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
            cpLine.PerformCallback();
            
        }

        function colBookingDetailDebit_ValueChanged(s, e) {
        }

        function colBookingDetailCredit_ValueChanged(s, e) {
        }

        function grdPurchase_Init(s, e) {
            RemoveKeyboardNavigationTo(grdPurchase, 'header');
            AddKeyboardNavigationTo(grdPurchase, 'header');
        }

        ///////////////////////////////////////////////////////////////

        // Schedule
//        function grdDeliveryBillItem_FocusedRowChanged(s, e) {                     
//            if (grdDeliveryBillItem.GetFocusedRowIndex() > -1) {
//                cpLine.PerformCallback('itemproperty|' + grdDeliveryBillItem.GetRowKey(grdDeliveryBillItem.GetFocusedRowIndex()) + '|' + grdDeliveryBillItem.GetFocusedRowIndex());
//            }
//        }

        function grdDeliveryBillItemActual_FocusedRowChanged(s, e) {
            if (grdDeliveryBillItemActual.GetFocusedRowIndex() > -1) {
                cpLine.PerformCallback('itempropertyactual|' + grdDeliveryBillItemActual.GetRowKey(grdDeliveryBillItemActual.GetFocusedRowIndex()) + '|' + grdDeliveryBillItemActual.GetFocusedRowIndex());
            }
        }

        // Product

        function grdPurchaseEditProduct_Init(s, e) {                      
            //ASPxClientUtils.DetachEventFromElement(s.GetMainElement(), 'keydown', aspxClassesDocumentKeyDown);
            RemoveKeyboardNavigationTo(grdPurchaseEditProduct, 'line');
            AddKeyboardNavigationTo(grdPurchaseEditProduct, 'line');
            
        }

        function grdPurchaseEditProduct_EndCallback(s, e) {
            if (s.cpSumProductItem) {                
                cpLine.PerformCallback('sumProductItem|'+s.cpSumProductItem);
                delete (s.cpSumProductItem);
            }
        }

        function txtCode_ValueChanged(s, e) {
            cpLine.PerformCallback('txtCode_Checkexists');
            sLastFocusedEditorName = 'txtCode';
        }

        function txtIssuedDate_ValueChanged(s, e) {
            cpLine.PerformCallback('txtIssuedDate_Checkperiod');
            sLastFocusedEditorName = 'txtIssuedDate';
        }

        function cboUser_ValueChanged(s, e) {
            //cpLine.PerformCallback('cboUser_ValueChanged');
            sLastFocusedEditorName = 'cboUser';
        }

        function cboSupplier_ValueChanged(s, e) {
            //cpLine.PerformCallback('cboSupplier_ValueChanged');
            sLastFocusedEditorName = 'cboSupplier';
        }

        function txtProductDiscountAmount_ValueChanged(s, e) {
            cpLine.PerformCallback('txtProductDiscountAmount_ValueChanged');

        }

        var byProductTaxKeyDown = false;
        function txtProductTax_ValueChanged(s, e) {
            if (byProductTaxKeyDown) {
                cpLine.PerformCallback('txtProductTax_ValueChanged');
                byProductTaxKeyDown = false;
            }            
        }

        function txtProductTax_KeyDown(s, e) {
            byProductTaxKeyDown = true;
        }

        function txtProductDiscountSum_ValueChanged(s, e) {
            cpLine.PerformCallback('txtProductDiscountSum_ValueChanged');
        }

        // form close
        function formPurchaseEdit_Close(s, e) {                        
            grdPurchaseEditProduct.CancelEdit();
            grdPurchaseEditService.CancelEdit();
            
            grdPurchase.Refresh();
        }

        // Service

        function grdPurchaseEditService_Init(s, e) {
            //ASPxClientUtils.DetachEventFromElement(s.GetMainElement(), 'keydown', aspxClassesDocumentKeyDown);
            RemoveKeyboardNavigationTo(grdPurchaseEditService, 'line');
            AddKeyboardNavigationTo(grdPurchaseEditService, 'line');

        }

        function grdPurchaseEditService_EndCallback(s, e) {
            if (s.cpSumServiceItem) {
                cpLine.PerformCallback('sumServiceItem' + '|' + s.cpSumServiceItem);
                delete (s.cpSumServiceItem);
            }
        }

        function txtServiceDiscountSum_ValueChanged(s, e) {
            cpLine.PerformCallback('txtServiceDiscountSum_ValueChanged');
        }

        function txtServiceDiscountAmount_ValueChanged(s, e) {
            cpLine.PerformCallback('txtServiceDiscountAmount_ValueChanged');
        }

        function txtServiceTax_ValueChanged(s, e) {
            cpLine.PerformCallback('txtServiceTax_ValueChanged');
        }

        function buttonPrint_Click(s, e) {
            hReportBillId.Set("id", grdPurchase.GetRowKey(grdPurchase.GetFocusedRowIndex()));
            formReportViewer.PerformCallback();
        }

        function formReportViewer_EndCallback(s, e) {
            if (s.cpShowReport) {
                formReportViewer.Show();
                delete(s.cpShowReport);
            }
        }

        function grdBooking_EndCallback(s, e) {
//            alert('cpRefresh');
//            if (s.cpRefresh) {
//                alert('cpRefresh');
//                grdBooking.Refresh();
//                grdBooking_Detail.Refresh();
//                delete (s.cpRefresh);
//            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">  
    <script type="text/javascript">
        var target = new EventTarget();
        function handleEvent(event) {
            grdDeliveryScheduleActual.PerformCallback();
        };
        target.addListener("SharedClientEvent", handleEvent);

    </script>  
    <div style="margin-bottom: 10px;">       
        <dx:aspxlabel id="lblHeader" runat="server" text="Phiếu Bán Hàng" font-bold="True"
            font-size="Small">
        </dx:aspxlabel>        
    </div>
    <div style="float: left;width:75%;margin-bottom:10px;"> 
        <uc3:ToolBar ID="ToolBar1" runat="server" />
    </div>
    <table style="width: 100%">
        <tr>
            <td style="vertical-align: top">
                <div class="gridContainer">
                    <dx:aspxcallbackpanel id="cpHeader" runat="server" width="100%" clientinstancename="cpHeader"
                        hidecontentoncallback="True">                               
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                        <dx:XpoDataSource ID="PurchaseReceiptXDS" runat="server" DefaultSorting="" 
                            TypeName="NAS.DAL.Invoice.SalesInvoice" ServerMode="True" Criteria="">
                        </dx:XpoDataSource>
                        <dx:ASPxGridView ID="grdPurchase" runat="server" AutoGenerateColumns="False" KeyFieldName="BillId"
                            OnRowDeleting="grdData_RowDeleting" OnStartRowEditing="grdData_StartRowEditing"
                            Width="100%" OnInitNewRow="grdData_InitNewRow" 
                            ClientInstanceName="grdPurchase" DataSourceID="PurchaseReceiptXDS" 
                            KeyboardSupport="True" OnCustomCallback="grdPurchase_CustomCallback">
                            <ClientSideEvents EndCallback="grdPurchase_EndCallback" 
                                CustomButtonClick="grdPurchase_CustomButtonClick" Init="grdPurchase_Init">                             
                            </ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Khách hàng" 
                                    FieldName="SourceOrganizationId.Name" ShowInCustomizationForm="True" 
                                    VisibleIndex="3">                                   
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="7"
                                    ButtonType="Image" Width="100px">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="copy" Visibility="Invisible">
                                            <Image ToolTip="Sao chép">                                          
                                                <SpriteProperties CssClass="Sprite_Copy"></SpriteProperties>
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <EditButton Visible="True">
                                        <Image>                                          
                                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                        </Image>
                                    </EditButton>
                                    <NewButton Visible="false">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                        </Image>
                                    </NewButton>
                                    <DeleteButton Visible="false">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                        </Image>
                                    </DeleteButton>
                                    <CancelButton Visible="True">
                                    </CancelButton>
                                    <UpdateButton Visible="True">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                        </Image>
                                    </UpdateButton>
                                    <ClearFilterButton Visible="True">
                                        <Image>
                                          <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                                        </Image>
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Đơn Hàng" FieldName="Code" Name="Code" ShowInCustomizationForm="True"
                                    VisibleIndex="1" Width="150px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="BillId" 
                                    FieldName="BillId" ShowInCustomizationForm="True" VisibleIndex="0" 
                                    Name="BillId" Width="0px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Nhân viên bán hàng" 
                                    FieldName="TargetOrganizationId.Name" Name="BillActors"
                                    ShowInCustomizationForm="True" VisibleIndex="4" Width="250px">                                  
                                </dx:GridViewDataTextColumn>                               
                                <dx:GridViewDataDateColumn Caption="Ngày Mua" FieldName="CreateDate" 
                                    ShowInCustomizationForm="True" VisibleIndex="2" Width="100px">
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="Tổng giá trị" FieldName="Total" 
                                    ShowInCustomizationForm="True" VisibleIndex="5" Width="150px">
                                    <PropertiesTextEdit DisplayFormatString="0,0">
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowGroup="False"
                                ConfirmDelete="True" ColumnResizeMode="Control" AllowFocusedRow="True" />
                            <SettingsBehavior AllowGroup="False" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"
                                ConfirmDelete="True"></SettingsBehavior>
                            <SettingsPager PageSize="50" ShowEmptyDataRows="true" RenderMode="Classic">
                            </SettingsPager>                                                       
                            <SettingsEditing Mode="Inline"></SettingsEditing>
                            <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" 
                                ShowFilterRowMenu="True"></Settings>
                            <Styles>
                                <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                </Header>
                                <HeaderPanel HorizontalAlign="Center">
                                </HeaderPanel>
                                <CommandColumn HorizontalAlign="Center" Spacing="10px">
                                </CommandColumn>
                            </Styles>
                        </dx:ASPxGridView>
                
                    </dx:PanelContent>
                </PanelCollection>
            </dx:aspxcallbackpanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <uc2:ReportViewer ID="ReportViewer1" runat="server" />
                <uc1:uOrderListEdit ID="uOrderListEdit1" runat="server" />
            </td>
        </tr>      
        <script type="text/javascript">
                    
        </script>
    </table>
</asp:Content>
