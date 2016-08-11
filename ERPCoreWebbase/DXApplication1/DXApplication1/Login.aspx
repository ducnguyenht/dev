<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DXApplication1.GUI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sâm Ngọc Linh</title>   
    <%--<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>
    <script type="text/javascript">
//        jQuery.fn.center = function (obj) {
//            if (typeof obj === "undefined") {
//                obj = this.parent();
//            }
//            //alert(obj.width() + '-' + this.outerWidth());
//            this.css({
//                "position": "absolute",
//                "left": (obj.width() - this.outerWidth()) / 2 + 'px'
//            });
//            return this;
//        };
//        $(document).ready(function () {
//            $(".abscenter").each(function () {
//                $(this).center();
//            });
//        });
    </script>
    <style type="text/css">
    .abscenter 
    {
        margin: 0 auto !important;
    }
    #container 
    {
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        position: fixed;
        z-index: 100500; 
    }   
    
    
    div#page {
        border:0px solid #CCCCCC;
        width: 960px;
        margin:0 auto;
        padding:5px;
        position:relative;
        text-align:left;       
        height:90%; 
    }
    
    div 
    {
        text-align:center;
    }
    
    div#header    
    {
        margin-top:5px;     
        width:100%;
        height:40px;
        background-image: url('../images/NASID/NASERPBanner.png');
        background-repeat:no-repeat;
        background-position:center top;
    }
    
    div#reference {        
        /*width:50%;*/
        width:100%;
        /*margin:10px 0px 10px 5px;*/
        margin: 0 auto;
        height:500px;
        padding-top: 40px;
        /*float:left;*/
    }
    
    div#line {      
        border:2px solid blue;
        width:1%;
        height:500px;
        margin: 10px 0px 10px 50%;
        min-height:500px;
        height:500px  
    }   
    
    div#login 
    {        
       position:absolute;
       top:15px;
       right:10px;       
       width:49%;
       margin:0;
       height:500px;
       border-left:1px solid #CCCCCC;
    }
    
    div#footer {      
      width:100%;
      height:60px;
      bottom:0;
      top:0;
      background-image: url('../images/NASID/NASERPFooter.png');
      background-repeat:no-repeat;
      background-position:center top;
    }
    

    </style>

</head>
<body>
<form id="form1" runat="server">
<div id="container">
    <div id="header"></div>
    <div id="page">
        <div id="reference">   
            
            <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2" Width="100%">
                <Items>
                    <dx:LayoutItem ColSpan="2" Height="30px" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="ASPxFormLayout2_E1" runat="server" Font-Bold="True" 
                                    Font-Size="Larger" Text="TÀI KHOẢN CỦA BẠN">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Height="30px" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <CaptionSettings HorizontalAlign="Left" />
                        <BorderTop BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                        <Border BorderStyle="None" />
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" Height="30px" HorizontalAlign="Left">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="ASPxFormLayout2_E2" runat="server" 
                                    Text="Chọn 01 tài khoản mà bạn có :">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                        <CaptionSettings HorizontalAlign="Left" Location="Left" />
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False" ColSpan="2" HorizontalAlign="Center">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton CssClass="abscenter" ID="btnGoogleLogin" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" EnableTheming="False" 
                                    CommandArgument="https://www.google.com/accounts/o8/id" 
                                    OnCommand="OpenIdLogin_Click">
                                    <Image Url="~/images/google.png" ToolTip="Đăng nhập với Google">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ColSpan="2" ShowCaption="False" VerticalAlign="Middle">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton CssClass="abscenter" ID="btnYahooLogin" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" EnableTheming="False" 
                                    CommandArgument="https://me.yahoo.com" OnCommand="OpenIdLogin_Click">
                                    <Image Url="~/images/yahoo.png" ToolTip="Đăng nhập với Yahoo">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ColSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton CssClass="abscenter" ID="btnFacebookLogin" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" EnableTheming="False" 
                                    CommandArgument="http://facebook-openid.appspot.com" 
                                    OnCommand="OpenIdLogin_Click">
                                    <Image Url="~/images/facebook.png" ToolTip="Đăng nhập với Facebook">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ColSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton CssClass="abscenter" ID="btnTwitterLogin" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" EnableTheming="False" 
                                    OnCommand="OpenIdLogin_Click">
                                    <Image Url="~/images/twitter.png" ToolTip="Đăng nhập với Twitter">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ColSpan="2" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton CssClass="abscenter" ID="btnLinkedInLogin" runat="server" Cursor="pointer" 
                                    EnableDefaultAppearance="False" EnableTheming="False" 
                                    OnCommand="OpenIdLogin_Click">
                                    <Image Url="~/images/linkedin.png" ToolTip="Đăng nhập với LinkedIn">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ColSpan="2" HorizontalAlign="Center" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="lblOpenIdStatus" runat="server" Font-Bold="True" 
                                    ForeColor="Red" Text="Tài khoản của bạn chưa được cấp quyền sử dụng hệ thống." 
                                    Visible="False">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <SettingsItems Height="50px" HorizontalAlign="Left" />
            </dx:ASPxFormLayout>
            
        </div>        
        <%--<div id="login">                    
           <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2">
	            <Items>
		            <dx:LayoutItem ColSpan="2" Height="30px" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="ASPxFormLayout1_E6" runat="server" Font-Bold="True" 
                                    Font-Size="Larger" Text="TÀI KHOẢN NAANID" Wrap="False">
                                </dx:ASPxLabel>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
		            <dx:LayoutItem Caption="EMAIL *" VerticalAlign="Top" ColSpan="2" Height="50px">
			            <LayoutItemNestedControlCollection>
				            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" 
					            SupportsDisabledAttribute="True">
					            <dx:ASPxTextBox ID="tbemail" runat="server" Width="200px" 
                                    Text="user@naansolution.com">
					            </dx:ASPxTextBox>
				            </dx:LayoutItemNestedControlContainer>
			            </LayoutItemNestedControlCollection>
		                <CaptionSettings Location="Top" />
		            </dx:LayoutItem>
		            <dx:LayoutItem Caption="PASSWORD *" VerticalAlign="Top" ColSpan="2">
			            <LayoutItemNestedControlCollection>
				            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" 
					            SupportsDisabledAttribute="True">
					            <dx:ASPxTextBox ID="tbPassword" runat="server" Width="200px" Text="user">
					            </dx:ASPxTextBox>
				            </dx:LayoutItemNestedControlContainer>
			            </LayoutItemNestedControlCollection>
		            </dx:LayoutItem>
	                <dx:LayoutItem ColSpan="2" Height="50px" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxCheckBox ID="ASPxFormLayout1_E4" runat="server" CheckState="Unchecked" 
                                    Text="Giữ chế độ đăng nhập">
                                </dx:ASPxCheckBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btsubmit" runat="server" Text="Đăng Nhập" 
                                    Wrap="False"  OnClick="btsubmit_Click1">
                                    <Image Url="~/images/DXR.png">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" 
                                SupportsDisabledAttribute="True">
                                <dx:ASPxHyperLink ID="ASPxFormLayout1_E5" runat="server" Cursor="pointer" 
                                    Text="Tôi không vào được tài khoản của mình ">
                                </dx:ASPxHyperLink>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
	            </Items>
	            <SettingsItems VerticalAlign="Top" Height="50px" />
                <SettingsItemCaptions Location="Top" />
            </dx:ASPxFormLayout>              
        </div>--%>        
    </div>
    <div id="footer"></div>
</div>
</form>
</body>
</html>
