using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BrevetRegistrationPage : System.Web.UI.Page
{
    private BrevetDAO brevetDAO = new BrevetDAO();
    private Brevet_RiderDAO brevet_riderDAO = new Brevet_RiderDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(false);//true); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            viewStateNew();
            createBrevetList();  // Populate BrevetList for the first time
        }
    }

    protected void btRegister_Click(object sender, EventArgs e)
    {
        string Username = Session["username"].ToString();
        int Brevetid = Convert.ToInt32(lbBrevetList.SelectedValue);
        int result = brevet_riderDAO.InsertBrevetRegistration(Username, Brevetid);
        if (result == -1)
            showErrorMessage("Database error!");
    }

    private void createBrevetList()
    {
        List<Brevet> BrevetList = brevetDAO.GetAllBrevetsOrderedById();

        lbBrevetList.Items.Clear();
        if (BrevetList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Brevet brevet in BrevetList)
            {
                String text = brevet.Distance + " km: " + brevet.BrevetDate.Day + "-" + brevet.BrevetDate.Month + "-" + brevet.BrevetDate.Year + ", " + brevet.Location;
                ListItem listItem = new ListItem(text, "" + brevet.Brevetid);
                lbBrevetList.Items.Add(listItem);
            }
        }
    }
    protected void lbBrevetList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int brevetId = Convert.ToInt32(lbBrevetList.SelectedValue);
        Brevet brevet = brevetDAO.GetBrevetByBrevetId(brevetId);

        if (brevet != null)
        {
            //resetForm();
            modelToScreen(brevet);
            viewStateDetailsDisplayed();
            showNoMessage();
        }
    }


    private void modelToScreen(Brevet brevet)
    {
        ddlDistance.SelectedValue = brevet.Distance.ToString();
        tbDate.Text = brevet.BrevetDate.Day + "-" + brevet.BrevetDate.Month + "-" + brevet.BrevetDate.Year;
        tbLocation.Text = brevet.Location;
        tbClimbing.Text = brevet.Climbing.ToString();
    }

    private void resetForm()
    {
        ddlDistance.SelectedIndex = 0;
        tbDate.Text = "";
        tbLocation.Text = "";
        tbClimbing.Text = "";
    }

    private void showErrorMessage(String message)
    {
        lbMessage.Text = message;
        lbMessage.ForeColor = System.Drawing.Color.Red;
    }

    private void showNoMessage()
    {
        lbMessage.Text = "";
        lbMessage.ForeColor = System.Drawing.Color.Black;
    }

    private void viewStateDetailsDisplayed()
    {
        btRegister.Enabled = true;
    }

    private void viewStateNew()
    {
        btRegister.Enabled = false;

//        resetForm();
        lbBrevetList.SelectedIndex = -1;
        showNoMessage();
    }


    /* **********************************************************************
* LOGIN MANAGEMENT CODE 
* - This is the special code to be used on your ASPX pages.
* - DO NOT change anything else but the HyperLink controls here!
*   HyperLink controls are managed under comments (1), (2), and (3)
*********************************************************************** */
    private void checkLogin(bool loginRequired)
    {
        Response.Cache.SetNoStore();    // Should disable browser's Back Button

        // (1) Hide all hyperlinks that are available for autenthicated users only
        hyperLinkBrevetRegistration.Visible = false;
        hyperLinkBrevetManagement.Visible = false;
        //hyperBrevetManagement.Visible = false;
        hyperClubManagement.Visible = false;
        hyperLinkUpdateResults.Visible = false;

        if (loginRequired == true && Session["username"] == null)
        {
            Page.Response.Redirect("HomePage.aspx");  // Jump to the login page.
        }

        if (Session["username"] == null)
        {
            lbLoginInfo.Text = "You are not logged in";
            btLogout.Visible = false;
        }

        if (Session["username"] != null)
        {

            lbLoginInfo.Text = "You are logged in as " + Session["username"];
            btLogout.Visible = true;

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

    protected void btLogout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Session["administrator"] = null;
        Page.Response.Redirect("HomePage.aspx");
    }
    /* LOGIN MANAGEMENT code ends here  */




}