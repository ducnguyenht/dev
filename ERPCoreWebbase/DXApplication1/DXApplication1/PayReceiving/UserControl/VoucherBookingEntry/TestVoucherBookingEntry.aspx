<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestVoucherBookingEntry.aspx.cs" Inherits="WebModule.PayReceiving.UserControl.VoucherBookingEntry.TestVoucherBookingEntry" %>

<%@ Register src="uVoucherBookingEntry.ascx" tagname="uVoucherBookingEntry" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function EventTarget() {
            this._listeners = {};
        }

        EventTarget.prototype = {

            constructor: EventTarget,

            addListener: function (type, listener) {
                if (typeof this._listeners[type] == "undefined") {
                    this._listeners[type] = [];
                }

                this._listeners[type].push(listener);
            },

            fire: function (event) {
                if (typeof event == "string") {
                    event = { type: event };
                }
                if (!event.target) {
                    event.target = this;
                }

                if (!event.type) {  //falsy
                    throw new Error("Event object missing 'type' property.");
                }

                if (this._listeners[event.type] instanceof Array) {
                    var listeners = this._listeners[event.type];
                    for (var i = 0, len = listeners.length; i < len; i++) {
                        listeners[i].call(this, event);
                    }
                }
            },

            removeListener: function (type, listener) {
                if (this._listeners[type] instanceof Array) {
                    var listeners = this._listeners[type];
                    for (var i = 0, len = listeners.length; i < len; i++) {
                        if (listeners[i] === listener) {
                            listeners.splice(i, 1);
                            break;
                        }
                    }
                }
            }
        };

        function handleEvent(event) { alert('Đã xử lý thành công'); }
        var target = new EventTarget();
        target.addListener("bookingComplete", handleEvent);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxButton ID="btnBookingEntry" runat="server" Text="Hạch toán" AutoPostBack="false">
        </dx:ASPxButton>
    </div>
    </form>
</body>
</html>
