<%@ Page Language="C#" AutoEventWireup="true" CodeFile="offer.aspx.cs" Inherits="offer" EnableViewState="False" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
    <title>Metro-Rail Marketplace</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css" integrity="sha384-/Y6pD6FV/Vv2HJnA6t+vslU6fwYXjCFtcEpHbNJ0lyAFsXTsjBbfaDjzALeQsN6M" crossorigin="anonymous">
    <link href="airport_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jquery-1.7.2.min.js"></script>  
     <script type="text/javascript"> 
        $("#btnBack").click(function () {
            window.location = "offers.aspx";
        });     
        </script> 
</head>

<body>
   <div style="text-align:center;width:400px;margin:0 auto;"><div>
   <div class="double-border-logo"><a href="offers.aspx"><img width="339" alt="image" src="Metro-logo.png" /></a></div>
   <div style="clear:both"></div> 

   <div class="double-border-content" style="text-align:center">
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblAirport" runat="server" CssClass="airport"></asp:Label>
    <div style="background-color:#98AFC7;width:100%;font-size:12pt;color:White;font-weight:bold;">Offer</div>
    <div>&nbsp;</div>
    <asp:Literal ID="ltOffers" runat="server"></asp:Literal>    
    <img src="client_img/barcode.jpg" />
    <div>
    <div>&nbsp;</div>
        </div>
    </div>
        
    </form>    
</div>

     </div>

   </div>
</body>
</html>