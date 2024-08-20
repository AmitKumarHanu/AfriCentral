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
    public partial class frmBranchMaster : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(CommonFunctions.connection.ToString());
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
                string qry = "   Select Br_ID ,Br_Name,Br_Address,Br_PhoneNo,Br_Email,Br_Website,Br_Country,Br_State,Br_VATRegNo,Br_VAT,Br_Discount,Br_Remark,isActive,CreatedOn,CreatedBy,Currency  from tbl_BranchMaster where Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "'  order by Br_Name asc  ";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdBranchDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdBranchDetails.DataSource = dt;
                    grdBranchDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdBranchDetails.DataSource = null;
                    grdBranchDetails.DataBind();
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


        //-------------- Bill Reg No Generate -----------------------
        private void BindRegNo()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "SP_Get_BranchRegNo";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@ComRegNo", dt_login_details.Rows[0]["Com_Id"].ToString() );
                cmd1.Parameters.AddWithValue("@Flag", "New");
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                Adp.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBranchID.Text = ds.Tables[0].Rows[0]["BrRegNo"].ToString();
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
                //Br_ID,Br_Name,Br_Address,Br_PhoneNo,Br_Email,Br_Website,Br_Country,Br_State,Br_VATRegNo
                //,Br_VAT,Br_Discount,Br_Remark,isActive,CreatedOn,UserType,Currency,UserName,Com_Name,GroupCode,GroupName

                string qry = "  Select * From  vw_BranchDetails where Br_ID like '" + txtSearch.Value.Trim() + "%' or Br_VATRegNo like '" + txtSearch.Value.Trim() + "%' or Br_VATRegNo like '" + txtSearch.Value.Trim() + "%' or Br_Name like '" + txtSearch.Value.Trim() + "%' and Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by Br_Name ";


                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdBranchDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdBranchDetails.DataSource = dt;
                    grdBranchDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdBranchDetails.DataSource = null;
                    grdBranchDetails.DataBind();
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
                    //Br_ID,Br_Name,Br_Address,Br_PhoneNo,Br_Email,Br_Website,Br_Country,Br_State,Br_VATRegNo
                    //,Br_VAT,Br_Discount,Br_Remark,isActive,CreatedOn,UserType,Currency,UserName,Com_Name,GroupCode,GroupName

                  
                    string qry = "  Select * From  vw_BranchDetails where Br_Id like '" + txtSearch.Value.Trim()  +"%' or Br_Name like '" + txtSearch.Value.Trim() + "%' or Br_VATRegNo like '" + txtSearch.Value.Trim() + "%' or Br_Email like '" + txtSearch.Value.Trim() + "%' and Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by Br_Name ";

                    DataTable dt = new DataTable();
                    dt = CommonFunctions.fetchdata(qry);

                    if (dt.Rows.Count > 0)
                    {
                        //Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, Com_Country, Com_State, 
                        //Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive,
                        //CreatedOn,   //CreatedBy from tbl_CompanyMaster

                        txtEBranchID.Text = dt.Rows[0]["Br_ID"].ToString();
                        txtEBranchName.Text = dt.Rows[0]["Br_Name"].ToString();
                        txtEBranchAddress.Text = dt.Rows[0]["Br_Address"].ToString();
                        txtEPhoneNo.Text = dt.Rows[0]["Br_PhoneNo"].ToString();
                        txtEEmail.Text = dt.Rows[0]["Br_Email"].ToString();
                        txtEWebsiteAddress.Text = dt.Rows[0]["Br_Website"].ToString();

                        drpECountryName.SelectedIndex = drpECountryName.Items.IndexOf(drpECountryName.Items.FindByValue(dt.Rows[0]["Br_Country"].ToString()));
                        txtEState.Text = dt.Rows[0]["Br_State"].ToString();
                        txtEVATRegNo.Text = dt.Rows[0]["Br_VATRegNo"].ToString();
                        txtEVATPercentage.Text = dt.Rows[0]["Br_VAT"].ToString();
                        txtEDiscountPercentage.Text = dt.Rows[0]["Br_Discount"].ToString();
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


        protected void grdBranchDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdBranchDetails.PageIndex = e.NewPageIndex;
            this.bindgrid();
        }

        protected void grdBranchDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdBranchDetails.Rows[rowIndex];
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void grdBranchDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdBranchDetails, "Select$" + e.Row.RowIndex);
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

        protected void grdBranchDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Label lblBrID = (Label) grdBranchDetails.SelectedRow.FindControl("lblBrID");
                string qry = " Select top 1 * From  vw_BranchDetails where Br_ID = '" + lblBrID.Text.Trim() + "' and Com_Id= '" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by Br_Name";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    //Br_ID,Br_Name,Br_Address,Br_PhoneNo,Br_Email,Br_Website,Br_Country,Br_State,Br_VATRegNo
                    //,Br_VAT,Br_Discount,Br_Remark,isActive,CreatedOn,UserType,Currency,UserName,Com_Name,GroupCode,GroupName
                    

                    lblBranchID.Text = dt.Rows[0]["Br_ID"].ToString();
                    lblBranchName.Text = dt.Rows[0]["Br_Name"].ToString();
                    lblWebsiteAddress.Text = dt.Rows[0]["Br_Website"].ToString();
                    lblBranchAddress.Text = dt.Rows[0]["Br_Address"].ToString();
                    lblCountryName.Text = dt.Rows[0]["CountryName"].ToString();
                    lblState.Text = dt.Rows[0]["Br_State"].ToString();

                    lblVATRegNo.Text = dt.Rows[0]["Br_VATRegNo"].ToString();
                    lblStatus.Text = dt.Rows[0]["Status"].ToString();
                    lblVATPercentage.Text = dt.Rows[0]["Br_VAT"].ToString();
                    lblDiscountPercentage.Text = dt.Rows[0]["Br_Discount"].ToString();
                    lblPhoneNo.Text = dt.Rows[0]["Br_PhoneNo"].ToString();
                    lblEmail.Text = dt.Rows[0]["Br_Email"].ToString();
                    lblCurrency.Text = dt.Rows[0]["Currency"].ToString();



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

        protected void btnBackSave_Click(object sender, EventArgs e)
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

        protected void btnSaveBranch_Click(object sender, EventArgs e)
        {   //----------Save Country Master Details-----------------
            try
            {

                if (txtBranchName.Text.ToString() != string.Empty && txtVATRegNo.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_Branch";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, Com_Country, Com_State, 
                    //Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive,
                    //CreatedOn,   //CreatedBy from tbl_CompanyMaster

                    cmd.Parameters.AddWithValue("@Br_ID", txtBranchID.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_Name", txtBranchName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_Address", txtBranchAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@Br_PhoneNo", txtPhoneNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_Email", txtEmail.Text.Trim().ToString()); ;
                    cmd.Parameters.AddWithValue("@Br_Website", txtWebsiteAddress.Text.ToString()); ;

                    cmd.Parameters.AddWithValue("@Br_Country", drpCountryName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Br_State", txtState.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_VATRegNo", txtVATRegNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Br_VAT", Convert.ToDecimal(txtVATPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Br_Discount", Convert.ToDecimal(txtDiscountPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@isActive", drpStatus.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Currency", drpCurrency.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Com_ID", dt_login_details.Rows[0]["Com_id"].ToString());

                   

                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "IBranch");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();

                    if (R != String.Empty)
                    {
                        Response.Redirect("frmBranchMaster.aspx");


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

                if (txtEBranchName.Text.ToString() != string.Empty && txtEVATRegNo.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_Branch";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Com_ID, Com_Name, Com_Address, Com_PhoneNo, Com_Email, Com_Website, Com_Country, Com_State, 
                    //Com_VATRegNo, Com_VAT, Com_Discount,  Com_Remark, isActive,
                    //CreatedOn,   //CreatedBy from tbl_CompanyMaster

                    cmd.Parameters.AddWithValue("@Br_ID", txtEBranchID.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_Name", txtEBranchName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_Address", txtEBranchAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@Br_PhoneNo", txtEPhoneNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_Email", txtEEmail.Text.Trim().ToString()); ;
                    cmd.Parameters.AddWithValue("@Br_Website", txtEWebsiteAddress.Text.ToString()); ;

                    cmd.Parameters.AddWithValue("@Br_Country", drpECountryName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Br_State", txtEState.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_VATRegNo", txtEVATRegNo.Text.ToString());
                    cmd.Parameters.AddWithValue("@Br_VAT", Convert.ToDecimal(txtEVATPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@Br_Discount", Convert.ToDecimal(txtEDiscountPercentage.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@isActive", drpEStatus.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Currency", drpECurrency.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Com_ID", dt_login_details.Rows[0]["Com_id"].ToString());


                

                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "EBranch");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();



                    if (R != String.Empty)
                    {
                        Response.Redirect("frmBranchMaster.aspx");
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
    }
}