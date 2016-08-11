<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopulateCustomObject.aspx.cs" Inherits="WebModule.PopulateCustomObject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <dx:aspxcallbackpanel runat="server" width="200px" ClientInstanceName="Unnamed1"
                oncallback="Unnamed1_Callback"><PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    Số item được cập nhật:
                    <dx:ASPxLabel runat="server" Text="0" ID="lblnum">
                    </dx:ASPxLabel>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Populate" AutoPostBack="false">
                        <ClientSideEvents Click="function(s, e) {
	Unnamed1.PerformCallback();
}" />
                    </dx:ASPxButton>
                </dx:PanelContent>
            </PanelCollection>
        </dx:aspxcallbackpanel>

    </div>
    </form>
</body>
</html>
