<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemAllotment.aspx.cs" Inherits="Afri_Central_Code.ItemAllotment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

 <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script> 
    <!--Current Date -->
    <script type="text/javascript">
        $(document).ready(function () {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = dd + '/' + mm + '/' + yyyy;

            $('#<%= txtBillDate.ClientID %>').val(today)


                });

    </script>  
    
       <!-- ------------Autocomplete function----------------- -->
  <link rel="stylesheet" href="http://localhost:44305/code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
     <!--Autocompile dropbox design -->
    <style>
        .ui-autocomplete  
        {
        font-size:11px;
        text-align:left;         
        width: 50px;
        max-height: 300px;
        overflow-y: auto;
        /* prevent horizontal scrollbar */
        overflow-x: hidden;
        border:1px solid #ccc;
        }

        .ui-autocomplete-row
        {
        max-height: 100px;
        overflow-y: auto;
        /* prevent horizontal scrollbar */
        overflow-x: hidden;
        padding:8px;
        background-color: #f4f4f4;
        border-bottom:1px solid #ccc;
        font-weight:bold;          
        }
        .ui-autocomplete-row:hover
        { 
              
        background-color: #f05223;   
        color:#fff;
        cursor:pointer;
        }

    </style>

      <!-- Get Brach Details by Comapnyname -->
    <script language="javascript" type="text/javascript">
        $(function () {
            $('#<%=txtBrName.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "ItemAllotment.aspx/GetBranch",
                        data: "{ 'Br':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    Br_ID: item.Br_ID,
                                    Br_Name: item.Br_Name,
                                    Br_PhoneNo: item.Br_PhoneNo,
                                    Br_VATRegNo: item.Br_VATRegNo,
                                    json: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                focus: function (event, ui) {
                    $('#<%=txtBrName.ClientID%>').val(ui.item.Br_Name);
                    $('#<%=txtBrMobileNo.ClientID%>').val(ui.item.Br_PhoneNo);
                    $('#<%=txtBrVatRegNo.ClientID%>').val(ui.item.Br_VATRegNo);

                    return false;
                },
                select: function (event, ui) {
                    $('#<%=txtBrName.ClientID%>').val(ui.item.Br_Name);
                    $('#<%=txtBrMobileNo.ClientID%>').val(ui.item.Br_PhoneNo);
                    $('#<%=txtBrVatRegNo.ClientID%>').val(ui.item.Br_VATRegNo);
                    return false;
                },
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li class='ui-autocomplete-row'></li>")
                    .append("<a>" + item.Br_Name + "</a>")
                    .appendTo(ul);
            };
        });
    </script>
    
    <!-- Get Item Details by Barcode -->
    <script language="javascript" type="text/javascript">
        $(function () {
            $('#<%=txtBarCodeNo.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "ItemAllotment.aspx/GetBarcode",
                        data: "{ 'BarCpde':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    Name: item.Name,
                                    Industry: item.Industry,
                                    Specification: item.Specification,
                                    BarCodeNo: item.BarCodeNo,
                                    Quantity: item.Quantity,
                                    SalesPrice: item.SalesPrice,
                                    CostPrice: item.CostPrice,
                                    ItemRegNo: item.ItemRegNo,
                                    json: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                focus: function (event, ui) {
                    $('#<%=txtBarCodeNo.ClientID%>').val(ui.item.BarCodeNo);
                    $('#<%=txtItemName.ClientID%>').val(ui.item.Name);          
                    $('#<%=txtItemQuantity.ClientID%>').val(ui.item.Quantity);
                    $('#<%=txtCostPrice.ClientID%>').val(ui.item.CostPrice);
                    $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                    return false;
                },
                select: function (event, ui) {
                    $('#<%=txtBarCodeNo.ClientID%>').val(ui.item.BarCodeNo);
                    $('#<%=txtItemName.ClientID%>').val(ui.item.Name);                 
                    $('#<%=txtItemQuantity.ClientID%>').val(ui.item.Quantity);
                    $('#<%=txtCostPrice.ClientID%>').val(ui.item.CostPrice);
                    $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                    return false;
                },
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li class='ui-autocomplete-row'></li>")
                    .append("<a>BarCodeNo:" + item.BarCodeNo + "<br>Name: " + item.Name + "<br>Quantity: " + item.Quantity + "</a>")
                    .appendTo(ul);
            };
        });
    </script>

    <!-- Get Item Details by item Name -->
     <script language="javascript" type="text/javascript">
         $(function () {
             $('#<%=txtItemName.ClientID%>').autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         url: "ItemAllotment.aspx/GetitemName",
                         data: "{ 'ItemName':'" + request.term + "'}",
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return {
                                     Name: item.Name,
                                     Industry: item.Industry,
                                     Specification: item.Specification,
                                     BarCodeNo: item.BarCodeNo,
                                     Quantity: item.Quantity,
                                     SalesPrice: item.SalesPrice,
                                     CostPrice: item.CostPrice,
                                     ItemRegNo: item.ItemRegNo,
                                     json: item
                                 }
                             }))
                         },
                         error: function (XMLHttpRequest, textStatus, errorThrown) {
                             alert(textStatus);
                         }
                     });
                 },
                 focus: function (event, ui) {
                     $('#<%=txtBarCodeNo.ClientID%>').val(ui.item.BarCodeNo);
                     $('#<%=txtItemName.ClientID%>').val(ui.item.Name);           
                     $('#<%=txtItemQuantity.ClientID%>').val(ui.item.Quantity);
                     $('#<%=txtCostPrice.ClientID%>').val(ui.item.CostPrice);
                     $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                     return false;
                 },
                 select: function (event, ui) {
                     $('#<%=txtBarCodeNo.ClientID%>').val(ui.item.BarCodeNo);
                    $('#<%=txtItemName.ClientID%>').val(ui.item.Name);           
                    $('#<%=txtItemQuantity.ClientID%>').val(ui.item.Quantity);
                    $('#<%=txtCostPrice.ClientID%>').val(ui.item.CostPrice);
                    $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                    return false;
                },
             }).data("ui-autocomplete")._renderItem = function (ul, item) {
                 return $("<li class='ui-autocomplete-row'></li>")
                     .append("<a>BarCodeNo:" + item.BarCodeNo + "<br>Name: " + item.Name + "<br>Quantity: " + item.Quantity + "</a>")
                     .appendTo(ul);
             };
         });
     </script> 
    
    <!-- Alert Msg CSS -->
    <style type="text/css">
       
 /*@import url('//maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css');*/
 
