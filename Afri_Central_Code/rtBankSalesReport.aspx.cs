using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RPermit_Admin
{
    public partial class rtBankSalesReport : System.Web.UI.Page
    {
        SqlConnection cn1 = new SqlConnection(CommonFunctions.connection.ToString());
        DataTable dt_login_details = new DataTable();


        protected DataTable objDs = new DataTable();
        protected DataTable objDsRep = new DataTable();

        protected DataTable objSDs = new DataTable();
        protected DataTable objSDsRep = new DataTable();

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
             
                //-------------Bind Nationality----------------
                String qry1 = " Select Nationality, Ncode  From CountryMaster   order by Nationality ";
                DataTable dt1 = new DataTable();
                dt1 = CommonFunctions.fetchdata(qry1);

                drpNationality.DataSource = dt1;
                drpNationality.DataValueField = "Ncode";
                drpNationality.DataTextField = "Nationality";
                drpNationality.DataBind();
                drpNationality.Items.Insert(0, new ListItem("-Nationality-", "0"));


                //-------------Bind Location or Zone Master------------------
                String qry2 = "";
                DataTable dt2 = new DataTable();
                //------------Zonename display on drpLocation----------------
                if (dt_login_details.Rows[0]["GroupID"].ToString() == "101" || dt_login_details.Rows[0]["GroupID"].ToString() == "108")
                {
                    qry2 = " SELECT  zoneID, ZoneName, isActive  FROM tbl_ZoneMaster where IsActive=1  order by  ZoneName ";
                    dt2 = CommonFunctions.fetchdata(qry2);

                    drpLocation.DataSource = dt2;
                    drpLocation.DataValueField = "ZoneID";
                    drpLocation.DataTextField = "ZoneName";
                    drpLocation.DataBind();
                    drpLocation.Items.Insert(0, new ListItem("-Location-", "0"));
                }
                else
                {
                    qry2 = " SELECT  zoneID, ZoneName, isActive  FROM tbl_ZoneMaster where IsActive=1 and zoneID=" + dt_login_details.Rows[0]["ZoneCode"].ToString() + "   order by  ZoneName ";
                    dt2 = CommonFunctions.fetchdata(qry2);

                    drpLocation.DataSource = dt2;
                    drpLocation.DataValueField = "ZoneID";
                    drpLocation.DataTextField = "ZoneName";
                    drpLocation.DataBind();
                    drpLocation.Items.Insert(0, new ListItem("-Location-", "0"));

                }
             
              

             
            }
            catch (Exception ex)
            {
                DivMsg.Attributes.Add("class", "active");
                DivMsg.Attributes["style"] = "color:red; font-weight:bold;";
                DivMsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
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
                    pnlSummary.Visible = false;
                    pnlSummary.Attributes.Remove("class");

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

                        ////DateTime dtF = Convert.ToDateTime(dt1);
                        //string F1 = dtF.ToString("yyyy/MM/dd");

                        //DateTime dtS = Convert.ToDateTime(dt2);
                        //string F2 = dtS.ToString("yyyy/MM/dd");

                        //------------Filter Conditions-----------------

                        string condition = "", Case = "", Heading = "";
                        //drp_status.Tex
                        if (drpReportType.SelectedValue == "Summary")
                            Case = "1";
                        else if (drpReportType.SelectedValue == "Details")
                            Case = "2";

                        if (drpLocation.SelectedIndex == 0)
                        {
                            condition = " and isnull(ZoneCode,0)   != " + drpLocation.SelectedValue;
                            Heading = " , ZoneName ";
                        }
                        else if (drpLocation.SelectedValue != "0")
                        {
                            condition = " and ZoneCode = " + drpLocation.SelectedValue.ToString();
                            Heading = " , ZoneName ";
                        }

                        if (drpNationality.SelectedIndex == 0)
                        {
                            condition = condition + " and isnull(NCode,0)  !=" + drpNationality.SelectedValue;
                            Heading = Heading + " , Nationality ";
                        }
                        else if (drpNationality.SelectedValue != "0")
                        {
                            condition = condition + " and NCode =" + drpNationality.SelectedValue;
                            Heading = Heading + " , Nationality ";
                        }
                        if (Case == "1")
                        {
                            string qry = "";
                            //--------------Central Report for Find Bank Records details in database -----------------------
                            if (dt_login_details.Rows[0]["GroupID"].ToString() == "108" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
                            {
                              
                                    qry = " Select year(CreatedOn) as Year, (SELECT DateName(mm,DATEADD(mm,Month(CreatedOn),-1)) as [MonthName]) as [MonthName], " +
                                    " (SELECT DateName(dd, DATEADD(dd, day(CreatedOn), -1)) as [Date]) as [Date] ,  " +
                                    " Count(*) as Count, sum(Payment_Cost) AS Amount "+ Heading + " " +
                                    " from vw_BankReport where ( year(CreatedOn)  between  " + YearF + " and " + YearT + " )  and  ( month(CreatedOn) between  " + MonthF + " and " + MonthT + " ) " + condition + " " +
                                    " group by year(CreatedOn) , Month(CreatedOn), day(CreatedOn)  " + Heading + " " +
                                    " order by year(CreatedOn) , Month(CreatedOn), day(CreatedOn)";
                               
                            }
                         

                            DataTable dtR = new DataTable();
                            dtR = CommonFunctions.fetchdata(qry);
                            objSDs = dtR;

                            if (dtR.Rows.Count > 0)
                            {
                                Session["BankReportS"] = dtR;

                                lbl_total.Text = dtR.Rows.Count.ToString();
                                lblSDateF.Text = dt1.ToString();
                                lblSDateD.Text = dt2.ToString();
                                
                                pnlMain.Attributes.Add("style", "display:block;");
                               
                                pnlSummary.Visible = true;
                                pnlSummary.Attributes.Add("class", "active");

                                Session["BankReportD"] = null;

                            }
                            else
                            {

                                lbl_total.Text = dtR.Rows.Count.ToString();
                                lblSDateF.Text = dt1.ToString();
                                lblSDateD.Text = dt2.ToString();
                            }
                     
                        }

                        if (Case == "2")
                        {
                            string qry = "";
                            //--------------Central Report for Find Bank Records details in database -----------------------
                            if (dt_login_details.Rows[0]["GroupID"].ToString() == "108" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
                            {
                                qry = "  Select Applicant_Id, Title + ' ' + FirstName + ' ' + LastName as AppName, Gender, Passport_No, CountryName, ZoneName,  PaidOn, CurrencyType, Payment_Cost FROM vw_BankReport where IsPaid =1 and PaidOn  between '" + dateF.ToString() + "  00:00:00.000' and '" + dateT.ToString() + " 23:59:59.000' " + condition  + "  order by year(PaidOn) desc, month(PaidOn) desc, day(PaidOn) desc";
                      
                            }
                            
                            DataTable dtR = new DataTable();
                            dtR = CommonFunctions.fetchdata(qry);
                            objDs = dtR;

                            if (dtR.Rows.Count > 0)
                            {
                                Session["BankReportD"] = dtR;
                                lbl_total.Text = dtR.Rows.Count.ToString();
                                lblFDate.Text = dt1.ToString();
                                lblTDate.Text = dt2.ToString();

                              
                                pnlMain.Attributes.Add("style", "display:block;");                          

                                pnlDetails.Visible = true;
                                pnlDetails.Attributes.Add("class", "active");
                                Session["BankReportS"] = null;
                            }
                            else
                            {

                                lbl_total.Text = dtR.Rows.Count.ToString();
                                lblFDate.Text = dt1.ToString();
                                lblTDate.Text = dt2.ToString();
                            }
                     
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
         

            if (Session["BankReportD"] != null)
            {
                lblFDateR.Text = lblFDate.Text;
                lblTDateR.Text = lblTDate.Text;
                objDs = (DataTable)Session["BankReportD"];
                objDsRep = (DataTable)Session["BankReportD"];
             
                ScriptManager.RegisterStartupScript(this, GetType(), "key", "PrintContent();", true);
            
            }


            if (Session["BankReportS"] != null)
            {
                lblSDateFRep.Text = lblSDateF.Text;
                lblSDateDRep.Text = lblSDateD.Text;
                objSDs = (DataTable)Session["BankReportS"];
                objSDsRep = (DataTable)Session["BankReportS"];
                ScriptManager.RegisterStartupScript(this, GetType(), "key", "PrintContentSummary();", true);
            
            }

        }

    }
}