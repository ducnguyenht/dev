<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="MovingSheet.aspx.cs" Inherits="WebModule.Warehouse.Command.MovingSheet" %>
<%@ Register src="~/Warehouse/Command/PopupCommand/EdittingMovingInventoryCommand/uEdittingMovingInventoryCommand.ascx" 
    tagname="uEdittingMovingInventoryCommand" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <script type="text/javascript">
        var target = new EventTarget();
        function handleEvent(event) {
            grdInventoryCommand.Refresh();
        };
        target.addListener("SharedClientEvent", handleEvent);
    </script>
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Danh sách phiếu chuyển kho" Font-Bold="True"
        Font-Size="Small">
    </dx:ASPxLabel>
    <dx:ASPxGridView ID="grdInventoryCommand" 
        ClientInstanceName="grdInventoryCommand" runat="server" AutoGenerateColumns="False" 
        DataSourceID="InventoryCommandXDS" KeyFieldName="InventoryCommandId" 
        Width="100%" onload="grdInventoryCommand_Load">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã phiếu chuyển" FieldName="Code" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên phiếu" FieldName="Name" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày lập" FieldName="IssueDate" 
                VisibleIndex="3" Width="200px">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="Diễn giải" FieldName="Description" 
                VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="5">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Edit">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Edit"/>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Add">
                        <Image>
                            <SpriteProperties CssClass="Sprite_New"/>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Delete">
                        <Image>
                            <SpriteProperties CssClass="Sprite_Delete"/>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Templates>
            <EmptyDataRow>
                <center>
                    <dx:ASPxButton ID="btnEmptyInventoryCommandRow" runat="server" Visible="true" 
                        AutoPostBack="false" onload="btnEmptyInventoryCommandRow_Load">
                        <Image AlternateText="Thêm mới">
                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
		                </Image>
                    </dx:ASPxButton>
                </center>
            </EmptyDataRow>
        </Templates>
        <Settings ShowFilterRow="true" GridLines="Both" />
        <SettingsBehavior AllowSort="true" AllowSelectSingleRowOnly="true" />
        <SettingsText EmptyDataRow="Chưa có phiếu kho" />
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="InventoryCommandXDS" runat="server" DefaultSorting="" 
        TypeName="NAS.DAL.Inventory.Command.InventoryCommand" 
        Criteria="[RowStatus] = 1 And [CommandType] = 'M'">
    </dx:XpoDataSource>
    <uc1:uEdittingMovingInventoryCommand ID="uEdittingMovingInventoryCommand1" runat="server" />
</asp:Content>

