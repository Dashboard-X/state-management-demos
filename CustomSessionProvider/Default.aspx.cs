using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["user_id"] = "This is a string value test in custom sessions";

        ArrayList tmpArr = new ArrayList();
        tmpArr.Add("This is a string kept in an object in custom sessions");
        tmpArr.Add(20);

        Session["user_settings"] = tmpArr;

        Response.Write(Session["user_id"]);

        ArrayList tmpArr2 = (ArrayList)Session["user_settings"];
    }
}
