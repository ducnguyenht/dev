<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uCustomerService.ascx.cs" Inherits="WebModule.NAANAdmin.Customer.Usercontrol.uCustomerService" %>
<dx:ASPxCallbackPanel ID="cpLine" runat="server" Width="100%" ClientInstanceName="cpLine"
    >
    <ClientSideEvents EndCallback="cpLine_EndCallback" />
    <ClientSideEvents EndCallback="cpLine_EndCallback"></ClientSideEvents>
    <PanelCollection>
        <dx:PanelContent ID="PanelContent12" runat="server" SupportsDisabledAttribute="True">
<dx:aspxpagecontrol id="pc" clientinstancename="pc" runat="server" activetabindex="1"
            rendermode="Lightweight" width="100%" height="100%">
            <TabPages>
                <dx:TabPage Text="Chọn ứng dụng" ClientEnabled="True" Name="Personal">
                    <ContentCollection>
                        <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                                <div style="margin-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblHeader1" runat="server" Text="Danh sách ứng dụng" Font-Bold="True"
                                        Font-Size="Small">
                                    </dx:ASPxLabel>
                                </div>
                                <dx:ASPxGridView ID="gvApplication" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" ShowSelectCheckbox="True"
                                            VisibleIndex="0">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" ShowInCustomizationForm="True"
                                            VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" ShowInCustomizationForm="True"
                                            VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" ShowInCustomizationForm="True"
                                            VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thể loại" FieldName="Type" 
                                            ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                    <Settings ShowFilterRow="True"></Settings>
                                </dx:ASPxGridView>
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage ClientEnabled="False" Name="Confirmation" Text="Thông tin sử dụng ứng dụng">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl3" runat="server" SupportsDisabledAttribute="True">
                            <div style="overflow: auto; height: 450px">
                            <dx:ASPxFormLayout ID="ASPxFormLayout55" runat="server" Width="100%" Height="16px">
                                    <Items>
                                        <dx:LayoutGroup Caption="Layout Group" ColCount="2" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem Caption="Từ ngày">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer155" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server">
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Ngày hết hạn">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer156" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server">
                                                            </dx:ASPxDateEdit>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                    <SettingsItemCaptions HorizontalAlign="Right" />
                                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                                </dx:ASPxFormLayout>
                                </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage ClientEnabled="False" Text="Hoàn tất" Name="Finish">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControlFinish" runat="server">
                            <div style="overflow: auto; height: 450px">
                                <dx:ASPxFormLayout ID="ASPxFormLayout5" runat="server" Width="100%" Height="16px">
                                    <Items>
                                        <dx:LayoutGroup Caption="Layout Group" ColCount="2" GroupBoxDecoration="None">
                                            <Items>
                                                <dx:LayoutItem Caption="Từ ngày">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="15/08/2013">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                                <dx:LayoutItem Caption="Ngày hết hạn">
                                                    <LayoutItemNestedControlCollection>
                                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server"
                                                            SupportsDisabledAttribute="True">
                                                            <dx:ASPxLabel ID="ASPxLabel55" runat="server" Text="15/08/2015">
                                                            </dx:ASPxLabel>
                                                        </dx:LayoutItemNestedControlContainer>
                                                    </LayoutItemNestedControlCollection>
                                                </dx:LayoutItem>
                                            </Items>
                                        </dx:LayoutGroup>
                                    </Items>
                                    <SettingsItemCaptions HorizontalAlign="Right" />
                                    <SettingsItemCaptions HorizontalAlign="Right"></SettingsItemCaptions>
                                </dx:ASPxFormLayout>
                                <br>
                                <br></br>
                                <dx:ASPxGridView ID="gvApplication1" runat="server" AutoGenerateColumns="False" 
                                    Width="100%">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Mã số" FieldName="Code" 
                                            ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên ứng dụng" FieldName="Name" 
                                            ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Mô tả" FieldName="Description" 
                                            ShowInCustomizationForm="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Thể loại" FieldName="Type" 
                                            ShowInCustomizationForm="True" VisibleIndex="4">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" />
                                </dx:ASPxGridView>
                               </br>
                                                                <dx:ASPxFormLayout ID="ASPxFormLayout4" runat="server" ColCount="2" Width="100%">
                                                    <Items>
                                                        
                                                        <dx:LayoutItem ShowCaption="False" Width = "50%">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxButton ID="ASPxButton1" AutoPostBack = "false" runat="server" CssClass="float_right" Text="In">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Print" />
                                                                            <SpriteProperties CssClass="Sprite_Print"></SpriteProperties>
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>
                                                        <dx:LayoutItem ShowCaption="False" Width = "50%" CaptionSettings-Location="Right">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                                    <dx:ASPxButton ID="btnGoToSchedule" AutoPostBack = "false" runat="server" CssClass="afinishAreaButton" HorizontalAlign="Center"
                                                                        Text="Tiếp tục chọn ứng dụng" UseSubmitBehavior="False">
                                                                        <Image>
                                                                            <SpriteProperties CssClass="Sprite_Repeat" />
                                                                            <SpriteProperties CssClass="Sprite_Repeat"></SpriteProperties>
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                            <CaptionSettings Location="Right"></CaptionSettings>
                                                        </dx:LayoutItem>
                                                    </Items>
                                                </dx:ASPxFormLayout>
                               
                            </div>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:aspxpagecontrol>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
         <dx:aspxpanel id="dxpError" clientinstancename="dxpError" runat="server" cssclass="errorPanel"
            clientvisible="false">
            <PanelCollection>
                <dx:PanelContent>
                    Please complete or correct the fields highlighted in red.
                </dx:PanelContent>
            </PanelCollection>
        </dx:aspxpanel>
        <dx:aspxhiddenfield id="hfRegInfo" clientinstancename="hfRegInfo" runat="server"
            syncwithserver="true" viewstatemode="Enabled">
        </dx:aspxhiddenfield>
    <div class="buttonsArea">
        <div class="buttons" align="right">
            <table cellpadding="0" cellspacing="2" border="0" class="buttonsTable">
                <tr>
                    <td>
                        <dx:aspxbutton id="btnBack" clientinstancename="btnBack" runat="server" autopostback="false"
                            clientvisible="false" causesvalidation="false" usesubmitbehavior="False" text="Trở về">
                            <ClientSideEvents Click="OnBackButtonClick" />
                             <Image>
                            <SpriteProperties CssClass="Sprite_Backward" />
                        </Image>
                        </dx:aspxbutton>
                    </td>
                    <td>
                        <dx:aspxbutton id="btnNext" clientinstancename="btnNext" runat="server" autopostback="false"
                            causesvalidation="false" usesubmitbehavior="true" text="Tiếp theo">
                            <ClientSideEvents Click="OnNextButtonClick" />
                            <Image>
                                 <SpriteProperties CssClass="Sprite_Forward" />
                            </Image>
                        </dx:aspxbutton>
                    </td>
                    <td>
                        <dx:aspxbutton id="btnFinish" clientinstancename="btnFinish" runat="server" autopostback="false"
                            clientvisible="false" usesubmitbehavior="false" text="Hoàn tất">
                            <ClientSideEvents Click="OnFinishButtonClick" />
                            <Image>
                                        <SpriteProperties CssClass="Sprite_Finished" />
                                    </Image>
                        </dx:aspxbutton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
