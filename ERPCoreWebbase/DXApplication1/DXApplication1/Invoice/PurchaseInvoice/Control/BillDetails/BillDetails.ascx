<%@ Control Language="C#" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="BillDetails.ascx.cs"
    Inherits="WebModule.Invoice.PurchaseInvoice.Control.BillDetails.BillDetails" %>
<%@ Register Src="../../../Control/BillItemEditForm/BillItemEditForm.ascx" TagName="BillItemEditForm"
    TagPrefix="uc1" %>
<%@ Register Src="../DeliverySchedule/ucDeliverySchedule.ascx" TagName="ucDeliverySchedule"
    TagPrefix="uc2" %>
<%@ Register Src="../PaymentPlanning/ucPaymentPlanning.ascx" TagName="ucPaymentPlanning"
    TagPrefix="uc3" %>
<script type="text/javascript">

    var BillDetailsClass = function () {
        this.RefreshBillItems = function () {
            //billItemEditForm.Refresh();
            if (pageBillDetails.GetActiveTabIndex() == 0)
                SetActiveTabIndex(0);
        };
        this.RefreshDeliverySchedule = function () {
            if (pageBillDetails.GetActiveTabIndex() == 1)
                SetActiveTabIndex(1);
        };
        this.RefreshPayment = function () {
            if (pageBillDetails.GetActiveTabIndex() == 2)
                SetActiveTabIndex(2);
        };
        this.ResetActiveTabIndex = function () {
            pageBillDetails.SetActiveTabIndex(0);
        };

        this.GridViewBillItems = {};
    };

    var nasObj = new BillDetailsClass();
    window['<%= _ClientInstanceName %>'] = nasObj;

    var pendingCommand;
    function SetActiveTabIndex(tabIndex) {
        var command;
        switch (tabIndex) {
            case 0:
                command = 'BillItems';
                break;
            case 1:
                command = 'DeliverySchedule';
                break;
            case 2:
                command = 'PaymentPlanning';
                break;
            default:
                break;
        }
        if (command) {
            if (panelBillDetails.InCallback())
                pendingCommand = command;
            else
                panelBillDetails.PerformCallback(command);
        }
    }

    function pageBillDetails_ActiveTabChanged(s, e) {
        var tabIndex = e.tab.index;
        SetActiveTabIndex(tabIndex);
    }

    function panelBillDetails_EndCallback(s, e) {
        if (pendingCommand) {
            panelBillDetails.PerformCallback(pendingCommand);
            pendingCommand = null;
        }
    }
</script>
<dx:ASPxCallbackPanel ID="panelBillDetails" ClientInstanceName="panelBillDetails"
    runat="server" Width="100%" OnCallback="panelBillDetails_Callback">
    <ClientSideEvents EndCallback="panelBillDetails_EndCallback" />
    <PanelCollection>
        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
            <dx:ASPxPageControl ClientInstanceName="pageBillDetails" ID="pageBillDetails" runat="server"
                ActiveTabIndex="1" RenderMode="Classic" Width="100%">
                <ClientSideEvents ActiveTabChanged="pageBillDetails_ActiveTabChanged" />
                <TabPages>
                    <dx:TabPage Text="Hàng hóa/Dịch vụ">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <uc1:BillItemEditForm ID="billItemEditForm" runat="server" ClientInstanceName="billItemEditForm"/>
                                <script type="text/javascript">
                                    window['<%= _ClientInstanceName %>'].GridViewBillItems = billItemEditForm;
                                </script>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Giao hàng">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <uc2:ucDeliverySchedule ID="deliverySchedule" runat="server" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Thanh toán">
                        <ContentCollection>
                            <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                <uc3:ucPaymentPlanning ID="paymentPlanning" runat="server" />
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
            </dx:ASPxPageControl>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>
