using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Afri_Central_Code
{
    public partial class frmItemMaster : System.Web.UI.Page
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
                txtSearch.Focus();

            }

            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }


        public void bindgrid()
        {
            //----------Bind Data Grid-----------------
            try
            {
                //-------------Bind Supplier Master------------------
                string qrySt = " Select SId , SupplierName  from tbl_SupplierMaster where  Com_ID='" + dt_login_details.Rows[0]["Com_ID"].ToString() + "' and isActive=1 order by SupplierName asc ";
                DataTable dtSt = new DataTable();
                dtSt = CommonFunctions.fetchdata(qrySt);

                drpSupplierName.DataSource = dtSt;
                drpSupplierName.DataValueField = "SId";
                drpSupplierName.DataTextField = "SupplierName";
                drpSupplierName.DataBind();
                drpSupplierName.Items.Insert(0, new ListItem("--Select Supplier--", "0"));



                drpESupplierName.DataSource = dtSt;
                drpESupplierName.DataValueField = "SId";
                drpESupplierName.DataTextField = "SupplierName";
                drpESupplierName.DataBind();
                drpESupplierName.Items.Insert(0, new ListItem("--Select Supplier--", "0"));


                //-------------Bind Category Master------------------
                string qryCt = " Select CId , CategoryName  from tbl_ItemCategory where Com_ID='" + dt_login_details.Rows[0]["Com_ID"].ToString() + "' and isActive=1 order by CategoryName asc ";
                DataTable dtCt = new DataTable();
                dtCt = CommonFunctions.fetchdata(qryCt);

                drpCategoryName.DataSource = dtCt;
                drpCategoryName.DataValueField = "CId";
                drpCategoryName.DataTextField = "CategoryName";
                drpCategoryName.DataBind();
                drpCategoryName.Items.Insert(0, new ListItem("--Select Category--", "0"));


                drpECategoryName.DataSource = dtCt;
                drpECategoryName.DataValueField = "CId";
                drpECategoryName.DataTextField = "CategoryName";
                drpECategoryName.DataBind();
                drpECategoryName.Items.Insert(0, new ListItem("--Select Category--", "0"));

                //SELECT Name, Specification, BarCodeNo, Quantity, CostPrice, SalesPrice, Discount, isTax, Tax
                //, SupplierName, SupplierCompany, CategoryName, Image, UserName, CreateON
                //FROM AfriSmart.dbo.vw_ItemDetails

                string qry = "   Select ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPrice,SalesPrice,Discount " +
                   " ,DiscountAmount,SupplierCode,CategoryCode,CategoryName,Mfgdate,Expdate,Warranty,Image,UserName,CreateON, SupplierName " +
                   " from vw_ItemDetails where ComID='" + dt_login_details.Rows[0]["Com_Id"].ToString() + "' and isActive=1 order by ItemName asc  ";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCompanyDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grditemDetails.DataSource = dt;
                    grditemDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grditemDetails.DataSource = null;
                    grditemDetails.DataBind();
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
               
          

                pnlMain.Attributes.Add("style", "display:none;");
                pnlAddDiv.Attributes.Add("style", "display:block;");
                pnlViewDiv.Attributes.Add("style", "display:none;");
                pnlEdit.Attributes.Add("style", "display:none;");
                txtInvoiceNo.Focus();
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

                string qry = "  Select * From  vw_ItemDetails where   ComID='" + dt_login_details.Rows[0]["Com_Id"].ToString() + "' and ( ItemRegNo like '" + txtSearch.Value.Trim() + "%' or ItemName like '" + txtSearch.Value.Trim() + "%' or ItemSpecification like '" + txtSearch.Value.Trim() + "%' or BarCodeNo like '" + txtSearch.Value.Trim() + "%' or SupplierName like '" + txtSearch.Value.Trim() + "%' or CategoryName like '" + txtSearch.Value.Trim() + "%' ) and isActive=1 order by ItemName ";


                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {
                    Session["grdCompanyDetails"] = dt;
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grditemDetails.DataSource = dt;
                    grditemDetails.DataBind();
                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    grditemDetails.DataSource = null;
                    grditemDetails.DataBind();
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

        //--------------Add Option display div -----------------------
        protected void BtnEditOpt(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Value.Trim() != String.Empty)
                {


                    string qry = " Select * From  vw_ItemDetails where   ComID='" + dt_login_details.Rows[0]["Com_Id"].ToString() + "' and BarCodeNo like  '" + txtSearch.Value.Trim() + "%' or ItemRegNo  like '" + txtSearch.Value.Trim() + "%' and isActive=1  order by ItemName ";
                    DataTable dt = new DataTable();
                    dt = CommonFunctions.fetchdata(qry);

                    if (dt.Rows.Count > 0)
                    {

                        //----item Images---------------
                        if ((dt.Rows[0]["Image"].ToString()) != "")
                        {
                            byte[] imagem = (byte[])(dt.Rows[0]["Image"]);
                            string PROFILE_PIC = Convert.ToBase64String(imagem);
                            Session["img"] = imagem;
                            ImgEitem.Src = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                        }
                        else
                        {
                            string imgPath1 = "Content/assets/images/itemImage.jpg";
                            ImgEitem.Src = "~/" + imgPath1;
                        }


                        //ID,ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPrice,SalesPrice,Discount
                        //,DiscountAmount,CategoryId,CategoryName,Mfgdate,Expdate,Warranty,Image,UserName,UserType,CreateON,isComp,Com_Name
                        //,CompON,isStore,Br_ID,Br_Name,StoreON,SupplierCode,SupplierName

                        txtEItemRegNo.Text = dt.Rows[0]["ItemRegNo"].ToString();
                        txtEInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
                        txtEItemName.Text = dt.Rows[0]["ItemName"].ToString();
                        txtEItemSpecification.Text = dt.Rows[0]["ItemSpecification"].ToString();
                        txtEBrandName.Text = dt.Rows[0]["Brand"].ToString();
                        txtEBarCodeNo.Text = dt.Rows[0]["BarCodeNo"].ToString();                      
                        txtEItemQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                        txtECostPrice.Text = dt.Rows[0]["CostPrice"].ToString();
                        txtESalesPrice.Text = dt.Rows[0]["SalesPrice"].ToString();

                        txtEDiscount.Text = dt.Rows[0]["Discount"].ToString();
                        txtEDisAmount.Text = dt.Rows[0]["DiscountAmount"].ToString();
                        drpESupplierName.SelectedIndex = drpESupplierName.Items.IndexOf(drpESupplierName.Items.FindByValue(dt.Rows[0]["SID"].ToString()));
                        drpECategoryName.SelectedIndex = drpECategoryName.Items.IndexOf(drpECategoryName.Items.FindByValue(dt.Rows[0]["CID"].ToString()));

                        txtEWarranty.Text = dt.Rows[0]["Warranty"].ToString();
                        txtEMfgDate.Text = dt.Rows[0]["Mfgdate"].ToString();
                        txtEExpDate.Text = dt.Rows[0]["Expdate"].ToString();

                        pnlMain.Attributes.Add("style", "display:none;");
                        pnlAddDiv.Attributes.Add("style", "display:none;");
                        pnlViewDiv.Attributes.Add("style", "display:none;");
                        pnlEdit.Attributes.Add("style", "display:block;");

                        txtEInvoiceNo.Focus();
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


        protected void grditemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grditemDetails.PageIndex = e.NewPageIndex;
            this.bindgrid();
        }

        protected void grditemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = grditemDetails.Rows[rowIndex];
            }
            catch (Exception ex)
            {
                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void grditemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grditemDetails, "Select$" + e.Row.RowIndex);
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

        protected void grditemDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
        

                Label lblBarCodeNo = (Label)grditemDetails.SelectedRow.FindControl("lblIBarCodeNo");
                string qry = "  Select top 1 * From  vw_ItemDetails where BarCodeNo = '" + lblBarCodeNo.Text.Trim() + "' and isActive=1 order by ItemName";

                DataTable dt = new DataTable();
                dt = CommonFunctions.fetchdata(qry);

                if (dt.Rows.Count > 0)
                {

                    //ID,ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPrice,SalesPrice,Discount
                    //,DiscountAmount,CategoryId,CategoryName,Mfgdate,Expdate,Warranty,Image,UserName,UserType,CreateON,isComp,Com_Name
                    //,CompON,isStore,Br_ID,Br_Name,StoreON,SupplierCode,SupplierName

                    //----item Images---------------
                    if ((dt.Rows[0]["Image"].ToString()) != "")
                    {
                        byte[] imagem = (byte[])(dt.Rows[0]["Image"]);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);
                        Session["img"] = imagem;
                        ItemVImage.Src = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                    }
                    else
                    {
                        string imgPath1 = "Content/assets/images/itemImage.jpg";
                        ItemVImage.Src = "~/" + imgPath1;
                    }
                    lblInvoiceNo.Text = dt.Rows[0]["InvoiceNo"].ToString();
                    lblItemRegNo.Text = dt.Rows[0]["ItemRegNo"].ToString();
                    lblBrandName.Text = dt.Rows[0]["Brand"].ToString();
                    lblItemName.Text = dt.Rows[0]["ItemName"].ToString();
                    lblItemSpecification.Text = dt.Rows[0]["ItemSpecification"].ToString();
                    lbltxtBarCodeNo.Text = dt.Rows[0]["BarCodeNo"].ToString();
                    lblQuantity.Text = dt.Rows[0]["Quantity"].ToString();


                    lblCostPrice.Text = dt.Rows[0]["CostPrice"].ToString();
                    lblSalesPrice.Text = dt.Rows[0]["SalesPrice"].ToString();
                    lblDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    lblDisAmount.Text = dt.Rows[0]["DiscountAmount"].ToString();

                    lblSupplierName.Text = dt.Rows[0]["SupplierName"].ToString();
                    lblCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();

                    lblWarranty.Text = dt.Rows[0]["Warranty"].ToString();
                    lblMfgDate.Text = dt.Rows[0]["Mfgdate"].ToString();
                    lblExpDate.Text = dt.Rows[0]["Expdate"].ToString();



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
            //----------Edit Country Master Details-----------------
            try
            {

                if (txtEItemName.Text.ToString() != string.Empty && txtEItemQuantity.Text.ToString() != string.Empty)
                {
                    String R = "0", RI = "0";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_AddItem";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //ID,ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPrice,SalesPrice,Discount
                    //,DiscountAmount,CategoryId,CategoryName,Mfgdate,Expdate,Warranty,Image,UserName,UserType,CreateON,isComp,Com_Name
                    //,CompON,isStore,Br_ID,Br_Name,StoreON,SupplierCode,SupplierName

                    cmd.Parameters.AddWithValue("@ItemRegNo", txtEItemRegNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@InvoiceNo", txtEInvoiceNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemName", txtEItemName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemSpecification", txtEItemSpecification.Text.ToString());
                    cmd.Parameters.AddWithValue("@Brand", txtEBrandName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@BarCodeNo", txtEBarCodeNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Quantity", txtEItemQuantity.Text.Trim().ToString());

                    cmd.Parameters.AddWithValue("@CostPrice", txtECostPrice.Text.ToString());
                    cmd.Parameters.AddWithValue("@SalesPrice", txtESalesPrice.Text.ToString());

                    cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(txtEDiscount.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtEDisAmount.Text).ToString());

                    cmd.Parameters.AddWithValue("@SupplierCode", drpESupplierName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@CategoryCode", drpECategoryName.SelectedValue.Trim());

                    cmd.Parameters.AddWithValue("@Warranty", txtEWarranty.Text.Trim().ToString());


                    if (string.IsNullOrEmpty(Convert.ToString(txtEMfgDate.Text.Trim())))
                    {
                        txtEMfgDate.Text = Convert.ToString(hdnEMfgDate.Value);
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(txtEExpDate.Text.Trim())))
                    {
                        txtEExpDate.Text = Convert.ToString(hdnExpDate.Value);
                    }

                    cmd.Parameters.AddWithValue("@Mfgdate", DateTime.ParseExact(txtEMfgDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("@Expdate", DateTime.ParseExact(txtEExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "Eitem");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    R = output.Value.ToString();
                    con.Close();

                    if (R != String.Empty)
                    {
                        //-----------Update Item Image------------

                        if (FileUpload2.HasFile)
                        {
                            string filename = Path.GetFileName(FileUpload2.PostedFile.FileName);
                            string contentType = FileUpload2.PostedFile.ContentType;
                            using (Stream fs = FileUpload2.PostedFile.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(fs))
                                {
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                    SqlCommand cmdI = new SqlCommand();
                                    cmdI.Connection = con;
                                    cmdI.CommandText = "SP_AF_ItemImage";
                                    cmdI.CommandType = CommandType.StoredProcedure;
                                    cmdI.Parameters.AddWithValue("@ItemImage", bytes);
                                    cmdI.Parameters.AddWithValue("@BarCodeNo", txtEBarCodeNo.Text.Trim().ToString());
                                    cmdI.Parameters.AddWithValue("@InvoiceNo", txtEInvoiceNo.Text.Trim().ToString());

                                    cmdI.Parameters.AddWithValue("@Flag", "EitemImage");
                                    SqlParameter outputI = new SqlParameter("@Success", SqlDbType.Int);
                                    outputI.Direction = ParameterDirection.Output;
                                    cmdI.Parameters.Add(outputI);
                                    con.Open();
                                    cmdI.ExecuteNonQuery();
                                    RI = output.Value.ToString();
                                    con.Close();
                                }
                            }

                        }

                        if (R == "1" || RI == "1")
                        {
                            Response.Redirect("frmItemMaster.aspx");
                        }
                        else
                        {
                            pnlAddDiv.Visible = false;
                            pnlEdit.Visible = false;
                            pnlViewDiv.Visible = false;

                            lblloginmsg.Attributes.Add("class", "active");
                            lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                            lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Iteam details are already exist !" + " </h4>";
                        }
                    }
                    else
                    {
                        pnlAddDiv.Visible = false;
                        pnlEdit.Visible = false;
                        pnlViewDiv.Visible = false;

                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please fill details on all the require fields !" + " </h4>";
                    }

                }
            }
            catch (Exception ex)
            {
                pnlAddDiv.Visible = false;
                pnlEdit.Visible = false;
                pnlViewDiv.Visible = false;

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
                pnlAddDiv.Visible = false;
                pnlEdit.Visible = false;
                pnlViewDiv.Visible = false;

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void btnBackAdditem_Click(object sender, EventArgs e)
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
                pnlAddDiv.Visible = false;
                pnlEdit.Visible = false;
                pnlViewDiv.Visible = false;

                lblloginmsg.Attributes.Add("class", "active");
                lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Please find correct details !" + " </h4>";
            }
        }

        protected void btnSaveAddItem_Click(object sender, EventArgs e)
        {
            //----------Save Country Master Details-----------------
            try
            {

                if (txtItemName.Text.ToString() != string.Empty && txtItemQuantity.Text.ToString() != string.Empty)
                {
                    String R ="0", RI="0";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "SP_AF_AddItem";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //ID,ItemRegNo,InvoiceNo,ItemName,ItemSpecification,Brand,BarCodeNo,Quantity,CostPrice,SalesPrice,Discount
                    //,DiscountAmount,CategoryId,CategoryName,Mfgdate,Expdate,Warranty,Image,UserName,UserType,CreateON,isComp,Com_Name
                    //,CompON,isStore,Br_ID,Br_Name,StoreON,SupplierCode,SupplierName

                    cmd.Parameters.AddWithValue("@ItemRegNo", "");
                    cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@ItemSpecification", txtItemSpecification.Text.ToString());
                    cmd.Parameters.AddWithValue("@Brand", txtBrandName.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@BarCodeNo", txtBarCodeNo.Text.Trim().ToString());
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtItemQuantity.Text.Trim()).ToString()); 
                    cmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(txtCostPrice.Text).ToString());
                    cmd.Parameters.AddWithValue("@SalesPrice", Convert.ToDecimal(txtSalesPrice.Text).ToString()); 
                    cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(txtDiscount.Text.Trim()).ToString());
                    cmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDisAmount.Text).ToString());

                    cmd.Parameters.AddWithValue("@SupplierCode", drpSupplierName.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@CategoryCode", drpCategoryName.SelectedValue.Trim());

                    cmd.Parameters.AddWithValue("@Warranty", txtWarranty.Text.Trim().ToString());
    
                    if (string.IsNullOrEmpty(Convert.ToString(txtMfgDate.Text.Trim())))
                    {
                        txtMfgDate.Text = Convert.ToString(hdnMfgDate.Value);
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(txtExpDate.Text.Trim())))
                    {
                        txtExpDate.Text = Convert.ToString(hdnExpDate.Value);
                    }

                    cmd.Parameters.AddWithValue("@Mfgdate", DateTime.ParseExact(txtMfgDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)); 
                    cmd.Parameters.AddWithValue("@Expdate", DateTime.ParseExact(txtExpDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

                    cmd.Parameters.AddWithValue("@Userid", dt_login_details.Rows[0]["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Flag", "Iitem");
                    SqlParameter output = new SqlParameter("@Success", SqlDbType.Int);
                    output.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    R = output.Value.ToString();
                    con.Close();

                    if (R != String.Empty)
                    {
                        //-----------Update Item Image------------

                        if (FileUpload1.HasFile)
                        {
                            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            string contentType = FileUpload1.PostedFile.ContentType;
                            using (Stream fs = FileUpload1.PostedFile.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(fs))
                                {
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                    SqlCommand cmdI = new SqlCommand();
                                    cmdI.Connection = con;
                                    cmdI.CommandText = "SP_AF_ItemImage";
                                    cmdI.CommandType = CommandType.StoredProcedure;
                                    cmdI.Parameters.AddWithValue("@ItemImage", bytes);
                                    cmdI.Parameters.AddWithValue("@BarCodeNo", txtBarCodeNo.Text.Trim().ToString());
                                    cmdI.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text.Trim().ToString());

                                    cmdI.Parameters.AddWithValue("@Flag", "IitemImage");
                                    SqlParameter outputI = new SqlParameter("@Success", SqlDbType.Int);
                                    outputI.Direction = ParameterDirection.Output;
                                    cmdI.Parameters.Add(outputI);
                                    con.Open();
                                    cmdI.ExecuteNonQuery();
                                    RI = outputI.Value.ToString();
                                    con.Close();
                                }
                            }

                        }
                    }
                   

                    if (R == "1" && RI == "1")
                    {
                        Response.Redirect("frmItemMaster.aspx");
                    }
                    else if ( R == "2")
                    {
                        pnlAddDiv.Visible = false; 
                        pnlEdit.Visible = false;
                        pnlViewDiv.Visible = false;

                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Iteam details are already exist !" + " </h4>";
                    }
                    else
                    {
                        pnlAddDiv.Visible = false;
                        pnlEdit.Visible = false;
                        pnlViewDiv.Visible = false;

                        lblloginmsg.Attributes.Add("class", "active");
                        lblloginmsg.Attributes["style"] = "color:red; font-weight:bold;";
                        lblloginmsg.InnerHtml = " <strong>Warning!</strong> <h4 >" + " Iteam details are not inserting in database !" + " </h4>";
                    }
                }
                else
                {
                    pnlAddDiv.Visible = false;
                    pnlEdit.Visible = false;
                    pnlViewDiv.Visible = false;

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