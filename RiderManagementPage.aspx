<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RiderManagementPage.aspx.cs" Inherits="RiderManagementPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=iso-8859-1" http-equiv="content-type" />
    <link href="CSS/ModelCaseStyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Rider Management</title>
</head>

<body>
    <form id="form1" runat="server">
        <div id="div_CONTAINER">


            <div id="div_HEADER">
                <div id="div_header_TEXT">
                    <h1>Rider Management</h1>
                </div>

                <div id="div_header_LOGIN_STATUS">
                    <asp:Label ID="lbLoginInfo" runat="server"></asp:Label>.<br />
                    <asp:LinkButton ID="btLogout" runat="server" CssClass="logout_link" OnClick="btLogout_Click" CausesValidation="False">LOGOUT</asp:LinkButton>
                </div>
            </div>

            <div id="div_LEFT">
                <div id="div_NAV">
                <div>
                    <asp:HyperLink ID="hyperLinkHomePage" runat="server" CssClass="current_page_link" NavigateUrl="~/HomePage.aspx">Home</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperLinkRiderLists" runat="server" CssClass="other_page_link" NavigateUrl="~/RiderListsPage.aspx">Rider Lists</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperLinkBrevetResults" runat="server" CssClass="other_page_link" NavigateUrl="~/BrevetResultsPage.aspx">Brevet Results</asp:HyperLink><br />
                    <br />
                </div>

                <div>
                    <asp:HyperLink ID="hyperLinkBrevetRegistration" runat="server" CssClass="other_page_link" NavigateUrl="~/BrevetRegistrationPage.aspx">Brevet Registration</asp:HyperLink>
                    <br />
                    <br />
                </div>

                <div>
                    <asp:HyperLink ID="hyperLinkBrevetManagement" runat="server" CssClass="other_page_link" NavigateUrl="~/BrevetManagementPage.aspx">Brevet Management</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperRiderManagement" runat="server" CssClass="other_page_link" NavigateUrl="~/RiderManagementPage.aspx">Rider Management</asp:HyperLink><br />
                    <asp:HyperLink ID="hyperClubManagement" runat="server" CssClass="other_page_link" NavigateUrl="~/ClubManagementPage.aspx">Club Management</asp:HyperLink>
                    <br />
                    <br />
                </div>

                <div>
                    <asp:HyperLink ID="hyperLinkUpdateResults" runat="server" CssClass="other_page_link">Update Results</asp:HyperLink><br />
                </div>
            </div>
            </div>    



            <div id="div_CENTER">
                <div class="div_center_HEADER">
                    Riders
                </div>

                <div id="div_center_LISTBOX">
                    <asp:ListBox ID="listBoxRiders" runat="server" AutoPostBack="True" OnSelectedIndexChanged="listBoxRiders_SelectedIndexChanged" CssClass="listbox_main"></asp:ListBox>
                </div>

                <div id="div_center_IMAGE">
                    <img id="main_image" src="images/rider.png" alt="Rider management image" />
                </div>
            </div>



            <div id="div_RIGHT">
                <div id="div_right_HEADER">
                    Rider Details
                </div>

                <div id="div_right_DETAILS">

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbRiderId" runat="server" Text="Rider ID:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbRiderId" runat="server" CssClass="detail_textbox" MaxLength="4"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRiderId"
                            runat="server" ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbRiderId" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbSurname" runat="server" Text="Surname:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbSurname" runat="server" CssClass="detail_textbox" MaxLength="50"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbSurname" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbGivenName" runat="server" Text="Given name:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbGivenName" runat="server" CssClass="detail_textbox" MaxLength="10"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbGivenName" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbGender" runat="server" Text="Gender:" CssClass="detail_label"></asp:Label>
                        <asp:RadioButton ID="RadioButtonFemale" GroupName="gender" runat="server" Text="Female" />
                        <asp:RadioButton ID="RadioButtonMale" GroupName="gender" runat="server" Text="Male" />
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbPhone" runat="server" Text="Phone:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbPhone" runat="server" CssClass="detail_textbox" MaxLength="15"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbPhone" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbRiderEmail" runat="server" Text="Email:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbEmail" runat="server" CssClass="detail_textbox" MaxLength="100"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbEmail" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbClubName" runat="server" Text="Club name:" CssClass="detail_label"></asp:Label>
                        <asp:DropDownList ID="ddlClubName" runat="server" Width="255px" Height="16px"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorClubName" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="ddlClubName" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbUsername" runat="server" Text="Username:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbUsername" runat="server" CssClass="detail_textbox" MaxLength="15"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbUsername" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbPassword" runat="server" Text="Password:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbPassword" runat="server" CssClass="detail_textbox" MaxLength="15" CausesValidation="True" ValidationGroup="password"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbPassword" CssClass="validatorMessage"></asp:RequiredFieldValidator>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbRePassword" runat="server" Text="Re-enter pwd:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbRePassword" runat="server" CssClass="detail_textbox" MaxLength="15" CausesValidation="True" ValidationGroup="password"></asp:TextBox>

                        <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbRePassword" CssClass="validatorMessage" ErrorMessage="Not match!"></asp:CompareValidator>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbRole" runat="server" Text="Gender:" CssClass="detail_label"></asp:Label>
                        <asp:RadioButton ID="RadioButtonUser" GroupName="user_admin" runat="server" Text="Normal user" />
                        <asp:RadioButton ID="RadioButtonAdmin" GroupName="user_admin" runat="server" Text="Administrator" />
                    </div>



                </div>
                <!-- End of div_right_DETAILS -->


                <div id="div_right_BUTTONS">
                    <asp:Button ID="btNew" runat="server" Text="New" OnClick="btNew_Click" CausesValidation="False" CssClass="div_right_buttons_button" />
                    <asp:Button ID="btAdd" runat="server" Text="Add" OnClick="btAdd_Click" CausesValidation="True" CssClass="div_right_buttons_button" />
                    <asp:Button ID="btUpdate" runat="server" Text="Update" OnClick="btUpdate_Click" CausesValidation="True" CssClass="div_right_buttons_button" />
                    <asp:Button ID="btDelete" runat="server" Text="Delete" OnClick="btDelete_Click" CausesValidation="False" CssClass="div_right_buttons_button" />
                </div>


                <div id="div_right_VALIDATORS">
                    <div>
                        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                    </div>

                    <asp:RangeValidator ID="RangeValidator_Deptno" runat="server"
                        ControlToValidate="tbRiderId" ErrorMessage="Rider number should be between 10 and 99999"
                        Type="Integer" MinimumValue="10" MaximumValue="99999"
                        SetFocusOnError="True" CssClass="validatorMessage"></asp:RangeValidator>
                    <br />

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server"
                        ControlToValidate="tbEmail" ErrorMessage="Email is not correct"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        SetFocusOnError="True" CssClass="validatorMessage">
                    </asp:RegularExpressionValidator>

                </div>
                <!-- End of div_right_VALIDATORS -->
            </div>
            <!-- End of div_RIGHT -->



            <div id="div_FOOTER">
                <div id="div_footer_W3C_ICONS">
                    <a href="http://validator.w3.org/check?uri=referer">
                        <img class="w3c_icon" src="images/valid-xhtml10.png" alt="Valid XHTML 1.0 Transitional" /></a>
                    <a href="http://jigsaw.w3.org/css-validator/">
                        <img class="w3c_icon" src="images/vcss.png" alt="Valid CSS!" /></a>
                </div>

                <div id="div_footer_AUTHOR">
                    Alexandru Oat 2014 v1.0
                </div>
            </div>


        </div>
        <!-- End of div_CONTAINER -->
    </form>
</body>
</html>
