using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class offers : System.Web.UI.Page
{
    string appsource = System.Configuration.ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        bool airportfound = false;
        if (Session["airport"] != null)
        {
            if(Session["airport"].ToString() != "")airportfound = true;
        }
        if (!airportfound)
        {                
            Response.Redirect("invalid.aspx", true);
        }
        else
        {

            lblAirport.Text = Session["airport"].ToString();
            DisplayOffers();
        }
    }

    private void DisplayOffers()
    {
        string content = "<div>No Offers</div>";
        StringBuilder sb = new StringBuilder();
        Offer o = new Offer();
        Offer[] off = o.GetOffers(Session["airport"].ToString(), appsource);
        if (off.Length > 0)
        {
            //foreach (Offer oo in off)
            //{
            //    sb.Append("<div><img src=\"\\client_img\\" + oo.mediafile + "\"></div>");
            //    sb.Append("<div class=\"offertitle\"><a href='offer.aspx?o=" + oo.id + "'>" + oo.offer_header + "</a></div>");
            //    //sb.Append("<div>" + oo.offer_description + "</div>");
            //    sb.Append("<div class=\"offerdeal\">" + oo.offer_deal + "</div>");
            //    sb.Append("<div>&nbsp;</div>");

            //}
            //content = sb.ToString();
            ListView1.DataSource = off;
            ListView1.DataBind();
        }



        //ltOffers.Text = content;
    }

    string lastOfferType = "";
    public string AddGroupingRowIfLastOfferHasChanged()
    {
        string returnvalue = "";
        string currentOfferType = Eval("offertype").ToString();
        if (lastOfferType != currentOfferType)
        {
            lastOfferType = currentOfferType;
            returnvalue = lastOfferType;
        }
        else
        {
            returnvalue = "";
        }
        return returnvalue;

    }
}