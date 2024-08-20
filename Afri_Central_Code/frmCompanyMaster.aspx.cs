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
    public partial class frmCompanyMaster : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(CommonFunctions.connection.ToString());
        DataTable dt_login_details = new DataTable();

        //----------Load Fun-----------------
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
        
        //----------Bind Data Grid-----------------
        public void bindgrid()
        {

            try
            {
                //-------------Bind NationalityMaster------------------
                string qryNt = " Select CountryCode ,CountryName  from tbl_CountryMaster order by CountryName";
                DataTable dtNt = new DataTable();
                dtNt = CommonFunctions.fetchdata(qryNt);


                drpCountryName.DataSource = dtNt;
                drpCountryName.DataValueField = "CountryCode";
                drpCountryName.DataTextField = "CountryName";
                drpCountryName.DataBind();
                drpCountryName.Items.Insert(0, new ListItem("--Select Country--", "0"));

                drpECountryName.DataSource = dtNt;
                drpECountryName.DataValueField = "CountryCode";
                drpECountryName.DataTextField = "CountryName";
                drpECountryName.DataBind();
                drpECountryName.Items.Insert(0, new ListItem("--Select Country--", "0"));

                //-------------Bind Currency-----------------
                string qryCry = " Select Code , name  from tbl_CurrencyMaster order by Name";
                DataTable dtCry = new DataTable();
                dtCry = CommonFunctions.fetchdata(qryCry);


                drpCurrency.DataSource = dtCry;
                drpCurrency.DataValueField = "Code";
                drpCurrency.DataTextField = "Code";
                drpCurrency.DataBind();
                drpCurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));


                drpECurrency.DataSource = dtCry;
                drpECurrency.DataValueField = "Code";
                drpECurrency.DataTextField = "Code";
                drpECurrency.DataBind();
                drpECurrency.Items.Insert(0, new ListItem("--Select Currency--", "0"));

                //-------------Bind Data Grid-----------------
                string qry = "   Select Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, CountryName, Com_State, Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive, Status  from vw_CompanyDetails where  isnull(isActive,0) =1  order by Com_Name asc  ";
            
                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCompanyDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCompanyDetails.DataSource = dt;
                    grdCompanyDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCompanyDetails.DataSource = null;
                    grdCompanyDetails.DataBind();
                }

              

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
                Session["grdCompanyDetails"] = null;
                BindRegNo();
                pnlMain.Attributes.Add("style", "display:none;");
                pnlAddDiv.Attributes.Add("style", "display:block;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }

        }


        //-------------- Bill No Generate -----------------------
        private void BindRegNo()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "SP_Get_CompanyID";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Flag", "New");
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                Adp.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                   txtCompanyID.Text = ds.Tables[0].Rows[0]["ComRegNo"].ToString();


                }

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please correction internet connectivity, Page binding dependent showing error !" + " </h4>";
            }
        }


        //--------------Display Serach Div-----------------------
        protected void BtnSearchOpt(object sender, EventArgs e)
        {
            try
            {

                string qry = "  Select * From  vw_CompanyDetails where Com_Name like '" + txtSearch.Value.Trim() + "%' or Com_VATRegNo like '" + txtSearch.Value.Trim() + "%' or Com_Email like '" + txtSearch.Value.Trim() + "%' order by Com_Name ";


                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCompanyDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCompanyDetails.DataSource = dt;
                    grdCompanyDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdCompanyDetails.DataSource = null;
                    grdCompanyDetails.DataBind();
                }

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");

            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }


        }

        //--------------Edit Option display div -----------------------
        protected void BtnEditOpt(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Value.Trim() != String.Empty)
                {
                    string qry = "  Select * From  tbl_CompanyMaster where Com_Name like '" + txtSearch.Value.Trim() + "%' or Com_VATRegNo like '" + txtSearch.Value.Trim() + "%' or Com_Email like '" + txtSearch.Value.Trim() + "%' order by Com_Name ";
                
                    DataTable dt = new DataTable();
                    dt = CommonFunctions.fetchdata(qry);

                    if (dt.Rows.Count > 0)
                    {  
                        //Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, Com_Country, Com_State, 
                       //Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive,
                       //CreatedOn,   //CreatedBy from tbl_CompanyMaster

                        txtECompanyID.Text = dt.Rows[0]["Com_ID"].ToString();
                        txtECompanyName.Text = dt.Rows[0]["Com_Name"].ToString();
                        txtECompanyAddress.Text = dt.Rows[0]["Com_Address"].ToString();
                        txtEPhoneNo.Text = dt.Rows[0]["Com_PhoneNo"].ToString();
                        txtEEmail.Text = dt.Rows[0]["Com_Email"].ToString();
                        txtEWebsiteAddress.Text = dt.Rows[0]["Com_Website"].ToString();

                        drpECountryName.SelectedIndex = drpECountryName.Items.IndexOf(drpECountryName.Items.FindByValue(dt.Rows[0]["Com_Country"].ToString()));
                        txtEState.Text = dt.Rows[0]["Com_State"].ToString();
                        txtEVATRegNo.Text = dt.Rows[0]["Com_VATRegNo"].ToString();
                        txtEVATPercentage.Text = dt.Rows[0]["Com_VAT"].ToString();
                        txtEDiscountPercentage.Text = dt.Rows[0]["Com_Discount"].ToString();
                        drpEStatus.SelectedIndex = drpEStatus.Items.IndexOf(drpEStatus.Items.FindByValue(dt.Rows[0]["isActive"].ToString()));
                        drpECurrency.SelectedIndex = drpECurrency.Items.IndexOf(drpECurrency.Items.FindByValue(dt.Rows[0]["Currency"].ToString()));

                       

                        pnlMain.Attributes.Add("style", "display:none;");
                        pnlAddDiv.Attributes.Add("style", "display:none;");
                        pnlViewDiv.Attributes.Add("style", "display:none;");
                        pnlEdit.Attributes.Add("style", "display:block;");
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

        //--------------Find Serach  details in database -----------------------
        protected void grdCompanyDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCompanyDetails.PageIndex = e.NewPageIndex;
            this.bindgrid();
        }

        protected void grdCompanyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdCompanyDetails.Rows[rowIndex];
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void grdCompanyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdCompanyDetails, "Select$" + e.Row.RowIndex);
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

        protected void grdCompanyDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Label lblCompVATRegNo = (Label)grdCompanyDetails.SelectedRow.FindControl("lblCompVATRegNo");
                string qry = "";

                qry = "  Select top 1 * From  vw_CompanyDetails where Com_VATRegNo = '" + lblCompVATRegNo.Text.Trim() + "' order by Com_VATRegNo";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    // [Com_ID],[Com_Name],[Com_Address],[Com_PhoneNo],[Com_Email],[Com_Website],[Com_Country],[Com_State],[Com_VATRegNo],
                    // [Com_VAT],[Com_Discount],[Com_Remark],[isActive],Status

                    lblCompanyID.Text = dt.Rows[0]["Com_ID"].ToString();
                    lblCompanyName.Text = dt.Rows[0]["Com_Name"].ToString();
                    lblWebsiteAddress.Text = dt.Rows[0]["Com_Website"].ToString();
                    lblCompanyAddress.Text = dt.Rows[0]["Com_Address"].ToString();
                    lblCountryName.Text = dt.Rows[0]["CountryName"].ToString();
                    lblState.Text = dt.Rows[0]["Com_State"].ToString();

                    lblVATRegNo.Text = dt.Rows[0]["Com_VATRegNo"].ToString();
                    lblStatus.Text = dt.Rows[0]["Status"].ToString();
                    lblVATPercentage.Text = dt.Rows[0]["Com_VAT"].ToString();
                    lblDiscountPercentage.Text = dt.Rows[0]["Com_Discount"].ToString();
                    lblPhoneNo.Text = dt.Rows[0]["Com_PhoneNo"].ToString();
                    lblEmail.Text = dt.Rows[0]["Com_Email"].ToString();
                    lblCurrency.Text = dt.Rows[0]["CurrencyCode"].ToString();



                    pnlMain.Attributes.Add("style", "display:none;");
                    pnlAddDiv.Attributes.Add("style", "display:none;");
                    pnlViewDiv.Attributes.Add("style", "display:block;");
                    pnlEdit.Attributes.Add("style", "display:none;");
                }


            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }
        

        protected void BtnBackFind_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
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
                pnlViewDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");


            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void btnEUpdate_Click(object sender, EventArgs e)
        {

            //----------Save Country Master Details-----------------
            try
            {

                if (txtECompanyName.Text.ToString() != string.Empty && txtEVATRegNo.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_Company";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, Com_Country, Com_State, 
                    //Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive,
                    //CreatedOn,   //CreatedBy from tbl_CompanyMaster

                    cmd.Parameters.AddWithValue("@Com_ID", txtECompanyID.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_Name", txtECompanyName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_Address", txtECompanyAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@Com_PhoneNo", txtEPhoneNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_Email", txtEEmail.Text.Trim().ToString()); ;
                    cmd.Parameters.AddWithValue("@Com_Website", txtEWebsiteAddress.Text.ToString()); ;

                    cmd.Parameters.AddWithValue("@Com_Country", drpECountryName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Com_State", txtEState.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_VATRegNo", txtEVATRegNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Com_VAT", Convert.ToDecimal(txtEVATPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Com_Discount", Convert.ToDecimal(txtEDiscountPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@isActive", drpEStatus.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Currency", drpECurrency.SelectedValue.Trim());


                


                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "ECom");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();

            

                    if (R != String.Empty)
                    {
                        Response.Redirect("frmCompanyMaster.aspx");
                    }
                    else
                    {
                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Company details are not found !" + " </h4>";
                    }
                }
                else
                {
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

        protected void btnSaveCompany_Click(object sender, EventArgs e)
        {
            //----------Save Country Master Details-----------------
            try
            {

                if (txtCompanyName.Text.ToString() != string.Empty && txtVATRegNo.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_Company";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, Com_Country, Com_State, 
                    //Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive,
                    //CreatedOn,   //CreatedBy from tbl_CompanyMaster

                    cmd.Parameters.AddWithValue("@Com_ID", txtCompanyID.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_Name", txtCompanyName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_Address", txtCompanyAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@Com_PhoneNo", txtPhoneNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_Email", txtEmail.Text.Trim().ToString()); ;
                    cmd.Parameters.AddWithValue("@Com_Website", txtWebsiteAddress.Text.ToString()); ;

                    cmd.Parameters.AddWithValue("@Com_Country", drpCountryName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Com_State", txtState.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Com_VATRegNo", txtVATRegNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Com_VAT", Convert.ToDecimal(txtVATPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Com_Discount", Convert.ToDecimal(txtDiscountPercentage.Text.Trim()).ToString() );              
                    cmd.Parameters.AddWithValue("@isActive", drpStatus.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Currency", drpCurrency.SelectedValue.Trim());

                 

                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "ICom");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();

                    if (R != String.Empty)
                    {
                        Response.Redirect("frmCompanyMaster.aspx");


                    }
                    else
                    {
                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Company details are already exist !" + " </h4>";
                    }
                }
                else
                {
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

        protected void btnBackSaveCompany_Click(object sender, EventArgs e)
        {
            try
            {

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");


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