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

namespace Afri_Central_Code
{
    public partial class frmItemQuantityAdjustment : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(CommonFunctions.connection);
        SqlConnection cn1 = new SqlConnection(CommonFunctions.conBranch);
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
                    bindgrid();
                }

                lblloginmsg.InnerHtml = "";
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

            try
            {
                //-------------- Store Details------------------
                string qry = " Select Upper(Br_Name) as Br_Name, Br_ID from tbl_BranchMaster where Com_ID='" + dt_login_details.Rows[0]["Com_ID"].ToString() + "' order by Br_Name";
                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                drpStore.DataSource = dt;
                drpStore.DataValueField = "Br_ID";
                drpStore.DataTextField = "Br_Name";
                drpStore.DataBind();
                drpStore.Items.Insert(0, new ListItem("--Store Name-", "0"));



                string qry1 = "   Select  BarCodeNo, ItemName, ItemSpecification, BranchName, Brand ,  Quantity  FROM tbl_ItemStockAdjustment  where isAdjQtry =1  order by year(AdjQtryOn) desc,  month(AdjQtryOn) desc,   day(AdjQtryOn) desc  ";


                DataTable dt1 = new DataTable();
                dt1 = CommonFunctions.fetchdata(qry1);

                if (dt.Rows.Count > 0)
                {
                    Session["grdIteamDetails"] = dt1;
                    grdIteamDetails.DataSource = dt1;
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


        protected void btnCorrectionOpt(object sender, EventArgs e)
        {
            //----------Save Country Master Details-----------------
            try
            {


                if (txtItemName.Text.ToString() != string.Empty && txtCostPrice.Text.ToString() != string.Empty && txtSalesPrice.Text.ToString() != string.Empty)
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
                    cmd.Connection = cn1;
                    cmd.CommandText = "SP_AF_ItemQuantityAdjustment";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //ID,ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPriceOld,SalesPriceOld,CostPrice
                    //,SalesPrice,Image,isCreated,CreateBy,CreateON,isApproved,AppID,AppDate,isComp,CompID,CompON,isStore,StoreID
                    //,StoreON,UpdateBy,UpdateON,flag,isActive
                    //FROM AfriSmartA.dbo.tbl_PriceCorrection


                    cmd.Parameters.AddWithValue("@InvoiceNo", txtBillNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@TicketNo", txtTicketNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemRegNo", txtItemRegNo.Text.Trim().ToString());                    
                    cmd.Parameters.AddWithValue("@BarCodeNo", txtBarCodeNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemSpecification", txtItemSpecification.Text.ToString());

                    cmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(txtCostPrice.Text).ToString());
                    cmd.Parameters.AddWithValue("@SalesPrice", Convert.ToDecimal(txtSalesPrice.Text).ToString());
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuanity.Text).ToString());
                    cmd.Parameters.AddWithValue("@StoreID", drpStore.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "IAdjustment");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    cn1.Open();
                    cmd.ExecuteNonQuery();
                    R = output.Value.ToString();
                    cn1.Close();

                    if (R != String.Empty)
                    {
                        Response.Redirect("frmItemQuantityAdjustment.aspx");
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


        //-------------- Bill No Generate -----------------------
        private void BindBillNo()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "SP_Get_NewBillNo";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Flag", "ItemAdjustment");
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




        //--------------Display Serach -----------------------
        protected void BtnSearchOpt(object sender, EventArgs e)
        {
            try
            {
                if (drpStore.SelectedIndex > 0)
                {
                    Session["AddToCard"] = null;
                    BindBillNo();

                    hdnComID.Value = dt_login_details.Rows[0]["Com_Id"].ToString();
                    hdnBrID.Value = drpStore.SelectedValue.ToString();
                    lblStoreName.Text = drpStore.SelectedItem.ToString();
                    DivMain.Attributes.Add("style", "display:none;");
                    DivGrid.Attributes.Add("style", "display:none;");
                    DivAdd.Attributes.Add("style", "display:block;");
                }
                else
                {
                    lblloginmsg.Attributes.Add("class", "active");
                    lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                    lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please Select store name first !" + " </h4>";
                }



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
        public static List<itemResult> GetBarcode(string BarCode, string ComID, string BrID)
        {
            List<itemResult> dataList = new List<itemResult>();
           // string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity, CAST(CostPrice AS DECIMAL(11, 2)) AS CostPrice , CAST(SalesPrice AS DECIMAL(11, 2)) AS SalesPrice, ItemRegNo From vw_ItemDetails where ComID='" + ComID.ToString() + "'  and  BarCodeNo  LIKE '" + BarCode + "%'  and Quantity >= 0 order by ItemName asc";
            string sqlStatment = " Select  ItemName,ItemSpecification,BarCodeNo,Quantity, CAST(SalesPrice AS DECIMAL(11, 2)) AS SalesPrice, CAST(CostPrice AS DECIMAL(11, 2)) AS CostPrice, ItemRegNo, TicketNo  From vw_ItemStock where Com_ID='" + ComID.ToString() + "' and Br_ID='" + BrID.ToString() + "'  and  BarCodeNo  LIKE '" + BarCode + "%' and Quantity > 0 and isVerify=1  order by ItemName asc";

            using (SqlConnection cn1 = new SqlConnection(CommonFunctions.conBranch.ToString()))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatment, cn1))
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
                        detail.SalesPrice = Convert.ToDecimal(reader[4].ToString());
                        detail.CostPrice = Convert.ToDecimal(reader[5].ToString());            

                        detail.ItemRegNo = reader[6].ToString();
                        detail.TicketNo = reader[7].ToString();
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
        public static List<itemResult> GetitemName(string ItemName, string ComID, string BrID)
        {

            List<itemResult> dataList = new List<itemResult>();
            string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity,SalesPrice, CostPrice, ItemRegNo, TicketNo From vw_ItemStock where Com_ID='" + ComID.ToString() + "' and Br_ID='" + BrID.ToString() + "' and  ItemName  LIKE '" + ItemName + "%' and Quantity > 0 and isVerify=1 order by ItemName asc";

            using (SqlConnection cn1 = new SqlConnection(CommonFunctions.conBranch.ToString()))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatment, cn1))
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

                        detail.SalesPrice = Convert.ToDecimal(reader[4].ToString());
                        detail.CostPrice = Convert.ToDecimal(reader[5].ToString());
                        detail.ItemRegNo = reader[6].ToString();
                        detail.TicketNo = reader[7].ToString();
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
            public string TicketNo { get; set; }

        }



    }
}