<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="LegalInvoiceArtifact.aspx.cs" Inherits="WebModule.Accounting.LegalInvoiceArtifact" %>
<%@ Register src="UserControl/uLegalInvoiceArtifact/Page/uLegalInvoiceArtifact.ascx" tagname="uLegalInvoiceArtifact" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function GridLegalInvoiceArtifact_CustomButtonClick(s, e) {
            switch (e.buttonID) {
                case 'Create':
                    cpLegalInvoiceArtifact.PerformCallback('Create');
                    break;
                case 'Edit':
                    var key = 'Edit|' + s.GetRowKey(e.visibleIndex);
                    cpLegalInvoiceArtifact.PerformCallback(key);
                    break;
                case 'Delete':
                    var key = 'Delete|' + s.GetRowKey(e.visibleIndex);
                    cpLegalInvoiceArtifact.PerformCallback(key);
                    break;
                default:
                    break;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <dx:ASPxPageControl ID="pageLegalInvoiceArtifact" RenderMode="Lightweight" runat="server"
        ActiveTabIndex="0" Width="100%" ContentStyle-HorizontalAlign="Center" ClientInstanceName="pageLegalInvoiceArtifact">
        <TabPages>
            <dx:TabPage Text="Hóa Đơn GTGT">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxGridView ID="GridLegalInvoiceArtifact" runat="server" AutoGenerateColumns="False"
                            DataSourceID="DBLegalInvoiceArtifact" 
                            KeyFieldName="LegalInvoiceArtifactId" Width="100%"
                            ClientInstanceName="GridLegalInvoiceArtifact" >
                            <ClientSideEvents CustomButtonClick="GridLegalInvoiceArtifact_CustomButtonClick" />
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="LegalInvoiceArtifactId" ReadOnly="True" Visible="False"
                                    VisibleIndex="0">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Số" FieldName="Code" VisibleIndex="1"
                                    Width="10%">
                                    <PropertiesTextEdit>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="Bắt buộc nhập" IsRequired="True" />
                                        </ValidationSettings>
                                    </PropertiesTextEdit>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="Ngày Tạo" Name="IssuedDate" FieldName="CreateDate"
                                    VisibleIndex="2" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Diễn Giải" FieldName="Description" VisibleIndex="3">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Caption="Nơi Xuất"  VisibleIndex="4">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="Nơi Nhận"  VisibleIndex="5">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Phân Loại"  VisibleIndex="6">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Số Lần In"  VisibleIndex="7">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Kê Khai"  VisibleIndex="8">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="Thao Tác" VisibleIndex="9" ButtonType="Image"
                                    AllowDragDrop="True" Width="10%">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Create">
                                            <Image Url="~/images/icon/16x16/ico-add.png">
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="Edit">
                                            <Image Url="~/images/icon/16x16/ico-edit.png">
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                         <dx:GridViewCommandColumnCustomButton ID="Delete">
                                            <Image Url="~/images/icon/16x16/ico-delete.png">
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </dx:GridViewCommandColumn>
                            </Columns>
                            <SettingsEditing Mode="Inline" />
                            <SettingsPager PageSize="20" Position="Bottom">
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <SettingsBehavior ConfirmDelete="True" ColumnResizeMode="NextColumn" 
                                AllowFocusedRow="True" EnableCustomizationWindow="True" />
                            <SettingsText ConfirmDelete="Bạn Có Chắc Muốn Xóa Không?" />
                            <Settings ShowFilterRow="True" />
                            <SettingsCookies StoreControlWidth="True" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
        <ContentStyle HorizontalAlign="Center">
        </ContentStyle>
    </dx:ASPxPageControl>
    <uc1:uLegalInvoiceArtifact ID="uLegalInvoiceArtifact1" runat="server" />
    <dx:XpoDataSource ID="DBLegalInvoiceArtifact" runat="server" DefaultSorting="" 
        TypeName="NAS.DAL.Accounting.LegalInvoice.LegalInvoiceArtifact" 
        Criteria="[RowStatus] &gt; 0s">
    </dx:XpoDataSource>
</asp:Content>

