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

    protected void Page_init(object sender, EventArgs e)
    {
        
        clsHttpModule.objArrayList.Add("Page:Init");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        clsHttpModule.objArrayList.Add("Page:Load");
    }
    public override void Validate() 
    {
        clsHttpModule.objArrayList.Add("Page:Validate");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        clsHttpModule.objArrayList.Add("Page:Event");
    }
    protected override void Render(HtmlTextWriter output) 
    {
        clsHttpModule.objArrayList.Add("Page:Render");
        base.Render(output);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        clsHttpModule.objArrayList.Add("Page:UnLoad");
    }
}
