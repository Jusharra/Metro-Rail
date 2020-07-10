using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for GeoLocation
/// </summary>
public class GeoLocation
{
    public static string conn = "";
    public string GetDistance(string lat1, string long1, string lat2, string long2, string appsource)
    {
        string returnvalue = "";
        string sql = "geolocation_get_distance " + lat1 + "," + long1 + "," + lat2 + "," + long2;
        SqlConnection conn = new SqlConnection(appsource);
        SqlCommand cmd = new SqlCommand(sql, conn);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.Read())
        {
            returnvalue = dr[0].ToString();
        }
        return returnvalue;
    }
    public string GetNearest(string latitude, string longitude, string appsource)
    {
        StringBuilder sb = new StringBuilder();
        string returnvalue = "";
        string sql = "geolocation_get_nearest " + latitude + "," + longitude;
        SqlConnection conn = new SqlConnection(appsource);
        SqlCommand cmd = new SqlCommand(sql, conn);
        conn.Open();
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.HasRows)
        {
            
            do
            {
                while (dr.Read())
                {
                    sb.AppendLine(dr[0].ToString());               
                }
            } while (dr.NextResult());
            returnvalue = sb.ToString();
        }
        return returnvalue;
    }    
}