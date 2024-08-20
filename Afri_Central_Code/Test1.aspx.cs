using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Afri_Central_Code
{
    public partial class Test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetCompanyName(string pre)
        {
            //List<string> allCompanyName = new List<string>();
            //using (AfriSmartEntities1 dc = new AfriSmartEntities1())
            //{
            //    allCompanyName = (from a in dc.Demo_TopCompanies
            //                      where a.CompanyName.StartsWith(pre)
            //                      select a.CompanyName).ToList();
            //}
            //return allCompanyName;

            //List<string> itemResult = new List<string>();

            List<string> allCompanyName = new List<string>();


            string sqlStatment = " Select  CompanyName From Demo_TopCompanies where CompanyName  LIKE '" + pre + "%' order by CompanyName asc";

            using (SqlConnection con = new SqlConnection(CommonFunctions.connection))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatment, con))
                {
                    cmd.Connection.Open();
                    System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        allCompanyName.Add(reader["CompanyName"].ToString());
                    }


                    reader.Close();
                    cmd.Connection.Close();
                    // return itemResult;
                }
            }

            return allCompanyName;

        }
    }
}