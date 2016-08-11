<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="payment_schedule.ascx.cs" Inherits="WebModule.GUI.usercontrol.payment_schedule" %>
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
    <Items>
        <dx:LayoutGroup Caption="Lịch thanh toán đơn hàng">
            <Items>
                <dx:LayoutItem Caption="Lịch thanh toán" ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxScheduler ID="ASPxScheduler1" runat="server" ActiveViewType="Month" 
                                ClientIDMode="AutoID" Start="2013-07-22" Theme="MetropolisBlue">
                                <Views>
                                    <DayView>
                                        <TimeRulers>
                                            <dx:TimeRuler />
                                        </TimeRulers>
                                    </DayView>
                                    <WorkWeekView>
                                        <TimeRulers>
                                            <dx:TimeRuler />
                                        </TimeRulers>
                                    </WorkWeekView>
                                </Views>
                            </dx:ASPxScheduler>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem ShowCaption="False">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server" 
                            SupportsDisabledAttribute="True">
                            <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" 
                                Text="Xử lý thanh toán trong ngày" Width="180px" Height="18px">
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>


