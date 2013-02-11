using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            // Set Session State values
            Session["FirstName"] = FirstNameTextBox.Text;
            Session["LastName"] = LastNameTextBox.Text;

            // Display Button
            ReadBtn.Visible = true;
        }
    }
    protected void ReadSessionStateValues(object sender, EventArgs e)
    {
        Response.Redirect("DisplaySessionState.aspx");
    }
}