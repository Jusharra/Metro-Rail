using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
//using BarCode;
//using Barcode1D2D;
public partial class offer : System.Web.UI.Page
{
    string appsource = System.Configuration.ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool airportfound = false;
        if (Session["airport"] != null)
        {
            if (Session["airport"].ToString() != "") airportfound = true;
        }
        if (!airportfound)
        {
            Response.Redirect("invalid.aspx", true);
        }
        else
        {
            



            lblAirport.Text = Session["airport"].ToString();
            
            DisplayOfferDetail();
        }
    }

    private void DisplayOfferDetail()
    { 
        string content = "<div>No Offer Available</div>";
        try
        {
            Guid id = new Guid(Request["o"]);
            StringBuilder sb = new StringBuilder();
            Offer o = new Offer();
            Offer oo = o.GetOfferDetail(id, appsource);
            if (oo != null)
            {
                sb.Append("<div><img src=\"client_img/" + oo.mediafile + "\"></div>");
                sb.Append("<div class=\"offertitle\">" + oo.offer_header + "</div>");
                sb.Append("<div class=\"offerdeal\">" + oo.offer_deal + "</div>");
                sb.Append("<div class=\"offerdescription\">" + oo.offer_description + "</div>");
                sb.Append("<div><class=\"display_code\">" + oo.display_code + "\"></div>");
                sb.Append("<div><class=\"begin_date\">" + oo.begin_date + "\"></div>");
                sb.Append("<div><class=\"end_date\">" + oo.end_date + "\"></div>");
                sb.Append("<div>&nbsp;</div>");
                content = sb.ToString();
            }
        }
        catch
        { }
        ltOffers.Text = content;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}