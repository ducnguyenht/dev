<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uInitInventory.ascx.cs"
    Inherits="WebModule.Warehouse.UserControl.uInitInventory" %>
    
<script type="text/javascript">

    function formManufacturerEdit_Init(s, e) {
        //grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        //grmanufacturer0.SetHeight($("#testheight").height() - 120);
    }
    function formManufacturerEdit_AfterResizing(s, e) {
        // grdDataManufacturerGroup.SetHeight($("#testheight").height() - 120);
        // grmanufacturer0.SetHeight($("#testheight").height() - 120);

        ASPxClientControl.AdjustControls();
    }

    var ManuWarehouseUnitEditForm = {
        Show: function (headerText, recordId) {
            if (headerText) {
                formWarehouseUnitEdit.SetHeaderText(headerText);
            }
            if (recordId) {
                this._recordId = recordId;
                formWarehouseUnitEdit.PerformCallback('edit' + '|' + recordId);
            }
            else {
                this._recordId = null;
                formWarehouseUnitEdit.PerformCallback('new');
            }
            formWarehouseUnitEdit.Show();
            /////2013-09-21 ERP-580 Khoa.Truong INS START
            $(ManuWarehouseUnitEditForm).triggerHandler('shown');

            /////2013-09-22 ERP-580 Khoa.Truong INS END
        },
        Save: function () {
            /////2013-09-20 ERP-580 Khoa.Truong MOD START
            //Validate all editors in form
            var validated = ASPxClientEdit.ValidateEditorsInContainer(pagWarehouseUnitEdit.GetMainElement(), null, true);
            if (validated) {
                if (!formWarehouseUnitEdit.InCallback()) {
                    var args = 'save';
                    if (this._recordId) {
                        args += '|' + this._recordId;
                    }
                    formWarehouseUnitEdit.PerformCallback(args);
                }
            }
            else {
                pagWarehouseUnitEdit.SetActiveTabIndex(0);
            }
            //formWarehouseUnitEdit.Hide();
            /////2013-09-20 ERP-580 Khoa.Truong MOD END
        },
        Hide: function () { formWarehouseUnitEdit.Hide(); },

        btnSave_Click: function (s, e) {
            ManuWarehouseUnitEditForm.Save();
        },

        btnCancel_Click: function (s, e) {
            ManuWarehouseUnitEditForm.Hide();
        },

        EndCallback: function (s, e) {
            if (s.cpInvalid) {
                delete s.cpInvalid;
                return;
            }
            if (s.cpCallbackArgs) {
                formWarehouseUnitEdit.Hide();
                var args = jQuery.parseJSON(s.cpCallbackArgs);
                $(ManuWarehouseUnitEditForm).triggerHandler('saved', args);
                delete s.cpCallbackArgs;
            }
            /////2013-09-21 ERP-580 Khoa.Truong INS END

        },
        //Bind Saved Event method
        BindSavedEvent: function (callback) {
            $(ManuWarehouseUnitEditForm).on('saved', callback);
        },
        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Bind Shown Event
        BindShownEvent: function (callback) {
            $(ManuWarehouseUnitEditForm).on('shown', callback);
        },
        /////2013-09-21 ERP-580 Khoa.Truong INS END


        /////2013-09-21 ERP-580 Khoa.Truong INS START
        //Custom validation
        ValidateForm: function (s, e) {
            switch (s.name) {
               
                default:
                    break;
            }
        }
        /////2013-09-21 ERP-580 Khoa.Truong INS END


    };
