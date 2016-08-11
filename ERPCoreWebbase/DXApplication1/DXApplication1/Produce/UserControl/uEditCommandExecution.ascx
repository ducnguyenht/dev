<%@ Control Language="C#" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="uEditCommandExecution.ascx.cs"
    Inherits="WebModule.Produce.UserControl.uEditCommandExecution" %>
<dx:ASPxPageControl ID="pc_editCommand" runat="server" ActiveTabIndex="0" Width="100%"
    Height="100%">
    <TabPages>
        <dx:TabPage Text="Đầu Ra">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxPanel ID="ASPxPanel1" runat="server" ScrollBars="Vertical" Height="400px"
                        Width="100%">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxNavBar ID="nvbOutput" runat="server" Height="500px" Width="100%">
                                    <Groups>
                                        <dx:NavBarGroup Text="Sản phẩm dở dang">
                                            <Items>
                                                <dx:NavBarItem>
                                                    <Template>
                                                        <dx:ASPxGridView ID="grdataExecutionUnFinishedProductOut" runat="server" AutoGenerateColumns="False"
                                                            KeyFieldName="ExecutionUnFinishedProductID" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6">
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
                                                                    <CancelButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <UpdateButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã sản phẩm dở dang" FieldName="ExecutionUnFinishedProductID"
                                                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên sản phẩm dở dang" FieldName="ExecutionUnFinishedProductName"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="ExecutionUnFinishedParcel"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="ExecutionUnFinishedQuantity"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ExecutionUnFinishedUnit"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ExecutionUnFinishedCost"
                                                                    VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
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
                                        <dx:NavBarGroup Text="Hàng hóa">
                                            <Items>
                                                <dx:NavBarItem>
                                                    <Template>
                                                        <dx:ASPxGridView ID="grdataExecutionProductOut" runat="server" AutoGenerateColumns="False"
                                                            KeyFieldName="ExecutionProductID" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6">
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
                                                                    <CancelButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <UpdateButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã hàng hóa" FieldName="ExecutionProductID" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên hàng hóa" FieldName="ExecutionProductName"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="ExecutionProductDescription"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="ExecutionProductQuantity"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ExecutionProductUnit"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ExecutionProductCost" VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
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
                                    </Groups>
                                </dx:ASPxNavBar>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Đầu vào">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxPanel ID="panelInputExecution" runat="server" Width="100%" Height="400px"
                        ScrollBars="Vertical">
                        <PanelCollection>
                            <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxNavBar ID="nvbInput" runat="server" RenderMode="Lightweight" Width="100%"
                                    Height="450px">
                                    <Groups>
                                        <dx:NavBarGroup Text="Nguyên vật liệu">
                                            <Items>
                                                <dx:NavBarItem>
                                                    <Template>
                                                        <dx:ASPxGridView ID="grdataExecutionMaterial" runat="server" AutoGenerateColumns="False"
                                                            KeyFieldName="ExecutionMaterialID" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
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
                                                                    <CancelButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <UpdateButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataComboBoxColumn Caption="Mã nguyên vật liệu" FieldName="ExecutionMaterialID"
                                                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên nguyên vật liệu" FieldName="ExecutionMaterialName"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ExecutionMaterialUnit"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="ExecutionMaterialQuantity"
                                                                    ShowInCustomizationForm="True" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ExecutionMaterialCost" VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
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
                                        <dx:NavBarGroup Text="Sản phẩm dở dang">
                                            <Items>
                                                <dx:NavBarItem>
                                                    <Template>
                                                        <dx:ASPxGridView ID="grdataExecutionUnFinishedProduct" runat="server" AutoGenerateColumns="False"
                                                            KeyFieldName="ExecutionUnFinishedProductID" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6">
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
                                                                    <CancelButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <UpdateButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã sản phẩm dở dang" FieldName="ExecutionUnFinishedProductID"
                                                                    ShowInCustomizationForm="True" VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên sản phẩm dở dang" FieldName="ExecutionUnFinishedProductName"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="ExecutionUnFinishedParcel"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="ExecutionUnFinishedQuantity"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ExecutionUnFinishedUnit"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ExecutionUnFinishedCost"
                                                                    VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
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
                                        <dx:NavBarGroup Text="Hàng Hóa">
                                            <Items>
                                                <dx:NavBarItem>
                                                    <Template>
                                                        <dx:ASPxGridView ID="grdataExecutionProduct" runat="server" AutoGenerateColumns="False"
                                                            KeyFieldName="ExecutionProductID" Width="100%">
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                                                    VisibleIndex="6">
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
                                                                    <CancelButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Cancel" />
                                                                        </Image>
                                                                    </CancelButton>
                                                                    <UpdateButton Visible="True">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Apply" />
                                                                        </Image>
                                                                    </UpdateButton>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="Mã Hàng Hóa" FieldName="ExecutionProductID" ShowInCustomizationForm="True"
                                                                    VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Tên Hàng Hóa" FieldName="ExecutionProductName"
                                                                    ShowInCustomizationForm="True" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lô" FieldName="ExecutionProductDescription"
                                                                    ShowInCustomizationForm="True" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="ExecutionProductQuantity"
                                                                    VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="ExecutionProductUnit"
                                                                    VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ExecutionProductCost" VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
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
                                    </Groups>
                                </dx:ASPxNavBar>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nhân lực">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdataExecutionPeople" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="BacNghe">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                VisibleIndex="5">
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
                                    </Image>
                                </CancelButton>
                                <UpdateButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </UpdateButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Bậc nghề" FieldName="BacNghe" ShowInCustomizationForm="True"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thời lượng" FieldName="ThoiLuong" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ChiPhi" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                        </Settings>
                        <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Nhà Xưởng Máy Móc">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdataExecutionCompany" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="code">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                VisibleIndex="6">
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
                                    </Image>
                                </CancelButton>
                                <UpdateButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </UpdateButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Mã nhà xưởng máy móc" FieldName="code" ShowInCustomizationForm="True"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên nhà xưởng máy móc" FieldName="name" ShowInCustomizationForm="True"
                                VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Thời lượng" FieldName="Time" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Công suất" FieldName="CongSuat" ShowInCustomizationForm="True"
                                VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Chi phí vận hành" FieldName="ChiPhiVanHanh" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowFilterRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                        </Settings>
                        <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Công Cụ Dụng Cụ">
            <ContentCollection>
                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                    <dx:ASPxGridView ID="grdataExecutionDevice" runat="server" AutoGenerateColumns="False"
                        Width="100%" KeyFieldName="code">
                        <Columns>
                            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" ShowInCustomizationForm="True"
                                VisibleIndex="12">
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
                                    </Image>
                                </CancelButton>
                                <UpdateButton Visible="True">
                                    <Image>
                                        <SpriteProperties CssClass="Sprite_Apply" />
                                    </Image>
                                </UpdateButton>
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="Mã  công cụ dụng cụ" FieldName="code" ShowInCustomizationForm="True"
                                VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Tên  công cụ dụng cụ" FieldName="name" ShowInCustomizationForm="True"
                                VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Số lượng" FieldName="SoLuong" ShowInCustomizationForm="True"
                                VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Đơn vị tính" FieldName="DonViTinh" ShowInCustomizationForm="True"
                                VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GhiChu" ShowInCustomizationForm="True"
                                VisibleIndex="8">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Chi phí" FieldName="ChiPhi" ShowInCustomizationForm="True"
                                VisibleIndex="10">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <Settings ShowFilterRowMenu="True" ShowHeaderFilterButton="True" ShowFilterRow="True" />
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True">
                        </Settings>
                        <Styles>
                            <Header Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
