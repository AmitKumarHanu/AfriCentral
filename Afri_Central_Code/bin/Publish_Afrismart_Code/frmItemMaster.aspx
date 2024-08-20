<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmItemMaster.aspx.cs" Inherits="Afri_Central_Code.frmItemMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

   
 

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css" type="text/css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js" type="text/javascript"></script>


<script type="text/javascript">

    function ShowImage(input) {
        if (input.files && input.files[0]) {

            var reader = new FileReader();
            reader.onload = function (e) {

                $('#<%=Imgitem.ClientID%>').prop('src', e.target.result)
                    .width(125)
                    .height(160);
            };
            reader.readAsDataURL(input.files[0]);

        }
    }


    function EditShowImage(input) {
        if (input.files && input.files[0]) {

            var reader = new FileReader();
            reader.onload = function (e) {
                $('#<%=ImgEitem.ClientID%>').prop('src', e.target.result)
                    .width(125)
                    .height(160);
            };
            reader.readAsDataURL(input.files[0]);

        }
    }


    $(function () {
        $(document).ready(function () {
            var today = new Date();
             //---Warranty Date Manufacture ------
            $("[id$=txtMfgDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                autoclose: true,
                endDate: "today",
                maxDate: today,
                yearRange: '-115:+10',
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 99999999999999);

                    }, 0);
                },
                onSelect: function (selected, evnt) {
                    $("[id$=hdnMfgDate]").val(selected);
                }
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');

            });

            $("[id$=txtMfgDate]").keyup(function () {
                if (this.value.match(/[^0-9]/g)) {
                    this.value = this.value.replace(/[^0-9^-]/g, '');
                }

            });


            //---Warranty Date Expiry ------
            $("[id$=txtExpDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: +1,
                yearRange: '-115:+10',
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 99999999999999);

                    }, 0);
                },
                onSelect: function (selected, evnt) {
                    $("[id$=hdnExpDate]").val(selected);
                }
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');

            });

            $("[id$=txtExpDate]").keyup(function () {
                if (this.value.match(/[^0-9]/g)) {
                    this.value = this.value.replace(/[^0-9^-]/g, '');
                }

            });



            //---Warranty Date Edit Manufacture ------
            $("[id$=txtEMfgDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                autoclose: true,
                endDate: "today",
                maxDate: today,
                yearRange: '-115:+10',
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 99999999999999);

                    }, 0);
                },
                onSelect: function (selected, evnt) {
                    $("[id$=hdnEMfgDate]").val(selected);
                }
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');

            });

            $("[id$=txtEMfgDate]").keyup(function () {
                if (this.value.match(/[^0-9]/g)) {
                    this.value = this.value.replace(/[^0-9^-]/g, '');
                }

            });


            //---Warranty Date Edit Expiry ------
            $("[id$=txtEExpDate]").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                minDate: +1,
                yearRange: '-115:+10',
                beforeShow: function () {
                    setTimeout(function () {
                        $('.ui-datepicker').css('z-index', 99999999999999);

                    }, 0);
                },
                onSelect: function (selected, evnt) {
                    $("[id$=hdnEExpDate]").val(selected);
                }
            }).on('changeDate', function (ev) {
                $(this).datepicker('hide');

            });

            $("[id$=txtEExpDate]").keyup(function () {
                if (this.value.match(/[^0-9]/g)) {
                    this.value = this.value.replace(/[^0-9^-]/g, '');
                }

            });


        });
    });


    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (prm != null) {
        prm.add_endRequest(function (sender, e) {
            if (sender._postBackSettings.panelsToUpdate != null) {
                $(function () {
                    $(document).ready(function () {
                        var today = new Date();
                       
                        //---Warranty Date Manufacture ------
                        $("[id$=txtMfgDate]").datepicker({
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                            autoclose: true,
                            endDate: "today",
                            maxDate: today,
                            yearRange: '-115:+10',
                            beforeShow: function () {
                                setTimeout(function () {
                                    $('.ui-datepicker').css('z-index', 99999999999999);

                                }, 0);
                            },
                            onSelect: function (selected, evnt) {
                                $("[id$=hdnMfgDate]").val(selected);
                            }
                        }).on('changeDate', function (ev) {
                            $(this).datepicker('hide');

                        });

                        $("[id$=txtMfgDate]").keyup(function () {
                            if (this.value.match(/[^0-9]/g)) {
                                this.value = this.value.replace(/[^0-9^-]/g, '');
                            }

                        });


                        //---Warranty Date Expiry ------
                        $("[id$=txtExpDate]").datepicker({
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                            minDate: +1,
                            yearRange: '-115:+10',
                            beforeShow: function () {
                                setTimeout(function () {
                                    $('.ui-datepicker').css('z-index', 99999999999999);

                                }, 0);
                            },
                            onSelect: function (selected, evnt) {
                                $("[id$=hdnExpDate]").val(selected);
                            }
                        }).on('changeDate', function (ev) {
                            $(this).datepicker('hide');

                        });

                        $("[id$=txtExpDate]").keyup(function () {
                            if (this.value.match(/[^0-9]/g)) {
                                this.value = this.value.replace(/[^0-9^-]/g, '');
                            }

                        });




                        //---Warranty Date Edit Manufacture ------
                        $("[id$=txtEMfgDate]").datepicker({
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                            autoclose: true,
                            endDate: "today",
                            maxDate: today,
                            yearRange: '-115:+10',
                            beforeShow: function () {
                                setTimeout(function () {
                                    $('.ui-datepicker').css('z-index', 99999999999999);

                                }, 0);
                            },
                            onSelect: function (selected, evnt) {
                                $("[id$=hdnEMfgDate]").val(selected);
                            }
                        }).on('changeDate', function (ev) {
                            $(this).datepicker('hide');

                        });

                        $("[id$=txtEMfgDate]").keyup(function () {
                            if (this.value.match(/[^0-9]/g)) {
                                this.value = this.value.replace(/[^0-9^-]/g, '');
                            }

                        });


                        //---Warranty Date Edit Expiry ------
                        $("[id$=txtEExpDate]").datepicker({
                            dateFormat: 'dd/mm/yy',
                            changeMonth: true,
                            changeYear: true,
                            minDate: +1,
                            yearRange: '-115:+10',
                            beforeShow: function () {
                                setTimeout(function () {
                                    $('.ui-datepicker').css('z-index', 99999999999999);

                                }, 0);
                            },
                            onSelect: function (selected, evnt) {
                                $("[id$=hdnEExpDate]").val(selected);
                            }
                        }).on('changeDate', function (ev) {
                            $(this).datepicker('hide');

                        });

                        $("[id$=txtEExpDate]").keyup(function () {
                            if (this.value.match(/[^0-9]/g)) {
                                this.value = this.value.replace(/[^0-9^-]/g, '');
                            }

                        });


                    });

                });
            }
        });
    };


