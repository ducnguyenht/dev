<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingPage.aspx.cs" Inherits="WebModule.Accounting.CurrencyGridLookup.TestingPage" %>

<%@ Register Src="CurrencyGridLookup.ascx" TagName="CurrencyGridLookup" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:CurrencyGridLookup ID="CurrencyGridLookup1" runat="server">
            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                <RequiredField IsRequired="true" ErrorText="*" />
            </ValidationSettings>
        </uc1:CurrencyGridLookup>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton">
        </dx:ASPxButton>
    </div>
    </form>
</body>
</html>
