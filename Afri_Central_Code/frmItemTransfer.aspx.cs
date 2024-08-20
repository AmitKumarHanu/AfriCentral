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
    public partial class frmItemTransfer : System.Web.UI.Page
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

                hdnComID.Value = dt_login_details.Rows[0]["Com_Id"].ToString();
                txtSearch.Focus();
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


                string qry = " Select distinct(X.Ticket), X.Br_Name, X.Transfer, X.Verify, Convert(varchar(10), X.CreatedOn,103) AS CreateON From  " +
                " (Select X1.TicketNo as Ticket, X3.Br_Name, CASE WHEN X1.isTransfer = 1 THEN 'Transfer' WHEN X1.isTransfer = 0  THEN 'Pending' END as Transfer,  " +
                " CASE WHEN X1.isVerify = 1 THEN 'Verified' WHEN X1.isVerify = 0  THEN 'Pending' END as Verify, X1.CreateON as CreatedOn from tbl_ItemTransferBranch X1, tbl_UserMaster X2 , tbl_BranchMaster X3 " +
                " where X1.CreateBy = X2.UserID and X1.CompID = X2.Com_Id  and X1.StoreID=X3.Br_ID ) X   group by X.Ticket, X.Br_Name, X.Transfer, X.Verify, X.CreatedOn order by CreateON desc ";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdOrder"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdIteamTransfer.DataSource = dt;
                    grdIteamTransfer.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdIteamTransfer.DataSource = null;
                    grdIteamTransfer.DataBind();
                }

                pnlOrderTransfer.Attributes.Add("style", "display:block;");
                pnlOrderDetails.Attributes.Add("style", "display:none;");
                lblloginmsg.Attributes.Add("style", "display:none;");

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        //-------------- Get Branch Name Autoresponse fun -----------------------
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<BrResult> GetBranch(string Br, string ComID)
        {
            List<BrResult> dataList = new List<BrResult>();
            string sqlStatment = " Select  Br_ID, Br_Name , Br_PhoneNo, Br_VATRegNo  FROM tbl_BranchMaster  where Com_ID='" + ComID.ToString() + "'  and Br_Name  LIKE '" + Br + "%'  order by Br_Name asc";

            using (SqlConnection con = new SqlConnection(CommonFunctions.connection))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatment, con))
                {
                    cmd.Connection.Open();
                    System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BrResult detail = new BrResult();
                        detail.Br_ID = reader[0].ToString();
                        detail.Br_Name = reader[1].ToString();
                        detail.Br_PhoneNo = reader[2].ToString();
                        detail.Br_VATRegNo = reader[3].ToString();
                        dataList.Add(detail);
                    }
                    reader.Close();
                    cmd.Connection.Close();
                }
            }
            return dataList;

        }

        public class BrResult
        {
            public string Br_ID { get; set; }
            public string Br_Name { get; set; }
            public string Br_PhoneNo { get; set; }
            public string Br_VATRegNo { get; set; }

        }

        //-------------- Get BarCode in Autoresponse fun -----------------------
        [System.Web.Services.WebMethod]
        public static List<itemResult> GetBarcode(string BarCode, string ComID)
        {
            List<itemResult> dataList = new List<itemResult>();
            string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity,CostPrice,SalesPrice, ItemRegNo From vw_ItemDetails where ComID='" + ComID.ToString() + "'  and  BarCodeNo  LIKE '" + BarCode + "%'   and Quantity > 0  order by ItemName asc";

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
            string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity,CostPrice,SalesPrice, ItemRegNo From vw_ItemDetails where ComID='" + ComID.ToString() + "'  and  ItemName  LIKE '" + ItemName + "%'  and Quantity > 0  order by ItemName asc";

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



        //--------------New Add Bill display div ----------------------      
        protected void BtnNewOpt(object sender, EventArgs e)
        {
            try
            {

                Session["AddToCard"] = null;
                BindBillNo();      
                DivMain.Attributes.Add("style", "display:none;");
                DivAdd.Attributes.Add("style", "display:block;");

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }

        }


        //--------------Display Serach Div-----------------------
        protected void BtnSearchOpt(object sender, EventArgs e)
        {
            try
            {

                string qry = "  Select * From  vw_UserDetails where UserName like '" + txtSearch.Value.Trim() + "%' or First_Name like '" + txtSearch.Value.Trim() + "%' or Last_Name like '" + txtSearch.Value.Trim() + "%' order by First_Name, Last_Name ";


                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCompanyDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    // grdUserDetails.DataSource = dt;
                    //grdUserDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    // grdUserDetails.DataSource = null;
                    // grdUserDetails.DataBind();
                }

                DivMain.Attributes.Add("style", "display:block;");
                DivAdd.Attributes.Add("style", "display:none;");


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
                cmd1.Parameters.AddWithValue("@Flag", "New");
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


        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (txtBarCodeNo.Text != string.Empty)
            {
                //-------Please check Qty from database-------------
           

                string qry = " Select Quantity FROM tbl_ItemMaster where  BarCodeNo like  '" + txtBarCodeNo.Text.Trim() + "%' and itemRegNo='" + txtItemRegNo.Text.Trim() + "'";
                DataTable dtR = new DataTable();
                dtR = CommonFunctions.fetchdata(qry);

                if (Convert.ToInt32(dtR.Rows[0]["Quantity"].ToString()) > 0)
                {

                    lblMessage.Attributes.Add("style", "display:none;");

                    // Check if the Session has a data assoiciated within it.If
                    if (Session["AddToCard"] != null)
                    {
                        DataTable dt = (DataTable)Session["AddToCard"];
                        int count = dt.Rows.Count;
                        BindGrid(count);
                    }
                    else
                    {
                        BindGrid(1);

                    }

                    if (Session["AddToCard"] != null)
                    {
                        DataTable dt = (DataTable)Session["AddToCard"];
                        int count = dt.Rows.Count;


                        if (count >= 1)
                        {
                            txtBarCodeNo.Text = " ";
                            txtItemName.Text = " ";
                            txtItemSpecification.Text = " ";
                            txtSalesPrice.Text = " ";
                            txtItemQuantity.Text = " ";
                            txtItemRegNo.Text = " ";

                            decimal Subtotal = 0, disAmt = 0, VatAmt = 0;
                            int TotalItem = 0;
                            for (int i = 0; i < count; i++)
                            {
                                Subtotal = Subtotal + Convert.ToDecimal(dt.Rows[i]["TotalAmount"].ToString());
                                TotalItem = TotalItem + Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                            }

                            txtitemCount.Text = TotalItem.ToString();
                            txtSubTotal.Text = Subtotal.ToString();

                            decimal SAmt = Convert.ToDecimal(txtSubTotal.Text);

                            decimal TPayment = SAmt;
                            lblTotalPayment.Text = TPayment.ToString();

                        }
                    }

                }
                else
                {
                    lblMessage.Attributes.Add("style", "display:block;");
                    lblMessage.Attributes["style"] = "color:red; font-weight:bold; background-color:white; ";
                    lblMessage.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Item Out Of Stock !! </h4>";

                    //string input = "Item Out Of Stock";
                    //ClientScript.RegisterStartupScript(this.GetType(), "messagebox", "alert('Message:" + input + "');", true); ;
                }
            }
        }

        

        private void BindGrid(int rowcount)
        {
            DataTable dt = new DataTable();
            DataRow dr;
           
            dt.Columns.Add(new System.Data.DataColumn("Name", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Specification", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("Quantity", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("SalesPrice", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("TotalAmount", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("ItemRegNo", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("BarcodeNo", typeof(String)));
            if (Session["AddToCard"] != null)
            {
                int flag = 0;
                for (int i = 0; i < rowcount; i++)
                {

                    dt = (DataTable)Session["AddToCard"];
                    if (dt.Rows.Count > 0)
                    {
                        //---itemRegNo already exist Update Qty-------
                        if (dt.Rows[i][5].ToString().Trim() == txtItemRegNo.Text.Trim())
                        {
                            int Qty = Convert.ToInt32(dt.Rows[i][2].ToString().Trim()) + Convert.ToInt32(txtItemQuantity.Text);
                            dt.Rows[i][2] = Qty.ToString();

                            decimal q = (Qty) * Convert.ToDecimal(dt.Rows[i][3].ToString().Trim());
                            dt.Rows[i][4] = q.ToString();
                            flag = 2;
                        }

                        //-----Add next item----
                        if (dt.Rows[i][5].ToString().Trim() != txtItemRegNo.Text.Trim() && flag == 0)
                        {
                            flag = 1;
                        }
                    }
                }

                if (flag == 1)
                {

                    dr = dt.NewRow();
                    dr[0] = txtItemName.Text;
                    dr[1] = txtItemSpecification.Text;
                    dr[2] = txtItemQuantity.Text;
                    dr[3] = txtSalesPrice.Text;
                    decimal q = Convert.ToInt32(txtItemQuantity.Text) * Convert.ToDecimal(txtSalesPrice.Text);
                    dr[4] = q.ToString();
                    dr[5] = txtItemRegNo.Text;
                    dr[6] = txtBarCodeNo.Text;
                    dt.Rows.Add(dr);
                }

            }
            else
            {
                //----------New First time Item Add------
                dr = dt.NewRow();
                dr[0] = txtItemName.Text;
                dr[1] = txtItemSpecification.Text;
                dr[2] = txtItemQuantity.Text;
                dr[3] = txtSalesPrice.Text;
                decimal q = Convert.ToInt32(txtItemQuantity.Text) * Convert.ToDecimal(txtSalesPrice.Text);
                dr[4] = q.ToString();
                dr[5] = txtItemRegNo.Text;
                dr[6] = txtBarCodeNo.Text;
                dt.Rows.Add(dr);

            }

            // If Session has a data then use the value as the DataSource
            if (Session["AddToCard"] != null)
            {
                grdAddToCard.DataSource = (DataTable)Session["AddToCard"];
                grdAddToCard.DataBind();
            }
            else
            {
                // Bind GridView with the initial data assocaited in the DataTable
                grdAddToCard.DataSource = dt;
                grdAddToCard.DataBind();
            }
            // Store the DataTable in Session to retain the values
            Session["AddToCard"] = dt;

            if (dt.Rows.Count > 0)
            {
                ////---------Update Select Quantity in tbl_itemmaster---------------------
                //SqlCommand cmdI = new SqlCommand();
                //cmdI.Connection = con;
                //cmdI.CommandText = "SP_AF_ItemQty";
                //cmdI.CommandType = CommandType.StoredProcedure;
                //cmdI.Parameters.AddWithValue("@Qty", Convert.ToInt32(txtItemQuantity.Text));
                //cmdI.Parameters.AddWithValue("@BarCodeNo", txtBarCodeNo.Text.Trim().ToString());
                //cmdI.Parameters.AddWithValue("@itemRegNo", txtItemRegNo.Text.Trim().ToString());

                //cmdI.Parameters.AddWithValue("@Flag", "IitemQty");
                //SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                //output.Direction = ParameterDirection.Output;
                //cmdI.Parameters.Add(output);
                //con.Open();
                //cmdI.ExecuteNonQuery();
                //string RI = output.Value.ToString();
                //con.Close();
            }


        }


        //-------------- item Transfer location -----------------------
        protected void btnTransferItem(object sender, EventArgs e)
        {

            if (Session["AddToCard"] != null)
            {
                DataTable dt = (DataTable)Session["AddToCard"];
                int count = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    string RI = "";
                   
                    //---------Trafer Ticker Details to Branch / Store---------------------

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //---------Trafer Ticker Details to Branch / Store---------------------
                        SqlCommand cmdI = new SqlCommand();
                        cmdI.Connection = con;
                        cmdI.CommandText = "SP_AF_ItemTransferBranch";
                        cmdI.CommandType = CommandType.StoredProcedure;

                        cmdI.Parameters.AddWithValue("@TicketNo", txtBillNo.Text.Trim().ToString());

                        cmdI.Parameters.AddWithValue("@TicketDate", DateTime.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                        cmdI.Parameters.AddWithValue("@BranchName", txtBrName.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Mobile", txtBrMobileNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@VatRegNo", txtBrVatRegNo.Text.Trim().ToString());

                        cmdI.Parameters.AddWithValue("@itemName", dt.Rows[i][0].ToString().Trim());
                        cmdI.Parameters.AddWithValue("@Quantity", Convert.ToInt32(dt.Rows[i][2].ToString().Trim()));
                        cmdI.Parameters.AddWithValue("@SalesPrice", Convert.ToDecimal(dt.Rows[i][3].ToString().Trim()));
                        cmdI.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(dt.Rows[i][4].ToString().Trim()));
                        cmdI.Parameters.AddWithValue("@ItemRegNo", dt.Rows[i][5].ToString().Trim());
                        cmdI.Parameters.AddWithValue("@BarCodeNo", dt.Rows[i][6].ToString().Trim());

                        cmdI.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                        cmdI.Parameters.AddWithValue("@Flag", "ITicket");
                        SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmdI.Parameters.Add(output);
                        con.Open();
                        cmdI.ExecuteNonQuery();
                        RI = output.Value.ToString();
                        con.Close();
                       
                    }
                    if (dt.Rows.Count > 0 && RI == "1")
                    {
                        //---------Update Remove Quantity in tbl_itemmaster---------------------
                        SqlCommand cmdI = new SqlCommand();
                        cmdI.Connection = con;
                        cmdI.CommandText = "SP_AF_Transfer";
                        cmdI.CommandType = CommandType.StoredProcedure;
                        cmdI.Parameters.AddWithValue("@TicketNo", txtBillNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@BranchName", txtBrName.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());

                        cmdI.Parameters.AddWithValue("@Flag", "Transfer");
                        SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmdI.Parameters.Add(output);
                        con.Open();
                        cmdI.ExecuteNonQuery();
                        string TF = output.Value.ToString();
                        con.Close();

                        if (TF == "1")
                        {

                            Response.Redirect("frmItemTransfer.aspx");
                        }
                        else
                        {
                            DivMain.Attributes.Add("style", "display:block;");
                            DivAdd.Attributes.Add("style", "display:none;");

                            lblloginmsg.Attributes.Add("style", "display:block;");
                            lblloginmsg.Attributes["style"] = "color:green; font-weight:bold; background-color:white; ";
                            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Save iteam details successfull but not transfer !! </h4>";
                        }
                        //Response.Redirect("frmItemTransfer.aspx");
                    }
                }
            }
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //---------Delete Row from date set and bind with datagrid
                String id = ((sender as LinkButton).CommandArgument);
                DataTable dt = (DataTable)Session["AddToCard"];
                int HQty = 0; string BarCodeNo = "";
                if (Session["AddToCard"] != null)
                {

                    int count = dt.Rows.Count;

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if (dr["ItemRegNo"].ToString() == id.ToString())
                        {
                            HQty = Convert.ToInt32(dr["Quantity"].ToString());
                            BarCodeNo = dr["BarcodeNo"].ToString();
                            dr.Delete();
                        }
                    }


                    int count1 = dt.Rows.Count;
                    if (Session["AddToCard"] != null)
                    {
                        grdAddToCard.DataSource = (DataTable)Session["AddToCard"];
                        grdAddToCard.DataBind();


                    }
                    else
                    {
                        // Bind GridView with the initial data assocaited in the DataTable
                        grdAddToCard.DataSource = dt;
                        grdAddToCard.DataBind();
                   

                    }
                    Session["AddToCard"] = dt;

                }
                if (Session["AddToCard"] != null)
                {
                    //DataTable dt = (DataTable)Session["AddToCard"];
                    int count = dt.Rows.Count;


                    if (count >= 1)
                    {
                        txtBarCodeNo.Text = " ";
                        txtItemName.Text = " ";
                        txtItemSpecification.Text = " ";
                        txtSalesPrice.Text = " ";
                        txtItemQuantity.Text = " ";
                        txtItemRegNo.Text = " ";

                        decimal Subtotal = 0;
                        int TotalItem = 0;
                        for (int i = 0; i < count; i++)
                        {
                            Subtotal = Subtotal + Convert.ToDecimal(dt.Rows[i]["TotalAmount"].ToString());
                            TotalItem = TotalItem + Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                        }

                        txtitemCount.Text = TotalItem.ToString();
                        txtSubTotal.Text = Subtotal.ToString();
                        
                        decimal SAmt = Convert.ToDecimal(txtSubTotal.Text);                      
                        decimal TPayment = SAmt;
                        lblTotalPayment.Text = TPayment.ToString();

                        

                    }
                    if (count < 1)
                    {
                        txtBarCodeNo.Text = " ";
                        txtItemName.Text = " ";
                        txtItemSpecification.Text = " ";
                        txtSalesPrice.Text = " ";
                        txtItemQuantity.Text = " ";
                        txtItemRegNo.Text = " ";
                        txtitemCount.Text = "";
                        txtSubTotal.Text = "0";
                   
                        lblTotalPayment.Text = "0";
                      
                      
                        Session["AddToCard"] = null;
                    }


                    ////---------Update Remove Quantity in tbl_itemmaster---------------------
                    //SqlCommand cmdI = new SqlCommand();
                    //cmdI.Connection = con;
                    //cmdI.CommandText = "SP_AF_ItemQty";
                    //cmdI.CommandType = CommandType.StoredProcedure;
                    //cmdI.Parameters.AddWithValue("@Qty", HQty);
                    //cmdI.Parameters.AddWithValue("@BarCodeNo", BarCodeNo.ToString());
                    //cmdI.Parameters.AddWithValue("@itemRegNo", id.ToString());

                    //cmdI.Parameters.AddWithValue("@Flag", "RitemQty");
                    //SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    //output.Direction = ParameterDirection.Output;
                    //cmdI.Parameters.Add(output);
                    //con.Open();
                    //cmdI.ExecuteNonQuery();
                    //string RI = output.Value.ToString();
                    //con.Close();
                }
            }

            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void grdIteamTransfer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdIteamTransfer.PageIndex = e.NewPageIndex;
            this.bindgrid();
        }

        protected void grdIteamTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdIteamTransfer.Rows[rowIndex];
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void grdIteamTransfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdIteamTransfer, "Select$" + e.Row.RowIndex);
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

        protected void grdIteamTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Label lblTicketNo = (Label)grdIteamTransfer.SelectedRow.FindControl("lblTicketNo");

                string qry = " Select X1.ItemRegNo, X1.ItemName, X1.ItemSpecification  , X1.Brand, X1.BarCodeNo, X1.Quantity, X1.Hold_Quantity, X1.CostPrice, X1.SalesPrice, X1.Discount, X1.DiscountAmount, X1.SupplierCode, X1.CategoryCode, X1.Mfgdate, X1.Expdate, X1.Warranty, X1.Image, X1.isTransfer, X1.isVerify, X3.Br_Name    " +
                 " From  tbl_ItemTransferBranch  X1, tbl_UserMaster X2 , tbl_BranchMaster X3 where X1.CompID=X2.Com_Id  and X1.TicketNo like '" + lblTicketNo.Text.Trim() + "%' " +
                 "  and   X2.EmployeeID='" + dt_login_details.Rows[0]["EmployeeID"].ToString() + "'  and X1.StoreID=X3.Br_ID order by X1.TicketNo,  year(X1.CreateON) , month(X1.CreateON)  , day(X1.CreateON)  asc ";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdOrderDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdOrderDetails.DataSource = dt;
                    grdOrderDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdOrderDetails.DataSource = null;
                    grdOrderDetails.DataBind();
                }


                pnlOrderTransfer.Attributes.Add("style", "display:none;");
                pnlOrderDetails.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes.Add("style", "display:none;");

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }
    }
}