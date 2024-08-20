using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Afri_Central_Code
{
    public partial class frmitemReturnVerification : System.Web.UI.Page
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
               


                hdnComID.Value = dt_login_details.Rows[0]["Com_Id"].ToString();

            }

            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }


        //----------Bind Data Grid-----------------
        public void bindgrid()
        {

            try
            {


                //SELECT Name, Specification, BarCodeNo, Quantity, CostPrice, SalesPrice, Discount, isTax, Tax
                //, SupplierName, SupplierCompany, CategoryName, Image, UserName, CreateON
                //FROM AfriSmart.dbo.vw_ItemDetails

                string qry = "   Select RTicketNo,TicketNo, ItemName,ItemSpecification ,Brand,BarCodeNo, Quantity, Br_Name, Status, ItemRegNo FROM vw_ItemRequestReturn where isReturn=1 and isReturnVerify<>1  order by year(isReturnOn) desc,  month(isReturnOn) desc,   day(isReturnOn) desc ";


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


        //----------Central verify stock return  Details-----------------
        protected void btnVerifyOpt(object sender, EventArgs e)
        {
            
            try
            {

                int Count = 0;
                foreach (GridViewRow r in grdIteamDetails.Rows)
                {
                    CheckBox ctl = (CheckBox)r.FindControl("ChkVerify");
                    if (ctl.Checked)
                    {
                        Label lblRTicketNo = (Label)r.FindControl("lblRTicketNo");
                        Label lblTicketNo = (Label)r.FindControl("lblTicketNo");
                        Label lblItemRegNo = (Label)r.FindControl("lblItemRegNo");
                        Label lblQty = (Label)r.FindControl("lblQuantity");
                        Label lblBarcodeNo = (Label)r.FindControl("lblBarcodeNo");
                        Label lblBranchName = (Label)r.FindControl("lblBranchName");

                        //----Func Update flag isVerify--------
                        //---------Trafer Ticker Details to Branch / Store---------------------
                        SqlCommand cmdI = new SqlCommand();
                        cmdI.Connection = con;
                        cmdI.CommandText = "SP_AF_ItemStockReturnVerify";
                        cmdI.CommandType = CommandType.StoredProcedure;

                        cmdI.Parameters.AddWithValue("@RTicketNo", lblRTicketNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@TicketNo", lblTicketNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@ItemRegNo", lblItemRegNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Qty", lblQty.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@BarcodeNo", lblBarcodeNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@BranchName", lblBranchName.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                        cmdI.Parameters.AddWithValue("@Flag", "StockItemVerify");
                        SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmdI.Parameters.Add(output);
                        con.Open();
                        cmdI.ExecuteNonQuery();
                        string RI = output.Value.ToString();
                        con.Close();
                        if (RI == "1")
                            Count++;

                    }
                }
                if (Count > 0)
                {
                    pnlMain.Attributes.Add("style", "display:none;");
                   
                    lblloginmsg.Attributes.Add("style", "display:block;");
                    lblloginmsg.Attributes["style"] = "color:green; font-weight:bold; background-color:white; ";
                    lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Item Return Verified Successful !" + " </h4>";
                     Response.Redirect("frmitemReturnVerification.aspx");
                }

            }
              

            
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please check database connection details !" + " </h4>";
            }
        }


        //----------Central reject stock return  Details-----------------
        protected void btnRejectOpt(object sender, EventArgs e)
        {
            try
            {

                int Count = 0;
                foreach (GridViewRow r in grdIteamDetails.Rows)
                {
                    CheckBox ctl = (CheckBox)r.FindControl("ChkVerify");
                    if (ctl.Checked)
                    {
                        Label lblRTicketNo = (Label)r.FindControl("lblRTicketNo");
                        Label lblTicketNo = (Label)r.FindControl("lblTicketNo");
                        Label lblItemRegNo = (Label)r.FindControl("lblItemRegNo");
                        Label lblQty = (Label)r.FindControl("lblQuantity");
                        Label lblBarcodeNo = (Label)r.FindControl("lblBarcodeNo");
                        Label lblBranchName = (Label)r.FindControl("lblBranchName");

                    
                        SqlCommand cmdI = new SqlCommand();
                        cmdI.Connection = con;
                        cmdI.CommandText = "SP_AF_ItemStockReturnVerify";
                        cmdI.CommandType = CommandType.StoredProcedure;

                        cmdI.Parameters.AddWithValue("@RTicketNo", lblRTicketNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@TicketNo", lblTicketNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@ItemRegNo", lblItemRegNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Qty", lblQty.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@BarcodeNo", lblBarcodeNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@BranchName", lblBranchName.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                        cmdI.Parameters.AddWithValue("@Flag", "StockItemReject");
                        SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmdI.Parameters.Add(output);
                        con.Open();
                        cmdI.ExecuteNonQuery();
                        string RI = output.Value.ToString();
                        con.Close();
                        if (RI == "1")
                            Count++;

                    }
                }
                if (Count > 0)
                {
                    pnlMain.Attributes.Add("style", "display:none;");
                 
                    lblloginmsg.Attributes.Add("style", "display:block;");
                    lblloginmsg.Attributes["style"] = "color:green; font-weight:bold; background-color:white; ";
                    lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Item Return Verified Successful !" + " </h4>";
                    Response.Redirect("frmitemReturnVerification.aspx");
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

        }
    }
}