<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CookiesDemo.aspx.cs" Inherits="ViewState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Click Me" />
            <br />
            <br />
            Persistance Cookie :
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            Non Persistance Cookie :
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
