<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" EnableEventValidation="false" CodeBehind="frmCategoryMaster.aspx.cs" Inherits="Afri_Central_Code.frmCategoryMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


<asp:UpdatePanel runat="server" ID="UpdatePanel1">

<Triggers>
<asp:AsyncPostBackTrigger ControlID="grdCategory" />
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
                                <h3 style="font-size: 25px;" class="emov-applications-title">Item Category </h3>
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
                                            <td>  <input type="text" id="txtSearch" runat="server" class="emov-a-header-input"  placeholder=" Category Name " /></td>
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
                                                                  
                                                                     <asp:GridView ID="grdCategory" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data" 
                                                                        OnPageIndexChanging="grdCategory_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%" 
                                                                        OnRowCommand="grdCategory_RowCommand" OnRowDataBound="grdCategory_RowDataBound" OnSelectedIndexChanged="grdCategory_SelectedIndexChanged">
                                                                        <AlternatingRowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Category Id" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCatId" runat="server" Text='<%# Bind("CID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Category Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
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

                    <div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding">        
                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <%--  Add Category Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <!--changes -->
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">Add item Category Details</h2>
                                </div>
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1" >
                                        <tr>
                                            <td colspan="2" style="height: 8px;"></td>
                                        </tr>
                                        <%--Company Name  --%>
                                        <tr>
                                            <td  >  <p>Category Name* :</p>    </td>
                                            <td >
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="true" type="text" ID="txtCategoryName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Item Category Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCategoryName" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="2" style="height: 8px;"></td>
                                        </tr>    
                                      <tr>
                                              <td  ></td>
                                            <td  style="height:8px;">                                             
                                                    <asp:Button ID="btnBackAddItem" runat="server" class="login-btn" OnClick="btnBackAddItem_Click"  style="float: right;" Text="BACK" Visible="true" />
                                          
                                                    <asp:Button ID="btnAddItem" runat="server" class="login-btn" OnClick="btnAddItem_Click"  style="float: right;"  Text="SAVE" ValidationGroup="AddSign" Visible="true"  />                                                
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
                                    <h2 class="h2label">View item Category Details</h2>
                                </div>
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">

                                        <%--Category ID  --%>
                                        <tr>
                                        <td colspan="2"><p></p></td>
                                        <td colspan="2"><p><asp:Label ID="lblCategoryID" runat="server" Text=""  Height="1px" Visible="false"></asp:Label></p></td>  
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>
                                        <%--Category Name  --%>
                                        <tr>
                                        <td  colspan="2">  <p>Category Name* :</p>    </td>
                                        <td colspan="2">
                                        <p>
                                        <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" ReadOnly="true"  AutoPostBack="false" type="text" ID="lblCategoryName" />
                                        </p>
                                        </td>
                                        </tr>  

                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr> 
                                        
                                        <tr>   
                                        <td colspan="3" style="height:8px;"></td>
                                        <td>  
                                        <div style="margin-top:10px;"  > 
                                            <asp:Button ID="BtnVBack" runat="server" Text="BACK" Visible="true" style="float: right;"  class="login-btn"     OnClick="BtnVBack_Click"/>   </div> 
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
                                    <h2 class="h2label">Edit item Category Details</h2>
                                </div>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">

                                          <%--Category ID  --%>
                                        <tr>
                                        <td colspan="2"><p></p></td>
                                        <td colspan="2"><p><asp:Label ID="txtECategoryID" runat="server" Text=""  Height="1px" Visible="false"></asp:Label></p></td>  
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>

                                      
                                        <%--Company Name  --%>
                                        <tr>
                                            <td  colspan="2">  <p>Category Name* :</p>    </td>
                                            <td colspan="2">
                                                 <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtECategoryName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Item Category Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtECategoryName" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>


                                        </tr>  
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                        </tr>    
                                        <tr>   
                                        <td colspan="3" style="height:8px;"></td>
                                        <td >  
                                            <div  style="margin-top:10px;"  > 
                                            <asp:Button ID="BtnEBack" runat="server" Text="BACK" Visible="true" class="login-btn"   style="float: right;"   OnClick="BtnEBack_Click"/>  
                                            <asp:Button ID="btnEUpdate" runat="server" Text="UPDATE" Visible="true" class="login-btn" style="float: right;"   ValidationGroup="AddSign1"   OnClick="btnEUpdate_Click"/> </div>
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

