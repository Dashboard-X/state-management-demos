<%@ Page Language="C#"  ValidateRequest="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var personList = new List<Person>();
                     
            personList.Add(new Person("Raed", 29));
            personList.Add(new Person("Mohammed", 31));
            personList.Add(new Person("Issa", 17));
            personList.Add(new Person("Momani", 18));
             
            HttpCookie cokiName = new HttpCookie("Name", "Raed");
            cokiName.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cokiName);

            ViewState["Name"] = "Raed";

            Application["Name"] = "Raed";

            Session["Name"] = "Raed";

            Cache["Name"] = "Raed";

            hidnName.Value = "Raed";

            /////////////////
            var serializedObject = SerializeAnObject(personList);


            HttpCookie cokiObjPerson = new HttpCookie("ObjPerson", serializedObject); // string
           cokiName.Expires = DateTime.Now.AddDays(1);
           Response.Cookies.Add(cokiObjPerson);

            
            
            ViewState["ObjPerson"] = personList; //Serializable only

            Application["ObjPerson"] = personList;// without serialzation

            Session["ObjPerson"] = personList;// without serialzation

            Cache["ObjPerson"] = personList; // without serialzation

            hidnObjectPerson.Value = serializedObject; // string
            
            
        
        }
    }
    
   

    protected void btnGetValue_Click(object sender, EventArgs e)
    {


        if (rdionList.SelectedValue == "Cookie")
        {
            lblValue.Text = Request.Cookies["Name"].Value.ToString();
        }
        if (rdionList.SelectedValue == "View State")
        {
            lblValue.Text = ViewState["Name"].ToString();
        }
        if (rdionList.SelectedValue == "Application")
        {
            lblValue.Text = Application["Name"].ToString();
        }
        if (rdionList.SelectedValue == "Session")
        {
            lblValue.Text = Session["Name"].ToString();
        }
        if (rdionList.SelectedValue == "Cache")
        {

            lblValue.Text = Cache["Name"].ToString();
        }

        if (rdionList.SelectedValue == "Hidden Field")
        {

            lblValue.Text = hidnName.Value;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (rdionList.SelectedValue == "Cookie")
        {
            Response.Cookies.Clear();
        }
        if (rdionList.SelectedValue == "View State")
        {
            ViewState.Clear();
        }
        if (rdionList.SelectedValue == "Application")
        {
             Application.Clear();
        }
        if (rdionList.SelectedValue == "Session")
        {
          Session.Clear();
        }
        if (rdionList.SelectedValue == "Cache")
        {

            Cache.Remove("Name");
        }

         if (rdionList.SelectedValue == "Hidden Field")
        {

            hidnName.Value = null ;
        }
    }

    protected void btnGetObjectValue_Click(object sender, EventArgs e)
    {
        var personList = new List<Person>();

     //   Response.Write(Request.Cookies["ObjPerson"].Value);
      //  Response.End();

        if (rdionList.SelectedValue == "Cookie")
        {
            personList = (List<Person>)DeSerializeAnObject(Request.Cookies["ObjPerson"].Value);

        }
        if (rdionList.SelectedValue == "View State")
        {
            personList = (List<Person>)ViewState["ObjPerson"];
        }
        if (rdionList.SelectedValue == "Application")
        {
            personList = (List<Person>)Application["ObjPerson"];
        }
        if (rdionList.SelectedValue == "Session")
        {
            personList = (List<Person>)Session["ObjPerson"];
        }
        if (rdionList.SelectedValue == "Cache")
        {

            personList = (List<Person>)Cache["ObjPerson"];
        }

        if (rdionList.SelectedValue == "Hidden Field")
        {
            personList = (List<Person>)DeSerializeAnObject(hidnObjectPerson.Value);

        }

        gridObject.DataSource = personList;
        gridObject.DataBind();
        
    }


    #region
    private string SerializeAnObject(object obj)
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        try
        {
            serializer.Serialize(stream, obj);
            stream.Position = 0;
            doc.Load(stream);
            return doc.InnerXml;
        }
        catch
        {
           throw;
       
        }
        finally
        {
            stream.Close();
            stream.Dispose();
        }
      
    }
    private object DeSerializeAnObject(string xmlOfAnObject)
    {
        var myObject = new List<Person>();
        System.IO.StringReader read = new System.IO.StringReader(xmlOfAnObject);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(myObject.GetType());
        System.Xml.XmlReader reader = new System.Xml.XmlTextReader(read);
        try
        {
            myObject = (List<Person>)serializer.Deserialize(reader);
            return myObject;
        }

        catch 
        {
             throw;
       
        }

        finally
        {

            reader.Close();
            read.Close();
           read.Dispose();

        }
 
    }

    #endregion


    [Serializable]
    public class Person
    {
        public Person()
        { }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;

        }


        public string Name { set; get; }
        public int Age { set; get; }


    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
   <asp:HiddenField ID="hidnName" runat="server"  />
   <asp:HiddenField ID="hidnObjectPerson" runat="server"  />
    
    <asp:Button ID="btnGetValue" runat="server" onclick="btnGetValue_Click" 
            Text="Get Value Of" />
    &nbsp;
   
    
        <asp:RadioButtonList ID="rdionList" runat="server" RepeatDirection="Horizontal" 
            RepeatLayout="Flow">
            <asp:ListItem Selected="True">Cookie</asp:ListItem>
            <asp:ListItem>View State</asp:ListItem>
            <asp:ListItem>Application</asp:ListItem>
            <asp:ListItem>Session</asp:ListItem>
            <asp:ListItem>Cache</asp:ListItem>
            <asp:ListItem>Hidden Field</asp:ListItem>
        </asp:RadioButtonList>
    
   
    &nbsp; :
    <asp:Label ID="lblValue" runat="server" Text="" style="font-weight: 700"></asp:Label>
        &nbsp;|
    <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
        Text="Clear" />
        <br />
    
    <asp:Button ID="btnGetObjectValue" runat="server" onclick="btnGetObjectValue_Click" 
            Text="Get Object Of" />
    <asp:GridView ID="gridObject" runat="server">
    </asp:GridView>
    
    </form>
   
           
</body>
</html>
