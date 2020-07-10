using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//QRCode
using System.Drawing.Imaging;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

public partial class _2dimage : System.Web.UI.Page
{
    private string appsource = "";
    long mid = -1;

    protected void Page_Load(object sender, EventArgs e)
    {        
        string ocr = Request["d"].Trim();
        if (ocr.Length < 300)
        {
         generateOCR(ocr);
        }
    }

    private void generateOCR(string ocrData)
    {
        //if (txtEncodeData.Text.Trim() == String.Empty)
        //{
        //    MessageBox.Show("Data must not be empty.");
        //    return;
        //}

        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        String encoding = "Byte";
        if (encoding == "Byte")
        {
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        }
        else if (encoding == "AlphaNumeric")
        {
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
        }
        else if (encoding == "Numeric")
        {
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
        }
        try
        {
            int scale = 6;
            qrCodeEncoder.QRCodeScale = scale;
        }
        catch (Exception ex)
        {
        }
        try
        {
            int version = 7;
            qrCodeEncoder.QRCodeVersion = version;
        }
        catch (Exception ex)
        {

        }

        string errorCorrect = "M";
        if (errorCorrect == "L")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
        else if (errorCorrect == "M")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
        else if (errorCorrect == "Q")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
        else if (errorCorrect == "H")
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

        System.Drawing.Image image;
        String data = ocrData;
        image = qrCodeEncoder.Encode(data);
        //picEncode.Image = image;  
        Response.ContentType = "image/jpeg";
        image.Save(Response.OutputStream, ImageFormat.Jpeg);
        image.Dispose();
    }


    private long GetOfferId()
    {
        long returnvalue = -1;
        if (Session["offermedia_id"] != null)
        {
            returnvalue = Convert.ToInt64(Session["offermedia_id"].ToString());
        }
        else
        {
            Response.Redirect("offers.aspx", true);
        }
        return returnvalue;
    }
}
