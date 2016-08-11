<%@ Control Language="C#"  ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="uReceipVoucherSplit.ascx.cs"
    Inherits="WebModule.Accounting.UserControl.uReceipVoucherSplit" %>
<style type = "text/css">
.hd
{
    display:none;
    padding:0px;
}
.float_right
{
    float:right;
}
</style>
<div style="margin-bottom: 10px;">
<dx:ASPxPageControl ID="pc" runat="server" ActiveTabIndex="2" RenderMode="Lightweight" ClientInstanceName = "pc"
    Width="100%">
    <TabPages>
        <dx:TabPage Name="Personal" Text="Phiếu Thu Nguồn" ClientEnabled="True">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True" >
                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewCommandColumn Caption=" " ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                VisibleIndex="0">
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Mã Phiếu Thu" FieldName="ID" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ngày Thu" FieldName="Date" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Mô Tả" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Doc" Text="Mô tả">
                                        <Image>
                                            <SpriteProperties CssClass="Sprite_Document" />
                                        </Image>
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                    </dx:ASPxGridView>                    
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage ClientEnabled="False" Name="Confirmation" Text="Phiếu Thu Đích">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="STT" FieldName="STT" ShowInCustomizationForm="True"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Mã Phiếu Thu" FieldName="ID" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Người Nộp" FieldName="NN" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số tiền" FieldName="Amount" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" VisibleIndex="7">
                                <EditButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Edit" />
                                        <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                                    </Image>
                                </EditButton>
                                <NewButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_New" />
                                        <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                                    </Image>
                                </NewButton>
                                <DeleteButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Delete" />
                                        <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                                    </Image>
                                </DeleteButton>
                                <CancelButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                        <SpriteProperties CssClass="Sprite_Cancel"></SpriteProperties>
                                    </Image>
                                </CancelButton>
                                <UpdateButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                        <SpriteProperties CssClass="Sprite_Apply"></SpriteProperties>
                                    </Image>
                                </UpdateButton>
                                <ClearFilterButton Visible="True">
                                </ClearFilterButton>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" />
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxLabel ID = "Label1" runat = "server" Text = "Danh Sách Phiếu Thu Sau Khi Tách" Height = "35px" Font-Bold = "true" Font-Size = "Medium">                    
                    </dx:ASPxLabel>
                    <dx:ASPxGridView ID="grdData" runat="server" AutoGenerateColumns="False" ClientInstanceName="grdData"
                        KeyFieldName="ProductId" Width="100%">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Mã Phiếu Thu" FieldName="Code" ShowInCustomizationForm="True"
                                VisibleIndex="0" Width="15%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Người Nộp Tiền" FieldName="Customer" ShowInCustomizationForm="True"
                                VisibleIndex="1" Width="20%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Địa Chỉ" FieldName="Address" ShowInCustomizationForm="True"
                                VisibleIndex="2" Width="20%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="dg" ShowInCustomizationForm="True"
                                VisibleIndex="5" Width="20%">
                                <DataItemTemplate>
                                <dx:ASPxTextBox runat = "server" ID="ASPxTextBox1" Width="100%">                                    
                                </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="status" ShowInCustomizationForm="True"
                                VisibleIndex="6" Width="15%">
                                <DataItemTemplate>
                                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" 
                                        Width="100%">
                                    </dx:ASPxComboBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số Tiền" FieldName="Amount" ShowInCustomizationForm="True"
                                VisibleIndex="3" Width="10%">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" />
                    </dx:ASPxGridView>
                    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
                        <Items>
                            <dx:LayoutItem ColSpan="3" ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Height="35px" Text="Hoàn thành việc tách hóa đơn">
                                        </dx:ASPxLabel>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:EmptyLayoutItem Width="85%">
                            </dx:EmptyLayoutItem>                            
                            <dx:LayoutItem ShowCaption="False" Width="15%" CaptionSettings-Location = "Left" CaptionCellStyle-CssClass = "hd">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer runat="server" SupportsDisabledAttribute="True" >
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Tiếp tục" CssClass = "float_right">
                                            <Image>
                                                <SpriteProperties CssClass="Sprite_Repeat" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:ASPxFormLayout>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
</div>
<dx:aspxpanel id="dxpError" clientinstancename="dxpError" runat="server" cssclass="errorPanel"
    clientvisible="false">
    <PanelCollection>
    </PanelCollection>
</dx:aspxpanel>
<dx:aspxhiddenfield id="hfRegInfo" clientinstancename="hfRegInfo" runat="server"
    syncwithserver="true" viewstatemode="Enabled">
</dx:aspxhiddenfield>