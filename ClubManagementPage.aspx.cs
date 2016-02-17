/* **************************************************************************
 * ClubManagementPage.cs  Original version: Kari Silpiö 18.3.2014 v1.0
 *                             Modified by: Alexandru Oat 14.12.2014 v1.0 
 * -------------------------------------------------------------------------
 *  Application: DWA Model Case
 *  Class:       Code-behind class for ClubManagementPage.aspx
 * -------------------------------------------------------------------------
 * NOTE: This file can be included in your solution.
 *   If you modify this file, write your name & date after "Modified by:"
 *   DO NOT REMOVE THIS COMMENT.
 ************************************************************************** */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// ClubManagementPage - Code behind part for the ASPX Page 
/// <remarks>Kari Silpiö 2014 
///          Modified by: Alexandru Oat 2014</remarks>
/// </summary>
public partial class ClubManagementPage : System.Web.UI.Page
{
    private ClubDAO clubDAO = new ClubDAO();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        checkLogin(true); // true = login is required for accessing this page

        if (this.IsPostBack == false)
        {
            viewStateNew();
            createClubList(); // Populate Club List for the first time
        }

        addButtonScripts();
    }
    
    private void addButtonScripts()
    {
        btDelete.Attributes.Add("onclick",
          "return confirm('Are you sure you want to delete the data?');");
    }

    private void createClubList()
    {
        List<Club> ClubList = clubDAO.GetAllClubsOrderedByName();

        listBoxClubs.Items.Clear();
        if (ClubList == null)
        {
            showErrorMessage("DATABASE TEMPORARILY OUT OF USE (see Database.log)");
        }
        else
        {
            foreach (Club club in ClubList)
            {
                String text = club.ClubName + ", " + club.City;
                ListItem listItem = new ListItem(text, "" + club.Clubid);
                listBoxClubs.Items.Add(listItem);
            }
        }
    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        Club Club = screenToModel();
        int insertOk = clubDAO.InsertClub(Club);

        if (insertOk == 0) // Insert succeeded
        {
            createClubList();
            listBoxClubs.SelectedValue = Club.Clubid.ToString();
            viewStateDetailsDisplayed();
            showNoMessage();
        }
        else if (insertOk == 1)
        {
            showErrorMessage("Club id " + Club.Clubid +
              " is already in use. No record inserted into the database.");
            tbClubid.Focus();
        }
        else
        {
            showErrorMessage("No record inserted into the database. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void btDelete_Click(object sender, EventArgs e)
    {
        int ClubId = Convert.ToInt32(listBoxClubs.SelectedValue);
        int deleteOk = clubDAO.DeleteClub(ClubId);

        if (deleteOk == 0) // Delete succeeded
        {
            createClubList();
            viewStateNew();
            showNoMessage();
        }
        else if (deleteOk == 1)
        {
            showErrorMessage("No record deleted. " +
              "Please delete the Club's riders first.");
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
        tbClubid.Text = clubDAO.GetNextAvailableId().ToString();    // fill in the next available clubId value
    }

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        Club Club = screenToModel();
        int updateOk = clubDAO.UpdateClub(Club);

        if (updateOk == 0) // Update succeeded
        {
            String selectedValue = listBoxClubs.SelectedValue;
            createClubList();
            listBoxClubs.SelectedValue = selectedValue;
            showNoMessage();
        }
        else
        {
            showErrorMessage("No record updated. " +
              "THE DATABASE IS TEMPORARILY OUT OF USE.");
        }
    }

    protected void listBoxClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ClubId = Convert.ToInt32(listBoxClubs.SelectedValue);
        Club Club = clubDAO.GetClubByClubId(ClubId);

        if (Club != null)
        {
            modelToScreen(Club);
            viewStateDetailsDisplayed();
            showNoMessage();
        }
    }

    private void modelToScreen(Club Club)
    {
        tbClubid.Text = "" + Club.Clubid;
        tbName.Text = Club.ClubName;
        tbCity.Text = Club.City;
        tbEmail.Text = Club.Email;
    }

    private void resetForm()
    {
        tbClubid.Text = "";
        tbName.Text = "";
        tbCity.Text = "";
        tbEmail.Text = "";
    }

    private Club screenToModel()
    {
        Club Club = new Club();

        Club.Clubid = Convert.ToInt32(tbClubid.Text.Trim());
        Club.ClubName = tbName.Text.Trim();
        Club.City = tbCity.Text.Trim();
        Club.Email = tbEmail.Text.Trim();
        return Club;
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
        tbClubid.Enabled = false;

        btAdd.Enabled = false;
        btDelete.Enabled = true;
        btNew.Enabled = true;
        btUpdate.Enabled = true;
    }

    private void viewStateNew()
    {
        tbClubid.Enabled = true;
        tbName.Focus();

        btAdd.Enabled = true;
        btDelete.Enabled = false;
        btNew.Enabled = true;
        btUpdate.Enabled = false;

        resetForm();

        listBoxClubs.SelectedIndex = -1;
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
// End
