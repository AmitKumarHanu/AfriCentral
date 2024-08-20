<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" EnableEventValidation="false" CodeBehind="frmItemTransfer.aspx.cs" Inherits="Afri_Central_Code.frmItemTransfer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <%--    --%>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script> 
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
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
      <!--Autocompile dropbox design -->
    <style>
        .ui-autocomplete  
        {
        font-size:11px;
        text-align:left;         
        width: 50px;
        max-height: 300px;
        overflow-y: auto;
        prevent horizontal scrollbar 
        overflow-x: hidden;
        border:1px solid #ccc;
        }

        .ui-autocomplete-row
        {
        max-height: 100px;
        overflow-y: auto;
        prevent horizontal scrollbar
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


    <!--Javascript function for popup box  -->
    <script type='text/javascript'>
        $(function () {
            var overlay = $('<div id="overlay"></div>');
            $('.close').click(function () {
                $('.popup').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('.x').click(function () {
                $('.popup').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('.click').click(function () {
                overlay.show();
                overlay.appendTo(document.body);
                $('.popup').show();
                return false;
            });
        });
</script>
    
       <!-- ------------Autocomplete function----------------- -->
  <link rel="stylesheet" href="http://localhost:63262/code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

      <!-- Get Brach Details by Comapnyname -->
    <script language="javascript" type="text/javascript">
        $(function () {
            var hv = $("#<%=hdnComID.ClientID %>").val();
                   
            $('#<%=txtBrName.ClientID%>').autocomplete({           
                source: function (request, response) {                 
                    $.ajax({
                        url: "frmItemTransfer.aspx/GetBranch",
                        data: "{ 'Br':'" + request.term + "', 'ComID':'" + hv + "'}",
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
            var hv = $("#<%=hdnComID.ClientID %>").val();

            $('#<%=txtBarCodeNo.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "frmItemTransfer.aspx/GetBarcode",
                        data: "{ 'BarCode':'" + request.term + "', 'ComID':'" + hv + "'}",
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
                    $('#<%=txtItemSpecification.ClientID%>').val(ui.item.Specification);
                    $('#<%=txtSalesPrice.ClientID%>').val(ui.item.SalesPrice);
                    $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                    return false;
                },
                select: function (event, ui) {
                    $('#<%=txtBarCodeNo.ClientID%>').val(ui.item.BarCodeNo);
                    $('#<%=txtItemName.ClientID%>').val(ui.item.Name);
                    $('#<%=txtItemSpecification.ClientID%>').val(ui.item.Specification);
                    $('#<%=txtSalesPrice.ClientID%>').val(ui.item.SalesPrice);
                    $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                    return false;
                },
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li class='ui-autocomplete-row'></li>")
                   .append("<a>BarCodeNo:" + item.BarCodeNo + "<br>Name: " + item.Name + "<br>Specification: " + item.Specification + "<br>Quantity: " + item.Quantity + "</a>")
                   .appendTo(ul);
            };
        });
    </script>

    <!-- Get Item Details by item Name -->
     <script language="javascript" type="text/javascript">
         $(function () {
             var hv = $("#<%=hdnComID.ClientID %>").val();

             $('#<%=txtItemName.ClientID%>').autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         url: "frmItemTransfer.aspx/GetitemName",
                         data: "{ 'ItemName':'" + request.term  + "', 'ComID':'" + hv + "'}",
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
                     $('#<%=txtItemSpecification.ClientID%>').val(ui.item.Specification);

                     $('#<%=txtSalesPrice.ClientID%>').val(ui.item.SalesPrice);
                     $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                     return false;
                 },
                 select: function (event, ui) {
                     $('#<%=txtBarCodeNo.ClientID%>').val(ui.item.BarCodeNo);
                    $('#<%=txtItemName.ClientID%>').val(ui.item.Name);
                    $('#<%=txtItemSpecification.ClientID%>').val(ui.item.Specification);

                    $('#<%=txtSalesPrice.ClientID%>').val(ui.item.SalesPrice);
                    $('#<%=txtItemRegNo.ClientID%>').val(ui.item.ItemRegNo);
                    return false;
                },
             }).data("ui-autocomplete")._renderItem = function (ul, item) {
                 return $("<li class='ui-autocomplete-row'></li>")
                     .append("<a>BarCodeNo:" + item.BarCodeNo + "<br>Name: " + item.Name + "<br>Specification: " + item.Specification + "<br>Quantity: " + item.Quantity + "</a>")
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

    <!-- Receipt CSS -->
    <style type="text/css">

        #invoice-POS{
  box-shadow: 0 0 1in -0.25in rgba(0, 0, 0, 0.5);
  padding:11mm;
  margin: 0 auto;
  width: 82mm;
  background: #FFF;
  
  
::selection {background: #f31544; color: #FFF;}
::moz-selection {background: #f31544; color: #FFF;}
h1{
  font-size: 1.5em;
  color: #222;
}
h2{font-size: .9em;}
h3{
  font-size: 1.2em;
  font-weight: 300;
  line-height: 2em;
}
p{
  font-size: .7em;
  color: #666;
  line-height: 1.2em;
}
 
#top, #mid,#bot{ /* Targets all id with 'col-' */
  border-bottom: 1px solid #EEE;
}

#top{min-height: 100px;}
#mid{min-height: 80px;} 
#bot{ min-height: 50px;}

#top .logo{
  //float: left;
	height: 60px;
	width: 60px;
	background: url(http://michaeltruong.ca/images/logo1.png) no-repeat;
	background-size: 60px 60px;
}
.clientlogo{
  float: left;
	height: 60px;
	width: 60px;
	background: url(http://michaeltruong.ca/images/client.jpg) no-repeat;
	background-size: 60px 60px;
  border-radius: 50px;
}
.info{
  display: block;
  //float:left;
  margin-left: 0;
}
.title{
  float: right;
}
.title p{text-align: right;} 
table{
  width: 100%;
  border-collapse: collapse;
}
td{
  //padding: 5px 0 5px 15px;
  //border: 1px solid #EEE
}
.tabletitle{
  padding: 5px;
  font-size: .5em;
  background: #EEE;
}
.service{border-bottom: 1px solid #EEE;}
.item{width: 24mm;}
.itemtext{font-size: .5em;}

#legalcopy{
  margin-top: 5mm;
}

  
  
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
            <h3 style="font-size: 25px;" class="emov-applications-title">Item Transfer Details</h3>
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
                            <input type="text" id="txtSearch" runat="server" class="emov-a-header-input" ReadOnly="readonly" placeholder="Tticket No. " /></td>

                        <td></td>
                        <td>
                            <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" ReadOnly="true"  onserverclick="BtnSearchOpt">SEARCH <i class="fa fa-search" aria-hidden="true"></i></button>
                        </td>
                        <td>
                            <button id="btnNew" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;"  ReadOnly="true" onserverclick="BtnNewOpt">New <i class="fa fa-plus" aria-hidden="true"></i></button>
                        </td>
                   
                    </tr>
                </table>


            </div>
        </div>

        

    </div>


</div>
   </asp:Panel>

<!-- Message Div -->
<div id="lblloginmsg" runat="server" class="my-notify-error" style="display:none;">   </div>
     <!-- Data Grid Design-->
<asp:Panel ID="pnlOrderTransfer" runat="server">
         
    <div class="emov-a-rc-table-cover">
                          
    <div id="div1" runat="server">
    <!-- table within data grid-->
        
    <table style="width:100%; margin-top:-4%;" >                              
    <caption>
    <tr> 
    <td>  
    <br />
  
    <div>
    <table style="width:100%;">
    <tr>
    <td>
    <div style="overflow-x:scroll; text-align:center">
       <%-- ----%>
        <asp:GridView ID="grdIteamTransfer" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data" 
         PageSize="10" ShowHeaderWhenEmpty="true" Width="100%" OnPageIndexChanging="grdIteamTransfer_PageIndexChanging"
         OnRowCommand="grdIteamTransfer_RowCommand"   OnRowDataBound="grdIteamTransfer_RowDataBound"   OnSelectedIndexChanged="grdIteamTransfer_SelectedIndexChanged">
        <AlternatingRowStyle />
        <Columns>
     
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Ticket No">
        <ItemTemplate>
        <asp:Label ID="lblTicketNo" runat="server" Text='<%# Bind("Ticket") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Store Name">
        <ItemTemplate>
        <asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("Br_Name") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Date">
        <ItemTemplate>
        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("CreateON") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Transfer">
        <ItemTemplate>
        <asp:Label ID="lblTransfer" runat="server" Text='<%# Bind("Transfer") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Verification">
        <ItemTemplate>
        <asp:Label ID="lblVerify" runat="server" Text='<%# Bind("Verify") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
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
    </td>
    </tr>
  
    </table>
    </div>
 
    </td>
    </tr>   
    </caption>
    </table>

    </div>

    </div>
          

</asp:Panel>


              
<asp:Panel ID="pnlOrderDetails" runat="server"  class="emov-page-main emov-page-main-no-top-padding" width="100%"    style="display:none">    
    
  <div class="emov-a-single-user-card-header">   <h2 class="h2label">Transfer Item Details </h2>  </div> 
         
    <!-- Data Grid Design-->
  <div class="emov-a-rc-table-cover">                          
   
    <!-- table within data grid-->
        
    <table style="width:100%; margin-top:-4%;" >                              
    <caption>
    <tr> 
    <td>  
    <br />
  
    <div>
    <table style="width:100%;">
    <tr>
    <td>
    <div style="overflow-x:scroll; text-align:center">
       <%-- ----%>
        <asp:GridView ID="grdOrderDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data" 
         PageSize="10" ShowHeaderWhenEmpty="true" Width="100%" >
        <AlternatingRowStyle />
        <Columns>
     
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Item Name">
        <ItemTemplate>
        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>



        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Brand Name">
        <ItemTemplate>
        <asp:Label ID="lblBrand" runat="server" Text='<%# Bind("Brand") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Item Details">
        <ItemTemplate>
        <asp:Label ID="lblItemSpecification" runat="server" Text='<%# Bind("ItemSpecification") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>        


        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Quantity">
        <ItemTemplate>
        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Cost Price">
        <ItemTemplate>
        <asp:Label ID="lblCostPrice" runat="server" Text='<%# Bind("CostPrice") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        

            
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Sales Price">
        <ItemTemplate>
        <asp:Label ID="lblSalesPrice" runat="server" Text='<%# Bind("SalesPrice") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

            
         <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Store Name">
        <ItemTemplate>
        <asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("Br_Name") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>


        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Warranty (Months)" Visible="false" >
        <ItemTemplate>
        <asp:Label ID="lblWarranty" runat="server" Text='<%# Bind("Warranty") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>


            
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="BarcodeNo" Visible="false" >
        <ItemTemplate>
        <asp:Label ID="lblBarcodeNo" runat="server" Text='<%# Bind("BarCodeNo") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

                   
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="ItemRegNo" Visible="false" >
        <ItemTemplate>
        <asp:Label ID="lblItemRegNo" runat="server" Text='<%# Bind("ItemRegNo") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
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
    </td>
    </tr>
  
    </table>
    </div>
 
    </td>
    </tr> 
          
    </caption>
        
    </table>

    </div> 

</asp:Panel>



<!-- Add New Bill Details -->
<asp:Panel ID="Panel1" runat="server" > 
<div id="DivAdd" runat="server"  class="emov-page-main emov-page-main-no-top-padding" width="100%" style="display:none;">      
    <!-- Pofile Blue Div -->

    <div class="emov-a-single-user-card-header">   <h2 class="h2label">New Ticket </h2>  </div> 
         
      
    <div class="emov-a-single-user-card-cover">
            <!--changes -->    
         
             <div class="emov-a-profile-full-info" style="background: white">
                <table class="emov-a-table-data1">
                <tr>
                <td colspan="6">
                <div class="emov-a-single-user-uid-box">  
              
                <table class="emov-a-table-data1">
                 

                <%--Bill Infor --%>
                <tr>
                <td style="width: 15%;">  <p>Ticket No.:</p>   </td>
                <td style="width: auto;"> <p>    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  AutoPostBack="false" type="text" ID="txtBillNo" />                            </p>
                </td>
                <td style="width: auto;">   <p>Date.:&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</p>   </td>

                <td style="width: auto;">      <p> <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBillDate" />                            </p>
                </td>
                <td style="width: auto;">     <p>Currency:</p>   </td>
                <td style="width:  auto;">   <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" Text="NGN" type="text"  ID="txtCurrency" /> </p></td>                    </tr>
                     

                </table>
                </div>

                </td>
                </tr>
                  
                <tr>
                <td colspan="6" style="height: 8px;"></td>
                </tr>

                <%--BarCode No & ItemRegNo--%>
                <tr>
                <td style="width: 15%;"><p>BarCode/SKU No. :</p>   </td>
                <td >      <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  AutoPostBack="false" type="text" ID="txtBarCodeNo" />       </td>                      
                <td colspan="2" style="width: 15%;"></td>
                <td style="width: 15%;"> <p>Item RegNo.:</p>    </td>
                <td>   <p>  <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" AutoPostBack="false" type="text" ID="txtItemRegNo" ReadOnly="false" /> </p>      </td>                      
                </tr>
                    
                <%--item Name  --%>
                <tr>
                <td>  <p>Item Name :</p>    </td>
                <td colspan="5">   <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtItemName" /></p>       </td>                     
                </tr>

                   
                <%-- Specification --%>
                <tr>
                <td> <p>Item Specification:</p>    </td>
                <td colspan="5">    <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtItemSpecification" />  </p>            </td>
                </tr>

                 
                <%--Unit Price & Quantity  --%>
                <tr>
                <td style="width: 15%;">     <p><span>Sales Price :</span></p>      </td>
                <td>                           
                <p>
                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtSalesPrice" />
                </p>
                </td> 
                <td colspan="2" style="width: 15%;"></td>
                <td style="width: 15%;">     <p>Quantity:</p>     </td>
                <td>
                <p>
                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  Width="100%"   AutoPostBack="false" type="text" ID="txtItemQuantity" Text="1" />
                </p>
                </td>
                </tr>
                 
                <tr>
                <td style="width: 15%;"></td>
                <td style="width: 15%;">  </td>
                <td  colspan="4">
                          
                <asp:Button ID="btnAddProduct" runat="server" class="login-btn" style="float: right;" OnClick="btnAddProduct_Click" Text="Add"  Visible="true" />
                            
                </td>
                </tr>

                    
                                      
                
                </table>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>    
                <div id="lblMessage" runat="server" class="my-notify-error" style="display:none;"> </div>
                <table class="emov-a-table-data1" id="tgrid" runat="server"  width="100%;">
                <tr>
                <td   colspan="5"  style="border-color:antiquewhite;  border-width:2px" onscroll="ture">
                <%--   Data Grid --%>                    
                <fieldset style="  width: 100%; height: 320px;">
                <legend> Items </legend>          
                <div id="div_search_results" runat="server" style="max-height: 300px;  overflow-y: auto;   overflow-x: hidden;">                      

                <asp:GridView ID="grdAddToCard" runat="server"  AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data" 
                ShowHeaderWhenEmpty="true"  style="width: 100%;"   >
                <AlternatingRowStyle />
                <Columns>
                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="item Name">
                <ItemTemplate>
                <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Item Details">
                <ItemTemplate>
                <asp:Label ID="lblItemDetails" runat="server" Text='<%# Bind("Specification") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Price">
                <ItemTemplate>
                <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("SalesPrice") %>'></asp:Label>
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


                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Barcode" Visible="false">
                <ItemTemplate>
                <asp:Label ID="lblBarcode" runat="server" Text='<%# Bind("BarcodeNo") %>'></asp:Label>
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
                    
                <%--item count  --%>
                <tr>
                <td style="width: 15%;">
                <p>
                Total item :</p>
                </td>
                <td >
                <p>
                <asp:TextBox ID="txtitemCount" runat="server" AutoPostBack="false" class="emov-a-slot-creation-time-input emov-a-header-input-mob" type="text" Text="0"  />
                </p>
                </td>
                <td style="width: 15%;">
                <p>
                Sub Total :</p>
                </td>
                <td >
                <p>
                <asp:TextBox ID="txtSubTotal" runat="server" AutoPostBack="false" class="emov-a-slot-creation-time-input emov-a-header-input-mob" type="text" Text="0"  />
                </p>
                </td>
                <td class="auto-style1" style=" text-align:center; width: 30%;" >
                <p>
                <asp:Label ID="lblTotalPayment" runat="server" Font-Bold="true" Font-Size="XX-Large" Height="1px" Text="0" ></asp:Label><span>₦</span>
                </p>
                </td>
                </tr>
                              
                               
                </table>                      
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAddProduct" EventName="Click" /> 
                </Triggers>
                </asp:UpdatePanel>

                <table class="emov-a-table-data1">
                <tr>
                <td colspan="6">       
                <div class="emov-a-single-user-uid-box">  
              
                <table class="emov-a-table-data1">                   

                <%--Bill Infor --%>
                <tr>
                <td style="width: 15%;">   <p> Store Name.:</p>   </td>
                <td style="width: auto;"> <p>    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  AutoPostBack="false" type="text" ID="txtBrName" />  </p>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Stroe Name is required!" Display="Dynamic" ForeColor="Red"   ControlToValidate="txtBrName" ValidationGroup="AddSign" runat="server" />   
                </td>
                <td style="width: auto;">   <p>Mobile No.:</p>   </td>

                <td style="width: auto;">      <p> <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBrMobileNo" /> </p>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Mobile No is required!" Display="Dynamic" ForeColor="Red"   ControlToValidate="txtBrMobileNo" ValidationGroup="AddSign" runat="server" />   
                </td>
                <td style="width: auto;">     <p>VAT No:</p>   </td>
                <td style="width:  auto;">   <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" Text="" type="text"  ID="txtBrVatRegNo" /> </p>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="VAT Reg No. is required!" Display="Dynamic" ForeColor="Red"   ControlToValidate="txtBrVatRegNo" ValidationGroup="AddSign" runat="server" />   
                </td> 
                </tr>

                </table>
                </div>
                </td></tr>
                <tr>                   
                <td colspan="5" ></td>                    
                <td > <button id="btnTransfer" type="button" runat="server" class="login-btn click"  style="float: right;"  ValidationGroup="AddSign"  onserverclick="btnTransferItem"> Transfer </button></td>
                </tr>
                </table>          
              </div>

        </div>
</div>

    </asp:Panel>
    <asp:HiddenField ID="hdnComID" runat="server" ClientIDMode="Static" Value=""  />
 
   
</div>


</asp:Content>
