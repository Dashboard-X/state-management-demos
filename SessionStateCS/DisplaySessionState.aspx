<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplaySessionState.aspx.cs" Inherits="DisplaySessionState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session State Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Read Session State Values</h1>
        Session FirstName: <asp:TextBox ID="NewFirstNameTextBox" runat="server"></asp:TextBox>
        <br />
        Session LastName: <asp:TextBox ID="NewLastNameTextBox" runat="server"></asp:TextBox>
        <p></p><p></p>
        <h3>Session Information</h3>
        CookieMode: <asp:TextBox ID="CookieMode" runat="server"></asp:TextBox>
        <br />
        Count: <asp:TextBox ID="Count" runat="server"></asp:TextBox>
        <br />
        IsCookieless: <asp:TextBox ID="IsCookieless" runat="server"></asp:TextBox>
        <br />
        IsNewSession: <asp:TextBox ID="IsNewSession" runat="server"></asp:TextBox>
        <br />
        IsReadOnly: <asp:TextBox ID="IsReadOnly" runat="server"></asp:TextBox>
        <br />
        isSynchronized: <asp:TextBox ID="isSynchronized" runat="server"></asp:TextBox>
        <br />
        Keys[0]: <asp:TextBox ID="Keys" runat="server"></asp:TextBox>
        <br />
        LCID: <asp:TextBox ID="LCID" runat="server"></asp:TextBox>
        <br />
        Mode: <asp:TextBox ID="Mode" runat="server"></asp:TextBox>
        <br />
        Timeout: <asp:TextBox ID="Timeout" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
