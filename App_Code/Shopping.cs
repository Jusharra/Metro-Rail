using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public class Product
{
    private long _id = -1;
    private long _manufacturer_id = -1;
    private long _line_id = -1;
    private string _sku = "";
    private string _product_name = string.Empty;
    private string _product_description = string.Empty;
    private string _image_url = string.Empty;
    private decimal _average_cost = 0.00M;
    private decimal _retail_price = 0.00M;
    private string _special_order = string.Empty;
    private string _size = string.Empty;

    public long id
    {
        get { return _id; }
        set { _id = value; }
    }
    public long manufacturer_id
    {
        get { return _manufacturer_id; }
        set { _manufacturer_id = value; }
    }
    public long line_id
    {
        get { return _line_id; }
        set { _line_id = value; }
    }
    public string sku
    {
        get { return _sku; }
        set { _sku = value; }
    }
    public string product_name
    {
        get { return _product_name; }
        set { _product_name = value; }
    }
    public string product_description
    {
        get { return _product_description; }
        set { _product_description = value; }
    }
    public string image_url
    {
        get { return _image_url; }
        set { _image_url = value; }
    }
    public decimal average_cost
    {
        get { return _average_cost; }
        set { _average_cost = value; }
    }
    public decimal retail_price
    {
        get { return _retail_price; }
        set { _retail_price = value; }
    }
    public string special_order
    {
        get { return _special_order; }
        set { _special_order = value; }
    }
    public string size
    {
        get { return _size; }
        set { _size = value; }
    }
}

public class ProductNutrition
{
    private long _id = -1;
    private long _product_id = -1;
    private int _calories = -1;
    private int _total_fat = -1;
    private int _sodium = -1;
    private int _carbs = -1;
    private int _sugars = -1;
    private int _protein = -1;

    public long id 
    {
        get { return _id; }
        set { _id = value; }
    }
    public long product_id 
    {
        get { return _product_id; }
        set { _product_id = value; }
    }
    public int calories 
    {
        get { return _calories; }
        set { calories = value; }
    }
    public int total_fat 
    {
        get { return _total_fat; }
        set { _total_fat = value; }
    }
    public int sodium 
    {
        get { return _sodium; }
        set { _sodium = value; }
    }
    public int carbs 
    {
        get { return _carbs; }
        set { _carbs = value; }
    }
    public int sugars 
    {
        get { return _sugars; }
        set { _sugars = value; }
    }
    public int protein 
    {
        get { return _protein; }
        set { _protein = value; }
    }


    

}

[Serializable()]
public class Cart
{
    public long id = -1;
    public long member_id = -1;
    public decimal total_price = 0.00M;
    public decimal total_discount = 0.00M;
    public decimal total_taxes = 0.00M;
    public decimal grand_total = 0.00M;
}

[Serializable()]
public class CartProducts
{
    public long id = -1;
    public long cart_id = -1;
    public long product_id = -1;
    public decimal price = 0.00M;
}

[Serializable()]
public class DebitOrder
{
    public long id = -1;
    public long cart_id = -1;
    public string display_id = "";
    public long member_id = -1;
    public long account_id = -1;
    public string order_description = "";
    public string order_status = "";
    public decimal total_taxes = 0.00M;
    public decimal total_discount = 0.00M;
    public decimal total_cost = 0.00M;
    public DateTime create_date = new DateTime(2000, 1, 1);
}

public class DebitOrderDetail
{
    public long id = -1;
    public long order_id = -1;
    public long product_id = -1;
    public long item_id = -1;
    public decimal retail_price = 0.00M;
    public decimal total_discounts = 0.00M;
    public decimal total_taxes = 0.00M;
    public decimal total_price = 0.00M;
    public string product_name = "";
}

public class ProductItem
{
    public long id { get; set; }
    public long product_id { get; set; }
    public DateTime expiration_date = new DateTime(2099, 1, 1);
    public DateTime date_entered = new DateTime(2000, 1, 1);
    public string location = "";

