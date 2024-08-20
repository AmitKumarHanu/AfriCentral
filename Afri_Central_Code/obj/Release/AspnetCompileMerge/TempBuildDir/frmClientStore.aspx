<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frmClientStore.aspx.cs" Inherits="Afri_Central_Code.frmClientStore" %>

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
                                <h3 style="font-size: 25px;" class="emov-applications-title">Client Store Details</h3>
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
                                                <button id="btnAddOpt" type="button" runat="server" class="emov-dash-icon-cover1" style="margin-right: 10px;" onserverclick="BtnAddOpt">ADD STORE <i class="fa fa-plus" aria-hidden="true"></i></button>
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

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" Visible="false" HeaderText="Country">
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

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" Visible="false" HeaderText="Related Company">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("Com_Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="No of Stores">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStoreNo" runat="server" Text='<%# Bind("StoreNo") %>'></asp:Label>
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

                <!-- View Client Store Details -->
                <asp:Panel ID="pnlViewDiv" runat="server" Style="display: none;">

                    <div id="DivView" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">

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
                                                                    <asp:GridView ID="grdClientStore" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="False" Border="0px" BorderColor="White" CssClass="emov-a-table-data"
                                                                        PageSize="10" ShowHeaderWhenEmpty="true" Width="100%">
                                                                        <AlternatingRowStyle />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Store ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_ID" runat="server" Text='<%# Bind("Br_ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Store Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClient_Name" runat="server" Text='<%# Bind("Br_Name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-CssClass="emov-a-table-heading" HeaderText="Store Address">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBr_Address" runat="server" Text='<%# Bind("Br_Address") %>'></asp:Label>
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


                        <div class="emov-a-single-user-card-inner-info">
                            <table>
                                <tr>
                                    <td colspan="5" style="height: 8px;"></td>
                                </tr>



                                <tr>
                                    <td colspan="4" style="height: 8px;"></td>
                                    <td>
                                        <div style="margin-top: 0px;">
                                            <asp:Button ID="BtnBackView" runat="server" Text="BACK" Style="float: right;" Visible="true" class="login-btn" OnClick="BtnBackView_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>


                <!-- Add Llgoin User Details -->
                <asp:Panel ID="pnlAddDiv" runat="server" Style="display: none;">

                    <div id="DivAdd" runat="server" class="emov-page-main emov-page-main-no-top-padding" width="100%">


                        <!-- White Div -->
                        <div class="emov-a-profile-full-info" style="background: white">

                            <%--  Edit Branch Details Master ---%>
                            <div class="emov-a-single-user-card-cover">
                                <div class="emov-a-single-user-card-header">
                                    <h2 class="h2label"> Client Store Details </h2>
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
                                                    <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpEClientName" runat="server"></asp:DropDownList>
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
                                                <p><span>Store Name *:</span></p>
                                            </td>
                                            <td colspan="2">
                                                <p>
                                                    <asp:DropDownList class="emov-a-slot-creation-time-input emov-a-header-input-mob" Width="100%" ID="drpEStoreName" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="Store name is required!" Display="Dynamic" ForeColor="Red" ControlToValidate="drpEStoreName" InitialValue="0" ValidationGroup="AddSign1" runat="server" />
                                                </p>
                                            </td>
                                            <td style="width: 5%;"></td>

                                        </tr>

                                        <tr>
                                            <td colspan="2" style="height: 8px;"></td>

                                            <td colspan="3" style="height: 8px;">

                                                <asp:Button ID="BtnEBack" runat="server" Text="BACK" Style="float: right;" Visible="true" class="login-btn" OnClick="BtnEBack_Click" />

                                                <asp:Button ID="btnAddStore" runat="server" Text="SAVE" Style="float: right;" Visible="true" class="login-btn" ValidationGroup="AddSign1" OnClick="btnAddStore_Click" />

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
