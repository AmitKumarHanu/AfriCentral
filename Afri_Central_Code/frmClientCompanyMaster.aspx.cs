using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Afri_Central_Code
{
    public partial class frmClientCompanyMaster : System.Web.UI.Page
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
                //-------------Bind Company Name------------------
                string qryCt = " Select Com_ID ,Com_Name  from tbl_CompanyMaster order by Com_Name";
                DataTable dtCt = new DataTable();
                dtCt = CommonFunctions.fetchdata(qryCt);


                drpCompanyName.DataSource = dtCt;
                drpCompanyName.DataValueField = "Com_ID";
                drpCompanyName.DataTextField = "Com_Name";
                drpCompanyName.DataBind();
                drpCompanyName.Items.Insert(0, new ListItem("--Select Company--", "0"));

                //-------------Bind NationalityMaster------------------
                string qryNt = " Select CountryCode ,CountryName  from tbl_CountryMaster order by CountryName";
                DataTable dtNt = new DataTable();
                dtNt = CommonFunctions.fetchdata(qryNt);


                drpCountryName.DataSource = dtNt;
                drpCountryName.DataValueField = "CountryCode";
                drpCountryName.DataTextField = "CountryName";
                drpCountryName.DataBind();
                drpCountryName.Items.Insert(0, new ListItem("--Select Country--", "0"));

                

                //-------------Bind Currency-----------------
                string qryCry = " Select Code , name  from tbl_CurrencyMaster order by Name";
                DataTable dtCry = new DataTable();
                dtCry = CommonFunctions.fetchdata(qryCry);


                //-------------Bind Data Grid-----------------
                string qry = "   Select X.Client_ID ,X.Client_Name,X.Client_Address,X.Client_PhoneNo,X.Client_Email,X.Client_Website,X.Client_Country,X.isActive, Y.Com_Name from tbl_ComClientMaster X, tbl_CompanyMaster Y where X.Com_ID=Y.Com_ID order by X.Client_Name asc  ";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdClientDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdClientDetails.DataSource = dt;
                    grdClientDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdClientDetails.DataSource = null;
                    grdClientDetails.DataBind();
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
                Session["grdClientDetails"] = null;
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
                cmd1.Parameters.AddWithValue("@ComRegNo", dt_login_details.Rows[0]["Com_Id"].ToString());
                cmd1.Parameters.AddWithValue("@Flag", "Client");
                DataSet ds = new DataSet();
                con.Open();
                SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
                Adp.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtClientID.Text = ds.Tables[0].Rows[0]["CMRegNo"].ToString();
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
              
                string qry = "   Select X.Client_ID ,X.Client_Name,X.Client_Address,X.Client_PhoneNo,X.Client_Email,X.Client_Website,X.Client_Country,X.isActive, Y.Com_Name from tbl_ComClientMaster X, tbl_CompanyMaster Y where X.Com_ID=Y.Com_ID and X.Client_ID like '" + txtSearch.Value.Trim() + "%'  or X.Client_Name like '" + txtSearch.Value.Trim() + "%' order by X.Client_Name asc  ";


                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdClientDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdClientDetails.DataSource = dt;
                    grdClientDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdClientDetails.DataSource = null;
                    grdClientDetails.DataBind();
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


        protected void grdClientDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdClientDetails.PageIndex = e.NewPageIndex;
            this.bindgrid();
        }

        protected void grdClientDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grdClientDetails.Rows[rowIndex];
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void grdClientDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdClientDetails, "Select$" + e.Row.RowIndex);
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

        protected void grdClientDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Label lblClient_ID = (Label)grdClientDetails.SelectedRow.FindControl("lblClient_ID");
                Label lblCompanyName = (Label)grdClientDetails.SelectedRow.FindControl("lblCompanyName");

                string qry = "   Select Client_ID,Client_Name,Client_Address,Client_PhoneNo,Client_Email,Client_Website,Client_Country,CountryName, isActive,Status,CreatedOn,UserName,Com_Name,Com_ID,GroupCode,GroupName , Client_CountryName from AfriSmartA.dbo.vw_ClientDetails where Client_ID like '" + lblClient_ID.Text.Trim() + "%'  or Com_Name like '" + lblCompanyName.Text.Trim() + "%' order by Client_Name asc  ";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    // Client_ID,Client_Name,Client_Address,Client_PhoneNo,Client_Email,Client_Website,Client_Country,CountryName
                    //,isActive,Status,CreatedOn,UserName,Com_Name,Com_ID,GroupCode,GroupName
                    // FROM AfriSmartA.dbo.vw_ClientDetails


                    lblClientID.Text = dt.Rows[0]["Client_ID"].ToString();
                    lblClientName.Text = dt.Rows[0]["Client_Name"].ToString();
                    lblWebsiteAddress.Text = dt.Rows[0]["Client_Website"].ToString();
                    lblClientAddress.Text = dt.Rows[0]["Client_Address"].ToString();
                    lblCountryName.Text = dt.Rows[0]["Client_CountryName"].ToString();                   
                    lblStatus.Text = dt.Rows[0]["Status"].ToString();                    
                    lblPhoneNo.Text = dt.Rows[0]["Client_PhoneNo"].ToString();
                    lblEmail.Text = dt.Rows[0]["Client_Email"].ToString();
                    lblComName.Text = dt.Rows[0]["Com_Name"].ToString();

                    pnlMain.Attributes.Add("style", "display:none;");
                    pnlAddDiv.Attributes.Add("style", "display:none;");
                    pnlViewDiv.Attributes.Add("style", "display:block;");
                    pnlEdit.Attributes.Add("style", "display:none;");

                    BindClientUser();
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
        {
            //----------Save Country Master Details-----------------
            try
            {

                if (txtClientName.Text.ToString() != string.Empty)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_Client";
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Client_ID,Client_Name,Client_Address,Client_PhoneNo,Client_Email,Client_Website,
                    // Client_Country,Com_ID,isActive,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy
                    // FROM AfriSmartA.dbo.tbl_ComClientMaster

                    cmd.Parameters.AddWithValue("@Client_ID", txtClientID.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Client_Name", txtClientName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Client_Address", txtClientAddress.Text.ToString());
                    cmd.Parameters.AddWithValue("@Client_PhoneNo", txtPhoneNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Client_Email", txtEmail.Text.Trim().ToString()); ;
                    cmd.Parameters.AddWithValue("@Client_Website", txtWebsiteAddress.Text.ToString()); ;

                    cmd.Parameters.AddWithValue("@Client_Country", drpCountryName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@Com_ID", drpCompanyName.SelectedValue.Trim().ToString());                  
                    cmd.Parameters.AddWithValue("@isActive", drpStatus.SelectedValue.Trim());    
                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "ICompany");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();

                    if (R != String.Empty)
                    {
                        Response.Redirect("frmClientCompanyMaster.aspx");


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

      
        protected void BtnAddUser_Click(object sender, EventArgs e)
        {

            //-------------Bind Company Name------------------
            string qryC = " Select Com_ID ,Com_Name  from tbl_CompanyMaster where Com_Name='" + lblComName.Text.Trim() + "'  order by Com_Name";
            DataTable dtC = new DataTable();
            dtC = CommonFunctions.fetchdata(qryC);


            drpECompanyName.DataSource = dtC;
            drpECompanyName.DataValueField = "Com_ID";
            drpECompanyName.DataTextField = "Com_Name";
            drpECompanyName.DataBind();

            //-------------Bind Client Name------------------
            string qryCt = "Select Client_ID, Client_Name From tbl_ComClientMaster where Client_Name='" + lblClientName.Text.Trim() + "'  order by Client_Name";
            DataTable dtCt = new DataTable();
            dtCt = CommonFunctions.fetchdata(qryCt);

            drpEClientName.DataSource = dtCt;
            drpEClientName.DataValueField = "Client_ID";
            drpEClientName.DataTextField = "Client_Name";
            drpEClientName.DataBind();

            //-------------Bind Client Manager Grop Users------------------
            string qryUser = "  Select UserID, UserName From tbl_UserMaster where Com_id='" + drpECompanyName.SelectedValue + "' and GroupID='110' and GroupCode='CLMG' order by UserName ";
            DataTable dtUser = new DataTable();
            dtUser = CommonFunctions.fetchdata(qryUser);

            drpEManagerName.DataSource = dtUser;
            drpEManagerName.DataValueField = "UserID";
            drpEManagerName.DataTextField = "UserName";
            drpEManagerName.DataBind();
            drpEManagerName.Items.Insert(0, new ListItem("--Select User Login ID--", "0"));


            pnlMain.Attributes.Add("style", "display:none;");
            pnlAddDiv.Attributes.Add("style", "display:none;");
            pnlViewDiv.Attributes.Add("style", "display:none;");
            pnlEdit.Attributes.Add("style", "display:block;");

            BindClientUser();
        }

        protected void btnEClientUser_Click(object sender, EventArgs e)
        {
            //----------Save Client Application User Details-----------------
            try
            {

                if (drpEManagerName.SelectedIndex != 0 )
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_ClientUser";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Com1_ID", drpECompanyName.SelectedValue.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Client_ID", drpEClientName.SelectedValue.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Login_ID", drpEManagerName.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@UseName", drpEManagerName.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@GroupID", "110");
                    cmd.Parameters.AddWithValue("@GroupCode", "CLMG");
                    cmd.Parameters.AddWithValue("@Com_ID", dt_login_details.Rows[0]["Com_id"].ToString());
                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "AddClientUser");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();



                    if (R != String.Empty)
                    {
                        BindClientUser();
                      //  Response.Redirect("frmClientCompanyMaster.aspx");
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


        public void BindClientUser()
        {
            //-------------Bind Data Grid-----------------
            string qry = "  Select EmployeeID, First_Name, Last_Name, USerName as LoginID, Designation,  Y.GroupName, Z.Client_Name  From tbl_UserMaster X, tbl_GroupMaster Y, tbl_ComClientMaster Z where X.GroupID=Y.GroupID and X.GroupCode = Y.GroupCode  and X.ClientID=Z.Client_ID and X.Com_Id=Z.Com_ID   and X.Com_id='" + drpECompanyName.SelectedValue + "' and X.GroupID='110' and X.GroupCode='CLMG' and X.ClientID='" + drpEClientName.SelectedValue + "' order by UserName  asc  ";

            DataTable dt = new DataTable();
            dt = CommonFunctions.fetchdata(qry);

            if (dt.Rows.Count > 0)
            {
                Session["grdClientUser"] = dt;
                lbl_total.Text = dt.Rows.Count.ToString();
                grdClientUser.DataSource = dt;
                grdClientUser.DataBind();
            }
            else
            {
                lbl_total.Text = dt.Rows.Count.ToString();
                grdClientUser.DataSource = null;
                grdClientUser.DataBind();
            }
        }

        protected void grdClientUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdClientUser.PageIndex = e.NewPageIndex;
            this.BindClientUser();
        }

        protected void drpEClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindClientUser();
        }
    }
}