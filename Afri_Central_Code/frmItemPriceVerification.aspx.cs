using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmItemPriceVerification : System.Web.UI.Page
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
            DivView.Attributes.Add("style", "display:none;");


            hdnComID.Value = dt_login_details.Rows[0]["Com_Id"].ToString();

        }

        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
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


    protected void grdIteamDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIteamDetails.PageIndex = e.NewPageIndex;
        this.bindgrid();
    }

    protected void grdIteamDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = grdIteamDetails.Rows[rowIndex];
        }
        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
        }
    }

    protected void grdIteamDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdIteamDetails, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        catch (Exception ex)
        {
            lblloginmsg.Attributes.Add("class", "active");
            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
        }
    }

    protected void grdIteamDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Label lblBarCodeNo = (Label)grdIteamDetails.SelectedRow.FindControl("lblBarCodeNo");
            Label lblInvoiceNo = (Label)grdIteamDetails.SelectedRow.FindControl("lblInvoiceNo");
           
            string qry = "   Select  InvoiceNo, ItemRegNo, ItemName, ItemSpecification, Brand, BarCodeNo, CostPriceOld, SalesPriceOld, CostPrice, SalesPrice FROM vw_PriceCorrection where BarCodeNo ='" + lblBarCodeNo.Text.Trim() + "' and InvoiceNo = '" + lblInvoiceNo.Text.Trim() + "' and isCreated=1 and isApproved=0  order by year(CreateON) desc,  month(CreateON) desc,   day(CreateON) desc ";


            DataTable dt = new DataTable();
            dt = CommonFunctions.fetchdata(qry);

            if (dt.Rows.Count > 0)
            {
                txtItemRegNo.Text = dt.Rows[0]["ItemRegNo"].ToString();
                txtBarCodeNo.Text = dt.Rows[0]["BarCodeNo"].ToString();

                txtBillNo.Text =  dt.Rows[0]["InvoiceNo"].ToString();
                txtItemName.Text = dt.Rows[0]["ItemName"].ToString();
                txtItemSpecification.Text = dt.Rows[0]["ItemSpecification"].ToString();
                txtOldCostPrice.Text = dt.Rows[0]["CostPriceOld"].ToString();
                txtOldSalesPrice.Text = dt.Rows[0]["SalesPriceOld"].ToString();
                txtNewCostPrice.Text = dt.Rows[0]["CostPrice"].ToString();
                txtNewSalesPrice.Text = dt.Rows[0]["SalesPrice"].ToString();



                DivMain.Attributes.Add("style", "display:none;");
                DivGrid.Attributes.Add("style", "display:none;");
                DivView.Attributes.Add("style", "display:block;");
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


            if (txtItemName.Text.ToString() != string.Empty && txtNewCostPrice.Text.ToString() != string.Empty && txtNewSalesPrice.Text.ToString() != string.Empty)
            {
                if (Convert.ToDecimal(txtNewCostPrice.Text) >= Convert.ToDecimal(txtNewSalesPrice.Text))
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

                cmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(txtNewCostPrice.Text).ToString());
                cmd.Parameters.AddWithValue("@SalesPrice", Convert.ToDecimal(txtNewSalesPrice.Text).ToString());

                cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Flag", "IPriceUpdate");
                SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();
                R = output.Value.ToString();
                con.Close();

                if (R != String.Empty)
                {
                    Response.Redirect("frmItemPriceVerification.aspx");
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


    protected void btnRejectOpt(object sender, EventArgs e)
    {
        //----------Save Country Master Details-----------------
        try
        {


            if (txtItemName.Text.ToString() != string.Empty && txtNewCostPrice.Text.ToString() != string.Empty && txtNewSalesPrice.Text.ToString() != string.Empty)
            {
                if (Convert.ToDecimal(txtNewCostPrice.Text) >= Convert.ToDecimal(txtNewSalesPrice.Text))
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

                cmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(txtNewCostPrice.Text).ToString());
                cmd.Parameters.AddWithValue("@SalesPrice", Convert.ToDecimal(txtNewSalesPrice.Text).ToString());

                cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Flag", "IPriceReject");
                SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();
                R = output.Value.ToString();
                con.Close();

                if (R != String.Empty)
                {
                    Response.Redirect("frmItemPriceVerification.aspx");
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

}
