using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DisplaySessionState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Display Session values
        NewFirstNameTextBox.Text = Session["FirstName"].ToString();
        NewLastNameTextBox.Text = Session["LastName"].ToString();

        // Display Session Details
        CookieMode.Text = Session.CookieMode.ToString();
        Count.Text = Session.Count.ToString();
        IsCookieless.Text = Session.IsCookieless.ToString();
        IsNewSession.Text = Session.IsNewSession.ToString();
        IsReadOnly.Text = Session.IsReadOnly.ToString();
        isSynchronized.Text = Session.IsSynchronized.ToString();
        Keys.Text = Session.Keys[0].ToString();
        LCID.Text = Session.LCID.ToString();
        Mode.Text = Session.Mode.ToString();
        Timeout.Text = Session.Timeout.ToString();
    }
}