<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="frmSupplierMaster.aspx.cs" Inherits="Afri_Central_Code.frmSupplierMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel runat="server" ID="UpdatePanel1">

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdSupplierDetails" />
        </Triggers>
        <ContentTemplate>


            <div class="emov-page-container emov-step-wrapper">


                <!-- Main Panel -->
                <asp:Panel ID="pnlMain" runat="server" Style="display: block">

                    <div id="div_main" runat="server" class="emov-page-main emov-page-main-no-top-padding">
                        <!-- Header Menu-->
                        <div class="emov-a-rc-header emov-a-rc-header-seun">
                            <!-- Page Title-->
                            <div class="emov-header-page-title emov-header-page-title-table-vigo" id="emov-application-title">
                                <h3 style="font-size: 25px;" class="emov-applications-title">Supplier Master</h3>
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
                                                <input type="text" id="txtSearch" runat="server" class="emov-a-header-input" placeholder="Company Name / Supplier Name " /></td>
                                            <td></td>
                                            <td>
                                                <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnSearchOpt">SEARCH <i class="fa fa-search" aria-hidden="true"></i></button>
                                            </td>
                                            <td>
                                                <button id="btnAddSupplier" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnAddOpt" >ADD <i class="fa fa-plus" aria-hidden="true"></i></button>
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
                                                                    
                                                                    <asp:GridView ID="grdSupplierDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data"
                                                                        OnPageIndexChanging="grdSupplierDetails_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%"
                                                                        OnRowCancelingEdit="grdSupplierDetails_RowCancelingEdit"   
  
  OnRowEditing ="grdSupplierDetails_RowEditing" OnRowUpdating="grdSupplierDetails_RowUpdating">

                                                                        <AlternatingRowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Company Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompName" runat="server" Text='<%# Bind("Company") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <EditItemTemplate>  
                                                            <asp:TextBox ID="txt_Name" runat="server" Text='<%#Eval("Company") %>'></asp:TextBox>  
                                                            </EditItemTemplate>  
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Phone No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompanyPhoneNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                 <EditItemTemplate>  
                                                            <asp:TextBox ID="txt_Contact" runat="server" Text='<%#Eval("ContactNo") %>'></asp:TextBox>  
                                                            </EditItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Address">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompanyAddress" runat="server" Text='<%# Bind("CompanyAddress") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>  
                                                            <asp:TextBox ID="txt_CompanyAddress" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:TextBox>  
                                                            </EditItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Supplier">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <EditItemTemplate>  
                                                            <asp:TextBox ID="txt_Supplier" runat="server" Text='<%#Eval("SupplierName") %>'></asp:TextBox>  
                                                            </EditItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField  HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Action">  
                    <ItemTemplate >  
                        <asp:Button ID="btn_Edit" runat="server" CssClass="emov-dash-icon-cover1" style="margin-right: 10px;" ForeColor="salmon"  Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" CssClass="emov-dash-icon-cover1" style="margin-right: 10px;" ForeColor="green" runat="server" Text="Update"  CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" CssClass="emov-dash-icon-cover1" style="margin-right: 10px;" ForeColor="red" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  

                </asp:TemplateField> 
                           <asp:TemplateField>
                               <ItemTemplate>
                                    <asp:Label ID="lblupdate" Text="" runat="server"></asp:Label>
                               </ItemTemplate>
                              
                           </asp:TemplateField>                                                 
                <asp:TemplateField HeaderText="ID" Visible="false">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ID" runat="server" Text='<%#Eval("SupplierId") %>'></asp:Label>  
                    </ItemTemplate>  
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
                <div class="my-notify-error" runat="server" id="lblloginmsg" style="margin-top: 1%; width: 100%; text-align: left; display: none;"></div>

                <!-- Add Add Name -->
                <asp:Panel ID="pnlAddDiv" runat="server" Style="display: none">

                    <div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">        
                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <%--  Add User Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <!--changes -->
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">Add Supplier Master Details</h2>
                                </div>
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">
                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>

                                        
                                            
                                          <%--Company Name  --%>
                                        <tr>
                                            <td>  <p>Company Name* :</p>    </td>
                                            <td colspan="4">
                                                 <p>
                                              <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  type="text" ID="txtCompanyName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Country name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCompanyName" InitialValue="0" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>
                                         <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <p>Address * :</p>
                                            </td>
                                            <td colspan="4">
                                                <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtCompanyAddress" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Address ID is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtCompanyAddress" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>Phone No. * :</p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" ID="txtPhoneNo" />
                                                  
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Enter Contact Number only till 10 digit" ValidationGroup="AddSign" ForeColor="Red" ValidationExpression="[0-9]{10}" />
                                                </p>
                                            </td>


                                            

                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        
                                       
                                        <tr>
                                            <td>
                                                <p>Supplier * :</p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtSupplier" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Mobile No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtSupplier" ValidationGroup="AddSign" runat="server" />
                                                  
                                                </p>

                                            </td>
                                            <td style="width: 5%;"></td>

                                            
                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                          
                                        
                                       
                                        <tr>
                                            <td colspan="4" style="height: 8px;"></td>
                                            <td>
                                                <div style="margin-top: 0px;">
                                                    <asp:Button ID="btnBackAddUser" runat="server" class="login-btn"  style="margin-right: 10px;"  OnClick="btnBackAddUser_Click" Text="BACK" Visible="true" />
                                                
                                                    <asp:Button ID="btnSaveAddUser" runat="server" class="login-btn"  style="margin-right: 10px;"   OnClick="btnSaveAddUser_Click" Text="SAVE" ValidationGroup="AddSign" Visible="true" />
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
