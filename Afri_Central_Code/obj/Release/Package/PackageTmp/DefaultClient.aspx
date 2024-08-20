<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultClient.aspx.cs" Inherits="Afri_Central_Code.DefaultClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
        <!-- Store By Sales -->
<script type="text/javascript">
    //----------PieChart Design-----------------
    // Load the Visualization API and the corechart package.
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {
      
        var inputVal = document.getElementById('<%=lblZoneCode.ClientID %>').innerText;
        var ClientVal = document.getElementById('<%=lblClientID.ClientID %>').innerText;
     

        try {
            // Create the data table.
            $.ajax({
                type: "POST",
                url: "DefaultClient.aspx/GetStoreClientSalesChart",
                data: JSON.stringify({ ZoneCode: inputVal, ClientID: ClientVal }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {

                    drawchartClient(response.d);
                },
                error: function () {
                    alert("Error 1 loading data...........");
                },
                404: function () {
                    alert("Page fun GetDate not call ..........");
                },
                complete: function () {
                    if (!weHaveSuccess) {
                        alert('Success!');
                    }
                }

            });
        } catch (ex) {
            //alert(ex);
        }

    };
    function drawchartClient(dataValues) {

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        // alert('Hi');

        var data = new google.visualization.DataTable();
        data.addColumn('string', 'StoreName');
        data.addColumn('number', 'Amount');
        for (var i = 0; i < dataValues.length; i++) {
            data.addRow([dataValues[i].StoreName, dataValues[i].Amount]);

        }
        // Instantiate and draw our chart, passing in some options
        var chart = new google.visualization.PieChart(document.getElementById('PieChartClientAmountSales'));

        chart.draw(data,
            {
                title: "Client Sales Statics ",
                legend: { position: 'bottom' },
                fontsize: "10px",
                chartArea: { width: '90%' },
                pieHole: 0.6,

                //is3D: true,
                //legend: 'none'
            });
    }

</script>


    <div class="emov-page-main">
        <br />
        <br />

        <div class="emov-page-heading">
            <h2>Welcome To <span>
                <asp:Label ID="lblZoneName" runat="server" Text="" Height="1px"></asp:Label></span></h2>
        </div>
              

        <div class="emov-page-main" >
             <h2><span>  <asp:Label ID="lblClientName" runat="server" Text="" Height="1px"></asp:Label></span> </h2>
        </div>
        <!-- 1nd Chart-->
        <div class="emov-dash-stats-charts-wrapper" style="border-radius: 20%;">
             <div class="donut-bg"></div>
            <div class="donut-bg">
                <div id="PieChartClientAmountSales" style="width: 100%; height: 500px"></div>
            </div>
            <div class="donut-bg"></div>


          
        </div>
          <br />
  <br />
 
         <asp:Label  ID="lblClientID" runat="server" Font-Size="Medium" Text=""  Font-Italic="true" Font-Bold="false" Visible="true" ForeColor="white" BackColor="White"></asp:Label>
         <asp:Label  ID="lblZoneCode" runat="server" Font-Size="Medium"  Font-Italic="true" Font-Bold="false" Visible="true" ForeColor="white" BackColor="White"></asp:Label>
                  
    </div>
</asp:Content>
