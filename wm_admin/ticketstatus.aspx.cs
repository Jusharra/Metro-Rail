using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class wm_admin_ticketstatus : System.Web.UI.Page
{

    long mid = -1;
    string appsource = System.Configuration.ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
    string product_image_folder = "../media/products/";
    long order_id = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool isvalidcode = true;
        string confirmcode = Request["c"].ToString();
        if (confirmcode.Trim() != string.Empty)
        {

            DebitOrder d = new DebitOrder();
            DebitOrderDetail dod = new DebitOrderDetail();
            DebitOrderManager dom = new DebitOrderManager();
            d = dom.GetOrderByConfirmationCode(confirmcode, appsource);
            if (d.id < 1) { isvalidcode = false; }
            if (isvalidcode)
            {
                order_id = d.id;
                if (d.order_description == "Purchase")
                {
                    ltTitle.Text = "Purchase Order Ticket";
                    //pnlPurchaseInstructions.Visible = true;
                }
                else
                {
                    //pickup
                    ltTitle.Text = "Pickup Order Ticket";
                    //pnlPickupInstructions.Visible = true;
                }
                ltOrderStatus.Text = d.order_status;
                ltConfirmationNumber.Text = d.display_id;
                GetReceipt();
                


            }
            else
            {
                Response.Write("<div>Invalid Code</div>");
            }
        }
       




    }

    private void GetReceipt()
    {
        Decimal grandtotal = 0.00M;
        StringBuilder sb = new StringBuilder();
        DebitOrderDetail[] details = new DebitOrderDetail[0];
        DebitOrderManager dom = new DebitOrderManager();

        details = dom.GetDebitOrderDetailById(order_id, appsource);
        if (details.Length > 0)
        {
            foreach (DebitOrderDetail d in details)
            {
                //decimal discount = cm.GetDiscounts(p.id, appsource);
                //decimal taxes = cm.GetTaxes(p.id,p.retail_price, appsource);
                decimal subtotal = d.retail_price - d.total_discounts;
                decimal linetotal = (d.retail_price - d.total_discounts) + d.total_taxes;
                sb.Append("<tr><td class=\"table_data_cell\">" + d.product_name + "</td>");
                sb.Append("<td class=\"table_data_cell\">" + d.retail_price.ToString("c") + "</td>");
                sb.Append("<td class=\"table_data_cell\">" + d.total_discounts.ToString("c") + "</td>");
                sb.Append("<td class=\"table_data_cell\">" + subtotal.ToString("c") + "</td>");
                sb.Append("<td class=\"table_data_cell\">" + d.total_taxes.ToString("c") + "</td>");
                sb.Append("<td class=\"table_data_cell\">" + linetotal.ToString("c") + "</td></tr>");
                sb.AppendLine("");
                grandtotal = grandtotal + linetotal;

                //
                //
                //
                //
                //
                //

            }
            ltCartList.Text = sb.ToString();
            lblTotalPurchase.Text = grandtotal.ToString("c");
        }
    }

    protected void btnMarkFulfilled_Click(object sender, EventArgs e)
    {
        string confirmcode = ltConfirmationNumber.Text;
        DebitOrder d = new DebitOrder();
        DebitOrderManager dom = new DebitOrderManager();
        d = dom.GetOrderByConfirmationCode(confirmcode, appsource);
        d.order_status = "Fulfilled";
        dom.Update(d, appsource);
        Response.Redirect("ticketstatus.aspx?c=" + confirmcode,true);
        

    }
    protected void btnMarkCancelled_Click(object sender, EventArgs e)
    {
        string confirmcode = ltConfirmationNumber.Text;
        DebitOrder d = new DebitOrder();
        DebitOrderManager dom = new DebitOrderManager();
        d = dom.GetOrderByConfirmationCode(confirmcode, appsource);
        d.order_status = "Cancelled";
        dom.Update(d, appsource);
        Response.Redirect("ticketstatus.aspx?c=" + confirmcode,true);
    }
    protected void btnMarkReady_Click(object sender, EventArgs e)
    {
        string confirmcode = ltConfirmationNumber.Text;
        DebitOrder d = new DebitOrder();
        DebitOrderManager dom = new DebitOrderManager();
        d = dom.GetOrderByConfirmationCode(confirmcode, appsource);
        d.order_status = "Ready";
        dom.Update(d, appsource);
        Response.Redirect("ticketstatus.aspx?c=" + confirmcode, true);
    }
}