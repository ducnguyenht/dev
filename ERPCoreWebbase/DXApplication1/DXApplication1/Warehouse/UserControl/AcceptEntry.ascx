<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AcceptEntry.ascx.cs" Inherits="WebModule.Purchasing.UserControl.AcceptEntry" %>
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" 
        ClientInstanceName="cpLine" oncallback="cpLine_Callback">
    <ClientSideEvents EndCallback="cpLine_EndCallback" />
<PanelCollection>
    <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
        <dx:ASPxPopupControl ID="formAcceptEntry" runat="server" 
            HeaderText="Xác nhận bút toán tồn kho" Height="450px" Modal="True" 
            RenderMode="Lightweight"  
            Width="550px" ClientInstanceName="formAcceptEntry" AllowResize="True" 
            AllowDragging="True" PopupHorizontalAlign="WindowCenter" 
            PopupVerticalAlign="WindowCenter" LoadingPanelDelay="1000">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">                         
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" EnableTheming="True" 
    Theme="Default">
    <Items>
        <dx:LayoutItem Caption="Mã hàng hóa">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Height="17px" Width="199px" 
                        Enabled="False">
                        <DisabledStyle BackColor="#33CCCC">
                        </DisabledStyle>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="SL theo chứng từ">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="170px" Enabled="False">
                        <DisabledStyle BackColor="#33CCCC">
                        </DisabledStyle>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="SL thực tế">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="170px" Enabled="False">
                        <DisabledStyle BackColor="#33CCCC">
                        </DisabledStyle>
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="SL bút toán">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxTextBox ID="ASPxTextBox4" runat="server" Width="170px">
                    </dx:ASPxTextBox>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
        <dx:LayoutItem Caption="Ghi chú">
            <LayoutItemNestedControlCollection>
                <dx:LayoutItemNestedControlContainer runat="server" 
                    SupportsDisabledAttribute="True">
                    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="71px" Width="618px">
                    </dx:ASPxMemo>
                </dx:LayoutItemNestedControlContainer>
            </LayoutItemNestedControlCollection>
        </dx:LayoutItem>
    </Items>

</dx:ASPxFormLayout>

<div class="buttonsArea">
                <div class="buttons">
                    <table cellpadding="0" cellspacing="0" border="0" class="buttonsTable">
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnBack" ClientInstanceName="btnAccept" runat="server" 
                                    AutoPostBack="false" ClientVisible="true" CausesValidation="false" UseSubmitBehavior="true" Text="Đồng ý">
                                    <ClientSideEvents Click="OnBackButtonClick" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnNext" ClientInstanceName="btnNext" runat="server" 
                                    AutoPostBack="false" ClientVisible="true" CausesValidation="false" UseSubmitBehavior="true" Text="Trở về">
                                    <ClientSideEvents Click="OnNextButtonClick" />
                                </dx:ASPxButton>
                            </td>
                            
                        </tr>
                    </table>
                </div>
            </div>
            </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

    </dx:PanelContent>
</PanelCollection>
</dx:ASPxCallbackPanel>