</script>

<asp:UpdatePanel runat="server" ID="UpdatePanel1">
<Triggers><asp:AsyncPostBackTrigger ControlID="grditemDetails" /></Triggers>    
<Triggers>  <asp:PostBackTrigger ControlID="btnSaveAddItem" /> </Triggers>    
<Triggers>  <asp:PostBackTrigger ControlID="btnEUpdate" /> </Triggers>    

<ContentTemplate>


<div class="emov-page-container emov-step-wrapper">


<!-- Main Panel -->
<asp:Panel ID="pnlMain" runat="server" Style="display: block">

<div id="div_main" runat="server" class="emov-page-main emov-page-main-no-top-padding">
    <!-- Header Menu-->
    <div class="emov-a-rc-header emov-a-rc-header-seun">
        <!-- Page Title-->
        <div class="emov-header-page-title emov-header-page-title-table-vigo" id="emov-application-title">
            <h3 style="font-size: 25px;" class="emov-applications-title">Item Details</h3>
            <!-- Total Count-->
            <div class="emov-a-header-counter">
                <p>
                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                    Total
                </p>
            </div>
        </div>

        <div class="emov-a-header-action-group">
            <div class="emov-t-actions-group" style="display: block;">

                <table id="btnOpt" runat="server" style="width: 100%; display: block">
                    <tr>
                        <td>
                            <input type="text" id="txtSearch" runat="server" class="emov-a-header-input" placeholder="Item ID / Item Name / Barcode" /></td>
                        <td></td>
                        <td>
                            <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnSearchOpt">SEARCH <i class="fa fa-search" aria-hidden="true"></i></button>
                        </td>
                        <td>
                            <button id="btnAddCountry" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnAddOpt">ADD <i class="fa fa-plus" aria-hidden="true"></i></button>
                        </td>
                        <td>
                            <button id="btnUpdate" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnEditOpt">Edit <i class="fa fa-edit" aria-hidden="true"></i></button>
                        </td>
                    </tr>
                </table>


            </div>
        </div>

    </div>

    <!-- Data Grid Design-->
    <div class="emov-a-rc-table-cover">

        <div id="div_search_results" runat="server">
            <!-- table within data grid-->
            <table style="width: 100%; margin-top: -4%;">

                <caption>
                    <br />
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <div style="overflow-x: scroll; text-align: center">
                                                <%-- ,[Name],[Specification],[Quantity],[CostPrice],[SalesPrice],[Discount],[SupplierCode],[CategoryCode],--%>
                                                <asp:GridView ID="grditemDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data"
                                                    OnPageIndexChanging="grditemDetails_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%"
                                                    OnRowCommand="grditemDetails_RowCommand"
                                                    OnRowDataBound="grditemDetails_RowDataBound"
                                                    OnSelectedIndexChanged="grditemDetails_SelectedIndexChanged">
                                                    <AlternatingRowStyle />
                                                    <Columns>
                                                     <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Barcode.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIBarCodeNo" runat="server" Text='<%# Bind("BarCodeNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="item Details">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSpecification" runat="server" Text='<%# Bind("ItemSpecification") %>'></asp:Label>
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
                                                        
                                                        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Category Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>  

                                                        <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Supplier Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>  
                                            
                                                    </Columns>

                                                    <HeaderStyle CssClass="emov-a-home-table" />
                                                    <PagerStyle Height="100px" />
                                                    <RowStyle CssClass="emov-a-table-data" />
                                                    <EditRowStyle CssClass="emov-a-table-data" />
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center;">
                                                            No records found.
                                                        </div>
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

