<%@ Page Language="C#" AutoEventWireup="true" CodeFile="geoairport.aspx.cs" Inherits="_default" EnableViewState="False" %>

<!DOCTYPE HTML>
<html lang="en">
<head runat="server">
    <title>Metro-Rail Marketplace</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/css/bootstrap.min.css" integrity="sha384-/Y6pD6FV/Vv2HJnA6t+vslU6fwYXjCFtcEpHbNJ0lyAFsXTsjBbfaDjzALeQsN6M" crossorigin="anonymous">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js" integrity="sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1" crossorigin="anonymous"></script>
    <link href="airport_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="jquery-1.7.2.min.js"></script>  
    <script type="text/javascript">
        jQuery(window).ready(function () {
            jQuery("#btnGetGeo").click(initiate_geolocation);
        });

        $.ajaxSetup({
            cache: false
        });
        var ajax_load = "<img src='load.gif' alt='loading...' />";

        function initiate_geolocation() {
            navigator.geolocation.getCurrentPosition(handle_geolocation_query, handle_errors);
        }

        function handle_errors(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED: alert("user did not share geolocation data");
                    break;

                case error.POSITION_UNAVAILABLE: alert("could not detect current position");
                    break;

                case error.TIMEOUT: alert("retrieving position timed out");
                    break;

                default: alert("unknown error");
                    break;
            }
        }

        function handle_geolocation_query(position) {
//                        alert('Lat: ' + position.coords.latitude + ' ' +
//                              'Lon: ' + position.coords.longitude);


            //This is the live URL
            //var loadUrl = 'geolocation.aspx?action=getnearest&latitude=' + position.coords.latitude + '&longitude=' + position.coords.longitude;
            
            //This is the test URL for My location
            var loadUrl = 'geolocation.aspx?action=getnearest&latitude=34.205906&longitude=-118.388298';
            
            
            //jQuery("#htmlResult").html(ajax_load).load(loadUrl);
            $.get(loadUrl, function (data) {
                if (data == 'nodata') {
                    window.location = "invalid.aspx";
                }
                else {
                    //alert("Data Loaded: " + data);
                    window.location = "offers.aspx";
                }
            });
                        
        }  
    </script>  
</head>
<body>
   
   <div style="text-align:center;width:419px; margin:0 auto; height: 695px;"><div>  
   
    
    <div class="double-border-logo"><img width="339" alt="image" src="Metro-logo.png" /></div>

    <div style="clear:both"></div> 


    <div class="double-border-content" style="text-align:center"> 
        <div><b>Get Offers</b></div>   
        <br />    
        <div><button id="btnGetGeo" >Start</button></div>
    </div>
       <div style="clear:both"></div>
       <br />

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


    </div>

   </div>
    
</body>
</html>
