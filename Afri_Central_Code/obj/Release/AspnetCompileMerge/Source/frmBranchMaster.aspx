<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="frmBranchMaster.aspx.cs" EnableEventValidation="false"  Inherits="Afri_Central_Code.frmBranchMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
<asp:UpdatePanel runat="server" ID="UpdatePanel1">

<Triggers>
<asp:AsyncPostBackTrigger ControlID="grdBranchDetails" />
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
            <h3 style="font-size: 25px;" class="emov-applications-title">Branch Details</h3>
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
                            <input type="text" id="txtSearch" runat="server" class="emov-a-header-input" placeholder="Branch Name / VAT Reg No. " /></td>
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
                    <%-- ,[Com_Name],[Com_Address]     ,[Com_PhoneNo],[Com_Website]  ,[Com_Country],[Com_State],[Com_LogoImage],isActive--%>
                    <asp:GridView ID="grdBranchDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data"
                        OnPageIndexChanging="grdBranchDetails_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%"
                        OnRowCommand="grdBranchDetails_RowCommand"
                        OnRowDataBound="grdBranchDetails_RowDataBound"
                        OnSelectedIndexChanged="grdBranchDetails_SelectedIndexChanged">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrID" runat="server" Text='<%# Bind("Br_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrName" runat="server" Text='<%# Bind("Br_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Phone No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrPhoneNo" runat="server" Text='<%# Bind("Br_PhoneNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrCountry" runat="server" Text='<%# Bind("Br_Country") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="VAT Reg. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrVATRegNo" runat="server" Text='<%# Bind("Br_VATRegNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                                                       

                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Currency">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
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

<!-- Add Branch Details -->
<asp:Panel ID="pnlAddDiv" runat="server" Style="display: none;">