</div>
</asp:Panel>

<!-- Message Div -->
             <div style="margin-top:5%;width:100%;text-align:left; ">	<span>    <div id="lblloginmsg" runat="server" class="my-notify-error" Visible="false"></div></span><a href="#" class="txt3">		</a>		</div>

<!-- Add item Details -->
<asp:Panel ID="pnlAddDiv" runat="server" Style="display: none;">

<div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">      
    <!-- Pofile Blue Div -->
    <div class="emov-a-single-profile-cover">
       
        <!-- Item Photo -->
        <img id="Imgitem" src="Content/assets/images/ItemImage.jpg" runat="server" alt="" style="margin-bottom:11px; border-radius:12px; width:125px; height:155px;" />
        <!-- Item Photo -->                                     
        <div class="emov-a-single-user-uid-box">                         
        <p style="margin-left: 32px;">  <label class="btn btn-info btn-lg" >
        <i class="fas fa-folder-open"></i> Upload Item Image <asp:FileUpload ID="FileUpload1"  runat="server" Style="display: none;" onchange="ShowImage(this);"></asp:FileUpload>
      <%--  <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FileUpload1"   runat="server" Display="Dynamic" ForeColor="Red" EnableClientScript="true" ValidationGroup="File" />    --%>
        <asp:RegularExpressionValidator ID="ValidateEx" runat="server"   ForeColor="Red"  ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG|.jfif|.JFIF)$" ControlToValidate="FileUpload1" ValidationGroup="AddSign" ErrorMessage="Please select a Item image Jpegs, Pdf, png, and Gifs only"></asp:RegularExpressionValidator>
        </label> </p> 
        </div>  
        </div>


    <!-- White Div -->
    <div class="emov-a-profile-full-info" style="background: white">
        <%--  Add item Master ---%>
        <div class="emov-a-single-user-card-cover">
            <!--changes -->
            <div class="emov-a-single-user-card-header">
                <h2 class="h2label">Add item  Details</h2>
            </div>

            <div class="emov-a-single-user-card-inner-info">

