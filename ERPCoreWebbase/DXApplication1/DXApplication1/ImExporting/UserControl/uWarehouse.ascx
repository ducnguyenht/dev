<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uWarehouse.ascx.cs" Inherits="WebModule.ImExporting.UserControl.uWarehouse" %>
<style type="text/css">

    .style21
    {
        height: 16px;
        width: 356px;
    }
    .style30
    {
        height: 16px;
        width: 29px;
    }
    .style12
    {
        width: 483px;
    }
    .style5
    {
        height: 16px;
    }
    .style11
    {
        height: 16px;
    }
    .style27
    {
        height: 16px;
        width: 44px;
    }
    .style7
    {        
        height: 16px;
    }
    .style26
    {
        height: 17px;
    }
    .style25
    {
        width: 609px;
    }
    </style>


<div id="lineContainer"> 
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine" oncallback="cpLine_Callback">
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formMaterialEdit" runat="server" 
            HeaderText="Cập Nhật Kho Trung Chuyển" Height="617px" Modal="True" 
            RenderMode="Lightweight"  
            Width="850px" ClientInstanceName="formMaterialEdit" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000" 
            ClientIDMode="AutoID">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
                    <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                        Height="520px" RenderMode="Lightweight" Width="100%">
                        <TabPages>
                            <dx:TabPage Name="tabGeneral" Text="Thông Tin Chung">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl11" runat="server" SupportsDisabledAttribute="True">
                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx" 
                                                    style="height: 470px; width: 100%;">
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" 
                                                                Text="Mã số" >
                                                            </dx:ASPxLabel>
                                                            <span style="color:Red">&nbsp;* </span>
                                                        </td>
                                                        <td class="style30">
                                                            <dx:ASPxImage ID="ASPxImage3" runat="server" Cursor="pointer" 
                                                                ImageAlign="Middle" ImageUrl="~/images/method.png" style="margin-left: 0px" 
                                                                ToolTip="" Height="20px" Width="20px">
                                                            </dx:ASPxImage>
                                                        </td>
                                                        <td class="style12">
                                                            <dx:ASPxTextBox ID="txtCode" runat="server" ClientInstanceName="txtCode" 
                                                                NullText="Tối đa 128 ký tự" 
                                                                 Width="200px" OnValidation="txtCode_Validation">
                                                                <NullTextStyle ForeColor="Silver">
                                                                </NullTextStyle>
                                                                <ValidationSettings ErrorText="" SetFocusOnError="True">
                                                                    <RequiredField ErrorText="Chưa nhập mã nguyên vật liệu" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa nhập m&#227; nguy&#234;n vật liệu"></RequiredField>
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td class="style12">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Trạng Thái" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="2">
                                                            <dx:ASPxComboBox ID="cboRowStatus" runat="server" NullText="Tự động tạo mới" 
                                                                Width="200px" ClientInstanceName="cboRowStatus">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Đang sử dụng" Value="A" />
                                                                    <dx:ListEditItem Text="Tạm ngưng sử dụng" Value="&quot;I&quot;" />
                                                                </Items>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" 
                                                                Text="Tên" >
                                                            </dx:ASPxLabel>
                                                            <span style="color:Red">&nbsp;* </span>
                                                        </td>
                                                        <td class="style5" colspan="2">
                                                            <dx:ASPxTextBox ID="txtName" runat="server" ClientInstanceName="txtName" 
                                                                NullText="255 ký tự, không cho phép trùng lắp" 
                                                                 Width="400px" OnValidation="txtName_Validation" 
                                                                MaxLength="255">
                                                                <NullTextStyle ForeColor="Silver">
                                                                </NullTextStyle>
                                                                <ValidationSettings SetFocusOnError="True" ErrorText="">
                                                                    <RequiredField ErrorText="Chưa nhập tên nguyên vật liệu" IsRequired="True" />
