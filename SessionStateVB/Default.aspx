<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Session State Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Write Session State Values</h1>
        <p></p>
        First Name: <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
        <br />
        Last Name: <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
        <p></p>
        <asp:Button ID="Submit" runat="server" Text="Submit" PostBackUrl="~/Default.aspx" />
        <p></p>
        <div id="ReadBtn" runat="server" visible="false">
            <asp:Button ID="SavedSessionState" runat="server" 
                Text="Read session state saved values" onclick="ReadSessionStateValues" />
        </div>
    </div>
    </form>
</body>
</html>