<%--    [ID],[ItemRegNo],[InvoiceNo],[ItemName],[ItemSpecification],[Brand],[BarCodeNo],[Quantity],[CostPrice]
      ,[SalesPrice],[Discount],[DiscountAmount],[SupplierCode],[CategoryCode],[Mfgdate],[Expdate],[Warranty],[Image]--%>

                <table class="emov-a-table-data1">
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                      <%--Invoice Number & Brand  --%>
                    <tr>
                        <td>
                            <p>Invoice No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtInvoiceNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="InvoiceNo No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtInvoiceNo" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                           <td style="width: 4%"></td>
                        <td>
                            <p>Brand Name * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBrandName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ErrorMessage="Brand Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBrandName" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>

                    </tr>

                     <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--item Name  --%>
                    <tr>
                        <td>  <p>Item Name * :</p>    </td>
                        <td colspan="4">
                                <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtItemName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Item Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtItemName" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>


                    </tr>

                                       
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Item Specification --%>
                    <tr>
                        <td>
                            <p>Item Specification * :</p>
                        </td>
                        <td colspan="4">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtItemSpecification" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Item Specification is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtItemSpecification" ValidationGroup="AddSign" runat="server" />
                             </p>

                        </td>
                                                                  
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                        <%--BarCodeNo & Quantity  --%>
                    <tr>
                        <td>
                            <p>BarCode No./ SKU No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtBarCodeNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="BarCode No. \ SKU No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBarCodeNo" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                          <td style="width: 4%"></td>
                        <td>
                            <p>Quantity * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtItemQuantity" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ErrorMessage="Item quantity is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtItemQuantity" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--[CostPrice],[SalesPrice] --%>
                    <tr>
                        <td>
                            <p><span>Cost Price *:</span></p>
                        </td>
                        <td>                           
                            <p>
                               <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtCostPrice" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Item Cost Price is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCostPrice" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%;"></td>
                        <td>
                            <p>Sales Price * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtSalesPrice" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="Item Sales Price is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtSalesPrice" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--Discount &  Discount Amount --%>
                    <tr>
                        <td>    <p>Discount  (%) * :</p>      </td>
                        <td>    <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text"  Text="0"  ID="txtDiscount" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Discount % is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtDiscount" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%;"></td>
                        <td>    <p>Discount Amount :</p>      </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text"  Text="0.00" ID="txtDisAmount" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Discout Amount isrequired!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtDisAmount" ValidationGroup="AddSign" runat="server" />
                            </p> 
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--,SupplierCode,CategoryCode --%>
                    <tr>
                        <td style="width: 24%;">
                            <p>Supplier Name  * :   </p>
                        </td>
                        <td style="width: 24%;">
                            <p>
                               <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpSupplierName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ErrorMessage="Item supplier name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpSupplierName" InitialValue="0" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%"></td>
                        <td style="width: 24%;">
                            <p>Category Name  * :</p>
                        </td>
                        <td style="width: 24%;">
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpCategoryName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Item Category name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpCategoryName" InitialValue="0" ValidationGroup="AddSign" runat="server" />
                           </p>
                        </td>
                    </tr>
                                       

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>                                      
               
                                           
                        <%--,Warranty Details --%>
                    <tr>
                        <td style="width: 24%;">
                            <p>Warranty Details  * :   </p>
                        </td>
                        <td style="width: 24%;">
                            <p><asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text"  Text="0"  ID="txtWarranty" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ErrorMessage="Warranty mmpnths is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWarranty" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%"></td>
                        <td style="width: 24%;">
                            <p><asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtMfgDate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" ErrorMessage="Item manufacture date is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtMfgDate" ValidationGroup="AddSign" runat="server" />
                                <asp:HiddenField ID="hdnMfgDate" runat="server" Value="" />
                            </p>
                        </td>
                        <td style="width: 24%;">
                            <p><asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtExpDate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ErrorMessage="Item expiry date is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtExpDate" ValidationGroup="AddSign" runat="server" />
                                <asp:HiddenField ID="hdnExpDate" runat="server" Value="" />
                            </p>
                        </td>
                    </tr>
                       
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>   
                    
                    <tr>
                        <td colspan="3" style="height: 8px;"></td>
                        <td>
                            <div class="emov-a-single-user-final-actions-box emov-a-single-user-final-actions-box-login" style="margin-top: 0px;">
                                <asp:Button ID="btnBackAdditem" runat="server" class="login-btn" OnClick="btnBackAdditem_Click" Text="BACK" Visible="true" />
                            </div>
                        </td>
                        <td>
                            <div class="emov-a-single-user-final-actions-box emov-a-single-user-final-actions-box-login" style="margin-top: 0px;">
                                <asp:Button ID="btnSaveAddItem" runat="server" class="login-btn" OnClick="btnSaveAddItem_Click" Text="SAVE" ValidationGroup="AddSign" Visible="true" />
                            </div>
                        </td>
                      
                    </tr>
                               

                </table>

            </div>
        </div>
    </div>
