/* *************************************************************************
 * HomePage.cs   Original version: Kari Silpiö 20.3.2014 v1.0
 *               Modified by: - 
 * -------------------------------------------------------------------------
 *  Application: DWA Model Case
 *  Class:       Code-behind class for the ASPX page HomePage.aspx
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************* */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin();
    }



    /* **********************************************************************
    * LOGIN MANAGEMENT CODE 
    * - This is the special code for the HOME PAGE only! 
    * - DO NOT change anything else but the HyperLink controls here!
    *   HyperLink controls are managed under comments (1), (2), and (3)
    *********************************************************************** */
    private void checkLogin()
    {
        Response.Cache.SetNoStore();   // Should disable browser's Back Button

        // (1) Hide all hyperlinks that are available for autenthicated users only
        hyperLinkBrevetRegistration.Visible = false;
        hyperLinkBrevetManagement.Visible = false;
        hyperRiderManagement.Visible = false;
        hyperClubManagement.Visible = false;
        hyperLinkUpdateResults.Visible = false;

        if (Session["username"] == null)
        {
            lbLoginInfo.Text = "You are not logged in";
            btLogout.Visible = false;
            
            // LOGIN PAGE only: Enable the login panel
            panelLogin.Enabled = true;
        }

        if (Session["username"] != null)
        {
            lbLoginInfo.Text = "You are logged in as " + Session["username"];
            btLogout.Visible = true;

            // LOGIN PAGE only: Disable the login panel' visibility 
            panelLogin.Visible = false;

            // (2) Show all hyperlinks that are available for autenthicated users only
            hyperLinkBrevetRegistration.Visible = true;
        }

        if (Session["administrator"] != null)
        {
            // (3) In addition, show all hyperlinks that are available for administrators only
            
            hyperLinkBrevetManagement.Visible = true;
            hyperRiderManagement.Visible = true;
            hyperClubManagement.Visible = true;
            hyperLinkUpdateResults.Visible = true;
        }
    }

    /// <summary>
    /// btLogin_Click: DO LOGIN
    /// </summary>
    protected void btLogin_Click(object sender, EventArgs e)
    {
        LoginDAO loginDAO = new LoginDAO();
        LoginRole loginRole;
        
        loginRole = loginDAO.GetLoginRole(tbUsername.Text, tbPassword.Text);

        if (loginRole.Role == null)
        {
            lbMessage.Text = "Username/password do not match. Try again.";
        }

        if (loginRole.Role != null)
        {
            Session["username"] = tbUsername.Text;

            if (loginRole.Role == "administrator")
            {
                Session["administrator"] = tbUsername.Text;
            }

            lbMessage.Text = "";
            Page.Response.Redirect("HomePage.aspx");
        }
    }

    /// <summary>
    /// btLogin_Click: DO LOGOUT
    /// </summary>
    protected void btLogout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Session["administrator"] = null;
        Page.Response.Redirect("HomePage.aspx");
    }
    /* LOGIN MANAGEMENT code ends here  */
}
// End