<div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">        
    <!-- White Div -->
    <div class="emov-a-profile-full-info" style="background: white">
        <%--  Add Branch Master ---%>
        <div class="emov-a-single-user-card-cover">
            <!--changes -->
            <div class="emov-a-single-user-card-header">
                <h2 class="h2label">Add Branch  Details</h2>
            </div>

            <div class="emov-a-single-user-card-inner-info">


                <table class="emov-a-table-data1">
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--BranchID --%>
                    <tr>
                        <td style="width: 20%;">   <p>Branch ID * :</p>   </td>
                        <td style="width: 20%;">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtBranchID" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Branch ID is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBranchID" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 20%"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Branch Name  --%>
                    <tr>
                        <td>  <p>Branch Name* :</p>    </td>
                        <td colspan="4">
                                <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtBranchName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Branch Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBranchName" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>


                    </tr>

                                       
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Website Address --%>
                    <tr>
                        <td>
                            <p>Website * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtWebsiteAddress" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ErrorMessage="Website address is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtWebsiteAddress" ValidationGroup="AddSign" runat="server" />
                                <%--<asp:RegularExpressionValidator ID="rvDigits" runat="server" ControlToValidate="txtWebsiteAddress" ErrorMessage="Enter website address only till 10 digit" ValidationGroup="AddSign"   ForeColor="Red" ValidationExpression="[0-9]{11}" /> --%>
                            </p>

                        </td>
                        <td style="width: 5%;"></td>
                         <td>
                            <p><span>Currency *:</span></p>
                        </td>
                        <td>                           
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpCurrency" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ErrorMessage="Currency name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpCurrency" InitialValue="0" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                                            
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                        <%--Address  --%>
                    <tr>
                        <td>
                            <p>Address * :</p>
                        </td>
                        <td colspan="4">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtBranchAddress" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Address ID is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtBranchAddress" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>


                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Country & State --%>
                    <tr>
                        <td>
                            <p><span>Country Name *:</span></p>
                        </td>
                        <td>                           
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpCountryName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Country name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpCountryName" InitialValue="0" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 5%;"></td>
                        <td>
                            <p>State * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtState" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ErrorMessage="State name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtState" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--Com_VATRegNo  --%>
                    <tr>
                        <td>    <p>VAT Reg. No * :</p>      </td>
                        <td>    <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtVATRegNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="VAT registration number is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtVATRegNo" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                        <td style="width: 20%;"></td>
                        <td>    <p>Status * :</p>      </td>
                        <td>
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" name="Gender" Width="100%" ID="drpStatus" runat="server">
                                    <asp:ListItem Value="2" Selected disabled>--Select Status--</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Deactive</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Login status is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpStatus" InitialValue="2" ValidationGroup="AddSign" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--,[Br_VAT],[Br_Discount] --%>
                    <tr>
                        <td style="width: 20%;">
                            <p>VAT (%) * :   </p>
                        </td>
                        <td style="width: 20%;">
                            <p>
                                <asp:TextBox ID="txtVATPercentage" runat="server" AutoPostBack="false" class="emov-a-slot-creation-time-input emov-a-header-input-mob" type="text" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtVATPercentage" Display="Dynamic" ErrorMessage="VAT % is required!" ForeColor="Red" ValidationGroup="AddSign" />
                            </p>
                        </td>
                        <td style="width: 20%"></td>
                        <td style="width: 20%;">
                            <p>  Discount (%) * :   </p>
                        </td>
                        <td style="width: 20%;">
                            <p>
                                <asp:TextBox ID="txtDiscountPercentage" runat="server" AutoPostBack="false" class="emov-a-slot-creation-time-input emov-a-header-input-mob" type="text" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDiscountPercentage" Display="Dynamic" ErrorMessage="Discount % is required!" ForeColor="Red" ValidationGroup="AddSign" />
                            </p>
                        </td>
                    </tr>
                                       

                        <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                                       
                    <%--Phone & Status--%>
                    <tr>
                        <td>
                            <p>Phone No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" ID="txtPhoneNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ErrorMessage="Phone No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNo" ValidationGroup="AddSign" runat="server" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Enter phone number  only 11 digit" ValidationGroup="AddSign" ForeColor="Red" ValidationExpression="[0-9]{11}" />
                            </p>
                        </td>


                        <td style="width: 5%;"></td>
                        <td>
                            <p>Email * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEmail" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ErrorMessage="Emaild Address is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmail" ValidationGroup="AddSign" runat="server" />
                                <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ValidationGroup="AddSign" ForeColor="Red" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                            </p>
                        </td>

                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>                                          

                    
                    
                    <tr>
                         <td colspan="2" style="height: 8px;"></td>
                       
                        <td colspan="3" style="height: 8px;">                        
                                <asp:Button ID="btnBackSave" runat="server" class="login-btn"  style="float: right;"  OnClick="btnBackSave_Click" Text="BACK" Visible="true" />                           
                                <asp:Button ID="btnSaveBranch" runat="server" class="login-btn"  style="float: right;"  OnClick="btnSaveBranch_Click" Text="SAVE" ValidationGroup="AddSign" Visible="true" />
                          </td>
                        </tr>
                    </table>
                        

            </div>
        </div>
    </div>
</div>

</asp:Panel>




<!-- View Branch Details  -->
<asp:Panel ID="pnlViewDiv" runat="server" Style="display:none;">
<!-- View Branch SLIDE IN -->

