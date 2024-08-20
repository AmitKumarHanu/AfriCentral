<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rtBankSalesReport.aspx.cs" Inherits="RPermit_Admin.rtBankSalesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
<%--<script src="https://code.jquery.com/jquery-1.12.4.js" type="text/javascript"></script>
<script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js" type="text/javascript"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>  --%>

<%-- date filter details--%>

<script type="text/javascript" src="Content/assets/scripts/jquery.min.js"></script>
<script type="text/javascript" src="Content/assets/scripts/moment.min.js"></script>
<script type="text/javascript" src="Content/assets/scripts/daterangepicker.min.js"></script>
<link rel="stylesheet" type="text/css" href="Content/assets/css/daterangepicker.css" />


   
    <%-- JQuery Date Fun  --%>
<script type="text/javascript">
$(function() {

var start = moment().subtract(29, 'days');
var end = moment();

function cb(start, end) {
    //$('#txtSearch span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    $('#txtSearch span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
}

$('#txtSearch').daterangepicker({
    startDate: start,
    endDate: end,
    ranges: {
        'Today': [moment(), moment()],
        'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        'Last 7 Days': [moment().subtract(6, 'days'), moment()],
        'Last 30 Days': [moment().subtract(29, 'days'), moment()],
        'This Month': [moment().startOf('month'), moment().endOf('month')],
        'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    }
}, cb);

cb(start, end);

    });

</script>
    <%--  Vildate Date fun--%>
<script type="text/Jscript"> 
    function getDtRange() {      
        $("#hfProduct").val($("#txtSearch").text()  ) ;        
       // alert('Hi 2 ' + $("#hfProduct").val());
            }
</script>
    
    <%--  Printer  fun--%>
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


      function PrintContentSummary() {
            var html = "<html>";
            html += document.getElementById("DivPrintSummary").innerHTML;
            html += "</html>";

            var printWin = window.open('', '', 'location=no,width=10,height=10,left=50,top=50,toolbar=no,scrollbars=no,status=0,titlebar=no');

            printWin.document.write(html);
            printWin.document.close();
            printWin.focus();
            printWin.print();
            printWin.close();
        }
     
</script>
    <%--  Convert html to PDF  fun--%>
<%-- <script src="https://code.jquery.com/jquery-1.12.4.js"></script>--%>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.3/jspdf.min.js"></script>
<script src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>
<script>
function getPDF(){

var HTML_Width = $(".canvas_div_pdf").width();
var HTML_Height = $(".canvas_div_pdf").height();
var top_left_margin = 15;
var PDF_Width = HTML_Width+(top_left_margin*2);
var PDF_Height = (PDF_Width*1.5)+(top_left_margin*2);
var canvas_image_width = HTML_Width;
var canvas_image_height = HTML_Height;
		
var totalPDFPages = Math.ceil(HTML_Height/PDF_Height)-1;
		

html2canvas($(".canvas_div_pdf")[0],{allowTaint:true}).then(function(canvas) {
canvas.getContext('2d');
			
console.log(canvas.height+"  "+canvas.width);
			
			
var imgData = canvas.toDataURL("image/jpeg", 1.0);
var pdf = new jsPDF('p', 'pt',  [PDF_Width, PDF_Height]);
pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin,canvas_image_width,canvas_image_height);
			
			
for (var i = 1; i <= totalPDFPages; i++) { 
	pdf.addPage(PDF_Width, PDF_Height);
	pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height*i)+(top_left_margin*4),canvas_image_width,canvas_image_height);
}
var n = Date.now();			
pdf.save("Bank_Report_" + n +".pdf");
});
};
</script>


