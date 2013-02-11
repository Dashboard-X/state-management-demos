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

public partial class ViewState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Response.Cookies["nameWithNPCookies"].Value = "This is A Non Persistance Cookie";

            HttpCookie aCookieValNonPer = new HttpCookie("NonPersistance");
            aCookieValNonPer.Value = "This is A Non Persistance Cookie";
            Response.Cookies.Add(aCookieValNonPer);
            
            
            
        

            //Response.Cookies["nameWithPCookies"].Value = "This is A Persistance Cookie";
            //Response.Cookies["nameWithPCookies"].Expires = DateTime.Now.AddSeconds(10);



            HttpCookie aCookieValPer = new HttpCookie("Persistance");
            aCookieValPer.Value = "This is A Persistance Cookie";
            //Response.Cookies["nameWithPCookies"].Expires = DateTime.Now.AddSeconds(5);
            aCookieValPer.Expires = DateTime.Now.AddSeconds(10);
            Response.Cookies.Add(aCookieValPer);

           
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label2.Text = ""; Label1.Text = "";
        //if (Request.Cookies["nameWithNPCookies"] != null)
        //    Label2.Text = Server.HtmlEncode(Request.Cookies["nameWithNPCookies"].Value);
        //if (Request.Cookies["nameWithPCookies"] != null)
        //    Label1.Text = Server.HtmlEncode(Request.Cookies["nameWithPCookies"].Value);
        //if (Request.Cookies["NonPersistance"] != null)
        //{
        //    //HttpCookie aCookie = Request.Cookies["NonPersistance"];
        //    //Label1.Text = Server.HtmlEncode(aCookie.Value);
        //    Label2.Text = Request.Cookies["NonPersistance"].Value;
        //}


        if (Request.Cookies["NonPersistance"] != null)
        {
            Label2.Text = Request.Cookies["NonPersistance"].Value;
        }


        if (Request.Cookies["Persistance"] != null)
        {

            Label1.Text=Request.Cookies["Persistance"].Value;
        }

        //if (Request.Cookies["aCookieValNonPer"]!= null)
        //    Label2.Text = Request.Cookies.Get(aCookieValNonPer).ToString();
        //    //Label2.Text = Server.HtmlEncode(Request.Cookies["aCookieValNonPer"].Value);
        //if (Request.Cookies["aCookieValPer"] != null)
        //    Label2.Text = Request.Cookies.Get(aCookieValPer).ToString();
        //    //Label1.Text = Server.HtmlEncode(Request.Cookies["aCookieValPer"].Value);


      }
}