<div id="divView" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">
                    
    <!-- White Div -->
    <div class="emov-a-profile-full-info" style="background: white">
        <div class="emov-a-single-user-card-cover">
            <%--  View Branch Master ---%>
            <div class="emov-a-single-user-card-header">
                <h2 class="h2label">View Branch  Details</h2>
            </div>
            <div class="emov-a-single-user-card-inner-info">

                <table class="emov-a-table-data1">
                   <%-- [Com_ID],[Com_Name],[Com_Address],[Com_PhoneNoe],[Com_Email],[Com_Website],[Com_Country],--%>
                   <%-- [Com_State],[Com_VATRegNo],[Com_VAT],[Com_Discount],[Com_Remark],[isActive]--%>
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                            <%--BranchID --%>
                    <tr>
                        <td style="width: 20%;"> <p>Branch ID * :</p>  </td>
                        <td style="width: 20%;">
                            <p>  <asp:Label ID="lblBranchID" runat="server" Text="" Height="1px"></asp:Label>  </p>
                        </td>
                        <td style="width: 20%"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                    </tr>


                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                            <%--Branch Name  --%>
                    <tr>
                        <td> <p>Branch Name:</p>   </td>
                        <td>
                            <p>
                                <asp:Label ID="lblBranchName" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%"></td>                                          

                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                      <%--Website Address --%>
                    <tr>
                        <td>  <p>Website * :</p>   </td>
                        <td>  <p>  <asp:Label ID="lblWebsiteAddress" runat="server" Text="" Visible="true" Height="1px"></asp:Label>   </p>      </td>
                        <td style="width: 5%;"></td>
                         <td>  <p>Currency * :</p>    </td>
                        <td>   <p>   <asp:Label ID="lblCurrency" runat="server" Text="" Height="1px"></asp:Label>     </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                      <%--Address  --%>
                    <tr>
                        <td>
                              <p>Address * :</p>
                        </td>
                          <td colspan="4">
                            <p>
                                <asp:Label ID="lblBranchAddress" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                
                        
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                       <%--Country & State --%>
                    <tr>
                        <td>
                            <p><span>Country Name *:</span></p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblCountryName" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 5%;"></td>
                        <td>
                             <p>State * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblState" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                       <%--VATRegNo  --%>
                    <tr>
                        <td>  <p>VAT Reg. No * :</p>     </td>
                        <td>  <p>   <asp:Label ID="lblVATRegNo" runat="server" Text="" Height="1px"></asp:Label>    </p>      </td>
                        <td style="width: 20%"></td>
                        <td>     <p>Status * :</p>    </td>
                        <td>
                            <p><asp:Label ID="lblStatus" runat="server" Text="" Height="1px"></asp:Label></p>                         
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                      <%--,[VAT],[Discount] --%>

                    <tr>
                        <td>
                            <p>VAT (%) * :   </p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblVATPercentage" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%;"></td>
                        <td>
                               <p>  Discount (%) * :   </p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblDiscountPercentage" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--Phone & Status--%>

                    <tr>
                        <td>
                            <p>Phone No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblPhoneNo" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%;"></td>
                        <td>
                               <p>Email * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:Label ID="lblEmail" runat="server" Text="" Height="1px"></asp:Label>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                 

                    <tr>
                        <td colspan="4" style="height: 8px;"></td>
                        <td>
                            <div  style="margin-top: 0px;">
                                <asp:Button ID="BtnBackFind" runat="server" Text="BACK" style="float: right;"  Visible="true" class="login-btn" OnClick="BtnBackFind_Click" />
                         </div>
                        </td>
                    </tr>

                </table>
            </div>
        </div>
    </div>

</div>

</asp:Panel>




<!-- Edit Branch Details -->
<asp:Panel ID="pnlEdit" runat="server" Style="display: none;">

