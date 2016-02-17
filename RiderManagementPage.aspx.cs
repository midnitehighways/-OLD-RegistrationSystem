using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RiderManagementPage : System.Web.UI.Page
{
    private RiderDAO riderDAO = new RiderDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(true); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            viewStateNew();
            createRiderList();  // Populate RiderList for the first time
            createClubList();   // Populate ddlClubList
        }

        addButtonScripts();
    }


    private void addButtonScripts()
    {
        btDelete.Attributes.Add("onclick",
          "return confirm('Are you sure you want to delete the data?');");
    }

    private void createRiderList()
    {
        List<Rider> RiderList = riderDAO.GetAllRidersOrderedByName();

        listBoxRiders.Items.Clear();
        if (RiderList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Rider rider in RiderList)
            {
                String text = rider.Riderid + ": " + rider.FamilyName + ", " + rider.GivenName;
                ListItem listItem = new ListItem(text, "" + rider.Riderid);
                listBoxRiders.Items.Add(listItem);
            }
        }
    }

    private void createClubList()
    {
        ClubDAO clubDAO = new ClubDAO();
        List<Club> ClubList = clubDAO.GetAllClubsOrderedByName();

        ddlClubName.Items.Clear();
        if (ClubList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Club club in ClubList)
            {
                String text = club.ClubName;

                ListItem listItem = new ListItem(text, "" + club.Clubid);
                ddlClubName.Items.Add(listItem);
            }
        }
    }


    protected void btAdd_Click(object sender, EventArgs e)
    {
        Rider rider = screenToModel();
        int insertOk = riderDAO.InsertRider(rider);

        if (insertOk == 0) // Insert succeeded
        {
            createRiderList();
            listBoxRiders.SelectedValue = rider.Riderid.ToString();
            viewStateDetailsDisplayed();
            showNoMessage();
        }
        else if (insertOk == 1)
        {
            showErrorMessage("Rider id " + rider.Riderid +
              " is already in use. No record inserted into the database.");
            tbRiderId.Focus();
        }
        else
        {
            showErrorMessage("No record inserted into the database. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        int riderId = Convert.ToInt32(listBoxRiders.SelectedValue);
        int deleteOk = riderDAO.DeleteRider(riderId);

        if (deleteOk == 0) // Delete succeeded
        {
            createRiderList();
            viewStateNew();
            showNoMessage();
        }
        else if (deleteOk == 1)
        {
            showErrorMessage("No record deleted. " +
              "Please delete the Rider's employees first.");
        }
        else
        {
            showErrorMessage("No record deleted. " +
             "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void btNew_Click(object sender, EventArgs e)
    {
        viewStateNew();
        tbRiderId.Text = riderDAO.GetNextAvailableId().ToString();      // fill in the next available clubId value in the riderId textbox
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        Rider rider = screenToModel();
        int updateOk = riderDAO.UpdateRider(rider);

        if (updateOk == 0) // Update succeeded
        {
            String selectedValue = listBoxRiders.SelectedValue;

            createRiderList();
            listBoxRiders.SelectedValue = selectedValue;
            showNoMessage();
        }
        else
        {
            showErrorMessage("No record updated. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void listBoxRiders_SelectedIndexChanged(object sender, EventArgs e)
    {
        int riderId = Convert.ToInt32(listBoxRiders.SelectedValue);
        Rider rider = riderDAO.GetRiderByRiderId(riderId);

        if (rider != null)
        {
            resetForm();
            modelToScreen(rider);
            viewStateDetailsDisplayed();
            showNoMessage();
        }
    }


    private void modelToScreen(Rider rider)
    {
        tbRiderId.Text = "" + rider.Riderid;
        tbSurname.Text = rider.FamilyName;
        tbGivenName.Text = rider.GivenName;
        if (rider.Gender == "F")
            RadioButtonFemale.Checked = true;
        else
            RadioButtonMale.Checked = true;
        tbPhone.Text = rider.Phone;
        tbEmail.Text = rider.Email;
        ddlClubName.SelectedValue = rider.Clubid.ToString();
        tbUsername.Text = rider.Username;
        if (rider.Role == "user")
            RadioButtonUser.Checked = true;
        else
            RadioButtonAdmin.Checked = true;
    }

    private void resetForm()
    {
        tbRiderId.Text = "";
        tbSurname.Text = "";
        tbGivenName.Text = "";
        tbPhone.Text = "";
        tbEmail.Text = "";
        ddlClubName.SelectedIndex = 0;
        tbUsername.Text = "";
        tbPassword.Text = "";
        tbRePassword.Text = "";
        RadioButtonAdmin.Checked = RadioButtonUser.Checked = false;
        RadioButtonFemale.Checked = RadioButtonMale.Checked = false;
    }

    private Rider screenToModel()
    {
        Rider rider = new Rider();

        rider.Riderid = Convert.ToInt32(tbRiderId.Text.Trim());
        rider.FamilyName = tbSurname.Text.Trim();
        rider.GivenName = tbGivenName.Text.Trim();
        if (RadioButtonFemale.Checked)
            rider.Gender = "F";
        else
            rider.Gender = "M";


        rider.Phone = tbPhone.Text.Trim();
        rider.Email = tbEmail.Text.Trim(); 
        rider.Clubid = Convert.ToInt32(ddlClubName.SelectedValue.Trim());
        rider.Username = tbUsername.Text.Trim();
        rider.Password = tbPassword.Text.Trim();
        if (RadioButtonUser.Checked)
            rider.Role = "user";
        else
            rider.Role = "administrator";
        return rider;
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
        tbRiderId.Enabled = false;

        btAdd.Enabled = false;
        btDelete.Enabled = true;
        btNew.Enabled = true;
        btUpdate.Enabled = true;
    }

    private void viewStateNew()
    {
        tbRiderId.Enabled = true;
        tbRiderId.Focus();

        btAdd.Enabled = true;
        btDelete.Enabled = false;
        btNew.Enabled = true;
        btUpdate.Enabled = false;

        resetForm();
        listBoxRiders.SelectedIndex = -1;
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