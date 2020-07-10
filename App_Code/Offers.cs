using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// Summary description for Offers
/// </summary>
public class Offer
{
    public Guid id { get; set; }
    public string vendor_id { get; set; }
    public string location_id { get; set; }
    public string offertype_id { get; set; }
    public string offertype { get; set; }
    public string display_code { get; set; }
    public string media_id { get; set; }
    public string mediafile { get; set; }
    public string offer_header { get; set; }
    public string offer_description { get; set; }
    public string offer_deal { get; set; }
    public DateTime begin_date { get; set; }
    public DateTime end_date { get; set; }


    public Offer[] GetOffers(string location,string appsource)
    {
        Offer[] returnvalue = new Offer[0];
        string sql = "offers_get_location '" + location.Replace("\r\n","").Replace("'","''") + "'";
        DataSet dataset = new DataSet();
        SqlConnection oConn = new SqlConnection(appsource);
        SqlCommand oCmd = oCmd = new SqlCommand(sql, oConn);
        oCmd.CommandType = CommandType.Text;
        SqlDataAdapter adapter = new SqlDataAdapter(oCmd);
        adapter.Fill(dataset);
        if (dataset.Tables.Count > 0)
        {
            returnvalue = new Offer[dataset.Tables[0].Rows.Count];
            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                Offer p = new Offer();
                p.id = new Guid(dataset.Tables[0].Rows[i]["id"].ToString());
                p.offertype = dataset.Tables[0].Rows[i]["offer_type"].ToString();
                p.display_code = dataset.Tables[0].Rows[i]["display_code"].ToString();
                p.mediafile = dataset.Tables[0].Rows[i]["media_file"].ToString();
                p.offer_header = dataset.Tables[0].Rows[i]["offer_header"].ToString();
                p.offer_description = dataset.Tables[0].Rows[i]["offer_description"].ToString();
                p.offer_deal = dataset.Tables[0].Rows[i]["offer_deal"].ToString();
                p.begin_date = Convert.ToDateTime(dataset.Tables[0].Rows[i]["begin_date"].ToString());
                p.end_date = Convert.ToDateTime(dataset.Tables[0].Rows[i]["end_date"].ToString());
                returnvalue[i] = p;
                p = null;
            }
        }
        return returnvalue;
    }
    public Offer GetOfferDetail(Guid id,string appsource)
    {
        Offer returnval = new Offer();
        string sql = "offers_get_detail '" + id + "'";
        SqlConnection oConn = null;
        SqlCommand oCmd = null;
        SqlDataReader dr = null;
        try
        {
            oConn = new SqlConnection(appsource);
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql;
            oConn.Open();
            dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            { 
                returnval.id = new Guid(dr["id"].ToString());
                returnval.offertype = dr["offer_type"].ToString();
                returnval.display_code = dr["display_code"].ToString();
                returnval.mediafile = dr["media_file"].ToString();
                returnval.offer_header = dr["offer_header"].ToString();
                returnval.offer_description = dr["offer_description"].ToString();
                returnval.offer_deal = dr["offer_deal"].ToString();
                returnval.begin_date = Convert.ToDateTime(dr["begin_date"].ToString());
                returnval.end_date = Convert.ToDateTime(dr["end_date"].ToString());
            }
        }
        catch (Exception ex)
        {
          
        }
        finally
        {
            if (oConn != null)
            {
                oConn.Close();
                oConn.Dispose();
            }
        }
        return returnval;
    }

    public void InsertOffer(Offer o, string appsource)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("offers_insert ");
        sb.Append("'" + o.vendor_id + "',");
        sb.Append("'" + o.location_id +"',");
        sb.Append("'" + o.offertype_id +"',");
        sb.Append("'" + o.media_id + "',");
        sb.Append("'" + o.offer_header +"',");
        sb.Append("'" + o.offer_description +"',");
        sb.Append("'" + o.offer_deal +"',");
        sb.Append("'" + o.begin_date +"',");
        sb.Append("'" + o.end_date +"'");     
        string sql = sb.ToString();
        SqlConnection conn = new SqlConnection(appsource);
        SqlCommand cmd = new SqlCommand(sql, conn);
        conn.Open();
        long i = cmd.ExecuteNonQuery();
        conn.Close();
    }
}

