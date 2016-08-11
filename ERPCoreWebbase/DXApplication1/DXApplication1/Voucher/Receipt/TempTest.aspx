<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempTest.aspx.cs" Inherits="WebModule.Voucher.Receipt.TempTest" %>

<%@ Register src="../../Accounting/Journal/Transaction/Control/GridViewBookingEntries.ascx" tagname="GridViewBookingEntries" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../scripts/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="../../scripts/shortcut.js" type="text/javascript"></script>
    <script src="../../scripts/script.js" type="text/javascript"></script>
    <script type="text/javascript">
        function grid1_GridViewDataChanged() {
            alert('raised');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <uc1:GridViewBookingEntries ID="GridViewBookingEntries1" runat="server" 
            GridViewDataChanged="grid1_GridViewDataChanged" />

        <uc1:GridViewBookingEntries ID="GridViewBookingEntries2" runat="server" 
            GridViewDataChanged="function(s, e) { console.log('grid2 GridViewDataChanged'); }" />
        
    </div>
    </form>
</body>
</html>
