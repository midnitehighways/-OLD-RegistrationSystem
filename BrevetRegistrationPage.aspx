<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrevetRegistrationPage.aspx.cs" Inherits="BrevetRegistrationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=iso-8859-1" http-equiv="content-type" />
    <link href="CSS/ModelCaseStyleSheet.css" rel="stylesheet" type="text/css" />
    <title>Brevet Registration</title>
</head>

<body>
    <form id="formBrevetRegistration" runat="server">
        <div id="div_CONTAINER">
            <div id="div_HEADER">
                <div id="div_header_TEXT">
                    <h1>Brevet Registration</h1>
                </div>

                <div id="div_header_LOGIN_STATUS">
                    <asp:Label ID="lbLoginInfo" runat="server"></asp:Label>.<br />
                    <asp:LinkButton ID="btLogout" runat="server" CssClass="logout_link" OnClick="btLogout_Click">LOGOUT</asp:LinkButton>
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
                <div id="div_center_HEADER">
                    Select a Brevet
                </div>
                <asp:ListBox ID="lbBrevetList" runat="server" Height="351px" Width="278px" AutoPostBack="True" OnSelectedIndexChanged="lbBrevetList_SelectedIndexChanged"></asp:ListBox>
                <br /><br />
                <img src="images/brevet.png" alt="Cyclists" /><br />
                <br />
            </div>


            <div id="div_RIGHT">
                <div id="div_right_HEADER">
                    Brevet Details
                </div>

                <div id="div_right_DETAILS">

                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbDistance" runat="server" Text="Distance:" CssClass="detail_label"></asp:Label>
                        <asp:DropDownList ID="ddlDistance" runat="server" Width="255px" Height="16px">
                            <asp:ListItem>200</asp:ListItem>
                            <asp:ListItem>300</asp:ListItem>
                            <asp:ListItem>400</asp:ListItem>
                            <asp:ListItem>600</asp:ListItem>
                            <asp:ListItem>1000</asp:ListItem>
                            <asp:ListItem>1200</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbDate" runat="server" Text="Date:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbDate" runat="server" CssClass="detail_textbox" MaxLength="15"></asp:TextBox>

                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbLocation" runat="server" Text="Location:" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbLocation" runat="server" CssClass="detail_textbox" MaxLength="15"></asp:TextBox>

                    </div>
                    <div class="div_right_details_ROW">
                        <asp:Label ID="lbClimbing" runat="server" Text="Climbing (m):" CssClass="detail_label"></asp:Label>
                        <asp:TextBox ID="tbClimbing" runat="server" CssClass="detail_textbox" MaxLength="15"></asp:TextBox>

                    </div>

                </div>
                <!-- End of div_right_DETAILS -->


                <div id="div_right_BUTTONS">
                    <asp:Button ID="btRegister" runat="server" Text="Click here to register" OnClick="btRegister_Click" CausesValidation="True" CssClass="div_right_buttons_button" Width="146px" />
                </div>


                <div id="div_right_VALIDATORS">
                    <div>
                        <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                    </div>
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
