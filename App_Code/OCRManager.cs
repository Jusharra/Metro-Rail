using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


    public class OCR
    {
        private long _id = -1;
        private long _memberid = -1;
        private string _ocrCode = string.Empty;
        private string _image = string.Empty;
        private string _ocrTitle = string.Empty;
        private string _ocrDescription = string.Empty;
        private int _active = 0;
        private DateTime _dateadded = new DateTime();
        private DateTime _last_access_date = new DateTime();
        private long _access_count = 0;

        public long id
        {
            get { return _id; }
            set { _id = value; }
        }
        public long memberid
        {
            get { return _memberid; }
            set { _memberid = value; }
        }
        public string ocrCode
        {
            get { return _ocrCode; }
            set { _ocrCode = value; }
        }
        public string image
        {
            get { return _image; }
            set { _image = value; }
        }
        public string ocrTitle
        {
            get { return _ocrTitle; }
            set { _ocrTitle = value; }
        }
        public string ocrDescription
        {
            get { return _ocrDescription; }
            set { _ocrDescription = value; }
        }
        public int active
        {
            get { return _active; }
            set { _active = value; }
        }
        public DateTime dateadded
        {
            get { return _dateadded; }
            set { _dateadded = value; }
        }
        public DateTime last_access_date
        {
            get { return _last_access_date; }
            set { _last_access_date = value; }
        }
        public long access_count
        {
            get { return _access_count; }
            set { _access_count = value; }
        }
    }

    public class OCRManager
    {
        public long Activate(OCR o, string appsource)
        {
            long returnvalue = -1;
            o.active = 1;
            returnvalue = Update(o, appsource);
            return returnvalue;
        }
        public long Deactivate(OCR o, string appsource)
        {
            long returnvalue = -1;
            o.active = 0;
            returnvalue = Update(o, appsource);
            return returnvalue;
        }

        public long Update(OCR o, string appsource)
        {
            int returnvalue = -1;
            StringBuilder sql = new StringBuilder();
            sql.Append("sp_ocr_update ");
            sql.Append(o.id + ",");
            sql.Append(o.memberid + ",");
            sql.Append("'" + o.ocrCode + "',");
            sql.Append("'" + o.image + "',");
            sql.Append("'" + o.ocrTitle + "',");
            sql.Append("'" + o.ocrDescription + "',");            
            sql.Append(o.active + ",");
            sql.Append("'" + o.dateadded + "',");
            sql.Append("'" + o.last_access_date + "',");
            sql.Append(o.access_count);
            DataService ds = new DataService();
            returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
            return returnvalue;
        }

       

        public OCR[] SelectByMemberId(long memberid, string appsource)
        {
            OCR[] returnvalue = new OCR[0];
            string sql = "sp_ocr_select_memberid " + memberid;
            DataService ds = new DataService();
            DataSet dataset = ds.GetDataSet(sql, appsource);
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new OCR[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    OCR o = new OCR();
                    o.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["ocr_id"].ToString());
                    o.memberid = Convert.ToInt64(dataset.Tables[0].Rows[i]["member_id"].ToString());
                    o.ocrCode = dataset.Tables[0].Rows[i]["ocr_code"].ToString();
                    o.image = dataset.Tables[0].Rows[i]["ocr_image"].ToString();
                    o.ocrTitle = dataset.Tables[0].Rows[i]["ocr_title"].ToString();
                    o.ocrDescription = dataset.Tables[0].Rows[i]["ocr_description"].ToString();                    
                    o.active = Convert.ToInt32(dataset.Tables[0].Rows[i]["active"].ToString());
                    o.dateadded = Convert.ToDateTime(dataset.Tables[0].Rows[i]["date_added"].ToString());
                    o.last_access_date = Convert.ToDateTime(dataset.Tables[0].Rows[i]["last_access_date"].ToString());
                    o.access_count = Convert.ToInt64(dataset.Tables[0].Rows[i]["access_count"].ToString());
                    returnvalue[i] = o;
                    o = null;
                }
            }
            return returnvalue;
        }
       
    }

