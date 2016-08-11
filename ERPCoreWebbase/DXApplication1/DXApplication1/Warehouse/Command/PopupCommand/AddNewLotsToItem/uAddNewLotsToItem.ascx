<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uAddNewLotsToItem.ascx.cs" Inherits="WebModule.Warehouse.Command.PopupCommand.AddNewLotsToItem.uAddNewLotsToItem" %>

<dx:ASPxCallbackPanel ID="cpItemByLoad" runat="server" ShowLoadingPanel="false" ShowLoadingPanelImage="false"
    Width="100%" oncallback="cpItemByLoad_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxLoadingPanel ID="ldpnItemByLot" runat="server" HorizontalAlign="Center" Text="Đang xử lý..." VerticalAlign="Middle" Modal="True">
                <LoadingDivStyle BackColor="Transparent"></LoadingDivStyle>
            </dx:ASPxLoadingPanel>
            <dx:ASPxPopupControl ID="popupItemByLot" runat="server" AllowDragging="True" AllowResize="True"
                HeaderText="Thêm mới số lô" Height="200px" PopupHorizontalAlign="WindowCenter" 
                PopupVerticalAlign="WindowCenter" Width="400px" ShowFooter="True" ShowMaximizeButton="True"
                CloseAction="CloseButton" 
                LoadingDivStyle-BackColor="Transparent"
                ModalBackgroundStyle-BackColor="Transparent"
                ShowSizeGrip="False"
                Maximized="false"
                Modal="True">
                <FooterTemplate>
                    <div style="padding: 10px;">
                        <div style="float: left">
                            <div style="float: left">
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                            </div>
                        </div>
                        <div style="float: right">
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnSavePopup" AutoPostBack="false" runat="server" 
                                    Text="Lưu lại" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="float: left; margin-left: 4px">
                                <!-- Places button here -->
                                <dx:ASPxButton ID="btnClosePopup" AutoPostBack="false" runat="server" 
                                    Text="Thoát" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                    </div>
                </FooterTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxFormLayout ID="frmAddLotByItem" runat="server" ColCount="1">
                            <Items>
                                <dx:LayoutItem Caption="Mã hàng hóa">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblItem" runat="server">
                                            </dx:ASPxLabel>
                                            <%--<dx:ASPxComboBox ID="cboItem" runat="server"
                                                EnableCallbackMode="True" IncrementalFilteringMode="Contains"
                                                CallbackPageSize="5"
                                                DropDownRows="5"
                                                ValueType="System.Guid"
                                                ValueField="ItemId"
                                                TextField="Code" 
                                                TextFormatString="{0}" >
                                                <Columns>
                                                    <dx:ListBoxColumn Caption="ItemId" FieldName="ItemId" Visible="false"/>
                                                    <dx:ListBoxColumn Caption="Mã" FieldName="Code" />
                                                    <dx:ListBoxColumn Caption="Tên hàng hóa" FieldName="Name" />
                                                </Columns>
                                            </dx:ASPxComboBox>--%>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Số lô">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxTextBox ID="txtLotCode" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Hạn dùng">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxDateEdit ID="DateEditExpiredDate" runat="server" ReadOnlyStyle-BackColor="ButtonFace" 
                                            ReadOnlyStyle-Cursor="default">
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                         <dx:XpoDataSource ID="LotXDS" runat="server" TypeName="NAS.DAL.Inventory.Lot.Lot"
                            Criteria="[RowStatus] = 1 ">
                        </dx:XpoDataSource>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
