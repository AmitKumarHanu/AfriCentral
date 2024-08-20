using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : Page
{
    SqlConnection cn1 = new SqlConnection(CommonFunctions.connection.ToString());
    DataTable dt_login_details = new DataTable();
    Label LabelMessage = null;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginId"] == null)
            {
                Server.Transfer("Login.aspx", false);
            }


            dt_login_details = (DataTable)Session["LoginDetails"];

            if (!IsPostBack)
            {
                BindFetch();

            }

            LabelMessage = (Label)this.Page.Master.FindControl("lblmsg");

        }

        catch (Exception ex)
        {
            //div_msg.Attributes.Add("style", "display:block");
            //lblmsg.Style.Add("fore-color", "red");
            //lblmsg.InnerText = "Something Wrong. Please try again.";
        }
    }


    public void BindFetch()
    {

        try
        {
            string Symbole = "";
            string QytCurrency = "Select Currency from tbl_CompanyMaster where Com_ID = '" + dt_login_details.Rows[0]["Com_ID"].ToString() + "'";
            DataTable dtCurrency = new DataTable();
            dtCurrency = CommonFunctions.fetchdata(QytCurrency);

            if (dtCurrency.Rows.Count > 0)
            {
                Symbole = dtCurrency.Rows[0]["Currency"].ToString();
            }

            String qry = "";
            //--------Zone Name----------------



            if (dt_login_details.Rows[0]["GroupCode"].ToString() == "ADMN")
            {
                lblZoneName.Text = " Central Application ";
                Label lblZoneVal = (Label)Page.Master.FindControl("lblZoneCode");
                lblZoneVal.Text = "0";
            }


            //---------Total Sales Count & Total Item Sales-------------

            qry = " Select count(distinct(TicketNo)) as PrintCount,  sum(Quantity) as Quantity, Sum(TotalAmount) as TotalAmount, sum((isnull(SalesPrice,0) - isnull(CostPrice,0) ) -isnull(DiscountAmount,0) ) as  PLAmount FROM vw_StoreSalesSummary where  isReceipt=1 and isPayment=1 " ;

            DataTable dt = new DataTable();
            dt = CommonFunctions.fetchdata(qry);


            if (dt.Rows.Count > 0)
            {
                lblReeciptCount.Text = dt.Rows[0]["PrintCount"].ToString();
                lblItemSales.Text = dt.Rows[0]["Quantity"].ToString();
                lblTotalSales.Text = dt.Rows[0]["TotalAmount"].ToString() + " " + Symbole.ToString();
                lblProfit.Text = dt.Rows[0]["PLAmount"].ToString() + " " + Symbole.ToString();
            }
            else
            {
                lblReeciptCount.Text = "0";
                lblItemSales.Text = "0";
                lblTotalSales.Text = "0" + " " + Symbole.ToString();
                lblProfit.Text = "0" + " " + Symbole.ToString();
            }




            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "107" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
            //{
            //    qry = " Select ZoneName, zoneID, GroupID, GroupName from vw_UserDetails where GroupID=   " + dt_login_details.Rows[0]["GroupID"].ToString() + " and " +
            //        " ZoneId = " + dt_login_details.Rows[0]["ZoneCode"].ToString() + "";

            //    DataTable dtz = new DataTable();
            //    dtz = CommonFunctions.fetchdata(qry);

            //    if (dtz.Rows.Count > 0)
            //    {
            //        lblZoneName.Text = dtz.Rows[0]["ZoneName"].ToString();
            //        lblZoneCode.Text = dtz.Rows[0]["zoneID"].ToString();
            //        Label lblZoneVal = (Label)Page.Master.FindControl("lblZoneCode");
            //        lblZoneVal.Text = lblZoneCode.Text.ToString();
            //    }
            //    else
            //        lblZoneName.Text = dtz.Rows.Count.ToString();
            //}
            ////---------Total Registration-------------
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "101" || dt_login_details.Rows[0]["GroupID"].ToString() == "108")
            //{
            //    qry = " Select count(*) as RegCount From tbl_ApplicantRegistration where iscompleted=1 and IsPaid=1 ";
            //}
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "107" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
            //{
            //    qry = "   Select count(*) as RegCount From tbl_ApplicantRegistration where AppointmentZone = " + dt_login_details.Rows[0]["ZoneCode"].ToString() + " and iscompleted = 1 and IsPaid = 1    ";
            //}
            //DataTable dt = new DataTable();
            //dt = CommonFunctions.fetchdata(qry);

            //if (dt.Rows.Count > 0)
            //    lblRegistionCount.Text = dt.Rows[0]["RegCount"].ToString();
            //else
            //    lblRegistionCount.Text = dt.Rows.Count.ToString();

            ////---------Total Payment-------------
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "101" || dt_login_details.Rows[0]["GroupID"].ToString() == "108")
            //{
            //    qry = "  Select count(*) as PaidCount, sum(TotalAmount) as Payment_Cost  From vw_TotalPayment where  IsPaid=1 ";
            //}
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "107" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
            //{
            //    qry = " Select count(*) as PaidCount, sum(TotalAmount) as Payment_Cost  From vw_TotalPayment where  IsPaid=1     and AppointmentZone = " + dt_login_details.Rows[0]["ZoneCode"].ToString() + "";
            //}
            //dt = null;
            //dt = CommonFunctions.fetchdata(qry);
            //// lblSalesCount.Text = "L$ " + Convert.ToInt32(Convert.ToDecimal(dt.Rows[0]["Payment_Cost"].ToString())) + " LRD";
            //if (dt.Rows.Count > 0)
            //     lblSalesCount.Text = "L$ " + Convert.ToInt32(Convert.ToDecimal(dt.Rows[0]["Payment_Cost"].ToString())) + " LRD";
            //else
            //     lblSalesCount.Text = dt.Rows.Count.ToString();

            ////---------Total Print Permit-------------
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "101" || dt_login_details.Rows[0]["GroupID"].ToString() == "108")
            //{
            //    qry = " Select count(*) as PrintCount from tbl_Approval where isnull(isLevel1,0) = 1 and isnull(isLevel2,0)=1 and isnull(isLevel3,0)=1 and isnull(isLevel4,0)=1   ";
            //}
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "107" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
            //{
            //    qry = " Select count(*) as PrintCount from tbl_Approval where isnull(isLevel1, 0) = 1 and isnull(isLevel2,0)= 1 and isnull(isLevel3,0)= 1 and isnull(isLevel4,0)= 1   " +
            //          " and ZoneCode = " + dt_login_details.Rows[0]["ZoneCode"].ToString() + "";
            //}
            //dt = null;
            //dt = CommonFunctions.fetchdata(qry);

            //if (dt.Rows.Count > 0)
            //    lblPermitCount.Text = dt.Rows[0]["PrintCount"].ToString();
            //else
            //    lblPermitCount.Text = dt.Rows.Count.ToString();

            //-----------Last 10 Record Display-------------


            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "101" || dt_login_details.Rows[0]["GroupID"].ToString() == "108")
            //{
            //    qry = " Select Applicant_Id, FirstName, LastName, Passport_No, RP_Name, CountryName  From vw_AppointmentConfrim   where isnull(IsCompleted,0)=1    and isnull(IsTimeSlotApproved ,0)=0 order by Year(CreatedOn),month(CreatedOn),day(CreatedOn) desc ";
            //}
            //if (dt_login_details.Rows[0]["GroupID"].ToString() == "107" || dt_login_details.Rows[0]["GroupID"].ToString() == "109")
            //{
            //    qry = " Select Applicant_Id, FirstName, LastName, Passport_No, RP_Name, CountryName From vw_AppointmentConfrim where isnull(IsCompleted, 0) = 1    and isnull(IsTimeSlotApproved ,0)= 0     and ZoneCode = " + dt_login_details.Rows[0]["ZoneCode"].ToString() + "  order by Year(CreatedOn),month(CreatedOn),day(CreatedOn) desc ";
            //}
            //dt = null;
            //dt = CommonFunctions.fetchdata(qry);

            //if (dt.Rows.Count > 0)
            //{

            //    lbl_total.Text = "Total : " + dt.Rows.Count.ToString();
            //    grdLevel1.DataSource = dt;
            //    grdLevel1.DataBind();
            //}
            //else
            //{

            //    lbl_total.Text = dt.Rows.Count.ToString();
            //    grdLevel1.DataSource = null;
            //    grdLevel1.DataBind();
            //}

        }

        catch (Exception ex)
        {
            //div_msg.Attributes.Add("style", "display:block");
            //lblmsg.Style.Add("fore-color", "red");
            //lblmsg.InnerText = "Something Wrong. Please try again.";
        }
    }



    [System.Web.Services.WebMethod]
    public static List<GetData1> GetData(int ZoneCode)
    {
        using (SqlConnection con = new SqlConnection(CommonFunctions.connection.ToString()))
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (ZoneCode.ToString() !="0")
            {
                SqlCommand cmd = new SqlCommand("SP_RP_GetChartData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ZoneCode", ZoneCode.ToString());
                cmd.Parameters.AddWithValue("@Flag", "ZonePieChart");

                da.SelectCommand = cmd;
                da.Fill(dt);
            } else
            {
                SqlCommand cmd = new SqlCommand("SP_RP_GetChartData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ZoneCode", ZoneCode.ToString());
                cmd.Parameters.AddWithValue("@Flag", "PieChart");

                da.SelectCommand = cmd;        
                da.Fill(dt);

            }


            List<GetData1> dataList = new List<GetData1>();

            foreach (DataRow dtrow in dt.Rows)
            {
                GetData1 detail = new GetData1();
                detail.CategoryName = dtrow[0].ToString();
                detail.InvoiceNo = Convert.ToInt32(dtrow[1].ToString());

                dataList.Add(detail);
            }
            return dataList;
        }
    }
    


    public class GetData1
    {
        public string CategoryName { get; set; }
        public int InvoiceNo { get; set; }

    }




    [System.Web.Services.WebMethod]
    public static List<GetData2> GetLineChart(int ZoneCode)
    {
        using (SqlConnection con = new SqlConnection(CommonFunctions.connection.ToString()))
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            if (ZoneCode.ToString() != "0")
            {

                SqlCommand cmd = new SqlCommand("SP_RP_GetChartData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ZoneCode", ZoneCode.ToString());
                cmd.Parameters.AddWithValue("@Flag", "LineChart");
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SP_RP_GetChartData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ZoneCode", ZoneCode.ToString());
                cmd.Parameters.AddWithValue("@Flag", "LineChart");
                da.SelectCommand = cmd;
                da.Fill(dt);
            }


            List<GetData2> dataList = new List<GetData2>();

            foreach (DataRow dtrow in dt.Rows)
            {
                GetData2 detail = new GetData2();
                detail.Year = Convert.ToInt32(dtrow[0]);
                detail.Registered = Convert.ToInt32(dtrow[1]);
                detail.Captured = Convert.ToInt32(dtrow[2]);
                detail.Paid = Convert.ToInt32(dtrow[3]);
                detail.Overstay = Convert.ToInt32(dtrow[4]);
                dataList.Add(detail);
            }
            return dataList;
        }
    }



    public class GetData2
    {
        public int Year { get; set; }

        public int Registered { get; set; }

        public int Captured { get; set; }

        public int Paid { get; set; }

        public int Overstay { get; set; }

    }


   
}
