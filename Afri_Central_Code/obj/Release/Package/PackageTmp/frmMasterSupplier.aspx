<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="frmMasterSupplier.aspx.cs" Inherits="Afri_Central_Code.frmMasterSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
<asp:UpdatePanel runat="server" ID="UpdatePanel1">

<Triggers>
<asp:AsyncPostBackTrigger ControlID="grdSupplier" />
</Triggers>
<ContentTemplate>

    <div class="emov-page-container emov-step-wrapper">


<!-- Main Panel -->

        <div class="emov-page-container emov-step-wrapper">


                <!-- Main Panel -->
                <asp:Panel ID="pnlMain" runat="server" Style="display: block">

                    <div id="div_main" runat="server" class="emov-page-main emov-page-main-no-top-padding">
                        <!-- Header Menu-->
                        <div class="emov-a-rc-header emov-a-rc-header-seun">
                            <!-- Page Title-->
                            <div class="emov-header-page-title emov-header-page-title-table-vigo" id="emov-application-title">
                                <h3 style="font-size: 25px;" class="emov-applications-title">Supplier Master </h3>
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
                                      <!-- Header Menu Options -->
                                    <table id="btnOpt" runat="server" style="width: 100%; display: block">
                                        <tr>
                                            <td>  <input type="text" id="txtSearch" runat="server" class="emov-a-header-input"  placeholder=" Supplier Name " /></td>
                                            <td></td>
                                            <td> <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1"  style="margin-right: 10px;" onserverclick="BtnSearchOpt">SEARCH <i class="fa fa-search" aria-hidden="true"></i></button> </td>
                                            <td>  <button id="btnAddCategory" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnAddOpt">ADD <i class="fa fa-plus" aria-hidden="true"></i></button> </td>
                                             <td><button id="btnUpdate" type="button" runat="server"  class="emov-dash-icon-cover1" style="margin-right: 10px;"   onserverclick="BtnEditOpt"  >Edit <i class="fa fa-edit" aria-hidden="true"></i></button></td> 
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
                                                                    <%--  [CategoryId],[CategoryName]--%>
                                                                  
                                                                     <asp:GridView ID="grdSupplier" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data" 
                                                                        OnPageIndexChanging="grdSupplier_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%" 
                                                                        OnRowCommand="grdSupplier_RowCommand" OnRowDataBound="grdSupplier_RowDataBound" OnSelectedIndexChanged="grdSupplier_SelectedIndexChanged">
                                                                        <AlternatingRowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Supplier Id" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSupplierId" runat="server" Text='<%# Bind("SId") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Category Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                                                                </ItemTemplate>         
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Contact No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                                </ItemTemplate>         
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Company Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                                                </ItemTemplate>         
                                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Company Address">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompanyAddress" runat="server" Text='<%# Bind("CompanyAddress") %>'></asp:Label>
                                                                                </ItemTemplate>         
                                                                                        <ItemStyle HorizontalAlign="Center" />
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

                    </div>
                </asp:Panel>

                <!-- Message Div -->
                 
                    <div style="margin-top:5%;width:100%;text-align:left; ">	<span>   
                        <div id="lblloginmsg" runat="server" class="my-notify-error" Visible="false"></div></span>
                        <a href="#" class="txt3">		</a>		</div>	   	    </div>


      <!-- Add Category -->
        <asp:Panel ID="pnlAddDiv" runat="server" Style="display: none">

                    <div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">        
                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <%--  Add Category Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <!--changes -->
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">Add item Supplier Details</h2>
                                </div>
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>
                                        <%--Supplier Name  --%>
                                        <tr>
                                            <td  >  <p>Suppier Name* :</p>    </td>
                                            <td >
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="true" type="text" ID="txtSupplierName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Supplier Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtSupplierName" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 
                                           <%--ContactNo  --%>
                                        <tr>
                                            <td  >  <p>Contact No.* :</p>    </td>
                                            <td >
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="true" type="text" ID="txtContactNo" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Supplier contact number is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtContactNo" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 

                                           <%--CompanyName  --%>
                                        <tr>
                                            <td  >  <p>Company Name* :</p>    </td>
                                            <td >
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="true" type="text" ID="txtCompanyName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Company name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCompanyName" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 


                                           <%--CompanyAddress  --%>
                                        <tr>
                                            <td  >  <p>Company Address* :</p>    </td>
                                            <td >
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="true" type="text" ID="txtCompanyAddress" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Company addresse is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCompanyAddress" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 

                                      <tr>
                                            
                                            <td colspan="2" style="height:8px;">
                                                <div  style="margin-top: 10px;">
                                                     <asp:Button ID="btnAddItem" runat="server" class="login-btn" OnClick="btnAddItem_Click"  style="float: right;"   Text="SAVE" ValidationGroup="AddSign" Visible="true"  />
                                                    <asp:Button ID="btnBackAddItem" runat="server" class="login-btn" OnClick="btnBackAddItem_Click" style="float: right;"   Text="BACK" Visible="true" />  
                                                </div>
                                            </td>
                                        </tr>
                               

                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>

  
     <!-- View Category  -->      
        <asp:Panel ID="pnlViewDiv" runat="server"   style="display:none">
            
        <div id="DivView" runat="server"  class="emov-page-main emov-page-main-no-top-padding"  width="100%"    >
        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <%--  View Category Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <!--changes -->
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">View item Suplier Details</h2>
                                </div>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">

                                        <%--Supplier ID  --%>
                                        <tr>
                                        <td colspan="2"><p></p></td>
                                        <td colspan="2"><p><asp:Label ID="lblSupplierId" runat="server" Text=""  Height="1px" Visible="false"></asp:Label></p></td>  
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>
                                        <%--Supplier Name  --%>
                                        <tr>
                                        <td  colspan="2">  <p>Supplier Name* :</p>    </td>
                                        <td colspan="2">
                                        <p>
                                        <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" ReadOnly="true"  AutoPostBack="false" type="text" ID="lblSupplierName" />
                                        </p>
                                        </td>
                                        </tr>  

                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 
                                        
                                          <%--ContactNo  --%>
                                        <tr>
                                        <td  colspan="2">  <p>Contact No.* :</p>    </td>
                                        <td colspan="2">
                                        <p>
                                        <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" ReadOnly="true"  AutoPostBack="false" type="text" ID="lblContactNo" />
                                        </p>
                                        </td>
                                        </tr>  

                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 
                                          <%--Company Name  --%>
                                        <tr>
                                        <td  colspan="2">  <p>Company Name* :</p>    </td>
                                        <td colspan="2">
                                        <p>
                                        <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" ReadOnly="true"  AutoPostBack="false" type="text" ID="lblCompany" />
                                        </p>
                                        </td>
                                        </tr>  

                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 
                                          <%--CompanyAddress  --%>
                                        <tr>
                                        <td  colspan="2">  <p>Address* :</p>    </td>
                                        <td colspan="2">
                                        <p>
                                        <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" ReadOnly="true"  AutoPostBack="false" type="text" ID="lblCompanyAddress" />
                                        </p>
                                        </td>
                                        </tr>  

                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 

                                        <tr>   
                                        <td colspan="3" style="height:8px;"></td>
                                        <td>  
                                        <div   style="margin-top:10px;"  > 
                                            <asp:Button ID="BtnVBack" runat="server" Text="BACK" Visible="true" class="login-btn"   style="float: right;"    OnClick="BtnVBack_Click"/>   </div> 
                                        </td>
                                      
                                        </tr>
                               

                                    </table>

                                </div>
                            </div>
                        </div>

        </div>
      
        </asp:Panel>  
                
      
     <!-- Edit Add Name -->      
        <asp:Panel ID="pnlEdit" runat="server"   style="display:none">
            
        <div id="DivEdit" runat="server"  class="emov-page-main emov-page-main-no-top-padding"  width="100%"    >
        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <%--  Edit Category Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <!--changes -->
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">Edit item Supplier Details</h2>
                                </div>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">

                                          <%--Supplier ID  --%>
                                        <tr>
                                        <td colspan="2"><p></p></td>
                                        <td colspan="2"><p><asp:Label ID="txtESupplierId" runat="server" Text=""  Height="1px" Visible="false"></asp:Label></p></td>  
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>

                                      
                                        <%--SupplierName  --%>
                                        <tr>
                                            <td  colspan="2">  <p>Category Name* :</p>    </td>
                                            <td colspan="2">
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtESupplierName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Supplier Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtESupplierName" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>    

                                             <%--ContactNo  --%>
                                        <tr>
                                            <td  colspan="2">  <p>Contact No.* :</p>    </td>
                                            <td colspan="2">
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtEContactNo" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Contact number is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEContactNo" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>  

                                             <%--Company  --%>
                                        <tr>
                                            <td  colspan="2">  <p>Company Name* :</p>    </td>
                                            <td colspan="2">
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtECompanyName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Company name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtECompanyName" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>  

                                             <%--SupplierName  --%>
                                        <tr>
                                            <td  colspan="2">  <p>Address* :</p>    </td>
                                            <td colspan="2">
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtECompanyAddress" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage="Supplier Addressame is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtECompanyAddress" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>  
                                        <tr>   
                                        <td colspan="2" style="height:8px;"></td>
                                         
                                        <td >  
                                            <div style="margin-top:10px;"  > 
                                            <asp:Button ID="BtnEBack" runat="server" Text="BACK" Visible="true" class="login-btn"    style="float: right;"   OnClick="BtnEBack_Click"/>   
                                            <asp:Button ID="btnEUpdate" runat="server" Text="UPDATE" Visible="true" class="login-btn"  style="float: right;"   ValidationGroup="AddSign1"   OnClick="btnEUpdate_Click"/> </div>
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
