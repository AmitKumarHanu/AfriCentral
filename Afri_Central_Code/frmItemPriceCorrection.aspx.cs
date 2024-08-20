using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmItemPriceCorrection : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(CommonFunctions.connection);
    DataTable dt_login_details = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        //----------Load Fun-----------------
        try
        {
            if (Session["LoginId"] == null)
            {
                Server.Transfer("Login.aspx", false);
            }

            dt_login_details = (DataTable)Session["LoginDetails"];

            if (!IsPostBack)
            {
                Session["AddToCard"] = null;
                bindgrid();
            }

            lblloginmsg.InnerHtml = "";

            DivMain.Attributes.Add("style", "display:block;");
            DivGrid.Attributes.Add("style", "display:block;");
            DivAdd.Attributes.Add("style", "display:none;");


            hdnComID.Value = dt_login_details.Rows[0]["Com_Id"].ToString();

        }

        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
        }
    }


    //-------------- Get BarCode in Autoresponse fun -----------------------
    [System.Web.Services.WebMethod]
    public static List<itemResult> GetBarcode(string BarCode, string ComID)
    {
        List<itemResult> dataList = new List<itemResult>();
        string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity, CAST(CostPrice AS DECIMAL(11, 2)) AS CostPrice , CAST(SalesPrice AS DECIMAL(11, 2)) AS SalesPrice, ItemRegNo From vw_ItemDetails where ComID='" + ComID.ToString() + "'  and  BarCodeNo  LIKE '" + BarCode + "%'  and Quantity >= 0 order by ItemName asc";

        using (SqlConnection con = new SqlConnection(CommonFunctions.connection))
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatment, con))
            {
                cmd.Connection.Open();
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    itemResult detail = new itemResult();
                    detail.Name = reader[0].ToString();
                    detail.Specification = reader[1].ToString();
                    detail.BarCodeNo = reader[2].ToString();
                    int Q = Convert.ToInt32(reader[3].ToString());
                    //detail.Quantity = Convert.ToInt32( reader[3].ToString());
                    if (Q >= 1)
                        detail.Quantity = Q;
                    else
                        detail.Quantity = 0;
                    detail.CostPrice = Convert.ToDecimal(reader[4].ToString());
                    detail.SalesPrice = Convert.ToDecimal(reader[5].ToString());

                    detail.ItemRegNo = reader[6].ToString();
                    dataList.Add(detail);
                }
                reader.Close();
                cmd.Connection.Close();
            }
        }
        return dataList;

    }


    //-------------- Get ItemName in Autoresponse fun -----------------------
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<itemResult> GetitemName(string ItemName, string ComID)
    {

        List<itemResult> dataList = new List<itemResult>();
        string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity,  CAST(CostPrice AS DECIMAL(11, 2)) AS CostPrice , CAST(SalesPrice AS DECIMAL(11, 2)) AS SalesPrice, ItemRegNo From vw_ItemDetails where ComID='" + ComID.ToString() + "'  and  ItemName  LIKE '" + ItemName + "%'  and Quantity >= 0 order by ItemName asc";

        using (SqlConnection con = new SqlConnection(CommonFunctions.connection))
        {
            using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatment, con))
            {
                cmd.Connection.Open();
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    itemResult detail = new itemResult();
                    detail.Name = reader[0].ToString();
                    detail.Specification = reader[1].ToString();
                    detail.BarCodeNo = reader[2].ToString();
                    int Q = Convert.ToInt32(reader[3].ToString());
                    //detail.Quantity = Convert.ToInt32( reader[3].ToString());
                    if (Q >= 1)
                        detail.Quantity = Q;
                    else
                        detail.Quantity = 0;

                    detail.CostPrice = Convert.ToDecimal(reader[4].ToString());
                    detail.SalesPrice = Convert.ToDecimal(reader[5].ToString());
                    detail.ItemRegNo = reader[6].ToString();
                    dataList.Add(detail);



                }
                reader.Close();
                cmd.Connection.Close();
            }
        }
        return dataList;
    }


    public class itemResult
    {
        public string Name { get; set; }
        public string Specification { get; set; }
        public string BarCodeNo { get; set; }
        public int Quantity { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal CostPrice { get; set; }
        public string ItemRegNo { get; set; }


    }



    //--------------Display Serach -----------------------
    protected void BtnSearchOpt(object sender, EventArgs e)
    {
        try
        {
            Session["AddToCard"] = null;
            BindBillNo();

            DivMain.Attributes.Add("style", "display:none;");
            DivGrid.Attributes.Add("style", "display:none;");
            DivAdd.Attributes.Add("style", "display:block;");




        }
        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
        }


    }

    //-------------- Bill No Generate -----------------------
    private void BindBillNo()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "SP_Get_NewBillNo";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Flag", "PriceCorrection");
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                Adp.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBillNo.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();

                }

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please correction internet connectivity, Page binding dependent showing error !" + " </h4>";
            }
        }


    public void bindgrid()
    {
        //----------Bind Data Grid-----------------
        try
        {


            //SELECT Name, Specification, BarCodeNo, Quantity, CostPrice, SalesPrice, Discount, isTax, Tax
            //, SupplierName, SupplierCompany, CategoryName, Image, UserName, CreateON
            //FROM AfriSmart.dbo.vw_ItemDetails

            string qry = "   Select  InvoiceNo,ItemName,ItemSpecification ,Brand,BarCodeNo,Status FROM vw_PriceCorrection where isCreated=1  order by year(CreateON) desc,  month(CreateON) desc,   day(CreateON) desc ";
              

            DataTable dt = new DataTable();
            dt = CommonFunctions.fetchdata(qry);

            if (dt.Rows.Count > 0)
            {
                Session["grdIteamDetails"] = dt;               
                grdIteamDetails.DataSource = dt;
                grdIteamDetails.DataBind();
            }
            else
            {
              
                grdIteamDetails.DataSource = null;
                grdIteamDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
        }
    }



    protected void btnCorrectionOpt(object sender, EventArgs e)
    {
        //----------Save Country Master Details-----------------
        try
        {
           

            if (txtItemName.Text.ToString() != string.Empty && txtCostPrice .Text.ToString() != string.Empty  && txtSalesPrice.Text.ToString() != string.Empty )
            {
                if (Convert.ToDecimal(txtCostPrice.Text) >= Convert.ToDecimal(txtSalesPrice.Text))
                {

                    lblloginmsg.Attributes.Add("class", "active");
                    lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                    lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Item sales price always greater then item cost price !" + " </h4>";

                    return;
                }

                String R = "0", RI = "0";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SP_AF_PriceCorrection";
                cmd.CommandType = CommandType.StoredProcedure;

                //ID,ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPriceOld,SalesPriceOld,CostPrice
                //,SalesPrice,Image,isCreated,CreateBy,CreateON,isApproved,AppID,AppDate,isComp,CompID,CompON,isStore,StoreID
                //,StoreON,UpdateBy,UpdateON,flag,isActive
                //FROM AfriSmartA.dbo.tbl_PriceCorrection

               
                cmd.Parameters.AddWithValue("@InvoiceNo", txtBillNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ItemRegNo", txtItemRegNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@BarCodeNo", txtBarCodeNo.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@ItemSpecification", txtItemSpecification.Text.ToString());              
           
                cmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(txtCostPrice.Text).ToString());
                cmd.Parameters.AddWithValue("@SalesPrice", Convert.ToDecimal(txtSalesPrice.Text).ToString());

                cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Flag", "IPriceCorrection");
                SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();
                R = output.Value.ToString();
                con.Close();

                if (R != String.Empty)
                {
                    Response.Redirect("frmItemPriceCorrection.aspx");
                }
              
            }
            else
            {
                //pnlAddDiv.Visible = false;
                //pnlEdit.Visible = false;
                //pnlViewDiv.Visible = false;

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please fill details on all the require fields !" + " </h4>";
            }

        }
        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please check database connection details !" + " </h4>";
        }
    }

    protected void grdIteamDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIteamDetails.PageIndex = e.NewPageIndex;
        this.bindgrid();
    }
}
