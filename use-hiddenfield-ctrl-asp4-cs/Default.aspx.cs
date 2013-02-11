using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    // Power. Stability. Flexibility. Web hosting from http://www.ServerIntellect.com
    // For more ASP.NET Tutorials visit http://www.AspNetTutorials.com
    protected void Page_Load(object sender, EventArgs e)
    {
        //set our label text to the value of our hiddenfield
        Label1.Text = HiddenField1.Value;
    }
}