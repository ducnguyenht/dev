<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uPhaseEdit.ascx.cs"
    Inherits="WebModule.Produce.Config.UserControl.uPhaseEdit" %>
<style type="text/css">
    .float_right
    {
        float: right;
        margin-bottom: 10px;
        margin-top: 10px;
    }
    .float_left
    {
        float: left;
    }
    .dl
    {
        display: inline;
    }
    .mg
    {
        margin: 2px;
    }
    .dxpc-footerContent
    {
        width: 97% !important;
    }
    .footer_bt
    {
        height: 45px;
    }
    .style8
    {
        height: 18px;
    }
</style>
<div id="lineContainerPhase">
    <dx:aspxcallbackpanel id="cbpanelUserPhase" runat="server" width="100%" clientinstancename="cbpanelUserPhase"
        oncallback="cbpanelUserPhase_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                <dx:ASPxPopupControl ID="formPhaseEdit" runat="server" HeaderText="Thông Tin Công Đoạn -"
                    Height="650px" Modal="True" Width="1100px" ClientInstanceName="formPhaseEdit"
                    AllowDragging="True" RenderMode="Classic" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" ShowFooter="true" ShowSizeGrip="False" AllowResize="true"
                    ScrollBars="Auto" ShowMaximizeButton="True">
                    <FooterStyle CssClass="footer_bt" HorizontalAlign="Center" />
                    <FooterContentTemplate>
                        <div id="Footer" style="display: inline; width: 100%;">
                            <div style="display: inline; float: left">
                                <dx:ASPxButton ID="ASPxButton3" AutoPostBack="false" runat="server" CssClass="float_left dl mg"
                                    Text="Trợ Giúp" Wrap="False">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Help" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonCancelDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonCancelDevice" Text="Thoát" Wrap="False">
                                    <ClientSideEvents Click="buttonCancelDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Cancel" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                            <div style="display: inline; float: right;">
                                <dx:ASPxButton ID="buttonAcceptDevice" runat="server" AutoPostBack="False" CssClass="float_right dl mg"
                                    ClientInstanceName="buttonSaveDevice" Text="Lưu Lại" Wrap="False">
                                    <ClientSideEvents Click="buttonSaveDevice_Click" />
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </dx:ASPxButton>
                            </div>
                        </div>
                    </FooterContentTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" ColCount="2">
                                <Items>
                                    <dx:LayoutItem Caption="Mã Công Đoạn" RequiredMarkDisplayMode="Required">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" NullText="Tối đa 128 kí tự" 
                                                    Width="170px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tên Công Đoạn" RequiredMarkDisplayMode="Required">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" 
                                                    NullText="255 kí tự và không cho phép trùng lắp" Width="270px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Đơn Vị Thời Gian" RequiredMarkDisplayMode="Required">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" SelectedIndex="0">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="Ngày" Value="Ngay" />
                                                        <dx:ListEditItem Text="Giờ" Value="Gio" />
                                                        <dx:ListEditItem Text="Tháng" Value="Thang" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Thời Lượng Kế Hoạch">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" 
                                                    NullText="Nhập vào thời lượng kế hoạch" Width="270px">
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0"
                                Width="100%" 
                                OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged">
                                <TabPages>
                                    <%--<dx:TabPage Text="Thông Tin Chung">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxFormLayout runat="server" Width="100%">
                                                    <Items>
                                                        <dx:LayoutGroup ShowCaption="False">
                                                            <Items>
                                                                <dx:LayoutItem Caption="Mã Công Đoạn" RequiredMarkDisplayMode="Required">
                                                                    <layoutitemnestedcontrolcollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="tbPhaseID" runat="server" NullText="Tối đa 128 kí tự" 
                                                                                Width="170px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </layoutitemnestedcontrolcollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tên Công Đoạn" RequiredMarkDisplayMode="Required">
                                                                    <layoutitemnestedcontrolcollection>
                                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="tbPhaseName" runat="server" 
                                                                                NullText="255 kí tự và không cho phép trùng lắp" Width="270px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </layoutitemnestedcontrolcollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Đơn Vị Thời Gian" RequiredMarkDisplayMode="Required">
                                                                    <layoutitemnestedcontrolcollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" SelectedIndex="0">
                                                                                <Items>
                                                                                    <dx:ListEditItem Selected="True" Text="Ngày" Value="Ngay" />
                                                                                    <dx:ListEditItem Text="Giờ" Value="Gio" />
                                                                                    <dx:ListEditItem Text="Tháng" Value="Thang" />
                                                                                </Items>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </layoutitemnestedcontrolcollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Thời Lượng Kế Hoạch">
                                                                    <layoutitemnestedcontrolcollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxTextBox ID="tbTimePhase" runat="server" 
                                                                                NullText="Nhập vào thời lượng kế hoạch" Width="270px">
                                                                            </dx:ASPxTextBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </layoutitemnestedcontrolcollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Mô Tả">
                                                                    <layoutitemnestedcontrolcollection>
                                                                        <dx:LayoutItemNestedControlContainer runat="server" 
                                                                            SupportsDisabledAttribute="True">
                                                                            <dx:ASPxMemo runat="server" Height="100px" Width="400px">
                                                                            </dx:ASPxMemo>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </layoutitemnestedcontrolcollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                        </dx:LayoutGroup>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                                                   <div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>--%>
                                    <dx:TabPage Text="Nguyên Liệu Đầu Vào">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel1" runat="server" ScrollBars="Vertical" Height="300px"
                                                    Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" Height="300px" RenderMode="Lightweight"
                                                                Style="margin-top: 6px" Width="100%">
                                                                <Groups>
                                                                    <dx:NavBarGroup Text="Nguyên Vật Liệu">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxGridView ID="grdataUserPhaseMaterial" runat="server" AutoGenerateColumns="False"
                                                                                        KeyFieldName="PhaseMaterialID" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataComboBoxColumn Caption="Mã Nguyên Vật Liệu" FieldName="PhaseMaterialID"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            </dx:GridViewDataComboBoxColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Nguyên Vật Liệu" FieldName="PhaseMaterialName"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="PhaseMaterialUnit" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="PhaseMaterialQuantity" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Giá" FieldName="Price" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="4">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Thành Tiền" FieldName="Total" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="5">
                                                                                                <FooterTemplate>
                                                                                                    .........
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tỉ lệ hao hụt (%)" FieldName="LossRate" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="6">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="7">
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
                                                                                        </Columns>
                                                                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                            </Header>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </Template>
                                                                            </dx:NavBarItem>
                                                                        </Items>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Text="Sản Phẩm Dở Dang">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxGridView ID="grdataUserUnFinishedProduct" runat="server" AutoGenerateColumns="False"
                                                                                        KeyFieldName="PhaseUnFinishedProductID" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="Mã Sản Phẩm Dang Dở" FieldName="PhaseUnFinishedProductID"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Sản Phẩm Dang Dở" FieldName="PhaseUnFinishedProductName"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                <FooterTemplate>
                                                                                                    Cộng
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="PhaseUnFinishedUnit" 
                                                                                                VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="PhaseUnFinishedQuantity" 
                                                                                                VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Giá" FieldName="Price" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="4">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Thành Tiền" FieldName="Total" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="5">
                                                                                                <FooterTemplate>
                                                                                                    .........
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tỉ lệ hao hụt (%)" FieldName="LossRate" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="6">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="7">
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
                                                                                        </Columns>
                                                                                        <Settings ShowFilterRow="True" ShowFooter="true" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                            </Header>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </Template>
                                                                            </dx:NavBarItem>
                                                                        </Items>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Text="Hàng Hóa">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxGridView ID="grdataUserPhaseProduct" runat="server" AutoGenerateColumns="False"
                                                                                        KeyFieldName="PhaseProductID" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="PhaseProductID" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="PhaseProductName" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="1">
                                                                                                <FooterTemplate>
                                                                                                    Cộng
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="PhaseProductDescription"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                             <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="PhaseProductUnit" 
                                                                                                VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="PhaseProductQuantity" 
                                                                                                VisibleIndex="4">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Giá" FieldName="Price" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="5">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Thành Tiền" FieldName="Total" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="5">
                                                                                                <FooterTemplate>
                                                                                                    .........
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tỉ Lệ Hao Hụt (%)" FieldName="LossRate" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="6">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                           <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="7">
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
                                                                                        </Columns>
                                                                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowFooter="true"/>
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                            </Header>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </Template>
                                                                            </dx:NavBarItem>
                                                                        </Items>
                                                                    </dx:NavBarGroup>
                                                                </Groups>
                                                            </dx:ASPxNavBar>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                                <%--<div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>--%>
                                            </dx:ContentControl>                                             
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Nhân Lực">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel5" runat="server" ScrollBars="Vertical" Height="300px"
                                                    Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent5" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxGridView ID="grdataUserPeople" runat="server" AutoGenerateColumns="False"
                                                                Width="100%" KeyFieldName="BacNghe">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Bậc Nghề" FieldName="BacNghe" ShowInCustomizationForm="True"
                                                                        VisibleIndex="0">
                                                                        <FooterTemplate>
                                                                            Cộng
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                                                        VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Thời Lượng" FieldName="ThoiLuong" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="Price" ShowInCustomizationForm="True"
                                                                        VisibleIndex="3">
                                                                        <FooterTemplate>
                                                                            .........
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                                                        VisibleIndex="4">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                        VisibleIndex="5">
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
                                                                </Columns>
                                                                <Settings ShowFilterRow="True" ShowFooter="true" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>
                                                                <Styles>
                                                                    <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    </Header>
                                                                </Styles>
                                                            </dx:ASPxGridView>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                                <%--<div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabelw41" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>--%>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Nhà Xưởng Máy Móc">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel4" runat="server" ScrollBars="Vertical" Height="300px"
                                                    Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent4" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxGridView ID="grdataUserCompany" runat="server" AutoGenerateColumns="False"
                                                                Width="100%" KeyFieldName="code">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Mã Nhà Xương Máy Móc" FieldName="code" ShowInCustomizationForm="True"
                                                                        VisibleIndex="0">
                                                                        <FooterTemplate>
                                                                            Cộng
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Tên Nhà Xướng Máy Móc" FieldName="name" ShowInCustomizationForm="True"
                                                                        VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Thời Lượng" FieldName="Time" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Công Suất" FieldName="CongSuat" ShowInCustomizationForm="True"
                                                                        VisibleIndex="3">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Chi Phí Vận Hành" FieldName="ChiPhiVanHanh" ShowInCustomizationForm="True"
                                                                        VisibleIndex="4">
                                                                        <FooterTemplate>
                                                                            .........
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                                                        VisibleIndex="6">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                        VisibleIndex="7">
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
                                                                </Columns>
                                                                <Settings ShowFilterRowMenu="True" ShowFooter="true" ShowHeaderFilterButton="True" ShowFilterRow="True" />

            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>

                                                                <Styles>
                                                                    <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    </Header>
                                                                </Styles>
                                                            </dx:ASPxGridView>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                                <%--<div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabe3rel4" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>--%>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Công Cụ Dụng Cụ">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel3" runat="server" ScrollBars="Vertical" Height="300px"
                                                    Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxGridView ID="grdataUserDevice" runat="server" AutoGenerateColumns="False"
                                                                Width="100%" KeyFieldName="code">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="Mã Công Cụ Dụng Cụ" FieldName="code" ShowInCustomizationForm="True"
                                                                        VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Tên Công Cụ Dụng Cụ" FieldName="name" ShowInCustomizationForm="True"
                                                                        VisibleIndex="1">
                                                                        <FooterTemplate>
                                                                            Cộng
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="DonViTinh" ShowInCustomizationForm="True"
                                                                        VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                                                        VisibleIndex="3">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Đơn Giá" FieldName="Price" ShowInCustomizationForm="True"
                                                                        VisibleIndex="4">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Thành Tiền" FieldName="Total" ShowInCustomizationForm="True"
                                                                        VisibleIndex="5">
                                                                        <FooterTemplate>
                                                                            .........
                                                                        </FooterTemplate>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                                                        VisibleIndex="6">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                        VisibleIndex="7">
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
                                                                </Columns>
                                                                <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"></Settings>
                                                                <Styles>
                                                                    <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    </Header>
                                                                </Styles>
                                                            </dx:ASPxGridView>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                                <%--<div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabfdeld4" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>--%>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="Đầu Ra">
                                        <ContentCollection>
                                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxPanel ID="ASPxPanel2" runat="server" ScrollBars="Vertical" Height="300px"
                                                    Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                            <dx:ASPxNavBar ID="ASPxNavBar2" runat="server" RenderMode="Lightweight" Height="300px"
                                                                Width="100%">
                                                                <Groups>
                                                                    <dx:NavBarGroup Text="Sản Phẩm Dở Dang">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxGridView ID="grdataUserUnFinishedProductOut" runat="server" AutoGenerateColumns="False"
                                                                                        KeyFieldName="PhaseUnFinishedProductID" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="Mã Sản Phẩm Dang Dở" FieldName="PhaseUnFinishedProductID"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Sản Phẩm Dang Dở" FieldName="PhaseUnFinishedProductName"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                <FooterTemplate>
                                                                                                    Cộng
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" 
                                                                                                FieldName="PhaseUnFinishedProductUnit" VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng" 
                                                                                                FieldName="PhaseUnFinishedProductQuantity" VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Chi Phí" 
                                                                                                FieldName="Price" VisibleIndex="4">
                                                                                                <FooterTemplate>
                                                                                                    .........
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="5">
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
                                                                                        </Columns>
                                                                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFooter="true" ShowHeaderFilterButton="True" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                            </Header>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </Template>
                                                                            </dx:NavBarItem>
                                                                        </Items>
                                                                    </dx:NavBarGroup>
                                                                    <dx:NavBarGroup Text="Hàng Hóa">
                                                                        <Items>
                                                                            <dx:NavBarItem>
                                                                                <Template>
                                                                                    <dx:ASPxGridView ID="grdataUserPhaseProductOut" runat="server" AutoGenerateColumns="False"
                                                                                        KeyFieldName="PhaseProductID" Width="100%">
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="PhaseProductID" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="0">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="PhaseProductName" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="1">
                                                                                                <FooterTemplate>
                                                                                                    Cộng
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Diễn Giải" FieldName="PhaseProductDescription"
                                                                                                ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Đơn Vị Tính" FieldName="PhaseProductUnit" 
                                                                                                VisibleIndex="2">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Số Lượng" FieldName="PhaseProductQuantity" 
                                                                                                VisibleIndex="3">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Chi Phí" 
                                                                                                FieldName="Price" VisibleIndex="4">
                                                                                                <FooterTemplate>
                                                                                                    .........
                                                                                                </FooterTemplate>
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao Tác" ShowInCustomizationForm="True"
                                                                                                VisibleIndex="5">
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
                                                                                        </Columns>
                                                                                        <Settings ShowFilterRow="True" ShowFooter="true" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                                                                                        <Styles>
                                                                                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                                            </Header>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </Template>
                                                                            </dx:NavBarItem>
                                                                        </Items>
                                                                    </dx:NavBarGroup>
                                                                </Groups>
                                                            </dx:ASPxNavBar>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxPanel>
                                                <%--<div class="quickHelp">
                                                    <dx:ASPxLabel ID="ASPxLabel41" runat="server" Font-Italic="False" ForeColor="Gray"
                                                        Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                                        Font-Bold="False" Font-Size="XX-Small">
                                                    </dx:ASPxLabel>
                                                </div>--%>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>    
                            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" Width="100%" ColCount="2">
                                <Items>
                                    <dx:LayoutItem Caption="Tổng chi phí nguyên liệu đầu vào">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="lblFee" runat="server" Text="........."
                                                    Width="170px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng chi phí nhân lực">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="........."
                                                    Width="170px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng chi phí nhà xưởng máy móc">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="........."
                                                    Width="170px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng chi phí công cụ dụng cụ">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="........."
                                                    Width="170px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Tổng chi phí">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="........."
                                                    Width="170px">
                                                </dx:ASPxLabel>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                    <dx:LayoutItem Caption="Mô Tả" ColSpan="2">
                                        <layoutitemnestedcontrolcollection>
                                            <dx:LayoutItemNestedControlContainer runat="server" 
                                                SupportsDisabledAttribute="True">
                                                <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="25px" Width="100%">
                                                </dx:ASPxMemo>
                                            </dx:LayoutItemNestedControlContainer>
                                        </layoutitemnestedcontrolcollection>
                                    </dx:LayoutItem>
                                </Items>
                            </dx:ASPxFormLayout>
                            <div class="quickHelp">
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Italic="False" ForeColor="Gray"
                                    Text="(*) là trường bắt buộc | Ctrl + S - Lưu | Alt + F4 - Thoát | F1 - Trợ Giúp"
                                    Font-Bold="False" Font-Size="XX-Small">
                                </dx:ASPxLabel>
                            </div>                 
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </dx:PanelContent>
        </PanelCollection>
    </dx:aspxcallbackpanel>
</div>