.my-notify-info, .my-notify-success, .my-notify-warning, .my-notify-error {
    padding:10px;
    margin:10px 0;
 
}
.my-notify-info:before, .my-notify-success:before, .my-notify-warning:before, .my-notify-error:before {
    font-family:FontAwesome;
    font-style:normal;
    font-weight:400;
    speak:none;
    display:inline-block;
    text-decoration:inherit;
    width:1em;
    margin-right:.2em;
    text-align:center;
    font-variant:normal;
    text-transform:none;
    line-height:1em;
    margin-left:.2em;
    -webkit-font-smoothing:antialiased;
    -moz-osx-font-smoothing:grayscale
}
.my-notify-info:before {
    content:"\f05a";
}
.my-notify-success:before {
    content:'\f00c';
}
.my-notify-warning:before {
    content:'\f071';
}
.my-notify-error:before {
    content:'\f057';
}
.my-notify-info {
    color: #00529B;
    background-color: #BDE5F8;
     font-size:14px;
}
.my-notify-success {
    color: #4F8A10;
    background-color: #DFF2BF;
     font-size:14px;
}
.my-notify-warning {
    color: #9F6000;
    background-color: #FEEFB3;
     font-size:14px;
}
.my-notify-error {
    color: #D8000C;
    background-color: #FFD2D2;
    font-size:14px;
}
    </style>


    <!--   Printer  fun -->
    <script type="text/javascript" >
        function PrintContent() {
            var html = "<html>";
            html += document.getElementById("DivPrint").innerHTML;
            html += "</html>";

            var printWin = window.open('', '', 'location=no,width=10,height=10,left=50,top=50,toolbar=no,scrollbars=no,status=0,titlebar=no');

            printWin.document.write(html);
            printWin.document.close();
            printWin.focus();
            printWin.print();
            printWin.close();
        }

     </script>

   


<div class="emov-page-container emov-step-wrapper">


