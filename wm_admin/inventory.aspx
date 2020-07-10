<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inventory.aspx.cs" Inherits="wm_admin_inventory" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .inventory_title
    {
       width:80%;
       font-size:18pt;
       font-weight:bold;
       font-family:Corbel,Arial,San-Serif;
       text-align:center;
       padding:15px 15px 15px 15px;
       border:2px solid white;
    }
    .inventory_category_box
    {
        width:80%;
        border:2px solid #d3d3d3;
        margin-top:10px;
        padding:15px 15px 15px 15px;
        font-size:16pt;
        height:30px;
        text-align:center;
        font-family:Corbel,Arial,San-Serif;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
 <asp:Panel ID="pnlCategory" runat="server">

 <div class="inventory_title">Choose Category</div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkWater" 
         runat="server" onclick="lnkWater_Click">Water</asp:LinkButton></div>
 <div class="inventory_category_box box_corners">
     <asp:LinkButton ID="lnkEnhancedWater" runat="server" 
         onclick="lnkEnhancedWater_Click">Enhanced Water</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkSports" 
         runat="server" onclick="lnkSports_Click">Sports</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkEnergy" 
         runat="server" onclick="lnkEnergy_Click">Energy</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkJuices" 
         runat="server" onclick="lnkJuices_Click">Juices</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkSparkling" 
         runat="server" onclick="lnkSparkling_Click">Sparkling</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkSodas" 
         runat="server" onclick="lnkSodas_Click">Sodas</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkTea" 
         runat="server" onclick="lnkTea_Click">Tea</asp:LinkButton></div>
 <div class="inventory_category_box box_corners"><asp:LinkButton ID="lnkCoffee" 
         runat="server" onclick="lnkCoffee_Click">Coffee</asp:LinkButton></div>
 <div class="inventory_category_box box_corners">
     <asp:LinkButton ID="lnkMealReplacement" runat="server" 
         onclick="lnkMealReplacement_Click">Meal Replacement</asp:LinkButton></div>

 </asp:Panel>

 <asp:Panel ID="pnlProducts" runat="server">
 <asp:Repeater ID="rptResults" runat="server" 
        onitemdatabound="rptResults_ItemDataBound">

<ItemTemplate>
<div class="results_container box_corners">
    <div class="results_image_container"><img class="results_img" alt="" src="../media/products/<%# DataBinder.Eval(Container.DataItem, "image_url") %>" /></div>
    <div class="results_content_container">
        <div class="results_productname"><%# DataBinder.Eval(Container.DataItem, "product_name") %></div>
          <div class="results_price"><asp:TextBox ID="frmCount" runat="server" Width="50" />&nbsp;<asp:Button CssClass="button" ID="btnAdjust" runat="server" Text="Adjust" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "sku") %>' CommandName="Adjust" OnCommand="btnAdjust_Click" /></div>
         <div class="results_button"><asp:Button CssClass="button" ID="btnRecord" runat="server" Text="Record Sale" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "sku") %>' CommandName="Record" OnCommand="btnRecord_Click" /></div>
    </div>
</div>
</ItemTemplate>
</asp:Repeater>
 </asp:Panel>

 <asp:Panel ID="pnlNoProducts" runat="server" Visible="false">
 </asp:Panel>



    </form>
</body>
</html>