</div>

</asp:Panel>




<!-- View item Details  -->
<asp:Panel ID="pnlViewDiv" runat="server" Style="display: none;">
<!-- View item SLIDE IN -->

<div id="divView" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">
       
    <!-- Pofile Blue Div -->
        <div class="emov-a-single-profile-cover">       
        <!-- Pofile Photo -->
        <img id="ItemVImage" src="Content/assets/images/ItemImage.jpg" runat="server" alt="" style="margin-bottom:11px; border-radius:12px; width:125px; height:155px;" />
        <!-- EmployeeID Photo -->                                     
        <div class="emov-a-single-user-uid-box">   <p><asp:Label ID="lblVItemID" runat="server" Text="" ></asp:Label></p>   </div>  
        </div>

    <!-- White Div -->
    <div class="emov-a-profile-full-info" style="background: white">
        <div class="emov-a-single-user-card-cover">
            <%--  View item Master ---%>
            <div class="emov-a-single-user-card-header">
                <h2 class="h2label">View item  Details</h2>
            </div>
            <div class="emov-a-single-user-card-inner-info">

                <table class="emov-a-table-data1">
                
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                              <%--Invoice No --%>
                    <tr>
                        <td style="width: 24%;"> <p>Invoice No. * :</p>  </td>
                        <td style="width: 24%;">
                            <p>  <asp:Label ID="lblInvoiceNo" runat="server" Text="" Height="1px"></asp:Label>  </p>
                        </td>
                        <td style="width: 4%"></td>
                      <td>
                            <p>Brand Name * :</p>
                        </td>
                        <td>
                            <p>  <asp:Label ID="lblBrandName" runat="server" Text="" Height="1px"></asp:Label>    </p>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                               <%--ItemID --%>
                    <tr>
                        <td style="width: 24%;"> <p>Item RegNo. * :</p>  </td>
                        <td style="width: 24%;">
                            <p>  <asp:Label ID="lblItemRegNo" runat="server" Text="" Height="1px"></asp:Label>  </p>
                        </td>
                        <td style="width: 4%"></td>
                        <td style="width: 24%;"></td>
                        <td style="width: 24%;"></td>
                    </tr>


                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                            <%--Item Name  --%>
                    <tr>
                        <td> <p>Item Name:</p>   </td>
                        <td>
                            <p>
                                <asp:Label ID="lblItemName" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 4%"></td>                                          

                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Item Specification --%>
                    <tr>
                        <td>  <p>Item Specification * :</p>   </td>
                        <td colspan="4"> <p>  <asp:Label ID="lblItemSpecification" runat="server" Text="" Visible="true" Height="1px"></asp:Label>   </p>      </td>
                        
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                      <%--BarCodeNo & Quantity  --%>
                    <tr>
                        <td>
                              <p>BarCode No./ SKU No. * :</p>
                        </td>
                          <td >
                            <p>
                                <asp:Label ID="lbltxtBarCodeNo" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                
                         <td style="width: 5%;"></td>
                        <td>
                             <p>Quantity * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblQuantity" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                    </tr>

                     <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                      <%--CostPrice & SalesPrice  --%>
                    <tr>
                        <td>
                              <p>CostPrice * :</p>
                        </td>
                          <td >
                            <p>
                                <asp:Label ID="lblCostPrice" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                
                         <td style="width: 5%;"></td>
                        <td>
                             <p>SalesPrice * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblSalesPrice" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                        <%--txtDiscount &  Tax --%>
                    <tr>
                        <td>
                           <p>Discount  (%) * :</p> 
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblDiscount" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 5%;"></td>
                        <td>
                             <p>Discount Amount * :</p> 
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblDisAmount" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                        <%--,SupplierCode,CategoryCode --%>
                    <tr>
                        <td>    <p>Supplier Name  * :   </p>    </td>
                        <td>  <p>   <asp:Label ID="lblSupplierName" runat="server" Text="" Height="1px"></asp:Label>    </p>      </td>
                        <td style="width: 4%"></td>
                        <td>       <p>  Category Name  * :   </p>   </td>
                        <td>
                            <p><asp:Label ID="lblCategoryName" runat="server" Text="" Height="1px"></asp:Label></p>                         
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>                   
                    <%--,Warranty Details --%>
                    <tr>
                        <td>  <p>Warranty Details * :   </p>    </td>
                        <td>  <p>   <asp:Label ID="lblWarranty" runat="server" Text="" Height="1px"></asp:Label>    </p>      </td>
                        <td style="width: 4%"></td>
                        <td>  <p>   <asp:Label ID="lblMfgDate" runat="server" Text="" Height="1px"></asp:Label>    </p>  </td>
                        <td>  <p>   <asp:Label ID="lblExpDate" runat="server" Text="" Height="1px"></asp:Label>    </p>                
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="height: 8px;"></td>
                        <td>
                            <div class="emov-a-single-user-final-actions-box emov-a-single-user-final-actions-box-login" style="margin-top: 0px;">
                                <asp:Button ID="BtnBackFind" runat="server" Text="BACK" Visible="true" class="login-btn" OnClick="BtnBackFind_Click" />
                            </div>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>

