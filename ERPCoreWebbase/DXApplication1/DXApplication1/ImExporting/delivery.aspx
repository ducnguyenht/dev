<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="delivery.aspx.cs" Inherits="WebModule.ImExporting.delivery" %>
<%@ Register src="~/ImExporting/UserControl/uPurchaseEdit.ascx" tagname="uPurchaseEdit" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
<script type="text/javascript">
    function buttonSave_Click(s, e) {
    }

    function buttonCancel_Click(s, e) {
        formPurchaseEdit.Hide();
    }
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="margin-bottom:10px;">
        <dx:ASPxLabel ID="lblHeader" runat="server" Text="Tiến Độ Đơn Hàng" 
        Font-Bold="True" Font-Size="Small">
        </dx:ASPxLabel>
    </div>
    <div style="">
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="Utility.CustomEvent" DeleteMethod="DeleteMethodHandler" 
        InsertMethod="InsertMethodHandler" 
        onobjectcreated="ObjectDataSource1_ObjectCreated" 
        SelectMethod="SelectMethodHandler" TypeName="Utility.CustomEventDataSource" 
        UpdateMethod="UpdateMethodHandler"></asp:ObjectDataSource>
        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" 
        ID="grdData" oninitnewrow="grdData_InitNewRow" 
        onrowdeleting="grdData_RowDeleting" 
        onstartrowediting="grdData_StartRowEditing" KeyFieldName="Code">
            <ClientSideEvents CustomButtonClick="function(s, e) {
	formPurchaseEdit.Show();
    buttonSave.SetVisible(false);
}" />
            <Columns>
                <dx:GridViewDataComboBoxColumn Width="10%" 
            Caption="Mã Đơn Hàng" VisibleIndex="0" FieldName="Code">
                    <PropertiesComboBox>
                        <Columns>
                            <dx:ListBoxColumn Width="150px" Caption="M&#227; Nh&#243;m H&#224;ng H&#243;a">
                            </dx:ListBoxColumn>
                            <dx:ListBoxColumn Width="300px" Caption="T&#234;n Nh&#243;m H&#224;ng H&#243;a">
                            </dx:ListBoxColumn>
                        </Columns>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Width="10%" 
            Caption="Ngày " VisibleIndex="1" FieldName="Date">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Width="20%" 
            Caption="Nhà Cung Cấp" VisibleIndex="2" FieldName="Supplier">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Tổng Thành Tiền" VisibleIndex="4" Width="15%" 
            FieldName="Amount">
                    <PropertiesTextEdit DisplayFormatString="c2">
                        <Style HorizontalAlign="Right">
                        </Style>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Nhân Viên Đề Nghị" VisibleIndex="3" Width="20%" 
            FieldName="Department">
                </dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn ButtonType="Image" 
            Width="10%" Caption="Thao T&#225;c" VisibleIndex="6" Visible="False">
                    <EditButton Visible="True">
    <Image ToolTip="Sửa">
        <SpriteProperties CssClass="Sprite_Edit" />
    </Image>
</EditButton>
<NewButton Visible="True">
    <Image ToolTip="Thêm">
        <SpriteProperties CssClass="Sprite_New" />
    </Image>
</NewButton>
<DeleteButton Visible="True">
    <Image ToolTip="Xóa">
        <SpriteProperties CssClass="Sprite_Delete" />
    </Image>
</DeleteButton>
<ClearFilterButton Visible="True">
    <Image ToolTip="Hủy">
        <SpriteProperties CssClass="Sprite_Clear" />
    </Image>
</ClearFilterButton>
<UpdateButton>
    <Image ToolTip="Cập nhật">
        <SpriteProperties CssClass="Sprite_Apply" />
    </Image>
</UpdateButton>
<CancelButton>
    <Image ToolTip="Bỏ qua">
        <SpriteProperties CssClass="Sprite_Cancel" />
    </Image>
</CancelButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn ButtonType="Image" Caption="Chi Tiết" 
            VisibleIndex="5" Width="5%">
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton>
                            <Image ToolTip="Chi tiết">
    <SpriteProperties CssClass="Sprite_Document" />
</Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
            AllowSelectSingleRowOnly="True" />
            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" 
            AllowSelectSingleRowOnly="True">
            </SettingsBehavior>
            <SettingsPager PageSize="100" Mode="EndlessPaging">
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" 
            ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowFilterRowMenuLikeItem="True" />
            <SettingsEditing Mode="Inline">
            </SettingsEditing>
            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" 
            ShowFilterRowMenuLikeItem="True" VerticalScrollableHeight="300" 
            VerticalScrollBarMode="Auto">
            </Settings>
            <Styles>
                <CommandColumn Spacing="10px">
                </CommandColumn>
            </Styles>
        </dx:ASPxGridView>
    </div>
    <dx:ASPxPageControl runat="server" ActiveTabIndex="0" RenderMode="Lightweight" 
        Width="100%" Height="100%" ID="ASPxPageControl1">
        <TabPages>
            <dx:TabPage Text="Tiến Độ Giao H&#224;ng">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="overflow:scroll; height:500px;">
                            <dx:ASPxScheduler runat="server" Start="2013-08-24" Width="100%" 
                ClientIDMode="AutoID" ID="ASPxScheduler1" 
                OnPrepareAppointmentFormPopupContainer="ASPxScheduler1_PrepareAppointmentFormPopupContainer">
                                <Views>
                                    <DayView>
                                        <TimeRulers>
                                            <dx:TimeRuler>
                                            </dx:TimeRuler>
                                        </TimeRulers>
                                    </DayView>
                                    <WorkWeekView>
                                        <TimeRulers>
                                            <dx:TimeRuler>
                                            </dx:TimeRuler>
                                        </TimeRulers>
                                    </WorkWeekView>
                                </Views>
                            </dx:ASPxScheduler>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="tabDetail" Text="Tiến Độ Thanh To&#225;n">
                <ContentCollection>
                    <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                        <div style="overflow:scroll; height:500px;">
                            <dx:ASPxScheduler runat="server" Start="2013-08-24" Width="100%" 
                ClientIDMode="AutoID" ID="ASPxScheduler2">
                                <Views>
                                    <DayView>
                                        <TimeRulers>
                                            <dx:TimeRuler>
                                            </dx:TimeRuler>
                                        </TimeRulers>
                                    </DayView>
                                    <WorkWeekView>
                                        <TimeRulers>
                                            <dx:TimeRuler>
                                            </dx:TimeRuler>
                                        </TimeRulers>
                                    </WorkWeekView>
                                </Views>
                            </dx:ASPxScheduler>
                        </div>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <uc2:uPurchaseEdit ID="uPurchaseEdit1" runat="server" />
</asp:Content>
