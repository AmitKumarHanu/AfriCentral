<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmItemPriceVerification.aspx.cs" Inherits="frmItemPriceVerification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    
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
<asp:Panel ID="pnlMain" runat="server" > 
<div id="DivMain" runat="server" class="emov-page-main emov-page-main-no-top-padding" style="display:block;">   
    <!-- Header Menu-->
    <div class="emov-a-rc-header emov-a-rc-header-seun">
        <!-- Page Title-->
        <div class="emov-header-page-title emov-header-page-title-table-vigo" id="emov-application-title">
            <h3 style="font-size: 25px;" class="emov-applications-title">Item Price Verification</h3>
  
        </div>

        <div class="emov-a-header-action-group">
            <div class="emov-t-actions-group" style="display:block;">

                <table id="btnOpt" runat="server" style="width: 100%; display: block">
                    <tr>
                        <td>
                           </td>

                        <td></td>
                        <td>
                           
                        </td>
                        <td>
                 
                        </td>
                   
                    </tr>
                </table>


            </div>
        </div>

        

    </div>


</div>
    <!-- Data Grid Menu-->
<div class="emov-a-rc-table-cover">
                          
    <div id="DivGrid" runat="server" style="display:none;">

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
        <asp:GridView ID="grdIteamDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data" 
         PageSize="10" ShowHeaderWhenEmpty="true" Width="100%" OnPageIndexChanging="grdIteamDetails_PageIndexChanging"
         OnRowCommand="grdIteamDetails_RowCommand"   OnRowDataBound="grdIteamDetails_RowDataBound"   OnSelectedIndexChanged="grdIteamDetails_SelectedIndexChanged">
        <AlternatingRowStyle />
        <Columns>
     
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="BarCode No.">
        <ItemTemplate>
        <asp:Label ID="lblBarCodeNo" runat="server" Text='<%# Bind("BarCodeNo") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Item Name">
        <ItemTemplate>
        <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Item Details">
        <ItemTemplate>
        <asp:Label ID="lblItemSpecification" runat="server" Text='<%# Bind("ItemSpecification") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Status">
        <ItemTemplate>
        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>


        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="InvoiceNo" Visible="false">
        <ItemTemplate>
        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Bind("InvoiceNo") %>'></asp:Label>
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


        <!-- Message Div -->
<div id="lblloginmsg" runat="server" class="my-notify-error" style="display:none;">   </div>      




<!-- View Price Details -->
<asp:Panel ID="Panel1" runat="server" > 
<div id="DivView" runat="server"  class="emov-page-main emov-page-main-no-top-padding" width="100%" style="display:none;">      
    <!-- Pofile Blue Div -->

    <div class="emov-a-single-user-card-header">   <h2 class="h2label">Item Price Correction </h2>  </div> 
       
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
                <td >      <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  AutoPostBack="false" type="text" ID="txtBarCodeNo" ReadOnly="true"/>       </td>                      
                <td colspan="2" style="width: 15%;"></td>
                <td style="width: 15%;"> <p>Item RegNo.:</p>    </td>
                <td>   <p>  <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" AutoPostBack="false" type="text" ID="txtItemRegNo" ReadOnly="true" /> </p>      </td>                      
                </tr>
                    
                <%--item Name  --%>
                <tr>
                <td>  <p>Item Name :</p>    </td>
                <td colspan="5">   <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtItemName" ReadOnly="true" /></p>       </td>                     
                </tr>

                   
                <%-- Specification --%>
                <tr>
                <td> <p>Item Specification:</p>    </td>
                <td colspan="5">    <p>   <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtItemSpecification"  ReadOnly="true"/>  </p>            </td>
                </tr>

                      
                <%--Old Sales & Old Cost Price  --%>
                <tr> 
                <td style="width: 15%;"><p><span>Cost Price (Old) :</span></p>     </td>
                <td>
                <p>
                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false"   Width="100%"  type="text" ID="txtOldCostPrice" ReadOnly="true" />
                </p>
                </td>
                <td colspan="2" style="width: 15%;"></td>
                <td style="width: 15%;">     <p><span>Sales Price (Old):</span></p>      </td>
                <td>                           
                <p>
                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false"  Width="100%"  type="text" ID="txtOldSalesPrice" ReadOnly="true" />
                </p>
                </td> 
                </tr>
                    


                <%--Unit Sales & Cost Price  --%>
                <tr> 
                <td style="width: 15%;"><p><span>Cost Price :</span></p>     </td>
                <td>
                <p>
                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false"   Width="100%"  type="text" ID="txtNewCostPrice" ReadOnly="true" />
                </p>
                </td>
                <td colspan="2" style="width: 15%;"></td>
                <td style="width: 15%;">     <p><span>Sales Price :</span></p>      </td>
                <td>                           
                <p>
                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false"  Width="100%"  type="text" ID="txtNewSalesPrice" ReadOnly="true" />
                </p>
                </td> 
                </tr>
                    
                                      
              
                </table>

                <table class="emov-a-table-data1">       
                <tr>                   
                <td colspan="4" ></td>    
                <td > <button id="btnReject" type="button" runat="server" class="login-btn click"  style="float: right;"    onserverclick="btnRejectOpt"> Reject Correction </button></td>
                <td > <button id="btnCorrection" type="button" runat="server" class="login-btn click"  style="float: right;"  ValidationGroup="AddSign"  onserverclick="btnCorrectionOpt"> Save Correction </button></td>
                  
                </tr>
                </table>    

                          
              </div>

        </div>

</div>

    </asp:Panel>
    <asp:HiddenField ID="hdnComID" runat="server" ClientIDMode="Static" Value=""  />
 
   
</div>

</asp:Content>