<!-- Main Panel -->
<asp:Panel ID="Panel2" runat="server" > 
<div id="DivMain" runat="server" class="emov-page-main emov-page-main-no-top-padding" style="display:block;">   
    <!-- Header Menu-->
    <div class="emov-a-rc-header emov-a-rc-header-seun">
        <!-- Page Title-->
        <div class="emov-header-page-title emov-header-page-title-table-vigo" id="emov-application-title">
            <h3 style="font-size: 25px;" class="emov-applications-title">Item Allotment Details</h3>
            <!-- Total Count-->
            <div class="emov-a-header-counter">
                <p>
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>   Total
                </p>
            </div>
        </div>

        <div class="emov-a-header-action-group">
            <div class="emov-t-actions-group" style="display:block;">

                <table id="btnOpt" runat="server" style="width: 100%; display: block">
                    <tr>
                        <td>
                            <input type="text" id="txtSearch" runat="server" class="emov-a-header-input" placeholder="Invoice / Bill No. " /></td>
                        <td></td>
                        <td>
                            <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnSearchOpt">SEARCH <i class="fa fa-search" aria-hidden="true"></i></button>
                        </td>
                        <td>
                            <button id="btnNew" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;"  onserverclick="BtnNewOpt">New <i class="fa fa-plus" aria-hidden="true"></i></button>
                        </td>
                         <td>
                            <button id="btnAdd" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" visible="false" onserverclick="BtnAddOpt">Add <i class="fa fa-save" aria-hidden="true"></i></button>
                        </td>
                        <td>
                            <button id="btnEdit" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" visible="false" onserverclick="BtnEditOpt">Edit <i class="fa fa-edit" aria-hidden="true"></i></button>
                        </td>
                    </tr>
                </table>


            </div>
        </div>

    </div>


</div>
   </asp:Panel>
<!-- Message Div -->
<div id="lblloginmsg" runat="server" class="my-notify-error" style="display:none;  margin-bottom: 20%; ">   </div>
               
