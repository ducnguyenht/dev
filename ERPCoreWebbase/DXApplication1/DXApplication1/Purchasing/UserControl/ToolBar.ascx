<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToolBar.ascx.cs" Inherits="WebModule.Purchasing.UserControl.ToolBar" %>
<dx:ASPxRoundPanel SkinID="None" ID="ASPxRoundPanel1" runat="server" 
    ShowHeader="False" Height="10px" Width="160px">
    <ContentPaddings Padding="0px" />
    <PanelCollection>
        <dx:PanelContent runat="server">
            <dx:ASPxMenu SkinID="None" ID="mnuToolbar" runat="server" SeparatorWidth="2px" BackColor="Transparent"
                SeparatorHeight="10px" ItemSpacing="1px">
                <SeparatorBackgroundImage ImageUrl=""
                    Repeat="NoRepeat" VerticalPosition="center" />
                <Border BorderWidth="0px" />
                <ClientSideEvents ItemClick="ToolBar_ItemClick" />
                <Paddings Padding="0px" PaddingLeft="2px" />
                <ItemStyle Wrap="False">
                    <Paddings Padding="3px" PaddingLeft="4px" />
                    <HoverStyle>
                        <BackgroundImage ImageUrl=""
                            Repeat="RepeatX" />
                        <Border BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                    </HoverStyle>
                    <CheckedStyle>
                        <BackgroundImage ImageUrl=""
                            Repeat="RepeatX" />
                        <Border BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" />
                    </CheckedStyle>
                </ItemStyle>
                <ItemImage Height="16px" Width="16px" />
                <SeparatorPaddings PaddingLeft="2px" PaddingRight="2px" />
                <Items>
                    <dx:MenuItem Text="" ToolTip="Thêm mới" Name="new">
                        <Image Url="~/images/icon/Actions/AddFile_32x32.png" />
                    </dx:MenuItem>
                    <dx:MenuItem Text="" ToolTip="Chỉnh sửa" Name="edit">
                        <Image Url="~/images/icon/Edit/Edit_32x32.png">
                        </Image>
                    </dx:MenuItem>
                    <dx:MenuItem Text="" ToolTip="Xóa phiếu" Name="delete">
                        <Image Url="~/images/icon/Actions/DeleteList_32x32.png">
                        </Image>
                    </dx:MenuItem>
               <%--     <dx:MenuItem Text="" ToolTip="Copy phiếu" Name="copy" BeginGroup="true">
                        <Image Url="~/images/icon/Edit/Copy_32x32.png" />
                    </dx:MenuItem>  --%>               
                </Items>
            </dx:ASPxMenu>
        </dx:PanelContent>
    </PanelCollection>   
</dx:ASPxRoundPanel>
