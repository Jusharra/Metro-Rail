using System;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Xml;
using System.Net.Mail;
using System.Globalization;
using System.Threading;



	public class Utilities 
	{
		public Utilities()
		{
		}

		private static Regex _isNumber = new Regex(@"^\d+$");
		public static bool IsInteger(string theValue)
		{
			Match m = _isNumber.Match(theValue);
			return m.Success;
		}
		public string CensorContent(string RawContent)
		{
			RawContent.Replace("fuck","###");
			RawContent.Replace("nigger","###");
			RawContent.Replace("shit","###");
			RawContent.Replace(" ass "," ### ");
			RawContent.Replace("bitch","###");			
			return RawContent;			
		}
		public bool isMature(string RawContent)
		{
			//TODO:Check
			bool NastyContent = false;
			
			int [] intCountNasty = new int[6];			
			intCountNasty[0] = RawContent.IndexOf("");
			intCountNasty[1] = RawContent.IndexOf("");
			intCountNasty[2] = RawContent.IndexOf("");
			intCountNasty[3] = RawContent.IndexOf("");
			intCountNasty[4] = RawContent.IndexOf("");
			intCountNasty[5] = RawContent.IndexOf("");
			
			for (int intLoop = 0; intLoop < 6; intLoop++)
			{
				if (intCountNasty[intLoop] != -1)
				{
					NastyContent = true;
					break;
				}
			}			
			return NastyContent;			
		}

        public bool HasScriptCharacters(string content)
        {
            bool returnvalue = false;
            int i = content.IndexOfAny(new char[] { '[', '*', '&', '<', '>', '=', '|', '\\', ']' });
            if (i > 0) returnvalue = true;
            return returnvalue;
        }

        public string ClipText(string strText, int length)
        {
            string returnValue = strText;
            try
            {
                if (length > 0)
                {
                    if (strText.Trim().Length > 0)
                    {
                        if (strText.Trim().Length > length)
                        {
                            returnValue = strText.Substring(0, length);
                            int lastspace = returnValue.LastIndexOf(" ");
                            returnValue = strText.Substring(0, lastspace) + " ...";                
                        }
                    }
                }
            }
            catch (Exception ex)
            {
           
                throw ex;
            }            
            return returnValue;
        }

		public string ScrapePage(string url)
		{
				
			string result;
			try
			{
				WebResponse objResponse;
				WebRequest objRequest = System.Net.HttpWebRequest.Create(url);
				objResponse = objRequest.GetResponse();
				using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()) )
				{
					result = sr.ReadToEnd();
					// Close and clean up the StreamReader
					sr.Close();
				}
			}
			catch(Exception ex)
			{
                string errormsg = ex.Message;
				result = "";
			}
	      	
			return result;		
		}
        public void GetLoggyWithThis(string logkey, string logvalue, string ipaddress, string appsource)
		{
			Utilities util = null;
			try
			{
				util = new Utilities();
				using(SqlConnection oConn = new SqlConnection(appsource))
				{
					SqlCommand cmd = oConn.CreateCommand();
					cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "logs_insert '" + logkey.Replace("'", "''") + "','" + logvalue.Replace("'", "''") + "','" + ipaddress + "'";
					oConn.Open();
					cmd.ExecuteNonQuery();				
				}
			}
			catch(Exception ex)
			{
                string error = ex.Message;
			}
			finally
			{
				if(util != null)util=null;
			}
		}
		public string ScrapePostPage(string url, string strPost)
		{
			
			string result = "";
			//String strPost = "x=1&y=2&z=YouPostedOk";
			StreamWriter myWriter = null;
		      
			HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
			objRequest.Method = "POST";
			objRequest.ContentLength = strPost.Length;
			objRequest.ContentType = "application/x-www-form-urlencoded";
		      
			try
			{
				myWriter = new StreamWriter(objRequest.GetRequestStream());
				myWriter.Write(strPost);
			}
			catch (Exception e) 
			{
				return e.Message;
			}
			finally 
			{
				myWriter.Close();
			}
		         
			try
			{
				HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
				using (StreamReader sr = 
						   new StreamReader(objResponse.GetResponseStream()) )
				{
					result = sr.ReadToEnd();
		
					// Close and clean up the StreamReader
					sr.Close();
				}
			}
			catch
			{
				result = "";
			}


			return result;
		}

		public string GetParseString(string strPage, string strStart, string strEnd)
		{
			string returnvalue = string.Empty;
			try
			{
				int pos = 1;
				pos = strPage.IndexOf(strStart,pos);
				int sStart = pos + strStart.Length;
				int eos = strPage.IndexOf(strEnd,pos);
				int sLength = eos - pos - strStart.Length;
				string ParseString = strPage.Substring(sStart,sLength);
				returnvalue = ParseString;
			}
			catch
			{
			}
			return returnvalue;
		}
		public string PrepContent(string content)
		{
			//remove all tabs,carriage returns and line breaks from content
			content = content.Replace("\u000D","  ");
			content = content.Replace("\u000A","  ");
			content = content.Replace("\u0009","  "); //tabl
			//whitespace killa
			content = content.Replace("       ","  ");
			content = content.Replace("      ","  ");
			content = content.Replace("     ","  ");
			content = content.Replace("    ","  ");
			content = content.Replace("   ","  ");
			content = content.Replace("  "," ");
			return content;
		}
		public string PrepDBText(string strText)
		{	
			string formattedText = strText;            
			formattedText = strText.Replace("'","''");
			return formattedText.Trim();
		}
		public string CleanInput(string strIn)
		{
			string returnvalue = strIn;
			returnvalue = this.stripHtml(returnvalue);
			/*if(returnvalue == "-")returnvalue = "";
			if(returnvalue == "<")returnvalue = "";
			if(returnvalue == ">")returnvalue = "";	
			if(returnvalue == "&")returnvalue = "";
			*/
			if(returnvalue.EndsWith(","))returnvalue = returnvalue.Substring(0,returnvalue.Length);
			returnvalue = returnvalue.Replace("...","");
			returnvalue = returnvalue.Replace("''","'");
			returnvalue = returnvalue.Replace("''","'");
			//returnvalue = returnvalue.Replace(","," ");
			return returnvalue;
		}

		public string stripHtml(string strHtml) 
		{ 
			//Strips the HTML tags from strHTML 
			System.Text.RegularExpressions.Regex objRegExp 
				= new System.Text.RegularExpressions.Regex("<(.|\n)+?>"); 

			// Replace all tags with a space, otherwise words either side 
			// of a tag might be concatenated 
			string strOutput = objRegExp.Replace(strHtml, " "); 

			// Replace all < and > with < and > 
			strOutput = strOutput.Replace("<", "<"); 
			strOutput = strOutput.Replace(">", ">"); 

			return strOutput; 
		}
		
		//get the value from an XML file.
        //public string getConfigValue(string sKey, string appsource)
        //{   
        //    string returnvalue = string.Empty;
        //    string configpath = @"c:\scorpion\" + appsource + ".xml";
        //    XmlDocument xdoc = new XmlDocument();
        //    xdoc.Load(configpath);
        //    //xdoc.Load(@"c:\scorpion\fooky.xml");
        //    XmlNodeList xnl = xdoc.SelectNodes("/fooky/key");
        //    foreach(XmlNode n in xnl)
        //    {				
        //        if(n.Attributes.Item(0).Value == sKey)
        //        {
        //            returnvalue = n.Attributes.Item(1).Value;
        //            break;
        //        }
        //    }
        //    return returnvalue;
        //}
        public int sendEmailMessage(string from, string to, string subject, string htmlmessage, string ipaddress, string appsource)
		{
			int returnvalue = 0;
			try
			{
                SmtpClient client = new SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                //client.Credentials = new System.Net.NetworkCredential("info@mypagecom", "password");
                client.Port = 25;
                client.EnableSsl = false;
                using (MailMessage oMsg = new MailMessage())
                {
                    oMsg.From = new MailAddress(from);
				    oMsg.To.Add(new MailAddress(to));
                    oMsg.IsBodyHtml = true;
				    oMsg.Subject = subject;
                    oMsg.Body = htmlmessage;
                    client.Send(oMsg);
                }
				returnvalue = 1;
				this.GetLoggyWithThis("Email Message",from + ":" + htmlmessage.Replace("'","''"),ipaddress,appsource);
				
			}
			catch(Exception ex)
			{	
				string c = ex.Message;
				returnvalue = -1;
			}
			return returnvalue;
		}
		public bool IsXMLWellFormed(string content)
		{
			bool returnvalue = false;
//			XmlDocument xdoc = null;
//			try
//			{
//				xdoc = new XmlDocument();
//				xdoc.LoadXml(content);
				returnvalue = true;
//			}
//			catch
//			{
//			}
//			finally
//			{
//				if(xdoc != null)xdoc = null;
//			}
			return returnvalue;
		}
		public bool hasillegalcharacters(string dataline)
		{
			bool returnvalue = false;
			if(dataline.IndexOf("/") > -1)returnvalue = true;
			if(dataline.IndexOf("?") > -1)returnvalue = true;
			if(dataline.IndexOf("<") > -1)returnvalue = true;
			if(dataline.IndexOf(">") > -1)returnvalue = true;
			if(dataline.IndexOf("\\") > -1)returnvalue = true;
			if(dataline.IndexOf(":") > -1)returnvalue = true;
			if(dataline.IndexOf("*") > -1)returnvalue = true;
			if(dataline.IndexOf("|") > -1)returnvalue = true;
			if(dataline.IndexOf("\"") > -1)returnvalue = true;			
			if(dataline.IndexOf("[") > -1)returnvalue = true;
			if(dataline.IndexOf("]") > -1)returnvalue = true;
			if(dataline.IndexOf("+") > -1)returnvalue = true;
			if(dataline.IndexOf(";") > -1)returnvalue = true;
			if(dataline.IndexOf("=") > -1)returnvalue = true;
            if (dataline.IndexOf("!") > -1) returnvalue = true;
            if (dataline.IndexOf("@") > -1) returnvalue = true;
            if (dataline.IndexOf("#") > -1) returnvalue = true;
            if (dataline.IndexOf("$") > -1) returnvalue = true;
            if (dataline.IndexOf("%") > -1) returnvalue = true;
            if (dataline.IndexOf("^") > -1) returnvalue = true;
            if (dataline.IndexOf("&") > -1) returnvalue = true;
            if (dataline.IndexOf("*") > -1) returnvalue = true;
            if (dataline.IndexOf("(") > -1) returnvalue = true;
            if (dataline.IndexOf("~") > -1) returnvalue = true;
            if (dataline.IndexOf("`") > -1) returnvalue = true;
            if (dataline.IndexOf(")") > -1) returnvalue = true;
            if (dataline.IndexOf("_") > -1) returnvalue = true;
            if (dataline.IndexOf("-") > -1) returnvalue = true;
            if (dataline.IndexOf("'") > -1) returnvalue = true;
            if (dataline.IndexOf(",") > -1) returnvalue = true;
            if (dataline.IndexOf(".") > -1) returnvalue = true; 

			return returnvalue;
		}	

		public string getProperCase(string content)
		{
			string returnvalue = content;
			CultureInfo cultureInfo   = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;
			returnvalue = textInfo.ToTitleCase(content);
			return returnvalue;
		}

        public int getRandomNumber(int min, int max, int seed)
        {
            int returnvalue = 0;
            Random random = new Random(System.DateTime.Now.Millisecond * seed);   
            returnvalue = random.Next(min, max);
            return returnvalue;            
        }
        public string generateGUID()
        {
            return System.Guid.NewGuid().ToString();
        }
        private DateTime FormatDate(string date)
        {
            string RFC822 = "ddd, dd MMM yyyy HH:mm:ss zzz";

            //string RFC1123 = "yyyyMMddTHHmmss";


            //string RFCUnknown = "yyyy-MM-ddTHH:mm:ssZ";


            int indexOfPlus = date.LastIndexOf('+');
            if (indexOfPlus > 0)
                date = date.Substring(0, indexOfPlus - 1);

            string[] formats = new string[] { "r", "S", "U" };
            try
            {
                // Parse the dates using the standard

                // universal date format

                return DateTime.Parse(date,
                     CultureInfo.InvariantCulture,
                     DateTimeStyles.AdjustToUniversal);
            }
            catch
            {
                try
                {
                    // Standard formats failed, try the "r" "S"

                    // and "U" formats


                    return DateTime.ParseExact(date, formats,
                              DateTimeFormatInfo.InvariantInfo,
                              DateTimeStyles.AdjustToUniversal);
                }
                catch
                {
                    try
                    {
                        // All the standards formats have failed,


                        //try the dreaded RFC822 format

                        return DateTime.ParseExact(date, RFC822,
                                DateTimeFormatInfo.InvariantInfo,
                                DateTimeStyles.AdjustToUniversal);
                    }
                    catch
                    {
                        // All failed! The RSS Feed source

                        // should be sued


                        return DateTime.Now.AddYears(-3);
                        
                    }
                }
            }
        }       
	}

