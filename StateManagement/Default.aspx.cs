using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(IsPostBack == true)
        {
            //Store in Viewstate --------------------------------------------------------------
            if (ViewState["number"] != null) //Lets retrieve, increase and store again
            {
                ViewState["number"] = Convert.ToInt32(ViewState["number"]) + 1;
            }
            else //First postback, lets store the info
            {
                ViewState["number"] = 1;
            }

            Label1.Text = ViewState["number"].ToString();

            //Store in Hidden Field -----------------------------------------------------------
            int newVal = Convert.ToInt32(HiddenField1.Value) + 1;
            HiddenField1.Value = newVal.ToString();
            Label2.Text = HiddenField1.Value;


            //Store in Cookies --------------------------------------------------------------
            int postbacks = 0;
            if (Request.Cookies["number"] != null) //Lets retrieve, increase and store again
            {
                postbacks = Convert.ToInt32(Request.Cookies["number"].Value) + 1;
            }
            else //First postback, lets store the info
            {
                postbacks = 1;
            }
            Response.Cookies["number"].Value = postbacks.ToString();

            Label3.Text = Response.Cookies["number"].Value;


            //Store in Application State --------------------------------------------------------------
            Application.Lock();
            Application["number"] = Convert.ToInt32(Application["number"]) + 1;
            Application.UnLock();

            Label5.Text = Application["number"].ToString();

            //Store in Session State --------------------------------------------------------------           
            Session["number"] = Convert.ToInt32(Session["number"]) + 1;

            Label6.Text = Session["number"].ToString();
        }


        //GetDataItem from querystring         
        if (Request.QueryString["number"] != null) //Lets retrieve, increase and store again
        {
            Label4.Text = Request.QueryString["number"];
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //set in query string
        int postbacks = 0;

        if (Request.QueryString["number"] != null) //Lets retrieve, increase and store again
        {
            postbacks = Convert.ToInt32(Request.QueryString["number"]) + 1;
        }
        else //First postback, lets store the info
        {
            postbacks = 1;
        }

        Response.Redirect("default.aspx?number=" + postbacks);
    }
}
