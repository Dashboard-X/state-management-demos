using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {      
            if (ViewState["Name"] != null)
            {
                ViewState["Name"] = TextBox1.Text;
                Label1.Text = ViewState["Name"].ToString();
            }
            else
            {
                ViewState["Name"] = "No Value in View State";
                Label1.Text = ViewState["Name"].ToString();
            } 
       

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      

    }
}
