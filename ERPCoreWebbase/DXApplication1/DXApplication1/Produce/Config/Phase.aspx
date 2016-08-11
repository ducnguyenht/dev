<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Phase.aspx.cs" Inherits="WebModule.Produce.Config.Phase" %>

<%@ Register Src="UserControl/uPhaseEdit.ascx" TagName="uPhaseEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        //Phase
        function grDataPhase_EndCallback(s, e) {
            if (s.cpEditPhase) {
                formPhaseEdit.Show();
                ASPxClientEdit.ClearEditorsInContainerById('lineContainerPhasel');
                //       cboRowStatusProduct.SetValue('A');
                if (s.cpEditPhase == 'edit') {
                    cbpanelUserPhase.PerformCallback('edit');
                }
                else {
                    cbpanelUserPhase.PerformCallback('new');
                }
                delete (s.cpEditPhase);
                return;
            }
        }
        function btnCancelPhase_Click(s, e) {
            formPhaseEdit.Hide();
        }
        function buttonCancelDevice_Click(s, e) {
        }
        function buttonSaveDevice_Click(s, e) {
        }
        //Phase
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom: 10px;">
        <div class="captionFormName">
            <dx:ASPxLabel ID="lblHeader" runat="server" Text="Danh Mục Công Đoạn" Font-Bold="True"
                Font-Size="Small">
            </dx:ASPxLabel>
        </div>
        <dx:ASPxCallbackPanel ID="cbpanelPhase" runat="server" ClientInstanceName="cbpanelPhase"
            Width="100%">
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdataPhase" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdataPhase"
                        Width="100%" OnInitNewRow="grdataPhase_InitNewRow">
                        <ClientSideEvents EndCallback="grDataPhase_EndCallback" />
                        <Columns>
                            <dx:GridViewCommandColumn Caption="Thao Tác" ShowInCustomizationForm="True" VisibleIndex="7"
                                ButtonType="Image">
                                <EditButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Edit" />
                                    </Image>
                                </EditButton>
                                <NewButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_New" />
                                    </Image>
                                </NewButton>
                                <DeleteButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Delete" />
                                    </Image>
                                </DeleteButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Mã Công Đoạn" FieldName="PhaseID" ShowInCustomizationForm="True"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên Công Đoạn" FieldName="Phase" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewBandColumn Caption="Thời gian sử dụng" 
                                ShowInCustomizationForm="True" VisibleIndex="4">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Từ ngày" FieldName="Start" 
                                        ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Đến ngày" FieldName="End" 
                                        ShowInCustomizationForm="True" VisibleIndex="1">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="PhaseRowStatus" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                                <PropertiesComboBox>
                                    <Items>
                                        <dx:ListEditItem Text="Sử dụng" Value="A" />
                                        <dx:ListEditItem Text="Tạm ngưng" Value="I" />
                                    </Items>
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn Vị Thời Gian" FieldName="PhaseTimeUnit" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thời Lượng Kế Hoạch" FieldName="PhaseTime" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mô Tả" FieldName="PhaseDescription" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                        <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
        <uc1:uPhaseEdit ID="uPhaseEdit1" runat="server" />
    </div>
</asp:Content>
