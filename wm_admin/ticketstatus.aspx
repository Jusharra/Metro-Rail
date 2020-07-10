<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ticketstatus.aspx.cs" Inherits="wm_admin_ticketstatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_title"><asp:Literal ID="ltTitle" runat="server"></asp:Literal></div>
<div class="page_subtitle">Confirmation Number: <asp:Literal ID="ltConfirmationNumber" runat="server" /></div>
<div class="page_subtitle">Order Status: <asp:Literal ID="ltOrderStatus" runat="server" /></div>
     <div class="orderticket_container_product" style="width:80%;margin-left:auto;margin-right:auto;">
    <table class="table_data">
    <tr class="table_data_headerrow"><td class="table_data_headerdata" colspan="7">Items</td></tr>
    <tr><td class="table_data_headerdata" style="width:125px;word-wrap: break-word;" >Product</td><td class="table_data_headerdata">Price</td><td class="table_data_headerdata">Discount</td><td class="table_data_headerdata">Sub-Total</td><td class="table_data_headerdata">Total Taxes</td><td class="table_data_headerdata">Total Price</td></tr>
    <!--<tr><td class="table_data_cell">Diet Pespi</td><td class="table_data_cell">.75</td><td class="table_data_cell">0</td><td class="table_data_cell">.75</td><td class="table_data_cell">.25</td><td class="table_data_cell">$1.00</td></tr>-->
    <asp:Literal ID="ltCartList" runat="server" />
    <tr><td class="table_data_cell" colspan="5">Total Purchase</td><td class="table_data_cell"><asp:Label ID="lblTotalPurchase" runat="server" /></td></tr>
    </table>
    </div>

    <div style="clear:both;"></div>
     <br />
    <br />
    <div style="text-align:center;"><asp:Button ID="btnMarkReady" runat="server" 
            Text="Mark Ready" onclick="btnMarkReady_Click" 
          /></div>
    
     <br />
    <br />
    <div style="text-align:center;"><asp:Button ID="btnMarkFulfilled" runat="server" Text="Mark Fulfilled" 
            onclick="btnMarkFulfilled_Click" /></div>
    <br />
    <br />
    <div style="text-align:center;"><asp:Button ID="btnMarkCancelled" runat="server" Text="Mark Cancelled" 
            onclick="btnMarkCancelled_Click" /></div>
    </form>
</body>
</html>
