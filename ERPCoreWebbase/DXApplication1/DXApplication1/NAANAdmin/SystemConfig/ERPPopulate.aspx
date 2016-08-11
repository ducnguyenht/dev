<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ERPPopulate.aspx.cs" Inherits="WebModule.NAANAdmin.SystemConfig.ERPPopulate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadHolder" runat="server">
    <script type="text/javascript">
        function btnExecute_Click(s, e) {
            cpPopulation.PerformCallback();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainHolder" runat="server">
    <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" ParentTitleSize="16px"
        Style="display: block;" Title="Nhập dữ liệu đầu" TitleSize="26px" />
    <dx:ASPxCallbackPanel ID="cpPopulation" ClientInstanceName="cpPopulation" runat="server"
        Width="100%" oncallback="cpPopulation_Callback">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxFormLayout ID="frmPopulation" runat="server">
                    <Items>
                        <dx:LayoutItem Caption="Thư mục chứa dữ liệu" RequiredMarkDisplayMode="Required">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server"
                                    SupportsDisabledAttribute="True">
                                    <dx:ASPxTextBox NullText="~/Populate" Text="~/Populate" ID="txtDataPath" runat="server" Width="170px">
                                        <ValidationSettings>
                                            <RequiredField IsRequired="true" ErrorText="Chưa nhập đường dẫn thư mục chứa dữ liệu" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:ASPxFormLayout>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
<asp:Content ID="CenterSubmitContainer" ContentPlaceHolderID="CenterSubmitContainer"
    runat="server">
    <dx:ASPxButton ID="btnExecute" runat="server" AutoPostBack="False" Text="Thực hiện">
        <Image>
            <SpriteProperties CssClass="Sprite_Apply" />
        </Image>
        <ClientSideEvents Click="btnExecute_Click" />
    </dx:ASPxButton>
</asp:Content>
