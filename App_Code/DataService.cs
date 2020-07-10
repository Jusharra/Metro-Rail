using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


    public class DataService
    {
        
        public DataSet GetDataSet(string sql, string appsource)
        {
            Utilities util = null;
            SqlConnection oConn = null;
            SqlCommand oCmd = null;
            DataSet ods = new DataSet();
            try
            {
                util = new Utilities();
                string sConn = appsource;
                oConn = new SqlConnection(sConn);
                oCmd = new SqlCommand(sql, oConn);
                oCmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(oCmd);
                adapter.Fill(ods);
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally
            {
                if (util != null) util = null;
            }
            return ods;
        }
        public int ExecuteNonQuery(string sql, string appsource)
        {
            int returnvalue = -1;
            Utilities util = null;
            try
            {
                util = new Utilities();
                using (SqlConnection oConn = new SqlConnection(appsource))
                {
                    SqlCommand cmd = oConn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    oConn.Open();
                    returnvalue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            finally
            {
                if (util != null) util = null;
            }
            return returnvalue;
        }
    }