</div>

</asp:Panel>




<!-- Edit item Details -->
<asp:Panel ID="pnlEdit" runat="server" Style="display: none;">

<div id="DivEdit" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">
    <!-- Pofile Blue Div -->
    <div class="emov-a-single-profile-cover">
       
        <!-- Item Photo -->
        <img id="ImgEitem" src="Content/assets/images/ItemImage.jpg" runat="server" alt="" style="margin-bottom:11px; border-radius:12px; width:125px; height:155px;" />
        <!-- Item Photo -->                                     
        <div class="emov-a-single-user-uid-box">                         
        <p>  <label class="btn btn-info btn-lg" >
        <i class="fas fa-folder-open"></i> Upload Item Image <asp:FileUpload ID="FileUpload2"  runat="server" Style="display: none;" onchange="EditShowImage(this);"></asp:FileUpload>
      <%--  <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="FileUpload1"   runat="server" Display="Dynamic" ForeColor="Red" EnableClientScript="true" ValidationGroup="File" />    --%>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"   ForeColor="Red"  ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG|.jfif|.JFIF)$" ControlToValidate="FileUpload2" ErrorMessage="Please select a Item image Jpegs, Pdf, png, and Gifs only"></asp:RegularExpressionValidator>
        </label> </p> 
        </div>  
        </div>

                      
    <!-- White Div -->
    <div class="emov-a-profile-full-info" style="background: white">

        <%--  Edit item Details Master ---%>
        <div class="emov-a-single-user-card-cover">
            <div class="emov-a-single-user-card-header">
                <h2>Edit item Details </h2>
            </div>
            <div class="emov-a-single-user-card-inner-info">

                <table class="emov-a-table-data1">

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    
                      <%--Invoice Number & Item Reg No  --%>
                    <tr>
                        <td>
                            <p>Invoice No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtEInvoiceNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ErrorMessage="InvoiceNo No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEInvoiceNo" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                         <td style="width: 4%;"></td>
                        <td>
                            <p>Brand Name * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" AutoPostBack="false" type="text" ID="txtEBrandName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ErrorMessage="Brand Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEBrandName" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>

                    </tr>

                     <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                  
                    <%--ItemID --%>
                    <tr>
                        <td style="width: 24%;">   <p>Item ID * :</p>   </td>
                        <td style="width: 24%;">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEItemRegNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Item registration number is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEItemRegNo" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                         <td style="width: 4%"></td>
                        <td style="width: 24%;"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--item Name  --%>
                    <tr>
                        <td>  <p>Item Name * :</p>    </td>
                        <td colspan="4">
                                <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtEItemName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Item Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEItemName" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                    </tr>

                                       
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Item Specification --%>
                    <tr>
                        <td>
                            <p>Item Specification * :</p>
                        </td>
                        <td colspan="4">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtEItemSpecification" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Item Specification is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEItemSpecification" ValidationGroup="AddSign1" runat="server" />
                             </p>

                        </td>        
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                        <%--BarCodeNo & Quantity  --%>
                    <tr>
                        <td>
                            <p>BarCode No./ SKU No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtEBarCodeNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ErrorMessage="BarCode No. \ SKU No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEBarCodeNo" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                         <td style="width: 4%;"></td>
                        <td>
                            <p>Quantity * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false"  Width="100%"  type="text" ID="txtEItemQuantity" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="Item quantity is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEItemQuantity" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--[CostPrice],[SalesPrice] --%>
                    <tr>
                        <td>
                            <p><span>Cost Price *:</span></p>
                        </td>
                        <td>                           
                            <p>
                               <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtECostPrice" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="Item Cost Price is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtECostPrice" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%;"></td>
                        <td>
                            <p>Sales Price * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  Width="100%" AutoPostBack="false" type="text" ID="txtESalesPrice" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="Item Sales Price is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtESalesPrice" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--txtDiscount &  Tax --%>
                    <tr>
                        <td>    <p>Discount  (%) * :</p>      </td>
                        <td>    <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" Text="0" ID="txtEDiscount" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="Discount % is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEDiscount" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%;"></td>
                        <td>    <p>Discount Amount * :</p>      </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  Width="100%"  AutoPostBack="false"  Text="0.00"  type="text" ID="txtEDisAmount" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="Discount Amount is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEDisAmount" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--,SupplierCode,CategoryCode --%>
                    <tr>
                        <td style="width: 24%;">
                            <p>Supplier Name  * :   </p>
                        </td>
                        <td style="width: 24%;">
                            <p>
                               <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpESupplierName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ErrorMessage="Item supplier name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpESupplierName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%"></td>
                        <td style="width: 24%;">
                            <p>Category Name  * :</p>
                        </td>                      
                         <td style="width: 24%;">
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpECategoryName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" ErrorMessage="Item Category name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpECategoryName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                           </p>
                        </td>

                    </tr>
                                       

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    
                                           
                        <%--,Warranty Details --%>
                    <tr>
                        <td style="width: 24%;">
                            <p>Warranty Details  * :   </p>
                        </td>
                        <td style="width: 24%;">
                            <p><asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEWarranty" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" ErrorMessage="Warranty mmpnths is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEWarranty" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 4%"></td>
                        <td style="width: 24%;">
                            <p><asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEMfgDate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" ErrorMessage="Item manufacture date % is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEMfgDate" ValidationGroup="AddSign1" runat="server" />
                                   <asp:HiddenField ID="hdnEMfgDate" runat="server" Value="" />
                            </p>
                        </td>
                        <td style="width: 24%;">
                            <p><asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob"  Width="100%"  AutoPostBack="false" type="text" ID="txtEExpDate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" ErrorMessage="Item expiry date is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEExpDate" ValidationGroup="AddSign1" runat="server" />
                                  <asp:HiddenField ID="hdnEExpDate" runat="server" Value="" />
                            </p>
                        </td>
                    </tr>
                       
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>   

                    <tr>
                        <td colspan="3" style="height: 8px;"></td>
                        <td>
                            <div class="emov-a-single-user-final-actions-box emov-a-single-user-final-actions-box-login" style="margin-top: 0px;">
                                <asp:Button ID="BtnEBack" runat="server" Text="BACK" Visible="true" class="login-btn" OnClick="BtnEBack_Click" />
                            </div>
                        </td>
                        <td>
                            <div class="emov-a-single-user-final-actions-box emov-a-single-user-final-actions-box-login" style="margin-top: 0px;">
                                <asp:Button ID="btnEUpdate" runat="server" Text="UPDATE" Visible="true" class="login-btn" ValidationGroup="AddSign1" OnClick="btnEUpdate_Click" />
                            </div>
                        </td>
                    </tr>

                </table>

            </div>
        </div>
    </div>

</div>

</asp:Panel>

</div>
</ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