<div id="DivEdit" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">

                      
    <!-- White Div -->
    <div class="emov-a-profile-full-info" style="background: white">

        <%--  Edit Branch Details Master ---%>
        <div class="emov-a-single-user-card-cover">
            <div class="emov-a-single-user-card-header">
                <h2  class="h2label">Edit Branch Details </h2>
            </div>
            <div class="emov-a-single-user-card-inner-info">

                <table class="emov-a-table-data1">

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--BranchID --%>
                    <tr>
                        <td style="width: 20%;">   <p>Branch ID * :</p>   </td>
                        <td style="width: 20%;">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEBranchID" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Branch ID is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEBranchID" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 20%"></td>
                        <td style="width: 20%;"></td>
                        <td style="width: 20%;"></td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Branch Name  --%>
                    <tr>
                        <td>  <p>Branch Name* :</p>    </td>
                        <td colspan="4">
                                <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%"  AutoPostBack="false" type="text" ID="txtEBranchName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ErrorMessage="Branch Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEBranchName" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>


                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Website Address --%>
                    <tr>
                        <td>
                            <p>Website * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEWebsiteAddress" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Website address is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEWebsiteAddress" ValidationGroup="AddSign1" runat="server" />
                                <%--<asp:RegularExpressionValidator ID="rvDigits" runat="server" ControlToValidate="txtWebsiteAddress" ErrorMessage="Enter website address only till 10 digit" ValidationGroup="AddSign"   ForeColor="Red" ValidationExpression="[0-9]{11}" /> --%>
                            </p>

                        </td>
                        <td style="width: 5%;"></td>
                        <td>
                            <p><span>Currency *:</span></p>
                        </td>
                        <td>                           
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpECurrency" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ErrorMessage="Currency name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpECurrency" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                                            
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                        <%--Address  --%>
                    <tr>
                        <td>
                            <p>Address * :</p>
                        </td>
                        <td colspan="4">
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtEBranchAddress" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="Address ID is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEBranchAddress" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>


                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                    <%--Country & State --%>
                    <tr>
                        <td>
                            <p><span>Country Name *:</span></p>
                        </td>
                        <td>                           
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpECountryName" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="Country name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpECountryName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 5%;"></td>
                        <td>
                            <p>State * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEState" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ErrorMessage="State name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEState" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--VATRegNo  --%>
                    <tr>
                        <td>    <p>VAT Reg. No * :</p>      </td>
                        <td>    <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEVATRegNo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ErrorMessage="VAT registration number is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEVATRegNo" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                        <td style="width: 20%;"></td>
                        <td>    <p>Status * :</p>      </td>
                        <td>
                            <p>
                                <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" name="Gender" Width="100%" ID="drpEStatus" runat="server">
                                    <asp:ListItem Value="2" Selected disabled>--Select Status--</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Deactive</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ErrorMessage="Login status is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpEStatus" InitialValue="2" ValidationGroup="AddSign1" runat="server" />
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    <%--,[VAT],[Discount] --%>
                    <tr>
                        <td style="width: 20%;">
                            <p>VAT (%) * :   </p>
                        </td>
                        <td style="width: 20%;">
                            <p>
                                <asp:TextBox ID="txtEVATPercentage" runat="server" AutoPostBack="false" class="emov-a-slot-creation-time-input emov-a-header-input-mob" type="text" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtEVATPercentage" Display="Dynamic" ErrorMessage="VAT % is required!" ForeColor="Red" ValidationGroup="AddSign1" />
                            </p>
                        </td>
                        <td style="width: 20%"></td>
                        <td style="width: 20%;">
                            <p>  Discount (%) * :   </p>
                        </td>
                        <td style="width: 20%;">
                            <p>
                                <asp:TextBox ID="txtEDiscountPercentage" runat="server" AutoPostBack="false" class="emov-a-slot-creation-time-input emov-a-header-input-mob" type="text" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtEDiscountPercentage" Display="Dynamic" ErrorMessage="Discount % is required!" ForeColor="Red" ValidationGroup="AddSign1" />
                            </p>
                        </td>
                    </tr>
                                       

                        <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>
                                       
                    <%--Phone & Status--%>
                    <tr>
                        <td>
                            <p>Phone No. * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" ID="txtEPhoneNo" />
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Phone No. is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtPhoneNo" ValidationGroup="AddSign" runat="server" />--%>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEPhoneNo" ErrorMessage="Enter phone number only till 11 digit" ValidationGroup="AddSign1" ForeColor="Red" ValidationExpression="[0-9]{11}" />
                            </p>
                        </td>


                        <td style="width: 5%;"></td>
                        <td>
                            <p>Email * :</p>
                        </td>
                        <td>
                            <p>
                                <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtEEmail" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ErrorMessage="Emaild Address is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtEEmail" ValidationGroup="AddSign1" runat="server" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEEmail" ValidationGroup="AddSign1" ForeColor="Red" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                            </p>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="5" style="height: 8px;"></td>
                    </tr>

                    
                     


                    <tr>
                        <td colspan="2" style="height: 8px;"></td>
                       
                        <td colspan="3" style="height: 8px;">
                           
                                <asp:Button ID="BtnEBack" runat="server" Text="BACK" style="float: right;"  Visible="true" class="login-btn" OnClick="BtnEBack_Click" />
                           
                                <asp:Button ID="btnEUpdate" runat="server" Text="UPDATE" style="float: right;"  Visible="true" class="login-btn" ValidationGroup="AddSign1" OnClick="btnEUpdate_Click" />
                       
                        </td>
                    </tr>

                </table>

            </div>

        </div>

    </div>


</div>

    </asp:Panel>

</ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
