<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebModule.Sales.UserControl.PopupOrder.WebForm1" %>

<%@ Register src="ucPopupOrder.ascx" tagname="ucPopupOrder" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script type="text/javascript">
            function cmdBillActor_Click(s, e) {
                formBillActor.ShowAtElementByID('popupAnchor');
                formBillActor.PerformCallback();
            }
        </script>
        <uc1:ucPopupOrder ID="ucPopupOrder1" runat="server" />
    
    </div>
    </form>
</body>
</html>
