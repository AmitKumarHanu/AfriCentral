<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmClientCompanyMaster.aspx.cs" Inherits="Afri_Central_Code.frmClientCompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:UpdatePanel runat="server" ID="UpdatePanel1">

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdClientDetails" />
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
                                <h3 style="font-size: 25px;" class="emov-applications-title">Company Client Details</h3>
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
                                                <input type="text" id="txtSearch" runat="server" class="emov-a-header-input" placeholder="Client ID / Client Name " /></td>
                                            <td></td>
                                            <td>
                                                <button id="btnSearch" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnSearchOpt">SEARCH <i class="fa fa-search" aria-hidden="true"></i></button>
                                            </td>
                                            <td>
                                                <button id="btnAddClient" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnAddOpt">ADD <i class="fa fa-plus" aria-hidden="true"></i></button>
                                            </td>
                                            <td></td>
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
                                                                    <asp:GridView ID="grdClientDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data"
                                                                        OnPageIndexChanging="grdClientDetails_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%"
                                                                        OnRowCommand="grdClientDetails_RowCommand"
                                                                        OnRowDataBound="grdClientDetails_RowDataBound"
                                                                        OnSelectedIndexChanged="grdClientDetails_SelectedIndexChanged">
                                                                        <AlternatingRowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Client ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_ID" runat="server" Text='<%# Bind("Client_ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Client Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_Name" runat="server" Text='<%# Bind("Client_Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Phone No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_PhoneNo" runat="server" Text='<%# Bind("Client_PhoneNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Email Address">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_Email" runat="server" Text='<%# Bind("Client_Email") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Country">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_Country" runat="server" Text='<%# Bind("Client_Country") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Website">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_Website" runat="server" Text='<%# Bind("Client_Website") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Related Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("Com_Name") %>'></asp:Label>
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
                <div style="margin-top: 5%; width: 100%; text-align: left;">
                    <span>
                        <div id="lblloginmsg" runat="server" class="my-notify-error" visible="false"></div>
                    </span><a href="#" class="txt3"></a>
                </div>

                <!-- Add Branch Details -->
                <asp:Panel ID="pnlAddDiv" runat="server" Style="display: none;">

                    <div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">
                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <%--  Add Branch Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <!--changes -->
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">Add Company Client  Details</h2>
                                </div>

                                <div class="emov-a-single-user-card-inner-info">


                                    <table class="emov-a-table-data1">
                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>

                                        <%--Client ID and Company Name --%>
                                        <tr>
                                            <td style="width: 20%;">
                                                <p>Client ID * :</p>
                                            </td>
                                            <td style="width: 20%;">
                                                <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input emov-a-header-input-mob" AutoPostBack="false" type="text" ID="txtClientID" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ErrorMessage="Client ID is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtClientID" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>
                                            <td style="width: 20%"></td>
                                            <td>
                                                <p><span>Company Name *:</span></p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpCompanyName" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ErrorMessage="Country name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpCompanyName" InitialValue="0" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Client Name  --%>
                                        <tr>
                                            <td>
                                                <p>Client Name* :</p>
                                            </td>
                                            <td colspan="4">
                                                <p>
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtClientName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Client Name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtClientName" ValidationGroup="AddSign" runat="server" />
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
                                                <p><span></span></p>
                                            </td>
                                            <td>
                                                <p></p>
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
                                                    <asp:TextBox runat="server" class="emov-a-slot-creation-time-input" Width="100%" AutoPostBack="false" type="text" ID="txtClientAddress" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="Client Address is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="txtClientAddress" ValidationGroup="AddSign" runat="server" />
                                                </p>
                                            </td>


                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Phone & Email--%>
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
                                                <p>Status * :</p>
                                            </td>
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






                                        <tr>
                                            <td colspan="2" style="height: 8px;"></td>

                                            <td colspan="3" style="height: 8px;">
                                                <asp:Button ID="btnBackSave" runat="server" class="login-btn" Style="float: right;" OnClick="btnBackSave_Click" Text="BACK" Visible="true" />
                                                <asp:Button ID="btnSaveBranch" runat="server" class="login-btn" Style="float: right;" OnClick="btnSaveBranch_Click" Text="SAVE" ValidationGroup="AddSign" Visible="true" />
                                            </td>
                                        </tr>
                                    </table>


                                </div>
                            </div>
                        </div>
                    </div>

                </asp:Panel>




                <!-- View Branch Details  -->
                <asp:Panel ID="pnlViewDiv" runat="server" Style="display: none;">
                    <!-- View Branch SLIDE IN -->

                    <div id="divView" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">

                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">
                            <div class="emov-a-single-user-card-cover">
                                <%--  View Branch Master ---%>
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">View Company Client  Details</h2>
                                </div>
                                <div class="emov-a-single-user-card-inner-info">

                                    <table class="emov-a-table-data1">
                                        <%-- [Com_ID],[Com_Name],[Com_Address],[Com_PhoneNoe],[Com_Email],[Com_Website],[Com_Country],--%>
                                        <%-- [Com_State],[Com_VATRegNo],[Com_VAT],[Com_Discount],[Com_Remark],[isActive]--%>
                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Client IF and Company Name --%>
                                        <tr>
                                            <td style="width: 20%;">
                                                <p>Client ID  :</p>
                                            </td>
                                            <td style="width: 20%;">
                                                <p>
                                                    <asp:Label ID="lblClientID" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                            <td style="width: 20%"></td>
                                            <td>
                                                <p>Company Name :</p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:Label ID="lblComName" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Client Name  --%>
                                        <tr>
                                            <td>
                                                <p>Client Name :</p>
                                            </td>
                                            <td colspan="3">
                                                <p>
                                                    <asp:Label ID="lblClientName" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                            <td style="width: 20%"></td>

                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Website Address --%>
                                        <tr>
                                            <td>
                                                <p>Website :</p>
                                            </td>
                                            <td colspan="3">
                                                <p>
                                                    <asp:Label ID="lblWebsiteAddress" runat="server" Text="" Visible="true" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                            <td style="width: 5%;"></td>

                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Address  --%>
                                        <tr>
                                            <td>
                                                <p>Address :</p>
                                            </td>
                                            <td colspan="4">
                                                <p>
                                                    <asp:Label ID="lblClientAddress" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>


                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>



                                        <%--Phone & Status--%>

                                        <tr>
                                            <td>
                                                <p>Phone No. :</p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:Label ID="lblPhoneNo" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                            <td style="width: 20%;"></td>
                                            <td>
                                                <p>Email :</p>
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

                                        <%--Country & State --%>
                                        <tr>
                                            <td>
                                                <p><span>Country Name :</span></p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:Label ID="lblCountryName" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                            <td style="width: 5%;"></td>
                                            <td>
                                                <p>State :</p>
                                            </td>
                                            <td>
                                                <p>
                                                    <asp:Label ID="lblStatus" runat="server" Text="" Height="1px"></asp:Label>
                                                </p>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="height: 8px;"></td>
                                            <td>
                                                <div style="margin-top: 0px;">
                                                    <asp:Button ID="BtnAddUser" runat="server" Text="Add User" Style="float: right;" Visible="true" class="login-btn" OnClick="BtnAddUser_Click" />
                                                </div>
                                            </td>
                                            <td>
                                                <div style="margin-top: 0px;">
                                                    <asp:Button ID="BtnBackFind" runat="server" Text="BACK" Style="float: right;" Visible="true" class="login-btn" OnClick="BtnBackFind_Click" />
                                                </div>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>

                </asp:Panel>




                <!-- Add Llgoin User Details -->
                <asp:Panel ID="pnlEdit" runat="server" Style="display: none;">

                    <div id="DivEdit" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">


                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">

                            <%--  Edit Branch Details Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label">Add Client Application Users Details </h2>
                                </div>
                                <div class="emov-a-single-user-card-inner-info">

                                    <table class="emov-a-table-data1">

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>


                                        <%--Country Name --%>
                                        <tr>
                                            <td>
                                                <p><span>Company Name *:</span></p>
                                            </td>
                                            <td colspan="2">
                                                <p>
                                                    <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpECompanyName" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="Company name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpECompanyName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>
                                            <td style="width: 5%;"></td>

                                        </tr>

                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>
                                        <%--Client Name--%>
                                        <tr>
                                            <td>
                                                <p><span>Client Name *:</span></p>
                                            </td>
                                            <td colspan="2">
                                                <p>
                                                    <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpEClientName" runat="server" OnSelectedIndexChanged="drpEClientName_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage="Client name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpEClientName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>
                                            <td style="width: 5%;"></td>

                                        </tr>


                                        <tr>
                                            <td colspan="5" style="height: 8px;"></td>
                                        </tr>


                                        <%--Clent Company Manage Login --%>
                                        <tr>
                                            <td>
                                                <p><span>Manager Name *:</span></p>
                                            </td>
                                            <td colspan="2">
                                                <p>
                                                    <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpEManagerName" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Manager name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpEManagerName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>
                                            <td style="width: 5%;"></td>

                                        </tr>

                                        <tr>
                                            <td colspan="2" style="height: 8px;"></td>

                                            <td colspan="3" style="height: 8px;">

                                                <asp:Button ID="BtnEBack" runat="server" Text="BACK" Style="float: right;" Visible="true" class="login-btn" OnClick="BtnEBack_Click" />

                                                <asp:Button ID="btnEClientUser" runat="server" Text="SAVE" Style="float: right;" Visible="true" class="login-btn" ValidationGroup="AddSign1" OnClick="btnEClientUser_Click" />

                                            </td>
                                        </tr>

                                    </table>

                                </div>

                            </div>

                            <!-- Data Grid Design-->
                            <div class="emov-a-rc-table-cover">

                                <div id="div1" runat="server">




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
                                                                        <asp:GridView ID="grdClientUser" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data"
                                                                            OnPageIndexChanging="grdClientUser_PageIndexChanging" PageSize="10" ShowHeaderWhenEmpty="true" Width="100%">
                                                                            <AlternatingRowStyle />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="ID">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="First Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFirst_Name" runat="server" Text='<%# Bind("First_Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Last Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLast_Name" runat="server" Text='<%# Bind("Last_Name") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Login ID">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Designation">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Group Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblGroupName" runat="server" Text='<%# Bind("GroupName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Client Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblClient_Name" runat="server" Text='<%# Bind("Client_Name") %>'></asp:Label>
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


                    </div>

                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
