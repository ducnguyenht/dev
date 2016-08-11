<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ArtifactCodeRuleListing.aspx.cs" Inherits="WebModule.ERPSystem.ArtifactCode.GUI.ArtifactCodeRuleListing" %>
<%@ Register src="ArtifactCodeRuleEditingForm.ascx" tagname="ArtifactCodeRuleEditingForm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {

            $(ArtifactCodeRuleEditingForm).on(
                ArtifactCodeRuleEditingForm.events.eClosing, 
                function (evt) {
                    gridArtifactCodeRule.Refresh();
                }
            );

        });

        function imgEmptyDataNewCommand_Click(s, e) {
            ArtifactCodeRuleEditingForm.Show();
        }

        function gridArtifactCodeRule_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case 'New':
                    ArtifactCodeRuleEditingForm.Show();
                    break;
                case 'Edit':
                    var recordId = s.GetRowKey(e.visibleIndex);
                    ArtifactCodeRuleEditingForm.Show(recordId);
                    break;
                case 'Delete':
                    var confirmMessage = confirm("Bạn có chắc chắn muốn xóa bản ghi này không?");
                    if (confirmMessage == true) {
                        var args = 'Delete|';
                        args += s.GetRowKey(e.visibleIndex);
                        gridArtifactCodeRule.PerformCallback(args)
                    }
                    break;
                default:
                    break;
            }
        }

        function gridArtifactCodeRule_EndCallback(s, e) {
            if (s.cpEvent) {
                if (s.cpEvent == 'deleted') {
                    gridArtifactCodeRule.Refresh();
                }
                delete s.cpEvent;
            }
        } 

    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <div style="padding:10px">
        
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" 
            Font-Names="Segoe UI" Font-Size="Medium" 
            Text="Danh Mục Phương Pháp Tạo Mã Tự Động">
        </dx:ASPxLabel>
        
    </div>
    <dx:ASPxGridView ID="gridArtifactCodeRule" runat="server" ClientInstanceName="gridArtifactCodeRule"
        AutoGenerateColumns="False" DataSourceID="dsArtifactCodeRule" 
        KeyFieldName="ArtifactCodeRuleId" Width="100%" 
        oncustomcallback="gridArtifactCodeRule_CustomCallback">
        <ClientSideEvents EndCallback="gridArtifactCodeRule_EndCallback" CustomButtonClick="gridArtifactCodeRule_CustomButtonClick" />
        <Templates>
            <EmptyDataRow>
                <dx:ASPxImage ID="imgEmptyDataNewCommand" Cursor="pointer" runat="server" SpriteCssClass="Sprite_New"
                    ShowLoadingImage="true">
                    <ClientSideEvents Click="imgEmptyDataNewCommand_Click" />
                </dx:ASPxImage>
                <br />
                <dx:ASPxLabel ID="lblGridviewEmptyText" runat="server" Text="No data to display">
                </dx:ASPxLabel>
            </EmptyDataRow>
        </Templates>
        <Columns>
            <dx:GridViewDataTextColumn Caption="Loại chứng từ" 
                FieldName="ArtifactTypeId.Name" VisibleIndex="2"> 
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Tên phương pháp" FieldName="Name" 
                VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ngày tạo" FieldName="CreateDate" 
                VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ButtonType="Image" Caption="Thao tác" 
                VisibleIndex="4">
                <ClearFilterButton Visible="True">
                    <Image>
                        <SpriteProperties CssClass="Sprite_Clear" />
                    </Image>
                </ClearFilterButton>
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="New">
                        <Image ToolTip="Thêm">
                            <SpriteProperties CssClass="Sprite_New" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Edit">
                        <Image ToolTip="Sửa">
                            <SpriteProperties CssClass="Sprite_Edit" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Delete">
                        <Image ToolTip="Xóa">
                            <SpriteProperties CssClass="Sprite_Delete" />
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterRowMenu="True" />
        <Styles>
            <Header HorizontalAlign="Center">
            </Header>
            <CommandColumn Spacing="4px">
            </CommandColumn>
        </Styles>
        <BorderLeft BorderWidth="0px" />
        <BorderRight BorderWidth="0px" />
    </dx:ASPxGridView>
    <dx:XpoDataSource ID="dsArtifactCodeRule" runat="server" 
        Criteria="[ArtifactTypeId.OrganizationId.OrganizationId] = ? And [RowStatus] &gt; 0s" 
        TypeName="NAS.DAL.System.ArtifactCode.ArtifactCodeRule">
        <CriteriaParameters>
            <asp:Parameter Name="OrganizationId" />
        </CriteriaParameters>
    </dx:XpoDataSource>
    <uc1:ArtifactCodeRuleEditingForm ID="ArtifactCodeRuleEditingForm1" 
        runat="server" />
</asp:Content>