<RequiredField IsRequired="True" ErrorText="Chưa nhập t&#234;n nguy&#234;n vật liệu"></RequiredField>
                                                                </ValidationSettings>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            &nbsp;</td>
                                                        <td class="style11" colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" 
                                                                Text="Thể loại lưu trữ" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style11" colspan="2">
                                                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            &nbsp;</td>
                                                        <td class="style11" colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" 
                                                                Text="<%$ Resources:Resources, uItemEdit_ASPxLabel3_Text %>" >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td class="style5" colspan="2">
                                                            <dx:ASPxTextBox ID="txtDescription" runat="server" 
                                                                ClientInstanceName="txtDescription" Height="100px" 
                                                                NullText="1000 ký tự"  
                                                                Width="400px" MaxLength="1000">
                                                                <NullTextStyle ForeColor="Silver">
                                                                </NullTextStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            </td>
                                                        <td colspan="2" class="style5">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21">
                                                            <span style="color:Red">&nbsp;</span></td>
                                                        <td class="style30">
                                                            &nbsp;</td>
                                                        <td class="style27">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            &nbsp;</td>
                                                        <td colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                            </td>
                                                        <td colspan="2" class="style5">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style21" colspan="2">
                                                        </td>
                                                        <td class="style5" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7" colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26" colspan="4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" colspan="4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style26" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5" colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7" colspan="4">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style5" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style7" colspan="4">
                                                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Italic="True" 
                                                                ForeColor="Gray" Text="<%$ Resources:Resources, uItemEdit_ASPxLabel6_Text %>" 
                                                                >
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Thể loại kho">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                        <table cellpadding="0" cellspacing="0" class="dxflInternalEditorTable_DevEx">
                                            <tr>
                                                <td>
                                                    <table class="dxflInternalEditorTable_DevEx">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxGridView ID="grdBuyingProductCategory" runat="server" 
                                                                    AutoGenerateColumns="False" 
                                                                    OnCellEditorInitialize="grdBuyingProductCategory_CellEditorInitialize" 
                                                                    Width="100%">
                                                                    <Columns>
                                                                        <dx:GridViewDataComboBoxColumn Caption="Mã số" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="0" Width="150px" 
                                                                            FieldName="materialcategorycode">
                                                                            <PropertiesComboBox>
                                                                                <Columns>
                                                                                    <dx:ListBoxColumn Caption="Mã nhóm nguyên vật liệu" Width="150px" />
                                                                                    <dx:ListBoxColumn Caption="Tên nhóm nguyên vật liệu" Width="300px" />
                                                                                </Columns>
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Tên" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="1" Width="250px" 
                                                                            FieldName="categoryname">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Diễn Giải" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="2" Width="200px" FieldName="categorydescription">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Ghi Chú" ShowInCustomizationForm="True" 
                                                                            VisibleIndex="3">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" 
                                                                            ShowInCustomizationForm="True" VisibleIndex="4" Width="100px">
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
                                                                    </Columns>
                                                                    <SettingsPager PageSize="30">
                                                                    </SettingsPager>
                                                                    <Styles>
                                                                        <CommandColumn Spacing="10px">
                                                                        </CommandColumn>
                                                                    </Styles>
                                                                </dx:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <dx:ASPxButton ID="ASPxButton1" runat="server" Font-Bold="True" 
                                                                    Text="Thêm thể loại kho" Width="191px">
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Text="Cấu hình lưu trữ">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                        <table class="dxflInternalEditorTable_DevEx">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                                        Width="100%" KeyFieldName="OrganizationId" 
                                                        ParentFieldName="ParentOrganizationId">
                                                        <Columns>
                                                            <dx:TreeListTextColumn Caption="Mã" FieldName="code" ShowInCustomizationForm="True" 
                                                                VisibleIndex="0" Width="100px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Tên" FieldName="name" ShowInCustomizationForm="True" 
                                                                VisibleIndex="1" Width="300px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Bao gồm" FieldName="amount" ShowInCustomizationForm="True" 
                                                                VisibleIndex="2" Width="100px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="Diễn Giải" FieldName="Description" ShowInCustomizationForm="True" 
                                                                VisibleIndex="3" Width="300px">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListCommandColumn ButtonType="Image" ShowInCustomizationForm="True" 
                                                                VisibleIndex="3" Width="100px" Caption="Thao tác">
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
                                                            </dx:TreeListCommandColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedNode="True" />
                                                        <Styles>
                                                            <CommandButton Spacing="10px" VerticalAlign="Middle">
                                                            </CommandButton>
                                                        </Styles>
                                                    </dx:ASPxTreeList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Font-Bold="True" 
                                                        Text="Thêm đơn vị lưu trữ" Width="191px">
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                            <dx:TabPage Name="tabDetail" Text="Thông Tin Chi Tiết">
                                <ContentCollection>
                                    <dx:ContentControl ID="ContentControl4" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxHtmlEditor ID="ASPxHtmlEditor3" runat="server" Height="462px" 
                                            Width="100%">
                                            <Settings AllowHtmlView="False" AllowPreview="False" />
<Settings AllowHtmlView="False" AllowPreview="False"></Settings>
                                        </dx:ASPxHtmlEditor>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:TabPage>
                        </TabPages>
                    </dx:ASPxPageControl>
                    <table style="width:100%; margin-top:10px">
                        <tr>
                            <td>
                                <table align="right" style="width:100%;">
                                <tr>
                                    <td align="left" class="style25">
                                        <dx:ASPxButton ID="buttonHelp" runat="server" AutoPostBack="False" 
                                            Text="Trợ Giúp" >
                                            <Image ToolTip="Trợ giúp">
                                                <SpriteProperties CssClass="Sprite_Help" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonAccept" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonSave" Text="Lưu Lại" 
                                            >
                                            <ClientSideEvents Click="buttonSave_Click" />
<ClientSideEvents Click="buttonSave_Click"></ClientSideEvents>

                                            <Image ToolTip="Lưu">
                                                <SpriteProperties CssClass="Sprite_Apply" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td align="right">
                                        <dx:ASPxButton ID="buttonCancel" runat="server" AutoPostBack="False" 
                                            ClientInstanceName="buttonCancel" Text="Bỏ Qua" 
                                            >
                                            <ClientSideEvents Click="buttonCancel_Click" />
<ClientSideEvents Click="buttonCancel_Click"></ClientSideEvents>

                                            <Image ToolTip="Bỏ qua">
                                                <SpriteProperties CssClass="Sprite_Cancel" />
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>
</div>
<asp:SqlDataSource ID="ManufacturerSDS" runat="server" 
    ConnectionString="Data Source=192.168.1.120;Initial Catalog=ERPCORE;Integrated Security=True" 
    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
<asp:SqlDataSource ID="SupplierSDS" runat="server" 
    ConnectionString="Data Source=192.168.1.120;Initial Catalog=ERPCORE;Integrated Security=True">
</asp:SqlDataSource>