    public long Create(ProductItem p, string conn)
    {
        long returnvalue = -1;
        using (SqlConnection oConn = new SqlConnection(conn))
        {
            SqlCommand cmd = oConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "item_insert";

            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.BigInt);
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@product_id", p.product_id);
            cmd.Parameters.AddWithValue("@expiration_date", p.expiration_date);
            oConn.Open();
            cmd.ExecuteNonQuery();
            returnvalue = Convert.ToInt64(cmd.Parameters["@id"].Value);
        }
        return returnvalue;
    }

    public ProductItem[] SelectItemByProductId(long product_id, string appsource)
    {
        ProductItem [] returnvalue = new ProductItem[0];
        string sql = "product_item_select_productid " + product_id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new ProductItem[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    ProductItem p = new ProductItem();
                    p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    returnvalue[i] = p;
                    p = null;
                }
            }
        }
        return returnvalue;
    }

    public long UpdateLocationAsGhost(long item_id, string appsource)
    {
        long returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("update product_item set location = 'ghost', date_claimed = getdate() where id = " + item_id);
        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }
}



[Serializable()]
public class ProductManager
{
    public Product[] SelectProductsByCategory(string categoryname, string appsource)
    {
        Product[] returnvalue = new Product[0];
        string sql = "product_select_category '" + categoryname.Replace("_", " ") + "'";
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new Product[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    Product p = new Product();
                    p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    p.sku = dataset.Tables[0].Rows[i]["sku"].ToString();
                    p.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                    p.image_url = dataset.Tables[0].Rows[i]["image_url"].ToString();
                    p.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                    p.manufacturer_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["manufacturer_id"].ToString());
                    returnvalue[i] = p;
                    p = null;
                }
            }
        }
        return returnvalue;
    }

    public Product SelectProductById(long id, string appsource)
    {
        Product returnvalue = new Product();
        string sql = "select * from product where id = " + id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {      
                if(dataset.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    Product p = new Product();
                    p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    p.sku = dataset.Tables[0].Rows[i]["sku"].ToString();
                    p.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                    p.image_url = dataset.Tables[0].Rows[i]["image_url"].ToString();
                    p.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                    p.product_description = dataset.Tables[0].Rows[i]["product_description"].ToString();
                    p.size = dataset.Tables[0].Rows[i]["product_size"].ToString();
                    p.manufacturer_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["manufacturer_id"].ToString());
                    returnvalue = p;
                }         
        }
        return returnvalue;
    }

    public Product SelectProductBySKU(string sku, string appsource)
    {
        Product returnvalue = new Product();
        string sql = "select * from product where sku = '" + sku + "'";
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                Product p = new Product();
                p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                p.sku = dataset.Tables[0].Rows[i]["sku"].ToString();
                p.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                p.image_url = dataset.Tables[0].Rows[i]["image_url"].ToString();
                p.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                p.product_description = dataset.Tables[0].Rows[i]["product_description"].ToString();
                p.size = dataset.Tables[0].Rows[i]["product_size"].ToString();
                p.manufacturer_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["manufacturer_id"].ToString());
                returnvalue = p;
            }
        }
        return returnvalue;
    }


    public Product [] SelectProductByCartId(long cart_id, string appsource)
    {
        Product[] returnvalue = new Product[0];
        string sql = "cart_products_select_cartid " + cart_id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new Product[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    Product p = new Product();
                    p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    p.sku = dataset.Tables[0].Rows[i]["sku"].ToString();
                    p.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                    p.image_url = dataset.Tables[0].Rows[i]["image_url"].ToString();
                    p.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                    p.manufacturer_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["manufacturer_id"].ToString());
                    returnvalue[i] = p;
                    p = null;
                }
            }
        }
        return returnvalue;
    }

    public long AddFavorites(long memberid, long productid, string appsource)
    {
        long returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("member_favorites_add ");
        sql.Append(memberid + ",");
        sql.Append(productid);
        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }
    public long RemoveFavorites(long memberid, long productid, string appsource)
    {
        long returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("member_favorites_remove ");
        sql.Append(memberid + ",");
        sql.Append(productid);
        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }

    public Product[] SelectFavorites(long memberid, string appsource)
    {
        Product[] returnvalue = new Product[0];
        string sql = "member_favorites_select " + memberid;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new Product[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    Product p = new Product();
                    p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    p.sku = dataset.Tables[0].Rows[i]["sku"].ToString();
                    p.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                    p.image_url = dataset.Tables[0].Rows[i]["image_url"].ToString();
                    p.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                    p.manufacturer_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["manufacturer_id"].ToString());
                    returnvalue[i] = p;
                    p = null;
                }
            }
        }
        return returnvalue;
    }
    public Product[] SelectBestSellers(string appsource)
    {
        Product[] returnvalue = new Product[0];
        string sql = "product_select_bestsellers";
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new Product[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    Product p = new Product();
                    p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    p.sku = dataset.Tables[0].Rows[i]["sku"].ToString();
                    p.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                    p.image_url = dataset.Tables[0].Rows[i]["image_url"].ToString();
                    p.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                    p.manufacturer_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["manufacturer_id"].ToString());
                    returnvalue[i] = p;
                    p = null;
                }
            }
        }
        return returnvalue;
    }



    public int SelectProductInventory(long id, string appsource)
    {
        int returnvalue = 0;
        return returnvalue;
    }
    public string SelectProductManufacturer(long id, string appsource)
    {
        string returnvalue = "";
        string sql = "select manufacturer_name from Product_Manufacturer where id =  " + id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables[0].Rows.Count > 0)
            {
                returnvalue = dataset.Tables[0].Rows[0]["manufacturer_name"].ToString();
            }
        }
        return returnvalue;
    }

    public string SelectCategoryImage(string categoryname, string appsource)
    {
        string returnvalue = "";
        string sql = "product_image_getcategory '" + categoryname + "'";
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables[0].Rows.Count > 0)
            {
                returnvalue = dataset.Tables[0].Rows[0]["image_url"].ToString();
            }
        }
        return returnvalue;
    }

    public long SelectProductInventoryCount(long product_id, string appsource)
    {
        long returnvalue = -1;
        string sql = "select COUNT(product_id) from Product_Item where date_claimed is null and product_id = " + product_id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables[0].Rows.Count > 0)
            {
                returnvalue = Convert.ToInt64(dataset.Tables[0].Rows[0][0].ToString());
            }
        }
        return returnvalue;
    }
 }

