<%@ Page Language="C#" AutoEventWireup="true" CodeFile="offers.aspx.cs" Inherits="offers" EnableViewState="False" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
    <title>Metro-Rail Marketplace</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css" integrity="sha384-/Y6pD6FV/Vv2HJnA6t+vslU6fwYXjCFtcEpHbNJ0lyAFsXTsjBbfaDjzALeQsN6M" crossorigin="anonymous">
    <link href="airport_style.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <div style="text-align:center;width:400px;margin:0 auto;">
   <div>
   <div class="double-border-logo"><a href="offers.aspx"><img width="339" alt="image" src="Metro-logo.png" /></a></div>
   <div style="clear:both"></div> 

   <div class="double-border-content" style="text-align:center">  
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblAirport" runat="server" CssClass="airport"></asp:Label>
    </div>

    <asp:ListView ID="ListView1" runat="server">
    <LayoutTemplate>
        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
    </LayoutTemplate>

    <ItemTemplate>
    <div style="background-color:#98AFC7;width:100%;font-size:12pt;color:White;font-weight:bold;"><%# AddGroupingRowIfLastOfferHasChanged() %> Offers</div>
    <div>&nbsp;</div>
    <div><img src="client_img/<%#Eval("mediafile")%>" \></div>
    <div class="offertitle"><a href="offer.aspx?o=<%#Eval("id")%>"><%#Eval("offer_header")%></a></div>
    <div class="offerdeal"><%#Eval("offer_deal")%></div>
    <div><%#Eval("offer_description")%></div>
    
    <div>&nbsp;</div>
    <div style="width:250px; border-top:1px solid #4FD5D6;margin-left:auto;margin-right:auto;margin-top:5px;margin-bottom:5px;">&nbsp;</div>    
    </ItemTemplate>

    <EmptyDataTemplate>
    <div style="text-align:center;">No Offers Available.</div>
    </EmptyDataTemplate>



    </asp:ListView>


    </form>
    </div>
       </div>
       <br />
       <br />
        <!--start footer-->
        <div class="footer_section" style="text-align:center">
            <div class="footer_buttons">        
                <div class="footer_button"><a class="footer_button_link" href="howtobuy.aspx">How to Buy</a></div>             
                <!--<div class="footer_button"><a class="footer_button_link" href="microformats.aspx">Micro-Format</a></div>-->
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
       
    </div>
    </div>

</body>
</html>