</script>
<div id="lineContainerWarehouseUnit"> 
<dx:ASPxCallbackPanel ID="cpLineWarehouseUnit" runat="server" Width="100%" 
        ClientInstanceName="cpLineWarehouseUnit" oncallback="cpLineWarehouseUnit_Callback">
 
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formWarehouseUnitEdit" runat="server" 
            HeaderText="Thông tin đơn vị lưu trữ -" Height="600px" Modal="True"  
            Width="900px" ClientInstanceName="formWarehouseUnitEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" OnWindowCallback="popWarehouseUnitEdit_WindowCallback" 
            ShowFooter="True" ScrollBars="Auto" ShowMaximizeButton="True" 
            ShowSizeGrip="False">
            <ClientSideEvents 
                        Init="formManufacturerEdit_Init" 
                        AfterResizing="formManufacturerEdit_AfterResizing"
                        EndCallback="ManuWarehouseUnitEditForm.EndCallback">
            </ClientSideEvents>
          <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False" ToolTip="Trợ Giúp - Ctrl + H">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelWarehouse" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelWarehouse" Text="Thoát" Wrap="False" 
                                    ToolTip="Thoát  - Ctrl + C">
                                    <ClientSideEvents Click="ManuWarehouseUnitEditForm.btnCancel_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptWarehouse" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveWarehouse" Text="Lưu Lại" Wrap="False" 
                                    ToolTip="Lưu và Đóng - Ctr + S">
                                    <ClientSideEvents Click="ManuWarehouseUnitEditForm.btnSave_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
                    <dx:ASPxPageControl ID="pagWarehouseUnitEdit" ClientInstanceName="pagWarehouseUnitEdit" runat="server" ActiveTabIndex="0" 
                        Height="100%" Width="100%" 
                        EnableTabScrolling="True">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Nhập tồn kho ban đầu">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxFormLayout ID="frmlInfoGeneral" runat="server" DataSourceID="" 
                                            AlignItemCaptionsInAllGroups="True" EnableTheming="True" Width="100%">
                                            <Items>
                                                <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="None">
                                                    <Border BorderStyle="None" />
                                                    <Items>
                                                        <dx:LayoutItem Caption="Mã thẻ nhập" FieldName="Code" CaptionCellStyle-CssClass="CaptionStyle"
                                                            HelpText="Tối đa 36 ký tự, không cho trùng lắp" RequiredMarkDisplayMode="Required">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxCallbackPanel ShowLoadingPanel="false" ClientInstanceName="cpntxtCode" ID="cpntxtCode"
                                                                        runat="server" Width="200px">
                                                                        <PanelCollection>
                                                                            <dx:PanelContent>
                                                                                <dx:ASPxTextBox ID="txtCode" runat="server" Width="200px" OnValidation="txtCode_Validation">
                                                                                    <ClientSideEvents Validation="ManuWarehouseUnitEditForm.ValidateForm"></ClientSideEvents>
                                                                                    <ValidationSettings ErrorDisplayMode="ImageWithText" ErrorText="">
                                                                                        <RequiredField IsRequired="True" ErrorText="Chưa nhập mã"></RequiredField>
                                                                                    </ValidationSettings>
                                                                                </dx:ASPxTextBox>
                                                                            </dx:PanelContent>
                                                                        </PanelCollection>
                                                                    </dx:ASPxCallbackPanel>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                            <CaptionCellStyle CssClass="CaptionStyle">
                                                            </CaptionCellStyle>
                                                        </dx:LayoutItem>
<%--                                                        <dx:LayoutItem Caption="Ngày Lập">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server"
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxDateEdit ID="txtIssuedDate" runat="server" Width="200px" ClientInstanceName="txtIssuedDate">
                                                                        <ClientSideEvents ValueChanged="txtIssuedDate_ValueChanged"></ClientSideEvents>
                                                                    </dx:ASPxDateEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
--%>                                                        <dx:LayoutItem Caption="Số lượng tồn" FieldName="Balance" >
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="txtBalance" runat="server" 
                                                                        ClientInstanceName="txtBalance" Width="200px">
                                                                        <ClientSideEvents Validation="ManuWarehouseUnitEditForm.ValidateForm"></ClientSideEvents>
                                                                        <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                            <RequiredField ErrorText="Chưa nhập số lượng tồn" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem Caption="Giá thành" FieldName="Price">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                                                    SupportsDisabledAttribute="True">
                                                                    <dx:ASPxTextBox ID="txtPrice" runat="server" 
                                                                        ClientInstanceName="txtPrice" Width="200px">
                                                                        <ClientSideEvents Validation="ManuWarehouseUnitEditForm.ValidateForm"></ClientSideEvents>
                                                                        <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                            <RequiredField ErrorText="Chưa nhập giá thành sản phẩm" IsRequired="True" />
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:LayoutGroup>
                                            </Items>
                                            <Border BorderStyle="None" />
                                        </dx:ASPxFormLayout>
                                    </dx:ContentControl>                                   
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
</div>