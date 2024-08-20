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
    
    public partial class frmSupplierMaster : System.Web.UI.Page
    {
        SqlConnection cn1 = new SqlConnection(CommonFunctions.connection.ToString());
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

        #region BindData
        public void bindgrid()
        {
            //----------Bind Data Grid-----------------
            try
            {
                DataTable dt = new DataTable();
                string qry = "";
                qry = "Usp_BindData";         //Stored Procedure name   
                SqlCommand com = new SqlCommand(qry, cn1);  //creating  SqlCommand  object  
               
                com.Parameters.AddWithValue("@Query", "Bind");
                using (var da = new SqlDataAdapter(com))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                
                //dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {

                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdSupplierDetails.DataSource = dt;
                    grdSupplierDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = "0";
                    grdSupplierDetails.DataSource = null;
                    grdSupplierDetails.DataBind();
                }



            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }
        #endregion


        #region Event
        //--------------Add Option display div ----------------------
        protected void BtnAddOpt(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Attributes.Add("style", "display:none;");
                pnlAddDiv.Attributes.Add("style", "display:block;");

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

                string query = "";
                query = "Usp_BindData";         //Stored Procedure name   
                SqlCommand com = new SqlCommand(query, cn1);  //creating  SqlCommand  object  
                 com.Parameters.AddWithValue("@Query", "Search");
                com.Parameters.AddWithValue("@Company", txtSearch.Value);        //Company Name  
                com.Parameters.AddWithValue("@Supplier", txtSearch.Value);       //Supplier  Name 
                cn1.Open();
                com.ExecuteNonQuery();                     //executing the sqlcommand  
                cn1.Close();
                DataTable dt = new DataTable();
                
                using (var da = new SqlDataAdapter(com))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }


                if (dt.Rows.Count > 0)
                {
                   
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdSupplierDetails.DataSource = dt;
                 grdSupplierDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdSupplierDetails.DataSource = dt;
                    grdSupplierDetails.DataBind();
                }
                 
                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");


            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }


        }


        #endregion

        #region GridView
        protected void grdSupplierDetails_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            grdSupplierDetails.EditIndex = e.NewEditIndex;
            bindgrid();
        }
        protected void grdSupplierDetails_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //Finding the controls from Gridview for the row which is going to update  
            Label id = grdSupplierDetails.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox Company = grdSupplierDetails.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            TextBox ContactNo = grdSupplierDetails.Rows[e.RowIndex].FindControl("txt_Contact") as TextBox;
            TextBox CompanyAddress = grdSupplierDetails.Rows[e.RowIndex].FindControl("txt_CompanyAddress") as TextBox;
            TextBox SupplierName = grdSupplierDetails.Rows[e.RowIndex].FindControl("txt_Supplier") as TextBox;
            btnUpdate(id, Company, ContactNo, CompanyAddress, SupplierName);
            Label lblupdate = grdSupplierDetails.Rows[e.RowIndex].FindControl("lblupdate") as Label;
            lblupdate.Visible = true;
            lblupdate.ForeColor = System.Drawing.Color.Green;
            lblupdate.Text = "Suppliers are Updated Successfully";
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            grdSupplierDetails.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            bindgrid();
        }
        protected void grdSupplierDetails_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            grdSupplierDetails.EditIndex = -1;
            bindgrid();
        }
        protected void grdSupplierDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

       
        #endregion

        #region Method
        protected void btnBackAddUser_Click(object sender, EventArgs e)
        {
            try
            {

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");



            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }
        private void clearTextboxes()
        {
            txtCompanyName.Text = "";
            txtCompanyAddress.Text = "";
            txtPhoneNo.Text = "";
            txtSupplier.Text = "";
        }
        protected void btnSaveAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                query = "Usp_InsDelSupplier";         //Stored Procedure name   
                SqlCommand com = new SqlCommand(query, cn1);  //creating  SqlCommand  object  
                com.CommandType = CommandType.StoredProcedure;  //here we declaring command type as stored Procedure  
                com.Parameters.AddWithValue("@Company", txtCompanyName.Text.ToString());        //Company Name  
                com.Parameters.AddWithValue("@Add", txtCompanyAddress.Text.ToString());     //Supplier Company Address  
                com.Parameters.AddWithValue("@Contact", txtPhoneNo.Text.ToString());       //Supplier Phone No
                com.Parameters.AddWithValue("@Supplier", txtSupplier.Text.ToString());       //Supplier  Name 
                cn1.Open();
                com.ExecuteNonQuery();                     //executing the sqlcommand  
                cn1.Close();
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Green;
                lblmsg.Text = "Suppliers are Added Successfully";
                clearTextboxes();

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Error!</strong> <h4 >" + " Please Add correct details !" + " </h4>";
            }



        }

        protected void BtnBackFind_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");

            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void BtnEBack_Click(object sender, EventArgs e)
        {
            try
            {

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");



            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        private void btnUpdate(Label id, TextBox Company, TextBox ContactNo, TextBox CompanyAddress, TextBox SupplierName)
        {
            try
            {
                string query = "";
                query = "Usp_InsDelSupplier";         //Stored Procedure name   
                SqlCommand com = new SqlCommand(query, cn1);  //creating  SqlCommand  object  
                com.CommandType = CommandType.StoredProcedure;  //here we declaring command type as stored Procedure 
                com.Parameters.AddWithValue("@Query", "Update_Supplier");
                com.Parameters.AddWithValue("@Company", Company.Text.ToString());        //Company Name  
                com.Parameters.AddWithValue("@Add", CompanyAddress.Text.ToString());     //Supplier Company Address  
                com.Parameters.AddWithValue("@Contact", ContactNo.Text.ToString());       //Supplier Phone No
                com.Parameters.AddWithValue("@Supplier", SupplierName.Text.ToString());       //Supplier  Name 
                com.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
                cn1.Open();
                com.ExecuteNonQuery();                     //executing the sqlcommand  
                cn1.Close();
                
                

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Error!</strong> <h4 >" + " Please Update correct details !" + " </h4>";
            }
        }
        #endregion

    }
}
 