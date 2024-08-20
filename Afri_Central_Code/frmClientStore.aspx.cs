using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Afri_Central_Code
{
    public partial class frmClientStore : System.Web.UI.Page
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
                

                //-------------Bind Data Grid-----------------
                string qry = "  SELECT TOP (100) PERCENT X.Client_ID, X.Client_Name, X.Client_Address, X.Client_PhoneNo, X.Client_Email, X.Client_Website, X.Client_Country, X.isActive, Y.Com_Name, COUNT(Z.Client_ID) AS StoreNo FROM     dbo.tbl_ComClientMaster AS X INNER JOIN                  dbo.tbl_CompanyMaster AS Y ON X.Com_ID = Y.Com_ID LEFT OUTER JOIN                 dbo.tbl_ClientStoreRelation AS Z ON X.Com_ID = Z.Com_ID GROUP BY X.Client_ID, X.Client_Name, X.Client_Address, X.Client_PhoneNo, X.Client_Email, X.Client_Website, X.Client_Country, X.isActive, Y.Com_Name ORDER BY X.Client_Name  ";

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



        //--------------Display Serach Div-----------------------
        protected void BtnSearchOpt(object sender, EventArgs e)
        {
            try
            {

                string qry = "  Select X.Client_ID ,X.Client_Name,X.Client_Address,X.Client_PhoneNo,X.Client_Email,X.Client_Website,X.Client_Country,X.isActive,   Y.Com_Name, Count(Z.Client_ID) as StoreNo from tbl_ComClientMaster X, tbl_CompanyMaster Y , tbl_ClientStoreRelation Z where     X.Com_ID=Y.Com_ID and X.Com_ID=z.Com_ID and X.Client_ID like 'CMFC11720638604%'  or X.Client_Name like 'CMFC11720638604%'    Group by X.Client_ID ,X.Client_Name,X.Client_Address,X.Client_PhoneNo,X.Client_Email,X.Client_Website,X.Client_Country,X.isActive,    Y.Com_Name    order by X.Client_Name asc   ";


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
                //pnlAddDiv.Attributes.Add("style", "display:none;");
                //pnlViewDiv.Attributes.Add("style", "display:none;");
                //pnlEdit.Attributes.Add("style", "display:none;");

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

                //-------------Bind Data Grid-----------------
                string qry = "  Select Br_ID, Br_Name, Br_Address from tbl_ClientStoreRelation where Com_id='" + dt_login_details.Rows[0]["Com_id"].ToString() +  "' and Client_ID='" + lblClient_ID.Text.Trim() + "' ";
                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdClientStore"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdClientStore.DataSource = dt;
                    grdClientStore.DataBind();


                    pnlMain.Attributes.Add("style", "display:none;");
                    pnlAddDiv.Attributes.Add("style", "display:none;");
                    pnlViewDiv.Attributes.Add("style", "display:block;");
                   

                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grdClientStore.DataSource = null;
                    grdClientStore.DataBind();

                    pnlMain.Attributes.Add("style", "display:block;");
                    pnlAddDiv.Attributes.Add("style", "display:none;");
                    pnlViewDiv.Attributes.Add("style", "display:none;");
                   
                }



            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void BtnBackView_Click(object sender, EventArgs e)
        {
            try
            {

                pnlMain.Attributes.Add("style", "display:block;");
                pnlAddDiv.Attributes.Add("style", "display:none;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
               

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
                //-------------Bind Company Name------------------
                string qryC = " Select Com_ID ,Com_Name  from tbl_CompanyMaster where Com_ID='" + dt_login_details.Rows[0]["Com_id"].ToString() + "'  order by Com_Name";
                DataTable dtC = new DataTable();
                dtC = CommonFunctions.fetchdata(qryC);

                drpECompanyName.DataSource = dtC;
                drpECompanyName.DataValueField = "Com_ID";
                drpECompanyName.DataTextField = "Com_Name";
                drpECompanyName.DataBind();

                //-------------Bind Client Name------------------
                string qryCt = "Select Client_ID, Client_Name From tbl_ComClientMaster  where Com_ID='" + dt_login_details.Rows[0]["Com_id"].ToString() + "'  order by Client_Name";
                DataTable dtCt = new DataTable();
                dtCt = CommonFunctions.fetchdata(qryCt);

                drpEClientName.DataSource = dtCt;
                drpEClientName.DataValueField = "Client_ID";
                drpEClientName.DataTextField = "Client_Name";
                drpEClientName.DataBind();

                //-------------Bind Client Manager Grop Users------------------
                string qryUser = " Select Br_ID, Br_Name From tbl_BranchMaster  where Com_id='" + dt_login_details.Rows[0]["Com_id"].ToString() + "' order by Br_Name ";
                DataTable dtUser = new DataTable();
                dtUser = CommonFunctions.fetchdata(qryUser);

                drpEStoreName.DataSource = dtUser;
                drpEStoreName.DataValueField = "Br_ID";
                drpEStoreName.DataTextField = "Br_Name";
                drpEStoreName.DataBind();
                drpEStoreName.Items.Insert(0, new ListItem("--Select Store Name--", "0"));


                pnlMain.Attributes.Add("style", "display:none;");
                pnlAddDiv.Attributes.Add("style", "display:block;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
                
              

            
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

            }
            catch (Exception ex)
            {

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void btnAddStore_Click(object sender, EventArgs e)
        {
            //----------Save Client Application User Details-----------------
            try
            {

                if (drpEStoreName.SelectedIndex != 0)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_ClientStore";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Com1_ID", drpECompanyName.SelectedValue.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Client_ID", drpEClientName.SelectedValue.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Br_ID", drpEStoreName.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Br_Name", drpEStoreName.SelectedItem.Text.ToString());
                    cmd.Parameters.AddWithValue("@Com_ID", dt_login_details.Rows[0]["Com_id"].ToString());
                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "AddClientStore");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    String R = output.Value.ToString();
                    con.Close();



                    if (R != String.Empty)
                    {

                        Response.Redirect("frmClientStore.aspx");
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