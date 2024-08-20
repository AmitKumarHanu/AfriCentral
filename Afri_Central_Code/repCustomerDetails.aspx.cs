using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.ExceptionServices;

namespace Afri_Central_Code
{
    public partial class repCustomerDetails : System.Web.UI.Page
    {
        SqlConnection cn1 = new SqlConnection(CommonFunctions.connection.ToString());
        DataTable dt_login_details = new DataTable();


        protected DataTable objDs = new DataTable();
        protected DataTable objDsRep = new DataTable();
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

                lblfilter.Text = "";
                lblRFilter.Text = "";
                DivMsg.InnerHtml = "";
            }

            catch (Exception ex)
            {
                DivMsg.Attributes.Add("class", "active");
                DivMsg.Attributes["style"] = "color:red; font-weight:bold;";
                DivMsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }


        public void bindgrid()
        {
            //----------Bind Data Grid-----------------
            try
            {
                //-------------- Store Details------------------
                String qry = " Select Upper(Br_Name) as Br_Name, Br_ID from tbl_BranchMaster where Com_ID='" + dt_login_details.Rows[0]["Com_ID"].ToString() + "' order by Br_Name";
                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                drpStore.DataSource = dt;
                drpStore.DataValueField = "Br_ID";
                drpStore.DataTextField = "Br_Name";
                drpStore.DataBind();
                drpStore.Items.Insert(0, new ListItem("--Store Name-", "0"));
            }
            catch (Exception ex)
            {

            }
        }
        protected void BtnSearchOpt(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    pnlDetails.Visible = false;
                    pnlDetails.Attributes.Remove("class");

                    var val = hfProduct.Value.ToString();
                    //--------------Split Date----------
                    if (val.ToString() != String.Empty)
                    {
                        var dt = val.ToString();
                        var da = dt.Split('-');
                        var dt1 = da[0].Substring(0, da[0].Length - 1).Trim(); ;
                        var dt2 = da[1].Substring(0, da[1].Length - 1).Trim(); ;

                        //-----------Convert date format------
                        string joinstring = "-";

                        string[] tempsplitF = dt1.Split('/');
                        string dateF = tempsplitF[2] + joinstring + tempsplitF[1] + joinstring + tempsplitF[0];
                        string YearF = tempsplitF[2];
                        string MonthF = tempsplitF[1];

                        string[] tempsplitT = dt2.Split('/');
                        string dateT = tempsplitT[2] + joinstring + tempsplitT[1] + joinstring + tempsplitT[0];
                        string YearT = tempsplitT[2];
                        string MonthT = tempsplitT[1];

                        //DateTime dtF = Convert.ToDateTime(dt1);
                        //string F1 = dtF.ToString("yyyy/MM/dd");
                        string F1 = dateF;

                        //DateTime dtS = Convert.ToDateTime(dt2);
                        //string F2 = dtS.ToString("yyyy/MM/dd");
                        string F2 = dateT;

                        //------------Filter Conditions-----------------

                        string Heading = "";



                        lblfilter.Text = Heading.ToString().ToUpper();
                        lblRFilter.Text = Heading.ToString().ToUpper();

                        //------------Filter Conditions-----------------

                        string condition = "" ;

                        if (drpStore.SelectedIndex != 0)
                        {
                            condition = condition + " and  StoreID  ='" + drpStore.SelectedValue + "' ";
                            //Heading = Heading + " STORE NAME : " + drpStore.SelectedItem + " ";
                        }


                        // string qry = " Select ItemName, ItemSpecification, CategoryName, Brand,   SupplierName,  CostPrice, Quantity, (Quantity* CostPrice)  AS Amount,  convert(varchar(10),CreateOn,103) as CreateOn   from vw_ItemStock where Com_ID='" + dt_login_details.Rows[0]["Com_ID"].ToString() + " '" + condition + "  order by  CategoryName, Brand  ";

                        string qry = " Select distinct(CustomerEmail), CustomerName, CustomerEmail, CustomerMobile, BranchName from tbl_itemTransaction where TicketDate  between '" + dateF.ToString() + "  00:00:00.000' and '" + dateT.ToString() + " 23:59:59.000' " + condition + " group by  BranchName, CustomerMobile, CustomerName, CustomerEmail ";


                        DataTable dtR = new DataTable();
                        dtR = CommonFunctions.fetchdata(qry);
                        objDs = dtR;

                        if (dtR.Rows.Count > 0)
                        {
                            Session["CustomerReportS"] = dtR;
                            lbl_total.Text = dtR.Rows.Count.ToString();
                            lblFDate.Text = dt1.ToString();
                            lblTDate.Text = dt2.ToString();


                            pnlMain.Attributes.Add("style", "display:block;");
                            pnlDetails.Visible = true;
                            pnlDetails.Attributes.Add("class", "active");

                        }
                        else
                        {

                            lbl_total.Text = dtR.Rows.Count.ToString();

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                DivMsg.Attributes.Add("class", "active");
                DivMsg.Attributes["style"] = "color:red; font-weight:bold;";
                DivMsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please check database connection details !" + " </h4>";
            }

        }


        protected void BtnPrinter(object sender, EventArgs e)
        {

            if (Session["CustomerReportS"] != null)
            {

                objDs = (DataTable)Session["CustomerReportS"];
                objDsRep = (DataTable)Session["CustomerReportS"];

                lblRFDate.Text = lblFDate.Text;
                lblRTDate.Text = lblTDate.Text;

                ScriptManager.RegisterStartupScript(this, GetType(), "key", "PrintContent();", true);
            }

        }




        public void ExportToExcel(ref string html, string fileName)
        {
            Label LabelMessage = (Label)this.Page.Master.FindControl("lblmsg");
            string Message = string.Empty;

            try
            {
                html = html.Replace("&gt;", ">");
                html = html.Replace("&lt;", "<");
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
                HttpContext.Current.Response.ContentType = "application/xls";
                HttpContext.Current.Response.Write(html);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Label LabelMessage = (Label)this.Page.Master.FindControl("lblmsg");
            string Message = string.Empty;


            try
            {

                if (Session["CustomerReportS"] != null)
                {
                    //lblSFDateR.Text = lblFDate.Text;
                    //lblSTDateR.Text = lblTDate.Text;
                    objDs = (DataTable)Session["CustomerReportS"];
                    objDsRep = (DataTable)Session["CustomerReportS"];

                    string html = HdnValue.Value;
                    ExportToExcel(ref html, "Customer_Details_Reports");
                }


            }

            catch (Exception ex)
            {

            }
        }
    }
}