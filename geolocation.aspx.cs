using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class geolocation : System.Web.UI.Page
{
    string appsource = System.Configuration.ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        string content = this.GetResult();
        if (content == "") content = "nodata";
        Response.Write(content);
    }

    public string GetResult()
    {
        StringBuilder sb = new StringBuilder();
        string result = "";
        bool validrequest = true;
        if (Request["action"] == null)
        {
            validrequest = false;
        }
        if (Request["action"] == null)
        {
            validrequest = false;
        }
        if (validrequest)
        {

            GeoLocation geo = new GeoLocation();
            switch (Request["action"].ToLower())
            {
                case "getdistance":
                    if (Request["lat1"] == "") validrequest = false;
                    if (Request["long1"] == "") validrequest = false;
                    if (Request["lat2"] == "") validrequest = false;
                    if (Request["long2"] == "") validrequest = false;
                    if (validrequest)
                    {
                        string lat1 = Request["lat1"];
                        string long1 = Request["long1"];
                        string lat2 = Request["lat2"];
                        string long2 = Request["long2"];
                        result = geo.GetDistance(lat1, long1, lat2, long2, appsource);
                    }
                    break;
                case "getnearest":
                    if (Request["latitude"] == "") validrequest = false;
                    if (Request["longitude"] == "") validrequest = false;
                    if (validrequest)
                    {
                        string latitude = Request["latitude"];
                        string longitude = Request["longitude"];
                        result = geo.GetNearest(latitude, longitude, appsource);
                        if (result != "") Session["airport"] = result;
                    }
                    break;
                default:
                    validrequest = false;
                    break;
            }
            if (result != "")
            {
                //sb.AppendLine("<Response>");
                sb.AppendLine(result);
                //sb.AppendLine("</Response>");
                result = sb.ToString();
            }
        }
        else
        {
            //sb.AppendLine("<Response>");
            sb.AppendLine("Invalid Request");
            //sb.AppendLine("</Response>");
            result = sb.ToString();
        }
        return result;
    }
}