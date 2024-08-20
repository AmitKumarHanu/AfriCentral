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
    public partial class ItemAllotment : System.Web.UI.Page
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
                }

                lblloginmsg.InnerHtml = "";


                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];



            }

            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        //--------------Add Option display div ----------------------
        protected void BtnAddOpt(object sender, EventArgs e)
        {
            try
            {
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

        //-------------- Get Branch Name Autoresponse fun -----------------------
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<BrResult> GetBranch(string Br)
        {
            List<BrResult> dataList = new List<BrResult>();
            string sqlStatment = " Select  Br_ID, Br_Name , Br_PhoneNo, Br_VATRegNo  FROM tbl_BranchMaster  where Br_Name  LIKE '" + Br + "%'  order by Br_Name asc";
            //string sqlStatment = " Select  Br_ID, Br_Name , Br_PhoneNo, Br_VATRegNo  FROM tbl_BranchMaster  where Br_Name  LIKE '" + Br + "%' and Com_Id= '" + Com_ID.ToString() + "' order by Br_Name asc";
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

        //--------------Add Option display div -----------------------
        protected void BtnEditOpt(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Value.Trim() != String.Empty)
                {
                    string qry = " Select * From  tbl_CompanyMaster where Com_Name = '" + txtSearch.Value.Trim() + "'  order by Com_Name, Com_ID ";
                    DataTable dt = new DataTable();
                    dt = CommonFunctions.fetchdata(qry);

                    if (dt.Rows.Count > 0)
                    {

                        ////lblEEmployeeID.Text = dt.Rows[0]["EmployeeID"].ToString();
                        //txtEFirstName.Text = dt.Rows[0]["First_Name"].ToString();
                        //txtELastName.Text = dt.Rows[0]["Last_Name"].ToString();
                        //txtELoginID.Text = dt.Rows[0]["UserName"].ToString();
                        //txtEPassword.Text = dt.Rows[0]["Password"].ToString();
                        //drpEGender.SelectedIndex = drpEGender.Items.IndexOf(drpEGender.Items.FindByValue(dt.Rows[0]["Gender"].ToString()));
                        //drpEStatus.SelectedIndex = drpEStatus.Items.IndexOf(drpEStatus.Items.FindByValue(dt.Rows[0]["IFlag"].ToString()));

                        //txtEMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                        //txtEEmail.Text = dt.Rows[0]["Email"].ToString();
                        //txtEDesignation.Text = dt.Rows[0]["Designation"].ToString();
                        //txtELocation.Text = dt.Rows[0]["Location"].ToString();
                        //drpELoginMode.SelectedIndex = drpELoginMode.Items.IndexOf(drpELoginMode.Items.FindByValue(dt.Rows[0]["LoginMode"].ToString()));
                        //drpEUserType.SelectedIndex = drpEUserType.Items.IndexOf(drpEUserType.Items.FindByValue(dt.Rows[0]["UserTypeId"].ToString()));
                        //drpEZoneName.SelectedIndex = drpEZoneName.Items.IndexOf(drpEZoneName.Items.FindByValue(dt.Rows[0]["ZoneCode"].ToString()));



                        DivMain.Attributes.Add("style", "display:none;");
                        DivAdd.Attributes.Add("style", "display:none;");
                     
                    }
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
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<itemResult> GetBarcode(string BarCpde)
        {
            List<itemResult> dataList = new List<itemResult>();
            string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity,CostPrice,SalesPrice, ItemRegNo From vw_ItemDetails where BarCodeNo  LIKE '" + BarCpde + "%' order by ItemName asc";

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
                        detail.SalesPrice = Convert.ToDecimal(reader[4].ToString());
                        detail.CostPrice = Convert.ToDecimal(reader[5].ToString());
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
        public static List<itemResult> GetitemName(string ItemName)
        {

            List<itemResult> dataList = new List<itemResult>();
            string sqlStatment = " Select   ItemName,ItemSpecification,BarCodeNo,Quantity,CostPrice,SalesPrice, ItemRegNo From vw_ItemDetails where ItemName  LIKE '" + ItemName + "%' order by ItemName asc";

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
                        detail.SalesPrice = Convert.ToDecimal(reader[4].ToString());
                        detail.CostPrice = Convert.ToDecimal(reader[5].ToString());
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

        //-------------- Add Item List -----------------------
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (txtBarCodeNo.Text != string.Empty && txtItemName.Text != string.Empty)
            {
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
                       

                        decimal Subtotal = 0;
                        int TotalItem = 0;
                        for (int i = 0; i < count; i++)
                        {
                            Subtotal = Subtotal + Convert.ToDecimal(dt.Rows[i]["TotalAmount"].ToString());
                            TotalItem = TotalItem + Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                        }

                        lblTotalPayment.Text = Subtotal.ToString();
                        lblTotalitem.Text = TotalItem.ToString();
                        
                      
                    }
                    txtBarCodeNo.Text = "";
                    txtItemName.Text = "";
                    txtCostPrice.Text = "";
                    txtItemQuantity.Text = "";
                   
                }
            }
        }


        private void BindGrid(int rowcount)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new System.Data.DataColumn("Name", typeof(String)));           
            dt.Columns.Add(new System.Data.DataColumn("Quantity", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("CostPrice", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("TotalAmount", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("ItemRegNo", typeof(String)));
            dt.Columns.Add(new System.Data.DataColumn("BarCodeNo", typeof(String)));
            if (Session["AddToCard"] != null)
            {
                int flag = 0;
                for (int i = 0; i < rowcount; i++)
                {

                    dt = (DataTable)Session["AddToCard"];
                    if (dt.Rows.Count > 0)
                    {
                        //---itemRegNo already exist Update Qty-------
                        if (dt.Rows[i][4].ToString().Trim() == txtItemRegNo.Text.Trim())
                        {
                            int Qty = Convert.ToInt32(dt.Rows[i][1].ToString().Trim()) + Convert.ToInt32(txtItemQuantity.Text);
                            dt.Rows[i][1] = Qty.ToString();

                            decimal q = (Qty) * Convert.ToDecimal(dt.Rows[i][2].ToString().Trim());
                            dt.Rows[i][3] = q.ToString();
                            flag = 2;
                        }

                        //-----Add next item----
                        if (dt.Rows[i][4].ToString().Trim() != txtItemRegNo.Text.Trim() && flag == 0)
                        {
                            flag = 1;
                        }
                    }
                }

                if (flag == 1)
                {

                    dr = dt.NewRow();
                    dr[0] = txtItemName.Text;                
                    dr[1] = txtItemQuantity.Text;
                    dr[2] = txtCostPrice.Text;
                    decimal q = Convert.ToInt32(txtItemQuantity.Text) * Convert.ToDecimal(txtCostPrice.Text);
                    dr[3] = q.ToString();
                    dr[4] = txtItemRegNo.Text;
                    dr[5] = txtBarCodeNo.Text;
                    dt.Rows.Add(dr);
                }

            }
            else
            {
                //----------New First time Item Add------
                dr = dt.NewRow();
                dr[0] = txtItemName.Text;
                dr[1] = txtItemQuantity.Text;
                dr[2] = txtCostPrice.Text;
                decimal q = Convert.ToInt32(txtItemQuantity.Text) * Convert.ToDecimal(txtCostPrice.Text);
                dr[3] = q.ToString();
                dr[4] = txtItemRegNo.Text;
                dr[5] = txtBarCodeNo.Text;
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
                //---------Update Select Quantity in tbl_itemmaster---------------------
                SqlCommand cmdI = new SqlCommand();
                cmdI.Connection = con;
                cmdI.CommandText = "SP_AF_ItemQty";
                cmdI.CommandType = CommandType.StoredProcedure;
                cmdI.Parameters.AddWithValue("@Qty", Convert.ToInt32(txtItemQuantity.Text));
                cmdI.Parameters.AddWithValue("@BarCodeNo", txtBarCodeNo.Text.Trim().ToString());
                cmdI.Parameters.AddWithValue("@itemRegNo", txtItemRegNo.Text.Trim().ToString());

                cmdI.Parameters.AddWithValue("@Flag", "IitemQty");
                SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmdI.Parameters.Add(output);
                con.Open();
                cmdI.ExecuteNonQuery();
                string RI = output.Value.ToString();
                con.Close();
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
                            BarCodeNo = dr["BarCodeNo"].ToString();
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
                        txtCostPrice.Text = " ";
                        txtItemQuantity.Text = " ";
                        txtItemRegNo.Text = " ";

                        decimal Subtotal = 0;
                        int TotalItem = 0;
                        for (int i = 0; i < count; i++)
                        {
                            Subtotal = Subtotal + Convert.ToDecimal(dt.Rows[i]["TotalAmount"].ToString());
                            TotalItem = TotalItem + Convert.ToInt32(dt.Rows[i]["Quantity"].ToString());
                        }
                        lblTotalPayment.Text = Subtotal.ToString();
                        lblTotalitem.Text = TotalItem.ToString();  
                    }
                    if (count < 1)
                    {
                        txtBarCodeNo.Text = "";
                        txtItemName.Text = "";
                        txtCostPrice.Text = "";
                        txtItemQuantity.Text = "";
                        txtItemRegNo.Text = "";
                        lblTotalPayment.Text = "";
                        lblTotalitem.Text = "";               
                        Session["AddToCard"] = null;
                    }

                    //---------Update Remove Quantity in tbl_itemmaster---------------------
                    SqlCommand cmdI = new SqlCommand();
                    cmdI.Connection = con;
                    cmdI.CommandText = "SP_AF_ItemQty";
                    cmdI.CommandType = CommandType.StoredProcedure;
                    cmdI.Parameters.AddWithValue("@Qty", HQty);
                    cmdI.Parameters.AddWithValue("@BarCodeNo", BarCodeNo.ToString());
                    cmdI.Parameters.AddWithValue("@itemRegNo", id.ToString());

                    cmdI.Parameters.AddWithValue("@Flag", "RitemQty");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmdI.Parameters.Add(output);
                    con.Open();
                    cmdI.ExecuteNonQuery();
                    string RI = output.Value.ToString();
                    con.Close();

                }
            }

            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
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
                    //---------Trafer Ticker Details to Branch / Store---------------------

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //---------Trafer Ticker Details to Branch / Store---------------------
                        SqlCommand cmdI = new SqlCommand();
                        cmdI.Connection = con;
                        cmdI.CommandText = "SP_AF_ItemTransferBranch";
                        cmdI.CommandType = CommandType.StoredProcedure;

                        cmdI.Parameters.AddWithValue("@TicketNo", txtBillNo.Text.Trim().ToString());
                    
                        cmdI.Parameters.AddWithValue("@TicketDate", DateTime.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) );
                        cmdI.Parameters.AddWithValue("@BranchName", txtBrName.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@Mobile", txtBrMobileNo.Text.Trim().ToString());
                        cmdI.Parameters.AddWithValue("@VatRegNo", txtBrVatRegNo.Text.Trim().ToString());

                        cmdI.Parameters.AddWithValue("@itemName", dt.Rows[i][0].ToString().Trim());
                        cmdI.Parameters.AddWithValue("@Quantity", Convert.ToInt32(dt.Rows[i][1].ToString().Trim()));
                        cmdI.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(dt.Rows[i][2].ToString().Trim()));
                        cmdI.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(dt.Rows[i][3].ToString().Trim()));
                        cmdI.Parameters.AddWithValue("@ItemRegNo", dt.Rows[i][4].ToString().Trim());
                        cmdI.Parameters.AddWithValue("@BarCodeNo", dt.Rows[i][5].ToString().Trim());

                        cmdI.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                        cmdI.Parameters.AddWithValue("@Flag", "ITicket");
                        SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmdI.Parameters.Add(output);
                        con.Open();
                        cmdI.ExecuteNonQuery();
                        string RI = output.Value.ToString();
                        con.Close();

                        if (RI == "1")
                        {

                                DivMain.Attributes.Add("style", "display:block;");
                                DivAdd.Attributes.Add("style", "display:none;");

                                lblloginmsg.Attributes.Add("style", "display:block;");
                                lblloginmsg.Attributes["style"] = "color:green; font-weight:bold; background-color:white; ";
                                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Item Transfer Successful !" + " </h4>";
                                Response.Redirect("ItemAllotment.aspx");
                        }
                    }

                }

            }
            
        }
    }
}