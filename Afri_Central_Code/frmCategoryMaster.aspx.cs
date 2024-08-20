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
    public partial class frmCategoryMaster : System.Web.UI.Page
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
                string qry = " Select CategoryId, CategoryName, CID  from tbl_ItemCategory where Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by CategoryId asc";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCategory"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCategory.DataSource = dt;
                    grdCategory.DataBind();
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

                string qry = "Select * From  tbl_ItemCategory where CategoryName like '%" + txtSearch.Value.Trim() + "%' and  Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "'   order by CategoryName asc";


                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCategory"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCategory.DataSource = dt;
                    grdCategory.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCategory.DataSource = null;
                    grdCategory.DataBind();
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
                    string qry = "Select * From  tbl_ItemCategory where CategoryName like '" + txtSearch.Value.Trim() + "%' and  Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "'   order by CategoryName asc";


                    DataTable dt = new DataTable();
                    dt = CommonFunctions.fetchdata(qry);

                    if (dt.Rows.Count > 0)
                    {

                        txtECategoryID.Text = dt.Rows[0]["CID"].ToString();
                        txtECategoryName.Text = dt.Rows[0]["CategoryName"].ToString();

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



        //--------------Data Grid Functiond Function -----------------------  

        protected void grdCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCategory.PageIndex = e.NewPageIndex;
            this.bindgrid();
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

                if (txtECategoryName.Text.ToString() != string.Empty )
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn1;
                    cmd.CommandText = "SP_AF_Category";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID",  txtECategoryID.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Data", txtECategoryName.Text.Trim().ToString());                   
                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "EC");
                    
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    cn1.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    cn1.Close();

                    if (R == "1")
                    {
                        Response.Redirect("frmCategoryMaster.aspx");

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

        protected void grdCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdCategory.Rows[rowIndex];
            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes.Add("style", "display:block;");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please cross check server connection details !" + " </h4>";
            }

        }

        protected void grdCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdCategory, "Select$" + e.Row.RowIndex);
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

        protected void grdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label lblCatId = (Label)grdCategory.SelectedRow.FindControl("lblCatId");
                string qry = "Select * From  tbl_ItemCategory where CID like '" + lblCatId.Text.Trim() + "%'  order by CategoryName asc";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {

                    lblCategoryID.Text = dt.Rows[0]["CategoryId"].ToString();
                    lblCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();
                  

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



        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            //----------Save Category Master Details-----------------
            try
            {

                if (txtCategoryName.Text.ToString() != string.Empty)
                {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn1;
                cmd.CommandText = "SP_AF_Category";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", "0");
                cmd.Parameters.AddWithValue("@Data", txtCategoryName.Text.Trim().ToString());
                cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Flag", "IC");
                SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);
                cn1.Open();
                cmd.ExecuteNonQuery();
                String R = output.Value.ToString();
                cn1.Close();

                    if (R == "1")
                    {
                        Response.Redirect("frmCategoryMaster.aspx");

                        //pnlMain.Attributes.Add("style", "display:block;");
                        //pnlAddDiv.Attributes.Add("style", "display:none;");
                        //pnlViewDiv.Attributes.Add("style", "display:none;");
                        //pnlEdit.Attributes.Add("style", "display:none;");


                        //lblloginmsg.Attributes.Add("class", "active");
                        //lblloginmsg.Attributes["style"] = "color:Green; font-weight:bold;";
                        //lblloginmsg.Attributes.Add("style", "display:block;");
                        //lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h2 >" + " New Category add successfull !" + " </h2>";

                    }
                    else
                    {
                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes.Add("style", "display:block;");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " The category details already exist in master !" + " </h4>";
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
    }
}