public class ProductNutritionManager
{
    public long Insert(ProductNutrition p, string appsource)
    {
        long returnvalue = -1;

        Utilities util = new Utilities();
        using (SqlConnection oConn = new SqlConnection(appsource))
        {
            SqlCommand cmd = oConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "product_nutrition_insert";

            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.BigInt);
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@product_id", p.product_id);
            cmd.Parameters.AddWithValue("@calories", p.calories);
            cmd.Parameters.AddWithValue("@total_fat", p.total_fat);
            cmd.Parameters.AddWithValue("@sodium", p.sodium);
            cmd.Parameters.AddWithValue("@total_carbs", p.carbs);
            cmd.Parameters.AddWithValue("@sugars", p.sugars);
            cmd.Parameters.AddWithValue("@protein", p.protein);           

            oConn.Open();
            cmd.ExecuteNonQuery();
            returnvalue = Convert.ToInt64(cmd.Parameters["@id"].Value);
        }
        return returnvalue;
    }
    public int Update(ProductNutrition p, string appsource)
    {

        int returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("product_nutrition_update ");
        sql.Append(p.id + ",");
        sql.Append(p.product_id + ",");
        sql.Append(p.calories + ",");
        sql.Append(p.total_fat + ",");
        sql.Append(p.sodium + ",");
        sql.Append(p.carbs + ",");
        sql.Append(p.sugars + ",");
        sql.Append(p.protein);

        

        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }
    public ProductNutrition SelectByProductId(long product_id, string appsource)
    {

        ProductNutrition returnvalue = new ProductNutrition();
        string sql = "select * from product_nutrition where product_id = " + product_id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                ProductNutrition p = new ProductNutrition();
                p.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                p.product_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["product_id"].ToString());
                p.calories = Convert.ToInt32(dataset.Tables[0].Rows[i]["calories"].ToString());
                p.total_fat = Convert.ToInt32(dataset.Tables[0].Rows[i]["total_fat"].ToString());
                p.sodium = Convert.ToInt32(dataset.Tables[0].Rows[i]["sodium"].ToString());
                p.carbs = Convert.ToInt32(dataset.Tables[0].Rows[i]["total_carbohydrates"].ToString());
                p.sugars = Convert.ToInt32(dataset.Tables[0].Rows[i]["sugars"].ToString());
                p.protein = Convert.ToInt32(dataset.Tables[0].Rows[i]["protein"].ToString());
                returnvalue = p;
            }
        }
        return returnvalue;
    }

}

