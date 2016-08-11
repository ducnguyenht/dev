<%@ Page MasterPageFile="~/Site.master" Language="C#" AutoEventWireup="true" CodeBehind="AllocationListingPage.aspx.cs"
    Inherits="WebModule.Accounting.AllocationConfigure.GUI.AllocationListing" %>

<%@ Register Src="AllocationEditingForm.ascx" TagName="AllocationEditingForm" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $(AllocationEditingForm).on(
                AllocationEditingForm.events.eClosing,
                function (evt) {
                    gridviewAllocation.Refresh();
                }
            );

        });

        function gridviewAllocation_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case 'New':
                    AllocationEditingForm.Show();
                    break;
                case 'Edit':
                    var recordId = s.GetRowKey(e.visibleIndex);
                    AllocationEditingForm.Show(recordId);
                    break;
                case 'Delete':
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'Delete|';
                        args += s.GetRowKey(e.visibleIndex);
                        gridviewAllocation.PerformCallback(args)
                    }
                    break;
                default:
                    break;
            }
        }

        function btnEmptyDataNewCommand_Click(s, e) {
            AllocationEditingForm.Show();
        }

        function gridviewAllocation_EndCallback(s, e) {
            if (s.cpEvent) {
                if (s.cpEvent == 'deleted') {
                    gridviewAllocation.Refresh();
                }
                delete s.cpEvent;
            }
        } 

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxGridView ID="gridviewAllocation" ClientInstanceName="gridviewAllocation"
        runat="server" DataSourceID="dsAllocation" AutoGenerateColumns="False" KeyFieldName="AllocationId"
        Width="100%" oncustomcallback="gridviewAllocation_CustomCallback">
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center">
            </Header>
        </Styles>
        <Templates>
            <EmptyDataRow>
                <dx:ASPxImage ID="imgEmptyDataNewCommand" Cursor="pointer" runat="server" SpriteCssClass="Sprite_New"
                    ShowLoadingImage="true">
                    <ClientSideEvents Click="btnEmptyDataNewCommand_Click" />
                </dx:ASPxImage>
                <br />
                <dx:ASPxLabel ID="lblGridviewEmptyText" runat="server" Text="No data to display">
                </dx:ASPxLabel>
            </EmptyDataRow>
        </Templates>
        <ClientSideEvents CustomButtonClick="gridviewAllocation_CustomButtonClick" EndCallback="gridviewAllocation_EndCallback" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Code" VisibleIndex="0" Caption="Mã phân bổ"
                Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3" Caption="Mô tả">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Caption="Tên phân bổ">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Loại phân bổ" FieldName="AllocationTypeId!Key"
                VisibleIndex="2" Width="200px">
                <PropertiesComboBox CallbackPageSize="10" DataSourceID="dsAllocationType" EnableCallbackMode="True"
                    IncrementalFilteringMode="Contains" TextField="Name" ValueField="AllocationTypeId"
                    ValueType="System.Guid">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" VisibleIndex="4"
                Width="100px">
                <ClearFilterButton Visible="True">
                    <Image ToolTip="Hủy">
                        <SpriteProperties CssClass="Sprite_Clear"></SpriteProperties>
                    </Image>
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Edit">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="New">
                        <Image ToolTip="Thêm">
                            <SpriteProperties CssClass="Sprite_New"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Delete">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete"></SpriteProperties>
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="dsAllocation" runat="server" TypeName="NAS.DAL.Accounting.Configure.Allocation">
    </dx:XpoDataSource>
    <dx:XpoDataSource ID="dsAllocationType" runat="server" TypeName="NAS.DAL.Accounting.Configure.AllocationType">
    </dx:XpoDataSource>
    <uc1:AllocationEditingForm ID="AllocationEditingForm1" runat="server" />
</asp:Content>