<!-- Add New Bill Details -->
<asp:Panel ID="Panel1" runat="server" > 
<div id="DivAdd" runat="server"  class="emov-page-main emov-page-main-no-top-padding" width="100%" style="display:none;">      
    <!-- Pofile Blue Div -->

    <div class="emov-a-single-user-card-header">   <h2 class="h2label">New Invoice Details</h2>  </div> 
         
   
    <div class="emov-a-single-user-card-cover">

        <!-- New Design-->
        <div class="emov-a-profile-full-info" style="background: white">
            
             <table class="emov-a-table-data1" style="width:100%;" >
                 <%--  1st Row Section ---%>
                    <tr>  <td style="width: 75%; height: 225px;" >
                     <table class="emov-a-table-data1">
                       
                    <%--BarCode No & ItemRegNo--%>
                    <tr>
                        <td style="width:auto;"><p>BarCode/SKU No. :</p>   </td>
                        <td style="width:auto;"> <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBarCodeNo" />       </td>                      
                        <td style="width:auto;"></td>
                        <td>  <p>Item Name :</p>    </td>
                        <td>   <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtItemName" /></p>       </td>                     
                                 
                     </tr>
                                 
                        <%--Unit Price & Quantity  --%>
                    <tr>
                          <td style="width:auto;">    <p><span>Cost Price :</span></p>      </td>
                          <td style="width:auto;"><p>  <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtCostPrice" />  </p> </td> 
                          <td style="width:auto;"></td>
                          <td style="width:auto;">     <p>Quantity:</p>     </td>
                       <td><p> <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtItemQuantity" />  </p> 
                           
                       </td>

                    </tr>
                           
                      </table>
                          </td> 
                          <td   rowspan="2"  style="width: 5%;"></td>
                          <td  rowspan="2" style="width: 20%;"">
                              
                             <table class="emov-a-table-data1">
                    <tr>
                        <td colspan="6" style="height: auto;"> </td>
                    </tr>

                    <%--Bill Intro --%>
                    <tr>
                        <td style="width: auto;"><p>Ticket No.:</p> <p><asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  AutoPostBack="false" type="text" ID="txtBillNo" /> </p> </td></tr>
                    <tr>                     
                         <td style="width: auto;"><p>Date.:&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</p>   <p> <asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBillDate" />  </p></td></tr>
                    <tr>
                        <td style="width: auto;"><p>Currency:</p><p><asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" Text="NGN" type="text"  ID="txtCurrency" /> </p></td></tr>
                   
                    <%--Client Intro --%>
                    <tr>
                        <td style="width: auto;"><p>Store Name.:</p><p><asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  AutoPostBack="false" type="text" ID="txtBrName" />  </p></td></tr>
                    <tr>
                        <td style="width: auto;"><p>Mobile No. &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</p><p> <asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBrMobileNo" /> </p></td></tr>
                    <tr>
                        <td style="width: auto;"><p>VAT RegNo :</p><p><asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" Text="" type="text"  ID="txtBrVatRegNo" /> </p></td></tr>
                     <tr>
                     <td style="width: auto;"><p>Item RegNo :</p><p>  <asp:TextBox runat="server" Width="100%" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtItemRegNo" ReadOnly="false"  Visible="true"/> </p></tr>
                    <%--Show Total Intro --%>
                      <tr>
                          <td style="width: auto;"> <button id="btnTransfer" type="button" runat="server" class="login-btn" onserverclick="btnTransferItem"  > Transfer </button></td>
                      </tr>

                </table>
                               
                              </td>
                         
                    </tr> 
                  <%--  1st Row Section ---%>
                
                          <tr>  <td style="width: 75%;" >
                          
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>  
                            <table class="emov-a-table-data1" id="tgrid" runat="server" style="width: 100%;">
                            <tr>                        
                            <td style="width: auto; "><p>  <asp:Label ID="lblTotalPayment" runat="server" Font-Bold="true" Font-Size="XX-Large" Height="1px" Text="0" ></asp:Label><span>₦</span>  </p><p>Total Amount</p> </td>
                            <td style="width: auto; "><p>  <asp:Label ID="lblTotalitem" runat="server" Font-Bold="true" Font-Size="XX-Large" Height="1px" Text="0" ></asp:Label></p><p>Total Item</p> </td>
                            <td style="width:300px;  "><asp:LinkButton ID="btnAddProduct" runat="server"  class="login-btn"   OnClick="btnAddProduct_Click" Text="Add item" ValidationGroup="AddSign" Visible="true" > Add items <i class="fa fa-shopping-cart"  style="color:white"  aria-hidden="true"></i></asp:LinkButton></td>
                            </tr>
                            <tr >  
                            <td colspan="3"  style="border-color:antiquewhite;  border-width:2px" onscroll="ture">
                            <%--   Data Grid --%>                    
                            <fieldset style="  width: 100%; height: 320px;">
                            <legend> Item Details</legend>
                            <div id="div_search_results" runat="server" style="max-height: 300px;  overflow-y: auto;   overflow-x: hidden;   ">                      

                            <asp:GridView ID="grdAddToCard" runat="server"  AllowSorting="true" AutoGenerateColumns="False" Border="1px" BorderColor="White" CssClass="emov-a-table-data" 
                            ShowHeaderWhenEmpty="true"  style="width: 100%;"   >
                            <AlternatingRowStyle />
                            <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="item Name">
                            <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                                
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Price">
                            <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("CostPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
       
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Quantity">
                            <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Total Amount">
                            <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Bind("TotalAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="BarCode" Visible="false">
                            <ItemTemplate>
                            <asp:Label ID="lblBarCodeNo" runat="server" Text='<%# Bind("BarCodeNo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                       
                      
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading"  HeaderText="Action">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>

                            <asp:Label runat="server" ID="lblActions" Text='' />
                            <asp:LinkButton ID="linkDelete" runat="server" CommandArgument='<%# Eval("ItemRegNo") %>' OnClick="linkDelete_Click"><i class="fas fa-trash" style='color:red'></i> </asp:LinkButton>

                            </ItemTemplate>

                            </asp:TemplateField>


                            </Columns>
                            <HeaderStyle CssClass="emov-a-home-table" />
                            <PagerStyle Height="100px" />
                            <RowStyle CssClass="emov-a-table-data" />
                            <EditRowStyle CssClass="emov-a-table-data" />
                            <EmptyDataTemplate>
                            <div style="text-align:center;">
                            No records found.</div>
                            </EmptyDataTemplate>
                            </asp:GridView>   
                            </div>
                            </fieldset>   
                            </td>
                            </tr>
                              
                            </table> 
                            </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddProduct" EventName="Click" /> 
                            </Triggers>
                            </asp:UpdatePanel>
                                 
                           </td>                           
                    </tr>
                
                
              </table>
            
        </div>
        <!--New Design End -->    
         
        </div>

</div>

    </asp:Panel>

</div>
</asp:Content>

