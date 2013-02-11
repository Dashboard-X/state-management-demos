<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%-- This tutorial is sponsored by http://www.ServerIntellect.com web hosting. 
	 Check out http://www.AspNetTutorials.com for more great tutorials! --%>

    <script type="text/javascript">
        function displayHiddenValue()
        {
            alert(form1.HiddenField1.value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:HiddenField ID="HiddenField1" runat="server" Value="Hello Hidden Field!" />
        <input type="submit" name="btnDisplay" value="Click Me!" onclick="displayHiddenValue()" />
    </div>
    </form>
</body>
</html>