<div class="emov-page-container emov-step-wrapper">

      <!-- Main Div -->
     <asp:Panel ID="pnlMain" runat="server"   style="display:block">

     <div id="div_main" runat="server" class="emov-page-main emov-page-main-no-top-padding">

     <!-- Header Menu-->
    <div class="emov-a-rc-header emov-a-rc-header-seun">
     <!-- Page Title-->
    <div  class="emov-header-page-title emov-header-page-title-table-vigo"   id="emov-application-title">
        <h3 style="font-size:25px;" class="emov-applications-title">Bank Report</h3>
        <!-- Total Count-->
        <div class="emov-a-header-counter">   <p><asp:Label ID="lbl_total" runat="server"></asp:Label> Total</p>  </div>  
    </div>

    <div class="emov-a-header-action-group">
        <div class="emov-t-actions-group" style="display:block;">  
        <table id="btnOpt" runat="server" style="width:100%; display:block">
        <tr>
       
        <td > <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onmousemove="getDtRange()"  onserverclick="BtnSearchOpt"   ><i class="fa fa-search" aria-hidden="true"></i> SEARCH </button></td>      
        <td > <button id="btnPrint" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;"  onserverclick="BtnPrinter" ><i class="fa fa-print" aria-hidden="true"></i>PRINTER </button></td>  
        <td > <button id="btnDownload" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;"  onclick="getPDF();"   ><i class="fa fa-download" aria-hidden="true"></i>DOWNLOAD </button></td>      
       
        </tr>
        </table>
        </div>
        </div>

    </div>    

    <div class="emov-a-rc-table-cover">  

        <table>    
        <tr>               
        <td><button id=""  class="emov-dash-icon-cover1" style="margin-right: 5px;" ><i class="fa fa-filter" aria-hidden="true"></i> FILTER BY :</button></td>
        <td ><asp:DropDownList ID="drpReportType" runat="server" class="emov-a-header-input"  width="100%"    name="ReportType">
                <asp:ListItem Value="0" Selected disabled>--Report Type--</asp:ListItem>
                <asp:ListItem Text="Summary Report " Value="Summary"></asp:ListItem>
                <asp:ListItem Text="Details Report " Value="Details"></asp:ListItem>                   
                </asp:DropDownList>
       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Report Type is required!" Display="Dynamic" ForeColor="Red"  ControlToValidate="drpReportType" InitialValue="0"  ValidationGroup="AddSign" runat="server" />--%>
        </td>
        <td><asp:DropDownList class="emov-a-header-input"  width="100%"   ID="drpLocation" runat="server" >   </asp:DropDownList>
       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Location is required!" Display="Dynamic" ForeColor="Red"  ControlToValidate="drpLocation" InitialValue="0"  ValidationGroup="AddSign" runat="server" /></td>--%>
    
        <td><asp:DropDownList class="emov-a-header-input"  width="100%"   ID="drpNationality" runat="server">   </asp:DropDownList>
       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Nationality is required!" Display="Dynamic" ForeColor="Red"  ControlToValidate="drpNationality" InitialValue="0"  ValidationGroup="AddSign" runat="server" /></td>--%>
    
        <td><div id="txtSearch" class="emov-a-header-input"  style="font-size:small; font-weight:lighter;"   ><i class="fa fa-calendar"></i>&nbsp;     <span></span> <i class="fa fa-caret-down"></i>    </div> </td>
        <td></td>
        </tr>
        <tr>
  
        <td colspan="6"><asp:HiddenField runat="server" ID="hfProduct" ClientIDMode="Static" /></td>
        </tr>
        </table>
       
         
    </div> 

    <!-- Data Grid Design-->
    <div class="emov-a-rc-table-cover">
                          
    <div id="div_Report" runat="server">
    <!-- table within data grid-->
  
        <div id="R1" align="center" style="overflow: scroll; height:350px;"  runat="server" >
    
        <div id="DivReport" style="width: 800px;">
          

         <div class="canvas_div_pdf">  

         <asp:panel id="pnlDetails" runat="server" Visible="false"  >
        <!-- Report Header-->
        <table style="width:100%;  align="center">

   
         <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
        <tr>     
             <!--changes -->
        <td align="center" class="h2label" colspan="8">BANK DETAILED REPORT</td>       
        </tr>
        
        <tr>
             <!--changes -->
        <td align="center" colspan="8"class="h2label" >( FROM  <asp:Label ID="lblFDate" runat="server"></asp:Label>   &nbsp;TO&nbsp;<asp:Label ID="lblTDate" runat="server"></asp:Label>   )</td>   
        </tr>
        <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
                            
        </table>
           
       
        <!-- Report Details-->
        <% 
        if (objDs.Rows.Count > 0)
        { %>
 <!--changes -->        <table Class="emov-a-table-data"  border="1" cellspacing="1"  rules="all" style="width: 100%; overflow: scroll; clip: rect(auto, auto, auto, auto);">
  <!--changes -->       <tr class="emov-a-table-heading">
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Sl.No.</th>                                                         
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Applicant Id</th>
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Applicant Name</th> 
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Gender</th>  
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Passport_No</th>   
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">CountryName</th> 
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Location</th>  
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">PaidOn</th>  
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Currency</th>  
        <th align="center" Class="emov-a-home-table" scope="col" style="white-space: nowrap;" valign="middle">Amount</th>    
        </tr>
                           
        <%  int c = 1;
            decimal T =0;

            for (int i = 0; i < objDs.Rows.Count; i++)
            {
                if (i>=0)
                {

        %>
        <tr class="Grid_Item_Alternaterow">
        <td align="center" valign="middle"> 
        <%=Convert.ToString(i+1)%>
        </td>
                                
        <td align="center" valign="middle">
        <%=Convert.ToString(objDs.Rows[i]["Applicant_Id"])%>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString(objDs.Rows[i]["AppName"])%> 
        </td>
                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objDs.Rows[i]["Gender"])%>
        </td>                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objDs.Rows[i]["Passport_No"])%>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString  (objDs.Rows[i]["CountryName"]) %>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString  (objDs.Rows[i]["ZoneName"]) %>
        </td>
         <td align="center" valign="middle"> 
        <%=Convert.ToDateTime  (objDs.Rows[i]["PaidOn"]).ToShortDateString() %>
        </td>
         <td align="center" valign="middle"> 
        <%=Convert.ToString  (objDs.Rows[i]["CurrencyType"]) %>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToDecimal  (objDs.Rows[i]["Payment_Cost"]) %>
        </td>
        </tr>            
        <%      
                    T = T + Convert.ToDecimal(objDs.Rows[i]["Payment_Cost"]);
                }
            }%>

        <tr class="Grid_Item_Alternaterow">
        <td colspan="9" align="right" valign="right" style="font-weight:bold" >Total &nbsp;  &nbsp;</td>
        <td  align="center"align="right" valign="right" style="font-weight:bold"> 
            <%=Convert.ToDecimal(T)%>
        </td>
        </table>
         <%}     %>                    
        <br />
        </asp:panel>    

         <asp:panel id="pnlSummary" runat="server" Visible="false" >
        <!-- Report Summary Header-->
        <table style="width:100%;" align="center" >   
        <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
        <tr>  
             <!--changes -->
        <td align="center" colspan="8" class="h2label">BANK SUMMARY REPORT</td>       
        </tr>
        
        <tr>
             <!--changes -->
        <td align="center" colspan="8" class="h2label">( FROM  <asp:Label ID="lblSDateF" runat="server"></asp:Label>   &nbsp;TO&nbsp;<asp:Label ID="lblSDateD" runat="server"></asp:Label>   )</td>   
        </tr>
        <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
                            
        </table>
       <!-- Report Summary Display-->
        
               <% 
        if (objSDs.Rows.Count > 0)
        { %>
              <!--changes -->
        <table Class="emov-a-table-data"   border="1" cellspacing="1"  rules="all" style="width: 100%; overflow: scroll; clip: rect(auto, auto, auto, auto);">
  <!--changes -->       <tr class="emov-a-table-heading">
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Sl.No.</th>   
        <th align="center"  scope="col" style="white-space: nowrap;" valign="middle">Date</th>                                                         
        <th align="center"  scope="col" style="white-space: nowrap;" valign="middle">Location</th>
        <th align="center"  scope="col" style="white-space: nowrap;" valign="middle">Nationality</th> 
        <th align="center"  scope="col" style="white-space: nowrap;" valign="middle">Count</th>  
        <th align="center"  scope="col" style="white-space: nowrap;" valign="middle">Amount</th>    
        </tr>
                           
        <%  int c = 1;
            decimal T =0;

            for (int i = 0; i < objSDs.Rows.Count; i++)
            {
                if (i>=0)
                {

        %>
        <tr class="Grid_Item_Alternaterow">
        <td align="center" valign="middle"> 
        <%=Convert.ToString(i+1)%>
        </td>
                                
        <td align="center" valign="middle">
        <%=Convert.ToString(objSDs.Rows[i]["Date"])%>     <%=Convert.ToString(objSDs.Rows[i]["MonthName"])%>    <%=Convert.ToString(objSDs.Rows[i]["Year"])%> 
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString(objSDs.Rows[i]["ZoneName"])%> 
        </td>
                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objSDs.Rows[i]["Nationality"])%>
        </td>                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objSDs.Rows[i]["Count"])%>
        </td>
       
        <td align="center" valign="middle"> 
        <%=Convert.ToDecimal  (objSDs.Rows[i]["Amount"]) %>
        </td>
        </tr>            
        <%      
                    T = T + Convert.ToDecimal(objSDs.Rows[i]["Amount"]);
                }
            }%>

        <tr class="Grid_Item_Alternaterow">
        <td colspan="5" align="right" valign="right" style="font-weight:bold" >Total &nbsp;  &nbsp;</td>
        <td  align="center"align="right" valign="right" style="font-weight:bold"> 
            <%=Convert.ToDecimal(T)%>
        </td>
        </table>
         <%}     %>                    
        <br />
        </asp:panel>  

        </div>
        </div>    </div>


    <!-- table within data grid-->
    </div>  </div>   

     </div>
     </asp:Panel>     
     
     <!-- Message Div -->
     <div  class="my-notify-error"  runat="server" id="DivMsg" style="margin-top:1%;width:100%;text-align:left; display:none;">   </div>


     <!-- Printing Div --> 
   
        <div id="DivPrint" style="width: 800px; display:none;">
         
        <asp:panel id="pnlPrntDetails" runat="server"   >
          <!-- Reoirt Header-->
        <table style="width:100%;" align="center">
        <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
        <tr>     
             <!--changes -->
        <td align="center" colspan="8" class="h2label">BANK REPORT</td>       
        </tr>        
        <tr>
             <!--changes -->
        <td align="center" colspan="8"class="h2label">( FROM  <asp:Label ID="lblFDateR" runat="server"></asp:Label>   &nbsp;TO&nbsp;<asp:Label ID="lblTDateR" runat="server"></asp:Label>   )</td>   
        </tr>
        <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>                            
        </table>
           
     

        <!-- Report Details-->
        <% 
        if (objDsRep.Rows.Count > 0)
        { %>
             <!--changes -->
        <table Class="emov-a-table-data"   border="1" cellspacing="1"  rules="all" style="width: 100%; overflow: scroll; clip: rect(auto, auto, auto, auto);">
  <!--changes -->       <tr class="emov-a-table-heading">
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Sl.No.</th>                                                         
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Applicant Id</th>
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Applicant Name</th> 
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Gender</th>  
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Passport_No</th>   
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">CountryName</th> 
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Location</th>  
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">PaidOn</th>  
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Currency</th>  
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Amount</th>    
        </tr>
                           
        <%  int c = 1;
             decimal T =0;                   
        for (int i = 0; i < objDsRep.Rows.Count; i++)
        {
        if (i>=0)
        {

        %>
        <tr class="Grid_Item_Alternaterow">
        <td align="center" valign="middle"> 
        <%=Convert.ToString(i+1)%>
        </td>
                                
        <td align="center" valign="middle">
        <%=Convert.ToString(objDsRep.Rows[i]["Applicant_Id"])%>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString(objDsRep.Rows[i]["AppName"])%> 
        </td>
                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objDsRep.Rows[i]["Gender"])%>
        </td>                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objDsRep.Rows[i]["Passport_No"])%>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString  (objDsRep.Rows[i]["CountryName"]) %>
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString  (objDsRep.Rows[i]["ZoneName"]) %>
        </td>
         <td align="center" valign="middle"> 
        <%=Convert.ToDateTime  (objDsRep.Rows[i]["PaidOn"]).ToShortDateString() %>
        </td>
         <td align="center" valign="middle"> 
        <%=Convert.ToString  (objDsRep.Rows[i]["CurrencyType"]) %>
        </td>
       <td align="center" valign="middle"> 
        <%=Convert.ToDecimal  (objDsRep.Rows[i]["Payment_Cost"]) %>
        </td>
        </tr>            
        <%      
                    T = T + Convert.ToDecimal(objDsRep.Rows[i]["Payment_Cost"]);
                }
            }%>

        <tr class="Grid_Item_Alternaterow">
        <td colspan="9" align="right" valign="right" style="font-weight:bold" >Total &nbsp;  &nbsp;</td>
        <td  align="center"align="right" valign="right" style="font-weight:bold"> 
            <%=Convert.ToDecimal(T)%>
        </td>
        </table>
         <%}     %>                    
        <br />
        </asp:panel>
        </div>
        <div id="DivPrintSummary" style="width: 800px; display:none;">
     
        <asp:panel id="pnlPrntSumary" runat="server" >
         <!-- Report Summary Header-->
        <table style="width:100%;" align="center" >   
        <tr>
            
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
        <tr> 
            <!--changes -->
        <td align="center" colspan="8" class="h2label">BANK SUMMARY REPORT</td>       
        </tr>
        
        <tr>
            <!--changes -->
        <td align="center" colspan="8" class="h2label">( FROM  <asp:Label ID="lblSDateFRep" runat="server"></asp:Label>   &nbsp;TO&nbsp;<asp:Label ID="lblSDateDRep" runat="server"></asp:Label>   )</td>   
        </tr>
        <tr>
        <td align="center" colspan="8">&nbsp;</td>
        </tr>
                            
        </table>
       <!-- Report Summary Display-->
        
               <% 
        if (objSDsRep.Rows.Count > 0)
        { %>
            <!--changes -->
        <table Class="emov-a-table-data"   border="1" cellspacing="1"  rules="all" style="width: 100%; overflow: scroll; clip: rect(auto, auto, auto, auto);">
  <!--changes -->      <tr class="emov-a-table-heading">
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Sl.No.</th>   
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Date</th>                                                         
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Location</th>
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Nationality</th> 
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Count</th>  
        <th align="center" scope="col" style="white-space: nowrap;" valign="middle">Amount</th>    
        </tr>
                           
        <%  int c = 1;
            decimal T =0;

            for (int i = 0; i < objSDsRep.Rows.Count; i++)
            {
                if (i>=0)
                {

        %>
        <tr class="Grid_Item_Alternaterow">
        <td align="center" valign="middle"> 
        <%=Convert.ToString(i+1)%>
        </td>
                                
        <td align="center" valign="middle">
        <%=Convert.ToString(objSDsRep.Rows[i]["Date"])%>     <%=Convert.ToString(objSDsRep.Rows[i]["MonthName"])%>    <%=Convert.ToString(objSDsRep.Rows[i]["Year"])%> 
        </td>
        <td align="center" valign="middle"> 
        <%=Convert.ToString(objSDsRep.Rows[i]["ZoneName"])%> 
        </td>
                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objSDsRep.Rows[i]["Nationality"])%>
        </td>                               
        <td align="center" valign="middle">
        <%=Convert.ToString(objSDsRep.Rows[i]["Count"])%>
        </td>
       
        <td align="center" valign="middle"> 
        <%=Convert.ToDecimal  (objSDsRep.Rows[i]["Amount"]) %>
        </td>
        </tr>            
        <%      
                    T = T + Convert.ToDecimal(objSDsRep.Rows[i]["Amount"]);
                }
            }%>

        <tr class="Grid_Item_Alternaterow">
        <td colspan="5" align="right" valign="right" style="font-weight:bold" >Total &nbsp;  &nbsp;</td>
        <td  align="center"align="right" valign="right" style="font-weight:bold"> 
            <%=Convert.ToDecimal(T)%>
        </td>
        </table>
         <%}     %>                    
        <br />
       </asp:panel>

        </div>
  
</div>


</asp:Content>
