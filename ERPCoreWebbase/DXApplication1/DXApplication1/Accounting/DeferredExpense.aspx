<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DeferredExpense.aspx.cs" Inherits="WebModule.Accounting.DeferredExpense" %>
<%@ Register src="UserControl/AccountingEntrylst.ascx" tagname="AccountingEntrylst" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainHolder" runat="server">
    <cc:ContentTitle ID="titlePage" runat="server" CssClass="content-title" 
    Style="display: block;" Title="Kết chuyển"
    TitleSize="26px" />
    
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
        <Items>
            <dx:LayoutItem Caption="Hệ thống T.K">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxComboBox ID="cbo_hethongtk" runat="server" ValueType="System.String" Width="170px">
                        </dx:ASPxComboBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Ngày">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxDateEdit ID="txt_date" runat="server">
                        </dx:ASPxDateEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" 
                        SupportsDisabledAttribute="True">
                        <dx:ASPxButton ID="btn_ketchuyen" runat="server" Text="Kết chuyển" 
                            OnClick="btn_ketchuyen_Click">
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <uc1:AccountingEntrylst ID="AccountingEntrylst1" runat="server"/>
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="btn_save" runat="server" Text="Lưu lại"/>
            </td>
            <td>
                <dx:ASPxButton ID="btn_cancel" runat="server" Text="Bỏ qua"/>
            </td>
        </tr>
    </table>
    
    
</asp:Content>
