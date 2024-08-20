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
    public partial class frmMasterSupplier : System.Web.UI.Page
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
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }




        public void bindgrid()
        {
            //----------Bind Data Grid-----------------
            try
            {
                 string qry = " Select SupplierId ,SupplierName, ContactNo , Company, CompanyAddress , SId from tbl_SupplierMaster where Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by SupplierName asc";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdSupplier"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdSupplier.DataSource = dt;
                    grdSupplier.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();

                }
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }



        //--------------Add Option display div ----------------------
        protected void BtnAddOpt(object sender, EventArgs e)
        {
            try
            {

                pnlMain.Attributes.Add("style", "display:none;");
                pnlAddDiv.Attributes.Add("style", "display:block;");
                pnlEdit.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }

        }



        //--------------Display Serach Div-----------------------
        protected void BtnSearchOpt(object sender, EventArgs e)
        {
            try
            {
                string qry = " Select SupplierId ,SupplierName, ContactNo , Company, CompanyAddress, SId  from tbl_SupplierMaster  where SupplierName like '%" + txtSearch.Value.Trim() + "%' and Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by SupplierName asc";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdSupplier"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdSupplier.DataSource = dt;
                    grdSupplier.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdSupplier.DataSource = null;
                    grdSupplier.DataBind();
                }



                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }


        }

        //--------------Edit Option display div -----------------------
        protected void BtnEditOpt(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Value.Trim() != String.Empty)
                {
                    string qry = " Select SupplierId ,SupplierName, ContactNo , Company, CompanyAddress, SId  from tbl_SupplierMaster  where SupplierName like '" + txtSearch.Value.Trim() + "%' and Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by SupplierName asc";


                    DataTable dt = new DataTable();
                    dt = CommonFunctions.fetchdata(qry);

                    if (dt.Rows.Count > 0)
                    {

                        txtESupplierId.Text = dt.Rows[0]["SId"].ToString();
                        txtESupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
                        txtEContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                        txtECompanyName.Text = dt.Rows[0]["Company"].ToString();
                        txtECompanyAddress.Text = dt.Rows[0]["CompanyAddress"].ToString();


                        pnlMain.Attributes.Add("style", "display:none;");
                        pnlAddDiv.Attributes.Add("style", "display:none;");
                        pnlEdit.Attributes.Add("style", "display:block;");
                        pnlViewDiv.Attributes.Add("style", "display:none;");

                    }
                }

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }




        }

        protected void grdSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupplier.PageIndex = e.NewPageIndex;
            this.bindgrid();
        }

        protected void grdSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdSupplier.Rows[rowIndex];
            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }

        protected void grdSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdSupplier, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes["style"] = "cursor:pointer";
                }
            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }

        protected void grdSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label lblSupplier = (Label)grdSupplier.SelectedRow.FindControl("lblSupplierId");
             
                string qry = " Select SupplierId ,SupplierName, ContactNo , Company, CompanyAddress, SId  from tbl_SupplierMaster  where SId like '" + lblSupplier.Text.Trim()  + "%' order by SupplierName asc";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {

                    lblSupplierId.Text = dt.Rows[0]["SId"].ToString();
                    lblSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
                    lblContactNo.Text = dt.Rows[0]["ContactNo"].ToString();
                    lblCompany.Text = dt.Rows[0]["Company"].ToString();
                    lblCompanyAddress.Text = dt.Rows[0]["CompanyAddress"].ToString();
               

                    pnlMain.Attributes.Add("style", "display:none;");
                    pnlAddDiv.Attributes.Add("style", "display:none;");
                    pnlViewDiv.Attributes.Add("style", "display:block;");
                    pnlEdit.Attributes.Add("style", "display:none;");
                }


            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }

        protected void btnBackAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            //----------Save Supplier Master Details-----------------
            try
            {
                //--SupplierId ,SupplierName,ContactNo,Company,CompanyAddress,

                if (txtSupplierName.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn1;
                    cmd.CommandText = "SP_AF_Supplier";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierId", "0");
                    cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Company", txtCompanyName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@CompanyAddress", txtCompanyAddress.Text.Trim().ToString());                 
                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "IS");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    cn1.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    cn1.Close();

                    if (R == "1")
                    {
                        Response.Redirect("frmMasterSupplier.aspx");

                

                    }
                    else
                    {
                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes.Add("style", "display:block;");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " The supplier details already exist in master !" + " </h4>";
                    }

                }
                else
                {
                    lblloginmsg.Attributes.Add("class", "active");
                    lblloginmsg.Attributes.Add("style", "display:block;");
                    lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                    lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please fill details on all the require fields !" + " </h4>";
                }
            }
            catch (Exception ex)
            {


                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }


        }

        protected void BtnVBack_Click(object sender, EventArgs e)
        {
            try
            {

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");

            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }

        protected void BtnEBack_Click(object sender, EventArgs e)
        {

            try
            {

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");


            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }
        }

        protected void btnEUpdate_Click(object sender, EventArgs e)
        {
            //----------Save Country Master Details-----------------
            try
            {

                if (txtESupplierName.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn1;
                    cmd.CommandText = "SP_AF_Supplier";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierId", txtESupplierId.Text.Trim().ToString()) ;
                    cmd.Parameters.AddWithValue("@SupplierName", txtESupplierName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ContactNo", txtEContactNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Company", txtECompanyName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@CompanyAddress", txtECompanyAddress.Text.Trim().ToString());

                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "ES");

                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    cn1.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    cn1.Close();

                    if (R == "1")
                    {
                        Response.Redirect("frmMasterSupplier.aspx");

                        pnlMain.Attributes.Add("style", "display:block;");
                        pnlAddDiv.Attributes.Add("style", "display:none;");
                        pnlEdit.Attributes.Add("style", "display:none;");
                        pnlViewDiv.Attributes.Add("style", "display:none;");

                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes.Add("style", "display:block;");
                        lblloginmsg.Attributes["style"] = "color:Green; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h2 >" + " Edit Category add successfull !" + " </h2>";

                    }
                    else
                    {
                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes.Add("style", "display:block;");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " The ccategory details already exist in master !" + " </h4>";
                    }
                }
                else
                {
                    lblloginmsg.Attributes.Add("class", "active");
                    lblloginmsg.Attributes.Add("style", "display:block;");
                    lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                    lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please fill details on all the require fields !" + " </h4>";
                }

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please check database connection details !" + " </h4>";
            }
        }
    }
}