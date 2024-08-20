using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class DefaultCompany : System.Web.UI.Page
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
            //lblloginmsg.Attributes.Add("class", "active");
            //lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
            //lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
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

            string qry = "";

            if (dt_login_details.Rows.Count > 0)
            {
                qry = " Select Com_Name,  GroupName from vw_UserDetails where GroupID = " + dt_login_details.Rows[0]["GroupID"].ToString() + " and " +
                        " Com_id = '" + dt_login_details.Rows[0]["Com_id"].ToString() + "'";

                DataTable dtz = new DataTable();
                dtz = CommonFunctions.fetchdata(qry);

                if (dtz.Rows.Count > 0)
                {
                    lblZoneName.Text = dtz.Rows[0]["Com_Name"].ToString();
                    lblZoneCode.Text = dt_login_details.Rows[0]["Com_id"].ToString();
                    Label lblZoneVal = (Label)Page.Master.FindControl("lblZoneCode");
                    lblZoneVal.Text = lblZoneCode.Text.ToString();
                }
                else
                    lblZoneName.Text = "";




                //---------Total Sales Count & Total Item Sales-------------

                qry = " Select count(distinct(TicketNo)) as PrintCount,  sum(Quantity) as Quantity, Sum(TotalAmount) as TotalAmount, sum((isnull(SalesPrice,0) - isnull(CostPrice,0) ) -isnull(DiscountAmount,0) ) as  PLAmount FROM vw_StoreSalesSummary where  isReceipt=1 and isPayment=1 and " +
                "  CompID = '" + dt_login_details.Rows[0]["Com_ID"].ToString() + "' ";



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


            }

        }

        catch (Exception ex)
        {
            //div_msg.Attributes.Add("style", "display:block");
            //lblmsg.Style.Add("fore-color", "red");
            //lblmsg.InnerText = "Something Wrong. Please try again.";
        }
    }



    [System.Web.Services.WebMethod]
    public static List<GetData1> GetData(string ZoneCode)
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
                cmd.Parameters.AddWithValue("@Flag", "ZonePieChart");

                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            else
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
    public static List<GetData2> GetLineChart(string ZoneCode)
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



    [System.Web.Services.WebMethod]
    public static List<GetData3> GetStoreSalesChart(string ZoneCode)
    {


        using (SqlConnection con = new SqlConnection(CommonFunctions.connection.ToString()))
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string Qty = " Select count(distinct(TicketNo)) as PrintCount, sum(Quantity) as Quantity,  CAST( Sum(TotalAmount) as int )as TotalAmount, StoreID, BranchName" +
                        " FROM tbl_ItemTransaction where  isReceipt = 1 and isPayment = 1 and CompID = '" + ZoneCode.ToString() + "' group by StoreID, BranchName";


            dt = CommonFunctions.fetchdata(Qty);
            //da.Fill(dt);

            List<GetData3> dataList = new List<GetData3>();

            foreach (DataRow dtrow in dt.Rows)
            {
                GetData3 detail = new GetData3();
                detail.StoreName = dtrow[4].ToString();
                detail.Amount = Convert.ToInt32(dtrow[2].ToString());

                dataList.Add(detail);
            }
            return dataList;

        }
    }





    public class GetData3
    {
        public string StoreName { get; set; }
        public int Amount { get; set; }

    }



    //--GetSalesData
    [System.Web.Services.WebMethod]
    public static List<GetDailySales> GetSroreSalesData(string ZoneCode)
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
                cmd.Parameters.AddWithValue("@Flag", "DailySales");
                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SP_RP_GetChartData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ZoneCode", ZoneCode.ToString());
                cmd.Parameters.AddWithValue("@Flag", "DailySales");
                da.SelectCommand = cmd;
                da.Fill(dt);
            }


            List<GetDailySales> dataList = new List<GetDailySales>();

            foreach (DataRow dtrow in dt.Rows)
            {
                GetDailySales detail = new GetDailySales();
                             
               
                //detail.month = Convert.ToInt32(dtrow[2].ToString());
                detail.Daily = ( dtrow[0].ToString() + ", " + dtrow[2].ToString() +", " + dtrow[3].ToString() );
                detail.Amount = Convert.ToInt32( Convert.ToDecimal(dtrow[6].ToString()) ) ;
                dataList.Add(detail);
            }
            return dataList;

        }
    }


    public class GetDailySales
    {
        
        public string Daily { get; set; }    

        public int Amount { get; set; }

    }

}
