using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RiderListsPage : System.Web.UI.Page
{
    private BrevetDAO brevetDAO = new BrevetDAO();
    private Brevet_RiderDAO brevet_riderDAO = new Brevet_RiderDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(false); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            createBrevetList(); // Populate Brevet List for the first time
        }

    }
    private void createBrevetList()
    {
        List<Brevet> brevetList = brevetDAO.GetAllBrevetsOrderedById();

        lbBrevetList.Items.Clear();
        if (brevetList == null)
        {
            lbBrevetList.Text = "DATABASE TEMPORARILY OUT OF USE (see Database.log)";
        }
        else
        {
            foreach (Brevet brevet in brevetList)
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
        List<Rider> riderList = brevet_riderDAO.GetAllBrevetParticipants(brevetId);

        lbRiderList.Items.Clear();
        if (riderList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Rider rider in riderList)
            {
                String text = rider.FamilyName + ", " + rider.GivenName + " (" + rider.Clubid + ")";

                ListItem listItem = new ListItem(text, "" + rider.Riderid);
                lbRiderList.Items.Add(listItem);
            }
        }
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
        hyperRiderManagement.Visible = false;
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
