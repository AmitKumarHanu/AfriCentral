<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" EnableEventValidation="false" CodeBehind="frmitemReturnVerification.aspx.cs" Inherits="Afri_Central_Code.frmitemReturnVerification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js" type="text/javascript"></script> 
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
   
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

  !-- Select Order Details Row Checkbox -->
    <script type = "text/javascript">
        function ChkVerify_Click(objRef) {

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
    <!-- Select Order Details All Checkbox -->  
    <script type = "text/javascript">
        function checkVerifyAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
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
<asp:Panel ID="pnlMain" runat="server" > 
<div id="DivMain" runat="server" class="emov-page-main emov-page-main-no-top-padding" style="display:block;">   
    <!-- Header Menu-->
    <div class="emov-a-rc-header emov-a-rc-header-seun">
        <!-- Page Title-->
        <div class="emov-header-page-title emov-header-page-title-table-vigo" id="emov-application-title">
            <h3 style="font-size: 25px;" class="emov-applications-title">Item Stock Return Verification</h3>
  
        </div>

        <div class="emov-a-header-action-group">
            <div class="emov-t-actions-group" style="display:block;">

                <table id="btnOpt" runat="server" style="width: 100%; display: block">
                    <tr>
                        <td>
                           </td>

                        <td></td>
                        <td > <button id="btnVerify" type="button" runat="server" class="login-btn click"  style="float: right;"  ValidationGroup="AddSign" onserverclick="btnVerifyOpt"  > Verify </button></td>
                        <td > <button id="btnReject" type="button" runat="server" class="login-btn click"  style="float: right;"  ValidationGroup="AddSign"  onserverclick="btnRejectOpt" > Reject </button></td>
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
         PageSize="10" OnPageIndexChanging="grdIteamDetails_PageIndexChanging" ShowHeaderWhenEmpty="true" Width="100%" >
        <AlternatingRowStyle />
        <Columns>
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Return ID.">
        <ItemTemplate>
        <asp:Label ID="lblRTicketNo" runat="server" Text='<%# Bind("RTicketNo") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

     
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

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Quantity">
        <ItemTemplate>
        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

            
        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Store Name">
        <ItemTemplate>
        <asp:Label ID="lblBranchName" runat="server" Text='<%# Bind("Br_Name") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>


        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Status">
        <ItemTemplate>
        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

         <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="ItemRegNo" Visible="false">
        <ItemTemplate>
        <asp:Label ID="lblItemRegNo" runat="server" Text='<%# Bind("ItemRegNo") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Outward TicketNo" Visible="false">
        <ItemTemplate>
        <asp:Label ID="lblTicketNo" runat="server" Text='<%# Bind("TicketNo") %>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>


        <asp:TemplateField>
        <HeaderTemplate>
        <asp:CheckBox ID="checkAll" runat="server" onclick = "checkVerifyAll(this);" />
        </HeaderTemplate> 
        <ItemTemplate>
        <asp:CheckBox ID="ChkVerify" runat="server" onclick = "ChkVerify_Click(this)" />
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




    <asp:HiddenField ID="hdnComID" runat="server" ClientIDMode="Static" Value=""  />
 
   
</div>


</asp:Content>