[Serializable()]
public class CartManager
{

    public long CreateCart(long memberid, string appsource)
    {
        long returnvalue = -1;

        Utilities util = new Utilities();
        using (SqlConnection oConn = new SqlConnection(appsource))
        {
            SqlCommand cmd = oConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cart_create";

            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.BigInt);
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@member_id", memberid);

            oConn.Open();
            cmd.ExecuteNonQuery();
            returnvalue = Convert.ToInt64(cmd.Parameters["@id"].Value);
        }
        return returnvalue;
    }
    public Cart GetLatestCart(long memberid, string appsource)
    {
        Cart returnvalue = new Cart();
        string sql = "cart_member_select_latest " + memberid;
        Utilities util = null;
        SqlConnection oConn = null;
        SqlCommand oCmd = null;
        SqlDataReader dr = null;
        try
        {
            util = new Utilities();
            oConn = new SqlConnection(appsource);
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql;
            oConn.Open();
            dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                returnvalue.id = Convert.ToInt32(dr["id"].ToString());
               // returnvalue.total_price = Convert.ToDecimal(dr["total_price"].ToString());
               // returnvalue.total_discount = Convert.ToDecimal(dr["total_discount"].ToString());
                //returnvalue.total_taxes = Convert.ToDecimal(dr["total_taxes"].ToString());
                //returnvalue.grand_total = Convert.ToDecimal(dr["grand_total"].ToString());
            }
        }
        catch (Exception ex)
        {
      
            throw ex;
        }
        finally
        {
            if (oConn != null)
            {
                oConn.Close();
                oConn.Dispose();
            }
            if (util != null) util = null;
        }
        return returnvalue;
    }

    public long GetCartCount(long memberid, string appsource)
    {
        long returnvalue = -1;
        string sql = "cart_member_select_count " + memberid;
        Utilities util = null;
        SqlConnection oConn = null;
        SqlCommand oCmd = null;
        SqlDataReader dr = null;
        try
        {
            util = new Utilities();
            oConn = new SqlConnection(appsource);
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql;
            oConn.Open();
            dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())
            {
                returnvalue = Convert.ToInt64(dr["item_count"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (oConn != null)
            {
                oConn.Close();
                oConn.Dispose();
            }
            if (util != null) util = null;
        }   
        return returnvalue;
    }

    public long AddProductToCart(long cart_id, long product_id, decimal price, string appsource)
    {
        long returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("cart_products_add ");
        sql.Append(cart_id + ",");
        sql.Append(product_id + ",");
        sql.Append(price);
        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }
    public long RemoveProductFromCart(long cart_id, string sku, string appsource)
    {
        long returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("cart_products_remove_sku ");
        sql.Append(cart_id + ",");
        sql.Append("'" + sku + "'");
        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }

    public long ClearCart(long cart_id, string appsource)
    {
        long returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("update cart set endtime = '" + DateTime.Now + "'");
        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }

    public decimal GetTaxes(long product_id, decimal price, string appsource)
    {
        decimal returnvalue = 0.00M;
        
        string sql = "cart_products_select_taxes " + product_id;
        Utilities util = null;
        SqlConnection oConn = null;
        SqlCommand oCmd = null;
        SqlDataReader dr = null;
        try
        {
            util = new Utilities();
            oConn = new SqlConnection(appsource);
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql;
            oConn.Open();
            dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    returnvalue = returnvalue + (Convert.ToDecimal(dr["tax_rate"].ToString()) * price);
                }
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (oConn != null)
            {
                oConn.Close();
                oConn.Dispose();
            }
            if (util != null) util = null;
        }

        return returnvalue;
    }
    public decimal GetDiscounts(long product_id, string appsource)
    {
        decimal returnvalue = 0.00M;
        


       
        return returnvalue;





    }

}

