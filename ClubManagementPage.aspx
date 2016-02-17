<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClubManagementPage.aspx.cs" Inherits="ClubManagementPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=iso-8859-1" http-equiv="content-type" />
    <link href="CSS/ModelCaseStyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Club Management</title>
</head>

<body>
    <form id="form1" runat="server">
        <div id="div_CONTAINER">


            <div id="div_HEADER">
                <div id="div_header_TEXT">
                    <h1>Club Management</h1>
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
                    Clubs
                </div>

                <div id="div_center_LISTBOX">
                    <asp:ListBox ID="listBoxClubs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="listBoxClubs_SelectedIndexChanged" CssClass="listbox_main"></asp:ListBox>
                </div>

                <div id="div_center_IMAGE">
                    <img id="main_image" src="images/team.png" alt="Club manegement image" />
                </div>
            </div>



            <div id="div_RIGHT">
                <div id="div_right_HEADER">
                    Club Details
                </div>

                <div id="div_right_DETAILS">

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbClubid" runat="server" Text="Club ID:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbClubid" runat="server" CssClass="detail_textbox" MaxLength="4"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorClubId"
                            runat="server" ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbClubid" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbClubName" runat="server" Text="Club name:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbName" runat="server" CssClass="detail_textbox" MaxLength="50"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbName" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbCity" runat="server" Text="City:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbCity" runat="server" CssClass="detail_textbox" MaxLength="10"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbCity" CssClass="validatorMessage">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbClubEmail" runat="server" Text="Email:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbEmail" runat="server" CssClass="detail_textbox" MaxLength="100"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server"
                            ErrorMessage="Required!" SetFocusOnError="True"
                            ControlToValidate="tbEmail" CssClass="validatorMessage"></asp:RequiredFieldValidator>
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

                    <asp:RangeValidator ID="RangeValidator_Clubid" runat="server"
                        ControlToValidate="tbClubid" ErrorMessage="Club number should be between 101 and 9999"
                        Type="Integer" MinimumValue="101" MaximumValue="9999"
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
