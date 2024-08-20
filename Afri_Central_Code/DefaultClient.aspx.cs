using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static DefaultCompany;

namespace Afri_Central_Code
{
    public partial class DefaultClient : System.Web.UI.Page
    {
        SqlConnection cn1 = new SqlConnection(CommonFunctions.connection.ToString());
        DataTable dt_login_details = new DataTable();

        public string LabelMessage
        {
            get { return this.lblClientID.Text;  }
            set { this.lblClientID.Text = value; }
        }

        public string LabelMessage1
        {
            

            get { return this.lblZoneCode.Text; }
            set { this.lblZoneCode.Text = value; }
        }

        // Label LabelMessage = null;



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

                //LabelMessage = (Label)this.Page.Master.FindControl("lblmsg");

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
                    qry = "  Select Com_Name,  GroupName, client_Id, Client_Name  from vw_ClientUserDetails  where GroupID = " + dt_login_details.Rows[0]["GroupID"].ToString() + " and " +
                            " Com_id = '" + dt_login_details.Rows[0]["Com_id"].ToString() + "'";

                    DataTable dtz = new DataTable();
                    dtz = CommonFunctions.fetchdata(qry);

                    if (dtz.Rows.Count > 0)
                    {
                        lblZoneName.Text = dtz.Rows[0]["Com_Name"].ToString();
                        lblZoneCode.Text = dt_login_details.Rows[0]["Com_id"].ToString();
                        Label lblZoneVal = (Label)Page.Master.FindControl("lblZoneCode");
                        lblZoneVal.Text = lblZoneCode.Text.ToString();
                        lblClientName.Text = dtz.Rows[0]["Client_Name"].ToString();
                        lblClientID.Text = dtz.Rows[0]["client_Id"].ToString();
                    }
                    else
                        lblZoneName.Text = "";


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
        public static List<GetDataStore> GetStoreClientSalesChart(string ZoneCode, String ClientID)
        {


            using (SqlConnection con = new SqlConnection(CommonFunctions.connection.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                string Qty = " Select count(distinct(TicketNo)) as PrintCount, sum(Quantity) as Quantity,  CAST( Sum(TotalAmount) as int )as TotalAmount, StoreID, BranchName" +
                            " FROM tbl_ItemTransaction where  isReceipt = 1 and isPayment = 1 and CompID = '" + ZoneCode.ToString() + "' " +
                            "  and StoreID in (Select Br_ID from tbl_ClientStoreRelation where Client_ID='" + ClientID.ToString() + "' and Com_ID='" + ZoneCode.ToString() + "') group by StoreID, BranchName";


                dt = CommonFunctions.fetchdata(Qty);
                //da.Fill(dt);

                List<GetDataStore> dataList = new List<GetDataStore>();

                foreach (DataRow dtrow in dt.Rows)
                {
                    GetDataStore detail = new GetDataStore();
                    detail.StoreName = dtrow[4].ToString();
                    detail.Amount = Convert.ToInt32(dtrow[2].ToString());

                    dataList.Add(detail);
                }
                return dataList;

            }
        }




        public class GetDataStore
        {
            public string StoreName { get; set; }
            public int Amount { get; set; }

        }
    }
}