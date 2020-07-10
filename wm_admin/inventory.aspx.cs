using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class wm_admin_inventory : System.Web.UI.Page
{
    long mid = -1;
    string appsource = System.Configuration.ConfigurationManager.ConnectionStrings["Shop"].ConnectionString;
    string product_image_folder = "../media/products/";
    long order_id = -1;

    protected void Page_Load(object sender, EventArgs e)
    {

        //DisplayProductsByCategory("sodas");

      
    }

    private void DisplayCategories()
    {
        pnlCategory.Visible = true;
        pnlProducts.Visible = false;
        pnlNoProducts.Visible = false;
    }
    private void DisplayProductsByCategory(string categoryname)
    {
        Product[] products = new Product[0];
        ProductManager pm = new ProductManager();
        //products = GetSampleProducts();
        products = pm.SelectProductsByCategory(categoryname, appsource);
        if (products.Length > 0)
        {
            rptResults.DataSource = products;
            rptResults.DataBind();
        }
        else
        {
            DisplayNoProducts();

        }


        pnlCategory.Visible = false;
        pnlProducts.Visible = true;
        pnlNoProducts.Visible = false;
    }
    private void DisplayNoProducts()
    {
        pnlCategory.Visible = false;
        pnlProducts.Visible = false;
        pnlNoProducts.Visible = true;
    }
    protected void lnkWater_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("water");
    }
    protected void lnkEnhancedWater_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("enhanced water");
    }
    protected void lnkSports_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("sports");
    }
    protected void lnkEnergy_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("energy");
    }
    protected void lnkJuices_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("juices");
    }
    protected void lnkSparkling_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("sparkling");
    }
    protected void lnkSodas_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("sodas");
    }
    protected void lnkTea_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("tea");
    }
    protected void lnkCoffee_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("coffee");
    }
    protected void lnkMealReplacement_Click(object sender, EventArgs e)
    {
        DisplayProductsByCategory("meal replacement");
    }


    protected void rptResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Button b = (Button)e.Item.FindControl("btnRecord");
        TextBox t = (TextBox)e.Item.FindControl("frmCount");

        Product p = (Product)e.Item.DataItem;
        ProductManager pm = new ProductManager();
        long product_count = pm.SelectProductInventoryCount(p.id, appsource);
        t.Text = product_count.ToString();
        
        if (product_count < 1)
        {
        //    //find the button


            b.Visible = false;

        }
    }



    protected void btnRecord_Click(object sender, CommandEventArgs e)
    {
        string sku = e.CommandArgument.ToString();
        Product p = new Product();
        ProductManager pm = new ProductManager();
        p = pm.SelectProductBySKU(sku, appsource);
        DebitOrderManager dom = new DebitOrderManager();
        long item_id = dom.PickProductItemFromStock(p.id, appsource);
        DisplayCategories();
    }
    protected void btnAdjust_Click(object sender, CommandEventArgs e)
    {
        long fieldcount = 0;
        long dbcount = 0;
        long iteration_count = 0;
        //get the count from the field
        foreach (RepeaterItem rptItem in rptResults.Items)
        {
            Button b = (Button)rptItem.FindControl("btnAdjust");
            if (b.CommandArgument.ToString() == e.CommandArgument.ToString())
            {
                TextBox t = (TextBox)rptItem.FindControl("frmCount");              
                fieldcount = Convert.ToInt64(t.Text);
                break;
            }
        }         

        //get the count from the database
        string sku = e.CommandArgument.ToString();
        Product p = new Product();
        ProductManager pm = new ProductManager();
        p = pm.SelectProductBySKU(sku, appsource);
        dbcount = pm.SelectProductInventoryCount(p.id, appsource);
        ProductItem pi = new ProductItem();
        if (fieldcount > dbcount)
        {
            //if field is greater than database, insert the rows with iteration
            iteration_count = fieldcount - dbcount;
            
            pi.product_id = p.id;           
            for (long x = 0; x < iteration_count; x++)
            {
                pi.Create(pi, appsource);
            }

        }
        else
        {
            //if field is less than database, select rows id and remove them. - this should be a concern. mark as disappeared
            iteration_count = dbcount - fieldcount;
            ProductItem[] items = pi.SelectItemByProductId(p.id, appsource);
            if (items.Length > 0)
            {
                for (long x = 0; x < iteration_count; x++)
                {
                    pi.UpdateLocationAsGhost(items[x].id, appsource);
                }
            }

        }
        DisplayCategories();

    }
}