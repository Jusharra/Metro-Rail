<%@ Page Language="C#" AutoEventWireup="true" CodeFile="orderticket_qr.aspx.cs" Inherits="orderticket_qr" EnableViewState="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Metro-Rail Marketplace</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css" integrity="sha384-/Y6pD6FV/Vv2HJnA6t+vslU6fwYXjCFtcEpHbNJ0lyAFsXTsjBbfaDjzALeQsN6M" crossorigin="anonymous">
    <link href="airport_style.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <div style="text-align:center;width:400px;margin:0 auto;"></div>
  <div class="double-border-logo"><a href="offers.aspx"><img width="339" alt="image" src="Metro-logo.png" /></a></div>
   <div style="clear:both"></div>
    <form id="form1" runat="server">

    <div class="ticket_container_qr box_corners">
        <asp:Literal ID="display_code" runat="server"  />
        <br />
    </div>
        <!--start footer-->
        <div class="footer_section" style="text-align:center">
            <div class="footer_buttons">        
                <div class="footer_button"><a class="footer_button_link" href="howtobuy.aspx">How to Buy</a></div>             
                <div class="footer_button"><a class="footer_button_link" href="microformats.aspx">Micro-Format</a></div>
            </div>
            <div style="clear:both;"></div>

            <div class="footer_content">
                <span>&copy; 2019 Metro-Rail Marketplace.&nbsp;-&nbsp;</span>
                <span><a class="footer_links" href="terms.aspx"> Our terms and conditions</a>&nbsp;-&nbsp;</span>
                <span><a class="footer_links" href="privacy.aspx">Privacy</a>&nbsp;-&nbsp;</span>
                <span><a class="footer_links" href="vendors.aspx">Vendors</a></span>
            </div>
        </div>
        <!--end footer-->
    </form>
</body>
</html>