public class DebitOrderManager
{

    public long Insert(DebitOrder d, string appsource)
    {
        long returnvalue = -1;

        Utilities util = new Utilities();
        using (SqlConnection oConn = new SqlConnection(appsource))
        {
            SqlCommand cmd = oConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "debitorder_insert";

            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.BigInt);
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@cart_id", d.cart_id);
            cmd.Parameters.AddWithValue("@display_id", d.display_id);
            cmd.Parameters.AddWithValue("@member_id", d.member_id);
            cmd.Parameters.AddWithValue("@account_id", d.account_id);
            cmd.Parameters.AddWithValue("@order_description", d.order_description);
            cmd.Parameters.AddWithValue("@order_status", d.order_status);           

            oConn.Open();
            cmd.ExecuteNonQuery();
            returnvalue = Convert.ToInt64(cmd.Parameters["@id"].Value);
        }
        return returnvalue;
    }

    public long InsertDetail(DebitOrderDetail d, string appsource)
    {
        long returnvalue = -1;

        Utilities util = new Utilities();
        using (SqlConnection oConn = new SqlConnection(appsource))
        {
            SqlCommand cmd = oConn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "debitorder_details_insert";

            SqlParameter param = cmd.Parameters.Add("@id", SqlDbType.BigInt);
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@order_id", d.order_id);
            cmd.Parameters.AddWithValue("@product_id", d.product_id);
            cmd.Parameters.AddWithValue("@item_id", d.item_id);
            cmd.Parameters.AddWithValue("@retail_price", d.retail_price);
            cmd.Parameters.AddWithValue("@total_discounts", d.total_discounts);
            cmd.Parameters.AddWithValue("@total_taxes", d.total_taxes);
            cmd.Parameters.AddWithValue("@total_price", d.total_price);
          
            oConn.Open();
            cmd.ExecuteNonQuery();
            returnvalue = Convert.ToInt64(cmd.Parameters["@id"].Value);
        }
        return returnvalue;
    }


    public int Update(DebitOrder d, string appsource)
    {

        int returnvalue = -1;
        StringBuilder sql = new StringBuilder();
        sql.Append("debitorder_update ");
        sql.Append(d.id + ",");
        sql.Append("'" + d.display_id + "',");
        sql.Append(d.member_id + ",");
        sql.Append(d.account_id + ",");
        sql.Append("'" + d.order_description + "',");
        sql.Append("'" + d.order_status + "',");
        sql.Append(d.total_taxes + ",");
        sql.Append(d.total_discount + ",");
        sql.Append(d.total_cost);



        DataService ds = new DataService();
        returnvalue = ds.ExecuteNonQuery(sql.ToString(), appsource);
        return returnvalue;
    }




    public long PickProductItemFromStock(long product_id, string appsource)
    {
        long returnvalue = -1;
        string sql = "item_pick " + product_id;
        Utilities util = null;
        SqlConnection oConn = null;
        SqlCommand oCmd = null;
        SqlDataReader dr = null;
        try
        {
            util = new Utilities();
            oConn = new SqlConnection(appsource);
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql;
            oConn.Open();
            dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    if (dr["id"].ToString() != "")
                    {
                        returnvalue = Convert.ToInt64(dr["id"].ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (oConn != null)
            {
                oConn.Close();
                oConn.Dispose();
            }
            if (util != null) util = null;
        }
        return returnvalue;
    }


    public DebitOrder GetOrderByConfirmationCode(string confirm_code, string appsource)
    {
        DebitOrder returnvalue = new DebitOrder();

        string sql = "debitorder_select_confirmcode '" + confirm_code + "'";
        Utilities util = null;
        SqlConnection oConn = null;
        SqlCommand oCmd = null;
        SqlDataReader dr = null;
        try
        {
            util = new Utilities();
            oConn = new SqlConnection(appsource);
            oCmd = oConn.CreateCommand();
            oCmd.CommandType = CommandType.Text;
            oCmd.CommandText = sql;
            oConn.Open();
            dr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    returnvalue.id = Convert.ToInt64(dr["id"].ToString());
                    returnvalue.display_id = dr["display_id"].ToString();
                    returnvalue.member_id = Convert.ToInt64(dr["member_id"].ToString());
                    returnvalue.order_description = dr["order_description"].ToString();
                    returnvalue.order_status = dr["order_status"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (oConn != null)
            {
                oConn.Close();
                oConn.Dispose();
            }
            if (util != null) util = null;
        }

        return returnvalue;
    }

    public DebitOrder[] GetDebitOrdersByMemberId(long id, string appsource)
    {
        DebitOrder[] returnvalue = new DebitOrder[0];
        string sql = "debitorder_select_member_id " + id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new DebitOrder[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    DebitOrder d = new DebitOrder();
                    d.display_id = dataset.Tables[0].Rows[i]["display_id"].ToString();
                    d.order_status = dataset.Tables[0].Rows[i]["order_status"].ToString();
                    d.create_date = Convert.ToDateTime(dataset.Tables[0].Rows[i]["createdate"].ToString());
                    returnvalue[i] = d;
                    d = null;
                }
            }
        }
        
        
        return returnvalue;
    }


    public DebitOrderDetail[] GetDebitOrderDetailById(long id, string appsource)
    {
        DebitOrderDetail[] returnvalue = new DebitOrderDetail[0];
        string sql = "debitorder_detail_select_id " + id;
        DataService ds = new DataService();
        DataSet dataset = ds.GetDataSet(sql, appsource);
        if (dataset.Tables.Count > 0)
        {
            if (dataset.Tables.Count > 0)
            {
                returnvalue = new DebitOrderDetail[dataset.Tables[0].Rows.Count];
                for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
                {
                    DebitOrderDetail dod = new DebitOrderDetail();
                    dod.id = Convert.ToInt64(dataset.Tables[0].Rows[i]["id"].ToString());
                    dod.order_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["order_id"].ToString());
                    dod.product_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["product_id"].ToString());
                    dod.item_id = Convert.ToInt64(dataset.Tables[0].Rows[i]["item_id"].ToString());
                    dod.retail_price = Convert.ToDecimal(dataset.Tables[0].Rows[i]["retail_price"].ToString());
                    dod.total_discounts = Convert.ToDecimal(dataset.Tables[0].Rows[i]["total_discounts"].ToString());
                    dod.total_taxes = Convert.ToDecimal(dataset.Tables[0].Rows[i]["total_taxes"].ToString());
                    dod.product_name = dataset.Tables[0].Rows[i]["product_name"].ToString();
                    returnvalue[i] = dod;
                    dod = null;
                }
            }
        }
        return returnvalue;
    }



}

